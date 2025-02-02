using System;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000045 RID: 69
	public struct AcknowledgeEventIdOptions
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x000062D7 File Offset: 0x000044D7
		// (set) Token: 0x060003FB RID: 1019 RVA: 0x000062DF File Offset: 0x000044DF
		public ulong UiEventId { get; set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x000062E8 File Offset: 0x000044E8
		// (set) Token: 0x060003FD RID: 1021 RVA: 0x000062F0 File Offset: 0x000044F0
		public Result Result { get; set; }
	}
}
