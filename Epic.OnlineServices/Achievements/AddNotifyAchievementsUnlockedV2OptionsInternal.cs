using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000665 RID: 1637
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyAchievementsUnlockedV2OptionsInternal : ISettable<AddNotifyAchievementsUnlockedV2Options>, IDisposable
	{
		// Token: 0x060029FB RID: 10747 RVA: 0x0003EED0 File Offset: 0x0003D0D0
		public void Set(ref AddNotifyAchievementsUnlockedV2Options other)
		{
			this.m_ApiVersion = 2;
		}

		// Token: 0x060029FC RID: 10748 RVA: 0x0003EEDC File Offset: 0x0003D0DC
		public void Set(ref AddNotifyAchievementsUnlockedV2Options? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 2;
			}
		}

		// Token: 0x060029FD RID: 10749 RVA: 0x0003EEFD File Offset: 0x0003D0FD
		public void Dispose()
		{
		}

		// Token: 0x0400131E RID: 4894
		private int m_ApiVersion;
	}
}
