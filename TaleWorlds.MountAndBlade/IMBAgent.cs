using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001A2 RID: 418
	[ScriptingInterfaceBase]
	internal interface IMBAgent
	{
		// Token: 0x060015BD RID: 5565
		[EngineMethod("get_movement_flags", false)]
		uint GetMovementFlags(UIntPtr agentPointer);

		// Token: 0x060015BE RID: 5566
		[EngineMethod("set_movement_flags", false)]
		void SetMovementFlags(UIntPtr agentPointer, Agent.MovementControlFlag value);

		// Token: 0x060015BF RID: 5567
		[EngineMethod("get_movement_input_vector", false)]
		Vec2 GetMovementInputVector(UIntPtr agentPointer);

		// Token: 0x060015C0 RID: 5568
		[EngineMethod("set_movement_input_vector", false)]
		void SetMovementInputVector(UIntPtr agentPointer, Vec2 value);

		// Token: 0x060015C1 RID: 5569
		[EngineMethod("get_collision_capsule", false)]
		void GetCollisionCapsule(UIntPtr agentPointer, ref CapsuleData value);

		// Token: 0x060015C2 RID: 5570
		[EngineMethod("set_attack_state", false)]
		void SetAttackState(UIntPtr agentPointer, int attackState);

		// Token: 0x060015C3 RID: 5571
		[EngineMethod("get_agent_visuals", false)]
		MBAgentVisuals GetAgentVisuals(UIntPtr agentPointer);

		// Token: 0x060015C4 RID: 5572
		[EngineMethod("get_event_control_flags", false)]
		uint GetEventControlFlags(UIntPtr agentPointer);

		// Token: 0x060015C5 RID: 5573
		[EngineMethod("set_event_control_flags", false)]
		void SetEventControlFlags(UIntPtr agentPointer, Agent.EventControlFlag eventflag);

		// Token: 0x060015C6 RID: 5574
		[EngineMethod("set_average_ping_in_milliseconds", false)]
		void SetAveragePingInMilliseconds(UIntPtr agentPointer, double averagePingInMilliseconds);

		// Token: 0x060015C7 RID: 5575
		[EngineMethod("set_look_agent", false)]
		void SetLookAgent(UIntPtr agentPointer, UIntPtr lookAtAgentPointer);

		// Token: 0x060015C8 RID: 5576
		[EngineMethod("get_look_agent", false)]
		Agent GetLookAgent(UIntPtr agentPointer);

		// Token: 0x060015C9 RID: 5577
		[EngineMethod("get_target_agent", false)]
		Agent GetTargetAgent(UIntPtr agentPointer);

		// Token: 0x060015CA RID: 5578
		[EngineMethod("set_target_agent", false)]
		void SetTargetAgent(UIntPtr agentPointer, int targetAgentIndex);

		// Token: 0x060015CB RID: 5579
		[EngineMethod("set_automatic_target_agent_selection", false)]
		void SetAutomaticTargetSelection(UIntPtr agentPointer, bool enable);

		// Token: 0x060015CC RID: 5580
		[EngineMethod("set_interaction_agent", false)]
		void SetInteractionAgent(UIntPtr agentPointer, UIntPtr interactionAgentPointer);

		// Token: 0x060015CD RID: 5581
		[EngineMethod("set_look_to_point_of_interest", false)]
		void SetLookToPointOfInterest(UIntPtr agentPointer, Vec3 point);

		// Token: 0x060015CE RID: 5582
		[EngineMethod("disable_look_to_point_of_interest", false)]
		void DisableLookToPointOfInterest(UIntPtr agentPointer);

		// Token: 0x060015CF RID: 5583
		[EngineMethod("is_enemy", false)]
		bool IsEnemy(UIntPtr agentPointer1, UIntPtr agentPointer2);

		// Token: 0x060015D0 RID: 5584
		[EngineMethod("is_friend", false)]
		bool IsFriend(UIntPtr agentPointer1, UIntPtr agentPointer2);

		// Token: 0x060015D1 RID: 5585
		[EngineMethod("get_agent_flags", false)]
		uint GetAgentFlags(UIntPtr agentPointer);

		// Token: 0x060015D2 RID: 5586
		[EngineMethod("set_agent_flags", false)]
		void SetAgentFlags(UIntPtr agentPointer, uint agentFlags);

		// Token: 0x060015D3 RID: 5587
		[EngineMethod("set_selected_mount_index", false)]
		void SetSelectedMountIndex(UIntPtr agentPointer, int mount_index);

		// Token: 0x060015D4 RID: 5588
		[EngineMethod("get_selected_mount_index", false)]
		int GetSelectedMountIndex(UIntPtr agentPointer);

		// Token: 0x060015D5 RID: 5589
		[EngineMethod("get_firing_order", false)]
		int GetFiringOrder(UIntPtr agentPointer);

		// Token: 0x060015D6 RID: 5590
		[EngineMethod("get_riding_order", false)]
		int GetRidingOrder(UIntPtr agentPointer);

		// Token: 0x060015D7 RID: 5591
		[EngineMethod("get_stepped_entity_id", false)]
		UIntPtr GetSteppedEntityId(UIntPtr agentPointer);

		// Token: 0x060015D8 RID: 5592
		[EngineMethod("set_network_peer", false)]
		void SetNetworkPeer(UIntPtr agentPointer, int networkPeerIndex);

		// Token: 0x060015D9 RID: 5593
		[EngineMethod("die", false)]
		void Die(UIntPtr agentPointer, ref Blow b, sbyte overrideKillInfo);

		// Token: 0x060015DA RID: 5594
		[EngineMethod("make_dead", false)]
		void MakeDead(UIntPtr agentPointer, bool isKilled, int actionIndex);

		// Token: 0x060015DB RID: 5595
		[EngineMethod("set_formation_frame_disabled", false)]
		void SetFormationFrameDisabled(UIntPtr agentPointer);

		// Token: 0x060015DC RID: 5596
		[EngineMethod("set_formation_frame_enabled", false)]
		bool SetFormationFrameEnabled(UIntPtr agentPointer, WorldPosition position, Vec2 direction, Vec2 positionVelocity, float formationDirectionEnforcingFactor);

		// Token: 0x060015DD RID: 5597
		[EngineMethod("set_should_catch_up_with_formation", false)]
		void SetShouldCatchUpWithFormation(UIntPtr agentPointer, bool value);

		// Token: 0x060015DE RID: 5598
		[EngineMethod("set_formation_integrity_data", false)]
		void SetFormationIntegrityData(UIntPtr agentPointer, Vec2 position, Vec2 currentFormationDirection, Vec2 averageVelocityOfCloseAgents, float averageMaxUnlimitedSpeedOfCloseAgents, float deviationOfPositions);

		// Token: 0x060015DF RID: 5599
		[EngineMethod("set_formation_info", false)]
		void SetFormationInfo(UIntPtr agentPointer, int fileIndex, int rankIndex, int fileCount, int rankCount, Vec2 wallDir, int unitSpacing);

		// Token: 0x060015E0 RID: 5600
		[EngineMethod("set_retreat_mode", false)]
		void SetRetreatMode(UIntPtr agentPointer, WorldPosition retreatPos, bool retreat);

		// Token: 0x060015E1 RID: 5601
		[EngineMethod("is_retreating", false)]
		bool IsRetreating(UIntPtr agentPointer);

		// Token: 0x060015E2 RID: 5602
		[EngineMethod("is_fading_out", false)]
		bool IsFadingOut(UIntPtr agentPointer);

		// Token: 0x060015E3 RID: 5603
		[EngineMethod("start_fading_out", false)]
		void StartFadingOut(UIntPtr agentPointer);

		// Token: 0x060015E4 RID: 5604
		[EngineMethod("set_render_check_enabled", false)]
		void SetRenderCheckEnabled(UIntPtr agentPointer, bool value);

		// Token: 0x060015E5 RID: 5605
		[EngineMethod("get_render_check_enabled", false)]
		bool GetRenderCheckEnabled(UIntPtr agentPointer);

		// Token: 0x060015E6 RID: 5606
		[EngineMethod("get_retreat_pos", false)]
		WorldPosition GetRetreatPos(UIntPtr agentPointer);

		// Token: 0x060015E7 RID: 5607
		[EngineMethod("get_team", false)]
		int GetTeam(UIntPtr agentPointer);

		// Token: 0x060015E8 RID: 5608
		[EngineMethod("set_team", false)]
		void SetTeam(UIntPtr agentPointer, int teamIndex);

		// Token: 0x060015E9 RID: 5609
		[EngineMethod("set_courage", false)]
		void SetCourage(UIntPtr agentPointer, float courage);

		// Token: 0x060015EA RID: 5610
		[EngineMethod("update_driven_properties", false)]
		void UpdateDrivenProperties(UIntPtr agentPointer, float[] values);

		// Token: 0x060015EB RID: 5611
		[EngineMethod("get_look_direction", false)]
		Vec3 GetLookDirection(UIntPtr agentPointer);

		// Token: 0x060015EC RID: 5612
		[EngineMethod("set_look_direction", false)]
		void SetLookDirection(UIntPtr agentPointer, Vec3 lookDirection);

		// Token: 0x060015ED RID: 5613
		[EngineMethod("get_look_down_limit", false)]
		float GetLookDownLimit(UIntPtr agentPointer);

		// Token: 0x060015EE RID: 5614
		[EngineMethod("get_position", false)]
		Vec3 GetPosition(UIntPtr agentPointer);

		// Token: 0x060015EF RID: 5615
		[EngineMethod("set_position", false)]
		void SetPosition(UIntPtr agentPointer, ref Vec3 position);

		// Token: 0x060015F0 RID: 5616
		[EngineMethod("get_rotation_frame", false)]
		void GetRotationFrame(UIntPtr agentPointer, ref MatrixFrame outFrame);

		// Token: 0x060015F1 RID: 5617
		[EngineMethod("get_eye_global_height", false)]
		float GetEyeGlobalHeight(UIntPtr agentPointer);

		// Token: 0x060015F2 RID: 5618
		[EngineMethod("get_movement_velocity", false)]
		Vec2 GetMovementVelocity(UIntPtr agentPointer);

		// Token: 0x060015F3 RID: 5619
		[EngineMethod("get_average_velocity", false)]
		Vec3 GetAverageVelocity(UIntPtr agentPointer);

		// Token: 0x060015F4 RID: 5620
		[EngineMethod("get_is_left_stance", false)]
		bool GetIsLeftStance(UIntPtr agentPointer);

		// Token: 0x060015F5 RID: 5621
		[EngineMethod("invalidate_target_agent", false)]
		void InvalidateTargetAgent(UIntPtr agentPointer);

		// Token: 0x060015F6 RID: 5622
		[EngineMethod("invalidate_ai_weapon_selections", false)]
		void InvalidateAIWeaponSelections(UIntPtr agentPointer);

		// Token: 0x060015F7 RID: 5623
		[EngineMethod("reset_enemy_caches", false)]
		void ResetEnemyCaches(UIntPtr agentPointer);

		// Token: 0x060015F8 RID: 5624
		[EngineMethod("get_ai_state_flags", false)]
		Agent.AIStateFlag GetAIStateFlags(UIntPtr agentPointer);

		// Token: 0x060015F9 RID: 5625
		[EngineMethod("set_ai_state_flags", false)]
		void SetAIStateFlags(UIntPtr agentPointer, Agent.AIStateFlag aiStateFlags);

		// Token: 0x060015FA RID: 5626
		[EngineMethod("get_state_flags", false)]
		AgentState GetStateFlags(UIntPtr agentPointer);

		// Token: 0x060015FB RID: 5627
		[EngineMethod("set_state_flags", false)]
		void SetStateFlags(UIntPtr agentPointer, AgentState StateFlags);

		// Token: 0x060015FC RID: 5628
		[EngineMethod("get_mount_agent", false)]
		Agent GetMountAgent(UIntPtr agentPointer);

		// Token: 0x060015FD RID: 5629
		[EngineMethod("set_mount_agent", false)]
		void SetMountAgent(UIntPtr agentPointer, int mountAgentIndex);

		// Token: 0x060015FE RID: 5630
		[EngineMethod("get_rider_agent", false)]
		Agent GetRiderAgent(UIntPtr agentPointer);

		// Token: 0x060015FF RID: 5631
		[EngineMethod("set_controller", false)]
		void SetController(UIntPtr agentPointer, Agent.ControllerType controller);

		// Token: 0x06001600 RID: 5632
		[EngineMethod("get_controller", false)]
		Agent.ControllerType GetController(UIntPtr agentPointer);

		// Token: 0x06001601 RID: 5633
		[EngineMethod("set_initial_frame", false)]
		void SetInitialFrame(UIntPtr agentPointer, in Vec3 initialPosition, in Vec2 initialDirection, bool canSpawnOutsideOfMissionBoundary);

		// Token: 0x06001602 RID: 5634
		[EngineMethod("weapon_equipped", false)]
		void WeaponEquipped(UIntPtr agentPointer, int equipmentSlot, in WeaponData weaponData, WeaponStatsData[] weaponStatsData, int weaponStatsDataLength, in WeaponData ammoWeaponData, WeaponStatsData[] ammoWeaponStatsData, int ammoWeaponStatsDataLength, UIntPtr weaponEntity, bool removeOldWeaponFromScene, bool isWieldedOnSpawn);

		// Token: 0x06001603 RID: 5635
		[EngineMethod("drop_item", false)]
		void DropItem(UIntPtr agentPointer, int itemIndex, int pickedUpItemType);

		// Token: 0x06001604 RID: 5636
		[EngineMethod("set_weapon_amount_in_slot", false)]
		void SetWeaponAmountInSlot(UIntPtr agentPointer, int equipmentSlot, short amount, bool enforcePrimaryItem);

		// Token: 0x06001605 RID: 5637
		[EngineMethod("clear_equipment", false)]
		void ClearEquipment(UIntPtr agentPointer);

		// Token: 0x06001606 RID: 5638
		[EngineMethod("get_wielded_item_index", false)]
		EquipmentIndex GetWieldedItemIndex(UIntPtr agentPointer, int handIndex);

		// Token: 0x06001607 RID: 5639
		[EngineMethod("set_wielded_item_index_as_client", false)]
		void SetWieldedItemIndexAsClient(UIntPtr agentPointer, int handIndex, int wieldedItemIndex, bool isWieldedInstantly, bool isWieldedOnSpawn, int mainHandCurrentUsageIndex);

		// Token: 0x06001608 RID: 5640
		[EngineMethod("set_usage_index_of_weapon_in_slot_as_client", false)]
		void SetUsageIndexOfWeaponInSlotAsClient(UIntPtr agentPointer, int slotIndex, int usageIndex);

		// Token: 0x06001609 RID: 5641
		[EngineMethod("set_weapon_hit_points_in_slot", false)]
		void SetWeaponHitPointsInSlot(UIntPtr agentPointer, int wieldedItemIndex, short hitPoints);

		// Token: 0x0600160A RID: 5642
		[EngineMethod("set_weapon_ammo_as_client", false)]
		void SetWeaponAmmoAsClient(UIntPtr agentPointer, int equipmentIndex, int ammoEquipmentIndex, short ammo);

		// Token: 0x0600160B RID: 5643
		[EngineMethod("set_weapon_reload_phase_as_client", false)]
		void SetWeaponReloadPhaseAsClient(UIntPtr agentPointer, int wieldedItemIndex, short reloadPhase);

		// Token: 0x0600160C RID: 5644
		[EngineMethod("set_reload_ammo_in_slot", false)]
		void SetReloadAmmoInSlot(UIntPtr agentPointer, int slotIndex, int ammoSlotIndex, short reloadedAmmo);

		// Token: 0x0600160D RID: 5645
		[EngineMethod("start_switching_weapon_usage_index_as_client", false)]
		void StartSwitchingWeaponUsageIndexAsClient(UIntPtr agentPointer, int wieldedItemIndex, int usageIndex, Agent.UsageDirection currentMovementFlagUsageDirection);

		// Token: 0x0600160E RID: 5646
		[EngineMethod("try_to_wield_weapon_in_slot", false)]
		void TryToWieldWeaponInSlot(UIntPtr agentPointer, int equipmentSlot, int type, bool isWieldedOnSpawn);

		// Token: 0x0600160F RID: 5647
		[EngineMethod("get_weapon_entity_from_equipment_slot", false)]
		UIntPtr GetWeaponEntityFromEquipmentSlot(UIntPtr agentPointer, int equipmentSlot);

		// Token: 0x06001610 RID: 5648
		[EngineMethod("prepare_weapon_for_drop_in_equipment_slot", false)]
		void PrepareWeaponForDropInEquipmentSlot(UIntPtr agentPointer, int equipmentSlot, bool dropWithHolster);

		// Token: 0x06001611 RID: 5649
		[EngineMethod("try_to_sheath_weapon_in_hand", false)]
		void TryToSheathWeaponInHand(UIntPtr agentPointer, int handIndex, int type);

		// Token: 0x06001612 RID: 5650
		[EngineMethod("update_weapons", false)]
		void UpdateWeapons(UIntPtr agentPointer);

		// Token: 0x06001613 RID: 5651
		[EngineMethod("attach_weapon_to_bone", false)]
		void AttachWeaponToBone(UIntPtr agentPointer, in WeaponData weaponData, WeaponStatsData[] weaponStatsData, int weaponStatsDataLength, UIntPtr weaponEntity, sbyte boneIndex, ref MatrixFrame attachLocalFrame);

		// Token: 0x06001614 RID: 5652
		[EngineMethod("delete_attached_weapon_from_bone", false)]
		void DeleteAttachedWeaponFromBone(UIntPtr agentPointer, int attachedWeaponIndex);

		// Token: 0x06001615 RID: 5653
		[EngineMethod("attach_weapon_to_weapon_in_slot", false)]
		void AttachWeaponToWeaponInSlot(UIntPtr agentPointer, in WeaponData weaponData, WeaponStatsData[] weaponStatsData, int weaponStatsDataLength, UIntPtr weaponEntity, int slotIndex, ref MatrixFrame attachLocalFrame);

		// Token: 0x06001616 RID: 5654
		[EngineMethod("build", false)]
		void Build(UIntPtr agentPointer, Vec3 eyeOffsetWrtHead);

		// Token: 0x06001617 RID: 5655
		[EngineMethod("lock_agent_replication_table_with_current_reliable_sequence_no", false)]
		void LockAgentReplicationTableDataWithCurrentReliableSequenceNo(UIntPtr agentPointer, int peerIndex);

		// Token: 0x06001618 RID: 5656
		[EngineMethod("set_agent_exclude_state_for_face_group_id", false)]
		void SetAgentExcludeStateForFaceGroupId(UIntPtr agentPointer, int faceGroupId, bool isExcluded);

		// Token: 0x06001619 RID: 5657
		[EngineMethod("set_agent_scale", false)]
		void SetAgentScale(UIntPtr agentPointer, float scale);

		// Token: 0x0600161A RID: 5658
		[EngineMethod("initialize_agent_record", false)]
		void InitializeAgentRecord(UIntPtr agentPointer);

		// Token: 0x0600161B RID: 5659
		[EngineMethod("get_current_velocity", false)]
		Vec2 GetCurrentVelocity(UIntPtr agentPointer);

		// Token: 0x0600161C RID: 5660
		[EngineMethod("get_turn_speed", false)]
		float GetTurnSpeed(UIntPtr agentPointer);

		// Token: 0x0600161D RID: 5661
		[EngineMethod("get_movement_direction_as_angle", false)]
		float GetMovementDirectionAsAngle(UIntPtr agentPointer);

		// Token: 0x0600161E RID: 5662
		[EngineMethod("get_movement_direction", false)]
		Vec2 GetMovementDirection(UIntPtr agentPointer);

		// Token: 0x0600161F RID: 5663
		[EngineMethod("set_movement_direction", false)]
		void SetMovementDirection(UIntPtr agentPointer, in Vec2 direction);

		// Token: 0x06001620 RID: 5664
		[EngineMethod("get_current_speed_limit", false)]
		float GetCurrentSpeedLimit(UIntPtr agentPointer);

		// Token: 0x06001621 RID: 5665
		[EngineMethod("set_maximum_speed_limit", false)]
		void SetMaximumSpeedLimit(UIntPtr agentPointer, float maximumSpeedLimit, bool isMultiplier);

		// Token: 0x06001622 RID: 5666
		[EngineMethod("get_maximum_speed_limit", false)]
		float GetMaximumSpeedLimit(UIntPtr agentPointer);

		// Token: 0x06001623 RID: 5667
		[EngineMethod("get_maximum_forward_unlimited_speed", false)]
		float GetMaximumForwardUnlimitedSpeed(UIntPtr agentPointer);

		// Token: 0x06001624 RID: 5668
		[EngineMethod("fade_out", false)]
		void FadeOut(UIntPtr agentPointer, bool hideInstantly);

		// Token: 0x06001625 RID: 5669
		[EngineMethod("fade_in", false)]
		void FadeIn(UIntPtr agentPointer);

		// Token: 0x06001626 RID: 5670
		[EngineMethod("get_scripted_flags", false)]
		int GetScriptedFlags(UIntPtr agentPointer);

		// Token: 0x06001627 RID: 5671
		[EngineMethod("set_scripted_flags", false)]
		void SetScriptedFlags(UIntPtr agentPointer, int flags);

		// Token: 0x06001628 RID: 5672
		[EngineMethod("get_scripted_combat_flags", false)]
		int GetScriptedCombatFlags(UIntPtr agentPointer);

		// Token: 0x06001629 RID: 5673
		[EngineMethod("set_scripted_combat_flags", false)]
		void SetScriptedCombatFlags(UIntPtr agentPointer, int flags);

		// Token: 0x0600162A RID: 5674
		[EngineMethod("set_scripted_position_and_direction", false)]
		bool SetScriptedPositionAndDirection(UIntPtr agentPointer, ref WorldPosition targetPosition, float targetDirection, bool addHumanLikeDelay, int additionalFlags);

		// Token: 0x0600162B RID: 5675
		[EngineMethod("set_scripted_position", false)]
		bool SetScriptedPosition(UIntPtr agentPointer, ref WorldPosition targetPosition, bool addHumanLikeDelay, int additionalFlags);

		// Token: 0x0600162C RID: 5676
		[EngineMethod("set_scripted_target_entity", false)]
		void SetScriptedTargetEntity(UIntPtr agentPointer, UIntPtr entityId, ref WorldPosition specialPosition, int additionalFlags, bool ignoreIfAlreadyAttacking);

		// Token: 0x0600162D RID: 5677
		[EngineMethod("disable_scripted_movement", false)]
		void DisableScriptedMovement(UIntPtr agentPointer);

		// Token: 0x0600162E RID: 5678
		[EngineMethod("disable_scripted_combat_movement", false)]
		void DisableScriptedCombatMovement(UIntPtr agentPointer);

		// Token: 0x0600162F RID: 5679
		[EngineMethod("force_ai_behavior_selection", false)]
		void ForceAiBehaviorSelection(UIntPtr agentPointer);

		// Token: 0x06001630 RID: 5680
		[EngineMethod("has_path_through_navigation_face_id_from_direction", false)]
		bool HasPathThroughNavigationFaceIdFromDirection(UIntPtr agentPointer, int navigationFaceId, ref Vec2 direction);

		// Token: 0x06001631 RID: 5681
		[EngineMethod("has_path_through_navigation_faces_id_from_direction", false)]
		bool HasPathThroughNavigationFacesIDFromDirection(UIntPtr agentPointer, int navigationFaceID_1, int navigationFaceID_2, int navigationFaceID_3, ref Vec2 direction);

		// Token: 0x06001632 RID: 5682
		[EngineMethod("can_move_directly_to_position", false)]
		bool CanMoveDirectlyToPosition(UIntPtr agentPointer, in Vec2 position);

		// Token: 0x06001633 RID: 5683
		[EngineMethod("check_path_to_ai_target_agent_passes_through_navigation_face_id_from_direction", false)]
		bool CheckPathToAITargetAgentPassesThroughNavigationFaceIdFromDirection(UIntPtr agentPointer, int navigationFaceId, ref Vec3 direction, float overridenCostForFaceId);

		// Token: 0x06001634 RID: 5684
		[EngineMethod("get_path_distance_to_point", false)]
		float GetPathDistanceToPoint(UIntPtr agentPointer, ref Vec3 direction);

		// Token: 0x06001635 RID: 5685
		[EngineMethod("get_current_navigation_face_id", false)]
		int GetCurrentNavigationFaceId(UIntPtr agentPointer);

		// Token: 0x06001636 RID: 5686
		[EngineMethod("get_world_position", false)]
		WorldPosition GetWorldPosition(UIntPtr agentPointer);

		// Token: 0x06001637 RID: 5687
		[EngineMethod("set_agent_facial_animation", false)]
		void SetAgentFacialAnimation(UIntPtr agentPointer, int channel, string animationName, bool loop);

		// Token: 0x06001638 RID: 5688
		[EngineMethod("get_agent_facial_animation", false)]
		string GetAgentFacialAnimation(UIntPtr agentPointer);

		// Token: 0x06001639 RID: 5689
		[EngineMethod("get_agent_voice_definiton", false)]
		string GetAgentVoiceDefinition(UIntPtr agentPointer);

		// Token: 0x0600163A RID: 5690
		[EngineMethod("get_current_animation_flags", false)]
		ulong GetCurrentAnimationFlags(UIntPtr agentPointer, int channelNo);

		// Token: 0x0600163B RID: 5691
		[EngineMethod("get_current_action", false)]
		int GetCurrentAction(UIntPtr agentPointer, int channelNo);

		// Token: 0x0600163C RID: 5692
		[EngineMethod("get_current_action_type", false)]
		int GetCurrentActionType(UIntPtr agentPointer, int channelNo);

		// Token: 0x0600163D RID: 5693
		[EngineMethod("get_current_action_stage", false)]
		int GetCurrentActionStage(UIntPtr agentPointer, int channelNo);

		// Token: 0x0600163E RID: 5694
		[EngineMethod("get_current_action_direction", false)]
		int GetCurrentActionDirection(UIntPtr agentPointer, int channelNo);

		// Token: 0x0600163F RID: 5695
		[EngineMethod("compute_animation_displacement", false)]
		Vec3 ComputeAnimationDisplacement(UIntPtr agentPointer, float dt);

		// Token: 0x06001640 RID: 5696
		[EngineMethod("get_current_action_priority", false)]
		int GetCurrentActionPriority(UIntPtr agentPointer, int channelNo);

		// Token: 0x06001641 RID: 5697
		[EngineMethod("get_current_action_progress", false)]
		float GetCurrentActionProgress(UIntPtr agentPointer, int channelNo);

		// Token: 0x06001642 RID: 5698
		[EngineMethod("set_current_action_progress", false)]
		void SetCurrentActionProgress(UIntPtr agentPointer, int channelNo, float progress);

		// Token: 0x06001643 RID: 5699
		[EngineMethod("set_action_channel", false)]
		bool SetActionChannel(UIntPtr agentPointer, int channelNo, int actionNo, ulong additionalFlags, bool ignorePriority, float blendWithNextActionFactor, float actionSpeed, float blendInPeriod, float blendOutPeriodToNoAnim, float startProgress, bool useLinearSmoothing, float blendOutPeriod, bool forceFaceMorphRestart);

		// Token: 0x06001644 RID: 5700
		[EngineMethod("set_current_action_speed", false)]
		void SetCurrentActionSpeed(UIntPtr agentPointer, int channelNo, float actionSpeed);

		// Token: 0x06001645 RID: 5701
		[EngineMethod("tick_action_channels", false)]
		void TickActionChannels(UIntPtr agentPointer, float dt);

		// Token: 0x06001646 RID: 5702
		[EngineMethod("get_action_channel_weight", false)]
		float GetActionChannelWeight(UIntPtr agentPointer, int channelNo);

		// Token: 0x06001647 RID: 5703
		[EngineMethod("get_action_channel_current_action_weight", false)]
		float GetActionChannelCurrentActionWeight(UIntPtr agentPointer, int channelNo);

		// Token: 0x06001648 RID: 5704
		[EngineMethod("set_action_set", false)]
		void SetActionSet(UIntPtr agentPointer, ref AnimationSystemData animationSystemData);

		// Token: 0x06001649 RID: 5705
		[EngineMethod("get_action_set_no", false)]
		int GetActionSetNo(UIntPtr agentPointer);

		// Token: 0x0600164A RID: 5706
		[EngineMethod("get_movement_locked_state", false)]
		AgentMovementLockedState GetMovementLockedState(UIntPtr agentPointer);

		// Token: 0x0600164B RID: 5707
		[EngineMethod("get_aiming_timer", false)]
		float GetAimingTimer(UIntPtr agentPointer);

		// Token: 0x0600164C RID: 5708
		[EngineMethod("get_target_position", false)]
		Vec2 GetTargetPosition(UIntPtr agentPointer);

		// Token: 0x0600164D RID: 5709
		[EngineMethod("set_target_position", false)]
		void SetTargetPosition(UIntPtr agentPointer, ref Vec2 targetPosition);

		// Token: 0x0600164E RID: 5710
		[EngineMethod("get_target_direction", false)]
		Vec3 GetTargetDirection(UIntPtr agentPointer);

		// Token: 0x0600164F RID: 5711
		[EngineMethod("set_target_position_and_direction", false)]
		void SetTargetPositionAndDirection(UIntPtr agentPointer, ref Vec2 targetPosition, ref Vec3 targetDirection);

		// Token: 0x06001650 RID: 5712
		[EngineMethod("clear_target_frame", false)]
		void ClearTargetFrame(UIntPtr agentPointer);

		// Token: 0x06001651 RID: 5713
		[EngineMethod("get_is_look_direction_locked", false)]
		bool GetIsLookDirectionLocked(UIntPtr agentPointer);

		// Token: 0x06001652 RID: 5714
		[EngineMethod("set_is_look_direction_locked", false)]
		void SetIsLookDirectionLocked(UIntPtr agentPointer, bool isLocked);

		// Token: 0x06001653 RID: 5715
		[EngineMethod("set_mono_object", false)]
		void SetMonoObject(UIntPtr agentPointer, Agent monoObject);

		// Token: 0x06001654 RID: 5716
		[EngineMethod("get_eye_global_position", false)]
		Vec3 GetEyeGlobalPosition(UIntPtr agentPointer);

		// Token: 0x06001655 RID: 5717
		[EngineMethod("get_chest_global_position", false)]
		Vec3 GetChestGlobalPosition(UIntPtr agentPointer);

		// Token: 0x06001656 RID: 5718
		[EngineMethod("add_mesh_to_bone", false)]
		void AddMeshToBone(UIntPtr agentPointer, UIntPtr meshPointer, sbyte boneIndex);

		// Token: 0x06001657 RID: 5719
		[EngineMethod("remove_mesh_from_bone", false)]
		void RemoveMeshFromBone(UIntPtr agentPointer, UIntPtr meshPointer, sbyte boneIndex);

		// Token: 0x06001658 RID: 5720
		[EngineMethod("add_prefab_to_agent_bone", false)]
		CompositeComponent AddPrefabToAgentBone(UIntPtr agentPointer, string prefabName, sbyte boneIndex);

		// Token: 0x06001659 RID: 5721
		[EngineMethod("wield_next_weapon", false)]
		void WieldNextWeapon(UIntPtr agentPointer, int handIndex, int wieldActionType);

		// Token: 0x0600165A RID: 5722
		[EngineMethod("preload_for_rendering", false)]
		void PreloadForRendering(UIntPtr agentPointer);

		// Token: 0x0600165B RID: 5723
		[EngineMethod("get_agent_scale", false)]
		float GetAgentScale(UIntPtr agentPointer);

		// Token: 0x0600165C RID: 5724
		[EngineMethod("get_crouch_mode", false)]
		bool GetCrouchMode(UIntPtr agentPointer);

		// Token: 0x0600165D RID: 5725
		[EngineMethod("get_walk_mode", false)]
		bool GetWalkMode(UIntPtr agentPointer);

		// Token: 0x0600165E RID: 5726
		[EngineMethod("get_visual_position", false)]
		Vec3 GetVisualPosition(UIntPtr agentPointer);

		// Token: 0x0600165F RID: 5727
		[EngineMethod("is_look_rotation_in_slow_motion", false)]
		bool IsLookRotationInSlowMotion(UIntPtr agentPointer);

		// Token: 0x06001660 RID: 5728
		[EngineMethod("get_look_direction_as_angle", false)]
		float GetLookDirectionAsAngle(UIntPtr agentPointer);

		// Token: 0x06001661 RID: 5729
		[EngineMethod("set_look_direction_as_angle", false)]
		void SetLookDirectionAsAngle(UIntPtr agentPointer, float value);

		// Token: 0x06001662 RID: 5730
		[EngineMethod("attack_direction_to_movement_flag", false)]
		Agent.MovementControlFlag AttackDirectionToMovementFlag(UIntPtr agentPointer, Agent.UsageDirection direction);

		// Token: 0x06001663 RID: 5731
		[EngineMethod("defend_direction_to_movement_flag", false)]
		Agent.MovementControlFlag DefendDirectionToMovementFlag(UIntPtr agentPointer, Agent.UsageDirection direction);

		// Token: 0x06001664 RID: 5732
		[EngineMethod("get_head_camera_mode", false)]
		bool GetHeadCameraMode(UIntPtr agentPointer);

		// Token: 0x06001665 RID: 5733
		[EngineMethod("set_head_camera_mode", false)]
		void SetHeadCameraMode(UIntPtr agentPointer, bool value);

		// Token: 0x06001666 RID: 5734
		[EngineMethod("kick_clear", false)]
		bool KickClear(UIntPtr agentPointer);

		// Token: 0x06001667 RID: 5735
		[EngineMethod("reset_guard", false)]
		void ResetGuard(UIntPtr agentPointer);

		// Token: 0x06001668 RID: 5736
		[EngineMethod("get_current_guard_mode", false)]
		Agent.GuardMode GetCurrentGuardMode(UIntPtr agentPointer);

		// Token: 0x06001669 RID: 5737
		[EngineMethod("get_defend_movement_flag", false)]
		Agent.MovementControlFlag GetDefendMovementFlag(UIntPtr agentPointer);

		// Token: 0x0600166A RID: 5738
		[EngineMethod("get_attack_direction", false)]
		Agent.UsageDirection GetAttackDirection(UIntPtr agentPointer);

		// Token: 0x0600166B RID: 5739
		[EngineMethod("player_attack_direction", false)]
		Agent.UsageDirection PlayerAttackDirection(UIntPtr agentPointer);

		// Token: 0x0600166C RID: 5740
		[EngineMethod("get_wielded_weapon_info", false)]
		bool GetWieldedWeaponInfo(UIntPtr agentPointer, int handIndex, ref bool isMeleeWeapon, ref bool isRangedWeapon);

		// Token: 0x0600166D RID: 5741
		[EngineMethod("get_immediate_enemy", false)]
		Agent GetImmediateEnemy(UIntPtr agentPointer);

		// Token: 0x0600166E RID: 5742
		[EngineMethod("try_get_immediate_agent_movement_data", false)]
		bool TryGetImmediateEnemyAgentMovementData(UIntPtr agentPointer, out float maximumForwardUnlimitedSpeed, out Vec3 position);

		// Token: 0x0600166F RID: 5743
		[EngineMethod("get_is_doing_passive_attack", false)]
		bool GetIsDoingPassiveAttack(UIntPtr agentPointer);

		// Token: 0x06001670 RID: 5744
		[EngineMethod("get_is_passive_usage_conditions_are_met", false)]
		bool GetIsPassiveUsageConditionsAreMet(UIntPtr agentPointer);

		// Token: 0x06001671 RID: 5745
		[EngineMethod("get_current_aiming_turbulance", false)]
		float GetCurrentAimingTurbulance(UIntPtr agentPointer);

		// Token: 0x06001672 RID: 5746
		[EngineMethod("get_current_aiming_error", false)]
		float GetCurrentAimingError(UIntPtr agentPointer);

		// Token: 0x06001673 RID: 5747
		[EngineMethod("get_body_rotation_constraint", false)]
		Vec3 GetBodyRotationConstraint(UIntPtr agentPointer, int channelIndex);

		// Token: 0x06001674 RID: 5748
		[EngineMethod("get_action_direction", false)]
		Agent.UsageDirection GetActionDirection(int actionIndex);

		// Token: 0x06001675 RID: 5749
		[EngineMethod("get_attack_direction_usage", false)]
		Agent.UsageDirection GetAttackDirectionUsage(UIntPtr agentPointer);

		// Token: 0x06001676 RID: 5750
		[EngineMethod("handle_blow_aux", false)]
		void HandleBlowAux(UIntPtr agentPointer, ref Blow blow);

		// Token: 0x06001677 RID: 5751
		[EngineMethod("make_voice", false)]
		void MakeVoice(UIntPtr agentPointer, int voiceType, int predictionType);

		// Token: 0x06001678 RID: 5752
		[EngineMethod("set_hand_inverse_kinematics_frame", false)]
		bool SetHandInverseKinematicsFrame(UIntPtr agentPointer, ref MatrixFrame leftGlobalFrame, ref MatrixFrame rightGlobalFrame);

		// Token: 0x06001679 RID: 5753
		[EngineMethod("set_hand_inverse_kinematics_frame_for_mission_object_usage", false)]
		bool SetHandInverseKinematicsFrameForMissionObjectUsage(UIntPtr agentPointer, in MatrixFrame localIKFrame, in MatrixFrame boundEntityGlobalFrame, float animationHeightDifference);

		// Token: 0x0600167A RID: 5754
		[EngineMethod("clear_hand_inverse_kinematics", false)]
		void ClearHandInverseKinematics(UIntPtr agentPointer);

		// Token: 0x0600167B RID: 5755
		[EngineMethod("debug_more", false)]
		void DebugMore(UIntPtr agentPointer);

		// Token: 0x0600167C RID: 5756
		[EngineMethod("is_on_land", false)]
		bool IsOnLand(UIntPtr agentPointer);

		// Token: 0x0600167D RID: 5757
		[EngineMethod("is_sliding", false)]
		bool IsSliding(UIntPtr agentPointer);

		// Token: 0x0600167E RID: 5758
		[EngineMethod("is_running_away", false)]
		bool IsRunningAway(UIntPtr agentPointer);

		// Token: 0x0600167F RID: 5759
		[EngineMethod("get_cur_weapon_offset", false)]
		Vec3 GetCurWeaponOffset(UIntPtr agentPointer);

		// Token: 0x06001680 RID: 5760
		[EngineMethod("get_walking_speed_limit_of_mountable", false)]
		float GetWalkSpeedLimitOfMountable(UIntPtr agentPointer);

		// Token: 0x06001681 RID: 5761
		[EngineMethod("create_blood_burst_at_limb", false)]
		void CreateBloodBurstAtLimb(UIntPtr agentPointer, sbyte realBoneIndex, float scale);

		// Token: 0x06001682 RID: 5762
		[EngineMethod("get_native_action_index", false)]
		int GetNativeActionIndex(string actionName);

		// Token: 0x06001683 RID: 5763
		[EngineMethod("set_guarded_agent_index", false)]
		void SetGuardedAgentIndex(UIntPtr agentPointer, int guardedAgentIndex);

		// Token: 0x06001684 RID: 5764
		[EngineMethod("set_columnwise_follow_agent", false)]
		void SetColumnwiseFollowAgent(UIntPtr agentPointer, int followAgentIndex, ref Vec2 followPosition);

		// Token: 0x06001685 RID: 5765
		[EngineMethod("get_monster_usage_index", false)]
		int GetMonsterUsageIndex(string monsterUsage);

		// Token: 0x06001686 RID: 5766
		[EngineMethod("get_missile_range_with_height_difference", false)]
		float GetMissileRangeWithHeightDifference(UIntPtr agentPointer, float targetZ);

		// Token: 0x06001687 RID: 5767
		[EngineMethod("set_formation_no", false)]
		void SetFormationNo(UIntPtr agentPointer, int formationNo);

		// Token: 0x06001688 RID: 5768
		[EngineMethod("enforce_shield_usage", false)]
		void EnforceShieldUsage(UIntPtr agentPointer, Agent.UsageDirection direction);

		// Token: 0x06001689 RID: 5769
		[EngineMethod("set_firing_order", false)]
		void SetFiringOrder(UIntPtr agentPointer, int order);

		// Token: 0x0600168A RID: 5770
		[EngineMethod("set_riding_order", false)]
		void SetRidingOrder(UIntPtr agentPointer, int order);

		// Token: 0x0600168B RID: 5771
		[EngineMethod("get_target_formation_index", false)]
		int GetTargetFormationIndex(UIntPtr agentPointer);

		// Token: 0x0600168C RID: 5772
		[EngineMethod("set_target_formation_index", false)]
		void SetTargetFormationIndex(UIntPtr agentPointer, int targetFormationIndex);

		// Token: 0x0600168D RID: 5773
		[EngineMethod("set_direction_change_tendency", false)]
		void SetDirectionChangeTendency(UIntPtr agentPointer, float tendency);

		// Token: 0x0600168E RID: 5774
		[EngineMethod("set_ai_behavior_params", false)]
		void SetAIBehaviorParams(UIntPtr agentPointer, int behavior, float y1, float x2, float y2, float x3, float y3);

		// Token: 0x0600168F RID: 5775
		[EngineMethod("set_all_ai_behavior_params", false)]
		void SetAllAIBehaviorParams(UIntPtr agentPointer, HumanAIComponent.BehaviorValues[] behaviorParams);

		// Token: 0x06001690 RID: 5776
		[EngineMethod("set_body_armor_material_type", false)]
		void SetBodyArmorMaterialType(UIntPtr agentPointer, ArmorComponent.ArmorMaterialTypes bodyArmorMaterialType);

		// Token: 0x06001691 RID: 5777
		[EngineMethod("get_maximum_number_of_agents", false)]
		int GetMaximumNumberOfAgents();

		// Token: 0x06001692 RID: 5778
		[EngineMethod("get_running_simulation_data_until_maximum_speed_reached", false)]
		void GetRunningSimulationDataUntilMaximumSpeedReached(UIntPtr agentPointer, ref float combatAccelerationTime, ref float maxSpeed, float[] speedValues);

		// Token: 0x06001693 RID: 5779
		[EngineMethod("get_last_target_visibility_state", false)]
		int GetLastTargetVisibilityState(UIntPtr agentPointer);

		// Token: 0x06001694 RID: 5780
		[EngineMethod("get_missile_range", false)]
		float GetMissileRange(UIntPtr agentPointer);
	}
}
