using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x02000461 RID: 1121
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AcceptInviteOptionsInternal : ISettable<AcceptInviteOptions>, IDisposable
	{
		// Token: 0x1700082B RID: 2091
		// (set) Token: 0x06001CBB RID: 7355 RVA: 0x0002A771 File Offset: 0x00028971
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x1700082C RID: 2092
		// (set) Token: 0x06001CBC RID: 7356 RVA: 0x0002A781 File Offset: 0x00028981
		public EpicAccountId TargetUserId
		{
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x06001CBD RID: 7357 RVA: 0x0002A791 File Offset: 0x00028991
		public void Set(ref AcceptInviteOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
		}

		// Token: 0x06001CBE RID: 7358 RVA: 0x0002A7B8 File Offset: 0x000289B8
		public void Set(ref AcceptInviteOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.TargetUserId = other.Value.TargetUserId;
			}
		}

		// Token: 0x06001CBF RID: 7359 RVA: 0x0002A803 File Offset: 0x00028A03
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x04000CB9 RID: 3257
		private int m_ApiVersion;

		// Token: 0x04000CBA RID: 3258
		private IntPtr m_LocalUserId;

		// Token: 0x04000CBB RID: 3259
		private IntPtr m_TargetUserId;
	}
}
