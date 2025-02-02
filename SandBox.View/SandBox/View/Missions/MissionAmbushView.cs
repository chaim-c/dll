using System;
using SandBox.Missions.AgentControllers;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.MissionViews;
using TaleWorlds.MountAndBlade.View.MissionViews.SiegeWeapon;
using TaleWorlds.MountAndBlade.View.MissionViews.Singleplayer;

namespace SandBox.View.Missions
{
	// Token: 0x02000010 RID: 16
	public class MissionAmbushView : MissionView
	{
		// Token: 0x06000069 RID: 105 RVA: 0x00004FBC File Offset: 0x000031BC
		public override void AfterStart()
		{
			base.AfterStart();
			this._ambushMissionController = base.Mission.GetMissionBehavior<AmbushMissionController>();
			this._deploymentBoundaryMarkerHandler = base.Mission.GetMissionBehavior<MissionDeploymentBoundaryMarker>();
			this._ambushMissionController.PlayerDeploymentFinish += this.OnPlayerDeploymentFinish;
			this._ambushMissionController.IntroFinish += this.OnIntroFinish;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00005020 File Offset: 0x00003220
		public override void OnMissionTick(float dt)
		{
			if (this._firstTick)
			{
				this._firstTick = false;
				if (this._ambushMissionController.IsPlayerAmbusher)
				{
					this._ambushDeploymentView = new MissionAmbushDeploymentView();
					base.MissionScreen.AddMissionView(this._ambushDeploymentView);
					this._ambushDeploymentView.OnBehaviorInitialize();
					this._ambushDeploymentView.EarlyStart();
					this._ambushDeploymentView.AfterStart();
				}
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00005088 File Offset: 0x00003288
		public void OnIntroFinish()
		{
			if (this._deploymentBoundaryMarkerHandler != null)
			{
				base.Mission.RemoveMissionBehavior(this._deploymentBoundaryMarkerHandler);
			}
			base.MissionScreen.AddMissionView(ViewCreator.CreateMissionAgentStatusUIHandler(null));
			base.MissionScreen.AddMissionView(ViewCreator.CreateMissionMainAgentEquipmentController(null));
			base.MissionScreen.AddMissionView(ViewCreator.CreateMissionMainAgentCheerBarkControllerView(null));
			base.MissionScreen.AddMissionView(ViewCreator.CreateMissionAgentLockVisualizerView(null));
			base.MissionScreen.AddMissionView(ViewCreator.CreateMissionBoundaryCrossingView());
			base.MissionScreen.AddMissionView(new MissionBoundaryWallView());
			base.MissionScreen.AddMissionView(new MissionMainAgentController());
			base.MissionScreen.AddMissionView(new MissionCrosshair());
			base.MissionScreen.AddMissionView(new RangedSiegeWeaponViewController());
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00005142 File Offset: 0x00003342
		public void OnPlayerDeploymentFinish()
		{
			if (this._ambushMissionController.IsPlayerAmbusher)
			{
				base.Mission.RemoveMissionBehavior(this._ambushDeploymentView);
			}
		}

		// Token: 0x04000025 RID: 37
		private AmbushMissionController _ambushMissionController;

		// Token: 0x04000026 RID: 38
		private MissionDeploymentBoundaryMarker _deploymentBoundaryMarkerHandler;

		// Token: 0x04000027 RID: 39
		private MissionAmbushDeploymentView _ambushDeploymentView;

		// Token: 0x04000028 RID: 40
		private bool _firstTick = true;
	}
}
