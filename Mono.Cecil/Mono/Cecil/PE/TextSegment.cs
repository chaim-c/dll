using System;

namespace Mono.Cecil.PE
{
	// Token: 0x0200004C RID: 76
	internal enum TextSegment
	{
		// Token: 0x04000365 RID: 869
		ImportAddressTable,
		// Token: 0x04000366 RID: 870
		CLIHeader,
		// Token: 0x04000367 RID: 871
		Code,
		// Token: 0x04000368 RID: 872
		Resources,
		// Token: 0x04000369 RID: 873
		Data,
		// Token: 0x0400036A RID: 874
		StrongNameSignature,
		// Token: 0x0400036B RID: 875
		MetadataHeader,
		// Token: 0x0400036C RID: 876
		TableHeap,
		// Token: 0x0400036D RID: 877
		StringHeap,
		// Token: 0x0400036E RID: 878
		UserStringHeap,
		// Token: 0x0400036F RID: 879
		GuidHeap,
		// Token: 0x04000370 RID: 880
		BlobHeap,
		// Token: 0x04000371 RID: 881
		DebugDirectory,
		// Token: 0x04000372 RID: 882
		ImportDirectory,
		// Token: 0x04000373 RID: 883
		ImportHintNameTable,
		// Token: 0x04000374 RID: 884
		StartupStub
	}
}
