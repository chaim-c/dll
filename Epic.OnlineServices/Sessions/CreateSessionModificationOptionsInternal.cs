using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000D8 RID: 216
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CreateSessionModificationOptionsInternal : ISettable<CreateSessionModificationOptions>, IDisposable
	{
		// Token: 0x17000170 RID: 368
		// (set) Token: 0x06000748 RID: 1864 RVA: 0x0000AFD0 File Offset: 0x000091D0
		public Utf8String SessionName
		{
			set
			{
				Helper.Set(value, ref this.m_SessionName);
			}
		}

		// Token: 0x17000171 RID: 369
		// (set) Token: 0x06000749 RID: 1865 RVA: 0x0000AFE0 File Offset: 0x000091E0
		public Utf8String BucketId
		{
			set
			{
				Helper.Set(value, ref this.m_BucketId);
			}
		}

		// Token: 0x17000172 RID: 370
		// (set) Token: 0x0600074A RID: 1866 RVA: 0x0000AFF0 File Offset: 0x000091F0
		public uint MaxPlayers
		{
			set
			{
				this.m_MaxPlayers = value;
			}
		}

		// Token: 0x17000173 RID: 371
		// (set) Token: 0x0600074B RID: 1867 RVA: 0x0000AFFA File Offset: 0x000091FA
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000174 RID: 372
		// (set) Token: 0x0600074C RID: 1868 RVA: 0x0000B00A File Offset: 0x0000920A
		public bool PresenceEnabled
		{
			set
			{
				Helper.Set(value, ref this.m_PresenceEnabled);
			}
		}

		// Token: 0x17000175 RID: 373
		// (set) Token: 0x0600074D RID: 1869 RVA: 0x0000B01A File Offset: 0x0000921A
		public Utf8String SessionId
		{
			set
			{
				Helper.Set(value, ref this.m_SessionId);
			}
		}

		// Token: 0x17000176 RID: 374
		// (set) Token: 0x0600074E RID: 1870 RVA: 0x0000B02A File Offset: 0x0000922A
		public bool SanctionsEnabled
		{
			set
			{
				Helper.Set(value, ref this.m_SanctionsEnabled);
			}
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0000B03C File Offset: 0x0000923C
		public void Set(ref CreateSessionModificationOptions other)
		{
			this.m_ApiVersion = 4;
			this.SessionName = other.SessionName;
			this.BucketId = other.BucketId;
			this.MaxPlayers = other.MaxPlayers;
			this.LocalUserId = other.LocalUserId;
			this.PresenceEnabled = other.PresenceEnabled;
			this.SessionId = other.SessionId;
			this.SanctionsEnabled = other.SanctionsEnabled;
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x0000B0AC File Offset: 0x000092AC
		public void Set(ref CreateSessionModificationOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 4;
				this.SessionName = other.Value.SessionName;
				this.BucketId = other.Value.BucketId;
				this.MaxPlayers = other.Value.MaxPlayers;
				this.LocalUserId = other.Value.LocalUserId;
				this.PresenceEnabled = other.Value.PresenceEnabled;
				this.SessionId = other.Value.SessionId;
				this.SanctionsEnabled = other.Value.SanctionsEnabled;
			}
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x0000B163 File Offset: 0x00009363
		public void Dispose()
		{
			Helper.Dispose(ref this.m_SessionName);
			Helper.Dispose(ref this.m_BucketId);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_SessionId);
		}

		// Token: 0x0400037A RID: 890
		private int m_ApiVersion;

		// Token: 0x0400037B RID: 891
		private IntPtr m_SessionName;

		// Token: 0x0400037C RID: 892
		private IntPtr m_BucketId;

		// Token: 0x0400037D RID: 893
		private uint m_MaxPlayers;

		// Token: 0x0400037E RID: 894
		private IntPtr m_LocalUserId;

		// Token: 0x0400037F RID: 895
		private int m_PresenceEnabled;

		// Token: 0x04000380 RID: 896
		private IntPtr m_SessionId;

		// Token: 0x04000381 RID: 897
		private int m_SanctionsEnabled;
	}
}
