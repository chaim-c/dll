using System;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001A6 RID: 422
	[ScriptingInterfaceBase]
	internal interface IMBEditor
	{
		// Token: 0x060016B3 RID: 5811
		[EngineMethod("is_edit_mode", false)]
		bool IsEditMode();

		// Token: 0x060016B4 RID: 5812
		[EngineMethod("is_edit_mode_enabled", false)]
		bool IsEditModeEnabled();

		// Token: 0x060016B5 RID: 5813
		[EngineMethod("update_scene_tree", false)]
		void UpdateSceneTree();

		// Token: 0x060016B6 RID: 5814
		[EngineMethod("is_entity_selected", false)]
		bool IsEntitySelected(UIntPtr entityId);

		// Token: 0x060016B7 RID: 5815
		[EngineMethod("add_editor_warning", false)]
		void AddEditorWarning(string msg);

		// Token: 0x060016B8 RID: 5816
		[EngineMethod("render_editor_mesh", false)]
		void RenderEditorMesh(UIntPtr metaMeshId, ref MatrixFrame frame);

		// Token: 0x060016B9 RID: 5817
		[EngineMethod("enter_edit_mode", false)]
		void EnterEditMode(UIntPtr sceneWidgetPointer, ref MatrixFrame initialCameraFrame, float initialCameraElevation, float initialCameraBearing);

		// Token: 0x060016BA RID: 5818
		[EngineMethod("tick_edit_mode", false)]
		void TickEditMode(float dt);

		// Token: 0x060016BB RID: 5819
		[EngineMethod("leave_edit_mode", false)]
		void LeaveEditMode();

		// Token: 0x060016BC RID: 5820
		[EngineMethod("enter_edit_mission_mode", false)]
		void EnterEditMissionMode(UIntPtr missionPointer);

		// Token: 0x060016BD RID: 5821
		[EngineMethod("leave_edit_mission_mode", false)]
		void LeaveEditMissionMode();

		// Token: 0x060016BE RID: 5822
		[EngineMethod("activate_scene_editor_presentation", false)]
		void ActivateSceneEditorPresentation();

		// Token: 0x060016BF RID: 5823
		[EngineMethod("deactivate_scene_editor_presentation", false)]
		void DeactivateSceneEditorPresentation();

		// Token: 0x060016C0 RID: 5824
		[EngineMethod("tick_scene_editor_presentation", false)]
		void TickSceneEditorPresentation(float dt);

		// Token: 0x060016C1 RID: 5825
		[EngineMethod("get_editor_scene_view", false)]
		SceneView GetEditorSceneView();

		// Token: 0x060016C2 RID: 5826
		[EngineMethod("helpers_enabled", false)]
		bool HelpersEnabled();

		// Token: 0x060016C3 RID: 5827
		[EngineMethod("border_helpers_enabled", false)]
		bool BorderHelpersEnabled();

		// Token: 0x060016C4 RID: 5828
		[EngineMethod("zoom_to_position", false)]
		void ZoomToPosition(Vec3 pos);

		// Token: 0x060016C5 RID: 5829
		[EngineMethod("add_entity_warning", false)]
		void AddEntityWarning(UIntPtr entityId, string msg);

		// Token: 0x060016C6 RID: 5830
		[EngineMethod("get_all_prefabs_and_child_with_tag", false)]
		string GetAllPrefabsAndChildWithTag(string tag);

		// Token: 0x060016C7 RID: 5831
		[EngineMethod("set_upgrade_level_visibility", false)]
		void SetUpgradeLevelVisibility(string cumulated_string);

		// Token: 0x060016C8 RID: 5832
		[EngineMethod("set_level_visibility", false)]
		void SetLevelVisibility(string cumulated_string);

		// Token: 0x060016C9 RID: 5833
		[EngineMethod("exit_edit_mode", false)]
		void ExitEditMode();

		// Token: 0x060016CA RID: 5834
		[EngineMethod("is_replay_manager_recording", false)]
		bool IsReplayManagerRecording();

		// Token: 0x060016CB RID: 5835
		[EngineMethod("is_replay_manager_rendering", false)]
		bool IsReplayManagerRendering();

		// Token: 0x060016CC RID: 5836
		[EngineMethod("is_replay_manager_replaying", false)]
		bool IsReplayManagerReplaying();
	}
}
