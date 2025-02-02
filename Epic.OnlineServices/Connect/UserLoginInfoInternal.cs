using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000569 RID: 1385
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UserLoginInfoInternal : IGettable<UserLoginInfo>, ISettable<UserLoginInfo>, IDisposable
	{
		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x06002361 RID: 9057 RVA: 0x00034578 File Offset: 0x00032778
		// (set) Token: 0x06002362 RID: 9058 RVA: 0x00034599 File Offset: 0x00032799
		public Utf8String DisplayName
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_DisplayName, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_DisplayName);
			}
		}

		// Token: 0x06002363 RID: 9059 RVA: 0x000345A9 File Offset: 0x000327A9
		public void Set(ref UserLoginInfo other)
		{
			this.m_ApiVersion = 1;
			this.DisplayName = other.DisplayName;
		}

		// Token: 0x06002364 RID: 9060 RVA: 0x000345C0 File Offset: 0x000327C0
		public void Set(ref UserLoginInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.DisplayName = other.Value.DisplayName;
			}
		}

		// Token: 0x06002365 RID: 9061 RVA: 0x000345F6 File Offset: 0x000327F6
		public void Dispose()
		{
			Helper.Dispose(ref this.m_DisplayName);
		}

		// Token: 0x06002366 RID: 9062 RVA: 0x00034605 File Offset: 0x00032805
		public void Get(out UserLoginInfo output)
		{
			output = default(UserLoginInfo);
			output.Set(ref this);
		}

		// Token: 0x04000F8C RID: 3980
		private int m_ApiVersion;

		// Token: 0x04000F8D RID: 3981
		private IntPtr m_DisplayName;
	}
}
