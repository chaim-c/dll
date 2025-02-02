using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001B3 RID: 435
	[ScriptingInterfaceBase]
	internal interface IMBWindowManager
	{
		// Token: 0x060017A2 RID: 6050
		[EngineMethod("erase_message_lines", false)]
		void EraseMessageLines();

		// Token: 0x060017A3 RID: 6051
		[EngineMethod("world_to_screen", false)]
		float WorldToScreen(UIntPtr cameraPointer, Vec3 worldSpacePosition, ref float screenX, ref float screenY, ref float w);

		// Token: 0x060017A4 RID: 6052
		[EngineMethod("world_to_screen_with_fixed_z", false)]
		float WorldToScreenWithFixedZ(UIntPtr cameraPointer, Vec3 cameraPosition, Vec3 worldSpacePosition, ref float screenX, ref float screenY, ref float w);

		// Token: 0x060017A5 RID: 6053
		[EngineMethod("dont_change_cursor_pos", false)]
		void DontChangeCursorPos();

		// Token: 0x060017A6 RID: 6054
		[EngineMethod("pre_display", false)]
		void PreDisplay();

		// Token: 0x060017A7 RID: 6055
		[EngineMethod("screen_to_world", false)]
		void ScreenToWorld(UIntPtr pointer, float screenX, float screenY, float z, ref Vec3 worldSpacePosition);
	}
}
