using System;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001B2 RID: 434
	[ScriptingInterfaceBase]
	internal interface IMBMessageManager
	{
		// Token: 0x0600179F RID: 6047
		[EngineMethod("display_message", false)]
		void DisplayMessage(string message);

		// Token: 0x060017A0 RID: 6048
		[EngineMethod("display_message_with_color", false)]
		void DisplayMessageWithColor(string message, uint color);

		// Token: 0x060017A1 RID: 6049
		[EngineMethod("set_message_manager", false)]
		void SetMessageManager(MessageManagerBase messageManager);
	}
}
