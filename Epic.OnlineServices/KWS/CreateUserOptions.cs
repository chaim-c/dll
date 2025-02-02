using System;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x0200042B RID: 1067
	public struct CreateUserOptions
	{
		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x06001B7E RID: 7038 RVA: 0x00028BC0 File Offset: 0x00026DC0
		// (set) Token: 0x06001B7F RID: 7039 RVA: 0x00028BC8 File Offset: 0x00026DC8
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06001B80 RID: 7040 RVA: 0x00028BD1 File Offset: 0x00026DD1
		// (set) Token: 0x06001B81 RID: 7041 RVA: 0x00028BD9 File Offset: 0x00026DD9
		public Utf8String DateOfBirth { get; set; }

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06001B82 RID: 7042 RVA: 0x00028BE2 File Offset: 0x00026DE2
		// (set) Token: 0x06001B83 RID: 7043 RVA: 0x00028BEA File Offset: 0x00026DEA
		public Utf8String ParentEmail { get; set; }
	}
}
