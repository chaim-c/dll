using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x0200024F RID: 591
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PresenceModificationSetJoinInfoOptionsInternal : ISettable<PresenceModificationSetJoinInfoOptions>, IDisposable
	{
		// Token: 0x17000443 RID: 1091
		// (set) Token: 0x06001048 RID: 4168 RVA: 0x0001832E File Offset: 0x0001652E
		public Utf8String JoinInfo
		{
			set
			{
				Helper.Set(value, ref this.m_JoinInfo);
			}
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x0001833E File Offset: 0x0001653E
		public void Set(ref PresenceModificationSetJoinInfoOptions other)
		{
			this.m_ApiVersion = 1;
			this.JoinInfo = other.JoinInfo;
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x00018358 File Offset: 0x00016558
		public void Set(ref PresenceModificationSetJoinInfoOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.JoinInfo = other.Value.JoinInfo;
			}
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x0001838E File Offset: 0x0001658E
		public void Dispose()
		{
			Helper.Dispose(ref this.m_JoinInfo);
		}

		// Token: 0x0400074D RID: 1869
		private int m_ApiVersion;

		// Token: 0x0400074E RID: 1870
		private IntPtr m_JoinInfo;
	}
}
