using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003B2 RID: 946
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LocalRTCOptionsInternal : IGettable<LocalRTCOptions>, ISettable<LocalRTCOptions>, IDisposable
	{
		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x060018D9 RID: 6361 RVA: 0x00025AF4 File Offset: 0x00023CF4
		// (set) Token: 0x060018DA RID: 6362 RVA: 0x00025B0C File Offset: 0x00023D0C
		public uint Flags
		{
			get
			{
				return this.m_Flags;
			}
			set
			{
				this.m_Flags = value;
			}
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x060018DB RID: 6363 RVA: 0x00025B18 File Offset: 0x00023D18
		// (set) Token: 0x060018DC RID: 6364 RVA: 0x00025B39 File Offset: 0x00023D39
		public bool UseManualAudioInput
		{
			get
			{
				bool result;
				Helper.Get(this.m_UseManualAudioInput, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_UseManualAudioInput);
			}
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x060018DD RID: 6365 RVA: 0x00025B4C File Offset: 0x00023D4C
		// (set) Token: 0x060018DE RID: 6366 RVA: 0x00025B6D File Offset: 0x00023D6D
		public bool UseManualAudioOutput
		{
			get
			{
				bool result;
				Helper.Get(this.m_UseManualAudioOutput, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_UseManualAudioOutput);
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x060018DF RID: 6367 RVA: 0x00025B80 File Offset: 0x00023D80
		// (set) Token: 0x060018E0 RID: 6368 RVA: 0x00025BA1 File Offset: 0x00023DA1
		public bool LocalAudioDeviceInputStartsMuted
		{
			get
			{
				bool result;
				Helper.Get(this.m_LocalAudioDeviceInputStartsMuted, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_LocalAudioDeviceInputStartsMuted);
			}
		}

		// Token: 0x060018E1 RID: 6369 RVA: 0x00025BB1 File Offset: 0x00023DB1
		public void Set(ref LocalRTCOptions other)
		{
			this.m_ApiVersion = 1;
			this.Flags = other.Flags;
			this.UseManualAudioInput = other.UseManualAudioInput;
			this.UseManualAudioOutput = other.UseManualAudioOutput;
			this.LocalAudioDeviceInputStartsMuted = other.LocalAudioDeviceInputStartsMuted;
		}

		// Token: 0x060018E2 RID: 6370 RVA: 0x00025BF0 File Offset: 0x00023DF0
		public void Set(ref LocalRTCOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.Flags = other.Value.Flags;
				this.UseManualAudioInput = other.Value.UseManualAudioInput;
				this.UseManualAudioOutput = other.Value.UseManualAudioOutput;
				this.LocalAudioDeviceInputStartsMuted = other.Value.LocalAudioDeviceInputStartsMuted;
			}
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x00025C65 File Offset: 0x00023E65
		public void Dispose()
		{
		}

		// Token: 0x060018E4 RID: 6372 RVA: 0x00025C68 File Offset: 0x00023E68
		public void Get(out LocalRTCOptions output)
		{
			output = default(LocalRTCOptions);
			output.Set(ref this);
		}

		// Token: 0x04000B5E RID: 2910
		private int m_ApiVersion;

		// Token: 0x04000B5F RID: 2911
		private uint m_Flags;

		// Token: 0x04000B60 RID: 2912
		private int m_UseManualAudioInput;

		// Token: 0x04000B61 RID: 2913
		private int m_UseManualAudioOutput;

		// Token: 0x04000B62 RID: 2914
		private int m_LocalAudioDeviceInputStartsMuted;
	}
}
