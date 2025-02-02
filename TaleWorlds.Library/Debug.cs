using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace TaleWorlds.Library
{
	// Token: 0x02000027 RID: 39
	public static class Debug
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060000FF RID: 255 RVA: 0x000056F8 File Offset: 0x000038F8
		// (remove) Token: 0x06000100 RID: 256 RVA: 0x0000572C File Offset: 0x0000392C
		public static event Action<string, ulong> OnPrint;

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000101 RID: 257 RVA: 0x0000575F File Offset: 0x0000395F
		// (set) Token: 0x06000102 RID: 258 RVA: 0x00005766 File Offset: 0x00003966
		public static IDebugManager DebugManager { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000103 RID: 259 RVA: 0x0000576E File Offset: 0x0000396E
		// (set) Token: 0x06000104 RID: 260 RVA: 0x00005775 File Offset: 0x00003975
		public static ITelemetryManager TelemetryManager { get; set; }

		// Token: 0x06000105 RID: 261 RVA: 0x0000577D File Offset: 0x0000397D
		public static uint GetTelemetryLevelMask()
		{
			ITelemetryManager telemetryManager = Debug.TelemetryManager;
			if (telemetryManager == null)
			{
				return 4096U;
			}
			return telemetryManager.GetTelemetryLevelMask();
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00005793 File Offset: 0x00003993
		public static void SetCrashReportCustomString(string customString)
		{
			if (Debug.DebugManager != null)
			{
				Debug.DebugManager.SetCrashReportCustomString(customString);
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000057A7 File Offset: 0x000039A7
		public static void SetCrashReportCustomStack(string customStack)
		{
			if (Debug.DebugManager != null)
			{
				Debug.DebugManager.SetCrashReportCustomStack(customStack);
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000057BB File Offset: 0x000039BB
		[Conditional("_RGL_KEEP_ASSERTS")]
		public static void Assert(bool condition, string message, [CallerFilePath] string callerFile = "", [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0)
		{
			if (Debug.DebugManager != null)
			{
				Debug.DebugManager.Assert(condition, message, callerFile, callerMethod, callerLine);
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000057D4 File Offset: 0x000039D4
		public static void FailedAssert(string message, [CallerFilePath] string callerFile = "", [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0)
		{
			if (Debug.DebugManager != null)
			{
				Debug.DebugManager.Assert(false, message, callerFile, callerMethod, callerLine);
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000057EC File Offset: 0x000039EC
		public static void SilentAssert(bool condition, string message = "", bool getDump = false, [CallerFilePath] string callerFile = "", [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0)
		{
			if (Debug.DebugManager != null)
			{
				Debug.DebugManager.SilentAssert(condition, message, getDump, callerFile, callerMethod, callerLine);
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00005807 File Offset: 0x00003A07
		public static void ShowError(string message)
		{
			if (Debug.DebugManager != null)
			{
				Debug.DebugManager.ShowError(message);
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000581B File Offset: 0x00003A1B
		internal static void DoDelayedexit(int returnCode)
		{
			if (Debug.DebugManager != null)
			{
				Debug.DebugManager.DoDelayedexit(returnCode);
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000582F File Offset: 0x00003A2F
		[Conditional("_RGL_KEEP_ASSERTS")]
		public static void ShowWarning(string message)
		{
			if (Debug.DebugManager != null)
			{
				Debug.DebugManager.ShowWarning(message);
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00005843 File Offset: 0x00003A43
		public static void ReportMemoryBookmark(string message)
		{
			IDebugManager debugManager = Debug.DebugManager;
			if (debugManager == null)
			{
				return;
			}
			debugManager.ReportMemoryBookmark(message);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00005855 File Offset: 0x00003A55
		public static void Print(string message, int logLevel = 0, Debug.DebugColor color = Debug.DebugColor.White, ulong debugFilter = 17592186044416UL)
		{
			if (Debug.DebugManager != null)
			{
				debugFilter &= 18446744069414584320UL;
				if (debugFilter == 0UL)
				{
					return;
				}
				Debug.DebugManager.Print(message, logLevel, color, debugFilter);
				Action<string, ulong> onPrint = Debug.OnPrint;
				if (onPrint == null)
				{
					return;
				}
				onPrint(message, debugFilter);
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000588E File Offset: 0x00003A8E
		public static void ShowMessageBox(string lpText, string lpCaption, uint uType)
		{
			if (Debug.DebugManager != null)
			{
				Debug.DebugManager.ShowMessageBox(lpText, lpCaption, uType);
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000058A4 File Offset: 0x00003AA4
		[Conditional("_RGL_KEEP_ASSERTS")]
		public static void PrintWarning(string warning, ulong debugFilter = 17592186044416UL)
		{
			if (Debug.DebugManager != null)
			{
				Debug.DebugManager.PrintWarning(warning, debugFilter);
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000058B9 File Offset: 0x00003AB9
		[Conditional("_RGL_KEEP_ASSERTS")]
		public static void PrintError(string error, string stackTrace = null, ulong debugFilter = 17592186044416UL)
		{
			if (Debug.DebugManager != null)
			{
				Debug.DebugManager.PrintError(error, stackTrace, debugFilter);
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000058CF File Offset: 0x00003ACF
		[Conditional("_RGL_KEEP_ASSERTS")]
		public static void DisplayDebugMessage(string message)
		{
			if (Debug.DebugManager != null)
			{
				Debug.DebugManager.DisplayDebugMessage(message);
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000058E3 File Offset: 0x00003AE3
		[Conditional("_RGL_KEEP_ASSERTS")]
		public static void WatchVariable(string name, object value)
		{
			IDebugManager debugManager = Debug.DebugManager;
			if (debugManager == null)
			{
				return;
			}
			debugManager.WatchVariable(name, value);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000058F6 File Offset: 0x00003AF6
		[Conditional("NOT_SHIPPING")]
		[Conditional("ENABLE_PROFILING_APIS_IN_SHIPPING")]
		public static void StartTelemetryConnection(bool showErrors)
		{
			ITelemetryManager telemetryManager = Debug.TelemetryManager;
			if (telemetryManager == null)
			{
				return;
			}
			telemetryManager.StartTelemetryConnection(showErrors);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00005908 File Offset: 0x00003B08
		[Conditional("NOT_SHIPPING")]
		[Conditional("ENABLE_PROFILING_APIS_IN_SHIPPING")]
		public static void StopTelemetryConnection()
		{
			ITelemetryManager telemetryManager = Debug.TelemetryManager;
			if (telemetryManager == null)
			{
				return;
			}
			telemetryManager.StopTelemetryConnection();
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00005919 File Offset: 0x00003B19
		[Conditional("NOT_SHIPPING")]
		[Conditional("ENABLE_PROFILING_APIS_IN_SHIPPING")]
		internal static void BeginTelemetryScopeInternal(TelemetryLevelMask levelMask, string scopeName)
		{
			ITelemetryManager telemetryManager = Debug.TelemetryManager;
			if (telemetryManager == null)
			{
				return;
			}
			telemetryManager.BeginTelemetryScopeInternal(levelMask, scopeName);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x0000592C File Offset: 0x00003B2C
		[Conditional("NOT_SHIPPING")]
		[Conditional("ENABLE_PROFILING_APIS_IN_SHIPPING")]
		internal static void BeginTelemetryScopeBaseLevelInternal(TelemetryLevelMask levelMask, string scopeName)
		{
			ITelemetryManager telemetryManager = Debug.TelemetryManager;
			if (telemetryManager == null)
			{
				return;
			}
			telemetryManager.BeginTelemetryScopeBaseLevelInternal(levelMask, scopeName);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x0000593F File Offset: 0x00003B3F
		[Conditional("NOT_SHIPPING")]
		[Conditional("ENABLE_PROFILING_APIS_IN_SHIPPING")]
		internal static void EndTelemetryScopeInternal()
		{
			ITelemetryManager telemetryManager = Debug.TelemetryManager;
			if (telemetryManager == null)
			{
				return;
			}
			telemetryManager.EndTelemetryScopeInternal();
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00005950 File Offset: 0x00003B50
		[Conditional("NOT_SHIPPING")]
		[Conditional("ENABLE_PROFILING_APIS_IN_SHIPPING")]
		internal static void EndTelemetryScopeBaseLevelInternal()
		{
			ITelemetryManager telemetryManager = Debug.TelemetryManager;
			if (telemetryManager == null)
			{
				return;
			}
			telemetryManager.EndTelemetryScopeBaseLevelInternal();
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00005961 File Offset: 0x00003B61
		[Conditional("_RGL_KEEP_ASSERTS")]
		public static void WriteDebugLineOnScreen(string message)
		{
			IDebugManager debugManager = Debug.DebugManager;
			if (debugManager == null)
			{
				return;
			}
			debugManager.WriteDebugLineOnScreen(message);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00005973 File Offset: 0x00003B73
		[Conditional("_RGL_KEEP_ASSERTS")]
		public static void RenderDebugLine(Vec3 position, Vec3 direction, uint color = 4294967295U, bool depthCheck = false, float time = 0f)
		{
			IDebugManager debugManager = Debug.DebugManager;
			if (debugManager == null)
			{
				return;
			}
			debugManager.RenderDebugLine(position, direction, color, depthCheck, time);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000598A File Offset: 0x00003B8A
		[Conditional("_RGL_KEEP_ASSERTS")]
		public static void RenderDebugSphere(Vec3 position, float radius, uint color = 4294967295U, bool depthCheck = false, float time = 0f)
		{
			IDebugManager debugManager = Debug.DebugManager;
			if (debugManager == null)
			{
				return;
			}
			debugManager.RenderDebugSphere(position, radius, color, depthCheck, time);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000059A1 File Offset: 0x00003BA1
		[Conditional("_RGL_KEEP_ASSERTS")]
		public static void RenderDebugFrame(MatrixFrame frame, float lineLength, float time = 0f)
		{
			IDebugManager debugManager = Debug.DebugManager;
			if (debugManager == null)
			{
				return;
			}
			debugManager.RenderDebugFrame(frame, lineLength, time);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000059B5 File Offset: 0x00003BB5
		[Conditional("_RGL_KEEP_ASSERTS")]
		public static void RenderDebugText(float screenX, float screenY, string text, uint color = 4294967295U, float time = 0f)
		{
			IDebugManager debugManager = Debug.DebugManager;
			if (debugManager == null)
			{
				return;
			}
			debugManager.RenderDebugText(screenX, screenY, text, color, time);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000059CC File Offset: 0x00003BCC
		[Conditional("_RGL_KEEP_ASSERTS")]
		public static void RenderDebugRectWithColor(float left, float bottom, float right, float top, uint color = 4294967295U)
		{
			IDebugManager debugManager = Debug.DebugManager;
			if (debugManager == null)
			{
				return;
			}
			debugManager.RenderDebugRectWithColor(left, bottom, right, top, color);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000059E3 File Offset: 0x00003BE3
		[Conditional("_RGL_KEEP_ASSERTS")]
		public static void RenderDebugText3D(Vec3 position, string text, uint color = 4294967295U, int screenPosOffsetX = 0, int screenPosOffsetY = 0, float time = 0f)
		{
			IDebugManager debugManager = Debug.DebugManager;
			if (debugManager == null)
			{
				return;
			}
			debugManager.RenderDebugText3D(position, text, color, screenPosOffsetX, screenPosOffsetY, time);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000059FC File Offset: 0x00003BFC
		public static Vec3 GetDebugVector()
		{
			IDebugManager debugManager = Debug.DebugManager;
			if (debugManager == null)
			{
				return Vec3.Zero;
			}
			return debugManager.GetDebugVector();
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00005A12 File Offset: 0x00003C12
		public static void SetTestModeEnabled(bool testModeEnabled)
		{
			IDebugManager debugManager = Debug.DebugManager;
			if (debugManager == null)
			{
				return;
			}
			debugManager.SetTestModeEnabled(testModeEnabled);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00005A24 File Offset: 0x00003C24
		public static void AbortGame()
		{
			if (Debug.DebugManager != null)
			{
				Debug.DebugManager.AbortGame();
			}
		}

		// Token: 0x020000C7 RID: 199
		public enum DebugColor
		{
			// Token: 0x0400022F RID: 559
			DarkRed,
			// Token: 0x04000230 RID: 560
			DarkGreen,
			// Token: 0x04000231 RID: 561
			DarkBlue,
			// Token: 0x04000232 RID: 562
			Red,
			// Token: 0x04000233 RID: 563
			Green,
			// Token: 0x04000234 RID: 564
			Blue,
			// Token: 0x04000235 RID: 565
			DarkCyan,
			// Token: 0x04000236 RID: 566
			Cyan,
			// Token: 0x04000237 RID: 567
			DarkYellow,
			// Token: 0x04000238 RID: 568
			Yellow,
			// Token: 0x04000239 RID: 569
			Purple,
			// Token: 0x0400023A RID: 570
			Magenta,
			// Token: 0x0400023B RID: 571
			White,
			// Token: 0x0400023C RID: 572
			BrightWhite
		}

		// Token: 0x020000C8 RID: 200
		public enum DebugUserFilter : ulong
		{
			// Token: 0x0400023E RID: 574
			None,
			// Token: 0x0400023F RID: 575
			Unused0,
			// Token: 0x04000240 RID: 576
			Unused1,
			// Token: 0x04000241 RID: 577
			Koray = 4UL,
			// Token: 0x04000242 RID: 578
			Armagan = 8UL,
			// Token: 0x04000243 RID: 579
			Intern = 16UL,
			// Token: 0x04000244 RID: 580
			Mustafa = 32UL,
			// Token: 0x04000245 RID: 581
			Oguzhan = 64UL,
			// Token: 0x04000246 RID: 582
			Omer = 128UL,
			// Token: 0x04000247 RID: 583
			Ates = 256UL,
			// Token: 0x04000248 RID: 584
			Unused3 = 512UL,
			// Token: 0x04000249 RID: 585
			Basak = 1024UL,
			// Token: 0x0400024A RID: 586
			Can = 2048UL,
			// Token: 0x0400024B RID: 587
			Unused4 = 4096UL,
			// Token: 0x0400024C RID: 588
			Cem = 8192UL,
			// Token: 0x0400024D RID: 589
			Unused5 = 16384UL,
			// Token: 0x0400024E RID: 590
			Unused6 = 32768UL,
			// Token: 0x0400024F RID: 591
			Emircan = 65536UL,
			// Token: 0x04000250 RID: 592
			Unused7 = 131072UL,
			// Token: 0x04000251 RID: 593
			All = 4294967295UL,
			// Token: 0x04000252 RID: 594
			Default = 0UL,
			// Token: 0x04000253 RID: 595
			DamageDebug = 72UL
		}

		// Token: 0x020000C9 RID: 201
		public enum DebugSystemFilter : ulong
		{
			// Token: 0x04000255 RID: 597
			None,
			// Token: 0x04000256 RID: 598
			Graphics = 4294967296UL,
			// Token: 0x04000257 RID: 599
			ArtificialIntelligence = 8589934592UL,
			// Token: 0x04000258 RID: 600
			MultiPlayer = 17179869184UL,
			// Token: 0x04000259 RID: 601
			IO = 34359738368UL,
			// Token: 0x0400025A RID: 602
			Network = 68719476736UL,
			// Token: 0x0400025B RID: 603
			CampaignEvents = 137438953472UL,
			// Token: 0x0400025C RID: 604
			MemoryManager = 274877906944UL,
			// Token: 0x0400025D RID: 605
			TCP = 549755813888UL,
			// Token: 0x0400025E RID: 606
			FileManager = 1099511627776UL,
			// Token: 0x0400025F RID: 607
			NaturalInteractionDevice = 2199023255552UL,
			// Token: 0x04000260 RID: 608
			UDP = 4398046511104UL,
			// Token: 0x04000261 RID: 609
			ResourceManager = 8796093022208UL,
			// Token: 0x04000262 RID: 610
			Mono = 17592186044416UL,
			// Token: 0x04000263 RID: 611
			ONO = 35184372088832UL,
			// Token: 0x04000264 RID: 612
			Old = 70368744177664UL,
			// Token: 0x04000265 RID: 613
			Sound = 281474976710656UL,
			// Token: 0x04000266 RID: 614
			CombatLog = 562949953421312UL,
			// Token: 0x04000267 RID: 615
			Notifications = 1125899906842624UL,
			// Token: 0x04000268 RID: 616
			Quest = 2251799813685248UL,
			// Token: 0x04000269 RID: 617
			Dialog = 4503599627370496UL,
			// Token: 0x0400026A RID: 618
			Steam = 9007199254740992UL,
			// Token: 0x0400026B RID: 619
			All = 18446744069414584320UL,
			// Token: 0x0400026C RID: 620
			DefaultMask = 18446744069414584320UL
		}
	}
}
