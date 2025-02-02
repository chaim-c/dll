using System;
using System.Collections.Generic;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View.MissionViews;

namespace SandBox.View.Missions
{
	// Token: 0x02000016 RID: 22
	public class MissionCustomCameraView : MissionView
	{
		// Token: 0x0600008E RID: 142 RVA: 0x00006034 File Offset: 0x00004234
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			foreach (GameEntity gameEntity in base.Mission.Scene.FindEntitiesWithTag(this.tag))
			{
				Camera camera = Camera.CreateCamera();
				gameEntity.GetCameraParamsFromCameraScript(camera, ref this._dofParams);
				this._cameras.Add(camera);
			}
			base.MissionScreen.CustomCamera = this._cameras[0];
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000060C4 File Offset: 0x000042C4
		public override void OnMissionTick(float dt)
		{
			base.OnMissionTick(dt);
			if (base.DebugInput.IsHotKeyReleased("CustomCameraMissionViewHotkeyIncreaseCustomCameraIndex"))
			{
				this._currentCameraIndex++;
				if (this._currentCameraIndex >= this._cameras.Count)
				{
					this._currentCameraIndex = 0;
				}
				base.MissionScreen.CustomCamera = this._cameras[this._currentCameraIndex];
			}
		}

		// Token: 0x0400003E RID: 62
		public string tag = "customcamera";

		// Token: 0x0400003F RID: 63
		private readonly List<Camera> _cameras = new List<Camera>();

		// Token: 0x04000040 RID: 64
		public Vec3 _dofParams;

		// Token: 0x04000041 RID: 65
		private int _currentCameraIndex;
	}
}
