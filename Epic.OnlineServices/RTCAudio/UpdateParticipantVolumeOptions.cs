using System;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001E6 RID: 486
	public struct UpdateParticipantVolumeOptions
	{
		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000D91 RID: 3473 RVA: 0x00014238 File Offset: 0x00012438
		// (set) Token: 0x06000D92 RID: 3474 RVA: 0x00014240 File Offset: 0x00012440
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000D93 RID: 3475 RVA: 0x00014249 File Offset: 0x00012449
		// (set) Token: 0x06000D94 RID: 3476 RVA: 0x00014251 File Offset: 0x00012451
		public Utf8String RoomName { get; set; }

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000D95 RID: 3477 RVA: 0x0001425A File Offset: 0x0001245A
		// (set) Token: 0x06000D96 RID: 3478 RVA: 0x00014262 File Offset: 0x00012462
		public ProductUserId ParticipantId { get; set; }

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000D97 RID: 3479 RVA: 0x0001426B File Offset: 0x0001246B
		// (set) Token: 0x06000D98 RID: 3480 RVA: 0x00014273 File Offset: 0x00012473
		public float Volume { get; set; }
	}
}
