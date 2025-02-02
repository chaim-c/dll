using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;

namespace ManagedCallbacks
{
	// Token: 0x02000032 RID: 50
	internal class ScriptingInterfaceOfIView : IView
	{
		// Token: 0x060005BD RID: 1469 RVA: 0x00018389 File Offset: 0x00016589
		public void SetAutoDepthTargetCreation(UIntPtr ptr, bool value)
		{
			ScriptingInterfaceOfIView.call_SetAutoDepthTargetCreationDelegate(ptr, value);
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00018397 File Offset: 0x00016597
		public void SetClearColor(UIntPtr ptr, uint rgba)
		{
			ScriptingInterfaceOfIView.call_SetClearColorDelegate(ptr, rgba);
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x000183A5 File Offset: 0x000165A5
		public void SetDebugRenderFunctionality(UIntPtr ptr, bool value)
		{
			ScriptingInterfaceOfIView.call_SetDebugRenderFunctionalityDelegate(ptr, value);
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x000183B3 File Offset: 0x000165B3
		public void SetDepthTarget(UIntPtr ptr, UIntPtr texture_ptr)
		{
			ScriptingInterfaceOfIView.call_SetDepthTargetDelegate(ptr, texture_ptr);
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x000183C1 File Offset: 0x000165C1
		public void SetEnable(UIntPtr ptr, bool value)
		{
			ScriptingInterfaceOfIView.call_SetEnableDelegate(ptr, value);
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x000183D0 File Offset: 0x000165D0
		public void SetFileNameToSaveResult(UIntPtr ptr, string name)
		{
			byte[] array = null;
			if (name != null)
			{
				int byteCount = ScriptingInterfaceOfIView._utf8.GetByteCount(name);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIView._utf8.GetBytes(name, 0, name.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIView.call_SetFileNameToSaveResultDelegate(ptr, array);
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0001842C File Offset: 0x0001662C
		public void SetFilePathToSaveResult(UIntPtr ptr, string name)
		{
			byte[] array = null;
			if (name != null)
			{
				int byteCount = ScriptingInterfaceOfIView._utf8.GetByteCount(name);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIView._utf8.GetBytes(name, 0, name.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIView.call_SetFilePathToSaveResultDelegate(ptr, array);
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00018487 File Offset: 0x00016687
		public void SetFileTypeToSave(UIntPtr ptr, int type)
		{
			ScriptingInterfaceOfIView.call_SetFileTypeToSaveDelegate(ptr, type);
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00018495 File Offset: 0x00016695
		public void SetOffset(UIntPtr ptr, float x, float y)
		{
			ScriptingInterfaceOfIView.call_SetOffsetDelegate(ptr, x, y);
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x000184A4 File Offset: 0x000166A4
		public void SetRenderOnDemand(UIntPtr ptr, bool value)
		{
			ScriptingInterfaceOfIView.call_SetRenderOnDemandDelegate(ptr, value);
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x000184B2 File Offset: 0x000166B2
		public void SetRenderOption(UIntPtr ptr, int optionEnum, bool value)
		{
			ScriptingInterfaceOfIView.call_SetRenderOptionDelegate(ptr, optionEnum, value);
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x000184C1 File Offset: 0x000166C1
		public void SetRenderOrder(UIntPtr ptr, int value)
		{
			ScriptingInterfaceOfIView.call_SetRenderOrderDelegate(ptr, value);
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x000184CF File Offset: 0x000166CF
		public void SetRenderTarget(UIntPtr ptr, UIntPtr texture_ptr)
		{
			ScriptingInterfaceOfIView.call_SetRenderTargetDelegate(ptr, texture_ptr);
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x000184DD File Offset: 0x000166DD
		public void SetSaveFinalResultToDisk(UIntPtr ptr, bool value)
		{
			ScriptingInterfaceOfIView.call_SetSaveFinalResultToDiskDelegate(ptr, value);
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x000184EB File Offset: 0x000166EB
		public void SetScale(UIntPtr ptr, float x, float y)
		{
			ScriptingInterfaceOfIView.call_SetScaleDelegate(ptr, x, y);
		}

		// Token: 0x04000540 RID: 1344
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x04000541 RID: 1345
		public static ScriptingInterfaceOfIView.SetAutoDepthTargetCreationDelegate call_SetAutoDepthTargetCreationDelegate;

		// Token: 0x04000542 RID: 1346
		public static ScriptingInterfaceOfIView.SetClearColorDelegate call_SetClearColorDelegate;

		// Token: 0x04000543 RID: 1347
		public static ScriptingInterfaceOfIView.SetDebugRenderFunctionalityDelegate call_SetDebugRenderFunctionalityDelegate;

		// Token: 0x04000544 RID: 1348
		public static ScriptingInterfaceOfIView.SetDepthTargetDelegate call_SetDepthTargetDelegate;

		// Token: 0x04000545 RID: 1349
		public static ScriptingInterfaceOfIView.SetEnableDelegate call_SetEnableDelegate;

		// Token: 0x04000546 RID: 1350
		public static ScriptingInterfaceOfIView.SetFileNameToSaveResultDelegate call_SetFileNameToSaveResultDelegate;

		// Token: 0x04000547 RID: 1351
		public static ScriptingInterfaceOfIView.SetFilePathToSaveResultDelegate call_SetFilePathToSaveResultDelegate;

		// Token: 0x04000548 RID: 1352
		public static ScriptingInterfaceOfIView.SetFileTypeToSaveDelegate call_SetFileTypeToSaveDelegate;

		// Token: 0x04000549 RID: 1353
		public static ScriptingInterfaceOfIView.SetOffsetDelegate call_SetOffsetDelegate;

		// Token: 0x0400054A RID: 1354
		public static ScriptingInterfaceOfIView.SetRenderOnDemandDelegate call_SetRenderOnDemandDelegate;

		// Token: 0x0400054B RID: 1355
		public static ScriptingInterfaceOfIView.SetRenderOptionDelegate call_SetRenderOptionDelegate;

		// Token: 0x0400054C RID: 1356
		public static ScriptingInterfaceOfIView.SetRenderOrderDelegate call_SetRenderOrderDelegate;

		// Token: 0x0400054D RID: 1357
		public static ScriptingInterfaceOfIView.SetRenderTargetDelegate call_SetRenderTargetDelegate;

		// Token: 0x0400054E RID: 1358
		public static ScriptingInterfaceOfIView.SetSaveFinalResultToDiskDelegate call_SetSaveFinalResultToDiskDelegate;

		// Token: 0x0400054F RID: 1359
		public static ScriptingInterfaceOfIView.SetScaleDelegate call_SetScaleDelegate;

		// Token: 0x02000588 RID: 1416
		// (Invoke) Token: 0x06001B23 RID: 6947
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetAutoDepthTargetCreationDelegate(UIntPtr ptr, [MarshalAs(UnmanagedType.U1)] bool value);

		// Token: 0x02000589 RID: 1417
		// (Invoke) Token: 0x06001B27 RID: 6951
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetClearColorDelegate(UIntPtr ptr, uint rgba);

		// Token: 0x0200058A RID: 1418
		// (Invoke) Token: 0x06001B2B RID: 6955
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetDebugRenderFunctionalityDelegate(UIntPtr ptr, [MarshalAs(UnmanagedType.U1)] bool value);

		// Token: 0x0200058B RID: 1419
		// (Invoke) Token: 0x06001B2F RID: 6959
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetDepthTargetDelegate(UIntPtr ptr, UIntPtr texture_ptr);

		// Token: 0x0200058C RID: 1420
		// (Invoke) Token: 0x06001B33 RID: 6963
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetEnableDelegate(UIntPtr ptr, [MarshalAs(UnmanagedType.U1)] bool value);

		// Token: 0x0200058D RID: 1421
		// (Invoke) Token: 0x06001B37 RID: 6967
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetFileNameToSaveResultDelegate(UIntPtr ptr, byte[] name);

		// Token: 0x0200058E RID: 1422
		// (Invoke) Token: 0x06001B3B RID: 6971
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetFilePathToSaveResultDelegate(UIntPtr ptr, byte[] name);

		// Token: 0x0200058F RID: 1423
		// (Invoke) Token: 0x06001B3F RID: 6975
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetFileTypeToSaveDelegate(UIntPtr ptr, int type);

		// Token: 0x02000590 RID: 1424
		// (Invoke) Token: 0x06001B43 RID: 6979
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetOffsetDelegate(UIntPtr ptr, float x, float y);

		// Token: 0x02000591 RID: 1425
		// (Invoke) Token: 0x06001B47 RID: 6983
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetRenderOnDemandDelegate(UIntPtr ptr, [MarshalAs(UnmanagedType.U1)] bool value);

		// Token: 0x02000592 RID: 1426
		// (Invoke) Token: 0x06001B4B RID: 6987
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetRenderOptionDelegate(UIntPtr ptr, int optionEnum, [MarshalAs(UnmanagedType.U1)] bool value);

		// Token: 0x02000593 RID: 1427
		// (Invoke) Token: 0x06001B4F RID: 6991
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetRenderOrderDelegate(UIntPtr ptr, int value);

		// Token: 0x02000594 RID: 1428
		// (Invoke) Token: 0x06001B53 RID: 6995
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetRenderTargetDelegate(UIntPtr ptr, UIntPtr texture_ptr);

		// Token: 0x02000595 RID: 1429
		// (Invoke) Token: 0x06001B57 RID: 6999
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetSaveFinalResultToDiskDelegate(UIntPtr ptr, [MarshalAs(UnmanagedType.U1)] bool value);

		// Token: 0x02000596 RID: 1430
		// (Invoke) Token: 0x06001B5B RID: 7003
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetScaleDelegate(UIntPtr ptr, float x, float y);
	}
}
