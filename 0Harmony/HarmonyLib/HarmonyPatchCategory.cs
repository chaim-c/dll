using System;

namespace HarmonyLib
{
	// Token: 0x02000023 RID: 35
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class HarmonyPatchCategory : HarmonyAttribute
	{
		// Token: 0x060000C1 RID: 193 RVA: 0x000090E0 File Offset: 0x000072E0
		public HarmonyPatchCategory(string category)
		{
			this.info.category = category;
		}
	}
}
