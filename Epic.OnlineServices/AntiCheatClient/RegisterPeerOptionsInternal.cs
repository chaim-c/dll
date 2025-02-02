using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices.AntiCheatCommon;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x0200063C RID: 1596
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RegisterPeerOptionsInternal : ISettable<RegisterPeerOptions>, IDisposable
	{
		// Token: 0x17000C1F RID: 3103
		// (set) Token: 0x06002899 RID: 10393 RVA: 0x0003C630 File Offset: 0x0003A830
		public IntPtr PeerHandle
		{
			set
			{
				this.m_PeerHandle = value;
			}
		}

		// Token: 0x17000C20 RID: 3104
		// (set) Token: 0x0600289A RID: 10394 RVA: 0x0003C63A File Offset: 0x0003A83A
		public AntiCheatCommonClientType ClientType
		{
			set
			{
				this.m_ClientType = value;
			}
		}

		// Token: 0x17000C21 RID: 3105
		// (set) Token: 0x0600289B RID: 10395 RVA: 0x0003C644 File Offset: 0x0003A844
		public AntiCheatCommonClientPlatform ClientPlatform
		{
			set
			{
				this.m_ClientPlatform = value;
			}
		}

		// Token: 0x17000C22 RID: 3106
		// (set) Token: 0x0600289C RID: 10396 RVA: 0x0003C64E File Offset: 0x0003A84E
		public uint AuthenticationTimeout
		{
			set
			{
				this.m_AuthenticationTimeout = value;
			}
		}

		// Token: 0x17000C23 RID: 3107
		// (set) Token: 0x0600289D RID: 10397 RVA: 0x0003C658 File Offset: 0x0003A858
		public Utf8String AccountId_DEPRECATED
		{
			set
			{
				Helper.Set(value, ref this.m_AccountId_DEPRECATED);
			}
		}

		// Token: 0x17000C24 RID: 3108
		// (set) Token: 0x0600289E RID: 10398 RVA: 0x0003C668 File Offset: 0x0003A868
		public Utf8String IpAddress
		{
			set
			{
				Helper.Set(value, ref this.m_IpAddress);
			}
		}

		// Token: 0x17000C25 RID: 3109
		// (set) Token: 0x0600289F RID: 10399 RVA: 0x0003C678 File Offset: 0x0003A878
		public ProductUserId PeerProductUserId
		{
			set
			{
				Helper.Set(value, ref this.m_PeerProductUserId);
			}
		}

		// Token: 0x060028A0 RID: 10400 RVA: 0x0003C688 File Offset: 0x0003A888
		public void Set(ref RegisterPeerOptions other)
		{
			this.m_ApiVersion = 3;
			this.PeerHandle = other.PeerHandle;
			this.ClientType = other.ClientType;
			this.ClientPlatform = other.ClientPlatform;
			this.AuthenticationTimeout = other.AuthenticationTimeout;
			this.AccountId_DEPRECATED = other.AccountId_DEPRECATED;
			this.IpAddress = other.IpAddress;
			this.PeerProductUserId = other.PeerProductUserId;
		}

		// Token: 0x060028A1 RID: 10401 RVA: 0x0003C6F8 File Offset: 0x0003A8F8
		public void Set(ref RegisterPeerOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 3;
				this.PeerHandle = other.Value.PeerHandle;
				this.ClientType = other.Value.ClientType;
				this.ClientPlatform = other.Value.ClientPlatform;
				this.AuthenticationTimeout = other.Value.AuthenticationTimeout;
				this.AccountId_DEPRECATED = other.Value.AccountId_DEPRECATED;
				this.IpAddress = other.Value.IpAddress;
				this.PeerProductUserId = other.Value.PeerProductUserId;
			}
		}

		// Token: 0x060028A2 RID: 10402 RVA: 0x0003C7AF File Offset: 0x0003A9AF
		public void Dispose()
		{
			Helper.Dispose(ref this.m_PeerHandle);
			Helper.Dispose(ref this.m_AccountId_DEPRECATED);
			Helper.Dispose(ref this.m_IpAddress);
			Helper.Dispose(ref this.m_PeerProductUserId);
		}

		// Token: 0x0400124A RID: 4682
		private int m_ApiVersion;

		// Token: 0x0400124B RID: 4683
		private IntPtr m_PeerHandle;

		// Token: 0x0400124C RID: 4684
		private AntiCheatCommonClientType m_ClientType;

		// Token: 0x0400124D RID: 4685
		private AntiCheatCommonClientPlatform m_ClientPlatform;

		// Token: 0x0400124E RID: 4686
		private uint m_AuthenticationTimeout;

		// Token: 0x0400124F RID: 4687
		private IntPtr m_AccountId_DEPRECATED;

		// Token: 0x04001250 RID: 4688
		private IntPtr m_IpAddress;

		// Token: 0x04001251 RID: 4689
		private IntPtr m_PeerProductUserId;
	}
}
