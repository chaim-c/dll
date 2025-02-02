using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x020005ED RID: 1517
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LogGameRoundStartOptionsInternal : ISettable<LogGameRoundStartOptions>, IDisposable
	{
		// Token: 0x17000B67 RID: 2919
		// (set) Token: 0x060026A9 RID: 9897 RVA: 0x00039A35 File Offset: 0x00037C35
		public Utf8String SessionIdentifier
		{
			set
			{
				Helper.Set(value, ref this.m_SessionIdentifier);
			}
		}

		// Token: 0x17000B68 RID: 2920
		// (set) Token: 0x060026AA RID: 9898 RVA: 0x00039A45 File Offset: 0x00037C45
		public Utf8String LevelName
		{
			set
			{
				Helper.Set(value, ref this.m_LevelName);
			}
		}

		// Token: 0x17000B69 RID: 2921
		// (set) Token: 0x060026AB RID: 9899 RVA: 0x00039A55 File Offset: 0x00037C55
		public Utf8String ModeName
		{
			set
			{
				Helper.Set(value, ref this.m_ModeName);
			}
		}

		// Token: 0x17000B6A RID: 2922
		// (set) Token: 0x060026AC RID: 9900 RVA: 0x00039A65 File Offset: 0x00037C65
		public uint RoundTimeSeconds
		{
			set
			{
				this.m_RoundTimeSeconds = value;
			}
		}

		// Token: 0x060026AD RID: 9901 RVA: 0x00039A6F File Offset: 0x00037C6F
		public void Set(ref LogGameRoundStartOptions other)
		{
			this.m_ApiVersion = 1;
			this.SessionIdentifier = other.SessionIdentifier;
			this.LevelName = other.LevelName;
			this.ModeName = other.ModeName;
			this.RoundTimeSeconds = other.RoundTimeSeconds;
		}

		// Token: 0x060026AE RID: 9902 RVA: 0x00039AB0 File Offset: 0x00037CB0
		public void Set(ref LogGameRoundStartOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.SessionIdentifier = other.Value.SessionIdentifier;
				this.LevelName = other.Value.LevelName;
				this.ModeName = other.Value.ModeName;
				this.RoundTimeSeconds = other.Value.RoundTimeSeconds;
			}
		}

		// Token: 0x060026AF RID: 9903 RVA: 0x00039B25 File Offset: 0x00037D25
		public void Dispose()
		{
			Helper.Dispose(ref this.m_SessionIdentifier);
			Helper.Dispose(ref this.m_LevelName);
			Helper.Dispose(ref this.m_ModeName);
		}

		// Token: 0x04001151 RID: 4433
		private int m_ApiVersion;

		// Token: 0x04001152 RID: 4434
		private IntPtr m_SessionIdentifier;

		// Token: 0x04001153 RID: 4435
		private IntPtr m_LevelName;

		// Token: 0x04001154 RID: 4436
		private IntPtr m_ModeName;

		// Token: 0x04001155 RID: 4437
		private uint m_RoundTimeSeconds;
	}
}
