using System;

namespace Mono.Cecil
{
	// Token: 0x020000C5 RID: 197
	public interface IMemberDefinition : ICustomAttributeProvider, IMetadataTokenProvider
	{
		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000706 RID: 1798
		// (set) Token: 0x06000707 RID: 1799
		string Name { get; set; }

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000708 RID: 1800
		string FullName { get; }

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000709 RID: 1801
		// (set) Token: 0x0600070A RID: 1802
		bool IsSpecialName { get; set; }

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600070B RID: 1803
		// (set) Token: 0x0600070C RID: 1804
		bool IsRuntimeSpecialName { get; set; }

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x0600070D RID: 1805
		// (set) Token: 0x0600070E RID: 1806
		TypeDefinition DeclaringType { get; set; }
	}
}
