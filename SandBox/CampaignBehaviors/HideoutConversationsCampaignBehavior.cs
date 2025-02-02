using System;
using SandBox.Missions.MissionLogics;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.MountAndBlade;

namespace SandBox.CampaignBehaviors
{
	// Token: 0x020000AD RID: 173
	public class HideoutConversationsCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x06000822 RID: 2082 RVA: 0x0003EE53 File Offset: 0x0003D053
		public override void RegisterEvents()
		{
			CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnSessionLaunched));
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x0003EE6C File Offset: 0x0003D06C
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x0003EE6E File Offset: 0x0003D06E
		private void OnSessionLaunched(CampaignGameStarter campaignGameStarter)
		{
			this.AddDialogs(campaignGameStarter);
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x0003EE78 File Offset: 0x0003D078
		private void AddDialogs(CampaignGameStarter campaignGameStarter)
		{
			campaignGameStarter.AddDialogLine("bandit_hideout_start_defender", "start", "bandit_hideout_defender", "{=nYCXzAYH}You! You've cut quite a swathe through my men there, damn you. How about we settle this, one-on-one?", new ConversationSentence.OnConditionDelegate(this.bandit_hideout_start_defender_on_condition), null, 100, null);
			campaignGameStarter.AddPlayerLine("bandit_hideout_start_defender_1", "bandit_hideout_defender", "close_window", "{=dzXaXKaC}Very well.", null, new ConversationSentence.OnConsequenceDelegate(this.bandit_hideout_start_duel_fight_on_consequence), 100, null, null);
			campaignGameStarter.AddPlayerLine("bandit_hideout_start_defender_2", "bandit_hideout_defender", "close_window", "{=ukRZd2AA}I don't fight duels with brigands.", null, new ConversationSentence.OnConsequenceDelegate(this.bandit_hideout_continue_battle_on_consequence), 100, null, null);
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x0003EF08 File Offset: 0x0003D108
		private bool bandit_hideout_start_defender_on_condition()
		{
			PartyBase encounteredParty = PlayerEncounter.EncounteredParty;
			return encounteredParty != null && !encounteredParty.IsMobile && encounteredParty.MapFaction.IsBanditFaction && (encounteredParty.MapFaction.IsBanditFaction && encounteredParty.IsSettlement && encounteredParty.Settlement.IsHideout && Mission.Current != null) && Mission.Current.GetMissionBehavior<HideoutMissionController>() != null;
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x0003EF6D File Offset: 0x0003D16D
		private void bandit_hideout_start_duel_fight_on_consequence()
		{
			Campaign.Current.ConversationManager.ConversationEndOneShot += HideoutMissionController.StartBossFightDuelMode;
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x0003EF8A File Offset: 0x0003D18A
		private void bandit_hideout_continue_battle_on_consequence()
		{
			Campaign.Current.ConversationManager.ConversationEndOneShot += HideoutMissionController.StartBossFightBattleMode;
		}
	}
}
