using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x02000579 RID: 1401
	public struct Credentials
	{
		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x060023D5 RID: 9173 RVA: 0x00035544 File Offset: 0x00033744
		// (set) Token: 0x060023D6 RID: 9174 RVA: 0x0003554C File Offset: 0x0003374C
		public Utf8String Id { get; set; }

		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x060023D7 RID: 9175 RVA: 0x00035555 File Offset: 0x00033755
		// (set) Token: 0x060023D8 RID: 9176 RVA: 0x0003555D File Offset: 0x0003375D
		public Utf8String Token { get; set; }

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x060023D9 RID: 9177 RVA: 0x00035566 File Offset: 0x00033766
		// (set) Token: 0x060023DA RID: 9178 RVA: 0x0003556E File Offset: 0x0003376E
		public LoginCredentialType Type { get; set; }

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x060023DB RID: 9179 RVA: 0x00035577 File Offset: 0x00033777
		// (set) Token: 0x060023DC RID: 9180 RVA: 0x0003557F File Offset: 0x0003377F
		public IntPtr SystemAuthCredentialsOptions { get; set; }

		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x060023DD RID: 9181 RVA: 0x00035588 File Offset: 0x00033788
		// (set) Token: 0x060023DE RID: 9182 RVA: 0x00035590 File Offset: 0x00033790
		public ExternalCredentialType ExternalType { get; set; }

		// Token: 0x060023DF RID: 9183 RVA: 0x0003559C File Offset: 0x0003379C
		internal void Set(ref CredentialsInternal other)
		{
			this.Id = other.Id;
			this.Token = other.Token;
			this.Type = other.Type;
			this.SystemAuthCredentialsOptions = other.SystemAuthCredentialsOptions;
			this.ExternalType = other.ExternalType;
		}
	}
}
