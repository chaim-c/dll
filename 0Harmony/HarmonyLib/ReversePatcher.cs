using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HarmonyLib
{
	// Token: 0x02000044 RID: 68
	public class ReversePatcher
	{
		// Token: 0x060001AB RID: 427 RVA: 0x0000CDB3 File Offset: 0x0000AFB3
		public ReversePatcher(Harmony instance, MethodBase original, HarmonyMethod standin)
		{
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000CDD0 File Offset: 0x0000AFD0
		public MethodInfo Patch(HarmonyReversePatchType type = HarmonyReversePatchType.Original)
		{
			if (this.original == null)
			{
				throw new NullReferenceException("Null method for " + this.instance.Id);
			}
			this.standin.reversePatchType = new HarmonyReversePatchType?(type);
			MethodInfo transpiler = ReversePatcher.GetTranspiler(this.standin.method);
			return PatchFunctions.ReversePatch(this.standin, this.original, transpiler);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000CE34 File Offset: 0x0000B034
		internal static MethodInfo GetTranspiler(MethodInfo method)
		{
			string methodName = method.Name;
			Type declaringType = method.DeclaringType;
			List<MethodInfo> declaredMethods = AccessTools.GetDeclaredMethods(declaringType);
			Type ici = typeof(IEnumerable<CodeInstruction>);
			return declaredMethods.FirstOrDefault((MethodInfo m) => !(m.ReturnType != ici) && m.Name.StartsWith("<" + methodName + ">"));
		}

		// Token: 0x040000C5 RID: 197
		private readonly Harmony instance = instance;

		// Token: 0x040000C6 RID: 198
		private readonly MethodBase original = original;

		// Token: 0x040000C7 RID: 199
		private readonly HarmonyMethod standin = standin;
	}
}
