using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002B7 RID: 695
	public struct GetNextReceivedPacketSizeOptions
	{
		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x060012C0 RID: 4800 RVA: 0x0001BC14 File Offset: 0x00019E14
		// (set) Token: 0x060012C1 RID: 4801 RVA: 0x0001BC1C File Offset: 0x00019E1C
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x060012C2 RID: 4802 RVA: 0x0001BC25 File Offset: 0x00019E25
		// (set) Token: 0x060012C3 RID: 4803 RVA: 0x0001BC2D File Offset: 0x00019E2D
		public byte? RequestedChannel { get; set; }
	}
}
