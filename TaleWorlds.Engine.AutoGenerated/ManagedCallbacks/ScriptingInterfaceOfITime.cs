using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.Engine;

namespace ManagedCallbacks
{
	// Token: 0x0200002E RID: 46
	internal class ScriptingInterfaceOfITime : ITime
	{
		// Token: 0x0600051F RID: 1311 RVA: 0x0001675B File Offset: 0x0001495B
		public float GetApplicationTime()
		{
			return ScriptingInterfaceOfITime.call_GetApplicationTimeDelegate();
		}

		// Token: 0x040004A6 RID: 1190
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x040004A7 RID: 1191
		public static ScriptingInterfaceOfITime.GetApplicationTimeDelegate call_GetApplicationTimeDelegate;

		// Token: 0x020004F2 RID: 1266
		// (Invoke) Token: 0x060018CB RID: 6347
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetApplicationTimeDelegate();
	}
}
