using System;

namespace TaleWorlds.Library
{
	// Token: 0x0200002A RID: 42
	public class EngineMethod : Attribute
	{
		// Token: 0x06000158 RID: 344 RVA: 0x00005B46 File Offset: 0x00003D46
		public EngineMethod(string engineMethodName, bool activateTelemetryProfiling = false)
		{
			this.EngineMethodName = engineMethodName;
			this.ActivateTelemetryProfiling = activateTelemetryProfiling;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00005B5C File Offset: 0x00003D5C
		// (set) Token: 0x0600015A RID: 346 RVA: 0x00005B64 File Offset: 0x00003D64
		public string EngineMethodName { get; private set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00005B6D File Offset: 0x00003D6D
		// (set) Token: 0x0600015C RID: 348 RVA: 0x00005B75 File Offset: 0x00003D75
		public bool ActivateTelemetryProfiling { get; private set; }
	}
}
