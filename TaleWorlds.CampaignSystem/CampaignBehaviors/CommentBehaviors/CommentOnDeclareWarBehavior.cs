using System;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.LogEntries;
using TaleWorlds.CampaignSystem.MapNotificationTypes;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors.CommentBehaviors
{
	// Token: 0x020003EF RID: 1007
	public class CommentOnDeclareWarBehavior : CampaignBehaviorBase
	{
		// Token: 0x06003E96 RID: 16022 RVA: 0x00133939 File Offset: 0x00131B39
		public override void RegisterEvents()
		{
			CampaignEvents.WarDeclared.AddNonSerializedListener(this, new Action<IFaction, IFaction, DeclareWarAction.DeclareWarDetail>(this.OnWarDeclared));
		}

		// Token: 0x06003E97 RID: 16023 RVA: 0x00133952 File Offset: 0x00131B52
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x06003E98 RID: 16024 RVA: 0x00133954 File Offset: 0x00131B54
		private void OnWarDeclared(IFaction faction1, IFaction faction2, DeclareWarAction.DeclareWarDetail detail)
		{
			DeclareWarLogEntry declareWarLogEntry = new DeclareWarLogEntry(faction1, faction2);
			LogEntry.AddLogEntry(declareWarLogEntry);
			if (faction2 == Hero.MainHero.MapFaction || (faction1 == Hero.MainHero.MapFaction && detail != DeclareWarAction.DeclareWarDetail.CausedByKingdomDecision))
			{
				Campaign.Current.CampaignInformationManager.NewMapNoticeAdded(new WarMapNotification(faction1, faction2, declareWarLogEntry.GetEncyclopediaText()));
			}
		}
	}
}
