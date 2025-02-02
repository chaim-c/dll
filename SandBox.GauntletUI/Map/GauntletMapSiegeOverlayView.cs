using System;
using SandBox.View.Map;
using SandBox.ViewModelCollection.MapSiege;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Siege;
using TaleWorlds.Core;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View;

namespace SandBox.GauntletUI.Map
{
	// Token: 0x02000035 RID: 53
	[OverrideView(typeof(MapSiegeOverlayView))]
	public class GauntletMapSiegeOverlayView : MapView
	{
		// Token: 0x060001E8 RID: 488 RVA: 0x0000DB08 File Offset: 0x0000BD08
		protected override void CreateLayout()
		{
			base.CreateLayout();
			GauntletMapBasicView mapView = base.MapScreen.GetMapView<GauntletMapBasicView>();
			base.Layer = mapView.GauntletNameplateLayer;
			this._layerAsGauntletLayer = (base.Layer as GauntletLayer);
			PartyVisual visualOfParty = PartyVisualManager.Current.GetVisualOfParty(PlayerSiege.PlayerSiegeEvent.BesiegedSettlement.Party);
			this._dataSource = new MapSiegeVM(base.MapScreen._mapCameraView.Camera, visualOfParty.GetAttackerBatteringRamSiegeEngineFrames(), visualOfParty.GetAttackerRangedSiegeEngineFrames(), visualOfParty.GetAttackerTowerSiegeEngineFrames(), visualOfParty.GetDefenderRangedSiegeEngineFrames(), visualOfParty.GetBreachableWallFrames());
			CampaignEvents.SiegeEngineBuiltEvent.AddNonSerializedListener(this, new Action<SiegeEvent, BattleSideEnum, SiegeEngineType>(this.OnSiegeEngineBuilt));
			this._movie = this._layerAsGauntletLayer.LoadMovie("MapSiegeOverlay", this._dataSource);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000DBCA File Offset: 0x0000BDCA
		protected override void OnMapScreenUpdate(float dt)
		{
			base.OnMapScreenUpdate(dt);
			MapSiegeVM dataSource = this._dataSource;
			if (dataSource == null)
			{
				return;
			}
			dataSource.Update(base.MapScreen._mapCameraView.CameraDistance);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000DBF3 File Offset: 0x0000BDF3
		protected override void OnFinalize()
		{
			this._layerAsGauntletLayer.ReleaseMovie(this._movie);
			this._movie = null;
			this._dataSource = null;
			base.Layer = null;
			this._layerAsGauntletLayer = null;
			CampaignEvents.SiegeEngineBuiltEvent.ClearListeners(this);
			base.OnFinalize();
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000DC34 File Offset: 0x0000BE34
		protected override void OnSiegeEngineClick(MatrixFrame siegeEngineFrame)
		{
			base.OnSiegeEngineClick(siegeEngineFrame);
			UISoundsHelper.PlayUISound("event:/ui/panels/siege/engine_click");
			MapSiegeVM dataSource = this._dataSource;
			if (dataSource != null && dataSource.ProductionController.IsEnabled && this._dataSource.ProductionController.LatestSelectedPOI.MapSceneLocationFrame.NearlyEquals(siegeEngineFrame, 1E-05f))
			{
				this._dataSource.ProductionController.ExecuteDisable();
				return;
			}
			MapSiegeVM dataSource2 = this._dataSource;
			if (dataSource2 != null)
			{
				dataSource2.OnSelectionFromScene(siegeEngineFrame);
			}
			base.MapState.OnSiegeEngineClick(siegeEngineFrame);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000DCBF File Offset: 0x0000BEBF
		protected override void OnMapTerrainClick()
		{
			base.OnMapTerrainClick();
			MapSiegeVM dataSource = this._dataSource;
			if (dataSource == null)
			{
				return;
			}
			dataSource.ProductionController.ExecuteDisable();
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000DCDC File Offset: 0x0000BEDC
		private void OnSiegeEngineBuilt(SiegeEvent siegeEvent, BattleSideEnum side, SiegeEngineType siegeEngineType)
		{
			if (siegeEvent.IsPlayerSiegeEvent && side == PlayerSiege.PlayerSide)
			{
				UISoundsHelper.PlayUISound("event:/ui/panels/siege/engine_build_complete");
			}
		}

		// Token: 0x040000EE RID: 238
		private GauntletLayer _layerAsGauntletLayer;

		// Token: 0x040000EF RID: 239
		private MapSiegeVM _dataSource;

		// Token: 0x040000F0 RID: 240
		private IGauntletMovie _movie;
	}
}
