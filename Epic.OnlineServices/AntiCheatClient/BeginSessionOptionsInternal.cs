using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x02000620 RID: 1568
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct BeginSessionOptionsInternal : ISettable<BeginSessionOptions>, IDisposable
	{
		// Token: 0x17000BFC RID: 3068
		// (set) Token: 0x06002815 RID: 10261 RVA: 0x0003BEC9 File Offset: 0x0003A0C9
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000BFD RID: 3069
		// (set) Token: 0x06002816 RID: 10262 RVA: 0x0003BED9 File Offset: 0x0003A0D9
		public AntiCheatClientMode Mode
		{
			set
			{
				this.m_Mode = value;
			}
		}

		// Token: 0x06002817 RID: 10263 RVA: 0x0003BEE3 File Offset: 0x0003A0E3
		public void Set(ref BeginSessionOptions other)
		{
			this.m_ApiVersion = 3;
			this.LocalUserId = other.LocalUserId;
			this.Mode = other.Mode;
		}

		// Token: 0x06002818 RID: 10264 RVA: 0x0003BF08 File Offset: 0x0003A108
		public void Set(ref BeginSessionOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 3;
				this.LocalUserId = other.Value.LocalUserId;
				this.Mode = other.Value.Mode;
			}
		}

		// Token: 0x06002819 RID: 10265 RVA: 0x0003BF53 File Offset: 0x0003A153
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x0400121E RID: 4638
		private int m_ApiVersion;

		// Token: 0x0400121F RID: 4639
		private IntPtr m_LocalUserId;

		// Token: 0x04001220 RID: 4640
		private AntiCheatClientMode m_Mode;
	}
}
