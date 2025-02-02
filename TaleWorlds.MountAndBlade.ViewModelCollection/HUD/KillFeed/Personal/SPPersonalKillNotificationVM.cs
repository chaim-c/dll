using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.HUD.KillFeed.Personal
{
	// Token: 0x0200004E RID: 78
	public class SPPersonalKillNotificationVM : ViewModel
	{
		// Token: 0x0600065D RID: 1629 RVA: 0x00019CDF File Offset: 0x00017EDF
		public SPPersonalKillNotificationVM()
		{
			this.NotificationList = new MBBindingList<SPPersonalKillNotificationItemVM>();
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x00019CF4 File Offset: 0x00017EF4
		public void OnPersonalKill(int damageAmount, bool isMountDamage, bool isFriendlyFire, bool isHeadshot, string killedAgentName, bool isUnconscious)
		{
			this.NotificationList.Add(new SPPersonalKillNotificationItemVM(damageAmount, isMountDamage, isFriendlyFire, isHeadshot, killedAgentName, isUnconscious, new Action<SPPersonalKillNotificationItemVM>(this.RemoveItem)));
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x00019D26 File Offset: 0x00017F26
		public void OnPersonalHit(int damageAmount, bool isMountDamage, bool isFriendlyFire, string killedAgentName)
		{
			this.NotificationList.Add(new SPPersonalKillNotificationItemVM(damageAmount, isMountDamage, isFriendlyFire, killedAgentName, new Action<SPPersonalKillNotificationItemVM>(this.RemoveItem)));
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00019D49 File Offset: 0x00017F49
		public void OnPersonalAssist(string killedAgentName)
		{
			this.NotificationList.Add(new SPPersonalKillNotificationItemVM(killedAgentName, new Action<SPPersonalKillNotificationItemVM>(this.RemoveItem)));
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x00019D68 File Offset: 0x00017F68
		private void RemoveItem(SPPersonalKillNotificationItemVM item)
		{
			this.NotificationList.Remove(item);
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000662 RID: 1634 RVA: 0x00019D77 File Offset: 0x00017F77
		// (set) Token: 0x06000663 RID: 1635 RVA: 0x00019D7F File Offset: 0x00017F7F
		[DataSourceProperty]
		public MBBindingList<SPPersonalKillNotificationItemVM> NotificationList
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
					base.OnPropertyChangedWithValue<MBBindingList<SPPersonalKillNotificationItemVM>>(value, "NotificationList");
				}
			}
		}

		// Token: 0x04000304 RID: 772
		private MBBindingList<SPPersonalKillNotificationItemVM> _notificationList;
	}
}
