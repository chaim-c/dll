using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001DC RID: 476
	public class MBTestRun
	{
		// Token: 0x06001ACD RID: 6861 RVA: 0x0005D294 File Offset: 0x0005B494
		public static bool EnterEditMode()
		{
			return MBAPI.IMBTestRun.EnterEditMode();
		}

		// Token: 0x06001ACE RID: 6862 RVA: 0x0005D2A0 File Offset: 0x0005B4A0
		public static bool NewScene()
		{
			return MBAPI.IMBTestRun.NewScene();
		}

		// Token: 0x06001ACF RID: 6863 RVA: 0x0005D2AC File Offset: 0x0005B4AC
		public static bool LeaveEditMode()
		{
			return MBAPI.IMBTestRun.LeaveEditMode();
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x0005D2B8 File Offset: 0x0005B4B8
		public static bool OpenScene(string sceneName)
		{
			return MBAPI.IMBTestRun.OpenScene(sceneName);
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x0005D2C5 File Offset: 0x0005B4C5
		public static bool CloseScene()
		{
			return MBAPI.IMBTestRun.CloseScene();
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x0005D2D1 File Offset: 0x0005B4D1
		public static bool SaveScene()
		{
			return false;
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x0005D2D4 File Offset: 0x0005B4D4
		public static bool OpenDefaultScene()
		{
			return false;
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x0005D2D7 File Offset: 0x0005B4D7
		public static int GetFPS()
		{
			return MBAPI.IMBTestRun.GetFPS();
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x0005D2E3 File Offset: 0x0005B4E3
		public static void StartMission()
		{
			MBAPI.IMBTestRun.StartMission();
		}
	}
}
