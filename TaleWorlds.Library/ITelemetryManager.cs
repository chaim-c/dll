using System;

namespace TaleWorlds.Library
{
	// Token: 0x02000044 RID: 68
	public interface ITelemetryManager
	{
		// Token: 0x06000236 RID: 566
		uint GetTelemetryLevelMask();

		// Token: 0x06000237 RID: 567
		void StartTelemetryConnection(bool showErrors);

		// Token: 0x06000238 RID: 568
		void StopTelemetryConnection();

		// Token: 0x06000239 RID: 569
		void BeginTelemetryScopeInternal(TelemetryLevelMask levelMask, string scopeName);

		// Token: 0x0600023A RID: 570
		void BeginTelemetryScopeBaseLevelInternal(TelemetryLevelMask levelMask, string scopeName);

		// Token: 0x0600023B RID: 571
		void EndTelemetryScopeInternal();

		// Token: 0x0600023C RID: 572
		void EndTelemetryScopeBaseLevelInternal();
	}
}
