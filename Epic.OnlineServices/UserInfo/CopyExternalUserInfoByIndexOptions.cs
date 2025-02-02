using System;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000028 RID: 40
	public struct CopyExternalUserInfoByIndexOptions
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000316 RID: 790 RVA: 0x00004C13 File Offset: 0x00002E13
		// (set) Token: 0x06000317 RID: 791 RVA: 0x00004C1B File Offset: 0x00002E1B
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000318 RID: 792 RVA: 0x00004C24 File Offset: 0x00002E24
		// (set) Token: 0x06000319 RID: 793 RVA: 0x00004C2C File Offset: 0x00002E2C
		public EpicAccountId TargetUserId { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600031A RID: 794 RVA: 0x00004C35 File Offset: 0x00002E35
		// (set) Token: 0x0600031B RID: 795 RVA: 0x00004C3D File Offset: 0x00002E3D
		public uint Index { get; set; }
	}
}
