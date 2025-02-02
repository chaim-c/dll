using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.MountAndBlade;

namespace ManagedCallbacks
{
	// Token: 0x02000018 RID: 24
	internal class ScriptingInterfaceOfIMBMessageManager : IMBMessageManager
	{
		// Token: 0x0600023A RID: 570 RVA: 0x0000B2E0 File Offset: 0x000094E0
		public void DisplayMessage(string message)
		{
			byte[] array = null;
			if (message != null)
			{
				int byteCount = ScriptingInterfaceOfIMBMessageManager._utf8.GetByteCount(message);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMBMessageManager._utf8.GetBytes(message, 0, message.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIMBMessageManager.call_DisplayMessageDelegate(array);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000B33C File Offset: 0x0000953C
		public void DisplayMessageWithColor(string message, uint color)
		{
			byte[] array = null;
			if (message != null)
			{
				int byteCount = ScriptingInterfaceOfIMBMessageManager._utf8.GetByteCount(message);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMBMessageManager._utf8.GetBytes(message, 0, message.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIMBMessageManager.call_DisplayMessageWithColorDelegate(array, color);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000B397 File Offset: 0x00009597
		public void SetMessageManager(MessageManagerBase messageManager)
		{
			ScriptingInterfaceOfIMBMessageManager.call_SetMessageManagerDelegate((messageManager != null) ? messageManager.GetManagedId() : 0);
		}

		// Token: 0x040001C7 RID: 455
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x040001C8 RID: 456
		public static ScriptingInterfaceOfIMBMessageManager.DisplayMessageDelegate call_DisplayMessageDelegate;

		// Token: 0x040001C9 RID: 457
		public static ScriptingInterfaceOfIMBMessageManager.DisplayMessageWithColorDelegate call_DisplayMessageWithColorDelegate;

		// Token: 0x040001CA RID: 458
		public static ScriptingInterfaceOfIMBMessageManager.SetMessageManagerDelegate call_SetMessageManagerDelegate;

		// Token: 0x02000223 RID: 547
		// (Invoke) Token: 0x06000B31 RID: 2865
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void DisplayMessageDelegate(byte[] message);

		// Token: 0x02000224 RID: 548
		// (Invoke) Token: 0x06000B35 RID: 2869
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void DisplayMessageWithColorDelegate(byte[] message, uint color);

		// Token: 0x02000225 RID: 549
		// (Invoke) Token: 0x06000B39 RID: 2873
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetMessageManagerDelegate(int messageManager);
	}
}
