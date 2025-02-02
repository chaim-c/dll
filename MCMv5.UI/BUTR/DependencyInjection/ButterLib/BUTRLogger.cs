using System;
using System.Runtime.CompilerServices;
using BUTR.DependencyInjection.Logger;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BUTR.DependencyInjection.ButterLib
{
	// Token: 0x0200005B RID: 91
	[NullableContext(1)]
	[Nullable(0)]
	internal class BUTRLogger<[Nullable(2)] T> : IBUTRLogger<T>, IBUTRLogger
	{
		// Token: 0x060003BC RID: 956 RVA: 0x0000F9B4 File Offset: 0x0000DBB4
		public BUTRLogger(IServiceProvider serviceProvider)
		{
			this._logger = ServiceProviderServiceExtensions.GetRequiredService<ILogger<T>>(serviceProvider);
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000F9CC File Offset: 0x0000DBCC
		public void LogMessage(LogLevel logLevel, string message, params object[] args)
		{
			switch (logLevel)
			{
			case LogLevel.Trace:
				LoggerExtensions.LogTrace(this._logger, message, args);
				break;
			case LogLevel.Debug:
				LoggerExtensions.LogDebug(this._logger, message, args);
				break;
			case LogLevel.Information:
				LoggerExtensions.LogInformation(this._logger, message, args);
				break;
			case LogLevel.Warning:
				LoggerExtensions.LogWarning(this._logger, message, args);
				break;
			case LogLevel.Error:
				LoggerExtensions.LogError(this._logger, message, args);
				break;
			case LogLevel.Critical:
				LoggerExtensions.LogCritical(this._logger, message, args);
				break;
			}
		}

		// Token: 0x040000FF RID: 255
		private readonly ILogger _logger;
	}
}
