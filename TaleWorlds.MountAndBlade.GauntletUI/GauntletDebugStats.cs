using System;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.Library;
using TaleWorlds.ScreenSystem;

namespace TaleWorlds.MountAndBlade.GauntletUI
{
	// Token: 0x02000006 RID: 6
	public class GauntletDebugStats : GlobalLayer
	{
		// Token: 0x06000034 RID: 52 RVA: 0x00003C84 File Offset: 0x00001E84
		public void Initialize()
		{
			this._dataSource = new DebugStatsVM();
			GauntletLayer gauntletLayer = new GauntletLayer(15000, "GauntletLayer", false);
			gauntletLayer.LoadMovie("DebugStats", this._dataSource);
			gauntletLayer.InputRestrictions.SetInputRestrictions(false, InputUsageMask.Invalid);
			base.Layer = gauntletLayer;
			ScreenManager.AddGlobalLayer(this, true);
		}

		// Token: 0x04000030 RID: 48
		private DebugStatsVM _dataSource;
	}
}
