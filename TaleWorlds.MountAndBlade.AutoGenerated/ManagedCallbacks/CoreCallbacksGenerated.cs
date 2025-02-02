using System;
using System.Runtime.InteropServices;
using TaleWorlds.Core;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace ManagedCallbacks
{
	// Token: 0x02000007 RID: 7
	internal static class CoreCallbacksGenerated
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000010 RID: 16 RVA: 0x0000255C File Offset: 0x0000075C
		// (set) Token: 0x06000011 RID: 17 RVA: 0x00002563 File Offset: 0x00000763
		internal static Delegate[] Delegates { get; private set; }

		// Token: 0x06000012 RID: 18 RVA: 0x0000256C File Offset: 0x0000076C
		public static void Initialize()
		{
			CoreCallbacksGenerated.Delegates = new Delegate[104];
			CoreCallbacksGenerated.Delegates[0] = new CoreCallbacksGenerated.Agent_DebugGetHealth_delegate(CoreCallbacksGenerated.Agent_DebugGetHealth);
			CoreCallbacksGenerated.Delegates[1] = new CoreCallbacksGenerated.Agent_GetFormationUnitSpacing_delegate(CoreCallbacksGenerated.Agent_GetFormationUnitSpacing);
			CoreCallbacksGenerated.Delegates[2] = new CoreCallbacksGenerated.Agent_GetMissileRangeWithHeightDifferenceAux_delegate(CoreCallbacksGenerated.Agent_GetMissileRangeWithHeightDifferenceAux);
			CoreCallbacksGenerated.Delegates[3] = new CoreCallbacksGenerated.Agent_GetSoundAndCollisionInfoClassName_delegate(CoreCallbacksGenerated.Agent_GetSoundAndCollisionInfoClassName);
			CoreCallbacksGenerated.Delegates[4] = new CoreCallbacksGenerated.Agent_GetWeaponInaccuracy_delegate(CoreCallbacksGenerated.Agent_GetWeaponInaccuracy);
			CoreCallbacksGenerated.Delegates[5] = new CoreCallbacksGenerated.Agent_IsInSameFormationWith_delegate(CoreCallbacksGenerated.Agent_IsInSameFormationWith);
			CoreCallbacksGenerated.Delegates[6] = new CoreCallbacksGenerated.Agent_OnAgentAlarmedStateChanged_delegate(CoreCallbacksGenerated.Agent_OnAgentAlarmedStateChanged);
			CoreCallbacksGenerated.Delegates[7] = new CoreCallbacksGenerated.Agent_OnDismount_delegate(CoreCallbacksGenerated.Agent_OnDismount);
			CoreCallbacksGenerated.Delegates[8] = new CoreCallbacksGenerated.Agent_OnMount_delegate(CoreCallbacksGenerated.Agent_OnMount);
			CoreCallbacksGenerated.Delegates[9] = new CoreCallbacksGenerated.Agent_OnRemoveWeapon_delegate(CoreCallbacksGenerated.Agent_OnRemoveWeapon);
			CoreCallbacksGenerated.Delegates[10] = new CoreCallbacksGenerated.Agent_OnRetreating_delegate(CoreCallbacksGenerated.Agent_OnRetreating);
			CoreCallbacksGenerated.Delegates[11] = new CoreCallbacksGenerated.Agent_OnShieldDamaged_delegate(CoreCallbacksGenerated.Agent_OnShieldDamaged);
			CoreCallbacksGenerated.Delegates[12] = new CoreCallbacksGenerated.Agent_OnWeaponAmmoConsume_delegate(CoreCallbacksGenerated.Agent_OnWeaponAmmoConsume);
			CoreCallbacksGenerated.Delegates[13] = new CoreCallbacksGenerated.Agent_OnWeaponAmmoReload_delegate(CoreCallbacksGenerated.Agent_OnWeaponAmmoReload);
			CoreCallbacksGenerated.Delegates[14] = new CoreCallbacksGenerated.Agent_OnWeaponAmmoRemoved_delegate(CoreCallbacksGenerated.Agent_OnWeaponAmmoRemoved);
			CoreCallbacksGenerated.Delegates[15] = new CoreCallbacksGenerated.Agent_OnWeaponAmountChange_delegate(CoreCallbacksGenerated.Agent_OnWeaponAmountChange);
			CoreCallbacksGenerated.Delegates[16] = new CoreCallbacksGenerated.Agent_OnWeaponReloadPhaseChange_delegate(CoreCallbacksGenerated.Agent_OnWeaponReloadPhaseChange);
			CoreCallbacksGenerated.Delegates[17] = new CoreCallbacksGenerated.Agent_OnWeaponSwitchingToAlternativeStart_delegate(CoreCallbacksGenerated.Agent_OnWeaponSwitchingToAlternativeStart);
			CoreCallbacksGenerated.Delegates[18] = new CoreCallbacksGenerated.Agent_OnWeaponUsageIndexChange_delegate(CoreCallbacksGenerated.Agent_OnWeaponUsageIndexChange);
			CoreCallbacksGenerated.Delegates[19] = new CoreCallbacksGenerated.Agent_OnWieldedItemIndexChange_delegate(CoreCallbacksGenerated.Agent_OnWieldedItemIndexChange);
			CoreCallbacksGenerated.Delegates[20] = new CoreCallbacksGenerated.Agent_SetAgentAIPerformingRetreatBehavior_delegate(CoreCallbacksGenerated.Agent_SetAgentAIPerformingRetreatBehavior);
			CoreCallbacksGenerated.Delegates[21] = new CoreCallbacksGenerated.Agent_UpdateAgentStats_delegate(CoreCallbacksGenerated.Agent_UpdateAgentStats);
			CoreCallbacksGenerated.Delegates[22] = new CoreCallbacksGenerated.Agent_UpdateMountAgentCache_delegate(CoreCallbacksGenerated.Agent_UpdateMountAgentCache);
			CoreCallbacksGenerated.Delegates[23] = new CoreCallbacksGenerated.Agent_UpdateRiderAgentCache_delegate(CoreCallbacksGenerated.Agent_UpdateRiderAgentCache);
			CoreCallbacksGenerated.Delegates[24] = new CoreCallbacksGenerated.BannerlordTableauManager_RegisterCharacterTableauScene_delegate(CoreCallbacksGenerated.BannerlordTableauManager_RegisterCharacterTableauScene);
			CoreCallbacksGenerated.Delegates[25] = new CoreCallbacksGenerated.BannerlordTableauManager_RequestCharacterTableauSetup_delegate(CoreCallbacksGenerated.BannerlordTableauManager_RequestCharacterTableauSetup);
			CoreCallbacksGenerated.Delegates[26] = new CoreCallbacksGenerated.CoreManaged_CheckSharedStructureSizes_delegate(CoreCallbacksGenerated.CoreManaged_CheckSharedStructureSizes);
			CoreCallbacksGenerated.Delegates[27] = new CoreCallbacksGenerated.CoreManaged_EngineApiMethodInterfaceInitializer_delegate(CoreCallbacksGenerated.CoreManaged_EngineApiMethodInterfaceInitializer);
			CoreCallbacksGenerated.Delegates[28] = new CoreCallbacksGenerated.CoreManaged_FillEngineApiPointers_delegate(CoreCallbacksGenerated.CoreManaged_FillEngineApiPointers);
			CoreCallbacksGenerated.Delegates[29] = new CoreCallbacksGenerated.CoreManaged_Finalize_delegate(CoreCallbacksGenerated.CoreManaged_Finalize);
			CoreCallbacksGenerated.Delegates[30] = new CoreCallbacksGenerated.CoreManaged_OnLoadCommonFinished_delegate(CoreCallbacksGenerated.CoreManaged_OnLoadCommonFinished);
			CoreCallbacksGenerated.Delegates[31] = new CoreCallbacksGenerated.CoreManaged_Start_delegate(CoreCallbacksGenerated.CoreManaged_Start);
			CoreCallbacksGenerated.Delegates[32] = new CoreCallbacksGenerated.GameNetwork_HandleConsoleCommand_delegate(CoreCallbacksGenerated.GameNetwork_HandleConsoleCommand);
			CoreCallbacksGenerated.Delegates[33] = new CoreCallbacksGenerated.GameNetwork_HandleDisconnect_delegate(CoreCallbacksGenerated.GameNetwork_HandleDisconnect);
			CoreCallbacksGenerated.Delegates[34] = new CoreCallbacksGenerated.GameNetwork_HandleNetworkPacketAsClient_delegate(CoreCallbacksGenerated.GameNetwork_HandleNetworkPacketAsClient);
			CoreCallbacksGenerated.Delegates[35] = new CoreCallbacksGenerated.GameNetwork_HandleNetworkPacketAsServer_delegate(CoreCallbacksGenerated.GameNetwork_HandleNetworkPacketAsServer);
			CoreCallbacksGenerated.Delegates[36] = new CoreCallbacksGenerated.GameNetwork_HandleRemovePlayer_delegate(CoreCallbacksGenerated.GameNetwork_HandleRemovePlayer);
			CoreCallbacksGenerated.Delegates[37] = new CoreCallbacksGenerated.GameNetwork_SyncRelevantGameOptionsToServer_delegate(CoreCallbacksGenerated.GameNetwork_SyncRelevantGameOptionsToServer);
			CoreCallbacksGenerated.Delegates[38] = new CoreCallbacksGenerated.ManagedOptions_GetConfigCount_delegate(CoreCallbacksGenerated.ManagedOptions_GetConfigCount);
			CoreCallbacksGenerated.Delegates[39] = new CoreCallbacksGenerated.ManagedOptions_GetConfigValue_delegate(CoreCallbacksGenerated.ManagedOptions_GetConfigValue);
			CoreCallbacksGenerated.Delegates[40] = new CoreCallbacksGenerated.MBEditor_CloseEditorScene_delegate(CoreCallbacksGenerated.MBEditor_CloseEditorScene);
			CoreCallbacksGenerated.Delegates[41] = new CoreCallbacksGenerated.MBEditor_DestroyEditor_delegate(CoreCallbacksGenerated.MBEditor_DestroyEditor);
			CoreCallbacksGenerated.Delegates[42] = new CoreCallbacksGenerated.MBEditor_SetEditorScene_delegate(CoreCallbacksGenerated.MBEditor_SetEditorScene);
			CoreCallbacksGenerated.Delegates[43] = new CoreCallbacksGenerated.MBMultiplayerData_GetCurrentPlayerCount_delegate(CoreCallbacksGenerated.MBMultiplayerData_GetCurrentPlayerCount);
			CoreCallbacksGenerated.Delegates[44] = new CoreCallbacksGenerated.MBMultiplayerData_GetGameModule_delegate(CoreCallbacksGenerated.MBMultiplayerData_GetGameModule);
			CoreCallbacksGenerated.Delegates[45] = new CoreCallbacksGenerated.MBMultiplayerData_GetGameType_delegate(CoreCallbacksGenerated.MBMultiplayerData_GetGameType);
			CoreCallbacksGenerated.Delegates[46] = new CoreCallbacksGenerated.MBMultiplayerData_GetMap_delegate(CoreCallbacksGenerated.MBMultiplayerData_GetMap);
			CoreCallbacksGenerated.Delegates[47] = new CoreCallbacksGenerated.MBMultiplayerData_GetPlayerCountLimit_delegate(CoreCallbacksGenerated.MBMultiplayerData_GetPlayerCountLimit);
			CoreCallbacksGenerated.Delegates[48] = new CoreCallbacksGenerated.MBMultiplayerData_GetServerId_delegate(CoreCallbacksGenerated.MBMultiplayerData_GetServerId);
			CoreCallbacksGenerated.Delegates[49] = new CoreCallbacksGenerated.MBMultiplayerData_GetServerName_delegate(CoreCallbacksGenerated.MBMultiplayerData_GetServerName);
			CoreCallbacksGenerated.Delegates[50] = new CoreCallbacksGenerated.MBMultiplayerData_UpdateGameServerInfo_delegate(CoreCallbacksGenerated.MBMultiplayerData_UpdateGameServerInfo);
			CoreCallbacksGenerated.Delegates[51] = new CoreCallbacksGenerated.Mission_ApplySkeletonScaleToAllEquippedItems_delegate(CoreCallbacksGenerated.Mission_ApplySkeletonScaleToAllEquippedItems);
			CoreCallbacksGenerated.Delegates[52] = new CoreCallbacksGenerated.Mission_ChargeDamageCallback_delegate(CoreCallbacksGenerated.Mission_ChargeDamageCallback);
			CoreCallbacksGenerated.Delegates[53] = new CoreCallbacksGenerated.Mission_DebugLogNativeMissionNetworkEvent_delegate(CoreCallbacksGenerated.Mission_DebugLogNativeMissionNetworkEvent);
			CoreCallbacksGenerated.Delegates[54] = new CoreCallbacksGenerated.Mission_EndMission_delegate(CoreCallbacksGenerated.Mission_EndMission);
			CoreCallbacksGenerated.Delegates[55] = new CoreCallbacksGenerated.Mission_FallDamageCallback_delegate(CoreCallbacksGenerated.Mission_FallDamageCallback);
			CoreCallbacksGenerated.Delegates[56] = new CoreCallbacksGenerated.Mission_GetAgentState_delegate(CoreCallbacksGenerated.Mission_GetAgentState);
			CoreCallbacksGenerated.Delegates[57] = new CoreCallbacksGenerated.Mission_GetClosestFleePositionForAgent_delegate(CoreCallbacksGenerated.Mission_GetClosestFleePositionForAgent);
			CoreCallbacksGenerated.Delegates[58] = new CoreCallbacksGenerated.Mission_GetDefendCollisionResults_delegate(CoreCallbacksGenerated.Mission_GetDefendCollisionResults);
			CoreCallbacksGenerated.Delegates[59] = new CoreCallbacksGenerated.Mission_MeleeHitCallback_delegate(CoreCallbacksGenerated.Mission_MeleeHitCallback);
			CoreCallbacksGenerated.Delegates[60] = new CoreCallbacksGenerated.Mission_MissileAreaDamageCallback_delegate(CoreCallbacksGenerated.Mission_MissileAreaDamageCallback);
			CoreCallbacksGenerated.Delegates[61] = new CoreCallbacksGenerated.Mission_MissileCalculatePassbySoundParametersCallbackMT_delegate(CoreCallbacksGenerated.Mission_MissileCalculatePassbySoundParametersCallbackMT);
			CoreCallbacksGenerated.Delegates[62] = new CoreCallbacksGenerated.Mission_MissileHitCallback_delegate(CoreCallbacksGenerated.Mission_MissileHitCallback);
			CoreCallbacksGenerated.Delegates[63] = new CoreCallbacksGenerated.Mission_OnAgentAddedAsCorpse_delegate(CoreCallbacksGenerated.Mission_OnAgentAddedAsCorpse);
			CoreCallbacksGenerated.Delegates[64] = new CoreCallbacksGenerated.Mission_OnAgentDeleted_delegate(CoreCallbacksGenerated.Mission_OnAgentDeleted);
			CoreCallbacksGenerated.Delegates[65] = new CoreCallbacksGenerated.Mission_OnAgentHitBlocked_delegate(CoreCallbacksGenerated.Mission_OnAgentHitBlocked);
			CoreCallbacksGenerated.Delegates[66] = new CoreCallbacksGenerated.Mission_OnAgentRemoved_delegate(CoreCallbacksGenerated.Mission_OnAgentRemoved);
			CoreCallbacksGenerated.Delegates[67] = new CoreCallbacksGenerated.Mission_OnAgentShootMissile_delegate(CoreCallbacksGenerated.Mission_OnAgentShootMissile);
			CoreCallbacksGenerated.Delegates[68] = new CoreCallbacksGenerated.Mission_OnMissileRemoved_delegate(CoreCallbacksGenerated.Mission_OnMissileRemoved);
			CoreCallbacksGenerated.Delegates[69] = new CoreCallbacksGenerated.Mission_OnPreTick_delegate(CoreCallbacksGenerated.Mission_OnPreTick);
			CoreCallbacksGenerated.Delegates[70] = new CoreCallbacksGenerated.Mission_OnSceneCreated_delegate(CoreCallbacksGenerated.Mission_OnSceneCreated);
			CoreCallbacksGenerated.Delegates[71] = new CoreCallbacksGenerated.Mission_PauseMission_delegate(CoreCallbacksGenerated.Mission_PauseMission);
			CoreCallbacksGenerated.Delegates[72] = new CoreCallbacksGenerated.Mission_ResetMission_delegate(CoreCallbacksGenerated.Mission_ResetMission);
			CoreCallbacksGenerated.Delegates[73] = new CoreCallbacksGenerated.Mission_SpawnWeaponAsDropFromAgent_delegate(CoreCallbacksGenerated.Mission_SpawnWeaponAsDropFromAgent);
			CoreCallbacksGenerated.Delegates[74] = new CoreCallbacksGenerated.Mission_TickAgentsAndTeams_delegate(CoreCallbacksGenerated.Mission_TickAgentsAndTeams);
			CoreCallbacksGenerated.Delegates[75] = new CoreCallbacksGenerated.Mission_UpdateMissionTimeCache_delegate(CoreCallbacksGenerated.Mission_UpdateMissionTimeCache);
			CoreCallbacksGenerated.Delegates[76] = new CoreCallbacksGenerated.Module_CreateProcessedActionSetsXMLForNative_delegate(CoreCallbacksGenerated.Module_CreateProcessedActionSetsXMLForNative);
			CoreCallbacksGenerated.Delegates[77] = new CoreCallbacksGenerated.Module_CreateProcessedActionTypesXMLForNative_delegate(CoreCallbacksGenerated.Module_CreateProcessedActionTypesXMLForNative);
			CoreCallbacksGenerated.Delegates[78] = new CoreCallbacksGenerated.Module_CreateProcessedAnimationsXMLForNative_delegate(CoreCallbacksGenerated.Module_CreateProcessedAnimationsXMLForNative);
			CoreCallbacksGenerated.Delegates[79] = new CoreCallbacksGenerated.Module_CreateProcessedModuleDataXMLForNative_delegate(CoreCallbacksGenerated.Module_CreateProcessedModuleDataXMLForNative);
			CoreCallbacksGenerated.Delegates[80] = new CoreCallbacksGenerated.Module_CreateProcessedSkinsXMLForNative_delegate(CoreCallbacksGenerated.Module_CreateProcessedSkinsXMLForNative);
			CoreCallbacksGenerated.Delegates[81] = new CoreCallbacksGenerated.Module_CreateProcessedVoiceDefinitionsXMLForNative_delegate(CoreCallbacksGenerated.Module_CreateProcessedVoiceDefinitionsXMLForNative);
			CoreCallbacksGenerated.Delegates[82] = new CoreCallbacksGenerated.Module_GetGameStatus_delegate(CoreCallbacksGenerated.Module_GetGameStatus);
			CoreCallbacksGenerated.Delegates[83] = new CoreCallbacksGenerated.Module_GetHorseMaterialNames_delegate(CoreCallbacksGenerated.Module_GetHorseMaterialNames);
			CoreCallbacksGenerated.Delegates[84] = new CoreCallbacksGenerated.Module_GetInstance_delegate(CoreCallbacksGenerated.Module_GetInstance);
			CoreCallbacksGenerated.Delegates[85] = new CoreCallbacksGenerated.Module_GetItemMeshNames_delegate(CoreCallbacksGenerated.Module_GetItemMeshNames);
			CoreCallbacksGenerated.Delegates[86] = new CoreCallbacksGenerated.Module_GetMetaMeshPackageMapping_delegate(CoreCallbacksGenerated.Module_GetMetaMeshPackageMapping);
			CoreCallbacksGenerated.Delegates[87] = new CoreCallbacksGenerated.Module_GetMissionControllerClassNames_delegate(CoreCallbacksGenerated.Module_GetMissionControllerClassNames);
			CoreCallbacksGenerated.Delegates[88] = new CoreCallbacksGenerated.Module_Initialize_delegate(CoreCallbacksGenerated.Module_Initialize);
			CoreCallbacksGenerated.Delegates[89] = new CoreCallbacksGenerated.Module_MBThrowException_delegate(CoreCallbacksGenerated.Module_MBThrowException);
			CoreCallbacksGenerated.Delegates[90] = new CoreCallbacksGenerated.Module_OnCloseSceneEditorPresentation_delegate(CoreCallbacksGenerated.Module_OnCloseSceneEditorPresentation);
			CoreCallbacksGenerated.Delegates[91] = new CoreCallbacksGenerated.Module_OnDumpCreated_delegate(CoreCallbacksGenerated.Module_OnDumpCreated);
			CoreCallbacksGenerated.Delegates[92] = new CoreCallbacksGenerated.Module_OnDumpCreationStarted_delegate(CoreCallbacksGenerated.Module_OnDumpCreationStarted);
			CoreCallbacksGenerated.Delegates[93] = new CoreCallbacksGenerated.Module_OnEnterEditMode_delegate(CoreCallbacksGenerated.Module_OnEnterEditMode);
			CoreCallbacksGenerated.Delegates[94] = new CoreCallbacksGenerated.Module_OnImguiProfilerTick_delegate(CoreCallbacksGenerated.Module_OnImguiProfilerTick);
			CoreCallbacksGenerated.Delegates[95] = new CoreCallbacksGenerated.Module_OnSceneEditorModeOver_delegate(CoreCallbacksGenerated.Module_OnSceneEditorModeOver);
			CoreCallbacksGenerated.Delegates[96] = new CoreCallbacksGenerated.Module_OnSkinsXMLHasChanged_delegate(CoreCallbacksGenerated.Module_OnSkinsXMLHasChanged);
			CoreCallbacksGenerated.Delegates[97] = new CoreCallbacksGenerated.Module_RunTest_delegate(CoreCallbacksGenerated.Module_RunTest);
			CoreCallbacksGenerated.Delegates[98] = new CoreCallbacksGenerated.Module_SetEditorScreenAsRootScreen_delegate(CoreCallbacksGenerated.Module_SetEditorScreenAsRootScreen);
			CoreCallbacksGenerated.Delegates[99] = new CoreCallbacksGenerated.Module_SetLoadingFinished_delegate(CoreCallbacksGenerated.Module_SetLoadingFinished);
			CoreCallbacksGenerated.Delegates[100] = new CoreCallbacksGenerated.Module_StartMissionForEditor_delegate(CoreCallbacksGenerated.Module_StartMissionForEditor);
			CoreCallbacksGenerated.Delegates[101] = new CoreCallbacksGenerated.Module_StartMissionForReplayEditor_delegate(CoreCallbacksGenerated.Module_StartMissionForReplayEditor);
			CoreCallbacksGenerated.Delegates[102] = new CoreCallbacksGenerated.Module_TickTest_delegate(CoreCallbacksGenerated.Module_TickTest);
			CoreCallbacksGenerated.Delegates[103] = new CoreCallbacksGenerated.WeaponComponentMissionExtensions_CalculateCenterOfMass_delegate(CoreCallbacksGenerated.WeaponComponentMissionExtensions_CalculateCenterOfMass);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002D9C File Offset: 0x00000F9C
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Agent_DebugGetHealth_delegate))]
		internal static float Agent_DebugGetHealth(int thisPointer)
		{
			return (DotNetObject.GetManagedObjectWithId(thisPointer) as Agent).DebugGetHealth();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002DAE File Offset: 0x00000FAE
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Agent_GetFormationUnitSpacing_delegate))]
		internal static int Agent_GetFormationUnitSpacing(int thisPointer)
		{
			return (DotNetObject.GetManagedObjectWithId(thisPointer) as Agent).GetFormationUnitSpacing();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002DC0 File Offset: 0x00000FC0
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Agent_GetMissileRangeWithHeightDifferenceAux_delegate))]
		internal static float Agent_GetMissileRangeWithHeightDifferenceAux(int thisPointer, float targetZ)
		{
			return (DotNetObject.GetManagedObjectWithId(thisPointer) as Agent).GetMissileRangeWithHeightDifferenceAux(targetZ);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002DD4 File Offset: 0x00000FD4
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Agent_GetSoundAndCollisionInfoClassName_delegate))]
		internal static UIntPtr Agent_GetSoundAndCollisionInfoClassName(int thisPointer)
		{
			string soundAndCollisionInfoClassName = (DotNetObject.GetManagedObjectWithId(thisPointer) as Agent).GetSoundAndCollisionInfoClassName();
			UIntPtr threadLocalCachedRglVarString = NativeStringHelper.GetThreadLocalCachedRglVarString();
			NativeStringHelper.SetRglVarString(threadLocalCachedRglVarString, soundAndCollisionInfoClassName);
			return threadLocalCachedRglVarString;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002DFE File Offset: 0x00000FFE
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Agent_GetWeaponInaccuracy_delegate))]
		internal static float Agent_GetWeaponInaccuracy(int thisPointer, EquipmentIndex weaponSlotIndex, int weaponUsageIndex)
		{
			return (DotNetObject.GetManagedObjectWithId(thisPointer) as Agent).GetWeaponInaccuracy(weaponSlotIndex, weaponUsageIndex);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002E14 File Offset: 0x00001014
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Agent_IsInSameFormationWith_delegate))]
		internal static bool Agent_IsInSameFormationWith(int thisPointer, int otherAgent)
		{
			Agent agent = DotNetObject.GetManagedObjectWithId(thisPointer) as Agent;
			Agent otherAgent2 = DotNetObject.GetManagedObjectWithId(otherAgent) as Agent;
			return agent.IsInSameFormationWith(otherAgent2);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002E3E File Offset: 0x0000103E
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Agent_OnAgentAlarmedStateChanged_delegate))]
		internal static void Agent_OnAgentAlarmedStateChanged(int thisPointer, Agent.AIStateFlag flag)
		{
			(DotNetObject.GetManagedObjectWithId(thisPointer) as Agent).OnAgentAlarmedStateChanged(flag);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002E54 File Offset: 0x00001054
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Agent_OnDismount_delegate))]
		internal static void Agent_OnDismount(int thisPointer, int mount)
		{
			Agent agent = DotNetObject.GetManagedObjectWithId(thisPointer) as Agent;
			Agent mount2 = DotNetObject.GetManagedObjectWithId(mount) as Agent;
			agent.OnDismount(mount2);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002E80 File Offset: 0x00001080
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Agent_OnMount_delegate))]
		internal static void Agent_OnMount(int thisPointer, int mount)
		{
			Agent agent = DotNetObject.GetManagedObjectWithId(thisPointer) as Agent;
			Agent mount2 = DotNetObject.GetManagedObjectWithId(mount) as Agent;
			agent.OnMount(mount2);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002EAA File Offset: 0x000010AA
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Agent_OnRemoveWeapon_delegate))]
		internal static void Agent_OnRemoveWeapon(int thisPointer, EquipmentIndex slotIndex)
		{
			(DotNetObject.GetManagedObjectWithId(thisPointer) as Agent).OnRemoveWeapon(slotIndex);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002EBD File Offset: 0x000010BD
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Agent_OnRetreating_delegate))]
		internal static void Agent_OnRetreating(int thisPointer)
		{
			(DotNetObject.GetManagedObjectWithId(thisPointer) as Agent).OnRetreating();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002ECF File Offset: 0x000010CF
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Agent_OnShieldDamaged_delegate))]
		internal static void Agent_OnShieldDamaged(int thisPointer, EquipmentIndex slotIndex, int inflictedDamage)
		{
			(DotNetObject.GetManagedObjectWithId(thisPointer) as Agent).OnShieldDamaged(slotIndex, inflictedDamage);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002EE3 File Offset: 0x000010E3
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Agent_OnWeaponAmmoConsume_delegate))]
		internal static void Agent_OnWeaponAmmoConsume(int thisPointer, EquipmentIndex slotIndex, short totalAmmo)
		{
			(DotNetObject.GetManagedObjectWithId(thisPointer) as Agent).OnWeaponAmmoConsume(slotIndex, totalAmmo);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002EF7 File Offset: 0x000010F7
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Agent_OnWeaponAmmoReload_delegate))]
		internal static void Agent_OnWeaponAmmoReload(int thisPointer, EquipmentIndex slotIndex, EquipmentIndex ammoSlotIndex, short totalAmmo)
		{
			(DotNetObject.GetManagedObjectWithId(thisPointer) as Agent).OnWeaponAmmoReload(slotIndex, ammoSlotIndex, totalAmmo);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002F0C File Offset: 0x0000110C
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Agent_OnWeaponAmmoRemoved_delegate))]
		internal static void Agent_OnWeaponAmmoRemoved(int thisPointer, EquipmentIndex slotIndex)
		{
			(DotNetObject.GetManagedObjectWithId(thisPointer) as Agent).OnWeaponAmmoRemoved(slotIndex);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002F1F File Offset: 0x0000111F
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Agent_OnWeaponAmountChange_delegate))]
		internal static void Agent_OnWeaponAmountChange(int thisPointer, EquipmentIndex slotIndex, short amount)
		{
			(DotNetObject.GetManagedObjectWithId(thisPointer) as Agent).OnWeaponAmountChange(slotIndex, amount);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002F33 File Offset: 0x00001133
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Agent_OnWeaponReloadPhaseChange_delegate))]
		internal static void Agent_OnWeaponReloadPhaseChange(int thisPointer, EquipmentIndex slotIndex, short reloadPhase)
		{
			(DotNetObject.GetManagedObjectWithId(thisPointer) as Agent).OnWeaponReloadPhaseChange(slotIndex, reloadPhase);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002F47 File Offset: 0x00001147
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Agent_OnWeaponSwitchingToAlternativeStart_delegate))]
		internal static void Agent_OnWeaponSwitchingToAlternativeStart(int thisPointer, EquipmentIndex slotIndex, int usageIndex)
		{
			(DotNetObject.GetManagedObjectWithId(thisPointer) as Agent).OnWeaponSwitchingToAlternativeStart(slotIndex, usageIndex);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002F5B File Offset: 0x0000115B
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Agent_OnWeaponUsageIndexChange_delegate))]
		internal static void Agent_OnWeaponUsageIndexChange(int thisPointer, EquipmentIndex slotIndex, int usageIndex)
		{
			(DotNetObject.GetManagedObjectWithId(thisPointer) as Agent).OnWeaponUsageIndexChange(slotIndex, usageIndex);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002F6F File Offset: 0x0000116F
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Agent_OnWieldedItemIndexChange_delegate))]
		internal static void Agent_OnWieldedItemIndexChange(int thisPointer, bool isOffHand, bool isWieldedInstantly, bool isWieldedOnSpawn)
		{
			(DotNetObject.GetManagedObjectWithId(thisPointer) as Agent).OnWieldedItemIndexChange(isOffHand, isWieldedInstantly, isWieldedOnSpawn);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002F84 File Offset: 0x00001184
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Agent_SetAgentAIPerformingRetreatBehavior_delegate))]
		internal static void Agent_SetAgentAIPerformingRetreatBehavior(int thisPointer, bool isAgentAIPerformingRetreatBehavior)
		{
			(DotNetObject.GetManagedObjectWithId(thisPointer) as Agent).SetAgentAIPerformingRetreatBehavior(isAgentAIPerformingRetreatBehavior);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002F97 File Offset: 0x00001197
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Agent_UpdateAgentStats_delegate))]
		internal static void Agent_UpdateAgentStats(int thisPointer)
		{
			(DotNetObject.GetManagedObjectWithId(thisPointer) as Agent).UpdateAgentStats();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002FAC File Offset: 0x000011AC
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Agent_UpdateMountAgentCache_delegate))]
		internal static void Agent_UpdateMountAgentCache(int thisPointer, int newMountAgent)
		{
			Agent agent = DotNetObject.GetManagedObjectWithId(thisPointer) as Agent;
			Agent newMountAgent2 = DotNetObject.GetManagedObjectWithId(newMountAgent) as Agent;
			agent.UpdateMountAgentCache(newMountAgent2);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002FD8 File Offset: 0x000011D8
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Agent_UpdateRiderAgentCache_delegate))]
		internal static void Agent_UpdateRiderAgentCache(int thisPointer, int newRiderAgent)
		{
			Agent agent = DotNetObject.GetManagedObjectWithId(thisPointer) as Agent;
			Agent newRiderAgent2 = DotNetObject.GetManagedObjectWithId(newRiderAgent) as Agent;
			agent.UpdateRiderAgentCache(newRiderAgent2);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00003004 File Offset: 0x00001204
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.BannerlordTableauManager_RegisterCharacterTableauScene_delegate))]
		internal static void BannerlordTableauManager_RegisterCharacterTableauScene(NativeObjectPointer scene, int type)
		{
			Scene scene2 = null;
			if (scene.Pointer != UIntPtr.Zero)
			{
				scene2 = new Scene(scene.Pointer);
			}
			BannerlordTableauManager.RegisterCharacterTableauScene(scene2, type);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003038 File Offset: 0x00001238
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.BannerlordTableauManager_RequestCharacterTableauSetup_delegate))]
		internal static void BannerlordTableauManager_RequestCharacterTableauSetup(int characterCodeId, NativeObjectPointer scene, NativeObjectPointer poseEntity)
		{
			Scene scene2 = null;
			if (scene.Pointer != UIntPtr.Zero)
			{
				scene2 = new Scene(scene.Pointer);
			}
			GameEntity poseEntity2 = null;
			if (poseEntity.Pointer != UIntPtr.Zero)
			{
				poseEntity2 = new GameEntity(poseEntity.Pointer);
			}
			BannerlordTableauManager.RequestCharacterTableauSetup(characterCodeId, scene2, poseEntity2);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000308D File Offset: 0x0000128D
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.CoreManaged_CheckSharedStructureSizes_delegate))]
		internal static void CoreManaged_CheckSharedStructureSizes()
		{
			CoreManaged.CheckSharedStructureSizes();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00003094 File Offset: 0x00001294
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.CoreManaged_EngineApiMethodInterfaceInitializer_delegate))]
		internal static void CoreManaged_EngineApiMethodInterfaceInitializer(int id, IntPtr pointer)
		{
			CoreManaged.EngineApiMethodInterfaceInitializer(id, pointer);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000309D File Offset: 0x0000129D
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.CoreManaged_FillEngineApiPointers_delegate))]
		internal static void CoreManaged_FillEngineApiPointers()
		{
			CoreManaged.FillEngineApiPointers();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000030A4 File Offset: 0x000012A4
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.CoreManaged_Finalize_delegate))]
		internal static void CoreManaged_Finalize()
		{
			CoreManaged.Finalize();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000030AB File Offset: 0x000012AB
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.CoreManaged_OnLoadCommonFinished_delegate))]
		internal static void CoreManaged_OnLoadCommonFinished()
		{
			CoreManaged.OnLoadCommonFinished();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000030B2 File Offset: 0x000012B2
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.CoreManaged_Start_delegate))]
		internal static void CoreManaged_Start()
		{
			CoreManaged.Start();
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000030B9 File Offset: 0x000012B9
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.GameNetwork_HandleConsoleCommand_delegate))]
		internal static void GameNetwork_HandleConsoleCommand(IntPtr command)
		{
			GameNetwork.HandleConsoleCommand(Marshal.PtrToStringAnsi(command));
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000030C6 File Offset: 0x000012C6
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.GameNetwork_HandleDisconnect_delegate))]
		internal static void GameNetwork_HandleDisconnect()
		{
			GameNetwork.HandleDisconnect();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000030CD File Offset: 0x000012CD
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.GameNetwork_HandleNetworkPacketAsClient_delegate))]
		internal static bool GameNetwork_HandleNetworkPacketAsClient()
		{
			return GameNetwork.HandleNetworkPacketAsClient();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000030D4 File Offset: 0x000012D4
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.GameNetwork_HandleNetworkPacketAsServer_delegate))]
		internal static bool GameNetwork_HandleNetworkPacketAsServer(int networkPeer)
		{
			return GameNetwork.HandleNetworkPacketAsServer(DotNetObject.GetManagedObjectWithId(networkPeer) as MBNetworkPeer);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000030E6 File Offset: 0x000012E6
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.GameNetwork_HandleRemovePlayer_delegate))]
		internal static void GameNetwork_HandleRemovePlayer(int peer, bool isTimedOut)
		{
			GameNetwork.HandleRemovePlayer(DotNetObject.GetManagedObjectWithId(peer) as MBNetworkPeer, isTimedOut);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000030F9 File Offset: 0x000012F9
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.GameNetwork_SyncRelevantGameOptionsToServer_delegate))]
		internal static void GameNetwork_SyncRelevantGameOptionsToServer()
		{
			GameNetwork.SyncRelevantGameOptionsToServer();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003100 File Offset: 0x00001300
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.ManagedOptions_GetConfigCount_delegate))]
		internal static int ManagedOptions_GetConfigCount()
		{
			return ManagedOptions.GetConfigCount();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003107 File Offset: 0x00001307
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.ManagedOptions_GetConfigValue_delegate))]
		internal static float ManagedOptions_GetConfigValue(int type)
		{
			return ManagedOptions.GetConfigValue(type);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000310F File Offset: 0x0000130F
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.MBEditor_CloseEditorScene_delegate))]
		internal static void MBEditor_CloseEditorScene()
		{
			MBEditor.CloseEditorScene();
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003118 File Offset: 0x00001318
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.MBEditor_DestroyEditor_delegate))]
		internal static void MBEditor_DestroyEditor(NativeObjectPointer scene)
		{
			Scene scene2 = null;
			if (scene.Pointer != UIntPtr.Zero)
			{
				scene2 = new Scene(scene.Pointer);
			}
			MBEditor.DestroyEditor(scene2);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000314C File Offset: 0x0000134C
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.MBEditor_SetEditorScene_delegate))]
		internal static void MBEditor_SetEditorScene(NativeObjectPointer scene)
		{
			Scene editorScene = null;
			if (scene.Pointer != UIntPtr.Zero)
			{
				editorScene = new Scene(scene.Pointer);
			}
			MBEditor.SetEditorScene(editorScene);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000317F File Offset: 0x0000137F
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.MBMultiplayerData_GetCurrentPlayerCount_delegate))]
		internal static int MBMultiplayerData_GetCurrentPlayerCount()
		{
			return MBMultiplayerData.GetCurrentPlayerCount();
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003188 File Offset: 0x00001388
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.MBMultiplayerData_GetGameModule_delegate))]
		internal static UIntPtr MBMultiplayerData_GetGameModule()
		{
			string gameModule = MBMultiplayerData.GetGameModule();
			UIntPtr threadLocalCachedRglVarString = NativeStringHelper.GetThreadLocalCachedRglVarString();
			NativeStringHelper.SetRglVarString(threadLocalCachedRglVarString, gameModule);
			return threadLocalCachedRglVarString;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000031A8 File Offset: 0x000013A8
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.MBMultiplayerData_GetGameType_delegate))]
		internal static UIntPtr MBMultiplayerData_GetGameType()
		{
			string gameType = MBMultiplayerData.GetGameType();
			UIntPtr threadLocalCachedRglVarString = NativeStringHelper.GetThreadLocalCachedRglVarString();
			NativeStringHelper.SetRglVarString(threadLocalCachedRglVarString, gameType);
			return threadLocalCachedRglVarString;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000031C8 File Offset: 0x000013C8
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.MBMultiplayerData_GetMap_delegate))]
		internal static UIntPtr MBMultiplayerData_GetMap()
		{
			string map = MBMultiplayerData.GetMap();
			UIntPtr threadLocalCachedRglVarString = NativeStringHelper.GetThreadLocalCachedRglVarString();
			NativeStringHelper.SetRglVarString(threadLocalCachedRglVarString, map);
			return threadLocalCachedRglVarString;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000031E7 File Offset: 0x000013E7
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.MBMultiplayerData_GetPlayerCountLimit_delegate))]
		internal static int MBMultiplayerData_GetPlayerCountLimit()
		{
			return MBMultiplayerData.GetPlayerCountLimit();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000031F0 File Offset: 0x000013F0
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.MBMultiplayerData_GetServerId_delegate))]
		internal static UIntPtr MBMultiplayerData_GetServerId()
		{
			string serverId = MBMultiplayerData.GetServerId();
			UIntPtr threadLocalCachedRglVarString = NativeStringHelper.GetThreadLocalCachedRglVarString();
			NativeStringHelper.SetRglVarString(threadLocalCachedRglVarString, serverId);
			return threadLocalCachedRglVarString;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003210 File Offset: 0x00001410
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.MBMultiplayerData_GetServerName_delegate))]
		internal static UIntPtr MBMultiplayerData_GetServerName()
		{
			string serverName = MBMultiplayerData.GetServerName();
			UIntPtr threadLocalCachedRglVarString = NativeStringHelper.GetThreadLocalCachedRglVarString();
			NativeStringHelper.SetRglVarString(threadLocalCachedRglVarString, serverName);
			return threadLocalCachedRglVarString;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003230 File Offset: 0x00001430
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.MBMultiplayerData_UpdateGameServerInfo_delegate))]
		internal static void MBMultiplayerData_UpdateGameServerInfo(IntPtr id, IntPtr gameServer, IntPtr gameModule, IntPtr gameType, IntPtr map, int currentPlayerCount, int maxPlayerCount, IntPtr address, int port)
		{
			string id2 = Marshal.PtrToStringAnsi(id);
			string gameServer2 = Marshal.PtrToStringAnsi(gameServer);
			string gameModule2 = Marshal.PtrToStringAnsi(gameModule);
			string gameType2 = Marshal.PtrToStringAnsi(gameType);
			string map2 = Marshal.PtrToStringAnsi(map);
			string address2 = Marshal.PtrToStringAnsi(address);
			MBMultiplayerData.UpdateGameServerInfo(id2, gameServer2, gameModule2, gameType2, map2, currentPlayerCount, maxPlayerCount, address2, port);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000327C File Offset: 0x0000147C
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Mission_ApplySkeletonScaleToAllEquippedItems_delegate))]
		internal static void Mission_ApplySkeletonScaleToAllEquippedItems(int thisPointer, IntPtr itemName)
		{
			Mission mission = DotNetObject.GetManagedObjectWithId(thisPointer) as Mission;
			string itemName2 = Marshal.PtrToStringAnsi(itemName);
			mission.ApplySkeletonScaleToAllEquippedItems(itemName2);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000032A4 File Offset: 0x000014A4
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Mission_ChargeDamageCallback_delegate))]
		internal static void Mission_ChargeDamageCallback(int thisPointer, ref AttackCollisionData collisionData, Blow blow, int attacker, int victim)
		{
			Mission mission = DotNetObject.GetManagedObjectWithId(thisPointer) as Mission;
			Agent attacker2 = DotNetObject.GetManagedObjectWithId(attacker) as Agent;
			Agent victim2 = DotNetObject.GetManagedObjectWithId(victim) as Agent;
			mission.ChargeDamageCallback(ref collisionData, blow, attacker2, victim2);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000032E0 File Offset: 0x000014E0
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Mission_DebugLogNativeMissionNetworkEvent_delegate))]
		internal static void Mission_DebugLogNativeMissionNetworkEvent(int eventEnum, IntPtr eventName, int bitCount)
		{
			string eventName2 = Marshal.PtrToStringAnsi(eventName);
			Mission.DebugLogNativeMissionNetworkEvent(eventEnum, eventName2, bitCount);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000032FC File Offset: 0x000014FC
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Mission_EndMission_delegate))]
		internal static void Mission_EndMission(int thisPointer)
		{
			(DotNetObject.GetManagedObjectWithId(thisPointer) as Mission).EndMission();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003310 File Offset: 0x00001510
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Mission_FallDamageCallback_delegate))]
		internal static void Mission_FallDamageCallback(int thisPointer, ref AttackCollisionData collisionData, Blow b, int attacker, int victim)
		{
			Mission mission = DotNetObject.GetManagedObjectWithId(thisPointer) as Mission;
			Agent attacker2 = DotNetObject.GetManagedObjectWithId(attacker) as Agent;
			Agent victim2 = DotNetObject.GetManagedObjectWithId(victim) as Agent;
			mission.FallDamageCallback(ref collisionData, b, attacker2, victim2);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000334C File Offset: 0x0000154C
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Mission_GetAgentState_delegate))]
		internal static AgentState Mission_GetAgentState(int thisPointer, int affectorAgent, int agent, DamageTypes damageType, WeaponFlags weaponFlags)
		{
			Mission mission = DotNetObject.GetManagedObjectWithId(thisPointer) as Mission;
			Agent affectorAgent2 = DotNetObject.GetManagedObjectWithId(affectorAgent) as Agent;
			Agent agent2 = DotNetObject.GetManagedObjectWithId(agent) as Agent;
			return mission.GetAgentState(affectorAgent2, agent2, damageType, weaponFlags);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003388 File Offset: 0x00001588
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Mission_GetClosestFleePositionForAgent_delegate))]
		internal static WorldPosition Mission_GetClosestFleePositionForAgent(int thisPointer, int agent)
		{
			Mission mission = DotNetObject.GetManagedObjectWithId(thisPointer) as Mission;
			Agent agent2 = DotNetObject.GetManagedObjectWithId(agent) as Agent;
			return mission.GetClosestFleePositionForAgent(agent2);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000033B4 File Offset: 0x000015B4
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Mission_GetDefendCollisionResults_delegate))]
		internal static void Mission_GetDefendCollisionResults(int thisPointer, int attackerAgent, int defenderAgent, CombatCollisionResult collisionResult, int attackerWeaponSlotIndex, bool isAlternativeAttack, StrikeType strikeType, Agent.UsageDirection attackDirection, float collisionDistanceOnWeapon, float attackProgress, bool attackIsParried, bool isPassiveUsageHit, bool isHeavyAttack, ref float defenderStunPeriod, ref float attackerStunPeriod, ref bool crushedThrough)
		{
			Mission mission = DotNetObject.GetManagedObjectWithId(thisPointer) as Mission;
			Agent attackerAgent2 = DotNetObject.GetManagedObjectWithId(attackerAgent) as Agent;
			Agent defenderAgent2 = DotNetObject.GetManagedObjectWithId(defenderAgent) as Agent;
			mission.GetDefendCollisionResults(attackerAgent2, defenderAgent2, collisionResult, attackerWeaponSlotIndex, isAlternativeAttack, strikeType, attackDirection, collisionDistanceOnWeapon, attackProgress, attackIsParried, isPassiveUsageHit, isHeavyAttack, ref defenderStunPeriod, ref attackerStunPeriod, ref crushedThrough);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003404 File Offset: 0x00001604
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Mission_MeleeHitCallback_delegate))]
		internal static void Mission_MeleeHitCallback(int thisPointer, ref AttackCollisionData collisionData, int attacker, int victim, NativeObjectPointer realHitEntity, ref float inOutMomentumRemaining, ref MeleeCollisionReaction colReaction, CrushThroughState crushThroughState, Vec3 blowDir, Vec3 swingDir, ref HitParticleResultData hitParticleResultData, bool crushedThroughWithoutAgentCollision)
		{
			Mission mission = DotNetObject.GetManagedObjectWithId(thisPointer) as Mission;
			Agent attacker2 = DotNetObject.GetManagedObjectWithId(attacker) as Agent;
			Agent victim2 = DotNetObject.GetManagedObjectWithId(victim) as Agent;
			GameEntity realHitEntity2 = null;
			if (realHitEntity.Pointer != UIntPtr.Zero)
			{
				realHitEntity2 = new GameEntity(realHitEntity.Pointer);
			}
			mission.MeleeHitCallback(ref collisionData, attacker2, victim2, realHitEntity2, ref inOutMomentumRemaining, ref colReaction, crushThroughState, blowDir, swingDir, ref hitParticleResultData, crushedThroughWithoutAgentCollision);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003470 File Offset: 0x00001670
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Mission_MissileAreaDamageCallback_delegate))]
		internal static void Mission_MissileAreaDamageCallback(int thisPointer, ref AttackCollisionData collisionDataInput, ref Blow blowInput, int alreadyDamagedAgent, int shooterAgent, bool isBigExplosion)
		{
			Mission mission = DotNetObject.GetManagedObjectWithId(thisPointer) as Mission;
			Agent alreadyDamagedAgent2 = DotNetObject.GetManagedObjectWithId(alreadyDamagedAgent) as Agent;
			Agent shooterAgent2 = DotNetObject.GetManagedObjectWithId(shooterAgent) as Agent;
			mission.MissileAreaDamageCallback(ref collisionDataInput, ref blowInput, alreadyDamagedAgent2, shooterAgent2, isBigExplosion);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000034AC File Offset: 0x000016AC
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Mission_MissileCalculatePassbySoundParametersCallbackMT_delegate))]
		internal static void Mission_MissileCalculatePassbySoundParametersCallbackMT(int thisPointer, int missileIndex, ref SoundEventParameter soundEventParameter)
		{
			(DotNetObject.GetManagedObjectWithId(thisPointer) as Mission).MissileCalculatePassbySoundParametersCallbackMT(missileIndex, ref soundEventParameter);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000034C0 File Offset: 0x000016C0
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Mission_MissileHitCallback_delegate))]
		internal static bool Mission_MissileHitCallback(int thisPointer, out int extraHitParticleIndex, ref AttackCollisionData collisionData, Vec3 missileStartingPosition, Vec3 missilePosition, Vec3 missileAngularVelocity, Vec3 movementVelocity, MatrixFrame attachGlobalFrame, MatrixFrame affectedShieldGlobalFrame, int numDamagedAgents, int attacker, int victim, NativeObjectPointer hitEntity)
		{
			Mission mission = DotNetObject.GetManagedObjectWithId(thisPointer) as Mission;
			Agent attacker2 = DotNetObject.GetManagedObjectWithId(attacker) as Agent;
			Agent victim2 = DotNetObject.GetManagedObjectWithId(victim) as Agent;
			GameEntity hitEntity2 = null;
			if (hitEntity.Pointer != UIntPtr.Zero)
			{
				hitEntity2 = new GameEntity(hitEntity.Pointer);
			}
			return mission.MissileHitCallback(out extraHitParticleIndex, ref collisionData, missileStartingPosition, missilePosition, missileAngularVelocity, movementVelocity, attachGlobalFrame, affectedShieldGlobalFrame, numDamagedAgents, attacker2, victim2, hitEntity2);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x0000352C File Offset: 0x0000172C
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Mission_OnAgentAddedAsCorpse_delegate))]
		internal static void Mission_OnAgentAddedAsCorpse(int thisPointer, int affectedAgent)
		{
			Mission mission = DotNetObject.GetManagedObjectWithId(thisPointer) as Mission;
			Agent affectedAgent2 = DotNetObject.GetManagedObjectWithId(affectedAgent) as Agent;
			mission.OnAgentAddedAsCorpse(affectedAgent2);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003558 File Offset: 0x00001758
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Mission_OnAgentDeleted_delegate))]
		internal static void Mission_OnAgentDeleted(int thisPointer, int affectedAgent)
		{
			Mission mission = DotNetObject.GetManagedObjectWithId(thisPointer) as Mission;
			Agent affectedAgent2 = DotNetObject.GetManagedObjectWithId(affectedAgent) as Agent;
			mission.OnAgentDeleted(affectedAgent2);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003584 File Offset: 0x00001784
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Mission_OnAgentHitBlocked_delegate))]
		internal static float Mission_OnAgentHitBlocked(int thisPointer, int affectedAgent, int affectorAgent, ref AttackCollisionData collisionData, Vec3 blowDirection, Vec3 swingDirection, bool isMissile)
		{
			Mission mission = DotNetObject.GetManagedObjectWithId(thisPointer) as Mission;
			Agent affectedAgent2 = DotNetObject.GetManagedObjectWithId(affectedAgent) as Agent;
			Agent affectorAgent2 = DotNetObject.GetManagedObjectWithId(affectorAgent) as Agent;
			return mission.OnAgentHitBlocked(affectedAgent2, affectorAgent2, ref collisionData, blowDirection, swingDirection, isMissile);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000035C4 File Offset: 0x000017C4
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Mission_OnAgentRemoved_delegate))]
		internal static void Mission_OnAgentRemoved(int thisPointer, int affectedAgent, int affectorAgent, AgentState agentState, KillingBlow killingBlow)
		{
			Mission mission = DotNetObject.GetManagedObjectWithId(thisPointer) as Mission;
			Agent affectedAgent2 = DotNetObject.GetManagedObjectWithId(affectedAgent) as Agent;
			Agent affectorAgent2 = DotNetObject.GetManagedObjectWithId(affectorAgent) as Agent;
			mission.OnAgentRemoved(affectedAgent2, affectorAgent2, agentState, killingBlow);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003600 File Offset: 0x00001800
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Mission_OnAgentShootMissile_delegate))]
		internal static void Mission_OnAgentShootMissile(int thisPointer, int shooterAgent, EquipmentIndex weaponIndex, Vec3 position, Vec3 velocity, Mat3 orientation, bool hasRigidBody, bool isPrimaryWeaponShot, int forcedMissileIndex)
		{
			Mission mission = DotNetObject.GetManagedObjectWithId(thisPointer) as Mission;
			Agent shooterAgent2 = DotNetObject.GetManagedObjectWithId(shooterAgent) as Agent;
			mission.OnAgentShootMissile(shooterAgent2, weaponIndex, position, velocity, orientation, hasRigidBody, isPrimaryWeaponShot, forcedMissileIndex);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003636 File Offset: 0x00001836
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Mission_OnMissileRemoved_delegate))]
		internal static void Mission_OnMissileRemoved(int thisPointer, int missileIndex)
		{
			(DotNetObject.GetManagedObjectWithId(thisPointer) as Mission).OnMissileRemoved(missileIndex);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003649 File Offset: 0x00001849
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Mission_OnPreTick_delegate))]
		internal static void Mission_OnPreTick(int thisPointer, float dt)
		{
			(DotNetObject.GetManagedObjectWithId(thisPointer) as Mission).OnPreTick(dt);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x0000365C File Offset: 0x0000185C
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Mission_OnSceneCreated_delegate))]
		internal static void Mission_OnSceneCreated(int thisPointer, NativeObjectPointer scene)
		{
			Mission mission = DotNetObject.GetManagedObjectWithId(thisPointer) as Mission;
			Scene scene2 = null;
			if (scene.Pointer != UIntPtr.Zero)
			{
				scene2 = new Scene(scene.Pointer);
			}
			mission.OnSceneCreated(scene2);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000369A File Offset: 0x0000189A
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Mission_PauseMission_delegate))]
		internal static void Mission_PauseMission(int thisPointer)
		{
			(DotNetObject.GetManagedObjectWithId(thisPointer) as Mission).PauseMission();
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000036AC File Offset: 0x000018AC
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Mission_ResetMission_delegate))]
		internal static void Mission_ResetMission(int thisPointer)
		{
			(DotNetObject.GetManagedObjectWithId(thisPointer) as Mission).ResetMission();
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000036C0 File Offset: 0x000018C0
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Mission_SpawnWeaponAsDropFromAgent_delegate))]
		internal static void Mission_SpawnWeaponAsDropFromAgent(int thisPointer, int agent, EquipmentIndex equipmentIndex, ref Vec3 velocity, ref Vec3 angularVelocity, Mission.WeaponSpawnFlags spawnFlags)
		{
			Mission mission = DotNetObject.GetManagedObjectWithId(thisPointer) as Mission;
			Agent agent2 = DotNetObject.GetManagedObjectWithId(agent) as Agent;
			mission.SpawnWeaponAsDropFromAgent(agent2, equipmentIndex, ref velocity, ref angularVelocity, spawnFlags);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000036F0 File Offset: 0x000018F0
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Mission_TickAgentsAndTeams_delegate))]
		internal static void Mission_TickAgentsAndTeams(int thisPointer, float dt)
		{
			(DotNetObject.GetManagedObjectWithId(thisPointer) as Mission).TickAgentsAndTeams(dt);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003703 File Offset: 0x00001903
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Mission_UpdateMissionTimeCache_delegate))]
		internal static void Mission_UpdateMissionTimeCache(int thisPointer, float curTime)
		{
			(DotNetObject.GetManagedObjectWithId(thisPointer) as Mission).UpdateMissionTimeCache(curTime);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003718 File Offset: 0x00001918
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Module_CreateProcessedActionSetsXMLForNative_delegate))]
		internal static UIntPtr Module_CreateProcessedActionSetsXMLForNative()
		{
			string text = Module.CreateProcessedActionSetsXMLForNative();
			UIntPtr threadLocalCachedRglVarString = NativeStringHelper.GetThreadLocalCachedRglVarString();
			NativeStringHelper.SetRglVarString(threadLocalCachedRglVarString, text);
			return threadLocalCachedRglVarString;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003738 File Offset: 0x00001938
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Module_CreateProcessedActionTypesXMLForNative_delegate))]
		internal static UIntPtr Module_CreateProcessedActionTypesXMLForNative()
		{
			string text = Module.CreateProcessedActionTypesXMLForNative();
			UIntPtr threadLocalCachedRglVarString = NativeStringHelper.GetThreadLocalCachedRglVarString();
			NativeStringHelper.SetRglVarString(threadLocalCachedRglVarString, text);
			return threadLocalCachedRglVarString;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003758 File Offset: 0x00001958
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Module_CreateProcessedAnimationsXMLForNative_delegate))]
		internal static UIntPtr Module_CreateProcessedAnimationsXMLForNative(out string animationsXmlPaths)
		{
			string text = Module.CreateProcessedAnimationsXMLForNative(out animationsXmlPaths);
			UIntPtr threadLocalCachedRglVarString = NativeStringHelper.GetThreadLocalCachedRglVarString();
			NativeStringHelper.SetRglVarString(threadLocalCachedRglVarString, text);
			return threadLocalCachedRglVarString;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003778 File Offset: 0x00001978
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Module_CreateProcessedModuleDataXMLForNative_delegate))]
		internal static UIntPtr Module_CreateProcessedModuleDataXMLForNative(IntPtr xmlType)
		{
			string text = Module.CreateProcessedModuleDataXMLForNative(Marshal.PtrToStringAnsi(xmlType));
			UIntPtr threadLocalCachedRglVarString = NativeStringHelper.GetThreadLocalCachedRglVarString();
			NativeStringHelper.SetRglVarString(threadLocalCachedRglVarString, text);
			return threadLocalCachedRglVarString;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000037A0 File Offset: 0x000019A0
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Module_CreateProcessedSkinsXMLForNative_delegate))]
		internal static UIntPtr Module_CreateProcessedSkinsXMLForNative(out string baseSkinsXmlPath)
		{
			string text = Module.CreateProcessedSkinsXMLForNative(out baseSkinsXmlPath);
			UIntPtr threadLocalCachedRglVarString = NativeStringHelper.GetThreadLocalCachedRglVarString();
			NativeStringHelper.SetRglVarString(threadLocalCachedRglVarString, text);
			return threadLocalCachedRglVarString;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000037C0 File Offset: 0x000019C0
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Module_CreateProcessedVoiceDefinitionsXMLForNative_delegate))]
		internal static UIntPtr Module_CreateProcessedVoiceDefinitionsXMLForNative()
		{
			string text = Module.CreateProcessedVoiceDefinitionsXMLForNative();
			UIntPtr threadLocalCachedRglVarString = NativeStringHelper.GetThreadLocalCachedRglVarString();
			NativeStringHelper.SetRglVarString(threadLocalCachedRglVarString, text);
			return threadLocalCachedRglVarString;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000037E0 File Offset: 0x000019E0
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Module_GetGameStatus_delegate))]
		internal static UIntPtr Module_GetGameStatus()
		{
			string gameStatus = Module.GetGameStatus();
			UIntPtr threadLocalCachedRglVarString = NativeStringHelper.GetThreadLocalCachedRglVarString();
			NativeStringHelper.SetRglVarString(threadLocalCachedRglVarString, gameStatus);
			return threadLocalCachedRglVarString;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003800 File Offset: 0x00001A00
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Module_GetHorseMaterialNames_delegate))]
		internal static UIntPtr Module_GetHorseMaterialNames(int thisPointer)
		{
			string horseMaterialNames = (DotNetObject.GetManagedObjectWithId(thisPointer) as Module).GetHorseMaterialNames();
			UIntPtr threadLocalCachedRglVarString = NativeStringHelper.GetThreadLocalCachedRglVarString();
			NativeStringHelper.SetRglVarString(threadLocalCachedRglVarString, horseMaterialNames);
			return threadLocalCachedRglVarString;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x0000382A File Offset: 0x00001A2A
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Module_GetInstance_delegate))]
		internal static int Module_GetInstance()
		{
			return Module.GetInstance().GetManagedId();
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003838 File Offset: 0x00001A38
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Module_GetItemMeshNames_delegate))]
		internal static UIntPtr Module_GetItemMeshNames(int thisPointer)
		{
			string itemMeshNames = (DotNetObject.GetManagedObjectWithId(thisPointer) as Module).GetItemMeshNames();
			UIntPtr threadLocalCachedRglVarString = NativeStringHelper.GetThreadLocalCachedRglVarString();
			NativeStringHelper.SetRglVarString(threadLocalCachedRglVarString, itemMeshNames);
			return threadLocalCachedRglVarString;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003864 File Offset: 0x00001A64
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Module_GetMetaMeshPackageMapping_delegate))]
		internal static UIntPtr Module_GetMetaMeshPackageMapping(int thisPointer)
		{
			string metaMeshPackageMapping = (DotNetObject.GetManagedObjectWithId(thisPointer) as Module).GetMetaMeshPackageMapping();
			UIntPtr threadLocalCachedRglVarString = NativeStringHelper.GetThreadLocalCachedRglVarString();
			NativeStringHelper.SetRglVarString(threadLocalCachedRglVarString, metaMeshPackageMapping);
			return threadLocalCachedRglVarString;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003890 File Offset: 0x00001A90
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Module_GetMissionControllerClassNames_delegate))]
		internal static UIntPtr Module_GetMissionControllerClassNames(int thisPointer)
		{
			string missionControllerClassNames = (DotNetObject.GetManagedObjectWithId(thisPointer) as Module).GetMissionControllerClassNames();
			UIntPtr threadLocalCachedRglVarString = NativeStringHelper.GetThreadLocalCachedRglVarString();
			NativeStringHelper.SetRglVarString(threadLocalCachedRglVarString, missionControllerClassNames);
			return threadLocalCachedRglVarString;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000038BA File Offset: 0x00001ABA
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Module_Initialize_delegate))]
		internal static void Module_Initialize(int thisPointer)
		{
			(DotNetObject.GetManagedObjectWithId(thisPointer) as Module).Initialize();
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000038CC File Offset: 0x00001ACC
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Module_MBThrowException_delegate))]
		internal static void Module_MBThrowException()
		{
			Module.MBThrowException();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000038D3 File Offset: 0x00001AD3
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Module_OnCloseSceneEditorPresentation_delegate))]
		internal static void Module_OnCloseSceneEditorPresentation(int thisPointer)
		{
			(DotNetObject.GetManagedObjectWithId(thisPointer) as Module).OnCloseSceneEditorPresentation();
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000038E5 File Offset: 0x00001AE5
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Module_OnDumpCreated_delegate))]
		internal static void Module_OnDumpCreated(int thisPointer)
		{
			(DotNetObject.GetManagedObjectWithId(thisPointer) as Module).OnDumpCreated();
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000038F7 File Offset: 0x00001AF7
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Module_OnDumpCreationStarted_delegate))]
		internal static void Module_OnDumpCreationStarted(int thisPointer)
		{
			(DotNetObject.GetManagedObjectWithId(thisPointer) as Module).OnDumpCreationStarted();
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003909 File Offset: 0x00001B09
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Module_OnEnterEditMode_delegate))]
		internal static void Module_OnEnterEditMode(int thisPointer, bool isFirstTime)
		{
			(DotNetObject.GetManagedObjectWithId(thisPointer) as Module).OnEnterEditMode(isFirstTime);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000391C File Offset: 0x00001B1C
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Module_OnImguiProfilerTick_delegate))]
		internal static void Module_OnImguiProfilerTick(int thisPointer)
		{
			(DotNetObject.GetManagedObjectWithId(thisPointer) as Module).OnImguiProfilerTick();
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000392E File Offset: 0x00001B2E
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Module_OnSceneEditorModeOver_delegate))]
		internal static void Module_OnSceneEditorModeOver(int thisPointer)
		{
			(DotNetObject.GetManagedObjectWithId(thisPointer) as Module).OnSceneEditorModeOver();
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003940 File Offset: 0x00001B40
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Module_OnSkinsXMLHasChanged_delegate))]
		internal static void Module_OnSkinsXMLHasChanged(int thisPointer)
		{
			(DotNetObject.GetManagedObjectWithId(thisPointer) as Module).OnSkinsXMLHasChanged();
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003954 File Offset: 0x00001B54
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Module_RunTest_delegate))]
		internal static void Module_RunTest(int thisPointer, IntPtr commandLine)
		{
			Module module = DotNetObject.GetManagedObjectWithId(thisPointer) as Module;
			string commandLine2 = Marshal.PtrToStringAnsi(commandLine);
			module.RunTest(commandLine2);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003979 File Offset: 0x00001B79
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Module_SetEditorScreenAsRootScreen_delegate))]
		internal static bool Module_SetEditorScreenAsRootScreen(int thisPointer)
		{
			return (DotNetObject.GetManagedObjectWithId(thisPointer) as Module).SetEditorScreenAsRootScreen();
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000398B File Offset: 0x00001B8B
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Module_SetLoadingFinished_delegate))]
		internal static void Module_SetLoadingFinished(int thisPointer)
		{
			(DotNetObject.GetManagedObjectWithId(thisPointer) as Module).SetLoadingFinished();
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000039A0 File Offset: 0x00001BA0
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Module_StartMissionForEditor_delegate))]
		internal static void Module_StartMissionForEditor(int thisPointer, IntPtr missionName, IntPtr sceneName, IntPtr levels)
		{
			Module module = DotNetObject.GetManagedObjectWithId(thisPointer) as Module;
			string missionName2 = Marshal.PtrToStringAnsi(missionName);
			string sceneName2 = Marshal.PtrToStringAnsi(sceneName);
			string levels2 = Marshal.PtrToStringAnsi(levels);
			module.StartMissionForEditor(missionName2, sceneName2, levels2);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000039D8 File Offset: 0x00001BD8
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Module_StartMissionForReplayEditor_delegate))]
		internal static void Module_StartMissionForReplayEditor(int thisPointer, IntPtr missionName, IntPtr sceneName, IntPtr levels, IntPtr fileName, bool record, float startTime, float endTime)
		{
			Module module = DotNetObject.GetManagedObjectWithId(thisPointer) as Module;
			string missionName2 = Marshal.PtrToStringAnsi(missionName);
			string sceneName2 = Marshal.PtrToStringAnsi(sceneName);
			string levels2 = Marshal.PtrToStringAnsi(levels);
			string fileName2 = Marshal.PtrToStringAnsi(fileName);
			module.StartMissionForReplayEditor(missionName2, sceneName2, levels2, fileName2, record, startTime, endTime);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003A1C File Offset: 0x00001C1C
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.Module_TickTest_delegate))]
		internal static void Module_TickTest(int thisPointer, float dt)
		{
			(DotNetObject.GetManagedObjectWithId(thisPointer) as Module).TickTest(dt);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003A30 File Offset: 0x00001C30
		[MonoPInvokeCallback(typeof(CoreCallbacksGenerated.WeaponComponentMissionExtensions_CalculateCenterOfMass_delegate))]
		internal static Vec3 WeaponComponentMissionExtensions_CalculateCenterOfMass(NativeObjectPointer body)
		{
			PhysicsShape body2 = null;
			if (body.Pointer != UIntPtr.Zero)
			{
				body2 = new PhysicsShape(body.Pointer);
			}
			return WeaponComponentMissionExtensions.CalculateCenterOfMass(body2);
		}

		// Token: 0x02000025 RID: 37
		// (Invoke) Token: 0x06000339 RID: 825
		internal delegate float Agent_DebugGetHealth_delegate(int thisPointer);

		// Token: 0x02000026 RID: 38
		// (Invoke) Token: 0x0600033D RID: 829
		internal delegate int Agent_GetFormationUnitSpacing_delegate(int thisPointer);

		// Token: 0x02000027 RID: 39
		// (Invoke) Token: 0x06000341 RID: 833
		internal delegate float Agent_GetMissileRangeWithHeightDifferenceAux_delegate(int thisPointer, float targetZ);

		// Token: 0x02000028 RID: 40
		// (Invoke) Token: 0x06000345 RID: 837
		internal delegate UIntPtr Agent_GetSoundAndCollisionInfoClassName_delegate(int thisPointer);

		// Token: 0x02000029 RID: 41
		// (Invoke) Token: 0x06000349 RID: 841
		internal delegate float Agent_GetWeaponInaccuracy_delegate(int thisPointer, EquipmentIndex weaponSlotIndex, int weaponUsageIndex);

		// Token: 0x0200002A RID: 42
		// (Invoke) Token: 0x0600034D RID: 845
		[return: MarshalAs(UnmanagedType.U1)]
		internal delegate bool Agent_IsInSameFormationWith_delegate(int thisPointer, int otherAgent);

		// Token: 0x0200002B RID: 43
		// (Invoke) Token: 0x06000351 RID: 849
		internal delegate void Agent_OnAgentAlarmedStateChanged_delegate(int thisPointer, Agent.AIStateFlag flag);

		// Token: 0x0200002C RID: 44
		// (Invoke) Token: 0x06000355 RID: 853
		internal delegate void Agent_OnDismount_delegate(int thisPointer, int mount);

		// Token: 0x0200002D RID: 45
		// (Invoke) Token: 0x06000359 RID: 857
		internal delegate void Agent_OnMount_delegate(int thisPointer, int mount);

		// Token: 0x0200002E RID: 46
		// (Invoke) Token: 0x0600035D RID: 861
		internal delegate void Agent_OnRemoveWeapon_delegate(int thisPointer, EquipmentIndex slotIndex);

		// Token: 0x0200002F RID: 47
		// (Invoke) Token: 0x06000361 RID: 865
		internal delegate void Agent_OnRetreating_delegate(int thisPointer);

		// Token: 0x02000030 RID: 48
		// (Invoke) Token: 0x06000365 RID: 869
		internal delegate void Agent_OnShieldDamaged_delegate(int thisPointer, EquipmentIndex slotIndex, int inflictedDamage);

		// Token: 0x02000031 RID: 49
		// (Invoke) Token: 0x06000369 RID: 873
		internal delegate void Agent_OnWeaponAmmoConsume_delegate(int thisPointer, EquipmentIndex slotIndex, short totalAmmo);

		// Token: 0x02000032 RID: 50
		// (Invoke) Token: 0x0600036D RID: 877
		internal delegate void Agent_OnWeaponAmmoReload_delegate(int thisPointer, EquipmentIndex slotIndex, EquipmentIndex ammoSlotIndex, short totalAmmo);

		// Token: 0x02000033 RID: 51
		// (Invoke) Token: 0x06000371 RID: 881
		internal delegate void Agent_OnWeaponAmmoRemoved_delegate(int thisPointer, EquipmentIndex slotIndex);

		// Token: 0x02000034 RID: 52
		// (Invoke) Token: 0x06000375 RID: 885
		internal delegate void Agent_OnWeaponAmountChange_delegate(int thisPointer, EquipmentIndex slotIndex, short amount);

		// Token: 0x02000035 RID: 53
		// (Invoke) Token: 0x06000379 RID: 889
		internal delegate void Agent_OnWeaponReloadPhaseChange_delegate(int thisPointer, EquipmentIndex slotIndex, short reloadPhase);

		// Token: 0x02000036 RID: 54
		// (Invoke) Token: 0x0600037D RID: 893
		internal delegate void Agent_OnWeaponSwitchingToAlternativeStart_delegate(int thisPointer, EquipmentIndex slotIndex, int usageIndex);

		// Token: 0x02000037 RID: 55
		// (Invoke) Token: 0x06000381 RID: 897
		internal delegate void Agent_OnWeaponUsageIndexChange_delegate(int thisPointer, EquipmentIndex slotIndex, int usageIndex);

		// Token: 0x02000038 RID: 56
		// (Invoke) Token: 0x06000385 RID: 901
		internal delegate void Agent_OnWieldedItemIndexChange_delegate(int thisPointer, [MarshalAs(UnmanagedType.U1)] bool isOffHand, [MarshalAs(UnmanagedType.U1)] bool isWieldedInstantly, [MarshalAs(UnmanagedType.U1)] bool isWieldedOnSpawn);

		// Token: 0x02000039 RID: 57
		// (Invoke) Token: 0x06000389 RID: 905
		internal delegate void Agent_SetAgentAIPerformingRetreatBehavior_delegate(int thisPointer, [MarshalAs(UnmanagedType.U1)] bool isAgentAIPerformingRetreatBehavior);

		// Token: 0x0200003A RID: 58
		// (Invoke) Token: 0x0600038D RID: 909
		internal delegate void Agent_UpdateAgentStats_delegate(int thisPointer);

		// Token: 0x0200003B RID: 59
		// (Invoke) Token: 0x06000391 RID: 913
		internal delegate void Agent_UpdateMountAgentCache_delegate(int thisPointer, int newMountAgent);

		// Token: 0x0200003C RID: 60
		// (Invoke) Token: 0x06000395 RID: 917
		internal delegate void Agent_UpdateRiderAgentCache_delegate(int thisPointer, int newRiderAgent);

		// Token: 0x0200003D RID: 61
		// (Invoke) Token: 0x06000399 RID: 921
		internal delegate void BannerlordTableauManager_RegisterCharacterTableauScene_delegate(NativeObjectPointer scene, int type);

		// Token: 0x0200003E RID: 62
		// (Invoke) Token: 0x0600039D RID: 925
		internal delegate void BannerlordTableauManager_RequestCharacterTableauSetup_delegate(int characterCodeId, NativeObjectPointer scene, NativeObjectPointer poseEntity);

		// Token: 0x0200003F RID: 63
		// (Invoke) Token: 0x060003A1 RID: 929
		internal delegate void CoreManaged_CheckSharedStructureSizes_delegate();

		// Token: 0x02000040 RID: 64
		// (Invoke) Token: 0x060003A5 RID: 933
		internal delegate void CoreManaged_EngineApiMethodInterfaceInitializer_delegate(int id, IntPtr pointer);

		// Token: 0x02000041 RID: 65
		// (Invoke) Token: 0x060003A9 RID: 937
		internal delegate void CoreManaged_FillEngineApiPointers_delegate();

		// Token: 0x02000042 RID: 66
		// (Invoke) Token: 0x060003AD RID: 941
		internal delegate void CoreManaged_Finalize_delegate();

		// Token: 0x02000043 RID: 67
		// (Invoke) Token: 0x060003B1 RID: 945
		internal delegate void CoreManaged_OnLoadCommonFinished_delegate();

		// Token: 0x02000044 RID: 68
		// (Invoke) Token: 0x060003B5 RID: 949
		internal delegate void CoreManaged_Start_delegate();

		// Token: 0x02000045 RID: 69
		// (Invoke) Token: 0x060003B9 RID: 953
		internal delegate void GameNetwork_HandleConsoleCommand_delegate(IntPtr command);

		// Token: 0x02000046 RID: 70
		// (Invoke) Token: 0x060003BD RID: 957
		internal delegate void GameNetwork_HandleDisconnect_delegate();

		// Token: 0x02000047 RID: 71
		// (Invoke) Token: 0x060003C1 RID: 961
		[return: MarshalAs(UnmanagedType.U1)]
		internal delegate bool GameNetwork_HandleNetworkPacketAsClient_delegate();

		// Token: 0x02000048 RID: 72
		// (Invoke) Token: 0x060003C5 RID: 965
		[return: MarshalAs(UnmanagedType.U1)]
		internal delegate bool GameNetwork_HandleNetworkPacketAsServer_delegate(int networkPeer);

		// Token: 0x02000049 RID: 73
		// (Invoke) Token: 0x060003C9 RID: 969
		internal delegate void GameNetwork_HandleRemovePlayer_delegate(int peer, [MarshalAs(UnmanagedType.U1)] bool isTimedOut);

		// Token: 0x0200004A RID: 74
		// (Invoke) Token: 0x060003CD RID: 973
		internal delegate void GameNetwork_SyncRelevantGameOptionsToServer_delegate();

		// Token: 0x0200004B RID: 75
		// (Invoke) Token: 0x060003D1 RID: 977
		internal delegate int ManagedOptions_GetConfigCount_delegate();

		// Token: 0x0200004C RID: 76
		// (Invoke) Token: 0x060003D5 RID: 981
		internal delegate float ManagedOptions_GetConfigValue_delegate(int type);

		// Token: 0x0200004D RID: 77
		// (Invoke) Token: 0x060003D9 RID: 985
		internal delegate void MBEditor_CloseEditorScene_delegate();

		// Token: 0x0200004E RID: 78
		// (Invoke) Token: 0x060003DD RID: 989
		internal delegate void MBEditor_DestroyEditor_delegate(NativeObjectPointer scene);

		// Token: 0x0200004F RID: 79
		// (Invoke) Token: 0x060003E1 RID: 993
		internal delegate void MBEditor_SetEditorScene_delegate(NativeObjectPointer scene);

		// Token: 0x02000050 RID: 80
		// (Invoke) Token: 0x060003E5 RID: 997
		internal delegate int MBMultiplayerData_GetCurrentPlayerCount_delegate();

		// Token: 0x02000051 RID: 81
		// (Invoke) Token: 0x060003E9 RID: 1001
		internal delegate UIntPtr MBMultiplayerData_GetGameModule_delegate();

		// Token: 0x02000052 RID: 82
		// (Invoke) Token: 0x060003ED RID: 1005
		internal delegate UIntPtr MBMultiplayerData_GetGameType_delegate();

		// Token: 0x02000053 RID: 83
		// (Invoke) Token: 0x060003F1 RID: 1009
		internal delegate UIntPtr MBMultiplayerData_GetMap_delegate();

		// Token: 0x02000054 RID: 84
		// (Invoke) Token: 0x060003F5 RID: 1013
		internal delegate int MBMultiplayerData_GetPlayerCountLimit_delegate();

		// Token: 0x02000055 RID: 85
		// (Invoke) Token: 0x060003F9 RID: 1017
		internal delegate UIntPtr MBMultiplayerData_GetServerId_delegate();

		// Token: 0x02000056 RID: 86
		// (Invoke) Token: 0x060003FD RID: 1021
		internal delegate UIntPtr MBMultiplayerData_GetServerName_delegate();

		// Token: 0x02000057 RID: 87
		// (Invoke) Token: 0x06000401 RID: 1025
		internal delegate void MBMultiplayerData_UpdateGameServerInfo_delegate(IntPtr id, IntPtr gameServer, IntPtr gameModule, IntPtr gameType, IntPtr map, int currentPlayerCount, int maxPlayerCount, IntPtr address, int port);

		// Token: 0x02000058 RID: 88
		// (Invoke) Token: 0x06000405 RID: 1029
		internal delegate void Mission_ApplySkeletonScaleToAllEquippedItems_delegate(int thisPointer, IntPtr itemName);

		// Token: 0x02000059 RID: 89
		// (Invoke) Token: 0x06000409 RID: 1033
		internal delegate void Mission_ChargeDamageCallback_delegate(int thisPointer, ref AttackCollisionData collisionData, Blow blow, int attacker, int victim);

		// Token: 0x0200005A RID: 90
		// (Invoke) Token: 0x0600040D RID: 1037
		internal delegate void Mission_DebugLogNativeMissionNetworkEvent_delegate(int eventEnum, IntPtr eventName, int bitCount);

		// Token: 0x0200005B RID: 91
		// (Invoke) Token: 0x06000411 RID: 1041
		internal delegate void Mission_EndMission_delegate(int thisPointer);

		// Token: 0x0200005C RID: 92
		// (Invoke) Token: 0x06000415 RID: 1045
		internal delegate void Mission_FallDamageCallback_delegate(int thisPointer, ref AttackCollisionData collisionData, Blow b, int attacker, int victim);

		// Token: 0x0200005D RID: 93
		// (Invoke) Token: 0x06000419 RID: 1049
		internal delegate AgentState Mission_GetAgentState_delegate(int thisPointer, int affectorAgent, int agent, DamageTypes damageType, WeaponFlags weaponFlags);

		// Token: 0x0200005E RID: 94
		// (Invoke) Token: 0x0600041D RID: 1053
		internal delegate WorldPosition Mission_GetClosestFleePositionForAgent_delegate(int thisPointer, int agent);

		// Token: 0x0200005F RID: 95
		// (Invoke) Token: 0x06000421 RID: 1057
		internal delegate void Mission_GetDefendCollisionResults_delegate(int thisPointer, int attackerAgent, int defenderAgent, CombatCollisionResult collisionResult, int attackerWeaponSlotIndex, [MarshalAs(UnmanagedType.U1)] bool isAlternativeAttack, StrikeType strikeType, Agent.UsageDirection attackDirection, float collisionDistanceOnWeapon, float attackProgress, [MarshalAs(UnmanagedType.U1)] bool attackIsParried, [MarshalAs(UnmanagedType.U1)] bool isPassiveUsageHit, [MarshalAs(UnmanagedType.U1)] bool isHeavyAttack, ref float defenderStunPeriod, ref float attackerStunPeriod, [MarshalAs(UnmanagedType.U1)] ref bool crushedThrough);

		// Token: 0x02000060 RID: 96
		// (Invoke) Token: 0x06000425 RID: 1061
		internal delegate void Mission_MeleeHitCallback_delegate(int thisPointer, ref AttackCollisionData collisionData, int attacker, int victim, NativeObjectPointer realHitEntity, ref float inOutMomentumRemaining, ref MeleeCollisionReaction colReaction, CrushThroughState crushThroughState, Vec3 blowDir, Vec3 swingDir, ref HitParticleResultData hitParticleResultData, [MarshalAs(UnmanagedType.U1)] bool crushedThroughWithoutAgentCollision);

		// Token: 0x02000061 RID: 97
		// (Invoke) Token: 0x06000429 RID: 1065
		internal delegate void Mission_MissileAreaDamageCallback_delegate(int thisPointer, ref AttackCollisionData collisionDataInput, ref Blow blowInput, int alreadyDamagedAgent, int shooterAgent, [MarshalAs(UnmanagedType.U1)] bool isBigExplosion);

		// Token: 0x02000062 RID: 98
		// (Invoke) Token: 0x0600042D RID: 1069
		internal delegate void Mission_MissileCalculatePassbySoundParametersCallbackMT_delegate(int thisPointer, int missileIndex, ref SoundEventParameter soundEventParameter);

		// Token: 0x02000063 RID: 99
		// (Invoke) Token: 0x06000431 RID: 1073
		[return: MarshalAs(UnmanagedType.U1)]
		internal delegate bool Mission_MissileHitCallback_delegate(int thisPointer, out int extraHitParticleIndex, ref AttackCollisionData collisionData, Vec3 missileStartingPosition, Vec3 missilePosition, Vec3 missileAngularVelocity, Vec3 movementVelocity, MatrixFrame attachGlobalFrame, MatrixFrame affectedShieldGlobalFrame, int numDamagedAgents, int attacker, int victim, NativeObjectPointer hitEntity);

		// Token: 0x02000064 RID: 100
		// (Invoke) Token: 0x06000435 RID: 1077
		internal delegate void Mission_OnAgentAddedAsCorpse_delegate(int thisPointer, int affectedAgent);

		// Token: 0x02000065 RID: 101
		// (Invoke) Token: 0x06000439 RID: 1081
		internal delegate void Mission_OnAgentDeleted_delegate(int thisPointer, int affectedAgent);

		// Token: 0x02000066 RID: 102
		// (Invoke) Token: 0x0600043D RID: 1085
		internal delegate float Mission_OnAgentHitBlocked_delegate(int thisPointer, int affectedAgent, int affectorAgent, ref AttackCollisionData collisionData, Vec3 blowDirection, Vec3 swingDirection, [MarshalAs(UnmanagedType.U1)] bool isMissile);

		// Token: 0x02000067 RID: 103
		// (Invoke) Token: 0x06000441 RID: 1089
		internal delegate void Mission_OnAgentRemoved_delegate(int thisPointer, int affectedAgent, int affectorAgent, AgentState agentState, KillingBlow killingBlow);

		// Token: 0x02000068 RID: 104
		// (Invoke) Token: 0x06000445 RID: 1093
		internal delegate void Mission_OnAgentShootMissile_delegate(int thisPointer, int shooterAgent, EquipmentIndex weaponIndex, Vec3 position, Vec3 velocity, Mat3 orientation, [MarshalAs(UnmanagedType.U1)] bool hasRigidBody, [MarshalAs(UnmanagedType.U1)] bool isPrimaryWeaponShot, int forcedMissileIndex);

		// Token: 0x02000069 RID: 105
		// (Invoke) Token: 0x06000449 RID: 1097
		internal delegate void Mission_OnMissileRemoved_delegate(int thisPointer, int missileIndex);

		// Token: 0x0200006A RID: 106
		// (Invoke) Token: 0x0600044D RID: 1101
		internal delegate void Mission_OnPreTick_delegate(int thisPointer, float dt);

		// Token: 0x0200006B RID: 107
		// (Invoke) Token: 0x06000451 RID: 1105
		internal delegate void Mission_OnSceneCreated_delegate(int thisPointer, NativeObjectPointer scene);

		// Token: 0x0200006C RID: 108
		// (Invoke) Token: 0x06000455 RID: 1109
		internal delegate void Mission_PauseMission_delegate(int thisPointer);

		// Token: 0x0200006D RID: 109
		// (Invoke) Token: 0x06000459 RID: 1113
		internal delegate void Mission_ResetMission_delegate(int thisPointer);

		// Token: 0x0200006E RID: 110
		// (Invoke) Token: 0x0600045D RID: 1117
		internal delegate void Mission_SpawnWeaponAsDropFromAgent_delegate(int thisPointer, int agent, EquipmentIndex equipmentIndex, ref Vec3 velocity, ref Vec3 angularVelocity, Mission.WeaponSpawnFlags spawnFlags);

		// Token: 0x0200006F RID: 111
		// (Invoke) Token: 0x06000461 RID: 1121
		internal delegate void Mission_TickAgentsAndTeams_delegate(int thisPointer, float dt);

		// Token: 0x02000070 RID: 112
		// (Invoke) Token: 0x06000465 RID: 1125
		internal delegate void Mission_UpdateMissionTimeCache_delegate(int thisPointer, float curTime);

		// Token: 0x02000071 RID: 113
		// (Invoke) Token: 0x06000469 RID: 1129
		internal delegate UIntPtr Module_CreateProcessedActionSetsXMLForNative_delegate();

		// Token: 0x02000072 RID: 114
		// (Invoke) Token: 0x0600046D RID: 1133
		internal delegate UIntPtr Module_CreateProcessedActionTypesXMLForNative_delegate();

		// Token: 0x02000073 RID: 115
		// (Invoke) Token: 0x06000471 RID: 1137
		internal delegate UIntPtr Module_CreateProcessedAnimationsXMLForNative_delegate(out string animationsXmlPaths);

		// Token: 0x02000074 RID: 116
		// (Invoke) Token: 0x06000475 RID: 1141
		internal delegate UIntPtr Module_CreateProcessedModuleDataXMLForNative_delegate(IntPtr xmlType);

		// Token: 0x02000075 RID: 117
		// (Invoke) Token: 0x06000479 RID: 1145
		internal delegate UIntPtr Module_CreateProcessedSkinsXMLForNative_delegate(out string baseSkinsXmlPath);

		// Token: 0x02000076 RID: 118
		// (Invoke) Token: 0x0600047D RID: 1149
		internal delegate UIntPtr Module_CreateProcessedVoiceDefinitionsXMLForNative_delegate();

		// Token: 0x02000077 RID: 119
		// (Invoke) Token: 0x06000481 RID: 1153
		internal delegate UIntPtr Module_GetGameStatus_delegate();

		// Token: 0x02000078 RID: 120
		// (Invoke) Token: 0x06000485 RID: 1157
		internal delegate UIntPtr Module_GetHorseMaterialNames_delegate(int thisPointer);

		// Token: 0x02000079 RID: 121
		// (Invoke) Token: 0x06000489 RID: 1161
		internal delegate int Module_GetInstance_delegate();

		// Token: 0x0200007A RID: 122
		// (Invoke) Token: 0x0600048D RID: 1165
		internal delegate UIntPtr Module_GetItemMeshNames_delegate(int thisPointer);

		// Token: 0x0200007B RID: 123
		// (Invoke) Token: 0x06000491 RID: 1169
		internal delegate UIntPtr Module_GetMetaMeshPackageMapping_delegate(int thisPointer);

		// Token: 0x0200007C RID: 124
		// (Invoke) Token: 0x06000495 RID: 1173
		internal delegate UIntPtr Module_GetMissionControllerClassNames_delegate(int thisPointer);

		// Token: 0x0200007D RID: 125
		// (Invoke) Token: 0x06000499 RID: 1177
		internal delegate void Module_Initialize_delegate(int thisPointer);

		// Token: 0x0200007E RID: 126
		// (Invoke) Token: 0x0600049D RID: 1181
		internal delegate void Module_MBThrowException_delegate();

		// Token: 0x0200007F RID: 127
		// (Invoke) Token: 0x060004A1 RID: 1185
		internal delegate void Module_OnCloseSceneEditorPresentation_delegate(int thisPointer);

		// Token: 0x02000080 RID: 128
		// (Invoke) Token: 0x060004A5 RID: 1189
		internal delegate void Module_OnDumpCreated_delegate(int thisPointer);

		// Token: 0x02000081 RID: 129
		// (Invoke) Token: 0x060004A9 RID: 1193
		internal delegate void Module_OnDumpCreationStarted_delegate(int thisPointer);

		// Token: 0x02000082 RID: 130
		// (Invoke) Token: 0x060004AD RID: 1197
		internal delegate void Module_OnEnterEditMode_delegate(int thisPointer, [MarshalAs(UnmanagedType.U1)] bool isFirstTime);

		// Token: 0x02000083 RID: 131
		// (Invoke) Token: 0x060004B1 RID: 1201
		internal delegate void Module_OnImguiProfilerTick_delegate(int thisPointer);

		// Token: 0x02000084 RID: 132
		// (Invoke) Token: 0x060004B5 RID: 1205
		internal delegate void Module_OnSceneEditorModeOver_delegate(int thisPointer);

		// Token: 0x02000085 RID: 133
		// (Invoke) Token: 0x060004B9 RID: 1209
		internal delegate void Module_OnSkinsXMLHasChanged_delegate(int thisPointer);

		// Token: 0x02000086 RID: 134
		// (Invoke) Token: 0x060004BD RID: 1213
		internal delegate void Module_RunTest_delegate(int thisPointer, IntPtr commandLine);

		// Token: 0x02000087 RID: 135
		// (Invoke) Token: 0x060004C1 RID: 1217
		[return: MarshalAs(UnmanagedType.U1)]
		internal delegate bool Module_SetEditorScreenAsRootScreen_delegate(int thisPointer);

		// Token: 0x02000088 RID: 136
		// (Invoke) Token: 0x060004C5 RID: 1221
		internal delegate void Module_SetLoadingFinished_delegate(int thisPointer);

		// Token: 0x02000089 RID: 137
		// (Invoke) Token: 0x060004C9 RID: 1225
		internal delegate void Module_StartMissionForEditor_delegate(int thisPointer, IntPtr missionName, IntPtr sceneName, IntPtr levels);

		// Token: 0x0200008A RID: 138
		// (Invoke) Token: 0x060004CD RID: 1229
		internal delegate void Module_StartMissionForReplayEditor_delegate(int thisPointer, IntPtr missionName, IntPtr sceneName, IntPtr levels, IntPtr fileName, [MarshalAs(UnmanagedType.U1)] bool record, float startTime, float endTime);

		// Token: 0x0200008B RID: 139
		// (Invoke) Token: 0x060004D1 RID: 1233
		internal delegate void Module_TickTest_delegate(int thisPointer, float dt);

		// Token: 0x0200008C RID: 140
		// (Invoke) Token: 0x060004D5 RID: 1237
		internal delegate Vec3 WeaponComponentMissionExtensions_CalculateCenterOfMass_delegate(NativeObjectPointer body);
	}
}
