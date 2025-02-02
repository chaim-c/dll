using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000526 RID: 1318
	public struct Credentials
	{
		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x060021D2 RID: 8658 RVA: 0x00032935 File Offset: 0x00030B35
		// (set) Token: 0x060021D3 RID: 8659 RVA: 0x0003293D File Offset: 0x00030B3D
		public Utf8String Token { get; set; }

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x060021D4 RID: 8660 RVA: 0x00032946 File Offset: 0x00030B46
		// (set) Token: 0x060021D5 RID: 8661 RVA: 0x0003294E File Offset: 0x00030B4E
		public ExternalCredentialType Type { get; set; }

		// Token: 0x060021D6 RID: 8662 RVA: 0x00032957 File Offset: 0x00030B57
		internal void Set(ref CredentialsInternal other)
		{
			this.Token = other.Token;
			this.Type = other.Type;
		}
	}
}
