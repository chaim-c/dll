using System;

namespace Epic.OnlineServices.ProgressionSnapshot
{
	// Token: 0x02000219 RID: 537
	public struct AddProgressionOptions
	{
		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000F12 RID: 3858 RVA: 0x000165B9 File Offset: 0x000147B9
		// (set) Token: 0x06000F13 RID: 3859 RVA: 0x000165C1 File Offset: 0x000147C1
		public uint SnapshotId { get; set; }

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000F14 RID: 3860 RVA: 0x000165CA File Offset: 0x000147CA
		// (set) Token: 0x06000F15 RID: 3861 RVA: 0x000165D2 File Offset: 0x000147D2
		public Utf8String Key { get; set; }

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000F16 RID: 3862 RVA: 0x000165DB File Offset: 0x000147DB
		// (set) Token: 0x06000F17 RID: 3863 RVA: 0x000165E3 File Offset: 0x000147E3
		public Utf8String Value { get; set; }
	}
}
