using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000663 RID: 1635
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyAchievementsUnlockedOptionsInternal : ISettable<AddNotifyAchievementsUnlockedOptions>, IDisposable
	{
		// Token: 0x060029F8 RID: 10744 RVA: 0x0003EE9F File Offset: 0x0003D09F
		public void Set(ref AddNotifyAchievementsUnlockedOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x060029F9 RID: 10745 RVA: 0x0003EEAC File Offset: 0x0003D0AC
		public void Set(ref AddNotifyAchievementsUnlockedOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x060029FA RID: 10746 RVA: 0x0003EECD File Offset: 0x0003D0CD
		public void Dispose()
		{
		}

		// Token: 0x0400131D RID: 4893
		private int m_ApiVersion;
	}
}
