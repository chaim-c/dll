using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x020005E7 RID: 1511
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LogEventParamPairInternal : IGettable<LogEventParamPair>, ISettable<LogEventParamPair>, IDisposable
	{
		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x06002665 RID: 9829 RVA: 0x000390E4 File Offset: 0x000372E4
		// (set) Token: 0x06002666 RID: 9830 RVA: 0x00039105 File Offset: 0x00037305
		public LogEventParamPairParamValue ParamValue
		{
			get
			{
				LogEventParamPairParamValue result;
				Helper.Get<LogEventParamPairParamValueInternal, LogEventParamPairParamValue>(ref this.m_ParamValue, out result);
				return result;
			}
			set
			{
				Helper.Set<LogEventParamPairParamValue, LogEventParamPairParamValueInternal>(ref value, ref this.m_ParamValue);
			}
		}

		// Token: 0x06002667 RID: 9831 RVA: 0x00039116 File Offset: 0x00037316
		public void Set(ref LogEventParamPair other)
		{
			this.ParamValue = other.ParamValue;
		}

		// Token: 0x06002668 RID: 9832 RVA: 0x00039128 File Offset: 0x00037328
		public void Set(ref LogEventParamPair? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ParamValue = other.Value.ParamValue;
			}
		}

		// Token: 0x06002669 RID: 9833 RVA: 0x00039157 File Offset: 0x00037357
		public void Dispose()
		{
			Helper.Dispose<LogEventParamPairParamValueInternal>(ref this.m_ParamValue);
		}

		// Token: 0x0600266A RID: 9834 RVA: 0x00039166 File Offset: 0x00037366
		public void Get(out LogEventParamPair output)
		{
			output = default(LogEventParamPair);
			output.Set(ref this);
		}

		// Token: 0x04001137 RID: 4407
		private LogEventParamPairParamValueInternal m_ParamValue;
	}
}
