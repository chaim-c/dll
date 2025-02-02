using System;
using TaleWorlds.Engine;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.Engine.Options;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.Screens;
using TaleWorlds.MountAndBlade.ViewModelCollection.GameOptions;
using TaleWorlds.MountAndBlade.ViewModelCollection.InitialMenu;
using TaleWorlds.ScreenSystem;

namespace TaleWorlds.MountAndBlade.GauntletUI
{
	// Token: 0x0200000C RID: 12
	[GameStateScreen(typeof(InitialState))]
	public class GauntletInitialScreen : MBInitialScreenBase
	{
		// Token: 0x06000055 RID: 85 RVA: 0x00004402 File Offset: 0x00002602
		public GauntletInitialScreen(InitialState initialState) : base(initialState)
		{
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000440C File Offset: 0x0000260C
		protected override void OnInitialize()
		{
			base.OnInitialize();
			this._dataSource = new InitialMenuVM(base._state);
			this._gauntletLayer = new GauntletLayer(1, "GauntletLayer", false);
			this._gauntletLayer.LoadMovie("InitialScreen", this._dataSource);
			this._gauntletLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.Mouse);
			this._gauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericPanelGameKeyCategory"));
			base.AddLayer(this._gauntletLayer);
			this._gauntletLayer.IsFocusLayer = true;
			ScreenManager.TrySetFocus(this._gauntletLayer);
			if (NativeOptions.GetConfig(NativeOptions.NativeOptionsType.BrightnessCalibrated) < 2f)
			{
				this._brightnessOptionDataSource = new BrightnessOptionVM(new Action<bool>(this.OnCloseBrightness))
				{
					Visible = true
				};
				this._gauntletBrightnessLayer = new GauntletLayer(2, "GauntletLayer", false);
				this._gauntletBrightnessLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.Mouse);
				this._brightnessOptionMovie = this._gauntletBrightnessLayer.LoadMovie("BrightnessOption", this._brightnessOptionDataSource);
				base.AddLayer(this._gauntletBrightnessLayer);
			}
			MouseManager.ShowCursor(false);
			MouseManager.ShowCursor(true);
			GauntletGameNotification gauntletGameNotification = GauntletGameNotification.Current;
			if (gauntletGameNotification != null)
			{
				gauntletGameNotification.LoadMovie(false);
			}
			GauntletChatLogView gauntletChatLogView = GauntletChatLogView.Current;
			if (gauntletChatLogView != null)
			{
				gauntletChatLogView.LoadMovie(false);
			}
			InformationManager.ClearAllMessages();
			base._state.OnGameContentUpdated += this.OnGameContentUpdated;
			this.SetGainNavigationAfterFrames(3);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00004574 File Offset: 0x00002774
		protected override void OnInitialScreenTick(float dt)
		{
			base.OnInitialScreenTick(dt);
			if (this._gauntletLayer.Input.IsHotKeyReleased("Exit"))
			{
				BrightnessOptionVM brightnessOptionDataSource = this._brightnessOptionDataSource;
				if (brightnessOptionDataSource != null && brightnessOptionDataSource.Visible)
				{
					UISoundsHelper.PlayUISound("event:/ui/default");
					this._brightnessOptionDataSource.ExecuteCancel();
					return;
				}
				ExposureOptionVM exposureOptionDataSource = this._exposureOptionDataSource;
				if (exposureOptionDataSource != null && exposureOptionDataSource.Visible)
				{
					UISoundsHelper.PlayUISound("event:/ui/default");
					this._exposureOptionDataSource.ExecuteCancel();
					return;
				}
			}
			else if (this._gauntletLayer.Input.IsHotKeyReleased("Confirm"))
			{
				BrightnessOptionVM brightnessOptionDataSource2 = this._brightnessOptionDataSource;
				if (brightnessOptionDataSource2 != null && brightnessOptionDataSource2.Visible)
				{
					UISoundsHelper.PlayUISound("event:/ui/default");
					this._brightnessOptionDataSource.ExecuteConfirm();
					return;
				}
				ExposureOptionVM exposureOptionDataSource2 = this._exposureOptionDataSource;
				if (exposureOptionDataSource2 != null && exposureOptionDataSource2.Visible)
				{
					UISoundsHelper.PlayUISound("event:/ui/default");
					this._exposureOptionDataSource.ExecuteConfirm();
				}
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00004660 File Offset: 0x00002860
		protected override void OnActivate()
		{
			base.OnActivate();
			InitialMenuVM dataSource = this._dataSource;
			if (dataSource != null)
			{
				dataSource.RefreshMenuOptions();
			}
			this.SetGainNavigationAfterFrames(3);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00004680 File Offset: 0x00002880
		private void SetGainNavigationAfterFrames(int frameCount)
		{
			this._gauntletLayer.UIContext.GamepadNavigation.GainNavigationAfterFrames(frameCount, delegate
			{
				BrightnessOptionVM brightnessOptionDataSource = this._brightnessOptionDataSource;
				if (brightnessOptionDataSource == null || !brightnessOptionDataSource.Visible)
				{
					ExposureOptionVM exposureOptionDataSource = this._exposureOptionDataSource;
					return exposureOptionDataSource == null || !exposureOptionDataSource.Visible;
				}
				return false;
			});
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000046A4 File Offset: 0x000028A4
		private void OnGameContentUpdated()
		{
			InitialMenuVM dataSource = this._dataSource;
			if (dataSource == null)
			{
				return;
			}
			dataSource.RefreshMenuOptions();
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000046B6 File Offset: 0x000028B6
		private void OnCloseBrightness(bool isConfirm)
		{
			this._gauntletBrightnessLayer.ReleaseMovie(this._brightnessOptionMovie);
			base.RemoveLayer(this._gauntletBrightnessLayer);
			this._brightnessOptionDataSource = null;
			this._gauntletBrightnessLayer = null;
			NativeOptions.SaveConfig();
			this.OpenExposureControl();
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000046F0 File Offset: 0x000028F0
		private void OpenExposureControl()
		{
			this._exposureOptionDataSource = new ExposureOptionVM(new Action<bool>(this.OnCloseExposureControl))
			{
				Visible = true
			};
			this._gauntletExposureLayer = new GauntletLayer(2, "GauntletLayer", false);
			this._gauntletExposureLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.Mouse);
			this._exposureOptionMovie = this._gauntletExposureLayer.LoadMovie("ExposureOption", this._exposureOptionDataSource);
			base.AddLayer(this._gauntletExposureLayer);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00004767 File Offset: 0x00002967
		private void OnCloseExposureControl(bool isConfirm)
		{
			this._gauntletExposureLayer.ReleaseMovie(this._exposureOptionMovie);
			base.RemoveLayer(this._gauntletExposureLayer);
			this._exposureOptionDataSource = null;
			this._gauntletExposureLayer = null;
			NativeOptions.SaveConfig();
		}

		// Token: 0x0600005E RID: 94 RVA: 0x0000479C File Offset: 0x0000299C
		protected override void OnFinalize()
		{
			base.OnFinalize();
			if (base._state != null)
			{
				base._state.OnGameContentUpdated -= this.OnGameContentUpdated;
			}
			if (this._gauntletLayer != null)
			{
				base.RemoveLayer(this._gauntletLayer);
			}
			InitialMenuVM dataSource = this._dataSource;
			if (dataSource != null)
			{
				dataSource.OnFinalize();
			}
			this._dataSource = null;
			this._gauntletLayer = null;
		}

		// Token: 0x04000044 RID: 68
		private GauntletLayer _gauntletLayer;

		// Token: 0x04000045 RID: 69
		private GauntletLayer _gauntletBrightnessLayer;

		// Token: 0x04000046 RID: 70
		private GauntletLayer _gauntletExposureLayer;

		// Token: 0x04000047 RID: 71
		private InitialMenuVM _dataSource;

		// Token: 0x04000048 RID: 72
		private BrightnessOptionVM _brightnessOptionDataSource;

		// Token: 0x04000049 RID: 73
		private ExposureOptionVM _exposureOptionDataSource;

		// Token: 0x0400004A RID: 74
		private IGauntletMovie _brightnessOptionMovie;

		// Token: 0x0400004B RID: 75
		private IGauntletMovie _exposureOptionMovie;
	}
}
