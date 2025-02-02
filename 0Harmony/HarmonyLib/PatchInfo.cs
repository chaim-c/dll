using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HarmonyLib
{
	// Token: 0x0200003E RID: 62
	[Serializable]
	public class PatchInfo
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000166 RID: 358 RVA: 0x0000B1C8 File Offset: 0x000093C8
		public bool Debugging
		{
			get
			{
				if (!this.prefixes.Any((Patch p) => p.debug))
				{
					if (!this.postfixes.Any((Patch p) => p.debug))
					{
						if (!this.transpilers.Any((Patch p) => p.debug))
						{
							return this.finalizers.Any((Patch p) => p.debug);
						}
					}
				}
				return true;
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000B288 File Offset: 0x00009488
		internal void AddPrefixes(string owner, params HarmonyMethod[] methods)
		{
			this.prefixes = PatchInfo.Add(owner, methods, this.prefixes);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000B2A0 File Offset: 0x000094A0
		[Obsolete("This method only exists for backwards compatibility since the class is public.")]
		public void AddPrefix(MethodInfo patch, string owner, int priority, string[] before, string[] after, bool debug)
		{
			this.AddPrefixes(owner, new HarmonyMethod[]
			{
				new HarmonyMethod(patch, priority, before, after, new bool?(debug))
			});
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000B2CF File Offset: 0x000094CF
		public void RemovePrefix(string owner)
		{
			this.prefixes = PatchInfo.Remove(owner, this.prefixes);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000B2E3 File Offset: 0x000094E3
		internal void AddPostfixes(string owner, params HarmonyMethod[] methods)
		{
			this.postfixes = PatchInfo.Add(owner, methods, this.postfixes);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000B2F8 File Offset: 0x000094F8
		[Obsolete("This method only exists for backwards compatibility since the class is public.")]
		public void AddPostfix(MethodInfo patch, string owner, int priority, string[] before, string[] after, bool debug)
		{
			this.AddPostfixes(owner, new HarmonyMethod[]
			{
				new HarmonyMethod(patch, priority, before, after, new bool?(debug))
			});
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000B327 File Offset: 0x00009527
		public void RemovePostfix(string owner)
		{
			this.postfixes = PatchInfo.Remove(owner, this.postfixes);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000B33B File Offset: 0x0000953B
		internal void AddTranspilers(string owner, params HarmonyMethod[] methods)
		{
			this.transpilers = PatchInfo.Add(owner, methods, this.transpilers);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000B350 File Offset: 0x00009550
		[Obsolete("This method only exists for backwards compatibility since the class is public.")]
		public void AddTranspiler(MethodInfo patch, string owner, int priority, string[] before, string[] after, bool debug)
		{
			this.AddTranspilers(owner, new HarmonyMethod[]
			{
				new HarmonyMethod(patch, priority, before, after, new bool?(debug))
			});
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000B37F File Offset: 0x0000957F
		public void RemoveTranspiler(string owner)
		{
			this.transpilers = PatchInfo.Remove(owner, this.transpilers);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000B393 File Offset: 0x00009593
		internal void AddFinalizers(string owner, params HarmonyMethod[] methods)
		{
			this.finalizers = PatchInfo.Add(owner, methods, this.finalizers);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000B3A8 File Offset: 0x000095A8
		[Obsolete("This method only exists for backwards compatibility since the class is public.")]
		public void AddFinalizer(MethodInfo patch, string owner, int priority, string[] before, string[] after, bool debug)
		{
			this.AddFinalizers(owner, new HarmonyMethod[]
			{
				new HarmonyMethod(patch, priority, before, after, new bool?(debug))
			});
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000B3D7 File Offset: 0x000095D7
		public void RemoveFinalizer(string owner)
		{
			this.finalizers = PatchInfo.Remove(owner, this.finalizers);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000B3EC File Offset: 0x000095EC
		public void RemovePatch(MethodInfo patch)
		{
			this.prefixes = (from p in this.prefixes
			where p.PatchMethod != patch
			select p).ToArray<Patch>();
			this.postfixes = (from p in this.postfixes
			where p.PatchMethod != patch
			select p).ToArray<Patch>();
			this.transpilers = (from p in this.transpilers
			where p.PatchMethod != patch
			select p).ToArray<Patch>();
			this.finalizers = (from p in this.finalizers
			where p.PatchMethod != patch
			select p).ToArray<Patch>();
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000B490 File Offset: 0x00009690
		private static Patch[] Add(string owner, HarmonyMethod[] add, Patch[] current)
		{
			if (add.Length == 0)
			{
				return current;
			}
			int initialIndex = current.Length;
			List<Patch> list = new List<Patch>();
			list.AddRange(current);
			list.AddRange((from method in add
			where method != null
			select method).Select((HarmonyMethod method, int i) => new Patch(method, i + initialIndex, owner)));
			return list.ToArray();
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000B508 File Offset: 0x00009708
		private static Patch[] Remove(string owner, Patch[] current)
		{
			if (!(owner == "*"))
			{
				return (from patch in current
				where patch.owner != owner
				select patch).ToArray<Patch>();
			}
			return Array.Empty<Patch>();
		}

		// Token: 0x0400009D RID: 157
		public Patch[] prefixes = Array.Empty<Patch>();

		// Token: 0x0400009E RID: 158
		public Patch[] postfixes = Array.Empty<Patch>();

		// Token: 0x0400009F RID: 159
		public Patch[] transpilers = Array.Empty<Patch>();

		// Token: 0x040000A0 RID: 160
		public Patch[] finalizers = Array.Empty<Patch>();
	}
}
