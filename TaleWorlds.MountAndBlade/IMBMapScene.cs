using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001B6 RID: 438
	[ScriptingInterfaceBase]
	internal interface IMBMapScene
	{
		// Token: 0x060017C0 RID: 6080
		[EngineMethod("get_accessible_point_near_position", false)]
		Vec3 GetAccessiblePointNearPosition(UIntPtr scenePointer, Vec2 position, float radius);

		// Token: 0x060017C1 RID: 6081
		[EngineMethod("remove_zero_corner_bodies", false)]
		void RemoveZeroCornerBodies(UIntPtr scenePointer);

		// Token: 0x060017C2 RID: 6082
		[EngineMethod("load_atmosphere_data", false)]
		void LoadAtmosphereData(UIntPtr scenePointer);

		// Token: 0x060017C3 RID: 6083
		[EngineMethod("get_face_index_for_multiple_positions", false)]
		void GetFaceIndexForMultiplePositions(UIntPtr scenePointer, int movedPartyCount, float[] positionArray, PathFaceRecord[] resultArray, bool check_if_disabled, bool check_height);

		// Token: 0x060017C4 RID: 6084
		[EngineMethod("tick_step_sound", false)]
		void TickStepSound(UIntPtr scenePointer, UIntPtr visualsPointer, int faceIndexterrainType, int soundType);

		// Token: 0x060017C5 RID: 6085
		[EngineMethod("tick_ambient_sounds", false)]
		void TickAmbientSounds(UIntPtr scenePointer, int terrainType);

		// Token: 0x060017C6 RID: 6086
		[EngineMethod("tick_visuals", false)]
		void TickVisuals(UIntPtr scenePointer, float tod, UIntPtr[] ticked_map_meshes, int tickedMapMeshesCount);

		// Token: 0x060017C7 RID: 6087
		[EngineMethod("validate_terrain_sound_ids", false)]
		void ValidateTerrainSoundIds();

		// Token: 0x060017C8 RID: 6088
		[EngineMethod("set_political_color", false)]
		void SetPoliticalColor(UIntPtr scenePointer, string value);

		// Token: 0x060017C9 RID: 6089
		[EngineMethod("set_frame_for_atmosphere", false)]
		void SetFrameForAtmosphere(UIntPtr scenePointer, float tod, float cameraElevation, bool forceLoadTextures);

		// Token: 0x060017CA RID: 6090
		[EngineMethod("get_color_grade_grid_data", false)]
		void GetColorGradeGridData(UIntPtr scenePointer, byte[] snowData, string textureName);

		// Token: 0x060017CB RID: 6091
		[EngineMethod("get_battle_scene_index_map_resolution", false)]
		void GetBattleSceneIndexMapResolution(UIntPtr scenePointer, ref int width, ref int height);

		// Token: 0x060017CC RID: 6092
		[EngineMethod("get_battle_scene_index_map", false)]
		void GetBattleSceneIndexMap(UIntPtr scenePointer, byte[] indexData);

		// Token: 0x060017CD RID: 6093
		[EngineMethod("set_terrain_dynamic_params", false)]
		void SetTerrainDynamicParams(UIntPtr scenePointer, Vec3 dynamic_params);

		// Token: 0x060017CE RID: 6094
		[EngineMethod("set_season_time_factor", false)]
		void SetSeasonTimeFactor(UIntPtr scenePointer, float seasonTimeFactor);

		// Token: 0x060017CF RID: 6095
		[EngineMethod("get_season_time_factor", false)]
		float GetSeasonTimeFactor(UIntPtr scenePointer);

		// Token: 0x060017D0 RID: 6096
		[EngineMethod("get_mouse_visible", false)]
		bool GetMouseVisible();

		// Token: 0x060017D1 RID: 6097
		[EngineMethod("send_mouse_key_down_event", false)]
		void SendMouseKeyEvent(int keyId, bool isDown);

		// Token: 0x060017D2 RID: 6098
		[EngineMethod("set_mouse_visible", false)]
		void SetMouseVisible(bool value);

		// Token: 0x060017D3 RID: 6099
		[EngineMethod("set_mouse_pos", false)]
		void SetMousePos(int posX, int posY);
	}
}
