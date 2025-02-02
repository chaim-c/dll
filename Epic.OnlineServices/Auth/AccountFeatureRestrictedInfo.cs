using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x0200056E RID: 1390
	public struct AccountFeatureRestrictedInfo
	{
		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x060023A4 RID: 9124 RVA: 0x00034CED File Offset: 0x00032EED
		// (set) Token: 0x060023A5 RID: 9125 RVA: 0x00034CF5 File Offset: 0x00032EF5
		public Utf8String VerificationURI { get; set; }

		// Token: 0x060023A6 RID: 9126 RVA: 0x00034CFE File Offset: 0x00032EFE
		internal void Set(ref AccountFeatureRestrictedInfoInternal other)
		{
			this.VerificationURI = other.VerificationURI;
		}
	}
}
