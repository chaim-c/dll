using System;
using System.Diagnostics.CodeAnalysis;

namespace MCM.LightInject
{
	// Token: 0x020000FB RID: 251
	[ExcludeFromCodeCoverage]
	internal class ConstructionInfoProvider : IConstructionInfoProvider
	{
		// Token: 0x06000606 RID: 1542 RVA: 0x000135B4 File Offset: 0x000117B4
		public ConstructionInfoProvider(IConstructionInfoBuilder constructionInfoBuilder)
		{
			this.constructionInfoBuilder = constructionInfoBuilder;
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x000135D0 File Offset: 0x000117D0
		public ConstructionInfo GetConstructionInfo(Registration registration)
		{
			return this.cache.GetOrAdd(registration, new Func<Registration, ConstructionInfo>(this.constructionInfoBuilder.Execute));
		}

		// Token: 0x040001B6 RID: 438
		private readonly IConstructionInfoBuilder constructionInfoBuilder;

		// Token: 0x040001B7 RID: 439
		private readonly ThreadSafeDictionary<Registration, ConstructionInfo> cache = new ThreadSafeDictionary<Registration, ConstructionInfo>();
	}
}
