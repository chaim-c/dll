using System;

namespace HarmonyLib
{
	// Token: 0x02000028 RID: 40
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public class HarmonyPriority : HarmonyAttribute
	{
		// Token: 0x060000EB RID: 235 RVA: 0x00009570 File Offset: 0x00007770
		public HarmonyPriority(int priority)
		{
			this.info.priority = priority;
		}
	}
}
