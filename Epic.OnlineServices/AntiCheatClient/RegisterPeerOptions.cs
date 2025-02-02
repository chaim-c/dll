using System;
using Epic.OnlineServices.AntiCheatCommon;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x0200063B RID: 1595
	public struct RegisterPeerOptions
	{
		// Token: 0x17000C18 RID: 3096
		// (get) Token: 0x0600288B RID: 10379 RVA: 0x0003C5B9 File Offset: 0x0003A7B9
		// (set) Token: 0x0600288C RID: 10380 RVA: 0x0003C5C1 File Offset: 0x0003A7C1
		public IntPtr PeerHandle { get; set; }

		// Token: 0x17000C19 RID: 3097
		// (get) Token: 0x0600288D RID: 10381 RVA: 0x0003C5CA File Offset: 0x0003A7CA
		// (set) Token: 0x0600288E RID: 10382 RVA: 0x0003C5D2 File Offset: 0x0003A7D2
		public AntiCheatCommonClientType ClientType { get; set; }

		// Token: 0x17000C1A RID: 3098
		// (get) Token: 0x0600288F RID: 10383 RVA: 0x0003C5DB File Offset: 0x0003A7DB
		// (set) Token: 0x06002890 RID: 10384 RVA: 0x0003C5E3 File Offset: 0x0003A7E3
		public AntiCheatCommonClientPlatform ClientPlatform { get; set; }

		// Token: 0x17000C1B RID: 3099
		// (get) Token: 0x06002891 RID: 10385 RVA: 0x0003C5EC File Offset: 0x0003A7EC
		// (set) Token: 0x06002892 RID: 10386 RVA: 0x0003C5F4 File Offset: 0x0003A7F4
		public uint AuthenticationTimeout { get; set; }

		// Token: 0x17000C1C RID: 3100
		// (get) Token: 0x06002893 RID: 10387 RVA: 0x0003C5FD File Offset: 0x0003A7FD
		// (set) Token: 0x06002894 RID: 10388 RVA: 0x0003C605 File Offset: 0x0003A805
		public Utf8String AccountId_DEPRECATED { get; set; }

		// Token: 0x17000C1D RID: 3101
		// (get) Token: 0x06002895 RID: 10389 RVA: 0x0003C60E File Offset: 0x0003A80E
		// (set) Token: 0x06002896 RID: 10390 RVA: 0x0003C616 File Offset: 0x0003A816
		public Utf8String IpAddress { get; set; }

		// Token: 0x17000C1E RID: 3102
		// (get) Token: 0x06002897 RID: 10391 RVA: 0x0003C61F File Offset: 0x0003A81F
		// (set) Token: 0x06002898 RID: 10392 RVA: 0x0003C627 File Offset: 0x0003A827
		public ProductUserId PeerProductUserId { get; set; }
	}
}
