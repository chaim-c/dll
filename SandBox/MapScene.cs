using System;
using System.Collections.Generic;
using System.Threading;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Map;
using TaleWorlds.Core;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace SandBox
{
	// Token: 0x0200001B RID: 27
	public class MapScene : IMapScene
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00004119 File Offset: 0x00002319
		public Scene Scene
		{
			get
			{
				return this._scene;
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00004121 File Offset: 0x00002321
		public MapScene()
		{
			this._sharedLock = new ReaderWriterLockSlim();
			this._sceneLevels = new Dictionary<string, uint>();
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000413F File Offset: 0x0000233F
		public Vec2 GetTerrainSize()
		{
			return this._terrainSize;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00004148 File Offset: 0x00002348
		public uint GetSceneLevel(string name)
		{
			this._sharedLock.EnterReadLock();
			uint num;
			bool flag = this._sceneLevels.TryGetValue(name, out num) && num != 2147483647U;
			this._sharedLock.ExitReadLock();
			if (flag)
			{
				return num;
			}
			uint upgradeLevelMaskOfLevelName = this._scene.GetUpgradeLevelMaskOfLevelName(name);
			this._sharedLock.EnterWriteLock();
			this._sceneLevels[name] = upgradeLevelMaskOfLevelName;
			this._sharedLock.ExitWriteLock();
			return upgradeLevelMaskOfLevelName;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000041C0 File Offset: 0x000023C0
		public void SetSceneLevels(List<string> levels)
		{
			foreach (string key in levels)
			{
				this._sceneLevels.Add(key, 2147483647U);
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00004218 File Offset: 0x00002418
		public List<AtmosphereState> GetAtmosphereStates()
		{
			List<AtmosphereState> list = new List<AtmosphereState>();
			foreach (GameEntity gameEntity in this.Scene.FindEntitiesWithTag("atmosphere_probe"))
			{
				MapAtmosphereProbe firstScriptOfType = gameEntity.GetFirstScriptOfType<MapAtmosphereProbe>();
				Vec3 globalPosition = gameEntity.GlobalPosition;
				AtmosphereState item = new AtmosphereState
				{
					Position = globalPosition,
					HumidityAverage = firstScriptOfType.rainDensity,
					HumidityVariance = 5f,
					TemperatureAverage = firstScriptOfType.temperature,
					TemperatureVariance = 5f,
					distanceForMaxWeight = firstScriptOfType.minRadius,
					distanceForMinWeight = firstScriptOfType.maxRadius,
					ColorGradeTexture = firstScriptOfType.colorGrade
				};
				list.Add(item);
			}
			return list;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000042E8 File Offset: 0x000024E8
		public void ValidateAgentVisualsReseted()
		{
			if (this._scene != null && this._agentRendererSceneController != null)
			{
				MBAgentRendererSceneController.ValidateAgentVisualsReseted(this._scene, this._agentRendererSceneController);
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00004311 File Offset: 0x00002511
		public void SetAtmosphereColorgrade(TerrainType terrainType)
		{
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00004314 File Offset: 0x00002514
		public void AddNewEntityToMapScene(string entityId, Vec2 position)
		{
			GameEntity gameEntity = GameEntity.Instantiate(this._scene, entityId, true);
			if (gameEntity != null)
			{
				gameEntity.SetLocalPosition(new Vec3(position.x, position.y, 0f, -1f)
				{
					z = this._scene.GetGroundHeightAtPosition(position.ToVec3(0f), BodyFlags.CommonCollisionExcludeFlags)
				});
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000437F File Offset: 0x0000257F
		public void GetFaceIndexForMultiplePositions(int movedPartyCount, float[] positionArray, PathFaceRecord[] resultArray)
		{
			MBMapScene.GetFaceIndexForMultiplePositions(this._scene, movedPartyCount, positionArray, resultArray, false, true);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00004394 File Offset: 0x00002594
		public void GetMapBorders(out Vec2 minimumPosition, out Vec2 maximumPosition, out float maximumHeight)
		{
			GameEntity firstEntityWithName = this._scene.GetFirstEntityWithName("border_min");
			GameEntity firstEntityWithName2 = this._scene.GetFirstEntityWithName("border_max");
			Vec2 vec;
			if (!(firstEntityWithName != null))
			{
				vec = Vec2.Zero;
			}
			else
			{
				MatrixFrame globalFrame = firstEntityWithName.GetGlobalFrame();
				vec = globalFrame.origin.AsVec2;
			}
			minimumPosition = vec;
			Vec2 vec2;
			if (!(firstEntityWithName2 != null))
			{
				vec2 = new Vec2(900f, 900f);
			}
			else
			{
				MatrixFrame globalFrame = firstEntityWithName2.GetGlobalFrame();
				vec2 = globalFrame.origin.AsVec2;
			}
			maximumPosition = vec2;
			maximumHeight = ((firstEntityWithName2 != null) ? firstEntityWithName2.GetGlobalFrame().origin.z : 670f);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00004444 File Offset: 0x00002644
		public void Load()
		{
			Debug.Print("Creating map scene", 0, Debug.DebugColor.White, 17592186044416UL);
			this._scene = Scene.CreateNewScene(false, true, DecalAtlasGroup.Worldmap, "MapScene");
			this._scene.SetClothSimulationState(true);
			this._agentRendererSceneController = MBAgentRendererSceneController.CreateNewAgentRendererSceneController(this._scene, 4096);
			this._agentRendererSceneController.SetDoTimerBasedForcedSkeletonUpdates(false);
			this._scene.SetOcclusionMode(true);
			SceneInitializationData sceneInitializationData = new SceneInitializationData(true);
			sceneInitializationData.UsePhysicsMaterials = false;
			sceneInitializationData.EnableFloraPhysics = false;
			sceneInitializationData.UseTerrainMeshBlending = false;
			sceneInitializationData.CreateOros = false;
			Debug.Print("Reading map scene", 0, Debug.DebugColor.White, 17592186044416UL);
			this._scene.Read("Main_map", ref sceneInitializationData, "");
			Utilities.SetAllocationAlwaysValidScene(this._scene);
			this._scene.DisableStaticShadows(true);
			this._scene.InvalidateTerrainPhysicsMaterials();
			this.LoadAtmosphereData(this._scene);
			this.DisableUnwalkableNavigationMeshes();
			MBMapScene.ValidateTerrainSoundIds();
			this._scene.OptimizeScene(true, false);
			Vec2i vec2i;
			float num;
			int num2;
			int num3;
			this._scene.GetTerrainData(out vec2i, out num, out num2, out num3);
			this._terrainSize.x = (float)vec2i.X * num;
			this._terrainSize.y = (float)vec2i.Y * num;
			MBMapScene.GetBattleSceneIndexMap(this._scene, ref this._battleTerrainIndexMap, ref this._battleTerrainIndexMapWidth, ref this._battleTerrainIndexMapHeight);
			Debug.Print("Ticking map scene for first initialization", 0, Debug.DebugColor.White, 17592186044416UL);
			this._scene.Tick(0.1f);
			AsyncTask campaignLateAITickTask = AsyncTask.CreateWithDelegate(new ManagedDelegate
			{
				Instance = new ManagedDelegate.DelegateDefinition(Campaign.LateAITick)
			}, false);
			Campaign.Current.CampaignLateAITickTask = campaignLateAITickTask;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000045F8 File Offset: 0x000027F8
		public void Destroy()
		{
			MBAgentRendererSceneController.DestructAgentRendererSceneController(this._scene, this._agentRendererSceneController, false);
			this._agentRendererSceneController = null;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00004614 File Offset: 0x00002814
		public void DisableUnwalkableNavigationMeshes()
		{
			this.Scene.SetAbilityOfFacesWithId(MapScene.GetNavigationMeshIndexOfTerrainType(TerrainType.Mountain), false);
			this.Scene.SetAbilityOfFacesWithId(MapScene.GetNavigationMeshIndexOfTerrainType(TerrainType.Lake), false);
			this.Scene.SetAbilityOfFacesWithId(MapScene.GetNavigationMeshIndexOfTerrainType(TerrainType.Water), false);
			this.Scene.SetAbilityOfFacesWithId(MapScene.GetNavigationMeshIndexOfTerrainType(TerrainType.River), false);
			this.Scene.SetAbilityOfFacesWithId(MapScene.GetNavigationMeshIndexOfTerrainType(TerrainType.Canyon), false);
			this.Scene.SetAbilityOfFacesWithId(MapScene.GetNavigationMeshIndexOfTerrainType(TerrainType.RuralArea), false);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00004694 File Offset: 0x00002894
		public PathFaceRecord GetFaceIndex(Vec2 position)
		{
			PathFaceRecord result = new PathFaceRecord(-1, -1, -1);
			this._scene.GetNavMeshFaceIndex(ref result, position, false, true);
			return result;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000046BC File Offset: 0x000028BC
		public bool AreFacesOnSameIsland(PathFaceRecord startingFace, PathFaceRecord endFace, bool ignoreDisabled)
		{
			return this._scene.DoesPathExistBetweenFaces(startingFace.FaceIndex, endFace.FaceIndex, ignoreDisabled);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000046D6 File Offset: 0x000028D6
		private void LoadAtmosphereData(Scene mapScene)
		{
			MBMapScene.LoadAtmosphereData(mapScene);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000046E0 File Offset: 0x000028E0
		public TerrainType GetTerrainTypeAtPosition(Vec2 position)
		{
			PathFaceRecord faceIndex = this.GetFaceIndex(position);
			return this.GetFaceTerrainType(faceIndex);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000046FC File Offset: 0x000028FC
		public TerrainType GetFaceTerrainType(PathFaceRecord navMeshFace)
		{
			if (!navMeshFace.IsValid())
			{
				Debug.FailedAssert("Null nav mesh face tried to get terrain type.", "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox\\MapScene.cs", "GetFaceTerrainType", 255);
				return TerrainType.Plain;
			}
			switch (navMeshFace.FaceGroupIndex)
			{
			case 1:
				return TerrainType.Plain;
			case 2:
				return TerrainType.Desert;
			case 3:
				return TerrainType.Snow;
			case 4:
				return TerrainType.Forest;
			case 5:
				return TerrainType.Steppe;
			case 6:
				return TerrainType.Fording;
			case 7:
				return TerrainType.Mountain;
			case 8:
				return TerrainType.Lake;
			case 10:
				return TerrainType.Water;
			case 11:
				return TerrainType.River;
			case 13:
				return TerrainType.Canyon;
			case 14:
				return TerrainType.RuralArea;
			}
			return TerrainType.Plain;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00004798 File Offset: 0x00002998
		public static int GetNavigationMeshIndexOfTerrainType(TerrainType terrainType)
		{
			switch (terrainType)
			{
			case TerrainType.Water:
				return 10;
			case TerrainType.Mountain:
				return 7;
			case TerrainType.Snow:
				return 3;
			case TerrainType.Steppe:
				return 5;
			case TerrainType.Plain:
				return 1;
			case TerrainType.Desert:
				return 2;
			case TerrainType.River:
				return 11;
			case TerrainType.Forest:
				return 4;
			case TerrainType.Fording:
				return 6;
			case TerrainType.Lake:
				return 8;
			case TerrainType.Canyon:
				return 13;
			case TerrainType.RuralArea:
				return 14;
			}
			return -1;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00004808 File Offset: 0x00002A08
		public List<TerrainType> GetEnvironmentTerrainTypes(Vec2 position)
		{
			List<TerrainType> list = new List<TerrainType>();
			Vec2 v = new Vec2(1f, 0f);
			list.Add(this.GetTerrainTypeAtPosition(position));
			for (int i = 0; i < 8; i++)
			{
				v.RotateCCW(0.7853982f * (float)i);
				for (int j = 1; j < 7; j++)
				{
					TerrainType terrainTypeAtPosition = this.GetTerrainTypeAtPosition(position + (float)j * v);
					this.GetFaceIndex(position + (float)j * v);
					if (!list.Contains(terrainTypeAtPosition))
					{
						list.Add(terrainTypeAtPosition);
					}
				}
			}
			return list;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000048A0 File Offset: 0x00002AA0
		public List<TerrainType> GetEnvironmentTerrainTypesCount(Vec2 position, out TerrainType currentPositionTerrainType)
		{
			List<TerrainType> list = new List<TerrainType>();
			Vec2 v = new Vec2(1f, 0f);
			currentPositionTerrainType = this.GetTerrainTypeAtPosition(position);
			list.Add(currentPositionTerrainType);
			for (int i = 0; i < 8; i++)
			{
				v.RotateCCW(0.7853982f * (float)i);
				for (int j = 1; j < 7; j++)
				{
					PathFaceRecord faceIndex = Campaign.Current.MapSceneWrapper.GetFaceIndex(position + (float)j * v);
					if (faceIndex.IsValid())
					{
						TerrainType faceTerrainType = this.GetFaceTerrainType(faceIndex);
						list.Add(faceTerrainType);
					}
				}
			}
			return list;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00004938 File Offset: 0x00002B38
		public MapPatchData GetMapPatchAtPosition(Vec2 position)
		{
			if (this._battleTerrainIndexMap != null)
			{
				int num = MathF.Floor(position.x / this._terrainSize.X * (float)this._battleTerrainIndexMapWidth);
				int value = MathF.Floor(position.y / this._terrainSize.Y * (float)this._battleTerrainIndexMapHeight);
				num = MBMath.ClampIndex(num, 0, this._battleTerrainIndexMapWidth);
				int num2 = (MBMath.ClampIndex(value, 0, this._battleTerrainIndexMapHeight) * this._battleTerrainIndexMapWidth + num) * 2;
				byte sceneIndex = this._battleTerrainIndexMap[num2];
				byte b = this._battleTerrainIndexMap[num2 + 1];
				Vec2 normalizedCoordinates = new Vec2((float)(b & 15) / 15f, (float)(b >> 4 & 15) / 15f);
				return new MapPatchData
				{
					sceneIndex = (int)sceneIndex,
					normalizedCoordinates = normalizedCoordinates
				};
			}
			return default(MapPatchData);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00004A0E File Offset: 0x00002C0E
		public Vec2 GetAccessiblePointNearPosition(Vec2 position, float radius)
		{
			return MBMapScene.GetAccessiblePointNearPosition(this._scene, position, radius);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004A20 File Offset: 0x00002C20
		public bool GetPathBetweenAIFaces(PathFaceRecord startingFace, PathFaceRecord endingFace, Vec2 startingPosition, Vec2 endingPosition, float agentRadius, NavigationPath path, int[] excludedFaceIds = null)
		{
			return this._scene.GetPathBetweenAIFaces(startingFace.FaceIndex, endingFace.FaceIndex, startingPosition, endingPosition, agentRadius, path, excludedFaceIds, 1f);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00004A52 File Offset: 0x00002C52
		public bool GetPathDistanceBetweenAIFaces(PathFaceRecord startingAiFace, PathFaceRecord endingAiFace, Vec2 startingPosition, Vec2 endingPosition, float agentRadius, float distanceLimit, out float distance)
		{
			return this._scene.GetPathDistanceBetweenAIFaces(startingAiFace.FaceIndex, endingAiFace.FaceIndex, startingPosition, endingPosition, agentRadius, distanceLimit, out distance);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00004A74 File Offset: 0x00002C74
		public bool IsLineToPointClear(PathFaceRecord startingFace, Vec2 position, Vec2 destination, float agentRadius)
		{
			return this._scene.IsLineToPointClear(startingFace.FaceIndex, position, destination, agentRadius);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00004A8B File Offset: 0x00002C8B
		public Vec2 GetLastPointOnNavigationMeshFromPositionToDestination(PathFaceRecord startingFace, Vec2 position, Vec2 destination)
		{
			return this._scene.GetLastPointOnNavigationMeshFromPositionToDestination(startingFace.FaceIndex, position, destination);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00004AA0 File Offset: 0x00002CA0
		public Vec2 GetNavigationMeshCenterPosition(PathFaceRecord face)
		{
			Vec3 zero = Vec3.Zero;
			this._scene.GetNavMeshCenterPosition(face.FaceIndex, ref zero);
			return zero.AsVec2;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004ACD File Offset: 0x00002CCD
		public int GetNumberOfNavigationMeshFaces()
		{
			return this._scene.GetNavMeshFaceCount();
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00004ADA File Offset: 0x00002CDA
		public bool GetHeightAtPoint(Vec2 point, ref float height)
		{
			return this._scene.GetHeightAtPoint(point, BodyFlags.CommonCollisionExcludeFlags, ref height);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00004AEE File Offset: 0x00002CEE
		public float GetWinterTimeFactor()
		{
			return this._scene.GetWinterTimeFactor();
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00004AFB File Offset: 0x00002CFB
		public float GetFaceVertexZ(PathFaceRecord navMeshFace)
		{
			return this._scene.GetNavMeshFaceFirstVertexZ(navMeshFace.FaceIndex);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00004B0E File Offset: 0x00002D0E
		public Vec3 GetGroundNormal(Vec2 position)
		{
			return this._scene.GetNormalAt(position);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00004B1C File Offset: 0x00002D1C
		public void GetTerrainHeightAndNormal(Vec2 position, out float height, out Vec3 normal)
		{
			this._scene.GetTerrainHeightAndNormal(position, out height, out normal);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00004B2C File Offset: 0x00002D2C
		public string GetTerrainTypeName(TerrainType type)
		{
			string result = "Invalid";
			switch (type)
			{
			case TerrainType.Water:
				result = "Water";
				break;
			case TerrainType.Mountain:
				result = "Mountain";
				break;
			case TerrainType.Snow:
				result = "Snow";
				break;
			case TerrainType.Steppe:
				result = "Steppe";
				break;
			case TerrainType.Plain:
				result = "Plain";
				break;
			case TerrainType.Desert:
				result = "Desert";
				break;
			case TerrainType.Swamp:
				result = "Swamp";
				break;
			case TerrainType.Dune:
				result = "Dune";
				break;
			case TerrainType.Bridge:
				result = "Bridge";
				break;
			case TerrainType.River:
				result = "River";
				break;
			case TerrainType.Forest:
				result = "Forest";
				break;
			case TerrainType.Fording:
				result = "Fording";
				break;
			case TerrainType.Lake:
				result = "Lake";
				break;
			case TerrainType.Canyon:
				result = "Canyon";
				break;
			}
			return result;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004BEE File Offset: 0x00002DEE
		public void GetSnowAmountData(byte[] snowData)
		{
			this._scene.GetSnowAmountData(snowData);
		}

		// Token: 0x04000028 RID: 40
		private Scene _scene;

		// Token: 0x04000029 RID: 41
		private MBAgentRendererSceneController _agentRendererSceneController;

		// Token: 0x0400002A RID: 42
		private Dictionary<string, uint> _sceneLevels;

		// Token: 0x0400002B RID: 43
		private int _battleTerrainIndexMapWidth;

		// Token: 0x0400002C RID: 44
		private int _battleTerrainIndexMapHeight;

		// Token: 0x0400002D RID: 45
		private byte[] _battleTerrainIndexMap;

		// Token: 0x0400002E RID: 46
		private Vec2 _terrainSize;

		// Token: 0x0400002F RID: 47
		private ReaderWriterLockSlim _sharedLock;
	}
}
