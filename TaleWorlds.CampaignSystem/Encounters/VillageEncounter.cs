using System;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.Encounters
{
	// Token: 0x02000297 RID: 663
	public class VillageEncounter : LocationEncounter
	{
		// Token: 0x0600246C RID: 9324 RVA: 0x0009AE10 File Offset: 0x00099010
		public VillageEncounter(Settlement settlement) : base(settlement)
		{
		}

		// Token: 0x0600246D RID: 9325 RVA: 0x0009AE1C File Offset: 0x0009901C
		public override IMission CreateAndOpenMissionController(Location nextLocation, Location previousLocation = null, CharacterObject talkToChar = null, string playerSpecialSpawnTag = null)
		{
			IMission result = null;
			if (nextLocation.StringId == "village_center")
			{
				result = CampaignMission.OpenVillageMission(nextLocation.GetSceneName(1), nextLocation, talkToChar);
			}
			return result;
		}
	}
}
