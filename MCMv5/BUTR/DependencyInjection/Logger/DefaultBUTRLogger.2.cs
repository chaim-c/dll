using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace BUTR.DependencyInjection.Logger
{
	// Token: 0x0200014D RID: 333
	public class DefaultBUTRLogger<[Nullable(2)] T> : IBUTRLogger<T>, IBUTRLogger
	{
		// Token: 0x060008F9 RID: 2297 RVA: 0x0001DD10 File Offset: 0x0001BF10
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
