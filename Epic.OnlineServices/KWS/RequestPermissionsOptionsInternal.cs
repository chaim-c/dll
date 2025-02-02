using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x0200044E RID: 1102
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RequestPermissionsOptionsInternal : ISettable<RequestPermissionsOptions>, IDisposable
	{
		// Token: 0x17000805 RID: 2053
		// (set) Token: 0x06001C4D RID: 7245 RVA: 0x00029D24 File Offset: 0x00027F24
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000806 RID: 2054
		// (set) Token: 0x06001C4E RID: 7246 RVA: 0x00029D34 File Offset: 0x00027F34
		public Utf8String[] PermissionKeys
		{
			set
			{
				Helper.Set<Utf8String>(value, ref this.m_PermissionKeys, true, out this.m_PermissionKeyCount);
			}
		}

		// Token: 0x06001C4F RID: 7247 RVA: 0x00029D4B File Offset: 0x00027F4B
		public void Set(ref RequestPermissionsOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.PermissionKeys = other.PermissionKeys;
		}

		// Token: 0x06001C50 RID: 7248 RVA: 0x00029D70 File Offset: 0x00027F70
		public void Set(ref RequestPermissionsOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.PermissionKeys = other.Value.PermissionKeys;
			}
		}

		// Token: 0x06001C51 RID: 7249 RVA: 0x00029DBB File Offset: 0x00027FBB
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_PermissionKeys);
		}

		// Token: 0x04000C81 RID: 3201
		private int m_ApiVersion;

		// Token: 0x04000C82 RID: 3202
		private IntPtr m_LocalUserId;

		// Token: 0x04000C83 RID: 3203
		private uint m_PermissionKeyCount;

		// Token: 0x04000C84 RID: 3204
		private IntPtr m_PermissionKeys;
	}
}
