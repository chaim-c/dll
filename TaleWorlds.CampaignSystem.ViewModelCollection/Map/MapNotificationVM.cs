using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem.MapNotificationTypes;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.ViewModelCollection.Input;
using TaleWorlds.CampaignSystem.ViewModelCollection.Map.MapNotificationTypes;
using TaleWorlds.Core;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Map
{
	// Token: 0x02000033 RID: 51
	public class MapNotificationVM : ViewModel
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060004FA RID: 1274 RVA: 0x0001A3B8 File Offset: 0x000185B8
		// (remove) Token: 0x060004FB RID: 1275 RVA: 0x0001A3F0 File Offset: 0x000185F0
		public event Action<MapNotificationItemBaseVM> ReceiveNewNotification;

		// Token: 0x060004FC RID: 1276 RVA: 0x0001A428 File Offset: 0x00018628
		public MapNotificationVM(INavigationHandler navigationHandler, Action<Vec2> fastMoveCameraToPosition)
		{
			this._navigationHandler = navigationHandler;
			this._fastMoveCameraToPosition = fastMoveCameraToPosition;
			MBInformationManager.OnAddMapNotice += this.AddMapNotification;
			this.NotificationItems = new MBBindingList<MapNotificationItemBaseVM>();
			this.PopulateTypeDictionary();
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0001A476 File Offset: 0x00018676
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.NotificationItems.ApplyActionOnAllItems(delegate(MapNotificationItemBaseVM x)
			{
				x.RefreshValues();
			});
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0001A4A8 File Offset: 0x000186A8
		private void PopulateTypeDictionary()
		{
			this._itemConstructors.Add(typeof(PeaceMapNotification), typeof(PeaceNotificationItemVM));
			this._itemConstructors.Add(typeof(SettlementRebellionMapNotification), typeof(RebellionNotificationItemVM));
			this._itemConstructors.Add(typeof(WarMapNotification), typeof(WarNotificationItemVM));
			this._itemConstructors.Add(typeof(ArmyDispersionMapNotification), typeof(ArmyDispersionItemVM));
			this._itemConstructors.Add(typeof(ChildBornMapNotification), typeof(NewBornNotificationItemVM));
			this._itemConstructors.Add(typeof(DeathMapNotification), typeof(DeathNotificationItemVM));
			this._itemConstructors.Add(typeof(MarriageMapNotification), typeof(MarriageNotificationItemVM));
			this._itemConstructors.Add(typeof(MarriageOfferMapNotification), typeof(MarriageOfferNotificationItemVM));
			this._itemConstructors.Add(typeof(MercenaryOfferMapNotification), typeof(MercenaryOfferMapNotificationItemVM));
			this._itemConstructors.Add(typeof(VassalOfferMapNotification), typeof(VassalOfferMapNotificationItemVM));
			this._itemConstructors.Add(typeof(ArmyCreationMapNotification), typeof(ArmyCreationNotificationItemVM));
			this._itemConstructors.Add(typeof(KingdomDecisionMapNotification), typeof(KingdomVoteNotificationItemVM));
			this._itemConstructors.Add(typeof(SettlementOwnerChangedMapNotification), typeof(SettlementOwnerChangedNotificationItemVM));
			this._itemConstructors.Add(typeof(SettlementUnderSiegeMapNotification), typeof(SettlementUnderSiegeMapNotificationItemVM));
			this._itemConstructors.Add(typeof(AlleyLeaderDiedMapNotification), typeof(AlleyLeaderDiedMapNotificationItemVM));
			this._itemConstructors.Add(typeof(AlleyUnderAttackMapNotification), typeof(AlleyUnderAttackMapNotificationItemVM));
			this._itemConstructors.Add(typeof(EducationMapNotification), typeof(EducationNotificationItemVM));
			this._itemConstructors.Add(typeof(TraitChangedMapNotification), typeof(TraitChangedNotificationItemVM));
			this._itemConstructors.Add(typeof(RansomOfferMapNotification), typeof(RansomNotificationItemVM));
			this._itemConstructors.Add(typeof(PeaceOfferMapNotification), typeof(PeaceOfferNotificationItemVM));
			this._itemConstructors.Add(typeof(PartyLeaderChangeNotification), typeof(PartyLeaderChangeNotificationVM));
			this._itemConstructors.Add(typeof(HeirComeOfAgeMapNotification), typeof(HeirComeOfAgeNotificationItemVM));
			this._itemConstructors.Add(typeof(KingdomDestroyedMapNotification), typeof(KingdomDestroyedNotificationItemVM));
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0001A77E File Offset: 0x0001897E
		public void RegisterMapNotificationType(Type data, Type item)
		{
			this._itemConstructors[data] = item;
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0001A78D File Offset: 0x0001898D
		public override void OnFinalize()
		{
			MBInformationManager.OnAddMapNotice -= this.AddMapNotification;
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0001A7A0 File Offset: 0x000189A0
		public void OnFrameTick(float dt)
		{
			for (int i = 0; i < this.NotificationItems.Count; i++)
			{
				this.NotificationItems[i].ManualRefreshRelevantStatus();
			}
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0001A7D4 File Offset: 0x000189D4
		public void OnMenuModeTick(float dt)
		{
			for (int i = 0; i < this.NotificationItems.Count; i++)
			{
				this.NotificationItems[i].ManualRefreshRelevantStatus();
			}
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0001A808 File Offset: 0x00018A08
		private void RemoveNotificationItem(MapNotificationItemBaseVM item)
		{
			item.OnFinalize();
			this.NotificationItems.Remove(item);
			MBInformationManager.MapNoticeRemoved(item.Data);
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0001A828 File Offset: 0x00018A28
		private void OnNotificationItemFocus(MapNotificationItemBaseVM item)
		{
			this.FocusedNotificationItem = item;
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0001A831 File Offset: 0x00018A31
		private void GoToSettlement(Settlement settlement)
		{
			this._fastMoveCameraToPosition(settlement.Position2D);
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0001A844 File Offset: 0x00018A44
		private void GoToPosOnMap(Vec2 posOnMap)
		{
			this._fastMoveCameraToPosition(posOnMap);
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0001A854 File Offset: 0x00018A54
		public void AddMapNotification(InformationData data)
		{
			MapNotificationItemBaseVM notificationFromData = this.GetNotificationFromData(data);
			if (notificationFromData != null)
			{
				this.NotificationItems.Add(notificationFromData);
				Action<MapNotificationItemBaseVM> receiveNewNotification = this.ReceiveNewNotification;
				if (receiveNewNotification == null)
				{
					return;
				}
				receiveNewNotification(notificationFromData);
			}
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0001A88C File Offset: 0x00018A8C
		public void RemoveAllNotifications()
		{
			foreach (MapNotificationItemBaseVM item in this.NotificationItems.ToList<MapNotificationItemBaseVM>())
			{
				this.RemoveNotificationItem(item);
			}
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0001A8E4 File Offset: 0x00018AE4
		private MapNotificationItemBaseVM GetNotificationFromData(InformationData data)
		{
			Type type = data.GetType();
			MapNotificationItemBaseVM mapNotificationItemBaseVM = null;
			if (this._itemConstructors.ContainsKey(type))
			{
				mapNotificationItemBaseVM = (MapNotificationItemBaseVM)Activator.CreateInstance(this._itemConstructors[type], new object[]
				{
					data
				});
				if (mapNotificationItemBaseVM != null)
				{
					mapNotificationItemBaseVM.OnRemove = new Action<MapNotificationItemBaseVM>(this.RemoveNotificationItem);
					mapNotificationItemBaseVM.OnFocus = new Action<MapNotificationItemBaseVM>(this.OnNotificationItemFocus);
					mapNotificationItemBaseVM.SetNavigationHandler(this._navigationHandler);
					mapNotificationItemBaseVM.SetFastMoveCameraToPosition(this._fastMoveCameraToPosition);
					if (this.RemoveInputKey != null)
					{
						mapNotificationItemBaseVM.RemoveInputKey = this.RemoveInputKey;
					}
				}
			}
			return mapNotificationItemBaseVM;
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0001A97D File Offset: 0x00018B7D
		public void SetRemoveInputKey(HotKey hotKey)
		{
			this.RemoveInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x0001A98C File Offset: 0x00018B8C
		// (set) Token: 0x0600050C RID: 1292 RVA: 0x0001A994 File Offset: 0x00018B94
		[DataSourceProperty]
		public InputKeyItemVM RemoveInputKey
		{
			get
			{
				return this._removeInputKey;
			}
			set
			{
				if (value != this._removeInputKey)
				{
					this._removeInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "RemoveInputKey");
					if (this._removeInputKey != null && this.NotificationItems != null)
					{
						for (int i = 0; i < this.NotificationItems.Count; i++)
						{
							this.NotificationItems[i].RemoveInputKey = this._removeInputKey;
						}
					}
				}
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x0001A9FA File Offset: 0x00018BFA
		// (set) Token: 0x0600050E RID: 1294 RVA: 0x0001AA02 File Offset: 0x00018C02
		[DataSourceProperty]
		public MapNotificationItemBaseVM FocusedNotificationItem
		{
			get
			{
				return this._focusedNotificationItem;
			}
			set
			{
				if (value != this._focusedNotificationItem)
				{
					this._focusedNotificationItem = value;
					base.OnPropertyChangedWithValue<MapNotificationItemBaseVM>(value, "FocusedNotificationItem");
				}
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x0600050F RID: 1295 RVA: 0x0001AA20 File Offset: 0x00018C20
		// (set) Token: 0x06000510 RID: 1296 RVA: 0x0001AA28 File Offset: 0x00018C28
		[DataSourceProperty]
		public MBBindingList<MapNotificationItemBaseVM> NotificationItems
		{
			get
			{
				return this._notificationItems;
			}
			set
			{
				if (value != this._notificationItems)
				{
					this._notificationItems = value;
					base.OnPropertyChangedWithValue<MBBindingList<MapNotificationItemBaseVM>>(value, "NotificationItems");
				}
			}
		}

		// Token: 0x04000221 RID: 545
		private INavigationHandler _navigationHandler;

		// Token: 0x04000222 RID: 546
		private Action<Vec2> _fastMoveCameraToPosition;

		// Token: 0x04000223 RID: 547
		private Dictionary<Type, Type> _itemConstructors = new Dictionary<Type, Type>();

		// Token: 0x04000224 RID: 548
		private InputKeyItemVM _removeInputKey;

		// Token: 0x04000225 RID: 549
		private MapNotificationItemBaseVM _focusedNotificationItem;

		// Token: 0x04000226 RID: 550
		private MBBindingList<MapNotificationItemBaseVM> _notificationItems;
	}
}
