using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.AgentOrigins;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace SandBox.CampaignBehaviors
{
	// Token: 0x020000A1 RID: 161
	public class BarberCampaignBehavior : CampaignBehaviorBase, IFacegenCampaignBehavior, ICampaignBehavior
	{
		// Token: 0x06000686 RID: 1670 RVA: 0x0003056A File Offset: 0x0002E76A
		public override void RegisterEvents()
		{
			CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnSessionLaunched));
			CampaignEvents.LocationCharactersAreReadyToSpawnEvent.AddNonSerializedListener(this, new Action<Dictionary<string, int>>(this.LocationCharactersAreReadyToSpawn));
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0003059A File Offset: 0x0002E79A
		public override void SyncData(IDataStore store)
		{
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0003059C File Offset: 0x0002E79C
		private void OnSessionLaunched(CampaignGameStarter campaignGameStarter)
		{
			this.AddDialogs(campaignGameStarter);
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x000305A8 File Offset: 0x0002E7A8
		private void AddDialogs(CampaignGameStarter campaignGameStarter)
		{
			campaignGameStarter.AddDialogLine("barber_start_talk_beggar", "start", "close_window", "{=pWzdxd7O}May the Heavens bless you, my poor {?PLAYER.GENDER}lady{?}fellow{\\?}, but I can't spare a coin right now.", new ConversationSentence.OnConditionDelegate(this.InDisguiseSpeakingToBarber), new ConversationSentence.OnConsequenceDelegate(this.InitializeBarberConversation), 100, null);
			campaignGameStarter.AddDialogLine("barber_start_talk", "start", "barber_question1", "{=2aXYYNBG}Come to have your hair cut, {?PLAYER.GENDER}my lady{?}my lord{\\?}? A new look for a new day?", new ConversationSentence.OnConditionDelegate(this.IsConversationAgentBarber), new ConversationSentence.OnConsequenceDelegate(this.InitializeBarberConversation), 100, null);
			campaignGameStarter.AddPlayerLine("player_accept_haircut", "barber_question1", "start_cut_token", "{=Q7wBRXtR}Yes, I have. ({GOLD_COST} {GOLD_ICON})", new ConversationSentence.OnConditionDelegate(this.GivePlayerAHaircutCondition), new ConversationSentence.OnConsequenceDelegate(this.GivePlayerAHaircut), 100, new ConversationSentence.OnClickableConditionDelegate(this.DoesPlayerHaveEnoughGold), null);
			campaignGameStarter.AddPlayerLine("player_refuse_haircut", "barber_question1", "no_haircut_conversation_token", "{=xPAAZAaI}My hair is fine as it is, thank you.", null, null, 100, null, null);
			campaignGameStarter.AddDialogLine("barber_ask_if_done", "start_cut_token", "finish_cut_token", "{=M3K8wUOO}So... Does this please you, {?PLAYER.GENDER}my lady{?}my lord{\\?}?", null, null, 100, null);
			campaignGameStarter.AddPlayerLine("player_done_with_haircut", "finish_cut_token", "finish_barber", "{=zTF4bJm0}Yes, it's fine.", null, null, 100, null, null);
			campaignGameStarter.AddPlayerLine("player_not_done_with_haircut", "finish_cut_token", "start_cut_token", "{=BnoSOi3r}Actually...", new ConversationSentence.OnConditionDelegate(this.GivePlayerAHaircutCondition), new ConversationSentence.OnConsequenceDelegate(this.GivePlayerAHaircut), 100, new ConversationSentence.OnClickableConditionDelegate(this.DoesPlayerHaveEnoughGold), null);
			campaignGameStarter.AddDialogLine("barber_no_haircut_talk", "no_haircut_conversation_token", "close_window", "{=BusYGTrN}Excellent! Have a good day, then, {?PLAYER.GENDER}my lady{?}my lord{\\?}.", null, null, 100, null);
			campaignGameStarter.AddDialogLine("barber_haircut_finished", "finish_barber", "player_had_a_haircut_token", "{=akqJbZpH}Marvellous! You cut a splendid appearance, {?PLAYER.GENDER}my lady{?}my lord{\\?}, if you don't mind my saying. Most splendid.", new ConversationSentence.OnConditionDelegate(this.DidPlayerHaveAHaircut), new ConversationSentence.OnConsequenceDelegate(this.ChargeThePlayer), 100, null);
			campaignGameStarter.AddDialogLine("barber_haircut_no_change", "finish_barber", "player_did_not_cut_token", "{=yLIZlaS1}Very well. Do come back when you're ready, {?PLAYER.GENDER}my lady{?}my lord{\\?}.", new ConversationSentence.OnConditionDelegate(this.DidPlayerNotHaveAHaircut), null, 100, null);
			campaignGameStarter.AddPlayerLine("player_no_haircut_finish_talk", "player_did_not_cut_token", "close_window", "{=oPUVNuhN}I'll keep you in mind", null, null, 100, null, null);
			campaignGameStarter.AddPlayerLine("player_haircut_finish_talk", "player_had_a_haircut_token", "close_window", "{=F9Xjbchh}Thank you.", null, null, 100, null, null);
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x000307CA File Offset: 0x0002E9CA
		private bool InDisguiseSpeakingToBarber()
		{
			return this.IsConversationAgentBarber() && Campaign.Current.IsMainHeroDisguised;
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x000307E0 File Offset: 0x0002E9E0
		private bool DoesPlayerHaveEnoughGold(out TextObject explanation)
		{
			if (Hero.MainHero.Gold < 100)
			{
				explanation = new TextObject("{=RYJdU43V}Not Enough Gold", null);
				return false;
			}
			explanation = TextObject.Empty;
			return true;
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x00030807 File Offset: 0x0002EA07
		private void ChargeThePlayer()
		{
			GiveGoldAction.ApplyBetweenCharacters(Hero.MainHero, null, 100, false);
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00030817 File Offset: 0x0002EA17
		private bool DidPlayerNotHaveAHaircut()
		{
			return !this.DidPlayerHaveAHaircut();
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x00030824 File Offset: 0x0002EA24
		private bool DidPlayerHaveAHaircut()
		{
			return Hero.MainHero.BodyProperties.StaticProperties != this._previousBodyProperties;
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0003084E File Offset: 0x0002EA4E
		private bool IsConversationAgentBarber()
		{
			Settlement currentSettlement = Settlement.CurrentSettlement;
			return ((currentSettlement != null) ? currentSettlement.Culture.Barber : null) == CharacterObject.OneToOneConversationCharacter;
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0003086D File Offset: 0x0002EA6D
		private bool GivePlayerAHaircutCondition()
		{
			MBTextManager.SetTextVariable("GOLD_COST", 100);
			return true;
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0003087C File Offset: 0x0002EA7C
		private void GivePlayerAHaircut()
		{
			this._isOpenedFromBarberDialogue = true;
			BarberState gameState = Game.Current.GameStateManager.CreateState<BarberState>(new object[]
			{
				Hero.MainHero.CharacterObject,
				this.GetFaceGenFilter()
			});
			this._isOpenedFromBarberDialogue = false;
			GameStateManager.Current.PushState(gameState, 0);
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x000308D0 File Offset: 0x0002EAD0
		private void InitializeBarberConversation()
		{
			this._previousBodyProperties = Hero.MainHero.BodyProperties.StaticProperties;
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x000308F8 File Offset: 0x0002EAF8
		private LocationCharacter CreateBarber(CultureObject culture, LocationCharacter.CharacterRelations relation)
		{
			CharacterObject barber = culture.Barber;
			int minValue;
			int maxValue;
			Campaign.Current.Models.AgeModel.GetAgeLimitForLocation(barber, out minValue, out maxValue, "Barber");
			return new LocationCharacter(new AgentData(new SimpleAgentOrigin(barber, -1, null, default(UniqueTroopDescriptor))).Monster(FaceGen.GetMonsterWithSuffix(barber.Race, "_settlement_slow")).Age(MBRandom.RandomInt(minValue, maxValue)), new LocationCharacter.AddBehaviorsDelegate(SandBoxManager.Instance.AgentBehaviorManager.AddWandererBehaviors), "sp_barber", true, relation, null, true, false, null, false, false, true);
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0003098C File Offset: 0x0002EB8C
		private void LocationCharactersAreReadyToSpawn(Dictionary<string, int> unusedUsablePointCount)
		{
			Location locationWithId = Settlement.CurrentSettlement.LocationComplex.GetLocationWithId("center");
			int num;
			if (CampaignMission.Current.Location == locationWithId && Campaign.Current.IsDay && unusedUsablePointCount.TryGetValue("sp_merchant_notary", out num))
			{
				locationWithId.AddLocationCharacters(new CreateLocationCharacterDelegate(this.CreateBarber), Settlement.CurrentSettlement.Culture, LocationCharacter.CharacterRelations.Neutral, 1);
			}
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x000309F4 File Offset: 0x0002EBF4
		public IFaceGeneratorCustomFilter GetFaceGenFilter()
		{
			return new BarberCampaignBehavior.BarberFaceGeneratorCustomFilter(!this._isOpenedFromBarberDialogue, this.GetAvailableHaircuts(), this.GetAvailableFacialHairs());
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x00030A10 File Offset: 0x0002EC10
		private int[] GetAvailableFacialHairs()
		{
			List<int> list = new List<int>();
			CultureCode cultureCode = (this._isOpenedFromBarberDialogue && Settlement.CurrentSettlement != null) ? Settlement.CurrentSettlement.Culture.GetCultureCode() : CultureCode.Invalid;
			if (!Hero.MainHero.IsFemale)
			{
				switch (cultureCode)
				{
				case CultureCode.Empire:
					list.AddRange(new int[]
					{
						5,
						6,
						9,
						12,
						23,
						24,
						25,
						26
					});
					break;
				case CultureCode.Sturgia:
					list.AddRange(new int[]
					{
						1,
						2,
						4,
						8,
						9,
						10,
						11,
						12,
						13,
						14,
						15,
						16,
						17,
						18,
						19,
						20,
						21,
						22,
						24,
						25,
						26,
						29,
						32,
						34,
						35
					});
					break;
				case CultureCode.Aserai:
					list.AddRange(new int[]
					{
						36,
						37,
						38,
						39,
						40,
						41
					});
					break;
				case CultureCode.Vlandia:
					list.AddRange(new int[]
					{
						1,
						2,
						3,
						5,
						6,
						7,
						8,
						9,
						10,
						12,
						13,
						14,
						22,
						23,
						24,
						25,
						26,
						32
					});
					break;
				case CultureCode.Khuzait:
					list.AddRange(new int[]
					{
						0,
						28,
						29,
						31,
						32,
						33
					});
					break;
				case CultureCode.Battania:
					list.AddRange(new int[]
					{
						0,
						1,
						2,
						4,
						8,
						10,
						11,
						12,
						13,
						14,
						15,
						16,
						17,
						18,
						19,
						20,
						21,
						22,
						24,
						29,
						31,
						32,
						34,
						35
					});
					break;
				}
				list.AddRange(new int[1]);
			}
			return list.Distinct<int>().ToArray<int>();
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x00030B28 File Offset: 0x0002ED28
		private int[] GetAvailableHaircuts()
		{
			List<int> list = new List<int>();
			CultureCode cultureCode = (this._isOpenedFromBarberDialogue && Settlement.CurrentSettlement != null) ? Settlement.CurrentSettlement.Culture.GetCultureCode() : CultureCode.Invalid;
			if (Hero.MainHero.IsFemale)
			{
				switch (cultureCode)
				{
				case CultureCode.Empire:
					list.AddRange(new int[0]);
					break;
				case CultureCode.Sturgia:
					list.AddRange(new int[]
					{
						8,
						9,
						13,
						15
					});
					break;
				case CultureCode.Aserai:
					list.AddRange(new int[]
					{
						13,
						17,
						18,
						19,
						20
					});
					break;
				case CultureCode.Vlandia:
					list.AddRange(new int[0]);
					break;
				case CultureCode.Khuzait:
					list.AddRange(new int[]
					{
						7,
						12,
						13
					});
					break;
				case CultureCode.Battania:
					list.AddRange(new int[]
					{
						8,
						9,
						15
					});
					break;
				}
				list.AddRange(new int[]
				{
					0,
					1,
					2,
					3,
					4,
					5,
					6,
					10,
					11,
					14,
					16,
					21
				});
			}
			else
			{
				switch (cultureCode)
				{
				case CultureCode.Empire:
					list.AddRange(new int[]
					{
						1,
						4,
						5,
						8,
						14,
						15
					});
					break;
				case CultureCode.Sturgia:
					list.AddRange(new int[]
					{
						1,
						2,
						3,
						4,
						5,
						8,
						10,
						16,
						18,
						20,
						27
					});
					break;
				case CultureCode.Aserai:
					list.AddRange(new int[]
					{
						1,
						2,
						3,
						4,
						5,
						21,
						22,
						23,
						24,
						25,
						26
					});
					break;
				case CultureCode.Vlandia:
					list.AddRange(new int[]
					{
						1,
						4,
						5,
						8,
						11,
						14,
						15,
						28
					});
					break;
				case CultureCode.Khuzait:
					list.AddRange(new int[]
					{
						12,
						17,
						28
					});
					break;
				case CultureCode.Battania:
					list.AddRange(new int[]
					{
						1,
						2,
						3,
						4,
						5,
						7,
						8,
						10,
						16,
						17,
						18,
						19,
						20
					});
					break;
				}
				list.AddRange(new int[]
				{
					0,
					6,
					9,
					13
				});
			}
			return list.Distinct<int>().ToArray<int>();
		}

		// Token: 0x040002D4 RID: 724
		private const int BarberCost = 100;

		// Token: 0x040002D5 RID: 725
		private bool _isOpenedFromBarberDialogue;

		// Token: 0x040002D6 RID: 726
		private StaticBodyProperties _previousBodyProperties;

		// Token: 0x0200018A RID: 394
		private class BarberFaceGeneratorCustomFilter : IFaceGeneratorCustomFilter
		{
			// Token: 0x0600107F RID: 4223 RVA: 0x00063D02 File Offset: 0x00061F02
			public BarberFaceGeneratorCustomFilter(bool useDefaultStages, int[] haircutIndices, int[] faircutIndices)
			{
				this._haircutIndices = haircutIndices;
				this._facialHairIndices = faircutIndices;
				this._defaultStages = useDefaultStages;
			}

			// Token: 0x06001080 RID: 4224 RVA: 0x00063D1F File Offset: 0x00061F1F
			public int[] GetHaircutIndices(BasicCharacterObject character)
			{
				return this._haircutIndices;
			}

			// Token: 0x06001081 RID: 4225 RVA: 0x00063D27 File Offset: 0x00061F27
			public int[] GetFacialHairIndices(BasicCharacterObject character)
			{
				return this._facialHairIndices;
			}

			// Token: 0x06001082 RID: 4226 RVA: 0x00063D2F File Offset: 0x00061F2F
			public FaceGeneratorStage[] GetAvailableStages()
			{
				if (this._defaultStages)
				{
					FaceGeneratorStage[] array = new FaceGeneratorStage[7];
					RuntimeHelpers.InitializeArray(array, fieldof(<PrivateImplementationDetails>.50567A6578C37E24118E2B7EE8F5C7930666F62F).FieldHandle);
					return array;
				}
				return new FaceGeneratorStage[]
				{
					FaceGeneratorStage.Hair
				};
			}

			// Token: 0x040006DE RID: 1758
			private readonly int[] _haircutIndices;

			// Token: 0x040006DF RID: 1759
			private readonly int[] _facialHairIndices;

			// Token: 0x040006E0 RID: 1760
			private readonly bool _defaultStages;
		}
	}
}
