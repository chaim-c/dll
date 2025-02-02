using System;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x020000AB RID: 171
	public struct IngestData
	{
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x000096C1 File Offset: 0x000078C1
		// (set) Token: 0x06000643 RID: 1603 RVA: 0x000096C9 File Offset: 0x000078C9
		public Utf8String StatName { get; set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000644 RID: 1604 RVA: 0x000096D2 File Offset: 0x000078D2
		// (set) Token: 0x06000645 RID: 1605 RVA: 0x000096DA File Offset: 0x000078DA
		public int IngestAmount { get; set; }

		// Token: 0x06000646 RID: 1606 RVA: 0x000096E3 File Offset: 0x000078E3
		internal void Set(ref IngestDataInternal other)
		{
			this.StatName = other.StatName;
			this.IngestAmount = other.IngestAmount;
		}
	}
}
