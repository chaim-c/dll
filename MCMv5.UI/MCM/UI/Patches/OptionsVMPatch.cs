using System;
using System.Runtime.CompilerServices;
using HarmonyLib;
using HarmonyLib.BUTR.Extensions;
using TaleWorlds.MountAndBlade.ViewModelCollection.GameOptions;

namespace MCM.UI.Patches
{
	// Token: 0x0200001B RID: 27
	internal static class OptionsVMPatch
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00004388 File Offset: 0x00002588
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x0000438F File Offset: 0x0000258F
		public static bool BlockSwitch { get; set; }

		// Token: 0x060000A9 RID: 169 RVA: 0x00004397 File Offset: 0x00002597
		[NullableContext(1)]
		public static void Patch(Harmony harmony)
		{
			harmony.Patch(AccessTools2.Method(typeof(OptionsVM), "SetSelectedCategory", null, null, true), new HarmonyMethod(typeof(OptionsVMPatch), "SetSelectedCategoryPatch", null), null, null, null);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000043D0 File Offset: 0x000025D0
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static bool SetSelectedCategoryPatch()
		{
			return !OptionsVMPatch.BlockSwitch;
		}
	}
}
