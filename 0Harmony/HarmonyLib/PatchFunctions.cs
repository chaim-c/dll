using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace HarmonyLib
{
	// Token: 0x02000018 RID: 24
	internal static class PatchFunctions
	{
		// Token: 0x060000A6 RID: 166 RVA: 0x00008277 File Offset: 0x00006477
		internal static List<MethodInfo> GetSortedPatchMethods(MethodBase original, Patch[] patches, bool debug)
		{
			return new PatchSorter(patches, debug).Sort(original);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00008288 File Offset: 0x00006488
		internal static MethodInfo UpdateWrapper(MethodBase original, PatchInfo patchInfo)
		{
			bool debug = patchInfo.Debugging || Harmony.DEBUG;
			List<MethodInfo> sortedPatchMethods = PatchFunctions.GetSortedPatchMethods(original, patchInfo.prefixes, debug);
			List<MethodInfo> sortedPatchMethods2 = PatchFunctions.GetSortedPatchMethods(original, patchInfo.postfixes, debug);
			List<MethodInfo> sortedPatchMethods3 = PatchFunctions.GetSortedPatchMethods(original, patchInfo.transpilers, debug);
			List<MethodInfo> sortedPatchMethods4 = PatchFunctions.GetSortedPatchMethods(original, patchInfo.finalizers, debug);
			MethodPatcher methodPatcher = new MethodPatcher(original, null, sortedPatchMethods, sortedPatchMethods2, sortedPatchMethods3, sortedPatchMethods4, debug);
			Dictionary<int, CodeInstruction> finalInstructions;
			MethodInfo methodInfo = methodPatcher.CreateReplacement(out finalInstructions);
			if (methodInfo == null)
			{
				throw new MissingMethodException("Cannot create replacement for " + original.FullDescription());
			}
			try
			{
				PatchTools.DetourMethod(original, methodInfo);
			}
			catch (Exception ex)
			{
				throw HarmonyException.Create(ex, finalInstructions);
			}
			return methodInfo;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000833C File Offset: 0x0000653C
		internal static MethodInfo ReversePatch(HarmonyMethod standin, MethodBase original, MethodInfo postTranspiler)
		{
			if (standin == null)
			{
				throw new ArgumentNullException("standin");
			}
			if (standin.method == null)
			{
				throw new ArgumentNullException("standin", "standin.method is NULL");
			}
			bool debug = standin.debug.GetValueOrDefault() || Harmony.DEBUG;
			List<MethodInfo> list = new List<MethodInfo>();
			HarmonyReversePatchType? reversePatchType = standin.reversePatchType;
			HarmonyReversePatchType harmonyReversePatchType = HarmonyReversePatchType.Snapshot;
			if (reversePatchType.GetValueOrDefault() == harmonyReversePatchType & reversePatchType != null)
			{
				Patches patchInfo = Harmony.GetPatchInfo(original);
				List<MethodInfo> list2 = list;
				ReadOnlyCollection<Patch> transpilers = patchInfo.Transpilers;
				int num = 0;
				Patch[] array = new Patch[transpilers.Count];
				foreach (Patch patch in transpilers)
				{
					array[num] = patch;
					num++;
				}
				list2.AddRange(PatchFunctions.GetSortedPatchMethods(original, array, debug));
			}
			if (postTranspiler != null)
			{
				list.Add(postTranspiler);
			}
			List<MethodInfo> list3 = new List<MethodInfo>();
			MethodPatcher methodPatcher = new MethodPatcher(standin.method, original, list3, list3, list, list3, debug);
			Dictionary<int, CodeInstruction> finalInstructions;
			MethodInfo methodInfo = methodPatcher.CreateReplacement(out finalInstructions);
			if (methodInfo == null)
			{
				throw new MissingMethodException("Cannot create replacement for " + standin.method.FullDescription());
			}
			try
			{
				PatchTools.DetourMethod(standin.method, methodInfo);
			}
			catch (Exception ex)
			{
				throw HarmonyException.Create(ex, finalInstructions);
			}
			return methodInfo;
		}
	}
}
