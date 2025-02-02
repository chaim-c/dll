using System;
using System.Runtime.CompilerServices;
using HarmonyLib;
using HarmonyLib.BUTR.Extensions;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.MountAndBlade.GauntletUI;
using TaleWorlds.TwoDimension;

namespace MCM.UI.Patches
{
	// Token: 0x0200001A RID: 26
	[NullableContext(1)]
	[Nullable(0)]
	internal static class OptionsGauntletScreenPatch
	{
		// Token: 0x060000A3 RID: 163 RVA: 0x00004260 File Offset: 0x00002460
		public static void Patch(Harmony harmony)
		{
			harmony.Patch(AccessTools2.Method(typeof(GauntletOptionsScreen), "OnInitialize", null, null, true), null, new HarmonyMethod(typeof(OptionsGauntletScreenPatch), "OnInitializePostfix", null), null, null);
			harmony.Patch(AccessTools2.Method(typeof(GauntletOptionsScreen), "OnFinalize", null, null, true), null, new HarmonyMethod(typeof(OptionsGauntletScreenPatch), "OnFinalizePostfix", null), null, null);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000042DC File Offset: 0x000024DC
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void OnInitializePostfix(object __instance)
		{
			SpriteCategory spriteCategoryMCMVal;
			SpriteCategory spriteCategoryMCM = UIResourceManager.SpriteData.SpriteCategories.TryGetValue("ui_mcm", out spriteCategoryMCMVal) ? spriteCategoryMCMVal : null;
			if (spriteCategoryMCM != null)
			{
				spriteCategoryMCM.Load(UIResourceManager.ResourceContext, UIResourceManager.UIResourceDepot);
			}
			OptionsGauntletScreenPatch._spriteCategoriesMCM.Add(__instance, spriteCategoryMCM);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000432C File Offset: 0x0000252C
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void OnFinalizePostfix(object __instance)
		{
			SpriteCategory value = OptionsGauntletScreenPatch._spriteCategoriesMCM.GetValue(__instance, (object _) => null);
			if (value != null)
			{
				value.Unload();
			}
			OptionsGauntletScreenPatch._spriteCategoriesMCM.Remove(__instance);
		}

		// Token: 0x0400002D RID: 45
		[Nullable(new byte[]
		{
			1,
			1,
			2
		})]
		private static readonly ConditionalWeakTable<object, SpriteCategory> _spriteCategoriesMCM = new ConditionalWeakTable<object, SpriteCategory>();
	}
}
