using System;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade.View
{
	// Token: 0x02000019 RID: 25
	public static class UISoundsHelper
	{
		// Token: 0x060000A6 RID: 166 RVA: 0x00006F57 File Offset: 0x00005157
		public static void PlayUISound(string soundName)
		{
			SoundEvent.PlaySound2D(soundName);
		}

		// Token: 0x02000087 RID: 135
		public static class DefaultSounds
		{
			// Token: 0x04000306 RID: 774
			public const string DefaultSound = "event:/ui/default";

			// Token: 0x04000307 RID: 775
			public const string CheckboxSound = "event:/ui/checkbox";

			// Token: 0x04000308 RID: 776
			public const string TabSound = "event:/ui/tab";

			// Token: 0x04000309 RID: 777
			public const string SortSound = "event:/ui/sort";

			// Token: 0x0400030A RID: 778
			public const string TransferSound = "event:/ui/transfer";
		}

		// Token: 0x02000088 RID: 136
		public static class PanelSounds
		{
			// Token: 0x0400030B RID: 779
			public const string NextPanelSound = "event:/ui/panels/next";

			// Token: 0x0400030C RID: 780
			public const string InventoryPanelOpenSound = "event:/ui/panels/panel_inventory_open";

			// Token: 0x0400030D RID: 781
			public const string ClanPanelOpenSound = "event:/ui/panels/panel_clan_open";

			// Token: 0x0400030E RID: 782
			public const string CharacterPanelOpenSound = "event:/ui/panels/panel_character_open";

			// Token: 0x0400030F RID: 783
			public const string KingdomPanelOpenSound = "event:/ui/panels/panel_kingdom_open";

			// Token: 0x04000310 RID: 784
			public const string PartyPanelOpenSound = "event:/ui/panels/panel_party_open";

			// Token: 0x04000311 RID: 785
			public const string QuestPanelOpenSound = "event:/ui/panels/panel_quest_open";
		}

		// Token: 0x02000089 RID: 137
		public static class SiegeSounds
		{
			// Token: 0x04000312 RID: 786
			public const string SiegeEngineClickSound = "event:/ui/panels/siege/engine_click";

			// Token: 0x04000313 RID: 787
			public const string SiegeEngineBuildCompleteSound = "event:/ui/panels/siege/engine_build_complete";
		}

		// Token: 0x0200008A RID: 138
		public static class InventorySounds
		{
			// Token: 0x04000314 RID: 788
			public const string TakeAllSound = "event:/ui/inventory/take_all";
		}

		// Token: 0x0200008B RID: 139
		public static class PartySounds
		{
			// Token: 0x04000315 RID: 789
			public const string UpgradeSound = "event:/ui/party/upgrade";

			// Token: 0x04000316 RID: 790
			public const string RecruitSound = "event:/ui/party/recruit_prisoner";
		}

		// Token: 0x0200008C RID: 140
		public static class CraftingSounds
		{
			// Token: 0x04000317 RID: 791
			public const string RefineTabSound = "event:/ui/crafting/refine_tab";

			// Token: 0x04000318 RID: 792
			public const string CraftTabSound = "event:/ui/crafting/craft_tab";

			// Token: 0x04000319 RID: 793
			public const string SmeltTabSound = "event:/ui/crafting/smelt_tab";

			// Token: 0x0400031A RID: 794
			public const string RefineSuccessSound = "event:/ui/crafting/refine_success";

			// Token: 0x0400031B RID: 795
			public const string CraftSuccessSound = "event:/ui/crafting/craft_success";

			// Token: 0x0400031C RID: 796
			public const string SmeltSuccessSound = "event:/ui/crafting/smelt_success";
		}

		// Token: 0x0200008D RID: 141
		public static class EndgameSounds
		{
			// Token: 0x0400031D RID: 797
			public const string ClanDestroyedSound = "event:/ui/endgame/end_clan_destroyed";

			// Token: 0x0400031E RID: 798
			public const string RetirementSound = "event:/ui/endgame/end_retirement";

			// Token: 0x0400031F RID: 799
			public const string VictorySound = "event:/ui/endgame/end_victory";
		}

		// Token: 0x0200008E RID: 142
		public static class NotificationSounds
		{
			// Token: 0x04000320 RID: 800
			public const string HideoutFoundSound = "event:/ui/notification/hideout_found";
		}

		// Token: 0x0200008F RID: 143
		public static class CampaignSounds
		{
			// Token: 0x04000321 RID: 801
			public const string PartySound = "event:/ui/campaign/click_party";

			// Token: 0x04000322 RID: 802
			public const string PartyEnemySound = "event:/ui/campaign/click_party_enemy";

			// Token: 0x04000323 RID: 803
			public const string SettlementSound = "event:/ui/campaign/click_settlement";

			// Token: 0x04000324 RID: 804
			public const string SettlementEnemySound = "event:/ui/campaign/click_settlement_enemy";
		}

		// Token: 0x02000090 RID: 144
		public static class MissionSounds
		{
			// Token: 0x04000325 RID: 805
			public const string DeploySound = "event:/ui/mission/deploy";
		}

		// Token: 0x02000091 RID: 145
		public static class MultiplayerSounds
		{
			// Token: 0x04000326 RID: 806
			public const string MatchReadySound = "event:/ui/multiplayer/match_ready";
		}
	}
}
