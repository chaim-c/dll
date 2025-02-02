using System;

namespace HarmonyLib
{
	// Token: 0x02000026 RID: 38
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
	public class HarmonyReversePatch : HarmonyAttribute
	{
		// Token: 0x060000E9 RID: 233 RVA: 0x0000954F File Offset: 0x0000774F
		public HarmonyReversePatch(HarmonyReversePatchType type = HarmonyReversePatchType.Original)
		{
			this.info.reversePatchType = new HarmonyReversePatchType?(type);
		}
	}
}
