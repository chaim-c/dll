using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.MountAndBlade;

namespace ManagedCallbacks
{
	// Token: 0x0200000E RID: 14
	internal class ScriptingInterfaceOfIMBBannerlordConfig : IMBBannerlordConfig
	{
		// Token: 0x060001CA RID: 458 RVA: 0x0000A4CA File Offset: 0x000086CA
		public void ValidateOptions()
		{
			ScriptingInterfaceOfIMBBannerlordConfig.call_ValidateOptionsDelegate();
		}

		// Token: 0x04000162 RID: 354
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x04000163 RID: 355
		public static ScriptingInterfaceOfIMBBannerlordConfig.ValidateOptionsDelegate call_ValidateOptionsDelegate;

		// Token: 0x020001C8 RID: 456
		// (Invoke) Token: 0x060009C5 RID: 2501
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ValidateOptionsDelegate();
	}
}
