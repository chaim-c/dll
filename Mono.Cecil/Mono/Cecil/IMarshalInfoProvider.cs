using System;

namespace Mono.Cecil
{
	// Token: 0x020000B0 RID: 176
	public interface IMarshalInfoProvider : IMetadataTokenProvider
	{
		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600066F RID: 1647
		bool HasMarshalInfo { get; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000670 RID: 1648
		// (set) Token: 0x06000671 RID: 1649
		MarshalInfo MarshalInfo { get; set; }
	}
}
