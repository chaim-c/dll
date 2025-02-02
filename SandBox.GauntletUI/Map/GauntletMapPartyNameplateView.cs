using System;
using SandBox.View.Map;
using SandBox.ViewModelCollection.Nameplate;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.MountAndBlade.View;

namespace SandBox.GauntletUI.Map
{
	// Token: 0x02000031 RID: 49
	[OverrideView(typeof(MapPartyNameplateView))]
	public class GauntletMapPartyNameplateView : MapView
	{
		// Token: 0x060001CE RID: 462 RVA: 0x0000D3A0 File Offset: 0x0000B5A0
		protected override void CreateLayout()
		{
			base.CreateLayout();
			this._dataSource = new PartyNameplatesVM(base.MapScreen._mapCameraView.Camera, new Action(base.MapScreen.FastMoveCameraToMainParty), new Func<bool>(this.IsShowPartyNamesEnabled));
			GauntletMapBasicView mapView = base.MapScreen.GetMapView<GauntletMapBasicView>();
			base.Layer = mapView.GauntletNameplateLayer;
			this._layerAsGauntletLayer = (base.Layer as GauntletLayer);
			this._movie = this._layerAsGauntletLayer.LoadMovie("PartyNameplate", this._dataSource);
			this._dataSource.Initialize();
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000D43C File Offset: 0x0000B63C
		protected override void OnMapScreenUpdate(float dt)
		{
			base.OnMapScreenUpdate(dt);
			this._dataSource.Update();
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000D450 File Offset: 0x0000B650
		protected override void OnResume()
		{
			base.OnResume();
			foreach (PartyNameplateVM partyNameplateVM in this._dataSource.Nameplates)
			{
				partyNameplateVM.RefreshDynamicProperties(true);
			}
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000D4A8 File Offset: 0x0000B6A8
		protected override void OnFinalize()
		{
			this._layerAsGauntletLayer.ReleaseMovie(this._movie);
			this._dataSource.OnFinalize();
			this._layerAsGauntletLayer = null;
			base.Layer = null;
			this._movie = null;
			this._dataSource = null;
			base.OnFinalize();
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000D4E8 File Offset: 0x0000B6E8
		private bool IsShowPartyNamesEnabled()
		{
			return base.MapScreen.SceneLayer.Input.IsGameKeyDown(5);
		}

		// Token: 0x040000E6 RID: 230
		private GauntletLayer _layerAsGauntletLayer;

		// Token: 0x040000E7 RID: 231
		private PartyNameplatesVM _dataSource;

		// Token: 0x040000E8 RID: 232
		private IGauntletMovie _movie;
	}
}
