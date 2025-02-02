using System;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x02000441 RID: 1089
	public struct PermissionsUpdateReceivedCallbackInfo : ICallbackInfo
	{
		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06001BE8 RID: 7144 RVA: 0x00029388 File Offset: 0x00027588
		// (set) Token: 0x06001BE9 RID: 7145 RVA: 0x00029390 File Offset: 0x00027590
		public object ClientData { get; set; }

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06001BEA RID: 7146 RVA: 0x00029399 File Offset: 0x00027599
		// (set) Token: 0x06001BEB RID: 7147 RVA: 0x000293A1 File Offset: 0x000275A1
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x06001BEC RID: 7148 RVA: 0x000293AC File Offset: 0x000275AC
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x06001BED RID: 7149 RVA: 0x000293C7 File Offset: 0x000275C7
		internal void Set(ref PermissionsUpdateReceivedCallbackInfoInternal other)
		{
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
		}
	}
}
