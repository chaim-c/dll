using System;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Engine;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.GauntletUI.SceneNotification;
using TaleWorlds.ScreenSystem;

namespace TaleWorlds.MountAndBlade.GauntletUI
{
	// Token: 0x02000008 RID: 8
	public class GauntletGameNotification : GlobalLayer
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00003D34 File Offset: 0x00001F34
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00003D3B File Offset: 0x00001F3B
		public static GauntletGameNotification Current { get; private set; }

		// Token: 0x0600003A RID: 58 RVA: 0x00003D44 File Offset: 0x00001F44
		private GauntletGameNotification()
		{
			this._dataSource = new GameNotificationVM();
			this._dataSource.ReceiveNewNotification += this.OnReceiveNewNotification;
			this._layer = new GauntletLayer(4007, "GauntletLayer", false);
			this.LoadMovie(false);
			base.Layer = this._layer;
			this._layer.InputRestrictions.SetInputRestrictions(false, InputUsageMask.Mouse);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003DB4 File Offset: 0x00001FB4
		private void OnReceiveNewNotification(GameNotificationItemVM notification)
		{
			if (!string.IsNullOrEmpty(notification.NotificationSoundId))
			{
				SoundEvent.PlaySound2D(notification.NotificationSoundId);
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003DCF File Offset: 0x00001FCF
		public static void Initialize()
		{
			if (GauntletGameNotification.Current == null)
			{
				GauntletGameNotification.Current = new GauntletGameNotification();
				ScreenManager.AddGlobalLayer(GauntletGameNotification.Current, false);
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003DED File Offset: 0x00001FED
		public static void OnFinalize()
		{
			GauntletGameNotification gauntletGameNotification = GauntletGameNotification.Current;
			if (gauntletGameNotification == null)
			{
				return;
			}
			GameNotificationVM dataSource = gauntletGameNotification._dataSource;
			if (dataSource == null)
			{
				return;
			}
			dataSource.ClearNotifications();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003E08 File Offset: 0x00002008
		public void LoadMovie(bool forMultiplayer)
		{
			if (this._movie != null)
			{
				this._layer.ReleaseMovie(this._movie);
			}
			if (forMultiplayer)
			{
				this._movie = this._layer.LoadMovie("MultiplayerGameNotificationUI", this._dataSource);
				return;
			}
			this._movie = this._layer.LoadMovie("GameNotificationUI", this._dataSource);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003E6C File Offset: 0x0000206C
		protected override void OnTick(float dt)
		{
			base.OnTick(dt);
			bool isLoadingWindowActive = LoadingWindow.IsLoadingWindowActive;
			bool isActive = GauntletSceneNotification.Current.IsActive;
			if (isActive != this._isSuspended)
			{
				ScreenManager.SetSuspendLayer(GauntletGameNotification.Current._layer, isActive);
				this._isSuspended = isActive;
			}
			if (isLoadingWindowActive)
			{
				dt = 0f;
			}
			if (!this._isSuspended)
			{
				this._dataSource.Tick(dt);
			}
		}

		// Token: 0x04000032 RID: 50
		private GameNotificationVM _dataSource;

		// Token: 0x04000033 RID: 51
		private GauntletLayer _layer;

		// Token: 0x04000034 RID: 52
		private IGauntletMovie _movie;

		// Token: 0x04000036 RID: 54
		private bool _isSuspended;
	}
}
