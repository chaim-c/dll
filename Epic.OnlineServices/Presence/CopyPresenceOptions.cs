using System;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000230 RID: 560
	public struct CopyPresenceOptions
	{
		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000F7B RID: 3963 RVA: 0x00016E58 File Offset: 0x00015058
		// (set) Token: 0x06000F7C RID: 3964 RVA: 0x00016E60 File Offset: 0x00015060
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000F7D RID: 3965 RVA: 0x00016E69 File Offset: 0x00015069
		// (set) Token: 0x06000F7E RID: 3966 RVA: 0x00016E71 File Offset: 0x00015071
		public EpicAccountId TargetUserId { get; set; }
	}
}
