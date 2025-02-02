using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Reflection.Emit;

namespace HarmonyLib
{
	// Token: 0x02000014 RID: 20
	internal class MethodCopier
	{
		// Token: 0x06000075 RID: 117 RVA: 0x00004908 File Offset: 0x00002B08
		internal MethodCopier(MethodBase fromMethod, ILGenerator toILGenerator, LocalBuilder[] existingVariables = null)
		{
			if (fromMethod == null)
			{
				throw new ArgumentNullException("fromMethod");
			}
			this.reader = new MethodBodyReader(fromMethod, toILGenerator);
			this.reader.DeclareVariables(existingVariables);
			this.reader.GenerateInstructions();
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00004958 File Offset: 0x00002B58
		internal void SetDebugging(bool debug)
		{
			this.reader.SetDebugging(debug);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004966 File Offset: 0x00002B66
		internal void AddTranspiler(MethodInfo transpiler)
		{
			this.transpilers.Add(transpiler);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00004974 File Offset: 0x00002B74
		internal List<CodeInstruction> Finalize(Emitter emitter, List<Label> endLabels, out bool hasReturnCode, out bool methodEndsInDeadCode)
		{
			return this.reader.FinalizeILCodes(emitter, this.transpilers, endLabels, out hasReturnCode, out methodEndsInDeadCode);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000498C File Offset: 0x00002B8C
		internal static List<CodeInstruction> GetInstructions(ILGenerator generator, MethodBase method, int maxTranspilers)
		{
			if (generator == null)
			{
				throw new ArgumentNullException("generator");
			}
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			LocalBuilder[] existingVariables = MethodPatcher.DeclareOriginalLocalVariables(generator, method);
			MethodCopier methodCopier = new MethodCopier(method, generator, existingVariables);
			Patches patchInfo = Harmony.GetPatchInfo(method);
			if (patchInfo != null)
			{
				ReadOnlyCollection<Patch> readOnlyCollection = patchInfo.Transpilers;
				int num = 0;
				Patch[] array = new Patch[readOnlyCollection.Count];
				foreach (Patch patch in readOnlyCollection)
				{
					array[num] = patch;
					num++;
				}
				List<MethodInfo> sortedPatchMethods = PatchFunctions.GetSortedPatchMethods(method, array, false);
				int num2 = 0;
				while (num2 < maxTranspilers && num2 < sortedPatchMethods.Count)
				{
					methodCopier.AddTranspiler(sortedPatchMethods[num2]);
					num2++;
				}
			}
			bool flag;
			bool flag2;
			return methodCopier.Finalize(null, null, out flag, out flag2);
		}

		// Token: 0x04000021 RID: 33
		private readonly MethodBodyReader reader;

		// Token: 0x04000022 RID: 34
		private readonly List<MethodInfo> transpilers = new List<MethodInfo>();
	}
}
