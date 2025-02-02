using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.HUD.KillFeed.Personal
{
	// Token: 0x0200004D RID: 77
	public class SPPersonalKillNotificationItemVM : ViewModel
	{
		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x0600064F RID: 1615 RVA: 0x00019B02 File Offset: 0x00017D02
		// (set) Token: 0x06000650 RID: 1616 RVA: 0x00019B0A File Offset: 0x00017D0A
		private SPPersonalKillNotificationItemVM.ItemTypes ItemTypeAsEnum
		{
			get
			{
				return this._itemTypeAsEnum;
			}
			set
			{
				this._itemType = (int)value;
				this._itemTypeAsEnum = value;
			}
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00019B1C File Offset: 0x00017D1C
		public SPPersonalKillNotificationItemVM(int damageAmount, bool isMountDamage, bool isFriendlyFire, bool isHeadshot, string killedAgentName, bool isUnconscious, Action<SPPersonalKillNotificationItemVM> onRemoveItem)
		{
			this._onRemoveItem = onRemoveItem;
			this.Amount = damageAmount;
			if (isFriendlyFire)
			{
				this.ItemTypeAsEnum = SPPersonalKillNotificationItemVM.ItemTypes.FriendlyFireKill;
				this.Message = killedAgentName;
				return;
			}
			if (isMountDamage)
			{
				this.ItemTypeAsEnum = SPPersonalKillNotificationItemVM.ItemTypes.MountDamage;
				this.Message = GameTexts.FindText("str_damage_delivered_message", null).ToString();
				return;
			}
			this.ItemTypeAsEnum = (isUnconscious ? (isHeadshot ? SPPersonalKillNotificationItemVM.ItemTypes.MakeUnconsciousHeadshot : SPPersonalKillNotificationItemVM.ItemTypes.MakeUnconscious) : (isHeadshot ? SPPersonalKillNotificationItemVM.ItemTypes.NormalKillHeadshot : SPPersonalKillNotificationItemVM.ItemTypes.NormalKill));
			this.Message = killedAgentName;
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00019B98 File Offset: 0x00017D98
		public SPPersonalKillNotificationItemVM(int amount, bool isMountDamage, bool isFriendlyFire, string killedAgentName, Action<SPPersonalKillNotificationItemVM> onRemoveItem)
		{
			this._onRemoveItem = onRemoveItem;
			this.Amount = amount;
			if (isFriendlyFire)
			{
				this.ItemTypeAsEnum = SPPersonalKillNotificationItemVM.ItemTypes.FriendlyFireDamage;
				this.Message = killedAgentName;
				return;
			}
			if (isMountDamage)
			{
				this.ItemTypeAsEnum = SPPersonalKillNotificationItemVM.ItemTypes.MountDamage;
				this.Message = GameTexts.FindText("str_damage_delivered_message", null).ToString();
				return;
			}
			this.ItemTypeAsEnum = SPPersonalKillNotificationItemVM.ItemTypes.NormalDamage;
			this.Message = GameTexts.FindText("str_damage_delivered_message", null).ToString();
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x00019C0B File Offset: 0x00017E0B
		public SPPersonalKillNotificationItemVM(string victimAgentName, Action<SPPersonalKillNotificationItemVM> onRemoveItem)
		{
			this._onRemoveItem = onRemoveItem;
			this.Amount = -1;
			this.Message = victimAgentName;
			this.ItemTypeAsEnum = SPPersonalKillNotificationItemVM.ItemTypes.Assist;
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x00019C2F File Offset: 0x00017E2F
		public void ExecuteRemove()
		{
			this._onRemoveItem(this);
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000655 RID: 1621 RVA: 0x00019C3D File Offset: 0x00017E3D
		// (set) Token: 0x06000656 RID: 1622 RVA: 0x00019C45 File Offset: 0x00017E45
		[DataSourceProperty]
		public string VictimType
		{
			get
			{
				return this._victimType;
			}
			set
			{
				if (value != this._victimType)
				{
					this._victimType = value;
					base.OnPropertyChangedWithValue<string>(value, "VictimType");
				}
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000657 RID: 1623 RVA: 0x00019C68 File Offset: 0x00017E68
		// (set) Token: 0x06000658 RID: 1624 RVA: 0x00019C70 File Offset: 0x00017E70
		[DataSourceProperty]
		public string Message
		{
			get
			{
				return this._message;
			}
			set
			{
				if (value != this._message)
				{
					this._message = value;
					base.OnPropertyChangedWithValue<string>(value, "Message");
				}
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000659 RID: 1625 RVA: 0x00019C93 File Offset: 0x00017E93
		// (set) Token: 0x0600065A RID: 1626 RVA: 0x00019C9B File Offset: 0x00017E9B
		[DataSourceProperty]
		public int ItemType
		{
			get
			{
				return this._itemType;
			}
			set
			{
				if (value != this._itemType)
				{
					this._itemType = value;
					base.OnPropertyChangedWithValue(value, "ItemType");
				}
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x00019CB9 File Offset: 0x00017EB9
		// (set) Token: 0x0600065C RID: 1628 RVA: 0x00019CC1 File Offset: 0x00017EC1
		[DataSourceProperty]
		public int Amount
		{
			get
			{
				return this._amount;
			}
			set
			{
				if (value != this._amount)
				{
					this._amount = value;
					base.OnPropertyChangedWithValue(value, "Amount");
				}
			}
		}

		// Token: 0x040002FE RID: 766
		private Action<SPPersonalKillNotificationItemVM> _onRemoveItem;

		// Token: 0x040002FF RID: 767
		private SPPersonalKillNotificationItemVM.ItemTypes _itemTypeAsEnum;

		// Token: 0x04000300 RID: 768
		private string _message;

		// Token: 0x04000301 RID: 769
		private string _victimType;

		// Token: 0x04000302 RID: 770
		private int _amount;

		// Token: 0x04000303 RID: 771
		private int _itemType;

		// Token: 0x020000D1 RID: 209
		private enum ItemTypes
		{
			// Token: 0x04000604 RID: 1540
			NormalDamage,
			// Token: 0x04000605 RID: 1541
			FriendlyFireDamage,
			// Token: 0x04000606 RID: 1542
			FriendlyFireKill,
			// Token: 0x04000607 RID: 1543
			MountDamage,
			// Token: 0x04000608 RID: 1544
			NormalKill,
			// Token: 0x04000609 RID: 1545
			Assist,
			// Token: 0x0400060A RID: 1546
			MakeUnconscious,
			// Token: 0x0400060B RID: 1547
			NormalKillHeadshot,
			// Token: 0x0400060C RID: 1548
			MakeUnconsciousHeadshot
		}
	}
}
