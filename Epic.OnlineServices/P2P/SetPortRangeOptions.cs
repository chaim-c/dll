using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002E6 RID: 742
	public struct SetPortRangeOptions
	{
		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x0600140D RID: 5133 RVA: 0x0001DC0A File Offset: 0x0001BE0A
		// (set) Token: 0x0600140E RID: 5134 RVA: 0x0001DC12 File Offset: 0x0001BE12
		public ushort Port { get; set; }

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x0600140F RID: 5135 RVA: 0x0001DC1B File Offset: 0x0001BE1B
		// (set) Token: 0x06001410 RID: 5136 RVA: 0x0001DC23 File Offset: 0x0001BE23
		public ushort MaxAdditionalPortsToTry { get; set; }
	}
}
