using System;
using System.Runtime.CompilerServices;
using BUTR.DependencyInjection.Logger;
using Microsoft.Extensions.Logging;

namespace MCM.UI.ButterLib
{
	// Token: 0x02000033 RID: 51
	[NullableContext(1)]
	[Nullable(0)]
	internal class LoggerWrapper : IBUTRLogger
	{
		// Token: 0x060001C0 RID: 448 RVA: 0x00008F18 File Offset: 0x00007118
		public LoggerWrapper(ILogger logger)
		{
			this._logger = logger;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00008F2C File Offset: 0x0000712C
		public void LogMessage(LogLevel logLevel, string message, params object[] args)
		{
			this._logger.Log<FormattedLogValues>(logLevel, default(EventId), new FormattedLogValues(message, args), null, (FormattedLogValues state, Exception _) => state.ToString());
		}

		// Token: 0x04000081 RID: 129
		private readonly ILogger _logger;
	}
}
