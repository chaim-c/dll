using System;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x0200003C RID: 60
	[ApplicationInterfaceBase]
	internal interface IScreen
	{
		// Token: 0x0600053D RID: 1341
		[EngineMethod("get_real_screen_resolution_width", false)]
		float GetRealScreenResolutionWidth();

		// Token: 0x0600053E RID: 1342
		[EngineMethod("get_real_screen_resolution_height", false)]
		float GetRealScreenResolutionHeight();

		// Token: 0x0600053F RID: 1343
		[EngineMethod("get_desktop_width", false)]
		float GetDesktopWidth();

		// Token: 0x06000540 RID: 1344
		[EngineMethod("get_desktop_height", false)]
		float GetDesktopHeight();

		// Token: 0x06000541 RID: 1345
		[EngineMethod("get_aspect_ratio", false)]
		float GetAspectRatio();

		// Token: 0x06000542 RID: 1346
		[EngineMethod("get_mouse_visible", false)]
		bool GetMouseVisible();

		// Token: 0x06000543 RID: 1347
		[EngineMethod("set_mouse_visible", false)]
		void SetMouseVisible(bool value);

		// Token: 0x06000544 RID: 1348
		[EngineMethod("get_usable_area_percentages", false)]
		Vec2 GetUsableAreaPercentages();

		// Token: 0x06000545 RID: 1349
		[EngineMethod("is_enter_button_cross", false)]
		bool IsEnterButtonCross();
	}
}
