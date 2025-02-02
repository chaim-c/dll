using System;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000077 RID: 119
	public struct ShowReportPlayerOptions
	{
		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x00007395 File Offset: 0x00005595
		// (set) Token: 0x060004E1 RID: 1249 RVA: 0x0000739D File Offset: 0x0000559D
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x000073A6 File Offset: 0x000055A6
		// (set) Token: 0x060004E3 RID: 1251 RVA: 0x000073AE File Offset: 0x000055AE
		public EpicAccountId TargetUserId { get; set; }
	}
}
