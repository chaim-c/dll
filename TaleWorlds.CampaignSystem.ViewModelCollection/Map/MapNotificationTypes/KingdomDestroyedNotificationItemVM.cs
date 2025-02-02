using System;
using TaleWorlds.CampaignSystem.MapNotificationTypes;
using TaleWorlds.CampaignSystem.SceneInformationPopupTypes;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Map.MapNotificationTypes
{
	// Token: 0x0200003E RID: 62
	public class KingdomDestroyedNotificationItemVM : MapNotificationItemBaseVM
	{
		// Token: 0x0600056D RID: 1389 RVA: 0x0001BA04 File Offset: 0x00019C04
		public KingdomDestroyedNotificationItemVM(KingdomDestroyedMapNotification data) : base(data)
		{
			KingdomDestroyedNotificationItemVM <>4__this = this;
			base.NotificationIdentifier = "kingdomdestroyed";
			this._onInspect = delegate()
			{
				<>4__this.OnInspect(data);
			};
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0001BA4E File Offset: 0x00019C4E
		private void OnInspect(KingdomDestroyedMapNotification data)
		{
			MBInformationManager.ShowSceneNotification(new KingdomDestroyedSceneNotificationItem(data.DestroyedKingdom, data.CreationTime));
			base.ExecuteRemove();
		}
	}
}
