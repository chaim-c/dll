using System;
using TaleWorlds.CampaignSystem.LogEntries;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors.CommentBehaviors
{
	// Token: 0x020003F7 RID: 1015
	public class CommentPregnancyBehavior : CampaignBehaviorBase
	{
		// Token: 0x06003EB6 RID: 16054 RVA: 0x00133BD7 File Offset: 0x00131DD7
		public override void RegisterEvents()
		{
			CampaignEvents.OnChildConceivedEvent.AddNonSerializedListener(this, new Action<Hero>(this.OnChildConceived));
		}

		// Token: 0x06003EB7 RID: 16055 RVA: 0x00133BF0 File Offset: 0x00131DF0
		private void OnChildConceived(Hero mother)
		{
			LogEntry.AddLogEntry(new PregnancyLogEntry(mother));
		}

		// Token: 0x06003EB8 RID: 16056 RVA: 0x00133BFD File Offset: 0x00131DFD
		public override void SyncData(IDataStore dataStore)
		{
		}
	}
}
