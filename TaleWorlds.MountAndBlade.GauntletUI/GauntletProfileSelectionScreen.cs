using System;
using TaleWorlds.Engine;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View.Screens;
using TaleWorlds.MountAndBlade.ViewModelCollection.ProfileSelection;
using TaleWorlds.PlatformService;
using TaleWorlds.ScreenSystem;

namespace TaleWorlds.MountAndBlade.GauntletUI
{
	// Token: 0x0200000E RID: 14
	[GameStateScreen(typeof(ProfileSelectionState))]
	public class GauntletProfileSelectionScreen : MBProfileSelectionScreenBase
	{
		// Token: 0x06000067 RID: 103 RVA: 0x00004F1A File Offset: 0x0000311A
		public GauntletProfileSelectionScreen(ProfileSelectionState state) : base(state)
		{
			this._state = state;
			this._state.OnProfileSelection += this.OnProfileSelection;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00004F41 File Offset: 0x00003141
		private void OnProfileSelection()
		{
			ProfileSelectionVM dataSource = this._dataSource;
			if (dataSource == null)
			{
				return;
			}
			dataSource.OnActivate(this._state.IsDirectPlayPossible);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00004F60 File Offset: 0x00003160
		protected override void OnInitialize()
		{
			base.OnInitialize();
			this._gauntletLayer = new GauntletLayer(1, "GauntletLayer", false);
			this._dataSource = new ProfileSelectionVM(this._state.IsDirectPlayPossible);
			ProfileSelectionVM dataSource = this._dataSource;
			if (dataSource != null)
			{
				dataSource.OnActivate(this._state.IsDirectPlayPossible);
			}
			this._gauntletLayer.LoadMovie("ProfileSelectionScreen", this._dataSource);
			this._gauntletLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
			this._gauntletLayer.IsFocusLayer = true;
			this._gauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericPanelGameKeyCategory"));
			ScreenManager.TrySetFocus(this._gauntletLayer);
			base.AddLayer(this._gauntletLayer);
			MouseManager.ShowCursor(false);
			MouseManager.ShowCursor(true);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00005029 File Offset: 0x00003229
		protected override void OnActivate()
		{
			base.OnActivate();
			ProfileSelectionVM dataSource = this._dataSource;
			if (dataSource == null)
			{
				return;
			}
			dataSource.OnActivate(this._state.IsDirectPlayPossible);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x0000504C File Offset: 0x0000324C
		protected override void OnFinalize()
		{
			base.OnFinalize();
			this._state.OnProfileSelection -= this.OnProfileSelection;
			this._gauntletLayer.IsFocusLayer = false;
			ScreenManager.TryLoseFocus(this._gauntletLayer);
			base.RemoveLayer(this._gauntletLayer);
			this._gauntletLayer = null;
			this._dataSource.OnFinalize();
			this._dataSource = null;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000050B4 File Offset: 0x000032B4
		protected override void OnProfileSelectionTick(float dt)
		{
			base.OnProfileSelectionTick(dt);
			if (!this._state.IsDirectPlayPossible || !this._gauntletLayer.Input.IsHotKeyReleased("Play"))
			{
				if (this._gauntletLayer.Input.IsHotKeyReleased("SelectProfile"))
				{
					base.OnActivateProfileSelection();
				}
				return;
			}
			if (PlatformServices.Instance.UserLoggedIn)
			{
				this._state.StartGame();
				return;
			}
			base.OnActivateProfileSelection();
		}

		// Token: 0x04000053 RID: 83
		private GauntletLayer _gauntletLayer;

		// Token: 0x04000054 RID: 84
		private ProfileSelectionVM _dataSource;

		// Token: 0x04000055 RID: 85
		private ProfileSelectionState _state;
	}
}
