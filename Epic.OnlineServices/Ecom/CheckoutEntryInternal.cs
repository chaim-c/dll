using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200048D RID: 1165
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CheckoutEntryInternal : IGettable<CheckoutEntry>, ISettable<CheckoutEntry>, IDisposable
	{
		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x06001E38 RID: 7736 RVA: 0x0002CC28 File Offset: 0x0002AE28
		// (set) Token: 0x06001E39 RID: 7737 RVA: 0x0002CC49 File Offset: 0x0002AE49
		public Utf8String OfferId
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_OfferId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_OfferId);
			}
		}

		// Token: 0x06001E3A RID: 7738 RVA: 0x0002CC59 File Offset: 0x0002AE59
		public void Set(ref CheckoutEntry other)
		{
			this.m_ApiVersion = 1;
			this.OfferId = other.OfferId;
		}

		// Token: 0x06001E3B RID: 7739 RVA: 0x0002CC70 File Offset: 0x0002AE70
		public void Set(ref CheckoutEntry? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.OfferId = other.Value.OfferId;
			}
		}

		// Token: 0x06001E3C RID: 7740 RVA: 0x0002CCA6 File Offset: 0x0002AEA6
		public void Dispose()
		{
			Helper.Dispose(ref this.m_OfferId);
		}

		// Token: 0x06001E3D RID: 7741 RVA: 0x0002CCB5 File Offset: 0x0002AEB5
		public void Get(out CheckoutEntry output)
		{
			output = default(CheckoutEntry);
			output.Set(ref this);
		}

		// Token: 0x04000D56 RID: 3414
		private int m_ApiVersion;

		// Token: 0x04000D57 RID: 3415
		private IntPtr m_OfferId;
	}
}
