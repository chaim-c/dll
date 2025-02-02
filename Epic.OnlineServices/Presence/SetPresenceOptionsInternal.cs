using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x0200025D RID: 605
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SetPresenceOptionsInternal : ISettable<SetPresenceOptions>, IDisposable
	{
		// Token: 0x1700045E RID: 1118
		// (set) Token: 0x06001097 RID: 4247 RVA: 0x00018998 File Offset: 0x00016B98
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x1700045F RID: 1119
		// (set) Token: 0x06001098 RID: 4248 RVA: 0x000189A8 File Offset: 0x00016BA8
		public PresenceModification PresenceModificationHandle
		{
			set
			{
				Helper.Set(value, ref this.m_PresenceModificationHandle);
			}
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x000189B8 File Offset: 0x00016BB8
		public void Set(ref SetPresenceOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.PresenceModificationHandle = other.PresenceModificationHandle;
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x000189DC File Offset: 0x00016BDC
		public void Set(ref SetPresenceOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.PresenceModificationHandle = other.Value.PresenceModificationHandle;
			}
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x00018A27 File Offset: 0x00016C27
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_PresenceModificationHandle);
		}

		// Token: 0x0400076A RID: 1898
		private int m_ApiVersion;

		// Token: 0x0400076B RID: 1899
		private IntPtr m_LocalUserId;

		// Token: 0x0400076C RID: 1900
		private IntPtr m_PresenceModificationHandle;
	}
}
