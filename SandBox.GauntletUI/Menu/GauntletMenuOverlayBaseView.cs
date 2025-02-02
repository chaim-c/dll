using System;
using SandBox.View.Map;
using SandBox.View.Menu;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Overlay;
using TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.Overlay;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.ScreenSystem;

namespace SandBox.GauntletUI.Menu
{
	// Token: 0x0200001C RID: 28
	[OverrideView(typeof(MenuOverlayBaseView))]
	public class GauntletMenuOverlayBaseView : MenuView
	{
		// Token: 0x06000125 RID: 293 RVA: 0x000092C4 File Offset: 0x000074C4
		protected override void OnInitialize()
		{
			GameOverlays.MenuOverlayType menuOverlayType = Campaign.Current.GameMenuManager.GetMenuOverlayType(base.MenuContext);
			this._overlayDataSource = GameMenuOverlay.GetOverlay(menuOverlayType);
			base.Layer = new GauntletLayer(200, "GauntletLayer", false);
			this._layerAsGauntletLayer = (base.Layer as GauntletLayer);
			this._layerAsGauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericPanelGameKeyCategory"));
			base.Layer.InputRestrictions.SetInputRestrictions(false, InputUsageMask.All);
			base.MenuViewContext.AddLayer(base.Layer);
			if (this._overlayDataSource is EncounterMenuOverlayVM)
			{
				this._layerAsGauntletLayer.LoadMovie("EncounterOverlay", this._overlayDataSource);
			}
			else if (this._overlayDataSource is SettlementMenuOverlayVM)
			{
				this._layerAsGauntletLayer.LoadMovie("SettlementOverlay", this._overlayDataSource);
			}
			else if (this._overlayDataSource is ArmyMenuOverlayVM)
			{
				Debug.FailedAssert("Trying to open army overlay in menu. Should be opened in map overlay", "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox.GauntletUI\\Menu\\GauntletMenuOverlayBaseView.cs", "OnInitialize", 47);
			}
			else
			{
				Debug.FailedAssert("Game menu overlay not supported in gauntlet overlay", "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox.GauntletUI\\Menu\\GauntletMenuOverlayBaseView.cs", "OnInitialize", 51);
			}
			base.OnInitialize();
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000093E4 File Offset: 0x000075E4
		protected override void OnFrameTick(float dt)
		{
			base.OnFrameTick(dt);
			GameMenuOverlay overlayDataSource = this._overlayDataSource;
			if (overlayDataSource != null)
			{
				overlayDataSource.OnFrameTick(dt);
			}
			if (ScreenManager.TopScreen is MapScreen && this._overlayDataSource != null)
			{
				GameMenuOverlay overlayDataSource2 = this._overlayDataSource;
				MapScreen mapScreen = ScreenManager.TopScreen as MapScreen;
				overlayDataSource2.IsInfoBarExtended = (mapScreen != null && mapScreen.IsBarExtended);
			}
			if (!this._isContextMenuEnabled && this._overlayDataSource.IsContextMenuEnabled)
			{
				this._isContextMenuEnabled = true;
				base.Layer.IsFocusLayer = true;
				ScreenManager.TrySetFocus(base.Layer);
			}
			else if (this._isContextMenuEnabled && !this._overlayDataSource.IsContextMenuEnabled)
			{
				this._isContextMenuEnabled = false;
				base.Layer.IsFocusLayer = false;
				ScreenManager.TryLoseFocus(base.Layer);
			}
			if (this._isContextMenuEnabled && base.Layer.Input.IsHotKeyReleased("Exit"))
			{
				UISoundsHelper.PlayUISound("event:/ui/default");
				this._overlayDataSource.IsContextMenuEnabled = false;
			}
		}

		// Token: 0x06000127 RID: 295 RVA: 0x000094DC File Offset: 0x000076DC
		protected override void OnHourlyTick()
		{
			base.OnHourlyTick();
			GameMenuOverlay overlayDataSource = this._overlayDataSource;
			if (overlayDataSource == null)
			{
				return;
			}
			overlayDataSource.Refresh();
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000094F4 File Offset: 0x000076F4
		protected override void OnOverlayTypeChange(GameOverlays.MenuOverlayType newType)
		{
			base.OnOverlayTypeChange(newType);
			GameMenuOverlay overlayDataSource = this._overlayDataSource;
			if (overlayDataSource == null)
			{
				return;
			}
			overlayDataSource.UpdateOverlayType(newType);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000950E File Offset: 0x0000770E
		protected override void OnActivate()
		{
			base.OnActivate();
			GameMenuOverlay overlayDataSource = this._overlayDataSource;
			if (overlayDataSource == null)
			{
				return;
			}
			overlayDataSource.Refresh();
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00009526 File Offset: 0x00007726
		protected override void OnFinalize()
		{
			base.MenuViewContext.RemoveLayer(base.Layer);
			this._overlayDataSource.OnFinalize();
			this._overlayDataSource = null;
			base.Layer = null;
			this._layerAsGauntletLayer = null;
			base.OnFinalize();
		}

		// Token: 0x04000079 RID: 121
		private GameMenuOverlay _overlayDataSource;

		// Token: 0x0400007A RID: 122
		private GauntletLayer _layerAsGauntletLayer;

		// Token: 0x0400007B RID: 123
		private bool _isContextMenuEnabled;
	}
}
