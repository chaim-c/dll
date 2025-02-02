using System;

namespace TaleWorlds.Engine
{
	// Token: 0x02000090 RID: 144
	public static class Time
	{
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000AEB RID: 2795 RVA: 0x0000BFED File Offset: 0x0000A1ED
		public static float ApplicationTime
		{
			get
			{
				return EngineApplicationInterface.ITime.GetApplicationTime();
			}
		}
	}
}
