using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.ObjectSystem;

namespace SandBox.Missions.MissionLogics
{
	// Token: 0x02000066 RID: 102
	public class SearchBodyMissionHandler : MissionLogic
	{
		// Token: 0x060003E3 RID: 995 RVA: 0x0001ADC0 File Offset: 0x00018FC0
		public override void OnAgentInteraction(Agent userAgent, Agent agent)
		{
			if (Campaign.Current.GameMode == CampaignGameMode.Campaign)
			{
				if (Game.Current.GameStateManager.ActiveState is MissionState)
				{
					if (base.Mission.Mode != MissionMode.Conversation && base.Mission.Mode != MissionMode.Battle && this.IsSearchable(agent))
					{
						this.AddItemsToPlayer(agent);
						return;
					}
				}
				else
				{
					Debug.FailedAssert("Agent interaction must occur in MissionState.", "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox\\Missions\\MissionLogics\\SearchBodyMissionHandler.cs", "OnAgentInteraction", 26);
				}
			}
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0001AE33 File Offset: 0x00019033
		public override bool IsThereAgentAction(Agent userAgent, Agent otherAgent)
		{
			return Mission.Current.Mode != MissionMode.Battle && base.Mission.Mode != MissionMode.Duel && base.Mission.Mode != MissionMode.Conversation && this.IsSearchable(otherAgent);
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0001AE6A File Offset: 0x0001906A
		private bool IsSearchable(Agent agent)
		{
			return !agent.IsActive() && agent.IsHuman && agent.Character.IsHero;
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0001AE8C File Offset: 0x0001908C
		private void AddItemsToPlayer(Agent interactedAgent)
		{
			CharacterObject characterObject = (CharacterObject)interactedAgent.Character;
			if (MBRandom.RandomInt(2) == 0)
			{
				characterObject.HeroObject.SpecialItems.Add(MBObjectManager.Instance.GetObject<ItemObject>("leafblade_throwing_knife"));
			}
			else
			{
				characterObject.HeroObject.SpecialItems.Add(MBObjectManager.Instance.GetObject<ItemObject>("falchion_sword_t2"));
				characterObject.HeroObject.SpecialItems.Add(MBObjectManager.Instance.GetObject<ItemObject>("cleaver_sword_t3"));
			}
			foreach (ItemObject itemObject in characterObject.HeroObject.SpecialItems)
			{
				PartyBase.MainParty.ItemRoster.AddToCounts(itemObject, 1);
				MBTextManager.SetTextVariable("ITEM_NAME", itemObject.Name, false);
				InformationManager.DisplayMessage(new InformationMessage(GameTexts.FindText("str_item_taken", null).ToString()));
			}
			characterObject.HeroObject.SpecialItems.Clear();
		}
	}
}
