using System;
using TaleWorlds.CampaignSystem.LogEntries;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors.CommentBehaviors
{
	// Token: 0x020003EE RID: 1006
	public class CommentOnClanLeaderChangedBehavior : CampaignBehaviorBase
	{
		// Token: 0x06003E92 RID: 16018 RVA: 0x00133908 File Offset: 0x00131B08
		public override void RegisterEvents()
		{
			CampaignEvents.OnClanLeaderChangedEvent.AddNonSerializedListener(this, new Action<Hero, Hero>(CommentOnClanLeaderChangedBehavior.OnClanLeaderChanged));
		}

		// Token: 0x06003E93 RID: 16019 RVA: 0x00133921 File Offset: 0x00131B21
		private static void OnClanLeaderChanged(Hero oldLeader, Hero newLeader)
		{
			LogEntry.AddLogEntry(new ClanLeaderChangedLogEntry(oldLeader, newLeader));
		}

		// Token: 0x06003E94 RID: 16020 RVA: 0x0013392F File Offset: 0x00131B2F
		public override void SyncData(IDataStore dataStore)
		{
		}
	}
}
