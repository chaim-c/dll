using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;

namespace ManagedCallbacks
{
	// Token: 0x02000013 RID: 19
	internal class ScriptingInterfaceOfIHighlights : IHighlights
	{
		// Token: 0x060001BC RID: 444 RVA: 0x0000F620 File Offset: 0x0000D820
		public void AddHighlight(string id, string name)
		{
			byte[] array = null;
			if (id != null)
			{
				int byteCount = ScriptingInterfaceOfIHighlights._utf8.GetByteCount(id);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIHighlights._utf8.GetBytes(id, 0, id.Length, array, 0);
				array[byteCount] = 0;
			}
			byte[] array2 = null;
			if (name != null)
			{
				int byteCount2 = ScriptingInterfaceOfIHighlights._utf8.GetByteCount(name);
				array2 = ((byteCount2 < 1024) ? CallbackStringBufferManager.StringBuffer1 : new byte[byteCount2 + 1]);
				ScriptingInterfaceOfIHighlights._utf8.GetBytes(name, 0, name.Length, array2, 0);
				array2[byteCount2] = 0;
			}
			ScriptingInterfaceOfIHighlights.call_AddHighlightDelegate(array, array2);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000F6C0 File Offset: 0x0000D8C0
		public void CloseGroup(string id, bool destroy)
		{
			byte[] array = null;
			if (id != null)
			{
				int byteCount = ScriptingInterfaceOfIHighlights._utf8.GetByteCount(id);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIHighlights._utf8.GetBytes(id, 0, id.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIHighlights.call_CloseGroupDelegate(array, destroy);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000F71B File Offset: 0x0000D91B
		public void Initialize()
		{
			ScriptingInterfaceOfIHighlights.call_InitializeDelegate();
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000F728 File Offset: 0x0000D928
		public void OpenGroup(string id)
		{
			byte[] array = null;
			if (id != null)
			{
				int byteCount = ScriptingInterfaceOfIHighlights._utf8.GetByteCount(id);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIHighlights._utf8.GetBytes(id, 0, id.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIHighlights.call_OpenGroupDelegate(array);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000F784 File Offset: 0x0000D984
		public void OpenSummary(string groups)
		{
			byte[] array = null;
			if (groups != null)
			{
				int byteCount = ScriptingInterfaceOfIHighlights._utf8.GetByteCount(groups);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIHighlights._utf8.GetBytes(groups, 0, groups.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIHighlights.call_OpenSummaryDelegate(array);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000F7E0 File Offset: 0x0000D9E0
		public void RemoveHighlight(string id)
		{
			byte[] array = null;
			if (id != null)
			{
				int byteCount = ScriptingInterfaceOfIHighlights._utf8.GetByteCount(id);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIHighlights._utf8.GetBytes(id, 0, id.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIHighlights.call_RemoveHighlightDelegate(array);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000F83C File Offset: 0x0000DA3C
		public void SaveScreenshot(string highlightId, string groupId)
		{
			byte[] array = null;
			if (highlightId != null)
			{
				int byteCount = ScriptingInterfaceOfIHighlights._utf8.GetByteCount(highlightId);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIHighlights._utf8.GetBytes(highlightId, 0, highlightId.Length, array, 0);
				array[byteCount] = 0;
			}
			byte[] array2 = null;
			if (groupId != null)
			{
				int byteCount2 = ScriptingInterfaceOfIHighlights._utf8.GetByteCount(groupId);
				array2 = ((byteCount2 < 1024) ? CallbackStringBufferManager.StringBuffer1 : new byte[byteCount2 + 1]);
				ScriptingInterfaceOfIHighlights._utf8.GetBytes(groupId, 0, groupId.Length, array2, 0);
				array2[byteCount2] = 0;
			}
			ScriptingInterfaceOfIHighlights.call_SaveScreenshotDelegate(array, array2);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000F8DC File Offset: 0x0000DADC
		public void SaveVideo(string highlightId, string groupId, int startDelta, int endDelta)
		{
			byte[] array = null;
			if (highlightId != null)
			{
				int byteCount = ScriptingInterfaceOfIHighlights._utf8.GetByteCount(highlightId);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIHighlights._utf8.GetBytes(highlightId, 0, highlightId.Length, array, 0);
				array[byteCount] = 0;
			}
			byte[] array2 = null;
			if (groupId != null)
			{
				int byteCount2 = ScriptingInterfaceOfIHighlights._utf8.GetByteCount(groupId);
				array2 = ((byteCount2 < 1024) ? CallbackStringBufferManager.StringBuffer1 : new byte[byteCount2 + 1]);
				ScriptingInterfaceOfIHighlights._utf8.GetBytes(groupId, 0, groupId.Length, array2, 0);
				array2[byteCount2] = 0;
			}
			ScriptingInterfaceOfIHighlights.call_SaveVideoDelegate(array, array2, startDelta, endDelta);
		}

		// Token: 0x0400015E RID: 350
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x0400015F RID: 351
		public static ScriptingInterfaceOfIHighlights.AddHighlightDelegate call_AddHighlightDelegate;

		// Token: 0x04000160 RID: 352
		public static ScriptingInterfaceOfIHighlights.CloseGroupDelegate call_CloseGroupDelegate;

		// Token: 0x04000161 RID: 353
		public static ScriptingInterfaceOfIHighlights.InitializeDelegate call_InitializeDelegate;

		// Token: 0x04000162 RID: 354
		public static ScriptingInterfaceOfIHighlights.OpenGroupDelegate call_OpenGroupDelegate;

		// Token: 0x04000163 RID: 355
		public static ScriptingInterfaceOfIHighlights.OpenSummaryDelegate call_OpenSummaryDelegate;

		// Token: 0x04000164 RID: 356
		public static ScriptingInterfaceOfIHighlights.RemoveHighlightDelegate call_RemoveHighlightDelegate;

		// Token: 0x04000165 RID: 357
		public static ScriptingInterfaceOfIHighlights.SaveScreenshotDelegate call_SaveScreenshotDelegate;

		// Token: 0x04000166 RID: 358
		public static ScriptingInterfaceOfIHighlights.SaveVideoDelegate call_SaveVideoDelegate;

		// Token: 0x020001C5 RID: 453
		// (Invoke) Token: 0x06000C17 RID: 3095
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddHighlightDelegate(byte[] id, byte[] name);

		// Token: 0x020001C6 RID: 454
		// (Invoke) Token: 0x06000C1B RID: 3099
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void CloseGroupDelegate(byte[] id, [MarshalAs(UnmanagedType.U1)] bool destroy);

		// Token: 0x020001C7 RID: 455
		// (Invoke) Token: 0x06000C1F RID: 3103
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void InitializeDelegate();

		// Token: 0x020001C8 RID: 456
		// (Invoke) Token: 0x06000C23 RID: 3107
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void OpenGroupDelegate(byte[] id);

		// Token: 0x020001C9 RID: 457
		// (Invoke) Token: 0x06000C27 RID: 3111
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void OpenSummaryDelegate(byte[] groups);

		// Token: 0x020001CA RID: 458
		// (Invoke) Token: 0x06000C2B RID: 3115
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RemoveHighlightDelegate(byte[] id);

		// Token: 0x020001CB RID: 459
		// (Invoke) Token: 0x06000C2F RID: 3119
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SaveScreenshotDelegate(byte[] highlightId, byte[] groupId);

		// Token: 0x020001CC RID: 460
		// (Invoke) Token: 0x06000C33 RID: 3123
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SaveVideoDelegate(byte[] highlightId, byte[] groupId, int startDelta, int endDelta);
	}
}
