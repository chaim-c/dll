using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x0200004E RID: 78
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetToggleFriendsKeyOptionsInternal : ISettable<GetToggleFriendsKeyOptions>, IDisposable
	{
		// Token: 0x06000412 RID: 1042 RVA: 0x000064AD File Offset: 0x000046AD
		public void Set(ref GetToggleFriendsKeyOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x000064B8 File Offset: 0x000046B8
		public void Set(ref GetToggleFriendsKeyOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x000064D9 File Offset: 0x000046D9
		public void Dispose()
		{
		}

		// Token: 0x040001B8 RID: 440
		private int m_ApiVersion;
	}
}
