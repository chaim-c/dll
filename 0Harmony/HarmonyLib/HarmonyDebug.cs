using System;

namespace HarmonyLib
{
	// Token: 0x0200002B RID: 43
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public class HarmonyDebug : HarmonyAttribute
	{
		// Token: 0x060000EE RID: 238 RVA: 0x000095AC File Offset: 0x000077AC
		public HarmonyDebug()
		{
			this.info.debug = new bool?(true);
		}
	}
}
