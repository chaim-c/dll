using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace ManagedCallbacks
{
	// Token: 0x02000023 RID: 35
	internal class ScriptingInterfaceOfIMBWindowManager : IMBWindowManager
	{
		// Token: 0x06000323 RID: 803 RVA: 0x0000CBFE File Offset: 0x0000ADFE
		public void DontChangeCursorPos()
		{
			ScriptingInterfaceOfIMBWindowManager.call_DontChangeCursorPosDelegate();
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000CC0A File Offset: 0x0000AE0A
		public void EraseMessageLines()
		{
			ScriptingInterfaceOfIMBWindowManager.call_EraseMessageLinesDelegate();
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000CC16 File Offset: 0x0000AE16
		public void PreDisplay()
		{
			ScriptingInterfaceOfIMBWindowManager.call_PreDisplayDelegate();
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000CC22 File Offset: 0x0000AE22
		public void ScreenToWorld(UIntPtr pointer, float screenX, float screenY, float z, ref Vec3 worldSpacePosition)
		{
			ScriptingInterfaceOfIMBWindowManager.call_ScreenToWorldDelegate(pointer, screenX, screenY, z, ref worldSpacePosition);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000CC35 File Offset: 0x0000AE35
		public float WorldToScreen(UIntPtr cameraPointer, Vec3 worldSpacePosition, ref float screenX, ref float screenY, ref float w)
		{
			return ScriptingInterfaceOfIMBWindowManager.call_WorldToScreenDelegate(cameraPointer, worldSpacePosition, ref screenX, ref screenY, ref w);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000CC48 File Offset: 0x0000AE48
		public float WorldToScreenWithFixedZ(UIntPtr cameraPointer, Vec3 cameraPosition, Vec3 worldSpacePosition, ref float screenX, ref float screenY, ref float w)
		{
			return ScriptingInterfaceOfIMBWindowManager.call_WorldToScreenWithFixedZDelegate(cameraPointer, cameraPosition, worldSpacePosition, ref screenX, ref screenY, ref w);
		}

		// Token: 0x040002A2 RID: 674
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x040002A3 RID: 675
		public static ScriptingInterfaceOfIMBWindowManager.DontChangeCursorPosDelegate call_DontChangeCursorPosDelegate;

		// Token: 0x040002A4 RID: 676
		public static ScriptingInterfaceOfIMBWindowManager.EraseMessageLinesDelegate call_EraseMessageLinesDelegate;

		// Token: 0x040002A5 RID: 677
		public static ScriptingInterfaceOfIMBWindowManager.PreDisplayDelegate call_PreDisplayDelegate;

		// Token: 0x040002A6 RID: 678
		public static ScriptingInterfaceOfIMBWindowManager.ScreenToWorldDelegate call_ScreenToWorldDelegate;

		// Token: 0x040002A7 RID: 679
		public static ScriptingInterfaceOfIMBWindowManager.WorldToScreenDelegate call_WorldToScreenDelegate;

		// Token: 0x040002A8 RID: 680
		public static ScriptingInterfaceOfIMBWindowManager.WorldToScreenWithFixedZDelegate call_WorldToScreenWithFixedZDelegate;

		// Token: 0x020002F3 RID: 755
		// (Invoke) Token: 0x06000E71 RID: 3697
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void DontChangeCursorPosDelegate();

		// Token: 0x020002F4 RID: 756
		// (Invoke) Token: 0x06000E75 RID: 3701
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void EraseMessageLinesDelegate();

		// Token: 0x020002F5 RID: 757
		// (Invoke) Token: 0x06000E79 RID: 3705
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void PreDisplayDelegate();

		// Token: 0x020002F6 RID: 758
		// (Invoke) Token: 0x06000E7D RID: 3709
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ScreenToWorldDelegate(UIntPtr pointer, float screenX, float screenY, float z, ref Vec3 worldSpacePosition);

		// Token: 0x020002F7 RID: 759
		// (Invoke) Token: 0x06000E81 RID: 3713
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float WorldToScreenDelegate(UIntPtr cameraPointer, Vec3 worldSpacePosition, ref float screenX, ref float screenY, ref float w);

		// Token: 0x020002F8 RID: 760
		// (Invoke) Token: 0x06000E85 RID: 3717
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float WorldToScreenWithFixedZDelegate(UIntPtr cameraPointer, Vec3 cameraPosition, Vec3 worldSpacePosition, ref float screenX, ref float screenY, ref float w);
	}
}
