using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Xml;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000094 RID: 148
	public static class Utilities
	{
		// Token: 0x06000AF5 RID: 2805 RVA: 0x0000C0D8 File Offset: 0x0000A2D8
		public static void ConstructMainThreadJob(Delegate function, params object[] parameters)
		{
			Utilities.MainThreadJob item = new Utilities.MainThreadJob(function, parameters);
			Utilities.jobs.Enqueue(item);
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x0000C0F8 File Offset: 0x0000A2F8
		public static void ConstructMainThreadJob(Semaphore semaphore, Delegate function, params object[] parameters)
		{
			Utilities.MainThreadJob item = new Utilities.MainThreadJob(semaphore, function, parameters);
			Utilities.jobs.Enqueue(item);
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x0000C11C File Offset: 0x0000A31C
		public static void RunJobs()
		{
			Utilities.MainThreadJob mainThreadJob;
			while (Utilities.jobs.TryDequeue(out mainThreadJob))
			{
				mainThreadJob.Invoke();
			}
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x0000C13F File Offset: 0x0000A33F
		public static void WaitJobs()
		{
			while (!Utilities.jobs.IsEmpty)
			{
			}
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x0000C14D File Offset: 0x0000A34D
		public static void OutputBenchmarkValuesToPerformanceReporter()
		{
			EngineApplicationInterface.IUtil.OutputBenchmarkValuesToPerformanceReporter();
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x0000C159 File Offset: 0x0000A359
		public static void SetLoadingScreenPercentage(float value)
		{
			EngineApplicationInterface.IUtil.SetLoadingScreenPercentage(value);
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x0000C166 File Offset: 0x0000A366
		public static void SetFixedDt(bool enabled, float dt)
		{
			EngineApplicationInterface.IUtil.SetFixedDt(enabled, dt);
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x0000C174 File Offset: 0x0000A374
		public static void SetBenchmarkStatus(int status, string def)
		{
			EngineApplicationInterface.IUtil.SetBenchmarkStatus(status, def);
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x0000C182 File Offset: 0x0000A382
		public static int GetBenchmarkStatus()
		{
			return EngineApplicationInterface.IUtil.GetBenchmarkStatus();
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x0000C18E File Offset: 0x0000A38E
		public static string GetApplicationMemoryStatistics()
		{
			return EngineApplicationInterface.IUtil.GetApplicationMemoryStatistics();
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0000C19A File Offset: 0x0000A39A
		public static bool IsBenchmarkQuited()
		{
			return EngineApplicationInterface.IUtil.IsBenchmarkQuited();
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x0000C1A6 File Offset: 0x0000A3A6
		public static string GetNativeMemoryStatistics()
		{
			return EngineApplicationInterface.IUtil.GetNativeMemoryStatistics();
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x0000C1B2 File Offset: 0x0000A3B2
		public static bool CommandLineArgumentExists(string str)
		{
			return EngineApplicationInterface.IUtil.CommandLineArgumentExists(str);
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x0000C1BF File Offset: 0x0000A3BF
		public static string GetConsoleHostMachine()
		{
			return EngineApplicationInterface.IUtil.GetConsoleHostMachine();
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x0000C1CB File Offset: 0x0000A3CB
		public static string ExportNavMeshFaceMarks(string file_name)
		{
			return EngineApplicationInterface.IUtil.ExportNavMeshFaceMarks(file_name);
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x0000C1D8 File Offset: 0x0000A3D8
		public static string TakeSSFromTop(string file_name)
		{
			return EngineApplicationInterface.IUtil.TakeSSFromTop(file_name);
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x0000C1E5 File Offset: 0x0000A3E5
		public static void CheckIfAssetsAndSourcesAreSame()
		{
			EngineApplicationInterface.IUtil.CheckIfAssetsAndSourcesAreSame();
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x0000C1F1 File Offset: 0x0000A3F1
		public static void DisableCoreGame()
		{
			EngineApplicationInterface.IUtil.DisableCoreGame();
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x0000C1FD File Offset: 0x0000A3FD
		public static float GetApplicationMemory()
		{
			return EngineApplicationInterface.IUtil.GetApplicationMemory();
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x0000C209 File Offset: 0x0000A409
		public static void GatherCoreGameReferences(string scene_names)
		{
			EngineApplicationInterface.IUtil.GatherCoreGameReferences(scene_names);
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x0000C216 File Offset: 0x0000A416
		public static bool IsOnlyCoreContentEnabled()
		{
			return EngineApplicationInterface.IUtil.GetCoreGameState() != 0;
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x0000C225 File Offset: 0x0000A425
		public static void FindMeshesWithoutLods(string module_name)
		{
			EngineApplicationInterface.IUtil.FindMeshesWithoutLods(module_name);
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x0000C232 File Offset: 0x0000A432
		public static void SetDisableDumpGeneration(bool value)
		{
			EngineApplicationInterface.IUtil.SetDisableDumpGeneration(value);
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x0000C23F File Offset: 0x0000A43F
		public static void SetPrintCallstackAtCrahses(bool value)
		{
			EngineApplicationInterface.IUtil.SetPrintCallstackAtCrahses(value);
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x0000C24C File Offset: 0x0000A44C
		public static string[] GetModulesNames()
		{
			return EngineApplicationInterface.IUtil.GetModulesCode().Split(new char[]
			{
				'*'
			});
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x0000C268 File Offset: 0x0000A468
		public static string GetFullModulePath(string moduleName)
		{
			return EngineApplicationInterface.IUtil.GetFullModulePath(moduleName);
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x0000C275 File Offset: 0x0000A475
		public static string[] GetFullModulePaths()
		{
			return EngineApplicationInterface.IUtil.GetFullModulePaths().Split(new char[]
			{
				'*'
			});
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x0000C291 File Offset: 0x0000A491
		public static string GetFullFilePathOfScene(string sceneName)
		{
			string fullFilePathOfScene = EngineApplicationInterface.IUtil.GetFullFilePathOfScene(sceneName);
			if (fullFilePathOfScene == "SCENE_NOT_FOUND")
			{
				throw new Exception("Scene '" + sceneName + "' was not found!");
			}
			return fullFilePathOfScene.Replace("$BASE/", Utilities.GetBasePath());
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x0000C2D0 File Offset: 0x0000A4D0
		public static bool TryGetFullFilePathOfScene(string sceneName, out string fullPath)
		{
			bool result;
			try
			{
				fullPath = Utilities.GetFullFilePathOfScene(sceneName);
				result = true;
			}
			catch (Exception)
			{
				fullPath = null;
				result = false;
			}
			return result;
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x0000C304 File Offset: 0x0000A504
		public static bool TryGetUniqueIdentifiersForScene(string sceneName, out UniqueSceneId identifiers)
		{
			identifiers = null;
			string xsceneFilePath;
			return Utilities.TryGetFullFilePathOfScene(sceneName, out xsceneFilePath) && Utilities.TryGetUniqueIdentifiersForSceneFile(xsceneFilePath, out identifiers);
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x0000C328 File Offset: 0x0000A528
		public static bool TryGetUniqueIdentifiersForSceneFile(string xsceneFilePath, out UniqueSceneId identifiers)
		{
			identifiers = null;
			using (XmlReader xmlReader = XmlReader.Create(xsceneFilePath))
			{
				string attribute;
				string attribute2;
				if (xmlReader.MoveToContent() == XmlNodeType.Element && xmlReader.Name == "scene" && (attribute = xmlReader.GetAttribute("unique_token")) != null && (attribute2 = xmlReader.GetAttribute("revision")) != null)
				{
					identifiers = new UniqueSceneId(attribute, attribute2);
				}
			}
			return identifiers != null;
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x0000C3A4 File Offset: 0x0000A5A4
		public static void PairSceneNameToModuleName(string sceneName, string moduleName)
		{
			EngineApplicationInterface.IUtil.PairSceneNameToModuleName(sceneName, moduleName);
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x0000C3B2 File Offset: 0x0000A5B2
		public static string[] GetSingleModuleScenesOfModule(string moduleName)
		{
			return EngineApplicationInterface.IUtil.GetSingleModuleScenesOfModule(moduleName).Split(new char[]
			{
				'*'
			});
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x0000C3CF File Offset: 0x0000A5CF
		public static string GetFullCommandLineString()
		{
			return EngineApplicationInterface.IUtil.GetFullCommandLineString();
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x0000C3DB File Offset: 0x0000A5DB
		public static void SetScreenTextRenderingState(bool state)
		{
			EngineApplicationInterface.IUtil.SetScreenTextRenderingState(state);
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x0000C3E8 File Offset: 0x0000A5E8
		public static void SetMessageLineRenderingState(bool state)
		{
			EngineApplicationInterface.IUtil.SetMessageLineRenderingState(state);
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x0000C3F5 File Offset: 0x0000A5F5
		public static bool CheckIfTerrainShaderHeaderGenerationFinished()
		{
			return EngineApplicationInterface.IUtil.CheckIfTerrainShaderHeaderGenerationFinished();
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x0000C401 File Offset: 0x0000A601
		public static void GenerateTerrainShaderHeaders(string targetPlatform, string targetConfig, string output_path)
		{
			EngineApplicationInterface.IUtil.GenerateTerrainShaderHeaders(targetPlatform, targetConfig, output_path);
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x0000C410 File Offset: 0x0000A610
		public static void CompileTerrainShadersDist(string targetPlatform, string targetConfig, string output_path)
		{
			EngineApplicationInterface.IUtil.CompileTerrainShadersDist(targetPlatform, targetConfig, output_path);
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x0000C41F File Offset: 0x0000A61F
		public static void SetCrashOnAsserts(bool val)
		{
			EngineApplicationInterface.IUtil.SetCrashOnAsserts(val);
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x0000C42C File Offset: 0x0000A62C
		public static void SetCrashOnWarnings(bool val)
		{
			EngineApplicationInterface.IUtil.SetCrashOnWarnings(val);
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x0000C439 File Offset: 0x0000A639
		public static void ToggleRender()
		{
			EngineApplicationInterface.IUtil.ToggleRender();
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x0000C445 File Offset: 0x0000A645
		public static void SetRenderAgents(bool value)
		{
			EngineApplicationInterface.IUtil.SetRenderAgents(value);
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x0000C452 File Offset: 0x0000A652
		public static bool CheckShaderCompilation()
		{
			return EngineApplicationInterface.IUtil.CheckShaderCompilation();
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x0000C45E File Offset: 0x0000A65E
		public static void CompileAllShaders(string targetPlatform)
		{
			EngineApplicationInterface.IUtil.CompileAllShaders(targetPlatform);
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x0000C46B File Offset: 0x0000A66B
		public static string GetExecutableWorkingDirectory()
		{
			return EngineApplicationInterface.IUtil.GetExecutableWorkingDirectory();
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x0000C477 File Offset: 0x0000A677
		public static void SetDumpFolderPath(string path)
		{
			EngineApplicationInterface.IUtil.SetDumpFolderPath(path);
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x0000C484 File Offset: 0x0000A684
		public static void CheckSceneForProblems(string sceneName)
		{
			EngineApplicationInterface.IUtil.CheckSceneForProblems(sceneName);
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x0000C491 File Offset: 0x0000A691
		public static void SetCoreGameState(int state)
		{
			EngineApplicationInterface.IUtil.SetCoreGameState(state);
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x0000C49E File Offset: 0x0000A69E
		public static int GetCoreGameState()
		{
			return EngineApplicationInterface.IUtil.GetCoreGameState();
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x0000C4AA File Offset: 0x0000A6AA
		public static string ExecuteCommandLineCommand(string command)
		{
			return EngineApplicationInterface.IUtil.ExecuteCommandLineCommand(command);
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x0000C4B7 File Offset: 0x0000A6B7
		public static void QuitGame()
		{
			EngineApplicationInterface.IUtil.QuitGame();
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x0000C4C3 File Offset: 0x0000A6C3
		public static void ExitProcess(int exitCode)
		{
			EngineApplicationInterface.IUtil.ExitProcess(exitCode);
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x0000C4D0 File Offset: 0x0000A6D0
		public static string GetBasePath()
		{
			return EngineApplicationInterface.IUtil.GetBaseDirectory();
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x0000C4DC File Offset: 0x0000A6DC
		public static string GetVisualTestsValidatePath()
		{
			return EngineApplicationInterface.IUtil.GetVisualTestsValidatePath();
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x0000C4E8 File Offset: 0x0000A6E8
		public static string GetVisualTestsTestFilesPath()
		{
			return EngineApplicationInterface.IUtil.GetVisualTestsTestFilesPath();
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x0000C4F4 File Offset: 0x0000A6F4
		public static string GetAttachmentsPath()
		{
			return EngineApplicationInterface.IUtil.GetAttachmentsPath();
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x0000C500 File Offset: 0x0000A700
		public static void StartScenePerformanceReport(string folderPath)
		{
			EngineApplicationInterface.IUtil.StartScenePerformanceReport(folderPath);
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x0000C50D File Offset: 0x0000A70D
		public static bool IsSceneReportFinished()
		{
			return EngineApplicationInterface.IUtil.IsSceneReportFinished();
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x0000C519 File Offset: 0x0000A719
		public static float GetFps()
		{
			return EngineApplicationInterface.IUtil.GetFps();
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x0000C525 File Offset: 0x0000A725
		public static float GetMainFps()
		{
			return EngineApplicationInterface.IUtil.GetMainFps();
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0000C531 File Offset: 0x0000A731
		public static float GetRendererFps()
		{
			return EngineApplicationInterface.IUtil.GetRendererFps();
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x0000C53D File Offset: 0x0000A73D
		public static void EnableSingleGPUQueryPerFrame()
		{
			EngineApplicationInterface.IUtil.EnableSingleGPUQueryPerFrame();
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x0000C549 File Offset: 0x0000A749
		public static void ClearDecalAtlas(DecalAtlasGroup atlasGroup)
		{
			EngineApplicationInterface.IUtil.clear_decal_atlas(atlasGroup);
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x0000C556 File Offset: 0x0000A756
		public static void FlushManagedObjectsMemory()
		{
			Common.MemoryCleanupGC(false);
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x0000C55E File Offset: 0x0000A75E
		public static void OnLoadingWindowEnabled()
		{
			EngineApplicationInterface.IUtil.OnLoadingWindowEnabled();
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x0000C56A File Offset: 0x0000A76A
		public static void DebugSetGlobalLoadingWindowState(bool newState)
		{
			EngineApplicationInterface.IUtil.DebugSetGlobalLoadingWindowState(newState);
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x0000C577 File Offset: 0x0000A777
		public static void OnLoadingWindowDisabled()
		{
			EngineApplicationInterface.IUtil.OnLoadingWindowDisabled();
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x0000C583 File Offset: 0x0000A783
		public static void DisableGlobalLoadingWindow()
		{
			EngineApplicationInterface.IUtil.DisableGlobalLoadingWindow();
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x0000C58F File Offset: 0x0000A78F
		public static void EnableGlobalLoadingWindow()
		{
			EngineApplicationInterface.IUtil.EnableGlobalLoadingWindow();
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x0000C59B File Offset: 0x0000A79B
		public static void EnableGlobalEditDataCacher()
		{
			EngineApplicationInterface.IUtil.EnableGlobalEditDataCacher();
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x0000C5A7 File Offset: 0x0000A7A7
		public static void DoFullBakeAllLevelsAutomated(string module, string scene)
		{
			EngineApplicationInterface.IUtil.DoFullBakeAllLevelsAutomated(module, scene);
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x0000C5B5 File Offset: 0x0000A7B5
		public static int GetReturnCode()
		{
			return EngineApplicationInterface.IUtil.GetReturnCode();
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x0000C5C1 File Offset: 0x0000A7C1
		public static void DisableGlobalEditDataCacher()
		{
			EngineApplicationInterface.IUtil.DisableGlobalEditDataCacher();
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x0000C5CD File Offset: 0x0000A7CD
		public static void DoFullBakeSingleLevelAutomated(string module, string scene)
		{
			EngineApplicationInterface.IUtil.DoFullBakeSingleLevelAutomated(module, scene);
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x0000C5DB File Offset: 0x0000A7DB
		public static void DoLightOnlyBakeSingleLevelAutomated(string module, string scene)
		{
			EngineApplicationInterface.IUtil.DoLightOnlyBakeSingleLevelAutomated(module, scene);
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x0000C5E9 File Offset: 0x0000A7E9
		public static void DoLightOnlyBakeAllLevelsAutomated(string module, string scene)
		{
			EngineApplicationInterface.IUtil.DoLightOnlyBakeAllLevelsAutomated(module, scene);
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x0000C5F7 File Offset: 0x0000A7F7
		public static bool DidAutomatedGIBakeFinished()
		{
			return EngineApplicationInterface.IUtil.DidAutomatedGIBakeFinished();
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x0000C604 File Offset: 0x0000A804
		public static void GetSelectedEntities(ref List<GameEntity> gameEntities)
		{
			int editorSelectedEntityCount = EngineApplicationInterface.IUtil.GetEditorSelectedEntityCount();
			UIntPtr[] array = new UIntPtr[editorSelectedEntityCount];
			EngineApplicationInterface.IUtil.GetEditorSelectedEntities(array);
			for (int i = 0; i < editorSelectedEntityCount; i++)
			{
				gameEntities.Add(new GameEntity(array[i]));
			}
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x0000C64C File Offset: 0x0000A84C
		public static void DeleteEntitiesInEditorScene(List<GameEntity> gameEntities)
		{
			int count = gameEntities.Count;
			UIntPtr[] array = new UIntPtr[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = gameEntities[i].Pointer;
			}
			EngineApplicationInterface.IUtil.DeleteEntitiesInEditorScene(array, count);
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x0000C690 File Offset: 0x0000A890
		public static void CreateSelectionInEditor(List<GameEntity> gameEntities, string name)
		{
			int count = gameEntities.Count;
			UIntPtr[] array = new UIntPtr[gameEntities.Count];
			for (int i = 0; i < count; i++)
			{
				array[i] = gameEntities[i].Pointer;
			}
			EngineApplicationInterface.IUtil.CreateSelectionInEditor(array, count, name);
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x0000C6D8 File Offset: 0x0000A8D8
		public static void SelectEntities(List<GameEntity> gameEntities)
		{
			int count = gameEntities.Count;
			UIntPtr[] array = new UIntPtr[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = gameEntities[i].Pointer;
			}
			EngineApplicationInterface.IUtil.SelectEntities(array, count);
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x0000C71C File Offset: 0x0000A91C
		public static void GetEntitiesOfSelectionSet(string selectionSetName, ref List<GameEntity> gameEntities)
		{
			int entityCountOfSelectionSet = EngineApplicationInterface.IUtil.GetEntityCountOfSelectionSet(selectionSetName);
			UIntPtr[] array = new UIntPtr[entityCountOfSelectionSet];
			EngineApplicationInterface.IUtil.GetEntitiesOfSelectionSet(selectionSetName, array);
			for (int i = 0; i < entityCountOfSelectionSet; i++)
			{
				gameEntities.Add(new GameEntity(array[i]));
			}
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x0000C763 File Offset: 0x0000A963
		public static void AddCommandLineFunction(string concatName)
		{
			EngineApplicationInterface.IUtil.AddCommandLineFunction(concatName);
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x0000C770 File Offset: 0x0000A970
		public static int GetNumberOfShaderCompilationsInProgress()
		{
			return EngineApplicationInterface.IUtil.GetNumberOfShaderCompilationsInProgress();
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x0000C77C File Offset: 0x0000A97C
		public static int IsDetailedSoundLogOn()
		{
			return EngineApplicationInterface.IUtil.IsDetailedSoundLogOn();
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x0000C788 File Offset: 0x0000A988
		public static ulong GetCurrentCpuMemoryUsageMB()
		{
			return EngineApplicationInterface.IUtil.GetCurrentCpuMemoryUsage();
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x0000C794 File Offset: 0x0000A994
		public static ulong GetGpuMemoryOfAllocationGroup(string name)
		{
			return EngineApplicationInterface.IUtil.GetGpuMemoryOfAllocationGroup(name);
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x0000C7A1 File Offset: 0x0000A9A1
		public static void GetGPUMemoryStats(ref float totalMemory, ref float renderTargetMemory, ref float depthTargetMemory, ref float srvMemory, ref float bufferMemory)
		{
			EngineApplicationInterface.IUtil.GetGPUMemoryStats(ref totalMemory, ref renderTargetMemory, ref depthTargetMemory, ref srvMemory, ref bufferMemory);
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x0000C7B3 File Offset: 0x0000A9B3
		public static void GetDetailedGPUMemoryData(ref int totalMemoryAllocated, ref int totalMemoryUsed, ref int emptyChunkTotalSize)
		{
			EngineApplicationInterface.IUtil.GetDetailedGPUBufferMemoryStats(ref totalMemoryAllocated, ref totalMemoryUsed, ref emptyChunkTotalSize);
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x0000C7C2 File Offset: 0x0000A9C2
		public static void SetRenderMode(Utilities.EngineRenderDisplayMode mode)
		{
			EngineApplicationInterface.IUtil.SetRenderMode((int)mode);
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x0000C7CF File Offset: 0x0000A9CF
		public static void SetForceDrawEntityID(bool value)
		{
			EngineApplicationInterface.IUtil.SetForceDrawEntityID(value);
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x0000C7DC File Offset: 0x0000A9DC
		public static void AddPerformanceReportToken(string performance_type, string name, float loading_time)
		{
			EngineApplicationInterface.IUtil.AddPerformanceReportToken(performance_type, name, loading_time);
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x0000C7EB File Offset: 0x0000A9EB
		public static void AddSceneObjectReport(string scene_name, string report_name, float report_value)
		{
			EngineApplicationInterface.IUtil.AddSceneObjectReport(scene_name, report_name, report_value);
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x0000C7FA File Offset: 0x0000A9FA
		public static void OutputPerformanceReports()
		{
			EngineApplicationInterface.IUtil.OutputPerformanceReports();
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000B54 RID: 2900 RVA: 0x0000C806 File Offset: 0x0000AA06
		public static int EngineFrameNo
		{
			get
			{
				return EngineApplicationInterface.IUtil.GetEngineFrameNo();
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000B55 RID: 2901 RVA: 0x0000C812 File Offset: 0x0000AA12
		public static bool EditModeEnabled
		{
			get
			{
				return EngineApplicationInterface.IUtil.IsEditModeEnabled();
			}
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x0000C81E File Offset: 0x0000AA1E
		public static void TakeScreenshot(PlatformFilePath path)
		{
			EngineApplicationInterface.IUtil.TakeScreenshotFromPlatformPath(path);
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x0000C82B File Offset: 0x0000AA2B
		public static void TakeScreenshot(string path)
		{
			EngineApplicationInterface.IUtil.TakeScreenshotFromStringPath(path);
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x0000C838 File Offset: 0x0000AA38
		public static void SetAllocationAlwaysValidScene(Scene scene)
		{
			EngineApplicationInterface.IUtil.SetAllocationAlwaysValidScene((scene != null) ? scene.Pointer : UIntPtr.Zero);
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x0000C85A File Offset: 0x0000AA5A
		public static void CheckResourceModifications()
		{
			EngineApplicationInterface.IUtil.CheckResourceModifications();
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x0000C866 File Offset: 0x0000AA66
		public static void SetGraphicsPreset(int preset)
		{
			EngineApplicationInterface.IUtil.SetGraphicsPreset(preset);
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x0000C873 File Offset: 0x0000AA73
		public static string GetLocalOutputPath()
		{
			return EngineApplicationInterface.IUtil.GetLocalOutputPath();
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x0000C87F File Offset: 0x0000AA7F
		public static string GetPCInfo()
		{
			return EngineApplicationInterface.IUtil.GetPCInfo();
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x0000C88B File Offset: 0x0000AA8B
		public static int GetGPUMemoryMB()
		{
			return EngineApplicationInterface.IUtil.GetGPUMemoryMB();
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x0000C897 File Offset: 0x0000AA97
		public static int GetCurrentEstimatedGPUMemoryCostMB()
		{
			return EngineApplicationInterface.IUtil.GetCurrentEstimatedGPUMemoryCostMB();
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0000C8A3 File Offset: 0x0000AAA3
		public static void DumpGPUMemoryStatistics(string filePath)
		{
			EngineApplicationInterface.IUtil.DumpGPUMemoryStatistics(filePath);
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x0000C8B0 File Offset: 0x0000AAB0
		public static int SaveDataAsTexture(string path, int width, int height, float[] data)
		{
			return EngineApplicationInterface.IUtil.SaveDataAsTexture(path, width, height, data);
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x0000C8C0 File Offset: 0x0000AAC0
		public static void ClearOldResourcesAndObjects()
		{
			EngineApplicationInterface.IUtil.ClearOldResourcesAndObjects();
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x0000C8CC File Offset: 0x0000AACC
		public static void LoadVirtualTextureTileset(string name)
		{
			EngineApplicationInterface.IUtil.LoadVirtualTextureTileset(name);
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x0000C8D9 File Offset: 0x0000AAD9
		public static float GetDeltaTime(int timerId)
		{
			return EngineApplicationInterface.IUtil.GetDeltaTime(timerId);
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x0000C8E6 File Offset: 0x0000AAE6
		public static void LoadSkyBoxes()
		{
			EngineApplicationInterface.IUtil.LoadSkyBoxes();
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x0000C8F2 File Offset: 0x0000AAF2
		public static string GetApplicationName()
		{
			return EngineApplicationInterface.IUtil.GetApplicationName();
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x0000C8FE File Offset: 0x0000AAFE
		public static void SetWindowTitle(string title)
		{
			EngineApplicationInterface.IUtil.SetWindowTitle(title);
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x0000C90B File Offset: 0x0000AB0B
		public static string ProcessWindowTitle(string title)
		{
			return EngineApplicationInterface.IUtil.ProcessWindowTitle(title);
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x0000C918 File Offset: 0x0000AB18
		public static uint GetCurrentProcessID()
		{
			return EngineApplicationInterface.IUtil.GetCurrentProcessID();
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x0000C924 File Offset: 0x0000AB24
		public static void DoDelayedexit(int returnCode)
		{
			EngineApplicationInterface.IUtil.DoDelayedexit(returnCode);
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x0000C931 File Offset: 0x0000AB31
		public static void SetAssertionsAndWarningsSetExitCode(bool value)
		{
			EngineApplicationInterface.IUtil.SetAssertionsAndWarningsSetExitCode(value);
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x0000C93E File Offset: 0x0000AB3E
		public static void SetReportMode(bool reportMode)
		{
			EngineApplicationInterface.IUtil.SetReportMode(reportMode);
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x0000C94B File Offset: 0x0000AB4B
		public static void SetAssertionAtShaderCompile(bool value)
		{
			EngineApplicationInterface.IUtil.SetAssertionAtShaderCompile(value);
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x0000C958 File Offset: 0x0000AB58
		public static void SetCrashReportCustomString(string customString)
		{
			EngineApplicationInterface.IUtil.SetCrashReportCustomString(customString);
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x0000C965 File Offset: 0x0000AB65
		public static void SetCrashReportCustomStack(string customStack)
		{
			EngineApplicationInterface.IUtil.SetCrashReportCustomStack(customStack);
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x0000C972 File Offset: 0x0000AB72
		public static int GetSteamAppId()
		{
			return EngineApplicationInterface.IUtil.GetSteamAppId();
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x0000C97E File Offset: 0x0000AB7E
		public static void SetForceVsync(bool value)
		{
			Debug.Print("Force VSync State is now " + (value ? "ACTIVE" : "DEACTIVATED"), 0, Debug.DebugColor.DarkBlue, 17592186044416UL);
			EngineApplicationInterface.IUtil.SetForceVsync(value);
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000B71 RID: 2929 RVA: 0x0000C9B4 File Offset: 0x0000ABB4
		private static PlatformFilePath DefaultBannerlordConfigFullPath
		{
			get
			{
				return new PlatformFilePath(EngineFilePaths.ConfigsPath, "BannerlordConfig.txt");
			}
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x0000C9C8 File Offset: 0x0000ABC8
		public static string LoadBannerlordConfigFile()
		{
			PlatformFilePath defaultBannerlordConfigFullPath = Utilities.DefaultBannerlordConfigFullPath;
			if (!FileHelper.FileExists(defaultBannerlordConfigFullPath))
			{
				return "";
			}
			return FileHelper.GetFileContentString(defaultBannerlordConfigFullPath);
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x0000C9F0 File Offset: 0x0000ABF0
		public static SaveResult SaveConfigFile(string configProperties)
		{
			PlatformFilePath defaultBannerlordConfigFullPath = Utilities.DefaultBannerlordConfigFullPath;
			SaveResult result;
			try
			{
				string data = configProperties.Substring(0, configProperties.Length - 1);
				FileHelper.SaveFileString(defaultBannerlordConfigFullPath, data);
				result = SaveResult.Success;
			}
			catch
			{
				Debug.Print("Could not create Bannerlord Config file", 0, Debug.DebugColor.White, 17592186044416UL);
				result = SaveResult.ConfigFileFailure;
			}
			return result;
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x0000CA4C File Offset: 0x0000AC4C
		public static void OpenOnscreenKeyboard(string initialText, string descriptionText, int maxLength, int keyboardTypeEnum)
		{
			EngineApplicationInterface.IUtil.OpenOnscreenKeyboard(initialText, descriptionText, maxLength, keyboardTypeEnum);
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x0000CA5C File Offset: 0x0000AC5C
		public static string GetSystemLanguage()
		{
			return EngineApplicationInterface.IUtil.GetSystemLanguage();
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x0000CA68 File Offset: 0x0000AC68
		public static int RegisterGPUAllocationGroup(string name)
		{
			return EngineApplicationInterface.IUtil.RegisterGPUAllocationGroup(name);
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x0000CA75 File Offset: 0x0000AC75
		public static int GetMemoryUsageOfCategory(int category)
		{
			return EngineApplicationInterface.IUtil.GetMemoryUsageOfCategory(category);
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x0000CA82 File Offset: 0x0000AC82
		public static string GetDetailedXBOXMemoryInfo()
		{
			return EngineApplicationInterface.IUtil.GetDetailedXBOXMemoryInfo();
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0000CA8E File Offset: 0x0000AC8E
		public static void SetFrameLimiterWithSleep(bool value)
		{
			EngineApplicationInterface.IUtil.SetFrameLimiterWithSleep(value);
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0000CA9B File Offset: 0x0000AC9B
		public static bool GetFrameLimiterWithSleep()
		{
			return EngineApplicationInterface.IUtil.GetFrameLimiterWithSleep();
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0000CAA7 File Offset: 0x0000ACA7
		public static int GetVertexBufferChunkSystemMemoryUsage()
		{
			return EngineApplicationInterface.IUtil.GetVertexBufferChunkSystemMemoryUsage();
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x0000CAB3 File Offset: 0x0000ACB3
		public static int GetBuildNumber()
		{
			return EngineApplicationInterface.IUtil.GetBuildNumber();
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0000CABF File Offset: 0x0000ACBF
		public static ApplicationVersion GetApplicationVersionWithBuildNumber()
		{
			return ApplicationVersion.FromParametersFile(null);
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x0000CAC7 File Offset: 0x0000ACC7
		public static void ParallelFor(int startIndex, int endIndex, long curKey, int grainSize)
		{
			EngineApplicationInterface.IUtil.ManagedParallelFor(startIndex, endIndex, curKey, grainSize);
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x0000CAD7 File Offset: 0x0000ACD7
		public static void ClearShaderMemory()
		{
			EngineApplicationInterface.IUtil.ClearShaderMemory();
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x0000CAE3 File Offset: 0x0000ACE3
		public static void RegisterMeshForGPUMorph(string metaMeshName)
		{
			EngineApplicationInterface.IUtil.RegisterMeshForGPUMorph(metaMeshName);
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0000CAF0 File Offset: 0x0000ACF0
		public static void ParallelForWithDt(int startIndex, int endIndex, long curKey, int grainSize)
		{
			EngineApplicationInterface.IUtil.ManagedParallelForWithDt(startIndex, endIndex, curKey, grainSize);
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x0000CB00 File Offset: 0x0000AD00
		public static ulong GetMainThreadId()
		{
			return EngineApplicationInterface.IUtil.GetMainThreadId();
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x0000CB0C File Offset: 0x0000AD0C
		public static ulong GetCurrentThreadId()
		{
			return EngineApplicationInterface.IUtil.GetCurrentThreadId();
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0000CB18 File Offset: 0x0000AD18
		public static void SetWatchdogValue(string fileName, string groupName, string key, string value)
		{
			EngineApplicationInterface.IUtil.SetWatchdogValue(fileName, groupName, key, value);
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0000CB28 File Offset: 0x0000AD28
		public static void DetachWatchdog()
		{
			EngineApplicationInterface.IUtil.DetachWatchdog();
		}

		// Token: 0x040001E9 RID: 489
		private static ConcurrentQueue<Utilities.MainThreadJob> jobs = new ConcurrentQueue<Utilities.MainThreadJob>();

		// Token: 0x040001EA RID: 490
		public static bool renderingActive = true;

		// Token: 0x020000C9 RID: 201
		public enum EngineRenderDisplayMode
		{
			// Token: 0x0400041A RID: 1050
			ShowNone,
			// Token: 0x0400041B RID: 1051
			ShowAlbedo,
			// Token: 0x0400041C RID: 1052
			ShowNormals,
			// Token: 0x0400041D RID: 1053
			ShowVertexNormals,
			// Token: 0x0400041E RID: 1054
			ShowSpecular,
			// Token: 0x0400041F RID: 1055
			ShowGloss,
			// Token: 0x04000420 RID: 1056
			ShowOcclusion,
			// Token: 0x04000421 RID: 1057
			ShowGbufferShadowMask,
			// Token: 0x04000422 RID: 1058
			ShowTranslucency,
			// Token: 0x04000423 RID: 1059
			ShowMotionVector,
			// Token: 0x04000424 RID: 1060
			ShowVertexColor,
			// Token: 0x04000425 RID: 1061
			ShowDepth,
			// Token: 0x04000426 RID: 1062
			ShowTiledLightOverdraw,
			// Token: 0x04000427 RID: 1063
			ShowTiledDecalOverdraw,
			// Token: 0x04000428 RID: 1064
			ShowMeshId,
			// Token: 0x04000429 RID: 1065
			ShowDisableSunLighting,
			// Token: 0x0400042A RID: 1066
			ShowDebugTexture,
			// Token: 0x0400042B RID: 1067
			ShowTextureDensity,
			// Token: 0x0400042C RID: 1068
			ShowOverdraw,
			// Token: 0x0400042D RID: 1069
			ShowVsComplexity,
			// Token: 0x0400042E RID: 1070
			ShowPsComplexity,
			// Token: 0x0400042F RID: 1071
			ShowDisableAmbientLighting,
			// Token: 0x04000430 RID: 1072
			ShowEntityId,
			// Token: 0x04000431 RID: 1073
			ShowPrtDiffuseAmbient,
			// Token: 0x04000432 RID: 1074
			ShowLightDebugMode,
			// Token: 0x04000433 RID: 1075
			ShowParticleShadingAtlas,
			// Token: 0x04000434 RID: 1076
			ShowTerrainAngle,
			// Token: 0x04000435 RID: 1077
			ShowParallaxDebug,
			// Token: 0x04000436 RID: 1078
			ShowAlbedoValidation,
			// Token: 0x04000437 RID: 1079
			NumDebugModes
		}

		// Token: 0x020000CA RID: 202
		private class MainThreadJob
		{
			// Token: 0x06000CAD RID: 3245 RVA: 0x0000FDDB File Offset: 0x0000DFDB
			internal MainThreadJob(Delegate function, object[] parameters)
			{
				this._function = function;
				this._parameters = parameters;
				this.wait_handle = null;
			}

			// Token: 0x06000CAE RID: 3246 RVA: 0x0000FDF8 File Offset: 0x0000DFF8
			internal MainThreadJob(Semaphore sema, Delegate function, object[] parameters)
			{
				this._function = function;
				this._parameters = parameters;
				this.wait_handle = sema;
			}

			// Token: 0x06000CAF RID: 3247 RVA: 0x0000FE15 File Offset: 0x0000E015
			internal void Invoke()
			{
				this._function.DynamicInvoke(this._parameters);
				if (this.wait_handle != null)
				{
					this.wait_handle.Release();
				}
			}

			// Token: 0x04000438 RID: 1080
			private Delegate _function;

			// Token: 0x04000439 RID: 1081
			private object[] _parameters;

			// Token: 0x0400043A RID: 1082
			private Semaphore wait_handle;
		}

		// Token: 0x020000CB RID: 203
		public class MainThreadPerformanceQuery : IDisposable
		{
			// Token: 0x06000CB0 RID: 3248 RVA: 0x0000FE3D File Offset: 0x0000E03D
			public MainThreadPerformanceQuery(string parent, string name)
			{
				this._name = name;
				this._parent = parent;
				this._stopWatch = new Stopwatch();
				this._stopWatch.Start();
			}

			// Token: 0x06000CB1 RID: 3249 RVA: 0x0000FE6C File Offset: 0x0000E06C
			public void Dispose()
			{
				this._stopWatch.Stop();
				float num = (float)this._stopWatch.Elapsed.TotalMilliseconds;
				num /= 1000f;
				EngineApplicationInterface.IUtil.AddMainThreadPerformanceQuery(this._parent, this._name, num);
			}

			// Token: 0x0400043B RID: 1083
			private string _name;

			// Token: 0x0400043C RID: 1084
			private string _parent;

			// Token: 0x0400043D RID: 1085
			private Stopwatch _stopWatch;
		}
	}
}
