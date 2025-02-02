using System;
using SandBox.View.Menu;
using TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.TournamentLeaderboard;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.ScreenSystem;

namespace SandBox.GauntletUI.Menu
{
	// Token: 0x0200001E RID: 30
	[OverrideView(typeof(MenuTournamentLeaderboardView))]
	public class GauntletMenuTournamentLeaderboardView : MenuView
	{
		// Token: 0x06000131 RID: 305 RVA: 0x00009908 File Offset: 0x00007B08
		protected override void OnInitialize()
		{
			base.OnInitialize();
			this._dataSource = new TournamentLeaderboardVM
			{
				IsEnabled = true
			};
			base.Layer = new GauntletLayer(206, "GauntletLayer", false)
			{
				Name = "MenuTournamentLeaderboard"
			};
			this._layerAsGauntletLayer = (base.Layer as GauntletLayer);
			base.Layer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
			base.Layer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericPanelGameKeyCategory"));
			this._dataSource.SetDoneInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Confirm"));
			this._movie = this._layerAsGauntletLayer.LoadMovie("GameMenuTournamentLeaderboard", this._dataSource);
			base.Layer.IsFocusLayer = true;
			ScreenManager.TrySetFocus(base.Layer);
			base.MenuViewContext.AddLayer(base.Layer);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000099F0 File Offset: 0x00007BF0
		protected override void OnFinalize()
		{
			base.Layer.IsFocusLayer = false;
			ScreenManager.TryLoseFocus(base.Layer);
			this._dataSource.OnFinalize();
			this._dataSource = null;
			this._layerAsGauntletLayer.ReleaseMovie(this._movie);
			base.MenuViewContext.RemoveLayer(base.Layer);
			this._movie = null;
			base.Layer = null;
			base.OnFinalize();
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00009A5C File Offset: 0x00007C5C
		protected override void OnFrameTick(float dt)
		{
			base.OnFrameTick(dt);
			if (base.Layer.Input.IsHotKeyReleased("Exit") || base.Layer.Input.IsHotKeyReleased("Confirm"))
			{
				UISoundsHelper.PlayUISound("event:/ui/default");
				this._dataSource.IsEnabled = false;
			}
			if (!this._dataSource.IsEnabled)
			{
				base.MenuViewContext.CloseTournamentLeaderboard();
			}
		}

		// Token: 0x0400007F RID: 127
		private GauntletLayer _layerAsGauntletLayer;

		// Token: 0x04000080 RID: 128
		private TournamentLeaderboardVM _dataSource;

		// Token: 0x04000081 RID: 129
		private IGauntletMovie _movie;
	}
}
