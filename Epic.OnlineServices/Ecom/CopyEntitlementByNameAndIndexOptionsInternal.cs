using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000495 RID: 1173
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyEntitlementByNameAndIndexOptionsInternal : ISettable<CopyEntitlementByNameAndIndexOptions>, IDisposable
	{
		// Token: 0x170008C5 RID: 2245
		// (set) Token: 0x06001E62 RID: 7778 RVA: 0x0002CFA9 File Offset: 0x0002B1A9
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x170008C6 RID: 2246
		// (set) Token: 0x06001E63 RID: 7779 RVA: 0x0002CFB9 File Offset: 0x0002B1B9
		public Utf8String EntitlementName
		{
			set
			{
				Helper.Set(value, ref this.m_EntitlementName);
			}
		}

		// Token: 0x170008C7 RID: 2247
		// (set) Token: 0x06001E64 RID: 7780 RVA: 0x0002CFC9 File Offset: 0x0002B1C9
		public uint Index
		{
			set
			{
				this.m_Index = value;
			}
		}

		// Token: 0x06001E65 RID: 7781 RVA: 0x0002CFD3 File Offset: 0x0002B1D3
		public void Set(ref CopyEntitlementByNameAndIndexOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.EntitlementName = other.EntitlementName;
			this.Index = other.Index;
		}

		// Token: 0x06001E66 RID: 7782 RVA: 0x0002D004 File Offset: 0x0002B204
		public void Set(ref CopyEntitlementByNameAndIndexOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.EntitlementName = other.Value.EntitlementName;
				this.Index = other.Value.Index;
			}
		}

		// Token: 0x06001E67 RID: 7783 RVA: 0x0002D064 File Offset: 0x0002B264
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_EntitlementName);
		}

		// Token: 0x04000D6D RID: 3437
		private int m_ApiVersion;

		// Token: 0x04000D6E RID: 3438
		private IntPtr m_LocalUserId;

		// Token: 0x04000D6F RID: 3439
		private IntPtr m_EntitlementName;

		// Token: 0x04000D70 RID: 3440
		private uint m_Index;
	}
}
