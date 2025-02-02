using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000257 RID: 599
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryPresenceOptionsInternal : ISettable<QueryPresenceOptions>, IDisposable
	{
		// Token: 0x17000453 RID: 1107
		// (set) Token: 0x06001073 RID: 4211 RVA: 0x000186F9 File Offset: 0x000168F9
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000454 RID: 1108
		// (set) Token: 0x06001074 RID: 4212 RVA: 0x00018709 File Offset: 0x00016909
		public EpicAccountId TargetUserId
		{
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x00018719 File Offset: 0x00016919
		public void Set(ref QueryPresenceOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x00018740 File Offset: 0x00016940
		public void Set(ref QueryPresenceOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.TargetUserId = other.Value.TargetUserId;
			}
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x0001878B File Offset: 0x0001698B
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x0400075F RID: 1887
		private int m_ApiVersion;

		// Token: 0x04000760 RID: 1888
		private IntPtr m_LocalUserId;

		// Token: 0x04000761 RID: 1889
		private IntPtr m_TargetUserId;
	}
}
