using System;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.GameComponents
{
	// Token: 0x02000112 RID: 274
	public class DefaultLocationModel : LocationModel
	{
		// Token: 0x0600160A RID: 5642 RVA: 0x00069113 File Offset: 0x00067313
		public override int GetSettlementUpgradeLevel(LocationEncounter locationEncounter)
		{
			return locationEncounter.Settlement.Town.GetWallLevel();
		}

		// Token: 0x0600160B RID: 5643 RVA: 0x00069128 File Offset: 0x00067328
		public override string GetCivilianSceneLevel(Settlement settlement)
		{
			string text = "civilian";
			if (settlement.IsFortification)
			{
				string upgradeLevelTag = this.GetUpgradeLevelTag(settlement.Town.GetWallLevel());
				if (!upgradeLevelTag.IsEmpty<char>())
				{
					text = text + " " + upgradeLevelTag;
				}
			}
			return text;
		}

		// Token: 0x0600160C RID: 5644 RVA: 0x0006916C File Offset: 0x0006736C
		public override string GetCivilianUpgradeLevelTag(int upgradeLevel)
		{
			if (upgradeLevel == 0)
			{
				return "";
			}
			string text = "civilian";
			string upgradeLevelTag = this.GetUpgradeLevelTag(upgradeLevel);
			if (!upgradeLevelTag.IsEmpty<char>())
			{
				text = text + " " + upgradeLevelTag;
			}
			return text;
		}

		// Token: 0x0600160D RID: 5645 RVA: 0x000691A6 File Offset: 0x000673A6
		public override string GetUpgradeLevelTag(int upgradeLevel)
		{
			switch (upgradeLevel)
			{
			case 1:
				return "level_1";
			case 2:
				return "level_2";
			case 3:
				return "level_3";
			default:
				return "";
			}
		}
	}
}
