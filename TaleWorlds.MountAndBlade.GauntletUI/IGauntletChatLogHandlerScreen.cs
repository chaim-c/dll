using System;
using TaleWorlds.InputSystem;

namespace TaleWorlds.MountAndBlade.GauntletUI
{
	// Token: 0x02000012 RID: 18
	public interface IGauntletChatLogHandlerScreen
	{
		// Token: 0x06000090 RID: 144
		void TryUpdateChatLogLayerParameters(ref bool isTeamChatAvailable, ref bool inputEnabled, ref InputContext inputContext);
	}
}
