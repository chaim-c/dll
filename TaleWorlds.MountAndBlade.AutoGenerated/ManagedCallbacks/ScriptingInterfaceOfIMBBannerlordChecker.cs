using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.MountAndBlade;

namespace ManagedCallbacks
{
	// Token: 0x0200000D RID: 13
	internal class ScriptingInterfaceOfIMBBannerlordChecker : IMBBannerlordChecker
	{
		// Token: 0x060001C6 RID: 454 RVA: 0x0000A3BC File Offset: 0x000085BC
		public IntPtr GetEngineStructMemberOffset(string className, string memberName)
		{
			byte[] array = null;
			if (className != null)
			{
				int byteCount = ScriptingInterfaceOfIMBBannerlordChecker._utf8.GetByteCount(className);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMBBannerlordChecker._utf8.GetBytes(className, 0, className.Length, array, 0);
				array[byteCount] = 0;
			}
			byte[] array2 = null;
			if (memberName != null)
			{
				int byteCount2 = ScriptingInterfaceOfIMBBannerlordChecker._utf8.GetByteCount(memberName);
				array2 = ((byteCount2 < 1024) ? CallbackStringBufferManager.StringBuffer1 : new byte[byteCount2 + 1]);
				ScriptingInterfaceOfIMBBannerlordChecker._utf8.GetBytes(memberName, 0, memberName.Length, array2, 0);
				array2[byteCount2] = 0;
			}
			return ScriptingInterfaceOfIMBBannerlordChecker.call_GetEngineStructMemberOffsetDelegate(array, array2);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000A45C File Offset: 0x0000865C
		public int GetEngineStructSize(string str)
		{
			byte[] array = null;
			if (str != null)
			{
				int byteCount = ScriptingInterfaceOfIMBBannerlordChecker._utf8.GetByteCount(str);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMBBannerlordChecker._utf8.GetBytes(str, 0, str.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfIMBBannerlordChecker.call_GetEngineStructSizeDelegate(array);
		}

		// Token: 0x0400015F RID: 351
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x04000160 RID: 352
		public static ScriptingInterfaceOfIMBBannerlordChecker.GetEngineStructMemberOffsetDelegate call_GetEngineStructMemberOffsetDelegate;

		// Token: 0x04000161 RID: 353
		public static ScriptingInterfaceOfIMBBannerlordChecker.GetEngineStructSizeDelegate call_GetEngineStructSizeDelegate;

		// Token: 0x020001C6 RID: 454
		// (Invoke) Token: 0x060009BD RID: 2493
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate IntPtr GetEngineStructMemberOffsetDelegate(byte[] className, byte[] memberName);

		// Token: 0x020001C7 RID: 455
		// (Invoke) Token: 0x060009C1 RID: 2497
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetEngineStructSizeDelegate(byte[] str);
	}
}
