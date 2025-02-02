using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200011C RID: 284
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SendInviteOptionsInternal : ISettable<SendInviteOptions>, IDisposable
	{
		// Token: 0x170001CE RID: 462
		// (set) Token: 0x0600089F RID: 2207 RVA: 0x0000C5E0 File Offset: 0x0000A7E0
		public Utf8String SessionName
		{
			set
			{
				Helper.Set(value, ref this.m_SessionName);
			}
		}

		// Token: 0x170001CF RID: 463
		// (set) Token: 0x060008A0 RID: 2208 RVA: 0x0000C5F0 File Offset: 0x0000A7F0
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x170001D0 RID: 464
		// (set) Token: 0x060008A1 RID: 2209 RVA: 0x0000C600 File Offset: 0x0000A800
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0000C610 File Offset: 0x0000A810
		public void Set(ref SendInviteOptions other)
		{
			this.m_ApiVersion = 1;
			this.SessionName = other.SessionName;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0000C644 File Offset: 0x0000A844
		public void Set(ref SendInviteOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.SessionName = other.Value.SessionName;
				this.LocalUserId = other.Value.LocalUserId;
				this.TargetUserId = other.Value.TargetUserId;
			}
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x0000C6A4 File Offset: 0x0000A8A4
		public void Dispose()
		{
			Helper.Dispose(ref this.m_SessionName);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x040003EC RID: 1004
		private int m_ApiVersion;

		// Token: 0x040003ED RID: 1005
		private IntPtr m_SessionName;

		// Token: 0x040003EE RID: 1006
		private IntPtr m_LocalUserId;

		// Token: 0x040003EF RID: 1007
		private IntPtr m_TargetUserId;
	}
}
