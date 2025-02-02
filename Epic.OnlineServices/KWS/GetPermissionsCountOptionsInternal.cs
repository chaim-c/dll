using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x02000430 RID: 1072
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetPermissionsCountOptionsInternal : ISettable<GetPermissionsCountOptions>, IDisposable
	{
		// Token: 0x170007DA RID: 2010
		// (set) Token: 0x06001B95 RID: 7061 RVA: 0x00028DBB File Offset: 0x00026FBB
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x06001B96 RID: 7062 RVA: 0x00028DCB File Offset: 0x00026FCB
		public void Set(ref GetPermissionsCountOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x06001B97 RID: 7063 RVA: 0x00028DE4 File Offset: 0x00026FE4
		public void Set(ref GetPermissionsCountOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06001B98 RID: 7064 RVA: 0x00028E1A File Offset: 0x0002701A
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000C46 RID: 3142
		private int m_ApiVersion;

		// Token: 0x04000C47 RID: 3143
		private IntPtr m_LocalUserId;
	}
}
