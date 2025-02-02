using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200013B RID: 315
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionModificationSetInvitesAllowedOptionsInternal : ISettable<SessionModificationSetInvitesAllowedOptions>, IDisposable
	{
		// Token: 0x1700020C RID: 524
		// (set) Token: 0x06000957 RID: 2391 RVA: 0x0000D99A File Offset: 0x0000BB9A
		public bool InvitesAllowed
		{
			set
			{
				Helper.Set(value, ref this.m_InvitesAllowed);
			}
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x0000D9AA File Offset: 0x0000BBAA
		public void Set(ref SessionModificationSetInvitesAllowedOptions other)
		{
			this.m_ApiVersion = 1;
			this.InvitesAllowed = other.InvitesAllowed;
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x0000D9C4 File Offset: 0x0000BBC4
		public void Set(ref SessionModificationSetInvitesAllowedOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.InvitesAllowed = other.Value.InvitesAllowed;
			}
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x0000D9FA File Offset: 0x0000BBFA
		public void Dispose()
		{
		}

		// Token: 0x0400044A RID: 1098
		private int m_ApiVersion;

		// Token: 0x0400044B RID: 1099
		private int m_InvitesAllowed;
	}
}
