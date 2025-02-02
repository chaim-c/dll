using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace ManagedCallbacks
{
	// Token: 0x02000022 RID: 34
	internal class ScriptingInterfaceOfIScene : IScene
	{
		// Token: 0x06000352 RID: 850 RVA: 0x0001281C File Offset: 0x00010A1C
		public void AddDecalInstance(UIntPtr scenePointer, UIntPtr decalMeshPointer, string decalSetID, bool deletable)
		{
			byte[] array = null;
			if (decalSetID != null)
			{
				int byteCount = ScriptingInterfaceOfIScene._utf8.GetByteCount(decalSetID);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIScene._utf8.GetBytes(decalSetID, 0, decalSetID.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIScene.call_AddDecalInstanceDelegate(scenePointer, decalMeshPointer, array, deletable);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0001287A File Offset: 0x00010A7A
		public int AddDirectionalLight(UIntPtr scenePointer, Vec3 position, Vec3 direction, float radius)
		{
			return ScriptingInterfaceOfIScene.call_AddDirectionalLightDelegate(scenePointer, position, direction, radius);
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0001288B File Offset: 0x00010A8B
		public void AddEntityWithMesh(UIntPtr scenePointer, UIntPtr meshPointer, ref MatrixFrame frame)
		{
			ScriptingInterfaceOfIScene.call_AddEntityWithMeshDelegate(scenePointer, meshPointer, ref frame);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0001289A File Offset: 0x00010A9A
		public void AddEntityWithMultiMesh(UIntPtr scenePointer, UIntPtr multiMeshPointer, ref MatrixFrame frame)
		{
			ScriptingInterfaceOfIScene.call_AddEntityWithMultiMeshDelegate(scenePointer, multiMeshPointer, ref frame);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x000128AC File Offset: 0x00010AAC
		public GameEntity AddItemEntity(UIntPtr scenePointer, ref MatrixFrame frame, UIntPtr meshPointer)
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIScene.call_AddItemEntityDelegate(scenePointer, ref frame, meshPointer);
			GameEntity result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new GameEntity(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x06000357 RID: 855 RVA: 0x000128F8 File Offset: 0x00010AF8
		public void AddPath(UIntPtr scenePointer, string name)
		{
			byte[] array = null;
			if (name != null)
			{
				int byteCount = ScriptingInterfaceOfIScene._utf8.GetByteCount(name);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIScene._utf8.GetBytes(name, 0, name.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIScene.call_AddPathDelegate(scenePointer, array);
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00012954 File Offset: 0x00010B54
		public void AddPathPoint(UIntPtr scenePointer, string name, ref MatrixFrame frame)
		{
			byte[] array = null;
			if (name != null)
			{
				int byteCount = ScriptingInterfaceOfIScene._utf8.GetByteCount(name);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIScene._utf8.GetBytes(name, 0, name.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIScene.call_AddPathPointDelegate(scenePointer, array, ref frame);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x000129B0 File Offset: 0x00010BB0
		public int AddPointLight(UIntPtr scenePointer, Vec3 position, float radius)
		{
			return ScriptingInterfaceOfIScene.call_AddPointLightDelegate(scenePointer, position, radius);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x000129BF File Offset: 0x00010BBF
		public bool AttachEntity(UIntPtr scenePointer, UIntPtr entity, bool showWarnings)
		{
			return ScriptingInterfaceOfIScene.call_AttachEntityDelegate(scenePointer, entity, showWarnings);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x000129D0 File Offset: 0x00010BD0
		public bool BoxCast(UIntPtr scenePointer, ref Vec3 boxPointBegin, ref Vec3 boxPointEnd, ref Vec3 dir, float distance, ref float collisionDistance, ref Vec3 closestPoint, ref UIntPtr entityIndex, BodyFlags bodyExcludeFlags)
		{
			return ScriptingInterfaceOfIScene.call_BoxCastDelegate(scenePointer, ref boxPointBegin, ref boxPointEnd, ref dir, distance, ref collisionDistance, ref closestPoint, ref entityIndex, bodyExcludeFlags);
		}

		// Token: 0x0600035C RID: 860 RVA: 0x000129F8 File Offset: 0x00010BF8
		public bool BoxCastOnlyForCamera(UIntPtr scenePointer, Vec3[] boxPoints, ref Vec3 centerPoint, ref Vec3 dir, float distance, ref float collisionDistance, ref Vec3 closestPoint, ref UIntPtr entityIndex, BodyFlags bodyExcludeFlags, bool preFilter, bool postFilter)
		{
			PinnedArrayData<Vec3> pinnedArrayData = new PinnedArrayData<Vec3>(boxPoints, false);
			IntPtr pointer = pinnedArrayData.Pointer;
			bool result = ScriptingInterfaceOfIScene.call_BoxCastOnlyForCameraDelegate(scenePointer, pointer, ref centerPoint, ref dir, distance, ref collisionDistance, ref closestPoint, ref entityIndex, bodyExcludeFlags, preFilter, postFilter);
			pinnedArrayData.Dispose();
			return result;
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00012A3A File Offset: 0x00010C3A
		public bool CalculateEffectiveLighting(UIntPtr scenePointer)
		{
			return ScriptingInterfaceOfIScene.call_CalculateEffectiveLightingDelegate(scenePointer);
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00012A48 File Offset: 0x00010C48
		public bool CheckPathEntitiesFrameChanged(UIntPtr scenePointer, string containsName)
		{
			byte[] array = null;
			if (containsName != null)
			{
				int byteCount = ScriptingInterfaceOfIScene._utf8.GetByteCount(containsName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIScene._utf8.GetBytes(containsName, 0, containsName.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfIScene.call_CheckPathEntitiesFrameChangedDelegate(scenePointer, array);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00012AA3 File Offset: 0x00010CA3
		public bool CheckPointCanSeePoint(UIntPtr scenePointer, Vec3 sourcePoint, Vec3 targetPoint, float distanceToCheck)
		{
			return ScriptingInterfaceOfIScene.call_CheckPointCanSeePointDelegate(scenePointer, sourcePoint, targetPoint, distanceToCheck);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00012AB4 File Offset: 0x00010CB4
		public void CheckResources(UIntPtr scenePointer)
		{
			ScriptingInterfaceOfIScene.call_CheckResourcesDelegate(scenePointer);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00012AC1 File Offset: 0x00010CC1
		public void ClearAll(UIntPtr scenePointer)
		{
			ScriptingInterfaceOfIScene.call_ClearAllDelegate(scenePointer);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00012ACE File Offset: 0x00010CCE
		public void ClearDecals(UIntPtr scenePointer)
		{
			ScriptingInterfaceOfIScene.call_ClearDecalsDelegate(scenePointer);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00012ADB File Offset: 0x00010CDB
		public bool ContainsTerrain(UIntPtr scenePointer)
		{
			return ScriptingInterfaceOfIScene.call_ContainsTerrainDelegate(scenePointer);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00012AE8 File Offset: 0x00010CE8
		public void CreateBurstParticle(Scene scene, int particleId, ref MatrixFrame frame)
		{
			UIntPtr scene2 = (scene != null) ? scene.Pointer : UIntPtr.Zero;
			ScriptingInterfaceOfIScene.call_CreateBurstParticleDelegate(scene2, particleId, ref frame);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00012B1C File Offset: 0x00010D1C
		public Scene CreateNewScene(bool initialize_physics, bool enable_decals, int atlasGroup, string sceneName)
		{
			byte[] array = null;
			if (sceneName != null)
			{
				int byteCount = ScriptingInterfaceOfIScene._utf8.GetByteCount(sceneName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIScene._utf8.GetBytes(sceneName, 0, sceneName.Length, array, 0);
				array[byteCount] = 0;
			}
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIScene.call_CreateNewSceneDelegate(initialize_physics, enable_decals, atlasGroup, array);
			Scene result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new Scene(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00012BB0 File Offset: 0x00010DB0
		public MetaMesh CreatePathMesh(UIntPtr scenePointer, string baseEntityName, bool isWaterPath)
		{
			byte[] array = null;
			if (baseEntityName != null)
			{
				int byteCount = ScriptingInterfaceOfIScene._utf8.GetByteCount(baseEntityName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIScene._utf8.GetBytes(baseEntityName, 0, baseEntityName.Length, array, 0);
				array[byteCount] = 0;
			}
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIScene.call_CreatePathMeshDelegate(scenePointer, array, isWaterPath);
			MetaMesh result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new MetaMesh(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00012C40 File Offset: 0x00010E40
		public MetaMesh CreatePathMesh2(UIntPtr scenePointer, UIntPtr[] pathNodes, int pathNodeCount, bool isWaterPath)
		{
			PinnedArrayData<UIntPtr> pinnedArrayData = new PinnedArrayData<UIntPtr>(pathNodes, false);
			IntPtr pointer = pinnedArrayData.Pointer;
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIScene.call_CreatePathMesh2Delegate(scenePointer, pointer, pathNodeCount, isWaterPath);
			pinnedArrayData.Dispose();
			MetaMesh result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new MetaMesh(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00012CA8 File Offset: 0x00010EA8
		public void DeletePathWithName(UIntPtr scenePointer, string name)
		{
			byte[] array = null;
			if (name != null)
			{
				int byteCount = ScriptingInterfaceOfIScene._utf8.GetByteCount(name);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIScene._utf8.GetBytes(name, 0, name.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIScene.call_DeletePathWithNameDelegate(scenePointer, array);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00012D03 File Offset: 0x00010F03
		public void DisableStaticShadows(UIntPtr ptr, bool value)
		{
			ScriptingInterfaceOfIScene.call_DisableStaticShadowsDelegate(ptr, value);
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00012D11 File Offset: 0x00010F11
		public bool DoesPathExistBetweenFaces(UIntPtr scenePointer, int firstNavMeshFace, int secondNavMeshFace, bool ignoreDisabled)
		{
			return ScriptingInterfaceOfIScene.call_DoesPathExistBetweenFacesDelegate(scenePointer, firstNavMeshFace, secondNavMeshFace, ignoreDisabled);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00012D22 File Offset: 0x00010F22
		public bool DoesPathExistBetweenPositions(UIntPtr scenePointer, WorldPosition position, WorldPosition destination)
		{
			return ScriptingInterfaceOfIScene.call_DoesPathExistBetweenPositionsDelegate(scenePointer, position, destination);
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00012D31 File Offset: 0x00010F31
		public void EnsurePostfxSystem(UIntPtr scenePointer)
		{
			ScriptingInterfaceOfIScene.call_EnsurePostfxSystemDelegate(scenePointer);
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00012D3E File Offset: 0x00010F3E
		public void FillEntityWithHardBorderPhysicsBarrier(UIntPtr scenePointer, UIntPtr entityPointer)
		{
			ScriptingInterfaceOfIScene.call_FillEntityWithHardBorderPhysicsBarrierDelegate(scenePointer, entityPointer);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00012D4C File Offset: 0x00010F4C
		public void FillTerrainHeightData(Scene scene, int xIndex, int yIndex, float[] heightArray)
		{
			UIntPtr scene2 = (scene != null) ? scene.Pointer : UIntPtr.Zero;
			PinnedArrayData<float> pinnedArrayData = new PinnedArrayData<float>(heightArray, false);
			IntPtr pointer = pinnedArrayData.Pointer;
			ScriptingInterfaceOfIScene.call_FillTerrainHeightDataDelegate(scene2, xIndex, yIndex, pointer);
			pinnedArrayData.Dispose();
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00012D98 File Offset: 0x00010F98
		public void FillTerrainPhysicsMaterialIndexData(Scene scene, int xIndex, int yIndex, short[] materialIndexArray)
		{
			UIntPtr scene2 = (scene != null) ? scene.Pointer : UIntPtr.Zero;
			PinnedArrayData<short> pinnedArrayData = new PinnedArrayData<short>(materialIndexArray, false);
			IntPtr pointer = pinnedArrayData.Pointer;
			ScriptingInterfaceOfIScene.call_FillTerrainPhysicsMaterialIndexDataDelegate(scene2, xIndex, yIndex, pointer);
			pinnedArrayData.Dispose();
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00012DE3 File Offset: 0x00010FE3
		public void FinishSceneSounds(UIntPtr scenePointer)
		{
			ScriptingInterfaceOfIScene.call_FinishSceneSoundsDelegate(scenePointer);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00012DF0 File Offset: 0x00010FF0
		public void ForceLoadResources(UIntPtr scenePointer)
		{
			ScriptingInterfaceOfIScene.call_ForceLoadResourcesDelegate(scenePointer);
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00012E00 File Offset: 0x00011000
		public int GenerateContactsWithCapsule(UIntPtr scenePointer, ref CapsuleData cap, BodyFlags exclude_flags, Intersection[] intersections)
		{
			PinnedArrayData<Intersection> pinnedArrayData = new PinnedArrayData<Intersection>(intersections, false);
			IntPtr pointer = pinnedArrayData.Pointer;
			int result = ScriptingInterfaceOfIScene.call_GenerateContactsWithCapsuleDelegate(scenePointer, ref cap, exclude_flags, pointer);
			pinnedArrayData.Dispose();
			return result;
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00012E34 File Offset: 0x00011034
		public string GetAllColorGradeNames(Scene scene)
		{
			UIntPtr scene2 = (scene != null) ? scene.Pointer : UIntPtr.Zero;
			if (ScriptingInterfaceOfIScene.call_GetAllColorGradeNamesDelegate(scene2) != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00012E70 File Offset: 0x00011070
		public void GetAllEntitiesWithScriptComponent(UIntPtr scenePointer, string scriptComponentName, UIntPtr output)
		{
			byte[] array = null;
			if (scriptComponentName != null)
			{
				int byteCount = ScriptingInterfaceOfIScene._utf8.GetByteCount(scriptComponentName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIScene._utf8.GetBytes(scriptComponentName, 0, scriptComponentName.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIScene.call_GetAllEntitiesWithScriptComponentDelegate(scenePointer, array, output);
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00012ECC File Offset: 0x000110CC
		public string GetAllFilterNames(Scene scene)
		{
			UIntPtr scene2 = (scene != null) ? scene.Pointer : UIntPtr.Zero;
			if (ScriptingInterfaceOfIScene.call_GetAllFilterNamesDelegate(scene2) != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00012F05 File Offset: 0x00011105
		public void GetBoundingBox(UIntPtr scenePointer, ref Vec3 min, ref Vec3 max)
		{
			ScriptingInterfaceOfIScene.call_GetBoundingBoxDelegate(scenePointer, ref min, ref max);
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00012F14 File Offset: 0x00011114
		public GameEntity GetCampaignEntityWithName(UIntPtr scenePointer, string entityName)
		{
			byte[] array = null;
			if (entityName != null)
			{
				int byteCount = ScriptingInterfaceOfIScene._utf8.GetByteCount(entityName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIScene._utf8.GetBytes(entityName, 0, entityName.Length, array, 0);
				array[byteCount] = 0;
			}
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIScene.call_GetCampaignEntityWithNameDelegate(scenePointer, array);
			GameEntity result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new GameEntity(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00012FA1 File Offset: 0x000111A1
		public void GetEntities(UIntPtr scenePointer, UIntPtr entityObjectsArrayPointer)
		{
			ScriptingInterfaceOfIScene.call_GetEntitiesDelegate(scenePointer, entityObjectsArrayPointer);
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00012FAF File Offset: 0x000111AF
		public int GetEntityCount(UIntPtr scenePointer)
		{
			return ScriptingInterfaceOfIScene.call_GetEntityCountDelegate(scenePointer);
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00012FBC File Offset: 0x000111BC
		public GameEntity GetEntityWithGuid(UIntPtr scenePointer, string guid)
		{
			byte[] array = null;
			if (guid != null)
			{
				int byteCount = ScriptingInterfaceOfIScene._utf8.GetByteCount(guid);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIScene._utf8.GetBytes(guid, 0, guid.Length, array, 0);
				array[byteCount] = 0;
			}
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIScene.call_GetEntityWithGuidDelegate(scenePointer, array);
			GameEntity result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new GameEntity(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0001304C File Offset: 0x0001124C
		public GameEntity GetFirstEntityWithName(UIntPtr scenePointer, string entityName)
		{
			byte[] array = null;
			if (entityName != null)
			{
				int byteCount = ScriptingInterfaceOfIScene._utf8.GetByteCount(entityName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIScene._utf8.GetBytes(entityName, 0, entityName.Length, array, 0);
				array[byteCount] = 0;
			}
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIScene.call_GetFirstEntityWithNameDelegate(scenePointer, array);
			GameEntity result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new GameEntity(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x0600037C RID: 892 RVA: 0x000130DC File Offset: 0x000112DC
		public GameEntity GetFirstEntityWithScriptComponent(UIntPtr scenePointer, string scriptComponentName)
		{
			byte[] array = null;
			if (scriptComponentName != null)
			{
				int byteCount = ScriptingInterfaceOfIScene._utf8.GetByteCount(scriptComponentName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIScene._utf8.GetBytes(scriptComponentName, 0, scriptComponentName.Length, array, 0);
				array[byteCount] = 0;
			}
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIScene.call_GetFirstEntityWithScriptComponentDelegate(scenePointer, array);
			GameEntity result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new GameEntity(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00013169 File Offset: 0x00011369
		public int GetFloraInstanceCount(UIntPtr scenePointer)
		{
			return ScriptingInterfaceOfIScene.call_GetFloraInstanceCountDelegate(scenePointer);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x00013176 File Offset: 0x00011376
		public int GetFloraRendererTextureUsage(UIntPtr scenePointer)
		{
			return ScriptingInterfaceOfIScene.call_GetFloraRendererTextureUsageDelegate(scenePointer);
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00013183 File Offset: 0x00011383
		public float GetFog(UIntPtr scenePointer)
		{
			return ScriptingInterfaceOfIScene.call_GetFogDelegate(scenePointer);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00013190 File Offset: 0x00011390
		public float GetGroundHeightAndNormalAtPosition(UIntPtr scenePointer, Vec3 position, ref Vec3 normal, uint excludeFlags)
		{
			return ScriptingInterfaceOfIScene.call_GetGroundHeightAndNormalAtPositionDelegate(scenePointer, position, ref normal, excludeFlags);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x000131A1 File Offset: 0x000113A1
		public float GetGroundHeightAtPosition(UIntPtr scenePointer, Vec3 position, uint excludeFlags)
		{
			return ScriptingInterfaceOfIScene.call_GetGroundHeightAtPositionDelegate(scenePointer, position, excludeFlags);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x000131B0 File Offset: 0x000113B0
		public Vec2 GetHardBoundaryVertex(UIntPtr scenePointer, int index)
		{
			return ScriptingInterfaceOfIScene.call_GetHardBoundaryVertexDelegate(scenePointer, index);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x000131BE File Offset: 0x000113BE
		public int GetHardBoundaryVertexCount(UIntPtr scenePointer)
		{
			return ScriptingInterfaceOfIScene.call_GetHardBoundaryVertexCountDelegate(scenePointer);
		}

		// Token: 0x06000384 RID: 900 RVA: 0x000131CB File Offset: 0x000113CB
		public bool GetHeightAtPoint(UIntPtr scenePointer, Vec2 point, BodyFlags excludeBodyFlags, ref float height)
		{
			return ScriptingInterfaceOfIScene.call_GetHeightAtPointDelegate(scenePointer, point, excludeBodyFlags, ref height);
		}

		// Token: 0x06000385 RID: 901 RVA: 0x000131DC File Offset: 0x000113DC
		public int GetIdOfNavMeshFace(UIntPtr scenePointer, int navMeshFace)
		{
			return ScriptingInterfaceOfIScene.call_GetIdOfNavMeshFaceDelegate(scenePointer, navMeshFace);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x000131EA File Offset: 0x000113EA
		public void GetLastFinalRenderCameraFrame(UIntPtr scenePointer, ref MatrixFrame outFrame)
		{
			ScriptingInterfaceOfIScene.call_GetLastFinalRenderCameraFrameDelegate(scenePointer, ref outFrame);
		}

		// Token: 0x06000387 RID: 903 RVA: 0x000131F8 File Offset: 0x000113F8
		public Vec3 GetLastFinalRenderCameraPosition(UIntPtr scenePointer)
		{
			return ScriptingInterfaceOfIScene.call_GetLastFinalRenderCameraPositionDelegate(scenePointer);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00013205 File Offset: 0x00011405
		public Vec2 GetLastPointOnNavigationMeshFromPositionToDestination(UIntPtr scenePointer, int startingFace, Vec2 position, Vec2 destination)
		{
			return ScriptingInterfaceOfIScene.call_GetLastPointOnNavigationMeshFromPositionToDestinationDelegate(scenePointer, startingFace, position, destination);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00013216 File Offset: 0x00011416
		public Vec3 GetLastPointOnNavigationMeshFromWorldPositionToDestination(UIntPtr scenePointer, ref WorldPosition position, Vec2 destination)
		{
			return ScriptingInterfaceOfIScene.call_GetLastPointOnNavigationMeshFromWorldPositionToDestinationDelegate(scenePointer, ref position, destination);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00013228 File Offset: 0x00011428
		public string GetLoadingStateName(Scene scene)
		{
			UIntPtr scene2 = (scene != null) ? scene.Pointer : UIntPtr.Zero;
			if (ScriptingInterfaceOfIScene.call_GetLoadingStateNameDelegate(scene2) != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00013261 File Offset: 0x00011461
		public string GetModulePath(UIntPtr scenePointer)
		{
			if (ScriptingInterfaceOfIScene.call_GetModulePathDelegate(scenePointer) != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00013278 File Offset: 0x00011478
		public string GetName(UIntPtr scenePointer)
		{
			if (ScriptingInterfaceOfIScene.call_GetNameDelegate(scenePointer) != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0001328F File Offset: 0x0001148F
		public bool GetNavigationMeshFaceForPosition(UIntPtr scenePointer, ref Vec3 position, ref int faceGroupId, float heightDifferenceLimit)
		{
			return ScriptingInterfaceOfIScene.call_GetNavigationMeshFaceForPositionDelegate(scenePointer, ref position, ref faceGroupId, heightDifferenceLimit);
		}

		// Token: 0x0600038E RID: 910 RVA: 0x000132A0 File Offset: 0x000114A0
		public void GetNavMeshFaceCenterPosition(UIntPtr scenePointer, int navMeshFace, ref Vec3 centerPos)
		{
			ScriptingInterfaceOfIScene.call_GetNavMeshFaceCenterPositionDelegate(scenePointer, navMeshFace, ref centerPos);
		}

		// Token: 0x0600038F RID: 911 RVA: 0x000132AF File Offset: 0x000114AF
		public int GetNavMeshFaceCount(UIntPtr scenePointer)
		{
			return ScriptingInterfaceOfIScene.call_GetNavMeshFaceCountDelegate(scenePointer);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x000132BC File Offset: 0x000114BC
		public float GetNavMeshFaceFirstVertexZ(UIntPtr scenePointer, int navMeshFaceIndex)
		{
			return ScriptingInterfaceOfIScene.call_GetNavMeshFaceFirstVertexZDelegate(scenePointer, navMeshFaceIndex);
		}

		// Token: 0x06000391 RID: 913 RVA: 0x000132CA File Offset: 0x000114CA
		public void GetNavMeshFaceIndex(UIntPtr scenePointer, ref PathFaceRecord record, Vec2 position, bool checkIfDisabled, bool ignoreHeight)
		{
			ScriptingInterfaceOfIScene.call_GetNavMeshFaceIndexDelegate(scenePointer, ref record, position, checkIfDisabled, ignoreHeight);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x000132DD File Offset: 0x000114DD
		public void GetNavMeshFaceIndex3(UIntPtr scenePointer, ref PathFaceRecord record, Vec3 position, bool checkIfDisabled)
		{
			ScriptingInterfaceOfIScene.call_GetNavMeshFaceIndex3Delegate(scenePointer, ref record, position, checkIfDisabled);
		}

		// Token: 0x06000393 RID: 915 RVA: 0x000132F0 File Offset: 0x000114F0
		public int GetNodeDataCount(Scene scene, int xIndex, int yIndex)
		{
			UIntPtr scene2 = (scene != null) ? scene.Pointer : UIntPtr.Zero;
			return ScriptingInterfaceOfIScene.call_GetNodeDataCountDelegate(scene2, xIndex, yIndex);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00013321 File Offset: 0x00011521
		public Vec3 GetNormalAt(UIntPtr scenePointer, Vec2 position)
		{
			return ScriptingInterfaceOfIScene.call_GetNormalAtDelegate(scenePointer, position);
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0001332F File Offset: 0x0001152F
		public float GetNorthAngle(UIntPtr scenePointer)
		{
			return ScriptingInterfaceOfIScene.call_GetNorthAngleDelegate(scenePointer);
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0001333C File Offset: 0x0001153C
		public int GetNumberOfPathsWithNamePrefix(UIntPtr ptr, string prefix)
		{
			byte[] array = null;
			if (prefix != null)
			{
				int byteCount = ScriptingInterfaceOfIScene._utf8.GetByteCount(prefix);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIScene._utf8.GetBytes(prefix, 0, prefix.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfIScene.call_GetNumberOfPathsWithNamePrefixDelegate(ptr, array);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00013398 File Offset: 0x00011598
		public bool GetPathBetweenAIFaceIndices(UIntPtr scenePointer, int startingAiFace, int endingAiFace, Vec2 startingPosition, Vec2 endingPosition, float agentRadius, Vec2[] result, ref int pathSize, int[] exclusionGroupIds, int exlusionGroupIdsCount, float extraCostMultiplier)
		{
			PinnedArrayData<Vec2> pinnedArrayData = new PinnedArrayData<Vec2>(result, false);
			IntPtr pointer = pinnedArrayData.Pointer;
			PinnedArrayData<int> pinnedArrayData2 = new PinnedArrayData<int>(exclusionGroupIds, false);
			IntPtr pointer2 = pinnedArrayData2.Pointer;
			bool result2 = ScriptingInterfaceOfIScene.call_GetPathBetweenAIFaceIndicesDelegate(scenePointer, startingAiFace, endingAiFace, startingPosition, endingPosition, agentRadius, pointer, ref pathSize, pointer2, exlusionGroupIdsCount, extraCostMultiplier);
			pinnedArrayData.Dispose();
			pinnedArrayData2.Dispose();
			return result2;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x000133F4 File Offset: 0x000115F4
		public bool GetPathBetweenAIFacePointers(UIntPtr scenePointer, UIntPtr startingAiFace, UIntPtr endingAiFace, Vec2 startingPosition, Vec2 endingPosition, float agentRadius, Vec2[] result, ref int pathSize, int[] exclusionGroupIds, int exlusionGroupIdsCount)
		{
			PinnedArrayData<Vec2> pinnedArrayData = new PinnedArrayData<Vec2>(result, false);
			IntPtr pointer = pinnedArrayData.Pointer;
			PinnedArrayData<int> pinnedArrayData2 = new PinnedArrayData<int>(exclusionGroupIds, false);
			IntPtr pointer2 = pinnedArrayData2.Pointer;
			bool result2 = ScriptingInterfaceOfIScene.call_GetPathBetweenAIFacePointersDelegate(scenePointer, startingAiFace, endingAiFace, startingPosition, endingPosition, agentRadius, pointer, ref pathSize, pointer2, exlusionGroupIdsCount);
			pinnedArrayData.Dispose();
			pinnedArrayData2.Dispose();
			return result2;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0001344C File Offset: 0x0001164C
		public bool GetPathDistanceBetweenAIFaces(UIntPtr scenePointer, int startingAiFace, int endingAiFace, Vec2 startingPosition, Vec2 endingPosition, float agentRadius, float distanceLimit, out float distance)
		{
			return ScriptingInterfaceOfIScene.call_GetPathDistanceBetweenAIFacesDelegate(scenePointer, startingAiFace, endingAiFace, startingPosition, endingPosition, agentRadius, distanceLimit, out distance);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00013470 File Offset: 0x00011670
		public bool GetPathDistanceBetweenPositions(UIntPtr scenePointer, ref WorldPosition position, ref WorldPosition destination, float agentRadius, ref float pathLength)
		{
			return ScriptingInterfaceOfIScene.call_GetPathDistanceBetweenPositionsDelegate(scenePointer, ref position, ref destination, agentRadius, ref pathLength);
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00013484 File Offset: 0x00011684
		public void GetPathsWithNamePrefix(UIntPtr ptr, UIntPtr[] points, string prefix)
		{
			PinnedArrayData<UIntPtr> pinnedArrayData = new PinnedArrayData<UIntPtr>(points, false);
			IntPtr pointer = pinnedArrayData.Pointer;
			byte[] array = null;
			if (prefix != null)
			{
				int byteCount = ScriptingInterfaceOfIScene._utf8.GetByteCount(prefix);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIScene._utf8.GetBytes(prefix, 0, prefix.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIScene.call_GetPathsWithNamePrefixDelegate(ptr, pointer, array);
			pinnedArrayData.Dispose();
		}

		// Token: 0x0600039C RID: 924 RVA: 0x000134F8 File Offset: 0x000116F8
		public Path GetPathWithName(UIntPtr scenePointer, string name)
		{
			byte[] array = null;
			if (name != null)
			{
				int byteCount = ScriptingInterfaceOfIScene._utf8.GetByteCount(name);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIScene._utf8.GetBytes(name, 0, name.Length, array, 0);
				array[byteCount] = 0;
			}
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIScene.call_GetPathWithNameDelegate(scenePointer, array);
			Path result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new Path(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00013588 File Offset: 0x00011788
		public void GetPhotoModeFocus(Scene scene, ref float focus, ref float focusStart, ref float focusEnd, ref float exposure, ref bool vignetteOn)
		{
			UIntPtr scene2 = (scene != null) ? scene.Pointer : UIntPtr.Zero;
			ScriptingInterfaceOfIScene.call_GetPhotoModeFocusDelegate(scene2, ref focus, ref focusStart, ref focusEnd, ref exposure, ref vignetteOn);
		}

		// Token: 0x0600039E RID: 926 RVA: 0x000135C0 File Offset: 0x000117C0
		public float GetPhotoModeFov(Scene scene)
		{
			UIntPtr scene2 = (scene != null) ? scene.Pointer : UIntPtr.Zero;
			return ScriptingInterfaceOfIScene.call_GetPhotoModeFovDelegate(scene2);
		}

		// Token: 0x0600039F RID: 927 RVA: 0x000135F0 File Offset: 0x000117F0
		public bool GetPhotoModeOn(Scene scene)
		{
			UIntPtr scene2 = (scene != null) ? scene.Pointer : UIntPtr.Zero;
			return ScriptingInterfaceOfIScene.call_GetPhotoModeOnDelegate(scene2);
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00013620 File Offset: 0x00011820
		public bool GetPhotoModeOrbit(Scene scene)
		{
			UIntPtr scene2 = (scene != null) ? scene.Pointer : UIntPtr.Zero;
			return ScriptingInterfaceOfIScene.call_GetPhotoModeOrbitDelegate(scene2);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00013650 File Offset: 0x00011850
		public float GetPhotoModeRoll(Scene scene)
		{
			UIntPtr scene2 = (scene != null) ? scene.Pointer : UIntPtr.Zero;
			return ScriptingInterfaceOfIScene.call_GetPhotoModeRollDelegate(scene2);
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0001367F File Offset: 0x0001187F
		public void GetPhysicsMinMax(UIntPtr scenePointer, ref Vec3 min_max)
		{
			ScriptingInterfaceOfIScene.call_GetPhysicsMinMaxDelegate(scenePointer, ref min_max);
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0001368D File Offset: 0x0001188D
		public float GetRainDensity(UIntPtr scenePointer)
		{
			return ScriptingInterfaceOfIScene.call_GetRainDensityDelegate(scenePointer);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0001369C File Offset: 0x0001189C
		public void GetRootEntities(Scene scene, NativeObjectArray output)
		{
			UIntPtr scene2 = (scene != null) ? scene.Pointer : UIntPtr.Zero;
			UIntPtr output2 = (output != null) ? output.Pointer : UIntPtr.Zero;
			ScriptingInterfaceOfIScene.call_GetRootEntitiesDelegate(scene2, output2);
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x000136E3 File Offset: 0x000118E3
		public int GetRootEntityCount(UIntPtr scenePointer)
		{
			return ScriptingInterfaceOfIScene.call_GetRootEntityCountDelegate(scenePointer);
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x000136F0 File Offset: 0x000118F0
		public int GetSceneColorGradeIndex(Scene scene)
		{
			UIntPtr scene2 = (scene != null) ? scene.Pointer : UIntPtr.Zero;
			return ScriptingInterfaceOfIScene.call_GetSceneColorGradeIndexDelegate(scene2);
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00013720 File Offset: 0x00011920
		public int GetSceneFilterIndex(Scene scene)
		{
			UIntPtr scene2 = (scene != null) ? scene.Pointer : UIntPtr.Zero;
			return ScriptingInterfaceOfIScene.call_GetSceneFilterIndexDelegate(scene2);
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00013750 File Offset: 0x00011950
		public GameEntity GetScriptedEntity(UIntPtr scenePointer, int index)
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIScene.call_GetScriptedEntityDelegate(scenePointer, index);
			GameEntity result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new GameEntity(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0001379B File Offset: 0x0001199B
		public int GetScriptedEntityCount(UIntPtr scenePointer)
		{
			return ScriptingInterfaceOfIScene.call_GetScriptedEntityCountDelegate(scenePointer);
		}

		// Token: 0x060003AA RID: 938 RVA: 0x000137A8 File Offset: 0x000119A8
		public Mesh GetSkyboxMesh(UIntPtr ptr)
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIScene.call_GetSkyboxMeshDelegate(ptr);
			Mesh result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new Mesh(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x060003AB RID: 939 RVA: 0x000137F4 File Offset: 0x000119F4
		public void GetSnowAmountData(UIntPtr scenePointer, byte[] bytes)
		{
			PinnedArrayData<byte> pinnedArrayData = new PinnedArrayData<byte>(bytes, false);
			IntPtr pointer = pinnedArrayData.Pointer;
			ManagedArray bytes2 = new ManagedArray(pointer, (bytes != null) ? bytes.Length : 0);
			ScriptingInterfaceOfIScene.call_GetSnowAmountDataDelegate(scenePointer, bytes2);
			pinnedArrayData.Dispose();
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00013836 File Offset: 0x00011A36
		public float GetSnowDensity(UIntPtr scenePointer)
		{
			return ScriptingInterfaceOfIScene.call_GetSnowDensityDelegate(scenePointer);
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00013843 File Offset: 0x00011A43
		public Vec2 GetSoftBoundaryVertex(UIntPtr scenePointer, int index)
		{
			return ScriptingInterfaceOfIScene.call_GetSoftBoundaryVertexDelegate(scenePointer, index);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00013851 File Offset: 0x00011A51
		public int GetSoftBoundaryVertexCount(UIntPtr scenePointer)
		{
			return ScriptingInterfaceOfIScene.call_GetSoftBoundaryVertexCountDelegate(scenePointer);
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0001385E File Offset: 0x00011A5E
		public Vec3 GetSunDirection(UIntPtr scenePointer)
		{
			return ScriptingInterfaceOfIScene.call_GetSunDirectionDelegate(scenePointer);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0001386C File Offset: 0x00011A6C
		public void GetTerrainData(Scene scene, out Vec2i nodeDimension, out float nodeSize, out int layerCount, out int layerVersion)
		{
			UIntPtr scene2 = (scene != null) ? scene.Pointer : UIntPtr.Zero;
			ScriptingInterfaceOfIScene.call_GetTerrainDataDelegate(scene2, out nodeDimension, out nodeSize, out layerCount, out layerVersion);
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x000138A1 File Offset: 0x00011AA1
		public float GetTerrainHeight(UIntPtr scenePointer, Vec2 position, bool checkHoles)
		{
			return ScriptingInterfaceOfIScene.call_GetTerrainHeightDelegate(scenePointer, position, checkHoles);
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x000138B0 File Offset: 0x00011AB0
		public void GetTerrainHeightAndNormal(UIntPtr scenePointer, Vec2 position, out float height, out Vec3 normal)
		{
			ScriptingInterfaceOfIScene.call_GetTerrainHeightAndNormalDelegate(scenePointer, position, out height, out normal);
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x000138C1 File Offset: 0x00011AC1
		public int GetTerrainMemoryUsage(UIntPtr scenePointer)
		{
			return ScriptingInterfaceOfIScene.call_GetTerrainMemoryUsageDelegate(scenePointer);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x000138D0 File Offset: 0x00011AD0
		public bool GetTerrainMinMaxHeight(Scene scene, ref float min, ref float max)
		{
			UIntPtr scene2 = (scene != null) ? scene.Pointer : UIntPtr.Zero;
			return ScriptingInterfaceOfIScene.call_GetTerrainMinMaxHeightDelegate(scene2, ref min, ref max);
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00013904 File Offset: 0x00011B04
		public void GetTerrainNodeData(Scene scene, int xIndex, int yIndex, out int vertexCountAlongAxis, out float quadLength, out float minHeight, out float maxHeight)
		{
			UIntPtr scene2 = (scene != null) ? scene.Pointer : UIntPtr.Zero;
			ScriptingInterfaceOfIScene.call_GetTerrainNodeDataDelegate(scene2, xIndex, yIndex, out vertexCountAlongAxis, out quadLength, out minHeight, out maxHeight);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00013940 File Offset: 0x00011B40
		public int GetTerrainPhysicsMaterialIndexAtLayer(Scene scene, int layerIndex)
		{
			UIntPtr scene2 = (scene != null) ? scene.Pointer : UIntPtr.Zero;
			return ScriptingInterfaceOfIScene.call_GetTerrainPhysicsMaterialIndexAtLayerDelegate(scene2, layerIndex);
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00013970 File Offset: 0x00011B70
		public float GetTimeOfDay(UIntPtr scenePointer)
		{
			return ScriptingInterfaceOfIScene.call_GetTimeOfDayDelegate(scenePointer);
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0001397D File Offset: 0x00011B7D
		public float GetTimeSpeed(UIntPtr scenePointer)
		{
			return ScriptingInterfaceOfIScene.call_GetTimeSpeedDelegate(scenePointer);
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0001398A File Offset: 0x00011B8A
		public int GetUpgradeLevelCount(UIntPtr scenePointer)
		{
			return ScriptingInterfaceOfIScene.call_GetUpgradeLevelCountDelegate(scenePointer);
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00013997 File Offset: 0x00011B97
		public uint GetUpgradeLevelMask(UIntPtr scenePointer)
		{
			return ScriptingInterfaceOfIScene.call_GetUpgradeLevelMaskDelegate(scenePointer);
		}

		// Token: 0x060003BB RID: 955 RVA: 0x000139A4 File Offset: 0x00011BA4
		public uint GetUpgradeLevelMaskOfLevelName(UIntPtr scenePointer, string levelName)
		{
			byte[] array = null;
			if (levelName != null)
			{
				int byteCount = ScriptingInterfaceOfIScene._utf8.GetByteCount(levelName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIScene._utf8.GetBytes(levelName, 0, levelName.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfIScene.call_GetUpgradeLevelMaskOfLevelNameDelegate(scenePointer, array);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x000139FF File Offset: 0x00011BFF
		public string GetUpgradeLevelNameOfIndex(UIntPtr scenePointer, int index)
		{
			if (ScriptingInterfaceOfIScene.call_GetUpgradeLevelNameOfIndexDelegate(scenePointer, index) != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00013A18 File Offset: 0x00011C18
		public float GetWaterLevel(Scene scene)
		{
			UIntPtr scene2 = (scene != null) ? scene.Pointer : UIntPtr.Zero;
			return ScriptingInterfaceOfIScene.call_GetWaterLevelDelegate(scene2);
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00013A48 File Offset: 0x00011C48
		public float GetWaterLevelAtPosition(Scene scene, Vec2 position, bool checkWaterBodyEntities)
		{
			UIntPtr scene2 = (scene != null) ? scene.Pointer : UIntPtr.Zero;
			return ScriptingInterfaceOfIScene.call_GetWaterLevelAtPositionDelegate(scene2, position, checkWaterBodyEntities);
		}

		// Token: 0x060003BF RID: 959 RVA: 0x00013A79 File Offset: 0x00011C79
		public float GetWinterTimeFactor(UIntPtr scenePointer)
		{
			return ScriptingInterfaceOfIScene.call_GetWinterTimeFactorDelegate(scenePointer);
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x00013A86 File Offset: 0x00011C86
		public bool HasTerrainHeightmap(UIntPtr scenePointer)
		{
			return ScriptingInterfaceOfIScene.call_HasTerrainHeightmapDelegate(scenePointer);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00013A93 File Offset: 0x00011C93
		public void InvalidateTerrainPhysicsMaterials(UIntPtr scenePointer)
		{
			ScriptingInterfaceOfIScene.call_InvalidateTerrainPhysicsMaterialsDelegate(scenePointer);
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00013AA0 File Offset: 0x00011CA0
		public bool IsAnyFaceWithId(UIntPtr scenePointer, int faceGroupId)
		{
			return ScriptingInterfaceOfIScene.call_IsAnyFaceWithIdDelegate(scenePointer, faceGroupId);
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00013AAE File Offset: 0x00011CAE
		public bool IsAtmosphereIndoor(UIntPtr scenePointer)
		{
			return ScriptingInterfaceOfIScene.call_IsAtmosphereIndoorDelegate(scenePointer);
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00013ABC File Offset: 0x00011CBC
		public bool IsDefaultEditorScene(Scene scene)
		{
			UIntPtr scene2 = (scene != null) ? scene.Pointer : UIntPtr.Zero;
			return ScriptingInterfaceOfIScene.call_IsDefaultEditorSceneDelegate(scene2);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00013AEB File Offset: 0x00011CEB
		public bool IsEditorScene(UIntPtr scenePointer)
		{
			return ScriptingInterfaceOfIScene.call_IsEditorSceneDelegate(scenePointer);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00013AF8 File Offset: 0x00011CF8
		public bool IsLineToPointClear(UIntPtr scenePointer, int startingFace, Vec2 position, Vec2 destination, float agentRadius)
		{
			return ScriptingInterfaceOfIScene.call_IsLineToPointClearDelegate(scenePointer, startingFace, position, destination, agentRadius);
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00013B0B File Offset: 0x00011D0B
		public bool IsLineToPointClear2(UIntPtr scenePointer, UIntPtr startingFace, Vec2 position, Vec2 destination, float agentRadius)
		{
			return ScriptingInterfaceOfIScene.call_IsLineToPointClear2Delegate(scenePointer, startingFace, position, destination, agentRadius);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00013B20 File Offset: 0x00011D20
		public bool IsLoadingFinished(Scene scene)
		{
			UIntPtr scene2 = (scene != null) ? scene.Pointer : UIntPtr.Zero;
			return ScriptingInterfaceOfIScene.call_IsLoadingFinishedDelegate(scene2);
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00013B50 File Offset: 0x00011D50
		public bool IsMultiplayerScene(Scene scene)
		{
			UIntPtr scene2 = (scene != null) ? scene.Pointer : UIntPtr.Zero;
			return ScriptingInterfaceOfIScene.call_IsMultiplayerSceneDelegate(scene2);
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00013B80 File Offset: 0x00011D80
		public void LoadNavMeshPrefab(UIntPtr scenePointer, string navMeshPrefabName, int navMeshGroupIdShift)
		{
			byte[] array = null;
			if (navMeshPrefabName != null)
			{
				int byteCount = ScriptingInterfaceOfIScene._utf8.GetByteCount(navMeshPrefabName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIScene._utf8.GetBytes(navMeshPrefabName, 0, navMeshPrefabName.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIScene.call_LoadNavMeshPrefabDelegate(scenePointer, array, navMeshGroupIdShift);
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00013BDC File Offset: 0x00011DDC
		public void MarkFacesWithIdAsLadder(UIntPtr scenePointer, int faceGroupId, bool isLadder)
		{
			ScriptingInterfaceOfIScene.call_MarkFacesWithIdAsLadderDelegate(scenePointer, faceGroupId, isLadder);
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00013BEB File Offset: 0x00011DEB
		public void MergeFacesWithId(UIntPtr scenePointer, int faceGroupId0, int faceGroupId1, int newFaceGroupId)
		{
			ScriptingInterfaceOfIScene.call_MergeFacesWithIdDelegate(scenePointer, faceGroupId0, faceGroupId1, newFaceGroupId);
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00013BFC File Offset: 0x00011DFC
		public void OptimizeScene(UIntPtr scenePointer, bool optimizeFlora, bool optimizeOro)
		{
			ScriptingInterfaceOfIScene.call_OptimizeSceneDelegate(scenePointer, optimizeFlora, optimizeOro);
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00013C0B File Offset: 0x00011E0B
		public void PauseSceneSounds(UIntPtr scenePointer)
		{
			ScriptingInterfaceOfIScene.call_PauseSceneSoundsDelegate(scenePointer);
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00013C18 File Offset: 0x00011E18
		public void PreloadForRendering(UIntPtr scenePointer)
		{
			ScriptingInterfaceOfIScene.call_PreloadForRenderingDelegate(scenePointer);
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00013C28 File Offset: 0x00011E28
		public bool RayCastForClosestEntityOrTerrain(UIntPtr scenePointer, ref Vec3 sourcePoint, ref Vec3 targetPoint, float rayThickness, ref float collisionDistance, ref Vec3 closestPoint, ref UIntPtr entityIndex, BodyFlags bodyExcludeFlags)
		{
			return ScriptingInterfaceOfIScene.call_RayCastForClosestEntityOrTerrainDelegate(scenePointer, ref sourcePoint, ref targetPoint, rayThickness, ref collisionDistance, ref closestPoint, ref entityIndex, bodyExcludeFlags);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00013C4C File Offset: 0x00011E4C
		public void Read(UIntPtr scenePointer, string sceneName, ref SceneInitializationData initData, string forcedAtmoName)
		{
			byte[] array = null;
			if (sceneName != null)
			{
				int byteCount = ScriptingInterfaceOfIScene._utf8.GetByteCount(sceneName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIScene._utf8.GetBytes(sceneName, 0, sceneName.Length, array, 0);
				array[byteCount] = 0;
			}
			byte[] array2 = null;
			if (forcedAtmoName != null)
			{
				int byteCount2 = ScriptingInterfaceOfIScene._utf8.GetByteCount(forcedAtmoName);
				array2 = ((byteCount2 < 1024) ? CallbackStringBufferManager.StringBuffer1 : new byte[byteCount2 + 1]);
				ScriptingInterfaceOfIScene._utf8.GetBytes(forcedAtmoName, 0, forcedAtmoName.Length, array2, 0);
				array2[byteCount2] = 0;
			}
			ScriptingInterfaceOfIScene.call_ReadDelegate(scenePointer, array, ref initData, array2);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00013CEF File Offset: 0x00011EEF
		public void ReadAndCalculateInitialCamera(UIntPtr scenePointer, ref MatrixFrame outFrame)
		{
			ScriptingInterfaceOfIScene.call_ReadAndCalculateInitialCameraDelegate(scenePointer, ref outFrame);
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00013CFD File Offset: 0x00011EFD
		public void RemoveEntity(UIntPtr scenePointer, UIntPtr entityId, int removeReason)
		{
			ScriptingInterfaceOfIScene.call_RemoveEntityDelegate(scenePointer, entityId, removeReason);
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00013D0C File Offset: 0x00011F0C
		public void ResumeLoadingRenderings(UIntPtr scenePointer)
		{
			ScriptingInterfaceOfIScene.call_ResumeLoadingRenderingsDelegate(scenePointer);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00013D19 File Offset: 0x00011F19
		public void ResumeSceneSounds(UIntPtr scenePointer)
		{
			ScriptingInterfaceOfIScene.call_ResumeSceneSoundsDelegate(scenePointer);
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00013D28 File Offset: 0x00011F28
		public int SelectEntitiesCollidedWith(UIntPtr scenePointer, ref Ray ray, UIntPtr[] entityIds, Intersection[] intersections)
		{
			PinnedArrayData<UIntPtr> pinnedArrayData = new PinnedArrayData<UIntPtr>(entityIds, false);
			IntPtr pointer = pinnedArrayData.Pointer;
			PinnedArrayData<Intersection> pinnedArrayData2 = new PinnedArrayData<Intersection>(intersections, false);
			IntPtr pointer2 = pinnedArrayData2.Pointer;
			int result = ScriptingInterfaceOfIScene.call_SelectEntitiesCollidedWithDelegate(scenePointer, ref ray, pointer, pointer2);
			pinnedArrayData.Dispose();
			pinnedArrayData2.Dispose();
			return result;
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00013D74 File Offset: 0x00011F74
		public int SelectEntitiesInBoxWithScriptComponent(UIntPtr scenePointer, ref Vec3 boundingBoxMin, ref Vec3 boundingBoxMax, UIntPtr[] entitiesOutput, int maxCount, string scriptComponentName)
		{
			PinnedArrayData<UIntPtr> pinnedArrayData = new PinnedArrayData<UIntPtr>(entitiesOutput, false);
			IntPtr pointer = pinnedArrayData.Pointer;
			byte[] array = null;
			if (scriptComponentName != null)
			{
				int byteCount = ScriptingInterfaceOfIScene._utf8.GetByteCount(scriptComponentName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIScene._utf8.GetBytes(scriptComponentName, 0, scriptComponentName.Length, array, 0);
				array[byteCount] = 0;
			}
			int result = ScriptingInterfaceOfIScene.call_SelectEntitiesInBoxWithScriptComponentDelegate(scenePointer, ref boundingBoxMin, ref boundingBoxMax, pointer, maxCount, array);
			pinnedArrayData.Dispose();
			return result;
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00013DF1 File Offset: 0x00011FF1
		public void SeparateFacesWithId(UIntPtr scenePointer, int faceGroupId0, int faceGroupId1)
		{
			ScriptingInterfaceOfIScene.call_SeparateFacesWithIdDelegate(scenePointer, faceGroupId0, faceGroupId1);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00013E00 File Offset: 0x00012000
		public void SetAberrationOffset(UIntPtr scenePointer, float aberrationOffset)
		{
			ScriptingInterfaceOfIScene.call_SetAberrationOffsetDelegate(scenePointer, aberrationOffset);
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00013E0E File Offset: 0x0001200E
		public void SetAberrationSize(UIntPtr scenePointer, float aberrationSize)
		{
			ScriptingInterfaceOfIScene.call_SetAberrationSizeDelegate(scenePointer, aberrationSize);
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00013E1C File Offset: 0x0001201C
		public void SetAberrationSmooth(UIntPtr scenePointer, float aberrationSmooth)
		{
			ScriptingInterfaceOfIScene.call_SetAberrationSmoothDelegate(scenePointer, aberrationSmooth);
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00013E2A File Offset: 0x0001202A
		public void SetAbilityOfFacesWithId(UIntPtr scenePointer, int faceGroupId, bool isEnabled)
		{
			ScriptingInterfaceOfIScene.call_SetAbilityOfFacesWithIdDelegate(scenePointer, faceGroupId, isEnabled);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00013E3C File Offset: 0x0001203C
		public void SetActiveVisibilityLevels(UIntPtr scenePointer, string levelsAppended)
		{
			byte[] array = null;
			if (levelsAppended != null)
			{
				int byteCount = ScriptingInterfaceOfIScene._utf8.GetByteCount(levelsAppended);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIScene._utf8.GetBytes(levelsAppended, 0, levelsAppended.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIScene.call_SetActiveVisibilityLevelsDelegate(scenePointer, array);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00013E97 File Offset: 0x00012097
		public void SetAntialiasingMode(UIntPtr scenePointer, bool mode)
		{
			ScriptingInterfaceOfIScene.call_SetAntialiasingModeDelegate(scenePointer, mode);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00013EA8 File Offset: 0x000120A8
		public void SetAtmosphereWithName(UIntPtr ptr, string name)
		{
			byte[] array = null;
			if (name != null)
			{
				int byteCount = ScriptingInterfaceOfIScene._utf8.GetByteCount(name);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIScene._utf8.GetBytes(name, 0, name.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIScene.call_SetAtmosphereWithNameDelegate(ptr, array);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00013F03 File Offset: 0x00012103
		public void SetBloom(UIntPtr scenePointer, bool mode)
		{
			ScriptingInterfaceOfIScene.call_SetBloomDelegate(scenePointer, mode);
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00013F11 File Offset: 0x00012111
		public void SetBloomAmount(UIntPtr scenePointer, float bloomAmount)
		{
			ScriptingInterfaceOfIScene.call_SetBloomAmountDelegate(scenePointer, bloomAmount);
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00013F1F File Offset: 0x0001211F
		public void SetBloomStrength(UIntPtr scenePointer, float bloomStrength)
		{
			ScriptingInterfaceOfIScene.call_SetBloomStrengthDelegate(scenePointer, bloomStrength);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00013F2D File Offset: 0x0001212D
		public void SetBrightpassTreshold(UIntPtr scenePointer, float threshold)
		{
			ScriptingInterfaceOfIScene.call_SetBrightpassTresholdDelegate(scenePointer, threshold);
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00013F3B File Offset: 0x0001213B
		public void SetClothSimulationState(UIntPtr scenePointer, bool state)
		{
			ScriptingInterfaceOfIScene.call_SetClothSimulationStateDelegate(scenePointer, state);
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00013F4C File Offset: 0x0001214C
		public void SetColorGradeBlend(UIntPtr scenePointer, string texture1, string texture2, float alpha)
		{
			byte[] array = null;
			if (texture1 != null)
			{
				int byteCount = ScriptingInterfaceOfIScene._utf8.GetByteCount(texture1);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIScene._utf8.GetBytes(texture1, 0, texture1.Length, array, 0);
				array[byteCount] = 0;
			}
			byte[] array2 = null;
			if (texture2 != null)
			{
				int byteCount2 = ScriptingInterfaceOfIScene._utf8.GetByteCount(texture2);
				array2 = ((byteCount2 < 1024) ? CallbackStringBufferManager.StringBuffer1 : new byte[byteCount2 + 1]);
				ScriptingInterfaceOfIScene._utf8.GetBytes(texture2, 0, texture2.Length, array2, 0);
				array2[byteCount2] = 0;
			}
			ScriptingInterfaceOfIScene.call_SetColorGradeBlendDelegate(scenePointer, array, array2, alpha);
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00013FEC File Offset: 0x000121EC
		public void SetDLSSMode(UIntPtr scenePointer, bool mode)
		{
			ScriptingInterfaceOfIScene.call_SetDLSSModeDelegate(scenePointer, mode);
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00013FFA File Offset: 0x000121FA
		public void SetDofFocus(UIntPtr scenePointer, float dofFocus)
		{
			ScriptingInterfaceOfIScene.call_SetDofFocusDelegate(scenePointer, dofFocus);
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00014008 File Offset: 0x00012208
		public void SetDofMode(UIntPtr scenePointer, bool mode)
		{
			ScriptingInterfaceOfIScene.call_SetDofModeDelegate(scenePointer, mode);
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00014016 File Offset: 0x00012216
		public void SetDofParams(UIntPtr scenePointer, float dofFocusStart, float dofFocusEnd, bool isVignetteOn)
		{
			ScriptingInterfaceOfIScene.call_SetDofParamsDelegate(scenePointer, dofFocusStart, dofFocusEnd, isVignetteOn);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00014027 File Offset: 0x00012227
		public void SetDoNotWaitForLoadingStatesToRender(UIntPtr scenePointer, bool value)
		{
			ScriptingInterfaceOfIScene.call_SetDoNotWaitForLoadingStatesToRenderDelegate(scenePointer, value);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00014035 File Offset: 0x00012235
		public void SetDrynessFactor(UIntPtr scenePointer, float drynessFactor)
		{
			ScriptingInterfaceOfIScene.call_SetDrynessFactorDelegate(scenePointer, drynessFactor);
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00014043 File Offset: 0x00012243
		public void SetDynamicShadowmapCascadesRadiusMultiplier(UIntPtr scenePointer, float extraRadius)
		{
			ScriptingInterfaceOfIScene.call_SetDynamicShadowmapCascadesRadiusMultiplierDelegate(scenePointer, extraRadius);
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00014051 File Offset: 0x00012251
		public void SetEnvironmentMultiplier(UIntPtr scenePointer, bool useMultiplier, float multiplier)
		{
			ScriptingInterfaceOfIScene.call_SetEnvironmentMultiplierDelegate(scenePointer, useMultiplier, multiplier);
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00014060 File Offset: 0x00012260
		public void SetExternalInjectionTexture(UIntPtr scenePointer, UIntPtr texturePointer)
		{
			ScriptingInterfaceOfIScene.call_SetExternalInjectionTextureDelegate(scenePointer, texturePointer);
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0001406E File Offset: 0x0001226E
		public void SetFog(UIntPtr scenePointer, float fogDensity, ref Vec3 fogColor, float fogFalloff)
		{
			ScriptingInterfaceOfIScene.call_SetFogDelegate(scenePointer, fogDensity, ref fogColor, fogFalloff);
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0001407F File Offset: 0x0001227F
		public void SetFogAdvanced(UIntPtr scenePointer, float fogFalloffOffset, float fogFalloffMinFog, float fogFalloffStartDist)
		{
			ScriptingInterfaceOfIScene.call_SetFogAdvancedDelegate(scenePointer, fogFalloffOffset, fogFalloffMinFog, fogFalloffStartDist);
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00014090 File Offset: 0x00012290
		public void SetFogAmbientColor(UIntPtr scenePointer, ref Vec3 fogAmbientColor)
		{
			ScriptingInterfaceOfIScene.call_SetFogAmbientColorDelegate(scenePointer, ref fogAmbientColor);
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0001409E File Offset: 0x0001229E
		public void SetForcedSnow(UIntPtr scenePointer, bool value)
		{
			ScriptingInterfaceOfIScene.call_SetForcedSnowDelegate(scenePointer, value);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x000140AC File Offset: 0x000122AC
		public void SetGrainAmount(UIntPtr scenePointer, float grainAmount)
		{
			ScriptingInterfaceOfIScene.call_SetGrainAmountDelegate(scenePointer, grainAmount);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x000140BA File Offset: 0x000122BA
		public void SetHexagonVignetteAlpha(UIntPtr scenePointer, float Alpha)
		{
			ScriptingInterfaceOfIScene.call_SetHexagonVignetteAlphaDelegate(scenePointer, Alpha);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x000140C8 File Offset: 0x000122C8
		public void SetHexagonVignetteColor(UIntPtr scenePointer, ref Vec3 p_hexagon_vignette_color)
		{
			ScriptingInterfaceOfIScene.call_SetHexagonVignetteColorDelegate(scenePointer, ref p_hexagon_vignette_color);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x000140D6 File Offset: 0x000122D6
		public void SetHumidity(UIntPtr scenePointer, float humidity)
		{
			ScriptingInterfaceOfIScene.call_SetHumidityDelegate(scenePointer, humidity);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x000140E4 File Offset: 0x000122E4
		public void SetLandscapeRainMaskData(UIntPtr scenePointer, byte[] data)
		{
			PinnedArrayData<byte> pinnedArrayData = new PinnedArrayData<byte>(data, false);
			IntPtr pointer = pinnedArrayData.Pointer;
			ManagedArray data2 = new ManagedArray(pointer, (data != null) ? data.Length : 0);
			ScriptingInterfaceOfIScene.call_SetLandscapeRainMaskDataDelegate(scenePointer, data2);
			pinnedArrayData.Dispose();
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00014126 File Offset: 0x00012326
		public void SetLensDistortion(UIntPtr scenePointer, float lensDistortion)
		{
			ScriptingInterfaceOfIScene.call_SetLensDistortionDelegate(scenePointer, lensDistortion);
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00014134 File Offset: 0x00012334
		public void SetLensFlareAberrationOffset(UIntPtr scenePointer, float lensFlareAberrationOffset)
		{
			ScriptingInterfaceOfIScene.call_SetLensFlareAberrationOffsetDelegate(scenePointer, lensFlareAberrationOffset);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00014142 File Offset: 0x00012342
		public void SetLensFlareAmount(UIntPtr scenePointer, float lensFlareAmount)
		{
			ScriptingInterfaceOfIScene.call_SetLensFlareAmountDelegate(scenePointer, lensFlareAmount);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00014150 File Offset: 0x00012350
		public void SetLensFlareBlurSigma(UIntPtr scenePointer, float lensFlareBlurSigma)
		{
			ScriptingInterfaceOfIScene.call_SetLensFlareBlurSigmaDelegate(scenePointer, lensFlareBlurSigma);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0001415E File Offset: 0x0001235E
		public void SetLensFlareBlurSize(UIntPtr scenePointer, int lensFlareBlurSize)
		{
			ScriptingInterfaceOfIScene.call_SetLensFlareBlurSizeDelegate(scenePointer, lensFlareBlurSize);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0001416C File Offset: 0x0001236C
		public void SetLensFlareDiffractionWeight(UIntPtr scenePointer, float lensFlareDiffractionWeight)
		{
			ScriptingInterfaceOfIScene.call_SetLensFlareDiffractionWeightDelegate(scenePointer, lensFlareDiffractionWeight);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0001417A File Offset: 0x0001237A
		public void SetLensFlareDirtWeight(UIntPtr scenePointer, float lensFlareDirtWeight)
		{
			ScriptingInterfaceOfIScene.call_SetLensFlareDirtWeightDelegate(scenePointer, lensFlareDirtWeight);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00014188 File Offset: 0x00012388
		public void SetLensFlareGhostSamples(UIntPtr scenePointer, int lensFlareGhostSamples)
		{
			ScriptingInterfaceOfIScene.call_SetLensFlareGhostSamplesDelegate(scenePointer, lensFlareGhostSamples);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00014196 File Offset: 0x00012396
		public void SetLensFlareGhostWeight(UIntPtr scenePointer, float lensFlareGhostWeight)
		{
			ScriptingInterfaceOfIScene.call_SetLensFlareGhostWeightDelegate(scenePointer, lensFlareGhostWeight);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x000141A4 File Offset: 0x000123A4
		public void SetLensFlareHaloWeight(UIntPtr scenePointer, float lensFlareHaloWeight)
		{
			ScriptingInterfaceOfIScene.call_SetLensFlareHaloWeightDelegate(scenePointer, lensFlareHaloWeight);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x000141B2 File Offset: 0x000123B2
		public void SetLensFlareHaloWidth(UIntPtr scenePointer, float lensFlareHaloWidth)
		{
			ScriptingInterfaceOfIScene.call_SetLensFlareHaloWidthDelegate(scenePointer, lensFlareHaloWidth);
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x000141C0 File Offset: 0x000123C0
		public void SetLensFlareStrength(UIntPtr scenePointer, float lensFlareStrength)
		{
			ScriptingInterfaceOfIScene.call_SetLensFlareStrengthDelegate(scenePointer, lensFlareStrength);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x000141CE File Offset: 0x000123CE
		public void SetLensFlareThreshold(UIntPtr scenePointer, float lensFlareThreshold)
		{
			ScriptingInterfaceOfIScene.call_SetLensFlareThresholdDelegate(scenePointer, lensFlareThreshold);
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x000141DC File Offset: 0x000123DC
		public void SetLightDiffuseColor(UIntPtr scenePointer, int lightIndex, Vec3 diffuseColor)
		{
			ScriptingInterfaceOfIScene.call_SetLightDiffuseColorDelegate(scenePointer, lightIndex, diffuseColor);
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x000141EB File Offset: 0x000123EB
		public void SetLightDirection(UIntPtr scenePointer, int lightIndex, Vec3 direction)
		{
			ScriptingInterfaceOfIScene.call_SetLightDirectionDelegate(scenePointer, lightIndex, direction);
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x000141FA File Offset: 0x000123FA
		public void SetLightPosition(UIntPtr scenePointer, int lightIndex, Vec3 position)
		{
			ScriptingInterfaceOfIScene.call_SetLightPositionDelegate(scenePointer, lightIndex, position);
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00014209 File Offset: 0x00012409
		public void SetMaxExposure(UIntPtr scenePointer, float maxExposure)
		{
			ScriptingInterfaceOfIScene.call_SetMaxExposureDelegate(scenePointer, maxExposure);
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00014217 File Offset: 0x00012417
		public void SetMiddleGray(UIntPtr scenePointer, float middleGray)
		{
			ScriptingInterfaceOfIScene.call_SetMiddleGrayDelegate(scenePointer, middleGray);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00014225 File Offset: 0x00012425
		public void SetMieScatterFocus(UIntPtr scenePointer, float strength)
		{
			ScriptingInterfaceOfIScene.call_SetMieScatterFocusDelegate(scenePointer, strength);
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00014233 File Offset: 0x00012433
		public void SetMieScatterStrength(UIntPtr scenePointer, float strength)
		{
			ScriptingInterfaceOfIScene.call_SetMieScatterStrengthDelegate(scenePointer, strength);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00014241 File Offset: 0x00012441
		public void SetMinExposure(UIntPtr scenePointer, float minExposure)
		{
			ScriptingInterfaceOfIScene.call_SetMinExposureDelegate(scenePointer, minExposure);
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0001424F File Offset: 0x0001244F
		public void SetMotionBlurMode(UIntPtr scenePointer, bool mode)
		{
			ScriptingInterfaceOfIScene.call_SetMotionBlurModeDelegate(scenePointer, mode);
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00014260 File Offset: 0x00012460
		public void SetName(UIntPtr scenePointer, string name)
		{
			byte[] array = null;
			if (name != null)
			{
				int byteCount = ScriptingInterfaceOfIScene._utf8.GetByteCount(name);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIScene._utf8.GetBytes(name, 0, name.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIScene.call_SetNameDelegate(scenePointer, array);
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x000142BB File Offset: 0x000124BB
		public void SetOcclusionMode(UIntPtr scenePointer, bool mode)
		{
			ScriptingInterfaceOfIScene.call_SetOcclusionModeDelegate(scenePointer, mode);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x000142C9 File Offset: 0x000124C9
		public void SetOwnerThread(UIntPtr scenePointer)
		{
			ScriptingInterfaceOfIScene.call_SetOwnerThreadDelegate(scenePointer);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x000142D8 File Offset: 0x000124D8
		public void SetPhotoModeFocus(Scene scene, float focusStart, float focusEnd, float focus, float exposure)
		{
			UIntPtr scene2 = (scene != null) ? scene.Pointer : UIntPtr.Zero;
			ScriptingInterfaceOfIScene.call_SetPhotoModeFocusDelegate(scene2, focusStart, focusEnd, focus, exposure);
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00014310 File Offset: 0x00012510
		public void SetPhotoModeFov(Scene scene, float verticalFov)
		{
			UIntPtr scene2 = (scene != null) ? scene.Pointer : UIntPtr.Zero;
			ScriptingInterfaceOfIScene.call_SetPhotoModeFovDelegate(scene2, verticalFov);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00014340 File Offset: 0x00012540
		public void SetPhotoModeOn(Scene scene, bool on)
		{
			UIntPtr scene2 = (scene != null) ? scene.Pointer : UIntPtr.Zero;
			ScriptingInterfaceOfIScene.call_SetPhotoModeOnDelegate(scene2, on);
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00014370 File Offset: 0x00012570
		public void SetPhotoModeOrbit(Scene scene, bool orbit)
		{
			UIntPtr scene2 = (scene != null) ? scene.Pointer : UIntPtr.Zero;
			ScriptingInterfaceOfIScene.call_SetPhotoModeOrbitDelegate(scene2, orbit);
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x000143A0 File Offset: 0x000125A0
		public void SetPhotoModeRoll(Scene scene, float roll)
		{
			UIntPtr scene2 = (scene != null) ? scene.Pointer : UIntPtr.Zero;
			ScriptingInterfaceOfIScene.call_SetPhotoModeRollDelegate(scene2, roll);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x000143D0 File Offset: 0x000125D0
		public void SetPhotoModeVignette(Scene scene, bool vignetteOn)
		{
			UIntPtr scene2 = (scene != null) ? scene.Pointer : UIntPtr.Zero;
			ScriptingInterfaceOfIScene.call_SetPhotoModeVignetteDelegate(scene2, vignetteOn);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00014400 File Offset: 0x00012600
		public void SetPlaySoundEventsAfterReadyToRender(UIntPtr ptr, bool value)
		{
			ScriptingInterfaceOfIScene.call_SetPlaySoundEventsAfterReadyToRenderDelegate(ptr, value);
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0001440E File Offset: 0x0001260E
		public void SetRainDensity(UIntPtr scenePointer, float density)
		{
			ScriptingInterfaceOfIScene.call_SetRainDensityDelegate(scenePointer, density);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0001441C File Offset: 0x0001261C
		public void SetSceneColorGrade(Scene scene, string textureName)
		{
			UIntPtr scene2 = (scene != null) ? scene.Pointer : UIntPtr.Zero;
			byte[] array = null;
			if (textureName != null)
			{
				int byteCount = ScriptingInterfaceOfIScene._utf8.GetByteCount(textureName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIScene._utf8.GetBytes(textureName, 0, textureName.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIScene.call_SetSceneColorGradeDelegate(scene2, array);
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00014490 File Offset: 0x00012690
		public void SetSceneColorGradeIndex(Scene scene, int index)
		{
			UIntPtr scene2 = (scene != null) ? scene.Pointer : UIntPtr.Zero;
			ScriptingInterfaceOfIScene.call_SetSceneColorGradeIndexDelegate(scene2, index);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x000144C0 File Offset: 0x000126C0
		public int SetSceneFilterIndex(Scene scene, int index)
		{
			UIntPtr scene2 = (scene != null) ? scene.Pointer : UIntPtr.Zero;
			return ScriptingInterfaceOfIScene.call_SetSceneFilterIndexDelegate(scene2, index);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x000144F0 File Offset: 0x000126F0
		public void SetShadow(UIntPtr scenePointer, bool shadowEnabled)
		{
			ScriptingInterfaceOfIScene.call_SetShadowDelegate(scenePointer, shadowEnabled);
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x000144FE File Offset: 0x000126FE
		public void SetSkyBrightness(UIntPtr scenePointer, float brightness)
		{
			ScriptingInterfaceOfIScene.call_SetSkyBrightnessDelegate(scenePointer, brightness);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0001450C File Offset: 0x0001270C
		public void SetSkyRotation(UIntPtr scenePointer, float rotation)
		{
			ScriptingInterfaceOfIScene.call_SetSkyRotationDelegate(scenePointer, rotation);
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0001451A File Offset: 0x0001271A
		public void SetSnowDensity(UIntPtr scenePointer, float density)
		{
			ScriptingInterfaceOfIScene.call_SetSnowDensityDelegate(scenePointer, density);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00014528 File Offset: 0x00012728
		public void SetStreakAmount(UIntPtr scenePointer, float streakAmount)
		{
			ScriptingInterfaceOfIScene.call_SetStreakAmountDelegate(scenePointer, streakAmount);
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00014536 File Offset: 0x00012736
		public void SetStreakIntensity(UIntPtr scenePointer, float stretchAmount)
		{
			ScriptingInterfaceOfIScene.call_SetStreakIntensityDelegate(scenePointer, stretchAmount);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00014544 File Offset: 0x00012744
		public void SetStreakStrength(UIntPtr scenePointer, float strengthAmount)
		{
			ScriptingInterfaceOfIScene.call_SetStreakStrengthDelegate(scenePointer, strengthAmount);
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00014552 File Offset: 0x00012752
		public void SetStreakStretch(UIntPtr scenePointer, float stretchAmount)
		{
			ScriptingInterfaceOfIScene.call_SetStreakStretchDelegate(scenePointer, stretchAmount);
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00014560 File Offset: 0x00012760
		public void SetStreakThreshold(UIntPtr scenePointer, float streakThreshold)
		{
			ScriptingInterfaceOfIScene.call_SetStreakThresholdDelegate(scenePointer, streakThreshold);
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0001456E File Offset: 0x0001276E
		public void SetStreakTint(UIntPtr scenePointer, ref Vec3 p_streak_tint_color)
		{
			ScriptingInterfaceOfIScene.call_SetStreakTintDelegate(scenePointer, ref p_streak_tint_color);
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0001457C File Offset: 0x0001277C
		public void SetSun(UIntPtr scenePointer, Vec3 color, float altitude, float angle, float intensity)
		{
			ScriptingInterfaceOfIScene.call_SetSunDelegate(scenePointer, color, altitude, angle, intensity);
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0001458F File Offset: 0x0001278F
		public void SetSunAngleAltitude(UIntPtr scenePointer, float angle, float altitude)
		{
			ScriptingInterfaceOfIScene.call_SetSunAngleAltitudeDelegate(scenePointer, angle, altitude);
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0001459E File Offset: 0x0001279E
		public void SetSunDirection(UIntPtr scenePointer, Vec3 direction)
		{
			ScriptingInterfaceOfIScene.call_SetSunDirectionDelegate(scenePointer, direction);
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x000145AC File Offset: 0x000127AC
		public void SetSunLight(UIntPtr scenePointer, Vec3 color, Vec3 direction)
		{
			ScriptingInterfaceOfIScene.call_SetSunLightDelegate(scenePointer, color, direction);
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x000145BB File Offset: 0x000127BB
		public void SetSunshaftMode(UIntPtr scenePointer, bool mode)
		{
			ScriptingInterfaceOfIScene.call_SetSunshaftModeDelegate(scenePointer, mode);
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x000145C9 File Offset: 0x000127C9
		public void SetSunShaftStrength(UIntPtr scenePointer, float strength)
		{
			ScriptingInterfaceOfIScene.call_SetSunShaftStrengthDelegate(scenePointer, strength);
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x000145D7 File Offset: 0x000127D7
		public void SetSunSize(UIntPtr scenePointer, float size)
		{
			ScriptingInterfaceOfIScene.call_SetSunSizeDelegate(scenePointer, size);
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x000145E5 File Offset: 0x000127E5
		public void SetTargetExposure(UIntPtr scenePointer, float targetExposure)
		{
			ScriptingInterfaceOfIScene.call_SetTargetExposureDelegate(scenePointer, targetExposure);
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x000145F3 File Offset: 0x000127F3
		public void SetTemperature(UIntPtr scenePointer, float temperature)
		{
			ScriptingInterfaceOfIScene.call_SetTemperatureDelegate(scenePointer, temperature);
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00014601 File Offset: 0x00012801
		public void SetTerrainDynamicParams(UIntPtr scenePointer, Vec3 dynamic_params)
		{
			ScriptingInterfaceOfIScene.call_SetTerrainDynamicParamsDelegate(scenePointer, dynamic_params);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0001460F File Offset: 0x0001280F
		public void SetTimeOfDay(UIntPtr scenePointer, float value)
		{
			ScriptingInterfaceOfIScene.call_SetTimeOfDayDelegate(scenePointer, value);
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0001461D File Offset: 0x0001281D
		public void SetTimeSpeed(UIntPtr scenePointer, float value)
		{
			ScriptingInterfaceOfIScene.call_SetTimeSpeedDelegate(scenePointer, value);
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0001462B File Offset: 0x0001282B
		public void SetUpgradeLevel(UIntPtr scenePointer, int level)
		{
			ScriptingInterfaceOfIScene.call_SetUpgradeLevelDelegate(scenePointer, level);
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0001463C File Offset: 0x0001283C
		public void SetUpgradeLevelVisibility(UIntPtr scenePointer, string concatLevels)
		{
			byte[] array = null;
			if (concatLevels != null)
			{
				int byteCount = ScriptingInterfaceOfIScene._utf8.GetByteCount(concatLevels);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIScene._utf8.GetBytes(concatLevels, 0, concatLevels.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIScene.call_SetUpgradeLevelVisibilityDelegate(scenePointer, array);
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00014697 File Offset: 0x00012897
		public void SetUpgradeLevelVisibilityWithMask(UIntPtr scenePointer, uint mask)
		{
			ScriptingInterfaceOfIScene.call_SetUpgradeLevelVisibilityWithMaskDelegate(scenePointer, mask);
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x000146A5 File Offset: 0x000128A5
		public void SetUseConstantTime(UIntPtr ptr, bool value)
		{
			ScriptingInterfaceOfIScene.call_SetUseConstantTimeDelegate(ptr, value);
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x000146B3 File Offset: 0x000128B3
		public void SetVignetteInnerRadius(UIntPtr scenePointer, float vignetteInnerRadius)
		{
			ScriptingInterfaceOfIScene.call_SetVignetteInnerRadiusDelegate(scenePointer, vignetteInnerRadius);
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x000146C1 File Offset: 0x000128C1
		public void SetVignetteOpacity(UIntPtr scenePointer, float vignetteOpacity)
		{
			ScriptingInterfaceOfIScene.call_SetVignetteOpacityDelegate(scenePointer, vignetteOpacity);
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x000146CF File Offset: 0x000128CF
		public void SetVignetteOuterRadius(UIntPtr scenePointer, float vignetteOuterRadius)
		{
			ScriptingInterfaceOfIScene.call_SetVignetteOuterRadiusDelegate(scenePointer, vignetteOuterRadius);
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x000146DD File Offset: 0x000128DD
		public void SetWinterTimeFactor(UIntPtr scenePointer, float winterTimeFactor)
		{
			ScriptingInterfaceOfIScene.call_SetWinterTimeFactorDelegate(scenePointer, winterTimeFactor);
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x000146EB File Offset: 0x000128EB
		public void StallLoadingRenderingsUntilFurtherNotice(UIntPtr scenePointer)
		{
			ScriptingInterfaceOfIScene.call_StallLoadingRenderingsUntilFurtherNoticeDelegate(scenePointer);
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x000146F8 File Offset: 0x000128F8
		public void SwapFaceConnectionsWithId(UIntPtr scenePointer, int hubFaceGroupID, int toBeSeparatedFaceGroupId, int toBeMergedFaceGroupId)
		{
			ScriptingInterfaceOfIScene.call_SwapFaceConnectionsWithIdDelegate(scenePointer, hubFaceGroupID, toBeSeparatedFaceGroupId, toBeMergedFaceGroupId);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0001470C File Offset: 0x0001290C
		public string TakePhotoModePicture(Scene scene, bool saveAmbientOcclusionPass, bool saveObjectIdPass, bool saveShadowPass)
		{
			UIntPtr scene2 = (scene != null) ? scene.Pointer : UIntPtr.Zero;
			if (ScriptingInterfaceOfIScene.call_TakePhotoModePictureDelegate(scene2, saveAmbientOcclusionPass, saveObjectIdPass, saveShadowPass) != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00014749 File Offset: 0x00012949
		public void Tick(UIntPtr scenePointer, float deltaTime)
		{
			ScriptingInterfaceOfIScene.call_TickDelegate(scenePointer, deltaTime);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00014757 File Offset: 0x00012957
		public void WorldPositionComputeNearestNavMesh(ref WorldPosition position)
		{
			ScriptingInterfaceOfIScene.call_WorldPositionComputeNearestNavMeshDelegate(ref position);
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00014764 File Offset: 0x00012964
		public void WorldPositionValidateZ(ref WorldPosition position, int minimumValidityState)
		{
			ScriptingInterfaceOfIScene.call_WorldPositionValidateZDelegate(ref position, minimumValidityState);
		}

		// Token: 0x040002E5 RID: 741
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x040002E6 RID: 742
		public static ScriptingInterfaceOfIScene.AddDecalInstanceDelegate call_AddDecalInstanceDelegate;

		// Token: 0x040002E7 RID: 743
		public static ScriptingInterfaceOfIScene.AddDirectionalLightDelegate call_AddDirectionalLightDelegate;

		// Token: 0x040002E8 RID: 744
		public static ScriptingInterfaceOfIScene.AddEntityWithMeshDelegate call_AddEntityWithMeshDelegate;

		// Token: 0x040002E9 RID: 745
		public static ScriptingInterfaceOfIScene.AddEntityWithMultiMeshDelegate call_AddEntityWithMultiMeshDelegate;

		// Token: 0x040002EA RID: 746
		public static ScriptingInterfaceOfIScene.AddItemEntityDelegate call_AddItemEntityDelegate;

		// Token: 0x040002EB RID: 747
		public static ScriptingInterfaceOfIScene.AddPathDelegate call_AddPathDelegate;

		// Token: 0x040002EC RID: 748
		public static ScriptingInterfaceOfIScene.AddPathPointDelegate call_AddPathPointDelegate;

		// Token: 0x040002ED RID: 749
		public static ScriptingInterfaceOfIScene.AddPointLightDelegate call_AddPointLightDelegate;

		// Token: 0x040002EE RID: 750
		public static ScriptingInterfaceOfIScene.AttachEntityDelegate call_AttachEntityDelegate;

		// Token: 0x040002EF RID: 751
		public static ScriptingInterfaceOfIScene.BoxCastDelegate call_BoxCastDelegate;

		// Token: 0x040002F0 RID: 752
		public static ScriptingInterfaceOfIScene.BoxCastOnlyForCameraDelegate call_BoxCastOnlyForCameraDelegate;

		// Token: 0x040002F1 RID: 753
		public static ScriptingInterfaceOfIScene.CalculateEffectiveLightingDelegate call_CalculateEffectiveLightingDelegate;

		// Token: 0x040002F2 RID: 754
		public static ScriptingInterfaceOfIScene.CheckPathEntitiesFrameChangedDelegate call_CheckPathEntitiesFrameChangedDelegate;

		// Token: 0x040002F3 RID: 755
		public static ScriptingInterfaceOfIScene.CheckPointCanSeePointDelegate call_CheckPointCanSeePointDelegate;

		// Token: 0x040002F4 RID: 756
		public static ScriptingInterfaceOfIScene.CheckResourcesDelegate call_CheckResourcesDelegate;

		// Token: 0x040002F5 RID: 757
		public static ScriptingInterfaceOfIScene.ClearAllDelegate call_ClearAllDelegate;

		// Token: 0x040002F6 RID: 758
		public static ScriptingInterfaceOfIScene.ClearDecalsDelegate call_ClearDecalsDelegate;

		// Token: 0x040002F7 RID: 759
		public static ScriptingInterfaceOfIScene.ContainsTerrainDelegate call_ContainsTerrainDelegate;

		// Token: 0x040002F8 RID: 760
		public static ScriptingInterfaceOfIScene.CreateBurstParticleDelegate call_CreateBurstParticleDelegate;

		// Token: 0x040002F9 RID: 761
		public static ScriptingInterfaceOfIScene.CreateNewSceneDelegate call_CreateNewSceneDelegate;

		// Token: 0x040002FA RID: 762
		public static ScriptingInterfaceOfIScene.CreatePathMeshDelegate call_CreatePathMeshDelegate;

		// Token: 0x040002FB RID: 763
		public static ScriptingInterfaceOfIScene.CreatePathMesh2Delegate call_CreatePathMesh2Delegate;

		// Token: 0x040002FC RID: 764
		public static ScriptingInterfaceOfIScene.DeletePathWithNameDelegate call_DeletePathWithNameDelegate;

		// Token: 0x040002FD RID: 765
		public static ScriptingInterfaceOfIScene.DisableStaticShadowsDelegate call_DisableStaticShadowsDelegate;

		// Token: 0x040002FE RID: 766
		public static ScriptingInterfaceOfIScene.DoesPathExistBetweenFacesDelegate call_DoesPathExistBetweenFacesDelegate;

		// Token: 0x040002FF RID: 767
		public static ScriptingInterfaceOfIScene.DoesPathExistBetweenPositionsDelegate call_DoesPathExistBetweenPositionsDelegate;

		// Token: 0x04000300 RID: 768
		public static ScriptingInterfaceOfIScene.EnsurePostfxSystemDelegate call_EnsurePostfxSystemDelegate;

		// Token: 0x04000301 RID: 769
		public static ScriptingInterfaceOfIScene.FillEntityWithHardBorderPhysicsBarrierDelegate call_FillEntityWithHardBorderPhysicsBarrierDelegate;

		// Token: 0x04000302 RID: 770
		public static ScriptingInterfaceOfIScene.FillTerrainHeightDataDelegate call_FillTerrainHeightDataDelegate;

		// Token: 0x04000303 RID: 771
		public static ScriptingInterfaceOfIScene.FillTerrainPhysicsMaterialIndexDataDelegate call_FillTerrainPhysicsMaterialIndexDataDelegate;

		// Token: 0x04000304 RID: 772
		public static ScriptingInterfaceOfIScene.FinishSceneSoundsDelegate call_FinishSceneSoundsDelegate;

		// Token: 0x04000305 RID: 773
		public static ScriptingInterfaceOfIScene.ForceLoadResourcesDelegate call_ForceLoadResourcesDelegate;

		// Token: 0x04000306 RID: 774
		public static ScriptingInterfaceOfIScene.GenerateContactsWithCapsuleDelegate call_GenerateContactsWithCapsuleDelegate;

		// Token: 0x04000307 RID: 775
		public static ScriptingInterfaceOfIScene.GetAllColorGradeNamesDelegate call_GetAllColorGradeNamesDelegate;

		// Token: 0x04000308 RID: 776
		public static ScriptingInterfaceOfIScene.GetAllEntitiesWithScriptComponentDelegate call_GetAllEntitiesWithScriptComponentDelegate;

		// Token: 0x04000309 RID: 777
		public static ScriptingInterfaceOfIScene.GetAllFilterNamesDelegate call_GetAllFilterNamesDelegate;

		// Token: 0x0400030A RID: 778
		public static ScriptingInterfaceOfIScene.GetBoundingBoxDelegate call_GetBoundingBoxDelegate;

		// Token: 0x0400030B RID: 779
		public static ScriptingInterfaceOfIScene.GetCampaignEntityWithNameDelegate call_GetCampaignEntityWithNameDelegate;

		// Token: 0x0400030C RID: 780
		public static ScriptingInterfaceOfIScene.GetEntitiesDelegate call_GetEntitiesDelegate;

		// Token: 0x0400030D RID: 781
		public static ScriptingInterfaceOfIScene.GetEntityCountDelegate call_GetEntityCountDelegate;

		// Token: 0x0400030E RID: 782
		public static ScriptingInterfaceOfIScene.GetEntityWithGuidDelegate call_GetEntityWithGuidDelegate;

		// Token: 0x0400030F RID: 783
		public static ScriptingInterfaceOfIScene.GetFirstEntityWithNameDelegate call_GetFirstEntityWithNameDelegate;

		// Token: 0x04000310 RID: 784
		public static ScriptingInterfaceOfIScene.GetFirstEntityWithScriptComponentDelegate call_GetFirstEntityWithScriptComponentDelegate;

		// Token: 0x04000311 RID: 785
		public static ScriptingInterfaceOfIScene.GetFloraInstanceCountDelegate call_GetFloraInstanceCountDelegate;

		// Token: 0x04000312 RID: 786
		public static ScriptingInterfaceOfIScene.GetFloraRendererTextureUsageDelegate call_GetFloraRendererTextureUsageDelegate;

		// Token: 0x04000313 RID: 787
		public static ScriptingInterfaceOfIScene.GetFogDelegate call_GetFogDelegate;

		// Token: 0x04000314 RID: 788
		public static ScriptingInterfaceOfIScene.GetGroundHeightAndNormalAtPositionDelegate call_GetGroundHeightAndNormalAtPositionDelegate;

		// Token: 0x04000315 RID: 789
		public static ScriptingInterfaceOfIScene.GetGroundHeightAtPositionDelegate call_GetGroundHeightAtPositionDelegate;

		// Token: 0x04000316 RID: 790
		public static ScriptingInterfaceOfIScene.GetHardBoundaryVertexDelegate call_GetHardBoundaryVertexDelegate;

		// Token: 0x04000317 RID: 791
		public static ScriptingInterfaceOfIScene.GetHardBoundaryVertexCountDelegate call_GetHardBoundaryVertexCountDelegate;

		// Token: 0x04000318 RID: 792
		public static ScriptingInterfaceOfIScene.GetHeightAtPointDelegate call_GetHeightAtPointDelegate;

		// Token: 0x04000319 RID: 793
		public static ScriptingInterfaceOfIScene.GetIdOfNavMeshFaceDelegate call_GetIdOfNavMeshFaceDelegate;

		// Token: 0x0400031A RID: 794
		public static ScriptingInterfaceOfIScene.GetLastFinalRenderCameraFrameDelegate call_GetLastFinalRenderCameraFrameDelegate;

		// Token: 0x0400031B RID: 795
		public static ScriptingInterfaceOfIScene.GetLastFinalRenderCameraPositionDelegate call_GetLastFinalRenderCameraPositionDelegate;

		// Token: 0x0400031C RID: 796
		public static ScriptingInterfaceOfIScene.GetLastPointOnNavigationMeshFromPositionToDestinationDelegate call_GetLastPointOnNavigationMeshFromPositionToDestinationDelegate;

		// Token: 0x0400031D RID: 797
		public static ScriptingInterfaceOfIScene.GetLastPointOnNavigationMeshFromWorldPositionToDestinationDelegate call_GetLastPointOnNavigationMeshFromWorldPositionToDestinationDelegate;

		// Token: 0x0400031E RID: 798
		public static ScriptingInterfaceOfIScene.GetLoadingStateNameDelegate call_GetLoadingStateNameDelegate;

		// Token: 0x0400031F RID: 799
		public static ScriptingInterfaceOfIScene.GetModulePathDelegate call_GetModulePathDelegate;

		// Token: 0x04000320 RID: 800
		public static ScriptingInterfaceOfIScene.GetNameDelegate call_GetNameDelegate;

		// Token: 0x04000321 RID: 801
		public static ScriptingInterfaceOfIScene.GetNavigationMeshFaceForPositionDelegate call_GetNavigationMeshFaceForPositionDelegate;

		// Token: 0x04000322 RID: 802
		public static ScriptingInterfaceOfIScene.GetNavMeshFaceCenterPositionDelegate call_GetNavMeshFaceCenterPositionDelegate;

		// Token: 0x04000323 RID: 803
		public static ScriptingInterfaceOfIScene.GetNavMeshFaceCountDelegate call_GetNavMeshFaceCountDelegate;

		// Token: 0x04000324 RID: 804
		public static ScriptingInterfaceOfIScene.GetNavMeshFaceFirstVertexZDelegate call_GetNavMeshFaceFirstVertexZDelegate;

		// Token: 0x04000325 RID: 805
		public static ScriptingInterfaceOfIScene.GetNavMeshFaceIndexDelegate call_GetNavMeshFaceIndexDelegate;

		// Token: 0x04000326 RID: 806
		public static ScriptingInterfaceOfIScene.GetNavMeshFaceIndex3Delegate call_GetNavMeshFaceIndex3Delegate;

		// Token: 0x04000327 RID: 807
		public static ScriptingInterfaceOfIScene.GetNodeDataCountDelegate call_GetNodeDataCountDelegate;

		// Token: 0x04000328 RID: 808
		public static ScriptingInterfaceOfIScene.GetNormalAtDelegate call_GetNormalAtDelegate;

		// Token: 0x04000329 RID: 809
		public static ScriptingInterfaceOfIScene.GetNorthAngleDelegate call_GetNorthAngleDelegate;

		// Token: 0x0400032A RID: 810
		public static ScriptingInterfaceOfIScene.GetNumberOfPathsWithNamePrefixDelegate call_GetNumberOfPathsWithNamePrefixDelegate;

		// Token: 0x0400032B RID: 811
		public static ScriptingInterfaceOfIScene.GetPathBetweenAIFaceIndicesDelegate call_GetPathBetweenAIFaceIndicesDelegate;

		// Token: 0x0400032C RID: 812
		public static ScriptingInterfaceOfIScene.GetPathBetweenAIFacePointersDelegate call_GetPathBetweenAIFacePointersDelegate;

		// Token: 0x0400032D RID: 813
		public static ScriptingInterfaceOfIScene.GetPathDistanceBetweenAIFacesDelegate call_GetPathDistanceBetweenAIFacesDelegate;

		// Token: 0x0400032E RID: 814
		public static ScriptingInterfaceOfIScene.GetPathDistanceBetweenPositionsDelegate call_GetPathDistanceBetweenPositionsDelegate;

		// Token: 0x0400032F RID: 815
		public static ScriptingInterfaceOfIScene.GetPathsWithNamePrefixDelegate call_GetPathsWithNamePrefixDelegate;

		// Token: 0x04000330 RID: 816
		public static ScriptingInterfaceOfIScene.GetPathWithNameDelegate call_GetPathWithNameDelegate;

		// Token: 0x04000331 RID: 817
		public static ScriptingInterfaceOfIScene.GetPhotoModeFocusDelegate call_GetPhotoModeFocusDelegate;

		// Token: 0x04000332 RID: 818
		public static ScriptingInterfaceOfIScene.GetPhotoModeFovDelegate call_GetPhotoModeFovDelegate;

		// Token: 0x04000333 RID: 819
		public static ScriptingInterfaceOfIScene.GetPhotoModeOnDelegate call_GetPhotoModeOnDelegate;

		// Token: 0x04000334 RID: 820
		public static ScriptingInterfaceOfIScene.GetPhotoModeOrbitDelegate call_GetPhotoModeOrbitDelegate;

		// Token: 0x04000335 RID: 821
		public static ScriptingInterfaceOfIScene.GetPhotoModeRollDelegate call_GetPhotoModeRollDelegate;

		// Token: 0x04000336 RID: 822
		public static ScriptingInterfaceOfIScene.GetPhysicsMinMaxDelegate call_GetPhysicsMinMaxDelegate;

		// Token: 0x04000337 RID: 823
		public static ScriptingInterfaceOfIScene.GetRainDensityDelegate call_GetRainDensityDelegate;

		// Token: 0x04000338 RID: 824
		public static ScriptingInterfaceOfIScene.GetRootEntitiesDelegate call_GetRootEntitiesDelegate;

		// Token: 0x04000339 RID: 825
		public static ScriptingInterfaceOfIScene.GetRootEntityCountDelegate call_GetRootEntityCountDelegate;

		// Token: 0x0400033A RID: 826
		public static ScriptingInterfaceOfIScene.GetSceneColorGradeIndexDelegate call_GetSceneColorGradeIndexDelegate;

		// Token: 0x0400033B RID: 827
		public static ScriptingInterfaceOfIScene.GetSceneFilterIndexDelegate call_GetSceneFilterIndexDelegate;

		// Token: 0x0400033C RID: 828
		public static ScriptingInterfaceOfIScene.GetScriptedEntityDelegate call_GetScriptedEntityDelegate;

		// Token: 0x0400033D RID: 829
		public static ScriptingInterfaceOfIScene.GetScriptedEntityCountDelegate call_GetScriptedEntityCountDelegate;

		// Token: 0x0400033E RID: 830
		public static ScriptingInterfaceOfIScene.GetSkyboxMeshDelegate call_GetSkyboxMeshDelegate;

		// Token: 0x0400033F RID: 831
		public static ScriptingInterfaceOfIScene.GetSnowAmountDataDelegate call_GetSnowAmountDataDelegate;

		// Token: 0x04000340 RID: 832
		public static ScriptingInterfaceOfIScene.GetSnowDensityDelegate call_GetSnowDensityDelegate;

		// Token: 0x04000341 RID: 833
		public static ScriptingInterfaceOfIScene.GetSoftBoundaryVertexDelegate call_GetSoftBoundaryVertexDelegate;

		// Token: 0x04000342 RID: 834
		public static ScriptingInterfaceOfIScene.GetSoftBoundaryVertexCountDelegate call_GetSoftBoundaryVertexCountDelegate;

		// Token: 0x04000343 RID: 835
		public static ScriptingInterfaceOfIScene.GetSunDirectionDelegate call_GetSunDirectionDelegate;

		// Token: 0x04000344 RID: 836
		public static ScriptingInterfaceOfIScene.GetTerrainDataDelegate call_GetTerrainDataDelegate;

		// Token: 0x04000345 RID: 837
		public static ScriptingInterfaceOfIScene.GetTerrainHeightDelegate call_GetTerrainHeightDelegate;

		// Token: 0x04000346 RID: 838
		public static ScriptingInterfaceOfIScene.GetTerrainHeightAndNormalDelegate call_GetTerrainHeightAndNormalDelegate;

		// Token: 0x04000347 RID: 839
		public static ScriptingInterfaceOfIScene.GetTerrainMemoryUsageDelegate call_GetTerrainMemoryUsageDelegate;

		// Token: 0x04000348 RID: 840
		public static ScriptingInterfaceOfIScene.GetTerrainMinMaxHeightDelegate call_GetTerrainMinMaxHeightDelegate;

		// Token: 0x04000349 RID: 841
		public static ScriptingInterfaceOfIScene.GetTerrainNodeDataDelegate call_GetTerrainNodeDataDelegate;

		// Token: 0x0400034A RID: 842
		public static ScriptingInterfaceOfIScene.GetTerrainPhysicsMaterialIndexAtLayerDelegate call_GetTerrainPhysicsMaterialIndexAtLayerDelegate;

		// Token: 0x0400034B RID: 843
		public static ScriptingInterfaceOfIScene.GetTimeOfDayDelegate call_GetTimeOfDayDelegate;

		// Token: 0x0400034C RID: 844
		public static ScriptingInterfaceOfIScene.GetTimeSpeedDelegate call_GetTimeSpeedDelegate;

		// Token: 0x0400034D RID: 845
		public static ScriptingInterfaceOfIScene.GetUpgradeLevelCountDelegate call_GetUpgradeLevelCountDelegate;

		// Token: 0x0400034E RID: 846
		public static ScriptingInterfaceOfIScene.GetUpgradeLevelMaskDelegate call_GetUpgradeLevelMaskDelegate;

		// Token: 0x0400034F RID: 847
		public static ScriptingInterfaceOfIScene.GetUpgradeLevelMaskOfLevelNameDelegate call_GetUpgradeLevelMaskOfLevelNameDelegate;

		// Token: 0x04000350 RID: 848
		public static ScriptingInterfaceOfIScene.GetUpgradeLevelNameOfIndexDelegate call_GetUpgradeLevelNameOfIndexDelegate;

		// Token: 0x04000351 RID: 849
		public static ScriptingInterfaceOfIScene.GetWaterLevelDelegate call_GetWaterLevelDelegate;

		// Token: 0x04000352 RID: 850
		public static ScriptingInterfaceOfIScene.GetWaterLevelAtPositionDelegate call_GetWaterLevelAtPositionDelegate;

		// Token: 0x04000353 RID: 851
		public static ScriptingInterfaceOfIScene.GetWinterTimeFactorDelegate call_GetWinterTimeFactorDelegate;

		// Token: 0x04000354 RID: 852
		public static ScriptingInterfaceOfIScene.HasTerrainHeightmapDelegate call_HasTerrainHeightmapDelegate;

		// Token: 0x04000355 RID: 853
		public static ScriptingInterfaceOfIScene.InvalidateTerrainPhysicsMaterialsDelegate call_InvalidateTerrainPhysicsMaterialsDelegate;

		// Token: 0x04000356 RID: 854
		public static ScriptingInterfaceOfIScene.IsAnyFaceWithIdDelegate call_IsAnyFaceWithIdDelegate;

		// Token: 0x04000357 RID: 855
		public static ScriptingInterfaceOfIScene.IsAtmosphereIndoorDelegate call_IsAtmosphereIndoorDelegate;

		// Token: 0x04000358 RID: 856
		public static ScriptingInterfaceOfIScene.IsDefaultEditorSceneDelegate call_IsDefaultEditorSceneDelegate;

		// Token: 0x04000359 RID: 857
		public static ScriptingInterfaceOfIScene.IsEditorSceneDelegate call_IsEditorSceneDelegate;

		// Token: 0x0400035A RID: 858
		public static ScriptingInterfaceOfIScene.IsLineToPointClearDelegate call_IsLineToPointClearDelegate;

		// Token: 0x0400035B RID: 859
		public static ScriptingInterfaceOfIScene.IsLineToPointClear2Delegate call_IsLineToPointClear2Delegate;

		// Token: 0x0400035C RID: 860
		public static ScriptingInterfaceOfIScene.IsLoadingFinishedDelegate call_IsLoadingFinishedDelegate;

		// Token: 0x0400035D RID: 861
		public static ScriptingInterfaceOfIScene.IsMultiplayerSceneDelegate call_IsMultiplayerSceneDelegate;

		// Token: 0x0400035E RID: 862
		public static ScriptingInterfaceOfIScene.LoadNavMeshPrefabDelegate call_LoadNavMeshPrefabDelegate;

		// Token: 0x0400035F RID: 863
		public static ScriptingInterfaceOfIScene.MarkFacesWithIdAsLadderDelegate call_MarkFacesWithIdAsLadderDelegate;

		// Token: 0x04000360 RID: 864
		public static ScriptingInterfaceOfIScene.MergeFacesWithIdDelegate call_MergeFacesWithIdDelegate;

		// Token: 0x04000361 RID: 865
		public static ScriptingInterfaceOfIScene.OptimizeSceneDelegate call_OptimizeSceneDelegate;

		// Token: 0x04000362 RID: 866
		public static ScriptingInterfaceOfIScene.PauseSceneSoundsDelegate call_PauseSceneSoundsDelegate;

		// Token: 0x04000363 RID: 867
		public static ScriptingInterfaceOfIScene.PreloadForRenderingDelegate call_PreloadForRenderingDelegate;

		// Token: 0x04000364 RID: 868
		public static ScriptingInterfaceOfIScene.RayCastForClosestEntityOrTerrainDelegate call_RayCastForClosestEntityOrTerrainDelegate;

		// Token: 0x04000365 RID: 869
		public static ScriptingInterfaceOfIScene.ReadDelegate call_ReadDelegate;

		// Token: 0x04000366 RID: 870
		public static ScriptingInterfaceOfIScene.ReadAndCalculateInitialCameraDelegate call_ReadAndCalculateInitialCameraDelegate;

		// Token: 0x04000367 RID: 871
		public static ScriptingInterfaceOfIScene.RemoveEntityDelegate call_RemoveEntityDelegate;

		// Token: 0x04000368 RID: 872
		public static ScriptingInterfaceOfIScene.ResumeLoadingRenderingsDelegate call_ResumeLoadingRenderingsDelegate;

		// Token: 0x04000369 RID: 873
		public static ScriptingInterfaceOfIScene.ResumeSceneSoundsDelegate call_ResumeSceneSoundsDelegate;

		// Token: 0x0400036A RID: 874
		public static ScriptingInterfaceOfIScene.SelectEntitiesCollidedWithDelegate call_SelectEntitiesCollidedWithDelegate;

		// Token: 0x0400036B RID: 875
		public static ScriptingInterfaceOfIScene.SelectEntitiesInBoxWithScriptComponentDelegate call_SelectEntitiesInBoxWithScriptComponentDelegate;

		// Token: 0x0400036C RID: 876
		public static ScriptingInterfaceOfIScene.SeparateFacesWithIdDelegate call_SeparateFacesWithIdDelegate;

		// Token: 0x0400036D RID: 877
		public static ScriptingInterfaceOfIScene.SetAberrationOffsetDelegate call_SetAberrationOffsetDelegate;

		// Token: 0x0400036E RID: 878
		public static ScriptingInterfaceOfIScene.SetAberrationSizeDelegate call_SetAberrationSizeDelegate;

		// Token: 0x0400036F RID: 879
		public static ScriptingInterfaceOfIScene.SetAberrationSmoothDelegate call_SetAberrationSmoothDelegate;

		// Token: 0x04000370 RID: 880
		public static ScriptingInterfaceOfIScene.SetAbilityOfFacesWithIdDelegate call_SetAbilityOfFacesWithIdDelegate;

		// Token: 0x04000371 RID: 881
		public static ScriptingInterfaceOfIScene.SetActiveVisibilityLevelsDelegate call_SetActiveVisibilityLevelsDelegate;

		// Token: 0x04000372 RID: 882
		public static ScriptingInterfaceOfIScene.SetAntialiasingModeDelegate call_SetAntialiasingModeDelegate;

		// Token: 0x04000373 RID: 883
		public static ScriptingInterfaceOfIScene.SetAtmosphereWithNameDelegate call_SetAtmosphereWithNameDelegate;

		// Token: 0x04000374 RID: 884
		public static ScriptingInterfaceOfIScene.SetBloomDelegate call_SetBloomDelegate;

		// Token: 0x04000375 RID: 885
		public static ScriptingInterfaceOfIScene.SetBloomAmountDelegate call_SetBloomAmountDelegate;

		// Token: 0x04000376 RID: 886
		public static ScriptingInterfaceOfIScene.SetBloomStrengthDelegate call_SetBloomStrengthDelegate;

		// Token: 0x04000377 RID: 887
		public static ScriptingInterfaceOfIScene.SetBrightpassTresholdDelegate call_SetBrightpassTresholdDelegate;

		// Token: 0x04000378 RID: 888
		public static ScriptingInterfaceOfIScene.SetClothSimulationStateDelegate call_SetClothSimulationStateDelegate;

		// Token: 0x04000379 RID: 889
		public static ScriptingInterfaceOfIScene.SetColorGradeBlendDelegate call_SetColorGradeBlendDelegate;

		// Token: 0x0400037A RID: 890
		public static ScriptingInterfaceOfIScene.SetDLSSModeDelegate call_SetDLSSModeDelegate;

		// Token: 0x0400037B RID: 891
		public static ScriptingInterfaceOfIScene.SetDofFocusDelegate call_SetDofFocusDelegate;

		// Token: 0x0400037C RID: 892
		public static ScriptingInterfaceOfIScene.SetDofModeDelegate call_SetDofModeDelegate;

		// Token: 0x0400037D RID: 893
		public static ScriptingInterfaceOfIScene.SetDofParamsDelegate call_SetDofParamsDelegate;

		// Token: 0x0400037E RID: 894
		public static ScriptingInterfaceOfIScene.SetDoNotWaitForLoadingStatesToRenderDelegate call_SetDoNotWaitForLoadingStatesToRenderDelegate;

		// Token: 0x0400037F RID: 895
		public static ScriptingInterfaceOfIScene.SetDrynessFactorDelegate call_SetDrynessFactorDelegate;

		// Token: 0x04000380 RID: 896
		public static ScriptingInterfaceOfIScene.SetDynamicShadowmapCascadesRadiusMultiplierDelegate call_SetDynamicShadowmapCascadesRadiusMultiplierDelegate;

		// Token: 0x04000381 RID: 897
		public static ScriptingInterfaceOfIScene.SetEnvironmentMultiplierDelegate call_SetEnvironmentMultiplierDelegate;

		// Token: 0x04000382 RID: 898
		public static ScriptingInterfaceOfIScene.SetExternalInjectionTextureDelegate call_SetExternalInjectionTextureDelegate;

		// Token: 0x04000383 RID: 899
		public static ScriptingInterfaceOfIScene.SetFogDelegate call_SetFogDelegate;

		// Token: 0x04000384 RID: 900
		public static ScriptingInterfaceOfIScene.SetFogAdvancedDelegate call_SetFogAdvancedDelegate;

		// Token: 0x04000385 RID: 901
		public static ScriptingInterfaceOfIScene.SetFogAmbientColorDelegate call_SetFogAmbientColorDelegate;

		// Token: 0x04000386 RID: 902
		public static ScriptingInterfaceOfIScene.SetForcedSnowDelegate call_SetForcedSnowDelegate;

		// Token: 0x04000387 RID: 903
		public static ScriptingInterfaceOfIScene.SetGrainAmountDelegate call_SetGrainAmountDelegate;

		// Token: 0x04000388 RID: 904
		public static ScriptingInterfaceOfIScene.SetHexagonVignetteAlphaDelegate call_SetHexagonVignetteAlphaDelegate;

		// Token: 0x04000389 RID: 905
		public static ScriptingInterfaceOfIScene.SetHexagonVignetteColorDelegate call_SetHexagonVignetteColorDelegate;

		// Token: 0x0400038A RID: 906
		public static ScriptingInterfaceOfIScene.SetHumidityDelegate call_SetHumidityDelegate;

		// Token: 0x0400038B RID: 907
		public static ScriptingInterfaceOfIScene.SetLandscapeRainMaskDataDelegate call_SetLandscapeRainMaskDataDelegate;

		// Token: 0x0400038C RID: 908
		public static ScriptingInterfaceOfIScene.SetLensDistortionDelegate call_SetLensDistortionDelegate;

		// Token: 0x0400038D RID: 909
		public static ScriptingInterfaceOfIScene.SetLensFlareAberrationOffsetDelegate call_SetLensFlareAberrationOffsetDelegate;

		// Token: 0x0400038E RID: 910
		public static ScriptingInterfaceOfIScene.SetLensFlareAmountDelegate call_SetLensFlareAmountDelegate;

		// Token: 0x0400038F RID: 911
		public static ScriptingInterfaceOfIScene.SetLensFlareBlurSigmaDelegate call_SetLensFlareBlurSigmaDelegate;

		// Token: 0x04000390 RID: 912
		public static ScriptingInterfaceOfIScene.SetLensFlareBlurSizeDelegate call_SetLensFlareBlurSizeDelegate;

		// Token: 0x04000391 RID: 913
		public static ScriptingInterfaceOfIScene.SetLensFlareDiffractionWeightDelegate call_SetLensFlareDiffractionWeightDelegate;

		// Token: 0x04000392 RID: 914
		public static ScriptingInterfaceOfIScene.SetLensFlareDirtWeightDelegate call_SetLensFlareDirtWeightDelegate;

		// Token: 0x04000393 RID: 915
		public static ScriptingInterfaceOfIScene.SetLensFlareGhostSamplesDelegate call_SetLensFlareGhostSamplesDelegate;

		// Token: 0x04000394 RID: 916
		public static ScriptingInterfaceOfIScene.SetLensFlareGhostWeightDelegate call_SetLensFlareGhostWeightDelegate;

		// Token: 0x04000395 RID: 917
		public static ScriptingInterfaceOfIScene.SetLensFlareHaloWeightDelegate call_SetLensFlareHaloWeightDelegate;

		// Token: 0x04000396 RID: 918
		public static ScriptingInterfaceOfIScene.SetLensFlareHaloWidthDelegate call_SetLensFlareHaloWidthDelegate;

		// Token: 0x04000397 RID: 919
		public static ScriptingInterfaceOfIScene.SetLensFlareStrengthDelegate call_SetLensFlareStrengthDelegate;

		// Token: 0x04000398 RID: 920
		public static ScriptingInterfaceOfIScene.SetLensFlareThresholdDelegate call_SetLensFlareThresholdDelegate;

		// Token: 0x04000399 RID: 921
		public static ScriptingInterfaceOfIScene.SetLightDiffuseColorDelegate call_SetLightDiffuseColorDelegate;

		// Token: 0x0400039A RID: 922
		public static ScriptingInterfaceOfIScene.SetLightDirectionDelegate call_SetLightDirectionDelegate;

		// Token: 0x0400039B RID: 923
		public static ScriptingInterfaceOfIScene.SetLightPositionDelegate call_SetLightPositionDelegate;

		// Token: 0x0400039C RID: 924
		public static ScriptingInterfaceOfIScene.SetMaxExposureDelegate call_SetMaxExposureDelegate;

		// Token: 0x0400039D RID: 925
		public static ScriptingInterfaceOfIScene.SetMiddleGrayDelegate call_SetMiddleGrayDelegate;

		// Token: 0x0400039E RID: 926
		public static ScriptingInterfaceOfIScene.SetMieScatterFocusDelegate call_SetMieScatterFocusDelegate;

		// Token: 0x0400039F RID: 927
		public static ScriptingInterfaceOfIScene.SetMieScatterStrengthDelegate call_SetMieScatterStrengthDelegate;

		// Token: 0x040003A0 RID: 928
		public static ScriptingInterfaceOfIScene.SetMinExposureDelegate call_SetMinExposureDelegate;

		// Token: 0x040003A1 RID: 929
		public static ScriptingInterfaceOfIScene.SetMotionBlurModeDelegate call_SetMotionBlurModeDelegate;

		// Token: 0x040003A2 RID: 930
		public static ScriptingInterfaceOfIScene.SetNameDelegate call_SetNameDelegate;

		// Token: 0x040003A3 RID: 931
		public static ScriptingInterfaceOfIScene.SetOcclusionModeDelegate call_SetOcclusionModeDelegate;

		// Token: 0x040003A4 RID: 932
		public static ScriptingInterfaceOfIScene.SetOwnerThreadDelegate call_SetOwnerThreadDelegate;

		// Token: 0x040003A5 RID: 933
		public static ScriptingInterfaceOfIScene.SetPhotoModeFocusDelegate call_SetPhotoModeFocusDelegate;

		// Token: 0x040003A6 RID: 934
		public static ScriptingInterfaceOfIScene.SetPhotoModeFovDelegate call_SetPhotoModeFovDelegate;

		// Token: 0x040003A7 RID: 935
		public static ScriptingInterfaceOfIScene.SetPhotoModeOnDelegate call_SetPhotoModeOnDelegate;

		// Token: 0x040003A8 RID: 936
		public static ScriptingInterfaceOfIScene.SetPhotoModeOrbitDelegate call_SetPhotoModeOrbitDelegate;

		// Token: 0x040003A9 RID: 937
		public static ScriptingInterfaceOfIScene.SetPhotoModeRollDelegate call_SetPhotoModeRollDelegate;

		// Token: 0x040003AA RID: 938
		public static ScriptingInterfaceOfIScene.SetPhotoModeVignetteDelegate call_SetPhotoModeVignetteDelegate;

		// Token: 0x040003AB RID: 939
		public static ScriptingInterfaceOfIScene.SetPlaySoundEventsAfterReadyToRenderDelegate call_SetPlaySoundEventsAfterReadyToRenderDelegate;

		// Token: 0x040003AC RID: 940
		public static ScriptingInterfaceOfIScene.SetRainDensityDelegate call_SetRainDensityDelegate;

		// Token: 0x040003AD RID: 941
		public static ScriptingInterfaceOfIScene.SetSceneColorGradeDelegate call_SetSceneColorGradeDelegate;

		// Token: 0x040003AE RID: 942
		public static ScriptingInterfaceOfIScene.SetSceneColorGradeIndexDelegate call_SetSceneColorGradeIndexDelegate;

		// Token: 0x040003AF RID: 943
		public static ScriptingInterfaceOfIScene.SetSceneFilterIndexDelegate call_SetSceneFilterIndexDelegate;

		// Token: 0x040003B0 RID: 944
		public static ScriptingInterfaceOfIScene.SetShadowDelegate call_SetShadowDelegate;

		// Token: 0x040003B1 RID: 945
		public static ScriptingInterfaceOfIScene.SetSkyBrightnessDelegate call_SetSkyBrightnessDelegate;

		// Token: 0x040003B2 RID: 946
		public static ScriptingInterfaceOfIScene.SetSkyRotationDelegate call_SetSkyRotationDelegate;

		// Token: 0x040003B3 RID: 947
		public static ScriptingInterfaceOfIScene.SetSnowDensityDelegate call_SetSnowDensityDelegate;

		// Token: 0x040003B4 RID: 948
		public static ScriptingInterfaceOfIScene.SetStreakAmountDelegate call_SetStreakAmountDelegate;

		// Token: 0x040003B5 RID: 949
		public static ScriptingInterfaceOfIScene.SetStreakIntensityDelegate call_SetStreakIntensityDelegate;

		// Token: 0x040003B6 RID: 950
		public static ScriptingInterfaceOfIScene.SetStreakStrengthDelegate call_SetStreakStrengthDelegate;

		// Token: 0x040003B7 RID: 951
		public static ScriptingInterfaceOfIScene.SetStreakStretchDelegate call_SetStreakStretchDelegate;

		// Token: 0x040003B8 RID: 952
		public static ScriptingInterfaceOfIScene.SetStreakThresholdDelegate call_SetStreakThresholdDelegate;

		// Token: 0x040003B9 RID: 953
		public static ScriptingInterfaceOfIScene.SetStreakTintDelegate call_SetStreakTintDelegate;

		// Token: 0x040003BA RID: 954
		public static ScriptingInterfaceOfIScene.SetSunDelegate call_SetSunDelegate;

		// Token: 0x040003BB RID: 955
		public static ScriptingInterfaceOfIScene.SetSunAngleAltitudeDelegate call_SetSunAngleAltitudeDelegate;

		// Token: 0x040003BC RID: 956
		public static ScriptingInterfaceOfIScene.SetSunDirectionDelegate call_SetSunDirectionDelegate;

		// Token: 0x040003BD RID: 957
		public static ScriptingInterfaceOfIScene.SetSunLightDelegate call_SetSunLightDelegate;

		// Token: 0x040003BE RID: 958
		public static ScriptingInterfaceOfIScene.SetSunshaftModeDelegate call_SetSunshaftModeDelegate;

		// Token: 0x040003BF RID: 959
		public static ScriptingInterfaceOfIScene.SetSunShaftStrengthDelegate call_SetSunShaftStrengthDelegate;

		// Token: 0x040003C0 RID: 960
		public static ScriptingInterfaceOfIScene.SetSunSizeDelegate call_SetSunSizeDelegate;

		// Token: 0x040003C1 RID: 961
		public static ScriptingInterfaceOfIScene.SetTargetExposureDelegate call_SetTargetExposureDelegate;

		// Token: 0x040003C2 RID: 962
		public static ScriptingInterfaceOfIScene.SetTemperatureDelegate call_SetTemperatureDelegate;

		// Token: 0x040003C3 RID: 963
		public static ScriptingInterfaceOfIScene.SetTerrainDynamicParamsDelegate call_SetTerrainDynamicParamsDelegate;

		// Token: 0x040003C4 RID: 964
		public static ScriptingInterfaceOfIScene.SetTimeOfDayDelegate call_SetTimeOfDayDelegate;

		// Token: 0x040003C5 RID: 965
		public static ScriptingInterfaceOfIScene.SetTimeSpeedDelegate call_SetTimeSpeedDelegate;

		// Token: 0x040003C6 RID: 966
		public static ScriptingInterfaceOfIScene.SetUpgradeLevelDelegate call_SetUpgradeLevelDelegate;

		// Token: 0x040003C7 RID: 967
		public static ScriptingInterfaceOfIScene.SetUpgradeLevelVisibilityDelegate call_SetUpgradeLevelVisibilityDelegate;

		// Token: 0x040003C8 RID: 968
		public static ScriptingInterfaceOfIScene.SetUpgradeLevelVisibilityWithMaskDelegate call_SetUpgradeLevelVisibilityWithMaskDelegate;

		// Token: 0x040003C9 RID: 969
		public static ScriptingInterfaceOfIScene.SetUseConstantTimeDelegate call_SetUseConstantTimeDelegate;

		// Token: 0x040003CA RID: 970
		public static ScriptingInterfaceOfIScene.SetVignetteInnerRadiusDelegate call_SetVignetteInnerRadiusDelegate;

		// Token: 0x040003CB RID: 971
		public static ScriptingInterfaceOfIScene.SetVignetteOpacityDelegate call_SetVignetteOpacityDelegate;

		// Token: 0x040003CC RID: 972
		public static ScriptingInterfaceOfIScene.SetVignetteOuterRadiusDelegate call_SetVignetteOuterRadiusDelegate;

		// Token: 0x040003CD RID: 973
		public static ScriptingInterfaceOfIScene.SetWinterTimeFactorDelegate call_SetWinterTimeFactorDelegate;

		// Token: 0x040003CE RID: 974
		public static ScriptingInterfaceOfIScene.StallLoadingRenderingsUntilFurtherNoticeDelegate call_StallLoadingRenderingsUntilFurtherNoticeDelegate;

		// Token: 0x040003CF RID: 975
		public static ScriptingInterfaceOfIScene.SwapFaceConnectionsWithIdDelegate call_SwapFaceConnectionsWithIdDelegate;

		// Token: 0x040003D0 RID: 976
		public static ScriptingInterfaceOfIScene.TakePhotoModePictureDelegate call_TakePhotoModePictureDelegate;

		// Token: 0x040003D1 RID: 977
		public static ScriptingInterfaceOfIScene.TickDelegate call_TickDelegate;

		// Token: 0x040003D2 RID: 978
		public static ScriptingInterfaceOfIScene.WorldPositionComputeNearestNavMeshDelegate call_WorldPositionComputeNearestNavMeshDelegate;

		// Token: 0x040003D3 RID: 979
		public static ScriptingInterfaceOfIScene.WorldPositionValidateZDelegate call_WorldPositionValidateZDelegate;

		// Token: 0x0200033D RID: 829
		// (Invoke) Token: 0x060011F7 RID: 4599
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddDecalInstanceDelegate(UIntPtr scenePointer, UIntPtr decalMeshPointer, byte[] decalSetID, [MarshalAs(UnmanagedType.U1)] bool deletable);

		// Token: 0x0200033E RID: 830
		// (Invoke) Token: 0x060011FB RID: 4603
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int AddDirectionalLightDelegate(UIntPtr scenePointer, Vec3 position, Vec3 direction, float radius);

		// Token: 0x0200033F RID: 831
		// (Invoke) Token: 0x060011FF RID: 4607
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddEntityWithMeshDelegate(UIntPtr scenePointer, UIntPtr meshPointer, ref MatrixFrame frame);

		// Token: 0x02000340 RID: 832
		// (Invoke) Token: 0x06001203 RID: 4611
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddEntityWithMultiMeshDelegate(UIntPtr scenePointer, UIntPtr multiMeshPointer, ref MatrixFrame frame);

		// Token: 0x02000341 RID: 833
		// (Invoke) Token: 0x06001207 RID: 4615
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer AddItemEntityDelegate(UIntPtr scenePointer, ref MatrixFrame frame, UIntPtr meshPointer);

		// Token: 0x02000342 RID: 834
		// (Invoke) Token: 0x0600120B RID: 4619
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddPathDelegate(UIntPtr scenePointer, byte[] name);

		// Token: 0x02000343 RID: 835
		// (Invoke) Token: 0x0600120F RID: 4623
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddPathPointDelegate(UIntPtr scenePointer, byte[] name, ref MatrixFrame frame);

		// Token: 0x02000344 RID: 836
		// (Invoke) Token: 0x06001213 RID: 4627
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int AddPointLightDelegate(UIntPtr scenePointer, Vec3 position, float radius);

		// Token: 0x02000345 RID: 837
		// (Invoke) Token: 0x06001217 RID: 4631
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool AttachEntityDelegate(UIntPtr scenePointer, UIntPtr entity, [MarshalAs(UnmanagedType.U1)] bool showWarnings);

		// Token: 0x02000346 RID: 838
		// (Invoke) Token: 0x0600121B RID: 4635
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool BoxCastDelegate(UIntPtr scenePointer, ref Vec3 boxPointBegin, ref Vec3 boxPointEnd, ref Vec3 dir, float distance, ref float collisionDistance, ref Vec3 closestPoint, ref UIntPtr entityIndex, BodyFlags bodyExcludeFlags);

		// Token: 0x02000347 RID: 839
		// (Invoke) Token: 0x0600121F RID: 4639
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool BoxCastOnlyForCameraDelegate(UIntPtr scenePointer, IntPtr boxPoints, ref Vec3 centerPoint, ref Vec3 dir, float distance, ref float collisionDistance, ref Vec3 closestPoint, ref UIntPtr entityIndex, BodyFlags bodyExcludeFlags, [MarshalAs(UnmanagedType.U1)] bool preFilter, [MarshalAs(UnmanagedType.U1)] bool postFilter);

		// Token: 0x02000348 RID: 840
		// (Invoke) Token: 0x06001223 RID: 4643
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool CalculateEffectiveLightingDelegate(UIntPtr scenePointer);

		// Token: 0x02000349 RID: 841
		// (Invoke) Token: 0x06001227 RID: 4647
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool CheckPathEntitiesFrameChangedDelegate(UIntPtr scenePointer, byte[] containsName);

		// Token: 0x0200034A RID: 842
		// (Invoke) Token: 0x0600122B RID: 4651
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool CheckPointCanSeePointDelegate(UIntPtr scenePointer, Vec3 sourcePoint, Vec3 targetPoint, float distanceToCheck);

		// Token: 0x0200034B RID: 843
		// (Invoke) Token: 0x0600122F RID: 4655
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void CheckResourcesDelegate(UIntPtr scenePointer);

		// Token: 0x0200034C RID: 844
		// (Invoke) Token: 0x06001233 RID: 4659
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ClearAllDelegate(UIntPtr scenePointer);

		// Token: 0x0200034D RID: 845
		// (Invoke) Token: 0x06001237 RID: 4663
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ClearDecalsDelegate(UIntPtr scenePointer);

		// Token: 0x0200034E RID: 846
		// (Invoke) Token: 0x0600123B RID: 4667
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool ContainsTerrainDelegate(UIntPtr scenePointer);

		// Token: 0x0200034F RID: 847
		// (Invoke) Token: 0x0600123F RID: 4671
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void CreateBurstParticleDelegate(UIntPtr scene, int particleId, ref MatrixFrame frame);

		// Token: 0x02000350 RID: 848
		// (Invoke) Token: 0x06001243 RID: 4675
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer CreateNewSceneDelegate([MarshalAs(UnmanagedType.U1)] bool initialize_physics, [MarshalAs(UnmanagedType.U1)] bool enable_decals, int atlasGroup, byte[] sceneName);

		// Token: 0x02000351 RID: 849
		// (Invoke) Token: 0x06001247 RID: 4679
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer CreatePathMeshDelegate(UIntPtr scenePointer, byte[] baseEntityName, [MarshalAs(UnmanagedType.U1)] bool isWaterPath);

		// Token: 0x02000352 RID: 850
		// (Invoke) Token: 0x0600124B RID: 4683
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer CreatePathMesh2Delegate(UIntPtr scenePointer, IntPtr pathNodes, int pathNodeCount, [MarshalAs(UnmanagedType.U1)] bool isWaterPath);

		// Token: 0x02000353 RID: 851
		// (Invoke) Token: 0x0600124F RID: 4687
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void DeletePathWithNameDelegate(UIntPtr scenePointer, byte[] name);

		// Token: 0x02000354 RID: 852
		// (Invoke) Token: 0x06001253 RID: 4691
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void DisableStaticShadowsDelegate(UIntPtr ptr, [MarshalAs(UnmanagedType.U1)] bool value);

		// Token: 0x02000355 RID: 853
		// (Invoke) Token: 0x06001257 RID: 4695
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool DoesPathExistBetweenFacesDelegate(UIntPtr scenePointer, int firstNavMeshFace, int secondNavMeshFace, [MarshalAs(UnmanagedType.U1)] bool ignoreDisabled);

		// Token: 0x02000356 RID: 854
		// (Invoke) Token: 0x0600125B RID: 4699
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool DoesPathExistBetweenPositionsDelegate(UIntPtr scenePointer, WorldPosition position, WorldPosition destination);

		// Token: 0x02000357 RID: 855
		// (Invoke) Token: 0x0600125F RID: 4703
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void EnsurePostfxSystemDelegate(UIntPtr scenePointer);

		// Token: 0x02000358 RID: 856
		// (Invoke) Token: 0x06001263 RID: 4707
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void FillEntityWithHardBorderPhysicsBarrierDelegate(UIntPtr scenePointer, UIntPtr entityPointer);

		// Token: 0x02000359 RID: 857
		// (Invoke) Token: 0x06001267 RID: 4711
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void FillTerrainHeightDataDelegate(UIntPtr scene, int xIndex, int yIndex, IntPtr heightArray);

		// Token: 0x0200035A RID: 858
		// (Invoke) Token: 0x0600126B RID: 4715
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void FillTerrainPhysicsMaterialIndexDataDelegate(UIntPtr scene, int xIndex, int yIndex, IntPtr materialIndexArray);

		// Token: 0x0200035B RID: 859
		// (Invoke) Token: 0x0600126F RID: 4719
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void FinishSceneSoundsDelegate(UIntPtr scenePointer);

		// Token: 0x0200035C RID: 860
		// (Invoke) Token: 0x06001273 RID: 4723
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ForceLoadResourcesDelegate(UIntPtr scenePointer);

		// Token: 0x0200035D RID: 861
		// (Invoke) Token: 0x06001277 RID: 4727
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GenerateContactsWithCapsuleDelegate(UIntPtr scenePointer, ref CapsuleData cap, BodyFlags exclude_flags, IntPtr intersections);

		// Token: 0x0200035E RID: 862
		// (Invoke) Token: 0x0600127B RID: 4731
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetAllColorGradeNamesDelegate(UIntPtr scene);

		// Token: 0x0200035F RID: 863
		// (Invoke) Token: 0x0600127F RID: 4735
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetAllEntitiesWithScriptComponentDelegate(UIntPtr scenePointer, byte[] scriptComponentName, UIntPtr output);

		// Token: 0x02000360 RID: 864
		// (Invoke) Token: 0x06001283 RID: 4739
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetAllFilterNamesDelegate(UIntPtr scene);

		// Token: 0x02000361 RID: 865
		// (Invoke) Token: 0x06001287 RID: 4743
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetBoundingBoxDelegate(UIntPtr scenePointer, ref Vec3 min, ref Vec3 max);

		// Token: 0x02000362 RID: 866
		// (Invoke) Token: 0x0600128B RID: 4747
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer GetCampaignEntityWithNameDelegate(UIntPtr scenePointer, byte[] entityName);

		// Token: 0x02000363 RID: 867
		// (Invoke) Token: 0x0600128F RID: 4751
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetEntitiesDelegate(UIntPtr scenePointer, UIntPtr entityObjectsArrayPointer);

		// Token: 0x02000364 RID: 868
		// (Invoke) Token: 0x06001293 RID: 4755
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetEntityCountDelegate(UIntPtr scenePointer);

		// Token: 0x02000365 RID: 869
		// (Invoke) Token: 0x06001297 RID: 4759
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer GetEntityWithGuidDelegate(UIntPtr scenePointer, byte[] guid);

		// Token: 0x02000366 RID: 870
		// (Invoke) Token: 0x0600129B RID: 4763
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer GetFirstEntityWithNameDelegate(UIntPtr scenePointer, byte[] entityName);

		// Token: 0x02000367 RID: 871
		// (Invoke) Token: 0x0600129F RID: 4767
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer GetFirstEntityWithScriptComponentDelegate(UIntPtr scenePointer, byte[] scriptComponentName);

		// Token: 0x02000368 RID: 872
		// (Invoke) Token: 0x060012A3 RID: 4771
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetFloraInstanceCountDelegate(UIntPtr scenePointer);

		// Token: 0x02000369 RID: 873
		// (Invoke) Token: 0x060012A7 RID: 4775
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetFloraRendererTextureUsageDelegate(UIntPtr scenePointer);

		// Token: 0x0200036A RID: 874
		// (Invoke) Token: 0x060012AB RID: 4779
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetFogDelegate(UIntPtr scenePointer);

		// Token: 0x0200036B RID: 875
		// (Invoke) Token: 0x060012AF RID: 4783
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetGroundHeightAndNormalAtPositionDelegate(UIntPtr scenePointer, Vec3 position, ref Vec3 normal, uint excludeFlags);

		// Token: 0x0200036C RID: 876
		// (Invoke) Token: 0x060012B3 RID: 4787
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetGroundHeightAtPositionDelegate(UIntPtr scenePointer, Vec3 position, uint excludeFlags);

		// Token: 0x0200036D RID: 877
		// (Invoke) Token: 0x060012B7 RID: 4791
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate Vec2 GetHardBoundaryVertexDelegate(UIntPtr scenePointer, int index);

		// Token: 0x0200036E RID: 878
		// (Invoke) Token: 0x060012BB RID: 4795
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetHardBoundaryVertexCountDelegate(UIntPtr scenePointer);

		// Token: 0x0200036F RID: 879
		// (Invoke) Token: 0x060012BF RID: 4799
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool GetHeightAtPointDelegate(UIntPtr scenePointer, Vec2 point, BodyFlags excludeBodyFlags, ref float height);

		// Token: 0x02000370 RID: 880
		// (Invoke) Token: 0x060012C3 RID: 4803
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetIdOfNavMeshFaceDelegate(UIntPtr scenePointer, int navMeshFace);

		// Token: 0x02000371 RID: 881
		// (Invoke) Token: 0x060012C7 RID: 4807
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetLastFinalRenderCameraFrameDelegate(UIntPtr scenePointer, ref MatrixFrame outFrame);

		// Token: 0x02000372 RID: 882
		// (Invoke) Token: 0x060012CB RID: 4811
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate Vec3 GetLastFinalRenderCameraPositionDelegate(UIntPtr scenePointer);

		// Token: 0x02000373 RID: 883
		// (Invoke) Token: 0x060012CF RID: 4815
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate Vec2 GetLastPointOnNavigationMeshFromPositionToDestinationDelegate(UIntPtr scenePointer, int startingFace, Vec2 position, Vec2 destination);

		// Token: 0x02000374 RID: 884
		// (Invoke) Token: 0x060012D3 RID: 4819
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate Vec3 GetLastPointOnNavigationMeshFromWorldPositionToDestinationDelegate(UIntPtr scenePointer, ref WorldPosition position, Vec2 destination);

		// Token: 0x02000375 RID: 885
		// (Invoke) Token: 0x060012D7 RID: 4823
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetLoadingStateNameDelegate(UIntPtr scene);

		// Token: 0x02000376 RID: 886
		// (Invoke) Token: 0x060012DB RID: 4827
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetModulePathDelegate(UIntPtr scenePointer);

		// Token: 0x02000377 RID: 887
		// (Invoke) Token: 0x060012DF RID: 4831
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetNameDelegate(UIntPtr scenePointer);

		// Token: 0x02000378 RID: 888
		// (Invoke) Token: 0x060012E3 RID: 4835
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool GetNavigationMeshFaceForPositionDelegate(UIntPtr scenePointer, ref Vec3 position, ref int faceGroupId, float heightDifferenceLimit);

		// Token: 0x02000379 RID: 889
		// (Invoke) Token: 0x060012E7 RID: 4839
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetNavMeshFaceCenterPositionDelegate(UIntPtr scenePointer, int navMeshFace, ref Vec3 centerPos);

		// Token: 0x0200037A RID: 890
		// (Invoke) Token: 0x060012EB RID: 4843
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetNavMeshFaceCountDelegate(UIntPtr scenePointer);

		// Token: 0x0200037B RID: 891
		// (Invoke) Token: 0x060012EF RID: 4847
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetNavMeshFaceFirstVertexZDelegate(UIntPtr scenePointer, int navMeshFaceIndex);

		// Token: 0x0200037C RID: 892
		// (Invoke) Token: 0x060012F3 RID: 4851
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetNavMeshFaceIndexDelegate(UIntPtr scenePointer, ref PathFaceRecord record, Vec2 position, [MarshalAs(UnmanagedType.U1)] bool checkIfDisabled, [MarshalAs(UnmanagedType.U1)] bool ignoreHeight);

		// Token: 0x0200037D RID: 893
		// (Invoke) Token: 0x060012F7 RID: 4855
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetNavMeshFaceIndex3Delegate(UIntPtr scenePointer, ref PathFaceRecord record, Vec3 position, [MarshalAs(UnmanagedType.U1)] bool checkIfDisabled);

		// Token: 0x0200037E RID: 894
		// (Invoke) Token: 0x060012FB RID: 4859
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetNodeDataCountDelegate(UIntPtr scene, int xIndex, int yIndex);

		// Token: 0x0200037F RID: 895
		// (Invoke) Token: 0x060012FF RID: 4863
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate Vec3 GetNormalAtDelegate(UIntPtr scenePointer, Vec2 position);

		// Token: 0x02000380 RID: 896
		// (Invoke) Token: 0x06001303 RID: 4867
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetNorthAngleDelegate(UIntPtr scenePointer);

		// Token: 0x02000381 RID: 897
		// (Invoke) Token: 0x06001307 RID: 4871
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetNumberOfPathsWithNamePrefixDelegate(UIntPtr ptr, byte[] prefix);

		// Token: 0x02000382 RID: 898
		// (Invoke) Token: 0x0600130B RID: 4875
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool GetPathBetweenAIFaceIndicesDelegate(UIntPtr scenePointer, int startingAiFace, int endingAiFace, Vec2 startingPosition, Vec2 endingPosition, float agentRadius, IntPtr result, ref int pathSize, IntPtr exclusionGroupIds, int exlusionGroupIdsCount, float extraCostMultiplier);

		// Token: 0x02000383 RID: 899
		// (Invoke) Token: 0x0600130F RID: 4879
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool GetPathBetweenAIFacePointersDelegate(UIntPtr scenePointer, UIntPtr startingAiFace, UIntPtr endingAiFace, Vec2 startingPosition, Vec2 endingPosition, float agentRadius, IntPtr result, ref int pathSize, IntPtr exclusionGroupIds, int exlusionGroupIdsCount);

		// Token: 0x02000384 RID: 900
		// (Invoke) Token: 0x06001313 RID: 4883
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool GetPathDistanceBetweenAIFacesDelegate(UIntPtr scenePointer, int startingAiFace, int endingAiFace, Vec2 startingPosition, Vec2 endingPosition, float agentRadius, float distanceLimit, out float distance);

		// Token: 0x02000385 RID: 901
		// (Invoke) Token: 0x06001317 RID: 4887
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool GetPathDistanceBetweenPositionsDelegate(UIntPtr scenePointer, ref WorldPosition position, ref WorldPosition destination, float agentRadius, ref float pathLength);

		// Token: 0x02000386 RID: 902
		// (Invoke) Token: 0x0600131B RID: 4891
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetPathsWithNamePrefixDelegate(UIntPtr ptr, IntPtr points, byte[] prefix);

		// Token: 0x02000387 RID: 903
		// (Invoke) Token: 0x0600131F RID: 4895
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer GetPathWithNameDelegate(UIntPtr scenePointer, byte[] name);

		// Token: 0x02000388 RID: 904
		// (Invoke) Token: 0x06001323 RID: 4899
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetPhotoModeFocusDelegate(UIntPtr scene, ref float focus, ref float focusStart, ref float focusEnd, ref float exposure, [MarshalAs(UnmanagedType.U1)] ref bool vignetteOn);

		// Token: 0x02000389 RID: 905
		// (Invoke) Token: 0x06001327 RID: 4903
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetPhotoModeFovDelegate(UIntPtr scene);

		// Token: 0x0200038A RID: 906
		// (Invoke) Token: 0x0600132B RID: 4907
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool GetPhotoModeOnDelegate(UIntPtr scene);

		// Token: 0x0200038B RID: 907
		// (Invoke) Token: 0x0600132F RID: 4911
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool GetPhotoModeOrbitDelegate(UIntPtr scene);

		// Token: 0x0200038C RID: 908
		// (Invoke) Token: 0x06001333 RID: 4915
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetPhotoModeRollDelegate(UIntPtr scene);

		// Token: 0x0200038D RID: 909
		// (Invoke) Token: 0x06001337 RID: 4919
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetPhysicsMinMaxDelegate(UIntPtr scenePointer, ref Vec3 min_max);

		// Token: 0x0200038E RID: 910
		// (Invoke) Token: 0x0600133B RID: 4923
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetRainDensityDelegate(UIntPtr scenePointer);

		// Token: 0x0200038F RID: 911
		// (Invoke) Token: 0x0600133F RID: 4927
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetRootEntitiesDelegate(UIntPtr scene, UIntPtr output);

		// Token: 0x02000390 RID: 912
		// (Invoke) Token: 0x06001343 RID: 4931
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetRootEntityCountDelegate(UIntPtr scenePointer);

		// Token: 0x02000391 RID: 913
		// (Invoke) Token: 0x06001347 RID: 4935
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetSceneColorGradeIndexDelegate(UIntPtr scene);

		// Token: 0x02000392 RID: 914
		// (Invoke) Token: 0x0600134B RID: 4939
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetSceneFilterIndexDelegate(UIntPtr scene);

		// Token: 0x02000393 RID: 915
		// (Invoke) Token: 0x0600134F RID: 4943
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer GetScriptedEntityDelegate(UIntPtr scenePointer, int index);

		// Token: 0x02000394 RID: 916
		// (Invoke) Token: 0x06001353 RID: 4947
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetScriptedEntityCountDelegate(UIntPtr scenePointer);

		// Token: 0x02000395 RID: 917
		// (Invoke) Token: 0x06001357 RID: 4951
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer GetSkyboxMeshDelegate(UIntPtr ptr);

		// Token: 0x02000396 RID: 918
		// (Invoke) Token: 0x0600135B RID: 4955
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetSnowAmountDataDelegate(UIntPtr scenePointer, ManagedArray bytes);

		// Token: 0x02000397 RID: 919
		// (Invoke) Token: 0x0600135F RID: 4959
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetSnowDensityDelegate(UIntPtr scenePointer);

		// Token: 0x02000398 RID: 920
		// (Invoke) Token: 0x06001363 RID: 4963
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate Vec2 GetSoftBoundaryVertexDelegate(UIntPtr scenePointer, int index);

		// Token: 0x02000399 RID: 921
		// (Invoke) Token: 0x06001367 RID: 4967
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetSoftBoundaryVertexCountDelegate(UIntPtr scenePointer);

		// Token: 0x0200039A RID: 922
		// (Invoke) Token: 0x0600136B RID: 4971
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate Vec3 GetSunDirectionDelegate(UIntPtr scenePointer);

		// Token: 0x0200039B RID: 923
		// (Invoke) Token: 0x0600136F RID: 4975
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetTerrainDataDelegate(UIntPtr scene, out Vec2i nodeDimension, out float nodeSize, out int layerCount, out int layerVersion);

		// Token: 0x0200039C RID: 924
		// (Invoke) Token: 0x06001373 RID: 4979
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetTerrainHeightDelegate(UIntPtr scenePointer, Vec2 position, [MarshalAs(UnmanagedType.U1)] bool checkHoles);

		// Token: 0x0200039D RID: 925
		// (Invoke) Token: 0x06001377 RID: 4983
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetTerrainHeightAndNormalDelegate(UIntPtr scenePointer, Vec2 position, out float height, out Vec3 normal);

		// Token: 0x0200039E RID: 926
		// (Invoke) Token: 0x0600137B RID: 4987
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetTerrainMemoryUsageDelegate(UIntPtr scenePointer);

		// Token: 0x0200039F RID: 927
		// (Invoke) Token: 0x0600137F RID: 4991
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool GetTerrainMinMaxHeightDelegate(UIntPtr scene, ref float min, ref float max);

		// Token: 0x020003A0 RID: 928
		// (Invoke) Token: 0x06001383 RID: 4995
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetTerrainNodeDataDelegate(UIntPtr scene, int xIndex, int yIndex, out int vertexCountAlongAxis, out float quadLength, out float minHeight, out float maxHeight);

		// Token: 0x020003A1 RID: 929
		// (Invoke) Token: 0x06001387 RID: 4999
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetTerrainPhysicsMaterialIndexAtLayerDelegate(UIntPtr scene, int layerIndex);

		// Token: 0x020003A2 RID: 930
		// (Invoke) Token: 0x0600138B RID: 5003
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetTimeOfDayDelegate(UIntPtr scenePointer);

		// Token: 0x020003A3 RID: 931
		// (Invoke) Token: 0x0600138F RID: 5007
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetTimeSpeedDelegate(UIntPtr scenePointer);

		// Token: 0x020003A4 RID: 932
		// (Invoke) Token: 0x06001393 RID: 5011
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetUpgradeLevelCountDelegate(UIntPtr scenePointer);

		// Token: 0x020003A5 RID: 933
		// (Invoke) Token: 0x06001397 RID: 5015
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate uint GetUpgradeLevelMaskDelegate(UIntPtr scenePointer);

		// Token: 0x020003A6 RID: 934
		// (Invoke) Token: 0x0600139B RID: 5019
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate uint GetUpgradeLevelMaskOfLevelNameDelegate(UIntPtr scenePointer, byte[] levelName);

		// Token: 0x020003A7 RID: 935
		// (Invoke) Token: 0x0600139F RID: 5023
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetUpgradeLevelNameOfIndexDelegate(UIntPtr scenePointer, int index);

		// Token: 0x020003A8 RID: 936
		// (Invoke) Token: 0x060013A3 RID: 5027
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetWaterLevelDelegate(UIntPtr scene);

		// Token: 0x020003A9 RID: 937
		// (Invoke) Token: 0x060013A7 RID: 5031
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetWaterLevelAtPositionDelegate(UIntPtr scene, Vec2 position, [MarshalAs(UnmanagedType.U1)] bool checkWaterBodyEntities);

		// Token: 0x020003AA RID: 938
		// (Invoke) Token: 0x060013AB RID: 5035
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetWinterTimeFactorDelegate(UIntPtr scenePointer);

		// Token: 0x020003AB RID: 939
		// (Invoke) Token: 0x060013AF RID: 5039
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool HasTerrainHeightmapDelegate(UIntPtr scenePointer);

		// Token: 0x020003AC RID: 940
		// (Invoke) Token: 0x060013B3 RID: 5043
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void InvalidateTerrainPhysicsMaterialsDelegate(UIntPtr scenePointer);

		// Token: 0x020003AD RID: 941
		// (Invoke) Token: 0x060013B7 RID: 5047
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsAnyFaceWithIdDelegate(UIntPtr scenePointer, int faceGroupId);

		// Token: 0x020003AE RID: 942
		// (Invoke) Token: 0x060013BB RID: 5051
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsAtmosphereIndoorDelegate(UIntPtr scenePointer);

		// Token: 0x020003AF RID: 943
		// (Invoke) Token: 0x060013BF RID: 5055
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsDefaultEditorSceneDelegate(UIntPtr scene);

		// Token: 0x020003B0 RID: 944
		// (Invoke) Token: 0x060013C3 RID: 5059
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsEditorSceneDelegate(UIntPtr scenePointer);

		// Token: 0x020003B1 RID: 945
		// (Invoke) Token: 0x060013C7 RID: 5063
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsLineToPointClearDelegate(UIntPtr scenePointer, int startingFace, Vec2 position, Vec2 destination, float agentRadius);

		// Token: 0x020003B2 RID: 946
		// (Invoke) Token: 0x060013CB RID: 5067
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsLineToPointClear2Delegate(UIntPtr scenePointer, UIntPtr startingFace, Vec2 position, Vec2 destination, float agentRadius);

		// Token: 0x020003B3 RID: 947
		// (Invoke) Token: 0x060013CF RID: 5071
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsLoadingFinishedDelegate(UIntPtr scene);

		// Token: 0x020003B4 RID: 948
		// (Invoke) Token: 0x060013D3 RID: 5075
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsMultiplayerSceneDelegate(UIntPtr scene);

		// Token: 0x020003B5 RID: 949
		// (Invoke) Token: 0x060013D7 RID: 5079
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void LoadNavMeshPrefabDelegate(UIntPtr scenePointer, byte[] navMeshPrefabName, int navMeshGroupIdShift);

		// Token: 0x020003B6 RID: 950
		// (Invoke) Token: 0x060013DB RID: 5083
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void MarkFacesWithIdAsLadderDelegate(UIntPtr scenePointer, int faceGroupId, [MarshalAs(UnmanagedType.U1)] bool isLadder);

		// Token: 0x020003B7 RID: 951
		// (Invoke) Token: 0x060013DF RID: 5087
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void MergeFacesWithIdDelegate(UIntPtr scenePointer, int faceGroupId0, int faceGroupId1, int newFaceGroupId);

		// Token: 0x020003B8 RID: 952
		// (Invoke) Token: 0x060013E3 RID: 5091
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void OptimizeSceneDelegate(UIntPtr scenePointer, [MarshalAs(UnmanagedType.U1)] bool optimizeFlora, [MarshalAs(UnmanagedType.U1)] bool optimizeOro);

		// Token: 0x020003B9 RID: 953
		// (Invoke) Token: 0x060013E7 RID: 5095
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void PauseSceneSoundsDelegate(UIntPtr scenePointer);

		// Token: 0x020003BA RID: 954
		// (Invoke) Token: 0x060013EB RID: 5099
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void PreloadForRenderingDelegate(UIntPtr scenePointer);

		// Token: 0x020003BB RID: 955
		// (Invoke) Token: 0x060013EF RID: 5103
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool RayCastForClosestEntityOrTerrainDelegate(UIntPtr scenePointer, ref Vec3 sourcePoint, ref Vec3 targetPoint, float rayThickness, ref float collisionDistance, ref Vec3 closestPoint, ref UIntPtr entityIndex, BodyFlags bodyExcludeFlags);

		// Token: 0x020003BC RID: 956
		// (Invoke) Token: 0x060013F3 RID: 5107
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ReadDelegate(UIntPtr scenePointer, byte[] sceneName, ref SceneInitializationData initData, byte[] forcedAtmoName);

		// Token: 0x020003BD RID: 957
		// (Invoke) Token: 0x060013F7 RID: 5111
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ReadAndCalculateInitialCameraDelegate(UIntPtr scenePointer, ref MatrixFrame outFrame);

		// Token: 0x020003BE RID: 958
		// (Invoke) Token: 0x060013FB RID: 5115
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RemoveEntityDelegate(UIntPtr scenePointer, UIntPtr entityId, int removeReason);

		// Token: 0x020003BF RID: 959
		// (Invoke) Token: 0x060013FF RID: 5119
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ResumeLoadingRenderingsDelegate(UIntPtr scenePointer);

		// Token: 0x020003C0 RID: 960
		// (Invoke) Token: 0x06001403 RID: 5123
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ResumeSceneSoundsDelegate(UIntPtr scenePointer);

		// Token: 0x020003C1 RID: 961
		// (Invoke) Token: 0x06001407 RID: 5127
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int SelectEntitiesCollidedWithDelegate(UIntPtr scenePointer, ref Ray ray, IntPtr entityIds, IntPtr intersections);

		// Token: 0x020003C2 RID: 962
		// (Invoke) Token: 0x0600140B RID: 5131
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int SelectEntitiesInBoxWithScriptComponentDelegate(UIntPtr scenePointer, ref Vec3 boundingBoxMin, ref Vec3 boundingBoxMax, IntPtr entitiesOutput, int maxCount, byte[] scriptComponentName);

		// Token: 0x020003C3 RID: 963
		// (Invoke) Token: 0x0600140F RID: 5135
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SeparateFacesWithIdDelegate(UIntPtr scenePointer, int faceGroupId0, int faceGroupId1);

		// Token: 0x020003C4 RID: 964
		// (Invoke) Token: 0x06001413 RID: 5139
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetAberrationOffsetDelegate(UIntPtr scenePointer, float aberrationOffset);

		// Token: 0x020003C5 RID: 965
		// (Invoke) Token: 0x06001417 RID: 5143
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetAberrationSizeDelegate(UIntPtr scenePointer, float aberrationSize);

		// Token: 0x020003C6 RID: 966
		// (Invoke) Token: 0x0600141B RID: 5147
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetAberrationSmoothDelegate(UIntPtr scenePointer, float aberrationSmooth);

		// Token: 0x020003C7 RID: 967
		// (Invoke) Token: 0x0600141F RID: 5151
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetAbilityOfFacesWithIdDelegate(UIntPtr scenePointer, int faceGroupId, [MarshalAs(UnmanagedType.U1)] bool isEnabled);

		// Token: 0x020003C8 RID: 968
		// (Invoke) Token: 0x06001423 RID: 5155
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetActiveVisibilityLevelsDelegate(UIntPtr scenePointer, byte[] levelsAppended);

		// Token: 0x020003C9 RID: 969
		// (Invoke) Token: 0x06001427 RID: 5159
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetAntialiasingModeDelegate(UIntPtr scenePointer, [MarshalAs(UnmanagedType.U1)] bool mode);

		// Token: 0x020003CA RID: 970
		// (Invoke) Token: 0x0600142B RID: 5163
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetAtmosphereWithNameDelegate(UIntPtr ptr, byte[] name);

		// Token: 0x020003CB RID: 971
		// (Invoke) Token: 0x0600142F RID: 5167
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetBloomDelegate(UIntPtr scenePointer, [MarshalAs(UnmanagedType.U1)] bool mode);

		// Token: 0x020003CC RID: 972
		// (Invoke) Token: 0x06001433 RID: 5171
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetBloomAmountDelegate(UIntPtr scenePointer, float bloomAmount);

		// Token: 0x020003CD RID: 973
		// (Invoke) Token: 0x06001437 RID: 5175
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetBloomStrengthDelegate(UIntPtr scenePointer, float bloomStrength);

		// Token: 0x020003CE RID: 974
		// (Invoke) Token: 0x0600143B RID: 5179
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetBrightpassTresholdDelegate(UIntPtr scenePointer, float threshold);

		// Token: 0x020003CF RID: 975
		// (Invoke) Token: 0x0600143F RID: 5183
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetClothSimulationStateDelegate(UIntPtr scenePointer, [MarshalAs(UnmanagedType.U1)] bool state);

		// Token: 0x020003D0 RID: 976
		// (Invoke) Token: 0x06001443 RID: 5187
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetColorGradeBlendDelegate(UIntPtr scenePointer, byte[] texture1, byte[] texture2, float alpha);

		// Token: 0x020003D1 RID: 977
		// (Invoke) Token: 0x06001447 RID: 5191
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetDLSSModeDelegate(UIntPtr scenePointer, [MarshalAs(UnmanagedType.U1)] bool mode);

		// Token: 0x020003D2 RID: 978
		// (Invoke) Token: 0x0600144B RID: 5195
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetDofFocusDelegate(UIntPtr scenePointer, float dofFocus);

		// Token: 0x020003D3 RID: 979
		// (Invoke) Token: 0x0600144F RID: 5199
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetDofModeDelegate(UIntPtr scenePointer, [MarshalAs(UnmanagedType.U1)] bool mode);

		// Token: 0x020003D4 RID: 980
		// (Invoke) Token: 0x06001453 RID: 5203
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetDofParamsDelegate(UIntPtr scenePointer, float dofFocusStart, float dofFocusEnd, [MarshalAs(UnmanagedType.U1)] bool isVignetteOn);

		// Token: 0x020003D5 RID: 981
		// (Invoke) Token: 0x06001457 RID: 5207
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetDoNotWaitForLoadingStatesToRenderDelegate(UIntPtr scenePointer, [MarshalAs(UnmanagedType.U1)] bool value);

		// Token: 0x020003D6 RID: 982
		// (Invoke) Token: 0x0600145B RID: 5211
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetDrynessFactorDelegate(UIntPtr scenePointer, float drynessFactor);

		// Token: 0x020003D7 RID: 983
		// (Invoke) Token: 0x0600145F RID: 5215
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetDynamicShadowmapCascadesRadiusMultiplierDelegate(UIntPtr scenePointer, float extraRadius);

		// Token: 0x020003D8 RID: 984
		// (Invoke) Token: 0x06001463 RID: 5219
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetEnvironmentMultiplierDelegate(UIntPtr scenePointer, [MarshalAs(UnmanagedType.U1)] bool useMultiplier, float multiplier);

		// Token: 0x020003D9 RID: 985
		// (Invoke) Token: 0x06001467 RID: 5223
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetExternalInjectionTextureDelegate(UIntPtr scenePointer, UIntPtr texturePointer);

		// Token: 0x020003DA RID: 986
		// (Invoke) Token: 0x0600146B RID: 5227
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetFogDelegate(UIntPtr scenePointer, float fogDensity, ref Vec3 fogColor, float fogFalloff);

		// Token: 0x020003DB RID: 987
		// (Invoke) Token: 0x0600146F RID: 5231
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetFogAdvancedDelegate(UIntPtr scenePointer, float fogFalloffOffset, float fogFalloffMinFog, float fogFalloffStartDist);

		// Token: 0x020003DC RID: 988
		// (Invoke) Token: 0x06001473 RID: 5235
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetFogAmbientColorDelegate(UIntPtr scenePointer, ref Vec3 fogAmbientColor);

		// Token: 0x020003DD RID: 989
		// (Invoke) Token: 0x06001477 RID: 5239
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetForcedSnowDelegate(UIntPtr scenePointer, [MarshalAs(UnmanagedType.U1)] bool value);

		// Token: 0x020003DE RID: 990
		// (Invoke) Token: 0x0600147B RID: 5243
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetGrainAmountDelegate(UIntPtr scenePointer, float grainAmount);

		// Token: 0x020003DF RID: 991
		// (Invoke) Token: 0x0600147F RID: 5247
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetHexagonVignetteAlphaDelegate(UIntPtr scenePointer, float Alpha);

		// Token: 0x020003E0 RID: 992
		// (Invoke) Token: 0x06001483 RID: 5251
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetHexagonVignetteColorDelegate(UIntPtr scenePointer, ref Vec3 p_hexagon_vignette_color);

		// Token: 0x020003E1 RID: 993
		// (Invoke) Token: 0x06001487 RID: 5255
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetHumidityDelegate(UIntPtr scenePointer, float humidity);

		// Token: 0x020003E2 RID: 994
		// (Invoke) Token: 0x0600148B RID: 5259
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetLandscapeRainMaskDataDelegate(UIntPtr scenePointer, ManagedArray data);

		// Token: 0x020003E3 RID: 995
		// (Invoke) Token: 0x0600148F RID: 5263
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetLensDistortionDelegate(UIntPtr scenePointer, float lensDistortion);

		// Token: 0x020003E4 RID: 996
		// (Invoke) Token: 0x06001493 RID: 5267
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetLensFlareAberrationOffsetDelegate(UIntPtr scenePointer, float lensFlareAberrationOffset);

		// Token: 0x020003E5 RID: 997
		// (Invoke) Token: 0x06001497 RID: 5271
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetLensFlareAmountDelegate(UIntPtr scenePointer, float lensFlareAmount);

		// Token: 0x020003E6 RID: 998
		// (Invoke) Token: 0x0600149B RID: 5275
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetLensFlareBlurSigmaDelegate(UIntPtr scenePointer, float lensFlareBlurSigma);

		// Token: 0x020003E7 RID: 999
		// (Invoke) Token: 0x0600149F RID: 5279
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetLensFlareBlurSizeDelegate(UIntPtr scenePointer, int lensFlareBlurSize);

		// Token: 0x020003E8 RID: 1000
		// (Invoke) Token: 0x060014A3 RID: 5283
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetLensFlareDiffractionWeightDelegate(UIntPtr scenePointer, float lensFlareDiffractionWeight);

		// Token: 0x020003E9 RID: 1001
		// (Invoke) Token: 0x060014A7 RID: 5287
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetLensFlareDirtWeightDelegate(UIntPtr scenePointer, float lensFlareDirtWeight);

		// Token: 0x020003EA RID: 1002
		// (Invoke) Token: 0x060014AB RID: 5291
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetLensFlareGhostSamplesDelegate(UIntPtr scenePointer, int lensFlareGhostSamples);

		// Token: 0x020003EB RID: 1003
		// (Invoke) Token: 0x060014AF RID: 5295
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetLensFlareGhostWeightDelegate(UIntPtr scenePointer, float lensFlareGhostWeight);

		// Token: 0x020003EC RID: 1004
		// (Invoke) Token: 0x060014B3 RID: 5299
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetLensFlareHaloWeightDelegate(UIntPtr scenePointer, float lensFlareHaloWeight);

		// Token: 0x020003ED RID: 1005
		// (Invoke) Token: 0x060014B7 RID: 5303
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetLensFlareHaloWidthDelegate(UIntPtr scenePointer, float lensFlareHaloWidth);

		// Token: 0x020003EE RID: 1006
		// (Invoke) Token: 0x060014BB RID: 5307
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetLensFlareStrengthDelegate(UIntPtr scenePointer, float lensFlareStrength);

		// Token: 0x020003EF RID: 1007
		// (Invoke) Token: 0x060014BF RID: 5311
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetLensFlareThresholdDelegate(UIntPtr scenePointer, float lensFlareThreshold);

		// Token: 0x020003F0 RID: 1008
		// (Invoke) Token: 0x060014C3 RID: 5315
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetLightDiffuseColorDelegate(UIntPtr scenePointer, int lightIndex, Vec3 diffuseColor);

		// Token: 0x020003F1 RID: 1009
		// (Invoke) Token: 0x060014C7 RID: 5319
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetLightDirectionDelegate(UIntPtr scenePointer, int lightIndex, Vec3 direction);

		// Token: 0x020003F2 RID: 1010
		// (Invoke) Token: 0x060014CB RID: 5323
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetLightPositionDelegate(UIntPtr scenePointer, int lightIndex, Vec3 position);

		// Token: 0x020003F3 RID: 1011
		// (Invoke) Token: 0x060014CF RID: 5327
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetMaxExposureDelegate(UIntPtr scenePointer, float maxExposure);

		// Token: 0x020003F4 RID: 1012
		// (Invoke) Token: 0x060014D3 RID: 5331
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetMiddleGrayDelegate(UIntPtr scenePointer, float middleGray);

		// Token: 0x020003F5 RID: 1013
		// (Invoke) Token: 0x060014D7 RID: 5335
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetMieScatterFocusDelegate(UIntPtr scenePointer, float strength);

		// Token: 0x020003F6 RID: 1014
		// (Invoke) Token: 0x060014DB RID: 5339
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetMieScatterStrengthDelegate(UIntPtr scenePointer, float strength);

		// Token: 0x020003F7 RID: 1015
		// (Invoke) Token: 0x060014DF RID: 5343
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetMinExposureDelegate(UIntPtr scenePointer, float minExposure);

		// Token: 0x020003F8 RID: 1016
		// (Invoke) Token: 0x060014E3 RID: 5347
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetMotionBlurModeDelegate(UIntPtr scenePointer, [MarshalAs(UnmanagedType.U1)] bool mode);

		// Token: 0x020003F9 RID: 1017
		// (Invoke) Token: 0x060014E7 RID: 5351
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetNameDelegate(UIntPtr scenePointer, byte[] name);

		// Token: 0x020003FA RID: 1018
		// (Invoke) Token: 0x060014EB RID: 5355
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetOcclusionModeDelegate(UIntPtr scenePointer, [MarshalAs(UnmanagedType.U1)] bool mode);

		// Token: 0x020003FB RID: 1019
		// (Invoke) Token: 0x060014EF RID: 5359
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetOwnerThreadDelegate(UIntPtr scenePointer);

		// Token: 0x020003FC RID: 1020
		// (Invoke) Token: 0x060014F3 RID: 5363
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetPhotoModeFocusDelegate(UIntPtr scene, float focusStart, float focusEnd, float focus, float exposure);

		// Token: 0x020003FD RID: 1021
		// (Invoke) Token: 0x060014F7 RID: 5367
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetPhotoModeFovDelegate(UIntPtr scene, float verticalFov);

		// Token: 0x020003FE RID: 1022
		// (Invoke) Token: 0x060014FB RID: 5371
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetPhotoModeOnDelegate(UIntPtr scene, [MarshalAs(UnmanagedType.U1)] bool on);

		// Token: 0x020003FF RID: 1023
		// (Invoke) Token: 0x060014FF RID: 5375
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetPhotoModeOrbitDelegate(UIntPtr scene, [MarshalAs(UnmanagedType.U1)] bool orbit);

		// Token: 0x02000400 RID: 1024
		// (Invoke) Token: 0x06001503 RID: 5379
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetPhotoModeRollDelegate(UIntPtr scene, float roll);

		// Token: 0x02000401 RID: 1025
		// (Invoke) Token: 0x06001507 RID: 5383
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetPhotoModeVignetteDelegate(UIntPtr scene, [MarshalAs(UnmanagedType.U1)] bool vignetteOn);

		// Token: 0x02000402 RID: 1026
		// (Invoke) Token: 0x0600150B RID: 5387
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetPlaySoundEventsAfterReadyToRenderDelegate(UIntPtr ptr, [MarshalAs(UnmanagedType.U1)] bool value);

		// Token: 0x02000403 RID: 1027
		// (Invoke) Token: 0x0600150F RID: 5391
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetRainDensityDelegate(UIntPtr scenePointer, float density);

		// Token: 0x02000404 RID: 1028
		// (Invoke) Token: 0x06001513 RID: 5395
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetSceneColorGradeDelegate(UIntPtr scene, byte[] textureName);

		// Token: 0x02000405 RID: 1029
		// (Invoke) Token: 0x06001517 RID: 5399
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetSceneColorGradeIndexDelegate(UIntPtr scene, int index);

		// Token: 0x02000406 RID: 1030
		// (Invoke) Token: 0x0600151B RID: 5403
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int SetSceneFilterIndexDelegate(UIntPtr scene, int index);

		// Token: 0x02000407 RID: 1031
		// (Invoke) Token: 0x0600151F RID: 5407
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetShadowDelegate(UIntPtr scenePointer, [MarshalAs(UnmanagedType.U1)] bool shadowEnabled);

		// Token: 0x02000408 RID: 1032
		// (Invoke) Token: 0x06001523 RID: 5411
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetSkyBrightnessDelegate(UIntPtr scenePointer, float brightness);

		// Token: 0x02000409 RID: 1033
		// (Invoke) Token: 0x06001527 RID: 5415
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetSkyRotationDelegate(UIntPtr scenePointer, float rotation);

		// Token: 0x0200040A RID: 1034
		// (Invoke) Token: 0x0600152B RID: 5419
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetSnowDensityDelegate(UIntPtr scenePointer, float density);

		// Token: 0x0200040B RID: 1035
		// (Invoke) Token: 0x0600152F RID: 5423
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetStreakAmountDelegate(UIntPtr scenePointer, float streakAmount);

		// Token: 0x0200040C RID: 1036
		// (Invoke) Token: 0x06001533 RID: 5427
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetStreakIntensityDelegate(UIntPtr scenePointer, float stretchAmount);

		// Token: 0x0200040D RID: 1037
		// (Invoke) Token: 0x06001537 RID: 5431
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetStreakStrengthDelegate(UIntPtr scenePointer, float strengthAmount);

		// Token: 0x0200040E RID: 1038
		// (Invoke) Token: 0x0600153B RID: 5435
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetStreakStretchDelegate(UIntPtr scenePointer, float stretchAmount);

		// Token: 0x0200040F RID: 1039
		// (Invoke) Token: 0x0600153F RID: 5439
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetStreakThresholdDelegate(UIntPtr scenePointer, float streakThreshold);

		// Token: 0x02000410 RID: 1040
		// (Invoke) Token: 0x06001543 RID: 5443
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetStreakTintDelegate(UIntPtr scenePointer, ref Vec3 p_streak_tint_color);

		// Token: 0x02000411 RID: 1041
		// (Invoke) Token: 0x06001547 RID: 5447
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetSunDelegate(UIntPtr scenePointer, Vec3 color, float altitude, float angle, float intensity);

		// Token: 0x02000412 RID: 1042
		// (Invoke) Token: 0x0600154B RID: 5451
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetSunAngleAltitudeDelegate(UIntPtr scenePointer, float angle, float altitude);

		// Token: 0x02000413 RID: 1043
		// (Invoke) Token: 0x0600154F RID: 5455
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetSunDirectionDelegate(UIntPtr scenePointer, Vec3 direction);

		// Token: 0x02000414 RID: 1044
		// (Invoke) Token: 0x06001553 RID: 5459
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetSunLightDelegate(UIntPtr scenePointer, Vec3 color, Vec3 direction);

		// Token: 0x02000415 RID: 1045
		// (Invoke) Token: 0x06001557 RID: 5463
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetSunshaftModeDelegate(UIntPtr scenePointer, [MarshalAs(UnmanagedType.U1)] bool mode);

		// Token: 0x02000416 RID: 1046
		// (Invoke) Token: 0x0600155B RID: 5467
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetSunShaftStrengthDelegate(UIntPtr scenePointer, float strength);

		// Token: 0x02000417 RID: 1047
		// (Invoke) Token: 0x0600155F RID: 5471
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetSunSizeDelegate(UIntPtr scenePointer, float size);

		// Token: 0x02000418 RID: 1048
		// (Invoke) Token: 0x06001563 RID: 5475
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetTargetExposureDelegate(UIntPtr scenePointer, float targetExposure);

		// Token: 0x02000419 RID: 1049
		// (Invoke) Token: 0x06001567 RID: 5479
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetTemperatureDelegate(UIntPtr scenePointer, float temperature);

		// Token: 0x0200041A RID: 1050
		// (Invoke) Token: 0x0600156B RID: 5483
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetTerrainDynamicParamsDelegate(UIntPtr scenePointer, Vec3 dynamic_params);

		// Token: 0x0200041B RID: 1051
		// (Invoke) Token: 0x0600156F RID: 5487
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetTimeOfDayDelegate(UIntPtr scenePointer, float value);

		// Token: 0x0200041C RID: 1052
		// (Invoke) Token: 0x06001573 RID: 5491
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetTimeSpeedDelegate(UIntPtr scenePointer, float value);

		// Token: 0x0200041D RID: 1053
		// (Invoke) Token: 0x06001577 RID: 5495
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetUpgradeLevelDelegate(UIntPtr scenePointer, int level);

		// Token: 0x0200041E RID: 1054
		// (Invoke) Token: 0x0600157B RID: 5499
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetUpgradeLevelVisibilityDelegate(UIntPtr scenePointer, byte[] concatLevels);

		// Token: 0x0200041F RID: 1055
		// (Invoke) Token: 0x0600157F RID: 5503
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetUpgradeLevelVisibilityWithMaskDelegate(UIntPtr scenePointer, uint mask);

		// Token: 0x02000420 RID: 1056
		// (Invoke) Token: 0x06001583 RID: 5507
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetUseConstantTimeDelegate(UIntPtr ptr, [MarshalAs(UnmanagedType.U1)] bool value);

		// Token: 0x02000421 RID: 1057
		// (Invoke) Token: 0x06001587 RID: 5511
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetVignetteInnerRadiusDelegate(UIntPtr scenePointer, float vignetteInnerRadius);

		// Token: 0x02000422 RID: 1058
		// (Invoke) Token: 0x0600158B RID: 5515
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetVignetteOpacityDelegate(UIntPtr scenePointer, float vignetteOpacity);

		// Token: 0x02000423 RID: 1059
		// (Invoke) Token: 0x0600158F RID: 5519
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetVignetteOuterRadiusDelegate(UIntPtr scenePointer, float vignetteOuterRadius);

		// Token: 0x02000424 RID: 1060
		// (Invoke) Token: 0x06001593 RID: 5523
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetWinterTimeFactorDelegate(UIntPtr scenePointer, float winterTimeFactor);

		// Token: 0x02000425 RID: 1061
		// (Invoke) Token: 0x06001597 RID: 5527
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void StallLoadingRenderingsUntilFurtherNoticeDelegate(UIntPtr scenePointer);

		// Token: 0x02000426 RID: 1062
		// (Invoke) Token: 0x0600159B RID: 5531
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SwapFaceConnectionsWithIdDelegate(UIntPtr scenePointer, int hubFaceGroupID, int toBeSeparatedFaceGroupId, int toBeMergedFaceGroupId);

		// Token: 0x02000427 RID: 1063
		// (Invoke) Token: 0x0600159F RID: 5535
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int TakePhotoModePictureDelegate(UIntPtr scene, [MarshalAs(UnmanagedType.U1)] bool saveAmbientOcclusionPass, [MarshalAs(UnmanagedType.U1)] bool saveObjectIdPass, [MarshalAs(UnmanagedType.U1)] bool saveShadowPass);

		// Token: 0x02000428 RID: 1064
		// (Invoke) Token: 0x060015A3 RID: 5539
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void TickDelegate(UIntPtr scenePointer, float deltaTime);

		// Token: 0x02000429 RID: 1065
		// (Invoke) Token: 0x060015A7 RID: 5543
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void WorldPositionComputeNearestNavMeshDelegate(ref WorldPosition position);

		// Token: 0x0200042A RID: 1066
		// (Invoke) Token: 0x060015AB RID: 5547
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void WorldPositionValidateZDelegate(ref WorldPosition position, int minimumValidityState);
	}
}
