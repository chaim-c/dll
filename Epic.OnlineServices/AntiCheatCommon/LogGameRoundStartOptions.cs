using System;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x020005EC RID: 1516
	public struct LogGameRoundStartOptions
	{
		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x060026A1 RID: 9889 RVA: 0x000399F1 File Offset: 0x00037BF1
		// (set) Token: 0x060026A2 RID: 9890 RVA: 0x000399F9 File Offset: 0x00037BF9
		public Utf8String SessionIdentifier { get; set; }

		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x060026A3 RID: 9891 RVA: 0x00039A02 File Offset: 0x00037C02
		// (set) Token: 0x060026A4 RID: 9892 RVA: 0x00039A0A File Offset: 0x00037C0A
		public Utf8String LevelName { get; set; }

		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x060026A5 RID: 9893 RVA: 0x00039A13 File Offset: 0x00037C13
		// (set) Token: 0x060026A6 RID: 9894 RVA: 0x00039A1B File Offset: 0x00037C1B
		public Utf8String ModeName { get; set; }

		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x060026A7 RID: 9895 RVA: 0x00039A24 File Offset: 0x00037C24
		// (set) Token: 0x060026A8 RID: 9896 RVA: 0x00039A2C File Offset: 0x00037C2C
		public uint RoundTimeSeconds { get; set; }
	}
}
