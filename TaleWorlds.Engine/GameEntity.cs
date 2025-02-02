using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000049 RID: 73
	[EngineClass("rglEntity")]
	public sealed class GameEntity : NativeObject
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x00003820 File Offset: 0x00001A20
		public Scene Scene
		{
			get
			{
				return EngineApplicationInterface.IGameEntity.GetScene(base.Pointer);
			}
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x00003832 File Offset: 0x00001A32
		private GameEntity()
		{
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0000383A File Offset: 0x00001A3A
		internal GameEntity(UIntPtr pointer)
		{
			base.Construct(pointer);
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00003849 File Offset: 0x00001A49
		public UIntPtr GetScenePointer()
		{
			return EngineApplicationInterface.IGameEntity.GetScenePointer(base.Pointer);
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0000385C File Offset: 0x00001A5C
		public override string ToString()
		{
			return base.Pointer.ToString();
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00003877 File Offset: 0x00001A77
		public void ClearEntityComponents(bool resetAll, bool removeScripts, bool deleteChildEntities)
		{
			EngineApplicationInterface.IGameEntity.ClearEntityComponents(base.Pointer, resetAll, removeScripts, deleteChildEntities);
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0000388C File Offset: 0x00001A8C
		public void ClearComponents()
		{
			EngineApplicationInterface.IGameEntity.ClearComponents(base.Pointer);
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0000389E File Offset: 0x00001A9E
		public void ClearOnlyOwnComponents()
		{
			EngineApplicationInterface.IGameEntity.ClearOnlyOwnComponents(base.Pointer);
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x000038B0 File Offset: 0x00001AB0
		public bool CheckResources(bool addToQueue, bool checkFaceResources)
		{
			return EngineApplicationInterface.IGameEntity.CheckResources(base.Pointer, addToQueue, checkFaceResources);
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x000038C4 File Offset: 0x00001AC4
		public void SetMobility(GameEntity.Mobility mobility)
		{
			EngineApplicationInterface.IGameEntity.SetMobility(base.Pointer, (int)mobility);
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x000038D7 File Offset: 0x00001AD7
		public void AddMesh(Mesh mesh, bool recomputeBoundingBox = true)
		{
			EngineApplicationInterface.IGameEntity.AddMesh(base.Pointer, mesh.Pointer, recomputeBoundingBox);
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x000038F0 File Offset: 0x00001AF0
		public void AddMultiMeshToSkeleton(MetaMesh metaMesh)
		{
			EngineApplicationInterface.IGameEntity.AddMultiMeshToSkeleton(base.Pointer, metaMesh.Pointer);
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00003908 File Offset: 0x00001B08
		public void AddMultiMeshToSkeletonBone(MetaMesh metaMesh, sbyte boneIndex)
		{
			EngineApplicationInterface.IGameEntity.AddMultiMeshToSkeletonBone(base.Pointer, metaMesh.Pointer, boneIndex);
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00003921 File Offset: 0x00001B21
		public IEnumerable<Mesh> GetAllMeshesWithTag(string tag)
		{
			List<GameEntity> list = new List<GameEntity>();
			this.GetChildrenRecursive(ref list);
			list.Add(this);
			foreach (GameEntity entity in list)
			{
				int num;
				for (int i = 0; i < entity.MultiMeshComponentCount; i = num + 1)
				{
					MetaMesh multiMesh = entity.GetMetaMesh(i);
					for (int j = 0; j < multiMesh.MeshCount; j = num + 1)
					{
						Mesh meshAtIndex = multiMesh.GetMeshAtIndex(j);
						if (meshAtIndex.HasTag(tag))
						{
							yield return meshAtIndex;
						}
						num = j;
					}
					multiMesh = null;
					num = i;
				}
				for (int i = 0; i < entity.ClothSimulatorComponentCount; i = num + 1)
				{
					ClothSimulatorComponent clothSimulator = entity.GetClothSimulator(i);
					MetaMesh multiMesh = clothSimulator.GetFirstMetaMesh();
					for (int j = 0; j < multiMesh.MeshCount; j = num + 1)
					{
						Mesh meshAtIndex2 = multiMesh.GetMeshAtIndex(j);
						if (meshAtIndex2.HasTag(tag))
						{
							yield return meshAtIndex2;
						}
						num = j;
					}
					multiMesh = null;
					num = i;
				}
				entity = null;
			}
			List<GameEntity>.Enumerator enumerator = default(List<GameEntity>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00003938 File Offset: 0x00001B38
		public void SetColor(uint color1, uint color2, string meshTag)
		{
			foreach (Mesh mesh in this.GetAllMeshesWithTag(meshTag))
			{
				mesh.Color = color1;
				mesh.Color2 = color2;
			}
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0000398C File Offset: 0x00001B8C
		public uint GetFactorColor()
		{
			return EngineApplicationInterface.IGameEntity.GetFactorColor(base.Pointer);
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0000399E File Offset: 0x00001B9E
		public void SetFactorColor(uint color)
		{
			EngineApplicationInterface.IGameEntity.SetFactorColor(base.Pointer, color);
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x000039B1 File Offset: 0x00001BB1
		public void SetAsReplayEntity()
		{
			EngineApplicationInterface.IGameEntity.SetAsReplayEntity(base.Pointer);
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x000039C3 File Offset: 0x00001BC3
		public void SetClothMaxDistanceMultiplier(float multiplier)
		{
			EngineApplicationInterface.IGameEntity.SetClothMaxDistanceMultiplier(base.Pointer, multiplier);
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x000039D6 File Offset: 0x00001BD6
		public void RemoveMultiMeshFromSkeleton(MetaMesh metaMesh)
		{
			EngineApplicationInterface.IGameEntity.RemoveMultiMeshFromSkeleton(base.Pointer, metaMesh.Pointer);
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x000039EE File Offset: 0x00001BEE
		public void RemoveMultiMeshFromSkeletonBone(MetaMesh metaMesh, sbyte boneIndex)
		{
			EngineApplicationInterface.IGameEntity.RemoveMultiMeshFromSkeletonBone(base.Pointer, metaMesh.Pointer, boneIndex);
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00003A07 File Offset: 0x00001C07
		public bool RemoveComponentWithMesh(Mesh mesh)
		{
			return EngineApplicationInterface.IGameEntity.RemoveComponentWithMesh(base.Pointer, mesh.Pointer);
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00003A1F File Offset: 0x00001C1F
		public void AddComponent(GameEntityComponent component)
		{
			EngineApplicationInterface.IGameEntity.AddComponent(base.Pointer, component.Pointer);
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x00003A37 File Offset: 0x00001C37
		public bool HasComponent(GameEntityComponent component)
		{
			return EngineApplicationInterface.IGameEntity.HasComponent(base.Pointer, component.Pointer);
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00003A4F File Offset: 0x00001C4F
		public bool RemoveComponent(GameEntityComponent component)
		{
			return EngineApplicationInterface.IGameEntity.RemoveComponent(base.Pointer, component.Pointer);
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00003A67 File Offset: 0x00001C67
		public string GetGuid()
		{
			return EngineApplicationInterface.IGameEntity.GetGuid(base.Pointer);
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x00003A79 File Offset: 0x00001C79
		public bool IsGuidValid()
		{
			return EngineApplicationInterface.IGameEntity.IsGuidValid(base.Pointer);
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00003A8B File Offset: 0x00001C8B
		public void SetEnforcedMaximumLodLevel(int lodLevel)
		{
			EngineApplicationInterface.IGameEntity.SetEnforcedMaximumLodLevel(base.Pointer, lodLevel);
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x00003A9E File Offset: 0x00001C9E
		public float GetLodLevelForDistanceSq(float distSq)
		{
			return EngineApplicationInterface.IGameEntity.GetLodLevelForDistanceSq(base.Pointer, distSq);
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x00003AB1 File Offset: 0x00001CB1
		public void GetQuickBoneEntitialFrame(sbyte index, ref MatrixFrame frame)
		{
			EngineApplicationInterface.IGameEntity.GetQuickBoneEntitialFrame(base.Pointer, index, ref frame);
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00003AC5 File Offset: 0x00001CC5
		public void UpdateVisibilityMask()
		{
			EngineApplicationInterface.IGameEntity.UpdateVisibilityMask(base.Pointer);
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x00003AD7 File Offset: 0x00001CD7
		public static GameEntity CreateEmpty(Scene scene, bool isModifiableFromEditor = true)
		{
			return EngineApplicationInterface.IGameEntity.CreateEmpty(scene.Pointer, isModifiableFromEditor, (UIntPtr)0UL);
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00003AF1 File Offset: 0x00001CF1
		public static GameEntity CreateEmptyDynamic(Scene scene, bool isModifiableFromEditor = true)
		{
			GameEntity gameEntity = GameEntity.CreateEmpty(scene, isModifiableFromEditor);
			gameEntity.SetMobility(GameEntity.Mobility.dynamic);
			return gameEntity;
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00003B01 File Offset: 0x00001D01
		public static GameEntity CreateEmptyWithoutScene()
		{
			return EngineApplicationInterface.IGameEntity.CreateEmptyWithoutScene();
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00003B0D File Offset: 0x00001D0D
		public static GameEntity CopyFrom(Scene scene, GameEntity entity)
		{
			return EngineApplicationInterface.IGameEntity.CreateEmpty(scene.Pointer, false, entity.Pointer);
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00003B26 File Offset: 0x00001D26
		public static GameEntity Instantiate(Scene scene, string prefabName, bool callScriptCallbacks)
		{
			if (scene != null)
			{
				return EngineApplicationInterface.IGameEntity.CreateFromPrefab(scene.Pointer, prefabName, callScriptCallbacks);
			}
			return EngineApplicationInterface.IGameEntity.CreateFromPrefab(new UIntPtr(0U), prefabName, callScriptCallbacks);
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00003B56 File Offset: 0x00001D56
		public void CallScriptCallbacks()
		{
			EngineApplicationInterface.IGameEntity.CallScriptCallbacks(base.Pointer);
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00003B68 File Offset: 0x00001D68
		public static GameEntity Instantiate(Scene scene, string prefabName, MatrixFrame frame)
		{
			return EngineApplicationInterface.IGameEntity.CreateFromPrefabWithInitialFrame(scene.Pointer, prefabName, ref frame);
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x00003B7D File Offset: 0x00001D7D
		private int ScriptCount
		{
			get
			{
				return EngineApplicationInterface.IGameEntity.GetScriptComponentCount(base.Pointer);
			}
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00003B8F File Offset: 0x00001D8F
		public bool IsGhostObject()
		{
			return EngineApplicationInterface.IGameEntity.IsGhostObject(base.Pointer);
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00003BA1 File Offset: 0x00001DA1
		public void CreateAndAddScriptComponent(string name)
		{
			EngineApplicationInterface.IGameEntity.CreateAndAddScriptComponent(base.Pointer, name);
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00003BB4 File Offset: 0x00001DB4
		public static bool PrefabExists(string name)
		{
			return EngineApplicationInterface.IGameEntity.PrefabExists(name);
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00003BC1 File Offset: 0x00001DC1
		public void RemoveScriptComponent(UIntPtr scriptComponent, int removeReason)
		{
			EngineApplicationInterface.IGameEntity.RemoveScriptComponent(base.Pointer, scriptComponent, removeReason);
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00003BD5 File Offset: 0x00001DD5
		public void SetEntityEnvMapVisibility(bool value)
		{
			EngineApplicationInterface.IGameEntity.SetEntityEnvMapVisibility(base.Pointer, value);
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00003BE8 File Offset: 0x00001DE8
		internal ScriptComponentBehavior GetScriptAtIndex(int index)
		{
			return EngineApplicationInterface.IGameEntity.GetScriptComponentAtIndex(base.Pointer, index);
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00003BFB File Offset: 0x00001DFB
		public bool HasScene()
		{
			return EngineApplicationInterface.IGameEntity.HasScene(base.Pointer);
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00003C0D File Offset: 0x00001E0D
		public bool HasScriptComponent(string scName)
		{
			return EngineApplicationInterface.IGameEntity.HasScriptComponent(base.Pointer, scName);
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00003C20 File Offset: 0x00001E20
		public IEnumerable<ScriptComponentBehavior> GetScriptComponents()
		{
			int count = this.ScriptCount;
			int num;
			for (int i = 0; i < count; i = num + 1)
			{
				yield return this.GetScriptAtIndex(i);
				num = i;
			}
			yield break;
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x00003C30 File Offset: 0x00001E30
		public IEnumerable<T> GetScriptComponents<T>() where T : ScriptComponentBehavior
		{
			int count = this.ScriptCount;
			int num;
			for (int i = 0; i < count; i = num + 1)
			{
				T t;
				if ((t = (this.GetScriptAtIndex(i) as T)) != null)
				{
					yield return t;
				}
				num = i;
			}
			yield break;
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00003C40 File Offset: 0x00001E40
		public bool HasScriptOfType<T>() where T : ScriptComponentBehavior
		{
			int scriptCount = this.ScriptCount;
			for (int i = 0; i < scriptCount; i++)
			{
				if (this.GetScriptAtIndex(i) is T)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x00003C74 File Offset: 0x00001E74
		public bool HasScriptOfType(Type t)
		{
			return this.GetScriptComponents().Any((ScriptComponentBehavior sc) => sc.GetType().IsAssignableFrom(t));
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00003CA8 File Offset: 0x00001EA8
		public T GetFirstScriptOfTypeInFamily<T>() where T : ScriptComponentBehavior
		{
			T firstScriptOfType = this.GetFirstScriptOfType<T>();
			GameEntity gameEntity = this;
			while (firstScriptOfType == null && gameEntity.Parent != null)
			{
				gameEntity = gameEntity.Parent;
				firstScriptOfType = gameEntity.GetFirstScriptOfType<T>();
			}
			return firstScriptOfType;
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00003CE8 File Offset: 0x00001EE8
		public T GetFirstScriptOfType<T>() where T : ScriptComponentBehavior
		{
			int scriptCount = this.ScriptCount;
			for (int i = 0; i < scriptCount; i++)
			{
				T result;
				if ((result = (this.GetScriptAtIndex(i) as T)) != null)
				{
					return result;
				}
			}
			return default(T);
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00003D2D File Offset: 0x00001F2D
		internal static GameEntity GetFirstEntityWithName(Scene scene, string entityName)
		{
			return EngineApplicationInterface.IGameEntity.FindWithName(scene.Pointer, entityName);
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x00003D40 File Offset: 0x00001F40
		// (set) Token: 0x06000615 RID: 1557 RVA: 0x00003D52 File Offset: 0x00001F52
		public string Name
		{
			get
			{
				return EngineApplicationInterface.IGameEntity.GetName(base.Pointer);
			}
			set
			{
				EngineApplicationInterface.IGameEntity.SetName(base.Pointer, value);
			}
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00003D65 File Offset: 0x00001F65
		public void SetAlpha(float alpha)
		{
			EngineApplicationInterface.IGameEntity.SetAlpha(base.Pointer, alpha);
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x00003D78 File Offset: 0x00001F78
		public void SetVisibilityExcludeParents(bool visible)
		{
			EngineApplicationInterface.IGameEntity.SetVisibilityExcludeParents(base.Pointer, visible);
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00003D8B File Offset: 0x00001F8B
		public void SetReadyToRender(bool ready)
		{
			EngineApplicationInterface.IGameEntity.SetReadyToRender(base.Pointer, ready);
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00003D9E File Offset: 0x00001F9E
		public bool GetVisibilityExcludeParents()
		{
			return EngineApplicationInterface.IGameEntity.GetVisibilityExcludeParents(base.Pointer);
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00003DB0 File Offset: 0x00001FB0
		public bool IsVisibleIncludeParents()
		{
			return EngineApplicationInterface.IGameEntity.IsVisibleIncludeParents(base.Pointer);
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00003DC2 File Offset: 0x00001FC2
		public uint GetVisibilityLevelMaskIncludingParents()
		{
			return EngineApplicationInterface.IGameEntity.GetVisibilityLevelMaskIncludingParents(base.Pointer);
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00003DD4 File Offset: 0x00001FD4
		public bool GetEditModeLevelVisibility()
		{
			return EngineApplicationInterface.IGameEntity.GetEditModeLevelVisibility(base.Pointer);
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x00003DE6 File Offset: 0x00001FE6
		public void Remove(int removeReason)
		{
			EngineApplicationInterface.IGameEntity.Remove(base.Pointer, removeReason);
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00003DF9 File Offset: 0x00001FF9
		internal static GameEntity GetFirstEntityWithTag(Scene scene, string tag)
		{
			return EngineApplicationInterface.IGameEntity.GetFirstEntityWithTag(scene.Pointer, tag);
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x00003E0C File Offset: 0x0000200C
		internal static GameEntity GetNextEntityWithTag(Scene scene, GameEntity startEntity, string tag)
		{
			if (!(startEntity == null))
			{
				return EngineApplicationInterface.IGameEntity.GetNextEntityWithTag(startEntity.Pointer, tag);
			}
			return GameEntity.GetFirstEntityWithTag(scene, tag);
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00003E30 File Offset: 0x00002030
		internal static GameEntity GetFirstEntityWithTagExpression(Scene scene, string tagExpression)
		{
			return EngineApplicationInterface.IGameEntity.GetFirstEntityWithTagExpression(scene.Pointer, tagExpression);
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00003E43 File Offset: 0x00002043
		internal static GameEntity GetNextEntityWithTagExpression(Scene scene, GameEntity startEntity, string tagExpression)
		{
			if (!(startEntity == null))
			{
				return EngineApplicationInterface.IGameEntity.GetNextEntityWithTagExpression(startEntity.Pointer, tagExpression);
			}
			return GameEntity.GetFirstEntityWithTagExpression(scene, tagExpression);
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00003E67 File Offset: 0x00002067
		internal static GameEntity GetNextPrefab(GameEntity current)
		{
			return EngineApplicationInterface.IGameEntity.GetNextPrefab((current == null) ? new UIntPtr(0U) : current.Pointer);
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00003E8A File Offset: 0x0000208A
		public static GameEntity CopyFromPrefab(GameEntity prefab)
		{
			if (!(prefab != null))
			{
				return null;
			}
			return EngineApplicationInterface.IGameEntity.CopyFromPrefab(prefab.Pointer);
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00003EA7 File Offset: 0x000020A7
		public void SetUpgradeLevelMask(GameEntity.UpgradeLevelMask mask)
		{
			EngineApplicationInterface.IGameEntity.SetUpgradeLevelMask(base.Pointer, (uint)mask);
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00003EBA File Offset: 0x000020BA
		public GameEntity.UpgradeLevelMask GetUpgradeLevelMask()
		{
			return (GameEntity.UpgradeLevelMask)EngineApplicationInterface.IGameEntity.GetUpgradeLevelMask(base.Pointer);
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00003ECC File Offset: 0x000020CC
		public GameEntity.UpgradeLevelMask GetUpgradeLevelMaskCumulative()
		{
			return (GameEntity.UpgradeLevelMask)EngineApplicationInterface.IGameEntity.GetUpgradeLevelMaskCumulative(base.Pointer);
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00003EE0 File Offset: 0x000020E0
		public int GetUpgradeLevelOfEntity()
		{
			int upgradeLevelMask = (int)this.GetUpgradeLevelMask();
			if ((upgradeLevelMask & 1) > 0)
			{
				return 0;
			}
			if ((upgradeLevelMask & 2) > 0)
			{
				return 1;
			}
			if ((upgradeLevelMask & 4) > 0)
			{
				return 2;
			}
			if ((upgradeLevelMask & 8) > 0)
			{
				return 3;
			}
			return -1;
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00003F15 File Offset: 0x00002115
		public string GetOldPrefabName()
		{
			return EngineApplicationInterface.IGameEntity.GetOldPrefabName(base.Pointer);
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x00003F27 File Offset: 0x00002127
		public string GetPrefabName()
		{
			return EngineApplicationInterface.IGameEntity.GetPrefabName(base.Pointer);
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x00003F39 File Offset: 0x00002139
		public void CopyScriptComponentFromAnotherEntity(GameEntity otherEntity, string scriptName)
		{
			EngineApplicationInterface.IGameEntity.CopyScriptComponentFromAnotherEntity(base.Pointer, otherEntity.Pointer, scriptName);
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x00003F52 File Offset: 0x00002152
		internal static IEnumerable<GameEntity> GetEntitiesWithTag(Scene scene, string tag)
		{
			GameEntity entity = GameEntity.GetFirstEntityWithTag(scene, tag);
			while (entity != null)
			{
				yield return entity;
				entity = GameEntity.GetNextEntityWithTag(scene, entity, tag);
			}
			yield break;
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00003F69 File Offset: 0x00002169
		internal static IEnumerable<GameEntity> GetEntitiesWithTagExpression(Scene scene, string tagExpression)
		{
			GameEntity entity = GameEntity.GetFirstEntityWithTagExpression(scene, tagExpression);
			while (entity != null)
			{
				yield return entity;
				entity = GameEntity.GetNextEntityWithTagExpression(scene, entity, tagExpression);
			}
			yield break;
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x00003F80 File Offset: 0x00002180
		public void SetFrame(ref MatrixFrame frame)
		{
			EngineApplicationInterface.IGameEntity.SetFrame(base.Pointer, ref frame);
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x00003F93 File Offset: 0x00002193
		public void SetClothComponentKeepState(MetaMesh metaMesh, bool state)
		{
			EngineApplicationInterface.IGameEntity.SetClothComponentKeepState(base.Pointer, metaMesh.Pointer, state);
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00003FAC File Offset: 0x000021AC
		public void SetClothComponentKeepStateOfAllMeshes(bool state)
		{
			EngineApplicationInterface.IGameEntity.SetClothComponentKeepStateOfAllMeshes(base.Pointer, state);
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x00003FBF File Offset: 0x000021BF
		public void SetPreviousFrameInvalid()
		{
			EngineApplicationInterface.IGameEntity.SetPreviousFrameInvalid(base.Pointer);
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x00003FD4 File Offset: 0x000021D4
		public MatrixFrame GetFrame()
		{
			MatrixFrame result = default(MatrixFrame);
			EngineApplicationInterface.IGameEntity.GetFrame(base.Pointer, ref result);
			return result;
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00003FFC File Offset: 0x000021FC
		public void GetFrame(out MatrixFrame frame)
		{
			frame = this.GetFrame();
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0000400A File Offset: 0x0000220A
		public void UpdateTriadFrameForEditor()
		{
			EngineApplicationInterface.IGameEntity.UpdateTriadFrameForEditor(base.Pointer);
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0000401C File Offset: 0x0000221C
		public void UpdateTriadFrameForEditorForAllChildren()
		{
			this.UpdateTriadFrameForEditor();
			List<GameEntity> list = new List<GameEntity>();
			this.GetChildrenRecursive(ref list);
			foreach (GameEntity gameEntity in list)
			{
				EngineApplicationInterface.IGameEntity.UpdateTriadFrameForEditor(gameEntity.Pointer);
			}
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00004088 File Offset: 0x00002288
		public MatrixFrame GetGlobalFrame()
		{
			MatrixFrame result;
			EngineApplicationInterface.IGameEntity.GetGlobalFrame(base.Pointer, out result);
			return result;
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x000040A8 File Offset: 0x000022A8
		public void SetGlobalFrame(in MatrixFrame frame)
		{
			EngineApplicationInterface.IGameEntity.SetGlobalFrame(base.Pointer, frame);
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x000040BC File Offset: 0x000022BC
		public void SetGlobalFrameMT(in MatrixFrame frame)
		{
			using (new TWSharedMutexReadLock(Scene.PhysicsAndRayCastLock))
			{
				EngineApplicationInterface.IGameEntity.SetGlobalFrame(base.Pointer, frame);
			}
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00004108 File Offset: 0x00002308
		public MatrixFrame GetPreviousGlobalFrame()
		{
			MatrixFrame result;
			EngineApplicationInterface.IGameEntity.GetPreviousGlobalFrame(base.Pointer, out result);
			return result;
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00004128 File Offset: 0x00002328
		public bool HasPhysicsBody()
		{
			return EngineApplicationInterface.IGameEntity.HasPhysicsBody(base.Pointer);
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0000413A File Offset: 0x0000233A
		public void SetLocalPosition(Vec3 position)
		{
			EngineApplicationInterface.IGameEntity.SetLocalPosition(base.Pointer, position);
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x0000414D File Offset: 0x0000234D
		// (set) Token: 0x0600063C RID: 1596 RVA: 0x0000415F File Offset: 0x0000235F
		public EntityFlags EntityFlags
		{
			get
			{
				return (EntityFlags)EngineApplicationInterface.IGameEntity.GetEntityFlags(base.Pointer);
			}
			set
			{
				EngineApplicationInterface.IGameEntity.SetEntityFlags(base.Pointer, (uint)value);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600063D RID: 1597 RVA: 0x00004172 File Offset: 0x00002372
		// (set) Token: 0x0600063E RID: 1598 RVA: 0x00004184 File Offset: 0x00002384
		public EntityVisibilityFlags EntityVisibilityFlags
		{
			get
			{
				return (EntityVisibilityFlags)EngineApplicationInterface.IGameEntity.GetEntityVisibilityFlags(base.Pointer);
			}
			set
			{
				EngineApplicationInterface.IGameEntity.SetEntityVisibilityFlags(base.Pointer, (uint)value);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600063F RID: 1599 RVA: 0x00004197 File Offset: 0x00002397
		// (set) Token: 0x06000640 RID: 1600 RVA: 0x000041A9 File Offset: 0x000023A9
		public BodyFlags BodyFlag
		{
			get
			{
				return (BodyFlags)EngineApplicationInterface.IGameEntity.GetBodyFlags(base.Pointer);
			}
			set
			{
				EngineApplicationInterface.IGameEntity.SetBodyFlags(base.Pointer, (uint)value);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000641 RID: 1601 RVA: 0x000041BC File Offset: 0x000023BC
		public BodyFlags PhysicsDescBodyFlag
		{
			get
			{
				return (BodyFlags)EngineApplicationInterface.IGameEntity.GetPhysicsDescBodyFlags(base.Pointer);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x000041CE File Offset: 0x000023CE
		public float Mass
		{
			get
			{
				return EngineApplicationInterface.IGameEntity.GetMass(base.Pointer);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000643 RID: 1603 RVA: 0x000041E0 File Offset: 0x000023E0
		public Vec3 CenterOfMass
		{
			get
			{
				return EngineApplicationInterface.IGameEntity.GetCenterOfMass(base.Pointer);
			}
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x000041F2 File Offset: 0x000023F2
		public void SetBodyFlags(BodyFlags bodyFlags)
		{
			this.BodyFlag = bodyFlags;
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x000041FB File Offset: 0x000023FB
		public void SetBodyFlagsRecursive(BodyFlags bodyFlags)
		{
			EngineApplicationInterface.IGameEntity.SetBodyFlagsRecursive(base.Pointer, (uint)bodyFlags);
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x00004210 File Offset: 0x00002410
		public void AddBodyFlags(BodyFlags bodyFlags, bool applyToChildren = true)
		{
			this.BodyFlag |= bodyFlags;
			if (applyToChildren)
			{
				foreach (GameEntity gameEntity in this.GetChildren())
				{
					gameEntity.AddBodyFlags(bodyFlags, true);
				}
			}
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x00004270 File Offset: 0x00002470
		public void RemoveBodyFlags(BodyFlags bodyFlags, bool applyToChildren = true)
		{
			this.BodyFlag &= ~bodyFlags;
			if (applyToChildren)
			{
				foreach (GameEntity gameEntity in this.GetChildren())
				{
					gameEntity.RemoveBodyFlags(bodyFlags, true);
				}
			}
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x000042D0 File Offset: 0x000024D0
		public Vec3 GetGlobalScale()
		{
			return EngineApplicationInterface.IGameEntity.GetGlobalScale(this);
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x000042E0 File Offset: 0x000024E0
		public Vec3 GetLocalScale()
		{
			return this.GetFrame().rotation.GetScaleVector();
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x00004300 File Offset: 0x00002500
		public Vec3 GlobalPosition
		{
			get
			{
				return this.GetGlobalFrame().origin;
			}
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x00004310 File Offset: 0x00002510
		public void SetAnimationSoundActivation(bool activate)
		{
			EngineApplicationInterface.IGameEntity.SetAnimationSoundActivation(base.Pointer, activate);
			foreach (GameEntity gameEntity in this.GetChildren())
			{
				gameEntity.SetAnimationSoundActivation(activate);
			}
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0000436C File Offset: 0x0000256C
		public void CopyComponentsToSkeleton()
		{
			EngineApplicationInterface.IGameEntity.CopyComponentsToSkeleton(base.Pointer);
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0000437E File Offset: 0x0000257E
		public void AddMeshToBone(sbyte boneIndex, Mesh mesh)
		{
			EngineApplicationInterface.IGameEntity.AddMeshToBone(base.Pointer, mesh.Pointer, boneIndex);
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x00004397 File Offset: 0x00002597
		public void ActivateRagdoll()
		{
			EngineApplicationInterface.IGameEntity.ActivateRagdoll(base.Pointer);
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x000043A9 File Offset: 0x000025A9
		public void PauseSkeletonAnimation()
		{
			EngineApplicationInterface.IGameEntity.Freeze(base.Pointer, true);
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x000043BC File Offset: 0x000025BC
		public void ResumeSkeletonAnimation()
		{
			EngineApplicationInterface.IGameEntity.Freeze(base.Pointer, false);
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x000043CF File Offset: 0x000025CF
		public bool IsSkeletonAnimationPaused()
		{
			return EngineApplicationInterface.IGameEntity.IsFrozen(base.Pointer);
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x000043E1 File Offset: 0x000025E1
		public sbyte GetBoneCount()
		{
			return EngineApplicationInterface.IGameEntity.GetBoneCount(base.Pointer);
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x000043F4 File Offset: 0x000025F4
		public MatrixFrame GetBoneEntitialFrameWithIndex(sbyte boneIndex)
		{
			MatrixFrame result = default(MatrixFrame);
			EngineApplicationInterface.IGameEntity.GetBoneEntitialFrameWithIndex(base.Pointer, boneIndex, ref result);
			return result;
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x00004420 File Offset: 0x00002620
		public MatrixFrame GetBoneEntitialFrameWithName(string boneName)
		{
			MatrixFrame result = default(MatrixFrame);
			EngineApplicationInterface.IGameEntity.GetBoneEntitialFrameWithName(base.Pointer, boneName, ref result);
			return result;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000655 RID: 1621 RVA: 0x00004449 File Offset: 0x00002649
		public string[] Tags
		{
			get
			{
				return EngineApplicationInterface.IGameEntity.GetTags(base.Pointer).Split(new char[]
				{
					' '
				});
			}
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x0000446B File Offset: 0x0000266B
		public void AddTag(string tag)
		{
			EngineApplicationInterface.IGameEntity.AddTag(base.Pointer, tag);
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x0000447E File Offset: 0x0000267E
		public void RemoveTag(string tag)
		{
			EngineApplicationInterface.IGameEntity.RemoveTag(base.Pointer, tag);
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00004491 File Offset: 0x00002691
		public bool HasTag(string tag)
		{
			return EngineApplicationInterface.IGameEntity.HasTag(base.Pointer, tag);
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x000044A4 File Offset: 0x000026A4
		public void AddChild(GameEntity gameEntity, bool autoLocalizeFrame = false)
		{
			EngineApplicationInterface.IGameEntity.AddChild(base.Pointer, gameEntity.Pointer, autoLocalizeFrame);
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x000044BD File Offset: 0x000026BD
		public void RemoveChild(GameEntity childEntity, bool keepPhysics, bool keepScenePointer, bool callScriptCallbacks, int removeReason)
		{
			EngineApplicationInterface.IGameEntity.RemoveChild(base.Pointer, childEntity.Pointer, keepPhysics, keepScenePointer, callScriptCallbacks, removeReason);
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x000044DB File Offset: 0x000026DB
		public void BreakPrefab()
		{
			EngineApplicationInterface.IGameEntity.BreakPrefab(base.Pointer);
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x000044ED File Offset: 0x000026ED
		public int ChildCount
		{
			get
			{
				return EngineApplicationInterface.IGameEntity.GetChildCount(base.Pointer);
			}
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x000044FF File Offset: 0x000026FF
		public GameEntity GetChild(int index)
		{
			return EngineApplicationInterface.IGameEntity.GetChild(base.Pointer, index);
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x00004512 File Offset: 0x00002712
		public GameEntity Parent
		{
			get
			{
				return EngineApplicationInterface.IGameEntity.GetParent(base.Pointer);
			}
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x00004524 File Offset: 0x00002724
		public bool HasComplexAnimTree()
		{
			return EngineApplicationInterface.IGameEntity.HasComplexAnimTree(base.Pointer);
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x00004538 File Offset: 0x00002738
		public GameEntity Root
		{
			get
			{
				GameEntity gameEntity = this;
				while (gameEntity.Parent != null)
				{
					gameEntity = gameEntity.Parent;
				}
				return gameEntity;
			}
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x0000455F File Offset: 0x0000275F
		public void AddMultiMesh(MetaMesh metaMesh, bool updateVisMask = true)
		{
			EngineApplicationInterface.IGameEntity.AddMultiMesh(base.Pointer, metaMesh.Pointer, updateVisMask);
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00004578 File Offset: 0x00002778
		public bool RemoveMultiMesh(MetaMesh metaMesh)
		{
			return EngineApplicationInterface.IGameEntity.RemoveMultiMesh(base.Pointer, metaMesh.Pointer);
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x00004590 File Offset: 0x00002790
		public int MultiMeshComponentCount
		{
			get
			{
				return EngineApplicationInterface.IGameEntity.GetComponentCount(base.Pointer, GameEntity.ComponentType.MetaMesh);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000664 RID: 1636 RVA: 0x000045A3 File Offset: 0x000027A3
		public int ClothSimulatorComponentCount
		{
			get
			{
				return EngineApplicationInterface.IGameEntity.GetComponentCount(base.Pointer, GameEntity.ComponentType.ClothSimulator);
			}
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x000045B6 File Offset: 0x000027B6
		public int GetComponentCount(GameEntity.ComponentType componentType)
		{
			return EngineApplicationInterface.IGameEntity.GetComponentCount(base.Pointer, componentType);
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x000045C9 File Offset: 0x000027C9
		public void AddAllMeshesOfGameEntity(GameEntity gameEntity)
		{
			EngineApplicationInterface.IGameEntity.AddAllMeshesOfGameEntity(base.Pointer, gameEntity.Pointer);
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x000045E1 File Offset: 0x000027E1
		public void SetFrameChanged()
		{
			EngineApplicationInterface.IGameEntity.SetFrameChanged(base.Pointer);
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x000045F3 File Offset: 0x000027F3
		public GameEntityComponent GetComponentAtIndex(int index, GameEntity.ComponentType componentType)
		{
			return EngineApplicationInterface.IGameEntity.GetComponentAtIndex(base.Pointer, componentType, index);
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x00004607 File Offset: 0x00002807
		public MetaMesh GetMetaMesh(int metaMeshIndex)
		{
			return (MetaMesh)EngineApplicationInterface.IGameEntity.GetComponentAtIndex(base.Pointer, GameEntity.ComponentType.MetaMesh, metaMeshIndex);
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x00004620 File Offset: 0x00002820
		public ClothSimulatorComponent GetClothSimulator(int clothSimulatorIndex)
		{
			return (ClothSimulatorComponent)EngineApplicationInterface.IGameEntity.GetComponentAtIndex(base.Pointer, GameEntity.ComponentType.ClothSimulator, clothSimulatorIndex);
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x00004639 File Offset: 0x00002839
		public void SetVectorArgument(float vectorArgument0, float vectorArgument1, float vectorArgument2, float vectorArgument3)
		{
			EngineApplicationInterface.IGameEntity.SetVectorArgument(base.Pointer, vectorArgument0, vectorArgument1, vectorArgument2, vectorArgument3);
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x00004650 File Offset: 0x00002850
		public void SetMaterialForAllMeshes(Material material)
		{
			EngineApplicationInterface.IGameEntity.SetMaterialForAllMeshes(base.Pointer, material.Pointer);
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x00004668 File Offset: 0x00002868
		public bool AddLight(Light light)
		{
			return EngineApplicationInterface.IGameEntity.AddLight(base.Pointer, light.Pointer);
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x00004680 File Offset: 0x00002880
		public Light GetLight()
		{
			return EngineApplicationInterface.IGameEntity.GetLight(base.Pointer);
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00004692 File Offset: 0x00002892
		public void AddParticleSystemComponent(string particleid)
		{
			EngineApplicationInterface.IGameEntity.AddParticleSystemComponent(base.Pointer, particleid);
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x000046A5 File Offset: 0x000028A5
		public void RemoveAllParticleSystems()
		{
			EngineApplicationInterface.IGameEntity.RemoveAllParticleSystems(base.Pointer);
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x000046B7 File Offset: 0x000028B7
		public bool CheckPointWithOrientedBoundingBox(Vec3 point)
		{
			return EngineApplicationInterface.IGameEntity.CheckPointWithOrientedBoundingBox(base.Pointer, point);
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x000046CA File Offset: 0x000028CA
		public void PauseParticleSystem(bool doChildren)
		{
			EngineApplicationInterface.IGameEntity.PauseParticleSystem(base.Pointer, doChildren);
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x000046DD File Offset: 0x000028DD
		public void ResumeParticleSystem(bool doChildren)
		{
			EngineApplicationInterface.IGameEntity.ResumeParticleSystem(base.Pointer, doChildren);
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x000046F0 File Offset: 0x000028F0
		public void BurstEntityParticle(bool doChildren)
		{
			EngineApplicationInterface.IGameEntity.BurstEntityParticle(base.Pointer, doChildren);
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x00004703 File Offset: 0x00002903
		public void SetRuntimeEmissionRateMultiplier(float emissionRateMultiplier)
		{
			EngineApplicationInterface.IGameEntity.SetRuntimeEmissionRateMultiplier(base.Pointer, emissionRateMultiplier);
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x00004716 File Offset: 0x00002916
		public Vec3 GetBoundingBoxMin()
		{
			return EngineApplicationInterface.IGameEntity.GetBoundingBoxMin(base.Pointer);
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00004728 File Offset: 0x00002928
		public float GetBoundingBoxRadius()
		{
			return EngineApplicationInterface.IGameEntity.GetRadius(base.Pointer);
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0000473A File Offset: 0x0000293A
		public Vec3 GetBoundingBoxMax()
		{
			return EngineApplicationInterface.IGameEntity.GetBoundingBoxMax(base.Pointer);
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0000474C File Offset: 0x0000294C
		public Vec3 GetPhysicsBoundingBoxMax()
		{
			return EngineApplicationInterface.IGameEntity.GetPhysicsBoundingBoxMax(base.Pointer);
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x0000475E File Offset: 0x0000295E
		public Vec3 GetPhysicsBoundingBoxMin()
		{
			return EngineApplicationInterface.IGameEntity.GetPhysicsBoundingBoxMin(base.Pointer);
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x00004770 File Offset: 0x00002970
		public void UpdateGlobalBounds()
		{
			EngineApplicationInterface.IGameEntity.UpdateGlobalBounds(base.Pointer);
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x00004782 File Offset: 0x00002982
		public void RecomputeBoundingBox()
		{
			EngineApplicationInterface.IGameEntity.RecomputeBoundingBox(this);
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0000478F File Offset: 0x0000298F
		public void ValidateBoundingBox()
		{
			EngineApplicationInterface.IGameEntity.ValidateBoundingBox(base.Pointer);
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x000047A1 File Offset: 0x000029A1
		public bool GetHasFrameChanged()
		{
			return EngineApplicationInterface.IGameEntity.HasFrameChanged(base.Pointer);
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x000047B3 File Offset: 0x000029B3
		public void SetBoundingboxDirty()
		{
			EngineApplicationInterface.IGameEntity.SetBoundingboxDirty(base.Pointer);
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x000047C5 File Offset: 0x000029C5
		public Mesh GetFirstMesh()
		{
			return EngineApplicationInterface.IGameEntity.GetFirstMesh(base.Pointer);
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x000047D7 File Offset: 0x000029D7
		public Vec3 GlobalBoxMax
		{
			get
			{
				return EngineApplicationInterface.IGameEntity.GetGlobalBoxMax(base.Pointer);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x000047E9 File Offset: 0x000029E9
		public Vec3 PhysicsGlobalBoxMax
		{
			get
			{
				return EngineApplicationInterface.IGameEntity.GetPhysicsBoundingBoxMax(base.Pointer);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000683 RID: 1667 RVA: 0x000047FB File Offset: 0x000029FB
		public Vec3 PhysicsGlobalBoxMin
		{
			get
			{
				return EngineApplicationInterface.IGameEntity.GetPhysicsBoundingBoxMin(base.Pointer);
			}
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x00004810 File Offset: 0x00002A10
		public void SetContourColor(uint? color, bool alwaysVisible = true)
		{
			if (color != null)
			{
				EngineApplicationInterface.IGameEntity.SetAsContourEntity(base.Pointer, color.Value);
				EngineApplicationInterface.IGameEntity.SetContourState(base.Pointer, alwaysVisible);
				return;
			}
			EngineApplicationInterface.IGameEntity.DisableContour(base.Pointer);
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x0000485F File Offset: 0x00002A5F
		public Vec3 GlobalBoxMin
		{
			get
			{
				return EngineApplicationInterface.IGameEntity.GetGlobalBoxMin(base.Pointer);
			}
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x00004871 File Offset: 0x00002A71
		public void SetExternalReferencesUsage(bool value)
		{
			EngineApplicationInterface.IGameEntity.SetExternalReferencesUsage(base.Pointer, value);
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x00004884 File Offset: 0x00002A84
		public void SetMorphFrameOfComponents(float value)
		{
			EngineApplicationInterface.IGameEntity.SetMorphFrameOfComponents(base.Pointer, value);
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x00004897 File Offset: 0x00002A97
		public void AddEditDataUserToAllMeshes(bool entityComponents, bool skeletonComponents)
		{
			EngineApplicationInterface.IGameEntity.AddEditDataUserToAllMeshes(base.Pointer, entityComponents, skeletonComponents);
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x000048AB File Offset: 0x00002AAB
		public void ReleaseEditDataUserToAllMeshes(bool entityComponents, bool skeletonComponents)
		{
			EngineApplicationInterface.IGameEntity.ReleaseEditDataUserToAllMeshes(base.Pointer, entityComponents, skeletonComponents);
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x000048BF File Offset: 0x00002ABF
		public void GetCameraParamsFromCameraScript(Camera cam, ref Vec3 dofParams)
		{
			EngineApplicationInterface.IGameEntity.GetCameraParamsFromCameraScript(base.Pointer, cam.Pointer, ref dofParams);
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x000048D8 File Offset: 0x00002AD8
		public void GetMeshBendedFrame(MatrixFrame worldSpacePosition, ref MatrixFrame output)
		{
			EngineApplicationInterface.IGameEntity.GetMeshBendedPosition(base.Pointer, ref worldSpacePosition, ref output);
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x000048ED File Offset: 0x00002AED
		public void ComputeTrajectoryVolume(float missileSpeed, float verticalAngleMaxInDegrees, float verticalAngleMinInDegrees, float horizontalAngleRangeInDegrees, float airFrictionConstant)
		{
			EngineApplicationInterface.IGameEntity.ComputeTrajectoryVolume(base.Pointer, missileSpeed, verticalAngleMaxInDegrees, verticalAngleMinInDegrees, horizontalAngleRangeInDegrees, airFrictionConstant);
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00004906 File Offset: 0x00002B06
		public void SetAnimTreeChannelParameterForceUpdate(float phase, int channelNo)
		{
			EngineApplicationInterface.IGameEntity.SetAnimTreeChannelParameter(base.Pointer, phase, channelNo);
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x0000491A File Offset: 0x00002B1A
		public void ChangeMetaMeshOrRemoveItIfNotExists(MetaMesh entityMetaMesh, MetaMesh newMetaMesh)
		{
			EngineApplicationInterface.IGameEntity.ChangeMetaMeshOrRemoveItIfNotExists(base.Pointer, (entityMetaMesh != null) ? entityMetaMesh.Pointer : UIntPtr.Zero, (newMetaMesh != null) ? newMetaMesh.Pointer : UIntPtr.Zero);
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x00004958 File Offset: 0x00002B58
		public void AttachNavigationMeshFaces(int faceGroupId, bool isConnected, bool isBlocker = false, bool autoLocalize = false)
		{
			EngineApplicationInterface.IGameEntity.AttachNavigationMeshFaces(base.Pointer, faceGroupId, isConnected, isBlocker, autoLocalize);
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0000496F File Offset: 0x00002B6F
		public void RemoveSkeleton()
		{
			this.Skeleton = null;
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000691 RID: 1681 RVA: 0x00004978 File Offset: 0x00002B78
		// (set) Token: 0x06000692 RID: 1682 RVA: 0x0000498A File Offset: 0x00002B8A
		public Skeleton Skeleton
		{
			get
			{
				return EngineApplicationInterface.IGameEntity.GetSkeleton(base.Pointer);
			}
			set
			{
				EngineApplicationInterface.IGameEntity.SetSkeleton(base.Pointer, (value != null) ? value.Pointer : UIntPtr.Zero);
			}
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x000049AC File Offset: 0x00002BAC
		public void RemoveAllChildren()
		{
			EngineApplicationInterface.IGameEntity.RemoveAllChildren(base.Pointer);
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x000049BE File Offset: 0x00002BBE
		public IEnumerable<GameEntity> GetChildren()
		{
			int count = this.ChildCount;
			int num;
			for (int i = 0; i < count; i = num + 1)
			{
				yield return this.GetChild(i);
				num = i;
			}
			yield break;
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x000049CE File Offset: 0x00002BCE
		public IEnumerable<GameEntity> GetEntityAndChildren()
		{
			yield return this;
			int count = this.ChildCount;
			int num;
			for (int i = 0; i < count; i = num + 1)
			{
				yield return this.GetChild(i);
				num = i;
			}
			yield break;
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x000049E0 File Offset: 0x00002BE0
		public void GetChildrenRecursive(ref List<GameEntity> children)
		{
			int childCount = this.ChildCount;
			for (int i = 0; i < childCount; i++)
			{
				GameEntity child = this.GetChild(i);
				children.Add(child);
				child.GetChildrenRecursive(ref children);
			}
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x00004A17 File Offset: 0x00002C17
		public bool IsSelectedOnEditor()
		{
			return EngineApplicationInterface.IGameEntity.IsEntitySelectedOnEditor(base.Pointer);
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x00004A29 File Offset: 0x00002C29
		public void SelectEntityOnEditor()
		{
			EngineApplicationInterface.IGameEntity.SelectEntityOnEditor(base.Pointer);
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x00004A3B File Offset: 0x00002C3B
		public void DeselectEntityOnEditor()
		{
			EngineApplicationInterface.IGameEntity.DeselectEntityOnEditor(base.Pointer);
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x00004A4D File Offset: 0x00002C4D
		public void SetAsPredisplayEntity()
		{
			EngineApplicationInterface.IGameEntity.SetAsPredisplayEntity(base.Pointer);
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x00004A5F File Offset: 0x00002C5F
		public void RemoveFromPredisplayEntity()
		{
			EngineApplicationInterface.IGameEntity.RemoveFromPredisplayEntity(base.Pointer);
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x00004A71 File Offset: 0x00002C71
		public void GetPhysicsMinMax(bool includeChildren, out Vec3 bbmin, out Vec3 bbmax, bool returnLocal)
		{
			bbmin = Vec3.Zero;
			bbmax = Vec3.Zero;
			EngineApplicationInterface.IGameEntity.GetPhysicsMinMax(base.Pointer, includeChildren, ref bbmin, ref bbmax, returnLocal);
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x00004A9E File Offset: 0x00002C9E
		public void SetCullMode(MBMeshCullingMode cullMode)
		{
			EngineApplicationInterface.IGameEntity.SetCullMode(base.Pointer, cullMode);
		}

		// Token: 0x020000AB RID: 171
		[EngineStruct("rglEntity_component_type", false)]
		public enum ComponentType : uint
		{
			// Token: 0x04000338 RID: 824
			MetaMesh,
			// Token: 0x04000339 RID: 825
			Light,
			// Token: 0x0400033A RID: 826
			CompositeComponent,
			// Token: 0x0400033B RID: 827
			ClothSimulator,
			// Token: 0x0400033C RID: 828
			ParticleSystemInstanced,
			// Token: 0x0400033D RID: 829
			TownIcon,
			// Token: 0x0400033E RID: 830
			CustomType1,
			// Token: 0x0400033F RID: 831
			Decal
		}

		// Token: 0x020000AC RID: 172
		public enum Mobility
		{
			// Token: 0x04000341 RID: 833
			stationary,
			// Token: 0x04000342 RID: 834
			dynamic,
			// Token: 0x04000343 RID: 835
			dynamic_forced
		}

		// Token: 0x020000AD RID: 173
		[Flags]
		public enum UpgradeLevelMask
		{
			// Token: 0x04000345 RID: 837
			None = 0,
			// Token: 0x04000346 RID: 838
			Level0 = 1,
			// Token: 0x04000347 RID: 839
			Level1 = 2,
			// Token: 0x04000348 RID: 840
			Level2 = 4,
			// Token: 0x04000349 RID: 841
			Level3 = 8,
			// Token: 0x0400034A RID: 842
			LevelAll = 15
		}
	}
}
