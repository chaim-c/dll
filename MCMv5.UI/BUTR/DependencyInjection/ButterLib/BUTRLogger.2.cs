using System;
using System.Runtime.CompilerServices;
using BUTR.DependencyInjection.Logger;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BUTR.DependencyInjection.ButterLib
{
	// Token: 0x0200005C RID: 92
	[NullableContext(1)]
	[Nullable(0)]
	internal class BUTRLogger : IBUTRLogger
	{
		// Token: 0x060003BE RID: 958 RVA: 0x0000FA5E File Offset: 0x0000DC5E
		public BUTRLogger(IServiceProvider serviceProvider)
		{
			this._logger = ServiceProviderServiceExtensions.GetRequiredService<ILogger>(serviceProvider);
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000FA74 File Offset: 0x0000DC74
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

		// Token: 0x04000100 RID: 256
		private readonly ILogger _logger;
	}
}
