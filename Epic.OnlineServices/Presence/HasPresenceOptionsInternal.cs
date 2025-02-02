using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000239 RID: 569
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HasPresenceOptionsInternal : ISettable<HasPresenceOptions>, IDisposable
	{
		// Token: 0x17000416 RID: 1046
		// (set) Token: 0x06000FA4 RID: 4004 RVA: 0x000171D8 File Offset: 0x000153D8
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000417 RID: 1047
		// (set) Token: 0x06000FA5 RID: 4005 RVA: 0x000171E8 File Offset: 0x000153E8
		public EpicAccountId TargetUserId
		{
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x000171F8 File Offset: 0x000153F8
		public void Set(ref HasPresenceOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x0001721C File Offset: 0x0001541C
		public void Set(ref HasPresenceOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.TargetUserId = other.Value.TargetUserId;
			}
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x00017267 File Offset: 0x00015467
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x04000700 RID: 1792
		private int m_ApiVersion;

		// Token: 0x04000701 RID: 1793
		private IntPtr m_LocalUserId;

		// Token: 0x04000702 RID: 1794
		private IntPtr m_TargetUserId;
	}
}
