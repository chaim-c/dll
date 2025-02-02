using System;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x02000470 RID: 1136
	public struct OnFriendsUpdateInfo : ICallbackInfo
	{
		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x06001CFB RID: 7419 RVA: 0x0002ADE2 File Offset: 0x00028FE2
		// (set) Token: 0x06001CFC RID: 7420 RVA: 0x0002ADEA File Offset: 0x00028FEA
		public object ClientData { get; set; }

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x06001CFD RID: 7421 RVA: 0x0002ADF3 File Offset: 0x00028FF3
		// (set) Token: 0x06001CFE RID: 7422 RVA: 0x0002ADFB File Offset: 0x00028FFB
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x06001CFF RID: 7423 RVA: 0x0002AE04 File Offset: 0x00029004
		// (set) Token: 0x06001D00 RID: 7424 RVA: 0x0002AE0C File Offset: 0x0002900C
		public EpicAccountId TargetUserId { get; set; }

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x06001D01 RID: 7425 RVA: 0x0002AE15 File Offset: 0x00029015
		// (set) Token: 0x06001D02 RID: 7426 RVA: 0x0002AE1D File Offset: 0x0002901D
		public FriendsStatus PreviousStatus { get; set; }

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x06001D03 RID: 7427 RVA: 0x0002AE26 File Offset: 0x00029026
		// (set) Token: 0x06001D04 RID: 7428 RVA: 0x0002AE2E File Offset: 0x0002902E
		public FriendsStatus CurrentStatus { get; set; }

		// Token: 0x06001D05 RID: 7429 RVA: 0x0002AE38 File Offset: 0x00029038
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x06001D06 RID: 7430 RVA: 0x0002AE54 File Offset: 0x00029054
		internal void Set(ref OnFriendsUpdateInfoInternal other)
		{
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
			this.PreviousStatus = other.PreviousStatus;
			this.CurrentStatus = other.CurrentStatus;
		}
	}
}
