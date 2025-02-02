using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace ManagedCallbacks
{
	// Token: 0x02000011 RID: 17
	internal class ScriptingInterfaceOfIGameEntity : IGameEntity
	{
		// Token: 0x060000FD RID: 253 RVA: 0x0000E0E6 File Offset: 0x0000C2E6
		public void ActivateRagdoll(UIntPtr entityId)
		{
			ScriptingInterfaceOfIGameEntity.call_ActivateRagdollDelegate(entityId);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x0000E0F3 File Offset: 0x0000C2F3
		public void AddAllMeshesOfGameEntity(UIntPtr entityId, UIntPtr copiedEntityId)
		{
			ScriptingInterfaceOfIGameEntity.call_AddAllMeshesOfGameEntityDelegate(entityId, copiedEntityId);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0000E101 File Offset: 0x0000C301
		public void AddChild(UIntPtr parententity, UIntPtr childentity, bool autoLocalizeFrame)
		{
			ScriptingInterfaceOfIGameEntity.call_AddChildDelegate(parententity, childentity, autoLocalizeFrame);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000E110 File Offset: 0x0000C310
		public void AddComponent(UIntPtr pointer, UIntPtr componentPointer)
		{
			ScriptingInterfaceOfIGameEntity.call_AddComponentDelegate(pointer, componentPointer);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000E11E File Offset: 0x0000C31E
		public void AddDistanceJoint(UIntPtr entityId, UIntPtr otherEntityId, float minDistance, float maxDistance)
		{
			ScriptingInterfaceOfIGameEntity.call_AddDistanceJointDelegate(entityId, otherEntityId, minDistance, maxDistance);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000E12F File Offset: 0x0000C32F
		public void AddEditDataUserToAllMeshes(UIntPtr entityId, bool entity_components, bool skeleton_components)
		{
			ScriptingInterfaceOfIGameEntity.call_AddEditDataUserToAllMeshesDelegate(entityId, entity_components, skeleton_components);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0000E13E File Offset: 0x0000C33E
		public bool AddLight(UIntPtr entityId, UIntPtr lightPointer)
		{
			return ScriptingInterfaceOfIGameEntity.call_AddLightDelegate(entityId, lightPointer);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x0000E14C File Offset: 0x0000C34C
		public void AddMesh(UIntPtr entityId, UIntPtr mesh, bool recomputeBoundingBox)
		{
			ScriptingInterfaceOfIGameEntity.call_AddMeshDelegate(entityId, mesh, recomputeBoundingBox);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x0000E15B File Offset: 0x0000C35B
		public void AddMeshToBone(UIntPtr entityId, UIntPtr multiMeshPointer, sbyte boneIndex)
		{
			ScriptingInterfaceOfIGameEntity.call_AddMeshToBoneDelegate(entityId, multiMeshPointer, boneIndex);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000E16A File Offset: 0x0000C36A
		public void AddMultiMesh(UIntPtr entityId, UIntPtr multiMeshPtr, bool updateVisMask)
		{
			ScriptingInterfaceOfIGameEntity.call_AddMultiMeshDelegate(entityId, multiMeshPtr, updateVisMask);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0000E179 File Offset: 0x0000C379
		public void AddMultiMeshToSkeleton(UIntPtr gameEntity, UIntPtr multiMesh)
		{
			ScriptingInterfaceOfIGameEntity.call_AddMultiMeshToSkeletonDelegate(gameEntity, multiMesh);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x0000E187 File Offset: 0x0000C387
		public void AddMultiMeshToSkeletonBone(UIntPtr gameEntity, UIntPtr multiMesh, sbyte boneIndex)
		{
			ScriptingInterfaceOfIGameEntity.call_AddMultiMeshToSkeletonBoneDelegate(gameEntity, multiMesh, boneIndex);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000E198 File Offset: 0x0000C398
		public void AddParticleSystemComponent(UIntPtr entityId, string particleid)
		{
			byte[] array = null;
			if (particleid != null)
			{
				int byteCount = ScriptingInterfaceOfIGameEntity._utf8.GetByteCount(particleid);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIGameEntity._utf8.GetBytes(particleid, 0, particleid.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIGameEntity.call_AddParticleSystemComponentDelegate(entityId, array);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0000E1F4 File Offset: 0x0000C3F4
		public void AddPhysics(UIntPtr entityId, UIntPtr body, float mass, ref Vec3 localCenterOfMass, ref Vec3 initialVelocity, ref Vec3 initialAngularVelocity, int physicsMaterial, bool isStatic, int collisionGroupID)
		{
			ScriptingInterfaceOfIGameEntity.call_AddPhysicsDelegate(entityId, body, mass, ref localCenterOfMass, ref initialVelocity, ref initialAngularVelocity, physicsMaterial, isStatic, collisionGroupID);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000E21A File Offset: 0x0000C41A
		public void AddSphereAsBody(UIntPtr entityId, Vec3 center, float radius, uint bodyFlags)
		{
			ScriptingInterfaceOfIGameEntity.call_AddSphereAsBodyDelegate(entityId, center, radius, bodyFlags);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000E22C File Offset: 0x0000C42C
		public void AddTag(UIntPtr entityId, string tag)
		{
			byte[] array = null;
			if (tag != null)
			{
				int byteCount = ScriptingInterfaceOfIGameEntity._utf8.GetByteCount(tag);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIGameEntity._utf8.GetBytes(tag, 0, tag.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIGameEntity.call_AddTagDelegate(entityId, array);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000E287 File Offset: 0x0000C487
		public void ApplyAccelerationToDynamicBody(UIntPtr entityId, ref Vec3 acceleration)
		{
			ScriptingInterfaceOfIGameEntity.call_ApplyAccelerationToDynamicBodyDelegate(entityId, ref acceleration);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000E295 File Offset: 0x0000C495
		public void ApplyForceToDynamicBody(UIntPtr entityId, ref Vec3 force)
		{
			ScriptingInterfaceOfIGameEntity.call_ApplyForceToDynamicBodyDelegate(entityId, ref force);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000E2A3 File Offset: 0x0000C4A3
		public void ApplyLocalForceToDynamicBody(UIntPtr entityId, ref Vec3 localPosition, ref Vec3 force)
		{
			ScriptingInterfaceOfIGameEntity.call_ApplyLocalForceToDynamicBodyDelegate(entityId, ref localPosition, ref force);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000E2B2 File Offset: 0x0000C4B2
		public void ApplyLocalImpulseToDynamicBody(UIntPtr entityId, ref Vec3 localPosition, ref Vec3 impulse)
		{
			ScriptingInterfaceOfIGameEntity.call_ApplyLocalImpulseToDynamicBodyDelegate(entityId, ref localPosition, ref impulse);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000E2C1 File Offset: 0x0000C4C1
		public void AttachNavigationMeshFaces(UIntPtr entityId, int faceGroupId, bool isConnected, bool isBlocker, bool autoLocalize)
		{
			ScriptingInterfaceOfIGameEntity.call_AttachNavigationMeshFacesDelegate(entityId, faceGroupId, isConnected, isBlocker, autoLocalize);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000E2D4 File Offset: 0x0000C4D4
		public void BreakPrefab(UIntPtr entityId)
		{
			ScriptingInterfaceOfIGameEntity.call_BreakPrefabDelegate(entityId);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000E2E1 File Offset: 0x0000C4E1
		public void BurstEntityParticle(UIntPtr entityId, bool doChildren)
		{
			ScriptingInterfaceOfIGameEntity.call_BurstEntityParticleDelegate(entityId, doChildren);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000E2EF File Offset: 0x0000C4EF
		public void CallScriptCallbacks(UIntPtr entityPointer)
		{
			ScriptingInterfaceOfIGameEntity.call_CallScriptCallbacksDelegate(entityPointer);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000E2FC File Offset: 0x0000C4FC
		public void ChangeMetaMeshOrRemoveItIfNotExists(UIntPtr entityId, UIntPtr entityMetaMeshPointer, UIntPtr newMetaMeshPointer)
		{
			ScriptingInterfaceOfIGameEntity.call_ChangeMetaMeshOrRemoveItIfNotExistsDelegate(entityId, entityMetaMeshPointer, newMetaMeshPointer);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000E30B File Offset: 0x0000C50B
		public bool CheckPointWithOrientedBoundingBox(UIntPtr entityId, Vec3 point)
		{
			return ScriptingInterfaceOfIGameEntity.call_CheckPointWithOrientedBoundingBoxDelegate(entityId, point);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000E319 File Offset: 0x0000C519
		public bool CheckResources(UIntPtr entityId, bool addToQueue, bool checkFaceResources)
		{
			return ScriptingInterfaceOfIGameEntity.call_CheckResourcesDelegate(entityId, addToQueue, checkFaceResources);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x0000E328 File Offset: 0x0000C528
		public void ClearComponents(UIntPtr entityId)
		{
			ScriptingInterfaceOfIGameEntity.call_ClearComponentsDelegate(entityId);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x0000E335 File Offset: 0x0000C535
		public void ClearEntityComponents(UIntPtr entityId, bool resetAll, bool removeScripts, bool deleteChildEntities)
		{
			ScriptingInterfaceOfIGameEntity.call_ClearEntityComponentsDelegate(entityId, resetAll, removeScripts, deleteChildEntities);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000E346 File Offset: 0x0000C546
		public void ClearOnlyOwnComponents(UIntPtr entityId)
		{
			ScriptingInterfaceOfIGameEntity.call_ClearOnlyOwnComponentsDelegate(entityId);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0000E353 File Offset: 0x0000C553
		public void ComputeTrajectoryVolume(UIntPtr gameEntity, float missileSpeed, float verticalAngleMaxInDegrees, float verticalAngleMinInDegrees, float horizontalAngleRangeInDegrees, float airFrictionConstant)
		{
			ScriptingInterfaceOfIGameEntity.call_ComputeTrajectoryVolumeDelegate(gameEntity, missileSpeed, verticalAngleMaxInDegrees, verticalAngleMinInDegrees, horizontalAngleRangeInDegrees, airFrictionConstant);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000E368 File Offset: 0x0000C568
		public void CopyComponentsToSkeleton(UIntPtr entityId)
		{
			ScriptingInterfaceOfIGameEntity.call_CopyComponentsToSkeletonDelegate(entityId);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000E378 File Offset: 0x0000C578
		public GameEntity CopyFromPrefab(UIntPtr prefab)
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIGameEntity.call_CopyFromPrefabDelegate(prefab);
			GameEntity result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new GameEntity(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000E3C4 File Offset: 0x0000C5C4
		public void CopyScriptComponentFromAnotherEntity(UIntPtr prefab, UIntPtr other_prefab, string script_name)
		{
			byte[] array = null;
			if (script_name != null)
			{
				int byteCount = ScriptingInterfaceOfIGameEntity._utf8.GetByteCount(script_name);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIGameEntity._utf8.GetBytes(script_name, 0, script_name.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIGameEntity.call_CopyScriptComponentFromAnotherEntityDelegate(prefab, other_prefab, array);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000E420 File Offset: 0x0000C620
		public void CreateAndAddScriptComponent(UIntPtr entityId, string name)
		{
			byte[] array = null;
			if (name != null)
			{
				int byteCount = ScriptingInterfaceOfIGameEntity._utf8.GetByteCount(name);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIGameEntity._utf8.GetBytes(name, 0, name.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIGameEntity.call_CreateAndAddScriptComponentDelegate(entityId, array);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000E47C File Offset: 0x0000C67C
		public GameEntity CreateEmpty(UIntPtr scenePointer, bool isModifiableFromEditor, UIntPtr entityId)
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIGameEntity.call_CreateEmptyDelegate(scenePointer, isModifiableFromEditor, entityId);
			GameEntity result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new GameEntity(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000E4C8 File Offset: 0x0000C6C8
		public GameEntity CreateEmptyWithoutScene()
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIGameEntity.call_CreateEmptyWithoutSceneDelegate();
			GameEntity result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new GameEntity(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000E514 File Offset: 0x0000C714
		public GameEntity CreateFromPrefab(UIntPtr scenePointer, string prefabid, bool callScriptCallbacks)
		{
			byte[] array = null;
			if (prefabid != null)
			{
				int byteCount = ScriptingInterfaceOfIGameEntity._utf8.GetByteCount(prefabid);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIGameEntity._utf8.GetBytes(prefabid, 0, prefabid.Length, array, 0);
				array[byteCount] = 0;
			}
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIGameEntity.call_CreateFromPrefabDelegate(scenePointer, array, callScriptCallbacks);
			GameEntity result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new GameEntity(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0000E5A4 File Offset: 0x0000C7A4
		public GameEntity CreateFromPrefabWithInitialFrame(UIntPtr scenePointer, string prefabid, ref MatrixFrame frame)
		{
			byte[] array = null;
			if (prefabid != null)
			{
				int byteCount = ScriptingInterfaceOfIGameEntity._utf8.GetByteCount(prefabid);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIGameEntity._utf8.GetBytes(prefabid, 0, prefabid.Length, array, 0);
				array[byteCount] = 0;
			}
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIGameEntity.call_CreateFromPrefabWithInitialFrameDelegate(scenePointer, array, ref frame);
			GameEntity result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new GameEntity(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000E632 File Offset: 0x0000C832
		public void DeselectEntityOnEditor(UIntPtr entityId)
		{
			ScriptingInterfaceOfIGameEntity.call_DeselectEntityOnEditorDelegate(entityId);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000E63F File Offset: 0x0000C83F
		public void DisableContour(UIntPtr entityId)
		{
			ScriptingInterfaceOfIGameEntity.call_DisableContourDelegate(entityId);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000E64C File Offset: 0x0000C84C
		public void DisableDynamicBodySimulation(UIntPtr entityId)
		{
			ScriptingInterfaceOfIGameEntity.call_DisableDynamicBodySimulationDelegate(entityId);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0000E659 File Offset: 0x0000C859
		public void DisableGravity(UIntPtr entityId)
		{
			ScriptingInterfaceOfIGameEntity.call_DisableGravityDelegate(entityId);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000E666 File Offset: 0x0000C866
		public void EnableDynamicBody(UIntPtr entityId)
		{
			ScriptingInterfaceOfIGameEntity.call_EnableDynamicBodyDelegate(entityId);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000E674 File Offset: 0x0000C874
		public GameEntity FindWithName(UIntPtr scenePointer, string name)
		{
			byte[] array = null;
			if (name != null)
			{
				int byteCount = ScriptingInterfaceOfIGameEntity._utf8.GetByteCount(name);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIGameEntity._utf8.GetBytes(name, 0, name.Length, array, 0);
				array[byteCount] = 0;
			}
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIGameEntity.call_FindWithNameDelegate(scenePointer, array);
			GameEntity result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new GameEntity(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000E701 File Offset: 0x0000C901
		public void Freeze(UIntPtr entityId, bool isFrozen)
		{
			ScriptingInterfaceOfIGameEntity.call_FreezeDelegate(entityId, isFrozen);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000E70F File Offset: 0x0000C90F
		public uint GetBodyFlags(UIntPtr entityId)
		{
			return ScriptingInterfaceOfIGameEntity.call_GetBodyFlagsDelegate(entityId);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000E71C File Offset: 0x0000C91C
		public PhysicsShape GetBodyShape(GameEntity entity)
		{
			UIntPtr entity2 = (entity != null) ? entity.Pointer : UIntPtr.Zero;
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIGameEntity.call_GetBodyShapeDelegate(entity2);
			PhysicsShape result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new PhysicsShape(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000E77D File Offset: 0x0000C97D
		public sbyte GetBoneCount(UIntPtr entityId)
		{
			return ScriptingInterfaceOfIGameEntity.call_GetBoneCountDelegate(entityId);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000E78A File Offset: 0x0000C98A
		public void GetBoneEntitialFrameWithIndex(UIntPtr entityId, sbyte boneIndex, ref MatrixFrame outEntitialFrame)
		{
			ScriptingInterfaceOfIGameEntity.call_GetBoneEntitialFrameWithIndexDelegate(entityId, boneIndex, ref outEntitialFrame);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000E79C File Offset: 0x0000C99C
		public void GetBoneEntitialFrameWithName(UIntPtr entityId, string boneName, ref MatrixFrame outEntitialFrame)
		{
			byte[] array = null;
			if (boneName != null)
			{
				int byteCount = ScriptingInterfaceOfIGameEntity._utf8.GetByteCount(boneName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIGameEntity._utf8.GetBytes(boneName, 0, boneName.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIGameEntity.call_GetBoneEntitialFrameWithNameDelegate(entityId, array, ref outEntitialFrame);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000E7F8 File Offset: 0x0000C9F8
		public Vec3 GetBoundingBoxMax(UIntPtr entityId)
		{
			return ScriptingInterfaceOfIGameEntity.call_GetBoundingBoxMaxDelegate(entityId);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000E805 File Offset: 0x0000CA05
		public Vec3 GetBoundingBoxMin(UIntPtr entityId)
		{
			return ScriptingInterfaceOfIGameEntity.call_GetBoundingBoxMinDelegate(entityId);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000E812 File Offset: 0x0000CA12
		public void GetCameraParamsFromCameraScript(UIntPtr entityId, UIntPtr camPtr, ref Vec3 dof_params)
		{
			ScriptingInterfaceOfIGameEntity.call_GetCameraParamsFromCameraScriptDelegate(entityId, camPtr, ref dof_params);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000E821 File Offset: 0x0000CA21
		public Vec3 GetCenterOfMass(UIntPtr entityId)
		{
			return ScriptingInterfaceOfIGameEntity.call_GetCenterOfMassDelegate(entityId);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000E830 File Offset: 0x0000CA30
		public GameEntity GetChild(UIntPtr entityId, int childIndex)
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIGameEntity.call_GetChildDelegate(entityId, childIndex);
			GameEntity result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new GameEntity(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000E87B File Offset: 0x0000CA7B
		public int GetChildCount(UIntPtr entityId)
		{
			return ScriptingInterfaceOfIGameEntity.call_GetChildCountDelegate(entityId);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000E888 File Offset: 0x0000CA88
		public GameEntityComponent GetComponentAtIndex(UIntPtr entityId, GameEntity.ComponentType componentType, int index)
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIGameEntity.call_GetComponentAtIndexDelegate(entityId, componentType, index);
			GameEntityComponent result = NativeObject.CreateNativeObjectWrapper<GameEntityComponent>(nativeObjectPointer);
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000E8CB File Offset: 0x0000CACB
		public int GetComponentCount(UIntPtr entityId, GameEntity.ComponentType componentType)
		{
			return ScriptingInterfaceOfIGameEntity.call_GetComponentCountDelegate(entityId, componentType);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000E8D9 File Offset: 0x0000CAD9
		public bool GetEditModeLevelVisibility(UIntPtr entityId)
		{
			return ScriptingInterfaceOfIGameEntity.call_GetEditModeLevelVisibilityDelegate(entityId);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000E8E6 File Offset: 0x0000CAE6
		public uint GetEntityFlags(UIntPtr entityId)
		{
			return ScriptingInterfaceOfIGameEntity.call_GetEntityFlagsDelegate(entityId);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000E8F3 File Offset: 0x0000CAF3
		public uint GetEntityVisibilityFlags(UIntPtr entityId)
		{
			return ScriptingInterfaceOfIGameEntity.call_GetEntityVisibilityFlagsDelegate(entityId);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000E900 File Offset: 0x0000CB00
		public uint GetFactorColor(UIntPtr entityId)
		{
			return ScriptingInterfaceOfIGameEntity.call_GetFactorColorDelegate(entityId);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000E910 File Offset: 0x0000CB10
		public GameEntity GetFirstEntityWithTag(UIntPtr scenePointer, string tag)
		{
			byte[] array = null;
			if (tag != null)
			{
				int byteCount = ScriptingInterfaceOfIGameEntity._utf8.GetByteCount(tag);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIGameEntity._utf8.GetBytes(tag, 0, tag.Length, array, 0);
				array[byteCount] = 0;
			}
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIGameEntity.call_GetFirstEntityWithTagDelegate(scenePointer, array);
			GameEntity result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new GameEntity(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000E9A0 File Offset: 0x0000CBA0
		public GameEntity GetFirstEntityWithTagExpression(UIntPtr scenePointer, string tagExpression)
		{
			byte[] array = null;
			if (tagExpression != null)
			{
				int byteCount = ScriptingInterfaceOfIGameEntity._utf8.GetByteCount(tagExpression);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIGameEntity._utf8.GetBytes(tagExpression, 0, tagExpression.Length, array, 0);
				array[byteCount] = 0;
			}
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIGameEntity.call_GetFirstEntityWithTagExpressionDelegate(scenePointer, array);
			GameEntity result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new GameEntity(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000EA30 File Offset: 0x0000CC30
		public Mesh GetFirstMesh(UIntPtr entityId)
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIGameEntity.call_GetFirstMeshDelegate(entityId);
			Mesh result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new Mesh(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000EA7A File Offset: 0x0000CC7A
		public void GetFrame(UIntPtr entityId, ref MatrixFrame outFrame)
		{
			ScriptingInterfaceOfIGameEntity.call_GetFrameDelegate(entityId, ref outFrame);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000EA88 File Offset: 0x0000CC88
		public Vec3 GetGlobalBoxMax(UIntPtr entityId)
		{
			return ScriptingInterfaceOfIGameEntity.call_GetGlobalBoxMaxDelegate(entityId);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000EA95 File Offset: 0x0000CC95
		public Vec3 GetGlobalBoxMin(UIntPtr entityId)
		{
			return ScriptingInterfaceOfIGameEntity.call_GetGlobalBoxMinDelegate(entityId);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000EAA2 File Offset: 0x0000CCA2
		public void GetGlobalFrame(UIntPtr meshPointer, out MatrixFrame outFrame)
		{
			ScriptingInterfaceOfIGameEntity.call_GetGlobalFrameDelegate(meshPointer, out outFrame);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000EAB0 File Offset: 0x0000CCB0
		public Vec3 GetGlobalScale(GameEntity entity)
		{
			UIntPtr entity2 = (entity != null) ? entity.Pointer : UIntPtr.Zero;
			return ScriptingInterfaceOfIGameEntity.call_GetGlobalScaleDelegate(entity2);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000EADF File Offset: 0x0000CCDF
		public string GetGuid(UIntPtr entityId)
		{
			if (ScriptingInterfaceOfIGameEntity.call_GetGuidDelegate(entityId) != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000EAF8 File Offset: 0x0000CCF8
		public Light GetLight(UIntPtr entityId)
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIGameEntity.call_GetLightDelegate(entityId);
			Light result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new Light(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000EB42 File Offset: 0x0000CD42
		public Vec3 GetLinearVelocity(UIntPtr entityPtr)
		{
			return ScriptingInterfaceOfIGameEntity.call_GetLinearVelocityDelegate(entityPtr);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000EB4F File Offset: 0x0000CD4F
		public float GetLodLevelForDistanceSq(UIntPtr entityId, float distanceSquared)
		{
			return ScriptingInterfaceOfIGameEntity.call_GetLodLevelForDistanceSqDelegate(entityId, distanceSquared);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000EB5D File Offset: 0x0000CD5D
		public float GetMass(UIntPtr entityId)
		{
			return ScriptingInterfaceOfIGameEntity.call_GetMassDelegate(entityId);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000EB6A File Offset: 0x0000CD6A
		public void GetMeshBendedPosition(UIntPtr entityId, ref MatrixFrame worldSpacePosition, ref MatrixFrame output)
		{
			ScriptingInterfaceOfIGameEntity.call_GetMeshBendedPositionDelegate(entityId, ref worldSpacePosition, ref output);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000EB79 File Offset: 0x0000CD79
		public string GetName(UIntPtr entityId)
		{
			if (ScriptingInterfaceOfIGameEntity.call_GetNameDelegate(entityId) != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000EB90 File Offset: 0x0000CD90
		public GameEntity GetNextEntityWithTag(UIntPtr currententityId, string tag)
		{
			byte[] array = null;
			if (tag != null)
			{
				int byteCount = ScriptingInterfaceOfIGameEntity._utf8.GetByteCount(tag);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIGameEntity._utf8.GetBytes(tag, 0, tag.Length, array, 0);
				array[byteCount] = 0;
			}
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIGameEntity.call_GetNextEntityWithTagDelegate(currententityId, array);
			GameEntity result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new GameEntity(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000EC20 File Offset: 0x0000CE20
		public GameEntity GetNextEntityWithTagExpression(UIntPtr currententityId, string tagExpression)
		{
			byte[] array = null;
			if (tagExpression != null)
			{
				int byteCount = ScriptingInterfaceOfIGameEntity._utf8.GetByteCount(tagExpression);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIGameEntity._utf8.GetBytes(tagExpression, 0, tagExpression.Length, array, 0);
				array[byteCount] = 0;
			}
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIGameEntity.call_GetNextEntityWithTagExpressionDelegate(currententityId, array);
			GameEntity result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new GameEntity(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000ECB0 File Offset: 0x0000CEB0
		public GameEntity GetNextPrefab(UIntPtr currentPrefab)
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIGameEntity.call_GetNextPrefabDelegate(currentPrefab);
			GameEntity result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new GameEntity(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000ECFA File Offset: 0x0000CEFA
		public string GetOldPrefabName(UIntPtr prefab)
		{
			if (ScriptingInterfaceOfIGameEntity.call_GetOldPrefabNameDelegate(prefab) != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000ED14 File Offset: 0x0000CF14
		public GameEntity GetParent(UIntPtr entityId)
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIGameEntity.call_GetParentDelegate(entityId);
			GameEntity result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new GameEntity(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000ED5E File Offset: 0x0000CF5E
		public Vec3 GetPhysicsBoundingBoxMax(UIntPtr entityId)
		{
			return ScriptingInterfaceOfIGameEntity.call_GetPhysicsBoundingBoxMaxDelegate(entityId);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000ED6B File Offset: 0x0000CF6B
		public Vec3 GetPhysicsBoundingBoxMin(UIntPtr entityId)
		{
			return ScriptingInterfaceOfIGameEntity.call_GetPhysicsBoundingBoxMinDelegate(entityId);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000ED78 File Offset: 0x0000CF78
		public uint GetPhysicsDescBodyFlags(UIntPtr entityId)
		{
			return ScriptingInterfaceOfIGameEntity.call_GetPhysicsDescBodyFlagsDelegate(entityId);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000ED85 File Offset: 0x0000CF85
		public void GetPhysicsMinMax(UIntPtr entityId, bool includeChildren, ref Vec3 bbmin, ref Vec3 bbmax, bool returnLocal)
		{
			ScriptingInterfaceOfIGameEntity.call_GetPhysicsMinMaxDelegate(entityId, includeChildren, ref bbmin, ref bbmax, returnLocal);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000ED98 File Offset: 0x0000CF98
		public bool GetPhysicsState(UIntPtr entityId)
		{
			return ScriptingInterfaceOfIGameEntity.call_GetPhysicsStateDelegate(entityId);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000EDA5 File Offset: 0x0000CFA5
		public string GetPrefabName(UIntPtr prefab)
		{
			if (ScriptingInterfaceOfIGameEntity.call_GetPrefabNameDelegate(prefab) != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000EDBC File Offset: 0x0000CFBC
		public void GetPreviousGlobalFrame(UIntPtr entityPtr, out MatrixFrame frame)
		{
			ScriptingInterfaceOfIGameEntity.call_GetPreviousGlobalFrameDelegate(entityPtr, out frame);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000EDCA File Offset: 0x0000CFCA
		public void GetQuickBoneEntitialFrame(UIntPtr entityId, sbyte index, ref MatrixFrame frame)
		{
			ScriptingInterfaceOfIGameEntity.call_GetQuickBoneEntitialFrameDelegate(entityId, index, ref frame);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000EDD9 File Offset: 0x0000CFD9
		public float GetRadius(UIntPtr entityId)
		{
			return ScriptingInterfaceOfIGameEntity.call_GetRadiusDelegate(entityId);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000EDE8 File Offset: 0x0000CFE8
		public Scene GetScene(UIntPtr entityId)
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIGameEntity.call_GetSceneDelegate(entityId);
			Scene result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new Scene(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000EE32 File Offset: 0x0000D032
		public UIntPtr GetScenePointer(UIntPtr entityId)
		{
			return ScriptingInterfaceOfIGameEntity.call_GetScenePointerDelegate(entityId);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000EE3F File Offset: 0x0000D03F
		public ScriptComponentBehavior GetScriptComponent(UIntPtr entityId)
		{
			return DotNetObject.GetManagedObjectWithId(ScriptingInterfaceOfIGameEntity.call_GetScriptComponentDelegate(entityId)) as ScriptComponentBehavior;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000EE56 File Offset: 0x0000D056
		public ScriptComponentBehavior GetScriptComponentAtIndex(UIntPtr entityId, int index)
		{
			return DotNetObject.GetManagedObjectWithId(ScriptingInterfaceOfIGameEntity.call_GetScriptComponentAtIndexDelegate(entityId, index)) as ScriptComponentBehavior;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000EE6E File Offset: 0x0000D06E
		public int GetScriptComponentCount(UIntPtr entityId)
		{
			return ScriptingInterfaceOfIGameEntity.call_GetScriptComponentCountDelegate(entityId);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000EE7C File Offset: 0x0000D07C
		public Skeleton GetSkeleton(UIntPtr entityId)
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIGameEntity.call_GetSkeletonDelegate(entityId);
			Skeleton result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new Skeleton(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000EEC6 File Offset: 0x0000D0C6
		public string GetTags(UIntPtr entityId)
		{
			if (ScriptingInterfaceOfIGameEntity.call_GetTagsDelegate(entityId) != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000EEDD File Offset: 0x0000D0DD
		public uint GetUpgradeLevelMask(UIntPtr prefab)
		{
			return ScriptingInterfaceOfIGameEntity.call_GetUpgradeLevelMaskDelegate(prefab);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000EEEA File Offset: 0x0000D0EA
		public uint GetUpgradeLevelMaskCumulative(UIntPtr prefab)
		{
			return ScriptingInterfaceOfIGameEntity.call_GetUpgradeLevelMaskCumulativeDelegate(prefab);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000EEF7 File Offset: 0x0000D0F7
		public bool GetVisibilityExcludeParents(UIntPtr entityId)
		{
			return ScriptingInterfaceOfIGameEntity.call_GetVisibilityExcludeParentsDelegate(entityId);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000EF04 File Offset: 0x0000D104
		public uint GetVisibilityLevelMaskIncludingParents(UIntPtr entityId)
		{
			return ScriptingInterfaceOfIGameEntity.call_GetVisibilityLevelMaskIncludingParentsDelegate(entityId);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000EF11 File Offset: 0x0000D111
		public bool HasBody(UIntPtr entityId)
		{
			return ScriptingInterfaceOfIGameEntity.call_HasBodyDelegate(entityId);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000EF1E File Offset: 0x0000D11E
		public bool HasComplexAnimTree(UIntPtr entityId)
		{
			return ScriptingInterfaceOfIGameEntity.call_HasComplexAnimTreeDelegate(entityId);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000EF2B File Offset: 0x0000D12B
		public bool HasComponent(UIntPtr pointer, UIntPtr componentPointer)
		{
			return ScriptingInterfaceOfIGameEntity.call_HasComponentDelegate(pointer, componentPointer);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000EF39 File Offset: 0x0000D139
		public bool HasFrameChanged(UIntPtr entityId)
		{
			return ScriptingInterfaceOfIGameEntity.call_HasFrameChangedDelegate(entityId);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000EF46 File Offset: 0x0000D146
		public bool HasPhysicsBody(UIntPtr entityId)
		{
			return ScriptingInterfaceOfIGameEntity.call_HasPhysicsBodyDelegate(entityId);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000EF53 File Offset: 0x0000D153
		public bool HasPhysicsDefinition(UIntPtr entityId, int excludeFlags)
		{
			return ScriptingInterfaceOfIGameEntity.call_HasPhysicsDefinitionDelegate(entityId, excludeFlags);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000EF61 File Offset: 0x0000D161
		public bool HasScene(UIntPtr entityId)
		{
			return ScriptingInterfaceOfIGameEntity.call_HasSceneDelegate(entityId);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000EF70 File Offset: 0x0000D170
		public bool HasScriptComponent(UIntPtr entityId, string scName)
		{
			byte[] array = null;
			if (scName != null)
			{
				int byteCount = ScriptingInterfaceOfIGameEntity._utf8.GetByteCount(scName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIGameEntity._utf8.GetBytes(scName, 0, scName.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfIGameEntity.call_HasScriptComponentDelegate(entityId, array);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000EFCC File Offset: 0x0000D1CC
		public bool HasTag(UIntPtr entityId, string tag)
		{
			byte[] array = null;
			if (tag != null)
			{
				int byteCount = ScriptingInterfaceOfIGameEntity._utf8.GetByteCount(tag);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIGameEntity._utf8.GetBytes(tag, 0, tag.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfIGameEntity.call_HasTagDelegate(entityId, array);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000F027 File Offset: 0x0000D227
		public bool IsDynamicBodyStationary(UIntPtr entityId)
		{
			return ScriptingInterfaceOfIGameEntity.call_IsDynamicBodyStationaryDelegate(entityId);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000F034 File Offset: 0x0000D234
		public bool IsEngineBodySleeping(UIntPtr entityId)
		{
			return ScriptingInterfaceOfIGameEntity.call_IsEngineBodySleepingDelegate(entityId);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000F041 File Offset: 0x0000D241
		public bool IsEntitySelectedOnEditor(UIntPtr entityId)
		{
			return ScriptingInterfaceOfIGameEntity.call_IsEntitySelectedOnEditorDelegate(entityId);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000F04E File Offset: 0x0000D24E
		public bool IsFrozen(UIntPtr entityId)
		{
			return ScriptingInterfaceOfIGameEntity.call_IsFrozenDelegate(entityId);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000F05B File Offset: 0x0000D25B
		public bool IsGhostObject(UIntPtr entityId)
		{
			return ScriptingInterfaceOfIGameEntity.call_IsGhostObjectDelegate(entityId);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000F068 File Offset: 0x0000D268
		public bool IsGuidValid(UIntPtr entityId)
		{
			return ScriptingInterfaceOfIGameEntity.call_IsGuidValidDelegate(entityId);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000F075 File Offset: 0x0000D275
		public bool IsVisibleIncludeParents(UIntPtr entityId)
		{
			return ScriptingInterfaceOfIGameEntity.call_IsVisibleIncludeParentsDelegate(entityId);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000F082 File Offset: 0x0000D282
		public void PauseParticleSystem(UIntPtr entityId, bool doChildren)
		{
			ScriptingInterfaceOfIGameEntity.call_PauseParticleSystemDelegate(entityId, doChildren);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000F090 File Offset: 0x0000D290
		public bool PrefabExists(string prefabName)
		{
			byte[] array = null;
			if (prefabName != null)
			{
				int byteCount = ScriptingInterfaceOfIGameEntity._utf8.GetByteCount(prefabName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIGameEntity._utf8.GetBytes(prefabName, 0, prefabName.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfIGameEntity.call_PrefabExistsDelegate(array);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000F0EC File Offset: 0x0000D2EC
		public void RecomputeBoundingBox(GameEntity entity)
		{
			UIntPtr entity2 = (entity != null) ? entity.Pointer : UIntPtr.Zero;
			ScriptingInterfaceOfIGameEntity.call_RecomputeBoundingBoxDelegate(entity2);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000F11B File Offset: 0x0000D31B
		public void ReleaseEditDataUserToAllMeshes(UIntPtr entityId, bool entity_components, bool skeleton_components)
		{
			ScriptingInterfaceOfIGameEntity.call_ReleaseEditDataUserToAllMeshesDelegate(entityId, entity_components, skeleton_components);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000F12A File Offset: 0x0000D32A
		public void Remove(UIntPtr entityId, int removeReason)
		{
			ScriptingInterfaceOfIGameEntity.call_RemoveDelegate(entityId, removeReason);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000F138 File Offset: 0x0000D338
		public void RemoveAllChildren(UIntPtr entityId)
		{
			ScriptingInterfaceOfIGameEntity.call_RemoveAllChildrenDelegate(entityId);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000F145 File Offset: 0x0000D345
		public void RemoveAllParticleSystems(UIntPtr entityId)
		{
			ScriptingInterfaceOfIGameEntity.call_RemoveAllParticleSystemsDelegate(entityId);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000F152 File Offset: 0x0000D352
		public void RemoveChild(UIntPtr parentEntity, UIntPtr childEntity, bool keepPhysics, bool keepScenePointer, bool callScriptCallbacks, int removeReason)
		{
			ScriptingInterfaceOfIGameEntity.call_RemoveChildDelegate(parentEntity, childEntity, keepPhysics, keepScenePointer, callScriptCallbacks, removeReason);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000F167 File Offset: 0x0000D367
		public bool RemoveComponent(UIntPtr pointer, UIntPtr componentPointer)
		{
			return ScriptingInterfaceOfIGameEntity.call_RemoveComponentDelegate(pointer, componentPointer);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000F175 File Offset: 0x0000D375
		public bool RemoveComponentWithMesh(UIntPtr entityId, UIntPtr mesh)
		{
			return ScriptingInterfaceOfIGameEntity.call_RemoveComponentWithMeshDelegate(entityId, mesh);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000F183 File Offset: 0x0000D383
		public void RemoveEnginePhysics(UIntPtr entityId)
		{
			ScriptingInterfaceOfIGameEntity.call_RemoveEnginePhysicsDelegate(entityId);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000F190 File Offset: 0x0000D390
		public void RemoveFromPredisplayEntity(UIntPtr entityId)
		{
			ScriptingInterfaceOfIGameEntity.call_RemoveFromPredisplayEntityDelegate(entityId);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000F19D File Offset: 0x0000D39D
		public bool RemoveMultiMesh(UIntPtr entityId, UIntPtr multiMeshPtr)
		{
			return ScriptingInterfaceOfIGameEntity.call_RemoveMultiMeshDelegate(entityId, multiMeshPtr);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000F1AB File Offset: 0x0000D3AB
		public void RemoveMultiMeshFromSkeleton(UIntPtr gameEntity, UIntPtr multiMesh)
		{
			ScriptingInterfaceOfIGameEntity.call_RemoveMultiMeshFromSkeletonDelegate(gameEntity, multiMesh);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000F1B9 File Offset: 0x0000D3B9
		public void RemoveMultiMeshFromSkeletonBone(UIntPtr gameEntity, UIntPtr multiMesh, sbyte boneIndex)
		{
			ScriptingInterfaceOfIGameEntity.call_RemoveMultiMeshFromSkeletonBoneDelegate(gameEntity, multiMesh, boneIndex);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000F1C8 File Offset: 0x0000D3C8
		public void RemovePhysics(UIntPtr entityId, bool clearingTheScene)
		{
			ScriptingInterfaceOfIGameEntity.call_RemovePhysicsDelegate(entityId, clearingTheScene);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000F1D6 File Offset: 0x0000D3D6
		public void RemoveScriptComponent(UIntPtr entityId, UIntPtr scriptComponentPtr, int removeReason)
		{
			ScriptingInterfaceOfIGameEntity.call_RemoveScriptComponentDelegate(entityId, scriptComponentPtr, removeReason);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000F1E8 File Offset: 0x0000D3E8
		public void RemoveTag(UIntPtr entityId, string tag)
		{
			byte[] array = null;
			if (tag != null)
			{
				int byteCount = ScriptingInterfaceOfIGameEntity._utf8.GetByteCount(tag);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIGameEntity._utf8.GetBytes(tag, 0, tag.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIGameEntity.call_RemoveTagDelegate(entityId, array);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000F243 File Offset: 0x0000D443
		public void ResumeParticleSystem(UIntPtr entityId, bool doChildren)
		{
			ScriptingInterfaceOfIGameEntity.call_ResumeParticleSystemDelegate(entityId, doChildren);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000F251 File Offset: 0x0000D451
		public void SelectEntityOnEditor(UIntPtr entityId)
		{
			ScriptingInterfaceOfIGameEntity.call_SelectEntityOnEditorDelegate(entityId);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000F25E File Offset: 0x0000D45E
		public void SetAlpha(UIntPtr entityId, float alpha)
		{
			ScriptingInterfaceOfIGameEntity.call_SetAlphaDelegate(entityId, alpha);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000F26C File Offset: 0x0000D46C
		public void SetAnimationSoundActivation(UIntPtr entityId, bool activate)
		{
			ScriptingInterfaceOfIGameEntity.call_SetAnimationSoundActivationDelegate(entityId, activate);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000F27A File Offset: 0x0000D47A
		public void SetAnimTreeChannelParameter(UIntPtr entityId, float phase, int channel_no)
		{
			ScriptingInterfaceOfIGameEntity.call_SetAnimTreeChannelParameterDelegate(entityId, phase, channel_no);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000F289 File Offset: 0x0000D489
		public void SetAsContourEntity(UIntPtr entityId, uint color)
		{
			ScriptingInterfaceOfIGameEntity.call_SetAsContourEntityDelegate(entityId, color);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000F297 File Offset: 0x0000D497
		public void SetAsPredisplayEntity(UIntPtr entityId)
		{
			ScriptingInterfaceOfIGameEntity.call_SetAsPredisplayEntityDelegate(entityId);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000F2A4 File Offset: 0x0000D4A4
		public void SetAsReplayEntity(UIntPtr gameEntity)
		{
			ScriptingInterfaceOfIGameEntity.call_SetAsReplayEntityDelegate(gameEntity);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000F2B1 File Offset: 0x0000D4B1
		public void SetBodyFlags(UIntPtr entityId, uint bodyFlags)
		{
			ScriptingInterfaceOfIGameEntity.call_SetBodyFlagsDelegate(entityId, bodyFlags);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000F2BF File Offset: 0x0000D4BF
		public void SetBodyFlagsRecursive(UIntPtr entityId, uint bodyFlags)
		{
			ScriptingInterfaceOfIGameEntity.call_SetBodyFlagsRecursiveDelegate(entityId, bodyFlags);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000F2CD File Offset: 0x0000D4CD
		public void SetBodyShape(UIntPtr entityId, UIntPtr shape)
		{
			ScriptingInterfaceOfIGameEntity.call_SetBodyShapeDelegate(entityId, shape);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000F2DB File Offset: 0x0000D4DB
		public void SetBoundingboxDirty(UIntPtr entityId)
		{
			ScriptingInterfaceOfIGameEntity.call_SetBoundingboxDirtyDelegate(entityId);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000F2E8 File Offset: 0x0000D4E8
		public void SetClothComponentKeepState(UIntPtr entityId, UIntPtr metaMesh, bool keepState)
		{
			ScriptingInterfaceOfIGameEntity.call_SetClothComponentKeepStateDelegate(entityId, metaMesh, keepState);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000F2F7 File Offset: 0x0000D4F7
		public void SetClothComponentKeepStateOfAllMeshes(UIntPtr entityId, bool keepState)
		{
			ScriptingInterfaceOfIGameEntity.call_SetClothComponentKeepStateOfAllMeshesDelegate(entityId, keepState);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000F305 File Offset: 0x0000D505
		public void SetClothMaxDistanceMultiplier(UIntPtr gameEntity, float multiplier)
		{
			ScriptingInterfaceOfIGameEntity.call_SetClothMaxDistanceMultiplierDelegate(gameEntity, multiplier);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000F313 File Offset: 0x0000D513
		public void SetContourState(UIntPtr entityId, bool alwaysVisible)
		{
			ScriptingInterfaceOfIGameEntity.call_SetContourStateDelegate(entityId, alwaysVisible);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000F321 File Offset: 0x0000D521
		public void SetCullMode(UIntPtr entityPtr, MBMeshCullingMode cullMode)
		{
			ScriptingInterfaceOfIGameEntity.call_SetCullModeDelegate(entityPtr, cullMode);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000F32F File Offset: 0x0000D52F
		public void SetDamping(UIntPtr entityId, float linearDamping, float angularDamping)
		{
			ScriptingInterfaceOfIGameEntity.call_SetDampingDelegate(entityId, linearDamping, angularDamping);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000F33E File Offset: 0x0000D53E
		public void SetEnforcedMaximumLodLevel(UIntPtr entityId, int lodLevel)
		{
			ScriptingInterfaceOfIGameEntity.call_SetEnforcedMaximumLodLevelDelegate(entityId, lodLevel);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000F34C File Offset: 0x0000D54C
		public void SetEntityEnvMapVisibility(UIntPtr entityId, bool value)
		{
			ScriptingInterfaceOfIGameEntity.call_SetEntityEnvMapVisibilityDelegate(entityId, value);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000F35A File Offset: 0x0000D55A
		public void SetEntityFlags(UIntPtr entityId, uint entityFlags)
		{
			ScriptingInterfaceOfIGameEntity.call_SetEntityFlagsDelegate(entityId, entityFlags);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000F368 File Offset: 0x0000D568
		public void SetEntityVisibilityFlags(UIntPtr entityId, uint entityVisibilityFlags)
		{
			ScriptingInterfaceOfIGameEntity.call_SetEntityVisibilityFlagsDelegate(entityId, entityVisibilityFlags);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000F376 File Offset: 0x0000D576
		public void SetExternalReferencesUsage(UIntPtr entityId, bool value)
		{
			ScriptingInterfaceOfIGameEntity.call_SetExternalReferencesUsageDelegate(entityId, value);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000F384 File Offset: 0x0000D584
		public void SetFactor2Color(UIntPtr entityId, uint factor2Color)
		{
			ScriptingInterfaceOfIGameEntity.call_SetFactor2ColorDelegate(entityId, factor2Color);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000F392 File Offset: 0x0000D592
		public void SetFactorColor(UIntPtr entityId, uint factorColor)
		{
			ScriptingInterfaceOfIGameEntity.call_SetFactorColorDelegate(entityId, factorColor);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000F3A0 File Offset: 0x0000D5A0
		public void SetFrame(UIntPtr entityId, ref MatrixFrame frame)
		{
			ScriptingInterfaceOfIGameEntity.call_SetFrameDelegate(entityId, ref frame);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000F3AE File Offset: 0x0000D5AE
		public void SetFrameChanged(UIntPtr entityId)
		{
			ScriptingInterfaceOfIGameEntity.call_SetFrameChangedDelegate(entityId);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000F3BB File Offset: 0x0000D5BB
		public void SetGlobalFrame(UIntPtr entityId, in MatrixFrame frame)
		{
			ScriptingInterfaceOfIGameEntity.call_SetGlobalFrameDelegate(entityId, frame);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000F3C9 File Offset: 0x0000D5C9
		public void SetLocalPosition(UIntPtr entityId, Vec3 position)
		{
			ScriptingInterfaceOfIGameEntity.call_SetLocalPositionDelegate(entityId, position);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000F3D7 File Offset: 0x0000D5D7
		public void SetMass(UIntPtr entityId, float mass)
		{
			ScriptingInterfaceOfIGameEntity.call_SetMassDelegate(entityId, mass);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000F3E5 File Offset: 0x0000D5E5
		public void SetMassSpaceInertia(UIntPtr entityId, ref Vec3 inertia)
		{
			ScriptingInterfaceOfIGameEntity.call_SetMassSpaceInertiaDelegate(entityId, ref inertia);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000F3F3 File Offset: 0x0000D5F3
		public void SetMaterialForAllMeshes(UIntPtr entityId, UIntPtr materialPointer)
		{
			ScriptingInterfaceOfIGameEntity.call_SetMaterialForAllMeshesDelegate(entityId, materialPointer);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000F401 File Offset: 0x0000D601
		public void SetMobility(UIntPtr entityId, int mobility)
		{
			ScriptingInterfaceOfIGameEntity.call_SetMobilityDelegate(entityId, mobility);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000F40F File Offset: 0x0000D60F
		public void SetMorphFrameOfComponents(UIntPtr entityId, float value)
		{
			ScriptingInterfaceOfIGameEntity.call_SetMorphFrameOfComponentsDelegate(entityId, value);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000F420 File Offset: 0x0000D620
		public void SetName(UIntPtr entityId, string name)
		{
			byte[] array = null;
			if (name != null)
			{
				int byteCount = ScriptingInterfaceOfIGameEntity._utf8.GetByteCount(name);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIGameEntity._utf8.GetBytes(name, 0, name.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIGameEntity.call_SetNameDelegate(entityId, array);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000F47B File Offset: 0x0000D67B
		public void SetPhysicsState(UIntPtr entityId, bool isEnabled, bool setChildren)
		{
			ScriptingInterfaceOfIGameEntity.call_SetPhysicsStateDelegate(entityId, isEnabled, setChildren);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000F48A File Offset: 0x0000D68A
		public void SetPreviousFrameInvalid(UIntPtr gameEntity)
		{
			ScriptingInterfaceOfIGameEntity.call_SetPreviousFrameInvalidDelegate(gameEntity);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000F497 File Offset: 0x0000D697
		public void SetReadyToRender(UIntPtr entityId, bool ready)
		{
			ScriptingInterfaceOfIGameEntity.call_SetReadyToRenderDelegate(entityId, ready);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000F4A5 File Offset: 0x0000D6A5
		public void SetRuntimeEmissionRateMultiplier(UIntPtr entityId, float emission_rate_multiplier)
		{
			ScriptingInterfaceOfIGameEntity.call_SetRuntimeEmissionRateMultiplierDelegate(entityId, emission_rate_multiplier);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000F4B3 File Offset: 0x0000D6B3
		public void SetSkeleton(UIntPtr entityId, UIntPtr skeletonPointer)
		{
			ScriptingInterfaceOfIGameEntity.call_SetSkeletonDelegate(entityId, skeletonPointer);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000F4C1 File Offset: 0x0000D6C1
		public void SetUpgradeLevelMask(UIntPtr prefab, uint mask)
		{
			ScriptingInterfaceOfIGameEntity.call_SetUpgradeLevelMaskDelegate(prefab, mask);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000F4CF File Offset: 0x0000D6CF
		public void SetVectorArgument(UIntPtr entityId, float vectorArgument0, float vectorArgument1, float vectorArgument2, float vectorArgument3)
		{
			ScriptingInterfaceOfIGameEntity.call_SetVectorArgumentDelegate(entityId, vectorArgument0, vectorArgument1, vectorArgument2, vectorArgument3);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000F4E2 File Offset: 0x0000D6E2
		public void SetVisibilityExcludeParents(UIntPtr entityId, bool visibility)
		{
			ScriptingInterfaceOfIGameEntity.call_SetVisibilityExcludeParentsDelegate(entityId, visibility);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000F4F0 File Offset: 0x0000D6F0
		public void UpdateGlobalBounds(UIntPtr entityPointer)
		{
			ScriptingInterfaceOfIGameEntity.call_UpdateGlobalBoundsDelegate(entityPointer);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000F4FD File Offset: 0x0000D6FD
		public void UpdateTriadFrameForEditor(UIntPtr meshPointer)
		{
			ScriptingInterfaceOfIGameEntity.call_UpdateTriadFrameForEditorDelegate(meshPointer);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000F50A File Offset: 0x0000D70A
		public void UpdateVisibilityMask(UIntPtr entityPtr)
		{
			ScriptingInterfaceOfIGameEntity.call_UpdateVisibilityMaskDelegate(entityPtr);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000F517 File Offset: 0x0000D717
		public void ValidateBoundingBox(UIntPtr entityPointer)
		{
			ScriptingInterfaceOfIGameEntity.call_ValidateBoundingBoxDelegate(entityPointer);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000F538 File Offset: 0x0000D738
		void IGameEntity.SetGlobalFrame(UIntPtr entityId, in MatrixFrame frame)
		{
			this.SetGlobalFrame(entityId, frame);
		}

		// Token: 0x040000A2 RID: 162
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x040000A3 RID: 163
		public static ScriptingInterfaceOfIGameEntity.ActivateRagdollDelegate call_ActivateRagdollDelegate;

		// Token: 0x040000A4 RID: 164
		public static ScriptingInterfaceOfIGameEntity.AddAllMeshesOfGameEntityDelegate call_AddAllMeshesOfGameEntityDelegate;

		// Token: 0x040000A5 RID: 165
		public static ScriptingInterfaceOfIGameEntity.AddChildDelegate call_AddChildDelegate;

		// Token: 0x040000A6 RID: 166
		public static ScriptingInterfaceOfIGameEntity.AddComponentDelegate call_AddComponentDelegate;

		// Token: 0x040000A7 RID: 167
		public static ScriptingInterfaceOfIGameEntity.AddDistanceJointDelegate call_AddDistanceJointDelegate;

		// Token: 0x040000A8 RID: 168
		public static ScriptingInterfaceOfIGameEntity.AddEditDataUserToAllMeshesDelegate call_AddEditDataUserToAllMeshesDelegate;

		// Token: 0x040000A9 RID: 169
		public static ScriptingInterfaceOfIGameEntity.AddLightDelegate call_AddLightDelegate;

		// Token: 0x040000AA RID: 170
		public static ScriptingInterfaceOfIGameEntity.AddMeshDelegate call_AddMeshDelegate;

		// Token: 0x040000AB RID: 171
		public static ScriptingInterfaceOfIGameEntity.AddMeshToBoneDelegate call_AddMeshToBoneDelegate;

		// Token: 0x040000AC RID: 172
		public static ScriptingInterfaceOfIGameEntity.AddMultiMeshDelegate call_AddMultiMeshDelegate;

		// Token: 0x040000AD RID: 173
		public static ScriptingInterfaceOfIGameEntity.AddMultiMeshToSkeletonDelegate call_AddMultiMeshToSkeletonDelegate;

		// Token: 0x040000AE RID: 174
		public static ScriptingInterfaceOfIGameEntity.AddMultiMeshToSkeletonBoneDelegate call_AddMultiMeshToSkeletonBoneDelegate;

		// Token: 0x040000AF RID: 175
		public static ScriptingInterfaceOfIGameEntity.AddParticleSystemComponentDelegate call_AddParticleSystemComponentDelegate;

		// Token: 0x040000B0 RID: 176
		public static ScriptingInterfaceOfIGameEntity.AddPhysicsDelegate call_AddPhysicsDelegate;

		// Token: 0x040000B1 RID: 177
		public static ScriptingInterfaceOfIGameEntity.AddSphereAsBodyDelegate call_AddSphereAsBodyDelegate;

		// Token: 0x040000B2 RID: 178
		public static ScriptingInterfaceOfIGameEntity.AddTagDelegate call_AddTagDelegate;

		// Token: 0x040000B3 RID: 179
		public static ScriptingInterfaceOfIGameEntity.ApplyAccelerationToDynamicBodyDelegate call_ApplyAccelerationToDynamicBodyDelegate;

		// Token: 0x040000B4 RID: 180
		public static ScriptingInterfaceOfIGameEntity.ApplyForceToDynamicBodyDelegate call_ApplyForceToDynamicBodyDelegate;

		// Token: 0x040000B5 RID: 181
		public static ScriptingInterfaceOfIGameEntity.ApplyLocalForceToDynamicBodyDelegate call_ApplyLocalForceToDynamicBodyDelegate;

		// Token: 0x040000B6 RID: 182
		public static ScriptingInterfaceOfIGameEntity.ApplyLocalImpulseToDynamicBodyDelegate call_ApplyLocalImpulseToDynamicBodyDelegate;

		// Token: 0x040000B7 RID: 183
		public static ScriptingInterfaceOfIGameEntity.AttachNavigationMeshFacesDelegate call_AttachNavigationMeshFacesDelegate;

		// Token: 0x040000B8 RID: 184
		public static ScriptingInterfaceOfIGameEntity.BreakPrefabDelegate call_BreakPrefabDelegate;

		// Token: 0x040000B9 RID: 185
		public static ScriptingInterfaceOfIGameEntity.BurstEntityParticleDelegate call_BurstEntityParticleDelegate;

		// Token: 0x040000BA RID: 186
		public static ScriptingInterfaceOfIGameEntity.CallScriptCallbacksDelegate call_CallScriptCallbacksDelegate;

		// Token: 0x040000BB RID: 187
		public static ScriptingInterfaceOfIGameEntity.ChangeMetaMeshOrRemoveItIfNotExistsDelegate call_ChangeMetaMeshOrRemoveItIfNotExistsDelegate;

		// Token: 0x040000BC RID: 188
		public static ScriptingInterfaceOfIGameEntity.CheckPointWithOrientedBoundingBoxDelegate call_CheckPointWithOrientedBoundingBoxDelegate;

		// Token: 0x040000BD RID: 189
		public static ScriptingInterfaceOfIGameEntity.CheckResourcesDelegate call_CheckResourcesDelegate;

		// Token: 0x040000BE RID: 190
		public static ScriptingInterfaceOfIGameEntity.ClearComponentsDelegate call_ClearComponentsDelegate;

		// Token: 0x040000BF RID: 191
		public static ScriptingInterfaceOfIGameEntity.ClearEntityComponentsDelegate call_ClearEntityComponentsDelegate;

		// Token: 0x040000C0 RID: 192
		public static ScriptingInterfaceOfIGameEntity.ClearOnlyOwnComponentsDelegate call_ClearOnlyOwnComponentsDelegate;

		// Token: 0x040000C1 RID: 193
		public static ScriptingInterfaceOfIGameEntity.ComputeTrajectoryVolumeDelegate call_ComputeTrajectoryVolumeDelegate;

		// Token: 0x040000C2 RID: 194
		public static ScriptingInterfaceOfIGameEntity.CopyComponentsToSkeletonDelegate call_CopyComponentsToSkeletonDelegate;

		// Token: 0x040000C3 RID: 195
		public static ScriptingInterfaceOfIGameEntity.CopyFromPrefabDelegate call_CopyFromPrefabDelegate;

		// Token: 0x040000C4 RID: 196
		public static ScriptingInterfaceOfIGameEntity.CopyScriptComponentFromAnotherEntityDelegate call_CopyScriptComponentFromAnotherEntityDelegate;

		// Token: 0x040000C5 RID: 197
		public static ScriptingInterfaceOfIGameEntity.CreateAndAddScriptComponentDelegate call_CreateAndAddScriptComponentDelegate;

		// Token: 0x040000C6 RID: 198
		public static ScriptingInterfaceOfIGameEntity.CreateEmptyDelegate call_CreateEmptyDelegate;

		// Token: 0x040000C7 RID: 199
		public static ScriptingInterfaceOfIGameEntity.CreateEmptyWithoutSceneDelegate call_CreateEmptyWithoutSceneDelegate;

		// Token: 0x040000C8 RID: 200
		public static ScriptingInterfaceOfIGameEntity.CreateFromPrefabDelegate call_CreateFromPrefabDelegate;

		// Token: 0x040000C9 RID: 201
		public static ScriptingInterfaceOfIGameEntity.CreateFromPrefabWithInitialFrameDelegate call_CreateFromPrefabWithInitialFrameDelegate;

		// Token: 0x040000CA RID: 202
		public static ScriptingInterfaceOfIGameEntity.DeselectEntityOnEditorDelegate call_DeselectEntityOnEditorDelegate;

		// Token: 0x040000CB RID: 203
		public static ScriptingInterfaceOfIGameEntity.DisableContourDelegate call_DisableContourDelegate;

		// Token: 0x040000CC RID: 204
		public static ScriptingInterfaceOfIGameEntity.DisableDynamicBodySimulationDelegate call_DisableDynamicBodySimulationDelegate;

		// Token: 0x040000CD RID: 205
		public static ScriptingInterfaceOfIGameEntity.DisableGravityDelegate call_DisableGravityDelegate;

		// Token: 0x040000CE RID: 206
		public static ScriptingInterfaceOfIGameEntity.EnableDynamicBodyDelegate call_EnableDynamicBodyDelegate;

		// Token: 0x040000CF RID: 207
		public static ScriptingInterfaceOfIGameEntity.FindWithNameDelegate call_FindWithNameDelegate;

		// Token: 0x040000D0 RID: 208
		public static ScriptingInterfaceOfIGameEntity.FreezeDelegate call_FreezeDelegate;

		// Token: 0x040000D1 RID: 209
		public static ScriptingInterfaceOfIGameEntity.GetBodyFlagsDelegate call_GetBodyFlagsDelegate;

		// Token: 0x040000D2 RID: 210
		public static ScriptingInterfaceOfIGameEntity.GetBodyShapeDelegate call_GetBodyShapeDelegate;

		// Token: 0x040000D3 RID: 211
		public static ScriptingInterfaceOfIGameEntity.GetBoneCountDelegate call_GetBoneCountDelegate;

		// Token: 0x040000D4 RID: 212
		public static ScriptingInterfaceOfIGameEntity.GetBoneEntitialFrameWithIndexDelegate call_GetBoneEntitialFrameWithIndexDelegate;

		// Token: 0x040000D5 RID: 213
		public static ScriptingInterfaceOfIGameEntity.GetBoneEntitialFrameWithNameDelegate call_GetBoneEntitialFrameWithNameDelegate;

		// Token: 0x040000D6 RID: 214
		public static ScriptingInterfaceOfIGameEntity.GetBoundingBoxMaxDelegate call_GetBoundingBoxMaxDelegate;

		// Token: 0x040000D7 RID: 215
		public static ScriptingInterfaceOfIGameEntity.GetBoundingBoxMinDelegate call_GetBoundingBoxMinDelegate;

		// Token: 0x040000D8 RID: 216
		public static ScriptingInterfaceOfIGameEntity.GetCameraParamsFromCameraScriptDelegate call_GetCameraParamsFromCameraScriptDelegate;

		// Token: 0x040000D9 RID: 217
		public static ScriptingInterfaceOfIGameEntity.GetCenterOfMassDelegate call_GetCenterOfMassDelegate;

		// Token: 0x040000DA RID: 218
		public static ScriptingInterfaceOfIGameEntity.GetChildDelegate call_GetChildDelegate;

		// Token: 0x040000DB RID: 219
		public static ScriptingInterfaceOfIGameEntity.GetChildCountDelegate call_GetChildCountDelegate;

		// Token: 0x040000DC RID: 220
		public static ScriptingInterfaceOfIGameEntity.GetComponentAtIndexDelegate call_GetComponentAtIndexDelegate;

		// Token: 0x040000DD RID: 221
		public static ScriptingInterfaceOfIGameEntity.GetComponentCountDelegate call_GetComponentCountDelegate;

		// Token: 0x040000DE RID: 222
		public static ScriptingInterfaceOfIGameEntity.GetEditModeLevelVisibilityDelegate call_GetEditModeLevelVisibilityDelegate;

		// Token: 0x040000DF RID: 223
		public static ScriptingInterfaceOfIGameEntity.GetEntityFlagsDelegate call_GetEntityFlagsDelegate;

		// Token: 0x040000E0 RID: 224
		public static ScriptingInterfaceOfIGameEntity.GetEntityVisibilityFlagsDelegate call_GetEntityVisibilityFlagsDelegate;

		// Token: 0x040000E1 RID: 225
		public static ScriptingInterfaceOfIGameEntity.GetFactorColorDelegate call_GetFactorColorDelegate;

		// Token: 0x040000E2 RID: 226
		public static ScriptingInterfaceOfIGameEntity.GetFirstEntityWithTagDelegate call_GetFirstEntityWithTagDelegate;

		// Token: 0x040000E3 RID: 227
		public static ScriptingInterfaceOfIGameEntity.GetFirstEntityWithTagExpressionDelegate call_GetFirstEntityWithTagExpressionDelegate;

		// Token: 0x040000E4 RID: 228
		public static ScriptingInterfaceOfIGameEntity.GetFirstMeshDelegate call_GetFirstMeshDelegate;

		// Token: 0x040000E5 RID: 229
		public static ScriptingInterfaceOfIGameEntity.GetFrameDelegate call_GetFrameDelegate;

		// Token: 0x040000E6 RID: 230
		public static ScriptingInterfaceOfIGameEntity.GetGlobalBoxMaxDelegate call_GetGlobalBoxMaxDelegate;

		// Token: 0x040000E7 RID: 231
		public static ScriptingInterfaceOfIGameEntity.GetGlobalBoxMinDelegate call_GetGlobalBoxMinDelegate;

		// Token: 0x040000E8 RID: 232
		public static ScriptingInterfaceOfIGameEntity.GetGlobalFrameDelegate call_GetGlobalFrameDelegate;

		// Token: 0x040000E9 RID: 233
		public static ScriptingInterfaceOfIGameEntity.GetGlobalScaleDelegate call_GetGlobalScaleDelegate;

		// Token: 0x040000EA RID: 234
		public static ScriptingInterfaceOfIGameEntity.GetGuidDelegate call_GetGuidDelegate;

		// Token: 0x040000EB RID: 235
		public static ScriptingInterfaceOfIGameEntity.GetLightDelegate call_GetLightDelegate;

		// Token: 0x040000EC RID: 236
		public static ScriptingInterfaceOfIGameEntity.GetLinearVelocityDelegate call_GetLinearVelocityDelegate;

		// Token: 0x040000ED RID: 237
		public static ScriptingInterfaceOfIGameEntity.GetLodLevelForDistanceSqDelegate call_GetLodLevelForDistanceSqDelegate;

		// Token: 0x040000EE RID: 238
		public static ScriptingInterfaceOfIGameEntity.GetMassDelegate call_GetMassDelegate;

		// Token: 0x040000EF RID: 239
		public static ScriptingInterfaceOfIGameEntity.GetMeshBendedPositionDelegate call_GetMeshBendedPositionDelegate;

		// Token: 0x040000F0 RID: 240
		public static ScriptingInterfaceOfIGameEntity.GetNameDelegate call_GetNameDelegate;

		// Token: 0x040000F1 RID: 241
		public static ScriptingInterfaceOfIGameEntity.GetNextEntityWithTagDelegate call_GetNextEntityWithTagDelegate;

		// Token: 0x040000F2 RID: 242
		public static ScriptingInterfaceOfIGameEntity.GetNextEntityWithTagExpressionDelegate call_GetNextEntityWithTagExpressionDelegate;

		// Token: 0x040000F3 RID: 243
		public static ScriptingInterfaceOfIGameEntity.GetNextPrefabDelegate call_GetNextPrefabDelegate;

		// Token: 0x040000F4 RID: 244
		public static ScriptingInterfaceOfIGameEntity.GetOldPrefabNameDelegate call_GetOldPrefabNameDelegate;

		// Token: 0x040000F5 RID: 245
		public static ScriptingInterfaceOfIGameEntity.GetParentDelegate call_GetParentDelegate;

		// Token: 0x040000F6 RID: 246
		public static ScriptingInterfaceOfIGameEntity.GetPhysicsBoundingBoxMaxDelegate call_GetPhysicsBoundingBoxMaxDelegate;

		// Token: 0x040000F7 RID: 247
		public static ScriptingInterfaceOfIGameEntity.GetPhysicsBoundingBoxMinDelegate call_GetPhysicsBoundingBoxMinDelegate;

		// Token: 0x040000F8 RID: 248
		public static ScriptingInterfaceOfIGameEntity.GetPhysicsDescBodyFlagsDelegate call_GetPhysicsDescBodyFlagsDelegate;

		// Token: 0x040000F9 RID: 249
		public static ScriptingInterfaceOfIGameEntity.GetPhysicsMinMaxDelegate call_GetPhysicsMinMaxDelegate;

		// Token: 0x040000FA RID: 250
		public static ScriptingInterfaceOfIGameEntity.GetPhysicsStateDelegate call_GetPhysicsStateDelegate;

		// Token: 0x040000FB RID: 251
		public static ScriptingInterfaceOfIGameEntity.GetPrefabNameDelegate call_GetPrefabNameDelegate;

		// Token: 0x040000FC RID: 252
		public static ScriptingInterfaceOfIGameEntity.GetPreviousGlobalFrameDelegate call_GetPreviousGlobalFrameDelegate;

		// Token: 0x040000FD RID: 253
		public static ScriptingInterfaceOfIGameEntity.GetQuickBoneEntitialFrameDelegate call_GetQuickBoneEntitialFrameDelegate;

		// Token: 0x040000FE RID: 254
		public static ScriptingInterfaceOfIGameEntity.GetRadiusDelegate call_GetRadiusDelegate;

		// Token: 0x040000FF RID: 255
		public static ScriptingInterfaceOfIGameEntity.GetSceneDelegate call_GetSceneDelegate;

		// Token: 0x04000100 RID: 256
		public static ScriptingInterfaceOfIGameEntity.GetScenePointerDelegate call_GetScenePointerDelegate;

		// Token: 0x04000101 RID: 257
		public static ScriptingInterfaceOfIGameEntity.GetScriptComponentDelegate call_GetScriptComponentDelegate;

		// Token: 0x04000102 RID: 258
		public static ScriptingInterfaceOfIGameEntity.GetScriptComponentAtIndexDelegate call_GetScriptComponentAtIndexDelegate;

		// Token: 0x04000103 RID: 259
		public static ScriptingInterfaceOfIGameEntity.GetScriptComponentCountDelegate call_GetScriptComponentCountDelegate;

		// Token: 0x04000104 RID: 260
		public static ScriptingInterfaceOfIGameEntity.GetSkeletonDelegate call_GetSkeletonDelegate;

		// Token: 0x04000105 RID: 261
		public static ScriptingInterfaceOfIGameEntity.GetTagsDelegate call_GetTagsDelegate;

		// Token: 0x04000106 RID: 262
		public static ScriptingInterfaceOfIGameEntity.GetUpgradeLevelMaskDelegate call_GetUpgradeLevelMaskDelegate;

		// Token: 0x04000107 RID: 263
		public static ScriptingInterfaceOfIGameEntity.GetUpgradeLevelMaskCumulativeDelegate call_GetUpgradeLevelMaskCumulativeDelegate;

		// Token: 0x04000108 RID: 264
		public static ScriptingInterfaceOfIGameEntity.GetVisibilityExcludeParentsDelegate call_GetVisibilityExcludeParentsDelegate;

		// Token: 0x04000109 RID: 265
		public static ScriptingInterfaceOfIGameEntity.GetVisibilityLevelMaskIncludingParentsDelegate call_GetVisibilityLevelMaskIncludingParentsDelegate;

		// Token: 0x0400010A RID: 266
		public static ScriptingInterfaceOfIGameEntity.HasBodyDelegate call_HasBodyDelegate;

		// Token: 0x0400010B RID: 267
		public static ScriptingInterfaceOfIGameEntity.HasComplexAnimTreeDelegate call_HasComplexAnimTreeDelegate;

		// Token: 0x0400010C RID: 268
		public static ScriptingInterfaceOfIGameEntity.HasComponentDelegate call_HasComponentDelegate;

		// Token: 0x0400010D RID: 269
		public static ScriptingInterfaceOfIGameEntity.HasFrameChangedDelegate call_HasFrameChangedDelegate;

		// Token: 0x0400010E RID: 270
		public static ScriptingInterfaceOfIGameEntity.HasPhysicsBodyDelegate call_HasPhysicsBodyDelegate;

		// Token: 0x0400010F RID: 271
		public static ScriptingInterfaceOfIGameEntity.HasPhysicsDefinitionDelegate call_HasPhysicsDefinitionDelegate;

		// Token: 0x04000110 RID: 272
		public static ScriptingInterfaceOfIGameEntity.HasSceneDelegate call_HasSceneDelegate;

		// Token: 0x04000111 RID: 273
		public static ScriptingInterfaceOfIGameEntity.HasScriptComponentDelegate call_HasScriptComponentDelegate;

		// Token: 0x04000112 RID: 274
		public static ScriptingInterfaceOfIGameEntity.HasTagDelegate call_HasTagDelegate;

		// Token: 0x04000113 RID: 275
		public static ScriptingInterfaceOfIGameEntity.IsDynamicBodyStationaryDelegate call_IsDynamicBodyStationaryDelegate;

		// Token: 0x04000114 RID: 276
		public static ScriptingInterfaceOfIGameEntity.IsEngineBodySleepingDelegate call_IsEngineBodySleepingDelegate;

		// Token: 0x04000115 RID: 277
		public static ScriptingInterfaceOfIGameEntity.IsEntitySelectedOnEditorDelegate call_IsEntitySelectedOnEditorDelegate;

		// Token: 0x04000116 RID: 278
		public static ScriptingInterfaceOfIGameEntity.IsFrozenDelegate call_IsFrozenDelegate;

		// Token: 0x04000117 RID: 279
		public static ScriptingInterfaceOfIGameEntity.IsGhostObjectDelegate call_IsGhostObjectDelegate;

		// Token: 0x04000118 RID: 280
		public static ScriptingInterfaceOfIGameEntity.IsGuidValidDelegate call_IsGuidValidDelegate;

		// Token: 0x04000119 RID: 281
		public static ScriptingInterfaceOfIGameEntity.IsVisibleIncludeParentsDelegate call_IsVisibleIncludeParentsDelegate;

		// Token: 0x0400011A RID: 282
		public static ScriptingInterfaceOfIGameEntity.PauseParticleSystemDelegate call_PauseParticleSystemDelegate;

		// Token: 0x0400011B RID: 283
		public static ScriptingInterfaceOfIGameEntity.PrefabExistsDelegate call_PrefabExistsDelegate;

		// Token: 0x0400011C RID: 284
		public static ScriptingInterfaceOfIGameEntity.RecomputeBoundingBoxDelegate call_RecomputeBoundingBoxDelegate;

		// Token: 0x0400011D RID: 285
		public static ScriptingInterfaceOfIGameEntity.ReleaseEditDataUserToAllMeshesDelegate call_ReleaseEditDataUserToAllMeshesDelegate;

		// Token: 0x0400011E RID: 286
		public static ScriptingInterfaceOfIGameEntity.RemoveDelegate call_RemoveDelegate;

		// Token: 0x0400011F RID: 287
		public static ScriptingInterfaceOfIGameEntity.RemoveAllChildrenDelegate call_RemoveAllChildrenDelegate;

		// Token: 0x04000120 RID: 288
		public static ScriptingInterfaceOfIGameEntity.RemoveAllParticleSystemsDelegate call_RemoveAllParticleSystemsDelegate;

		// Token: 0x04000121 RID: 289
		public static ScriptingInterfaceOfIGameEntity.RemoveChildDelegate call_RemoveChildDelegate;

		// Token: 0x04000122 RID: 290
		public static ScriptingInterfaceOfIGameEntity.RemoveComponentDelegate call_RemoveComponentDelegate;

		// Token: 0x04000123 RID: 291
		public static ScriptingInterfaceOfIGameEntity.RemoveComponentWithMeshDelegate call_RemoveComponentWithMeshDelegate;

		// Token: 0x04000124 RID: 292
		public static ScriptingInterfaceOfIGameEntity.RemoveEnginePhysicsDelegate call_RemoveEnginePhysicsDelegate;

		// Token: 0x04000125 RID: 293
		public static ScriptingInterfaceOfIGameEntity.RemoveFromPredisplayEntityDelegate call_RemoveFromPredisplayEntityDelegate;

		// Token: 0x04000126 RID: 294
		public static ScriptingInterfaceOfIGameEntity.RemoveMultiMeshDelegate call_RemoveMultiMeshDelegate;

		// Token: 0x04000127 RID: 295
		public static ScriptingInterfaceOfIGameEntity.RemoveMultiMeshFromSkeletonDelegate call_RemoveMultiMeshFromSkeletonDelegate;

		// Token: 0x04000128 RID: 296
		public static ScriptingInterfaceOfIGameEntity.RemoveMultiMeshFromSkeletonBoneDelegate call_RemoveMultiMeshFromSkeletonBoneDelegate;

		// Token: 0x04000129 RID: 297
		public static ScriptingInterfaceOfIGameEntity.RemovePhysicsDelegate call_RemovePhysicsDelegate;

		// Token: 0x0400012A RID: 298
		public static ScriptingInterfaceOfIGameEntity.RemoveScriptComponentDelegate call_RemoveScriptComponentDelegate;

		// Token: 0x0400012B RID: 299
		public static ScriptingInterfaceOfIGameEntity.RemoveTagDelegate call_RemoveTagDelegate;

		// Token: 0x0400012C RID: 300
		public static ScriptingInterfaceOfIGameEntity.ResumeParticleSystemDelegate call_ResumeParticleSystemDelegate;

		// Token: 0x0400012D RID: 301
		public static ScriptingInterfaceOfIGameEntity.SelectEntityOnEditorDelegate call_SelectEntityOnEditorDelegate;

		// Token: 0x0400012E RID: 302
		public static ScriptingInterfaceOfIGameEntity.SetAlphaDelegate call_SetAlphaDelegate;

		// Token: 0x0400012F RID: 303
		public static ScriptingInterfaceOfIGameEntity.SetAnimationSoundActivationDelegate call_SetAnimationSoundActivationDelegate;

		// Token: 0x04000130 RID: 304
		public static ScriptingInterfaceOfIGameEntity.SetAnimTreeChannelParameterDelegate call_SetAnimTreeChannelParameterDelegate;

		// Token: 0x04000131 RID: 305
		public static ScriptingInterfaceOfIGameEntity.SetAsContourEntityDelegate call_SetAsContourEntityDelegate;

		// Token: 0x04000132 RID: 306
		public static ScriptingInterfaceOfIGameEntity.SetAsPredisplayEntityDelegate call_SetAsPredisplayEntityDelegate;

		// Token: 0x04000133 RID: 307
		public static ScriptingInterfaceOfIGameEntity.SetAsReplayEntityDelegate call_SetAsReplayEntityDelegate;

		// Token: 0x04000134 RID: 308
		public static ScriptingInterfaceOfIGameEntity.SetBodyFlagsDelegate call_SetBodyFlagsDelegate;

		// Token: 0x04000135 RID: 309
		public static ScriptingInterfaceOfIGameEntity.SetBodyFlagsRecursiveDelegate call_SetBodyFlagsRecursiveDelegate;

		// Token: 0x04000136 RID: 310
		public static ScriptingInterfaceOfIGameEntity.SetBodyShapeDelegate call_SetBodyShapeDelegate;

		// Token: 0x04000137 RID: 311
		public static ScriptingInterfaceOfIGameEntity.SetBoundingboxDirtyDelegate call_SetBoundingboxDirtyDelegate;

		// Token: 0x04000138 RID: 312
		public static ScriptingInterfaceOfIGameEntity.SetClothComponentKeepStateDelegate call_SetClothComponentKeepStateDelegate;

		// Token: 0x04000139 RID: 313
		public static ScriptingInterfaceOfIGameEntity.SetClothComponentKeepStateOfAllMeshesDelegate call_SetClothComponentKeepStateOfAllMeshesDelegate;

		// Token: 0x0400013A RID: 314
		public static ScriptingInterfaceOfIGameEntity.SetClothMaxDistanceMultiplierDelegate call_SetClothMaxDistanceMultiplierDelegate;

		// Token: 0x0400013B RID: 315
		public static ScriptingInterfaceOfIGameEntity.SetContourStateDelegate call_SetContourStateDelegate;

		// Token: 0x0400013C RID: 316
		public static ScriptingInterfaceOfIGameEntity.SetCullModeDelegate call_SetCullModeDelegate;

		// Token: 0x0400013D RID: 317
		public static ScriptingInterfaceOfIGameEntity.SetDampingDelegate call_SetDampingDelegate;

		// Token: 0x0400013E RID: 318
		public static ScriptingInterfaceOfIGameEntity.SetEnforcedMaximumLodLevelDelegate call_SetEnforcedMaximumLodLevelDelegate;

		// Token: 0x0400013F RID: 319
		public static ScriptingInterfaceOfIGameEntity.SetEntityEnvMapVisibilityDelegate call_SetEntityEnvMapVisibilityDelegate;

		// Token: 0x04000140 RID: 320
		public static ScriptingInterfaceOfIGameEntity.SetEntityFlagsDelegate call_SetEntityFlagsDelegate;

		// Token: 0x04000141 RID: 321
		public static ScriptingInterfaceOfIGameEntity.SetEntityVisibilityFlagsDelegate call_SetEntityVisibilityFlagsDelegate;

		// Token: 0x04000142 RID: 322
		public static ScriptingInterfaceOfIGameEntity.SetExternalReferencesUsageDelegate call_SetExternalReferencesUsageDelegate;

		// Token: 0x04000143 RID: 323
		public static ScriptingInterfaceOfIGameEntity.SetFactor2ColorDelegate call_SetFactor2ColorDelegate;

		// Token: 0x04000144 RID: 324
		public static ScriptingInterfaceOfIGameEntity.SetFactorColorDelegate call_SetFactorColorDelegate;

		// Token: 0x04000145 RID: 325
		public static ScriptingInterfaceOfIGameEntity.SetFrameDelegate call_SetFrameDelegate;

		// Token: 0x04000146 RID: 326
		public static ScriptingInterfaceOfIGameEntity.SetFrameChangedDelegate call_SetFrameChangedDelegate;

		// Token: 0x04000147 RID: 327
		public static ScriptingInterfaceOfIGameEntity.SetGlobalFrameDelegate call_SetGlobalFrameDelegate;

		// Token: 0x04000148 RID: 328
		public static ScriptingInterfaceOfIGameEntity.SetLocalPositionDelegate call_SetLocalPositionDelegate;

		// Token: 0x04000149 RID: 329
		public static ScriptingInterfaceOfIGameEntity.SetMassDelegate call_SetMassDelegate;

		// Token: 0x0400014A RID: 330
		public static ScriptingInterfaceOfIGameEntity.SetMassSpaceInertiaDelegate call_SetMassSpaceInertiaDelegate;

		// Token: 0x0400014B RID: 331
		public static ScriptingInterfaceOfIGameEntity.SetMaterialForAllMeshesDelegate call_SetMaterialForAllMeshesDelegate;

		// Token: 0x0400014C RID: 332
		public static ScriptingInterfaceOfIGameEntity.SetMobilityDelegate call_SetMobilityDelegate;

		// Token: 0x0400014D RID: 333
		public static ScriptingInterfaceOfIGameEntity.SetMorphFrameOfComponentsDelegate call_SetMorphFrameOfComponentsDelegate;

		// Token: 0x0400014E RID: 334
		public static ScriptingInterfaceOfIGameEntity.SetNameDelegate call_SetNameDelegate;

		// Token: 0x0400014F RID: 335
		public static ScriptingInterfaceOfIGameEntity.SetPhysicsStateDelegate call_SetPhysicsStateDelegate;

		// Token: 0x04000150 RID: 336
		public static ScriptingInterfaceOfIGameEntity.SetPreviousFrameInvalidDelegate call_SetPreviousFrameInvalidDelegate;

		// Token: 0x04000151 RID: 337
		public static ScriptingInterfaceOfIGameEntity.SetReadyToRenderDelegate call_SetReadyToRenderDelegate;

		// Token: 0x04000152 RID: 338
		public static ScriptingInterfaceOfIGameEntity.SetRuntimeEmissionRateMultiplierDelegate call_SetRuntimeEmissionRateMultiplierDelegate;

		// Token: 0x04000153 RID: 339
		public static ScriptingInterfaceOfIGameEntity.SetSkeletonDelegate call_SetSkeletonDelegate;

		// Token: 0x04000154 RID: 340
		public static ScriptingInterfaceOfIGameEntity.SetUpgradeLevelMaskDelegate call_SetUpgradeLevelMaskDelegate;

		// Token: 0x04000155 RID: 341
		public static ScriptingInterfaceOfIGameEntity.SetVectorArgumentDelegate call_SetVectorArgumentDelegate;

		// Token: 0x04000156 RID: 342
		public static ScriptingInterfaceOfIGameEntity.SetVisibilityExcludeParentsDelegate call_SetVisibilityExcludeParentsDelegate;

		// Token: 0x04000157 RID: 343
		public static ScriptingInterfaceOfIGameEntity.UpdateGlobalBoundsDelegate call_UpdateGlobalBoundsDelegate;

		// Token: 0x04000158 RID: 344
		public static ScriptingInterfaceOfIGameEntity.UpdateTriadFrameForEditorDelegate call_UpdateTriadFrameForEditorDelegate;

		// Token: 0x04000159 RID: 345
		public static ScriptingInterfaceOfIGameEntity.UpdateVisibilityMaskDelegate call_UpdateVisibilityMaskDelegate;

		// Token: 0x0400015A RID: 346
		public static ScriptingInterfaceOfIGameEntity.ValidateBoundingBoxDelegate call_ValidateBoundingBoxDelegate;

		// Token: 0x0200010B RID: 267
		// (Invoke) Token: 0x0600092F RID: 2351
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ActivateRagdollDelegate(UIntPtr entityId);

		// Token: 0x0200010C RID: 268
		// (Invoke) Token: 0x06000933 RID: 2355
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddAllMeshesOfGameEntityDelegate(UIntPtr entityId, UIntPtr copiedEntityId);

		// Token: 0x0200010D RID: 269
		// (Invoke) Token: 0x06000937 RID: 2359
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddChildDelegate(UIntPtr parententity, UIntPtr childentity, [MarshalAs(UnmanagedType.U1)] bool autoLocalizeFrame);

		// Token: 0x0200010E RID: 270
		// (Invoke) Token: 0x0600093B RID: 2363
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddComponentDelegate(UIntPtr pointer, UIntPtr componentPointer);

		// Token: 0x0200010F RID: 271
		// (Invoke) Token: 0x0600093F RID: 2367
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddDistanceJointDelegate(UIntPtr entityId, UIntPtr otherEntityId, float minDistance, float maxDistance);

		// Token: 0x02000110 RID: 272
		// (Invoke) Token: 0x06000943 RID: 2371
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddEditDataUserToAllMeshesDelegate(UIntPtr entityId, [MarshalAs(UnmanagedType.U1)] bool entity_components, [MarshalAs(UnmanagedType.U1)] bool skeleton_components);

		// Token: 0x02000111 RID: 273
		// (Invoke) Token: 0x06000947 RID: 2375
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool AddLightDelegate(UIntPtr entityId, UIntPtr lightPointer);

		// Token: 0x02000112 RID: 274
		// (Invoke) Token: 0x0600094B RID: 2379
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddMeshDelegate(UIntPtr entityId, UIntPtr mesh, [MarshalAs(UnmanagedType.U1)] bool recomputeBoundingBox);

		// Token: 0x02000113 RID: 275
		// (Invoke) Token: 0x0600094F RID: 2383
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddMeshToBoneDelegate(UIntPtr entityId, UIntPtr multiMeshPointer, sbyte boneIndex);

		// Token: 0x02000114 RID: 276
		// (Invoke) Token: 0x06000953 RID: 2387
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddMultiMeshDelegate(UIntPtr entityId, UIntPtr multiMeshPtr, [MarshalAs(UnmanagedType.U1)] bool updateVisMask);

		// Token: 0x02000115 RID: 277
		// (Invoke) Token: 0x06000957 RID: 2391
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddMultiMeshToSkeletonDelegate(UIntPtr gameEntity, UIntPtr multiMesh);

		// Token: 0x02000116 RID: 278
		// (Invoke) Token: 0x0600095B RID: 2395
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddMultiMeshToSkeletonBoneDelegate(UIntPtr gameEntity, UIntPtr multiMesh, sbyte boneIndex);

		// Token: 0x02000117 RID: 279
		// (Invoke) Token: 0x0600095F RID: 2399
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddParticleSystemComponentDelegate(UIntPtr entityId, byte[] particleid);

		// Token: 0x02000118 RID: 280
		// (Invoke) Token: 0x06000963 RID: 2403
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddPhysicsDelegate(UIntPtr entityId, UIntPtr body, float mass, ref Vec3 localCenterOfMass, ref Vec3 initialVelocity, ref Vec3 initialAngularVelocity, int physicsMaterial, [MarshalAs(UnmanagedType.U1)] bool isStatic, int collisionGroupID);

		// Token: 0x02000119 RID: 281
		// (Invoke) Token: 0x06000967 RID: 2407
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddSphereAsBodyDelegate(UIntPtr entityId, Vec3 center, float radius, uint bodyFlags);

		// Token: 0x0200011A RID: 282
		// (Invoke) Token: 0x0600096B RID: 2411
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddTagDelegate(UIntPtr entityId, byte[] tag);

		// Token: 0x0200011B RID: 283
		// (Invoke) Token: 0x0600096F RID: 2415
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ApplyAccelerationToDynamicBodyDelegate(UIntPtr entityId, ref Vec3 acceleration);

		// Token: 0x0200011C RID: 284
		// (Invoke) Token: 0x06000973 RID: 2419
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ApplyForceToDynamicBodyDelegate(UIntPtr entityId, ref Vec3 force);

		// Token: 0x0200011D RID: 285
		// (Invoke) Token: 0x06000977 RID: 2423
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ApplyLocalForceToDynamicBodyDelegate(UIntPtr entityId, ref Vec3 localPosition, ref Vec3 force);

		// Token: 0x0200011E RID: 286
		// (Invoke) Token: 0x0600097B RID: 2427
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ApplyLocalImpulseToDynamicBodyDelegate(UIntPtr entityId, ref Vec3 localPosition, ref Vec3 impulse);

		// Token: 0x0200011F RID: 287
		// (Invoke) Token: 0x0600097F RID: 2431
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AttachNavigationMeshFacesDelegate(UIntPtr entityId, int faceGroupId, [MarshalAs(UnmanagedType.U1)] bool isConnected, [MarshalAs(UnmanagedType.U1)] bool isBlocker, [MarshalAs(UnmanagedType.U1)] bool autoLocalize);

		// Token: 0x02000120 RID: 288
		// (Invoke) Token: 0x06000983 RID: 2435
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void BreakPrefabDelegate(UIntPtr entityId);

		// Token: 0x02000121 RID: 289
		// (Invoke) Token: 0x06000987 RID: 2439
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void BurstEntityParticleDelegate(UIntPtr entityId, [MarshalAs(UnmanagedType.U1)] bool doChildren);

		// Token: 0x02000122 RID: 290
		// (Invoke) Token: 0x0600098B RID: 2443
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void CallScriptCallbacksDelegate(UIntPtr entityPointer);

		// Token: 0x02000123 RID: 291
		// (Invoke) Token: 0x0600098F RID: 2447
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ChangeMetaMeshOrRemoveItIfNotExistsDelegate(UIntPtr entityId, UIntPtr entityMetaMeshPointer, UIntPtr newMetaMeshPointer);

		// Token: 0x02000124 RID: 292
		// (Invoke) Token: 0x06000993 RID: 2451
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool CheckPointWithOrientedBoundingBoxDelegate(UIntPtr entityId, Vec3 point);

		// Token: 0x02000125 RID: 293
		// (Invoke) Token: 0x06000997 RID: 2455
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool CheckResourcesDelegate(UIntPtr entityId, [MarshalAs(UnmanagedType.U1)] bool addToQueue, [MarshalAs(UnmanagedType.U1)] bool checkFaceResources);

		// Token: 0x02000126 RID: 294
		// (Invoke) Token: 0x0600099B RID: 2459
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ClearComponentsDelegate(UIntPtr entityId);

		// Token: 0x02000127 RID: 295
		// (Invoke) Token: 0x0600099F RID: 2463
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ClearEntityComponentsDelegate(UIntPtr entityId, [MarshalAs(UnmanagedType.U1)] bool resetAll, [MarshalAs(UnmanagedType.U1)] bool removeScripts, [MarshalAs(UnmanagedType.U1)] bool deleteChildEntities);

		// Token: 0x02000128 RID: 296
		// (Invoke) Token: 0x060009A3 RID: 2467
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ClearOnlyOwnComponentsDelegate(UIntPtr entityId);

		// Token: 0x02000129 RID: 297
		// (Invoke) Token: 0x060009A7 RID: 2471
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ComputeTrajectoryVolumeDelegate(UIntPtr gameEntity, float missileSpeed, float verticalAngleMaxInDegrees, float verticalAngleMinInDegrees, float horizontalAngleRangeInDegrees, float airFrictionConstant);

		// Token: 0x0200012A RID: 298
		// (Invoke) Token: 0x060009AB RID: 2475
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void CopyComponentsToSkeletonDelegate(UIntPtr entityId);

		// Token: 0x0200012B RID: 299
		// (Invoke) Token: 0x060009AF RID: 2479
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer CopyFromPrefabDelegate(UIntPtr prefab);

		// Token: 0x0200012C RID: 300
		// (Invoke) Token: 0x060009B3 RID: 2483
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void CopyScriptComponentFromAnotherEntityDelegate(UIntPtr prefab, UIntPtr other_prefab, byte[] script_name);

		// Token: 0x0200012D RID: 301
		// (Invoke) Token: 0x060009B7 RID: 2487
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void CreateAndAddScriptComponentDelegate(UIntPtr entityId, byte[] name);

		// Token: 0x0200012E RID: 302
		// (Invoke) Token: 0x060009BB RID: 2491
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer CreateEmptyDelegate(UIntPtr scenePointer, [MarshalAs(UnmanagedType.U1)] bool isModifiableFromEditor, UIntPtr entityId);

		// Token: 0x0200012F RID: 303
		// (Invoke) Token: 0x060009BF RID: 2495
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer CreateEmptyWithoutSceneDelegate();

		// Token: 0x02000130 RID: 304
		// (Invoke) Token: 0x060009C3 RID: 2499
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer CreateFromPrefabDelegate(UIntPtr scenePointer, byte[] prefabid, [MarshalAs(UnmanagedType.U1)] bool callScriptCallbacks);

		// Token: 0x02000131 RID: 305
		// (Invoke) Token: 0x060009C7 RID: 2503
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer CreateFromPrefabWithInitialFrameDelegate(UIntPtr scenePointer, byte[] prefabid, ref MatrixFrame frame);

		// Token: 0x02000132 RID: 306
		// (Invoke) Token: 0x060009CB RID: 2507
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void DeselectEntityOnEditorDelegate(UIntPtr entityId);

		// Token: 0x02000133 RID: 307
		// (Invoke) Token: 0x060009CF RID: 2511
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void DisableContourDelegate(UIntPtr entityId);

		// Token: 0x02000134 RID: 308
		// (Invoke) Token: 0x060009D3 RID: 2515
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void DisableDynamicBodySimulationDelegate(UIntPtr entityId);

		// Token: 0x02000135 RID: 309
		// (Invoke) Token: 0x060009D7 RID: 2519
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void DisableGravityDelegate(UIntPtr entityId);

		// Token: 0x02000136 RID: 310
		// (Invoke) Token: 0x060009DB RID: 2523
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void EnableDynamicBodyDelegate(UIntPtr entityId);

		// Token: 0x02000137 RID: 311
		// (Invoke) Token: 0x060009DF RID: 2527
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer FindWithNameDelegate(UIntPtr scenePointer, byte[] name);

		// Token: 0x02000138 RID: 312
		// (Invoke) Token: 0x060009E3 RID: 2531
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void FreezeDelegate(UIntPtr entityId, [MarshalAs(UnmanagedType.U1)] bool isFrozen);

		// Token: 0x02000139 RID: 313
		// (Invoke) Token: 0x060009E7 RID: 2535
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate uint GetBodyFlagsDelegate(UIntPtr entityId);

		// Token: 0x0200013A RID: 314
		// (Invoke) Token: 0x060009EB RID: 2539
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer GetBodyShapeDelegate(UIntPtr entity);

		// Token: 0x0200013B RID: 315
		// (Invoke) Token: 0x060009EF RID: 2543
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate sbyte GetBoneCountDelegate(UIntPtr entityId);

		// Token: 0x0200013C RID: 316
		// (Invoke) Token: 0x060009F3 RID: 2547
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetBoneEntitialFrameWithIndexDelegate(UIntPtr entityId, sbyte boneIndex, ref MatrixFrame outEntitialFrame);

		// Token: 0x0200013D RID: 317
		// (Invoke) Token: 0x060009F7 RID: 2551
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetBoneEntitialFrameWithNameDelegate(UIntPtr entityId, byte[] boneName, ref MatrixFrame outEntitialFrame);

		// Token: 0x0200013E RID: 318
		// (Invoke) Token: 0x060009FB RID: 2555
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate Vec3 GetBoundingBoxMaxDelegate(UIntPtr entityId);

		// Token: 0x0200013F RID: 319
		// (Invoke) Token: 0x060009FF RID: 2559
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate Vec3 GetBoundingBoxMinDelegate(UIntPtr entityId);

		// Token: 0x02000140 RID: 320
		// (Invoke) Token: 0x06000A03 RID: 2563
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetCameraParamsFromCameraScriptDelegate(UIntPtr entityId, UIntPtr camPtr, ref Vec3 dof_params);

		// Token: 0x02000141 RID: 321
		// (Invoke) Token: 0x06000A07 RID: 2567
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate Vec3 GetCenterOfMassDelegate(UIntPtr entityId);

		// Token: 0x02000142 RID: 322
		// (Invoke) Token: 0x06000A0B RID: 2571
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer GetChildDelegate(UIntPtr entityId, int childIndex);

		// Token: 0x02000143 RID: 323
		// (Invoke) Token: 0x06000A0F RID: 2575
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetChildCountDelegate(UIntPtr entityId);

		// Token: 0x02000144 RID: 324
		// (Invoke) Token: 0x06000A13 RID: 2579
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer GetComponentAtIndexDelegate(UIntPtr entityId, GameEntity.ComponentType componentType, int index);

		// Token: 0x02000145 RID: 325
		// (Invoke) Token: 0x06000A17 RID: 2583
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetComponentCountDelegate(UIntPtr entityId, GameEntity.ComponentType componentType);

		// Token: 0x02000146 RID: 326
		// (Invoke) Token: 0x06000A1B RID: 2587
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool GetEditModeLevelVisibilityDelegate(UIntPtr entityId);

		// Token: 0x02000147 RID: 327
		// (Invoke) Token: 0x06000A1F RID: 2591
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate uint GetEntityFlagsDelegate(UIntPtr entityId);

		// Token: 0x02000148 RID: 328
		// (Invoke) Token: 0x06000A23 RID: 2595
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate uint GetEntityVisibilityFlagsDelegate(UIntPtr entityId);

		// Token: 0x02000149 RID: 329
		// (Invoke) Token: 0x06000A27 RID: 2599
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate uint GetFactorColorDelegate(UIntPtr entityId);

		// Token: 0x0200014A RID: 330
		// (Invoke) Token: 0x06000A2B RID: 2603
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer GetFirstEntityWithTagDelegate(UIntPtr scenePointer, byte[] tag);

		// Token: 0x0200014B RID: 331
		// (Invoke) Token: 0x06000A2F RID: 2607
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer GetFirstEntityWithTagExpressionDelegate(UIntPtr scenePointer, byte[] tagExpression);

		// Token: 0x0200014C RID: 332
		// (Invoke) Token: 0x06000A33 RID: 2611
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer GetFirstMeshDelegate(UIntPtr entityId);

		// Token: 0x0200014D RID: 333
		// (Invoke) Token: 0x06000A37 RID: 2615
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetFrameDelegate(UIntPtr entityId, ref MatrixFrame outFrame);

		// Token: 0x0200014E RID: 334
		// (Invoke) Token: 0x06000A3B RID: 2619
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate Vec3 GetGlobalBoxMaxDelegate(UIntPtr entityId);

		// Token: 0x0200014F RID: 335
		// (Invoke) Token: 0x06000A3F RID: 2623
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate Vec3 GetGlobalBoxMinDelegate(UIntPtr entityId);

		// Token: 0x02000150 RID: 336
		// (Invoke) Token: 0x06000A43 RID: 2627
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetGlobalFrameDelegate(UIntPtr meshPointer, out MatrixFrame outFrame);

		// Token: 0x02000151 RID: 337
		// (Invoke) Token: 0x06000A47 RID: 2631
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate Vec3 GetGlobalScaleDelegate(UIntPtr entity);

		// Token: 0x02000152 RID: 338
		// (Invoke) Token: 0x06000A4B RID: 2635
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetGuidDelegate(UIntPtr entityId);

		// Token: 0x02000153 RID: 339
		// (Invoke) Token: 0x06000A4F RID: 2639
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer GetLightDelegate(UIntPtr entityId);

		// Token: 0x02000154 RID: 340
		// (Invoke) Token: 0x06000A53 RID: 2643
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate Vec3 GetLinearVelocityDelegate(UIntPtr entityPtr);

		// Token: 0x02000155 RID: 341
		// (Invoke) Token: 0x06000A57 RID: 2647
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetLodLevelForDistanceSqDelegate(UIntPtr entityId, float distanceSquared);

		// Token: 0x02000156 RID: 342
		// (Invoke) Token: 0x06000A5B RID: 2651
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetMassDelegate(UIntPtr entityId);

		// Token: 0x02000157 RID: 343
		// (Invoke) Token: 0x06000A5F RID: 2655
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetMeshBendedPositionDelegate(UIntPtr entityId, ref MatrixFrame worldSpacePosition, ref MatrixFrame output);

		// Token: 0x02000158 RID: 344
		// (Invoke) Token: 0x06000A63 RID: 2659
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetNameDelegate(UIntPtr entityId);

		// Token: 0x02000159 RID: 345
		// (Invoke) Token: 0x06000A67 RID: 2663
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer GetNextEntityWithTagDelegate(UIntPtr currententityId, byte[] tag);

		// Token: 0x0200015A RID: 346
		// (Invoke) Token: 0x06000A6B RID: 2667
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer GetNextEntityWithTagExpressionDelegate(UIntPtr currententityId, byte[] tagExpression);

		// Token: 0x0200015B RID: 347
		// (Invoke) Token: 0x06000A6F RID: 2671
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer GetNextPrefabDelegate(UIntPtr currentPrefab);

		// Token: 0x0200015C RID: 348
		// (Invoke) Token: 0x06000A73 RID: 2675
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetOldPrefabNameDelegate(UIntPtr prefab);

		// Token: 0x0200015D RID: 349
		// (Invoke) Token: 0x06000A77 RID: 2679
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer GetParentDelegate(UIntPtr entityId);

		// Token: 0x0200015E RID: 350
		// (Invoke) Token: 0x06000A7B RID: 2683
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate Vec3 GetPhysicsBoundingBoxMaxDelegate(UIntPtr entityId);

		// Token: 0x0200015F RID: 351
		// (Invoke) Token: 0x06000A7F RID: 2687
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate Vec3 GetPhysicsBoundingBoxMinDelegate(UIntPtr entityId);

		// Token: 0x02000160 RID: 352
		// (Invoke) Token: 0x06000A83 RID: 2691
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate uint GetPhysicsDescBodyFlagsDelegate(UIntPtr entityId);

		// Token: 0x02000161 RID: 353
		// (Invoke) Token: 0x06000A87 RID: 2695
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetPhysicsMinMaxDelegate(UIntPtr entityId, [MarshalAs(UnmanagedType.U1)] bool includeChildren, ref Vec3 bbmin, ref Vec3 bbmax, [MarshalAs(UnmanagedType.U1)] bool returnLocal);

		// Token: 0x02000162 RID: 354
		// (Invoke) Token: 0x06000A8B RID: 2699
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool GetPhysicsStateDelegate(UIntPtr entityId);

		// Token: 0x02000163 RID: 355
		// (Invoke) Token: 0x06000A8F RID: 2703
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetPrefabNameDelegate(UIntPtr prefab);

		// Token: 0x02000164 RID: 356
		// (Invoke) Token: 0x06000A93 RID: 2707
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetPreviousGlobalFrameDelegate(UIntPtr entityPtr, out MatrixFrame frame);

		// Token: 0x02000165 RID: 357
		// (Invoke) Token: 0x06000A97 RID: 2711
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetQuickBoneEntitialFrameDelegate(UIntPtr entityId, sbyte index, ref MatrixFrame frame);

		// Token: 0x02000166 RID: 358
		// (Invoke) Token: 0x06000A9B RID: 2715
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetRadiusDelegate(UIntPtr entityId);

		// Token: 0x02000167 RID: 359
		// (Invoke) Token: 0x06000A9F RID: 2719
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer GetSceneDelegate(UIntPtr entityId);

		// Token: 0x02000168 RID: 360
		// (Invoke) Token: 0x06000AA3 RID: 2723
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate UIntPtr GetScenePointerDelegate(UIntPtr entityId);

		// Token: 0x02000169 RID: 361
		// (Invoke) Token: 0x06000AA7 RID: 2727
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetScriptComponentDelegate(UIntPtr entityId);

		// Token: 0x0200016A RID: 362
		// (Invoke) Token: 0x06000AAB RID: 2731
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetScriptComponentAtIndexDelegate(UIntPtr entityId, int index);

		// Token: 0x0200016B RID: 363
		// (Invoke) Token: 0x06000AAF RID: 2735
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetScriptComponentCountDelegate(UIntPtr entityId);

		// Token: 0x0200016C RID: 364
		// (Invoke) Token: 0x06000AB3 RID: 2739
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer GetSkeletonDelegate(UIntPtr entityId);

		// Token: 0x0200016D RID: 365
		// (Invoke) Token: 0x06000AB7 RID: 2743
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetTagsDelegate(UIntPtr entityId);

		// Token: 0x0200016E RID: 366
		// (Invoke) Token: 0x06000ABB RID: 2747
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate uint GetUpgradeLevelMaskDelegate(UIntPtr prefab);

		// Token: 0x0200016F RID: 367
		// (Invoke) Token: 0x06000ABF RID: 2751
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate uint GetUpgradeLevelMaskCumulativeDelegate(UIntPtr prefab);

		// Token: 0x02000170 RID: 368
		// (Invoke) Token: 0x06000AC3 RID: 2755
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool GetVisibilityExcludeParentsDelegate(UIntPtr entityId);

		// Token: 0x02000171 RID: 369
		// (Invoke) Token: 0x06000AC7 RID: 2759
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate uint GetVisibilityLevelMaskIncludingParentsDelegate(UIntPtr entityId);

		// Token: 0x02000172 RID: 370
		// (Invoke) Token: 0x06000ACB RID: 2763
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool HasBodyDelegate(UIntPtr entityId);

		// Token: 0x02000173 RID: 371
		// (Invoke) Token: 0x06000ACF RID: 2767
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool HasComplexAnimTreeDelegate(UIntPtr entityId);

		// Token: 0x02000174 RID: 372
		// (Invoke) Token: 0x06000AD3 RID: 2771
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool HasComponentDelegate(UIntPtr pointer, UIntPtr componentPointer);

		// Token: 0x02000175 RID: 373
		// (Invoke) Token: 0x06000AD7 RID: 2775
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool HasFrameChangedDelegate(UIntPtr entityId);

		// Token: 0x02000176 RID: 374
		// (Invoke) Token: 0x06000ADB RID: 2779
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool HasPhysicsBodyDelegate(UIntPtr entityId);

		// Token: 0x02000177 RID: 375
		// (Invoke) Token: 0x06000ADF RID: 2783
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool HasPhysicsDefinitionDelegate(UIntPtr entityId, int excludeFlags);

		// Token: 0x02000178 RID: 376
		// (Invoke) Token: 0x06000AE3 RID: 2787
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool HasSceneDelegate(UIntPtr entityId);

		// Token: 0x02000179 RID: 377
		// (Invoke) Token: 0x06000AE7 RID: 2791
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool HasScriptComponentDelegate(UIntPtr entityId, byte[] scName);

		// Token: 0x0200017A RID: 378
		// (Invoke) Token: 0x06000AEB RID: 2795
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool HasTagDelegate(UIntPtr entityId, byte[] tag);

		// Token: 0x0200017B RID: 379
		// (Invoke) Token: 0x06000AEF RID: 2799
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsDynamicBodyStationaryDelegate(UIntPtr entityId);

		// Token: 0x0200017C RID: 380
		// (Invoke) Token: 0x06000AF3 RID: 2803
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsEngineBodySleepingDelegate(UIntPtr entityId);

		// Token: 0x0200017D RID: 381
		// (Invoke) Token: 0x06000AF7 RID: 2807
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsEntitySelectedOnEditorDelegate(UIntPtr entityId);

		// Token: 0x0200017E RID: 382
		// (Invoke) Token: 0x06000AFB RID: 2811
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsFrozenDelegate(UIntPtr entityId);

		// Token: 0x0200017F RID: 383
		// (Invoke) Token: 0x06000AFF RID: 2815
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsGhostObjectDelegate(UIntPtr entityId);

		// Token: 0x02000180 RID: 384
		// (Invoke) Token: 0x06000B03 RID: 2819
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsGuidValidDelegate(UIntPtr entityId);

		// Token: 0x02000181 RID: 385
		// (Invoke) Token: 0x06000B07 RID: 2823
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsVisibleIncludeParentsDelegate(UIntPtr entityId);

		// Token: 0x02000182 RID: 386
		// (Invoke) Token: 0x06000B0B RID: 2827
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void PauseParticleSystemDelegate(UIntPtr entityId, [MarshalAs(UnmanagedType.U1)] bool doChildren);

		// Token: 0x02000183 RID: 387
		// (Invoke) Token: 0x06000B0F RID: 2831
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool PrefabExistsDelegate(byte[] prefabName);

		// Token: 0x02000184 RID: 388
		// (Invoke) Token: 0x06000B13 RID: 2835
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RecomputeBoundingBoxDelegate(UIntPtr entity);

		// Token: 0x02000185 RID: 389
		// (Invoke) Token: 0x06000B17 RID: 2839
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ReleaseEditDataUserToAllMeshesDelegate(UIntPtr entityId, [MarshalAs(UnmanagedType.U1)] bool entity_components, [MarshalAs(UnmanagedType.U1)] bool skeleton_components);

		// Token: 0x02000186 RID: 390
		// (Invoke) Token: 0x06000B1B RID: 2843
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RemoveDelegate(UIntPtr entityId, int removeReason);

		// Token: 0x02000187 RID: 391
		// (Invoke) Token: 0x06000B1F RID: 2847
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RemoveAllChildrenDelegate(UIntPtr entityId);

		// Token: 0x02000188 RID: 392
		// (Invoke) Token: 0x06000B23 RID: 2851
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RemoveAllParticleSystemsDelegate(UIntPtr entityId);

		// Token: 0x02000189 RID: 393
		// (Invoke) Token: 0x06000B27 RID: 2855
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RemoveChildDelegate(UIntPtr parentEntity, UIntPtr childEntity, [MarshalAs(UnmanagedType.U1)] bool keepPhysics, [MarshalAs(UnmanagedType.U1)] bool keepScenePointer, [MarshalAs(UnmanagedType.U1)] bool callScriptCallbacks, int removeReason);

		// Token: 0x0200018A RID: 394
		// (Invoke) Token: 0x06000B2B RID: 2859
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool RemoveComponentDelegate(UIntPtr pointer, UIntPtr componentPointer);

		// Token: 0x0200018B RID: 395
		// (Invoke) Token: 0x06000B2F RID: 2863
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool RemoveComponentWithMeshDelegate(UIntPtr entityId, UIntPtr mesh);

		// Token: 0x0200018C RID: 396
		// (Invoke) Token: 0x06000B33 RID: 2867
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RemoveEnginePhysicsDelegate(UIntPtr entityId);

		// Token: 0x0200018D RID: 397
		// (Invoke) Token: 0x06000B37 RID: 2871
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RemoveFromPredisplayEntityDelegate(UIntPtr entityId);

		// Token: 0x0200018E RID: 398
		// (Invoke) Token: 0x06000B3B RID: 2875
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool RemoveMultiMeshDelegate(UIntPtr entityId, UIntPtr multiMeshPtr);

		// Token: 0x0200018F RID: 399
		// (Invoke) Token: 0x06000B3F RID: 2879
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RemoveMultiMeshFromSkeletonDelegate(UIntPtr gameEntity, UIntPtr multiMesh);

		// Token: 0x02000190 RID: 400
		// (Invoke) Token: 0x06000B43 RID: 2883
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RemoveMultiMeshFromSkeletonBoneDelegate(UIntPtr gameEntity, UIntPtr multiMesh, sbyte boneIndex);

		// Token: 0x02000191 RID: 401
		// (Invoke) Token: 0x06000B47 RID: 2887
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RemovePhysicsDelegate(UIntPtr entityId, [MarshalAs(UnmanagedType.U1)] bool clearingTheScene);

		// Token: 0x02000192 RID: 402
		// (Invoke) Token: 0x06000B4B RID: 2891
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RemoveScriptComponentDelegate(UIntPtr entityId, UIntPtr scriptComponentPtr, int removeReason);

		// Token: 0x02000193 RID: 403
		// (Invoke) Token: 0x06000B4F RID: 2895
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RemoveTagDelegate(UIntPtr entityId, byte[] tag);

		// Token: 0x02000194 RID: 404
		// (Invoke) Token: 0x06000B53 RID: 2899
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ResumeParticleSystemDelegate(UIntPtr entityId, [MarshalAs(UnmanagedType.U1)] bool doChildren);

		// Token: 0x02000195 RID: 405
		// (Invoke) Token: 0x06000B57 RID: 2903
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SelectEntityOnEditorDelegate(UIntPtr entityId);

		// Token: 0x02000196 RID: 406
		// (Invoke) Token: 0x06000B5B RID: 2907
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetAlphaDelegate(UIntPtr entityId, float alpha);

		// Token: 0x02000197 RID: 407
		// (Invoke) Token: 0x06000B5F RID: 2911
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetAnimationSoundActivationDelegate(UIntPtr entityId, [MarshalAs(UnmanagedType.U1)] bool activate);

		// Token: 0x02000198 RID: 408
		// (Invoke) Token: 0x06000B63 RID: 2915
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetAnimTreeChannelParameterDelegate(UIntPtr entityId, float phase, int channel_no);

		// Token: 0x02000199 RID: 409
		// (Invoke) Token: 0x06000B67 RID: 2919
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetAsContourEntityDelegate(UIntPtr entityId, uint color);

		// Token: 0x0200019A RID: 410
		// (Invoke) Token: 0x06000B6B RID: 2923
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetAsPredisplayEntityDelegate(UIntPtr entityId);

		// Token: 0x0200019B RID: 411
		// (Invoke) Token: 0x06000B6F RID: 2927
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetAsReplayEntityDelegate(UIntPtr gameEntity);

		// Token: 0x0200019C RID: 412
		// (Invoke) Token: 0x06000B73 RID: 2931
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetBodyFlagsDelegate(UIntPtr entityId, uint bodyFlags);

		// Token: 0x0200019D RID: 413
		// (Invoke) Token: 0x06000B77 RID: 2935
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetBodyFlagsRecursiveDelegate(UIntPtr entityId, uint bodyFlags);

		// Token: 0x0200019E RID: 414
		// (Invoke) Token: 0x06000B7B RID: 2939
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetBodyShapeDelegate(UIntPtr entityId, UIntPtr shape);

		// Token: 0x0200019F RID: 415
		// (Invoke) Token: 0x06000B7F RID: 2943
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetBoundingboxDirtyDelegate(UIntPtr entityId);

		// Token: 0x020001A0 RID: 416
		// (Invoke) Token: 0x06000B83 RID: 2947
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetClothComponentKeepStateDelegate(UIntPtr entityId, UIntPtr metaMesh, [MarshalAs(UnmanagedType.U1)] bool keepState);

		// Token: 0x020001A1 RID: 417
		// (Invoke) Token: 0x06000B87 RID: 2951
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetClothComponentKeepStateOfAllMeshesDelegate(UIntPtr entityId, [MarshalAs(UnmanagedType.U1)] bool keepState);

		// Token: 0x020001A2 RID: 418
		// (Invoke) Token: 0x06000B8B RID: 2955
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetClothMaxDistanceMultiplierDelegate(UIntPtr gameEntity, float multiplier);

		// Token: 0x020001A3 RID: 419
		// (Invoke) Token: 0x06000B8F RID: 2959
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetContourStateDelegate(UIntPtr entityId, [MarshalAs(UnmanagedType.U1)] bool alwaysVisible);

		// Token: 0x020001A4 RID: 420
		// (Invoke) Token: 0x06000B93 RID: 2963
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetCullModeDelegate(UIntPtr entityPtr, MBMeshCullingMode cullMode);

		// Token: 0x020001A5 RID: 421
		// (Invoke) Token: 0x06000B97 RID: 2967
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetDampingDelegate(UIntPtr entityId, float linearDamping, float angularDamping);

		// Token: 0x020001A6 RID: 422
		// (Invoke) Token: 0x06000B9B RID: 2971
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetEnforcedMaximumLodLevelDelegate(UIntPtr entityId, int lodLevel);

		// Token: 0x020001A7 RID: 423
		// (Invoke) Token: 0x06000B9F RID: 2975
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetEntityEnvMapVisibilityDelegate(UIntPtr entityId, [MarshalAs(UnmanagedType.U1)] bool value);

		// Token: 0x020001A8 RID: 424
		// (Invoke) Token: 0x06000BA3 RID: 2979
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetEntityFlagsDelegate(UIntPtr entityId, uint entityFlags);

		// Token: 0x020001A9 RID: 425
		// (Invoke) Token: 0x06000BA7 RID: 2983
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetEntityVisibilityFlagsDelegate(UIntPtr entityId, uint entityVisibilityFlags);

		// Token: 0x020001AA RID: 426
		// (Invoke) Token: 0x06000BAB RID: 2987
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetExternalReferencesUsageDelegate(UIntPtr entityId, [MarshalAs(UnmanagedType.U1)] bool value);

		// Token: 0x020001AB RID: 427
		// (Invoke) Token: 0x06000BAF RID: 2991
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetFactor2ColorDelegate(UIntPtr entityId, uint factor2Color);

		// Token: 0x020001AC RID: 428
		// (Invoke) Token: 0x06000BB3 RID: 2995
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetFactorColorDelegate(UIntPtr entityId, uint factorColor);

		// Token: 0x020001AD RID: 429
		// (Invoke) Token: 0x06000BB7 RID: 2999
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetFrameDelegate(UIntPtr entityId, ref MatrixFrame frame);

		// Token: 0x020001AE RID: 430
		// (Invoke) Token: 0x06000BBB RID: 3003
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetFrameChangedDelegate(UIntPtr entityId);

		// Token: 0x020001AF RID: 431
		// (Invoke) Token: 0x06000BBF RID: 3007
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetGlobalFrameDelegate(UIntPtr entityId, in MatrixFrame frame);

		// Token: 0x020001B0 RID: 432
		// (Invoke) Token: 0x06000BC3 RID: 3011
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetLocalPositionDelegate(UIntPtr entityId, Vec3 position);

		// Token: 0x020001B1 RID: 433
		// (Invoke) Token: 0x06000BC7 RID: 3015
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetMassDelegate(UIntPtr entityId, float mass);

		// Token: 0x020001B2 RID: 434
		// (Invoke) Token: 0x06000BCB RID: 3019
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetMassSpaceInertiaDelegate(UIntPtr entityId, ref Vec3 inertia);

		// Token: 0x020001B3 RID: 435
		// (Invoke) Token: 0x06000BCF RID: 3023
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetMaterialForAllMeshesDelegate(UIntPtr entityId, UIntPtr materialPointer);

		// Token: 0x020001B4 RID: 436
		// (Invoke) Token: 0x06000BD3 RID: 3027
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetMobilityDelegate(UIntPtr entityId, int mobility);

		// Token: 0x020001B5 RID: 437
		// (Invoke) Token: 0x06000BD7 RID: 3031
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetMorphFrameOfComponentsDelegate(UIntPtr entityId, float value);

		// Token: 0x020001B6 RID: 438
		// (Invoke) Token: 0x06000BDB RID: 3035
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetNameDelegate(UIntPtr entityId, byte[] name);

		// Token: 0x020001B7 RID: 439
		// (Invoke) Token: 0x06000BDF RID: 3039
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetPhysicsStateDelegate(UIntPtr entityId, [MarshalAs(UnmanagedType.U1)] bool isEnabled, [MarshalAs(UnmanagedType.U1)] bool setChildren);

		// Token: 0x020001B8 RID: 440
		// (Invoke) Token: 0x06000BE3 RID: 3043
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetPreviousFrameInvalidDelegate(UIntPtr gameEntity);

		// Token: 0x020001B9 RID: 441
		// (Invoke) Token: 0x06000BE7 RID: 3047
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetReadyToRenderDelegate(UIntPtr entityId, [MarshalAs(UnmanagedType.U1)] bool ready);

		// Token: 0x020001BA RID: 442
		// (Invoke) Token: 0x06000BEB RID: 3051
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetRuntimeEmissionRateMultiplierDelegate(UIntPtr entityId, float emission_rate_multiplier);

		// Token: 0x020001BB RID: 443
		// (Invoke) Token: 0x06000BEF RID: 3055
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetSkeletonDelegate(UIntPtr entityId, UIntPtr skeletonPointer);

		// Token: 0x020001BC RID: 444
		// (Invoke) Token: 0x06000BF3 RID: 3059
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetUpgradeLevelMaskDelegate(UIntPtr prefab, uint mask);

		// Token: 0x020001BD RID: 445
		// (Invoke) Token: 0x06000BF7 RID: 3063
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetVectorArgumentDelegate(UIntPtr entityId, float vectorArgument0, float vectorArgument1, float vectorArgument2, float vectorArgument3);

		// Token: 0x020001BE RID: 446
		// (Invoke) Token: 0x06000BFB RID: 3067
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetVisibilityExcludeParentsDelegate(UIntPtr entityId, [MarshalAs(UnmanagedType.U1)] bool visibility);

		// Token: 0x020001BF RID: 447
		// (Invoke) Token: 0x06000BFF RID: 3071
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void UpdateGlobalBoundsDelegate(UIntPtr entityPointer);

		// Token: 0x020001C0 RID: 448
		// (Invoke) Token: 0x06000C03 RID: 3075
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void UpdateTriadFrameForEditorDelegate(UIntPtr meshPointer);

		// Token: 0x020001C1 RID: 449
		// (Invoke) Token: 0x06000C07 RID: 3079
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void UpdateVisibilityMaskDelegate(UIntPtr entityPtr);

		// Token: 0x020001C2 RID: 450
		// (Invoke) Token: 0x06000C0B RID: 3083
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ValidateBoundingBoxDelegate(UIntPtr entityPointer);
	}
}
