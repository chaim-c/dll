using System;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.LogEntries;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors.CommentBehaviors
{
	// Token: 0x020003F4 RID: 1012
	public class CommentOnLeaveFactionBehavior : CampaignBehaviorBase
	{
		// Token: 0x06003EAA RID: 16042 RVA: 0x00133ADE File Offset: 0x00131CDE
		public override void RegisterEvents()
		{
			CampaignEvents.OnClanChangedKingdomEvent.AddNonSerializedListener(this, new Action<Clan, Kingdom, Kingdom, ChangeKingdomAction.ChangeKingdomActionDetail, bool>(this.OnClanLeaveKingdom));
		}

		// Token: 0x06003EAB RID: 16043 RVA: 0x00133AF7 File Offset: 0x00131CF7
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x06003EAC RID: 16044 RVA: 0x00133AF9 File Offset: 0x00131CF9
		private void OnClanLeaveKingdom(Clan clan, Kingdom oldKingdom, Kingdom newKingdom, ChangeKingdomAction.ChangeKingdomActionDetail detail, bool showNotification)
		{
			LogEntry.AddLogEntry(new ClanChangeKingdomLogEntry(clan, oldKingdom, newKingdom, detail == ChangeKingdomAction.ChangeKingdomActionDetail.LeaveWithRebellion));
		}
	}
}
