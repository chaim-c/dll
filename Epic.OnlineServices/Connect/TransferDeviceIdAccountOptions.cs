using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000562 RID: 1378
	public struct TransferDeviceIdAccountOptions
	{
		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x06002339 RID: 9017 RVA: 0x000341EA File Offset: 0x000323EA
		// (set) Token: 0x0600233A RID: 9018 RVA: 0x000341F2 File Offset: 0x000323F2
		public ProductUserId PrimaryLocalUserId { get; set; }

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x0600233B RID: 9019 RVA: 0x000341FB File Offset: 0x000323FB
		// (set) Token: 0x0600233C RID: 9020 RVA: 0x00034203 File Offset: 0x00032403
		public ProductUserId LocalDeviceUserId { get; set; }

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x0600233D RID: 9021 RVA: 0x0003420C File Offset: 0x0003240C
		// (set) Token: 0x0600233E RID: 9022 RVA: 0x00034214 File Offset: 0x00032414
		public ProductUserId ProductUserIdToPreserve { get; set; }
	}
}
