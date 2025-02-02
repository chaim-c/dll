using System;
using TaleWorlds.Core;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000198 RID: 408
	[EngineClass("Agent_visuals")]
	public sealed class MBAgentVisuals : NativeObject
	{
		// Token: 0x060014EA RID: 5354 RVA: 0x0004F21B File Offset: 0x0004D41B
		internal MBAgentVisuals(UIntPtr pointer)
		{
			base.Construct(pointer);
		}

		// Token: 0x060014EB RID: 5355 RVA: 0x0004F22A File Offset: 0x0004D42A
		private UIntPtr GetPtr()
		{
			return base.Pointer;
		}

		// Token: 0x060014EC RID: 5356 RVA: 0x0004F232 File Offset: 0x0004D432
		public static MBAgentVisuals CreateAgentVisuals(Scene scene, string ownerName, Vec3 eyeOffset)
		{
			return MBAPI.IMBAgentVisuals.CreateAgentVisuals(scene.Pointer, ownerName, eyeOffset);
		}

		// Token: 0x060014ED RID: 5357 RVA: 0x0004F246 File Offset: 0x0004D446
		public void Tick(MBAgentVisuals parentAgentVisuals, float dt, bool entityMoving, float speed)
		{
			MBAPI.IMBAgentVisuals.Tick(this.GetPtr(), (parentAgentVisuals != null) ? parentAgentVisuals.GetPtr() : UIntPtr.Zero, dt, entityMoving, speed);
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x0004F26C File Offset: 0x0004D46C
		public MatrixFrame GetGlobalFrame()
		{
			MatrixFrame result = default(MatrixFrame);
			MBAPI.IMBAgentVisuals.GetGlobalFrame(this.GetPtr(), ref result);
			return result;
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x0004F294 File Offset: 0x0004D494
		public MatrixFrame GetFrame()
		{
			MatrixFrame result = default(MatrixFrame);
			MBAPI.IMBAgentVisuals.GetFrame(this.GetPtr(), ref result);
			return result;
		}

		// Token: 0x060014F0 RID: 5360 RVA: 0x0004F2BC File Offset: 0x0004D4BC
		public GameEntity GetEntity()
		{
			return MBAPI.IMBAgentVisuals.GetEntity(this.GetPtr());
		}

		// Token: 0x060014F1 RID: 5361 RVA: 0x0004F2CE File Offset: 0x0004D4CE
		public bool IsValid()
		{
			return MBAPI.IMBAgentVisuals.IsValid(this.GetPtr());
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x0004F2E0 File Offset: 0x0004D4E0
		public Vec3 GetGlobalStableEyePoint(bool isHumanoid)
		{
			return MBAPI.IMBAgentVisuals.GetGlobalStableEyePoint(this.GetPtr(), isHumanoid);
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x0004F2F3 File Offset: 0x0004D4F3
		public Vec3 GetGlobalStableNeckPoint(bool isHumanoid)
		{
			return MBAPI.IMBAgentVisuals.GetGlobalStableNeckPoint(this.GetPtr(), isHumanoid);
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x0004F308 File Offset: 0x0004D508
		public MatrixFrame GetBoneEntitialFrame(sbyte bone, bool useBoneMapping)
		{
			MatrixFrame result = default(MatrixFrame);
			MBAPI.IMBAgentVisuals.GetBoneEntitialFrame(base.Pointer, bone, useBoneMapping, ref result);
			return result;
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x0004F332 File Offset: 0x0004D532
		public RagdollState GetCurrentRagdollState()
		{
			return MBAPI.IMBAgentVisuals.GetCurrentRagdollState(base.Pointer);
		}

		// Token: 0x060014F6 RID: 5366 RVA: 0x0004F344 File Offset: 0x0004D544
		public sbyte GetRealBoneIndex(HumanBone boneType)
		{
			return MBAPI.IMBAgentVisuals.GetRealBoneIndex(this.GetPtr(), boneType);
		}

		// Token: 0x060014F7 RID: 5367 RVA: 0x0004F357 File Offset: 0x0004D557
		public CompositeComponent AddPrefabToAgentVisualBoneByBoneType(string prefabName, HumanBone boneType)
		{
			return MBAPI.IMBAgentVisuals.AddPrefabToAgentVisualBoneByBoneType(this.GetPtr(), prefabName, boneType);
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x0004F36B File Offset: 0x0004D56B
		public CompositeComponent AddPrefabToAgentVisualBoneByRealBoneIndex(string prefabName, sbyte realBoneIndex)
		{
			return MBAPI.IMBAgentVisuals.AddPrefabToAgentVisualBoneByRealBoneIndex(this.GetPtr(), prefabName, realBoneIndex);
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x0004F37F File Offset: 0x0004D57F
		public GameEntity GetAttachedWeaponEntity(int attachedWeaponIndex)
		{
			return MBAPI.IMBAgentVisuals.GetAttachedWeaponEntity(this.GetPtr(), attachedWeaponIndex);
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x0004F392 File Offset: 0x0004D592
		public void SetFrame(ref MatrixFrame frame)
		{
			MBAPI.IMBAgentVisuals.SetFrame(this.GetPtr(), ref frame);
		}

		// Token: 0x060014FB RID: 5371 RVA: 0x0004F3A5 File Offset: 0x0004D5A5
		public void SetEntity(GameEntity value)
		{
			MBAPI.IMBAgentVisuals.SetEntity(this.GetPtr(), value.Pointer);
		}

		// Token: 0x060014FC RID: 5372 RVA: 0x0004F3BD File Offset: 0x0004D5BD
		public static void FillEntityWithBodyMeshesWithoutAgentVisuals(GameEntity entity, SkinGenerationParams skinParams, BodyProperties bodyProperties, MetaMesh glovesMesh)
		{
			MBAPI.IMBAgentVisuals.FillEntityWithBodyMeshesWithoutAgentVisuals(entity.Pointer, ref skinParams, ref bodyProperties, glovesMesh);
		}

		// Token: 0x060014FD RID: 5373 RVA: 0x0004F3D4 File Offset: 0x0004D5D4
		public BoneBodyTypeData GetBoneTypeData(sbyte boneIndex)
		{
			BoneBodyTypeData result = default(BoneBodyTypeData);
			MBAPI.IMBAgentVisuals.GetBoneTypeData(base.Pointer, boneIndex, ref result);
			return result;
		}

		// Token: 0x060014FE RID: 5374 RVA: 0x0004F3FD File Offset: 0x0004D5FD
		public Skeleton GetSkeleton()
		{
			return MBAPI.IMBAgentVisuals.GetSkeleton(this.GetPtr());
		}

		// Token: 0x060014FF RID: 5375 RVA: 0x0004F40F File Offset: 0x0004D60F
		public void SetSkeleton(Skeleton newSkeleton)
		{
			MBAPI.IMBAgentVisuals.SetSkeleton(this.GetPtr(), newSkeleton.Pointer);
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x0004F428 File Offset: 0x0004D628
		public void CreateParticleSystemAttachedToBone(string particleName, sbyte boneIndex, ref MatrixFrame boneLocalParticleFrame)
		{
			int runtimeIdByName = ParticleSystemManager.GetRuntimeIdByName(particleName);
			this.CreateParticleSystemAttachedToBone(runtimeIdByName, boneIndex, ref boneLocalParticleFrame);
		}

		// Token: 0x06001501 RID: 5377 RVA: 0x0004F445 File Offset: 0x0004D645
		public void CreateParticleSystemAttachedToBone(int runtimeParticleindex, sbyte boneIndex, ref MatrixFrame boneLocalParticleFrame)
		{
			MBAPI.IMBAgentVisuals.CreateParticleSystemAttachedToBone(this.GetPtr(), runtimeParticleindex, boneIndex, ref boneLocalParticleFrame);
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x0004F45A File Offset: 0x0004D65A
		public void SetVisible(bool value)
		{
			MBAPI.IMBAgentVisuals.SetVisible(this.GetPtr(), value);
		}

		// Token: 0x06001503 RID: 5379 RVA: 0x0004F46D File Offset: 0x0004D66D
		public bool GetVisible()
		{
			return MBAPI.IMBAgentVisuals.GetVisible(this.GetPtr());
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x0004F47F File Offset: 0x0004D67F
		public void AddChildEntity(GameEntity entity)
		{
			MBAPI.IMBAgentVisuals.AddChildEntity(this.GetPtr(), entity.Pointer);
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x0004F498 File Offset: 0x0004D698
		public void SetClothWindToWeaponAtIndex(Vec3 windDirection, bool isLocal, EquipmentIndex weaponIndex)
		{
			MBAPI.IMBAgentVisuals.SetClothWindToWeaponAtIndex(this.GetPtr(), windDirection, isLocal, (int)weaponIndex);
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x0004F4AD File Offset: 0x0004D6AD
		public void RemoveChildEntity(GameEntity entity, int removeReason)
		{
			MBAPI.IMBAgentVisuals.RemoveChildEntity(this.GetPtr(), entity.Pointer, removeReason);
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x0004F4C6 File Offset: 0x0004D6C6
		public bool CheckResources(bool addToQueue)
		{
			return MBAPI.IMBAgentVisuals.CheckResources(this.GetPtr(), addToQueue);
		}

		// Token: 0x06001508 RID: 5384 RVA: 0x0004F4D9 File Offset: 0x0004D6D9
		public void AddSkinMeshes(SkinGenerationParams skinParams, BodyProperties bodyProperties, bool useGPUMorph, bool useFaceCache)
		{
			MBAPI.IMBAgentVisuals.AddSkinMeshesToAgentEntity(this.GetPtr(), ref skinParams, ref bodyProperties, useGPUMorph, useFaceCache);
		}

		// Token: 0x06001509 RID: 5385 RVA: 0x0004F4F2 File Offset: 0x0004D6F2
		public void SetFaceGenerationParams(FaceGenerationParams faceGenerationParams)
		{
			MBAPI.IMBAgentVisuals.SetFaceGenerationParams(this.GetPtr(), faceGenerationParams);
		}

		// Token: 0x0600150A RID: 5386 RVA: 0x0004F505 File Offset: 0x0004D705
		public void SetLodAtlasShadingIndex(int index, bool useTeamColor, uint teamColor1, uint teamColor2)
		{
			MBAPI.IMBAgentVisuals.SetLodAtlasShadingIndex(this.GetPtr(), index, useTeamColor, teamColor1, teamColor2);
		}

		// Token: 0x0600150B RID: 5387 RVA: 0x0004F51C File Offset: 0x0004D71C
		public void ClearVisualComponents(bool removeSkeleton)
		{
			MBAPI.IMBAgentVisuals.ClearVisualComponents(this.GetPtr(), removeSkeleton);
		}

		// Token: 0x0600150C RID: 5388 RVA: 0x0004F52F File Offset: 0x0004D72F
		public void LazyUpdateAgentRendererData()
		{
			MBAPI.IMBAgentVisuals.LazyUpdateAgentRendererData(this.GetPtr());
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x0004F541 File Offset: 0x0004D741
		public void AddMultiMesh(MetaMesh metaMesh, BodyMeshTypes bodyMeshIndex)
		{
			MBAPI.IMBAgentVisuals.AddMultiMesh(this.GetPtr(), metaMesh.Pointer, (int)bodyMeshIndex);
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x0004F55A File Offset: 0x0004D75A
		public void ApplySkeletonScale(Vec3 mountSitBoneScale, float mountRadiusAdder, sbyte[] boneIndices, Vec3[] boneScales)
		{
			MBAPI.IMBAgentVisuals.ApplySkeletonScale(base.Pointer, mountSitBoneScale, mountRadiusAdder, (byte)boneIndices.Length, boneIndices, boneScales);
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x0004F575 File Offset: 0x0004D775
		public void UpdateSkeletonScale(int bodyDeformType)
		{
			MBAPI.IMBAgentVisuals.UpdateSkeletonScale(base.Pointer, bodyDeformType);
		}

		// Token: 0x06001510 RID: 5392 RVA: 0x0004F588 File Offset: 0x0004D788
		public void AddHorseReinsClothMesh(MetaMesh reinMesh, MetaMesh ropeMesh)
		{
			MBAPI.IMBAgentVisuals.AddHorseReinsClothMesh(base.Pointer, reinMesh.Pointer, ropeMesh.Pointer);
		}

		// Token: 0x06001511 RID: 5393 RVA: 0x0004F5A6 File Offset: 0x0004D7A6
		public void BatchLastLodMeshes()
		{
			MBAPI.IMBAgentVisuals.BatchLastLodMeshes(this.GetPtr());
		}

		// Token: 0x06001512 RID: 5394 RVA: 0x0004F5B8 File Offset: 0x0004D7B8
		public void AddWeaponToAgentEntity(int slotIndex, in WeaponData weaponData, WeaponStatsData[] weaponStatsData, in WeaponData ammoWeaponData, WeaponStatsData[] ammoWeaponStatsData, GameEntity cachedEntity)
		{
			MBAPI.IMBAgentVisuals.AddWeaponToAgentEntity(this.GetPtr(), slotIndex, weaponData, weaponStatsData, weaponStatsData.Length, ammoWeaponData, ammoWeaponStatsData, ammoWeaponStatsData.Length, cachedEntity);
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x0004F5E5 File Offset: 0x0004D7E5
		public void UpdateQuiverMeshesWithoutAgent(int weaponIndex, int ammoCount)
		{
			MBAPI.IMBAgentVisuals.UpdateQuiverMeshesWithoutAgent(this.GetPtr(), weaponIndex, ammoCount);
		}

		// Token: 0x06001514 RID: 5396 RVA: 0x0004F5F9 File Offset: 0x0004D7F9
		public void SetWieldedWeaponIndices(int slotIndexRightHand, int slotIndexLeftHand)
		{
			MBAPI.IMBAgentVisuals.SetWieldedWeaponIndices(this.GetPtr(), slotIndexRightHand, slotIndexLeftHand);
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x0004F60D File Offset: 0x0004D80D
		public void ClearAllWeaponMeshes()
		{
			MBAPI.IMBAgentVisuals.ClearAllWeaponMeshes(this.GetPtr());
		}

		// Token: 0x06001516 RID: 5398 RVA: 0x0004F61F File Offset: 0x0004D81F
		public void ClearWeaponMeshes(EquipmentIndex index)
		{
			MBAPI.IMBAgentVisuals.ClearWeaponMeshes(this.GetPtr(), (int)index);
		}

		// Token: 0x06001517 RID: 5399 RVA: 0x0004F632 File Offset: 0x0004D832
		public void MakeVoice(int voiceId, Vec3 position)
		{
			MBAPI.IMBAgentVisuals.MakeVoice(this.GetPtr(), voiceId, ref position);
		}

		// Token: 0x06001518 RID: 5400 RVA: 0x0004F647 File Offset: 0x0004D847
		public void SetSetupMorphNode(bool value)
		{
			MBAPI.IMBAgentVisuals.SetSetupMorphNode(this.GetPtr(), value);
		}

		// Token: 0x06001519 RID: 5401 RVA: 0x0004F65A File Offset: 0x0004D85A
		public void UseScaledWeapons(bool value)
		{
			MBAPI.IMBAgentVisuals.UseScaledWeapons(this.GetPtr(), value);
		}

		// Token: 0x0600151A RID: 5402 RVA: 0x0004F66D File Offset: 0x0004D86D
		public void SetClothComponentKeepStateOfAllMeshes(bool keepState)
		{
			MBAPI.IMBAgentVisuals.SetClothComponentKeepStateOfAllMeshes(this.GetPtr(), keepState);
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x0004F680 File Offset: 0x0004D880
		public MatrixFrame GetFacegenScalingMatrix()
		{
			MatrixFrame identity = MatrixFrame.Identity;
			Vec3 currentHelmetScalingFactor = MBAPI.IMBAgentVisuals.GetCurrentHelmetScalingFactor(this.GetPtr());
			identity.rotation.ApplyScaleLocal(currentHelmetScalingFactor);
			return identity;
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x0004F6B4 File Offset: 0x0004D8B4
		public void ReplaceMeshWithMesh(MetaMesh oldMetaMesh, MetaMesh newMetaMesh, BodyMeshTypes bodyMeshIndex)
		{
			if (oldMetaMesh != null)
			{
				MBAPI.IMBAgentVisuals.RemoveMultiMesh(this.GetPtr(), oldMetaMesh.Pointer, (int)bodyMeshIndex);
			}
			if (newMetaMesh != null)
			{
				MBAPI.IMBAgentVisuals.AddMultiMesh(this.GetPtr(), newMetaMesh.Pointer, (int)bodyMeshIndex);
			}
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x0004F701 File Offset: 0x0004D901
		public void SetAgentActionChannel(int actionChannelNo, int actionIndex, float channelParameter = 0f, float blendPeriodOverride = -0.2f, bool forceFaceMorphRestart = true)
		{
			MBAPI.IMBSkeletonExtensions.SetAgentActionChannel(this.GetSkeleton().Pointer, actionChannelNo, actionIndex, channelParameter, blendPeriodOverride, forceFaceMorphRestart);
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x0004F71F File Offset: 0x0004D91F
		public void SetVoiceDefinitionIndex(int voiceDefinitionIndex, float voicePitch)
		{
			MBAPI.IMBAgentVisuals.SetVoiceDefinitionIndex(this.GetPtr(), voiceDefinitionIndex, voicePitch);
		}

		// Token: 0x0600151F RID: 5407 RVA: 0x0004F733 File Offset: 0x0004D933
		public void StartRhubarbRecord(string path, int soundId)
		{
			MBAPI.IMBAgentVisuals.StartRhubarbRecord(this.GetPtr(), path, soundId);
		}

		// Token: 0x06001520 RID: 5408 RVA: 0x0004F748 File Offset: 0x0004D948
		public void SetContourColor(uint? color, bool alwaysVisible = true)
		{
			if (color != null)
			{
				MBAPI.IMBAgentVisuals.SetAsContourEntity(this.GetPtr(), color.Value);
				MBAPI.IMBAgentVisuals.SetContourState(this.GetPtr(), alwaysVisible);
				return;
			}
			MBAPI.IMBAgentVisuals.DisableContour(this.GetPtr());
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x0004F797 File Offset: 0x0004D997
		public void SetEnableOcclusionCulling(bool enable)
		{
			MBAPI.IMBAgentVisuals.SetEnableOcclusionCulling(this.GetPtr(), enable);
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x0004F7AA File Offset: 0x0004D9AA
		public void SetAgentLodZeroOrMax(bool makeZero)
		{
			MBAPI.IMBAgentVisuals.SetAgentLodMakeZeroOrMax(this.GetPtr(), makeZero);
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x0004F7BD File Offset: 0x0004D9BD
		public void SetAgentLocalSpeed(Vec2 speed)
		{
			MBAPI.IMBAgentVisuals.SetAgentLocalSpeed(this.GetPtr(), speed);
		}

		// Token: 0x06001524 RID: 5412 RVA: 0x0004F7D0 File Offset: 0x0004D9D0
		public void SetLookDirection(Vec3 direction)
		{
			MBAPI.IMBAgentVisuals.SetLookDirection(this.GetPtr(), direction);
		}

		// Token: 0x06001525 RID: 5413 RVA: 0x0004F7E4 File Offset: 0x0004D9E4
		public static BodyMeshTypes GetBodyMeshIndex(EquipmentIndex equipmentIndex)
		{
			switch (equipmentIndex)
			{
			case EquipmentIndex.NumAllWeaponSlots:
				return BodyMeshTypes.Cap;
			case EquipmentIndex.Body:
				return BodyMeshTypes.Chestpiece;
			case EquipmentIndex.Leg:
				return BodyMeshTypes.Footwear;
			case EquipmentIndex.Gloves:
				return BodyMeshTypes.Gloves;
			case EquipmentIndex.Cape:
				return BodyMeshTypes.Shoulderpiece;
			default:
				Debug.FailedAssert("false", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Base\\MBAgentVisuals.cs", "GetBodyMeshIndex", 393);
				return BodyMeshTypes.Invalid;
			}
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x0004F836 File Offset: 0x0004DA36
		public void Reset()
		{
			MBAPI.IMBAgentVisuals.Reset(this.GetPtr());
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x0004F848 File Offset: 0x0004DA48
		public void ResetNextFrame()
		{
			MBAPI.IMBAgentVisuals.ResetNextFrame(this.GetPtr());
		}
	}
}
