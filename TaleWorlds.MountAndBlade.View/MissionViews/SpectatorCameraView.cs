using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.View.MissionViews
{
	// Token: 0x0200005A RID: 90
	public class SpectatorCameraView : MissionView
	{
		// Token: 0x060003EF RID: 1007 RVA: 0x0002173E File Offset: 0x0001F93E
		public override void OnMissionScreenInitialize()
		{
			base.OnMissionScreenInitialize();
			base.MissionScreen.SceneLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("MultiplayerHotkeyCategory"));
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00021768 File Offset: 0x0001F968
		public override void AfterStart()
		{
			for (int i = 0; i < 10; i++)
			{
				this._spectateCamerFrames.Add(MatrixFrame.Identity);
			}
			for (int j = 0; j < 10; j++)
			{
				string tag = "spectate_cam_" + j.ToString();
				List<GameEntity> list = Mission.Current.Scene.FindEntitiesWithTag(tag).ToList<GameEntity>();
				if (list.Count > 0)
				{
					this._spectateCamerFrames[j] = list[0].GetGlobalFrame();
				}
			}
		}

		// Token: 0x04000299 RID: 665
		private List<MatrixFrame> _spectateCamerFrames = new List<MatrixFrame>();
	}
}
