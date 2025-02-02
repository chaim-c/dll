using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000253 RID: 595
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PresenceModificationSetStatusOptionsInternal : ISettable<PresenceModificationSetStatusOptions>, IDisposable
	{
		// Token: 0x17000447 RID: 1095
		// (set) Token: 0x06001054 RID: 4180 RVA: 0x0001842E File Offset: 0x0001662E
		public Status Status
		{
			set
			{
				this.m_Status = value;
			}
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x00018438 File Offset: 0x00016638
		public void Set(ref PresenceModificationSetStatusOptions other)
		{
			this.m_ApiVersion = 1;
			this.Status = other.Status;
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x00018450 File Offset: 0x00016650
		public void Set(ref PresenceModificationSetStatusOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.Status = other.Value.Status;
			}
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x00018486 File Offset: 0x00016686
		public void Dispose()
		{
		}

		// Token: 0x04000753 RID: 1875
		private int m_ApiVersion;

		// Token: 0x04000754 RID: 1876
		private Status m_Status;
	}
}
