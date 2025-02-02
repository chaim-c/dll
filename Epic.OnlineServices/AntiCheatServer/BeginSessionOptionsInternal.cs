using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatServer
{
	// Token: 0x020005BF RID: 1471
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct BeginSessionOptionsInternal : ISettable<BeginSessionOptions>, IDisposable
	{
		// Token: 0x17000B20 RID: 2848
		// (set) Token: 0x060025E8 RID: 9704 RVA: 0x000387FF File Offset: 0x000369FF
		public uint RegisterTimeoutSeconds
		{
			set
			{
				this.m_RegisterTimeoutSeconds = value;
			}
		}

		// Token: 0x17000B21 RID: 2849
		// (set) Token: 0x060025E9 RID: 9705 RVA: 0x00038809 File Offset: 0x00036A09
		public Utf8String ServerName
		{
			set
			{
				Helper.Set(value, ref this.m_ServerName);
			}
		}

		// Token: 0x17000B22 RID: 2850
		// (set) Token: 0x060025EA RID: 9706 RVA: 0x00038819 File Offset: 0x00036A19
		public bool EnableGameplayData
		{
			set
			{
				Helper.Set(value, ref this.m_EnableGameplayData);
			}
		}

		// Token: 0x17000B23 RID: 2851
		// (set) Token: 0x060025EB RID: 9707 RVA: 0x00038829 File Offset: 0x00036A29
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x060025EC RID: 9708 RVA: 0x00038839 File Offset: 0x00036A39
		public void Set(ref BeginSessionOptions other)
		{
			this.m_ApiVersion = 3;
			this.RegisterTimeoutSeconds = other.RegisterTimeoutSeconds;
			this.ServerName = other.ServerName;
			this.EnableGameplayData = other.EnableGameplayData;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x060025ED RID: 9709 RVA: 0x00038878 File Offset: 0x00036A78
		public void Set(ref BeginSessionOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 3;
				this.RegisterTimeoutSeconds = other.Value.RegisterTimeoutSeconds;
				this.ServerName = other.Value.ServerName;
				this.EnableGameplayData = other.Value.EnableGameplayData;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x060025EE RID: 9710 RVA: 0x000388ED File Offset: 0x00036AED
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ServerName);
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x0400109B RID: 4251
		private int m_ApiVersion;

		// Token: 0x0400109C RID: 4252
		private uint m_RegisterTimeoutSeconds;

		// Token: 0x0400109D RID: 4253
		private IntPtr m_ServerName;

		// Token: 0x0400109E RID: 4254
		private int m_EnableGameplayData;

		// Token: 0x0400109F RID: 4255
		private IntPtr m_LocalUserId;
	}
}
