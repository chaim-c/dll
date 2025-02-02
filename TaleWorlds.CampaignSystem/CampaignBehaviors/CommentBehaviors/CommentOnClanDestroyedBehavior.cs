using System;
using TaleWorlds.CampaignSystem.LogEntries;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors.CommentBehaviors
{
	// Token: 0x020003ED RID: 1005
	public class CommentOnClanDestroyedBehavior : CampaignBehaviorBase
	{
		// Token: 0x06003E8E RID: 16014 RVA: 0x001338D8 File Offset: 0x00131AD8
		public override void RegisterEvents()
		{
			CampaignEvents.OnClanDestroyedEvent.AddNonSerializedListener(this, new Action<Clan>(this.OnClanDestroyed));
		}

		// Token: 0x06003E8F RID: 16015 RVA: 0x001338F1 File Offset: 0x00131AF1
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x06003E90 RID: 16016 RVA: 0x001338F3 File Offset: 0x00131AF3
		private void OnClanDestroyed(Clan destroyedClan)
		{
			LogEntry.AddLogEntry(new ClanDestroyedLogEntry(destroyedClan));
		}
	}
}
