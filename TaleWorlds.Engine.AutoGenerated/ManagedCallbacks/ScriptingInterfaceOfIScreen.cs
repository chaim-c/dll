using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace ManagedCallbacks
{
	// Token: 0x02000024 RID: 36
	internal class ScriptingInterfaceOfIScreen : IScreen
	{
		// Token: 0x06000461 RID: 1121 RVA: 0x000149C3 File Offset: 0x00012BC3
		public float GetAspectRatio()
		{
			return ScriptingInterfaceOfIScreen.call_GetAspectRatioDelegate();
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x000149CF File Offset: 0x00012BCF
		public float GetDesktopHeight()
		{
			return ScriptingInterfaceOfIScreen.call_GetDesktopHeightDelegate();
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x000149DB File Offset: 0x00012BDB
		public float GetDesktopWidth()
		{
			return ScriptingInterfaceOfIScreen.call_GetDesktopWidthDelegate();
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x000149E7 File Offset: 0x00012BE7
		public bool GetMouseVisible()
		{
			return ScriptingInterfaceOfIScreen.call_GetMouseVisibleDelegate();
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x000149F3 File Offset: 0x00012BF3
		public float GetRealScreenResolutionHeight()
		{
			return ScriptingInterfaceOfIScreen.call_GetRealScreenResolutionHeightDelegate();
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x000149FF File Offset: 0x00012BFF
		public float GetRealScreenResolutionWidth()
		{
			return ScriptingInterfaceOfIScreen.call_GetRealScreenResolutionWidthDelegate();
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00014A0B File Offset: 0x00012C0B
		public Vec2 GetUsableAreaPercentages()
		{
			return ScriptingInterfaceOfIScreen.call_GetUsableAreaPercentagesDelegate();
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00014A17 File Offset: 0x00012C17
		public bool IsEnterButtonCross()
		{
			return ScriptingInterfaceOfIScreen.call_IsEnterButtonCrossDelegate();
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00014A23 File Offset: 0x00012C23
		public void SetMouseVisible(bool value)
		{
			ScriptingInterfaceOfIScreen.call_SetMouseVisibleDelegate(value);
		}

		// Token: 0x040003F2 RID: 1010
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x040003F3 RID: 1011
		public static ScriptingInterfaceOfIScreen.GetAspectRatioDelegate call_GetAspectRatioDelegate;

		// Token: 0x040003F4 RID: 1012
		public static ScriptingInterfaceOfIScreen.GetDesktopHeightDelegate call_GetDesktopHeightDelegate;

		// Token: 0x040003F5 RID: 1013
		public static ScriptingInterfaceOfIScreen.GetDesktopWidthDelegate call_GetDesktopWidthDelegate;

		// Token: 0x040003F6 RID: 1014
		public static ScriptingInterfaceOfIScreen.GetMouseVisibleDelegate call_GetMouseVisibleDelegate;

		// Token: 0x040003F7 RID: 1015
		public static ScriptingInterfaceOfIScreen.GetRealScreenResolutionHeightDelegate call_GetRealScreenResolutionHeightDelegate;

		// Token: 0x040003F8 RID: 1016
		public static ScriptingInterfaceOfIScreen.GetRealScreenResolutionWidthDelegate call_GetRealScreenResolutionWidthDelegate;

		// Token: 0x040003F9 RID: 1017
		public static ScriptingInterfaceOfIScreen.GetUsableAreaPercentagesDelegate call_GetUsableAreaPercentagesDelegate;

		// Token: 0x040003FA RID: 1018
		public static ScriptingInterfaceOfIScreen.IsEnterButtonCrossDelegate call_IsEnterButtonCrossDelegate;

		// Token: 0x040003FB RID: 1019
		public static ScriptingInterfaceOfIScreen.SetMouseVisibleDelegate call_SetMouseVisibleDelegate;

		// Token: 0x02000448 RID: 1096
		// (Invoke) Token: 0x06001623 RID: 5667
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetAspectRatioDelegate();

		// Token: 0x02000449 RID: 1097
		// (Invoke) Token: 0x06001627 RID: 5671
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetDesktopHeightDelegate();

		// Token: 0x0200044A RID: 1098
		// (Invoke) Token: 0x0600162B RID: 5675
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetDesktopWidthDelegate();

		// Token: 0x0200044B RID: 1099
		// (Invoke) Token: 0x0600162F RID: 5679
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool GetMouseVisibleDelegate();

		// Token: 0x0200044C RID: 1100
		// (Invoke) Token: 0x06001633 RID: 5683
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetRealScreenResolutionHeightDelegate();

		// Token: 0x0200044D RID: 1101
		// (Invoke) Token: 0x06001637 RID: 5687
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetRealScreenResolutionWidthDelegate();

		// Token: 0x0200044E RID: 1102
		// (Invoke) Token: 0x0600163B RID: 5691
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate Vec2 GetUsableAreaPercentagesDelegate();

		// Token: 0x0200044F RID: 1103
		// (Invoke) Token: 0x0600163F RID: 5695
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsEnterButtonCrossDelegate();

		// Token: 0x02000450 RID: 1104
		// (Invoke) Token: 0x06001643 RID: 5699
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetMouseVisibleDelegate([MarshalAs(UnmanagedType.U1)] bool value);
	}
}
