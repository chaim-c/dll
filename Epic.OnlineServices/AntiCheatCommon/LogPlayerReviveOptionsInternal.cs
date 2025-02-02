using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x020005F1 RID: 1521
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LogPlayerReviveOptionsInternal : ISettable<LogPlayerReviveOptions>, IDisposable
	{
		// Token: 0x17000B6F RID: 2927
		// (set) Token: 0x060026BA RID: 9914 RVA: 0x00039BE7 File Offset: 0x00037DE7
		public IntPtr RevivedPlayerHandle
		{
			set
			{
				this.m_RevivedPlayerHandle = value;
			}
		}

		// Token: 0x17000B70 RID: 2928
		// (set) Token: 0x060026BB RID: 9915 RVA: 0x00039BF1 File Offset: 0x00037DF1
		public IntPtr ReviverPlayerHandle
		{
			set
			{
				this.m_ReviverPlayerHandle = value;
			}
		}

		// Token: 0x060026BC RID: 9916 RVA: 0x00039BFB File Offset: 0x00037DFB
		public void Set(ref LogPlayerReviveOptions other)
		{
			this.m_ApiVersion = 1;
			this.RevivedPlayerHandle = other.RevivedPlayerHandle;
			this.ReviverPlayerHandle = other.ReviverPlayerHandle;
		}

		// Token: 0x060026BD RID: 9917 RVA: 0x00039C20 File Offset: 0x00037E20
		public void Set(ref LogPlayerReviveOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.RevivedPlayerHandle = other.Value.RevivedPlayerHandle;
				this.ReviverPlayerHandle = other.Value.ReviverPlayerHandle;
			}
		}

		// Token: 0x060026BE RID: 9918 RVA: 0x00039C6B File Offset: 0x00037E6B
		public void Dispose()
		{
			Helper.Dispose(ref this.m_RevivedPlayerHandle);
			Helper.Dispose(ref this.m_ReviverPlayerHandle);
		}

		// Token: 0x0400115B RID: 4443
		private int m_ApiVersion;

		// Token: 0x0400115C RID: 4444
		private IntPtr m_RevivedPlayerHandle;

		// Token: 0x0400115D RID: 4445
		private IntPtr m_ReviverPlayerHandle;
	}
}
