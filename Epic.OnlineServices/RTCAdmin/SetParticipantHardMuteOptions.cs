using System;

namespace Epic.OnlineServices.RTCAdmin
{
	// Token: 0x0200020D RID: 525
	public struct SetParticipantHardMuteOptions
	{
		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000ECC RID: 3788 RVA: 0x00015FBD File Offset: 0x000141BD
		// (set) Token: 0x06000ECD RID: 3789 RVA: 0x00015FC5 File Offset: 0x000141C5
		public Utf8String RoomName { get; set; }

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000ECE RID: 3790 RVA: 0x00015FCE File Offset: 0x000141CE
		// (set) Token: 0x06000ECF RID: 3791 RVA: 0x00015FD6 File Offset: 0x000141D6
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000ED0 RID: 3792 RVA: 0x00015FDF File Offset: 0x000141DF
		// (set) Token: 0x06000ED1 RID: 3793 RVA: 0x00015FE7 File Offset: 0x000141E7
		public bool Mute { get; set; }
	}
}
