using System;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x0200023C RID: 572
	public struct JoinGameAcceptedCallbackInfo : ICallbackInfo
	{
		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000FD2 RID: 4050 RVA: 0x00017760 File Offset: 0x00015960
		// (set) Token: 0x06000FD3 RID: 4051 RVA: 0x00017768 File Offset: 0x00015968
		public object ClientData { get; set; }

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000FD4 RID: 4052 RVA: 0x00017771 File Offset: 0x00015971
		// (set) Token: 0x06000FD5 RID: 4053 RVA: 0x00017779 File Offset: 0x00015979
		public Utf8String JoinInfo { get; set; }

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000FD6 RID: 4054 RVA: 0x00017782 File Offset: 0x00015982
		// (set) Token: 0x06000FD7 RID: 4055 RVA: 0x0001778A File Offset: 0x0001598A
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000FD8 RID: 4056 RVA: 0x00017793 File Offset: 0x00015993
		// (set) Token: 0x06000FD9 RID: 4057 RVA: 0x0001779B File Offset: 0x0001599B
		public EpicAccountId TargetUserId { get; set; }

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000FDA RID: 4058 RVA: 0x000177A4 File Offset: 0x000159A4
		// (set) Token: 0x06000FDB RID: 4059 RVA: 0x000177AC File Offset: 0x000159AC
		public ulong UiEventId { get; set; }

		// Token: 0x06000FDC RID: 4060 RVA: 0x000177B8 File Offset: 0x000159B8
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x000177D4 File Offset: 0x000159D4
		internal void Set(ref JoinGameAcceptedCallbackInfoInternal other)
		{
			this.ClientData = other.ClientData;
			this.JoinInfo = other.JoinInfo;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
			this.UiEventId = other.UiEventId;
		}
	}
}
