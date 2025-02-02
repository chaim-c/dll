using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x020005E5 RID: 1509
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LogEventOptionsInternal : ISettable<LogEventOptions>, IDisposable
	{
		// Token: 0x17000B4B RID: 2891
		// (set) Token: 0x0600265C RID: 9820 RVA: 0x00038FE9 File Offset: 0x000371E9
		public IntPtr ClientHandle
		{
			set
			{
				this.m_ClientHandle = value;
			}
		}

		// Token: 0x17000B4C RID: 2892
		// (set) Token: 0x0600265D RID: 9821 RVA: 0x00038FF3 File Offset: 0x000371F3
		public uint EventId
		{
			set
			{
				this.m_EventId = value;
			}
		}

		// Token: 0x17000B4D RID: 2893
		// (set) Token: 0x0600265E RID: 9822 RVA: 0x00038FFD File Offset: 0x000371FD
		public LogEventParamPair[] Params
		{
			set
			{
				Helper.Set<LogEventParamPair, LogEventParamPairInternal>(ref value, ref this.m_Params, out this.m_ParamsCount);
			}
		}

		// Token: 0x0600265F RID: 9823 RVA: 0x00039014 File Offset: 0x00037214
		public void Set(ref LogEventOptions other)
		{
			this.m_ApiVersion = 1;
			this.ClientHandle = other.ClientHandle;
			this.EventId = other.EventId;
			this.Params = other.Params;
		}

		// Token: 0x06002660 RID: 9824 RVA: 0x00039048 File Offset: 0x00037248
		public void Set(ref LogEventOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.ClientHandle = other.Value.ClientHandle;
				this.EventId = other.Value.EventId;
				this.Params = other.Value.Params;
			}
		}

		// Token: 0x06002661 RID: 9825 RVA: 0x000390A8 File Offset: 0x000372A8
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientHandle);
			Helper.Dispose(ref this.m_Params);
		}

		// Token: 0x04001131 RID: 4401
		private int m_ApiVersion;

		// Token: 0x04001132 RID: 4402
		private IntPtr m_ClientHandle;

		// Token: 0x04001133 RID: 4403
		private uint m_EventId;

		// Token: 0x04001134 RID: 4404
		private uint m_ParamsCount;

		// Token: 0x04001135 RID: 4405
		private IntPtr m_Params;
	}
}
