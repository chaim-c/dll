using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x0200044A RID: 1098
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryPermissionsOptionsInternal : ISettable<QueryPermissionsOptions>, IDisposable
	{
		// Token: 0x170007FB RID: 2043
		// (set) Token: 0x06001C32 RID: 7218 RVA: 0x00029AC9 File Offset: 0x00027CC9
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x06001C33 RID: 7219 RVA: 0x00029AD9 File Offset: 0x00027CD9
		public void Set(ref QueryPermissionsOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x06001C34 RID: 7220 RVA: 0x00029AF0 File Offset: 0x00027CF0
		public void Set(ref QueryPermissionsOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06001C35 RID: 7221 RVA: 0x00029B26 File Offset: 0x00027D26
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000C77 RID: 3191
		private int m_ApiVersion;

		// Token: 0x04000C78 RID: 3192
		private IntPtr m_LocalUserId;
	}
}
