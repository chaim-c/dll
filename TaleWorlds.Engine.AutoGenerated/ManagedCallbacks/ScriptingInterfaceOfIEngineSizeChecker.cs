using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;

namespace ManagedCallbacks
{
	// Token: 0x02000010 RID: 16
	internal class ScriptingInterfaceOfIEngineSizeChecker : IEngineSizeChecker
	{
		// Token: 0x060000F9 RID: 249 RVA: 0x0000DFD8 File Offset: 0x0000C1D8
		public IntPtr GetEngineStructMemberOffset(string className, string memberName)
		{
			byte[] array = null;
			if (className != null)
			{
				int byteCount = ScriptingInterfaceOfIEngineSizeChecker._utf8.GetByteCount(className);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIEngineSizeChecker._utf8.GetBytes(className, 0, className.Length, array, 0);
				array[byteCount] = 0;
			}
			byte[] array2 = null;
			if (memberName != null)
			{
				int byteCount2 = ScriptingInterfaceOfIEngineSizeChecker._utf8.GetByteCount(memberName);
				array2 = ((byteCount2 < 1024) ? CallbackStringBufferManager.StringBuffer1 : new byte[byteCount2 + 1]);
				ScriptingInterfaceOfIEngineSizeChecker._utf8.GetBytes(memberName, 0, memberName.Length, array2, 0);
				array2[byteCount2] = 0;
			}
			return ScriptingInterfaceOfIEngineSizeChecker.call_GetEngineStructMemberOffsetDelegate(array, array2);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000E078 File Offset: 0x0000C278
		public int GetEngineStructSize(string str)
		{
			byte[] array = null;
			if (str != null)
			{
				int byteCount = ScriptingInterfaceOfIEngineSizeChecker._utf8.GetByteCount(str);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIEngineSizeChecker._utf8.GetBytes(str, 0, str.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfIEngineSizeChecker.call_GetEngineStructSizeDelegate(array);
		}

		// Token: 0x0400009F RID: 159
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x040000A0 RID: 160
		public static ScriptingInterfaceOfIEngineSizeChecker.GetEngineStructMemberOffsetDelegate call_GetEngineStructMemberOffsetDelegate;

		// Token: 0x040000A1 RID: 161
		public static ScriptingInterfaceOfIEngineSizeChecker.GetEngineStructSizeDelegate call_GetEngineStructSizeDelegate;

		// Token: 0x02000109 RID: 265
		// (Invoke) Token: 0x06000927 RID: 2343
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate IntPtr GetEngineStructMemberOffsetDelegate(byte[] className, byte[] memberName);

		// Token: 0x0200010A RID: 266
		// (Invoke) Token: 0x0600092B RID: 2347
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetEngineStructSizeDelegate(byte[] str);
	}
}
