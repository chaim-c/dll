using System;
using SandBox.Missions.MissionLogics.Arena;
using SandBox.View.Missions;
using SandBox.ViewModelCollection.Missions;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.MissionViews;

namespace SandBox.GauntletUI.Missions
{
	// Token: 0x02000013 RID: 19
	[OverrideView(typeof(MissionArenaPracticeFightView))]
	public class MissionGauntletArenaPracticeFightView : MissionView
	{
		// Token: 0x060000BA RID: 186 RVA: 0x00007534 File Offset: 0x00005734
		public override void OnMissionScreenInitialize()
		{
			base.OnMissionScreenInitialize();
			ArenaPracticeFightMissionController missionBehavior = base.Mission.GetMissionBehavior<ArenaPracticeFightMissionController>();
			this._dataSource = new MissionArenaPracticeFightVM(missionBehavior);
			this._gauntletLayer = new GauntletLayer(this.ViewOrderPriority, "GauntletLayer", false);
			this._movie = this._gauntletLayer.LoadMovie("ArenaPracticeFight", this._dataSource);
			base.MissionScreen.AddLayer(this._gauntletLayer);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000075A3 File Offset: 0x000057A3
		public override void OnMissionScreenTick(float dt)
		{
			base.OnMissionScreenTick(dt);
			this._dataSource.Tick();
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000075B7 File Offset: 0x000057B7
		public override void OnMissionScreenFinalize()
		{
			this._dataSource.OnFinalize();
			this._gauntletLayer.ReleaseMovie(this._movie);
			base.MissionScreen.RemoveLayer(this._gauntletLayer);
			base.OnMissionScreenFinalize();
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000075EC File Offset: 0x000057EC
		public override void OnPhotoModeActivated()
		{
			base.OnPhotoModeActivated();
			this._gauntletLayer.UIContext.ContextAlpha = 0f;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00007609 File Offset: 0x00005809
		public override void OnPhotoModeDeactivated()
		{
			base.OnPhotoModeDeactivated();
			this._gauntletLayer.UIContext.ContextAlpha = 1f;
		}

		// Token: 0x04000051 RID: 81
		private MissionArenaPracticeFightVM _dataSource;

		// Token: 0x04000052 RID: 82
		private GauntletLayer _gauntletLayer;

		// Token: 0x04000053 RID: 83
		private IGauntletMovie _movie;
	}
}
