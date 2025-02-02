using System;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000244 RID: 580
	public struct PresenceChangedCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06001005 RID: 4101 RVA: 0x00017A48 File Offset: 0x00015C48
		// (set) Token: 0x06001006 RID: 4102 RVA: 0x00017A50 File Offset: 0x00015C50
		public object ClientData { get; set; }

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06001007 RID: 4103 RVA: 0x00017A59 File Offset: 0x00015C59
		// (set) Token: 0x06001008 RID: 4104 RVA: 0x00017A61 File Offset: 0x00015C61
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06001009 RID: 4105 RVA: 0x00017A6A File Offset: 0x00015C6A
		// (set) Token: 0x0600100A RID: 4106 RVA: 0x00017A72 File Offset: 0x00015C72
		public EpicAccountId PresenceUserId { get; set; }

		// Token: 0x0600100B RID: 4107 RVA: 0x00017A7C File Offset: 0x00015C7C
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x00017A97 File Offset: 0x00015C97
		internal void Set(ref PresenceChangedCallbackInfoInternal other)
		{
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.PresenceUserId = other.PresenceUserId;
		}
	}
}
