using System;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x0200002F RID: 47
	[ApplicationInterfaceBase]
	internal interface IView
	{
		// Token: 0x06000439 RID: 1081
		[EngineMethod("set_render_option", false)]
		void SetRenderOption(UIntPtr ptr, int optionEnum, bool value);

		// Token: 0x0600043A RID: 1082
		[EngineMethod("set_render_order", false)]
		void SetRenderOrder(UIntPtr ptr, int value);

		// Token: 0x0600043B RID: 1083
		[EngineMethod("set_render_target", false)]
		void SetRenderTarget(UIntPtr ptr, UIntPtr texture_ptr);

		// Token: 0x0600043C RID: 1084
		[EngineMethod("set_depth_target", false)]
		void SetDepthTarget(UIntPtr ptr, UIntPtr texture_ptr);

		// Token: 0x0600043D RID: 1085
		[EngineMethod("set_scale", false)]
		void SetScale(UIntPtr ptr, float x, float y);

		// Token: 0x0600043E RID: 1086
		[EngineMethod("set_offset", false)]
		void SetOffset(UIntPtr ptr, float x, float y);

		// Token: 0x0600043F RID: 1087
		[EngineMethod("set_debug_render_functionality", false)]
		void SetDebugRenderFunctionality(UIntPtr ptr, bool value);

		// Token: 0x06000440 RID: 1088
		[EngineMethod("set_clear_color", false)]
		void SetClearColor(UIntPtr ptr, uint rgba);

		// Token: 0x06000441 RID: 1089
		[EngineMethod("set_enable", false)]
		void SetEnable(UIntPtr ptr, bool value);

		// Token: 0x06000442 RID: 1090
		[EngineMethod("set_render_on_demand", false)]
		void SetRenderOnDemand(UIntPtr ptr, bool value);

		// Token: 0x06000443 RID: 1091
		[EngineMethod("set_auto_depth_creation", false)]
		void SetAutoDepthTargetCreation(UIntPtr ptr, bool value);

		// Token: 0x06000444 RID: 1092
		[EngineMethod("set_save_final_result_to_disk", false)]
		void SetSaveFinalResultToDisk(UIntPtr ptr, bool value);

		// Token: 0x06000445 RID: 1093
		[EngineMethod("set_file_name_to_save_result", false)]
		void SetFileNameToSaveResult(UIntPtr ptr, string name);

		// Token: 0x06000446 RID: 1094
		[EngineMethod("set_file_type_to_save", false)]
		void SetFileTypeToSave(UIntPtr ptr, int type);

		// Token: 0x06000447 RID: 1095
		[EngineMethod("set_file_path_to_save_result", false)]
		void SetFilePathToSaveResult(UIntPtr ptr, string name);
	}
}
