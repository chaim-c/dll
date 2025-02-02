using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace MCM.LightInject
{
	// Token: 0x0200010F RID: 271
	[ExcludeFromCodeCoverage]
	internal class CachedTypeExtractor : ITypeExtractor
	{
		// Token: 0x0600067E RID: 1662 RVA: 0x0001425A File Offset: 0x0001245A
		public CachedTypeExtractor(ITypeExtractor typeExtractor)
		{
			this.typeExtractor = typeExtractor;
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x00014275 File Offset: 0x00012475
		public Type[] Execute(Assembly assembly)
		{
			return this.cache.GetOrAdd(assembly, new Func<Assembly, Type[]>(this.typeExtractor.Execute));
		}

		// Token: 0x040001E3 RID: 483
		private readonly ITypeExtractor typeExtractor;

		// Token: 0x040001E4 RID: 484
		private readonly ThreadSafeDictionary<Assembly, Type[]> cache = new ThreadSafeDictionary<Assembly, Type[]>();
	}
}
