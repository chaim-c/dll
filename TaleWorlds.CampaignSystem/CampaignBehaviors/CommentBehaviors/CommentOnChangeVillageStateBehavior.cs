using System;
using TaleWorlds.CampaignSystem.LogEntries;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors.CommentBehaviors
{
	// Token: 0x020003EB RID: 1003
	public class CommentOnChangeVillageStateBehavior : CampaignBehaviorBase
	{
		// Token: 0x06003E85 RID: 16005 RVA: 0x001336E7 File Offset: 0x001318E7
		public override void RegisterEvents()
		{
			CampaignEvents.VillageStateChanged.AddNonSerializedListener(this, new Action<Village, Village.VillageStates, Village.VillageStates, MobileParty>(this.OnVillageStateChanged));
		}

		// Token: 0x06003E86 RID: 16006 RVA: 0x00133700 File Offset: 0x00131900
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x06003E87 RID: 16007 RVA: 0x00133704 File Offset: 0x00131904
		private void OnVillageStateChanged(Village village, Village.VillageStates oldState, Village.VillageStates newState, MobileParty raiderParty)
		{
			if (newState != Village.VillageStates.Normal && raiderParty != null && (raiderParty.LeaderHero == Hero.MainHero || village.Owner.Settlement.OwnerClan.Leader == Hero.MainHero || village.Settlement.MapFaction.IsKingdomFaction || raiderParty.MapFaction.IsKingdomFaction))
			{
				LogEntry.AddLogEntry(new VillageStateChangedLogEntry(village, oldState, newState, raiderParty));
			}
		}
	}
}
