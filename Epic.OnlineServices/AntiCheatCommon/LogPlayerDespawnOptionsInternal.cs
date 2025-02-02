using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x020005EF RID: 1519
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LogPlayerDespawnOptionsInternal : ISettable<LogPlayerDespawnOptions>, IDisposable
	{
		// Token: 0x17000B6C RID: 2924
		// (set) Token: 0x060026B2 RID: 9906 RVA: 0x00039B5D File Offset: 0x00037D5D
		public IntPtr DespawnedPlayerHandle
		{
			set
			{
				this.m_DespawnedPlayerHandle = value;
			}
		}

		// Token: 0x060026B3 RID: 9907 RVA: 0x00039B67 File Offset: 0x00037D67
		public void Set(ref LogPlayerDespawnOptions other)
		{
			this.m_ApiVersion = 1;
			this.DespawnedPlayerHandle = other.DespawnedPlayerHandle;
		}

		// Token: 0x060026B4 RID: 9908 RVA: 0x00039B80 File Offset: 0x00037D80
		public void Set(ref LogPlayerDespawnOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.DespawnedPlayerHandle = other.Value.DespawnedPlayerHandle;
			}
		}

		// Token: 0x060026B5 RID: 9909 RVA: 0x00039BB6 File Offset: 0x00037DB6
		public void Dispose()
		{
			Helper.Dispose(ref this.m_DespawnedPlayerHandle);
		}

		// Token: 0x04001157 RID: 4439
		private int m_ApiVersion;

		// Token: 0x04001158 RID: 4440
		private IntPtr m_DespawnedPlayerHandle;
	}
}
