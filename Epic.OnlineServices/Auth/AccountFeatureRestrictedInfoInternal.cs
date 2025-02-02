using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x0200056F RID: 1391
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AccountFeatureRestrictedInfoInternal : IGettable<AccountFeatureRestrictedInfo>, ISettable<AccountFeatureRestrictedInfo>, IDisposable
	{
		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x060023A7 RID: 9127 RVA: 0x00034D10 File Offset: 0x00032F10
		// (set) Token: 0x060023A8 RID: 9128 RVA: 0x00034D31 File Offset: 0x00032F31
		public Utf8String VerificationURI
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_VerificationURI, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_VerificationURI);
			}
		}

		// Token: 0x060023A9 RID: 9129 RVA: 0x00034D41 File Offset: 0x00032F41
		public void Set(ref AccountFeatureRestrictedInfo other)
		{
			this.m_ApiVersion = 1;
			this.VerificationURI = other.VerificationURI;
		}

		// Token: 0x060023AA RID: 9130 RVA: 0x00034D58 File Offset: 0x00032F58
		public void Set(ref AccountFeatureRestrictedInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.VerificationURI = other.Value.VerificationURI;
			}
		}

		// Token: 0x060023AB RID: 9131 RVA: 0x00034D8E File Offset: 0x00032F8E
		public void Dispose()
		{
			Helper.Dispose(ref this.m_VerificationURI);
		}

		// Token: 0x060023AC RID: 9132 RVA: 0x00034D9D File Offset: 0x00032F9D
		public void Get(out AccountFeatureRestrictedInfo output)
		{
			output = default(AccountFeatureRestrictedInfo);
			output.Set(ref this);
		}

		// Token: 0x04000FAA RID: 4010
		private int m_ApiVersion;

		// Token: 0x04000FAB RID: 4011
		private IntPtr m_VerificationURI;
	}
}
