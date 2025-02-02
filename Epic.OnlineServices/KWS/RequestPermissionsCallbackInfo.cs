using System;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x0200044B RID: 1099
	public struct RequestPermissionsCallbackInfo : ICallbackInfo
	{
		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06001C36 RID: 7222 RVA: 0x00029B35 File Offset: 0x00027D35
		// (set) Token: 0x06001C37 RID: 7223 RVA: 0x00029B3D File Offset: 0x00027D3D
		public Result ResultCode { get; set; }

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x06001C38 RID: 7224 RVA: 0x00029B46 File Offset: 0x00027D46
		// (set) Token: 0x06001C39 RID: 7225 RVA: 0x00029B4E File Offset: 0x00027D4E
		public object ClientData { get; set; }

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x06001C3A RID: 7226 RVA: 0x00029B57 File Offset: 0x00027D57
		// (set) Token: 0x06001C3B RID: 7227 RVA: 0x00029B5F File Offset: 0x00027D5F
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x06001C3C RID: 7228 RVA: 0x00029B68 File Offset: 0x00027D68
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001C3D RID: 7229 RVA: 0x00029B85 File Offset: 0x00027D85
		internal void Set(ref RequestPermissionsCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
		}
	}
}
