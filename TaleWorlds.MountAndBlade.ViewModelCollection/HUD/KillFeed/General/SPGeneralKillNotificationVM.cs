using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.HUD.KillFeed.General
{
	// Token: 0x02000050 RID: 80
	public class SPGeneralKillNotificationVM : ViewModel
	{
		// Token: 0x0600066C RID: 1644 RVA: 0x0001A06B File Offset: 0x0001826B
		public SPGeneralKillNotificationVM()
		{
			this.NotificationList = new MBBindingList<SPGeneralKillNotificationItemVM>();
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x0001A07E File Offset: 0x0001827E
		public void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, Agent assistedAgent, bool isHeadshot)
		{
			this.NotificationList.Add(new SPGeneralKillNotificationItemVM(affectedAgent, affectorAgent, assistedAgent, isHeadshot, new Action<SPGeneralKillNotificationItemVM>(this.RemoveItem)));
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x0001A0A1 File Offset: 0x000182A1
		private void RemoveItem(SPGeneralKillNotificationItemVM item)
		{
			this.NotificationList.Remove(item);
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x0001A0B0 File Offset: 0x000182B0
		// (set) Token: 0x06000670 RID: 1648 RVA: 0x0001A0B8 File Offset: 0x000182B8
		[DataSourceProperty]
		public MBBindingList<SPGeneralKillNotificationItemVM> NotificationList
		{
			get
			{
				return this._notificationList;
			}
			set
			{
				if (value != this._notificationList)
				{
					this._notificationList = value;
					base.OnPropertyChangedWithValue<MBBindingList<SPGeneralKillNotificationItemVM>>(value, "NotificationList");
				}
			}
		}

		// Token: 0x0400030C RID: 780
		private MBBindingList<SPGeneralKillNotificationItemVM> _notificationList;
	}
}
