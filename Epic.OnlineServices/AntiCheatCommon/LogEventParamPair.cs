using System;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x020005E6 RID: 1510
	public struct LogEventParamPair
	{
		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x06002662 RID: 9826 RVA: 0x000390C3 File Offset: 0x000372C3
		// (set) Token: 0x06002663 RID: 9827 RVA: 0x000390CB File Offset: 0x000372CB
		public LogEventParamPairParamValue ParamValue { get; set; }

		// Token: 0x06002664 RID: 9828 RVA: 0x000390D4 File Offset: 0x000372D4
		internal void Set(ref LogEventParamPairInternal other)
		{
			this.ParamValue = other.ParamValue;
		}
	}
}
