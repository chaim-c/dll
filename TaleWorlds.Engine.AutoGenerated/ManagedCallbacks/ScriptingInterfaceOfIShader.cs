using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;

namespace ManagedCallbacks
{
	// Token: 0x02000026 RID: 38
	internal class ScriptingInterfaceOfIShader : IShader
	{
		// Token: 0x06000470 RID: 1136 RVA: 0x00014ACC File Offset: 0x00012CCC
		public Shader GetFromResource(string shaderName)
		{
			byte[] array = null;
			if (shaderName != null)
			{
				int byteCount = ScriptingInterfaceOfIShader._utf8.GetByteCount(shaderName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIShader._utf8.GetBytes(shaderName, 0, shaderName.Length, array, 0);
				array[byteCount] = 0;
			}
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIShader.call_GetFromResourceDelegate(array);
			Shader result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new Shader(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00014B58 File Offset: 0x00012D58
		public ulong GetMaterialShaderFlagMask(UIntPtr shaderPointer, string flagName, bool showError)
		{
			byte[] array = null;
			if (flagName != null)
			{
				int byteCount = ScriptingInterfaceOfIShader._utf8.GetByteCount(flagName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIShader._utf8.GetBytes(flagName, 0, flagName.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfIShader.call_GetMaterialShaderFlagMaskDelegate(shaderPointer, array, showError);
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00014BB4 File Offset: 0x00012DB4
		public string GetName(UIntPtr shaderPointer)
		{
			if (ScriptingInterfaceOfIShader.call_GetNameDelegate(shaderPointer) != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00014BCB File Offset: 0x00012DCB
		public void Release(UIntPtr shaderPointer)
		{
			ScriptingInterfaceOfIShader.call_ReleaseDelegate(shaderPointer);
		}

		// Token: 0x040003FF RID: 1023
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x04000400 RID: 1024
		public static ScriptingInterfaceOfIShader.GetFromResourceDelegate call_GetFromResourceDelegate;

		// Token: 0x04000401 RID: 1025
		public static ScriptingInterfaceOfIShader.GetMaterialShaderFlagMaskDelegate call_GetMaterialShaderFlagMaskDelegate;

		// Token: 0x04000402 RID: 1026
		public static ScriptingInterfaceOfIShader.GetNameDelegate call_GetNameDelegate;

		// Token: 0x04000403 RID: 1027
		public static ScriptingInterfaceOfIShader.ReleaseDelegate call_ReleaseDelegate;

		// Token: 0x02000453 RID: 1107
		// (Invoke) Token: 0x0600164F RID: 5711
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer GetFromResourceDelegate(byte[] shaderName);

		// Token: 0x02000454 RID: 1108
		// (Invoke) Token: 0x06001653 RID: 5715
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate ulong GetMaterialShaderFlagMaskDelegate(UIntPtr shaderPointer, byte[] flagName, [MarshalAs(UnmanagedType.U1)] bool showError);

		// Token: 0x02000455 RID: 1109
		// (Invoke) Token: 0x06001657 RID: 5719
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetNameDelegate(UIntPtr shaderPointer);

		// Token: 0x02000456 RID: 1110
		// (Invoke) Token: 0x0600165B RID: 5723
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ReleaseDelegate(UIntPtr shaderPointer);
	}
}
