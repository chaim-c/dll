using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001DD RID: 477
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SendAudioOptionsInternal : ISettable<SendAudioOptions>, IDisposable
	{
		// Token: 0x17000336 RID: 822
		// (set) Token: 0x06000D4B RID: 3403 RVA: 0x00013B2A File Offset: 0x00011D2A
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000337 RID: 823
		// (set) Token: 0x06000D4C RID: 3404 RVA: 0x00013B3A File Offset: 0x00011D3A
		public Utf8String RoomName
		{
			set
			{
				Helper.Set(value, ref this.m_RoomName);
			}
		}

		// Token: 0x17000338 RID: 824
		// (set) Token: 0x06000D4D RID: 3405 RVA: 0x00013B4A File Offset: 0x00011D4A
		public AudioBuffer? Buffer
		{
			set
			{
				Helper.Set<AudioBuffer, AudioBufferInternal>(ref value, ref this.m_Buffer);
			}
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x00013B5B File Offset: 0x00011D5B
		public void Set(ref SendAudioOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.RoomName = other.RoomName;
			this.Buffer = other.Buffer;
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x00013B8C File Offset: 0x00011D8C
		public void Set(ref SendAudioOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.RoomName = other.Value.RoomName;
				this.Buffer = other.Value.Buffer;
			}
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x00013BEC File Offset: 0x00011DEC
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_RoomName);
			Helper.Dispose(ref this.m_Buffer);
		}

		// Token: 0x040005F7 RID: 1527
		private int m_ApiVersion;

		// Token: 0x040005F8 RID: 1528
		private IntPtr m_LocalUserId;

		// Token: 0x040005F9 RID: 1529
		private IntPtr m_RoomName;

		// Token: 0x040005FA RID: 1530
		private IntPtr m_Buffer;
	}
}
