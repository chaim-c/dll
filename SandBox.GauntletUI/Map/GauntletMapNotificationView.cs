using System;
using SandBox.View.Map;
using TaleWorlds.CampaignSystem.ViewModelCollection.Map;
using TaleWorlds.CampaignSystem.ViewModelCollection.Map.MapNotificationTypes;
using TaleWorlds.Engine;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.ScreenSystem;

namespace SandBox.GauntletUI.Map
{
	// Token: 0x0200002F RID: 47
	[OverrideView(typeof(MapNotificationView))]
	public class GauntletMapNotificationView : MapNotificationView
	{
		// Token: 0x060001B7 RID: 439 RVA: 0x0000CA5C File Offset: 0x0000AC5C
		protected override void CreateLayout()
		{
			base.CreateLayout();
			this._mapNavigationHandler = new MapNavigationHandler();
			this._dataSource = new MapNotificationVM(this._mapNavigationHandler, new Action<Vec2>(base.MapScreen.FastMoveCameraToPosition));
			this._dataSource.ReceiveNewNotification += this.OnReceiveNewNotification;
			this._dataSource.SetRemoveInputKey(HotKeyManager.GetCategory("MapNotificationHotKeyCategory").GetHotKey("RemoveNotification"));
			base.Layer = new GauntletLayer(100, "GauntletLayer", false);
			this._layerAsGauntletLayer = (base.Layer as GauntletLayer);
			base.Layer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("MapNotificationHotKeyCategory"));
			base.Layer.InputRestrictions.SetInputRestrictions(false, InputUsageMask.MouseButtons | InputUsageMask.Keyboardkeys);
			this._movie = this._layerAsGauntletLayer.LoadMovie("MapNotificationUI", this._dataSource);
			base.MapScreen.AddLayer(base.Layer);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000CB4F File Offset: 0x0000AD4F
		private void OnReceiveNewNotification(MapNotificationItemBaseVM newNotification)
		{
			if (!string.IsNullOrEmpty(newNotification.SoundId))
			{
				SoundEvent.PlaySound2D(newNotification.SoundId);
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000CB6A File Offset: 0x0000AD6A
		public override void RegisterMapNotificationType(Type data, Type item)
		{
			this._dataSource.RegisterMapNotificationType(data, item);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000CB79 File Offset: 0x0000AD79
		protected override void OnFinalize()
		{
			base.OnFinalize();
			this._dataSource.OnFinalize();
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000CB8C File Offset: 0x0000AD8C
		protected override void OnFrameTick(float dt)
		{
			base.OnFrameTick(dt);
			this._dataSource.OnFrameTick(dt);
			this.HandleInput();
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000CBA7 File Offset: 0x0000ADA7
		protected override void OnMenuModeTick(float dt)
		{
			base.OnMenuModeTick(dt);
			this._dataSource.OnMenuModeTick(dt);
			this.HandleInput();
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000CBC4 File Offset: 0x0000ADC4
		private void HandleInput()
		{
			if (!this._isHoveringOnNotification && this._dataSource.FocusedNotificationItem != null)
			{
				this._isHoveringOnNotification = true;
				base.Layer.IsFocusLayer = true;
				ScreenManager.TrySetFocus(base.Layer);
			}
			else if (this._isHoveringOnNotification && this._dataSource.FocusedNotificationItem == null)
			{
				this._isHoveringOnNotification = false;
				base.Layer.IsFocusLayer = false;
				ScreenManager.TryLoseFocus(base.Layer);
			}
			if (this._isHoveringOnNotification && this._dataSource.FocusedNotificationItem != null && base.Layer.Input.IsHotKeyReleased("RemoveNotification") && !this._dataSource.FocusedNotificationItem.ForceInspection)
			{
				SoundEvent.PlaySound2D("event:/ui/default");
				this._dataSource.FocusedNotificationItem.ExecuteRemove();
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000CC92 File Offset: 0x0000AE92
		public override void ResetNotifications()
		{
			base.ResetNotifications();
			MapNotificationVM dataSource = this._dataSource;
			if (dataSource == null)
			{
				return;
			}
			dataSource.RemoveAllNotifications();
		}

		// Token: 0x040000D6 RID: 214
		private MapNotificationVM _dataSource;

		// Token: 0x040000D7 RID: 215
		private IGauntletMovie _movie;

		// Token: 0x040000D8 RID: 216
		private MapNavigationHandler _mapNavigationHandler;

		// Token: 0x040000D9 RID: 217
		private GauntletLayer _layerAsGauntletLayer;

		// Token: 0x040000DA RID: 218
		private bool _isHoveringOnNotification;

		// Token: 0x040000DB RID: 219
		private const string _defaultSound = "event:/ui/default";
	}
}
