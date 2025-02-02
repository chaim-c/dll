using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x0200007D RID: 125
	[EngineClass("rglScene")]
	public sealed class Scene : NativeObject
	{
		// Token: 0x060008F2 RID: 2290 RVA: 0x000091F9 File Offset: 0x000073F9
		private Scene()
		{
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x00009201 File Offset: 0x00007401
		internal Scene(UIntPtr pointer)
		{
			base.Construct(pointer);
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x00009210 File Offset: 0x00007410
		public bool IsDefaultEditorScene()
		{
			return EngineApplicationInterface.IScene.IsDefaultEditorScene(this);
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x0000921D File Offset: 0x0000741D
		public bool IsMultiplayerScene()
		{
			return EngineApplicationInterface.IScene.IsMultiplayerScene(this);
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0000922A File Offset: 0x0000742A
		public string TakePhotoModePicture(bool saveAmbientOcclusionPass, bool savingObjectIdPass, bool saveShadowPass)
		{
			return EngineApplicationInterface.IScene.TakePhotoModePicture(this, saveAmbientOcclusionPass, savingObjectIdPass, saveShadowPass);
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x0000923A File Offset: 0x0000743A
		public string GetAllColorGradeNames()
		{
			return EngineApplicationInterface.IScene.GetAllColorGradeNames(this);
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x00009247 File Offset: 0x00007447
		public string GetAllFilterNames()
		{
			return EngineApplicationInterface.IScene.GetAllFilterNames(this);
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x00009254 File Offset: 0x00007454
		public float GetPhotoModeRoll()
		{
			return EngineApplicationInterface.IScene.GetPhotoModeRoll(this);
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x00009261 File Offset: 0x00007461
		public bool GetPhotoModeOrbit()
		{
			return EngineApplicationInterface.IScene.GetPhotoModeOrbit(this);
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0000926E File Offset: 0x0000746E
		public bool GetPhotoModeOn()
		{
			return EngineApplicationInterface.IScene.GetPhotoModeOn(this);
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x0000927B File Offset: 0x0000747B
		public void GetPhotoModeFocus(ref float focus, ref float focusStart, ref float focusEnd, ref float exposure, ref bool vignetteOn)
		{
			EngineApplicationInterface.IScene.GetPhotoModeFocus(this, ref focus, ref focusStart, ref focusEnd, ref exposure, ref vignetteOn);
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x0000928F File Offset: 0x0000748F
		public int GetSceneColorGradeIndex()
		{
			return EngineApplicationInterface.IScene.GetSceneColorGradeIndex(this);
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x0000929C File Offset: 0x0000749C
		public int GetSceneFilterIndex()
		{
			return EngineApplicationInterface.IScene.GetSceneFilterIndex(this);
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x000092A9 File Offset: 0x000074A9
		public string GetLoadingStateName()
		{
			return EngineApplicationInterface.IScene.GetLoadingStateName(this);
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x000092B6 File Offset: 0x000074B6
		public bool IsLoadingFinished()
		{
			return EngineApplicationInterface.IScene.IsLoadingFinished(this);
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x000092C3 File Offset: 0x000074C3
		public void SetPhotoModeRoll(float roll)
		{
			EngineApplicationInterface.IScene.SetPhotoModeRoll(this, roll);
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x000092D1 File Offset: 0x000074D1
		public void SetPhotoModeOrbit(bool orbit)
		{
			EngineApplicationInterface.IScene.SetPhotoModeOrbit(this, orbit);
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x000092DF File Offset: 0x000074DF
		public void SetPhotoModeOn(bool on)
		{
			EngineApplicationInterface.IScene.SetPhotoModeOn(this, on);
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x000092ED File Offset: 0x000074ED
		public void SetPhotoModeFocus(float focusStart, float focusEnd, float focus, float exposure)
		{
			EngineApplicationInterface.IScene.SetPhotoModeFocus(this, focusStart, focusEnd, focus, exposure);
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x000092FF File Offset: 0x000074FF
		public void SetPhotoModeFov(float verticalFov)
		{
			EngineApplicationInterface.IScene.SetPhotoModeFov(this, verticalFov);
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x0000930D File Offset: 0x0000750D
		public float GetPhotoModeFov()
		{
			return EngineApplicationInterface.IScene.GetPhotoModeFov(this);
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x0000931A File Offset: 0x0000751A
		public void SetPhotoModeVignette(bool vignetteOn)
		{
			EngineApplicationInterface.IScene.SetPhotoModeVignette(this, vignetteOn);
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x00009328 File Offset: 0x00007528
		public void SetSceneColorGradeIndex(int index)
		{
			EngineApplicationInterface.IScene.SetSceneColorGradeIndex(this, index);
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x00009336 File Offset: 0x00007536
		public int SetSceneFilterIndex(int index)
		{
			return EngineApplicationInterface.IScene.SetSceneFilterIndex(this, index);
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x00009344 File Offset: 0x00007544
		public void SetSceneColorGrade(string textureName)
		{
			EngineApplicationInterface.IScene.SetSceneColorGrade(this, textureName);
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x00009352 File Offset: 0x00007552
		public void SetUpgradeLevel(int level)
		{
			EngineApplicationInterface.IScene.SetUpgradeLevel(base.Pointer, level);
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x00009365 File Offset: 0x00007565
		public void CreateBurstParticle(int particleId, MatrixFrame frame)
		{
			EngineApplicationInterface.IScene.CreateBurstParticle(this, particleId, ref frame);
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x00009378 File Offset: 0x00007578
		public float[] GetTerrainHeightData(int nodeXIndex, int nodeYIndex)
		{
			float[] array = new float[EngineApplicationInterface.IScene.GetNodeDataCount(this, nodeXIndex, nodeYIndex)];
			EngineApplicationInterface.IScene.FillTerrainHeightData(this, nodeXIndex, nodeYIndex, array);
			return array;
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x000093A8 File Offset: 0x000075A8
		public short[] GetTerrainPhysicsMaterialIndexData(int nodeXIndex, int nodeYIndex)
		{
			short[] array = new short[EngineApplicationInterface.IScene.GetNodeDataCount(this, nodeXIndex, nodeYIndex)];
			EngineApplicationInterface.IScene.FillTerrainPhysicsMaterialIndexData(this, nodeXIndex, nodeYIndex, array);
			return array;
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x000093D7 File Offset: 0x000075D7
		public void GetTerrainData(out Vec2i nodeDimension, out float nodeSize, out int layerCount, out int layerVersion)
		{
			EngineApplicationInterface.IScene.GetTerrainData(this, out nodeDimension, out nodeSize, out layerCount, out layerVersion);
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x000093E9 File Offset: 0x000075E9
		public void GetTerrainNodeData(int xIndex, int yIndex, out int vertexCountAlongAxis, out float quadLength, out float minHeight, out float maxHeight)
		{
			EngineApplicationInterface.IScene.GetTerrainNodeData(this, xIndex, yIndex, out vertexCountAlongAxis, out quadLength, out minHeight, out maxHeight);
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x00009400 File Offset: 0x00007600
		public PhysicsMaterial GetTerrainPhysicsMaterialAtLayer(int layerIndex)
		{
			int terrainPhysicsMaterialIndexAtLayer = EngineApplicationInterface.IScene.GetTerrainPhysicsMaterialIndexAtLayer(this, layerIndex);
			return new PhysicsMaterial(terrainPhysicsMaterialIndexAtLayer);
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x00009420 File Offset: 0x00007620
		public void SetSceneColorGrade(Scene scene, string textureName)
		{
			EngineApplicationInterface.IScene.SetSceneColorGrade(scene, textureName);
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x0000942E File Offset: 0x0000762E
		public float GetWaterLevel()
		{
			return EngineApplicationInterface.IScene.GetWaterLevel(this);
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x0000943B File Offset: 0x0000763B
		public float GetWaterLevelAtPosition(Vec2 position, bool checkWaterBodyEntities)
		{
			return EngineApplicationInterface.IScene.GetWaterLevelAtPosition(this, position, checkWaterBodyEntities);
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x0000944C File Offset: 0x0000764C
		public bool GetPathBetweenAIFaces(UIntPtr startingFace, UIntPtr endingFace, Vec2 startingPosition, Vec2 endingPosition, float agentRadius, NavigationPath path, int[] excludedFaceIds = null)
		{
			int size = path.PathPoints.Length;
			if (EngineApplicationInterface.IScene.GetPathBetweenAIFacePointers(base.Pointer, startingFace, endingFace, startingPosition, endingPosition, agentRadius, path.PathPoints, ref size, excludedFaceIds, (excludedFaceIds != null) ? excludedFaceIds.Length : 0))
			{
				path.Size = size;
				return true;
			}
			path.Size = 0;
			return false;
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x000094A8 File Offset: 0x000076A8
		public bool GetPathBetweenAIFaces(int startingFace, int endingFace, Vec2 startingPosition, Vec2 endingPosition, float agentRadius, NavigationPath path, int[] excludedFaceIds = null, float extraCostMultiplier = 1f)
		{
			int size = path.PathPoints.Length;
			if (EngineApplicationInterface.IScene.GetPathBetweenAIFaceIndices(base.Pointer, startingFace, endingFace, startingPosition, endingPosition, agentRadius, path.PathPoints, ref size, excludedFaceIds, (excludedFaceIds != null) ? excludedFaceIds.Length : 0, extraCostMultiplier))
			{
				path.Size = size;
				return true;
			}
			path.Size = 0;
			return false;
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x00009504 File Offset: 0x00007704
		public bool GetPathDistanceBetweenAIFaces(int startingAiFace, int endingAiFace, Vec2 startingPosition, Vec2 endingPosition, float agentRadius, float distanceLimit, out float distance)
		{
			return EngineApplicationInterface.IScene.GetPathDistanceBetweenAIFaces(base.Pointer, startingAiFace, endingAiFace, startingPosition, endingPosition, agentRadius, distanceLimit, out distance);
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x0000952C File Offset: 0x0000772C
		public void GetNavMeshFaceIndex(ref PathFaceRecord record, Vec2 position, bool checkIfDisabled, bool ignoreHeight = false)
		{
			EngineApplicationInterface.IScene.GetNavMeshFaceIndex(base.Pointer, ref record, position, checkIfDisabled, ignoreHeight);
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x00009543 File Offset: 0x00007743
		public void GetNavMeshFaceIndex(ref PathFaceRecord record, Vec3 position, bool checkIfDisabled)
		{
			EngineApplicationInterface.IScene.GetNavMeshFaceIndex3(base.Pointer, ref record, position, checkIfDisabled);
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x00009558 File Offset: 0x00007758
		public static Scene CreateNewScene(bool initialize_physics = true, bool enable_decals = true, DecalAtlasGroup atlasGroup = DecalAtlasGroup.All, string sceneName = "mono_renderscene")
		{
			return EngineApplicationInterface.IScene.CreateNewScene(initialize_physics, enable_decals, (int)atlasGroup, sceneName);
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x00009568 File Offset: 0x00007768
		public MetaMesh CreatePathMesh(string baseEntityName, bool isWaterPath)
		{
			return EngineApplicationInterface.IScene.CreatePathMesh(base.Pointer, baseEntityName, isWaterPath);
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x0000957C File Offset: 0x0000777C
		public void SetActiveVisibilityLevels(List<string> levelsToActivate)
		{
			string text = "";
			for (int i = 0; i < levelsToActivate.Count; i++)
			{
				if (!levelsToActivate[i].Contains("$"))
				{
					if (i != 0)
					{
						text += "$";
					}
					text += levelsToActivate[i];
				}
			}
			EngineApplicationInterface.IScene.SetActiveVisibilityLevels(base.Pointer, text);
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x000095E1 File Offset: 0x000077E1
		public void SetDoNotWaitForLoadingStatesToRender(bool value)
		{
			EngineApplicationInterface.IScene.SetDoNotWaitForLoadingStatesToRender(base.Pointer, value);
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x000095F4 File Offset: 0x000077F4
		public void GetSnowAmountData(byte[] snowData)
		{
			EngineApplicationInterface.IScene.GetSnowAmountData(base.Pointer, snowData);
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x00009608 File Offset: 0x00007808
		public MetaMesh CreatePathMesh(IList<GameEntity> pathNodes, bool isWaterPath = false)
		{
			return EngineApplicationInterface.IScene.CreatePathMesh2(base.Pointer, (from e in pathNodes
			select e.Pointer).ToArray<UIntPtr>(), pathNodes.Count, isWaterPath);
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x00009656 File Offset: 0x00007856
		public GameEntity GetEntityWithGuid(string guid)
		{
			return EngineApplicationInterface.IScene.GetEntityWithGuid(base.Pointer, guid);
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x00009669 File Offset: 0x00007869
		public bool IsEntityFrameChanged(string containsName)
		{
			return EngineApplicationInterface.IScene.CheckPathEntitiesFrameChanged(base.Pointer, containsName);
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0000967C File Offset: 0x0000787C
		public void GetTerrainHeightAndNormal(Vec2 position, out float height, out Vec3 normal)
		{
			EngineApplicationInterface.IScene.GetTerrainHeightAndNormal(base.Pointer, position, out height, out normal);
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x00009691 File Offset: 0x00007891
		public int GetFloraInstanceCount()
		{
			return EngineApplicationInterface.IScene.GetFloraInstanceCount(base.Pointer);
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x000096A3 File Offset: 0x000078A3
		public int GetFloraRendererTextureUsage()
		{
			return EngineApplicationInterface.IScene.GetFloraRendererTextureUsage(base.Pointer);
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x000096B5 File Offset: 0x000078B5
		public int GetTerrainMemoryUsage()
		{
			return EngineApplicationInterface.IScene.GetTerrainMemoryUsage(base.Pointer);
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x000096C7 File Offset: 0x000078C7
		public void StallLoadingRenderingsUntilFurtherNotice()
		{
			EngineApplicationInterface.IScene.StallLoadingRenderingsUntilFurtherNotice(base.Pointer);
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x000096D9 File Offset: 0x000078D9
		public int GetNavMeshFaceCount()
		{
			return EngineApplicationInterface.IScene.GetNavMeshFaceCount(base.Pointer);
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x000096EB File Offset: 0x000078EB
		public void ResumeLoadingRenderings()
		{
			EngineApplicationInterface.IScene.ResumeLoadingRenderings(base.Pointer);
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x000096FD File Offset: 0x000078FD
		public uint GetUpgradeLevelMask()
		{
			return EngineApplicationInterface.IScene.GetUpgradeLevelMask(base.Pointer);
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x0000970F File Offset: 0x0000790F
		public void SetUpgradeLevelVisibility(uint mask)
		{
			EngineApplicationInterface.IScene.SetUpgradeLevelVisibilityWithMask(base.Pointer, mask);
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x00009724 File Offset: 0x00007924
		public void SetUpgradeLevelVisibility(List<string> levels)
		{
			string text = "";
			for (int i = 0; i < levels.Count - 1; i++)
			{
				text = text + levels[i] + "|";
			}
			text += levels[levels.Count - 1];
			EngineApplicationInterface.IScene.SetUpgradeLevelVisibility(base.Pointer, text);
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x00009783 File Offset: 0x00007983
		public int GetIdOfNavMeshFace(int faceIndex)
		{
			return EngineApplicationInterface.IScene.GetIdOfNavMeshFace(base.Pointer, faceIndex);
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x00009796 File Offset: 0x00007996
		public void SetClothSimulationState(bool state)
		{
			EngineApplicationInterface.IScene.SetClothSimulationState(base.Pointer, state);
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x000097A9 File Offset: 0x000079A9
		public void GetNavMeshCenterPosition(int faceIndex, ref Vec3 centerPosition)
		{
			EngineApplicationInterface.IScene.GetNavMeshFaceCenterPosition(base.Pointer, faceIndex, ref centerPosition);
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x000097BD File Offset: 0x000079BD
		public GameEntity GetFirstEntityWithName(string name)
		{
			return EngineApplicationInterface.IScene.GetFirstEntityWithName(base.Pointer, name);
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x000097D0 File Offset: 0x000079D0
		public GameEntity GetCampaignEntityWithName(string name)
		{
			return EngineApplicationInterface.IScene.GetCampaignEntityWithName(base.Pointer, name);
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x000097E4 File Offset: 0x000079E4
		public void GetAllEntitiesWithScriptComponent<T>(ref List<GameEntity> entities) where T : ScriptComponentBehavior
		{
			NativeObjectArray nativeObjectArray = NativeObjectArray.Create();
			string name = typeof(T).Name;
			EngineApplicationInterface.IScene.GetAllEntitiesWithScriptComponent(base.Pointer, name, nativeObjectArray.Pointer);
			for (int i = 0; i < nativeObjectArray.Count; i++)
			{
				entities.Add(nativeObjectArray.GetElementAt(i) as GameEntity);
			}
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x00009844 File Offset: 0x00007A44
		public GameEntity GetFirstEntityWithScriptComponent<T>() where T : ScriptComponentBehavior
		{
			string name = typeof(T).Name;
			return EngineApplicationInterface.IScene.GetFirstEntityWithScriptComponent(base.Pointer, name);
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x00009872 File Offset: 0x00007A72
		public uint GetUpgradeLevelMaskOfLevelName(string levelName)
		{
			return EngineApplicationInterface.IScene.GetUpgradeLevelMaskOfLevelName(base.Pointer, levelName);
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x00009885 File Offset: 0x00007A85
		public string GetUpgradeLevelNameOfIndex(int index)
		{
			return EngineApplicationInterface.IScene.GetUpgradeLevelNameOfIndex(base.Pointer, index);
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x00009898 File Offset: 0x00007A98
		public int GetUpgradeLevelCount()
		{
			return EngineApplicationInterface.IScene.GetUpgradeLevelCount(base.Pointer);
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x000098AA File Offset: 0x00007AAA
		public float GetWinterTimeFactor()
		{
			return EngineApplicationInterface.IScene.GetWinterTimeFactor(base.Pointer);
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x000098BC File Offset: 0x00007ABC
		public float GetNavMeshFaceFirstVertexZ(int faceIndex)
		{
			return EngineApplicationInterface.IScene.GetNavMeshFaceFirstVertexZ(base.Pointer, faceIndex);
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x000098CF File Offset: 0x00007ACF
		public void SetWinterTimeFactor(float winterTimeFactor)
		{
			EngineApplicationInterface.IScene.SetWinterTimeFactor(base.Pointer, winterTimeFactor);
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x000098E2 File Offset: 0x00007AE2
		public void SetDrynessFactor(float drynessFactor)
		{
			EngineApplicationInterface.IScene.SetDrynessFactor(base.Pointer, drynessFactor);
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x000098F5 File Offset: 0x00007AF5
		public float GetFog()
		{
			return EngineApplicationInterface.IScene.GetFog(base.Pointer);
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x00009907 File Offset: 0x00007B07
		public void SetFog(float fogDensity, ref Vec3 fogColor, float fogFalloff)
		{
			EngineApplicationInterface.IScene.SetFog(base.Pointer, fogDensity, ref fogColor, fogFalloff);
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x0000991C File Offset: 0x00007B1C
		public void SetFogAdvanced(float fogFalloffOffset, float fogFalloffMinFog, float fogFalloffStartDist)
		{
			EngineApplicationInterface.IScene.SetFogAdvanced(base.Pointer, fogFalloffOffset, fogFalloffMinFog, fogFalloffStartDist);
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x00009931 File Offset: 0x00007B31
		public void SetFogAmbientColor(ref Vec3 fogAmbientColor)
		{
			EngineApplicationInterface.IScene.SetFogAmbientColor(base.Pointer, ref fogAmbientColor);
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x00009944 File Offset: 0x00007B44
		public void SetTemperature(float temperature)
		{
			EngineApplicationInterface.IScene.SetTemperature(base.Pointer, temperature);
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x00009957 File Offset: 0x00007B57
		public void SetHumidity(float humidity)
		{
			EngineApplicationInterface.IScene.SetHumidity(base.Pointer, humidity);
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x0000996A File Offset: 0x00007B6A
		public void SetDynamicShadowmapCascadesRadiusMultiplier(float multiplier)
		{
			EngineApplicationInterface.IScene.SetDynamicShadowmapCascadesRadiusMultiplier(base.Pointer, multiplier);
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x0000997D File Offset: 0x00007B7D
		public void SetEnvironmentMultiplier(bool useMultiplier, float multiplier)
		{
			EngineApplicationInterface.IScene.SetEnvironmentMultiplier(base.Pointer, useMultiplier, multiplier);
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x00009991 File Offset: 0x00007B91
		public void SetSkyRotation(float rotation)
		{
			EngineApplicationInterface.IScene.SetSkyRotation(base.Pointer, rotation);
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x000099A4 File Offset: 0x00007BA4
		public void SetSkyBrightness(float brightness)
		{
			EngineApplicationInterface.IScene.SetSkyBrightness(base.Pointer, brightness);
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x000099B7 File Offset: 0x00007BB7
		public void SetForcedSnow(bool value)
		{
			EngineApplicationInterface.IScene.SetForcedSnow(base.Pointer, value);
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x000099CA File Offset: 0x00007BCA
		public void SetSunLight(ref Vec3 color, ref Vec3 direction)
		{
			EngineApplicationInterface.IScene.SetSunLight(base.Pointer, color, direction);
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x000099E8 File Offset: 0x00007BE8
		public void SetSunDirection(ref Vec3 direction)
		{
			EngineApplicationInterface.IScene.SetSunDirection(base.Pointer, direction);
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x00009A00 File Offset: 0x00007C00
		public void SetSun(ref Vec3 color, float altitude, float angle, float intensity)
		{
			EngineApplicationInterface.IScene.SetSun(base.Pointer, color, altitude, angle, intensity);
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x00009A1C File Offset: 0x00007C1C
		public void SetSunAngleAltitude(float angle, float altitude)
		{
			EngineApplicationInterface.IScene.SetSunAngleAltitude(base.Pointer, angle, altitude);
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x00009A30 File Offset: 0x00007C30
		public void SetSunSize(float size)
		{
			EngineApplicationInterface.IScene.SetSunSize(base.Pointer, size);
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x00009A43 File Offset: 0x00007C43
		public void SetSunShaftStrength(float strength)
		{
			EngineApplicationInterface.IScene.SetSunShaftStrength(base.Pointer, strength);
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x00009A56 File Offset: 0x00007C56
		public float GetRainDensity()
		{
			return EngineApplicationInterface.IScene.GetRainDensity(base.Pointer);
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x00009A68 File Offset: 0x00007C68
		public void SetRainDensity(float density)
		{
			EngineApplicationInterface.IScene.SetRainDensity(base.Pointer, density);
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x00009A7B File Offset: 0x00007C7B
		public float GetSnowDensity()
		{
			return EngineApplicationInterface.IScene.GetSnowDensity(base.Pointer);
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x00009A8D File Offset: 0x00007C8D
		public void SetSnowDensity(float density)
		{
			EngineApplicationInterface.IScene.SetSnowDensity(base.Pointer, density);
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x00009AA0 File Offset: 0x00007CA0
		public void AddDecalInstance(Decal decal, string decalSetID, bool deletable)
		{
			EngineApplicationInterface.IScene.AddDecalInstance(base.Pointer, decal.Pointer, decalSetID, deletable);
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x00009ABA File Offset: 0x00007CBA
		public void SetShadow(bool shadowEnabled)
		{
			EngineApplicationInterface.IScene.SetShadow(base.Pointer, shadowEnabled);
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x00009ACD File Offset: 0x00007CCD
		public int AddPointLight(ref Vec3 position, float radius)
		{
			return EngineApplicationInterface.IScene.AddPointLight(base.Pointer, position, radius);
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x00009AE6 File Offset: 0x00007CE6
		public int AddDirectionalLight(ref Vec3 position, ref Vec3 direction, float radius)
		{
			return EngineApplicationInterface.IScene.AddDirectionalLight(base.Pointer, position, direction, radius);
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x00009B05 File Offset: 0x00007D05
		public void SetLightPosition(int lightIndex, ref Vec3 position)
		{
			EngineApplicationInterface.IScene.SetLightPosition(base.Pointer, lightIndex, position);
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x00009B1E File Offset: 0x00007D1E
		public void SetLightDiffuseColor(int lightIndex, ref Vec3 diffuseColor)
		{
			EngineApplicationInterface.IScene.SetLightDiffuseColor(base.Pointer, lightIndex, diffuseColor);
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x00009B37 File Offset: 0x00007D37
		public void SetLightDirection(int lightIndex, ref Vec3 direction)
		{
			EngineApplicationInterface.IScene.SetLightDirection(base.Pointer, lightIndex, direction);
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x00009B50 File Offset: 0x00007D50
		public void SetMieScatterFocus(float strength)
		{
			EngineApplicationInterface.IScene.SetMieScatterFocus(base.Pointer, strength);
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x00009B63 File Offset: 0x00007D63
		public void SetMieScatterStrength(float strength)
		{
			EngineApplicationInterface.IScene.SetMieScatterStrength(base.Pointer, strength);
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x00009B76 File Offset: 0x00007D76
		public void SetBrightpassThreshold(float threshold)
		{
			EngineApplicationInterface.IScene.SetBrightpassTreshold(base.Pointer, threshold);
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x00009B89 File Offset: 0x00007D89
		public void SetLensDistortion(float amount)
		{
			EngineApplicationInterface.IScene.SetLensDistortion(base.Pointer, amount);
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x00009B9C File Offset: 0x00007D9C
		public void SetHexagonVignetteAlpha(float amount)
		{
			EngineApplicationInterface.IScene.SetHexagonVignetteAlpha(base.Pointer, amount);
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x00009BAF File Offset: 0x00007DAF
		public void SetMinExposure(float minExposure)
		{
			EngineApplicationInterface.IScene.SetMinExposure(base.Pointer, minExposure);
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x00009BC2 File Offset: 0x00007DC2
		public void SetMaxExposure(float maxExposure)
		{
			EngineApplicationInterface.IScene.SetMaxExposure(base.Pointer, maxExposure);
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x00009BD5 File Offset: 0x00007DD5
		public void SetTargetExposure(float targetExposure)
		{
			EngineApplicationInterface.IScene.SetTargetExposure(base.Pointer, targetExposure);
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x00009BE8 File Offset: 0x00007DE8
		public void SetMiddleGray(float middleGray)
		{
			EngineApplicationInterface.IScene.SetMiddleGray(base.Pointer, middleGray);
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x00009BFB File Offset: 0x00007DFB
		public void SetBloomStrength(float bloomStrength)
		{
			EngineApplicationInterface.IScene.SetBloomStrength(base.Pointer, bloomStrength);
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x00009C0E File Offset: 0x00007E0E
		public void SetBloomAmount(float bloomAmount)
		{
			EngineApplicationInterface.IScene.SetBloomAmount(base.Pointer, bloomAmount);
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x00009C21 File Offset: 0x00007E21
		public void SetGrainAmount(float grainAmount)
		{
			EngineApplicationInterface.IScene.SetGrainAmount(base.Pointer, grainAmount);
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x00009C34 File Offset: 0x00007E34
		public GameEntity AddItemEntity(ref MatrixFrame placementFrame, MetaMesh metaMesh)
		{
			return EngineApplicationInterface.IScene.AddItemEntity(base.Pointer, ref placementFrame, metaMesh.Pointer);
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x00009C4D File Offset: 0x00007E4D
		public void RemoveEntity(GameEntity entity, int removeReason)
		{
			EngineApplicationInterface.IScene.RemoveEntity(base.Pointer, entity.Pointer, removeReason);
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x00009C66 File Offset: 0x00007E66
		public bool AttachEntity(GameEntity entity, bool showWarnings = false)
		{
			return EngineApplicationInterface.IScene.AttachEntity(base.Pointer, entity.Pointer, showWarnings);
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x00009C7F File Offset: 0x00007E7F
		public void AddEntityWithMesh(Mesh mesh, ref MatrixFrame frame)
		{
			EngineApplicationInterface.IScene.AddEntityWithMesh(base.Pointer, mesh.Pointer, ref frame);
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x00009C98 File Offset: 0x00007E98
		public void AddEntityWithMultiMesh(MetaMesh mesh, ref MatrixFrame frame)
		{
			EngineApplicationInterface.IScene.AddEntityWithMultiMesh(base.Pointer, mesh.Pointer, ref frame);
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x00009CB1 File Offset: 0x00007EB1
		public void Tick(float dt)
		{
			EngineApplicationInterface.IScene.Tick(base.Pointer, dt);
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x00009CC4 File Offset: 0x00007EC4
		public void ClearAll()
		{
			EngineApplicationInterface.IScene.ClearAll(base.Pointer);
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x00009CD8 File Offset: 0x00007ED8
		public void SetDefaultLighting()
		{
			Vec3 vec = new Vec3(1.15f, 1.2f, 1.25f, -1f);
			Vec3 vec2 = new Vec3(1f, -1f, -1f, -1f);
			vec2.Normalize();
			this.SetSunLight(ref vec, ref vec2);
			this.SetShadow(false);
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x00009D34 File Offset: 0x00007F34
		public bool CalculateEffectiveLighting()
		{
			return EngineApplicationInterface.IScene.CalculateEffectiveLighting(base.Pointer);
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x00009D46 File Offset: 0x00007F46
		public bool GetPathDistanceBetweenPositions(ref WorldPosition point0, ref WorldPosition point1, float agentRadius, out float pathDistance)
		{
			pathDistance = 0f;
			return EngineApplicationInterface.IScene.GetPathDistanceBetweenPositions(base.Pointer, ref point0, ref point1, agentRadius, ref pathDistance);
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x00009D65 File Offset: 0x00007F65
		public bool IsLineToPointClear(ref WorldPosition position, ref WorldPosition destination, float agentRadius)
		{
			return EngineApplicationInterface.IScene.IsLineToPointClear2(base.Pointer, position.GetNavMesh(), position.AsVec2, destination.AsVec2, agentRadius);
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x00009D8A File Offset: 0x00007F8A
		public bool IsLineToPointClear(int startingFace, Vec2 position, Vec2 destination, float agentRadius)
		{
			return EngineApplicationInterface.IScene.IsLineToPointClear(base.Pointer, startingFace, position, destination, agentRadius);
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x00009DA1 File Offset: 0x00007FA1
		public Vec2 GetLastPointOnNavigationMeshFromPositionToDestination(int startingFace, Vec2 position, Vec2 destination)
		{
			return EngineApplicationInterface.IScene.GetLastPointOnNavigationMeshFromPositionToDestination(base.Pointer, startingFace, position, destination);
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x00009DB6 File Offset: 0x00007FB6
		public Vec3 GetLastPointOnNavigationMeshFromWorldPositionToDestination(ref WorldPosition position, Vec2 destination)
		{
			return EngineApplicationInterface.IScene.GetLastPointOnNavigationMeshFromWorldPositionToDestination(base.Pointer, ref position, destination);
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x00009DCA File Offset: 0x00007FCA
		public bool DoesPathExistBetweenFaces(int firstNavMeshFace, int secondNavMeshFace, bool ignoreDisabled)
		{
			return EngineApplicationInterface.IScene.DoesPathExistBetweenFaces(base.Pointer, firstNavMeshFace, secondNavMeshFace, ignoreDisabled);
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x00009DDF File Offset: 0x00007FDF
		public bool GetHeightAtPoint(Vec2 point, BodyFlags excludeBodyFlags, ref float height)
		{
			return EngineApplicationInterface.IScene.GetHeightAtPoint(base.Pointer, point, excludeBodyFlags, ref height);
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x00009DF4 File Offset: 0x00007FF4
		public Vec3 GetNormalAt(Vec2 position)
		{
			return EngineApplicationInterface.IScene.GetNormalAt(base.Pointer, position);
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x00009E08 File Offset: 0x00008008
		public void GetEntities(ref List<GameEntity> entities)
		{
			NativeObjectArray nativeObjectArray = NativeObjectArray.Create();
			EngineApplicationInterface.IScene.GetEntities(base.Pointer, nativeObjectArray.Pointer);
			for (int i = 0; i < nativeObjectArray.Count; i++)
			{
				entities.Add(nativeObjectArray.GetElementAt(i) as GameEntity);
			}
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x00009E55 File Offset: 0x00008055
		public void GetRootEntities(NativeObjectArray entities)
		{
			EngineApplicationInterface.IScene.GetRootEntities(this, entities);
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000975 RID: 2421 RVA: 0x00009E63 File Offset: 0x00008063
		public int RootEntityCount
		{
			get
			{
				return EngineApplicationInterface.IScene.GetRootEntityCount(base.Pointer);
			}
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x00009E78 File Offset: 0x00008078
		public int SelectEntitiesInBoxWithScriptComponent<T>(ref Vec3 boundingBoxMin, ref Vec3 boundingBoxMax, GameEntity[] entitiesOutput, UIntPtr[] entityIds) where T : ScriptComponentBehavior
		{
			string name = typeof(T).Name;
			int num = EngineApplicationInterface.IScene.SelectEntitiesInBoxWithScriptComponent(base.Pointer, ref boundingBoxMin, ref boundingBoxMax, entityIds, entitiesOutput.Length, name);
			for (int i = 0; i < num; i++)
			{
				entitiesOutput[i] = new GameEntity(entityIds[i]);
			}
			return num;
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x00009EC7 File Offset: 0x000080C7
		public int SelectEntitiesCollidedWith(ref Ray ray, Intersection[] intersectionsOutput, UIntPtr[] entityIds)
		{
			return EngineApplicationInterface.IScene.SelectEntitiesCollidedWith(base.Pointer, ref ray, entityIds, intersectionsOutput);
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x00009EDC File Offset: 0x000080DC
		public int GenerateContactsWithCapsule(ref CapsuleData capsule, BodyFlags exclude_flags, Intersection[] intersectionsOutput)
		{
			return EngineApplicationInterface.IScene.GenerateContactsWithCapsule(base.Pointer, ref capsule, exclude_flags, intersectionsOutput);
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x00009EF1 File Offset: 0x000080F1
		public void InvalidateTerrainPhysicsMaterials()
		{
			EngineApplicationInterface.IScene.InvalidateTerrainPhysicsMaterials(base.Pointer);
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x00009F04 File Offset: 0x00008104
		public void Read(string sceneName)
		{
			SceneInitializationData sceneInitializationData = new SceneInitializationData(true);
			EngineApplicationInterface.IScene.Read(base.Pointer, sceneName, ref sceneInitializationData, "");
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x00009F31 File Offset: 0x00008131
		public void Read(string sceneName, ref SceneInitializationData initData, string forcedAtmoName = "")
		{
			EngineApplicationInterface.IScene.Read(base.Pointer, sceneName, ref initData, forcedAtmoName);
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x00009F48 File Offset: 0x00008148
		public MatrixFrame ReadAndCalculateInitialCamera()
		{
			MatrixFrame result = default(MatrixFrame);
			EngineApplicationInterface.IScene.ReadAndCalculateInitialCamera(base.Pointer, ref result);
			return result;
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x00009F70 File Offset: 0x00008170
		public void OptimizeScene(bool optimizeFlora = true, bool optimizeOro = false)
		{
			EngineApplicationInterface.IScene.OptimizeScene(base.Pointer, optimizeFlora, optimizeOro);
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x00009F84 File Offset: 0x00008184
		public float GetTerrainHeight(Vec2 position, bool checkHoles = true)
		{
			return EngineApplicationInterface.IScene.GetTerrainHeight(base.Pointer, position, checkHoles);
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x00009F98 File Offset: 0x00008198
		public void CheckResources()
		{
			EngineApplicationInterface.IScene.CheckResources(base.Pointer);
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x00009FAA File Offset: 0x000081AA
		public void ForceLoadResources()
		{
			EngineApplicationInterface.IScene.ForceLoadResources(base.Pointer);
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x00009FBC File Offset: 0x000081BC
		public void SetDepthOfFieldParameters(float depthOfFieldFocusStart, float depthOfFieldFocusEnd, bool isVignetteOn)
		{
			EngineApplicationInterface.IScene.SetDofParams(base.Pointer, depthOfFieldFocusStart, depthOfFieldFocusEnd, isVignetteOn);
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x00009FD1 File Offset: 0x000081D1
		public void SetDepthOfFieldFocus(float depthOfFieldFocus)
		{
			EngineApplicationInterface.IScene.SetDofFocus(base.Pointer, depthOfFieldFocus);
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x00009FE4 File Offset: 0x000081E4
		public void ResetDepthOfFieldParams()
		{
			EngineApplicationInterface.IScene.SetDofFocus(base.Pointer, 0f);
			EngineApplicationInterface.IScene.SetDofParams(base.Pointer, 0f, 0f, true);
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000984 RID: 2436 RVA: 0x0000A016 File Offset: 0x00008216
		public bool HasTerrainHeightmap
		{
			get
			{
				return EngineApplicationInterface.IScene.HasTerrainHeightmap(base.Pointer);
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000985 RID: 2437 RVA: 0x0000A028 File Offset: 0x00008228
		public bool ContainsTerrain
		{
			get
			{
				return EngineApplicationInterface.IScene.ContainsTerrain(base.Pointer);
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000987 RID: 2439 RVA: 0x0000A04D File Offset: 0x0000824D
		// (set) Token: 0x06000986 RID: 2438 RVA: 0x0000A03A File Offset: 0x0000823A
		public float TimeOfDay
		{
			get
			{
				return EngineApplicationInterface.IScene.GetTimeOfDay(base.Pointer);
			}
			set
			{
				EngineApplicationInterface.IScene.SetTimeOfDay(base.Pointer, value);
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000988 RID: 2440 RVA: 0x0000A05F File Offset: 0x0000825F
		public bool IsAtmosphereIndoor
		{
			get
			{
				return EngineApplicationInterface.IScene.IsAtmosphereIndoor(base.Pointer);
			}
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x0000A071 File Offset: 0x00008271
		public void PreloadForRendering()
		{
			EngineApplicationInterface.IScene.PreloadForRendering(base.Pointer);
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600098A RID: 2442 RVA: 0x0000A083 File Offset: 0x00008283
		public Vec3 LastFinalRenderCameraPosition
		{
			get
			{
				return EngineApplicationInterface.IScene.GetLastFinalRenderCameraPosition(base.Pointer);
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600098B RID: 2443 RVA: 0x0000A098 File Offset: 0x00008298
		public MatrixFrame LastFinalRenderCameraFrame
		{
			get
			{
				MatrixFrame result = default(MatrixFrame);
				EngineApplicationInterface.IScene.GetLastFinalRenderCameraFrame(base.Pointer, ref result);
				return result;
			}
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0000A0C0 File Offset: 0x000082C0
		public void SetColorGradeBlend(string texture1, string texture2, float alpha)
		{
			EngineApplicationInterface.IScene.SetColorGradeBlend(base.Pointer, texture1, texture2, alpha);
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0000A0D5 File Offset: 0x000082D5
		public float GetGroundHeightAtPosition(Vec3 position, BodyFlags excludeFlags = BodyFlags.CommonCollisionExcludeFlags)
		{
			return EngineApplicationInterface.IScene.GetGroundHeightAtPosition(base.Pointer, position, (uint)excludeFlags);
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x0000A0E9 File Offset: 0x000082E9
		public float GetGroundHeightAtPositionMT(Vec3 position, BodyFlags excludeFlags = BodyFlags.CommonCollisionExcludeFlags)
		{
			return EngineApplicationInterface.IScene.GetGroundHeightAtPosition(base.Pointer, position, (uint)excludeFlags);
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0000A0FD File Offset: 0x000082FD
		public float GetGroundHeightAtPosition(Vec3 position, out Vec3 normal, BodyFlags excludeFlags = BodyFlags.CommonCollisionExcludeFlags)
		{
			normal = Vec3.Invalid;
			return EngineApplicationInterface.IScene.GetGroundHeightAtPosition(base.Pointer, position, (uint)excludeFlags);
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0000A11C File Offset: 0x0000831C
		public float GetGroundHeightAtPositionMT(Vec3 position, out Vec3 normal, BodyFlags excludeFlags = BodyFlags.CommonCollisionExcludeFlags)
		{
			normal = Vec3.Invalid;
			return EngineApplicationInterface.IScene.GetGroundHeightAndNormalAtPosition(base.Pointer, position, ref normal, (uint)excludeFlags);
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0000A13C File Offset: 0x0000833C
		public void PauseSceneSounds()
		{
			EngineApplicationInterface.IScene.PauseSceneSounds(base.Pointer);
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0000A14E File Offset: 0x0000834E
		public void ResumeSceneSounds()
		{
			EngineApplicationInterface.IScene.ResumeSceneSounds(base.Pointer);
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x0000A160 File Offset: 0x00008360
		public void FinishSceneSounds()
		{
			EngineApplicationInterface.IScene.FinishSceneSounds(base.Pointer);
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x0000A174 File Offset: 0x00008374
		public bool BoxCastOnlyForCamera(Vec3[] boxPoints, Vec3 centerPoint, bool castSupportRay, Vec3 supportRaycastPoint, Vec3 dir, float distance, out float collisionDistance, out Vec3 closestPoint, out GameEntity collidedEntity, bool preFilter = true, bool postFilter = true, BodyFlags excludedBodyFlags = BodyFlags.Disabled | BodyFlags.Dynamic | BodyFlags.Ladder | BodyFlags.OnlyCollideWithRaycast | BodyFlags.AILimiter | BodyFlags.Barrier | BodyFlags.Barrier3D | BodyFlags.Ragdoll | BodyFlags.RagdollLimiter | BodyFlags.DroppedItem | BodyFlags.DoNotCollideWithRaycast | BodyFlags.DontCollideWithCamera | BodyFlags.AgentOnly | BodyFlags.MissileOnly)
		{
			collisionDistance = float.NaN;
			closestPoint = Vec3.Invalid;
			UIntPtr zero = UIntPtr.Zero;
			bool flag = castSupportRay && EngineApplicationInterface.IScene.RayCastForClosestEntityOrTerrain(base.Pointer, ref supportRaycastPoint, ref centerPoint, 0f, ref collisionDistance, ref closestPoint, ref zero, excludedBodyFlags);
			if (!flag)
			{
				flag = EngineApplicationInterface.IScene.BoxCastOnlyForCamera(base.Pointer, boxPoints, ref centerPoint, ref dir, distance, ref collisionDistance, ref closestPoint, ref zero, excludedBodyFlags, preFilter, postFilter);
			}
			if (flag && zero != UIntPtr.Zero)
			{
				collidedEntity = new GameEntity(zero);
			}
			else
			{
				collidedEntity = null;
			}
			return flag;
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x0000A20C File Offset: 0x0000840C
		public bool BoxCast(Vec3 boxMin, Vec3 boxMax, bool castSupportRay, Vec3 supportRaycastPoint, Vec3 dir, float distance, out float collisionDistance, out Vec3 closestPoint, out GameEntity collidedEntity, BodyFlags excludedBodyFlags = BodyFlags.CameraCollisionRayCastExludeFlags)
		{
			collisionDistance = float.NaN;
			closestPoint = Vec3.Invalid;
			UIntPtr zero = UIntPtr.Zero;
			Vec3 vec = (boxMin + boxMax) * 0.5f;
			bool flag = castSupportRay && EngineApplicationInterface.IScene.RayCastForClosestEntityOrTerrain(base.Pointer, ref supportRaycastPoint, ref vec, 0f, ref collisionDistance, ref closestPoint, ref zero, excludedBodyFlags);
			if (!flag)
			{
				flag = EngineApplicationInterface.IScene.BoxCast(base.Pointer, ref boxMin, ref boxMax, ref dir, distance, ref collisionDistance, ref closestPoint, ref zero, excludedBodyFlags);
			}
			if (flag && zero != UIntPtr.Zero)
			{
				collidedEntity = new GameEntity(zero);
			}
			else
			{
				collidedEntity = null;
			}
			return flag;
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0000A2B4 File Offset: 0x000084B4
		public bool RayCastForClosestEntityOrTerrain(Vec3 sourcePoint, Vec3 targetPoint, out float collisionDistance, out Vec3 closestPoint, out GameEntity collidedEntity, float rayThickness = 0.01f, BodyFlags excludeBodyFlags = BodyFlags.CommonFocusRayCastExcludeFlags)
		{
			collisionDistance = float.NaN;
			closestPoint = Vec3.Invalid;
			UIntPtr zero = UIntPtr.Zero;
			bool flag = EngineApplicationInterface.IScene.RayCastForClosestEntityOrTerrain(base.Pointer, ref sourcePoint, ref targetPoint, rayThickness, ref collisionDistance, ref closestPoint, ref zero, excludeBodyFlags);
			if (flag && zero != UIntPtr.Zero)
			{
				collidedEntity = new GameEntity(zero);
				return flag;
			}
			collidedEntity = null;
			return flag;
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x0000A318 File Offset: 0x00008518
		public bool RayCastForClosestEntityOrTerrainMT(Vec3 sourcePoint, Vec3 targetPoint, out float collisionDistance, out Vec3 closestPoint, out GameEntity collidedEntity, float rayThickness = 0.01f, BodyFlags excludeBodyFlags = BodyFlags.CommonFocusRayCastExcludeFlags)
		{
			collisionDistance = float.NaN;
			closestPoint = Vec3.Invalid;
			UIntPtr zero = UIntPtr.Zero;
			bool flag = EngineApplicationInterface.IScene.RayCastForClosestEntityOrTerrain(base.Pointer, ref sourcePoint, ref targetPoint, rayThickness, ref collisionDistance, ref closestPoint, ref zero, excludeBodyFlags);
			if (flag && zero != UIntPtr.Zero)
			{
				collidedEntity = new GameEntity(zero);
				return flag;
			}
			collidedEntity = null;
			return flag;
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x0000A37C File Offset: 0x0000857C
		public bool RayCastForClosestEntityOrTerrain(Vec3 sourcePoint, Vec3 targetPoint, out float collisionDistance, out GameEntity collidedEntity, float rayThickness = 0.01f, BodyFlags excludeBodyFlags = BodyFlags.CommonFocusRayCastExcludeFlags)
		{
			Vec3 vec;
			return this.RayCastForClosestEntityOrTerrain(sourcePoint, targetPoint, out collisionDistance, out vec, out collidedEntity, rayThickness, excludeBodyFlags);
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x0000A39C File Offset: 0x0000859C
		public bool RayCastForClosestEntityOrTerrainMT(Vec3 sourcePoint, Vec3 targetPoint, out float collisionDistance, out GameEntity collidedEntity, float rayThickness = 0.01f, BodyFlags excludeBodyFlags = BodyFlags.CommonFocusRayCastExcludeFlags)
		{
			Vec3 vec;
			return this.RayCastForClosestEntityOrTerrainMT(sourcePoint, targetPoint, out collisionDistance, out vec, out collidedEntity, rayThickness, excludeBodyFlags);
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x0000A3BC File Offset: 0x000085BC
		public bool RayCastForClosestEntityOrTerrain(Vec3 sourcePoint, Vec3 targetPoint, out float collisionDistance, out Vec3 closestPoint, float rayThickness = 0.01f, BodyFlags excludeBodyFlags = BodyFlags.CommonFocusRayCastExcludeFlags)
		{
			collisionDistance = float.NaN;
			closestPoint = Vec3.Invalid;
			UIntPtr zero = UIntPtr.Zero;
			return EngineApplicationInterface.IScene.RayCastForClosestEntityOrTerrain(base.Pointer, ref sourcePoint, ref targetPoint, rayThickness, ref collisionDistance, ref closestPoint, ref zero, excludeBodyFlags);
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x0000A400 File Offset: 0x00008600
		public bool RayCastForClosestEntityOrTerrainMT(Vec3 sourcePoint, Vec3 targetPoint, out float collisionDistance, out Vec3 closestPoint, float rayThickness = 0.01f, BodyFlags excludeBodyFlags = BodyFlags.CommonFocusRayCastExcludeFlags)
		{
			collisionDistance = float.NaN;
			closestPoint = Vec3.Invalid;
			UIntPtr zero = UIntPtr.Zero;
			return EngineApplicationInterface.IScene.RayCastForClosestEntityOrTerrain(base.Pointer, ref sourcePoint, ref targetPoint, rayThickness, ref collisionDistance, ref closestPoint, ref zero, excludeBodyFlags);
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x0000A444 File Offset: 0x00008644
		public bool RayCastForClosestEntityOrTerrain(Vec3 sourcePoint, Vec3 targetPoint, out float collisionDistance, float rayThickness = 0.01f, BodyFlags excludeBodyFlags = BodyFlags.CommonFocusRayCastExcludeFlags)
		{
			Vec3 vec;
			return this.RayCastForClosestEntityOrTerrain(sourcePoint, targetPoint, out collisionDistance, out vec, rayThickness, excludeBodyFlags);
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x0000A460 File Offset: 0x00008660
		public bool RayCastForClosestEntityOrTerrainMT(Vec3 sourcePoint, Vec3 targetPoint, out float collisionDistance, float rayThickness = 0.01f, BodyFlags excludeBodyFlags = BodyFlags.CommonFocusRayCastExcludeFlags)
		{
			Vec3 vec;
			return this.RayCastForClosestEntityOrTerrainMT(sourcePoint, targetPoint, out collisionDistance, out vec, rayThickness, excludeBodyFlags);
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x0000A47C File Offset: 0x0000867C
		public void ImportNavigationMeshPrefab(string navMeshPrefabName, int navMeshGroupShift)
		{
			EngineApplicationInterface.IScene.LoadNavMeshPrefab(base.Pointer, navMeshPrefabName, navMeshGroupShift);
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x0000A490 File Offset: 0x00008690
		public void MarkFacesWithIdAsLadder(int faceGroupId, bool isLadder)
		{
			EngineApplicationInterface.IScene.MarkFacesWithIdAsLadder(base.Pointer, faceGroupId, isLadder);
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x0000A4A4 File Offset: 0x000086A4
		public void SetAbilityOfFacesWithId(int faceGroupId, bool isEnabled)
		{
			EngineApplicationInterface.IScene.SetAbilityOfFacesWithId(base.Pointer, faceGroupId, isEnabled);
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x0000A4B8 File Offset: 0x000086B8
		public void SwapFaceConnectionsWithID(int hubFaceGroupID, int toBeSeparatedFaceGroupId, int toBeMergedFaceGroupId)
		{
			EngineApplicationInterface.IScene.SwapFaceConnectionsWithId(base.Pointer, hubFaceGroupID, toBeSeparatedFaceGroupId, toBeMergedFaceGroupId);
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x0000A4CD File Offset: 0x000086CD
		public void MergeFacesWithId(int faceGroupId0, int faceGroupId1, int newFaceGroupId)
		{
			EngineApplicationInterface.IScene.MergeFacesWithId(base.Pointer, faceGroupId0, faceGroupId1, newFaceGroupId);
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0000A4E2 File Offset: 0x000086E2
		public void SeparateFacesWithId(int faceGroupId0, int faceGroupId1)
		{
			EngineApplicationInterface.IScene.SeparateFacesWithId(base.Pointer, faceGroupId0, faceGroupId1);
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x0000A4F6 File Offset: 0x000086F6
		public bool IsAnyFaceWithId(int faceGroupId)
		{
			return EngineApplicationInterface.IScene.IsAnyFaceWithId(base.Pointer, faceGroupId);
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0000A50C File Offset: 0x0000870C
		public bool GetNavigationMeshForPosition(ref Vec3 position)
		{
			int num;
			return this.GetNavigationMeshForPosition(ref position, out num, 1.5f);
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x0000A527 File Offset: 0x00008727
		public bool GetNavigationMeshForPosition(ref Vec3 position, out int faceGroupId, float heightDifferenceLimit = 1.5f)
		{
			faceGroupId = int.MinValue;
			return EngineApplicationInterface.IScene.GetNavigationMeshFaceForPosition(base.Pointer, ref position, ref faceGroupId, heightDifferenceLimit);
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x0000A543 File Offset: 0x00008743
		public bool DoesPathExistBetweenPositions(WorldPosition position, WorldPosition destination)
		{
			return EngineApplicationInterface.IScene.DoesPathExistBetweenPositions(base.Pointer, position, destination);
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x0000A557 File Offset: 0x00008757
		public void SetLandscapeRainMaskData(byte[] data)
		{
			EngineApplicationInterface.IScene.SetLandscapeRainMaskData(base.Pointer, data);
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x0000A56A File Offset: 0x0000876A
		public void EnsurePostfxSystem()
		{
			EngineApplicationInterface.IScene.EnsurePostfxSystem(base.Pointer);
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x0000A57C File Offset: 0x0000877C
		public void SetBloom(bool mode)
		{
			EngineApplicationInterface.IScene.SetBloom(base.Pointer, mode);
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x0000A58F File Offset: 0x0000878F
		public void SetDofMode(bool mode)
		{
			EngineApplicationInterface.IScene.SetDofMode(base.Pointer, mode);
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x0000A5A2 File Offset: 0x000087A2
		public void SetOcclusionMode(bool mode)
		{
			EngineApplicationInterface.IScene.SetOcclusionMode(base.Pointer, mode);
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0000A5B5 File Offset: 0x000087B5
		public void SetExternalInjectionTexture(Texture texture)
		{
			EngineApplicationInterface.IScene.SetExternalInjectionTexture(base.Pointer, texture.Pointer);
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0000A5CD File Offset: 0x000087CD
		public void SetSunshaftMode(bool mode)
		{
			EngineApplicationInterface.IScene.SetSunshaftMode(base.Pointer, mode);
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x0000A5E0 File Offset: 0x000087E0
		public Vec3 GetSunDirection()
		{
			return EngineApplicationInterface.IScene.GetSunDirection(base.Pointer);
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x0000A5F2 File Offset: 0x000087F2
		public float GetNorthAngle()
		{
			return EngineApplicationInterface.IScene.GetNorthAngle(base.Pointer);
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x0000A604 File Offset: 0x00008804
		public float GetNorthRotation()
		{
			float northAngle = this.GetNorthAngle();
			return 0.017453292f * -northAngle;
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x0000A620 File Offset: 0x00008820
		public bool GetTerrainMinMaxHeight(out float minHeight, out float maxHeight)
		{
			minHeight = 0f;
			maxHeight = 0f;
			return EngineApplicationInterface.IScene.GetTerrainMinMaxHeight(this, ref minHeight, ref maxHeight);
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x0000A63D File Offset: 0x0000883D
		public void GetPhysicsMinMax(ref Vec3 min_max)
		{
			EngineApplicationInterface.IScene.GetPhysicsMinMax(base.Pointer, ref min_max);
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0000A650 File Offset: 0x00008850
		public bool IsEditorScene()
		{
			return EngineApplicationInterface.IScene.IsEditorScene(base.Pointer);
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0000A662 File Offset: 0x00008862
		public void SetMotionBlurMode(bool mode)
		{
			EngineApplicationInterface.IScene.SetMotionBlurMode(base.Pointer, mode);
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0000A675 File Offset: 0x00008875
		public void SetAntialiasingMode(bool mode)
		{
			EngineApplicationInterface.IScene.SetAntialiasingMode(base.Pointer, mode);
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x0000A688 File Offset: 0x00008888
		public void SetDLSSMode(bool mode)
		{
			EngineApplicationInterface.IScene.SetDLSSMode(base.Pointer, mode);
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0000A69B File Offset: 0x0000889B
		public IEnumerable<GameEntity> FindEntitiesWithTag(string tag)
		{
			return GameEntity.GetEntitiesWithTag(this, tag);
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x0000A6A4 File Offset: 0x000088A4
		public GameEntity FindEntityWithTag(string tag)
		{
			return GameEntity.GetFirstEntityWithTag(this, tag);
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x0000A6AD File Offset: 0x000088AD
		public GameEntity FindEntityWithName(string name)
		{
			return GameEntity.GetFirstEntityWithName(this, name);
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x0000A6B6 File Offset: 0x000088B6
		public IEnumerable<GameEntity> FindEntitiesWithTagExpression(string expression)
		{
			return GameEntity.GetEntitiesWithTagExpression(this, expression);
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x0000A6BF File Offset: 0x000088BF
		public int GetSoftBoundaryVertexCount()
		{
			return EngineApplicationInterface.IScene.GetSoftBoundaryVertexCount(base.Pointer);
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x0000A6D1 File Offset: 0x000088D1
		public int GetHardBoundaryVertexCount()
		{
			return EngineApplicationInterface.IScene.GetHardBoundaryVertexCount(base.Pointer);
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x0000A6E3 File Offset: 0x000088E3
		public Vec2 GetSoftBoundaryVertex(int index)
		{
			return EngineApplicationInterface.IScene.GetSoftBoundaryVertex(base.Pointer, index);
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x0000A6F6 File Offset: 0x000088F6
		public Vec2 GetHardBoundaryVertex(int index)
		{
			return EngineApplicationInterface.IScene.GetHardBoundaryVertex(base.Pointer, index);
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x0000A709 File Offset: 0x00008909
		public Path GetPathWithName(string name)
		{
			return EngineApplicationInterface.IScene.GetPathWithName(base.Pointer, name);
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x0000A71C File Offset: 0x0000891C
		public void DeletePathWithName(string name)
		{
			EngineApplicationInterface.IScene.DeletePathWithName(base.Pointer, name);
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x0000A72F File Offset: 0x0000892F
		public void AddPath(string name)
		{
			EngineApplicationInterface.IScene.AddPath(base.Pointer, name);
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x0000A742 File Offset: 0x00008942
		public void AddPathPoint(string name, MatrixFrame frame)
		{
			EngineApplicationInterface.IScene.AddPathPoint(base.Pointer, name, ref frame);
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x0000A757 File Offset: 0x00008957
		public void GetBoundingBox(out Vec3 min, out Vec3 max)
		{
			min = Vec3.Invalid;
			max = Vec3.Invalid;
			EngineApplicationInterface.IScene.GetBoundingBox(base.Pointer, ref min, ref max);
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x0000A781 File Offset: 0x00008981
		public void SetName(string name)
		{
			EngineApplicationInterface.IScene.SetName(base.Pointer, name);
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x0000A794 File Offset: 0x00008994
		public string GetName()
		{
			return EngineApplicationInterface.IScene.GetName(base.Pointer);
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x0000A7A6 File Offset: 0x000089A6
		public string GetModulePath()
		{
			return EngineApplicationInterface.IScene.GetModulePath(base.Pointer);
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060009C8 RID: 2504 RVA: 0x0000A7B8 File Offset: 0x000089B8
		// (set) Token: 0x060009C9 RID: 2505 RVA: 0x0000A7CA File Offset: 0x000089CA
		public float TimeSpeed
		{
			get
			{
				return EngineApplicationInterface.IScene.GetTimeSpeed(base.Pointer);
			}
			set
			{
				EngineApplicationInterface.IScene.SetTimeSpeed(base.Pointer, value);
			}
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x0000A7DD File Offset: 0x000089DD
		public void SetOwnerThread()
		{
			EngineApplicationInterface.IScene.SetOwnerThread(base.Pointer);
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x0000A7F0 File Offset: 0x000089F0
		public Path[] GetPathsWithNamePrefix(string prefix)
		{
			int numberOfPathsWithNamePrefix = EngineApplicationInterface.IScene.GetNumberOfPathsWithNamePrefix(base.Pointer, prefix);
			UIntPtr[] array = new UIntPtr[numberOfPathsWithNamePrefix];
			EngineApplicationInterface.IScene.GetPathsWithNamePrefix(base.Pointer, array, prefix);
			Path[] array2 = new Path[numberOfPathsWithNamePrefix];
			for (int i = 0; i < numberOfPathsWithNamePrefix; i++)
			{
				UIntPtr pointer = array[i];
				array2[i] = new Path(pointer);
			}
			return array2;
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x0000A84B File Offset: 0x00008A4B
		public void SetUseConstantTime(bool value)
		{
			EngineApplicationInterface.IScene.SetUseConstantTime(base.Pointer, value);
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x0000A85E File Offset: 0x00008A5E
		public bool CheckPointCanSeePoint(Vec3 source, Vec3 target, float? distanceToCheck = null)
		{
			if (distanceToCheck == null)
			{
				distanceToCheck = new float?(source.Distance(target));
			}
			return EngineApplicationInterface.IScene.CheckPointCanSeePoint(base.Pointer, source, target, distanceToCheck.Value);
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x0000A891 File Offset: 0x00008A91
		public void SetPlaySoundEventsAfterReadyToRender(bool value)
		{
			EngineApplicationInterface.IScene.SetPlaySoundEventsAfterReadyToRender(base.Pointer, value);
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x0000A8A4 File Offset: 0x00008AA4
		public void DisableStaticShadows(bool value)
		{
			EngineApplicationInterface.IScene.DisableStaticShadows(base.Pointer, value);
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0000A8B7 File Offset: 0x00008AB7
		public Mesh GetSkyboxMesh()
		{
			return EngineApplicationInterface.IScene.GetSkyboxMesh(base.Pointer);
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x0000A8C9 File Offset: 0x00008AC9
		public void SetAtmosphereWithName(string name)
		{
			EngineApplicationInterface.IScene.SetAtmosphereWithName(base.Pointer, name);
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x0000A8DC File Offset: 0x00008ADC
		public void FillEntityWithHardBorderPhysicsBarrier(GameEntity entity)
		{
			EngineApplicationInterface.IScene.FillEntityWithHardBorderPhysicsBarrier(base.Pointer, entity.Pointer);
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x0000A8F4 File Offset: 0x00008AF4
		public void ClearDecals()
		{
			EngineApplicationInterface.IScene.ClearDecals(base.Pointer);
		}

		// Token: 0x04000196 RID: 406
		public const float AutoClimbHeight = 1.5f;

		// Token: 0x04000197 RID: 407
		public const float NavMeshHeightLimit = 1.5f;

		// Token: 0x04000198 RID: 408
		public static readonly TWSharedMutex PhysicsAndRayCastLock = new TWSharedMutex();
	}
}
