using System;
using TaleWorlds.ScreenSystem;

namespace TaleWorlds.Engine
{
	// Token: 0x0200006A RID: 106
	public static class MouseManager
	{
		// Token: 0x06000877 RID: 2167 RVA: 0x000086DA File Offset: 0x000068DA
		public static void ActivateMouseCursor(CursorType mouseId)
		{
			EngineApplicationInterface.IMouseManager.ActivateMouseCursor((int)mouseId);
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x000086E7 File Offset: 0x000068E7
		public static void SetMouseCursor(CursorType mouseId, string mousePath)
		{
			EngineApplicationInterface.IMouseManager.SetMouseCursor((int)mouseId, mousePath);
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x000086F5 File Offset: 0x000068F5
		public static void ShowCursor(bool show)
		{
			EngineApplicationInterface.IMouseManager.ShowCursor(show);
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00008702 File Offset: 0x00006902
		public static void LockCursorAtCurrentPosition(bool lockCursor)
		{
			EngineApplicationInterface.IMouseManager.LockCursorAtCurrentPosition(lockCursor);
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0000870F File Offset: 0x0000690F
		public static void LockCursorAtPosition(float x, float y)
		{
			EngineApplicationInterface.IMouseManager.LockCursorAtPosition(x, y);
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x0000871D File Offset: 0x0000691D
		public static void UnlockCursor()
		{
			EngineApplicationInterface.IMouseManager.UnlockCursor();
		}
	}
}
