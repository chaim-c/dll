using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000EF RID: 239
	public struct JoinSessionOptions
	{
		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060007C2 RID: 1986 RVA: 0x0000BB55 File Offset: 0x00009D55
		// (set) Token: 0x060007C3 RID: 1987 RVA: 0x0000BB5D File Offset: 0x00009D5D
		public Utf8String SessionName { get; set; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060007C4 RID: 1988 RVA: 0x0000BB66 File Offset: 0x00009D66
		// (set) Token: 0x060007C5 RID: 1989 RVA: 0x0000BB6E File Offset: 0x00009D6E
		public SessionDetails SessionHandle { get; set; }

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060007C6 RID: 1990 RVA: 0x0000BB77 File Offset: 0x00009D77
		// (set) Token: 0x060007C7 RID: 1991 RVA: 0x0000BB7F File Offset: 0x00009D7F
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060007C8 RID: 1992 RVA: 0x0000BB88 File Offset: 0x00009D88
		// (set) Token: 0x060007C9 RID: 1993 RVA: 0x0000BB90 File Offset: 0x00009D90
		public bool PresenceEnabled { get; set; }
	}
}
