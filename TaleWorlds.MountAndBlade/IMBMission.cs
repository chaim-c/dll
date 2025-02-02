using System;
using TaleWorlds.Core;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001A7 RID: 423
	[ScriptingInterfaceBase]
	internal interface IMBMission
	{
		// Token: 0x060016CD RID: 5837
		[EngineMethod("clear_resources", false)]
		void ClearResources(UIntPtr missionPointer);

		// Token: 0x060016CE RID: 5838
		[EngineMethod("create_mission", false)]
		UIntPtr CreateMission(Mission mission);

		// Token: 0x060016CF RID: 5839
		[EngineMethod("tick_agents_and_teams_async", false)]
		void tickAgentsAndTeamsAsync(UIntPtr missionPointer, float dt);

		// Token: 0x060016D0 RID: 5840
		[EngineMethod("clear_agent_actions", false)]
		void ClearAgentActions(UIntPtr missionPointer);

		// Token: 0x060016D1 RID: 5841
		[EngineMethod("clear_missiles", false)]
		void ClearMissiles(UIntPtr missionPointer);

		// Token: 0x060016D2 RID: 5842
		[EngineMethod("clear_corpses", false)]
		void ClearCorpses(UIntPtr missionPointer, bool isMissionReset);

		// Token: 0x060016D3 RID: 5843
		[EngineMethod("get_pause_ai_tick", false)]
		bool GetPauseAITick(UIntPtr missionPointer);

		// Token: 0x060016D4 RID: 5844
		[EngineMethod("set_pause_ai_tick", false)]
		void SetPauseAITick(UIntPtr missionPointer, bool I);

		// Token: 0x060016D5 RID: 5845
		[EngineMethod("get_clear_scene_timer_elapsed_time", false)]
		float GetClearSceneTimerElapsedTime(UIntPtr missionPointer);

		// Token: 0x060016D6 RID: 5846
		[EngineMethod("reset_first_third_person_view", false)]
		void ResetFirstThirdPersonView(UIntPtr missionPointer);

		// Token: 0x060016D7 RID: 5847
		[EngineMethod("set_camera_is_first_person", false)]
		void SetCameraIsFirstPerson(bool value);

		// Token: 0x060016D8 RID: 5848
		[EngineMethod("set_camera_frame", false)]
		void SetCameraFrame(UIntPtr missionPointer, ref MatrixFrame cameraFrame, float zoomFactor, ref Vec3 attenuationPosition);

		// Token: 0x060016D9 RID: 5849
		[EngineMethod("get_camera_frame", false)]
		MatrixFrame GetCameraFrame(UIntPtr missionPointer);

		// Token: 0x060016DA RID: 5850
		[EngineMethod("get_is_loading_finished", false)]
		bool GetIsLoadingFinished(UIntPtr missionPointer);

		// Token: 0x060016DB RID: 5851
		[EngineMethod("clear_scene", false)]
		void ClearScene(UIntPtr missionPointer);

		// Token: 0x060016DC RID: 5852
		[EngineMethod("initialize_mission", false)]
		void InitializeMission(UIntPtr missionPointer, ref MissionInitializerRecord rec);

		// Token: 0x060016DD RID: 5853
		[EngineMethod("finalize_mission", false)]
		void FinalizeMission(UIntPtr missionPointer);

		// Token: 0x060016DE RID: 5854
		[EngineMethod("get_time", false)]
		float GetTime(UIntPtr missionPointer);

		// Token: 0x060016DF RID: 5855
		[EngineMethod("get_average_fps", false)]
		float GetAverageFps(UIntPtr missionPointer);

		// Token: 0x060016E0 RID: 5856
		[EngineMethod("get_combat_type", false)]
		int GetCombatType(UIntPtr missionPointer);

		// Token: 0x060016E1 RID: 5857
		[EngineMethod("set_combat_type", false)]
		void SetCombatType(UIntPtr missionPointer, int combatType);

		// Token: 0x060016E2 RID: 5858
		[EngineMethod("ray_cast_for_closest_agent", false)]
		Agent RayCastForClosestAgent(UIntPtr missionPointer, Vec3 SourcePoint, Vec3 RayFinishPoint, int ExcludeAgentIndex, ref float CollisionDistance, float RayThickness);

		// Token: 0x060016E3 RID: 5859
		[EngineMethod("ray_cast_for_closest_agents_limbs", false)]
		bool RayCastForClosestAgentsLimbs(UIntPtr missionPointer, Vec3 SourcePoint, Vec3 RayFinishPoint, int ExcludeAgentIndex, ref float CollisionDistance, ref int AgentIndex, ref sbyte BoneIndex);

		// Token: 0x060016E4 RID: 5860
		[EngineMethod("ray_cast_for_given_agents_limbs", false)]
		bool RayCastForGivenAgentsLimbs(UIntPtr missionPointer, Vec3 SourcePoint, Vec3 RayFinishPoint, int GivenAgentIndex, ref float CollisionDistance, ref sbyte BoneIndex);

		// Token: 0x060016E5 RID: 5861
		[EngineMethod("get_number_of_teams", false)]
		int GetNumberOfTeams(UIntPtr missionPointer);

		// Token: 0x060016E6 RID: 5862
		[EngineMethod("reset_teams", false)]
		void ResetTeams(UIntPtr missionPointer);

		// Token: 0x060016E7 RID: 5863
		[EngineMethod("add_team", false)]
		int AddTeam(UIntPtr missionPointer);

		// Token: 0x060016E8 RID: 5864
		[EngineMethod("restart_record", false)]
		void RestartRecord(UIntPtr missionPointer);

		// Token: 0x060016E9 RID: 5865
		[EngineMethod("is_position_inside_boundaries", false)]
		bool IsPositionInsideBoundaries(UIntPtr missionPointer, Vec2 position);

		// Token: 0x060016EA RID: 5866
		[EngineMethod("is_position_inside_any_blocker_nav_mesh_face_2d", false)]
		bool IsPositionInsideAnyBlockerNavMeshFace2D(UIntPtr missionPointer, Vec2 position);

		// Token: 0x060016EB RID: 5867
		[EngineMethod("get_alternate_position_for_navmeshless_or_out_of_bounds_position", false)]
		WorldPosition GetAlternatePositionForNavmeshlessOrOutOfBoundsPosition(UIntPtr ptr, ref Vec2 directionTowards, ref WorldPosition originalPosition, ref float positionPenalty);

		// Token: 0x060016EC RID: 5868
		[EngineMethod("add_missile", false)]
		int AddMissile(UIntPtr missionPointer, bool isPrediction, int shooterAgentIndex, in WeaponData weaponData, WeaponStatsData[] weaponStatsData, int weaponStatsDataLength, float damageBonus, ref Vec3 position, ref Vec3 direction, ref Mat3 orientation, float baseSpeed, float speed, bool addRigidBody, UIntPtr entityPointer, int forcedMissileIndex, bool isPrimaryWeaponShot, out UIntPtr missileEntity);

		// Token: 0x060016ED RID: 5869
		[EngineMethod("add_missile_single_usage", false)]
		int AddMissileSingleUsage(UIntPtr missionPointer, bool isPrediction, int shooterAgentIndex, in WeaponData weaponData, in WeaponStatsData weaponStatsData, float damageBonus, ref Vec3 position, ref Vec3 direction, ref Mat3 orientation, float baseSpeed, float speed, bool addRigidBody, UIntPtr entityPointer, int forcedMissileIndex, bool isPrimaryWeaponShot, out UIntPtr missileEntity);

		// Token: 0x060016EE RID: 5870
		[EngineMethod("get_missile_collision_point", false)]
		Vec3 GetMissileCollisionPoint(UIntPtr missionPointer, Vec3 missileStartingPosition, Vec3 missileDirection, float missileStartingSpeed, in WeaponData weaponData);

		// Token: 0x060016EF RID: 5871
		[EngineMethod("remove_missile", false)]
		void RemoveMissile(UIntPtr missionPointer, int missileIndex);

		// Token: 0x060016F0 RID: 5872
		[EngineMethod("get_missile_vertical_aim_correction", false)]
		float GetMissileVerticalAimCorrection(Vec3 vecToTarget, float missileStartingSpeed, ref WeaponStatsData weaponStatsData, float airFrictionConstant);

		// Token: 0x060016F1 RID: 5873
		[EngineMethod("get_missile_range", false)]
		float GetMissileRange(float missileStartingSpeed, float heightDifference);

		// Token: 0x060016F2 RID: 5874
		[EngineMethod("compute_exact_missile_range_at_height_difference", false)]
		float ComputeExactMissileRangeAtHeightDifference(float targetHeightDifference, float initialSpeed, float airFrictionConstant, float maxDuration);

		// Token: 0x060016F3 RID: 5875
		[EngineMethod("prepare_missile_weapon_for_drop", false)]
		void PrepareMissileWeaponForDrop(UIntPtr missionPointer, int missileIndex);

		// Token: 0x060016F4 RID: 5876
		[EngineMethod("add_particle_system_burst_by_name", false)]
		void AddParticleSystemBurstByName(UIntPtr missionPointer, string particleSystem, ref MatrixFrame frame, bool synchThroughNetwork);

		// Token: 0x060016F5 RID: 5877
		[EngineMethod("tick", false)]
		void Tick(UIntPtr missionPointer, float dt);

		// Token: 0x060016F6 RID: 5878
		[EngineMethod("idle_tick", false)]
		void IdleTick(UIntPtr missionPointer, float dt);

		// Token: 0x060016F7 RID: 5879
		[EngineMethod("make_sound", false)]
		void MakeSound(UIntPtr pointer, int nativeSoundCode, Vec3 position, bool soundCanBePredicted, bool isReliable, int relatedAgent1, int relatedAgent2);

		// Token: 0x060016F8 RID: 5880
		[EngineMethod("make_sound_with_parameter", false)]
		void MakeSoundWithParameter(UIntPtr pointer, int nativeSoundCode, Vec3 position, bool soundCanBePredicted, bool isReliable, int relatedAgent1, int relatedAgent2, SoundEventParameter parameter);

		// Token: 0x060016F9 RID: 5881
		[EngineMethod("make_sound_only_on_related_peer", false)]
		void MakeSoundOnlyOnRelatedPeer(UIntPtr pointer, int nativeSoundCode, Vec3 position, int relatedAgent);

		// Token: 0x060016FA RID: 5882
		[EngineMethod("add_sound_alarm_factor_to_agents", false)]
		void AddSoundAlarmFactorToAgents(UIntPtr pointer, int ownerId, Vec3 position, float alarmFactor);

		// Token: 0x060016FB RID: 5883
		[EngineMethod("get_enemy_alarm_state_indicator", false)]
		int GetEnemyAlarmStateIndicator(UIntPtr missionPointer);

		// Token: 0x060016FC RID: 5884
		[EngineMethod("get_player_alarm_indicator", false)]
		float GetPlayerAlarmIndicator(UIntPtr missionPointer);

		// Token: 0x060016FD RID: 5885
		[EngineMethod("create_agent", false)]
		Mission.AgentCreationResult CreateAgent(UIntPtr missionPointer, ulong monsterFlag, int forcedAgentIndex, bool isFemale, ref AgentSpawnData spawnData, ref CapsuleData bodyCapsule, ref CapsuleData crouchedBodyCapsule, ref AnimationSystemData animationSystemData, int instanceNo);

		// Token: 0x060016FE RID: 5886
		[EngineMethod("get_position_of_missile", false)]
		Vec3 GetPositionOfMissile(UIntPtr missionPointer, int index);

		// Token: 0x060016FF RID: 5887
		[EngineMethod("get_velocity_of_missile", false)]
		Vec3 GetVelocityOfMissile(UIntPtr missionPointer, int index);

		// Token: 0x06001700 RID: 5888
		[EngineMethod("get_missile_has_rigid_body", false)]
		bool GetMissileHasRigidBody(UIntPtr missionPointer, int index);

		// Token: 0x06001701 RID: 5889
		[EngineMethod("add_boundary", false)]
		bool AddBoundary(UIntPtr missionPointer, string name, Vec2[] boundaryPoints, int boundaryPointCount, bool isAllowanceInside);

		// Token: 0x06001702 RID: 5890
		[EngineMethod("remove_boundary", false)]
		bool RemoveBoundary(UIntPtr missionPointer, string name);

		// Token: 0x06001703 RID: 5891
		[EngineMethod("get_boundary_points", false)]
		void GetBoundaryPoints(UIntPtr missionPointer, string name, int boundaryPointOffset, Vec2[] boundaryPoints, int boundaryPointsSize, ref int retrievedPointCount);

		// Token: 0x06001704 RID: 5892
		[EngineMethod("get_boundary_count", false)]
		int GetBoundaryCount(UIntPtr missionPointer);

		// Token: 0x06001705 RID: 5893
		[EngineMethod("get_boundary_radius", false)]
		float GetBoundaryRadius(UIntPtr missionPointer, string name);

		// Token: 0x06001706 RID: 5894
		[EngineMethod("get_boundary_name", false)]
		string GetBoundaryName(UIntPtr missionPointer, int boundaryIndex);

		// Token: 0x06001707 RID: 5895
		[EngineMethod("get_closest_boundary_position", false)]
		Vec2 GetClosestBoundaryPosition(UIntPtr missionPointer, Vec2 position);

		// Token: 0x06001708 RID: 5896
		[EngineMethod("get_navigation_points", false)]
		bool GetNavigationPoints(UIntPtr missionPointer, ref NavigationData navigationData);

		// Token: 0x06001709 RID: 5897
		[EngineMethod("set_navigation_face_cost_with_id_around_position", false)]
		void SetNavigationFaceCostWithIdAroundPosition(UIntPtr missionPointer, int navigationFaceId, Vec3 position, float cost);

		// Token: 0x0600170A RID: 5898
		[EngineMethod("pause_mission_scene_sounds", false)]
		void PauseMissionSceneSounds(UIntPtr missionPointer);

		// Token: 0x0600170B RID: 5899
		[EngineMethod("resume_mission_scene_sounds", false)]
		void ResumeMissionSceneSounds(UIntPtr missionPointer);

		// Token: 0x0600170C RID: 5900
		[EngineMethod("process_record_until_time", false)]
		void ProcessRecordUntilTime(UIntPtr missionPointer, float time);

		// Token: 0x0600170D RID: 5901
		[EngineMethod("end_of_record", false)]
		bool EndOfRecord(UIntPtr missionPointer);

		// Token: 0x0600170E RID: 5902
		[EngineMethod("record_current_state", false)]
		void RecordCurrentState(UIntPtr missionPointer);

		// Token: 0x0600170F RID: 5903
		[EngineMethod("start_recording", false)]
		void StartRecording();

		// Token: 0x06001710 RID: 5904
		[EngineMethod("backup_record_to_file", false)]
		void BackupRecordToFile(UIntPtr missionPointer, string fileName, string gameType, string sceneLevels);

		// Token: 0x06001711 RID: 5905
		[EngineMethod("restore_record_from_file", false)]
		void RestoreRecordFromFile(UIntPtr missionPointer, string fileName);

		// Token: 0x06001712 RID: 5906
		[EngineMethod("clear_record_buffers", false)]
		void ClearRecordBuffers(UIntPtr missionPointer);

		// Token: 0x06001713 RID: 5907
		[EngineMethod("get_scene_name_for_replay", false)]
		string GetSceneNameForReplay(PlatformFilePath replayName);

		// Token: 0x06001714 RID: 5908
		[EngineMethod("get_game_type_for_replay", false)]
		string GetGameTypeForReplay(PlatformFilePath replayName);

		// Token: 0x06001715 RID: 5909
		[EngineMethod("get_scene_levels_for_replay", false)]
		string GetSceneLevelsForReplay(PlatformFilePath replayName);

		// Token: 0x06001716 RID: 5910
		[EngineMethod("get_atmosphere_name_for_replay", false)]
		string GetAtmosphereNameForReplay(PlatformFilePath replayName);

		// Token: 0x06001717 RID: 5911
		[EngineMethod("get_atmosphere_season_for_replay", false)]
		int GetAtmosphereSeasonForReplay(PlatformFilePath replayName);

		// Token: 0x06001718 RID: 5912
		[EngineMethod("get_closest_enemy", false)]
		Agent GetClosestEnemy(UIntPtr missionPointer, int teamIndex, Vec3 position, float radius);

		// Token: 0x06001719 RID: 5913
		[EngineMethod("get_closest_ally", false)]
		Agent GetClosestAlly(UIntPtr missionPointer, int teamIndex, Vec3 position, float radius);

		// Token: 0x0600171A RID: 5914
		[EngineMethod("is_agent_in_proximity_map", false)]
		bool IsAgentInProximityMap(UIntPtr missionPointer, int agentIndex);

		// Token: 0x0600171B RID: 5915
		[EngineMethod("has_any_agents_of_team_around", false)]
		bool HasAnyAgentsOfTeamAround(UIntPtr missionPointer, Vec3 origin, float radius, int teamNo);

		// Token: 0x0600171C RID: 5916
		[EngineMethod("get_agent_count_around_position", false)]
		void GetAgentCountAroundPosition(UIntPtr missionPointer, int teamIndex, Vec2 position, float radius, ref int allyCount, ref int enemyCount);

		// Token: 0x0600171D RID: 5917
		[EngineMethod("find_agent_with_index", false)]
		Agent FindAgentWithIndex(UIntPtr missionPointer, int index);

		// Token: 0x0600171E RID: 5918
		[EngineMethod("set_random_decide_time_of_agents", false)]
		void SetRandomDecideTimeOfAgents(UIntPtr missionPointer, int agentCount, int[] agentIndices, float minAIReactionTime, float maxAIReactionTime);

		// Token: 0x0600171F RID: 5919
		[EngineMethod("get_average_morale_of_agents", false)]
		float GetAverageMoraleOfAgents(UIntPtr missionPointer, int agentCount, int[] agentIndices);

		// Token: 0x06001720 RID: 5920
		[EngineMethod("get_best_slope_towards_direction", false)]
		WorldPosition GetBestSlopeTowardsDirection(UIntPtr missionPointer, ref WorldPosition centerPosition, float halfsize, ref WorldPosition referencePosition);

		// Token: 0x06001721 RID: 5921
		[EngineMethod("get_best_slope_angle_height_pos_for_defending", false)]
		WorldPosition GetBestSlopeAngleHeightPosForDefending(UIntPtr missionPointer, WorldPosition enemyPosition, WorldPosition defendingPosition, int sampleSize, float distanceRatioAllowedFromDefendedPos, float distanceSqrdAllowedFromBoundary, float cosinusOfBestSlope, float cosinusOfMaxAcceptedSlope, float minSlopeScore, float maxSlopeScore, float excessiveSlopePenalty, float nearConeCenterRatio, float nearConeCenterBonus, float heightDifferenceCeiling, float maxDisplacementPenalty);

		// Token: 0x06001722 RID: 5922
		[EngineMethod("get_nearby_agents_aux", false)]
		void GetNearbyAgentsAux(UIntPtr missionPointer, Vec2 center, float radius, int teamIndex, int friendOrEnemyOrAll, int agentsArrayOffset, ref EngineStackArray.StackArray40Int agentIds, ref int retrievedAgentCount);

		// Token: 0x06001723 RID: 5923
		[EngineMethod("get_weighted_point_of_enemies", false)]
		Vec2 GetWeightedPointOfEnemies(UIntPtr missionPointer, int agentIndex, Vec2 basePoint);

		// Token: 0x06001724 RID: 5924
		[EngineMethod("is_formation_unit_position_available", false)]
		bool IsFormationUnitPositionAvailable(UIntPtr missionPointer, ref WorldPosition orderPosition, ref WorldPosition unitPosition, ref WorldPosition nearestAvailableUnitPosition, float manhattanDistance);

		// Token: 0x06001725 RID: 5925
		[EngineMethod("get_straight_path_to_target", false)]
		WorldPosition GetStraightPathToTarget(UIntPtr scenePointer, Vec2 targetPosition, WorldPosition startingPosition, float samplingDistance, bool stopAtObstacle);

		// Token: 0x06001726 RID: 5926
		[EngineMethod("set_bow_missile_speed_modifier", false)]
		void SetBowMissileSpeedModifier(UIntPtr missionPointer, float modifier);

		// Token: 0x06001727 RID: 5927
		[EngineMethod("set_crossbow_missile_speed_modifier", false)]
		void SetCrossbowMissileSpeedModifier(UIntPtr missionPointer, float modifier);

		// Token: 0x06001728 RID: 5928
		[EngineMethod("set_throwing_missile_speed_modifier", false)]
		void SetThrowingMissileSpeedModifier(UIntPtr missionPointer, float modifier);

		// Token: 0x06001729 RID: 5929
		[EngineMethod("set_missile_range_modifier", false)]
		void SetMissileRangeModifier(UIntPtr missionPointer, float modifier);

		// Token: 0x0600172A RID: 5930
		[EngineMethod("set_last_movement_key_pressed", false)]
		void SetLastMovementKeyPressed(UIntPtr missionPointer, Agent.MovementControlFlag lastMovementKeyPressed);

		// Token: 0x0600172B RID: 5931
		[EngineMethod("fastforward_mission", false)]
		void FastForwardMission(UIntPtr missionPointer, float startTime, float endTime);

		// Token: 0x0600172C RID: 5932
		[EngineMethod("get_debug_agent", false)]
		int GetDebugAgent(UIntPtr missionPointer);

		// Token: 0x0600172D RID: 5933
		[EngineMethod("set_debug_agent", false)]
		void SetDebugAgent(UIntPtr missionPointer, int index);

		// Token: 0x0600172E RID: 5934
		[EngineMethod("add_ai_debug_text", false)]
		void AddAiDebugText(UIntPtr missionPointer, string text);

		// Token: 0x0600172F RID: 5935
		[EngineMethod("agent_proximity_map_begin_search", false)]
		AgentProximityMap.ProximityMapSearchStructInternal ProximityMapBeginSearch(UIntPtr missionPointer, Vec2 searchPos, float searchRadius);

		// Token: 0x06001730 RID: 5936
		[EngineMethod("agent_proximity_map_find_next", false)]
		void ProximityMapFindNext(UIntPtr missionPointer, ref AgentProximityMap.ProximityMapSearchStructInternal searchStruct);

		// Token: 0x06001731 RID: 5937
		[EngineMethod("agent_proximity_map_get_max_search_radius", false)]
		float ProximityMapMaxSearchRadius(UIntPtr missionPointer);

		// Token: 0x06001732 RID: 5938
		[EngineMethod("get_biggest_agent_collision_padding", false)]
		float GetBiggestAgentCollisionPadding(UIntPtr missionPointer);

		// Token: 0x06001733 RID: 5939
		[EngineMethod("set_mission_corpse_fade_out_time_in_seconds", false)]
		void SetMissionCorpseFadeOutTimeInSeconds(UIntPtr missionPointer, float corpseFadeOutTimeInSeconds);

		// Token: 0x06001734 RID: 5940
		[EngineMethod("set_report_stuck_agents_mode", false)]
		void SetReportStuckAgentsMode(UIntPtr missionPointer, bool value);

		// Token: 0x06001735 RID: 5941
		[EngineMethod("batch_formation_unit_positions", false)]
		void BatchFormationUnitPositions(UIntPtr missionPointer, Vec2i[] orderedPositionIndices, Vec2[] orderedLocalPositions, int[] availabilityTable, WorldPosition[] globalPositionTable, WorldPosition orderPosition, Vec2 direction, int fileCount, int rankCount);

		// Token: 0x06001736 RID: 5942
		[EngineMethod("toggle_disable_fall_avoid", false)]
		bool ToggleDisableFallAvoid();

		// Token: 0x06001737 RID: 5943
		[EngineMethod("get_water_level_at_position", false)]
		float GetWaterLevelAtPosition(UIntPtr missionPointer, Vec2 position);

		// Token: 0x06001738 RID: 5944
		[EngineMethod("find_convex_hull", false)]
		void FindConvexHull(Vec2[] boundaryPoints, int boundaryPointCount, ref int convexPointCount);
	}
}
