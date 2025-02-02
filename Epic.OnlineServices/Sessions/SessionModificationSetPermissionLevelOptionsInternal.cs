using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000141 RID: 321
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionModificationSetPermissionLevelOptionsInternal : ISettable<SessionModificationSetPermissionLevelOptions>, IDisposable
	{
		// Token: 0x17000212 RID: 530
		// (set) Token: 0x06000969 RID: 2409 RVA: 0x0000DAEE File Offset: 0x0000BCEE
		public OnlineSessionPermissionLevel PermissionLevel
		{
			set
			{
				this.m_PermissionLevel = value;
			}
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x0000DAF8 File Offset: 0x0000BCF8
		public void Set(ref SessionModificationSetPermissionLevelOptions other)
		{
			this.m_ApiVersion = 1;
			this.PermissionLevel = other.PermissionLevel;
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x0000DB10 File Offset: 0x0000BD10
		public void Set(ref SessionModificationSetPermissionLevelOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.PermissionLevel = other.Value.PermissionLevel;
			}
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x0000DB46 File Offset: 0x0000BD46
		public void Dispose()
		{
		}

		// Token: 0x04000453 RID: 1107
		private int m_ApiVersion;

		// Token: 0x04000454 RID: 1108
		private OnlineSessionPermissionLevel m_PermissionLevel;
	}
}
