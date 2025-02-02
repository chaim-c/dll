using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002EB RID: 747
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SocketIdInternal : IGettable<SocketId>, ISettable<SocketId>, IDisposable
	{
		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x0600141F RID: 5151 RVA: 0x0001DD40 File Offset: 0x0001BF40
		// (set) Token: 0x06001420 RID: 5152 RVA: 0x0001DD61 File Offset: 0x0001BF61
		public string SocketName
		{
			get
			{
				string result;
				Helper.Get(this.m_SocketName, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_SocketName, 33);
			}
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x0001DD73 File Offset: 0x0001BF73
		public void Set(ref SocketId other)
		{
			this.m_ApiVersion = 1;
			this.SocketName = other.SocketName;
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x0001DD8C File Offset: 0x0001BF8C
		public void Set(ref SocketId? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.SocketName = other.Value.SocketName;
			}
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x0001DDC2 File Offset: 0x0001BFC2
		public void Dispose()
		{
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x0001DDC5 File Offset: 0x0001BFC5
		public void Get(out SocketId output)
		{
			output = default(SocketId);
			output.Set(ref this);
		}

		// Token: 0x04000908 RID: 2312
		private int m_ApiVersion;

		// Token: 0x04000909 RID: 2313
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 33)]
		private byte[] m_SocketName;
	}
}
