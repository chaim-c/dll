using System;
using System.Runtime.CompilerServices;

namespace BUTR.DependencyInjection.Logger
{
	// Token: 0x0200014B RID: 331
	[NullableContext(1)]
	[Nullable(0)]
	public static class LoggerExtensions
	{
		// Token: 0x060008EB RID: 2283 RVA: 0x0001DA98 File Offset: 0x0001BC98
		public static void LogDebug(this IBUTRLogger logger, Exception exception, string message, params object[] args)
		{
			bool flag = logger == null;
			if (flag)
			{
				throw new ArgumentNullException("logger");
			}
			logger.LogMessage(LogLevel.Debug, message, args);
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x0001DAC4 File Offset: 0x0001BCC4
		public static void LogDebug(this IBUTRLogger logger, string message, params object[] args)
		{
			bool flag = logger == null;
			if (flag)
			{
				throw new ArgumentNullException("logger");
			}
			logger.LogMessage(LogLevel.Debug, message, args);
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x0001DAF0 File Offset: 0x0001BCF0
		public static void LogTrace(this IBUTRLogger logger, Exception exception, string message, params object[] args)
		{
			bool flag = logger == null;
			if (flag)
			{
				throw new ArgumentNullException("logger");
			}
			logger.LogMessage(LogLevel.Trace, message, args);
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x0001DB1C File Offset: 0x0001BD1C
		public static void LogTrace(this IBUTRLogger logger, string message, params object[] args)
		{
			bool flag = logger == null;
			if (flag)
			{
				throw new ArgumentNullException("logger");
			}
			logger.LogMessage(LogLevel.Trace, message, args);
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x0001DB48 File Offset: 0x0001BD48
		public static void LogInformation(this IBUTRLogger logger, Exception exception, string message, params object[] args)
		{
			bool flag = logger == null;
			if (flag)
			{
				throw new ArgumentNullException("logger");
			}
			logger.LogMessage(LogLevel.Information, message, args);
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0001DB74 File Offset: 0x0001BD74
		public static void LogInformation(this IBUTRLogger logger, string message, params object[] args)
		{
			bool flag = logger == null;
			if (flag)
			{
				throw new ArgumentNullException("logger");
			}
			logger.LogMessage(LogLevel.Information, message, args);
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0001DBA0 File Offset: 0x0001BDA0
		public static void LogWarning(this IBUTRLogger logger, Exception exception, string message, params object[] args)
		{
			bool flag = logger == null;
			if (flag)
			{
				throw new ArgumentNullException("logger");
			}
			logger.LogMessage(LogLevel.Warning, message, args);
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x0001DBCC File Offset: 0x0001BDCC
		public static void LogWarning(this IBUTRLogger logger, string message, params object[] args)
		{
			bool flag = logger == null;
			if (flag)
			{
				throw new ArgumentNullException("logger");
			}
			logger.LogMessage(LogLevel.Warning, message, args);
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0001DBF8 File Offset: 0x0001BDF8
		public static void LogError(this IBUTRLogger logger, Exception exception, string message, params object[] args)
		{
			bool flag = logger == null;
			if (flag)
			{
				throw new ArgumentNullException("logger");
			}
			logger.LogMessage(LogLevel.Error, message, args);
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x0001DC24 File Offset: 0x0001BE24
		public static void LogError(this IBUTRLogger logger, string message, params object[] args)
		{
			bool flag = logger == null;
			if (flag)
			{
				throw new ArgumentNullException("logger");
			}
			logger.LogMessage(LogLevel.Error, message, args);
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x0001DC50 File Offset: 0x0001BE50
		public static void LogCritical(this IBUTRLogger logger, Exception exception, string message, params object[] args)
		{
			bool flag = logger == null;
			if (flag)
			{
				throw new ArgumentNullException("logger");
			}
			logger.LogMessage(LogLevel.Critical, message, args);
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0001DC7C File Offset: 0x0001BE7C
		public static void LogCritical(this IBUTRLogger logger, string message, params object[] args)
		{
			bool flag = logger == null;
			if (flag)
			{
				throw new ArgumentNullException("logger");
			}
			logger.LogMessage(LogLevel.Critical, message, args);
		}
	}
}
