using System;
using Epic.OnlineServices.IntegratedPlatform;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x0200065B RID: 1627
	public struct WindowsOptions
	{
		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x060029A1 RID: 10657 RVA: 0x0003E21B File Offset: 0x0003C41B
		// (set) Token: 0x060029A2 RID: 10658 RVA: 0x0003E223 File Offset: 0x0003C423
		public IntPtr Reserved { get; set; }

		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x060029A3 RID: 10659 RVA: 0x0003E22C File Offset: 0x0003C42C
		// (set) Token: 0x060029A4 RID: 10660 RVA: 0x0003E234 File Offset: 0x0003C434
		public Utf8String ProductId { get; set; }

		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x060029A5 RID: 10661 RVA: 0x0003E23D File Offset: 0x0003C43D
		// (set) Token: 0x060029A6 RID: 10662 RVA: 0x0003E245 File Offset: 0x0003C445
		public Utf8String SandboxId { get; set; }

		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x060029A7 RID: 10663 RVA: 0x0003E24E File Offset: 0x0003C44E
		// (set) Token: 0x060029A8 RID: 10664 RVA: 0x0003E256 File Offset: 0x0003C456
		public ClientCredentials ClientCredentials { get; set; }

		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x060029A9 RID: 10665 RVA: 0x0003E25F File Offset: 0x0003C45F
		// (set) Token: 0x060029AA RID: 10666 RVA: 0x0003E267 File Offset: 0x0003C467
		public bool IsServer { get; set; }

		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x060029AB RID: 10667 RVA: 0x0003E270 File Offset: 0x0003C470
		// (set) Token: 0x060029AC RID: 10668 RVA: 0x0003E278 File Offset: 0x0003C478
		public Utf8String EncryptionKey { get; set; }

		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x060029AD RID: 10669 RVA: 0x0003E281 File Offset: 0x0003C481
		// (set) Token: 0x060029AE RID: 10670 RVA: 0x0003E289 File Offset: 0x0003C489
		public Utf8String OverrideCountryCode { get; set; }

		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x060029AF RID: 10671 RVA: 0x0003E292 File Offset: 0x0003C492
		// (set) Token: 0x060029B0 RID: 10672 RVA: 0x0003E29A File Offset: 0x0003C49A
		public Utf8String OverrideLocaleCode { get; set; }

		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x060029B1 RID: 10673 RVA: 0x0003E2A3 File Offset: 0x0003C4A3
		// (set) Token: 0x060029B2 RID: 10674 RVA: 0x0003E2AB File Offset: 0x0003C4AB
		public Utf8String DeploymentId { get; set; }

		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x060029B3 RID: 10675 RVA: 0x0003E2B4 File Offset: 0x0003C4B4
		// (set) Token: 0x060029B4 RID: 10676 RVA: 0x0003E2BC File Offset: 0x0003C4BC
		public PlatformFlags Flags { get; set; }

		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x060029B5 RID: 10677 RVA: 0x0003E2C5 File Offset: 0x0003C4C5
		// (set) Token: 0x060029B6 RID: 10678 RVA: 0x0003E2CD File Offset: 0x0003C4CD
		public Utf8String CacheDirectory { get; set; }

		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x060029B7 RID: 10679 RVA: 0x0003E2D6 File Offset: 0x0003C4D6
		// (set) Token: 0x060029B8 RID: 10680 RVA: 0x0003E2DE File Offset: 0x0003C4DE
		public uint TickBudgetInMilliseconds { get; set; }

		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x060029B9 RID: 10681 RVA: 0x0003E2E7 File Offset: 0x0003C4E7
		// (set) Token: 0x060029BA RID: 10682 RVA: 0x0003E2EF File Offset: 0x0003C4EF
		public WindowsRTCOptions? RTCOptions { get; set; }

		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x060029BB RID: 10683 RVA: 0x0003E2F8 File Offset: 0x0003C4F8
		// (set) Token: 0x060029BC RID: 10684 RVA: 0x0003E300 File Offset: 0x0003C500
		public IntegratedPlatformOptionsContainer IntegratedPlatformOptionsContainerHandle { get; set; }
	}
}
