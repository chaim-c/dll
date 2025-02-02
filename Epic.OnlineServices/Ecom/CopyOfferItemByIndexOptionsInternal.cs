using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004A5 RID: 1189
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyOfferItemByIndexOptionsInternal : ISettable<CopyOfferItemByIndexOptions>, IDisposable
	{
		// Token: 0x170008ED RID: 2285
		// (set) Token: 0x06001EB6 RID: 7862 RVA: 0x0002D6E2 File Offset: 0x0002B8E2
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x170008EE RID: 2286
		// (set) Token: 0x06001EB7 RID: 7863 RVA: 0x0002D6F2 File Offset: 0x0002B8F2
		public Utf8String OfferId
		{
			set
			{
				Helper.Set(value, ref this.m_OfferId);
			}
		}

		// Token: 0x170008EF RID: 2287
		// (set) Token: 0x06001EB8 RID: 7864 RVA: 0x0002D702 File Offset: 0x0002B902
		public uint ItemIndex
		{
			set
			{
				this.m_ItemIndex = value;
			}
		}

		// Token: 0x06001EB9 RID: 7865 RVA: 0x0002D70C File Offset: 0x0002B90C
		public void Set(ref CopyOfferItemByIndexOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.OfferId = other.OfferId;
			this.ItemIndex = other.ItemIndex;
		}

		// Token: 0x06001EBA RID: 7866 RVA: 0x0002D740 File Offset: 0x0002B940
		public void Set(ref CopyOfferItemByIndexOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.OfferId = other.Value.OfferId;
				this.ItemIndex = other.Value.ItemIndex;
			}
		}

		// Token: 0x06001EBB RID: 7867 RVA: 0x0002D7A0 File Offset: 0x0002B9A0
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_OfferId);
		}

		// Token: 0x04000D9D RID: 3485
		private int m_ApiVersion;

		// Token: 0x04000D9E RID: 3486
		private IntPtr m_LocalUserId;

		// Token: 0x04000D9F RID: 3487
		private IntPtr m_OfferId;

		// Token: 0x04000DA0 RID: 3488
		private uint m_ItemIndex;
	}
}
