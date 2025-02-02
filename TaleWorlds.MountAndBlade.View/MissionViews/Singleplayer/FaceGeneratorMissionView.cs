using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.ScreenSystem;

namespace TaleWorlds.MountAndBlade.View.MissionViews.Singleplayer
{
	// Token: 0x02000062 RID: 98
	public class FaceGeneratorMissionView : MissionView
	{
		// Token: 0x06000411 RID: 1041 RVA: 0x00022296 File Offset: 0x00020496
		public override void OnMissionTick(float dt)
		{
			base.OnMissionTick(dt);
			if (base.Input.IsGameKeyPressed(37) && !GameNetwork.IsSessionActive)
			{
				LoadingWindow.EnableGlobalLoadingWindow();
				ScreenManager.PushScreen(ViewCreator.CreateMBFaceGeneratorScreen(Game.Current.PlayerTroop, false, null));
			}
		}
	}
}
