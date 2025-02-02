using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.MountAndBlade;

namespace ManagedCallbacks
{
	// Token: 0x02000014 RID: 20
	internal class ScriptingInterfaceOfIMBGame : IMBGame
	{
		// Token: 0x0600020F RID: 527 RVA: 0x0000ACA4 File Offset: 0x00008EA4
		public void LoadModuleData(bool isLoadGame)
		{
			ScriptingInterfaceOfIMBGame.call_LoadModuleDataDelegate(isLoadGame);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000ACB1 File Offset: 0x00008EB1
		public void StartNew()
		{
			ScriptingInterfaceOfIMBGame.call_StartNewDelegate();
		}

		// Token: 0x040001A1 RID: 417
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x040001A2 RID: 418
		public static ScriptingInterfaceOfIMBGame.LoadModuleDataDelegate call_LoadModuleDataDelegate;

		// Token: 0x040001A3 RID: 419
		public static ScriptingInterfaceOfIMBGame.StartNewDelegate call_StartNewDelegate;

		// Token: 0x02000201 RID: 513
		// (Invoke) Token: 0x06000AA9 RID: 2729
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void LoadModuleDataDelegate([MarshalAs(UnmanagedType.U1)] bool isLoadGame);

		// Token: 0x02000202 RID: 514
		// (Invoke) Token: 0x06000AAD RID: 2733
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void StartNewDelegate();
	}
}
