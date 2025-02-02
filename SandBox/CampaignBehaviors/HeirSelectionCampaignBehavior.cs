using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.Extensions;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace SandBox.CampaignBehaviors
{
	// Token: 0x020000AC RID: 172
	public class HeirSelectionCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x06000818 RID: 2072 RVA: 0x0003E60C File Offset: 0x0003C80C
		public override void RegisterEvents()
		{
			CampaignEvents.OnBeforeMainCharacterDiedEvent.AddNonSerializedListener(this, new Action<Hero, Hero, KillCharacterAction.KillCharacterActionDetail, bool>(this.OnBeforeMainCharacterDied));
			CampaignEvents.OnBeforePlayerCharacterChangedEvent.AddNonSerializedListener(this, new Action<Hero, Hero>(this.OnBeforePlayerCharacterChanged));
			CampaignEvents.OnPlayerCharacterChangedEvent.AddNonSerializedListener(this, new Action<Hero, Hero, MobileParty, bool>(this.OnPlayerCharacterChanged));
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0003E65E File Offset: 0x0003C85E
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0003E660 File Offset: 0x0003C860
		private void OnBeforePlayerCharacterChanged(Hero oldPlayer, Hero newPlayer)
		{
			foreach (ItemRosterElement itemRosterElement in MobileParty.MainParty.ItemRoster)
			{
				this._itemsThatWillBeInherited.Add(itemRosterElement);
			}
			for (int i = 0; i < 12; i++)
			{
				if (!oldPlayer.BattleEquipment[i].IsEmpty)
				{
					this._equipmentsThatWillBeInherited.AddToCounts(oldPlayer.BattleEquipment[i], 1);
				}
				if (!oldPlayer.CivilianEquipment[i].IsEmpty)
				{
					this._equipmentsThatWillBeInherited.AddToCounts(oldPlayer.CivilianEquipment[i], 1);
				}
			}
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x0003E724 File Offset: 0x0003C924
		private void OnPlayerCharacterChanged(Hero oldPlayer, Hero newPlayer, MobileParty newMainParty, bool isMainPartyChanged)
		{
			if (isMainPartyChanged)
			{
				newMainParty.ItemRoster.Add(this._itemsThatWillBeInherited);
			}
			newMainParty.ItemRoster.Add(this._equipmentsThatWillBeInherited);
			this._itemsThatWillBeInherited.Clear();
			this._equipmentsThatWillBeInherited.Clear();
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x0003E764 File Offset: 0x0003C964
		private void OnBeforeMainCharacterDied(Hero victim, Hero killer, KillCharacterAction.KillCharacterActionDetail detail, bool showNotification = true)
		{
			Dictionary<Hero, int> heirApparents = Hero.MainHero.Clan.GetHeirApparents();
			if (heirApparents.Count == 0)
			{
				if (PlayerEncounter.Current != null && (PlayerEncounter.Battle == null || !PlayerEncounter.Battle.IsFinalized))
				{
					PlayerEncounter.Finish(true);
				}
				Dictionary<TroopRosterElement, int> dictionary = new Dictionary<TroopRosterElement, int>();
				foreach (TroopRosterElement troopRosterElement in MobileParty.MainParty.Party.MemberRoster.GetTroopRoster())
				{
					if (troopRosterElement.Character != CharacterObject.PlayerCharacter)
					{
						dictionary.Add(troopRosterElement, troopRosterElement.Number);
					}
				}
				foreach (KeyValuePair<TroopRosterElement, int> keyValuePair in dictionary)
				{
					MobileParty.MainParty.Party.MemberRoster.RemoveTroop(keyValuePair.Key.Character, keyValuePair.Value, default(UniqueTroopDescriptor), 0);
				}
				Hero.MainHero.AddDeathMark(null, detail);
				CampaignEventDispatcher.Instance.OnGameOver();
				this.GameOverCleanup();
				this.ShowGameStatistics();
				Campaign.Current.OnGameOver();
				return;
			}
			if (Hero.MainHero.IsPrisoner)
			{
				EndCaptivityAction.ApplyByDeath(Hero.MainHero);
			}
			if (PlayerEncounter.Current != null && (PlayerEncounter.Battle == null || !PlayerEncounter.Battle.IsFinalized))
			{
				PlayerEncounter.Finish(true);
			}
			List<InquiryElement> list = new List<InquiryElement>();
			foreach (KeyValuePair<Hero, int> keyValuePair2 in from x in heirApparents
			orderby x.Value
			select x)
			{
				TextObject textObject = new TextObject("{=!}{HERO.NAME}", null);
				StringHelpers.SetCharacterProperties("HERO", keyValuePair2.Key.CharacterObject, textObject, false);
				textObject.SetTextVariable("POINT", keyValuePair2.Value);
				string heroPropertiesHint = HeirSelectionCampaignBehavior.GetHeroPropertiesHint(keyValuePair2.Key);
				list.Add(new InquiryElement(keyValuePair2.Key, textObject.ToString(), new ImageIdentifier(CharacterCode.CreateFrom(keyValuePair2.Key.CharacterObject)), true, heroPropertiesHint));
			}
			MBInformationManager.ShowMultiSelectionInquiry(new MultiSelectionInquiryData(new TextObject("{=iHYAEEfv}SELECT AN HEIR", null).ToString(), string.Empty, list, false, 1, 1, GameTexts.FindText("str_done", null).ToString(), string.Empty, new Action<List<InquiryElement>>(HeirSelectionCampaignBehavior.OnHeirSelectionOver), null, "", false), false, false);
			Campaign.Current.TimeControlMode = CampaignTimeControlMode.Stop;
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x0003EA24 File Offset: 0x0003CC24
		private static string GetHeroPropertiesHint(Hero hero)
		{
			GameTexts.SetVariable("newline", "\n");
			string content = hero.Name.ToString();
			TextObject textObject = GameTexts.FindText("str_STR1_space_STR2", null);
			textObject.SetTextVariable("STR1", GameTexts.FindText("str_enc_sf_age", null).ToString());
			textObject.SetTextVariable("STR2", ((int)hero.Age).ToString());
			string content2 = GameTexts.FindText("str_attributes", null).ToString();
			foreach (CharacterAttribute characterAttribute in Attributes.All)
			{
				GameTexts.SetVariable("LEFT", characterAttribute.Name.ToString());
				GameTexts.SetVariable("RIGHT", hero.GetAttributeValue(characterAttribute));
				string content3 = GameTexts.FindText("str_LEFT_colon_RIGHT_wSpaceAfterColon", null).ToString();
				GameTexts.SetVariable("STR1", content2);
				GameTexts.SetVariable("STR2", content3);
				content2 = GameTexts.FindText("str_string_newline_string", null).ToString();
			}
			int num = 0;
			string content4 = GameTexts.FindText("str_skills", null).ToString();
			foreach (SkillObject skillObject in Skills.All)
			{
				int skillValue = hero.GetSkillValue(skillObject);
				if (skillValue > 50)
				{
					GameTexts.SetVariable("LEFT", skillObject.Name.ToString());
					GameTexts.SetVariable("RIGHT", skillValue);
					string content5 = GameTexts.FindText("str_LEFT_colon_RIGHT_wSpaceAfterColon", null).ToString();
					GameTexts.SetVariable("STR1", content4);
					GameTexts.SetVariable("STR2", content5);
					content4 = GameTexts.FindText("str_string_newline_string", null).ToString();
					num++;
				}
			}
			GameTexts.SetVariable("STR1", content);
			GameTexts.SetVariable("STR2", textObject.ToString());
			string text = GameTexts.FindText("str_string_newline_string", null).ToString();
			GameTexts.SetVariable("newline", "\n \n");
			GameTexts.SetVariable("STR1", text);
			GameTexts.SetVariable("STR2", content2);
			text = GameTexts.FindText("str_string_newline_string", null).ToString();
			if (num > 0)
			{
				GameTexts.SetVariable("STR1", text);
				GameTexts.SetVariable("STR2", content4);
				text = GameTexts.FindText("str_string_newline_string", null).ToString();
			}
			GameTexts.SetVariable("newline", "\n");
			return text;
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x0003ECAC File Offset: 0x0003CEAC
		private static void OnHeirSelectionOver(List<InquiryElement> element)
		{
			ApplyHeirSelectionAction.ApplyByDeath(element[0].Identifier as Hero);
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x0003ECC4 File Offset: 0x0003CEC4
		private void ShowGameStatistics()
		{
			object obj = new TextObject("{=oxb2FVz5}Clan Destroyed", null);
			TextObject textObject = new TextObject("{=T2GbF6lK}With no suitable heirs, the {CLAN_NAME} clan is no more. Your journey ends here.", null);
			textObject.SetTextVariable("CLAN_NAME", Clan.PlayerClan.Name);
			TextObject textObject2 = new TextObject("{=DM6luo3c}Continue", null);
			InformationManager.ShowInquiry(new InquiryData(obj.ToString(), textObject.ToString(), true, false, textObject2.ToString(), "", delegate()
			{
				GameOverState gameState = Game.Current.GameStateManager.CreateState<GameOverState>(new object[]
				{
					GameOverState.GameOverReason.ClanDestroyed
				});
				Game.Current.GameStateManager.CleanAndPushState(gameState, 0);
			}, null, "", 0f, null, null, null), true, false);
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x0003ED5C File Offset: 0x0003CF5C
		private void GameOverCleanup()
		{
			GiveGoldAction.ApplyBetweenCharacters(Hero.MainHero, null, Hero.MainHero.Gold, true);
			Campaign.Current.MainParty.Party.ItemRoster.Clear();
			Campaign.Current.MainParty.Party.MemberRoster.Clear();
			Campaign.Current.MainParty.Party.PrisonRoster.Clear();
			Campaign.Current.MainParty.IsVisible = false;
			Campaign.Current.CameraFollowParty = null;
			Campaign.Current.MainParty.IsActive = false;
			PartyBase.MainParty.SetVisualAsDirty();
			if (Hero.MainHero.MapFaction.IsKingdomFaction && Clan.PlayerClan.Kingdom.Leader == Hero.MainHero)
			{
				DestroyKingdomAction.ApplyByKingdomLeaderDeath(Clan.PlayerClan.Kingdom);
			}
		}

		// Token: 0x0400030F RID: 783
		private readonly ItemRoster _itemsThatWillBeInherited = new ItemRoster();

		// Token: 0x04000310 RID: 784
		private readonly ItemRoster _equipmentsThatWillBeInherited = new ItemRoster();
	}
}
