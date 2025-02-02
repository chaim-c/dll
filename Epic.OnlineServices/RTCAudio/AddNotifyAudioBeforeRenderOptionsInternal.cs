using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x0200019B RID: 411
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyAudioBeforeRenderOptionsInternal : ISettable<AddNotifyAudioBeforeRenderOptions>, IDisposable
	{
		// Token: 0x170002D2 RID: 722
		// (set) Token: 0x06000BC4 RID: 3012 RVA: 0x000118CD File Offset: 0x0000FACD
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x170002D3 RID: 723
		// (set) Token: 0x06000BC5 RID: 3013 RVA: 0x000118DD File Offset: 0x0000FADD
		public Utf8String RoomName
		{
			set
			{
				Helper.Set(value, ref this.m_RoomName);
			}
		}

		// Token: 0x170002D4 RID: 724
		// (set) Token: 0x06000BC6 RID: 3014 RVA: 0x000118ED File Offset: 0x0000FAED
		public bool UnmixedAudio
		{
			set
			{
				Helper.Set(value, ref this.m_UnmixedAudio);
			}
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x000118FD File Offset: 0x0000FAFD
		public void Set(ref AddNotifyAudioBeforeRenderOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.RoomName = other.RoomName;
			this.UnmixedAudio = other.UnmixedAudio;
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x00011930 File Offset: 0x0000FB30
		public void Set(ref AddNotifyAudioBeforeRenderOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.RoomName = other.Value.RoomName;
				this.UnmixedAudio = other.Value.UnmixedAudio;
			}
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x00011990 File Offset: 0x0000FB90
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_RoomName);
		}

		// Token: 0x04000563 RID: 1379
		private int m_ApiVersion;

		// Token: 0x04000564 RID: 1380
		private IntPtr m_LocalUserId;

		// Token: 0x04000565 RID: 1381
		private IntPtr m_RoomName;

		// Token: 0x04000566 RID: 1382
		private int m_UnmixedAudio;
	}
}
