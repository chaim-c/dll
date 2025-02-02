using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000EA RID: 234
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct IsUserInSessionOptionsInternal : ISettable<IsUserInSessionOptions>, IDisposable
	{
		// Token: 0x17000191 RID: 401
		// (set) Token: 0x0600079B RID: 1947 RVA: 0x0000B788 File Offset: 0x00009988
		public Utf8String SessionName
		{
			set
			{
				Helper.Set(value, ref this.m_SessionName);
			}
		}

		// Token: 0x17000192 RID: 402
		// (set) Token: 0x0600079C RID: 1948 RVA: 0x0000B798 File Offset: 0x00009998
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x0000B7A8 File Offset: 0x000099A8
		public void Set(ref IsUserInSessionOptions other)
		{
			this.m_ApiVersion = 1;
			this.SessionName = other.SessionName;
			this.TargetUserId = other.TargetUserId;
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x0000B7CC File Offset: 0x000099CC
		public void Set(ref IsUserInSessionOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.SessionName = other.Value.SessionName;
				this.TargetUserId = other.Value.TargetUserId;
			}
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0000B817 File Offset: 0x00009A17
		public void Dispose()
		{
			Helper.Dispose(ref this.m_SessionName);
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x040003A0 RID: 928
		private int m_ApiVersion;

		// Token: 0x040003A1 RID: 929
		private IntPtr m_SessionName;

		// Token: 0x040003A2 RID: 930
		private IntPtr m_TargetUserId;
	}
}
