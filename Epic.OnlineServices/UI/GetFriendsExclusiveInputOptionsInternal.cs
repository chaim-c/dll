using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x0200004A RID: 74
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetFriendsExclusiveInputOptionsInternal : ISettable<GetFriendsExclusiveInputOptions>, IDisposable
	{
		// Token: 0x17000074 RID: 116
		// (set) Token: 0x06000408 RID: 1032 RVA: 0x000063C1 File Offset: 0x000045C1
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x000063D1 File Offset: 0x000045D1
		public void Set(ref GetFriendsExclusiveInputOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x000063E8 File Offset: 0x000045E8
		public void Set(ref GetFriendsExclusiveInputOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000641E File Offset: 0x0000461E
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x040001B3 RID: 435
		private int m_ApiVersion;

		// Token: 0x040001B4 RID: 436
		private IntPtr m_LocalUserId;
	}
}
