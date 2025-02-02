using System;

namespace HarmonyLib
{
	// Token: 0x02000029 RID: 41
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public class HarmonyBefore : HarmonyAttribute
	{
		// Token: 0x060000EC RID: 236 RVA: 0x00009584 File Offset: 0x00007784
		public HarmonyBefore(params string[] before)
		{
			this.info.before = before;
		}
	}
}
