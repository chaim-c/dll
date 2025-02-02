using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001A1 RID: 417
	[ScriptingInterfaceBase]
	internal interface IMBAgentVisuals
	{
		// Token: 0x0600157B RID: 5499
		[EngineMethod("validate_agent_visuals_reseted", false)]
		void ValidateAgentVisualsReseted(UIntPtr scenePointer, UIntPtr agentRendererSceneControllerPointer);

		// Token: 0x0600157C RID: 5500
		[EngineMethod("create_agent_renderer_scene_controller", false)]
		UIntPtr CreateAgentRendererSceneController(UIntPtr scenePointer, int maxRenderCount);

		// Token: 0x0600157D RID: 5501
		[EngineMethod("destruct_agent_renderer_scene_controller", false)]
		void DestructAgentRendererSceneController(UIntPtr scenePointer, UIntPtr agentRendererSceneControllerPointer, bool deleteThisFrame);

		// Token: 0x0600157E RID: 5502
		[EngineMethod("set_do_timer_based_skeleton_forced_updates", false)]
		void SetDoTimerBasedForcedSkeletonUpdates(UIntPtr agentRendererSceneControllerPointer, bool value);

		// Token: 0x0600157F RID: 5503
		[EngineMethod("set_enforced_visibility_for_all_agents", false)]
		void SetEnforcedVisibilityForAllAgents(UIntPtr scenePointer, UIntPtr agentRendererSceneControllerPointer);

		// Token: 0x06001580 RID: 5504
		[EngineMethod("create_agent_visuals", false)]
		MBAgentVisuals CreateAgentVisuals(UIntPtr scenePtr, string ownerName, Vec3 eyeOffset);

		// Token: 0x06001581 RID: 5505
		[EngineMethod("tick", false)]
		void Tick(UIntPtr agentVisualsId, UIntPtr parentAgentVisualsId, float dt, bool entityMoving, float speed);

		// Token: 0x06001582 RID: 5506
		[EngineMethod("set_entity", false)]
		void SetEntity(UIntPtr agentVisualsId, UIntPtr entityPtr);

		// Token: 0x06001583 RID: 5507
		[EngineMethod("set_skeleton", false)]
		void SetSkeleton(UIntPtr agentVisualsId, UIntPtr skeletonPtr);

		// Token: 0x06001584 RID: 5508
		[EngineMethod("fill_entity_with_body_meshes_without_agent_visuals", false)]
		void FillEntityWithBodyMeshesWithoutAgentVisuals(UIntPtr entityPoinbter, ref SkinGenerationParams skinParams, ref BodyProperties bodyProperties, MetaMesh glovesMesh);

		// Token: 0x06001585 RID: 5509
		[EngineMethod("add_skin_meshes_to_agent_visuals", false)]
		void AddSkinMeshesToAgentEntity(UIntPtr agentVisualsId, ref SkinGenerationParams skinParams, ref BodyProperties bodyProperties, bool useGPUMorph, bool useFaceCache);

		// Token: 0x06001586 RID: 5510
		[EngineMethod("set_lod_atlas_shading_index", false)]
		void SetLodAtlasShadingIndex(UIntPtr agentVisualsId, int index, bool useTeamColor, uint teamColor1, uint teamColor2);

		// Token: 0x06001587 RID: 5511
		[EngineMethod("set_face_generation_params", false)]
		void SetFaceGenerationParams(UIntPtr agentVisualsId, FaceGenerationParams faceGenerationParams);

		// Token: 0x06001588 RID: 5512
		[EngineMethod("start_rhubarb_record", false)]
		void StartRhubarbRecord(UIntPtr agentVisualsId, string path, int soundId);

		// Token: 0x06001589 RID: 5513
		[EngineMethod("clear_visual_components", false)]
		void ClearVisualComponents(UIntPtr agentVisualsId, bool removeSkeleton);

		// Token: 0x0600158A RID: 5514
		[EngineMethod("lazy_update_agent_renderer_data", false)]
		void LazyUpdateAgentRendererData(UIntPtr agentVisualsId);

		// Token: 0x0600158B RID: 5515
		[EngineMethod("add_mesh", false)]
		void AddMesh(UIntPtr agentVisualsId, UIntPtr meshPointer);

		// Token: 0x0600158C RID: 5516
		[EngineMethod("remove_mesh", false)]
		void RemoveMesh(UIntPtr agentVisualsPtr, UIntPtr meshPointer);

		// Token: 0x0600158D RID: 5517
		[EngineMethod("add_multi_mesh", false)]
		void AddMultiMesh(UIntPtr agentVisualsPtr, UIntPtr multiMeshPointer, int bodyMeshIndex);

		// Token: 0x0600158E RID: 5518
		[EngineMethod("add_horse_reins_cloth_mesh", false)]
		void AddHorseReinsClothMesh(UIntPtr agentVisualsPtr, UIntPtr reinMeshPointer, UIntPtr ropeMeshPointer);

		// Token: 0x0600158F RID: 5519
		[EngineMethod("update_skeleton_scale", false)]
		void UpdateSkeletonScale(UIntPtr agentVisualsId, int bodyDeformType);

		// Token: 0x06001590 RID: 5520
		[EngineMethod("apply_skeleton_scale", false)]
		void ApplySkeletonScale(UIntPtr agentVisualsId, Vec3 mountSitBoneScale, float mountRadiusAdder, byte boneCount, sbyte[] boneIndices, Vec3[] boneScales);

		// Token: 0x06001591 RID: 5521
		[EngineMethod("batch_last_lod_meshes", false)]
		void BatchLastLodMeshes(UIntPtr agentVisualsPtr);

		// Token: 0x06001592 RID: 5522
		[EngineMethod("remove_multi_mesh", false)]
		void RemoveMultiMesh(UIntPtr agentVisualsPtr, UIntPtr multiMeshPointer, int bodyMeshIndex);

		// Token: 0x06001593 RID: 5523
		[EngineMethod("add_weapon_to_agent_entity", false)]
		void AddWeaponToAgentEntity(UIntPtr agentVisualsPtr, int slotIndex, in WeaponData agentEntityData, WeaponStatsData[] weaponStatsData, int weaponStatsDataLength, in WeaponData agentEntityAmmoData, WeaponStatsData[] ammoWeaponStatsData, int ammoWeaponStatsDataLength, GameEntity cachedEntity);

		// Token: 0x06001594 RID: 5524
		[EngineMethod("update_quiver_mesh_of_weapon_in_slot", false)]
		void UpdateQuiverMeshesWithoutAgent(UIntPtr agentVisualsId, int weaponIndex, int ammoCountToShow);

		// Token: 0x06001595 RID: 5525
		[EngineMethod("set_wielded_weapon_indices", false)]
		void SetWieldedWeaponIndices(UIntPtr agentVisualsId, int slotIndexRightHand, int slotIndexLeftHand);

		// Token: 0x06001596 RID: 5526
		[EngineMethod("clear_all_weapon_meshes", false)]
		void ClearAllWeaponMeshes(UIntPtr agentVisualsPtr);

		// Token: 0x06001597 RID: 5527
		[EngineMethod("clear_weapon_meshes", false)]
		void ClearWeaponMeshes(UIntPtr agentVisualsPtr, int weaponVisualIndex);

		// Token: 0x06001598 RID: 5528
		[EngineMethod("make_voice", false)]
		void MakeVoice(UIntPtr agentVisualsPtr, int voiceId, ref Vec3 position);

		// Token: 0x06001599 RID: 5529
		[EngineMethod("set_setup_morph_node", false)]
		void SetSetupMorphNode(UIntPtr agentVisualsPtr, bool value);

		// Token: 0x0600159A RID: 5530
		[EngineMethod("use_scaled_weapons", false)]
		void UseScaledWeapons(UIntPtr agentVisualsPtr, bool value);

		// Token: 0x0600159B RID: 5531
		[EngineMethod("set_cloth_component_keep_state_of_all_meshes", false)]
		void SetClothComponentKeepStateOfAllMeshes(UIntPtr agentVisualsPtr, bool keepState);

		// Token: 0x0600159C RID: 5532
		[EngineMethod("get_current_helmet_scaling_factor", false)]
		Vec3 GetCurrentHelmetScalingFactor(UIntPtr agentVisualsPtr);

		// Token: 0x0600159D RID: 5533
		[EngineMethod("set_voice_definition_index", false)]
		void SetVoiceDefinitionIndex(UIntPtr agentVisualsPtr, int voiceDefinitionIndex, float voicePitch);

		// Token: 0x0600159E RID: 5534
		[EngineMethod("set_agent_lod_make_zero_or_max", false)]
		void SetAgentLodMakeZeroOrMax(UIntPtr agentVisualsPtr, bool makeZero);

		// Token: 0x0600159F RID: 5535
		[EngineMethod("set_agent_local_speed", false)]
		void SetAgentLocalSpeed(UIntPtr agentVisualsPtr, Vec2 speed);

		// Token: 0x060015A0 RID: 5536
		[EngineMethod("set_look_direction", false)]
		void SetLookDirection(UIntPtr agentVisualsPtr, Vec3 direction);

		// Token: 0x060015A1 RID: 5537
		[EngineMethod("reset", false)]
		void Reset(UIntPtr agentVisualsPtr);

		// Token: 0x060015A2 RID: 5538
		[EngineMethod("reset_next_frame", false)]
		void ResetNextFrame(UIntPtr agentVisualsPtr);

		// Token: 0x060015A3 RID: 5539
		[EngineMethod("set_frame", false)]
		void SetFrame(UIntPtr agentVisualsPtr, ref MatrixFrame frame);

		// Token: 0x060015A4 RID: 5540
		[EngineMethod("get_frame", false)]
		void GetFrame(UIntPtr agentVisualsPtr, ref MatrixFrame outFrame);

		// Token: 0x060015A5 RID: 5541
		[EngineMethod("get_global_frame", false)]
		void GetGlobalFrame(UIntPtr agentVisualsPtr, ref MatrixFrame outFrame);

		// Token: 0x060015A6 RID: 5542
		[EngineMethod("set_visible", false)]
		void SetVisible(UIntPtr agentVisualsPtr, bool value);

		// Token: 0x060015A7 RID: 5543
		[EngineMethod("get_visible", false)]
		bool GetVisible(UIntPtr agentVisualsPtr);

		// Token: 0x060015A8 RID: 5544
		[EngineMethod("get_skeleton", false)]
		Skeleton GetSkeleton(UIntPtr agentVisualsPtr);

		// Token: 0x060015A9 RID: 5545
		[EngineMethod("get_entity", false)]
		GameEntity GetEntity(UIntPtr agentVisualsPtr);

		// Token: 0x060015AA RID: 5546
		[EngineMethod("is_valid", false)]
		bool IsValid(UIntPtr agentVisualsPtr);

		// Token: 0x060015AB RID: 5547
		[EngineMethod("get_global_stable_eye_point", false)]
		Vec3 GetGlobalStableEyePoint(UIntPtr agentVisualsPtr, bool isHumanoid);

		// Token: 0x060015AC RID: 5548
		[EngineMethod("get_global_stable_neck_point", false)]
		Vec3 GetGlobalStableNeckPoint(UIntPtr agentVisualsPtr, bool isHumanoid);

		// Token: 0x060015AD RID: 5549
		[EngineMethod("get_bone_entitial_frame", false)]
		void GetBoneEntitialFrame(UIntPtr agentVisualsPtr, sbyte bone, bool useBoneMapping, ref MatrixFrame outFrame);

		// Token: 0x060015AE RID: 5550
		[EngineMethod("get_current_ragdoll_state", false)]
		RagdollState GetCurrentRagdollState(UIntPtr agentVisualsPtr);

		// Token: 0x060015AF RID: 5551
		[EngineMethod("get_real_bone_index", false)]
		sbyte GetRealBoneIndex(UIntPtr agentVisualsPtr, HumanBone boneType);

		// Token: 0x060015B0 RID: 5552
		[EngineMethod("add_prefab_to_agent_visual_bone_by_bone_type", false)]
		CompositeComponent AddPrefabToAgentVisualBoneByBoneType(UIntPtr agentVisualsPtr, string prefabName, HumanBone boneType);

		// Token: 0x060015B1 RID: 5553
		[EngineMethod("add_prefab_to_agent_visual_bone_by_real_bone_index", false)]
		CompositeComponent AddPrefabToAgentVisualBoneByRealBoneIndex(UIntPtr agentVisualsPtr, string prefabName, sbyte realBoneIndex);

		// Token: 0x060015B2 RID: 5554
		[EngineMethod("get_attached_weapon_entity", false)]
		GameEntity GetAttachedWeaponEntity(UIntPtr agentVisualsPtr, int attachedWeaponIndex);

		// Token: 0x060015B3 RID: 5555
		[EngineMethod("create_particle_system_attached_to_bone", false)]
		void CreateParticleSystemAttachedToBone(UIntPtr agentVisualsPtr, int runtimeParticleindex, sbyte boneIndex, ref MatrixFrame boneLocalParticleFrame);

		// Token: 0x060015B4 RID: 5556
		[EngineMethod("check_resources", false)]
		bool CheckResources(UIntPtr agentVisualsPtr, bool addToQueue);

		// Token: 0x060015B5 RID: 5557
		[EngineMethod("add_child_entity", false)]
		bool AddChildEntity(UIntPtr agentVisualsPtr, UIntPtr EntityId);

		// Token: 0x060015B6 RID: 5558
		[EngineMethod("set_cloth_wind_to_weapon_at_index", false)]
		void SetClothWindToWeaponAtIndex(UIntPtr agentVisualsPtr, Vec3 windDirection, bool isLocal, int index);

		// Token: 0x060015B7 RID: 5559
		[EngineMethod("remove_child_entity", false)]
		void RemoveChildEntity(UIntPtr agentVisualsPtr, UIntPtr EntityId, int removeReason);

		// Token: 0x060015B8 RID: 5560
		[EngineMethod("disable_contour", false)]
		void DisableContour(UIntPtr agentVisualsPtr);

		// Token: 0x060015B9 RID: 5561
		[EngineMethod("set_as_contour_entity", false)]
		void SetAsContourEntity(UIntPtr agentVisualsPtr, uint color);

		// Token: 0x060015BA RID: 5562
		[EngineMethod("set_contour_state", false)]
		void SetContourState(UIntPtr agentVisualsPtr, bool alwaysVisible);

		// Token: 0x060015BB RID: 5563
		[EngineMethod("set_enable_occlusion_culling", false)]
		void SetEnableOcclusionCulling(UIntPtr agentVisualsPtr, bool enable);

		// Token: 0x060015BC RID: 5564
		[EngineMethod("get_bone_type_data", false)]
		void GetBoneTypeData(UIntPtr pointer, sbyte boneIndex, ref BoneBodyTypeData boneBodyTypeData);
	}
}
