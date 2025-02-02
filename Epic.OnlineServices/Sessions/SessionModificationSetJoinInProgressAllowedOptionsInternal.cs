using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200013D RID: 317
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionModificationSetJoinInProgressAllowedOptionsInternal : ISettable<SessionModificationSetJoinInProgressAllowedOptions>, IDisposable
	{
		// Token: 0x1700020E RID: 526
		// (set) Token: 0x0600095D RID: 2397 RVA: 0x0000DA0E File Offset: 0x0000BC0E
		public bool AllowJoinInProgress
		{
			set
			{
				Helper.Set(value, ref this.m_AllowJoinInProgress);
			}
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x0000DA1E File Offset: 0x0000BC1E
		public void Set(ref SessionModificationSetJoinInProgressAllowedOptions other)
		{
			this.m_ApiVersion = 1;
			this.AllowJoinInProgress = other.AllowJoinInProgress;
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x0000DA38 File Offset: 0x0000BC38
		public void Set(ref SessionModificationSetJoinInProgressAllowedOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.AllowJoinInProgress = other.Value.AllowJoinInProgress;
			}
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x0000DA6E File Offset: 0x0000BC6E
		public void Dispose()
		{
		}

		// Token: 0x0400044D RID: 1101
		private int m_ApiVersion;

		// Token: 0x0400044E RID: 1102
		private int m_AllowJoinInProgress;
	}
}
