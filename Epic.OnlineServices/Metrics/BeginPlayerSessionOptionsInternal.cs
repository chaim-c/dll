using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Metrics
{
	// Token: 0x0200030D RID: 781
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct BeginPlayerSessionOptionsInternal : ISettable<BeginPlayerSessionOptions>, IDisposable
	{
		// Token: 0x170005D5 RID: 1493
		// (set) Token: 0x0600150C RID: 5388 RVA: 0x0001F20F File Offset: 0x0001D40F
		public BeginPlayerSessionOptionsAccountId AccountId
		{
			set
			{
				Helper.Set<BeginPlayerSessionOptionsAccountId, BeginPlayerSessionOptionsAccountIdInternal>(ref value, ref this.m_AccountId);
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (set) Token: 0x0600150D RID: 5389 RVA: 0x0001F220 File Offset: 0x0001D420
		public Utf8String DisplayName
		{
			set
			{
				Helper.Set(value, ref this.m_DisplayName);
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (set) Token: 0x0600150E RID: 5390 RVA: 0x0001F230 File Offset: 0x0001D430
		public UserControllerType ControllerType
		{
			set
			{
				this.m_ControllerType = value;
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (set) Token: 0x0600150F RID: 5391 RVA: 0x0001F23A File Offset: 0x0001D43A
		public Utf8String ServerIp
		{
			set
			{
				Helper.Set(value, ref this.m_ServerIp);
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (set) Token: 0x06001510 RID: 5392 RVA: 0x0001F24A File Offset: 0x0001D44A
		public Utf8String GameSessionId
		{
			set
			{
				Helper.Set(value, ref this.m_GameSessionId);
			}
		}

		// Token: 0x06001511 RID: 5393 RVA: 0x0001F25C File Offset: 0x0001D45C
		public void Set(ref BeginPlayerSessionOptions other)
		{
			this.m_ApiVersion = 1;
			this.AccountId = other.AccountId;
			this.DisplayName = other.DisplayName;
			this.ControllerType = other.ControllerType;
			this.ServerIp = other.ServerIp;
			this.GameSessionId = other.GameSessionId;
		}

		// Token: 0x06001512 RID: 5394 RVA: 0x0001F2B4 File Offset: 0x0001D4B4
		public void Set(ref BeginPlayerSessionOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.AccountId = other.Value.AccountId;
				this.DisplayName = other.Value.DisplayName;
				this.ControllerType = other.Value.ControllerType;
				this.ServerIp = other.Value.ServerIp;
				this.GameSessionId = other.Value.GameSessionId;
			}
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x0001F33E File Offset: 0x0001D53E
		public void Dispose()
		{
			Helper.Dispose<BeginPlayerSessionOptionsAccountIdInternal>(ref this.m_AccountId);
			Helper.Dispose(ref this.m_DisplayName);
			Helper.Dispose(ref this.m_ServerIp);
			Helper.Dispose(ref this.m_GameSessionId);
		}

		// Token: 0x04000965 RID: 2405
		private int m_ApiVersion;

		// Token: 0x04000966 RID: 2406
		private BeginPlayerSessionOptionsAccountIdInternal m_AccountId;

		// Token: 0x04000967 RID: 2407
		private IntPtr m_DisplayName;

		// Token: 0x04000968 RID: 2408
		private UserControllerType m_ControllerType;

		// Token: 0x04000969 RID: 2409
		private IntPtr m_ServerIp;

		// Token: 0x0400096A RID: 2410
		private IntPtr m_GameSessionId;
	}
}
