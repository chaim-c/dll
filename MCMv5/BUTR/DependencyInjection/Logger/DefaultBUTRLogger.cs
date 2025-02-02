using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace BUTR.DependencyInjection.Logger
{
	// Token: 0x0200014C RID: 332
	public class DefaultBUTRLogger : IBUTRLogger
	{
		// Token: 0x060008F7 RID: 2295 RVA: 0x0001DCA8 File Offset: 0x0001BEA8
		[NullableContext(1)]
		public void LogMessage(LogLevel logLevel, string message, params object[] args)
		{
			switch (logLevel)
			{
			case LogLevel.Information:
				Trace.TraceInformation(message, args);
				break;
			case LogLevel.Warning:
				Trace.TraceWarning(message, args);
				break;
			case LogLevel.Error:
				Trace.TraceError(message, args);
				break;
			case LogLevel.Critical:
				Trace.TraceError(message, args);
				break;
			default:
				Trace.TraceInformation(message, args);
				break;
			}
		}
	}
}
