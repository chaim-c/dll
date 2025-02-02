using System;
using Epic.OnlineServices.IntegratedPlatform;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x02000654 RID: 1620
	public struct Options
	{
		// Token: 0x17000C64 RID: 3172
		// (get) Token: 0x06002963 RID: 10595 RVA: 0x0003DCFB File Offset: 0x0003BEFB
		// (set) Token: 0x06002964 RID: 10596 RVA: 0x0003DD03 File Offset: 0x0003BF03
		public IntPtr Reserved { get; set; }

		// Token: 0x17000C65 RID: 3173
		// (get) Token: 0x06002965 RID: 10597 RVA: 0x0003DD0C File Offset: 0x0003BF0C
		// (set) Token: 0x06002966 RID: 10598 RVA: 0x0003DD14 File Offset: 0x0003BF14
		public Utf8String ProductId { get; set; }

		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x06002967 RID: 10599 RVA: 0x0003DD1D File Offset: 0x0003BF1D
		// (set) Token: 0x06002968 RID: 10600 RVA: 0x0003DD25 File Offset: 0x0003BF25
		public Utf8String SandboxId { get; set; }

		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x06002969 RID: 10601 RVA: 0x0003DD2E File Offset: 0x0003BF2E
		// (set) Token: 0x0600296A RID: 10602 RVA: 0x0003DD36 File Offset: 0x0003BF36
		public ClientCredentials ClientCredentials { get; set; }

		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x0600296B RID: 10603 RVA: 0x0003DD3F File Offset: 0x0003BF3F
		// (set) Token: 0x0600296C RID: 10604 RVA: 0x0003DD47 File Offset: 0x0003BF47
		public bool IsServer { get; set; }

		// Token: 0x17000C69 RID: 3177
		// (get) Token: 0x0600296D RID: 10605 RVA: 0x0003DD50 File Offset: 0x0003BF50
		// (set) Token: 0x0600296E RID: 10606 RVA: 0x0003DD58 File Offset: 0x0003BF58
		public Utf8String EncryptionKey { get; set; }

		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x0600296F RID: 10607 RVA: 0x0003DD61 File Offset: 0x0003BF61
		// (set) Token: 0x06002970 RID: 10608 RVA: 0x0003DD69 File Offset: 0x0003BF69
		public Utf8String OverrideCountryCode { get; set; }

		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x06002971 RID: 10609 RVA: 0x0003DD72 File Offset: 0x0003BF72
		// (set) Token: 0x06002972 RID: 10610 RVA: 0x0003DD7A File Offset: 0x0003BF7A
		public Utf8String OverrideLocaleCode { get; set; }

		// Token: 0x17000C6C RID: 3180
		// (get) Token: 0x06002973 RID: 10611 RVA: 0x0003DD83 File Offset: 0x0003BF83
		// (set) Token: 0x06002974 RID: 10612 RVA: 0x0003DD8B File Offset: 0x0003BF8B
		public Utf8String DeploymentId { get; set; }

		// Token: 0x17000C6D RID: 3181
		// (get) Token: 0x06002975 RID: 10613 RVA: 0x0003DD94 File Offset: 0x0003BF94
		// (set) Token: 0x06002976 RID: 10614 RVA: 0x0003DD9C File Offset: 0x0003BF9C
		public PlatformFlags Flags { get; set; }

		// Token: 0x17000C6E RID: 3182
		// (get) Token: 0x06002977 RID: 10615 RVA: 0x0003DDA5 File Offset: 0x0003BFA5
		// (set) Token: 0x06002978 RID: 10616 RVA: 0x0003DDAD File Offset: 0x0003BFAD
		public Utf8String CacheDirectory { get; set; }

		// Token: 0x17000C6F RID: 3183
		// (get) Token: 0x06002979 RID: 10617 RVA: 0x0003DDB6 File Offset: 0x0003BFB6
		// (set) Token: 0x0600297A RID: 10618 RVA: 0x0003DDBE File Offset: 0x0003BFBE
		public uint TickBudgetInMilliseconds { get; set; }

		// Token: 0x17000C70 RID: 3184
		// (get) Token: 0x0600297B RID: 10619 RVA: 0x0003DDC7 File Offset: 0x0003BFC7
		// (set) Token: 0x0600297C RID: 10620 RVA: 0x0003DDCF File Offset: 0x0003BFCF
		public RTCOptions? RTCOptions { get; set; }

		// Token: 0x17000C71 RID: 3185
		// (get) Token: 0x0600297D RID: 10621 RVA: 0x0003DDD8 File Offset: 0x0003BFD8
		// (set) Token: 0x0600297E RID: 10622 RVA: 0x0003DDE0 File Offset: 0x0003BFE0
		public IntegratedPlatformOptionsContainer IntegratedPlatformOptionsContainerHandle { get; set; }
	}
}
