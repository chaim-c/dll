using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000332 RID: 818
	public struct AttributeData
	{
		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x0600158D RID: 5517 RVA: 0x0001FEDC File Offset: 0x0001E0DC
		// (set) Token: 0x0600158E RID: 5518 RVA: 0x0001FEE4 File Offset: 0x0001E0E4
		public Utf8String Key { get; set; }

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x0600158F RID: 5519 RVA: 0x0001FEED File Offset: 0x0001E0ED
		// (set) Token: 0x06001590 RID: 5520 RVA: 0x0001FEF5 File Offset: 0x0001E0F5
		public AttributeDataValue Value { get; set; }

		// Token: 0x06001591 RID: 5521 RVA: 0x0001FEFE File Offset: 0x0001E0FE
		internal void Set(ref AttributeDataInternal other)
		{
			this.Key = other.Key;
			this.Value = other.Value;
		}
	}
}
