using System;
using SandBox.View.Map;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Overlay;
using TaleWorlds.CampaignSystem.ViewModelCollection.ArmyManagement;
using TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.Overlay;
using TaleWorlds.Core;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.ScreenSystem;
using TaleWorlds.TwoDimension;

namespace SandBox.GauntletUI.Map
{
	// Token: 0x02000030 RID: 48
	[OverrideView(typeof(MapOverlayView))]
	public class GauntletMapOverlayView : MapView
	{
		// Token: 0x060001BF RID: 447 RVA: 0x0000CCAA File Offset: 0x0000AEAA
		public GauntletMapOverlayView(GameOverlays.MapOverlayType type)
		{
			this._type = type;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000CCBC File Offset: 0x0000AEBC
		protected override void CreateLayout()
		{
			base.CreateLayout();
			this._overlayDataSource = this.GetOverlay(this._type);
			base.Layer = new GauntletLayer(201, "GauntletLayer", false);
			this._layerAsGauntletLayer = (base.Layer as GauntletLayer);
			base.Layer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericPanelGameKeyCategory"));
			base.Layer.InputRestrictions.SetInputRestrictions(false, InputUsageMask.All);
			GameOverlays.MapOverlayType type = this._type;
			if (type == GameOverlays.MapOverlayType.Army)
			{
				this._movie = this._layerAsGauntletLayer.LoadMovie("ArmyOverlay", this._overlayDataSource);
				(this._overlayDataSource as ArmyMenuOverlayVM).OpenArmyManagement = new Action(this.OpenArmyManagement);
			}
			else
			{
				Debug.FailedAssert("This kind of overlay not supported in gauntlet", "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox.GauntletUI\\Map\\GauntletMapOverlayView.cs", "CreateLayout", 63);
			}
			base.MapScreen.AddLayer(base.Layer);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000CDA0 File Offset: 0x0000AFA0
		public GameMenuOverlay GetOverlay(GameOverlays.MapOverlayType mapOverlayType)
		{
			if (mapOverlayType == GameOverlays.MapOverlayType.Army)
			{
				return new ArmyMenuOverlayVM();
			}
			Debug.FailedAssert("Game menu overlay: " + mapOverlayType.ToString() + " could not be found", "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox.GauntletUI\\Map\\GauntletMapOverlayView.cs", "GetOverlay", 76);
			return null;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000CDDA File Offset: 0x0000AFDA
		protected override void OnArmyLeft()
		{
			base.MapScreen.RemoveArmyOverlay();
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000CDE8 File Offset: 0x0000AFE8
		protected override void OnFinalize()
		{
			if (this._armyManagementLayer != null)
			{
				this.CloseArmyManagement();
			}
			this._layerAsGauntletLayer.ReleaseMovie(this._movie);
			if (this._gauntletArmyManagementMovie != null)
			{
				this._layerAsGauntletLayer.ReleaseMovie(this._gauntletArmyManagementMovie);
			}
			base.MapScreen.RemoveLayer(base.Layer);
			this._movie = null;
			this._overlayDataSource = null;
			base.Layer = null;
			this._layerAsGauntletLayer = null;
			SpriteCategory armyManagementCategory = this._armyManagementCategory;
			if (armyManagementCategory != null)
			{
				armyManagementCategory.Unload();
			}
			base.OnFinalize();
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000CE71 File Offset: 0x0000B071
		protected override void OnHourlyTick()
		{
			base.OnHourlyTick();
			GameMenuOverlay overlayDataSource = this._overlayDataSource;
			if (overlayDataSource == null)
			{
				return;
			}
			overlayDataSource.HourlyTick();
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000CE8C File Offset: 0x0000B08C
		protected override void OnFrameTick(float dt)
		{
			base.OnFrameTick(dt);
			MapScreen mapScreen;
			if ((mapScreen = (ScreenManager.TopScreen as MapScreen)) != null && this._overlayDataSource != null)
			{
				this._overlayDataSource.IsInfoBarExtended = mapScreen.IsBarExtended;
			}
			GameMenuOverlay overlayDataSource = this._overlayDataSource;
			if (overlayDataSource == null)
			{
				return;
			}
			overlayDataSource.OnFrameTick(dt);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000CED8 File Offset: 0x0000B0D8
		protected override bool IsEscaped()
		{
			if (this._armyManagementDatasource != null)
			{
				this._armyManagementDatasource.ExecuteCancel();
				return true;
			}
			return false;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000CEF0 File Offset: 0x0000B0F0
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

		// Token: 0x060001C8 RID: 456 RVA: 0x0000CF08 File Offset: 0x0000B108
		protected override void OnResume()
		{
			base.OnResume();
			GameMenuOverlay overlayDataSource = this._overlayDataSource;
			if (overlayDataSource == null)
			{
				return;
			}
			overlayDataSource.Refresh();
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000CF20 File Offset: 0x0000B120
		protected override void OnMapScreenUpdate(float dt)
		{
			base.OnMapScreenUpdate(dt);
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
			this.HandleArmyManagementInput();
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000CFD8 File Offset: 0x0000B1D8
		protected override void OnMenuModeTick(float dt)
		{
			base.OnMenuModeTick(dt);
			MapScreen mapScreen;
			if ((mapScreen = (ScreenManager.TopScreen as MapScreen)) != null && this._overlayDataSource != null)
			{
				this._overlayDataSource.IsInfoBarExtended = mapScreen.IsBarExtended;
			}
			GameMenuOverlay overlayDataSource = this._overlayDataSource;
			if (overlayDataSource == null)
			{
				return;
			}
			overlayDataSource.OnFrameTick(dt);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000D024 File Offset: 0x0000B224
		private void OpenArmyManagement()
		{
			this._armyManagementDatasource = new ArmyManagementVM(new Action(this.CloseArmyManagement));
			this._armyManagementDatasource.SetCancelInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Exit"));
			this._armyManagementDatasource.SetDoneInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Confirm"));
			this._armyManagementDatasource.SetResetInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Reset"));
			this._armyManagementDatasource.SetRemoveInputKey(HotKeyManager.GetCategory("ArmyManagementHotkeyCategory").GetHotKey("RemoveParty"));
			SpriteData spriteData = UIResourceManager.SpriteData;
			TwoDimensionEngineResourceContext resourceContext = UIResourceManager.ResourceContext;
			ResourceDepot uiresourceDepot = UIResourceManager.UIResourceDepot;
			this._armyManagementCategory = spriteData.SpriteCategories["ui_armymanagement"];
			this._armyManagementCategory.Load(resourceContext, uiresourceDepot);
			this._armyManagementLayer = new GauntletLayer(300, "GauntletLayer", false);
			this._gauntletArmyManagementMovie = this._armyManagementLayer.LoadMovie("ArmyManagement", this._armyManagementDatasource);
			this._armyManagementLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
			this._armyManagementLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericPanelGameKeyCategory"));
			this._armyManagementLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("ArmyManagementHotkeyCategory"));
			this._armyManagementLayer.IsFocusLayer = true;
			ScreenManager.TrySetFocus(this._armyManagementLayer);
			base.MapScreen.AddLayer(this._armyManagementLayer);
			this._timeControlModeBeforeArmyManagementOpened = Campaign.Current.TimeControlMode;
			Campaign.Current.TimeControlMode = CampaignTimeControlMode.Stop;
			Campaign.Current.SetTimeControlModeLock(true);
			MapScreen mapScreen;
			if ((mapScreen = (ScreenManager.TopScreen as MapScreen)) != null)
			{
				mapScreen.SetIsInArmyManagement(true);
			}
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000D1D4 File Offset: 0x0000B3D4
		private void CloseArmyManagement()
		{
			if (this._armyManagementLayer != null && this._gauntletArmyManagementMovie != null)
			{
				this._armyManagementLayer.IsFocusLayer = false;
				ScreenManager.TryLoseFocus(this._armyManagementLayer);
				this._armyManagementLayer.InputRestrictions.ResetInputRestrictions();
				this._armyManagementLayer.ReleaseMovie(this._gauntletArmyManagementMovie);
				base.MapScreen.RemoveLayer(this._armyManagementLayer);
			}
			ArmyManagementVM armyManagementDatasource = this._armyManagementDatasource;
			if (armyManagementDatasource != null)
			{
				armyManagementDatasource.OnFinalize();
			}
			this._gauntletArmyManagementMovie = null;
			this._armyManagementDatasource = null;
			this._armyManagementLayer = null;
			GameMenuOverlay overlayDataSource = this._overlayDataSource;
			if (overlayDataSource != null)
			{
				overlayDataSource.Refresh();
			}
			MapScreen mapScreen;
			if ((mapScreen = (ScreenManager.TopScreen as MapScreen)) != null)
			{
				mapScreen.SetIsInArmyManagement(false);
			}
			Game.Current.EventManager.TriggerEvent<TutorialContextChangedEvent>(new TutorialContextChangedEvent(TutorialContexts.MapWindow));
			Campaign.Current.SetTimeControlModeLock(false);
			Campaign.Current.TimeControlMode = this._timeControlModeBeforeArmyManagementOpened;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000D2B8 File Offset: 0x0000B4B8
		private void HandleArmyManagementInput()
		{
			if (this._armyManagementLayer != null && this._armyManagementDatasource != null)
			{
				if (this._armyManagementLayer.Input.IsHotKeyReleased("Exit"))
				{
					UISoundsHelper.PlayUISound("event:/ui/default");
					this._armyManagementDatasource.ExecuteCancel();
					return;
				}
				if (this._armyManagementLayer.Input.IsHotKeyReleased("Confirm"))
				{
					UISoundsHelper.PlayUISound("event:/ui/default");
					this._armyManagementDatasource.ExecuteDone();
					return;
				}
				if (this._armyManagementLayer.Input.IsHotKeyReleased("Reset"))
				{
					UISoundsHelper.PlayUISound("event:/ui/default");
					this._armyManagementDatasource.ExecuteReset();
					return;
				}
				if (this._armyManagementLayer.Input.IsHotKeyReleased("RemoveParty") && this._armyManagementDatasource.FocusedItem != null)
				{
					UISoundsHelper.PlayUISound("event:/ui/default");
					this._armyManagementDatasource.FocusedItem.ExecuteAction();
				}
			}
		}

		// Token: 0x040000DC RID: 220
		private GauntletLayer _layerAsGauntletLayer;

		// Token: 0x040000DD RID: 221
		private GameMenuOverlay _overlayDataSource;

		// Token: 0x040000DE RID: 222
		private readonly GameOverlays.MapOverlayType _type;

		// Token: 0x040000DF RID: 223
		private IGauntletMovie _movie;

		// Token: 0x040000E0 RID: 224
		private bool _isContextMenuEnabled;

		// Token: 0x040000E1 RID: 225
		private GauntletLayer _armyManagementLayer;

		// Token: 0x040000E2 RID: 226
		private SpriteCategory _armyManagementCategory;

		// Token: 0x040000E3 RID: 227
		private ArmyManagementVM _armyManagementDatasource;

		// Token: 0x040000E4 RID: 228
		private IGauntletMovie _gauntletArmyManagementMovie;

		// Token: 0x040000E5 RID: 229
		private CampaignTimeControlMode _timeControlModeBeforeArmyManagementOpened;
	}
}
