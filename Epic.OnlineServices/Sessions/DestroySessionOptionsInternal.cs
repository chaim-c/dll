using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000DE RID: 222
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DestroySessionOptionsInternal : ISettable<DestroySessionOptions>, IDisposable
	{
		// Token: 0x1700017F RID: 383
		// (set) Token: 0x06000769 RID: 1897 RVA: 0x0000B366 File Offset: 0x00009566
		public Utf8String SessionName
		{
			set
			{
				Helper.Set(value, ref this.m_SessionName);
			}
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x0000B376 File Offset: 0x00009576
		public void Set(ref DestroySessionOptions other)
		{
			this.m_ApiVersion = 1;
			this.SessionName = other.SessionName;
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0000B390 File Offset: 0x00009590
		public void Set(ref DestroySessionOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.SessionName = other.Value.SessionName;
			}
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0000B3C6 File Offset: 0x000095C6
		public void Dispose()
		{
			Helper.Dispose(ref this.m_SessionName);
		}

		// Token: 0x0400038A RID: 906
		private int m_ApiVersion;

		// Token: 0x0400038B RID: 907
		private IntPtr m_SessionName;
	}
}
