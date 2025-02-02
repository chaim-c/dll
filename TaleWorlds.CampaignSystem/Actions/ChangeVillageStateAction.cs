using System;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x02000436 RID: 1078
	public static class ChangeVillageStateAction
	{
		// Token: 0x06004086 RID: 16518 RVA: 0x0013E37C File Offset: 0x0013C57C
		private static void ApplyInternal(Village village, Village.VillageStates newState, MobileParty raiderParty)
		{
			Village.VillageStates villageState = village.VillageState;
			if (newState != villageState)
			{
				village.VillageState = newState;
				CampaignEventDispatcher.Instance.OnVillageStateChanged(village, villageState, village.VillageState, raiderParty);
				village.Settlement.Party.SetLevelMaskIsDirty();
			}
		}

		// Token: 0x06004087 RID: 16519 RVA: 0x0013E3BE File Offset: 0x0013C5BE
		public static void ApplyBySettingToNormal(Settlement settlement)
		{
			ChangeVillageStateAction.ApplyInternal(settlement.Village, Village.VillageStates.Normal, null);
		}

		// Token: 0x06004088 RID: 16520 RVA: 0x0013E3CD File Offset: 0x0013C5CD
		public static void ApplyBySettingToBeingRaided(Settlement settlement, MobileParty raider)
		{
			ChangeVillageStateAction.ApplyInternal(settlement.Village, Village.VillageStates.BeingRaided, raider);
		}

		// Token: 0x06004089 RID: 16521 RVA: 0x0013E3DC File Offset: 0x0013C5DC
		public static void ApplyBySettingToBeingForcedForSupplies(Settlement settlement, MobileParty raider)
		{
			ChangeVillageStateAction.ApplyInternal(settlement.Village, Village.VillageStates.ForcedForSupplies, raider);
		}

		// Token: 0x0600408A RID: 16522 RVA: 0x0013E3EB File Offset: 0x0013C5EB
		public static void ApplyBySettingToBeingForcedForVolunteers(Settlement settlement, MobileParty raider)
		{
			ChangeVillageStateAction.ApplyInternal(settlement.Village, Village.VillageStates.ForcedForVolunteers, raider);
		}

		// Token: 0x0600408B RID: 16523 RVA: 0x0013E3FA File Offset: 0x0013C5FA
		public static void ApplyBySettingToLooted(Settlement settlement, MobileParty raider)
		{
			ChangeVillageStateAction.ApplyInternal(settlement.Village, Village.VillageStates.Looted, raider);
		}
	}
}
