using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000E9 RID: 233
	public struct IsUserInSessionOptions
	{
		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000797 RID: 1943 RVA: 0x0000B766 File Offset: 0x00009966
		// (set) Token: 0x06000798 RID: 1944 RVA: 0x0000B76E File Offset: 0x0000996E
		public Utf8String SessionName { get; set; }

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000799 RID: 1945 RVA: 0x0000B777 File Offset: 0x00009977
		// (set) Token: 0x0600079A RID: 1946 RVA: 0x0000B77F File Offset: 0x0000997F
		public ProductUserId TargetUserId { get; set; }
	}
}
