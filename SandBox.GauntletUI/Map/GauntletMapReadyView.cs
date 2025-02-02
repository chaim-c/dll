using System;
using SandBox.View.Map;
using TaleWorlds.Core.ViewModelCollection.Generic;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.MountAndBlade.View;

namespace SandBox.GauntletUI.Map
{
	// Token: 0x02000032 RID: 50
	[OverrideView(typeof(MapReadyView))]
	public class GauntletMapReadyView : MapReadyView
	{
		// Token: 0x060001D4 RID: 468 RVA: 0x0000D508 File Offset: 0x0000B708
		protected override void CreateLayout()
		{
			base.CreateLayout();
			this._dataSource = new BoolItemWithActionVM(null, true, null);
			base.Layer = new GauntletLayer(9999, "GauntletLayer", false);
			(base.Layer as GauntletLayer).LoadMovie("MapReadyBlocker", this._dataSource);
			base.MapScreen.AddLayer(base.Layer);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000D56C File Offset: 0x0000B76C
		protected override void OnFinalize()
		{
			base.OnFinalize();
			this._dataSource.OnFinalize();
			base.MapScreen.RemoveLayer(base.Layer);
			base.Layer = null;
			this._dataSource = null;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000D59E File Offset: 0x0000B79E
		public override void SetIsMapSceneReady(bool isReady)
		{
			base.SetIsMapSceneReady(isReady);
			this._dataSource.IsActive = !isReady;
		}

		// Token: 0x040000E9 RID: 233
		private BoolItemWithActionVM _dataSource;
	}
}
