using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003B1 RID: 945
	public struct LocalRTCOptions
	{
		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x060018D0 RID: 6352 RVA: 0x00025A79 File Offset: 0x00023C79
		// (set) Token: 0x060018D1 RID: 6353 RVA: 0x00025A81 File Offset: 0x00023C81
		public uint Flags { get; set; }

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x060018D2 RID: 6354 RVA: 0x00025A8A File Offset: 0x00023C8A
		// (set) Token: 0x060018D3 RID: 6355 RVA: 0x00025A92 File Offset: 0x00023C92
		public bool UseManualAudioInput { get; set; }

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x060018D4 RID: 6356 RVA: 0x00025A9B File Offset: 0x00023C9B
		// (set) Token: 0x060018D5 RID: 6357 RVA: 0x00025AA3 File Offset: 0x00023CA3
		public bool UseManualAudioOutput { get; set; }

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x060018D6 RID: 6358 RVA: 0x00025AAC File Offset: 0x00023CAC
		// (set) Token: 0x060018D7 RID: 6359 RVA: 0x00025AB4 File Offset: 0x00023CB4
		public bool LocalAudioDeviceInputStartsMuted { get; set; }

		// Token: 0x060018D8 RID: 6360 RVA: 0x00025ABD File Offset: 0x00023CBD
		internal void Set(ref LocalRTCOptionsInternal other)
		{
			this.Flags = other.Flags;
			this.UseManualAudioInput = other.UseManualAudioInput;
			this.UseManualAudioOutput = other.UseManualAudioOutput;
			this.LocalAudioDeviceInputStartsMuted = other.LocalAudioDeviceInputStartsMuted;
		}
	}
}
