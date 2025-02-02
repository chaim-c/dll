using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200067D RID: 1661
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetPlayerAchievementCountOptionsInternal : ISettable<GetPlayerAchievementCountOptions>, IDisposable
	{
		// Token: 0x17000CE9 RID: 3305
		// (set) Token: 0x06002AA3 RID: 10915 RVA: 0x00040025 File Offset: 0x0003E225
		public ProductUserId UserId
		{
			set
			{
				Helper.Set(value, ref this.m_UserId);
			}
		}

		// Token: 0x06002AA4 RID: 10916 RVA: 0x00040035 File Offset: 0x0003E235
		public void Set(ref GetPlayerAchievementCountOptions other)
		{
			this.m_ApiVersion = 1;
			this.UserId = other.UserId;
		}

		// Token: 0x06002AA5 RID: 10917 RVA: 0x0004004C File Offset: 0x0003E24C
		public void Set(ref GetPlayerAchievementCountOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.UserId = other.Value.UserId;
			}
		}

		// Token: 0x06002AA6 RID: 10918 RVA: 0x00040082 File Offset: 0x0003E282
		public void Dispose()
		{
			Helper.Dispose(ref this.m_UserId);
		}

		// Token: 0x04001373 RID: 4979
		private int m_ApiVersion;

		// Token: 0x04001374 RID: 4980
		private IntPtr m_UserId;
	}
}
