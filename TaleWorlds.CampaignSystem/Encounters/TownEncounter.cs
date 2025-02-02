using System;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.Encounters
{
	// Token: 0x02000296 RID: 662
	public class TownEncounter : LocationEncounter
	{
		// Token: 0x0600246A RID: 9322 RVA: 0x0009AD7C File Offset: 0x00098F7C
		public TownEncounter(Settlement settlement) : base(settlement)
		{
		}

		// Token: 0x0600246B RID: 9323 RVA: 0x0009AD88 File Offset: 0x00098F88
		public override IMission CreateAndOpenMissionController(Location nextLocation, Location previousLocation = null, CharacterObject talkToChar = null, string playerSpecialSpawnTag = null)
		{
			int num = base.Settlement.Town.GetWallLevel();
			string sceneName = nextLocation.GetSceneName(num);
			IMission result;
			if (nextLocation.StringId == "center")
			{
				result = CampaignMission.OpenTownCenterMission(sceneName, nextLocation, talkToChar, num, playerSpecialSpawnTag);
			}
			else if (nextLocation.StringId == "arena")
			{
				result = CampaignMission.OpenArenaStartMission(sceneName, nextLocation, talkToChar);
			}
			else
			{
				num = Campaign.Current.Models.LocationModel.GetSettlementUpgradeLevel(PlayerEncounter.LocationEncounter);
				result = CampaignMission.OpenIndoorMission(sceneName, num, nextLocation, talkToChar);
			}
			return result;
		}
	}
}
