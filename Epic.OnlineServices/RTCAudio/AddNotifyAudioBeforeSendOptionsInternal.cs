using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x0200019D RID: 413
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyAudioBeforeSendOptionsInternal : ISettable<AddNotifyAudioBeforeSendOptions>, IDisposable
	{
		// Token: 0x170002D7 RID: 727
		// (set) Token: 0x06000BCE RID: 3022 RVA: 0x000119CD File Offset: 0x0000FBCD
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x170002D8 RID: 728
		// (set) Token: 0x06000BCF RID: 3023 RVA: 0x000119DD File Offset: 0x0000FBDD
		public Utf8String RoomName
		{
			set
			{
				Helper.Set(value, ref this.m_RoomName);
			}
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x000119ED File Offset: 0x0000FBED
		public void Set(ref AddNotifyAudioBeforeSendOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.RoomName = other.RoomName;
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x00011A14 File Offset: 0x0000FC14
		public void Set(ref AddNotifyAudioBeforeSendOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.RoomName = other.Value.RoomName;
			}
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x00011A5F File Offset: 0x0000FC5F
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_RoomName);
		}

		// Token: 0x04000569 RID: 1385
		private int m_ApiVersion;

		// Token: 0x0400056A RID: 1386
		private IntPtr m_LocalUserId;

		// Token: 0x0400056B RID: 1387
		private IntPtr m_RoomName;
	}
}
