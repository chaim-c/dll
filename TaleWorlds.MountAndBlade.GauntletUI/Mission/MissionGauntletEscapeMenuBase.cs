using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View.MissionViews;
using TaleWorlds.MountAndBlade.ViewModelCollection.EscapeMenu;
using TaleWorlds.ScreenSystem;

namespace TaleWorlds.MountAndBlade.GauntletUI.Mission
{
	// Token: 0x02000026 RID: 38
	public abstract class MissionGauntletEscapeMenuBase : MissionEscapeMenuView
	{
		// Token: 0x06000189 RID: 393 RVA: 0x0000992A File Offset: 0x00007B2A
		protected MissionGauntletEscapeMenuBase(string viewFile)
		{
			base.OnMissionScreenInitialize();
			this._viewFile = viewFile;
			this.ViewOrderPriority = 50;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00009947 File Offset: 0x00007B47
		protected virtual List<EscapeMenuItemVM> GetEscapeMenuItems()
		{
			return null;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000994A File Offset: 0x00007B4A
		public override void OnMissionScreenFinalize()
		{
			this.DataSource.OnFinalize();
			this.DataSource = null;
			this._gauntletLayer = null;
			this._movie = null;
			base.OnMissionScreenFinalize();
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00009972 File Offset: 0x00007B72
		public override bool OnEscape()
		{
			if (!this._isRenderingStarted)
			{
				return false;
			}
			if (!base.IsActive)
			{
				this.DataSource.RefreshItems(this.GetEscapeMenuItems());
			}
			return this.OnEscapeMenuToggled(!base.IsActive);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000099A8 File Offset: 0x00007BA8
		protected bool OnEscapeMenuToggled(bool isOpened)
		{
			if (base.IsActive == isOpened)
			{
				return false;
			}
			base.IsActive = isOpened;
			if (isOpened)
			{
				this.DataSource.RefreshValues();
				if (!GameNetwork.IsMultiplayer)
				{
					MBCommon.PauseGameEngine();
					Game.Current.GameStateManager.RegisterActiveStateDisableRequest(this);
				}
				this._gauntletLayer = new GauntletLayer(this.ViewOrderPriority, "GauntletLayer", false);
				this._gauntletLayer.IsFocusLayer = true;
				this._gauntletLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
				this._gauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericPanelGameKeyCategory"));
				this._movie = this._gauntletLayer.LoadMovie(this._viewFile, this.DataSource);
				base.MissionScreen.AddLayer(this._gauntletLayer);
				ScreenManager.TrySetFocus(this._gauntletLayer);
			}
			else
			{
				if (!GameNetwork.IsMultiplayer)
				{
					MBCommon.UnPauseGameEngine();
					Game.Current.GameStateManager.UnregisterActiveStateDisableRequest(this);
				}
				this._gauntletLayer.InputRestrictions.ResetInputRestrictions();
				base.MissionScreen.RemoveLayer(this._gauntletLayer);
				this._movie = null;
				this._gauntletLayer = null;
			}
			return true;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00009ACC File Offset: 0x00007CCC
		public override void OnMissionScreenTick(float dt)
		{
			base.OnMissionScreenTick(dt);
			if (base.IsActive && (this._gauntletLayer.Input.IsHotKeyReleased("ToggleEscapeMenu") || this._gauntletLayer.Input.IsHotKeyReleased("Exit")))
			{
				this.OnEscapeMenuToggled(false);
			}
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00009B1E File Offset: 0x00007D1E
		public override void OnSceneRenderingStarted()
		{
			base.OnSceneRenderingStarted();
			this._isRenderingStarted = true;
		}

		// Token: 0x040000D8 RID: 216
		protected EscapeMenuVM DataSource;

		// Token: 0x040000D9 RID: 217
		private GauntletLayer _gauntletLayer;

		// Token: 0x040000DA RID: 218
		private IGauntletMovie _movie;

		// Token: 0x040000DB RID: 219
		private string _viewFile;

		// Token: 0x040000DC RID: 220
		private bool _isRenderingStarted;
	}
}
