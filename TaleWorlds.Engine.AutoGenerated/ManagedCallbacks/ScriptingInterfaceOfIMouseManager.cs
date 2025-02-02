using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;

namespace ManagedCallbacks
{
	// Token: 0x0200001C RID: 28
	internal class ScriptingInterfaceOfIMouseManager : IMouseManager
	{
		// Token: 0x060002FE RID: 766 RVA: 0x00011FF9 File Offset: 0x000101F9
		public void ActivateMouseCursor(int id)
		{
			ScriptingInterfaceOfIMouseManager.call_ActivateMouseCursorDelegate(id);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00012006 File Offset: 0x00010206
		public void LockCursorAtCurrentPosition(bool lockCursor)
		{
			ScriptingInterfaceOfIMouseManager.call_LockCursorAtCurrentPositionDelegate(lockCursor);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00012013 File Offset: 0x00010213
		public void LockCursorAtPosition(float x, float y)
		{
			ScriptingInterfaceOfIMouseManager.call_LockCursorAtPositionDelegate(x, y);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00012024 File Offset: 0x00010224
		public void SetMouseCursor(int id, string mousePath)
		{
			byte[] array = null;
			if (mousePath != null)
			{
				int byteCount = ScriptingInterfaceOfIMouseManager._utf8.GetByteCount(mousePath);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMouseManager._utf8.GetBytes(mousePath, 0, mousePath.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIMouseManager.call_SetMouseCursorDelegate(id, array);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0001207F File Offset: 0x0001027F
		public void ShowCursor(bool show)
		{
			ScriptingInterfaceOfIMouseManager.call_ShowCursorDelegate(show);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0001208C File Offset: 0x0001028C
		public void UnlockCursor()
		{
			ScriptingInterfaceOfIMouseManager.call_UnlockCursorDelegate();
		}

		// Token: 0x04000297 RID: 663
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x04000298 RID: 664
		public static ScriptingInterfaceOfIMouseManager.ActivateMouseCursorDelegate call_ActivateMouseCursorDelegate;

		// Token: 0x04000299 RID: 665
		public static ScriptingInterfaceOfIMouseManager.LockCursorAtCurrentPositionDelegate call_LockCursorAtCurrentPositionDelegate;

		// Token: 0x0400029A RID: 666
		public static ScriptingInterfaceOfIMouseManager.LockCursorAtPositionDelegate call_LockCursorAtPositionDelegate;

		// Token: 0x0400029B RID: 667
		public static ScriptingInterfaceOfIMouseManager.SetMouseCursorDelegate call_SetMouseCursorDelegate;

		// Token: 0x0400029C RID: 668
		public static ScriptingInterfaceOfIMouseManager.ShowCursorDelegate call_ShowCursorDelegate;

		// Token: 0x0400029D RID: 669
		public static ScriptingInterfaceOfIMouseManager.UnlockCursorDelegate call_UnlockCursorDelegate;

		// Token: 0x020002F5 RID: 757
		// (Invoke) Token: 0x060010D7 RID: 4311
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ActivateMouseCursorDelegate(int id);

		// Token: 0x020002F6 RID: 758
		// (Invoke) Token: 0x060010DB RID: 4315
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void LockCursorAtCurrentPositionDelegate([MarshalAs(UnmanagedType.U1)] bool lockCursor);

		// Token: 0x020002F7 RID: 759
		// (Invoke) Token: 0x060010DF RID: 4319
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void LockCursorAtPositionDelegate(float x, float y);

		// Token: 0x020002F8 RID: 760
		// (Invoke) Token: 0x060010E3 RID: 4323
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetMouseCursorDelegate(int id, byte[] mousePath);

		// Token: 0x020002F9 RID: 761
		// (Invoke) Token: 0x060010E7 RID: 4327
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ShowCursorDelegate([MarshalAs(UnmanagedType.U1)] bool show);

		// Token: 0x020002FA RID: 762
		// (Invoke) Token: 0x060010EB RID: 4331
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void UnlockCursorDelegate();
	}
}
