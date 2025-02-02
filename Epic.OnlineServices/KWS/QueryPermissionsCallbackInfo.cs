using System;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x02000447 RID: 1095
	public struct QueryPermissionsCallbackInfo : ICallbackInfo
	{
		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x06001C11 RID: 7185 RVA: 0x00029758 File Offset: 0x00027958
		// (set) Token: 0x06001C12 RID: 7186 RVA: 0x00029760 File Offset: 0x00027960
		public Result ResultCode { get; set; }

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x06001C13 RID: 7187 RVA: 0x00029769 File Offset: 0x00027969
		// (set) Token: 0x06001C14 RID: 7188 RVA: 0x00029771 File Offset: 0x00027971
		public object ClientData { get; set; }

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x06001C15 RID: 7189 RVA: 0x0002977A File Offset: 0x0002797A
		// (set) Token: 0x06001C16 RID: 7190 RVA: 0x00029782 File Offset: 0x00027982
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x06001C17 RID: 7191 RVA: 0x0002978B File Offset: 0x0002798B
		// (set) Token: 0x06001C18 RID: 7192 RVA: 0x00029793 File Offset: 0x00027993
		public Utf8String KWSUserId { get; set; }

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x06001C19 RID: 7193 RVA: 0x0002979C File Offset: 0x0002799C
		// (set) Token: 0x06001C1A RID: 7194 RVA: 0x000297A4 File Offset: 0x000279A4
		public Utf8String DateOfBirth { get; set; }

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x06001C1B RID: 7195 RVA: 0x000297AD File Offset: 0x000279AD
		// (set) Token: 0x06001C1C RID: 7196 RVA: 0x000297B5 File Offset: 0x000279B5
		public bool IsMinor { get; set; }

		// Token: 0x06001C1D RID: 7197 RVA: 0x000297C0 File Offset: 0x000279C0
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001C1E RID: 7198 RVA: 0x000297E0 File Offset: 0x000279E0
		internal void Set(ref QueryPermissionsCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.KWSUserId = other.KWSUserId;
			this.DateOfBirth = other.DateOfBirth;
			this.IsMinor = other.IsMinor;
		}
	}
}
