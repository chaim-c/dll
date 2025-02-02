using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x02000652 RID: 1618
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct InitializeThreadAffinityInternal : IGettable<InitializeThreadAffinity>, ISettable<InitializeThreadAffinity>, IDisposable
	{
		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x06002953 RID: 10579 RVA: 0x0003DB08 File Offset: 0x0003BD08
		// (set) Token: 0x06002954 RID: 10580 RVA: 0x0003DB20 File Offset: 0x0003BD20
		public ulong NetworkWork
		{
			get
			{
				return this.m_NetworkWork;
			}
			set
			{
				this.m_NetworkWork = value;
			}
		}

		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x06002955 RID: 10581 RVA: 0x0003DB2C File Offset: 0x0003BD2C
		// (set) Token: 0x06002956 RID: 10582 RVA: 0x0003DB44 File Offset: 0x0003BD44
		public ulong StorageIo
		{
			get
			{
				return this.m_StorageIo;
			}
			set
			{
				this.m_StorageIo = value;
			}
		}

		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x06002957 RID: 10583 RVA: 0x0003DB50 File Offset: 0x0003BD50
		// (set) Token: 0x06002958 RID: 10584 RVA: 0x0003DB68 File Offset: 0x0003BD68
		public ulong WebSocketIo
		{
			get
			{
				return this.m_WebSocketIo;
			}
			set
			{
				this.m_WebSocketIo = value;
			}
		}

		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x06002959 RID: 10585 RVA: 0x0003DB74 File Offset: 0x0003BD74
		// (set) Token: 0x0600295A RID: 10586 RVA: 0x0003DB8C File Offset: 0x0003BD8C
		public ulong P2PIo
		{
			get
			{
				return this.m_P2PIo;
			}
			set
			{
				this.m_P2PIo = value;
			}
		}

		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x0600295B RID: 10587 RVA: 0x0003DB98 File Offset: 0x0003BD98
		// (set) Token: 0x0600295C RID: 10588 RVA: 0x0003DBB0 File Offset: 0x0003BDB0
		public ulong HttpRequestIo
		{
			get
			{
				return this.m_HttpRequestIo;
			}
			set
			{
				this.m_HttpRequestIo = value;
			}
		}

		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x0600295D RID: 10589 RVA: 0x0003DBBC File Offset: 0x0003BDBC
		// (set) Token: 0x0600295E RID: 10590 RVA: 0x0003DBD4 File Offset: 0x0003BDD4
		public ulong RTCIo
		{
			get
			{
				return this.m_RTCIo;
			}
			set
			{
				this.m_RTCIo = value;
			}
		}

		// Token: 0x0600295F RID: 10591 RVA: 0x0003DBE0 File Offset: 0x0003BDE0
		public void Set(ref InitializeThreadAffinity other)
		{
			this.m_ApiVersion = 2;
			this.NetworkWork = other.NetworkWork;
			this.StorageIo = other.StorageIo;
			this.WebSocketIo = other.WebSocketIo;
			this.P2PIo = other.P2PIo;
			this.HttpRequestIo = other.HttpRequestIo;
			this.RTCIo = other.RTCIo;
		}

		// Token: 0x06002960 RID: 10592 RVA: 0x0003DC44 File Offset: 0x0003BE44
		public void Set(ref InitializeThreadAffinity? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 2;
				this.NetworkWork = other.Value.NetworkWork;
				this.StorageIo = other.Value.StorageIo;
				this.WebSocketIo = other.Value.WebSocketIo;
				this.P2PIo = other.Value.P2PIo;
				this.HttpRequestIo = other.Value.HttpRequestIo;
				this.RTCIo = other.Value.RTCIo;
			}
		}

		// Token: 0x06002961 RID: 10593 RVA: 0x0003DCE6 File Offset: 0x0003BEE6
		public void Dispose()
		{
		}

		// Token: 0x06002962 RID: 10594 RVA: 0x0003DCE9 File Offset: 0x0003BEE9
		public void Get(out InitializeThreadAffinity output)
		{
			output = default(InitializeThreadAffinity);
			output.Set(ref this);
		}

		// Token: 0x040012AC RID: 4780
		private int m_ApiVersion;

		// Token: 0x040012AD RID: 4781
		private ulong m_NetworkWork;

		// Token: 0x040012AE RID: 4782
		private ulong m_StorageIo;

		// Token: 0x040012AF RID: 4783
		private ulong m_WebSocketIo;

		// Token: 0x040012B0 RID: 4784
		private ulong m_P2PIo;

		// Token: 0x040012B1 RID: 4785
		private ulong m_HttpRequestIo;

		// Token: 0x040012B2 RID: 4786
		private ulong m_RTCIo;
	}
}
