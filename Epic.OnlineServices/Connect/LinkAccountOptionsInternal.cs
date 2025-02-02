using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000539 RID: 1337
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LinkAccountOptionsInternal : ISettable<LinkAccountOptions>, IDisposable
	{
		// Token: 0x17000A0B RID: 2571
		// (set) Token: 0x0600224C RID: 8780 RVA: 0x00033460 File Offset: 0x00031660
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000A0C RID: 2572
		// (set) Token: 0x0600224D RID: 8781 RVA: 0x00033470 File Offset: 0x00031670
		public ContinuanceToken ContinuanceToken
		{
			set
			{
				Helper.Set(value, ref this.m_ContinuanceToken);
			}
		}

		// Token: 0x0600224E RID: 8782 RVA: 0x00033480 File Offset: 0x00031680
		public void Set(ref LinkAccountOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.ContinuanceToken = other.ContinuanceToken;
		}

		// Token: 0x0600224F RID: 8783 RVA: 0x000334A4 File Offset: 0x000316A4
		public void Set(ref LinkAccountOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.ContinuanceToken = other.Value.ContinuanceToken;
			}
		}

		// Token: 0x06002250 RID: 8784 RVA: 0x000334EF File Offset: 0x000316EF
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_ContinuanceToken);
		}

		// Token: 0x04000F41 RID: 3905
		private int m_ApiVersion;

		// Token: 0x04000F42 RID: 3906
		private IntPtr m_LocalUserId;

		// Token: 0x04000F43 RID: 3907
		private IntPtr m_ContinuanceToken;
	}
}
