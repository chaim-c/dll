using System;
using SandBox.Missions.AgentControllers;
using SandBox.Missions.MissionLogics;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View.MissionViews;

namespace SandBox.View.Missions
{
	// Token: 0x0200000F RID: 15
	public class MissionAmbushIntroView : MissionView
	{
		// Token: 0x06000064 RID: 100 RVA: 0x00004D8C File Offset: 0x00002F8C
		public override void AfterStart()
		{
			base.AfterStart();
			this._ambushMissionController = base.Mission.GetMissionBehavior<AmbushMissionController>();
			this._ambushIntroLogic = base.Mission.GetMissionBehavior<AmbushIntroLogic>();
			this._isPlayerAmbusher = this._ambushMissionController.IsPlayerAmbusher;
			this._cameraStart = base.Mission.Scene.FindEntityWithTag(this._isPlayerAmbusher ? "intro_camera_attacker_start" : "intro_camera_defender_start").GetGlobalFrame();
			this._cameraEnd = base.Mission.Scene.FindEntityWithTag(this._isPlayerAmbusher ? "intro_camera_attacker_end" : "intro_camera_defender_end").GetGlobalFrame();
			this.IntroEndAction = new Action(this._ambushIntroLogic.OnIntroEnded);
			this._ambushIntroLogic.StartIntroAction = new Action(this.StartIntro);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00004E60 File Offset: 0x00003060
		public void StartIntro()
		{
			this._started = true;
			this._camera = Camera.CreateCamera();
			this._camera.FillParametersFrom(base.MissionScreen.CombatCamera);
			this._camera.Frame = this._cameraStart;
			base.MissionScreen.CustomCamera = this._camera;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00004EB8 File Offset: 0x000030B8
		public override void OnMissionTick(float dt)
		{
			if (this._firstTick)
			{
				this._firstTick = false;
			}
			if (!this._started)
			{
				return;
			}
			if (this._cameraLerping < 1f)
			{
				MatrixFrame frame;
				frame.origin = MBMath.Lerp(this._cameraStart.origin, this._cameraEnd.origin, this._cameraLerping, 1E-05f);
				frame.rotation = MBMath.Lerp(ref this._cameraStart.rotation, ref this._cameraEnd.rotation, this._cameraLerping, 1E-05f);
				this._camera.Frame = frame;
				this._cameraLerping += this._cameraMoveSpeed * dt;
				return;
			}
			this._camera.Frame = this._cameraEnd;
			this.CleanUp();
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00004F7D File Offset: 0x0000317D
		private void CleanUp()
		{
			base.MissionScreen.CustomCamera = null;
			this.IntroEndAction();
			base.Mission.RemoveMissionBehavior(this);
		}

		// Token: 0x0400001A RID: 26
		private AmbushMissionController _ambushMissionController;

		// Token: 0x0400001B RID: 27
		private AmbushIntroLogic _ambushIntroLogic;

		// Token: 0x0400001C RID: 28
		private bool _isPlayerAmbusher;

		// Token: 0x0400001D RID: 29
		private MatrixFrame _cameraStart;

		// Token: 0x0400001E RID: 30
		private MatrixFrame _cameraEnd;

		// Token: 0x0400001F RID: 31
		private float _cameraMoveSpeed = 0.1f;

		// Token: 0x04000020 RID: 32
		private float _cameraLerping;

		// Token: 0x04000021 RID: 33
		private Camera _camera;

		// Token: 0x04000022 RID: 34
		public Action IntroEndAction;

		// Token: 0x04000023 RID: 35
		private bool _started;

		// Token: 0x04000024 RID: 36
		private bool _firstTick = true;
	}
}
