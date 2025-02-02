using System;

namespace Mono.Cecil
{
	// Token: 0x020000AE RID: 174
	public interface IConstantProvider : IMetadataTokenProvider
	{
		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600066B RID: 1643
		// (set) Token: 0x0600066C RID: 1644
		bool HasConstant { get; set; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600066D RID: 1645
		// (set) Token: 0x0600066E RID: 1646
		object Constant { get; set; }
	}
}
