using System;
using SandBox.View.Map;
using SandBox.ViewModelCollection.Map;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Engine;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View;

namespace SandBox.GauntletUI.Map
{
	// Token: 0x0200002D RID: 45
	[OverrideView(typeof(MapEventVisualsView))]
	public class GauntletMapEventVisualsView : MapView, IGauntletMapEventVisualHandler
	{
		// Token: 0x060001A8 RID: 424 RVA: 0x0000C748 File Offset: 0x0000A948
		protected override void CreateLayout()
		{
			base.CreateLayout();
			this._dataSource = new MapEventVisualsVM(base.MapScreen._mapCameraView.Camera, new Func<Vec2, Vec3>(this.GetRealPositionOfMapEvent));
			GauntletMapBasicView mapView = base.MapScreen.GetMapView<GauntletMapBasicView>();
			base.Layer = mapView.GauntletNameplateLayer;
			this._layerAsGauntletLayer = (base.Layer as GauntletLayer);
			this._movie = this._layerAsGauntletLayer.LoadMovie("MapEventVisuals", this._dataSource);
			GauntletMapEventVisualCreator gauntletMapEventVisualCreator;
			if ((gauntletMapEventVisualCreator = (Campaign.Current.VisualCreator.MapEventVisualCreator as GauntletMapEventVisualCreator)) != null)
			{
				gauntletMapEventVisualCreator.Handlers.Add(this);
				foreach (GauntletMapEventVisual gauntletMapEventVisual in gauntletMapEventVisualCreator.GetCurrentEvents())
				{
					this._dataSource.OnMapEventStarted(gauntletMapEventVisual.MapEvent);
				}
			}
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000C838 File Offset: 0x0000AA38
		protected override void OnMapScreenUpdate(float dt)
		{
			base.OnMapScreenUpdate(dt);
			this._dataSource.Update(dt);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000C850 File Offset: 0x0000AA50
		protected override void OnFinalize()
		{
			GauntletMapEventVisualCreator gauntletMapEventVisualCreator;
			if ((gauntletMapEventVisualCreator = (Campaign.Current.VisualCreator.MapEventVisualCreator as GauntletMapEventVisualCreator)) != null)
			{
				gauntletMapEventVisualCreator.Handlers.Remove(this);
			}
			this._dataSource.OnFinalize();
			this._layerAsGauntletLayer.ReleaseMovie(this._movie);
			base.Layer = null;
			this._layerAsGauntletLayer = null;
			this._movie = null;
			this._dataSource = null;
			base.OnFinalize();
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000C8C0 File Offset: 0x0000AAC0
		private Vec3 GetRealPositionOfMapEvent(Vec2 mapEventPosition)
		{
			float z = 0f;
			((MapScene)Campaign.Current.MapSceneWrapper).Scene.GetHeightAtPoint(mapEventPosition, BodyFlags.CommonCollisionExcludeFlagsForCombat, ref z);
			return new Vec3(mapEventPosition.x, mapEventPosition.y, z, -1f);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000C90C File Offset: 0x0000AB0C
		void IGauntletMapEventVisualHandler.OnNewEventStarted(GauntletMapEventVisual newEvent)
		{
			this._dataSource.OnMapEventStarted(newEvent.MapEvent);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000C91F File Offset: 0x0000AB1F
		void IGauntletMapEventVisualHandler.OnInitialized(GauntletMapEventVisual newEvent)
		{
			this._dataSource.OnMapEventStarted(newEvent.MapEvent);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000C932 File Offset: 0x0000AB32
		void IGauntletMapEventVisualHandler.OnEventEnded(GauntletMapEventVisual newEvent)
		{
			this._dataSource.OnMapEventEnded(newEvent.MapEvent);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000C945 File Offset: 0x0000AB45
		void IGauntletMapEventVisualHandler.OnEventVisibilityChanged(GauntletMapEventVisual visibilityChangedEvent)
		{
			this._dataSource.OnMapEventVisibilityChanged(visibilityChangedEvent.MapEvent);
		}

		// Token: 0x040000D0 RID: 208
		private GauntletLayer _layerAsGauntletLayer;

		// Token: 0x040000D1 RID: 209
		private IGauntletMovie _movie;

		// Token: 0x040000D2 RID: 210
		private MapEventVisualsVM _dataSource;
	}
}
