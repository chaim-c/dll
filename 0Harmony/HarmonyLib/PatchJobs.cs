using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HarmonyLib
{
	// Token: 0x02000019 RID: 25
	internal class PatchJobs<T>
	{
		// Token: 0x060000A9 RID: 169 RVA: 0x000084A4 File Offset: 0x000066A4
		internal PatchJobs<T>.Job GetJob(MethodBase method)
		{
			if (method == null)
			{
				return null;
			}
			PatchJobs<T>.Job job;
			if (!this.state.TryGetValue(method, out job))
			{
				job = new PatchJobs<T>.Job
				{
					original = method
				};
				this.state[method] = job;
			}
			return job;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000084E1 File Offset: 0x000066E1
		internal List<PatchJobs<T>.Job> GetJobs()
		{
			return (from job in this.state.Values
			where job.prefixes.Count + job.postfixes.Count + job.transpilers.Count + job.finalizers.Count > 0
			select job).ToList<PatchJobs<T>.Job>();
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00008517 File Offset: 0x00006717
		internal List<T> GetReplacements()
		{
			return (from job in this.state.Values
			select job.replacement).ToList<T>();
		}

		// Token: 0x0400004B RID: 75
		internal Dictionary<MethodBase, PatchJobs<T>.Job> state = new Dictionary<MethodBase, PatchJobs<T>.Job>();

		// Token: 0x02000070 RID: 112
		internal class Job
		{
			// Token: 0x0600046B RID: 1131 RVA: 0x00016308 File Offset: 0x00014508
			internal void AddPatch(AttributePatch patch)
			{
				HarmonyPatchType? type = patch.type;
				if (type != null)
				{
					switch (type.GetValueOrDefault())
					{
					case HarmonyPatchType.Prefix:
						this.prefixes.Add(patch.info);
						return;
					case HarmonyPatchType.Postfix:
						this.postfixes.Add(patch.info);
						return;
					case HarmonyPatchType.Transpiler:
						this.transpilers.Add(patch.info);
						return;
					case HarmonyPatchType.Finalizer:
						this.finalizers.Add(patch.info);
						break;
					default:
						return;
					}
				}
			}

			// Token: 0x04000161 RID: 353
			internal MethodBase original;

			// Token: 0x04000162 RID: 354
			internal T replacement;

			// Token: 0x04000163 RID: 355
			internal List<HarmonyMethod> prefixes = new List<HarmonyMethod>();

			// Token: 0x04000164 RID: 356
			internal List<HarmonyMethod> postfixes = new List<HarmonyMethod>();

			// Token: 0x04000165 RID: 357
			internal List<HarmonyMethod> transpilers = new List<HarmonyMethod>();

			// Token: 0x04000166 RID: 358
			internal List<HarmonyMethod> finalizers = new List<HarmonyMethod>();
		}
	}
}
