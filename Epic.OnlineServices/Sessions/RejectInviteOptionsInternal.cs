using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000118 RID: 280
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RejectInviteOptionsInternal : ISettable<RejectInviteOptions>, IDisposable
	{
		// Token: 0x170001C4 RID: 452
		// (set) Token: 0x06000885 RID: 2181 RVA: 0x0000C3AF File Offset: 0x0000A5AF
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x170001C5 RID: 453
		// (set) Token: 0x06000886 RID: 2182 RVA: 0x0000C3BF File Offset: 0x0000A5BF
		public Utf8String InviteId
		{
			set
			{
				Helper.Set(value, ref this.m_InviteId);
			}
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x0000C3CF File Offset: 0x0000A5CF
		public void Set(ref RejectInviteOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.InviteId = other.InviteId;
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0000C3F4 File Offset: 0x0000A5F4
		public void Set(ref RejectInviteOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.InviteId = other.Value.InviteId;
			}
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0000C43F File Offset: 0x0000A63F
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_InviteId);
		}

		// Token: 0x040003E2 RID: 994
		private int m_ApiVersion;

		// Token: 0x040003E3 RID: 995
		private IntPtr m_LocalUserId;

		// Token: 0x040003E4 RID: 996
		private IntPtr m_InviteId;
	}
}
