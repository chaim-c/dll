using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000F0 RID: 240
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct JoinSessionOptionsInternal : ISettable<JoinSessionOptions>, IDisposable
	{
		// Token: 0x170001A3 RID: 419
		// (set) Token: 0x060007CA RID: 1994 RVA: 0x0000BB99 File Offset: 0x00009D99
		public Utf8String SessionName
		{
			set
			{
				Helper.Set(value, ref this.m_SessionName);
			}
		}

		// Token: 0x170001A4 RID: 420
		// (set) Token: 0x060007CB RID: 1995 RVA: 0x0000BBA9 File Offset: 0x00009DA9
		public SessionDetails SessionHandle
		{
			set
			{
				Helper.Set(value, ref this.m_SessionHandle);
			}
		}

		// Token: 0x170001A5 RID: 421
		// (set) Token: 0x060007CC RID: 1996 RVA: 0x0000BBB9 File Offset: 0x00009DB9
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x170001A6 RID: 422
		// (set) Token: 0x060007CD RID: 1997 RVA: 0x0000BBC9 File Offset: 0x00009DC9
		public bool PresenceEnabled
		{
			set
			{
				Helper.Set(value, ref this.m_PresenceEnabled);
			}
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0000BBD9 File Offset: 0x00009DD9
		public void Set(ref JoinSessionOptions other)
		{
			this.m_ApiVersion = 2;
			this.SessionName = other.SessionName;
			this.SessionHandle = other.SessionHandle;
			this.LocalUserId = other.LocalUserId;
			this.PresenceEnabled = other.PresenceEnabled;
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x0000BC18 File Offset: 0x00009E18
		public void Set(ref JoinSessionOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 2;
				this.SessionName = other.Value.SessionName;
				this.SessionHandle = other.Value.SessionHandle;
				this.LocalUserId = other.Value.LocalUserId;
				this.PresenceEnabled = other.Value.PresenceEnabled;
			}
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x0000BC8D File Offset: 0x00009E8D
		public void Dispose()
		{
			Helper.Dispose(ref this.m_SessionName);
			Helper.Dispose(ref this.m_SessionHandle);
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x040003B1 RID: 945
		private int m_ApiVersion;

		// Token: 0x040003B2 RID: 946
		private IntPtr m_SessionName;

		// Token: 0x040003B3 RID: 947
		private IntPtr m_SessionHandle;

		// Token: 0x040003B4 RID: 948
		private IntPtr m_LocalUserId;

		// Token: 0x040003B5 RID: 949
		private int m_PresenceEnabled;
	}
}
