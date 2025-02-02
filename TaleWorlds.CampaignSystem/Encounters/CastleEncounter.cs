using System;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.Encounters
{
	// Token: 0x02000292 RID: 658
	public class CastleEncounter : LocationEncounter
	{
		// Token: 0x06002458 RID: 9304 RVA: 0x0009AA79 File Offset: 0x00098C79
		public CastleEncounter(Settlement settlement) : base(settlement)
		{
		}

		// Token: 0x06002459 RID: 9305 RVA: 0x0009AA84 File Offset: 0x00098C84
		public override IMission CreateAndOpenMissionController(Location nextLocation, Location previousLocation = null, CharacterObject talkToChar = null, string playerSpecialSpawnTag = null)
		{
			int num = base.Settlement.Town.GetWallLevel();
			IMission result;
			if (nextLocation.StringId == "center")
			{
				result = CampaignMission.OpenCastleCourtyardMission(nextLocation.GetSceneName(num), nextLocation, talkToChar, num);
			}
			else if (nextLocation.StringId == "lordshall")
			{
				nextLocation.GetSceneName(num);
				result = CampaignMission.OpenIndoorMission(nextLocation.GetSceneName(num), num, nextLocation, talkToChar);
			}
			else
			{
				num = Campaign.Current.Models.LocationModel.GetSettlementUpgradeLevel(PlayerEncounter.LocationEncounter);
				nextLocation.GetSceneName(num);
				result = CampaignMission.OpenIndoorMission(nextLocation.GetSceneName(num), num, nextLocation, talkToChar);
			}
			return result;
		}
	}
}
