using System;

namespace Epic.OnlineServices.Metrics
{
	// Token: 0x0200030C RID: 780
	public struct BeginPlayerSessionOptions
	{
		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06001502 RID: 5378 RVA: 0x0001F1BA File Offset: 0x0001D3BA
		// (set) Token: 0x06001503 RID: 5379 RVA: 0x0001F1C2 File Offset: 0x0001D3C2
		public BeginPlayerSessionOptionsAccountId AccountId { get; set; }

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06001504 RID: 5380 RVA: 0x0001F1CB File Offset: 0x0001D3CB
		// (set) Token: 0x06001505 RID: 5381 RVA: 0x0001F1D3 File Offset: 0x0001D3D3
		public Utf8String DisplayName { get; set; }

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06001506 RID: 5382 RVA: 0x0001F1DC File Offset: 0x0001D3DC
		// (set) Token: 0x06001507 RID: 5383 RVA: 0x0001F1E4 File Offset: 0x0001D3E4
		public UserControllerType ControllerType { get; set; }

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06001508 RID: 5384 RVA: 0x0001F1ED File Offset: 0x0001D3ED
		// (set) Token: 0x06001509 RID: 5385 RVA: 0x0001F1F5 File Offset: 0x0001D3F5
		public Utf8String ServerIp { get; set; }

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x0600150A RID: 5386 RVA: 0x0001F1FE File Offset: 0x0001D3FE
		// (set) Token: 0x0600150B RID: 5387 RVA: 0x0001F206 File Offset: 0x0001D406
		public Utf8String GameSessionId { get; set; }
	}
}
