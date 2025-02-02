using System;

namespace Epic.OnlineServices.Logging
{
	// Token: 0x02000318 RID: 792
	public sealed class LoggingInterface
	{
		// Token: 0x06001542 RID: 5442 RVA: 0x0001F948 File Offset: 0x0001DB48
		public static Result SetCallback(LogMessageFunc callback)
		{
			LogMessageFuncInternal logMessageFuncInternal = new LogMessageFuncInternal(LoggingInterface.LogMessageFuncInternalImplementation);
			Helper.AddStaticCallback("LogMessageFuncInternalImplementation", callback, logMessageFuncInternal);
			return Bindings.EOS_Logging_SetCallback(logMessageFuncInternal);
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x0001F97C File Offset: 0x0001DB7C
		public static Result SetLogLevel(LogCategory logCategory, LogLevel logLevel)
		{
			return Bindings.EOS_Logging_SetLogLevel(logCategory, logLevel);
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x0001F998 File Offset: 0x0001DB98
		[MonoPInvokeCallback(typeof(LogMessageFuncInternal))]
		internal static void LogMessageFuncInternalImplementation(ref LogMessageInternal message)
		{
			LogMessageFunc logMessageFunc;
			bool flag = Helper.TryGetStaticCallback<LogMessageFunc>("LogMessageFuncInternalImplementation", out logMessageFunc);
			if (flag)
			{
				LogMessage logMessage;
				Helper.Get<LogMessageInternal, LogMessage>(ref message, out logMessage);
				logMessageFunc(ref logMessage);
			}
		}
	}
}
