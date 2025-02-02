using System;
using TaleWorlds.CampaignSystem.MapNotificationTypes;
using TaleWorlds.CampaignSystem.SceneInformationPopupTypes;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Map.MapNotificationTypes
{
	// Token: 0x02000041 RID: 65
	public class MarriageNotificationItemVM : MapNotificationItemBaseVM
	{
		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x0001BEDA File Offset: 0x0001A0DA
		// (set) Token: 0x06000596 RID: 1430 RVA: 0x0001BEE2 File Offset: 0x0001A0E2
		public Hero Suitor { get; private set; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x0001BEEB File Offset: 0x0001A0EB
		// (set) Token: 0x06000598 RID: 1432 RVA: 0x0001BEF3 File Offset: 0x0001A0F3
		public Hero Maiden { get; private set; }

		// Token: 0x06000599 RID: 1433 RVA: 0x0001BEFC File Offset: 0x0001A0FC
		public MarriageNotificationItemVM(MarriageMapNotification data) : base(data)
		{
			this.Suitor = data.Suitor;
			this.Maiden = data.Maiden;
			base.NotificationIdentifier = "marriage";
			this._onInspect = delegate()
			{
				MBInformationManager.ShowSceneNotification(new MarriageSceneNotificationItem(data.Suitor, data.Maiden, data.CreationTime, SceneNotificationData.RelevantContextType.Any));
			};
		}
	}
}
