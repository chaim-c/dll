using System;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x02000180 RID: 384
	public struct JoinRoomOptions
	{
		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000B02 RID: 2818 RVA: 0x000107EB File Offset: 0x0000E9EB
		// (set) Token: 0x06000B03 RID: 2819 RVA: 0x000107F3 File Offset: 0x0000E9F3
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000B04 RID: 2820 RVA: 0x000107FC File Offset: 0x0000E9FC
		// (set) Token: 0x06000B05 RID: 2821 RVA: 0x00010804 File Offset: 0x0000EA04
		public Utf8String RoomName { get; set; }

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000B06 RID: 2822 RVA: 0x0001080D File Offset: 0x0000EA0D
		// (set) Token: 0x06000B07 RID: 2823 RVA: 0x00010815 File Offset: 0x0000EA15
		public Utf8String ClientBaseUrl { get; set; }

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000B08 RID: 2824 RVA: 0x0001081E File Offset: 0x0000EA1E
		// (set) Token: 0x06000B09 RID: 2825 RVA: 0x00010826 File Offset: 0x0000EA26
		public Utf8String ParticipantToken { get; set; }

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000B0A RID: 2826 RVA: 0x0001082F File Offset: 0x0000EA2F
		// (set) Token: 0x06000B0B RID: 2827 RVA: 0x00010837 File Offset: 0x0000EA37
		public ProductUserId ParticipantId { get; set; }

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000B0C RID: 2828 RVA: 0x00010840 File Offset: 0x0000EA40
		// (set) Token: 0x06000B0D RID: 2829 RVA: 0x00010848 File Offset: 0x0000EA48
		public JoinRoomFlags Flags { get; set; }

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000B0E RID: 2830 RVA: 0x00010851 File Offset: 0x0000EA51
		// (set) Token: 0x06000B0F RID: 2831 RVA: 0x00010859 File Offset: 0x0000EA59
		public bool ManualAudioInputEnabled { get; set; }

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000B10 RID: 2832 RVA: 0x00010862 File Offset: 0x0000EA62
		// (set) Token: 0x06000B11 RID: 2833 RVA: 0x0001086A File Offset: 0x0000EA6A
		public bool ManualAudioOutputEnabled { get; set; }
	}
}
