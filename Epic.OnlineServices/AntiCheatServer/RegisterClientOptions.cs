using System;
using Epic.OnlineServices.AntiCheatCommon;

namespace Epic.OnlineServices.AntiCheatServer
{
	// Token: 0x020005CE RID: 1486
	public struct RegisterClientOptions
	{
		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x06002625 RID: 9765 RVA: 0x00038B7E File Offset: 0x00036D7E
		// (set) Token: 0x06002626 RID: 9766 RVA: 0x00038B86 File Offset: 0x00036D86
		public IntPtr ClientHandle { get; set; }

		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x06002627 RID: 9767 RVA: 0x00038B8F File Offset: 0x00036D8F
		// (set) Token: 0x06002628 RID: 9768 RVA: 0x00038B97 File Offset: 0x00036D97
		public AntiCheatCommonClientType ClientType { get; set; }

		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x06002629 RID: 9769 RVA: 0x00038BA0 File Offset: 0x00036DA0
		// (set) Token: 0x0600262A RID: 9770 RVA: 0x00038BA8 File Offset: 0x00036DA8
		public AntiCheatCommonClientPlatform ClientPlatform { get; set; }

		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x0600262B RID: 9771 RVA: 0x00038BB1 File Offset: 0x00036DB1
		// (set) Token: 0x0600262C RID: 9772 RVA: 0x00038BB9 File Offset: 0x00036DB9
		public Utf8String AccountId_DEPRECATED { get; set; }

		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x0600262D RID: 9773 RVA: 0x00038BC2 File Offset: 0x00036DC2
		// (set) Token: 0x0600262E RID: 9774 RVA: 0x00038BCA File Offset: 0x00036DCA
		public Utf8String IpAddress { get; set; }

		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x0600262F RID: 9775 RVA: 0x00038BD3 File Offset: 0x00036DD3
		// (set) Token: 0x06002630 RID: 9776 RVA: 0x00038BDB File Offset: 0x00036DDB
		public ProductUserId UserId { get; set; }
	}
}
