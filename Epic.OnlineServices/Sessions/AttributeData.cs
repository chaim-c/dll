using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000CB RID: 203
	public struct AttributeData
	{
		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060006F9 RID: 1785 RVA: 0x0000A800 File Offset: 0x00008A00
		// (set) Token: 0x060006FA RID: 1786 RVA: 0x0000A808 File Offset: 0x00008A08
		public Utf8String Key { get; set; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x0000A811 File Offset: 0x00008A11
		// (set) Token: 0x060006FC RID: 1788 RVA: 0x0000A819 File Offset: 0x00008A19
		public AttributeDataValue Value { get; set; }

		// Token: 0x060006FD RID: 1789 RVA: 0x0000A822 File Offset: 0x00008A22
		internal void Set(ref AttributeDataInternal other)
		{
			this.Key = other.Key;
			this.Value = other.Value;
		}
	}
}
