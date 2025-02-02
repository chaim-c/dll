using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200019F RID: 415
	[ScriptingInterfaceBase]
	internal interface IMBTestRun
	{
		// Token: 0x06001568 RID: 5480
		[EngineMethod("auto_continue", false)]
		int AutoContinue(int type);

		// Token: 0x06001569 RID: 5481
		[EngineMethod("get_fps", false)]
		int GetFPS();

		// Token: 0x0600156A RID: 5482
		[EngineMethod("enter_edit_mode", false)]
		bool EnterEditMode();

		// Token: 0x0600156B RID: 5483
		[EngineMethod("open_scene", false)]
		bool OpenScene(string sceneName);

		// Token: 0x0600156C RID: 5484
		[EngineMethod("close_scene", false)]
		bool CloseScene();

		// Token: 0x0600156D RID: 5485
		[EngineMethod("save_scene", false)]
		bool SaveScene();

		// Token: 0x0600156E RID: 5486
		[EngineMethod("open_default_scene", false)]
		bool OpenDefaultScene();

		// Token: 0x0600156F RID: 5487
		[EngineMethod("leave_edit_mode", false)]
		bool LeaveEditMode();

		// Token: 0x06001570 RID: 5488
		[EngineMethod("new_scene", false)]
		bool NewScene();

		// Token: 0x06001571 RID: 5489
		[EngineMethod("start_mission", false)]
		void StartMission();
	}
}
