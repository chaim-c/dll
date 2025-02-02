using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace ManagedCallbacks
{
	// Token: 0x02000030 RID: 48
	internal class ScriptingInterfaceOfIUtil : IUtil
	{
		// Token: 0x0600052C RID: 1324 RVA: 0x00016900 File Offset: 0x00014B00
		public void AddCommandLineFunction(string concatName)
		{
			byte[] array = null;
			if (concatName != null)
			{
				int byteCount = ScriptingInterfaceOfIUtil._utf8.GetByteCount(concatName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(concatName, 0, concatName.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIUtil.call_AddCommandLineFunctionDelegate(array);
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0001695C File Offset: 0x00014B5C
		public void AddMainThreadPerformanceQuery(string parent, string name, float seconds)
		{
			byte[] array = null;
			if (parent != null)
			{
				int byteCount = ScriptingInterfaceOfIUtil._utf8.GetByteCount(parent);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(parent, 0, parent.Length, array, 0);
				array[byteCount] = 0;
			}
			byte[] array2 = null;
			if (name != null)
			{
				int byteCount2 = ScriptingInterfaceOfIUtil._utf8.GetByteCount(name);
				array2 = ((byteCount2 < 1024) ? CallbackStringBufferManager.StringBuffer1 : new byte[byteCount2 + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(name, 0, name.Length, array2, 0);
				array2[byteCount2] = 0;
			}
			ScriptingInterfaceOfIUtil.call_AddMainThreadPerformanceQueryDelegate(array, array2, seconds);
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x000169FC File Offset: 0x00014BFC
		public void AddPerformanceReportToken(string performance_type, string name, float loading_time)
		{
			byte[] array = null;
			if (performance_type != null)
			{
				int byteCount = ScriptingInterfaceOfIUtil._utf8.GetByteCount(performance_type);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(performance_type, 0, performance_type.Length, array, 0);
				array[byteCount] = 0;
			}
			byte[] array2 = null;
			if (name != null)
			{
				int byteCount2 = ScriptingInterfaceOfIUtil._utf8.GetByteCount(name);
				array2 = ((byteCount2 < 1024) ? CallbackStringBufferManager.StringBuffer1 : new byte[byteCount2 + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(name, 0, name.Length, array2, 0);
				array2[byteCount2] = 0;
			}
			ScriptingInterfaceOfIUtil.call_AddPerformanceReportTokenDelegate(array, array2, loading_time);
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00016A9C File Offset: 0x00014C9C
		public void AddSceneObjectReport(string scene_name, string report_name, float report_value)
		{
			byte[] array = null;
			if (scene_name != null)
			{
				int byteCount = ScriptingInterfaceOfIUtil._utf8.GetByteCount(scene_name);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(scene_name, 0, scene_name.Length, array, 0);
				array[byteCount] = 0;
			}
			byte[] array2 = null;
			if (report_name != null)
			{
				int byteCount2 = ScriptingInterfaceOfIUtil._utf8.GetByteCount(report_name);
				array2 = ((byteCount2 < 1024) ? CallbackStringBufferManager.StringBuffer1 : new byte[byteCount2 + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(report_name, 0, report_name.Length, array2, 0);
				array2[byteCount2] = 0;
			}
			ScriptingInterfaceOfIUtil.call_AddSceneObjectReportDelegate(array, array2, report_value);
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00016B3A File Offset: 0x00014D3A
		public void CheckIfAssetsAndSourcesAreSame()
		{
			ScriptingInterfaceOfIUtil.call_CheckIfAssetsAndSourcesAreSameDelegate();
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00016B46 File Offset: 0x00014D46
		public bool CheckIfTerrainShaderHeaderGenerationFinished()
		{
			return ScriptingInterfaceOfIUtil.call_CheckIfTerrainShaderHeaderGenerationFinishedDelegate();
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x00016B52 File Offset: 0x00014D52
		public void CheckResourceModifications()
		{
			ScriptingInterfaceOfIUtil.call_CheckResourceModificationsDelegate();
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00016B60 File Offset: 0x00014D60
		public void CheckSceneForProblems(string path)
		{
			byte[] array = null;
			if (path != null)
			{
				int byteCount = ScriptingInterfaceOfIUtil._utf8.GetByteCount(path);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(path, 0, path.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIUtil.call_CheckSceneForProblemsDelegate(array);
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x00016BBA File Offset: 0x00014DBA
		public bool CheckShaderCompilation()
		{
			return ScriptingInterfaceOfIUtil.call_CheckShaderCompilationDelegate();
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00016BC6 File Offset: 0x00014DC6
		public void clear_decal_atlas(DecalAtlasGroup atlasGroup)
		{
			ScriptingInterfaceOfIUtil.call_clear_decal_atlasDelegate(atlasGroup);
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00016BD3 File Offset: 0x00014DD3
		public void ClearOldResourcesAndObjects()
		{
			ScriptingInterfaceOfIUtil.call_ClearOldResourcesAndObjectsDelegate();
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00016BDF File Offset: 0x00014DDF
		public void ClearShaderMemory()
		{
			ScriptingInterfaceOfIUtil.call_ClearShaderMemoryDelegate();
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00016BEC File Offset: 0x00014DEC
		public bool CommandLineArgumentExists(string str)
		{
			byte[] array = null;
			if (str != null)
			{
				int byteCount = ScriptingInterfaceOfIUtil._utf8.GetByteCount(str);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(str, 0, str.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfIUtil.call_CommandLineArgumentExistsDelegate(array);
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00016C48 File Offset: 0x00014E48
		public void CompileAllShaders(string targetPlatform)
		{
			byte[] array = null;
			if (targetPlatform != null)
			{
				int byteCount = ScriptingInterfaceOfIUtil._utf8.GetByteCount(targetPlatform);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(targetPlatform, 0, targetPlatform.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIUtil.call_CompileAllShadersDelegate(array);
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00016CA4 File Offset: 0x00014EA4
		public void CompileTerrainShadersDist(string targetPlatform, string targetConfig, string output_path)
		{
			byte[] array = null;
			if (targetPlatform != null)
			{
				int byteCount = ScriptingInterfaceOfIUtil._utf8.GetByteCount(targetPlatform);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(targetPlatform, 0, targetPlatform.Length, array, 0);
				array[byteCount] = 0;
			}
			byte[] array2 = null;
			if (targetConfig != null)
			{
				int byteCount2 = ScriptingInterfaceOfIUtil._utf8.GetByteCount(targetConfig);
				array2 = ((byteCount2 < 1024) ? CallbackStringBufferManager.StringBuffer1 : new byte[byteCount2 + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(targetConfig, 0, targetConfig.Length, array2, 0);
				array2[byteCount2] = 0;
			}
			byte[] array3 = null;
			if (output_path != null)
			{
				int byteCount3 = ScriptingInterfaceOfIUtil._utf8.GetByteCount(output_path);
				array3 = ((byteCount3 < 1024) ? CallbackStringBufferManager.StringBuffer2 : new byte[byteCount3 + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(output_path, 0, output_path.Length, array3, 0);
				array3[byteCount3] = 0;
			}
			ScriptingInterfaceOfIUtil.call_CompileTerrainShadersDistDelegate(array, array2, array3);
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00016D8C File Offset: 0x00014F8C
		public void CreateSelectionInEditor(UIntPtr[] gameEntities, int entityCount, string name)
		{
			PinnedArrayData<UIntPtr> pinnedArrayData = new PinnedArrayData<UIntPtr>(gameEntities, false);
			IntPtr pointer = pinnedArrayData.Pointer;
			byte[] array = null;
			if (name != null)
			{
				int byteCount = ScriptingInterfaceOfIUtil._utf8.GetByteCount(name);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(name, 0, name.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIUtil.call_CreateSelectionInEditorDelegate(pointer, entityCount, array);
			pinnedArrayData.Dispose();
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00016E00 File Offset: 0x00015000
		public void DebugSetGlobalLoadingWindowState(bool s)
		{
			ScriptingInterfaceOfIUtil.call_DebugSetGlobalLoadingWindowStateDelegate(s);
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00016E10 File Offset: 0x00015010
		public void DeleteEntitiesInEditorScene(UIntPtr[] gameEntities, int entityCount)
		{
			PinnedArrayData<UIntPtr> pinnedArrayData = new PinnedArrayData<UIntPtr>(gameEntities, false);
			IntPtr pointer = pinnedArrayData.Pointer;
			ScriptingInterfaceOfIUtil.call_DeleteEntitiesInEditorSceneDelegate(pointer, entityCount);
			pinnedArrayData.Dispose();
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00016E41 File Offset: 0x00015041
		public void DetachWatchdog()
		{
			ScriptingInterfaceOfIUtil.call_DetachWatchdogDelegate();
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00016E4D File Offset: 0x0001504D
		public bool DidAutomatedGIBakeFinished()
		{
			return ScriptingInterfaceOfIUtil.call_DidAutomatedGIBakeFinishedDelegate();
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00016E59 File Offset: 0x00015059
		public void DisableCoreGame()
		{
			ScriptingInterfaceOfIUtil.call_DisableCoreGameDelegate();
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00016E65 File Offset: 0x00015065
		public void DisableGlobalEditDataCacher()
		{
			ScriptingInterfaceOfIUtil.call_DisableGlobalEditDataCacherDelegate();
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00016E71 File Offset: 0x00015071
		public void DisableGlobalLoadingWindow()
		{
			ScriptingInterfaceOfIUtil.call_DisableGlobalLoadingWindowDelegate();
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00016E7D File Offset: 0x0001507D
		public void DoDelayedexit(int returnCode)
		{
			ScriptingInterfaceOfIUtil.call_DoDelayedexitDelegate(returnCode);
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00016E8C File Offset: 0x0001508C
		public void DoFullBakeAllLevelsAutomated(string module, string sceneName)
		{
			byte[] array = null;
			if (module != null)
			{
				int byteCount = ScriptingInterfaceOfIUtil._utf8.GetByteCount(module);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(module, 0, module.Length, array, 0);
				array[byteCount] = 0;
			}
			byte[] array2 = null;
			if (sceneName != null)
			{
				int byteCount2 = ScriptingInterfaceOfIUtil._utf8.GetByteCount(sceneName);
				array2 = ((byteCount2 < 1024) ? CallbackStringBufferManager.StringBuffer1 : new byte[byteCount2 + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(sceneName, 0, sceneName.Length, array2, 0);
				array2[byteCount2] = 0;
			}
			ScriptingInterfaceOfIUtil.call_DoFullBakeAllLevelsAutomatedDelegate(array, array2);
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00016F2C File Offset: 0x0001512C
		public void DoFullBakeSingleLevelAutomated(string module, string sceneName)
		{
			byte[] array = null;
			if (module != null)
			{
				int byteCount = ScriptingInterfaceOfIUtil._utf8.GetByteCount(module);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(module, 0, module.Length, array, 0);
				array[byteCount] = 0;
			}
			byte[] array2 = null;
			if (sceneName != null)
			{
				int byteCount2 = ScriptingInterfaceOfIUtil._utf8.GetByteCount(sceneName);
				array2 = ((byteCount2 < 1024) ? CallbackStringBufferManager.StringBuffer1 : new byte[byteCount2 + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(sceneName, 0, sceneName.Length, array2, 0);
				array2[byteCount2] = 0;
			}
			ScriptingInterfaceOfIUtil.call_DoFullBakeSingleLevelAutomatedDelegate(array, array2);
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00016FCC File Offset: 0x000151CC
		public void DoLightOnlyBakeAllLevelsAutomated(string module, string sceneName)
		{
			byte[] array = null;
			if (module != null)
			{
				int byteCount = ScriptingInterfaceOfIUtil._utf8.GetByteCount(module);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(module, 0, module.Length, array, 0);
				array[byteCount] = 0;
			}
			byte[] array2 = null;
			if (sceneName != null)
			{
				int byteCount2 = ScriptingInterfaceOfIUtil._utf8.GetByteCount(sceneName);
				array2 = ((byteCount2 < 1024) ? CallbackStringBufferManager.StringBuffer1 : new byte[byteCount2 + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(sceneName, 0, sceneName.Length, array2, 0);
				array2[byteCount2] = 0;
			}
			ScriptingInterfaceOfIUtil.call_DoLightOnlyBakeAllLevelsAutomatedDelegate(array, array2);
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0001706C File Offset: 0x0001526C
		public void DoLightOnlyBakeSingleLevelAutomated(string module, string sceneName)
		{
			byte[] array = null;
			if (module != null)
			{
				int byteCount = ScriptingInterfaceOfIUtil._utf8.GetByteCount(module);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(module, 0, module.Length, array, 0);
				array[byteCount] = 0;
			}
			byte[] array2 = null;
			if (sceneName != null)
			{
				int byteCount2 = ScriptingInterfaceOfIUtil._utf8.GetByteCount(sceneName);
				array2 = ((byteCount2 < 1024) ? CallbackStringBufferManager.StringBuffer1 : new byte[byteCount2 + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(sceneName, 0, sceneName.Length, array2, 0);
				array2[byteCount2] = 0;
			}
			ScriptingInterfaceOfIUtil.call_DoLightOnlyBakeSingleLevelAutomatedDelegate(array, array2);
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0001710C File Offset: 0x0001530C
		public void DumpGPUMemoryStatistics(string filePath)
		{
			byte[] array = null;
			if (filePath != null)
			{
				int byteCount = ScriptingInterfaceOfIUtil._utf8.GetByteCount(filePath);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(filePath, 0, filePath.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIUtil.call_DumpGPUMemoryStatisticsDelegate(array);
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00017166 File Offset: 0x00015366
		public void EnableGlobalEditDataCacher()
		{
			ScriptingInterfaceOfIUtil.call_EnableGlobalEditDataCacherDelegate();
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00017172 File Offset: 0x00015372
		public void EnableGlobalLoadingWindow()
		{
			ScriptingInterfaceOfIUtil.call_EnableGlobalLoadingWindowDelegate();
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x0001717E File Offset: 0x0001537E
		public void EnableSingleGPUQueryPerFrame()
		{
			ScriptingInterfaceOfIUtil.call_EnableSingleGPUQueryPerFrameDelegate();
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0001718C File Offset: 0x0001538C
		public string ExecuteCommandLineCommand(string command)
		{
			byte[] array = null;
			if (command != null)
			{
				int byteCount = ScriptingInterfaceOfIUtil._utf8.GetByteCount(command);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(command, 0, command.Length, array, 0);
				array[byteCount] = 0;
			}
			if (ScriptingInterfaceOfIUtil.call_ExecuteCommandLineCommandDelegate(array) != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x000171F0 File Offset: 0x000153F0
		public void ExitProcess(int exitCode)
		{
			ScriptingInterfaceOfIUtil.call_ExitProcessDelegate(exitCode);
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00017200 File Offset: 0x00015400
		public string ExportNavMeshFaceMarks(string file_name)
		{
			byte[] array = null;
			if (file_name != null)
			{
				int byteCount = ScriptingInterfaceOfIUtil._utf8.GetByteCount(file_name);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(file_name, 0, file_name.Length, array, 0);
				array[byteCount] = 0;
			}
			if (ScriptingInterfaceOfIUtil.call_ExportNavMeshFaceMarksDelegate(array) != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00017264 File Offset: 0x00015464
		public void FindMeshesWithoutLods(string module_name)
		{
			byte[] array = null;
			if (module_name != null)
			{
				int byteCount = ScriptingInterfaceOfIUtil._utf8.GetByteCount(module_name);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(module_name, 0, module_name.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIUtil.call_FindMeshesWithoutLodsDelegate(array);
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x000172BE File Offset: 0x000154BE
		public void FlushManagedObjectsMemory()
		{
			ScriptingInterfaceOfIUtil.call_FlushManagedObjectsMemoryDelegate();
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x000172CC File Offset: 0x000154CC
		public void GatherCoreGameReferences(string scene_names)
		{
			byte[] array = null;
			if (scene_names != null)
			{
				int byteCount = ScriptingInterfaceOfIUtil._utf8.GetByteCount(scene_names);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(scene_names, 0, scene_names.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIUtil.call_GatherCoreGameReferencesDelegate(array);
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00017328 File Offset: 0x00015528
		public void GenerateTerrainShaderHeaders(string targetPlatform, string targetConfig, string output_path)
		{
			byte[] array = null;
			if (targetPlatform != null)
			{
				int byteCount = ScriptingInterfaceOfIUtil._utf8.GetByteCount(targetPlatform);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(targetPlatform, 0, targetPlatform.Length, array, 0);
				array[byteCount] = 0;
			}
			byte[] array2 = null;
			if (targetConfig != null)
			{
				int byteCount2 = ScriptingInterfaceOfIUtil._utf8.GetByteCount(targetConfig);
				array2 = ((byteCount2 < 1024) ? CallbackStringBufferManager.StringBuffer1 : new byte[byteCount2 + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(targetConfig, 0, targetConfig.Length, array2, 0);
				array2[byteCount2] = 0;
			}
			byte[] array3 = null;
			if (output_path != null)
			{
				int byteCount3 = ScriptingInterfaceOfIUtil._utf8.GetByteCount(output_path);
				array3 = ((byteCount3 < 1024) ? CallbackStringBufferManager.StringBuffer2 : new byte[byteCount3 + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(output_path, 0, output_path.Length, array3, 0);
				array3[byteCount3] = 0;
			}
			ScriptingInterfaceOfIUtil.call_GenerateTerrainShaderHeadersDelegate(array, array2, array3);
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00017410 File Offset: 0x00015610
		public float GetApplicationMemory()
		{
			return ScriptingInterfaceOfIUtil.call_GetApplicationMemoryDelegate();
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0001741C File Offset: 0x0001561C
		public string GetApplicationMemoryStatistics()
		{
			if (ScriptingInterfaceOfIUtil.call_GetApplicationMemoryStatisticsDelegate() != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00017432 File Offset: 0x00015632
		public string GetApplicationName()
		{
			if (ScriptingInterfaceOfIUtil.call_GetApplicationNameDelegate() != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00017448 File Offset: 0x00015648
		public string GetAttachmentsPath()
		{
			if (ScriptingInterfaceOfIUtil.call_GetAttachmentsPathDelegate() != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0001745E File Offset: 0x0001565E
		public string GetBaseDirectory()
		{
			if (ScriptingInterfaceOfIUtil.call_GetBaseDirectoryDelegate() != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00017474 File Offset: 0x00015674
		public int GetBenchmarkStatus()
		{
			return ScriptingInterfaceOfIUtil.call_GetBenchmarkStatusDelegate();
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00017480 File Offset: 0x00015680
		public int GetBuildNumber()
		{
			return ScriptingInterfaceOfIUtil.call_GetBuildNumberDelegate();
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0001748C File Offset: 0x0001568C
		public string GetConsoleHostMachine()
		{
			if (ScriptingInterfaceOfIUtil.call_GetConsoleHostMachineDelegate() != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x000174A2 File Offset: 0x000156A2
		public int GetCoreGameState()
		{
			return ScriptingInterfaceOfIUtil.call_GetCoreGameStateDelegate();
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x000174AE File Offset: 0x000156AE
		public ulong GetCurrentCpuMemoryUsage()
		{
			return ScriptingInterfaceOfIUtil.call_GetCurrentCpuMemoryUsageDelegate();
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x000174BA File Offset: 0x000156BA
		public int GetCurrentEstimatedGPUMemoryCostMB()
		{
			return ScriptingInterfaceOfIUtil.call_GetCurrentEstimatedGPUMemoryCostMBDelegate();
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x000174C6 File Offset: 0x000156C6
		public uint GetCurrentProcessID()
		{
			return ScriptingInterfaceOfIUtil.call_GetCurrentProcessIDDelegate();
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x000174D2 File Offset: 0x000156D2
		public ulong GetCurrentThreadId()
		{
			return ScriptingInterfaceOfIUtil.call_GetCurrentThreadIdDelegate();
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x000174DE File Offset: 0x000156DE
		public float GetDeltaTime(int timerId)
		{
			return ScriptingInterfaceOfIUtil.call_GetDeltaTimeDelegate(timerId);
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x000174EB File Offset: 0x000156EB
		public void GetDetailedGPUBufferMemoryStats(ref int totalMemoryAllocated, ref int totalMemoryUsed, ref int emptyChunkCount)
		{
			ScriptingInterfaceOfIUtil.call_GetDetailedGPUBufferMemoryStatsDelegate(ref totalMemoryAllocated, ref totalMemoryUsed, ref emptyChunkCount);
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x000174FA File Offset: 0x000156FA
		public string GetDetailedXBOXMemoryInfo()
		{
			if (ScriptingInterfaceOfIUtil.call_GetDetailedXBOXMemoryInfoDelegate() != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00017510 File Offset: 0x00015710
		public void GetEditorSelectedEntities(UIntPtr[] gameEntitiesTemp)
		{
			PinnedArrayData<UIntPtr> pinnedArrayData = new PinnedArrayData<UIntPtr>(gameEntitiesTemp, false);
			IntPtr pointer = pinnedArrayData.Pointer;
			ScriptingInterfaceOfIUtil.call_GetEditorSelectedEntitiesDelegate(pointer);
			pinnedArrayData.Dispose();
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00017540 File Offset: 0x00015740
		public int GetEditorSelectedEntityCount()
		{
			return ScriptingInterfaceOfIUtil.call_GetEditorSelectedEntityCountDelegate();
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0001754C File Offset: 0x0001574C
		public int GetEngineFrameNo()
		{
			return ScriptingInterfaceOfIUtil.call_GetEngineFrameNoDelegate();
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00017558 File Offset: 0x00015758
		public void GetEntitiesOfSelectionSet(string name, UIntPtr[] gameEntitiesTemp)
		{
			byte[] array = null;
			if (name != null)
			{
				int byteCount = ScriptingInterfaceOfIUtil._utf8.GetByteCount(name);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(name, 0, name.Length, array, 0);
				array[byteCount] = 0;
			}
			PinnedArrayData<UIntPtr> pinnedArrayData = new PinnedArrayData<UIntPtr>(gameEntitiesTemp, false);
			IntPtr pointer = pinnedArrayData.Pointer;
			ScriptingInterfaceOfIUtil.call_GetEntitiesOfSelectionSetDelegate(array, pointer);
			pinnedArrayData.Dispose();
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x000175CC File Offset: 0x000157CC
		public int GetEntityCountOfSelectionSet(string name)
		{
			byte[] array = null;
			if (name != null)
			{
				int byteCount = ScriptingInterfaceOfIUtil._utf8.GetByteCount(name);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(name, 0, name.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfIUtil.call_GetEntityCountOfSelectionSetDelegate(array);
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00017626 File Offset: 0x00015826
		public string GetExecutableWorkingDirectory()
		{
			if (ScriptingInterfaceOfIUtil.call_GetExecutableWorkingDirectoryDelegate() != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0001763C File Offset: 0x0001583C
		public float GetFps()
		{
			return ScriptingInterfaceOfIUtil.call_GetFpsDelegate();
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00017648 File Offset: 0x00015848
		public bool GetFrameLimiterWithSleep()
		{
			return ScriptingInterfaceOfIUtil.call_GetFrameLimiterWithSleepDelegate();
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00017654 File Offset: 0x00015854
		public string GetFullCommandLineString()
		{
			if (ScriptingInterfaceOfIUtil.call_GetFullCommandLineStringDelegate() != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0001766C File Offset: 0x0001586C
		public string GetFullFilePathOfScene(string sceneName)
		{
			byte[] array = null;
			if (sceneName != null)
			{
				int byteCount = ScriptingInterfaceOfIUtil._utf8.GetByteCount(sceneName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(sceneName, 0, sceneName.Length, array, 0);
				array[byteCount] = 0;
			}
			if (ScriptingInterfaceOfIUtil.call_GetFullFilePathOfSceneDelegate(array) != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x000176D0 File Offset: 0x000158D0
		public string GetFullModulePath(string moduleName)
		{
			byte[] array = null;
			if (moduleName != null)
			{
				int byteCount = ScriptingInterfaceOfIUtil._utf8.GetByteCount(moduleName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(moduleName, 0, moduleName.Length, array, 0);
				array[byteCount] = 0;
			}
			if (ScriptingInterfaceOfIUtil.call_GetFullModulePathDelegate(array) != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00017734 File Offset: 0x00015934
		public string GetFullModulePaths()
		{
			if (ScriptingInterfaceOfIUtil.call_GetFullModulePathsDelegate() != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0001774A File Offset: 0x0001594A
		public int GetGPUMemoryMB()
		{
			return ScriptingInterfaceOfIUtil.call_GetGPUMemoryMBDelegate();
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00017758 File Offset: 0x00015958
		public ulong GetGpuMemoryOfAllocationGroup(string allocationName)
		{
			byte[] array = null;
			if (allocationName != null)
			{
				int byteCount = ScriptingInterfaceOfIUtil._utf8.GetByteCount(allocationName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(allocationName, 0, allocationName.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfIUtil.call_GetGpuMemoryOfAllocationGroupDelegate(array);
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x000177B2 File Offset: 0x000159B2
		public void GetGPUMemoryStats(ref float totalMemory, ref float renderTargetMemory, ref float depthTargetMemory, ref float srvMemory, ref float bufferMemory)
		{
			ScriptingInterfaceOfIUtil.call_GetGPUMemoryStatsDelegate(ref totalMemory, ref renderTargetMemory, ref depthTargetMemory, ref srvMemory, ref bufferMemory);
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x000177C5 File Offset: 0x000159C5
		public string GetLocalOutputPath()
		{
			if (ScriptingInterfaceOfIUtil.call_GetLocalOutputPathDelegate() != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x000177DB File Offset: 0x000159DB
		public float GetMainFps()
		{
			return ScriptingInterfaceOfIUtil.call_GetMainFpsDelegate();
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x000177E7 File Offset: 0x000159E7
		public ulong GetMainThreadId()
		{
			return ScriptingInterfaceOfIUtil.call_GetMainThreadIdDelegate();
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x000177F3 File Offset: 0x000159F3
		public int GetMemoryUsageOfCategory(int index)
		{
			return ScriptingInterfaceOfIUtil.call_GetMemoryUsageOfCategoryDelegate(index);
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00017800 File Offset: 0x00015A00
		public string GetModulesCode()
		{
			if (ScriptingInterfaceOfIUtil.call_GetModulesCodeDelegate() != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00017816 File Offset: 0x00015A16
		public string GetNativeMemoryStatistics()
		{
			if (ScriptingInterfaceOfIUtil.call_GetNativeMemoryStatisticsDelegate() != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0001782C File Offset: 0x00015A2C
		public int GetNumberOfShaderCompilationsInProgress()
		{
			return ScriptingInterfaceOfIUtil.call_GetNumberOfShaderCompilationsInProgressDelegate();
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00017838 File Offset: 0x00015A38
		public string GetPCInfo()
		{
			if (ScriptingInterfaceOfIUtil.call_GetPCInfoDelegate() != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0001784E File Offset: 0x00015A4E
		public float GetRendererFps()
		{
			return ScriptingInterfaceOfIUtil.call_GetRendererFpsDelegate();
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0001785A File Offset: 0x00015A5A
		public int GetReturnCode()
		{
			return ScriptingInterfaceOfIUtil.call_GetReturnCodeDelegate();
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00017868 File Offset: 0x00015A68
		public string GetSingleModuleScenesOfModule(string moduleName)
		{
			byte[] array = null;
			if (moduleName != null)
			{
				int byteCount = ScriptingInterfaceOfIUtil._utf8.GetByteCount(moduleName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(moduleName, 0, moduleName.Length, array, 0);
				array[byteCount] = 0;
			}
			if (ScriptingInterfaceOfIUtil.call_GetSingleModuleScenesOfModuleDelegate(array) != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x000178CC File Offset: 0x00015ACC
		public int GetSteamAppId()
		{
			return ScriptingInterfaceOfIUtil.call_GetSteamAppIdDelegate();
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x000178D8 File Offset: 0x00015AD8
		public string GetSystemLanguage()
		{
			if (ScriptingInterfaceOfIUtil.call_GetSystemLanguageDelegate() != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x000178EE File Offset: 0x00015AEE
		public int GetVertexBufferChunkSystemMemoryUsage()
		{
			return ScriptingInterfaceOfIUtil.call_GetVertexBufferChunkSystemMemoryUsageDelegate();
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x000178FA File Offset: 0x00015AFA
		public string GetVisualTestsTestFilesPath()
		{
			if (ScriptingInterfaceOfIUtil.call_GetVisualTestsTestFilesPathDelegate() != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00017910 File Offset: 0x00015B10
		public string GetVisualTestsValidatePath()
		{
			if (ScriptingInterfaceOfIUtil.call_GetVisualTestsValidatePathDelegate() != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00017926 File Offset: 0x00015B26
		public bool IsBenchmarkQuited()
		{
			return ScriptingInterfaceOfIUtil.call_IsBenchmarkQuitedDelegate();
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00017932 File Offset: 0x00015B32
		public int IsDetailedSoundLogOn()
		{
			return ScriptingInterfaceOfIUtil.call_IsDetailedSoundLogOnDelegate();
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0001793E File Offset: 0x00015B3E
		public bool IsEditModeEnabled()
		{
			return ScriptingInterfaceOfIUtil.call_IsEditModeEnabledDelegate();
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0001794A File Offset: 0x00015B4A
		public bool IsSceneReportFinished()
		{
			return ScriptingInterfaceOfIUtil.call_IsSceneReportFinishedDelegate();
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00017956 File Offset: 0x00015B56
		public void LoadSkyBoxes()
		{
			ScriptingInterfaceOfIUtil.call_LoadSkyBoxesDelegate();
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00017964 File Offset: 0x00015B64
		public void LoadVirtualTextureTileset(string name)
		{
			byte[] array = null;
			if (name != null)
			{
				int byteCount = ScriptingInterfaceOfIUtil._utf8.GetByteCount(name);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(name, 0, name.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIUtil.call_LoadVirtualTextureTilesetDelegate(array);
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x000179BE File Offset: 0x00015BBE
		public void ManagedParallelFor(int fromInclusive, int toExclusive, long curKey, int grainSize)
		{
			ScriptingInterfaceOfIUtil.call_ManagedParallelForDelegate(fromInclusive, toExclusive, curKey, grainSize);
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x000179CF File Offset: 0x00015BCF
		public void ManagedParallelForWithDt(int fromInclusive, int toExclusive, long curKey, int grainSize)
		{
			ScriptingInterfaceOfIUtil.call_ManagedParallelForWithDtDelegate(fromInclusive, toExclusive, curKey, grainSize);
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x000179E0 File Offset: 0x00015BE0
		public void OnLoadingWindowDisabled()
		{
			ScriptingInterfaceOfIUtil.call_OnLoadingWindowDisabledDelegate();
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x000179EC File Offset: 0x00015BEC
		public void OnLoadingWindowEnabled()
		{
			ScriptingInterfaceOfIUtil.call_OnLoadingWindowEnabledDelegate();
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x000179F8 File Offset: 0x00015BF8
		public void OpenOnscreenKeyboard(string initialText, string descriptionText, int maxLength, int keyboardTypeEnum)
		{
			byte[] array = null;
			if (initialText != null)
			{
				int byteCount = ScriptingInterfaceOfIUtil._utf8.GetByteCount(initialText);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(initialText, 0, initialText.Length, array, 0);
				array[byteCount] = 0;
			}
			byte[] array2 = null;
			if (descriptionText != null)
			{
				int byteCount2 = ScriptingInterfaceOfIUtil._utf8.GetByteCount(descriptionText);
				array2 = ((byteCount2 < 1024) ? CallbackStringBufferManager.StringBuffer1 : new byte[byteCount2 + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(descriptionText, 0, descriptionText.Length, array2, 0);
				array2[byteCount2] = 0;
			}
			ScriptingInterfaceOfIUtil.call_OpenOnscreenKeyboardDelegate(array, array2, maxLength, keyboardTypeEnum);
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00017A98 File Offset: 0x00015C98
		public void OutputBenchmarkValuesToPerformanceReporter()
		{
			ScriptingInterfaceOfIUtil.call_OutputBenchmarkValuesToPerformanceReporterDelegate();
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00017AA4 File Offset: 0x00015CA4
		public void OutputPerformanceReports()
		{
			ScriptingInterfaceOfIUtil.call_OutputPerformanceReportsDelegate();
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x00017AB0 File Offset: 0x00015CB0
		public void PairSceneNameToModuleName(string sceneName, string moduleName)
		{
			byte[] array = null;
			if (sceneName != null)
			{
				int byteCount = ScriptingInterfaceOfIUtil._utf8.GetByteCount(sceneName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(sceneName, 0, sceneName.Length, array, 0);
				array[byteCount] = 0;
			}
			byte[] array2 = null;
			if (moduleName != null)
			{
				int byteCount2 = ScriptingInterfaceOfIUtil._utf8.GetByteCount(moduleName);
				array2 = ((byteCount2 < 1024) ? CallbackStringBufferManager.StringBuffer1 : new byte[byteCount2 + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(moduleName, 0, moduleName.Length, array2, 0);
				array2[byteCount2] = 0;
			}
			ScriptingInterfaceOfIUtil.call_PairSceneNameToModuleNameDelegate(array, array2);
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00017B50 File Offset: 0x00015D50
		public string ProcessWindowTitle(string title)
		{
			byte[] array = null;
			if (title != null)
			{
				int byteCount = ScriptingInterfaceOfIUtil._utf8.GetByteCount(title);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(title, 0, title.Length, array, 0);
				array[byteCount] = 0;
			}
			if (ScriptingInterfaceOfIUtil.call_ProcessWindowTitleDelegate(array) != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x00017BB4 File Offset: 0x00015DB4
		public void QuitGame()
		{
			ScriptingInterfaceOfIUtil.call_QuitGameDelegate();
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x00017BC0 File Offset: 0x00015DC0
		public int RegisterGPUAllocationGroup(string name)
		{
			byte[] array = null;
			if (name != null)
			{
				int byteCount = ScriptingInterfaceOfIUtil._utf8.GetByteCount(name);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(name, 0, name.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfIUtil.call_RegisterGPUAllocationGroupDelegate(array);
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x00017C1C File Offset: 0x00015E1C
		public void RegisterMeshForGPUMorph(string metaMeshName)
		{
			byte[] array = null;
			if (metaMeshName != null)
			{
				int byteCount = ScriptingInterfaceOfIUtil._utf8.GetByteCount(metaMeshName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(metaMeshName, 0, metaMeshName.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIUtil.call_RegisterMeshForGPUMorphDelegate(array);
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x00017C78 File Offset: 0x00015E78
		public int SaveDataAsTexture(string path, int width, int height, float[] data)
		{
			byte[] array = null;
			if (path != null)
			{
				int byteCount = ScriptingInterfaceOfIUtil._utf8.GetByteCount(path);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(path, 0, path.Length, array, 0);
				array[byteCount] = 0;
			}
			PinnedArrayData<float> pinnedArrayData = new PinnedArrayData<float>(data, false);
			IntPtr pointer = pinnedArrayData.Pointer;
			int result = ScriptingInterfaceOfIUtil.call_SaveDataAsTextureDelegate(array, width, height, pointer);
			pinnedArrayData.Dispose();
			return result;
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00017CF0 File Offset: 0x00015EF0
		public void SelectEntities(UIntPtr[] gameEntities, int entityCount)
		{
			PinnedArrayData<UIntPtr> pinnedArrayData = new PinnedArrayData<UIntPtr>(gameEntities, false);
			IntPtr pointer = pinnedArrayData.Pointer;
			ScriptingInterfaceOfIUtil.call_SelectEntitiesDelegate(pointer, entityCount);
			pinnedArrayData.Dispose();
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00017D21 File Offset: 0x00015F21
		public void SetAllocationAlwaysValidScene(UIntPtr scene)
		{
			ScriptingInterfaceOfIUtil.call_SetAllocationAlwaysValidSceneDelegate(scene);
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00017D2E File Offset: 0x00015F2E
		public void SetAssertionAtShaderCompile(bool value)
		{
			ScriptingInterfaceOfIUtil.call_SetAssertionAtShaderCompileDelegate(value);
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00017D3B File Offset: 0x00015F3B
		public void SetAssertionsAndWarningsSetExitCode(bool value)
		{
			ScriptingInterfaceOfIUtil.call_SetAssertionsAndWarningsSetExitCodeDelegate(value);
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00017D48 File Offset: 0x00015F48
		public void SetBenchmarkStatus(int status, string def)
		{
			byte[] array = null;
			if (def != null)
			{
				int byteCount = ScriptingInterfaceOfIUtil._utf8.GetByteCount(def);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(def, 0, def.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIUtil.call_SetBenchmarkStatusDelegate(status, array);
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00017DA3 File Offset: 0x00015FA3
		public void SetCoreGameState(int state)
		{
			ScriptingInterfaceOfIUtil.call_SetCoreGameStateDelegate(state);
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00017DB0 File Offset: 0x00015FB0
		public void SetCrashOnAsserts(bool val)
		{
			ScriptingInterfaceOfIUtil.call_SetCrashOnAssertsDelegate(val);
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00017DBD File Offset: 0x00015FBD
		public void SetCrashOnWarnings(bool val)
		{
			ScriptingInterfaceOfIUtil.call_SetCrashOnWarningsDelegate(val);
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x00017DCC File Offset: 0x00015FCC
		public void SetCrashReportCustomStack(string customStack)
		{
			byte[] array = null;
			if (customStack != null)
			{
				int byteCount = ScriptingInterfaceOfIUtil._utf8.GetByteCount(customStack);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(customStack, 0, customStack.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIUtil.call_SetCrashReportCustomStackDelegate(array);
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x00017E28 File Offset: 0x00016028
		public void SetCrashReportCustomString(string customString)
		{
			byte[] array = null;
			if (customString != null)
			{
				int byteCount = ScriptingInterfaceOfIUtil._utf8.GetByteCount(customString);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(customString, 0, customString.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIUtil.call_SetCrashReportCustomStringDelegate(array);
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x00017E82 File Offset: 0x00016082
		public void SetDisableDumpGeneration(bool value)
		{
			ScriptingInterfaceOfIUtil.call_SetDisableDumpGenerationDelegate(value);
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00017E90 File Offset: 0x00016090
		public void SetDumpFolderPath(string path)
		{
			byte[] array = null;
			if (path != null)
			{
				int byteCount = ScriptingInterfaceOfIUtil._utf8.GetByteCount(path);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(path, 0, path.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIUtil.call_SetDumpFolderPathDelegate(array);
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00017EEA File Offset: 0x000160EA
		public void SetFixedDt(bool enabled, float dt)
		{
			ScriptingInterfaceOfIUtil.call_SetFixedDtDelegate(enabled, dt);
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00017EF8 File Offset: 0x000160F8
		public void SetForceDrawEntityID(bool value)
		{
			ScriptingInterfaceOfIUtil.call_SetForceDrawEntityIDDelegate(value);
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x00017F05 File Offset: 0x00016105
		public void SetForceVsync(bool value)
		{
			ScriptingInterfaceOfIUtil.call_SetForceVsyncDelegate(value);
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00017F12 File Offset: 0x00016112
		public void SetFrameLimiterWithSleep(bool value)
		{
			ScriptingInterfaceOfIUtil.call_SetFrameLimiterWithSleepDelegate(value);
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x00017F1F File Offset: 0x0001611F
		public void SetGraphicsPreset(int preset)
		{
			ScriptingInterfaceOfIUtil.call_SetGraphicsPresetDelegate(preset);
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00017F2C File Offset: 0x0001612C
		public void SetLoadingScreenPercentage(float value)
		{
			ScriptingInterfaceOfIUtil.call_SetLoadingScreenPercentageDelegate(value);
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00017F39 File Offset: 0x00016139
		public void SetMessageLineRenderingState(bool value)
		{
			ScriptingInterfaceOfIUtil.call_SetMessageLineRenderingStateDelegate(value);
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00017F46 File Offset: 0x00016146
		public void SetPrintCallstackAtCrahses(bool value)
		{
			ScriptingInterfaceOfIUtil.call_SetPrintCallstackAtCrahsesDelegate(value);
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00017F53 File Offset: 0x00016153
		public void SetRenderAgents(bool value)
		{
			ScriptingInterfaceOfIUtil.call_SetRenderAgentsDelegate(value);
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00017F60 File Offset: 0x00016160
		public void SetRenderMode(int mode)
		{
			ScriptingInterfaceOfIUtil.call_SetRenderModeDelegate(mode);
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00017F6D File Offset: 0x0001616D
		public void SetReportMode(bool reportMode)
		{
			ScriptingInterfaceOfIUtil.call_SetReportModeDelegate(reportMode);
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00017F7A File Offset: 0x0001617A
		public void SetScreenTextRenderingState(bool value)
		{
			ScriptingInterfaceOfIUtil.call_SetScreenTextRenderingStateDelegate(value);
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00017F88 File Offset: 0x00016188
		public void SetWatchdogValue(string fileName, string groupName, string key, string value)
		{
			byte[] array = null;
			if (fileName != null)
			{
				int byteCount = ScriptingInterfaceOfIUtil._utf8.GetByteCount(fileName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(fileName, 0, fileName.Length, array, 0);
				array[byteCount] = 0;
			}
			byte[] array2 = null;
			if (groupName != null)
			{
				int byteCount2 = ScriptingInterfaceOfIUtil._utf8.GetByteCount(groupName);
				array2 = ((byteCount2 < 1024) ? CallbackStringBufferManager.StringBuffer1 : new byte[byteCount2 + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(groupName, 0, groupName.Length, array2, 0);
				array2[byteCount2] = 0;
			}
			byte[] array3 = null;
			if (key != null)
			{
				int byteCount3 = ScriptingInterfaceOfIUtil._utf8.GetByteCount(key);
				array3 = ((byteCount3 < 1024) ? CallbackStringBufferManager.StringBuffer2 : new byte[byteCount3 + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(key, 0, key.Length, array3, 0);
				array3[byteCount3] = 0;
			}
			byte[] array4 = null;
			if (value != null)
			{
				int byteCount4 = ScriptingInterfaceOfIUtil._utf8.GetByteCount(value);
				array4 = ((byteCount4 < 1024) ? CallbackStringBufferManager.StringBuffer3 : new byte[byteCount4 + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(value, 0, value.Length, array4, 0);
				array4[byteCount4] = 0;
			}
			ScriptingInterfaceOfIUtil.call_SetWatchdogValueDelegate(array, array2, array3, array4);
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x000180C0 File Offset: 0x000162C0
		public void SetWindowTitle(string title)
		{
			byte[] array = null;
			if (title != null)
			{
				int byteCount = ScriptingInterfaceOfIUtil._utf8.GetByteCount(title);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(title, 0, title.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIUtil.call_SetWindowTitleDelegate(array);
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0001811C File Offset: 0x0001631C
		public void StartScenePerformanceReport(string folderPath)
		{
			byte[] array = null;
			if (folderPath != null)
			{
				int byteCount = ScriptingInterfaceOfIUtil._utf8.GetByteCount(folderPath);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(folderPath, 0, folderPath.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIUtil.call_StartScenePerformanceReportDelegate(array);
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00018176 File Offset: 0x00016376
		public void TakeScreenshotFromPlatformPath(PlatformFilePath path)
		{
			ScriptingInterfaceOfIUtil.call_TakeScreenshotFromPlatformPathDelegate(path);
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00018184 File Offset: 0x00016384
		public void TakeScreenshotFromStringPath(string path)
		{
			byte[] array = null;
			if (path != null)
			{
				int byteCount = ScriptingInterfaceOfIUtil._utf8.GetByteCount(path);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(path, 0, path.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIUtil.call_TakeScreenshotFromStringPathDelegate(array);
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x000181E0 File Offset: 0x000163E0
		public string TakeSSFromTop(string file_name)
		{
			byte[] array = null;
			if (file_name != null)
			{
				int byteCount = ScriptingInterfaceOfIUtil._utf8.GetByteCount(file_name);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIUtil._utf8.GetBytes(file_name, 0, file_name.Length, array, 0);
				array[byteCount] = 0;
			}
			if (ScriptingInterfaceOfIUtil.call_TakeSSFromTopDelegate(array) != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x00018244 File Offset: 0x00016444
		public void ToggleRender()
		{
			ScriptingInterfaceOfIUtil.call_ToggleRenderDelegate();
		}

		// Token: 0x040004B1 RID: 1201
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x040004B2 RID: 1202
		public static ScriptingInterfaceOfIUtil.AddCommandLineFunctionDelegate call_AddCommandLineFunctionDelegate;

		// Token: 0x040004B3 RID: 1203
		public static ScriptingInterfaceOfIUtil.AddMainThreadPerformanceQueryDelegate call_AddMainThreadPerformanceQueryDelegate;

		// Token: 0x040004B4 RID: 1204
		public static ScriptingInterfaceOfIUtil.AddPerformanceReportTokenDelegate call_AddPerformanceReportTokenDelegate;

		// Token: 0x040004B5 RID: 1205
		public static ScriptingInterfaceOfIUtil.AddSceneObjectReportDelegate call_AddSceneObjectReportDelegate;

		// Token: 0x040004B6 RID: 1206
		public static ScriptingInterfaceOfIUtil.CheckIfAssetsAndSourcesAreSameDelegate call_CheckIfAssetsAndSourcesAreSameDelegate;

		// Token: 0x040004B7 RID: 1207
		public static ScriptingInterfaceOfIUtil.CheckIfTerrainShaderHeaderGenerationFinishedDelegate call_CheckIfTerrainShaderHeaderGenerationFinishedDelegate;

		// Token: 0x040004B8 RID: 1208
		public static ScriptingInterfaceOfIUtil.CheckResourceModificationsDelegate call_CheckResourceModificationsDelegate;

		// Token: 0x040004B9 RID: 1209
		public static ScriptingInterfaceOfIUtil.CheckSceneForProblemsDelegate call_CheckSceneForProblemsDelegate;

		// Token: 0x040004BA RID: 1210
		public static ScriptingInterfaceOfIUtil.CheckShaderCompilationDelegate call_CheckShaderCompilationDelegate;

		// Token: 0x040004BB RID: 1211
		public static ScriptingInterfaceOfIUtil.clear_decal_atlasDelegate call_clear_decal_atlasDelegate;

		// Token: 0x040004BC RID: 1212
		public static ScriptingInterfaceOfIUtil.ClearOldResourcesAndObjectsDelegate call_ClearOldResourcesAndObjectsDelegate;

		// Token: 0x040004BD RID: 1213
		public static ScriptingInterfaceOfIUtil.ClearShaderMemoryDelegate call_ClearShaderMemoryDelegate;

		// Token: 0x040004BE RID: 1214
		public static ScriptingInterfaceOfIUtil.CommandLineArgumentExistsDelegate call_CommandLineArgumentExistsDelegate;

		// Token: 0x040004BF RID: 1215
		public static ScriptingInterfaceOfIUtil.CompileAllShadersDelegate call_CompileAllShadersDelegate;

		// Token: 0x040004C0 RID: 1216
		public static ScriptingInterfaceOfIUtil.CompileTerrainShadersDistDelegate call_CompileTerrainShadersDistDelegate;

		// Token: 0x040004C1 RID: 1217
		public static ScriptingInterfaceOfIUtil.CreateSelectionInEditorDelegate call_CreateSelectionInEditorDelegate;

		// Token: 0x040004C2 RID: 1218
		public static ScriptingInterfaceOfIUtil.DebugSetGlobalLoadingWindowStateDelegate call_DebugSetGlobalLoadingWindowStateDelegate;

		// Token: 0x040004C3 RID: 1219
		public static ScriptingInterfaceOfIUtil.DeleteEntitiesInEditorSceneDelegate call_DeleteEntitiesInEditorSceneDelegate;

		// Token: 0x040004C4 RID: 1220
		public static ScriptingInterfaceOfIUtil.DetachWatchdogDelegate call_DetachWatchdogDelegate;

		// Token: 0x040004C5 RID: 1221
		public static ScriptingInterfaceOfIUtil.DidAutomatedGIBakeFinishedDelegate call_DidAutomatedGIBakeFinishedDelegate;

		// Token: 0x040004C6 RID: 1222
		public static ScriptingInterfaceOfIUtil.DisableCoreGameDelegate call_DisableCoreGameDelegate;

		// Token: 0x040004C7 RID: 1223
		public static ScriptingInterfaceOfIUtil.DisableGlobalEditDataCacherDelegate call_DisableGlobalEditDataCacherDelegate;

		// Token: 0x040004C8 RID: 1224
		public static ScriptingInterfaceOfIUtil.DisableGlobalLoadingWindowDelegate call_DisableGlobalLoadingWindowDelegate;

		// Token: 0x040004C9 RID: 1225
		public static ScriptingInterfaceOfIUtil.DoDelayedexitDelegate call_DoDelayedexitDelegate;

		// Token: 0x040004CA RID: 1226
		public static ScriptingInterfaceOfIUtil.DoFullBakeAllLevelsAutomatedDelegate call_DoFullBakeAllLevelsAutomatedDelegate;

		// Token: 0x040004CB RID: 1227
		public static ScriptingInterfaceOfIUtil.DoFullBakeSingleLevelAutomatedDelegate call_DoFullBakeSingleLevelAutomatedDelegate;

		// Token: 0x040004CC RID: 1228
		public static ScriptingInterfaceOfIUtil.DoLightOnlyBakeAllLevelsAutomatedDelegate call_DoLightOnlyBakeAllLevelsAutomatedDelegate;

		// Token: 0x040004CD RID: 1229
		public static ScriptingInterfaceOfIUtil.DoLightOnlyBakeSingleLevelAutomatedDelegate call_DoLightOnlyBakeSingleLevelAutomatedDelegate;

		// Token: 0x040004CE RID: 1230
		public static ScriptingInterfaceOfIUtil.DumpGPUMemoryStatisticsDelegate call_DumpGPUMemoryStatisticsDelegate;

		// Token: 0x040004CF RID: 1231
		public static ScriptingInterfaceOfIUtil.EnableGlobalEditDataCacherDelegate call_EnableGlobalEditDataCacherDelegate;

		// Token: 0x040004D0 RID: 1232
		public static ScriptingInterfaceOfIUtil.EnableGlobalLoadingWindowDelegate call_EnableGlobalLoadingWindowDelegate;

		// Token: 0x040004D1 RID: 1233
		public static ScriptingInterfaceOfIUtil.EnableSingleGPUQueryPerFrameDelegate call_EnableSingleGPUQueryPerFrameDelegate;

		// Token: 0x040004D2 RID: 1234
		public static ScriptingInterfaceOfIUtil.ExecuteCommandLineCommandDelegate call_ExecuteCommandLineCommandDelegate;

		// Token: 0x040004D3 RID: 1235
		public static ScriptingInterfaceOfIUtil.ExitProcessDelegate call_ExitProcessDelegate;

		// Token: 0x040004D4 RID: 1236
		public static ScriptingInterfaceOfIUtil.ExportNavMeshFaceMarksDelegate call_ExportNavMeshFaceMarksDelegate;

		// Token: 0x040004D5 RID: 1237
		public static ScriptingInterfaceOfIUtil.FindMeshesWithoutLodsDelegate call_FindMeshesWithoutLodsDelegate;

		// Token: 0x040004D6 RID: 1238
		public static ScriptingInterfaceOfIUtil.FlushManagedObjectsMemoryDelegate call_FlushManagedObjectsMemoryDelegate;

		// Token: 0x040004D7 RID: 1239
		public static ScriptingInterfaceOfIUtil.GatherCoreGameReferencesDelegate call_GatherCoreGameReferencesDelegate;

		// Token: 0x040004D8 RID: 1240
		public static ScriptingInterfaceOfIUtil.GenerateTerrainShaderHeadersDelegate call_GenerateTerrainShaderHeadersDelegate;

		// Token: 0x040004D9 RID: 1241
		public static ScriptingInterfaceOfIUtil.GetApplicationMemoryDelegate call_GetApplicationMemoryDelegate;

		// Token: 0x040004DA RID: 1242
		public static ScriptingInterfaceOfIUtil.GetApplicationMemoryStatisticsDelegate call_GetApplicationMemoryStatisticsDelegate;

		// Token: 0x040004DB RID: 1243
		public static ScriptingInterfaceOfIUtil.GetApplicationNameDelegate call_GetApplicationNameDelegate;

		// Token: 0x040004DC RID: 1244
		public static ScriptingInterfaceOfIUtil.GetAttachmentsPathDelegate call_GetAttachmentsPathDelegate;

		// Token: 0x040004DD RID: 1245
		public static ScriptingInterfaceOfIUtil.GetBaseDirectoryDelegate call_GetBaseDirectoryDelegate;

		// Token: 0x040004DE RID: 1246
		public static ScriptingInterfaceOfIUtil.GetBenchmarkStatusDelegate call_GetBenchmarkStatusDelegate;

		// Token: 0x040004DF RID: 1247
		public static ScriptingInterfaceOfIUtil.GetBuildNumberDelegate call_GetBuildNumberDelegate;

		// Token: 0x040004E0 RID: 1248
		public static ScriptingInterfaceOfIUtil.GetConsoleHostMachineDelegate call_GetConsoleHostMachineDelegate;

		// Token: 0x040004E1 RID: 1249
		public static ScriptingInterfaceOfIUtil.GetCoreGameStateDelegate call_GetCoreGameStateDelegate;

		// Token: 0x040004E2 RID: 1250
		public static ScriptingInterfaceOfIUtil.GetCurrentCpuMemoryUsageDelegate call_GetCurrentCpuMemoryUsageDelegate;

		// Token: 0x040004E3 RID: 1251
		public static ScriptingInterfaceOfIUtil.GetCurrentEstimatedGPUMemoryCostMBDelegate call_GetCurrentEstimatedGPUMemoryCostMBDelegate;

		// Token: 0x040004E4 RID: 1252
		public static ScriptingInterfaceOfIUtil.GetCurrentProcessIDDelegate call_GetCurrentProcessIDDelegate;

		// Token: 0x040004E5 RID: 1253
		public static ScriptingInterfaceOfIUtil.GetCurrentThreadIdDelegate call_GetCurrentThreadIdDelegate;

		// Token: 0x040004E6 RID: 1254
		public static ScriptingInterfaceOfIUtil.GetDeltaTimeDelegate call_GetDeltaTimeDelegate;

		// Token: 0x040004E7 RID: 1255
		public static ScriptingInterfaceOfIUtil.GetDetailedGPUBufferMemoryStatsDelegate call_GetDetailedGPUBufferMemoryStatsDelegate;

		// Token: 0x040004E8 RID: 1256
		public static ScriptingInterfaceOfIUtil.GetDetailedXBOXMemoryInfoDelegate call_GetDetailedXBOXMemoryInfoDelegate;

		// Token: 0x040004E9 RID: 1257
		public static ScriptingInterfaceOfIUtil.GetEditorSelectedEntitiesDelegate call_GetEditorSelectedEntitiesDelegate;

		// Token: 0x040004EA RID: 1258
		public static ScriptingInterfaceOfIUtil.GetEditorSelectedEntityCountDelegate call_GetEditorSelectedEntityCountDelegate;

		// Token: 0x040004EB RID: 1259
		public static ScriptingInterfaceOfIUtil.GetEngineFrameNoDelegate call_GetEngineFrameNoDelegate;

		// Token: 0x040004EC RID: 1260
		public static ScriptingInterfaceOfIUtil.GetEntitiesOfSelectionSetDelegate call_GetEntitiesOfSelectionSetDelegate;

		// Token: 0x040004ED RID: 1261
		public static ScriptingInterfaceOfIUtil.GetEntityCountOfSelectionSetDelegate call_GetEntityCountOfSelectionSetDelegate;

		// Token: 0x040004EE RID: 1262
		public static ScriptingInterfaceOfIUtil.GetExecutableWorkingDirectoryDelegate call_GetExecutableWorkingDirectoryDelegate;

		// Token: 0x040004EF RID: 1263
		public static ScriptingInterfaceOfIUtil.GetFpsDelegate call_GetFpsDelegate;

		// Token: 0x040004F0 RID: 1264
		public static ScriptingInterfaceOfIUtil.GetFrameLimiterWithSleepDelegate call_GetFrameLimiterWithSleepDelegate;

		// Token: 0x040004F1 RID: 1265
		public static ScriptingInterfaceOfIUtil.GetFullCommandLineStringDelegate call_GetFullCommandLineStringDelegate;

		// Token: 0x040004F2 RID: 1266
		public static ScriptingInterfaceOfIUtil.GetFullFilePathOfSceneDelegate call_GetFullFilePathOfSceneDelegate;

		// Token: 0x040004F3 RID: 1267
		public static ScriptingInterfaceOfIUtil.GetFullModulePathDelegate call_GetFullModulePathDelegate;

		// Token: 0x040004F4 RID: 1268
		public static ScriptingInterfaceOfIUtil.GetFullModulePathsDelegate call_GetFullModulePathsDelegate;

		// Token: 0x040004F5 RID: 1269
		public static ScriptingInterfaceOfIUtil.GetGPUMemoryMBDelegate call_GetGPUMemoryMBDelegate;

		// Token: 0x040004F6 RID: 1270
		public static ScriptingInterfaceOfIUtil.GetGpuMemoryOfAllocationGroupDelegate call_GetGpuMemoryOfAllocationGroupDelegate;

		// Token: 0x040004F7 RID: 1271
		public static ScriptingInterfaceOfIUtil.GetGPUMemoryStatsDelegate call_GetGPUMemoryStatsDelegate;

		// Token: 0x040004F8 RID: 1272
		public static ScriptingInterfaceOfIUtil.GetLocalOutputPathDelegate call_GetLocalOutputPathDelegate;

		// Token: 0x040004F9 RID: 1273
		public static ScriptingInterfaceOfIUtil.GetMainFpsDelegate call_GetMainFpsDelegate;

		// Token: 0x040004FA RID: 1274
		public static ScriptingInterfaceOfIUtil.GetMainThreadIdDelegate call_GetMainThreadIdDelegate;

		// Token: 0x040004FB RID: 1275
		public static ScriptingInterfaceOfIUtil.GetMemoryUsageOfCategoryDelegate call_GetMemoryUsageOfCategoryDelegate;

		// Token: 0x040004FC RID: 1276
		public static ScriptingInterfaceOfIUtil.GetModulesCodeDelegate call_GetModulesCodeDelegate;

		// Token: 0x040004FD RID: 1277
		public static ScriptingInterfaceOfIUtil.GetNativeMemoryStatisticsDelegate call_GetNativeMemoryStatisticsDelegate;

		// Token: 0x040004FE RID: 1278
		public static ScriptingInterfaceOfIUtil.GetNumberOfShaderCompilationsInProgressDelegate call_GetNumberOfShaderCompilationsInProgressDelegate;

		// Token: 0x040004FF RID: 1279
		public static ScriptingInterfaceOfIUtil.GetPCInfoDelegate call_GetPCInfoDelegate;

		// Token: 0x04000500 RID: 1280
		public static ScriptingInterfaceOfIUtil.GetRendererFpsDelegate call_GetRendererFpsDelegate;

		// Token: 0x04000501 RID: 1281
		public static ScriptingInterfaceOfIUtil.GetReturnCodeDelegate call_GetReturnCodeDelegate;

		// Token: 0x04000502 RID: 1282
		public static ScriptingInterfaceOfIUtil.GetSingleModuleScenesOfModuleDelegate call_GetSingleModuleScenesOfModuleDelegate;

		// Token: 0x04000503 RID: 1283
		public static ScriptingInterfaceOfIUtil.GetSteamAppIdDelegate call_GetSteamAppIdDelegate;

		// Token: 0x04000504 RID: 1284
		public static ScriptingInterfaceOfIUtil.GetSystemLanguageDelegate call_GetSystemLanguageDelegate;

		// Token: 0x04000505 RID: 1285
		public static ScriptingInterfaceOfIUtil.GetVertexBufferChunkSystemMemoryUsageDelegate call_GetVertexBufferChunkSystemMemoryUsageDelegate;

		// Token: 0x04000506 RID: 1286
		public static ScriptingInterfaceOfIUtil.GetVisualTestsTestFilesPathDelegate call_GetVisualTestsTestFilesPathDelegate;

		// Token: 0x04000507 RID: 1287
		public static ScriptingInterfaceOfIUtil.GetVisualTestsValidatePathDelegate call_GetVisualTestsValidatePathDelegate;

		// Token: 0x04000508 RID: 1288
		public static ScriptingInterfaceOfIUtil.IsBenchmarkQuitedDelegate call_IsBenchmarkQuitedDelegate;

		// Token: 0x04000509 RID: 1289
		public static ScriptingInterfaceOfIUtil.IsDetailedSoundLogOnDelegate call_IsDetailedSoundLogOnDelegate;

		// Token: 0x0400050A RID: 1290
		public static ScriptingInterfaceOfIUtil.IsEditModeEnabledDelegate call_IsEditModeEnabledDelegate;

		// Token: 0x0400050B RID: 1291
		public static ScriptingInterfaceOfIUtil.IsSceneReportFinishedDelegate call_IsSceneReportFinishedDelegate;

		// Token: 0x0400050C RID: 1292
		public static ScriptingInterfaceOfIUtil.LoadSkyBoxesDelegate call_LoadSkyBoxesDelegate;

		// Token: 0x0400050D RID: 1293
		public static ScriptingInterfaceOfIUtil.LoadVirtualTextureTilesetDelegate call_LoadVirtualTextureTilesetDelegate;

		// Token: 0x0400050E RID: 1294
		public static ScriptingInterfaceOfIUtil.ManagedParallelForDelegate call_ManagedParallelForDelegate;

		// Token: 0x0400050F RID: 1295
		public static ScriptingInterfaceOfIUtil.ManagedParallelForWithDtDelegate call_ManagedParallelForWithDtDelegate;

		// Token: 0x04000510 RID: 1296
		public static ScriptingInterfaceOfIUtil.OnLoadingWindowDisabledDelegate call_OnLoadingWindowDisabledDelegate;

		// Token: 0x04000511 RID: 1297
		public static ScriptingInterfaceOfIUtil.OnLoadingWindowEnabledDelegate call_OnLoadingWindowEnabledDelegate;

		// Token: 0x04000512 RID: 1298
		public static ScriptingInterfaceOfIUtil.OpenOnscreenKeyboardDelegate call_OpenOnscreenKeyboardDelegate;

		// Token: 0x04000513 RID: 1299
		public static ScriptingInterfaceOfIUtil.OutputBenchmarkValuesToPerformanceReporterDelegate call_OutputBenchmarkValuesToPerformanceReporterDelegate;

		// Token: 0x04000514 RID: 1300
		public static ScriptingInterfaceOfIUtil.OutputPerformanceReportsDelegate call_OutputPerformanceReportsDelegate;

		// Token: 0x04000515 RID: 1301
		public static ScriptingInterfaceOfIUtil.PairSceneNameToModuleNameDelegate call_PairSceneNameToModuleNameDelegate;

		// Token: 0x04000516 RID: 1302
		public static ScriptingInterfaceOfIUtil.ProcessWindowTitleDelegate call_ProcessWindowTitleDelegate;

		// Token: 0x04000517 RID: 1303
		public static ScriptingInterfaceOfIUtil.QuitGameDelegate call_QuitGameDelegate;

		// Token: 0x04000518 RID: 1304
		public static ScriptingInterfaceOfIUtil.RegisterGPUAllocationGroupDelegate call_RegisterGPUAllocationGroupDelegate;

		// Token: 0x04000519 RID: 1305
		public static ScriptingInterfaceOfIUtil.RegisterMeshForGPUMorphDelegate call_RegisterMeshForGPUMorphDelegate;

		// Token: 0x0400051A RID: 1306
		public static ScriptingInterfaceOfIUtil.SaveDataAsTextureDelegate call_SaveDataAsTextureDelegate;

		// Token: 0x0400051B RID: 1307
		public static ScriptingInterfaceOfIUtil.SelectEntitiesDelegate call_SelectEntitiesDelegate;

		// Token: 0x0400051C RID: 1308
		public static ScriptingInterfaceOfIUtil.SetAllocationAlwaysValidSceneDelegate call_SetAllocationAlwaysValidSceneDelegate;

		// Token: 0x0400051D RID: 1309
		public static ScriptingInterfaceOfIUtil.SetAssertionAtShaderCompileDelegate call_SetAssertionAtShaderCompileDelegate;

		// Token: 0x0400051E RID: 1310
		public static ScriptingInterfaceOfIUtil.SetAssertionsAndWarningsSetExitCodeDelegate call_SetAssertionsAndWarningsSetExitCodeDelegate;

		// Token: 0x0400051F RID: 1311
		public static ScriptingInterfaceOfIUtil.SetBenchmarkStatusDelegate call_SetBenchmarkStatusDelegate;

		// Token: 0x04000520 RID: 1312
		public static ScriptingInterfaceOfIUtil.SetCoreGameStateDelegate call_SetCoreGameStateDelegate;

		// Token: 0x04000521 RID: 1313
		public static ScriptingInterfaceOfIUtil.SetCrashOnAssertsDelegate call_SetCrashOnAssertsDelegate;

		// Token: 0x04000522 RID: 1314
		public static ScriptingInterfaceOfIUtil.SetCrashOnWarningsDelegate call_SetCrashOnWarningsDelegate;

		// Token: 0x04000523 RID: 1315
		public static ScriptingInterfaceOfIUtil.SetCrashReportCustomStackDelegate call_SetCrashReportCustomStackDelegate;

		// Token: 0x04000524 RID: 1316
		public static ScriptingInterfaceOfIUtil.SetCrashReportCustomStringDelegate call_SetCrashReportCustomStringDelegate;

		// Token: 0x04000525 RID: 1317
		public static ScriptingInterfaceOfIUtil.SetDisableDumpGenerationDelegate call_SetDisableDumpGenerationDelegate;

		// Token: 0x04000526 RID: 1318
		public static ScriptingInterfaceOfIUtil.SetDumpFolderPathDelegate call_SetDumpFolderPathDelegate;

		// Token: 0x04000527 RID: 1319
		public static ScriptingInterfaceOfIUtil.SetFixedDtDelegate call_SetFixedDtDelegate;

		// Token: 0x04000528 RID: 1320
		public static ScriptingInterfaceOfIUtil.SetForceDrawEntityIDDelegate call_SetForceDrawEntityIDDelegate;

		// Token: 0x04000529 RID: 1321
		public static ScriptingInterfaceOfIUtil.SetForceVsyncDelegate call_SetForceVsyncDelegate;

		// Token: 0x0400052A RID: 1322
		public static ScriptingInterfaceOfIUtil.SetFrameLimiterWithSleepDelegate call_SetFrameLimiterWithSleepDelegate;

		// Token: 0x0400052B RID: 1323
		public static ScriptingInterfaceOfIUtil.SetGraphicsPresetDelegate call_SetGraphicsPresetDelegate;

		// Token: 0x0400052C RID: 1324
		public static ScriptingInterfaceOfIUtil.SetLoadingScreenPercentageDelegate call_SetLoadingScreenPercentageDelegate;

		// Token: 0x0400052D RID: 1325
		public static ScriptingInterfaceOfIUtil.SetMessageLineRenderingStateDelegate call_SetMessageLineRenderingStateDelegate;

		// Token: 0x0400052E RID: 1326
		public static ScriptingInterfaceOfIUtil.SetPrintCallstackAtCrahsesDelegate call_SetPrintCallstackAtCrahsesDelegate;

		// Token: 0x0400052F RID: 1327
		public static ScriptingInterfaceOfIUtil.SetRenderAgentsDelegate call_SetRenderAgentsDelegate;

		// Token: 0x04000530 RID: 1328
		public static ScriptingInterfaceOfIUtil.SetRenderModeDelegate call_SetRenderModeDelegate;

		// Token: 0x04000531 RID: 1329
		public static ScriptingInterfaceOfIUtil.SetReportModeDelegate call_SetReportModeDelegate;

		// Token: 0x04000532 RID: 1330
		public static ScriptingInterfaceOfIUtil.SetScreenTextRenderingStateDelegate call_SetScreenTextRenderingStateDelegate;

		// Token: 0x04000533 RID: 1331
		public static ScriptingInterfaceOfIUtil.SetWatchdogValueDelegate call_SetWatchdogValueDelegate;

		// Token: 0x04000534 RID: 1332
		public static ScriptingInterfaceOfIUtil.SetWindowTitleDelegate call_SetWindowTitleDelegate;

		// Token: 0x04000535 RID: 1333
		public static ScriptingInterfaceOfIUtil.StartScenePerformanceReportDelegate call_StartScenePerformanceReportDelegate;

		// Token: 0x04000536 RID: 1334
		public static ScriptingInterfaceOfIUtil.TakeScreenshotFromPlatformPathDelegate call_TakeScreenshotFromPlatformPathDelegate;

		// Token: 0x04000537 RID: 1335
		public static ScriptingInterfaceOfIUtil.TakeScreenshotFromStringPathDelegate call_TakeScreenshotFromStringPathDelegate;

		// Token: 0x04000538 RID: 1336
		public static ScriptingInterfaceOfIUtil.TakeSSFromTopDelegate call_TakeSSFromTopDelegate;

		// Token: 0x04000539 RID: 1337
		public static ScriptingInterfaceOfIUtil.ToggleRenderDelegate call_ToggleRenderDelegate;

		// Token: 0x020004FB RID: 1275
		// (Invoke) Token: 0x060018EF RID: 6383
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddCommandLineFunctionDelegate(byte[] concatName);

		// Token: 0x020004FC RID: 1276
		// (Invoke) Token: 0x060018F3 RID: 6387
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddMainThreadPerformanceQueryDelegate(byte[] parent, byte[] name, float seconds);

		// Token: 0x020004FD RID: 1277
		// (Invoke) Token: 0x060018F7 RID: 6391
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddPerformanceReportTokenDelegate(byte[] performance_type, byte[] name, float loading_time);

		// Token: 0x020004FE RID: 1278
		// (Invoke) Token: 0x060018FB RID: 6395
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddSceneObjectReportDelegate(byte[] scene_name, byte[] report_name, float report_value);

		// Token: 0x020004FF RID: 1279
		// (Invoke) Token: 0x060018FF RID: 6399
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void CheckIfAssetsAndSourcesAreSameDelegate();

		// Token: 0x02000500 RID: 1280
		// (Invoke) Token: 0x06001903 RID: 6403
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool CheckIfTerrainShaderHeaderGenerationFinishedDelegate();

		// Token: 0x02000501 RID: 1281
		// (Invoke) Token: 0x06001907 RID: 6407
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void CheckResourceModificationsDelegate();

		// Token: 0x02000502 RID: 1282
		// (Invoke) Token: 0x0600190B RID: 6411
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void CheckSceneForProblemsDelegate(byte[] path);

		// Token: 0x02000503 RID: 1283
		// (Invoke) Token: 0x0600190F RID: 6415
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool CheckShaderCompilationDelegate();

		// Token: 0x02000504 RID: 1284
		// (Invoke) Token: 0x06001913 RID: 6419
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void clear_decal_atlasDelegate(DecalAtlasGroup atlasGroup);

		// Token: 0x02000505 RID: 1285
		// (Invoke) Token: 0x06001917 RID: 6423
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ClearOldResourcesAndObjectsDelegate();

		// Token: 0x02000506 RID: 1286
		// (Invoke) Token: 0x0600191B RID: 6427
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ClearShaderMemoryDelegate();

		// Token: 0x02000507 RID: 1287
		// (Invoke) Token: 0x0600191F RID: 6431
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool CommandLineArgumentExistsDelegate(byte[] str);

		// Token: 0x02000508 RID: 1288
		// (Invoke) Token: 0x06001923 RID: 6435
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void CompileAllShadersDelegate(byte[] targetPlatform);

		// Token: 0x02000509 RID: 1289
		// (Invoke) Token: 0x06001927 RID: 6439
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void CompileTerrainShadersDistDelegate(byte[] targetPlatform, byte[] targetConfig, byte[] output_path);

		// Token: 0x0200050A RID: 1290
		// (Invoke) Token: 0x0600192B RID: 6443
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void CreateSelectionInEditorDelegate(IntPtr gameEntities, int entityCount, byte[] name);

		// Token: 0x0200050B RID: 1291
		// (Invoke) Token: 0x0600192F RID: 6447
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void DebugSetGlobalLoadingWindowStateDelegate([MarshalAs(UnmanagedType.U1)] bool s);

		// Token: 0x0200050C RID: 1292
		// (Invoke) Token: 0x06001933 RID: 6451
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void DeleteEntitiesInEditorSceneDelegate(IntPtr gameEntities, int entityCount);

		// Token: 0x0200050D RID: 1293
		// (Invoke) Token: 0x06001937 RID: 6455
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void DetachWatchdogDelegate();

		// Token: 0x0200050E RID: 1294
		// (Invoke) Token: 0x0600193B RID: 6459
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool DidAutomatedGIBakeFinishedDelegate();

		// Token: 0x0200050F RID: 1295
		// (Invoke) Token: 0x0600193F RID: 6463
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void DisableCoreGameDelegate();

		// Token: 0x02000510 RID: 1296
		// (Invoke) Token: 0x06001943 RID: 6467
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void DisableGlobalEditDataCacherDelegate();

		// Token: 0x02000511 RID: 1297
		// (Invoke) Token: 0x06001947 RID: 6471
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void DisableGlobalLoadingWindowDelegate();

		// Token: 0x02000512 RID: 1298
		// (Invoke) Token: 0x0600194B RID: 6475
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void DoDelayedexitDelegate(int returnCode);

		// Token: 0x02000513 RID: 1299
		// (Invoke) Token: 0x0600194F RID: 6479
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void DoFullBakeAllLevelsAutomatedDelegate(byte[] module, byte[] sceneName);

		// Token: 0x02000514 RID: 1300
		// (Invoke) Token: 0x06001953 RID: 6483
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void DoFullBakeSingleLevelAutomatedDelegate(byte[] module, byte[] sceneName);

		// Token: 0x02000515 RID: 1301
		// (Invoke) Token: 0x06001957 RID: 6487
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void DoLightOnlyBakeAllLevelsAutomatedDelegate(byte[] module, byte[] sceneName);

		// Token: 0x02000516 RID: 1302
		// (Invoke) Token: 0x0600195B RID: 6491
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void DoLightOnlyBakeSingleLevelAutomatedDelegate(byte[] module, byte[] sceneName);

		// Token: 0x02000517 RID: 1303
		// (Invoke) Token: 0x0600195F RID: 6495
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void DumpGPUMemoryStatisticsDelegate(byte[] filePath);

		// Token: 0x02000518 RID: 1304
		// (Invoke) Token: 0x06001963 RID: 6499
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void EnableGlobalEditDataCacherDelegate();

		// Token: 0x02000519 RID: 1305
		// (Invoke) Token: 0x06001967 RID: 6503
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void EnableGlobalLoadingWindowDelegate();

		// Token: 0x0200051A RID: 1306
		// (Invoke) Token: 0x0600196B RID: 6507
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void EnableSingleGPUQueryPerFrameDelegate();

		// Token: 0x0200051B RID: 1307
		// (Invoke) Token: 0x0600196F RID: 6511
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int ExecuteCommandLineCommandDelegate(byte[] command);

		// Token: 0x0200051C RID: 1308
		// (Invoke) Token: 0x06001973 RID: 6515
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ExitProcessDelegate(int exitCode);

		// Token: 0x0200051D RID: 1309
		// (Invoke) Token: 0x06001977 RID: 6519
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int ExportNavMeshFaceMarksDelegate(byte[] file_name);

		// Token: 0x0200051E RID: 1310
		// (Invoke) Token: 0x0600197B RID: 6523
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void FindMeshesWithoutLodsDelegate(byte[] module_name);

		// Token: 0x0200051F RID: 1311
		// (Invoke) Token: 0x0600197F RID: 6527
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void FlushManagedObjectsMemoryDelegate();

		// Token: 0x02000520 RID: 1312
		// (Invoke) Token: 0x06001983 RID: 6531
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GatherCoreGameReferencesDelegate(byte[] scene_names);

		// Token: 0x02000521 RID: 1313
		// (Invoke) Token: 0x06001987 RID: 6535
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GenerateTerrainShaderHeadersDelegate(byte[] targetPlatform, byte[] targetConfig, byte[] output_path);

		// Token: 0x02000522 RID: 1314
		// (Invoke) Token: 0x0600198B RID: 6539
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetApplicationMemoryDelegate();

		// Token: 0x02000523 RID: 1315
		// (Invoke) Token: 0x0600198F RID: 6543
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetApplicationMemoryStatisticsDelegate();

		// Token: 0x02000524 RID: 1316
		// (Invoke) Token: 0x06001993 RID: 6547
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetApplicationNameDelegate();

		// Token: 0x02000525 RID: 1317
		// (Invoke) Token: 0x06001997 RID: 6551
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetAttachmentsPathDelegate();

		// Token: 0x02000526 RID: 1318
		// (Invoke) Token: 0x0600199B RID: 6555
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetBaseDirectoryDelegate();

		// Token: 0x02000527 RID: 1319
		// (Invoke) Token: 0x0600199F RID: 6559
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetBenchmarkStatusDelegate();

		// Token: 0x02000528 RID: 1320
		// (Invoke) Token: 0x060019A3 RID: 6563
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetBuildNumberDelegate();

		// Token: 0x02000529 RID: 1321
		// (Invoke) Token: 0x060019A7 RID: 6567
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetConsoleHostMachineDelegate();

		// Token: 0x0200052A RID: 1322
		// (Invoke) Token: 0x060019AB RID: 6571
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetCoreGameStateDelegate();

		// Token: 0x0200052B RID: 1323
		// (Invoke) Token: 0x060019AF RID: 6575
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate ulong GetCurrentCpuMemoryUsageDelegate();

		// Token: 0x0200052C RID: 1324
		// (Invoke) Token: 0x060019B3 RID: 6579
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetCurrentEstimatedGPUMemoryCostMBDelegate();

		// Token: 0x0200052D RID: 1325
		// (Invoke) Token: 0x060019B7 RID: 6583
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate uint GetCurrentProcessIDDelegate();

		// Token: 0x0200052E RID: 1326
		// (Invoke) Token: 0x060019BB RID: 6587
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate ulong GetCurrentThreadIdDelegate();

		// Token: 0x0200052F RID: 1327
		// (Invoke) Token: 0x060019BF RID: 6591
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetDeltaTimeDelegate(int timerId);

		// Token: 0x02000530 RID: 1328
		// (Invoke) Token: 0x060019C3 RID: 6595
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetDetailedGPUBufferMemoryStatsDelegate(ref int totalMemoryAllocated, ref int totalMemoryUsed, ref int emptyChunkCount);

		// Token: 0x02000531 RID: 1329
		// (Invoke) Token: 0x060019C7 RID: 6599
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetDetailedXBOXMemoryInfoDelegate();

		// Token: 0x02000532 RID: 1330
		// (Invoke) Token: 0x060019CB RID: 6603
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetEditorSelectedEntitiesDelegate(IntPtr gameEntitiesTemp);

		// Token: 0x02000533 RID: 1331
		// (Invoke) Token: 0x060019CF RID: 6607
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetEditorSelectedEntityCountDelegate();

		// Token: 0x02000534 RID: 1332
		// (Invoke) Token: 0x060019D3 RID: 6611
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetEngineFrameNoDelegate();

		// Token: 0x02000535 RID: 1333
		// (Invoke) Token: 0x060019D7 RID: 6615
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetEntitiesOfSelectionSetDelegate(byte[] name, IntPtr gameEntitiesTemp);

		// Token: 0x02000536 RID: 1334
		// (Invoke) Token: 0x060019DB RID: 6619
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetEntityCountOfSelectionSetDelegate(byte[] name);

		// Token: 0x02000537 RID: 1335
		// (Invoke) Token: 0x060019DF RID: 6623
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetExecutableWorkingDirectoryDelegate();

		// Token: 0x02000538 RID: 1336
		// (Invoke) Token: 0x060019E3 RID: 6627
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetFpsDelegate();

		// Token: 0x02000539 RID: 1337
		// (Invoke) Token: 0x060019E7 RID: 6631
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool GetFrameLimiterWithSleepDelegate();

		// Token: 0x0200053A RID: 1338
		// (Invoke) Token: 0x060019EB RID: 6635
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetFullCommandLineStringDelegate();

		// Token: 0x0200053B RID: 1339
		// (Invoke) Token: 0x060019EF RID: 6639
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetFullFilePathOfSceneDelegate(byte[] sceneName);

		// Token: 0x0200053C RID: 1340
		// (Invoke) Token: 0x060019F3 RID: 6643
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetFullModulePathDelegate(byte[] moduleName);

		// Token: 0x0200053D RID: 1341
		// (Invoke) Token: 0x060019F7 RID: 6647
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetFullModulePathsDelegate();

		// Token: 0x0200053E RID: 1342
		// (Invoke) Token: 0x060019FB RID: 6651
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetGPUMemoryMBDelegate();

		// Token: 0x0200053F RID: 1343
		// (Invoke) Token: 0x060019FF RID: 6655
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate ulong GetGpuMemoryOfAllocationGroupDelegate(byte[] allocationName);

		// Token: 0x02000540 RID: 1344
		// (Invoke) Token: 0x06001A03 RID: 6659
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetGPUMemoryStatsDelegate(ref float totalMemory, ref float renderTargetMemory, ref float depthTargetMemory, ref float srvMemory, ref float bufferMemory);

		// Token: 0x02000541 RID: 1345
		// (Invoke) Token: 0x06001A07 RID: 6663
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetLocalOutputPathDelegate();

		// Token: 0x02000542 RID: 1346
		// (Invoke) Token: 0x06001A0B RID: 6667
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetMainFpsDelegate();

		// Token: 0x02000543 RID: 1347
		// (Invoke) Token: 0x06001A0F RID: 6671
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate ulong GetMainThreadIdDelegate();

		// Token: 0x02000544 RID: 1348
		// (Invoke) Token: 0x06001A13 RID: 6675
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetMemoryUsageOfCategoryDelegate(int index);

		// Token: 0x02000545 RID: 1349
		// (Invoke) Token: 0x06001A17 RID: 6679
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetModulesCodeDelegate();

		// Token: 0x02000546 RID: 1350
		// (Invoke) Token: 0x06001A1B RID: 6683
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetNativeMemoryStatisticsDelegate();

		// Token: 0x02000547 RID: 1351
		// (Invoke) Token: 0x06001A1F RID: 6687
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetNumberOfShaderCompilationsInProgressDelegate();

		// Token: 0x02000548 RID: 1352
		// (Invoke) Token: 0x06001A23 RID: 6691
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetPCInfoDelegate();

		// Token: 0x02000549 RID: 1353
		// (Invoke) Token: 0x06001A27 RID: 6695
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetRendererFpsDelegate();

		// Token: 0x0200054A RID: 1354
		// (Invoke) Token: 0x06001A2B RID: 6699
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetReturnCodeDelegate();

		// Token: 0x0200054B RID: 1355
		// (Invoke) Token: 0x06001A2F RID: 6703
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetSingleModuleScenesOfModuleDelegate(byte[] moduleName);

		// Token: 0x0200054C RID: 1356
		// (Invoke) Token: 0x06001A33 RID: 6707
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetSteamAppIdDelegate();

		// Token: 0x0200054D RID: 1357
		// (Invoke) Token: 0x06001A37 RID: 6711
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetSystemLanguageDelegate();

		// Token: 0x0200054E RID: 1358
		// (Invoke) Token: 0x06001A3B RID: 6715
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetVertexBufferChunkSystemMemoryUsageDelegate();

		// Token: 0x0200054F RID: 1359
		// (Invoke) Token: 0x06001A3F RID: 6719
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetVisualTestsTestFilesPathDelegate();

		// Token: 0x02000550 RID: 1360
		// (Invoke) Token: 0x06001A43 RID: 6723
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetVisualTestsValidatePathDelegate();

		// Token: 0x02000551 RID: 1361
		// (Invoke) Token: 0x06001A47 RID: 6727
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsBenchmarkQuitedDelegate();

		// Token: 0x02000552 RID: 1362
		// (Invoke) Token: 0x06001A4B RID: 6731
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int IsDetailedSoundLogOnDelegate();

		// Token: 0x02000553 RID: 1363
		// (Invoke) Token: 0x06001A4F RID: 6735
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsEditModeEnabledDelegate();

		// Token: 0x02000554 RID: 1364
		// (Invoke) Token: 0x06001A53 RID: 6739
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsSceneReportFinishedDelegate();

		// Token: 0x02000555 RID: 1365
		// (Invoke) Token: 0x06001A57 RID: 6743
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void LoadSkyBoxesDelegate();

		// Token: 0x02000556 RID: 1366
		// (Invoke) Token: 0x06001A5B RID: 6747
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void LoadVirtualTextureTilesetDelegate(byte[] name);

		// Token: 0x02000557 RID: 1367
		// (Invoke) Token: 0x06001A5F RID: 6751
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ManagedParallelForDelegate(int fromInclusive, int toExclusive, long curKey, int grainSize);

		// Token: 0x02000558 RID: 1368
		// (Invoke) Token: 0x06001A63 RID: 6755
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ManagedParallelForWithDtDelegate(int fromInclusive, int toExclusive, long curKey, int grainSize);

		// Token: 0x02000559 RID: 1369
		// (Invoke) Token: 0x06001A67 RID: 6759
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void OnLoadingWindowDisabledDelegate();

		// Token: 0x0200055A RID: 1370
		// (Invoke) Token: 0x06001A6B RID: 6763
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void OnLoadingWindowEnabledDelegate();

		// Token: 0x0200055B RID: 1371
		// (Invoke) Token: 0x06001A6F RID: 6767
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void OpenOnscreenKeyboardDelegate(byte[] initialText, byte[] descriptionText, int maxLength, int keyboardTypeEnum);

		// Token: 0x0200055C RID: 1372
		// (Invoke) Token: 0x06001A73 RID: 6771
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void OutputBenchmarkValuesToPerformanceReporterDelegate();

		// Token: 0x0200055D RID: 1373
		// (Invoke) Token: 0x06001A77 RID: 6775
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void OutputPerformanceReportsDelegate();

		// Token: 0x0200055E RID: 1374
		// (Invoke) Token: 0x06001A7B RID: 6779
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void PairSceneNameToModuleNameDelegate(byte[] sceneName, byte[] moduleName);

		// Token: 0x0200055F RID: 1375
		// (Invoke) Token: 0x06001A7F RID: 6783
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int ProcessWindowTitleDelegate(byte[] title);

		// Token: 0x02000560 RID: 1376
		// (Invoke) Token: 0x06001A83 RID: 6787
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void QuitGameDelegate();

		// Token: 0x02000561 RID: 1377
		// (Invoke) Token: 0x06001A87 RID: 6791
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int RegisterGPUAllocationGroupDelegate(byte[] name);

		// Token: 0x02000562 RID: 1378
		// (Invoke) Token: 0x06001A8B RID: 6795
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RegisterMeshForGPUMorphDelegate(byte[] metaMeshName);

		// Token: 0x02000563 RID: 1379
		// (Invoke) Token: 0x06001A8F RID: 6799
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int SaveDataAsTextureDelegate(byte[] path, int width, int height, IntPtr data);

		// Token: 0x02000564 RID: 1380
		// (Invoke) Token: 0x06001A93 RID: 6803
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SelectEntitiesDelegate(IntPtr gameEntities, int entityCount);

		// Token: 0x02000565 RID: 1381
		// (Invoke) Token: 0x06001A97 RID: 6807
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetAllocationAlwaysValidSceneDelegate(UIntPtr scene);

		// Token: 0x02000566 RID: 1382
		// (Invoke) Token: 0x06001A9B RID: 6811
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetAssertionAtShaderCompileDelegate([MarshalAs(UnmanagedType.U1)] bool value);

		// Token: 0x02000567 RID: 1383
		// (Invoke) Token: 0x06001A9F RID: 6815
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetAssertionsAndWarningsSetExitCodeDelegate([MarshalAs(UnmanagedType.U1)] bool value);

		// Token: 0x02000568 RID: 1384
		// (Invoke) Token: 0x06001AA3 RID: 6819
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetBenchmarkStatusDelegate(int status, byte[] def);

		// Token: 0x02000569 RID: 1385
		// (Invoke) Token: 0x06001AA7 RID: 6823
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetCoreGameStateDelegate(int state);

		// Token: 0x0200056A RID: 1386
		// (Invoke) Token: 0x06001AAB RID: 6827
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetCrashOnAssertsDelegate([MarshalAs(UnmanagedType.U1)] bool val);

		// Token: 0x0200056B RID: 1387
		// (Invoke) Token: 0x06001AAF RID: 6831
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetCrashOnWarningsDelegate([MarshalAs(UnmanagedType.U1)] bool val);

		// Token: 0x0200056C RID: 1388
		// (Invoke) Token: 0x06001AB3 RID: 6835
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetCrashReportCustomStackDelegate(byte[] customStack);

		// Token: 0x0200056D RID: 1389
		// (Invoke) Token: 0x06001AB7 RID: 6839
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetCrashReportCustomStringDelegate(byte[] customString);

		// Token: 0x0200056E RID: 1390
		// (Invoke) Token: 0x06001ABB RID: 6843
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetDisableDumpGenerationDelegate([MarshalAs(UnmanagedType.U1)] bool value);

		// Token: 0x0200056F RID: 1391
		// (Invoke) Token: 0x06001ABF RID: 6847
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetDumpFolderPathDelegate(byte[] path);

		// Token: 0x02000570 RID: 1392
		// (Invoke) Token: 0x06001AC3 RID: 6851
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetFixedDtDelegate([MarshalAs(UnmanagedType.U1)] bool enabled, float dt);

		// Token: 0x02000571 RID: 1393
		// (Invoke) Token: 0x06001AC7 RID: 6855
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetForceDrawEntityIDDelegate([MarshalAs(UnmanagedType.U1)] bool value);

		// Token: 0x02000572 RID: 1394
		// (Invoke) Token: 0x06001ACB RID: 6859
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetForceVsyncDelegate([MarshalAs(UnmanagedType.U1)] bool value);

		// Token: 0x02000573 RID: 1395
		// (Invoke) Token: 0x06001ACF RID: 6863
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetFrameLimiterWithSleepDelegate([MarshalAs(UnmanagedType.U1)] bool value);

		// Token: 0x02000574 RID: 1396
		// (Invoke) Token: 0x06001AD3 RID: 6867
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetGraphicsPresetDelegate(int preset);

		// Token: 0x02000575 RID: 1397
		// (Invoke) Token: 0x06001AD7 RID: 6871
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetLoadingScreenPercentageDelegate(float value);

		// Token: 0x02000576 RID: 1398
		// (Invoke) Token: 0x06001ADB RID: 6875
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetMessageLineRenderingStateDelegate([MarshalAs(UnmanagedType.U1)] bool value);

		// Token: 0x02000577 RID: 1399
		// (Invoke) Token: 0x06001ADF RID: 6879
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetPrintCallstackAtCrahsesDelegate([MarshalAs(UnmanagedType.U1)] bool value);

		// Token: 0x02000578 RID: 1400
		// (Invoke) Token: 0x06001AE3 RID: 6883
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetRenderAgentsDelegate([MarshalAs(UnmanagedType.U1)] bool value);

		// Token: 0x02000579 RID: 1401
		// (Invoke) Token: 0x06001AE7 RID: 6887
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetRenderModeDelegate(int mode);

		// Token: 0x0200057A RID: 1402
		// (Invoke) Token: 0x06001AEB RID: 6891
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetReportModeDelegate([MarshalAs(UnmanagedType.U1)] bool reportMode);

		// Token: 0x0200057B RID: 1403
		// (Invoke) Token: 0x06001AEF RID: 6895
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetScreenTextRenderingStateDelegate([MarshalAs(UnmanagedType.U1)] bool value);

		// Token: 0x0200057C RID: 1404
		// (Invoke) Token: 0x06001AF3 RID: 6899
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetWatchdogValueDelegate(byte[] fileName, byte[] groupName, byte[] key, byte[] value);

		// Token: 0x0200057D RID: 1405
		// (Invoke) Token: 0x06001AF7 RID: 6903
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetWindowTitleDelegate(byte[] title);

		// Token: 0x0200057E RID: 1406
		// (Invoke) Token: 0x06001AFB RID: 6907
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void StartScenePerformanceReportDelegate(byte[] folderPath);

		// Token: 0x0200057F RID: 1407
		// (Invoke) Token: 0x06001AFF RID: 6911
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void TakeScreenshotFromPlatformPathDelegate(PlatformFilePath path);

		// Token: 0x02000580 RID: 1408
		// (Invoke) Token: 0x06001B03 RID: 6915
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void TakeScreenshotFromStringPathDelegate(byte[] path);

		// Token: 0x02000581 RID: 1409
		// (Invoke) Token: 0x06001B07 RID: 6919
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int TakeSSFromTopDelegate(byte[] file_name);

		// Token: 0x02000582 RID: 1410
		// (Invoke) Token: 0x06001B0B RID: 6923
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ToggleRenderDelegate();
	}
}
