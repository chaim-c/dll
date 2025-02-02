using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x0200042E RID: 1070
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetPermissionByKeyOptionsInternal : ISettable<GetPermissionByKeyOptions>, IDisposable
	{
		// Token: 0x170007D7 RID: 2007
		// (set) Token: 0x06001B8E RID: 7054 RVA: 0x00028CFD File Offset: 0x00026EFD
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x170007D8 RID: 2008
		// (set) Token: 0x06001B8F RID: 7055 RVA: 0x00028D0D File Offset: 0x00026F0D
		public Utf8String Key
		{
			set
			{
				Helper.Set(value, ref this.m_Key);
			}
		}

		// Token: 0x06001B90 RID: 7056 RVA: 0x00028D1D File Offset: 0x00026F1D
		public void Set(ref GetPermissionByKeyOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.Key = other.Key;
		}

		// Token: 0x06001B91 RID: 7057 RVA: 0x00028D44 File Offset: 0x00026F44
		public void Set(ref GetPermissionByKeyOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.Key = other.Value.Key;
			}
		}

		// Token: 0x06001B92 RID: 7058 RVA: 0x00028D8F File Offset: 0x00026F8F
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_Key);
		}

		// Token: 0x04000C42 RID: 3138
		private int m_ApiVersion;

		// Token: 0x04000C43 RID: 3139
		private IntPtr m_LocalUserId;

		// Token: 0x04000C44 RID: 3140
		private IntPtr m_Key;
	}
}
