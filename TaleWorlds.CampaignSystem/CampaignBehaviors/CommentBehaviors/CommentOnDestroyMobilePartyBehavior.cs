using System;
using TaleWorlds.CampaignSystem.LogEntries;
using TaleWorlds.CampaignSystem.Party;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors.CommentBehaviors
{
	// Token: 0x020003F1 RID: 1009
	public class CommentOnDestroyMobilePartyBehavior : CampaignBehaviorBase
	{
		// Token: 0x06003E9E RID: 16030 RVA: 0x001339E2 File Offset: 0x00131BE2
		public override void RegisterEvents()
		{
			CampaignEvents.MobilePartyDestroyed.AddNonSerializedListener(this, new Action<MobileParty, PartyBase>(this.OnMobilePartyDestroyed));
		}

		// Token: 0x06003E9F RID: 16031 RVA: 0x001339FB File Offset: 0x00131BFB
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x06003EA0 RID: 16032 RVA: 0x00133A00 File Offset: 0x00131C00
		private void OnMobilePartyDestroyed(MobileParty mobileParty, PartyBase destroyerParty)
		{
			Hero hero = (destroyerParty != null) ? destroyerParty.LeaderHero : null;
			IFaction faction = (destroyerParty != null) ? destroyerParty.MapFaction : null;
			if (hero == Hero.MainHero || mobileParty.LeaderHero == Hero.MainHero || (faction != null && mobileParty.MapFaction != null && faction.IsKingdomFaction && mobileParty.MapFaction.IsKingdomFaction))
			{
				LogEntry.AddLogEntry(new DestroyMobilePartyLogEntry(mobileParty, destroyerParty));
			}
		}
	}
}
