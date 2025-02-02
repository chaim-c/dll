using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace ManagedCallbacks
{
	// Token: 0x02000010 RID: 16
	internal class ScriptingInterfaceOfIMBDebugExtensions : IMBDebugExtensions
	{
		// Token: 0x060001D2 RID: 466 RVA: 0x0000A578 File Offset: 0x00008778
		public void OverrideNativeParameter(string paramName, float value)
		{
			byte[] array = null;
			if (paramName != null)
			{
				int byteCount = ScriptingInterfaceOfIMBDebugExtensions._utf8.GetByteCount(paramName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMBDebugExtensions._utf8.GetBytes(paramName, 0, paramName.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIMBDebugExtensions.call_OverrideNativeParameterDelegate(array, value);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000A5D3 File Offset: 0x000087D3
		public void ReloadNativeParameters()
		{
			ScriptingInterfaceOfIMBDebugExtensions.call_ReloadNativeParametersDelegate();
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000A5E0 File Offset: 0x000087E0
		public void RenderDebugArcOnTerrain(UIntPtr scenePointer, ref MatrixFrame frame, float radius, float beginAngle, float endAngle, uint color, bool depthCheck, bool isDotted)
		{
			ScriptingInterfaceOfIMBDebugExtensions.call_RenderDebugArcOnTerrainDelegate(scenePointer, ref frame, radius, beginAngle, endAngle, color, depthCheck, isDotted);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000A604 File Offset: 0x00008804
		public void RenderDebugCircleOnTerrain(UIntPtr scenePointer, ref MatrixFrame frame, float radius, uint color, bool depthCheck, bool isDotted)
		{
			ScriptingInterfaceOfIMBDebugExtensions.call_RenderDebugCircleOnTerrainDelegate(scenePointer, ref frame, radius, color, depthCheck, isDotted);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000A61C File Offset: 0x0000881C
		public void RenderDebugLineOnTerrain(UIntPtr scenePointer, Vec3 position, Vec3 direction, uint color, bool depthCheck, float time, bool isDotted, float pointDensity)
		{
			ScriptingInterfaceOfIMBDebugExtensions.call_RenderDebugLineOnTerrainDelegate(scenePointer, position, direction, color, depthCheck, time, isDotted, pointDensity);
		}

		// Token: 0x04000168 RID: 360
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x04000169 RID: 361
		public static ScriptingInterfaceOfIMBDebugExtensions.OverrideNativeParameterDelegate call_OverrideNativeParameterDelegate;

		// Token: 0x0400016A RID: 362
		public static ScriptingInterfaceOfIMBDebugExtensions.ReloadNativeParametersDelegate call_ReloadNativeParametersDelegate;

		// Token: 0x0400016B RID: 363
		public static ScriptingInterfaceOfIMBDebugExtensions.RenderDebugArcOnTerrainDelegate call_RenderDebugArcOnTerrainDelegate;

		// Token: 0x0400016C RID: 364
		public static ScriptingInterfaceOfIMBDebugExtensions.RenderDebugCircleOnTerrainDelegate call_RenderDebugCircleOnTerrainDelegate;

		// Token: 0x0400016D RID: 365
		public static ScriptingInterfaceOfIMBDebugExtensions.RenderDebugLineOnTerrainDelegate call_RenderDebugLineOnTerrainDelegate;

		// Token: 0x020001CC RID: 460
		// (Invoke) Token: 0x060009D5 RID: 2517
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void OverrideNativeParameterDelegate(byte[] paramName, float value);

		// Token: 0x020001CD RID: 461
		// (Invoke) Token: 0x060009D9 RID: 2521
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ReloadNativeParametersDelegate();

		// Token: 0x020001CE RID: 462
		// (Invoke) Token: 0x060009DD RID: 2525
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RenderDebugArcOnTerrainDelegate(UIntPtr scenePointer, ref MatrixFrame frame, float radius, float beginAngle, float endAngle, uint color, [MarshalAs(UnmanagedType.U1)] bool depthCheck, [MarshalAs(UnmanagedType.U1)] bool isDotted);

		// Token: 0x020001CF RID: 463
		// (Invoke) Token: 0x060009E1 RID: 2529
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RenderDebugCircleOnTerrainDelegate(UIntPtr scenePointer, ref MatrixFrame frame, float radius, uint color, [MarshalAs(UnmanagedType.U1)] bool depthCheck, [MarshalAs(UnmanagedType.U1)] bool isDotted);

		// Token: 0x020001D0 RID: 464
		// (Invoke) Token: 0x060009E5 RID: 2533
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RenderDebugLineOnTerrainDelegate(UIntPtr scenePointer, Vec3 position, Vec3 direction, uint color, [MarshalAs(UnmanagedType.U1)] bool depthCheck, float time, [MarshalAs(UnmanagedType.U1)] bool isDotted, float pointDensity);
	}
}
