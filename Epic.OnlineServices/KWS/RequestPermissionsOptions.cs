using System;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x0200044D RID: 1101
	public struct RequestPermissionsOptions
	{
		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x06001C49 RID: 7241 RVA: 0x00029D02 File Offset: 0x00027F02
		// (set) Token: 0x06001C4A RID: 7242 RVA: 0x00029D0A File Offset: 0x00027F0A
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x06001C4B RID: 7243 RVA: 0x00029D13 File Offset: 0x00027F13
		// (set) Token: 0x06001C4C RID: 7244 RVA: 0x00029D1B File Offset: 0x00027F1B
		public Utf8String[] PermissionKeys { get; set; }
	}
}
