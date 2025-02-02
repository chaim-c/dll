using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x020005B1 RID: 1457
	public struct IOSCredentials
	{
		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x0600258B RID: 9611 RVA: 0x00037BA9 File Offset: 0x00035DA9
		// (set) Token: 0x0600258C RID: 9612 RVA: 0x00037BB1 File Offset: 0x00035DB1
		public Utf8String Id { get; set; }

		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x0600258D RID: 9613 RVA: 0x00037BBA File Offset: 0x00035DBA
		// (set) Token: 0x0600258E RID: 9614 RVA: 0x00037BC2 File Offset: 0x00035DC2
		public Utf8String Token { get; set; }

		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x0600258F RID: 9615 RVA: 0x00037BCB File Offset: 0x00035DCB
		// (set) Token: 0x06002590 RID: 9616 RVA: 0x00037BD3 File Offset: 0x00035DD3
		public LoginCredentialType Type { get; set; }

		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x06002591 RID: 9617 RVA: 0x00037BDC File Offset: 0x00035DDC
		// (set) Token: 0x06002592 RID: 9618 RVA: 0x00037BE4 File Offset: 0x00035DE4
		public IOSCredentialsSystemAuthCredentialsOptions? SystemAuthCredentialsOptions { get; set; }

		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x06002593 RID: 9619 RVA: 0x00037BED File Offset: 0x00035DED
		// (set) Token: 0x06002594 RID: 9620 RVA: 0x00037BF5 File Offset: 0x00035DF5
		public ExternalCredentialType ExternalType { get; set; }

		// Token: 0x06002595 RID: 9621 RVA: 0x00037C00 File Offset: 0x00035E00
		internal void Set(ref IOSCredentialsInternal other)
		{
			this.Id = other.Id;
			this.Token = other.Token;
			this.Type = other.Type;
			this.SystemAuthCredentialsOptions = other.SystemAuthCredentialsOptions;
			this.ExternalType = other.ExternalType;
		}
	}
}
