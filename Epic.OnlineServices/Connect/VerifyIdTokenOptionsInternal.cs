using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x0200056D RID: 1389
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct VerifyIdTokenOptionsInternal : ISettable<VerifyIdTokenOptions>, IDisposable
	{
		// Token: 0x17000A6F RID: 2671
		// (set) Token: 0x060023A0 RID: 9120 RVA: 0x00034C7D File Offset: 0x00032E7D
		public IdToken? IdToken
		{
			set
			{
				Helper.Set<IdToken, IdTokenInternal>(ref value, ref this.m_IdToken);
			}
		}

		// Token: 0x060023A1 RID: 9121 RVA: 0x00034C8E File Offset: 0x00032E8E
		public void Set(ref VerifyIdTokenOptions other)
		{
			this.m_ApiVersion = 1;
			this.IdToken = other.IdToken;
		}

		// Token: 0x060023A2 RID: 9122 RVA: 0x00034CA8 File Offset: 0x00032EA8
		public void Set(ref VerifyIdTokenOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.IdToken = other.Value.IdToken;
			}
		}

		// Token: 0x060023A3 RID: 9123 RVA: 0x00034CDE File Offset: 0x00032EDE
		public void Dispose()
		{
			Helper.Dispose(ref this.m_IdToken);
		}

		// Token: 0x04000FA7 RID: 4007
		private int m_ApiVersion;

		// Token: 0x04000FA8 RID: 4008
		private IntPtr m_IdToken;
	}
}
