using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004C2 RID: 1218
	public struct KeyImageInfo
	{
		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x06001F63 RID: 8035 RVA: 0x0002EE08 File Offset: 0x0002D008
		// (set) Token: 0x06001F64 RID: 8036 RVA: 0x0002EE10 File Offset: 0x0002D010
		public Utf8String Type { get; set; }

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x06001F65 RID: 8037 RVA: 0x0002EE19 File Offset: 0x0002D019
		// (set) Token: 0x06001F66 RID: 8038 RVA: 0x0002EE21 File Offset: 0x0002D021
		public Utf8String Url { get; set; }

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x06001F67 RID: 8039 RVA: 0x0002EE2A File Offset: 0x0002D02A
		// (set) Token: 0x06001F68 RID: 8040 RVA: 0x0002EE32 File Offset: 0x0002D032
		public uint Width { get; set; }

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x06001F69 RID: 8041 RVA: 0x0002EE3B File Offset: 0x0002D03B
		// (set) Token: 0x06001F6A RID: 8042 RVA: 0x0002EE43 File Offset: 0x0002D043
		public uint Height { get; set; }

		// Token: 0x06001F6B RID: 8043 RVA: 0x0002EE4C File Offset: 0x0002D04C
		internal void Set(ref KeyImageInfoInternal other)
		{
			this.Type = other.Type;
			this.Url = other.Url;
			this.Width = other.Width;
			this.Height = other.Height;
		}
	}
}
