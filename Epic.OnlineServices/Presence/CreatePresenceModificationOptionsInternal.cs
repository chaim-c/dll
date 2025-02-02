using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000233 RID: 563
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CreatePresenceModificationOptionsInternal : ISettable<CreatePresenceModificationOptions>, IDisposable
	{
		// Token: 0x1700040B RID: 1035
		// (set) Token: 0x06000F86 RID: 3974 RVA: 0x00016F37 File Offset: 0x00015137
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x00016F47 File Offset: 0x00015147
		public void Set(ref CreatePresenceModificationOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x00016F60 File Offset: 0x00015160
		public void Set(ref CreatePresenceModificationOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x00016F96 File Offset: 0x00015196
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x040006F2 RID: 1778
		private int m_ApiVersion;

		// Token: 0x040006F3 RID: 1779
		private IntPtr m_LocalUserId;
	}
}
