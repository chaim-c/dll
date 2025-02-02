using System;
using SandBox.View.Map;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.ScreenSystem;

namespace SandBox.GauntletUI.Map
{
	// Token: 0x02000021 RID: 33
	[OverrideView(typeof(MapBarView))]
	public class GauntletMapBarView : MapView
	{
		// Token: 0x0600013E RID: 318 RVA: 0x0000A130 File Offset: 0x00008330
		protected override void CreateLayout()
		{
			base.CreateLayout();
			this._gauntletMapBarGlobalLayer = new GauntletMapBarGlobalLayer();
			this._gauntletMapBarGlobalLayer.Initialize(base.MapScreen, 8.5f);
			ScreenManager.AddGlobalLayer(this._gauntletMapBarGlobalLayer, true);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000A165 File Offset: 0x00008365
		protected override void OnFinalize()
		{
			this._gauntletMapBarGlobalLayer.OnFinalize();
			ScreenManager.RemoveGlobalLayer(this._gauntletMapBarGlobalLayer);
			base.OnFinalize();
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000A183 File Offset: 0x00008383
		protected override void OnResume()
		{
			base.OnResume();
			this._gauntletMapBarGlobalLayer.Refresh();
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000A196 File Offset: 0x00008396
		protected override bool IsEscaped()
		{
			return this._gauntletMapBarGlobalLayer.IsEscaped();
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000A1A3 File Offset: 0x000083A3
		protected override void OnMapConversationStart()
		{
			this._gauntletMapBarGlobalLayer.OnMapConversationStart();
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000A1B0 File Offset: 0x000083B0
		protected override void OnMapConversationOver()
		{
			this._gauntletMapBarGlobalLayer.OnMapConversationEnd();
		}

		// Token: 0x0400008D RID: 141
		private GauntletMapBarGlobalLayer _gauntletMapBarGlobalLayer;
	}
}
