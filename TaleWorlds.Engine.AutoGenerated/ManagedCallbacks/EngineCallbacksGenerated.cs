using System;
using System.Runtime.InteropServices;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;

namespace ManagedCallbacks
{
	// Token: 0x02000006 RID: 6
	internal static class EngineCallbacksGenerated
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000020A9 File Offset: 0x000002A9
		// (set) Token: 0x0600000F RID: 15 RVA: 0x000020B0 File Offset: 0x000002B0
		internal static Delegate[] Delegates { get; private set; }

		// Token: 0x06000010 RID: 16 RVA: 0x000020B8 File Offset: 0x000002B8
		public static void Initialize()
		{
			EngineCallbacksGenerated.Delegates = new Delegate[65];
			EngineCallbacksGenerated.Delegates[0] = new EngineCallbacksGenerated.CrashInformationCollector_CollectInformation_delegate(EngineCallbacksGenerated.CrashInformationCollector_CollectInformation);
			EngineCallbacksGenerated.Delegates[1] = new EngineCallbacksGenerated.EngineController_GetApplicationPlatformName_delegate(EngineCallbacksGenerated.EngineController_GetApplicationPlatformName);
			EngineCallbacksGenerated.Delegates[2] = new EngineCallbacksGenerated.EngineController_GetModulesVersionStr_delegate(EngineCallbacksGenerated.EngineController_GetModulesVersionStr);
			EngineCallbacksGenerated.Delegates[3] = new EngineCallbacksGenerated.EngineController_GetVersionStr_delegate(EngineCallbacksGenerated.EngineController_GetVersionStr);
			EngineCallbacksGenerated.Delegates[4] = new EngineCallbacksGenerated.EngineController_Initialize_delegate(EngineCallbacksGenerated.EngineController_Initialize);
			EngineCallbacksGenerated.Delegates[5] = new EngineCallbacksGenerated.EngineController_OnConfigChange_delegate(EngineCallbacksGenerated.EngineController_OnConfigChange);
			EngineCallbacksGenerated.Delegates[6] = new EngineCallbacksGenerated.EngineController_OnConstrainedStateChange_delegate(EngineCallbacksGenerated.EngineController_OnConstrainedStateChange);
			EngineCallbacksGenerated.Delegates[7] = new EngineCallbacksGenerated.EngineController_OnControllerDisconnection_delegate(EngineCallbacksGenerated.EngineController_OnControllerDisconnection);
			EngineCallbacksGenerated.Delegates[8] = new EngineCallbacksGenerated.EngineManaged_CheckSharedStructureSizes_delegate(EngineCallbacksGenerated.EngineManaged_CheckSharedStructureSizes);
			EngineCallbacksGenerated.Delegates[9] = new EngineCallbacksGenerated.EngineManaged_EngineApiMethodInterfaceInitializer_delegate(EngineCallbacksGenerated.EngineManaged_EngineApiMethodInterfaceInitializer);
			EngineCallbacksGenerated.Delegates[10] = new EngineCallbacksGenerated.EngineManaged_FillEngineApiPointers_delegate(EngineCallbacksGenerated.EngineManaged_FillEngineApiPointers);
			EngineCallbacksGenerated.Delegates[11] = new EngineCallbacksGenerated.EngineScreenManager_InitializeLastPressedKeys_delegate(EngineCallbacksGenerated.EngineScreenManager_InitializeLastPressedKeys);
			EngineCallbacksGenerated.Delegates[12] = new EngineCallbacksGenerated.EngineScreenManager_LateTick_delegate(EngineCallbacksGenerated.EngineScreenManager_LateTick);
			EngineCallbacksGenerated.Delegates[13] = new EngineCallbacksGenerated.EngineScreenManager_OnGameWindowFocusChange_delegate(EngineCallbacksGenerated.EngineScreenManager_OnGameWindowFocusChange);
			EngineCallbacksGenerated.Delegates[14] = new EngineCallbacksGenerated.EngineScreenManager_OnOnscreenKeyboardCanceled_delegate(EngineCallbacksGenerated.EngineScreenManager_OnOnscreenKeyboardCanceled);
			EngineCallbacksGenerated.Delegates[15] = new EngineCallbacksGenerated.EngineScreenManager_OnOnscreenKeyboardDone_delegate(EngineCallbacksGenerated.EngineScreenManager_OnOnscreenKeyboardDone);
			EngineCallbacksGenerated.Delegates[16] = new EngineCallbacksGenerated.EngineScreenManager_PreTick_delegate(EngineCallbacksGenerated.EngineScreenManager_PreTick);
			EngineCallbacksGenerated.Delegates[17] = new EngineCallbacksGenerated.EngineScreenManager_Tick_delegate(EngineCallbacksGenerated.EngineScreenManager_Tick);
			EngineCallbacksGenerated.Delegates[18] = new EngineCallbacksGenerated.EngineScreenManager_Update_delegate(EngineCallbacksGenerated.EngineScreenManager_Update);
			EngineCallbacksGenerated.Delegates[19] = new EngineCallbacksGenerated.ManagedExtensions_CollectCommandLineFunctions_delegate(EngineCallbacksGenerated.ManagedExtensions_CollectCommandLineFunctions);
			EngineCallbacksGenerated.Delegates[20] = new EngineCallbacksGenerated.ManagedExtensions_CopyObjectFieldsFrom_delegate(EngineCallbacksGenerated.ManagedExtensions_CopyObjectFieldsFrom);
			EngineCallbacksGenerated.Delegates[21] = new EngineCallbacksGenerated.ManagedExtensions_CreateScriptComponentInstance_delegate(EngineCallbacksGenerated.ManagedExtensions_CreateScriptComponentInstance);
			EngineCallbacksGenerated.Delegates[22] = new EngineCallbacksGenerated.ManagedExtensions_ForceGarbageCollect_delegate(EngineCallbacksGenerated.ManagedExtensions_ForceGarbageCollect);
			EngineCallbacksGenerated.Delegates[23] = new EngineCallbacksGenerated.ManagedExtensions_GetEditorVisibilityOfField_delegate(EngineCallbacksGenerated.ManagedExtensions_GetEditorVisibilityOfField);
			EngineCallbacksGenerated.Delegates[24] = new EngineCallbacksGenerated.ManagedExtensions_GetObjectField_delegate(EngineCallbacksGenerated.ManagedExtensions_GetObjectField);
			EngineCallbacksGenerated.Delegates[25] = new EngineCallbacksGenerated.ManagedExtensions_GetScriptComponentClassNames_delegate(EngineCallbacksGenerated.ManagedExtensions_GetScriptComponentClassNames);
			EngineCallbacksGenerated.Delegates[26] = new EngineCallbacksGenerated.ManagedExtensions_GetTypeOfField_delegate(EngineCallbacksGenerated.ManagedExtensions_GetTypeOfField);
			EngineCallbacksGenerated.Delegates[27] = new EngineCallbacksGenerated.ManagedExtensions_SetObjectField_delegate(EngineCallbacksGenerated.ManagedExtensions_SetObjectField);
			EngineCallbacksGenerated.Delegates[28] = new EngineCallbacksGenerated.ManagedScriptHolder_CreateManagedScriptHolder_delegate(EngineCallbacksGenerated.ManagedScriptHolder_CreateManagedScriptHolder);
			EngineCallbacksGenerated.Delegates[29] = new EngineCallbacksGenerated.ManagedScriptHolder_GetNumberOfScripts_delegate(EngineCallbacksGenerated.ManagedScriptHolder_GetNumberOfScripts);
			EngineCallbacksGenerated.Delegates[30] = new EngineCallbacksGenerated.ManagedScriptHolder_RemoveScriptComponentFromAllTickLists_delegate(EngineCallbacksGenerated.ManagedScriptHolder_RemoveScriptComponentFromAllTickLists);
			EngineCallbacksGenerated.Delegates[31] = new EngineCallbacksGenerated.ManagedScriptHolder_SetScriptComponentHolder_delegate(EngineCallbacksGenerated.ManagedScriptHolder_SetScriptComponentHolder);
			EngineCallbacksGenerated.Delegates[32] = new EngineCallbacksGenerated.ManagedScriptHolder_TickComponents_delegate(EngineCallbacksGenerated.ManagedScriptHolder_TickComponents);
			EngineCallbacksGenerated.Delegates[33] = new EngineCallbacksGenerated.ManagedScriptHolder_TickComponentsEditor_delegate(EngineCallbacksGenerated.ManagedScriptHolder_TickComponentsEditor);
			EngineCallbacksGenerated.Delegates[34] = new EngineCallbacksGenerated.MessageManagerBase_PostMessageLine_delegate(EngineCallbacksGenerated.MessageManagerBase_PostMessageLine);
			EngineCallbacksGenerated.Delegates[35] = new EngineCallbacksGenerated.MessageManagerBase_PostMessageLineFormatted_delegate(EngineCallbacksGenerated.MessageManagerBase_PostMessageLineFormatted);
			EngineCallbacksGenerated.Delegates[36] = new EngineCallbacksGenerated.MessageManagerBase_PostSuccessLine_delegate(EngineCallbacksGenerated.MessageManagerBase_PostSuccessLine);
			EngineCallbacksGenerated.Delegates[37] = new EngineCallbacksGenerated.MessageManagerBase_PostWarningLine_delegate(EngineCallbacksGenerated.MessageManagerBase_PostWarningLine);
			EngineCallbacksGenerated.Delegates[38] = new EngineCallbacksGenerated.NativeParallelDriver_ParalelForLoopBodyCaller_delegate(EngineCallbacksGenerated.NativeParallelDriver_ParalelForLoopBodyCaller);
			EngineCallbacksGenerated.Delegates[39] = new EngineCallbacksGenerated.NativeParallelDriver_ParalelForLoopBodyWithDtCaller_delegate(EngineCallbacksGenerated.NativeParallelDriver_ParalelForLoopBodyWithDtCaller);
			EngineCallbacksGenerated.Delegates[40] = new EngineCallbacksGenerated.RenderTargetComponent_CreateRenderTargetComponent_delegate(EngineCallbacksGenerated.RenderTargetComponent_CreateRenderTargetComponent);
			EngineCallbacksGenerated.Delegates[41] = new EngineCallbacksGenerated.RenderTargetComponent_OnPaintNeeded_delegate(EngineCallbacksGenerated.RenderTargetComponent_OnPaintNeeded);
			EngineCallbacksGenerated.Delegates[42] = new EngineCallbacksGenerated.SceneProblemChecker_OnCheckForSceneProblems_delegate(EngineCallbacksGenerated.SceneProblemChecker_OnCheckForSceneProblems);
			EngineCallbacksGenerated.Delegates[43] = new EngineCallbacksGenerated.ScriptComponentBehavior_AddScriptComponentToTick_delegate(EngineCallbacksGenerated.ScriptComponentBehavior_AddScriptComponentToTick);
			EngineCallbacksGenerated.Delegates[44] = new EngineCallbacksGenerated.ScriptComponentBehavior_DeregisterAsPrefabScriptComponent_delegate(EngineCallbacksGenerated.ScriptComponentBehavior_DeregisterAsPrefabScriptComponent);
			EngineCallbacksGenerated.Delegates[45] = new EngineCallbacksGenerated.ScriptComponentBehavior_DeregisterAsUndoStackScriptComponent_delegate(EngineCallbacksGenerated.ScriptComponentBehavior_DeregisterAsUndoStackScriptComponent);
			EngineCallbacksGenerated.Delegates[46] = new EngineCallbacksGenerated.ScriptComponentBehavior_DisablesOroCreation_delegate(EngineCallbacksGenerated.ScriptComponentBehavior_DisablesOroCreation);
			EngineCallbacksGenerated.Delegates[47] = new EngineCallbacksGenerated.ScriptComponentBehavior_GetEditableFields_delegate(EngineCallbacksGenerated.ScriptComponentBehavior_GetEditableFields);
			EngineCallbacksGenerated.Delegates[48] = new EngineCallbacksGenerated.ScriptComponentBehavior_HandleOnRemoved_delegate(EngineCallbacksGenerated.ScriptComponentBehavior_HandleOnRemoved);
			EngineCallbacksGenerated.Delegates[49] = new EngineCallbacksGenerated.ScriptComponentBehavior_IsOnlyVisual_delegate(EngineCallbacksGenerated.ScriptComponentBehavior_IsOnlyVisual);
			EngineCallbacksGenerated.Delegates[50] = new EngineCallbacksGenerated.ScriptComponentBehavior_MovesEntity_delegate(EngineCallbacksGenerated.ScriptComponentBehavior_MovesEntity);
			EngineCallbacksGenerated.Delegates[51] = new EngineCallbacksGenerated.ScriptComponentBehavior_OnCheckForProblems_delegate(EngineCallbacksGenerated.ScriptComponentBehavior_OnCheckForProblems);
			EngineCallbacksGenerated.Delegates[52] = new EngineCallbacksGenerated.ScriptComponentBehavior_OnEditModeVisibilityChanged_delegate(EngineCallbacksGenerated.ScriptComponentBehavior_OnEditModeVisibilityChanged);
			EngineCallbacksGenerated.Delegates[53] = new EngineCallbacksGenerated.ScriptComponentBehavior_OnEditorInit_delegate(EngineCallbacksGenerated.ScriptComponentBehavior_OnEditorInit);
			EngineCallbacksGenerated.Delegates[54] = new EngineCallbacksGenerated.ScriptComponentBehavior_OnEditorTick_delegate(EngineCallbacksGenerated.ScriptComponentBehavior_OnEditorTick);
			EngineCallbacksGenerated.Delegates[55] = new EngineCallbacksGenerated.ScriptComponentBehavior_OnEditorValidate_delegate(EngineCallbacksGenerated.ScriptComponentBehavior_OnEditorValidate);
			EngineCallbacksGenerated.Delegates[56] = new EngineCallbacksGenerated.ScriptComponentBehavior_OnEditorVariableChanged_delegate(EngineCallbacksGenerated.ScriptComponentBehavior_OnEditorVariableChanged);
			EngineCallbacksGenerated.Delegates[57] = new EngineCallbacksGenerated.ScriptComponentBehavior_OnInit_delegate(EngineCallbacksGenerated.ScriptComponentBehavior_OnInit);
			EngineCallbacksGenerated.Delegates[58] = new EngineCallbacksGenerated.ScriptComponentBehavior_OnPhysicsCollision_delegate(EngineCallbacksGenerated.ScriptComponentBehavior_OnPhysicsCollision);
			EngineCallbacksGenerated.Delegates[59] = new EngineCallbacksGenerated.ScriptComponentBehavior_OnPreInit_delegate(EngineCallbacksGenerated.ScriptComponentBehavior_OnPreInit);
			EngineCallbacksGenerated.Delegates[60] = new EngineCallbacksGenerated.ScriptComponentBehavior_OnSceneSave_delegate(EngineCallbacksGenerated.ScriptComponentBehavior_OnSceneSave);
			EngineCallbacksGenerated.Delegates[61] = new EngineCallbacksGenerated.ScriptComponentBehavior_RegisterAsPrefabScriptComponent_delegate(EngineCallbacksGenerated.ScriptComponentBehavior_RegisterAsPrefabScriptComponent);
			EngineCallbacksGenerated.Delegates[62] = new EngineCallbacksGenerated.ScriptComponentBehavior_RegisterAsUndoStackScriptComponent_delegate(EngineCallbacksGenerated.ScriptComponentBehavior_RegisterAsUndoStackScriptComponent);
			EngineCallbacksGenerated.Delegates[63] = new EngineCallbacksGenerated.ScriptComponentBehavior_SetScene_delegate(EngineCallbacksGenerated.ScriptComponentBehavior_SetScene);
			EngineCallbacksGenerated.Delegates[64] = new EngineCallbacksGenerated.ThumbnailCreatorView_OnThumbnailRenderComplete_delegate(EngineCallbacksGenerated.ThumbnailCreatorView_OnThumbnailRenderComplete);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000025DC File Offset: 0x000007DC
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.CrashInformationCollector_CollectInformation_delegate))]
		internal static UIntPtr CrashInformationCollector_CollectInformation()
		{
			string text = CrashInformationCollector.CollectInformation();
			UIntPtr threadLocalCachedRglVarString = NativeStringHelper.GetThreadLocalCachedRglVarString();
			NativeStringHelper.SetRglVarString(threadLocalCachedRglVarString, text);
			return threadLocalCachedRglVarString;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000025FC File Offset: 0x000007FC
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.EngineController_GetApplicationPlatformName_delegate))]
		internal static UIntPtr EngineController_GetApplicationPlatformName()
		{
			string applicationPlatformName = EngineController.GetApplicationPlatformName();
			UIntPtr threadLocalCachedRglVarString = NativeStringHelper.GetThreadLocalCachedRglVarString();
			NativeStringHelper.SetRglVarString(threadLocalCachedRglVarString, applicationPlatformName);
			return threadLocalCachedRglVarString;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000261C File Offset: 0x0000081C
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.EngineController_GetModulesVersionStr_delegate))]
		internal static UIntPtr EngineController_GetModulesVersionStr()
		{
			string modulesVersionStr = EngineController.GetModulesVersionStr();
			UIntPtr threadLocalCachedRglVarString = NativeStringHelper.GetThreadLocalCachedRglVarString();
			NativeStringHelper.SetRglVarString(threadLocalCachedRglVarString, modulesVersionStr);
			return threadLocalCachedRglVarString;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000263C File Offset: 0x0000083C
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.EngineController_GetVersionStr_delegate))]
		internal static UIntPtr EngineController_GetVersionStr()
		{
			string versionStr = EngineController.GetVersionStr();
			UIntPtr threadLocalCachedRglVarString = NativeStringHelper.GetThreadLocalCachedRglVarString();
			NativeStringHelper.SetRglVarString(threadLocalCachedRglVarString, versionStr);
			return threadLocalCachedRglVarString;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000265B File Offset: 0x0000085B
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.EngineController_Initialize_delegate))]
		internal static void EngineController_Initialize()
		{
			EngineController.Initialize();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002662 File Offset: 0x00000862
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.EngineController_OnConfigChange_delegate))]
		internal static void EngineController_OnConfigChange()
		{
			EngineController.OnConfigChange();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002669 File Offset: 0x00000869
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.EngineController_OnConstrainedStateChange_delegate))]
		internal static void EngineController_OnConstrainedStateChange(bool isConstrained)
		{
			EngineController.OnConstrainedStateChange(isConstrained);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002671 File Offset: 0x00000871
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.EngineController_OnControllerDisconnection_delegate))]
		internal static void EngineController_OnControllerDisconnection()
		{
			EngineController.OnControllerDisconnection();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002678 File Offset: 0x00000878
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.EngineManaged_CheckSharedStructureSizes_delegate))]
		internal static void EngineManaged_CheckSharedStructureSizes()
		{
			EngineManaged.CheckSharedStructureSizes();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000267F File Offset: 0x0000087F
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.EngineManaged_EngineApiMethodInterfaceInitializer_delegate))]
		internal static void EngineManaged_EngineApiMethodInterfaceInitializer(int id, IntPtr pointer)
		{
			EngineManaged.EngineApiMethodInterfaceInitializer(id, pointer);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002688 File Offset: 0x00000888
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.EngineManaged_FillEngineApiPointers_delegate))]
		internal static void EngineManaged_FillEngineApiPointers()
		{
			EngineManaged.FillEngineApiPointers();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002690 File Offset: 0x00000890
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.EngineScreenManager_InitializeLastPressedKeys_delegate))]
		internal static void EngineScreenManager_InitializeLastPressedKeys(NativeObjectPointer lastKeysPressed)
		{
			NativeArray lastKeysPressed2 = null;
			if (lastKeysPressed.Pointer != UIntPtr.Zero)
			{
				lastKeysPressed2 = new NativeArray(lastKeysPressed.Pointer);
			}
			EngineScreenManager.InitializeLastPressedKeys(lastKeysPressed2);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000026C3 File Offset: 0x000008C3
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.EngineScreenManager_LateTick_delegate))]
		internal static void EngineScreenManager_LateTick(float dt)
		{
			EngineScreenManager.LateTick(dt);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000026CB File Offset: 0x000008CB
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.EngineScreenManager_OnGameWindowFocusChange_delegate))]
		internal static void EngineScreenManager_OnGameWindowFocusChange(bool focusGained)
		{
			EngineScreenManager.OnGameWindowFocusChange(focusGained);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000026D3 File Offset: 0x000008D3
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.EngineScreenManager_OnOnscreenKeyboardCanceled_delegate))]
		internal static void EngineScreenManager_OnOnscreenKeyboardCanceled()
		{
			EngineScreenManager.OnOnscreenKeyboardCanceled();
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000026DA File Offset: 0x000008DA
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.EngineScreenManager_OnOnscreenKeyboardDone_delegate))]
		internal static void EngineScreenManager_OnOnscreenKeyboardDone(IntPtr inputText)
		{
			EngineScreenManager.OnOnscreenKeyboardDone(Marshal.PtrToStringAnsi(inputText));
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000026E7 File Offset: 0x000008E7
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.EngineScreenManager_PreTick_delegate))]
		internal static void EngineScreenManager_PreTick(float dt)
		{
			EngineScreenManager.PreTick(dt);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000026EF File Offset: 0x000008EF
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.EngineScreenManager_Tick_delegate))]
		internal static void EngineScreenManager_Tick(float dt)
		{
			EngineScreenManager.Tick(dt);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000026F7 File Offset: 0x000008F7
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.EngineScreenManager_Update_delegate))]
		internal static void EngineScreenManager_Update()
		{
			EngineScreenManager.Update();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000026FE File Offset: 0x000008FE
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.ManagedExtensions_CollectCommandLineFunctions_delegate))]
		internal static void ManagedExtensions_CollectCommandLineFunctions()
		{
			ManagedExtensions.CollectCommandLineFunctions();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002708 File Offset: 0x00000908
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.ManagedExtensions_CopyObjectFieldsFrom_delegate))]
		internal static void ManagedExtensions_CopyObjectFieldsFrom(int dst, int src, IntPtr className, int callFieldChangeEventAsInteger)
		{
			DotNetObject managedObjectWithId = DotNetObject.GetManagedObjectWithId(dst);
			DotNetObject managedObjectWithId2 = DotNetObject.GetManagedObjectWithId(src);
			string className2 = Marshal.PtrToStringAnsi(className);
			ManagedExtensions.CopyObjectFieldsFrom(managedObjectWithId, managedObjectWithId2, className2, callFieldChangeEventAsInteger);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002734 File Offset: 0x00000934
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.ManagedExtensions_CreateScriptComponentInstance_delegate))]
		internal static int ManagedExtensions_CreateScriptComponentInstance(IntPtr className, NativeObjectPointer entity, NativeObjectPointer managedScriptComponent)
		{
			string className2 = Marshal.PtrToStringAnsi(className);
			GameEntity entity2 = null;
			if (entity.Pointer != UIntPtr.Zero)
			{
				entity2 = new GameEntity(entity.Pointer);
			}
			ManagedScriptComponent managedScriptComponent2 = null;
			if (managedScriptComponent.Pointer != UIntPtr.Zero)
			{
				managedScriptComponent2 = new ManagedScriptComponent(managedScriptComponent.Pointer);
			}
			return ManagedExtensions.CreateScriptComponentInstance(className2, entity2, managedScriptComponent2).GetManagedId();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002793 File Offset: 0x00000993
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.ManagedExtensions_ForceGarbageCollect_delegate))]
		internal static void ManagedExtensions_ForceGarbageCollect()
		{
			ManagedExtensions.ForceGarbageCollect();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000279C File Offset: 0x0000099C
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.ManagedExtensions_GetEditorVisibilityOfField_delegate))]
		internal static bool ManagedExtensions_GetEditorVisibilityOfField(IntPtr className, IntPtr fieldName)
		{
			string className2 = Marshal.PtrToStringAnsi(className);
			string fieldName2 = Marshal.PtrToStringAnsi(fieldName);
			return ManagedExtensions.GetEditorVisibilityOfField(className2, fieldName2);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000027BC File Offset: 0x000009BC
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.ManagedExtensions_GetObjectField_delegate))]
		internal static void ManagedExtensions_GetObjectField(int managedObject, ref ScriptComponentFieldHolder scriptComponentFieldHolder, IntPtr fieldName, int type)
		{
			DotNetObject managedObjectWithId = DotNetObject.GetManagedObjectWithId(managedObject);
			string fieldName2 = Marshal.PtrToStringAnsi(fieldName);
			ManagedExtensions.GetObjectField(managedObjectWithId, ref scriptComponentFieldHolder, fieldName2, type);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000027E0 File Offset: 0x000009E0
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.ManagedExtensions_GetScriptComponentClassNames_delegate))]
		internal static UIntPtr ManagedExtensions_GetScriptComponentClassNames()
		{
			string scriptComponentClassNames = ManagedExtensions.GetScriptComponentClassNames();
			UIntPtr threadLocalCachedRglVarString = NativeStringHelper.GetThreadLocalCachedRglVarString();
			NativeStringHelper.SetRglVarString(threadLocalCachedRglVarString, scriptComponentClassNames);
			return threadLocalCachedRglVarString;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002800 File Offset: 0x00000A00
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.ManagedExtensions_GetTypeOfField_delegate))]
		internal static int ManagedExtensions_GetTypeOfField(IntPtr className, IntPtr fieldName)
		{
			string className2 = Marshal.PtrToStringAnsi(className);
			string fieldName2 = Marshal.PtrToStringAnsi(fieldName);
			return ManagedExtensions.GetTypeOfField(className2, fieldName2);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002820 File Offset: 0x00000A20
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.ManagedExtensions_SetObjectField_delegate))]
		internal static void ManagedExtensions_SetObjectField(int managedObject, IntPtr fieldName, ref ScriptComponentFieldHolder scriptComponentHolder, int type, int callFieldChangeEventAsInteger)
		{
			DotNetObject managedObjectWithId = DotNetObject.GetManagedObjectWithId(managedObject);
			string fieldName2 = Marshal.PtrToStringAnsi(fieldName);
			ManagedExtensions.SetObjectField(managedObjectWithId, fieldName2, ref scriptComponentHolder, type, callFieldChangeEventAsInteger);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002844 File Offset: 0x00000A44
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.ManagedScriptHolder_CreateManagedScriptHolder_delegate))]
		internal static int ManagedScriptHolder_CreateManagedScriptHolder()
		{
			return ManagedScriptHolder.CreateManagedScriptHolder().GetManagedId();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002850 File Offset: 0x00000A50
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.ManagedScriptHolder_GetNumberOfScripts_delegate))]
		internal static int ManagedScriptHolder_GetNumberOfScripts(int thisPointer)
		{
			return (DotNetObject.GetManagedObjectWithId(thisPointer) as ManagedScriptHolder).GetNumberOfScripts();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002864 File Offset: 0x00000A64
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.ManagedScriptHolder_RemoveScriptComponentFromAllTickLists_delegate))]
		internal static void ManagedScriptHolder_RemoveScriptComponentFromAllTickLists(int thisPointer, int sc)
		{
			ManagedScriptHolder managedScriptHolder = DotNetObject.GetManagedObjectWithId(thisPointer) as ManagedScriptHolder;
			ScriptComponentBehavior sc2 = DotNetObject.GetManagedObjectWithId(sc) as ScriptComponentBehavior;
			managedScriptHolder.RemoveScriptComponentFromAllTickLists(sc2);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002890 File Offset: 0x00000A90
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.ManagedScriptHolder_SetScriptComponentHolder_delegate))]
		internal static void ManagedScriptHolder_SetScriptComponentHolder(int thisPointer, int sc)
		{
			ManagedScriptHolder managedScriptHolder = DotNetObject.GetManagedObjectWithId(thisPointer) as ManagedScriptHolder;
			ScriptComponentBehavior scriptComponentHolder = DotNetObject.GetManagedObjectWithId(sc) as ScriptComponentBehavior;
			managedScriptHolder.SetScriptComponentHolder(scriptComponentHolder);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000028BA File Offset: 0x00000ABA
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.ManagedScriptHolder_TickComponents_delegate))]
		internal static void ManagedScriptHolder_TickComponents(int thisPointer, float dt)
		{
			(DotNetObject.GetManagedObjectWithId(thisPointer) as ManagedScriptHolder).TickComponents(dt);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000028CD File Offset: 0x00000ACD
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.ManagedScriptHolder_TickComponentsEditor_delegate))]
		internal static void ManagedScriptHolder_TickComponentsEditor(int thisPointer, float dt)
		{
			(DotNetObject.GetManagedObjectWithId(thisPointer) as ManagedScriptHolder).TickComponentsEditor(dt);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000028E0 File Offset: 0x00000AE0
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.MessageManagerBase_PostMessageLine_delegate))]
		internal static void MessageManagerBase_PostMessageLine(int thisPointer, IntPtr text, uint color)
		{
			MessageManagerBase messageManagerBase = DotNetObject.GetManagedObjectWithId(thisPointer) as MessageManagerBase;
			string text2 = Marshal.PtrToStringAnsi(text);
			messageManagerBase.PostMessageLine(text2, color);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002908 File Offset: 0x00000B08
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.MessageManagerBase_PostMessageLineFormatted_delegate))]
		internal static void MessageManagerBase_PostMessageLineFormatted(int thisPointer, IntPtr text, uint color)
		{
			MessageManagerBase messageManagerBase = DotNetObject.GetManagedObjectWithId(thisPointer) as MessageManagerBase;
			string text2 = Marshal.PtrToStringAnsi(text);
			messageManagerBase.PostMessageLineFormatted(text2, color);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002930 File Offset: 0x00000B30
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.MessageManagerBase_PostSuccessLine_delegate))]
		internal static void MessageManagerBase_PostSuccessLine(int thisPointer, IntPtr text)
		{
			MessageManagerBase messageManagerBase = DotNetObject.GetManagedObjectWithId(thisPointer) as MessageManagerBase;
			string text2 = Marshal.PtrToStringAnsi(text);
			messageManagerBase.PostSuccessLine(text2);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002958 File Offset: 0x00000B58
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.MessageManagerBase_PostWarningLine_delegate))]
		internal static void MessageManagerBase_PostWarningLine(int thisPointer, IntPtr text)
		{
			MessageManagerBase messageManagerBase = DotNetObject.GetManagedObjectWithId(thisPointer) as MessageManagerBase;
			string text2 = Marshal.PtrToStringAnsi(text);
			messageManagerBase.PostWarningLine(text2);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000297D File Offset: 0x00000B7D
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.NativeParallelDriver_ParalelForLoopBodyCaller_delegate))]
		internal static void NativeParallelDriver_ParalelForLoopBodyCaller(long loopBodyKey, int localStartIndex, int localEndIndex)
		{
			NativeParallelDriver.ParalelForLoopBodyCaller(loopBodyKey, localStartIndex, localEndIndex);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002987 File Offset: 0x00000B87
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.NativeParallelDriver_ParalelForLoopBodyWithDtCaller_delegate))]
		internal static void NativeParallelDriver_ParalelForLoopBodyWithDtCaller(long loopBodyKey, int localStartIndex, int localEndIndex)
		{
			NativeParallelDriver.ParalelForLoopBodyWithDtCaller(loopBodyKey, localStartIndex, localEndIndex);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002994 File Offset: 0x00000B94
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.RenderTargetComponent_CreateRenderTargetComponent_delegate))]
		internal static int RenderTargetComponent_CreateRenderTargetComponent(NativeObjectPointer renderTarget)
		{
			Texture renderTarget2 = null;
			if (renderTarget.Pointer != UIntPtr.Zero)
			{
				renderTarget2 = new Texture(renderTarget.Pointer);
			}
			return RenderTargetComponent.CreateRenderTargetComponent(renderTarget2).GetManagedId();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000029CC File Offset: 0x00000BCC
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.RenderTargetComponent_OnPaintNeeded_delegate))]
		internal static void RenderTargetComponent_OnPaintNeeded(int thisPointer)
		{
			(DotNetObject.GetManagedObjectWithId(thisPointer) as RenderTargetComponent).OnPaintNeeded();
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000029E0 File Offset: 0x00000BE0
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.SceneProblemChecker_OnCheckForSceneProblems_delegate))]
		internal static bool SceneProblemChecker_OnCheckForSceneProblems(NativeObjectPointer scene)
		{
			Scene scene2 = null;
			if (scene.Pointer != UIntPtr.Zero)
			{
				scene2 = new Scene(scene.Pointer);
			}
			return SceneProblemChecker.OnCheckForSceneProblems(scene2);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002A13 File Offset: 0x00000C13
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.ScriptComponentBehavior_AddScriptComponentToTick_delegate))]
		internal static void ScriptComponentBehavior_AddScriptComponentToTick(int thisPointer)
		{
			(DotNetObject.GetManagedObjectWithId(thisPointer) as ScriptComponentBehavior).AddScriptComponentToTick();
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002A25 File Offset: 0x00000C25
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.ScriptComponentBehavior_DeregisterAsPrefabScriptComponent_delegate))]
		internal static void ScriptComponentBehavior_DeregisterAsPrefabScriptComponent(int thisPointer)
		{
			(DotNetObject.GetManagedObjectWithId(thisPointer) as ScriptComponentBehavior).DeregisterAsPrefabScriptComponent();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002A37 File Offset: 0x00000C37
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.ScriptComponentBehavior_DeregisterAsUndoStackScriptComponent_delegate))]
		internal static void ScriptComponentBehavior_DeregisterAsUndoStackScriptComponent(int thisPointer)
		{
			(DotNetObject.GetManagedObjectWithId(thisPointer) as ScriptComponentBehavior).DeregisterAsUndoStackScriptComponent();
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002A49 File Offset: 0x00000C49
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.ScriptComponentBehavior_DisablesOroCreation_delegate))]
		internal static bool ScriptComponentBehavior_DisablesOroCreation(int thisPointer)
		{
			return (DotNetObject.GetManagedObjectWithId(thisPointer) as ScriptComponentBehavior).DisablesOroCreation();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002A5B File Offset: 0x00000C5B
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.ScriptComponentBehavior_GetEditableFields_delegate))]
		internal static int ScriptComponentBehavior_GetEditableFields(IntPtr className)
		{
			return Managed.AddCustomParameter<string[]>(ScriptComponentBehavior.GetEditableFields(Marshal.PtrToStringAnsi(className))).GetManagedId();
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002A72 File Offset: 0x00000C72
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.ScriptComponentBehavior_HandleOnRemoved_delegate))]
		internal static void ScriptComponentBehavior_HandleOnRemoved(int thisPointer, int removeReason)
		{
			(DotNetObject.GetManagedObjectWithId(thisPointer) as ScriptComponentBehavior).HandleOnRemoved(removeReason);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002A85 File Offset: 0x00000C85
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.ScriptComponentBehavior_IsOnlyVisual_delegate))]
		internal static bool ScriptComponentBehavior_IsOnlyVisual(int thisPointer)
		{
			return (DotNetObject.GetManagedObjectWithId(thisPointer) as ScriptComponentBehavior).IsOnlyVisual();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002A97 File Offset: 0x00000C97
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.ScriptComponentBehavior_MovesEntity_delegate))]
		internal static bool ScriptComponentBehavior_MovesEntity(int thisPointer)
		{
			return (DotNetObject.GetManagedObjectWithId(thisPointer) as ScriptComponentBehavior).MovesEntity();
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002AA9 File Offset: 0x00000CA9
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.ScriptComponentBehavior_OnCheckForProblems_delegate))]
		internal static bool ScriptComponentBehavior_OnCheckForProblems(int thisPointer)
		{
			return (DotNetObject.GetManagedObjectWithId(thisPointer) as ScriptComponentBehavior).OnCheckForProblems();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002ABB File Offset: 0x00000CBB
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.ScriptComponentBehavior_OnEditModeVisibilityChanged_delegate))]
		internal static void ScriptComponentBehavior_OnEditModeVisibilityChanged(int thisPointer, bool currentVisibility)
		{
			(DotNetObject.GetManagedObjectWithId(thisPointer) as ScriptComponentBehavior).OnEditModeVisibilityChanged(currentVisibility);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002ACE File Offset: 0x00000CCE
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.ScriptComponentBehavior_OnEditorInit_delegate))]
		internal static void ScriptComponentBehavior_OnEditorInit(int thisPointer)
		{
			(DotNetObject.GetManagedObjectWithId(thisPointer) as ScriptComponentBehavior).OnEditorInit();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002AE0 File Offset: 0x00000CE0
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.ScriptComponentBehavior_OnEditorTick_delegate))]
		internal static void ScriptComponentBehavior_OnEditorTick(int thisPointer, float dt)
		{
			(DotNetObject.GetManagedObjectWithId(thisPointer) as ScriptComponentBehavior).OnEditorTick(dt);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002AF3 File Offset: 0x00000CF3
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.ScriptComponentBehavior_OnEditorValidate_delegate))]
		internal static void ScriptComponentBehavior_OnEditorValidate(int thisPointer)
		{
			(DotNetObject.GetManagedObjectWithId(thisPointer) as ScriptComponentBehavior).OnEditorValidate();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002B08 File Offset: 0x00000D08
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.ScriptComponentBehavior_OnEditorVariableChanged_delegate))]
		internal static void ScriptComponentBehavior_OnEditorVariableChanged(int thisPointer, IntPtr variableName)
		{
			ScriptComponentBehavior scriptComponentBehavior = DotNetObject.GetManagedObjectWithId(thisPointer) as ScriptComponentBehavior;
			string variableName2 = Marshal.PtrToStringAnsi(variableName);
			scriptComponentBehavior.OnEditorVariableChanged(variableName2);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002B2D File Offset: 0x00000D2D
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.ScriptComponentBehavior_OnInit_delegate))]
		internal static void ScriptComponentBehavior_OnInit(int thisPointer)
		{
			(DotNetObject.GetManagedObjectWithId(thisPointer) as ScriptComponentBehavior).OnInit();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002B3F File Offset: 0x00000D3F
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.ScriptComponentBehavior_OnPhysicsCollision_delegate))]
		internal static void ScriptComponentBehavior_OnPhysicsCollision(int thisPointer, ref PhysicsContact contact)
		{
			(DotNetObject.GetManagedObjectWithId(thisPointer) as ScriptComponentBehavior).OnPhysicsCollision(ref contact);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002B52 File Offset: 0x00000D52
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.ScriptComponentBehavior_OnPreInit_delegate))]
		internal static void ScriptComponentBehavior_OnPreInit(int thisPointer)
		{
			(DotNetObject.GetManagedObjectWithId(thisPointer) as ScriptComponentBehavior).OnPreInit();
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002B64 File Offset: 0x00000D64
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.ScriptComponentBehavior_OnSceneSave_delegate))]
		internal static void ScriptComponentBehavior_OnSceneSave(int thisPointer, IntPtr saveFolder)
		{
			ScriptComponentBehavior scriptComponentBehavior = DotNetObject.GetManagedObjectWithId(thisPointer) as ScriptComponentBehavior;
			string saveFolder2 = Marshal.PtrToStringAnsi(saveFolder);
			scriptComponentBehavior.OnSceneSave(saveFolder2);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002B89 File Offset: 0x00000D89
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.ScriptComponentBehavior_RegisterAsPrefabScriptComponent_delegate))]
		internal static void ScriptComponentBehavior_RegisterAsPrefabScriptComponent(int thisPointer)
		{
			(DotNetObject.GetManagedObjectWithId(thisPointer) as ScriptComponentBehavior).RegisterAsPrefabScriptComponent();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002B9B File Offset: 0x00000D9B
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.ScriptComponentBehavior_RegisterAsUndoStackScriptComponent_delegate))]
		internal static void ScriptComponentBehavior_RegisterAsUndoStackScriptComponent(int thisPointer)
		{
			(DotNetObject.GetManagedObjectWithId(thisPointer) as ScriptComponentBehavior).RegisterAsUndoStackScriptComponent();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002BB0 File Offset: 0x00000DB0
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.ScriptComponentBehavior_SetScene_delegate))]
		internal static void ScriptComponentBehavior_SetScene(int thisPointer, NativeObjectPointer scene)
		{
			ScriptComponentBehavior scriptComponentBehavior = DotNetObject.GetManagedObjectWithId(thisPointer) as ScriptComponentBehavior;
			Scene scene2 = null;
			if (scene.Pointer != UIntPtr.Zero)
			{
				scene2 = new Scene(scene.Pointer);
			}
			scriptComponentBehavior.SetScene(scene2);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002BF0 File Offset: 0x00000DF0
		[MonoPInvokeCallback(typeof(EngineCallbacksGenerated.ThumbnailCreatorView_OnThumbnailRenderComplete_delegate))]
		internal static void ThumbnailCreatorView_OnThumbnailRenderComplete(IntPtr renderId, NativeObjectPointer renderTarget)
		{
			string renderId2 = Marshal.PtrToStringAnsi(renderId);
			Texture renderTarget2 = null;
			if (renderTarget.Pointer != UIntPtr.Zero)
			{
				renderTarget2 = new Texture(renderTarget.Pointer);
			}
			ThumbnailCreatorView.OnThumbnailRenderComplete(renderId2, renderTarget2);
		}

		// Token: 0x02000033 RID: 51
		// (Invoke) Token: 0x060005CF RID: 1487
		internal delegate UIntPtr CrashInformationCollector_CollectInformation_delegate();

		// Token: 0x02000034 RID: 52
		// (Invoke) Token: 0x060005D3 RID: 1491
		internal delegate UIntPtr EngineController_GetApplicationPlatformName_delegate();

		// Token: 0x02000035 RID: 53
		// (Invoke) Token: 0x060005D7 RID: 1495
		internal delegate UIntPtr EngineController_GetModulesVersionStr_delegate();

		// Token: 0x02000036 RID: 54
		// (Invoke) Token: 0x060005DB RID: 1499
		internal delegate UIntPtr EngineController_GetVersionStr_delegate();

		// Token: 0x02000037 RID: 55
		// (Invoke) Token: 0x060005DF RID: 1503
		internal delegate void EngineController_Initialize_delegate();

		// Token: 0x02000038 RID: 56
		// (Invoke) Token: 0x060005E3 RID: 1507
		internal delegate void EngineController_OnConfigChange_delegate();

		// Token: 0x02000039 RID: 57
		// (Invoke) Token: 0x060005E7 RID: 1511
		internal delegate void EngineController_OnConstrainedStateChange_delegate([MarshalAs(UnmanagedType.U1)] bool isConstrained);

		// Token: 0x0200003A RID: 58
		// (Invoke) Token: 0x060005EB RID: 1515
		internal delegate void EngineController_OnControllerDisconnection_delegate();

		// Token: 0x0200003B RID: 59
		// (Invoke) Token: 0x060005EF RID: 1519
		internal delegate void EngineManaged_CheckSharedStructureSizes_delegate();

		// Token: 0x0200003C RID: 60
		// (Invoke) Token: 0x060005F3 RID: 1523
		internal delegate void EngineManaged_EngineApiMethodInterfaceInitializer_delegate(int id, IntPtr pointer);

		// Token: 0x0200003D RID: 61
		// (Invoke) Token: 0x060005F7 RID: 1527
		internal delegate void EngineManaged_FillEngineApiPointers_delegate();

		// Token: 0x0200003E RID: 62
		// (Invoke) Token: 0x060005FB RID: 1531
		internal delegate void EngineScreenManager_InitializeLastPressedKeys_delegate(NativeObjectPointer lastKeysPressed);

		// Token: 0x0200003F RID: 63
		// (Invoke) Token: 0x060005FF RID: 1535
		internal delegate void EngineScreenManager_LateTick_delegate(float dt);

		// Token: 0x02000040 RID: 64
		// (Invoke) Token: 0x06000603 RID: 1539
		internal delegate void EngineScreenManager_OnGameWindowFocusChange_delegate([MarshalAs(UnmanagedType.U1)] bool focusGained);

		// Token: 0x02000041 RID: 65
		// (Invoke) Token: 0x06000607 RID: 1543
		internal delegate void EngineScreenManager_OnOnscreenKeyboardCanceled_delegate();

		// Token: 0x02000042 RID: 66
		// (Invoke) Token: 0x0600060B RID: 1547
		internal delegate void EngineScreenManager_OnOnscreenKeyboardDone_delegate(IntPtr inputText);

		// Token: 0x02000043 RID: 67
		// (Invoke) Token: 0x0600060F RID: 1551
		internal delegate void EngineScreenManager_PreTick_delegate(float dt);

		// Token: 0x02000044 RID: 68
		// (Invoke) Token: 0x06000613 RID: 1555
		internal delegate void EngineScreenManager_Tick_delegate(float dt);

		// Token: 0x02000045 RID: 69
		// (Invoke) Token: 0x06000617 RID: 1559
		internal delegate void EngineScreenManager_Update_delegate();

		// Token: 0x02000046 RID: 70
		// (Invoke) Token: 0x0600061B RID: 1563
		internal delegate void ManagedExtensions_CollectCommandLineFunctions_delegate();

		// Token: 0x02000047 RID: 71
		// (Invoke) Token: 0x0600061F RID: 1567
		internal delegate void ManagedExtensions_CopyObjectFieldsFrom_delegate(int dst, int src, IntPtr className, int callFieldChangeEventAsInteger);

		// Token: 0x02000048 RID: 72
		// (Invoke) Token: 0x06000623 RID: 1571
		internal delegate int ManagedExtensions_CreateScriptComponentInstance_delegate(IntPtr className, NativeObjectPointer entity, NativeObjectPointer managedScriptComponent);

		// Token: 0x02000049 RID: 73
		// (Invoke) Token: 0x06000627 RID: 1575
		internal delegate void ManagedExtensions_ForceGarbageCollect_delegate();

		// Token: 0x0200004A RID: 74
		// (Invoke) Token: 0x0600062B RID: 1579
		[return: MarshalAs(UnmanagedType.U1)]
		internal delegate bool ManagedExtensions_GetEditorVisibilityOfField_delegate(IntPtr className, IntPtr fieldName);

		// Token: 0x0200004B RID: 75
		// (Invoke) Token: 0x0600062F RID: 1583
		internal delegate void ManagedExtensions_GetObjectField_delegate(int managedObject, ref ScriptComponentFieldHolder scriptComponentFieldHolder, IntPtr fieldName, int type);

		// Token: 0x0200004C RID: 76
		// (Invoke) Token: 0x06000633 RID: 1587
		internal delegate UIntPtr ManagedExtensions_GetScriptComponentClassNames_delegate();

		// Token: 0x0200004D RID: 77
		// (Invoke) Token: 0x06000637 RID: 1591
		internal delegate int ManagedExtensions_GetTypeOfField_delegate(IntPtr className, IntPtr fieldName);

		// Token: 0x0200004E RID: 78
		// (Invoke) Token: 0x0600063B RID: 1595
		internal delegate void ManagedExtensions_SetObjectField_delegate(int managedObject, IntPtr fieldName, ref ScriptComponentFieldHolder scriptComponentHolder, int type, int callFieldChangeEventAsInteger);

		// Token: 0x0200004F RID: 79
		// (Invoke) Token: 0x0600063F RID: 1599
		internal delegate int ManagedScriptHolder_CreateManagedScriptHolder_delegate();

		// Token: 0x02000050 RID: 80
		// (Invoke) Token: 0x06000643 RID: 1603
		internal delegate int ManagedScriptHolder_GetNumberOfScripts_delegate(int thisPointer);

		// Token: 0x02000051 RID: 81
		// (Invoke) Token: 0x06000647 RID: 1607
		internal delegate void ManagedScriptHolder_RemoveScriptComponentFromAllTickLists_delegate(int thisPointer, int sc);

		// Token: 0x02000052 RID: 82
		// (Invoke) Token: 0x0600064B RID: 1611
		internal delegate void ManagedScriptHolder_SetScriptComponentHolder_delegate(int thisPointer, int sc);

		// Token: 0x02000053 RID: 83
		// (Invoke) Token: 0x0600064F RID: 1615
		internal delegate void ManagedScriptHolder_TickComponents_delegate(int thisPointer, float dt);

		// Token: 0x02000054 RID: 84
		// (Invoke) Token: 0x06000653 RID: 1619
		internal delegate void ManagedScriptHolder_TickComponentsEditor_delegate(int thisPointer, float dt);

		// Token: 0x02000055 RID: 85
		// (Invoke) Token: 0x06000657 RID: 1623
		internal delegate void MessageManagerBase_PostMessageLine_delegate(int thisPointer, IntPtr text, uint color);

		// Token: 0x02000056 RID: 86
		// (Invoke) Token: 0x0600065B RID: 1627
		internal delegate void MessageManagerBase_PostMessageLineFormatted_delegate(int thisPointer, IntPtr text, uint color);

		// Token: 0x02000057 RID: 87
		// (Invoke) Token: 0x0600065F RID: 1631
		internal delegate void MessageManagerBase_PostSuccessLine_delegate(int thisPointer, IntPtr text);

		// Token: 0x02000058 RID: 88
		// (Invoke) Token: 0x06000663 RID: 1635
		internal delegate void MessageManagerBase_PostWarningLine_delegate(int thisPointer, IntPtr text);

		// Token: 0x02000059 RID: 89
		// (Invoke) Token: 0x06000667 RID: 1639
		internal delegate void NativeParallelDriver_ParalelForLoopBodyCaller_delegate(long loopBodyKey, int localStartIndex, int localEndIndex);

		// Token: 0x0200005A RID: 90
		// (Invoke) Token: 0x0600066B RID: 1643
		internal delegate void NativeParallelDriver_ParalelForLoopBodyWithDtCaller_delegate(long loopBodyKey, int localStartIndex, int localEndIndex);

		// Token: 0x0200005B RID: 91
		// (Invoke) Token: 0x0600066F RID: 1647
		internal delegate int RenderTargetComponent_CreateRenderTargetComponent_delegate(NativeObjectPointer renderTarget);

		// Token: 0x0200005C RID: 92
		// (Invoke) Token: 0x06000673 RID: 1651
		internal delegate void RenderTargetComponent_OnPaintNeeded_delegate(int thisPointer);

		// Token: 0x0200005D RID: 93
		// (Invoke) Token: 0x06000677 RID: 1655
		[return: MarshalAs(UnmanagedType.U1)]
		internal delegate bool SceneProblemChecker_OnCheckForSceneProblems_delegate(NativeObjectPointer scene);

		// Token: 0x0200005E RID: 94
		// (Invoke) Token: 0x0600067B RID: 1659
		internal delegate void ScriptComponentBehavior_AddScriptComponentToTick_delegate(int thisPointer);

		// Token: 0x0200005F RID: 95
		// (Invoke) Token: 0x0600067F RID: 1663
		internal delegate void ScriptComponentBehavior_DeregisterAsPrefabScriptComponent_delegate(int thisPointer);

		// Token: 0x02000060 RID: 96
		// (Invoke) Token: 0x06000683 RID: 1667
		internal delegate void ScriptComponentBehavior_DeregisterAsUndoStackScriptComponent_delegate(int thisPointer);

		// Token: 0x02000061 RID: 97
		// (Invoke) Token: 0x06000687 RID: 1671
		[return: MarshalAs(UnmanagedType.U1)]
		internal delegate bool ScriptComponentBehavior_DisablesOroCreation_delegate(int thisPointer);

		// Token: 0x02000062 RID: 98
		// (Invoke) Token: 0x0600068B RID: 1675
		internal delegate int ScriptComponentBehavior_GetEditableFields_delegate(IntPtr className);

		// Token: 0x02000063 RID: 99
		// (Invoke) Token: 0x0600068F RID: 1679
		internal delegate void ScriptComponentBehavior_HandleOnRemoved_delegate(int thisPointer, int removeReason);

		// Token: 0x02000064 RID: 100
		// (Invoke) Token: 0x06000693 RID: 1683
		[return: MarshalAs(UnmanagedType.U1)]
		internal delegate bool ScriptComponentBehavior_IsOnlyVisual_delegate(int thisPointer);

		// Token: 0x02000065 RID: 101
		// (Invoke) Token: 0x06000697 RID: 1687
		[return: MarshalAs(UnmanagedType.U1)]
		internal delegate bool ScriptComponentBehavior_MovesEntity_delegate(int thisPointer);

		// Token: 0x02000066 RID: 102
		// (Invoke) Token: 0x0600069B RID: 1691
		[return: MarshalAs(UnmanagedType.U1)]
		internal delegate bool ScriptComponentBehavior_OnCheckForProblems_delegate(int thisPointer);

		// Token: 0x02000067 RID: 103
		// (Invoke) Token: 0x0600069F RID: 1695
		internal delegate void ScriptComponentBehavior_OnEditModeVisibilityChanged_delegate(int thisPointer, [MarshalAs(UnmanagedType.U1)] bool currentVisibility);

		// Token: 0x02000068 RID: 104
		// (Invoke) Token: 0x060006A3 RID: 1699
		internal delegate void ScriptComponentBehavior_OnEditorInit_delegate(int thisPointer);

		// Token: 0x02000069 RID: 105
		// (Invoke) Token: 0x060006A7 RID: 1703
		internal delegate void ScriptComponentBehavior_OnEditorTick_delegate(int thisPointer, float dt);

		// Token: 0x0200006A RID: 106
		// (Invoke) Token: 0x060006AB RID: 1707
		internal delegate void ScriptComponentBehavior_OnEditorValidate_delegate(int thisPointer);

		// Token: 0x0200006B RID: 107
		// (Invoke) Token: 0x060006AF RID: 1711
		internal delegate void ScriptComponentBehavior_OnEditorVariableChanged_delegate(int thisPointer, IntPtr variableName);

		// Token: 0x0200006C RID: 108
		// (Invoke) Token: 0x060006B3 RID: 1715
		internal delegate void ScriptComponentBehavior_OnInit_delegate(int thisPointer);

		// Token: 0x0200006D RID: 109
		// (Invoke) Token: 0x060006B7 RID: 1719
		internal delegate void ScriptComponentBehavior_OnPhysicsCollision_delegate(int thisPointer, ref PhysicsContact contact);

		// Token: 0x0200006E RID: 110
		// (Invoke) Token: 0x060006BB RID: 1723
		internal delegate void ScriptComponentBehavior_OnPreInit_delegate(int thisPointer);

		// Token: 0x0200006F RID: 111
		// (Invoke) Token: 0x060006BF RID: 1727
		internal delegate void ScriptComponentBehavior_OnSceneSave_delegate(int thisPointer, IntPtr saveFolder);

		// Token: 0x02000070 RID: 112
		// (Invoke) Token: 0x060006C3 RID: 1731
		internal delegate void ScriptComponentBehavior_RegisterAsPrefabScriptComponent_delegate(int thisPointer);

		// Token: 0x02000071 RID: 113
		// (Invoke) Token: 0x060006C7 RID: 1735
		internal delegate void ScriptComponentBehavior_RegisterAsUndoStackScriptComponent_delegate(int thisPointer);

		// Token: 0x02000072 RID: 114
		// (Invoke) Token: 0x060006CB RID: 1739
		internal delegate void ScriptComponentBehavior_SetScene_delegate(int thisPointer, NativeObjectPointer scene);

		// Token: 0x02000073 RID: 115
		// (Invoke) Token: 0x060006CF RID: 1743
		internal delegate void ThumbnailCreatorView_OnThumbnailRenderComplete_delegate(IntPtr renderId, NativeObjectPointer renderTarget);
	}
}
