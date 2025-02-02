using System;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x020005E4 RID: 1508
	public struct LogEventOptions
	{
		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x06002656 RID: 9814 RVA: 0x00038FB6 File Offset: 0x000371B6
		// (set) Token: 0x06002657 RID: 9815 RVA: 0x00038FBE File Offset: 0x000371BE
		public IntPtr ClientHandle { get; set; }

		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x06002658 RID: 9816 RVA: 0x00038FC7 File Offset: 0x000371C7
		// (set) Token: 0x06002659 RID: 9817 RVA: 0x00038FCF File Offset: 0x000371CF
		public uint EventId { get; set; }

		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x0600265A RID: 9818 RVA: 0x00038FD8 File Offset: 0x000371D8
		// (set) Token: 0x0600265B RID: 9819 RVA: 0x00038FE0 File Offset: 0x000371E0
		public LogEventParamPair[] Params { get; set; }
	}
}
