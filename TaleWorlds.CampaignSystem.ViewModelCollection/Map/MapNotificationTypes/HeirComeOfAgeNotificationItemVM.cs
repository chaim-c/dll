using System;
using TaleWorlds.CampaignSystem.MapNotificationTypes;
using TaleWorlds.CampaignSystem.SceneInformationPopupTypes;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Map.MapNotificationTypes
{
	// Token: 0x0200003D RID: 61
	public class HeirComeOfAgeNotificationItemVM : MapNotificationItemBaseVM
	{
		// Token: 0x0600056B RID: 1387 RVA: 0x0001B960 File Offset: 0x00019B60
		public HeirComeOfAgeNotificationItemVM(HeirComeOfAgeMapNotification data) : base(data)
		{
			HeirComeOfAgeNotificationItemVM <>4__this = this;
			base.NotificationIdentifier = "comeofage";
			this._onInspect = delegate()
			{
				<>4__this.OnInspect(data);
			};
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0001B9AC File Offset: 0x00019BAC
		private void OnInspect(HeirComeOfAgeMapNotification data)
		{
			SceneNotificationData data2;
			if (data.ComeOfAgeHero.IsFemale)
			{
				data2 = new HeirComingOfAgeFemaleSceneNotificationItem(data.MentorHero, data.ComeOfAgeHero, data.CreationTime);
			}
			else
			{
				data2 = new HeirComingOfAgeSceneNotificationItem(data.MentorHero, data.ComeOfAgeHero, data.CreationTime);
			}
			MBInformationManager.ShowSceneNotification(data2);
			base.ExecuteRemove();
		}
	}
}
