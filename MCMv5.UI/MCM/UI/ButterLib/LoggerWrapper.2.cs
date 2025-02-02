using System;
using System.Runtime.CompilerServices;
using BUTR.DependencyInjection.Logger;
using Microsoft.Extensions.Logging;

namespace MCM.UI.ButterLib
{
	// Token: 0x02000034 RID: 52
	[NullableContext(1)]
	[Nullable(0)]
	internal class LoggerWrapper<[Nullable(2)] T> : IBUTRLogger<T>, IBUTRLogger
	{
		// Token: 0x060001C2 RID: 450 RVA: 0x00008F77 File Offset: 0x00007177
		public LoggerWrapper(ILogger<T> logger)
		{
			this._logger = logger;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00008F88 File Offset: 0x00007188
		public void LogMessage(LogLevel logLevel, string message, params object[] args)
		{
			this._logger.Log<FormattedLogValues>(logLevel, default(EventId), new FormattedLogValues(message, args), null, (FormattedLogValues state, Exception _) => state.ToString());
		}

		// Token: 0x04000082 RID: 130
		private readonly ILogger<T> _logger;
	}
}
