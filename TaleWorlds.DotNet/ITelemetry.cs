using System;
using TaleWorlds.Library;

namespace TaleWorlds.DotNet
{
	// Token: 0x02000019 RID: 25
	[LibraryInterfaceBase]
	internal interface ITelemetry
	{
		// Token: 0x06000067 RID: 103
		[EngineMethod("get_telemetry_level_mask", false)]
		uint GetTelemetryLevelMask();

		// Token: 0x06000068 RID: 104
		[EngineMethod("start_telemetry_connection", false)]
		void StartTelemetryConnection(bool showErrors);

		// Token: 0x06000069 RID: 105
		[EngineMethod("stop_telemetry_connection", false)]
		void StopTelemetryConnection();

		// Token: 0x0600006A RID: 106
		[EngineMethod("begin_telemetry_scope", false)]
		void BeginTelemetryScope(TelemetryLevelMask levelMask, string scopeName);

		// Token: 0x0600006B RID: 107
		[EngineMethod("end_telemetry_scope", false)]
		void EndTelemetryScope();

		// Token: 0x0600006C RID: 108
		[EngineMethod("has_telemetry_connection", false)]
		bool HasTelemetryConnection();
	}
}
