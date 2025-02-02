using System;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x0200002E RID: 46
	[ApplicationInterfaceBase]
	internal interface ISceneView
	{
		// Token: 0x0600041C RID: 1052
		[EngineMethod("create_scene_view", false)]
		SceneView CreateSceneView();

		// Token: 0x0600041D RID: 1053
		[EngineMethod("set_scene", false)]
		void SetScene(UIntPtr ptr, UIntPtr scenePtr);

		// Token: 0x0600041E RID: 1054
		[EngineMethod("set_accept_global_debug_render_objects", false)]
		void SetAcceptGlobalDebugRenderObjects(UIntPtr ptr, bool value);

		// Token: 0x0600041F RID: 1055
		[EngineMethod("set_render_with_postfx", false)]
		void SetRenderWithPostfx(UIntPtr ptr, bool value);

		// Token: 0x06000420 RID: 1056
		[EngineMethod("set_force_shader_compilation", false)]
		void SetForceShaderCompilation(UIntPtr ptr, bool value);

		// Token: 0x06000421 RID: 1057
		[EngineMethod("check_scene_ready_to_render", false)]
		bool CheckSceneReadyToRender(UIntPtr ptr);

		// Token: 0x06000422 RID: 1058
		[EngineMethod("set_postfx_config_params", false)]
		void SetPostfxConfigParams(UIntPtr ptr, int value);

		// Token: 0x06000423 RID: 1059
		[EngineMethod("set_camera", false)]
		void SetCamera(UIntPtr ptr, UIntPtr cameraPtr);

		// Token: 0x06000424 RID: 1060
		[EngineMethod("set_resolution_scaling", false)]
		void SetResolutionScaling(UIntPtr ptr, bool value);

		// Token: 0x06000425 RID: 1061
		[EngineMethod("set_postfx_from_config", false)]
		void SetPostfxFromConfig(UIntPtr ptr);

		// Token: 0x06000426 RID: 1062
		[EngineMethod("world_point_to_screen_point", false)]
		Vec2 WorldPointToScreenPoint(UIntPtr ptr, Vec3 position);

		// Token: 0x06000427 RID: 1063
		[EngineMethod("screen_point_to_viewport_point", false)]
		Vec2 ScreenPointToViewportPoint(UIntPtr ptr, float position_x, float position_y);

		// Token: 0x06000428 RID: 1064
		[EngineMethod("projected_mouse_position_on_ground", false)]
		bool ProjectedMousePositionOnGround(UIntPtr pointer, out Vec3 groundPosition, out Vec3 groundNormal, bool mouseVisible, BodyFlags excludeBodyOwnerFlags, bool checkOccludedSurface);

		// Token: 0x06000429 RID: 1065
		[EngineMethod("translate_mouse", false)]
		void TranslateMouse(UIntPtr pointer, ref Vec3 worldMouseNear, ref Vec3 worldMouseFar, float maxDistance);

		// Token: 0x0600042A RID: 1066
		[EngineMethod("set_scene_uses_skybox", false)]
		void SetSceneUsesSkybox(UIntPtr pointer, bool value);

		// Token: 0x0600042B RID: 1067
		[EngineMethod("set_scene_uses_shadows", false)]
		void SetSceneUsesShadows(UIntPtr pointer, bool value);

		// Token: 0x0600042C RID: 1068
		[EngineMethod("set_scene_uses_contour", false)]
		void SetSceneUsesContour(UIntPtr pointer, bool value);

		// Token: 0x0600042D RID: 1069
		[EngineMethod("do_not_clear", false)]
		void DoNotClear(UIntPtr pointer, bool value);

		// Token: 0x0600042E RID: 1070
		[EngineMethod("add_clear_task", false)]
		void AddClearTask(UIntPtr ptr, bool clearOnlySceneview);

		// Token: 0x0600042F RID: 1071
		[EngineMethod("ready_to_render", false)]
		bool ReadyToRender(UIntPtr pointer);

		// Token: 0x06000430 RID: 1072
		[EngineMethod("set_clear_and_disable_after_succesfull_render", false)]
		void SetClearAndDisableAfterSucessfullRender(UIntPtr pointer, bool value);

		// Token: 0x06000431 RID: 1073
		[EngineMethod("set_clear_gbuffer", false)]
		void SetClearGbuffer(UIntPtr pointer, bool value);

		// Token: 0x06000432 RID: 1074
		[EngineMethod("set_shadowmap_resolution_multiplier", false)]
		void SetShadowmapResolutionMultiplier(UIntPtr pointer, float value);

		// Token: 0x06000433 RID: 1075
		[EngineMethod("set_pointlight_resolution_multiplier", false)]
		void SetPointlightResolutionMultiplier(UIntPtr pointer, float value);

		// Token: 0x06000434 RID: 1076
		[EngineMethod("set_clean_screen_until_loading_done", false)]
		void SetCleanScreenUntilLoadingDone(UIntPtr pointer, bool value);

		// Token: 0x06000435 RID: 1077
		[EngineMethod("clear_all", false)]
		void ClearAll(UIntPtr pointer, bool clear_scene, bool remove_terrain);

		// Token: 0x06000436 RID: 1078
		[EngineMethod("set_focused_shadowmap", false)]
		void SetFocusedShadowmap(UIntPtr ptr, bool enable, ref Vec3 center, float radius);

		// Token: 0x06000437 RID: 1079
		[EngineMethod("get_scene", false)]
		Scene GetScene(UIntPtr ptr);

		// Token: 0x06000438 RID: 1080
		[EngineMethod("ray_cast_for_closest_entity_or_terrain", false)]
		bool RayCastForClosestEntityOrTerrain(UIntPtr ptr, ref Vec3 sourcePoint, ref Vec3 targetPoint, float rayThickness, ref float collisionDistance, ref Vec3 closestPoint, ref UIntPtr entityIndex, BodyFlags bodyExcludeFlags);
	}
}
