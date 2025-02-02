using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.MountAndBlade;

namespace ManagedCallbacks
{
	// Token: 0x02000020 RID: 32
	internal class ScriptingInterfaceOfIMBTeam : IMBTeam
	{
		// Token: 0x0600030E RID: 782 RVA: 0x0000C9AE File Offset: 0x0000ABAE
		public bool IsEnemy(UIntPtr missionPointer, int teamIndex, int otherTeamIndex)
		{
			return ScriptingInterfaceOfIMBTeam.call_IsEnemyDelegate(missionPointer, teamIndex, otherTeamIndex);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000C9BD File Offset: 0x0000ABBD
		public void SetIsEnemy(UIntPtr missionPointer, int teamIndex, int otherTeamIndex, bool isEnemy)
		{
			ScriptingInterfaceOfIMBTeam.call_SetIsEnemyDelegate(missionPointer, teamIndex, otherTeamIndex, isEnemy);
		}

		// Token: 0x04000290 RID: 656
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x04000291 RID: 657
		public static ScriptingInterfaceOfIMBTeam.IsEnemyDelegate call_IsEnemyDelegate;

		// Token: 0x04000292 RID: 658
		public static ScriptingInterfaceOfIMBTeam.SetIsEnemyDelegate call_SetIsEnemyDelegate;

		// Token: 0x020002E4 RID: 740
		// (Invoke) Token: 0x06000E35 RID: 3637
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsEnemyDelegate(UIntPtr missionPointer, int teamIndex, int otherTeamIndex);

		// Token: 0x020002E5 RID: 741
		// (Invoke) Token: 0x06000E39 RID: 3641
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetIsEnemyDelegate(UIntPtr missionPointer, int teamIndex, int otherTeamIndex, [MarshalAs(UnmanagedType.U1)] bool isEnemy);
	}
}
