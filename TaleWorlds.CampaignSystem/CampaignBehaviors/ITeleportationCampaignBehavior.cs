using System;
using TaleWorlds.CampaignSystem.Map;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x020003A4 RID: 932
	public interface ITeleportationCampaignBehavior : ICampaignBehavior
	{
		// Token: 0x06003813 RID: 14355
		bool GetTargetOfTeleportingHero(Hero teleportingHero, out bool isGovernor, out bool isPartyLeader, out IMapPoint target);

		// Token: 0x06003814 RID: 14356
		CampaignTime GetHeroArrivalTimeToDestination(Hero teleportingHero);
	}
}
