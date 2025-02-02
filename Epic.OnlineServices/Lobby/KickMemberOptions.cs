using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200035E RID: 862
	public struct KickMemberOptions
	{
		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x060016D8 RID: 5848 RVA: 0x00021EE6 File Offset: 0x000200E6
		// (set) Token: 0x060016D9 RID: 5849 RVA: 0x00021EEE File Offset: 0x000200EE
		public Utf8String LobbyId { get; set; }

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x060016DA RID: 5850 RVA: 0x00021EF7 File Offset: 0x000200F7
		// (set) Token: 0x060016DB RID: 5851 RVA: 0x00021EFF File Offset: 0x000200FF
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x060016DC RID: 5852 RVA: 0x00021F08 File Offset: 0x00020108
		// (set) Token: 0x060016DD RID: 5853 RVA: 0x00021F10 File Offset: 0x00020110
		public ProductUserId TargetUserId { get; set; }
	}
}
