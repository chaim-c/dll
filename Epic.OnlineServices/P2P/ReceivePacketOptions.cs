using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002DF RID: 735
	public struct ReceivePacketOptions
	{
		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x060013DD RID: 5085 RVA: 0x0001D7D0 File Offset: 0x0001B9D0
		// (set) Token: 0x060013DE RID: 5086 RVA: 0x0001D7D8 File Offset: 0x0001B9D8
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x060013DF RID: 5087 RVA: 0x0001D7E1 File Offset: 0x0001B9E1
		// (set) Token: 0x060013E0 RID: 5088 RVA: 0x0001D7E9 File Offset: 0x0001B9E9
		public uint MaxDataSizeBytes { get; set; }

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x060013E1 RID: 5089 RVA: 0x0001D7F2 File Offset: 0x0001B9F2
		// (set) Token: 0x060013E2 RID: 5090 RVA: 0x0001D7FA File Offset: 0x0001B9FA
		public byte? RequestedChannel { get; set; }
	}
}
