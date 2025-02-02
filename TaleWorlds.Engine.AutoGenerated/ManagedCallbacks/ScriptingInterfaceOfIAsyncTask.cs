using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;

namespace ManagedCallbacks
{
	// Token: 0x02000008 RID: 8
	internal class ScriptingInterfaceOfIAsyncTask : IAsyncTask
	{
		// Token: 0x06000054 RID: 84 RVA: 0x0000CE40 File Offset: 0x0000B040
		public AsyncTask CreateWithDelegate(ManagedDelegate function, bool isBackground)
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIAsyncTask.call_CreateWithDelegateDelegate((function != null) ? function.GetManagedId() : 0, isBackground);
			AsyncTask result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new AsyncTask(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000CE96 File Offset: 0x0000B096
		public void Invoke(UIntPtr Pointer)
		{
			ScriptingInterfaceOfIAsyncTask.call_InvokeDelegate(Pointer);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000CEA3 File Offset: 0x0000B0A3
		public void Wait(UIntPtr Pointer)
		{
			ScriptingInterfaceOfIAsyncTask.call_WaitDelegate(Pointer);
		}

		// Token: 0x04000002 RID: 2
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x04000003 RID: 3
		public static ScriptingInterfaceOfIAsyncTask.CreateWithDelegateDelegate call_CreateWithDelegateDelegate;

		// Token: 0x04000004 RID: 4
		public static ScriptingInterfaceOfIAsyncTask.InvokeDelegate call_InvokeDelegate;

		// Token: 0x04000005 RID: 5
		public static ScriptingInterfaceOfIAsyncTask.WaitDelegate call_WaitDelegate;

		// Token: 0x02000074 RID: 116
		// (Invoke) Token: 0x060006D3 RID: 1747
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer CreateWithDelegateDelegate(int function, [MarshalAs(UnmanagedType.U1)] bool isBackground);

		// Token: 0x02000075 RID: 117
		// (Invoke) Token: 0x060006D7 RID: 1751
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void InvokeDelegate(UIntPtr Pointer);

		// Token: 0x02000076 RID: 118
		// (Invoke) Token: 0x060006DB RID: 1755
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void WaitDelegate(UIntPtr Pointer);
	}
}
