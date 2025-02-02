using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x0200051B RID: 1307
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyProductUserExternalAccountByIndexOptionsInternal : ISettable<CopyProductUserExternalAccountByIndexOptions>, IDisposable
	{
		// Token: 0x170009C9 RID: 2505
		// (set) Token: 0x06002199 RID: 8601 RVA: 0x000323FC File Offset: 0x000305FC
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x170009CA RID: 2506
		// (set) Token: 0x0600219A RID: 8602 RVA: 0x0003240C File Offset: 0x0003060C
		public uint ExternalAccountInfoIndex
		{
			set
			{
				this.m_ExternalAccountInfoIndex = value;
			}
		}

		// Token: 0x0600219B RID: 8603 RVA: 0x00032416 File Offset: 0x00030616
		public void Set(ref CopyProductUserExternalAccountByIndexOptions other)
		{
			this.m_ApiVersion = 1;
			this.TargetUserId = other.TargetUserId;
			this.ExternalAccountInfoIndex = other.ExternalAccountInfoIndex;
		}

		// Token: 0x0600219C RID: 8604 RVA: 0x0003243C File Offset: 0x0003063C
		public void Set(ref CopyProductUserExternalAccountByIndexOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.TargetUserId = other.Value.TargetUserId;
				this.ExternalAccountInfoIndex = other.Value.ExternalAccountInfoIndex;
			}
		}

		// Token: 0x0600219D RID: 8605 RVA: 0x00032487 File Offset: 0x00030687
		public void Dispose()
		{
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x04000EF8 RID: 3832
		private int m_ApiVersion;

		// Token: 0x04000EF9 RID: 3833
		private IntPtr m_TargetUserId;

		// Token: 0x04000EFA RID: 3834
		private uint m_ExternalAccountInfoIndex;
	}
}
