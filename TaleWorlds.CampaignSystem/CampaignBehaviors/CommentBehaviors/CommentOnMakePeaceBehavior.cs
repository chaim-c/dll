using System;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.LogEntries;
using TaleWorlds.CampaignSystem.MapNotificationTypes;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors.CommentBehaviors
{
	// Token: 0x020003F5 RID: 1013
	public class CommentOnMakePeaceBehavior : CampaignBehaviorBase
	{
		// Token: 0x06003EAE RID: 16046 RVA: 0x00133B15 File Offset: 0x00131D15
		public override void RegisterEvents()
		{
			CampaignEvents.MakePeace.AddNonSerializedListener(this, new Action<IFaction, IFaction, MakePeaceAction.MakePeaceDetail>(this.OnMakePeace));
		}

		// Token: 0x06003EAF RID: 16047 RVA: 0x00133B2E File Offset: 0x00131D2E
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x06003EB0 RID: 16048 RVA: 0x00133B30 File Offset: 0x00131D30
		private void OnMakePeace(IFaction faction1, IFaction faction2, MakePeaceAction.MakePeaceDetail detail)
		{
			MakePeaceLogEntry makePeaceLogEntry = new MakePeaceLogEntry(faction1, faction2);
			LogEntry.AddLogEntry(makePeaceLogEntry);
			if (faction2 == Hero.MainHero.MapFaction || (faction1 == Hero.MainHero.MapFaction && detail != MakePeaceAction.MakePeaceDetail.ByKingdomDecision))
			{
				Campaign.Current.CampaignInformationManager.NewMapNoticeAdded(new PeaceMapNotification(faction1, faction2, makePeaceLogEntry.GetEncyclopediaText()));
			}
		}
	}
}
