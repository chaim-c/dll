using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HarmonyLib
{
	// Token: 0x02000041 RID: 65
	public class Patches
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000190 RID: 400 RVA: 0x0000C6AC File Offset: 0x0000A8AC
		public ReadOnlyCollection<string> Owners
		{
			get
			{
				HashSet<string> hashSet = new HashSet<string>();
				hashSet.UnionWith(from p in this.Prefixes
				select p.owner);
				hashSet.UnionWith(from p in this.Postfixes
				select p.owner);
				hashSet.UnionWith(from p in this.Transpilers
				select p.owner);
				hashSet.UnionWith(from p in this.Finalizers
				select p.owner);
				return hashSet.ToList<string>().AsReadOnly();
			}
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000C78C File Offset: 0x0000A98C
		public Patches(Patch[] prefixes, Patch[] postfixes, Patch[] transpilers, Patch[] finalizers)
		{
			if (prefixes == null)
			{
				prefixes = Array.Empty<Patch>();
			}
			if (postfixes == null)
			{
				postfixes = Array.Empty<Patch>();
			}
			if (transpilers == null)
			{
				transpilers = Array.Empty<Patch>();
			}
			if (finalizers == null)
			{
				finalizers = Array.Empty<Patch>();
			}
			this.Prefixes = prefixes.ToList<Patch>().AsReadOnly();
			this.Postfixes = postfixes.ToList<Patch>().AsReadOnly();
			this.Transpilers = transpilers.ToList<Patch>().AsReadOnly();
			this.Finalizers = finalizers.ToList<Patch>().AsReadOnly();
		}

		// Token: 0x040000B1 RID: 177
		public readonly ReadOnlyCollection<Patch> Prefixes;

		// Token: 0x040000B2 RID: 178
		public readonly ReadOnlyCollection<Patch> Postfixes;

		// Token: 0x040000B3 RID: 179
		public readonly ReadOnlyCollection<Patch> Transpilers;

		// Token: 0x040000B4 RID: 180
		public readonly ReadOnlyCollection<Patch> Finalizers;
	}
}
