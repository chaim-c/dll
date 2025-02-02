using System;
using System.Runtime.CompilerServices;

namespace BUTR.DependencyInjection.Logger
{
	// Token: 0x0200014E RID: 334
	[NullableContext(1)]
	public interface IBUTRLogger
	{
		// Token: 0x060008FB RID: 2299
		void LogMessage(LogLevel logLevel, string message, params object[] args);
	}
}
