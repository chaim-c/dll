using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001AD RID: 429
	[ScriptingInterfaceBase]
	internal interface IMBScreen
	{
		// Token: 0x06001786 RID: 6022
		[EngineMethod("on_exit_button_click", false)]
		void OnExitButtonClick();

		// Token: 0x06001787 RID: 6023
		[EngineMethod("on_edit_mode_enter_press", false)]
		void OnEditModeEnterPress();

		// Token: 0x06001788 RID: 6024
		[EngineMethod("on_edit_mode_enter_release", false)]
		void OnEditModeEnterRelease();
	}
}
