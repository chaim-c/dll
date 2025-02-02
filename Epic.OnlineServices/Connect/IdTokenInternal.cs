using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000535 RID: 1333
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct IdTokenInternal : IGettable<IdToken>, ISettable<IdToken>, IDisposable
	{
		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x0600222D RID: 8749 RVA: 0x0003316C File Offset: 0x0003136C
		// (set) Token: 0x0600222E RID: 8750 RVA: 0x0003318D File Offset: 0x0003138D
		public ProductUserId ProductUserId
		{
			get
			{
				ProductUserId result;
				Helper.Get<ProductUserId>(this.m_ProductUserId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_ProductUserId);
			}
		}

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x0600222F RID: 8751 RVA: 0x000331A0 File Offset: 0x000313A0
		// (set) Token: 0x06002230 RID: 8752 RVA: 0x000331C1 File Offset: 0x000313C1
		public Utf8String JsonWebToken
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_JsonWebToken, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_JsonWebToken);
			}
		}

		// Token: 0x06002231 RID: 8753 RVA: 0x000331D1 File Offset: 0x000313D1
		public void Set(ref IdToken other)
		{
			this.m_ApiVersion = 1;
			this.ProductUserId = other.ProductUserId;
			this.JsonWebToken = other.JsonWebToken;
		}

		// Token: 0x06002232 RID: 8754 RVA: 0x000331F8 File Offset: 0x000313F8
		public void Set(ref IdToken? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.ProductUserId = other.Value.ProductUserId;
				this.JsonWebToken = other.Value.JsonWebToken;
			}
		}

		// Token: 0x06002233 RID: 8755 RVA: 0x00033243 File Offset: 0x00031443
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ProductUserId);
			Helper.Dispose(ref this.m_JsonWebToken);
		}

		// Token: 0x06002234 RID: 8756 RVA: 0x0003325E File Offset: 0x0003145E
		public void Get(out IdToken output)
		{
			output = default(IdToken);
			output.Set(ref this);
		}

		// Token: 0x04000F36 RID: 3894
		private int m_ApiVersion;

		// Token: 0x04000F37 RID: 3895
		private IntPtr m_ProductUserId;

		// Token: 0x04000F38 RID: 3896
		private IntPtr m_JsonWebToken;
	}
}
