using System;
using SandBox.View.Map;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View;

namespace SandBox.GauntletUI.Map
{
	// Token: 0x02000024 RID: 36
	[OverrideView(typeof(MapBasicView))]
	public class GauntletMapBasicView : MapView
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000153 RID: 339 RVA: 0x0000AC8C File Offset: 0x00008E8C
		// (set) Token: 0x06000154 RID: 340 RVA: 0x0000AC94 File Offset: 0x00008E94
		public GauntletLayer GauntletLayer { get; private set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000155 RID: 341 RVA: 0x0000AC9D File Offset: 0x00008E9D
		// (set) Token: 0x06000156 RID: 342 RVA: 0x0000ACA5 File Offset: 0x00008EA5
		public GauntletLayer GauntletNameplateLayer { get; private set; }

		// Token: 0x06000157 RID: 343 RVA: 0x0000ACB0 File Offset: 0x00008EB0
		protected override void CreateLayout()
		{
			base.CreateLayout();
			this.GauntletLayer = new GauntletLayer(100, "GauntletLayer", false);
			this.GauntletLayer.InputRestrictions.SetInputRestrictions(false, InputUsageMask.All);
			this.GauntletLayer.Name = "BasicLayer";
			base.MapScreen.AddLayer(this.GauntletLayer);
			this.GauntletNameplateLayer = new GauntletLayer(90, "GauntletLayer", false);
			this.GauntletNameplateLayer.InputRestrictions.SetInputRestrictions(false, InputUsageMask.MouseButtons | InputUsageMask.Keyboardkeys);
			base.MapScreen.AddLayer(this.GauntletNameplateLayer);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000AD3F File Offset: 0x00008F3F
		protected override void OnMapConversationStart()
		{
			base.OnMapConversationStart();
			this.GauntletLayer.TwoDimensionView.SetEnable(false);
			this.GauntletNameplateLayer.TwoDimensionView.SetEnable(false);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000AD69 File Offset: 0x00008F69
		protected override void OnMapConversationOver()
		{
			base.OnMapConversationOver();
			this.GauntletLayer.TwoDimensionView.SetEnable(true);
			this.GauntletNameplateLayer.TwoDimensionView.SetEnable(true);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000AD93 File Offset: 0x00008F93
		protected override void OnFinalize()
		{
			base.MapScreen.RemoveLayer(this.GauntletLayer);
			this.GauntletLayer = null;
			base.OnFinalize();
		}
	}
}
