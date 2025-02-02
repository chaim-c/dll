using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x0200005E RID: 94
	public static class MBDebug
	{
		// Token: 0x060007B5 RID: 1973 RVA: 0x00007219 File Offset: 0x00005419
		[CommandLineFunctionality.CommandLineArgumentFunction("toggle_ui", "ui")]
		public static string DisableUI(List<string> strings)
		{
			if (strings.Count != 0)
			{
				return "Invalid input.";
			}
			MBDebug.DisableAllUI = !MBDebug.DisableAllUI;
			if (MBDebug.DisableAllUI)
			{
				return "UI is now disabled.";
			}
			return "UI is now enabled.";
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x00007254 File Offset: 0x00005454
		[Conditional("_RGL_KEEP_ASSERTS")]
		public static void AssertMemoryUsage(int memoryMB)
		{
			EngineApplicationInterface.IDebug.AssertMemoryUsage(memoryMB);
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x00007261 File Offset: 0x00005461
		public static void AbortGame(int ExitCode = 5)
		{
			EngineApplicationInterface.IDebug.AbortGame(ExitCode);
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x00007270 File Offset: 0x00005470
		public static void ShowWarning(string message)
		{
			bool flag = EngineApplicationInterface.IDebug.Warning(message);
			if (Debugger.IsAttached && flag)
			{
				Debugger.Break();
			}
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x00007298 File Offset: 0x00005498
		public static void ContentWarning(string message)
		{
			bool flag = EngineApplicationInterface.IDebug.ContentWarning(message);
			if (Debugger.IsAttached && flag)
			{
				Debugger.Break();
			}
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x000072C0 File Offset: 0x000054C0
		[Conditional("_RGL_KEEP_ASSERTS")]
		public static void ConditionalContentWarning(bool condition, string message)
		{
			if (!condition)
			{
				bool flag = EngineApplicationInterface.IDebug.ContentWarning(message);
				if (Debugger.IsAttached && flag)
				{
					Debugger.Break();
				}
			}
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x000072EC File Offset: 0x000054EC
		public static void ShowError(string message)
		{
			bool flag = EngineApplicationInterface.IDebug.Error(message);
			if (Debugger.IsAttached && flag)
			{
				Debugger.Break();
			}
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x00007313 File Offset: 0x00005513
		public static void ShowMessageBox(string lpText, string lpCaption, uint uType)
		{
			EngineApplicationInterface.IDebug.MessageBox(lpText, lpCaption, uType);
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x00007324 File Offset: 0x00005524
		[Conditional("_RGL_KEEP_ASSERTS")]
		public static void Assert(bool condition, string message, [CallerFilePath] string callerFile = "", [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0)
		{
			if (!condition)
			{
				bool flag = EngineApplicationInterface.IDebug.FailedAssert(message, callerFile, callerMethod, callerLine);
				if (Debugger.IsAttached && flag)
				{
					Debugger.Break();
				}
			}
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x00007352 File Offset: 0x00005552
		[Conditional("_RGL_KEEP_ASSERTS")]
		public static void FailedAssert(string message, [CallerFilePath] string callerFile = "", [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0)
		{
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x00007354 File Offset: 0x00005554
		public static void SilentAssert(bool condition, string message = "", bool getDump = false, [CallerFilePath] string callerFile = "", [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0)
		{
			if (!condition)
			{
				bool flag = EngineApplicationInterface.IDebug.SilentAssert(message, callerFile, callerMethod, callerLine, getDump);
				if (Debugger.IsAttached && flag)
				{
					Debugger.Break();
				}
			}
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x00007384 File Offset: 0x00005584
		[Conditional("DEBUG_MORE")]
		public static void AssertConditionOrCallerClassName(bool condition, string name)
		{
			StackFrame frame = new StackTrace(2, true).GetFrame(0);
			if (!condition)
			{
				string name2 = frame.GetMethod().DeclaringType.Name;
			}
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x000073B4 File Offset: 0x000055B4
		[Conditional("DEBUG_MORE")]
		public static void AssertConditionOrCallerClassNameSearchAllCallstack(bool condition, string name)
		{
			StackTrace stackTrace = new StackTrace(true);
			if (!condition)
			{
				int num = 0;
				while (num < stackTrace.FrameCount && !(stackTrace.GetFrame(num).GetMethod().DeclaringType.Name == name))
				{
					num++;
				}
			}
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x000073FC File Offset: 0x000055FC
		public static void Print(string message, int logLevel = 0, Debug.DebugColor color = Debug.DebugColor.White, ulong debugFilter = 17592186044416UL)
		{
			if (MBDebug.DisableLogging)
			{
				return;
			}
			debugFilter &= 18446744069414584320UL;
			if (debugFilter == 0UL)
			{
				return;
			}
			try
			{
				if (EngineApplicationInterface.IDebug != null)
				{
					EngineApplicationInterface.IDebug.WriteLine(logLevel, message, (int)color, debugFilter);
				}
			}
			catch
			{
			}
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00007450 File Offset: 0x00005650
		[Conditional("_RGL_KEEP_ASSERTS")]
		public static void ConsolePrint(string message, Debug.DebugColor color = Debug.DebugColor.White, ulong debugFilter = 17592186044416UL)
		{
			try
			{
				EngineApplicationInterface.IDebug.WriteLine(0, message, (int)color, debugFilter);
			}
			catch
			{
			}
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x00007480 File Offset: 0x00005680
		[Conditional("_RGL_KEEP_ASSERTS")]
		public static void WriteDebugLineOnScreen(string str)
		{
			EngineApplicationInterface.IDebug.WriteDebugLineOnScreen(str);
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x0000748D File Offset: 0x0000568D
		[Conditional("_RGL_KEEP_ASSERTS")]
		public static void RenderDebugText(float screenX, float screenY, string text, uint color = 4294967295U, float time = 0f)
		{
			EngineApplicationInterface.IDebug.RenderDebugText(screenX, screenY, text, color, time);
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x0000749F File Offset: 0x0000569F
		public static void RenderText(float screenX, float screenY, string text, uint color = 4294967295U, float time = 0f)
		{
			EngineApplicationInterface.IDebug.RenderDebugText(screenX, screenY, text, color, time);
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x000074B1 File Offset: 0x000056B1
		[Conditional("_RGL_KEEP_ASSERTS")]
		public static void RenderDebugRect(float left, float bottom, float right, float top)
		{
			EngineApplicationInterface.IDebug.RenderDebugRect(left, bottom, right, top);
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x000074C1 File Offset: 0x000056C1
		[Conditional("_RGL_KEEP_ASSERTS")]
		public static void RenderDebugRectWithColor(float left, float bottom, float right, float top, uint color = 4294967295U)
		{
			EngineApplicationInterface.IDebug.RenderDebugRectWithColor(left, bottom, right, top, color);
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x000074D3 File Offset: 0x000056D3
		[Conditional("_RGL_KEEP_ASSERTS")]
		public static void RenderDebugFrame(MatrixFrame frame, float lineLength, float time = 0f)
		{
			EngineApplicationInterface.IDebug.RenderDebugFrame(ref frame, lineLength, time);
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x000074E3 File Offset: 0x000056E3
		[Conditional("_RGL_KEEP_ASSERTS")]
		public static void RenderDebugText3D(Vec3 worldPosition, string str, uint color = 4294967295U, int screenPosOffsetX = 0, int screenPosOffsetY = 0, float time = 0f)
		{
			EngineApplicationInterface.IDebug.RenderDebugText3d(worldPosition, str, color, screenPosOffsetX, screenPosOffsetY, time);
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x000074F7 File Offset: 0x000056F7
		[Conditional("_RGL_KEEP_ASSERTS")]
		public static void RenderDebugDirectionArrow(Vec3 position, Vec3 direction, uint color = 4294967295U, bool depthCheck = false)
		{
			EngineApplicationInterface.IDebug.RenderDebugDirectionArrow(position, direction, color, depthCheck);
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x00007507 File Offset: 0x00005707
		[Conditional("_RGL_KEEP_ASSERTS")]
		public static void RenderDebugLine(Vec3 position, Vec3 direction, uint color = 4294967295U, bool depthCheck = false, float time = 0f)
		{
			EngineApplicationInterface.IDebug.RenderDebugLine(position, direction, color, depthCheck, time);
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x00007519 File Offset: 0x00005719
		[Conditional("_RGL_KEEP_ASSERTS")]
		public static void RenderDebugSphere(Vec3 position, float radius, uint color = 4294967295U, bool depthCheck = false, float time = 0f)
		{
			EngineApplicationInterface.IDebug.RenderDebugSphere(position, radius, color, depthCheck, time);
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x0000752B File Offset: 0x0000572B
		[Conditional("_RGL_KEEP_ASSERTS")]
		public static void RenderDebugCapsule(Vec3 p0, Vec3 p1, float radius, uint color = 4294967295U, bool depthCheck = false, float time = 0f)
		{
			EngineApplicationInterface.IDebug.RenderDebugCapsule(p0, p1, radius, color, depthCheck, time);
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x0000753F File Offset: 0x0000573F
		[Conditional("_RGL_KEEP_ASSERTS")]
		public static void ClearRenderObjects()
		{
			EngineApplicationInterface.IDebug.ClearAllDebugRenderObjects();
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060007D1 RID: 2001 RVA: 0x0000754B File Offset: 0x0000574B
		public static Vec3 DebugVector
		{
			get
			{
				return EngineApplicationInterface.IDebug.GetDebugVector();
			}
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x00007557 File Offset: 0x00005757
		[Conditional("_RGL_KEEP_ASSERTS")]
		public static void RenderDebugBoxObject(Vec3 min, Vec3 max, uint color = 4294967295U, bool depthCheck = false, float time = 0f)
		{
			EngineApplicationInterface.IDebug.RenderDebugBoxObject(min, max, color, depthCheck, time);
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x00007569 File Offset: 0x00005769
		[Conditional("_RGL_KEEP_ASSERTS")]
		public static void RenderDebugBoxObject(Vec3 min, Vec3 max, MatrixFrame frame, uint color = 4294967295U, bool depthCheck = false, float time = 0f)
		{
			EngineApplicationInterface.IDebug.RenderDebugBoxObjectWithFrame(min, max, ref frame, color, depthCheck, time);
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x0000757E File Offset: 0x0000577E
		[Conditional("_RGL_KEEP_ASSERTS")]
		public static void PostWarningLine(string line)
		{
			EngineApplicationInterface.IDebug.PostWarningLine(line);
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0000758B File Offset: 0x0000578B
		public static bool IsErrorReportModeActive()
		{
			return EngineApplicationInterface.IDebug.IsErrorReportModeActive();
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x00007597 File Offset: 0x00005797
		public static bool IsErrorReportModePauseMission()
		{
			return EngineApplicationInterface.IDebug.IsErrorReportModePauseMission();
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x000075A4 File Offset: 0x000057A4
		public static void SetErrorReportScene(Scene scene)
		{
			UIntPtr errorReportScene = (scene == null) ? UIntPtr.Zero : scene.Pointer;
			EngineApplicationInterface.IDebug.SetErrorReportScene(errorReportScene);
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x000075D3 File Offset: 0x000057D3
		public static void SetDumpGenerationDisabled(bool value)
		{
			EngineApplicationInterface.IDebug.SetDumpGenerationDisabled(value);
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x000075E0 File Offset: 0x000057E0
		public static void EchoCommandWindow(string content)
		{
			EngineApplicationInterface.IDebug.EchoCommandWindow(content);
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x000075ED File Offset: 0x000057ED
		[CommandLineFunctionality.CommandLineArgumentFunction("clear", "console")]
		public static string ClearConsole(List<string> strings)
		{
			Console.Clear();
			return "Debug console cleared.";
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x000075F9 File Offset: 0x000057F9
		[CommandLineFunctionality.CommandLineArgumentFunction("echo_command_window", "console")]
		public static string EchoCommandWindow(List<string> strings)
		{
			MBDebug.EchoCommandWindow(strings[0]);
			return "";
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x0000760C File Offset: 0x0000580C
		[CommandLineFunctionality.CommandLineArgumentFunction("echo_command_window_test", "console")]
		public static string EchoCommandWindowTest(List<string> strings)
		{
			MBDebug.EchoCommandWindowTestAux();
			return "";
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x00007618 File Offset: 0x00005818
		private static async void EchoCommandWindowTestAux()
		{
			MBDebug.EchoCommandWindow("5...");
			await Task.Delay(1000);
			MBDebug.EchoCommandWindow("4...");
			await Task.Delay(1000);
			MBDebug.EchoCommandWindow("3...");
			await Task.Delay(1000);
			MBDebug.EchoCommandWindow("2...");
			await Task.Delay(1000);
			MBDebug.EchoCommandWindow("1...");
			await Task.Delay(1000);
			MBDebug.EchoCommandWindow("Tada!");
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060007DE RID: 2014 RVA: 0x00007649 File Offset: 0x00005849
		// (set) Token: 0x060007DF RID: 2015 RVA: 0x00007655 File Offset: 0x00005855
		public static int ShowDebugInfoState
		{
			get
			{
				return EngineApplicationInterface.IDebug.GetShowDebugInfo();
			}
			set
			{
				EngineApplicationInterface.IDebug.SetShowDebugInfo(value);
			}
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x00007662 File Offset: 0x00005862
		public static bool IsTestMode()
		{
			return EngineApplicationInterface.IDebug.IsTestMode();
		}

		// Token: 0x04000101 RID: 257
		public static bool DisableAllUI;

		// Token: 0x04000102 RID: 258
		public static bool TestModeEnabled;

		// Token: 0x04000103 RID: 259
		public static bool ShouldAssertThrowException;

		// Token: 0x04000104 RID: 260
		public static bool IsDisplayingHighLevelAI;

		// Token: 0x04000105 RID: 261
		public static bool DisableLogging;

		// Token: 0x04000106 RID: 262
		private static readonly Dictionary<string, int> ProcessedFrameList = new Dictionary<string, int>();

		// Token: 0x020000BD RID: 189
		[Flags]
		public enum MessageBoxTypeFlag
		{
			// Token: 0x040003ED RID: 1005
			Ok = 1,
			// Token: 0x040003EE RID: 1006
			Warning = 2,
			// Token: 0x040003EF RID: 1007
			Error = 4,
			// Token: 0x040003F0 RID: 1008
			OkCancel = 8,
			// Token: 0x040003F1 RID: 1009
			RetryCancel = 16,
			// Token: 0x040003F2 RID: 1010
			YesNo = 32,
			// Token: 0x040003F3 RID: 1011
			YesNoCancel = 64,
			// Token: 0x040003F4 RID: 1012
			Information = 128,
			// Token: 0x040003F5 RID: 1013
			Exclamation = 256,
			// Token: 0x040003F6 RID: 1014
			Question = 512,
			// Token: 0x040003F7 RID: 1015
			AssertFailed = 1024
		}
	}
}
