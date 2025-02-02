using System;

namespace Epic.OnlineServices.RTCAdmin
{
	// Token: 0x0200020B RID: 523
	public struct SetParticipantHardMuteCompleteCallbackInfo : ICallbackInfo
	{
		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000EBD RID: 3773 RVA: 0x00015E67 File Offset: 0x00014067
		// (set) Token: 0x06000EBE RID: 3774 RVA: 0x00015E6F File Offset: 0x0001406F
		public Result ResultCode { get; set; }

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000EBF RID: 3775 RVA: 0x00015E78 File Offset: 0x00014078
		// (set) Token: 0x06000EC0 RID: 3776 RVA: 0x00015E80 File Offset: 0x00014080
		public object ClientData { get; set; }

		// Token: 0x06000EC1 RID: 3777 RVA: 0x00015E8C File Offset: 0x0001408C
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x00015EA9 File Offset: 0x000140A9
		internal void Set(ref SetParticipantHardMuteCompleteCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
		}
	}
}
