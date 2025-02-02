using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.MountAndBlade;

namespace ManagedCallbacks
{
	// Token: 0x02000021 RID: 33
	internal class ScriptingInterfaceOfIMBTestRun : IMBTestRun
	{
		// Token: 0x06000312 RID: 786 RVA: 0x0000C9E2 File Offset: 0x0000ABE2
		public int AutoContinue(int type)
		{
			return ScriptingInterfaceOfIMBTestRun.call_AutoContinueDelegate(type);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000C9EF File Offset: 0x0000ABEF
		public bool CloseScene()
		{
			return ScriptingInterfaceOfIMBTestRun.call_CloseSceneDelegate();
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000C9FB File Offset: 0x0000ABFB
		public bool EnterEditMode()
		{
			return ScriptingInterfaceOfIMBTestRun.call_EnterEditModeDelegate();
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000CA07 File Offset: 0x0000AC07
		public int GetFPS()
		{
			return ScriptingInterfaceOfIMBTestRun.call_GetFPSDelegate();
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000CA13 File Offset: 0x0000AC13
		public bool LeaveEditMode()
		{
			return ScriptingInterfaceOfIMBTestRun.call_LeaveEditModeDelegate();
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000CA1F File Offset: 0x0000AC1F
		public bool NewScene()
		{
			return ScriptingInterfaceOfIMBTestRun.call_NewSceneDelegate();
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000CA2B File Offset: 0x0000AC2B
		public bool OpenDefaultScene()
		{
			return ScriptingInterfaceOfIMBTestRun.call_OpenDefaultSceneDelegate();
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000CA38 File Offset: 0x0000AC38
		public bool OpenScene(string sceneName)
		{
			byte[] array = null;
			if (sceneName != null)
			{
				int byteCount = ScriptingInterfaceOfIMBTestRun._utf8.GetByteCount(sceneName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMBTestRun._utf8.GetBytes(sceneName, 0, sceneName.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfIMBTestRun.call_OpenSceneDelegate(array);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000CA92 File Offset: 0x0000AC92
		public bool SaveScene()
		{
			return ScriptingInterfaceOfIMBTestRun.call_SaveSceneDelegate();
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000CA9E File Offset: 0x0000AC9E
		public void StartMission()
		{
			ScriptingInterfaceOfIMBTestRun.call_StartMissionDelegate();
		}

		// Token: 0x04000293 RID: 659
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x04000294 RID: 660
		public static ScriptingInterfaceOfIMBTestRun.AutoContinueDelegate call_AutoContinueDelegate;

		// Token: 0x04000295 RID: 661
		public static ScriptingInterfaceOfIMBTestRun.CloseSceneDelegate call_CloseSceneDelegate;

		// Token: 0x04000296 RID: 662
		public static ScriptingInterfaceOfIMBTestRun.EnterEditModeDelegate call_EnterEditModeDelegate;

		// Token: 0x04000297 RID: 663
		public static ScriptingInterfaceOfIMBTestRun.GetFPSDelegate call_GetFPSDelegate;

		// Token: 0x04000298 RID: 664
		public static ScriptingInterfaceOfIMBTestRun.LeaveEditModeDelegate call_LeaveEditModeDelegate;

		// Token: 0x04000299 RID: 665
		public static ScriptingInterfaceOfIMBTestRun.NewSceneDelegate call_NewSceneDelegate;

		// Token: 0x0400029A RID: 666
		public static ScriptingInterfaceOfIMBTestRun.OpenDefaultSceneDelegate call_OpenDefaultSceneDelegate;

		// Token: 0x0400029B RID: 667
		public static ScriptingInterfaceOfIMBTestRun.OpenSceneDelegate call_OpenSceneDelegate;

		// Token: 0x0400029C RID: 668
		public static ScriptingInterfaceOfIMBTestRun.SaveSceneDelegate call_SaveSceneDelegate;

		// Token: 0x0400029D RID: 669
		public static ScriptingInterfaceOfIMBTestRun.StartMissionDelegate call_StartMissionDelegate;

		// Token: 0x020002E6 RID: 742
		// (Invoke) Token: 0x06000E3D RID: 3645
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int AutoContinueDelegate(int type);

		// Token: 0x020002E7 RID: 743
		// (Invoke) Token: 0x06000E41 RID: 3649
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool CloseSceneDelegate();

		// Token: 0x020002E8 RID: 744
		// (Invoke) Token: 0x06000E45 RID: 3653
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool EnterEditModeDelegate();

		// Token: 0x020002E9 RID: 745
		// (Invoke) Token: 0x06000E49 RID: 3657
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetFPSDelegate();

		// Token: 0x020002EA RID: 746
		// (Invoke) Token: 0x06000E4D RID: 3661
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool LeaveEditModeDelegate();

		// Token: 0x020002EB RID: 747
		// (Invoke) Token: 0x06000E51 RID: 3665
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool NewSceneDelegate();

		// Token: 0x020002EC RID: 748
		// (Invoke) Token: 0x06000E55 RID: 3669
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool OpenDefaultSceneDelegate();

		// Token: 0x020002ED RID: 749
		// (Invoke) Token: 0x06000E59 RID: 3673
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool OpenSceneDelegate(byte[] sceneName);

		// Token: 0x020002EE RID: 750
		// (Invoke) Token: 0x06000E5D RID: 3677
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool SaveSceneDelegate();

		// Token: 0x020002EF RID: 751
		// (Invoke) Token: 0x06000E61 RID: 3681
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void StartMissionDelegate();
	}
}
