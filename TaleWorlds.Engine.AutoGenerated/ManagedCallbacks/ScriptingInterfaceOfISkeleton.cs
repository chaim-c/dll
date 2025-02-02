using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace ManagedCallbacks
{
	// Token: 0x02000027 RID: 39
	internal class ScriptingInterfaceOfISkeleton : ISkeleton
	{
		// Token: 0x06000476 RID: 1142 RVA: 0x00014BEC File Offset: 0x00012DEC
		public void ActivateRagdoll(UIntPtr skeletonPointer)
		{
			ScriptingInterfaceOfISkeleton.call_ActivateRagdollDelegate(skeletonPointer);
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00014BF9 File Offset: 0x00012DF9
		public void AddComponent(UIntPtr skeletonPointer, UIntPtr componentPointer)
		{
			ScriptingInterfaceOfISkeleton.call_AddComponentDelegate(skeletonPointer, componentPointer);
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00014C08 File Offset: 0x00012E08
		public void AddComponentToBone(UIntPtr skeletonPointer, sbyte boneIndex, GameEntityComponent component)
		{
			UIntPtr component2 = (component != null) ? component.Pointer : UIntPtr.Zero;
			ScriptingInterfaceOfISkeleton.call_AddComponentToBoneDelegate(skeletonPointer, boneIndex, component2);
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00014C39 File Offset: 0x00012E39
		public void AddMesh(UIntPtr skeletonPointer, UIntPtr mesnPointer)
		{
			ScriptingInterfaceOfISkeleton.call_AddMeshDelegate(skeletonPointer, mesnPointer);
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00014C47 File Offset: 0x00012E47
		public void AddMeshToBone(UIntPtr skeletonPointer, UIntPtr multiMeshPointer, sbyte bone_index)
		{
			ScriptingInterfaceOfISkeleton.call_AddMeshToBoneDelegate(skeletonPointer, multiMeshPointer, bone_index);
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00014C58 File Offset: 0x00012E58
		public void AddPrefabEntityToBone(UIntPtr skeletonPointer, string prefab_name, sbyte boneIndex)
		{
			byte[] array = null;
			if (prefab_name != null)
			{
				int byteCount = ScriptingInterfaceOfISkeleton._utf8.GetByteCount(prefab_name);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfISkeleton._utf8.GetBytes(prefab_name, 0, prefab_name.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfISkeleton.call_AddPrefabEntityToBoneDelegate(skeletonPointer, array, boneIndex);
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00014CB4 File Offset: 0x00012EB4
		public void ClearComponents(UIntPtr skeletonPointer)
		{
			ScriptingInterfaceOfISkeleton.call_ClearComponentsDelegate(skeletonPointer);
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00014CC1 File Offset: 0x00012EC1
		public void ClearMeshes(UIntPtr skeletonPointer, bool clearBoneComponents)
		{
			ScriptingInterfaceOfISkeleton.call_ClearMeshesDelegate(skeletonPointer, clearBoneComponents);
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00014CCF File Offset: 0x00012ECF
		public void ClearMeshesAtBone(UIntPtr skeletonPointer, sbyte boneIndex)
		{
			ScriptingInterfaceOfISkeleton.call_ClearMeshesAtBoneDelegate(skeletonPointer, boneIndex);
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00014CE0 File Offset: 0x00012EE0
		public Skeleton CreateFromModel(string skeletonModelName)
		{
			byte[] array = null;
			if (skeletonModelName != null)
			{
				int byteCount = ScriptingInterfaceOfISkeleton._utf8.GetByteCount(skeletonModelName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfISkeleton._utf8.GetBytes(skeletonModelName, 0, skeletonModelName.Length, array, 0);
				array[byteCount] = 0;
			}
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfISkeleton.call_CreateFromModelDelegate(array);
			Skeleton result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new Skeleton(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00014D6C File Offset: 0x00012F6C
		public Skeleton CreateFromModelWithNullAnimTree(UIntPtr entityPointer, string skeletonModelName, float scale)
		{
			byte[] array = null;
			if (skeletonModelName != null)
			{
				int byteCount = ScriptingInterfaceOfISkeleton._utf8.GetByteCount(skeletonModelName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfISkeleton._utf8.GetBytes(skeletonModelName, 0, skeletonModelName.Length, array, 0);
				array[byteCount] = 0;
			}
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfISkeleton.call_CreateFromModelWithNullAnimTreeDelegate(entityPointer, array, scale);
			Skeleton result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new Skeleton(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00014DFA File Offset: 0x00012FFA
		public void ForceUpdateBoneFrames(UIntPtr skeletonPointer)
		{
			ScriptingInterfaceOfISkeleton.call_ForceUpdateBoneFramesDelegate(skeletonPointer);
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00014E07 File Offset: 0x00013007
		public void Freeze(UIntPtr skeletonPointer, bool isFrozen)
		{
			ScriptingInterfaceOfISkeleton.call_FreezeDelegate(skeletonPointer, isFrozen);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00014E18 File Offset: 0x00013018
		public void GetAllMeshes(Skeleton skeleton, NativeObjectArray nativeObjectArray)
		{
			UIntPtr skeleton2 = (skeleton != null) ? skeleton.Pointer : UIntPtr.Zero;
			UIntPtr nativeObjectArray2 = (nativeObjectArray != null) ? nativeObjectArray.Pointer : UIntPtr.Zero;
			ScriptingInterfaceOfISkeleton.call_GetAllMeshesDelegate(skeleton2, nativeObjectArray2);
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00014E5F File Offset: 0x0001305F
		public string GetAnimationAtChannel(UIntPtr skeletonPointer, int channelNo)
		{
			if (ScriptingInterfaceOfISkeleton.call_GetAnimationAtChannelDelegate(skeletonPointer, channelNo) != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00014E77 File Offset: 0x00013077
		public int GetAnimationIndexAtChannel(UIntPtr skeletonPointer, int channelNo)
		{
			return ScriptingInterfaceOfISkeleton.call_GetAnimationIndexAtChannelDelegate(skeletonPointer, channelNo);
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00014E85 File Offset: 0x00013085
		public void GetBoneBody(UIntPtr skeletonPointer, sbyte boneIndex, ref CapsuleData data)
		{
			ScriptingInterfaceOfISkeleton.call_GetBoneBodyDelegate(skeletonPointer, boneIndex, ref data);
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00014E94 File Offset: 0x00013094
		public sbyte GetBoneChildAtIndex(Skeleton skeleton, sbyte boneIndex, sbyte childIndex)
		{
			UIntPtr skeleton2 = (skeleton != null) ? skeleton.Pointer : UIntPtr.Zero;
			return ScriptingInterfaceOfISkeleton.call_GetBoneChildAtIndexDelegate(skeleton2, boneIndex, childIndex);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00014EC8 File Offset: 0x000130C8
		public sbyte GetBoneChildCount(Skeleton skeleton, sbyte boneIndex)
		{
			UIntPtr skeleton2 = (skeleton != null) ? skeleton.Pointer : UIntPtr.Zero;
			return ScriptingInterfaceOfISkeleton.call_GetBoneChildCountDelegate(skeleton2, boneIndex);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00014EF8 File Offset: 0x000130F8
		public GameEntityComponent GetBoneComponentAtIndex(UIntPtr skeletonPointer, sbyte boneIndex, int componentIndex)
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfISkeleton.call_GetBoneComponentAtIndexDelegate(skeletonPointer, boneIndex, componentIndex);
			GameEntityComponent result = NativeObject.CreateNativeObjectWrapper<GameEntityComponent>(nativeObjectPointer);
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00014F3B File Offset: 0x0001313B
		public int GetBoneComponentCount(UIntPtr skeletonPointer, sbyte boneIndex)
		{
			return ScriptingInterfaceOfISkeleton.call_GetBoneComponentCountDelegate(skeletonPointer, boneIndex);
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00014F49 File Offset: 0x00013149
		public sbyte GetBoneCount(UIntPtr skeletonPointer)
		{
			return ScriptingInterfaceOfISkeleton.call_GetBoneCountDelegate(skeletonPointer);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00014F56 File Offset: 0x00013156
		public void GetBoneEntitialFrame(UIntPtr skeletonPointer, sbyte boneIndex, ref MatrixFrame outFrame)
		{
			ScriptingInterfaceOfISkeleton.call_GetBoneEntitialFrameDelegate(skeletonPointer, boneIndex, ref outFrame);
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00014F65 File Offset: 0x00013165
		public void GetBoneEntitialFrameAtChannel(UIntPtr skeletonPointer, int channelNo, sbyte boneIndex, ref MatrixFrame outFrame)
		{
			ScriptingInterfaceOfISkeleton.call_GetBoneEntitialFrameAtChannelDelegate(skeletonPointer, channelNo, boneIndex, ref outFrame);
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00014F76 File Offset: 0x00013176
		public void GetBoneEntitialFrameWithIndex(UIntPtr skeletonPointer, sbyte boneIndex, ref MatrixFrame outEntitialFrame)
		{
			ScriptingInterfaceOfISkeleton.call_GetBoneEntitialFrameWithIndexDelegate(skeletonPointer, boneIndex, ref outEntitialFrame);
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00014F88 File Offset: 0x00013188
		public void GetBoneEntitialFrameWithName(UIntPtr skeletonPointer, string boneName, ref MatrixFrame outEntitialFrame)
		{
			byte[] array = null;
			if (boneName != null)
			{
				int byteCount = ScriptingInterfaceOfISkeleton._utf8.GetByteCount(boneName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfISkeleton._utf8.GetBytes(boneName, 0, boneName.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfISkeleton.call_GetBoneEntitialFrameWithNameDelegate(skeletonPointer, array, ref outEntitialFrame);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00014FE4 File Offset: 0x000131E4
		public void GetBoneEntitialRestFrame(UIntPtr skeletonPointer, sbyte boneIndex, bool useBoneMapping, ref MatrixFrame outFrame)
		{
			ScriptingInterfaceOfISkeleton.call_GetBoneEntitialRestFrameDelegate(skeletonPointer, boneIndex, useBoneMapping, ref outFrame);
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00014FF8 File Offset: 0x000131F8
		public sbyte GetBoneIndexFromName(string skeletonModelName, string boneName)
		{
			byte[] array = null;
			if (skeletonModelName != null)
			{
				int byteCount = ScriptingInterfaceOfISkeleton._utf8.GetByteCount(skeletonModelName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfISkeleton._utf8.GetBytes(skeletonModelName, 0, skeletonModelName.Length, array, 0);
				array[byteCount] = 0;
			}
			byte[] array2 = null;
			if (boneName != null)
			{
				int byteCount2 = ScriptingInterfaceOfISkeleton._utf8.GetByteCount(boneName);
				array2 = ((byteCount2 < 1024) ? CallbackStringBufferManager.StringBuffer1 : new byte[byteCount2 + 1]);
				ScriptingInterfaceOfISkeleton._utf8.GetBytes(boneName, 0, boneName.Length, array2, 0);
				array2[byteCount2] = 0;
			}
			return ScriptingInterfaceOfISkeleton.call_GetBoneIndexFromNameDelegate(array, array2);
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00015095 File Offset: 0x00013295
		public void GetBoneLocalRestFrame(UIntPtr skeletonPointer, sbyte boneIndex, bool useBoneMapping, ref MatrixFrame outFrame)
		{
			ScriptingInterfaceOfISkeleton.call_GetBoneLocalRestFrameDelegate(skeletonPointer, boneIndex, useBoneMapping, ref outFrame);
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x000150A8 File Offset: 0x000132A8
		public string GetBoneName(Skeleton skeleton, sbyte boneIndex)
		{
			UIntPtr skeleton2 = (skeleton != null) ? skeleton.Pointer : UIntPtr.Zero;
			if (ScriptingInterfaceOfISkeleton.call_GetBoneNameDelegate(skeleton2, boneIndex) != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x000150E4 File Offset: 0x000132E4
		public GameEntityComponent GetComponentAtIndex(UIntPtr skeletonPointer, GameEntity.ComponentType componentType, int index)
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfISkeleton.call_GetComponentAtIndexDelegate(skeletonPointer, componentType, index);
			GameEntityComponent result = NativeObject.CreateNativeObjectWrapper<GameEntityComponent>(nativeObjectPointer);
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00015127 File Offset: 0x00013327
		public int GetComponentCount(UIntPtr skeletonPointer, GameEntity.ComponentType componentType)
		{
			return ScriptingInterfaceOfISkeleton.call_GetComponentCountDelegate(skeletonPointer, componentType);
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00015135 File Offset: 0x00013335
		public RagdollState GetCurrentRagdollState(UIntPtr skeletonPointer)
		{
			return ScriptingInterfaceOfISkeleton.call_GetCurrentRagdollStateDelegate(skeletonPointer);
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00015144 File Offset: 0x00013344
		public string GetName(Skeleton skeleton)
		{
			UIntPtr skeleton2 = (skeleton != null) ? skeleton.Pointer : UIntPtr.Zero;
			if (ScriptingInterfaceOfISkeleton.call_GetNameDelegate(skeleton2) != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00015180 File Offset: 0x00013380
		public sbyte GetParentBoneIndex(Skeleton skeleton, sbyte boneIndex)
		{
			UIntPtr skeleton2 = (skeleton != null) ? skeleton.Pointer : UIntPtr.Zero;
			return ScriptingInterfaceOfISkeleton.call_GetParentBoneIndexDelegate(skeleton2, boneIndex);
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x000151B0 File Offset: 0x000133B0
		public float GetSkeletonAnimationParameterAtChannel(UIntPtr skeletonPointer, int channelNo)
		{
			return ScriptingInterfaceOfISkeleton.call_GetSkeletonAnimationParameterAtChannelDelegate(skeletonPointer, channelNo);
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x000151BE File Offset: 0x000133BE
		public float GetSkeletonAnimationSpeedAtChannel(UIntPtr skeletonPointer, int channelNo)
		{
			return ScriptingInterfaceOfISkeleton.call_GetSkeletonAnimationSpeedAtChannelDelegate(skeletonPointer, channelNo);
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x000151CC File Offset: 0x000133CC
		public sbyte GetSkeletonBoneMapping(UIntPtr skeletonPointer, sbyte boneIndex)
		{
			return ScriptingInterfaceOfISkeleton.call_GetSkeletonBoneMappingDelegate(skeletonPointer, boneIndex);
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x000151DC File Offset: 0x000133DC
		public bool HasBoneComponent(UIntPtr skeletonPointer, sbyte boneIndex, GameEntityComponent component)
		{
			UIntPtr component2 = (component != null) ? component.Pointer : UIntPtr.Zero;
			return ScriptingInterfaceOfISkeleton.call_HasBoneComponentDelegate(skeletonPointer, boneIndex, component2);
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0001520D File Offset: 0x0001340D
		public bool HasComponent(UIntPtr skeletonPointer, UIntPtr componentPointer)
		{
			return ScriptingInterfaceOfISkeleton.call_HasComponentDelegate(skeletonPointer, componentPointer);
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0001521B File Offset: 0x0001341B
		public bool IsFrozen(UIntPtr skeletonPointer)
		{
			return ScriptingInterfaceOfISkeleton.call_IsFrozenDelegate(skeletonPointer);
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00015228 File Offset: 0x00013428
		public void RemoveBoneComponent(UIntPtr skeletonPointer, sbyte boneIndex, GameEntityComponent component)
		{
			UIntPtr component2 = (component != null) ? component.Pointer : UIntPtr.Zero;
			ScriptingInterfaceOfISkeleton.call_RemoveBoneComponentDelegate(skeletonPointer, boneIndex, component2);
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00015259 File Offset: 0x00013459
		public void RemoveComponent(UIntPtr SkeletonPointer, UIntPtr componentPointer)
		{
			ScriptingInterfaceOfISkeleton.call_RemoveComponentDelegate(SkeletonPointer, componentPointer);
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00015267 File Offset: 0x00013467
		public void ResetCloths(UIntPtr skeletonPointer)
		{
			ScriptingInterfaceOfISkeleton.call_ResetClothsDelegate(skeletonPointer);
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00015274 File Offset: 0x00013474
		public void ResetFrames(UIntPtr skeletonPointer)
		{
			ScriptingInterfaceOfISkeleton.call_ResetFramesDelegate(skeletonPointer);
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00015281 File Offset: 0x00013481
		public void SetBoneLocalFrame(UIntPtr skeletonPointer, sbyte boneIndex, ref MatrixFrame localFrame)
		{
			ScriptingInterfaceOfISkeleton.call_SetBoneLocalFrameDelegate(skeletonPointer, boneIndex, ref localFrame);
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00015290 File Offset: 0x00013490
		public void SetSkeletonAnimationParameterAtChannel(UIntPtr skeletonPointer, int channelNo, float parameter)
		{
			ScriptingInterfaceOfISkeleton.call_SetSkeletonAnimationParameterAtChannelDelegate(skeletonPointer, channelNo, parameter);
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0001529F File Offset: 0x0001349F
		public void SetSkeletonAnimationSpeedAtChannel(UIntPtr skeletonPointer, int channelNo, float speed)
		{
			ScriptingInterfaceOfISkeleton.call_SetSkeletonAnimationSpeedAtChannelDelegate(skeletonPointer, channelNo, speed);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x000152AE File Offset: 0x000134AE
		public void SetSkeletonUptoDate(UIntPtr skeletonPointer, bool value)
		{
			ScriptingInterfaceOfISkeleton.call_SetSkeletonUptoDateDelegate(skeletonPointer, value);
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x000152BC File Offset: 0x000134BC
		public void SetUsePreciseBoundingVolume(UIntPtr skeletonPointer, bool value)
		{
			ScriptingInterfaceOfISkeleton.call_SetUsePreciseBoundingVolumeDelegate(skeletonPointer, value);
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x000152CC File Offset: 0x000134CC
		public bool SkeletonModelExist(string skeletonModelName)
		{
			byte[] array = null;
			if (skeletonModelName != null)
			{
				int byteCount = ScriptingInterfaceOfISkeleton._utf8.GetByteCount(skeletonModelName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfISkeleton._utf8.GetBytes(skeletonModelName, 0, skeletonModelName.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfISkeleton.call_SkeletonModelExistDelegate(array);
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x00015326 File Offset: 0x00013526
		public void TickAnimations(UIntPtr skeletonPointer, ref MatrixFrame globalFrame, float dt, bool tickAnimsForChildren)
		{
			ScriptingInterfaceOfISkeleton.call_TickAnimationsDelegate(skeletonPointer, ref globalFrame, dt, tickAnimsForChildren);
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00015337 File Offset: 0x00013537
		public void TickAnimationsAndForceUpdate(UIntPtr skeletonPointer, ref MatrixFrame globalFrame, float dt, bool tickAnimsForChildren)
		{
			ScriptingInterfaceOfISkeleton.call_TickAnimationsAndForceUpdateDelegate(skeletonPointer, ref globalFrame, dt, tickAnimsForChildren);
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00015348 File Offset: 0x00013548
		public void UpdateEntitialFramesFromLocalFrames(UIntPtr skeletonPointer)
		{
			ScriptingInterfaceOfISkeleton.call_UpdateEntitialFramesFromLocalFramesDelegate(skeletonPointer);
		}

		// Token: 0x04000404 RID: 1028
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x04000405 RID: 1029
		public static ScriptingInterfaceOfISkeleton.ActivateRagdollDelegate call_ActivateRagdollDelegate;

		// Token: 0x04000406 RID: 1030
		public static ScriptingInterfaceOfISkeleton.AddComponentDelegate call_AddComponentDelegate;

		// Token: 0x04000407 RID: 1031
		public static ScriptingInterfaceOfISkeleton.AddComponentToBoneDelegate call_AddComponentToBoneDelegate;

		// Token: 0x04000408 RID: 1032
		public static ScriptingInterfaceOfISkeleton.AddMeshDelegate call_AddMeshDelegate;

		// Token: 0x04000409 RID: 1033
		public static ScriptingInterfaceOfISkeleton.AddMeshToBoneDelegate call_AddMeshToBoneDelegate;

		// Token: 0x0400040A RID: 1034
		public static ScriptingInterfaceOfISkeleton.AddPrefabEntityToBoneDelegate call_AddPrefabEntityToBoneDelegate;

		// Token: 0x0400040B RID: 1035
		public static ScriptingInterfaceOfISkeleton.ClearComponentsDelegate call_ClearComponentsDelegate;

		// Token: 0x0400040C RID: 1036
		public static ScriptingInterfaceOfISkeleton.ClearMeshesDelegate call_ClearMeshesDelegate;

		// Token: 0x0400040D RID: 1037
		public static ScriptingInterfaceOfISkeleton.ClearMeshesAtBoneDelegate call_ClearMeshesAtBoneDelegate;

		// Token: 0x0400040E RID: 1038
		public static ScriptingInterfaceOfISkeleton.CreateFromModelDelegate call_CreateFromModelDelegate;

		// Token: 0x0400040F RID: 1039
		public static ScriptingInterfaceOfISkeleton.CreateFromModelWithNullAnimTreeDelegate call_CreateFromModelWithNullAnimTreeDelegate;

		// Token: 0x04000410 RID: 1040
		public static ScriptingInterfaceOfISkeleton.ForceUpdateBoneFramesDelegate call_ForceUpdateBoneFramesDelegate;

		// Token: 0x04000411 RID: 1041
		public static ScriptingInterfaceOfISkeleton.FreezeDelegate call_FreezeDelegate;

		// Token: 0x04000412 RID: 1042
		public static ScriptingInterfaceOfISkeleton.GetAllMeshesDelegate call_GetAllMeshesDelegate;

		// Token: 0x04000413 RID: 1043
		public static ScriptingInterfaceOfISkeleton.GetAnimationAtChannelDelegate call_GetAnimationAtChannelDelegate;

		// Token: 0x04000414 RID: 1044
		public static ScriptingInterfaceOfISkeleton.GetAnimationIndexAtChannelDelegate call_GetAnimationIndexAtChannelDelegate;

		// Token: 0x04000415 RID: 1045
		public static ScriptingInterfaceOfISkeleton.GetBoneBodyDelegate call_GetBoneBodyDelegate;

		// Token: 0x04000416 RID: 1046
		public static ScriptingInterfaceOfISkeleton.GetBoneChildAtIndexDelegate call_GetBoneChildAtIndexDelegate;

		// Token: 0x04000417 RID: 1047
		public static ScriptingInterfaceOfISkeleton.GetBoneChildCountDelegate call_GetBoneChildCountDelegate;

		// Token: 0x04000418 RID: 1048
		public static ScriptingInterfaceOfISkeleton.GetBoneComponentAtIndexDelegate call_GetBoneComponentAtIndexDelegate;

		// Token: 0x04000419 RID: 1049
		public static ScriptingInterfaceOfISkeleton.GetBoneComponentCountDelegate call_GetBoneComponentCountDelegate;

		// Token: 0x0400041A RID: 1050
		public static ScriptingInterfaceOfISkeleton.GetBoneCountDelegate call_GetBoneCountDelegate;

		// Token: 0x0400041B RID: 1051
		public static ScriptingInterfaceOfISkeleton.GetBoneEntitialFrameDelegate call_GetBoneEntitialFrameDelegate;

		// Token: 0x0400041C RID: 1052
		public static ScriptingInterfaceOfISkeleton.GetBoneEntitialFrameAtChannelDelegate call_GetBoneEntitialFrameAtChannelDelegate;

		// Token: 0x0400041D RID: 1053
		public static ScriptingInterfaceOfISkeleton.GetBoneEntitialFrameWithIndexDelegate call_GetBoneEntitialFrameWithIndexDelegate;

		// Token: 0x0400041E RID: 1054
		public static ScriptingInterfaceOfISkeleton.GetBoneEntitialFrameWithNameDelegate call_GetBoneEntitialFrameWithNameDelegate;

		// Token: 0x0400041F RID: 1055
		public static ScriptingInterfaceOfISkeleton.GetBoneEntitialRestFrameDelegate call_GetBoneEntitialRestFrameDelegate;

		// Token: 0x04000420 RID: 1056
		public static ScriptingInterfaceOfISkeleton.GetBoneIndexFromNameDelegate call_GetBoneIndexFromNameDelegate;

		// Token: 0x04000421 RID: 1057
		public static ScriptingInterfaceOfISkeleton.GetBoneLocalRestFrameDelegate call_GetBoneLocalRestFrameDelegate;

		// Token: 0x04000422 RID: 1058
		public static ScriptingInterfaceOfISkeleton.GetBoneNameDelegate call_GetBoneNameDelegate;

		// Token: 0x04000423 RID: 1059
		public static ScriptingInterfaceOfISkeleton.GetComponentAtIndexDelegate call_GetComponentAtIndexDelegate;

		// Token: 0x04000424 RID: 1060
		public static ScriptingInterfaceOfISkeleton.GetComponentCountDelegate call_GetComponentCountDelegate;

		// Token: 0x04000425 RID: 1061
		public static ScriptingInterfaceOfISkeleton.GetCurrentRagdollStateDelegate call_GetCurrentRagdollStateDelegate;

		// Token: 0x04000426 RID: 1062
		public static ScriptingInterfaceOfISkeleton.GetNameDelegate call_GetNameDelegate;

		// Token: 0x04000427 RID: 1063
		public static ScriptingInterfaceOfISkeleton.GetParentBoneIndexDelegate call_GetParentBoneIndexDelegate;

		// Token: 0x04000428 RID: 1064
		public static ScriptingInterfaceOfISkeleton.GetSkeletonAnimationParameterAtChannelDelegate call_GetSkeletonAnimationParameterAtChannelDelegate;

		// Token: 0x04000429 RID: 1065
		public static ScriptingInterfaceOfISkeleton.GetSkeletonAnimationSpeedAtChannelDelegate call_GetSkeletonAnimationSpeedAtChannelDelegate;

		// Token: 0x0400042A RID: 1066
		public static ScriptingInterfaceOfISkeleton.GetSkeletonBoneMappingDelegate call_GetSkeletonBoneMappingDelegate;

		// Token: 0x0400042B RID: 1067
		public static ScriptingInterfaceOfISkeleton.HasBoneComponentDelegate call_HasBoneComponentDelegate;

		// Token: 0x0400042C RID: 1068
		public static ScriptingInterfaceOfISkeleton.HasComponentDelegate call_HasComponentDelegate;

		// Token: 0x0400042D RID: 1069
		public static ScriptingInterfaceOfISkeleton.IsFrozenDelegate call_IsFrozenDelegate;

		// Token: 0x0400042E RID: 1070
		public static ScriptingInterfaceOfISkeleton.RemoveBoneComponentDelegate call_RemoveBoneComponentDelegate;

		// Token: 0x0400042F RID: 1071
		public static ScriptingInterfaceOfISkeleton.RemoveComponentDelegate call_RemoveComponentDelegate;

		// Token: 0x04000430 RID: 1072
		public static ScriptingInterfaceOfISkeleton.ResetClothsDelegate call_ResetClothsDelegate;

		// Token: 0x04000431 RID: 1073
		public static ScriptingInterfaceOfISkeleton.ResetFramesDelegate call_ResetFramesDelegate;

		// Token: 0x04000432 RID: 1074
		public static ScriptingInterfaceOfISkeleton.SetBoneLocalFrameDelegate call_SetBoneLocalFrameDelegate;

		// Token: 0x04000433 RID: 1075
		public static ScriptingInterfaceOfISkeleton.SetSkeletonAnimationParameterAtChannelDelegate call_SetSkeletonAnimationParameterAtChannelDelegate;

		// Token: 0x04000434 RID: 1076
		public static ScriptingInterfaceOfISkeleton.SetSkeletonAnimationSpeedAtChannelDelegate call_SetSkeletonAnimationSpeedAtChannelDelegate;

		// Token: 0x04000435 RID: 1077
		public static ScriptingInterfaceOfISkeleton.SetSkeletonUptoDateDelegate call_SetSkeletonUptoDateDelegate;

		// Token: 0x04000436 RID: 1078
		public static ScriptingInterfaceOfISkeleton.SetUsePreciseBoundingVolumeDelegate call_SetUsePreciseBoundingVolumeDelegate;

		// Token: 0x04000437 RID: 1079
		public static ScriptingInterfaceOfISkeleton.SkeletonModelExistDelegate call_SkeletonModelExistDelegate;

		// Token: 0x04000438 RID: 1080
		public static ScriptingInterfaceOfISkeleton.TickAnimationsDelegate call_TickAnimationsDelegate;

		// Token: 0x04000439 RID: 1081
		public static ScriptingInterfaceOfISkeleton.TickAnimationsAndForceUpdateDelegate call_TickAnimationsAndForceUpdateDelegate;

		// Token: 0x0400043A RID: 1082
		public static ScriptingInterfaceOfISkeleton.UpdateEntitialFramesFromLocalFramesDelegate call_UpdateEntitialFramesFromLocalFramesDelegate;

		// Token: 0x02000457 RID: 1111
		// (Invoke) Token: 0x0600165F RID: 5727
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ActivateRagdollDelegate(UIntPtr skeletonPointer);

		// Token: 0x02000458 RID: 1112
		// (Invoke) Token: 0x06001663 RID: 5731
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddComponentDelegate(UIntPtr skeletonPointer, UIntPtr componentPointer);

		// Token: 0x02000459 RID: 1113
		// (Invoke) Token: 0x06001667 RID: 5735
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddComponentToBoneDelegate(UIntPtr skeletonPointer, sbyte boneIndex, UIntPtr component);

		// Token: 0x0200045A RID: 1114
		// (Invoke) Token: 0x0600166B RID: 5739
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddMeshDelegate(UIntPtr skeletonPointer, UIntPtr mesnPointer);

		// Token: 0x0200045B RID: 1115
		// (Invoke) Token: 0x0600166F RID: 5743
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddMeshToBoneDelegate(UIntPtr skeletonPointer, UIntPtr multiMeshPointer, sbyte bone_index);

		// Token: 0x0200045C RID: 1116
		// (Invoke) Token: 0x06001673 RID: 5747
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddPrefabEntityToBoneDelegate(UIntPtr skeletonPointer, byte[] prefab_name, sbyte boneIndex);

		// Token: 0x0200045D RID: 1117
		// (Invoke) Token: 0x06001677 RID: 5751
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ClearComponentsDelegate(UIntPtr skeletonPointer);

		// Token: 0x0200045E RID: 1118
		// (Invoke) Token: 0x0600167B RID: 5755
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ClearMeshesDelegate(UIntPtr skeletonPointer, [MarshalAs(UnmanagedType.U1)] bool clearBoneComponents);

		// Token: 0x0200045F RID: 1119
		// (Invoke) Token: 0x0600167F RID: 5759
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ClearMeshesAtBoneDelegate(UIntPtr skeletonPointer, sbyte boneIndex);

		// Token: 0x02000460 RID: 1120
		// (Invoke) Token: 0x06001683 RID: 5763
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer CreateFromModelDelegate(byte[] skeletonModelName);

		// Token: 0x02000461 RID: 1121
		// (Invoke) Token: 0x06001687 RID: 5767
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer CreateFromModelWithNullAnimTreeDelegate(UIntPtr entityPointer, byte[] skeletonModelName, float scale);

		// Token: 0x02000462 RID: 1122
		// (Invoke) Token: 0x0600168B RID: 5771
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ForceUpdateBoneFramesDelegate(UIntPtr skeletonPointer);

		// Token: 0x02000463 RID: 1123
		// (Invoke) Token: 0x0600168F RID: 5775
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void FreezeDelegate(UIntPtr skeletonPointer, [MarshalAs(UnmanagedType.U1)] bool isFrozen);

		// Token: 0x02000464 RID: 1124
		// (Invoke) Token: 0x06001693 RID: 5779
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetAllMeshesDelegate(UIntPtr skeleton, UIntPtr nativeObjectArray);

		// Token: 0x02000465 RID: 1125
		// (Invoke) Token: 0x06001697 RID: 5783
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetAnimationAtChannelDelegate(UIntPtr skeletonPointer, int channelNo);

		// Token: 0x02000466 RID: 1126
		// (Invoke) Token: 0x0600169B RID: 5787
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetAnimationIndexAtChannelDelegate(UIntPtr skeletonPointer, int channelNo);

		// Token: 0x02000467 RID: 1127
		// (Invoke) Token: 0x0600169F RID: 5791
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetBoneBodyDelegate(UIntPtr skeletonPointer, sbyte boneIndex, ref CapsuleData data);

		// Token: 0x02000468 RID: 1128
		// (Invoke) Token: 0x060016A3 RID: 5795
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate sbyte GetBoneChildAtIndexDelegate(UIntPtr skeleton, sbyte boneIndex, sbyte childIndex);

		// Token: 0x02000469 RID: 1129
		// (Invoke) Token: 0x060016A7 RID: 5799
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate sbyte GetBoneChildCountDelegate(UIntPtr skeleton, sbyte boneIndex);

		// Token: 0x0200046A RID: 1130
		// (Invoke) Token: 0x060016AB RID: 5803
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer GetBoneComponentAtIndexDelegate(UIntPtr skeletonPointer, sbyte boneIndex, int componentIndex);

		// Token: 0x0200046B RID: 1131
		// (Invoke) Token: 0x060016AF RID: 5807
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetBoneComponentCountDelegate(UIntPtr skeletonPointer, sbyte boneIndex);

		// Token: 0x0200046C RID: 1132
		// (Invoke) Token: 0x060016B3 RID: 5811
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate sbyte GetBoneCountDelegate(UIntPtr skeletonPointer);

		// Token: 0x0200046D RID: 1133
		// (Invoke) Token: 0x060016B7 RID: 5815
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetBoneEntitialFrameDelegate(UIntPtr skeletonPointer, sbyte boneIndex, ref MatrixFrame outFrame);

		// Token: 0x0200046E RID: 1134
		// (Invoke) Token: 0x060016BB RID: 5819
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetBoneEntitialFrameAtChannelDelegate(UIntPtr skeletonPointer, int channelNo, sbyte boneIndex, ref MatrixFrame outFrame);

		// Token: 0x0200046F RID: 1135
		// (Invoke) Token: 0x060016BF RID: 5823
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetBoneEntitialFrameWithIndexDelegate(UIntPtr skeletonPointer, sbyte boneIndex, ref MatrixFrame outEntitialFrame);

		// Token: 0x02000470 RID: 1136
		// (Invoke) Token: 0x060016C3 RID: 5827
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetBoneEntitialFrameWithNameDelegate(UIntPtr skeletonPointer, byte[] boneName, ref MatrixFrame outEntitialFrame);

		// Token: 0x02000471 RID: 1137
		// (Invoke) Token: 0x060016C7 RID: 5831
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetBoneEntitialRestFrameDelegate(UIntPtr skeletonPointer, sbyte boneIndex, [MarshalAs(UnmanagedType.U1)] bool useBoneMapping, ref MatrixFrame outFrame);

		// Token: 0x02000472 RID: 1138
		// (Invoke) Token: 0x060016CB RID: 5835
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate sbyte GetBoneIndexFromNameDelegate(byte[] skeletonModelName, byte[] boneName);

		// Token: 0x02000473 RID: 1139
		// (Invoke) Token: 0x060016CF RID: 5839
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetBoneLocalRestFrameDelegate(UIntPtr skeletonPointer, sbyte boneIndex, [MarshalAs(UnmanagedType.U1)] bool useBoneMapping, ref MatrixFrame outFrame);

		// Token: 0x02000474 RID: 1140
		// (Invoke) Token: 0x060016D3 RID: 5843
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetBoneNameDelegate(UIntPtr skeleton, sbyte boneIndex);

		// Token: 0x02000475 RID: 1141
		// (Invoke) Token: 0x060016D7 RID: 5847
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer GetComponentAtIndexDelegate(UIntPtr skeletonPointer, GameEntity.ComponentType componentType, int index);

		// Token: 0x02000476 RID: 1142
		// (Invoke) Token: 0x060016DB RID: 5851
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetComponentCountDelegate(UIntPtr skeletonPointer, GameEntity.ComponentType componentType);

		// Token: 0x02000477 RID: 1143
		// (Invoke) Token: 0x060016DF RID: 5855
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate RagdollState GetCurrentRagdollStateDelegate(UIntPtr skeletonPointer);

		// Token: 0x02000478 RID: 1144
		// (Invoke) Token: 0x060016E3 RID: 5859
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetNameDelegate(UIntPtr skeleton);

		// Token: 0x02000479 RID: 1145
		// (Invoke) Token: 0x060016E7 RID: 5863
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate sbyte GetParentBoneIndexDelegate(UIntPtr skeleton, sbyte boneIndex);

		// Token: 0x0200047A RID: 1146
		// (Invoke) Token: 0x060016EB RID: 5867
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetSkeletonAnimationParameterAtChannelDelegate(UIntPtr skeletonPointer, int channelNo);

		// Token: 0x0200047B RID: 1147
		// (Invoke) Token: 0x060016EF RID: 5871
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetSkeletonAnimationSpeedAtChannelDelegate(UIntPtr skeletonPointer, int channelNo);

		// Token: 0x0200047C RID: 1148
		// (Invoke) Token: 0x060016F3 RID: 5875
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate sbyte GetSkeletonBoneMappingDelegate(UIntPtr skeletonPointer, sbyte boneIndex);

		// Token: 0x0200047D RID: 1149
		// (Invoke) Token: 0x060016F7 RID: 5879
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool HasBoneComponentDelegate(UIntPtr skeletonPointer, sbyte boneIndex, UIntPtr component);

		// Token: 0x0200047E RID: 1150
		// (Invoke) Token: 0x060016FB RID: 5883
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool HasComponentDelegate(UIntPtr skeletonPointer, UIntPtr componentPointer);

		// Token: 0x0200047F RID: 1151
		// (Invoke) Token: 0x060016FF RID: 5887
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsFrozenDelegate(UIntPtr skeletonPointer);

		// Token: 0x02000480 RID: 1152
		// (Invoke) Token: 0x06001703 RID: 5891
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RemoveBoneComponentDelegate(UIntPtr skeletonPointer, sbyte boneIndex, UIntPtr component);

		// Token: 0x02000481 RID: 1153
		// (Invoke) Token: 0x06001707 RID: 5895
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RemoveComponentDelegate(UIntPtr SkeletonPointer, UIntPtr componentPointer);

		// Token: 0x02000482 RID: 1154
		// (Invoke) Token: 0x0600170B RID: 5899
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ResetClothsDelegate(UIntPtr skeletonPointer);

		// Token: 0x02000483 RID: 1155
		// (Invoke) Token: 0x0600170F RID: 5903
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ResetFramesDelegate(UIntPtr skeletonPointer);

		// Token: 0x02000484 RID: 1156
		// (Invoke) Token: 0x06001713 RID: 5907
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetBoneLocalFrameDelegate(UIntPtr skeletonPointer, sbyte boneIndex, ref MatrixFrame localFrame);

		// Token: 0x02000485 RID: 1157
		// (Invoke) Token: 0x06001717 RID: 5911
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetSkeletonAnimationParameterAtChannelDelegate(UIntPtr skeletonPointer, int channelNo, float parameter);

		// Token: 0x02000486 RID: 1158
		// (Invoke) Token: 0x0600171B RID: 5915
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetSkeletonAnimationSpeedAtChannelDelegate(UIntPtr skeletonPointer, int channelNo, float speed);

		// Token: 0x02000487 RID: 1159
		// (Invoke) Token: 0x0600171F RID: 5919
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetSkeletonUptoDateDelegate(UIntPtr skeletonPointer, [MarshalAs(UnmanagedType.U1)] bool value);

		// Token: 0x02000488 RID: 1160
		// (Invoke) Token: 0x06001723 RID: 5923
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetUsePreciseBoundingVolumeDelegate(UIntPtr skeletonPointer, [MarshalAs(UnmanagedType.U1)] bool value);

		// Token: 0x02000489 RID: 1161
		// (Invoke) Token: 0x06001727 RID: 5927
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool SkeletonModelExistDelegate(byte[] skeletonModelName);

		// Token: 0x0200048A RID: 1162
		// (Invoke) Token: 0x0600172B RID: 5931
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void TickAnimationsDelegate(UIntPtr skeletonPointer, ref MatrixFrame globalFrame, float dt, [MarshalAs(UnmanagedType.U1)] bool tickAnimsForChildren);

		// Token: 0x0200048B RID: 1163
		// (Invoke) Token: 0x0600172F RID: 5935
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void TickAnimationsAndForceUpdateDelegate(UIntPtr skeletonPointer, ref MatrixFrame globalFrame, float dt, [MarshalAs(UnmanagedType.U1)] bool tickAnimsForChildren);

		// Token: 0x0200048C RID: 1164
		// (Invoke) Token: 0x06001733 RID: 5939
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void UpdateEntitialFramesFromLocalFramesDelegate(UIntPtr skeletonPointer);
	}
}
