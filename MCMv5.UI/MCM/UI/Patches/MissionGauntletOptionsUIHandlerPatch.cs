using System;
using System.Runtime.CompilerServices;
using HarmonyLib;
using HarmonyLib.BUTR.Extensions;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.MountAndBlade.GauntletUI.Mission;
using TaleWorlds.TwoDimension;

namespace MCM.UI.Patches
{
	// Token: 0x02000019 RID: 25
	[NullableContext(1)]
	[Nullable(0)]
	internal static class MissionGauntletOptionsUIHandlerPatch
	{
		// Token: 0x0600009F RID: 159 RVA: 0x0000413C File Offset: 0x0000233C
		public static void Patch(Harmony harmony)
		{
			harmony.Patch(AccessTools2.Constructor(typeof(MissionGauntletOptionsUIHandler), null, false, true), null, new HarmonyMethod(typeof(MissionGauntletOptionsUIHandlerPatch), "OnInitializePostfix", null), null, null);
			harmony.Patch(AccessTools2.Method(typeof(MissionGauntletOptionsUIHandler), "OnMissionScreenFinalize", null, null, true), null, new HarmonyMethod(typeof(MissionGauntletOptionsUIHandlerPatch), "OnFinalizePostfix", null), null, null);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000041B4 File Offset: 0x000023B4
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void OnInitializePostfix(object __instance)
		{
			SpriteCategory spriteCategoryMCMVal;
			SpriteCategory spriteCategoryMCM = UIResourceManager.SpriteData.SpriteCategories.TryGetValue("ui_mcm", out spriteCategoryMCMVal) ? spriteCategoryMCMVal : null;
			if (spriteCategoryMCM != null)
			{
				spriteCategoryMCM.Load(UIResourceManager.ResourceContext, UIResourceManager.UIResourceDepot);
			}
			MissionGauntletOptionsUIHandlerPatch._spriteCategoriesMCM.Add(__instance, spriteCategoryMCM);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004204 File Offset: 0x00002404
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void OnFinalizePostfix(object __instance)
		{
			SpriteCategory value = MissionGauntletOptionsUIHandlerPatch._spriteCategoriesMCM.GetValue(__instance, (object _) => null);
			if (value != null)
			{
				value.Unload();
			}
			MissionGauntletOptionsUIHandlerPatch._spriteCategoriesMCM.Remove(__instance);
		}

		// Token: 0x0400002C RID: 44
		[Nullable(new byte[]
		{
			1,
			1,
			2
		})]
		private static readonly ConditionalWeakTable<object, SpriteCategory> _spriteCategoriesMCM = new ConditionalWeakTable<object, SpriteCategory>();
	}
}
