using System;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.MissionViews;
using TaleWorlds.MountAndBlade.View.MissionViews.Singleplayer;
using TaleWorlds.MountAndBlade.ViewModelCollection;
using TaleWorlds.ScreenSystem;

namespace TaleWorlds.MountAndBlade.GauntletUI.Mission.Singleplayer
{
	// Token: 0x0200002F RID: 47
	[OverrideView(typeof(MissionLeaveView))]
	public class MissionGauntletLeaveView : MissionView
	{
		// Token: 0x0600020C RID: 524 RVA: 0x0000C948 File Offset: 0x0000AB48
		public override void OnMissionScreenInitialize()
		{
			base.OnMissionScreenInitialize();
			this._dataSource = new MissionLeaveVM(new Func<float>(base.Mission.GetMissionEndTimerValue), new Func<float>(base.Mission.GetMissionEndTimeInSeconds));
			this._gauntletLayer = new GauntletLayer(47, "GauntletLayer", false);
			this._gauntletLayer.LoadMovie("LeaveUI", this._dataSource);
			base.MissionScreen.AddLayer(this._gauntletLayer);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000C9C3 File Offset: 0x0000ABC3
		public override void OnMissionScreenFinalize()
		{
			base.OnMissionScreenFinalize();
			this._gauntletLayer = null;
			this._dataSource.OnFinalize();
			this._dataSource = null;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000C9E4 File Offset: 0x0000ABE4
		public override void OnMissionTick(float dt)
		{
			base.OnMissionTick(dt);
			this._dataSource.Tick(dt);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000C9F9 File Offset: 0x0000ABF9
		private void OnEscapeMenuToggled(bool isOpened)
		{
			ScreenManager.SetSuspendLayer(this._gauntletLayer, !isOpened);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000CA0A File Offset: 0x0000AC0A
		public override void OnPhotoModeActivated()
		{
			base.OnPhotoModeActivated();
			this._gauntletLayer.UIContext.ContextAlpha = 0f;
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000CA27 File Offset: 0x0000AC27
		public override void OnPhotoModeDeactivated()
		{
			base.OnPhotoModeDeactivated();
			this._gauntletLayer.UIContext.ContextAlpha = 1f;
		}

		// Token: 0x04000129 RID: 297
		private GauntletLayer _gauntletLayer;

		// Token: 0x0400012A RID: 298
		private MissionLeaveVM _dataSource;
	}
}
