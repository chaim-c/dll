using System;

namespace HarmonyLib
{
	// Token: 0x0200002A RID: 42
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public class HarmonyAfter : HarmonyAttribute
	{
		// Token: 0x060000ED RID: 237 RVA: 0x00009598 File Offset: 0x00007798
		public HarmonyAfter(params string[] after)
		{
			this.info.after = after;
		}
	}
}
