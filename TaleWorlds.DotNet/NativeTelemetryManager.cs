using System;
using TaleWorlds.Library;

namespace TaleWorlds.DotNet
{
	// Token: 0x0200002E RID: 46
	public class NativeTelemetryManager : ITelemetryManager
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000123 RID: 291 RVA: 0x0000533E File Offset: 0x0000353E
		// (set) Token: 0x06000124 RID: 292 RVA: 0x00005345 File Offset: 0x00003545
		public static uint TelemetryLevelMask { get; private set; }

		// Token: 0x06000125 RID: 293 RVA: 0x0000534D File Offset: 0x0000354D
		public uint GetTelemetryLevelMask()
		{
			return NativeTelemetryManager.TelemetryLevelMask;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00005354 File Offset: 0x00003554
		public NativeTelemetryManager()
		{
			NativeTelemetryManager.TelemetryLevelMask = 4096U;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00005366 File Offset: 0x00003566
		internal void Update()
		{
			NativeTelemetryManager.TelemetryLevelMask = LibraryApplicationInterface.ITelemetry.GetTelemetryLevelMask();
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00005377 File Offset: 0x00003577
		public void StartTelemetryConnection(bool showErrors)
		{
			LibraryApplicationInterface.ITelemetry.StartTelemetryConnection(showErrors);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00005384 File Offset: 0x00003584
		public void StopTelemetryConnection()
		{
			LibraryApplicationInterface.ITelemetry.StopTelemetryConnection();
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00005390 File Offset: 0x00003590
		public void BeginTelemetryScopeInternal(TelemetryLevelMask levelMask, string scopeName)
		{
			if ((NativeTelemetryManager.TelemetryLevelMask & (uint)levelMask) != 0U)
			{
				LibraryApplicationInterface.ITelemetry.BeginTelemetryScope(levelMask, scopeName);
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000053A7 File Offset: 0x000035A7
		public void EndTelemetryScopeInternal()
		{
			LibraryApplicationInterface.ITelemetry.EndTelemetryScope();
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000053B3 File Offset: 0x000035B3
		public void BeginTelemetryScopeBaseLevelInternal(TelemetryLevelMask levelMask, string scopeName)
		{
			if ((NativeTelemetryManager.TelemetryLevelMask & (uint)levelMask) != 0U)
			{
				LibraryApplicationInterface.ITelemetry.BeginTelemetryScope(levelMask, scopeName);
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000053CA File Offset: 0x000035CA
		public void EndTelemetryScopeBaseLevelInternal()
		{
			LibraryApplicationInterface.ITelemetry.EndTelemetryScope();
		}
	}
}
