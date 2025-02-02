using System;
using System.Runtime.InteropServices;
using Epic.OnlineServices.AntiCheatCommon;

namespace Epic.OnlineServices.AntiCheatServer
{
	// Token: 0x020005CF RID: 1487
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RegisterClientOptionsInternal : ISettable<RegisterClientOptions>, IDisposable
	{
		// Token: 0x17000B36 RID: 2870
		// (set) Token: 0x06002631 RID: 9777 RVA: 0x00038BE4 File Offset: 0x00036DE4
		public IntPtr ClientHandle
		{
			set
			{
				this.m_ClientHandle = value;
			}
		}

		// Token: 0x17000B37 RID: 2871
		// (set) Token: 0x06002632 RID: 9778 RVA: 0x00038BEE File Offset: 0x00036DEE
		public AntiCheatCommonClientType ClientType
		{
			set
			{
				this.m_ClientType = value;
			}
		}

		// Token: 0x17000B38 RID: 2872
		// (set) Token: 0x06002633 RID: 9779 RVA: 0x00038BF8 File Offset: 0x00036DF8
		public AntiCheatCommonClientPlatform ClientPlatform
		{
			set
			{
				this.m_ClientPlatform = value;
			}
		}

		// Token: 0x17000B39 RID: 2873
		// (set) Token: 0x06002634 RID: 9780 RVA: 0x00038C02 File Offset: 0x00036E02
		public Utf8String AccountId_DEPRECATED
		{
			set
			{
				Helper.Set(value, ref this.m_AccountId_DEPRECATED);
			}
		}

		// Token: 0x17000B3A RID: 2874
		// (set) Token: 0x06002635 RID: 9781 RVA: 0x00038C12 File Offset: 0x00036E12
		public Utf8String IpAddress
		{
			set
			{
				Helper.Set(value, ref this.m_IpAddress);
			}
		}

		// Token: 0x17000B3B RID: 2875
		// (set) Token: 0x06002636 RID: 9782 RVA: 0x00038C22 File Offset: 0x00036E22
		public ProductUserId UserId
		{
			set
			{
				Helper.Set(value, ref this.m_UserId);
			}
		}

		// Token: 0x06002637 RID: 9783 RVA: 0x00038C34 File Offset: 0x00036E34
		public void Set(ref RegisterClientOptions other)
		{
			this.m_ApiVersion = 2;
			this.ClientHandle = other.ClientHandle;
			this.ClientType = other.ClientType;
			this.ClientPlatform = other.ClientPlatform;
			this.AccountId_DEPRECATED = other.AccountId_DEPRECATED;
			this.IpAddress = other.IpAddress;
			this.UserId = other.UserId;
		}

		// Token: 0x06002638 RID: 9784 RVA: 0x00038C98 File Offset: 0x00036E98
		public void Set(ref RegisterClientOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 2;
				this.ClientHandle = other.Value.ClientHandle;
				this.ClientType = other.Value.ClientType;
				this.ClientPlatform = other.Value.ClientPlatform;
				this.AccountId_DEPRECATED = other.Value.AccountId_DEPRECATED;
				this.IpAddress = other.Value.IpAddress;
				this.UserId = other.Value.UserId;
			}
		}

		// Token: 0x06002639 RID: 9785 RVA: 0x00038D3A File Offset: 0x00036F3A
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientHandle);
			Helper.Dispose(ref this.m_AccountId_DEPRECATED);
			Helper.Dispose(ref this.m_IpAddress);
			Helper.Dispose(ref this.m_UserId);
		}

		// Token: 0x040010B8 RID: 4280
		private int m_ApiVersion;

		// Token: 0x040010B9 RID: 4281
		private IntPtr m_ClientHandle;

		// Token: 0x040010BA RID: 4282
		private AntiCheatCommonClientType m_ClientType;

		// Token: 0x040010BB RID: 4283
		private AntiCheatCommonClientPlatform m_ClientPlatform;

		// Token: 0x040010BC RID: 4284
		private IntPtr m_AccountId_DEPRECATED;

		// Token: 0x040010BD RID: 4285
		private IntPtr m_IpAddress;

		// Token: 0x040010BE RID: 4286
		private IntPtr m_UserId;
	}
}
