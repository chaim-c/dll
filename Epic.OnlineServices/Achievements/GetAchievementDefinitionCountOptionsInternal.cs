using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200067B RID: 1659
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetAchievementDefinitionCountOptionsInternal : ISettable<GetAchievementDefinitionCountOptions>, IDisposable
	{
		// Token: 0x06002A9E RID: 10910 RVA: 0x0003FFE4 File Offset: 0x0003E1E4
		public void Set(ref GetAchievementDefinitionCountOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x06002A9F RID: 10911 RVA: 0x0003FFF0 File Offset: 0x0003E1F0
		public void Set(ref GetAchievementDefinitionCountOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06002AA0 RID: 10912 RVA: 0x00040011 File Offset: 0x0003E211
		public void Dispose()
		{
		}

		// Token: 0x04001371 RID: 4977
		private int m_ApiVersion;
	}
}
