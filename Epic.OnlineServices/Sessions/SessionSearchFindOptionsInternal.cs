using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000148 RID: 328
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionSearchFindOptionsInternal : ISettable<SessionSearchFindOptions>, IDisposable
	{
		// Token: 0x1700021B RID: 539
		// (set) Token: 0x06000990 RID: 2448 RVA: 0x0000DF7A File Offset: 0x0000C17A
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0000DF8A File Offset: 0x0000C18A
		public void Set(ref SessionSearchFindOptions other)
		{
			this.m_ApiVersion = 2;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0000DFA4 File Offset: 0x0000C1A4
		public void Set(ref SessionSearchFindOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 2;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x0000DFDA File Offset: 0x0000C1DA
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000465 RID: 1125
		private int m_ApiVersion;

		// Token: 0x04000466 RID: 1126
		private IntPtr m_LocalUserId;
	}
}
