using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000567 RID: 1383
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UnlinkAccountOptionsInternal : ISettable<UnlinkAccountOptions>, IDisposable
	{
		// Token: 0x17000A52 RID: 2642
		// (set) Token: 0x0600235A RID: 9050 RVA: 0x000344E7 File Offset: 0x000326E7
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x0600235B RID: 9051 RVA: 0x000344F7 File Offset: 0x000326F7
		public void Set(ref UnlinkAccountOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x0600235C RID: 9052 RVA: 0x00034510 File Offset: 0x00032710
		public void Set(ref UnlinkAccountOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x0600235D RID: 9053 RVA: 0x00034546 File Offset: 0x00032746
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000F89 RID: 3977
		private int m_ApiVersion;

		// Token: 0x04000F8A RID: 3978
		private IntPtr m_LocalUserId;
	}
}
