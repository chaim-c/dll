using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x02000463 RID: 1123
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyFriendsUpdateOptionsInternal : ISettable<AddNotifyFriendsUpdateOptions>, IDisposable
	{
		// Token: 0x06001CC0 RID: 7360 RVA: 0x0002A81E File Offset: 0x00028A1E
		public void Set(ref AddNotifyFriendsUpdateOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x06001CC1 RID: 7361 RVA: 0x0002A828 File Offset: 0x00028A28
		public void Set(ref AddNotifyFriendsUpdateOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06001CC2 RID: 7362 RVA: 0x0002A849 File Offset: 0x00028A49
		public void Dispose()
		{
		}

		// Token: 0x04000CBC RID: 3260
		private int m_ApiVersion;
	}
}
