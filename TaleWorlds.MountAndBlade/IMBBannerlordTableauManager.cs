using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001B9 RID: 441
	[ScriptingInterfaceBase]
	internal interface IMBBannerlordTableauManager
	{
		// Token: 0x060017DB RID: 6107
		[EngineMethod("request_character_tableau_render", false)]
		void RequestCharacterTableauRender(int characterCodeId, string path, UIntPtr poseEntity, UIntPtr cameraObject, int tableauType);

		// Token: 0x060017DC RID: 6108
		[EngineMethod("initialize_character_tableau_render_system", false)]
		void InitializeCharacterTableauRenderSystem();

		// Token: 0x060017DD RID: 6109
		[EngineMethod("get_number_of_pending_tableau_requests", false)]
		int GetNumberOfPendingTableauRequests();
	}
}
