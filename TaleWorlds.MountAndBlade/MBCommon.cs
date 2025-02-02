using System;
using TaleWorlds.DotNet;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001BE RID: 446
	public class MBCommon
	{
		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x060019AA RID: 6570 RVA: 0x0005B815 File Offset: 0x00059A15
		// (set) Token: 0x060019AB RID: 6571 RVA: 0x0005B81C File Offset: 0x00059A1C
		public static MBCommon.GameType CurrentGameType
		{
			get
			{
				return MBCommon._currentGameType;
			}
			set
			{
				MBCommon._currentGameType = value;
				MBAPI.IMBWorld.SetGameType((int)value);
			}
		}

		// Token: 0x060019AC RID: 6572 RVA: 0x0005B82F File Offset: 0x00059A2F
		public static void PauseGameEngine()
		{
			MBCommon.IsPaused = true;
			MBAPI.IMBWorld.PauseGame();
		}

		// Token: 0x060019AD RID: 6573 RVA: 0x0005B841 File Offset: 0x00059A41
		public static void UnPauseGameEngine()
		{
			MBCommon.IsPaused = false;
			MBAPI.IMBWorld.UnpauseGame();
		}

		// Token: 0x060019AE RID: 6574 RVA: 0x0005B853 File Offset: 0x00059A53
		public static float GetApplicationTime()
		{
			return MBAPI.IMBWorld.GetGlobalTime(MBCommon.TimeType.Application);
		}

		// Token: 0x060019AF RID: 6575 RVA: 0x0005B860 File Offset: 0x00059A60
		public static float GetTotalMissionTime()
		{
			return MBAPI.IMBWorld.GetGlobalTime(MBCommon.TimeType.Mission);
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x060019B0 RID: 6576 RVA: 0x0005B86D File Offset: 0x00059A6D
		public static bool IsDebugMode
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060019B1 RID: 6577 RVA: 0x0005B870 File Offset: 0x00059A70
		public static void FixSkeletons()
		{
			MBAPI.IMBWorld.FixSkeletons();
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x060019B2 RID: 6578 RVA: 0x0005B87C File Offset: 0x00059A7C
		// (set) Token: 0x060019B3 RID: 6579 RVA: 0x0005B883 File Offset: 0x00059A83
		public static bool IsPaused { get; private set; }

		// Token: 0x060019B4 RID: 6580 RVA: 0x0005B88B File Offset: 0x00059A8B
		public static void CheckResourceModifications()
		{
			MBAPI.IMBWorld.CheckResourceModifications();
		}

		// Token: 0x060019B5 RID: 6581 RVA: 0x0005B898 File Offset: 0x00059A98
		public static int Hash(int i, object o)
		{
			return (i * 397 ^ o.GetHashCode()).ToString().GetHashCode();
		}

		// Token: 0x040007E4 RID: 2020
		private static MBCommon.GameType _currentGameType;

		// Token: 0x020004D7 RID: 1239
		public enum GameType
		{
			// Token: 0x04001B27 RID: 6951
			Single,
			// Token: 0x04001B28 RID: 6952
			MultiClient,
			// Token: 0x04001B29 RID: 6953
			MultiServer,
			// Token: 0x04001B2A RID: 6954
			MultiClientServer,
			// Token: 0x04001B2B RID: 6955
			SingleReplay,
			// Token: 0x04001B2C RID: 6956
			SingleRecord
		}

		// Token: 0x020004D8 RID: 1240
		[EngineStruct("rglTimer_type", false)]
		public enum TimeType
		{
			// Token: 0x04001B2E RID: 6958
			Application,
			// Token: 0x04001B2F RID: 6959
			Mission
		}
	}
}
