using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x02000428 RID: 1064
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyPermissionByIndexOptionsInternal : ISettable<CopyPermissionByIndexOptions>, IDisposable
	{
		// Token: 0x170007C2 RID: 1986
		// (set) Token: 0x06001B5E RID: 7006 RVA: 0x0002884A File Offset: 0x00026A4A
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x170007C3 RID: 1987
		// (set) Token: 0x06001B5F RID: 7007 RVA: 0x0002885A File Offset: 0x00026A5A
		public uint Index
		{
			set
			{
				this.m_Index = value;
			}
		}

		// Token: 0x06001B60 RID: 7008 RVA: 0x00028864 File Offset: 0x00026A64
		public void Set(ref CopyPermissionByIndexOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.Index = other.Index;
		}

		// Token: 0x06001B61 RID: 7009 RVA: 0x00028888 File Offset: 0x00026A88
		public void Set(ref CopyPermissionByIndexOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.Index = other.Value.Index;
			}
		}

		// Token: 0x06001B62 RID: 7010 RVA: 0x000288D3 File Offset: 0x00026AD3
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000C2C RID: 3116
		private int m_ApiVersion;

		// Token: 0x04000C2D RID: 3117
		private IntPtr m_LocalUserId;

		// Token: 0x04000C2E RID: 3118
		private uint m_Index;
	}
}
