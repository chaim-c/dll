using System;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x0200042D RID: 1069
	public struct GetPermissionByKeyOptions
	{
		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x06001B8A RID: 7050 RVA: 0x00028CDB File Offset: 0x00026EDB
		// (set) Token: 0x06001B8B RID: 7051 RVA: 0x00028CE3 File Offset: 0x00026EE3
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x06001B8C RID: 7052 RVA: 0x00028CEC File Offset: 0x00026EEC
		// (set) Token: 0x06001B8D RID: 7053 RVA: 0x00028CF4 File Offset: 0x00026EF4
		public Utf8String Key { get; set; }
	}
}
