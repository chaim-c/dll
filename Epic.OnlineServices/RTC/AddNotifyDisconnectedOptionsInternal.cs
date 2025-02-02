using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x02000174 RID: 372
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyDisconnectedOptionsInternal : ISettable<AddNotifyDisconnectedOptions>, IDisposable
	{
		// Token: 0x17000266 RID: 614
		// (set) Token: 0x06000A98 RID: 2712 RVA: 0x0000FD1D File Offset: 0x0000DF1D
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000267 RID: 615
		// (set) Token: 0x06000A99 RID: 2713 RVA: 0x0000FD2D File Offset: 0x0000DF2D
		public Utf8String RoomName
		{
			set
			{
				Helper.Set(value, ref this.m_RoomName);
			}
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x0000FD3D File Offset: 0x0000DF3D
		public void Set(ref AddNotifyDisconnectedOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.RoomName = other.RoomName;
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x0000FD64 File Offset: 0x0000DF64
		public void Set(ref AddNotifyDisconnectedOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.RoomName = other.Value.RoomName;
			}
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x0000FDAF File Offset: 0x0000DFAF
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_RoomName);
		}

		// Token: 0x040004E3 RID: 1251
		private int m_ApiVersion;

		// Token: 0x040004E4 RID: 1252
		private IntPtr m_LocalUserId;

		// Token: 0x040004E5 RID: 1253
		private IntPtr m_RoomName;
	}
}
