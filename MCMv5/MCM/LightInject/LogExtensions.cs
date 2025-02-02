using System;
using System.Diagnostics.CodeAnalysis;

namespace MCM.LightInject
{
	// Token: 0x020000E6 RID: 230
	[ExcludeFromCodeCoverage]
	internal static class LogExtensions
	{
		// Token: 0x060004E4 RID: 1252 RVA: 0x0000D05D File Offset: 0x0000B25D
		public static void Info(this Action<LogEntry> logAction, string message)
		{
			logAction(new LogEntry(LogLevel.Info, message));
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0000D06D File Offset: 0x0000B26D
		public static void Warning(this Action<LogEntry> logAction, string message)
		{
			logAction(new LogEntry(LogLevel.Warning, message));
		}
	}
}
