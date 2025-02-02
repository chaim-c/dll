using System;
using System.Collections.Generic;

namespace TaleWorlds.Library
{
	// Token: 0x02000029 RID: 41
	public class DiamondDebugManager : IDebugManager
	{
		// Token: 0x0600013D RID: 317 RVA: 0x00005A37 File Offset: 0x00003C37
		void IDebugManager.SetCrashReportCustomString(string customString)
		{
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00005A39 File Offset: 0x00003C39
		void IDebugManager.SetCrashReportCustomStack(string customStack)
		{
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00005A3B File Offset: 0x00003C3B
		void IDebugManager.ShowMessageBox(string lpText, string lpCaption, uint uType)
		{
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00005A3D File Offset: 0x00003C3D
		void IDebugManager.ShowError(string message)
		{
			this.PrintMessage(message, DiamondDebugManager.DiamondDebugCategory.Error);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00005A47 File Offset: 0x00003C47
		void IDebugManager.ShowWarning(string message)
		{
			this.PrintMessage(message, DiamondDebugManager.DiamondDebugCategory.Warning);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00005A51 File Offset: 0x00003C51
		void IDebugManager.Assert(bool condition, string message, string callerFile, string callerMethod, int callerLine)
		{
			if (!condition)
			{
				throw new Exception(string.Format("Assertion failed: {0} in {1}, line:{2}", message, callerFile, callerLine));
			}
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00005A6F File Offset: 0x00003C6F
		void IDebugManager.SilentAssert(bool condition, string message, bool getDump, string callerFile, string callerMethod, int callerLine)
		{
			if (!condition)
			{
				this.PrintMessage(string.Format("Assertion failed: {0} in {1}, line:{2}", message, callerMethod, callerLine), DiamondDebugManager.DiamondDebugCategory.Warning);
			}
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00005A8F File Offset: 0x00003C8F
		void IDebugManager.Print(string message, int logLevel, Debug.DebugColor color, ulong debugFilter)
		{
			this.PrintMessage(message, DiamondDebugManager.DiamondDebugCategory.General);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00005A99 File Offset: 0x00003C99
		void IDebugManager.PrintError(string error, string stackTrace, ulong debugFilter)
		{
			this.PrintMessage(error + stackTrace, DiamondDebugManager.DiamondDebugCategory.Error);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00005AA9 File Offset: 0x00003CA9
		void IDebugManager.PrintWarning(string warning, ulong debugFilter)
		{
			this.PrintMessage(warning, DiamondDebugManager.DiamondDebugCategory.Warning);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00005AB3 File Offset: 0x00003CB3
		void IDebugManager.DisplayDebugMessage(string message)
		{
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00005AB5 File Offset: 0x00003CB5
		void IDebugManager.WatchVariable(string name, object value)
		{
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00005AB7 File Offset: 0x00003CB7
		void IDebugManager.WriteDebugLineOnScreen(string message)
		{
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00005AB9 File Offset: 0x00003CB9
		void IDebugManager.RenderDebugLine(Vec3 position, Vec3 direction, uint color, bool depthCheck, float time)
		{
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00005ABB File Offset: 0x00003CBB
		void IDebugManager.RenderDebugSphere(Vec3 position, float radius, uint color, bool depthCheck, float time)
		{
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00005ABD File Offset: 0x00003CBD
		void IDebugManager.RenderDebugFrame(MatrixFrame frame, float lineLength, float time)
		{
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00005ABF File Offset: 0x00003CBF
		void IDebugManager.RenderDebugText(float screenX, float screenY, string text, uint color, float time)
		{
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00005AC1 File Offset: 0x00003CC1
		void IDebugManager.RenderDebugText3D(Vec3 position, string text, uint color, int screenPosOffsetX, int screenPosOffsetY, float time)
		{
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00005AC3 File Offset: 0x00003CC3
		void IDebugManager.RenderDebugRectWithColor(float left, float bottom, float right, float top, uint color)
		{
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00005AC5 File Offset: 0x00003CC5
		Vec3 IDebugManager.GetDebugVector()
		{
			return Vec3.Zero;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00005ACC File Offset: 0x00003CCC
		void IDebugManager.SetTestModeEnabled(bool testModeEnabled)
		{
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00005ACE File Offset: 0x00003CCE
		void IDebugManager.AbortGame()
		{
			Environment.Exit(-5);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00005AD7 File Offset: 0x00003CD7
		void IDebugManager.DoDelayedexit(int returnCode)
		{
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00005AD9 File Offset: 0x00003CD9
		protected void PrintMessage(string message, DiamondDebugManager.DiamondDebugCategory debugCategory)
		{
			Console.Out.Flush();
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = DiamondDebugManager._colors[debugCategory];
			Console.Write(message);
			Console.ResetColor();
			Console.WriteLine();
			Console.Out.Flush();
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00005B15 File Offset: 0x00003D15
		void IDebugManager.ReportMemoryBookmark(string message)
		{
		}

		// Token: 0x0400007F RID: 127
		private static Dictionary<DiamondDebugManager.DiamondDebugCategory, ConsoleColor> _colors = new Dictionary<DiamondDebugManager.DiamondDebugCategory, ConsoleColor>
		{
			{
				DiamondDebugManager.DiamondDebugCategory.General,
				ConsoleColor.Green
			},
			{
				DiamondDebugManager.DiamondDebugCategory.Warning,
				ConsoleColor.Yellow
			},
			{
				DiamondDebugManager.DiamondDebugCategory.Error,
				ConsoleColor.Red
			}
		};

		// Token: 0x020000CA RID: 202
		public enum DiamondDebugCategory
		{
			// Token: 0x0400026E RID: 622
			General,
			// Token: 0x0400026F RID: 623
			Warning,
			// Token: 0x04000270 RID: 624
			Error
		}
	}
}
