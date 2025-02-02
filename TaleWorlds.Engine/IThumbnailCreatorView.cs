using System;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000033 RID: 51
	[ApplicationInterfaceBase]
	internal interface IThumbnailCreatorView
	{
		// Token: 0x06000454 RID: 1108
		[EngineMethod("create_thumbnail_creator_view", false)]
		ThumbnailCreatorView CreateThumbnailCreatorView();

		// Token: 0x06000455 RID: 1109
		[EngineMethod("register_scene", false)]
		void RegisterScene(UIntPtr pointer, UIntPtr scene_ptr, bool use_postfx);

		// Token: 0x06000456 RID: 1110
		[EngineMethod("clear_requests", false)]
		void ClearRequests(UIntPtr pointer);

		// Token: 0x06000457 RID: 1111
		[EngineMethod("cancel_request", false)]
		void CancelRequest(UIntPtr pointer, string render_id);

		// Token: 0x06000458 RID: 1112
		[EngineMethod("register_entity", false)]
		void RegisterEntity(UIntPtr pointer, UIntPtr scene_ptr, UIntPtr cam_ptr, UIntPtr texture_ptr, UIntPtr entity_ptr, string render_id, int allocationGroupIndex);

		// Token: 0x06000459 RID: 1113
		[EngineMethod("register_entity_without_texture", false)]
		void RegisterEntityWithoutTexture(UIntPtr pointer, UIntPtr scene_ptr, UIntPtr cam_ptr, UIntPtr entity_ptr, int width, int height, string render_id, string debug_name, int allocationGroupIndex);

		// Token: 0x0600045A RID: 1114
		[EngineMethod("get_number_of_pending_requests", false)]
		int GetNumberOfPendingRequests(UIntPtr pointer);

		// Token: 0x0600045B RID: 1115
		[EngineMethod("is_memory_cleared", false)]
		bool IsMemoryCleared(UIntPtr pointer);
	}
}
