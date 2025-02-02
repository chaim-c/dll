using System;
using SandBox.View.Menu;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View;

namespace SandBox.GauntletUI.Menu
{
	// Token: 0x0200001A RID: 26
	[OverrideView(typeof(MenuBackgroundView))]
	public class GauntlerMenuBackground : MenuView
	{
		// Token: 0x06000114 RID: 276 RVA: 0x00008F98 File Offset: 0x00007198
		protected override void OnInitialize()
		{
			base.OnInitialize();
			base.Layer = base.MenuViewContext.FindLayer<GauntletLayer>("BasicLayer");
			if (base.Layer == null)
			{
				base.Layer = new GauntletLayer(100, "GauntletLayer", false)
				{
					Name = "BasicLayer"
				};
				base.MenuViewContext.AddLayer(base.Layer);
			}
			GauntletLayer gauntletLayer = base.Layer as GauntletLayer;
			this._movie = gauntletLayer.LoadMovie("GameMenuBackground", null);
			base.Layer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00009028 File Offset: 0x00007228
		protected override void OnFinalize()
		{
			GauntletLayer gauntletLayer = base.Layer as GauntletLayer;
			if (gauntletLayer != null)
			{
				gauntletLayer.ReleaseMovie(this._movie);
			}
			base.Layer = null;
			this._movie = null;
			base.OnFinalize();
		}

		// Token: 0x04000075 RID: 117
		private IGauntletMovie _movie;
	}
}
