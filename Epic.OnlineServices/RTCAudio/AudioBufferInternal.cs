using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001AB RID: 427
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AudioBufferInternal : IGettable<AudioBuffer>, ISettable<AudioBuffer>, IDisposable
	{
		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000C2A RID: 3114 RVA: 0x000122D8 File Offset: 0x000104D8
		// (set) Token: 0x06000C2B RID: 3115 RVA: 0x000122FF File Offset: 0x000104FF
		public short[] Frames
		{
			get
			{
				short[] result;
				Helper.Get<short>(this.m_Frames, out result, this.m_FramesCount);
				return result;
			}
			set
			{
				Helper.Set<short>(value, ref this.m_Frames, out this.m_FramesCount);
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000C2C RID: 3116 RVA: 0x00012318 File Offset: 0x00010518
		// (set) Token: 0x06000C2D RID: 3117 RVA: 0x00012330 File Offset: 0x00010530
		public uint SampleRate
		{
			get
			{
				return this.m_SampleRate;
			}
			set
			{
				this.m_SampleRate = value;
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000C2E RID: 3118 RVA: 0x0001233C File Offset: 0x0001053C
		// (set) Token: 0x06000C2F RID: 3119 RVA: 0x00012354 File Offset: 0x00010554
		public uint Channels
		{
			get
			{
				return this.m_Channels;
			}
			set
			{
				this.m_Channels = value;
			}
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x0001235E File Offset: 0x0001055E
		public void Set(ref AudioBuffer other)
		{
			this.m_ApiVersion = 1;
			this.Frames = other.Frames;
			this.SampleRate = other.SampleRate;
			this.Channels = other.Channels;
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x00012390 File Offset: 0x00010590
		public void Set(ref AudioBuffer? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.Frames = other.Value.Frames;
				this.SampleRate = other.Value.SampleRate;
				this.Channels = other.Value.Channels;
			}
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x000123F0 File Offset: 0x000105F0
		public void Dispose()
		{
			Helper.Dispose(ref this.m_Frames);
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x000123FF File Offset: 0x000105FF
		public void Get(out AudioBuffer output)
		{
			output = default(AudioBuffer);
			output.Set(ref this);
		}

		// Token: 0x04000591 RID: 1425
		private int m_ApiVersion;

		// Token: 0x04000592 RID: 1426
		private IntPtr m_Frames;

		// Token: 0x04000593 RID: 1427
		private uint m_FramesCount;

		// Token: 0x04000594 RID: 1428
		private uint m_SampleRate;

		// Token: 0x04000595 RID: 1429
		private uint m_Channels;
	}
}
