using System;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000030 RID: 48
	[ApplicationInterfaceBase]
	internal interface ITableauView
	{
		// Token: 0x06000448 RID: 1096
		[EngineMethod("create_tableau_view", false)]
		TableauView CreateTableauView();

		// Token: 0x06000449 RID: 1097
		[EngineMethod("set_sort_meshes", false)]
		void SetSortingEnabled(UIntPtr pointer, bool value);

		// Token: 0x0600044A RID: 1098
		[EngineMethod("set_continous_rendering", false)]
		void SetContinousRendering(UIntPtr pointer, bool value);

		// Token: 0x0600044B RID: 1099
		[EngineMethod("set_do_not_render_this_frame", false)]
		void SetDoNotRenderThisFrame(UIntPtr pointer, bool value);

		// Token: 0x0600044C RID: 1100
		[EngineMethod("set_delete_after_rendering", false)]
		void SetDeleteAfterRendering(UIntPtr pointer, bool value);
	}
}
