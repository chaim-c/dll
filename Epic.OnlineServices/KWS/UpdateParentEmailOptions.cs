using System;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x02000451 RID: 1105
	public struct UpdateParentEmailOptions
	{
		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x06001C65 RID: 7269 RVA: 0x00029FA6 File Offset: 0x000281A6
		// (set) Token: 0x06001C66 RID: 7270 RVA: 0x00029FAE File Offset: 0x000281AE
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x06001C67 RID: 7271 RVA: 0x00029FB7 File Offset: 0x000281B7
		// (set) Token: 0x06001C68 RID: 7272 RVA: 0x00029FBF File Offset: 0x000281BF
		public Utf8String ParentEmail { get; set; }
	}
}
