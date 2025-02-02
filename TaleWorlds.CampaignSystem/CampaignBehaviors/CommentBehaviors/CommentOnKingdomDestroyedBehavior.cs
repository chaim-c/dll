using System;
using TaleWorlds.CampaignSystem.LogEntries;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors.CommentBehaviors
{
	// Token: 0x020003F3 RID: 1011
	public class CommentOnKingdomDestroyedBehavior : CampaignBehaviorBase
	{
		// Token: 0x06003EA6 RID: 16038 RVA: 0x00133AAE File Offset: 0x00131CAE
		public override void RegisterEvents()
		{
			CampaignEvents.KingdomDestroyedEvent.AddNonSerializedListener(this, new Action<Kingdom>(this.OnKingdomDestroyed));
		}

		// Token: 0x06003EA7 RID: 16039 RVA: 0x00133AC7 File Offset: 0x00131CC7
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x06003EA8 RID: 16040 RVA: 0x00133AC9 File Offset: 0x00131CC9
		private void OnKingdomDestroyed(Kingdom destroyedKingdom)
		{
			LogEntry.AddLogEntry(new KingdomDestroyedLogEntry(destroyedKingdom));
		}
	}
}
