using System;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000036 RID: 54
	[ApplicationInterfaceBase]
	internal interface IMouseManager
	{
		// Token: 0x06000484 RID: 1156
		[EngineMethod("activate_mouse_cursor", false)]
		void ActivateMouseCursor(int id);

		// Token: 0x06000485 RID: 1157
		[EngineMethod("set_mouse_cursor", false)]
		void SetMouseCursor(int id, string mousePath);

		// Token: 0x06000486 RID: 1158
		[EngineMethod("show_cursor", false)]
		void ShowCursor(bool show);

		// Token: 0x06000487 RID: 1159
		[EngineMethod("lock_cursor_at_current_pos", false)]
		void LockCursorAtCurrentPosition(bool lockCursor);

		// Token: 0x06000488 RID: 1160
		[EngineMethod("lock_cursor_at_position", false)]
		void LockCursorAtPosition(float x, float y);

		// Token: 0x06000489 RID: 1161
		[EngineMethod("unlock_cursor", false)]
		void UnlockCursor();
	}
}
