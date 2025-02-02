using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;

namespace ManagedCallbacks
{
	// Token: 0x0200002A RID: 42
	internal class ScriptingInterfaceOfITableauView : ITableauView
	{
		// Token: 0x060004EE RID: 1262 RVA: 0x00015DBC File Offset: 0x00013FBC
		public TableauView CreateTableauView()
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfITableauView.call_CreateTableauViewDelegate();
			TableauView result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new TableauView(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00015E05 File Offset: 0x00014005
		public void SetContinousRendering(UIntPtr pointer, bool value)
		{
			ScriptingInterfaceOfITableauView.call_SetContinousRenderingDelegate(pointer, value);
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00015E13 File Offset: 0x00014013
		public void SetDeleteAfterRendering(UIntPtr pointer, bool value)
		{
			ScriptingInterfaceOfITableauView.call_SetDeleteAfterRenderingDelegate(pointer, value);
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00015E21 File Offset: 0x00014021
		public void SetDoNotRenderThisFrame(UIntPtr pointer, bool value)
		{
			ScriptingInterfaceOfITableauView.call_SetDoNotRenderThisFrameDelegate(pointer, value);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00015E2F File Offset: 0x0001402F
		public void SetSortingEnabled(UIntPtr pointer, bool value)
		{
			ScriptingInterfaceOfITableauView.call_SetSortingEnabledDelegate(pointer, value);
		}

		// Token: 0x04000479 RID: 1145
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x0400047A RID: 1146
		public static ScriptingInterfaceOfITableauView.CreateTableauViewDelegate call_CreateTableauViewDelegate;

		// Token: 0x0400047B RID: 1147
		public static ScriptingInterfaceOfITableauView.SetContinousRenderingDelegate call_SetContinousRenderingDelegate;

		// Token: 0x0400047C RID: 1148
		public static ScriptingInterfaceOfITableauView.SetDeleteAfterRenderingDelegate call_SetDeleteAfterRenderingDelegate;

		// Token: 0x0400047D RID: 1149
		public static ScriptingInterfaceOfITableauView.SetDoNotRenderThisFrameDelegate call_SetDoNotRenderThisFrameDelegate;

		// Token: 0x0400047E RID: 1150
		public static ScriptingInterfaceOfITableauView.SetSortingEnabledDelegate call_SetSortingEnabledDelegate;

		// Token: 0x020004C9 RID: 1225
		// (Invoke) Token: 0x06001827 RID: 6183
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer CreateTableauViewDelegate();

		// Token: 0x020004CA RID: 1226
		// (Invoke) Token: 0x0600182B RID: 6187
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetContinousRenderingDelegate(UIntPtr pointer, [MarshalAs(UnmanagedType.U1)] bool value);

		// Token: 0x020004CB RID: 1227
		// (Invoke) Token: 0x0600182F RID: 6191
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetDeleteAfterRenderingDelegate(UIntPtr pointer, [MarshalAs(UnmanagedType.U1)] bool value);

		// Token: 0x020004CC RID: 1228
		// (Invoke) Token: 0x06001833 RID: 6195
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetDoNotRenderThisFrameDelegate(UIntPtr pointer, [MarshalAs(UnmanagedType.U1)] bool value);

		// Token: 0x020004CD RID: 1229
		// (Invoke) Token: 0x06001837 RID: 6199
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetSortingEnabledDelegate(UIntPtr pointer, [MarshalAs(UnmanagedType.U1)] bool value);
	}
}
