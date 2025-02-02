using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001A1 RID: 417
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyAudioInputStateOptionsInternal : ISettable<AddNotifyAudioInputStateOptions>, IDisposable
	{
		// Token: 0x170002DB RID: 731
		// (set) Token: 0x06000BDA RID: 3034 RVA: 0x00011ACA File Offset: 0x0000FCCA
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x170002DC RID: 732
		// (set) Token: 0x06000BDB RID: 3035 RVA: 0x00011ADA File Offset: 0x0000FCDA
		public Utf8String RoomName
		{
			set
			{
				Helper.Set(value, ref this.m_RoomName);
			}
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x00011AEA File Offset: 0x0000FCEA
		public void Set(ref AddNotifyAudioInputStateOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.RoomName = other.RoomName;
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x00011B10 File Offset: 0x0000FD10
		public void Set(ref AddNotifyAudioInputStateOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.RoomName = other.Value.RoomName;
			}
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x00011B5B File Offset: 0x0000FD5B
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_RoomName);
		}

		// Token: 0x0400056F RID: 1391
		private int m_ApiVersion;

		// Token: 0x04000570 RID: 1392
		private IntPtr m_LocalUserId;

		// Token: 0x04000571 RID: 1393
		private IntPtr m_RoomName;
	}
}
