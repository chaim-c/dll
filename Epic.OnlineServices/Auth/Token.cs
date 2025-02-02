using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x020005A7 RID: 1447
	public struct Token
	{
		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x06002508 RID: 9480 RVA: 0x00036D6A File Offset: 0x00034F6A
		// (set) Token: 0x06002509 RID: 9481 RVA: 0x00036D72 File Offset: 0x00034F72
		public Utf8String App { get; set; }

		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x0600250A RID: 9482 RVA: 0x00036D7B File Offset: 0x00034F7B
		// (set) Token: 0x0600250B RID: 9483 RVA: 0x00036D83 File Offset: 0x00034F83
		public Utf8String ClientId { get; set; }

		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x0600250C RID: 9484 RVA: 0x00036D8C File Offset: 0x00034F8C
		// (set) Token: 0x0600250D RID: 9485 RVA: 0x00036D94 File Offset: 0x00034F94
		public EpicAccountId AccountId { get; set; }

		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x0600250E RID: 9486 RVA: 0x00036D9D File Offset: 0x00034F9D
		// (set) Token: 0x0600250F RID: 9487 RVA: 0x00036DA5 File Offset: 0x00034FA5
		public Utf8String AccessToken { get; set; }

		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x06002510 RID: 9488 RVA: 0x00036DAE File Offset: 0x00034FAE
		// (set) Token: 0x06002511 RID: 9489 RVA: 0x00036DB6 File Offset: 0x00034FB6
		public double ExpiresIn { get; set; }

		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x06002512 RID: 9490 RVA: 0x00036DBF File Offset: 0x00034FBF
		// (set) Token: 0x06002513 RID: 9491 RVA: 0x00036DC7 File Offset: 0x00034FC7
		public Utf8String ExpiresAt { get; set; }

		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x06002514 RID: 9492 RVA: 0x00036DD0 File Offset: 0x00034FD0
		// (set) Token: 0x06002515 RID: 9493 RVA: 0x00036DD8 File Offset: 0x00034FD8
		public AuthTokenType AuthType { get; set; }

		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x06002516 RID: 9494 RVA: 0x00036DE1 File Offset: 0x00034FE1
		// (set) Token: 0x06002517 RID: 9495 RVA: 0x00036DE9 File Offset: 0x00034FE9
		public Utf8String RefreshToken { get; set; }

		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x06002518 RID: 9496 RVA: 0x00036DF2 File Offset: 0x00034FF2
		// (set) Token: 0x06002519 RID: 9497 RVA: 0x00036DFA File Offset: 0x00034FFA
		public double RefreshExpiresIn { get; set; }

		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x0600251A RID: 9498 RVA: 0x00036E03 File Offset: 0x00035003
		// (set) Token: 0x0600251B RID: 9499 RVA: 0x00036E0B File Offset: 0x0003500B
		public Utf8String RefreshExpiresAt { get; set; }

		// Token: 0x0600251C RID: 9500 RVA: 0x00036E14 File Offset: 0x00035014
		internal void Set(ref TokenInternal other)
		{
			this.App = other.App;
			this.ClientId = other.ClientId;
			this.AccountId = other.AccountId;
			this.AccessToken = other.AccessToken;
			this.ExpiresIn = other.ExpiresIn;
			this.ExpiresAt = other.ExpiresAt;
			this.AuthType = other.AuthType;
			this.RefreshToken = other.RefreshToken;
			this.RefreshExpiresIn = other.RefreshExpiresIn;
			this.RefreshExpiresAt = other.RefreshExpiresAt;
		}
	}
}
