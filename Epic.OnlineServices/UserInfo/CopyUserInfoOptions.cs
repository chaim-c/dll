using System;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x0200002A RID: 42
	public struct CopyUserInfoOptions
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000322 RID: 802 RVA: 0x00004D1F File Offset: 0x00002F1F
		// (set) Token: 0x06000323 RID: 803 RVA: 0x00004D27 File Offset: 0x00002F27
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000324 RID: 804 RVA: 0x00004D30 File Offset: 0x00002F30
		// (set) Token: 0x06000325 RID: 805 RVA: 0x00004D38 File Offset: 0x00002F38
		public EpicAccountId TargetUserId { get; set; }
	}
}
