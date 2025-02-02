using System;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001C0 RID: 448
	public class MBDebugManager : IDebugManager
	{
		// Token: 0x060019BA RID: 6586 RVA: 0x0005B938 File Offset: 0x00059B38
		void IDebugManager.SetCrashReportCustomString(string customString)
		{
			Utilities.SetCrashReportCustomString(customString);
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x0005B940 File Offset: 0x00059B40
		void IDebugManager.SetCrashReportCustomStack(string customStack)
		{
			Utilities.SetCrashReportCustomStack(customStack);
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x0005B948 File Offset: 0x00059B48
		void IDebugManager.ShowWarning(string message)
		{
			MBDebug.ShowWarning(message);
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x0005B950 File Offset: 0x00059B50
		void IDebugManager.ShowError(string message)
		{
			MBDebug.ShowError(message);
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x0005B958 File Offset: 0x00059B58
		void IDebugManager.ShowMessageBox(string lpText, string lpCaption, uint uType)
		{
			MBDebug.ShowMessageBox(lpText, lpCaption, uType);
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x0005B962 File Offset: 0x00059B62
		void IDebugManager.Assert(bool condition, string message, string callerFile, string callerMethod, int callerLine)
		{
		}

		// Token: 0x060019C0 RID: 6592 RVA: 0x0005B964 File Offset: 0x00059B64
		void IDebugManager.SilentAssert(bool condition, string message, bool getDump, string callerFile, string callerMethod, int callerLine)
		{
			MBDebug.SilentAssert(condition, message, getDump, callerFile, callerMethod, callerLine);
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x0005B974 File Offset: 0x00059B74
		void IDebugManager.Print(string message, int logLevel, Debug.DebugColor color, ulong debugFilter)
		{
			MBDebug.Print(message, logLevel, color, debugFilter);
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x0005B980 File Offset: 0x00059B80
		void IDebugManager.PrintError(string error, string stackTrace, ulong debugFilter)
		{
			MBDebug.Print(error, 0, Debug.DebugColor.White, debugFilter);
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x0005B98C File Offset: 0x00059B8C
		void IDebugManager.PrintWarning(string warning, ulong debugFilter)
		{
			MBDebug.Print(warning, 0, Debug.DebugColor.White, debugFilter);
		}

		// Token: 0x060019C4 RID: 6596 RVA: 0x0005B998 File Offset: 0x00059B98
		void IDebugManager.DisplayDebugMessage(string message)
		{
		}

		// Token: 0x060019C5 RID: 6597 RVA: 0x0005B99A File Offset: 0x00059B9A
		void IDebugManager.WatchVariable(string name, object value)
		{
		}

		// Token: 0x060019C6 RID: 6598 RVA: 0x0005B99C File Offset: 0x00059B9C
		void IDebugManager.WriteDebugLineOnScreen(string message)
		{
		}

		// Token: 0x060019C7 RID: 6599 RVA: 0x0005B99E File Offset: 0x00059B9E
		void IDebugManager.RenderDebugLine(Vec3 position, Vec3 direction, uint color, bool depthCheck, float time)
		{
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x0005B9A0 File Offset: 0x00059BA0
		void IDebugManager.RenderDebugSphere(Vec3 position, float radius, uint color, bool depthCheck, float time)
		{
		}

		// Token: 0x060019C9 RID: 6601 RVA: 0x0005B9A2 File Offset: 0x00059BA2
		void IDebugManager.RenderDebugFrame(MatrixFrame frame, float lineLength, float time)
		{
		}

		// Token: 0x060019CA RID: 6602 RVA: 0x0005B9A4 File Offset: 0x00059BA4
		void IDebugManager.RenderDebugText(float screenX, float screenY, string text, uint color, float time)
		{
		}

		// Token: 0x060019CB RID: 6603 RVA: 0x0005B9A6 File Offset: 0x00059BA6
		void IDebugManager.RenderDebugText3D(Vec3 position, string text, uint color, int screenPosOffsetX, int screenPosOffsetY, float time)
		{
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x0005B9A8 File Offset: 0x00059BA8
		void IDebugManager.RenderDebugRectWithColor(float left, float bottom, float right, float top, uint color)
		{
		}

		// Token: 0x060019CD RID: 6605 RVA: 0x0005B9AA File Offset: 0x00059BAA
		Vec3 IDebugManager.GetDebugVector()
		{
			return MBDebug.DebugVector;
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x0005B9B1 File Offset: 0x00059BB1
		void IDebugManager.SetTestModeEnabled(bool testModeEnabled)
		{
			MBDebug.TestModeEnabled = testModeEnabled;
		}

		// Token: 0x060019CF RID: 6607 RVA: 0x0005B9B9 File Offset: 0x00059BB9
		void IDebugManager.AbortGame()
		{
			MBDebug.AbortGame(5);
		}

		// Token: 0x060019D0 RID: 6608 RVA: 0x0005B9C1 File Offset: 0x00059BC1
		void IDebugManager.DoDelayedexit(int returnCode)
		{
			Utilities.DoDelayedexit(returnCode);
		}

		// Token: 0x060019D1 RID: 6609 RVA: 0x0005B9C9 File Offset: 0x00059BC9
		void IDebugManager.ReportMemoryBookmark(string message)
		{
		}
	}
}
