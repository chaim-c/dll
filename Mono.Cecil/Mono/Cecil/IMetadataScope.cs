using System;

namespace Mono.Cecil
{
	// Token: 0x0200005D RID: 93
	public interface IMetadataScope : IMetadataTokenProvider
	{
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000300 RID: 768
		MetadataScopeType MetadataScopeType { get; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000301 RID: 769
		// (set) Token: 0x06000302 RID: 770
		string Name { get; set; }
	}
}
