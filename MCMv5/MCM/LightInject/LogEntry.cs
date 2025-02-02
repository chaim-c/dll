using System;
using System.Diagnostics.CodeAnalysis;

namespace MCM.LightInject
{
	// Token: 0x020000EC RID: 236
	[ExcludeFromCodeCoverage]
	internal class LogEntry
	{
		// Token: 0x06000514 RID: 1300 RVA: 0x0000DD74 File Offset: 0x0000BF74
		public LogEntry(LogLevel level, string message)
		{
			this.Level = level;
			this.Message = message;
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x0000DD8E File Offset: 0x0000BF8E
		// (set) Token: 0x06000516 RID: 1302 RVA: 0x0000DD96 File Offset: 0x0000BF96
		public LogLevel Level { get; private set; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x0000DD9F File Offset: 0x0000BF9F
		// (set) Token: 0x06000518 RID: 1304 RVA: 0x0000DDA7 File Offset: 0x0000BFA7
		public string Message { get; private set; }
	}
}
