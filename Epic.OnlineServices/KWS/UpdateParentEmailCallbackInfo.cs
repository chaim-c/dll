using System;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x0200044F RID: 1103
	public struct UpdateParentEmailCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x06001C52 RID: 7250 RVA: 0x00029DD6 File Offset: 0x00027FD6
		// (set) Token: 0x06001C53 RID: 7251 RVA: 0x00029DDE File Offset: 0x00027FDE
		public Result ResultCode { get; set; }

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x06001C54 RID: 7252 RVA: 0x00029DE7 File Offset: 0x00027FE7
		// (set) Token: 0x06001C55 RID: 7253 RVA: 0x00029DEF File Offset: 0x00027FEF
		public object ClientData { get; set; }

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x06001C56 RID: 7254 RVA: 0x00029DF8 File Offset: 0x00027FF8
		// (set) Token: 0x06001C57 RID: 7255 RVA: 0x00029E00 File Offset: 0x00028000
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x06001C58 RID: 7256 RVA: 0x00029E0C File Offset: 0x0002800C
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001C59 RID: 7257 RVA: 0x00029E29 File Offset: 0x00028029
		internal void Set(ref UpdateParentEmailCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
		}
	}
}
