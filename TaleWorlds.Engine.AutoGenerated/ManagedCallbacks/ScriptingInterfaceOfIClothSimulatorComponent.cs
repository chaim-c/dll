using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.Engine;

namespace ManagedCallbacks
{
	// Token: 0x0200000B RID: 11
	internal class ScriptingInterfaceOfIClothSimulatorComponent : IClothSimulatorComponent
	{
		// Token: 0x0600007B RID: 123 RVA: 0x0000D187 File Offset: 0x0000B387
		public void SetMaxDistanceMultiplier(UIntPtr cloth_pointer, float multiplier)
		{
			ScriptingInterfaceOfIClothSimulatorComponent.call_SetMaxDistanceMultiplierDelegate(cloth_pointer, multiplier);
		}

		// Token: 0x04000026 RID: 38
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x04000027 RID: 39
		public static ScriptingInterfaceOfIClothSimulatorComponent.SetMaxDistanceMultiplierDelegate call_SetMaxDistanceMultiplierDelegate;

		// Token: 0x02000095 RID: 149
		// (Invoke) Token: 0x06000757 RID: 1879
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetMaxDistanceMultiplierDelegate(UIntPtr cloth_pointer, float multiplier);
	}
}
