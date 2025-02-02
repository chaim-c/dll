using System;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.MissionViews;
using TaleWorlds.MountAndBlade.ViewModelCollection;
using TaleWorlds.ScreenSystem;

namespace TaleWorlds.MountAndBlade.GauntletUI.Mission
{
	// Token: 0x02000022 RID: 34
	[OverrideView(typeof(MissionBoundaryCrossingView))]
	public class MissionGauntletBoundaryCrossingView : MissionGauntletBattleUIBase
	{
		// Token: 0x06000163 RID: 355 RVA: 0x00008B54 File Offset: 0x00006D54
		protected override void OnCreateView()
		{
			this._dataSource = new BoundaryCrossingVM(base.Mission, new Action<bool>(this.OnEscapeMenuToggled));
			this._gauntletLayer = new GauntletLayer(47, "GauntletLayer", false);
			this._gauntletLayer.LoadMovie("BoundaryCrossing", this._dataSource);
			base.MissionScreen.AddLayer(this._gauntletLayer);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00008BB9 File Offset: 0x00006DB9
		protected override void OnDestroyView()
		{
			this._gauntletLayer = null;
			this._dataSource.OnFinalize();
			this._dataSource = null;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00008BD4 File Offset: 0x00006DD4
		private void OnEscapeMenuToggled(bool isOpened)
		{
			if (base.IsViewActive)
			{
				ScreenManager.SetSuspendLayer(this._gauntletLayer, !isOpened);
			}
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00008BED File Offset: 0x00006DED
		public override void OnPhotoModeActivated()
		{
			base.OnPhotoModeActivated();
			if (base.IsViewActive)
			{
				this._gauntletLayer.UIContext.ContextAlpha = 0f;
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00008C12 File Offset: 0x00006E12
		public override void OnPhotoModeDeactivated()
		{
			base.OnPhotoModeDeactivated();
			if (base.IsViewActive)
			{
				this._gauntletLayer.UIContext.ContextAlpha = 1f;
			}
		}

		// Token: 0x040000C9 RID: 201
		private GauntletLayer _gauntletLayer;

		// Token: 0x040000CA RID: 202
		private BoundaryCrossingVM _dataSource;
	}
}
