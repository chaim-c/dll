using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.MountAndBlade;

namespace ManagedCallbacks
{
	// Token: 0x0200001D RID: 29
	internal class ScriptingInterfaceOfIMBScreen : IMBScreen
	{
		// Token: 0x060002F1 RID: 753 RVA: 0x0000C4D1 File Offset: 0x0000A6D1
		public void OnEditModeEnterPress()
		{
			ScriptingInterfaceOfIMBScreen.call_OnEditModeEnterPressDelegate();
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000C4DD File Offset: 0x0000A6DD
		public void OnEditModeEnterRelease()
		{
			ScriptingInterfaceOfIMBScreen.call_OnEditModeEnterReleaseDelegate();
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000C4E9 File Offset: 0x0000A6E9
		public void OnExitButtonClick()
		{
			ScriptingInterfaceOfIMBScreen.call_OnExitButtonClickDelegate();
		}

		// Token: 0x04000276 RID: 630
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x04000277 RID: 631
		public static ScriptingInterfaceOfIMBScreen.OnEditModeEnterPressDelegate call_OnEditModeEnterPressDelegate;

		// Token: 0x04000278 RID: 632
		public static ScriptingInterfaceOfIMBScreen.OnEditModeEnterReleaseDelegate call_OnEditModeEnterReleaseDelegate;

		// Token: 0x04000279 RID: 633
		public static ScriptingInterfaceOfIMBScreen.OnExitButtonClickDelegate call_OnExitButtonClickDelegate;

		// Token: 0x020002CD RID: 717
		// (Invoke) Token: 0x06000DD9 RID: 3545
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void OnEditModeEnterPressDelegate();

		// Token: 0x020002CE RID: 718
		// (Invoke) Token: 0x06000DDD RID: 3549
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void OnEditModeEnterReleaseDelegate();

		// Token: 0x020002CF RID: 719
		// (Invoke) Token: 0x06000DE1 RID: 3553
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void OnExitButtonClickDelegate();
	}
}
