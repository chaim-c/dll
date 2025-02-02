using System;
using System.Collections.Generic;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000088 RID: 136
	[EngineClass("rglSkeleton")]
	public sealed class Skeleton : NativeObject
	{
		// Token: 0x06000A3B RID: 2619 RVA: 0x0000B1F0 File Offset: 0x000093F0
		internal Skeleton(UIntPtr pointer)
		{
			base.Construct(pointer);
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0000B1FF File Offset: 0x000093FF
		public static Skeleton CreateFromModel(string modelName)
		{
			return EngineApplicationInterface.ISkeleton.CreateFromModel(modelName);
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0000B20C File Offset: 0x0000940C
		public static Skeleton CreateFromModelWithNullAnimTree(GameEntity entity, string modelName, float boneScale = 1f)
		{
			return EngineApplicationInterface.ISkeleton.CreateFromModelWithNullAnimTree(entity.Pointer, modelName, boneScale);
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000A3E RID: 2622 RVA: 0x0000B220 File Offset: 0x00009420
		public bool IsValid
		{
			get
			{
				return base.Pointer != UIntPtr.Zero;
			}
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0000B232 File Offset: 0x00009432
		public string GetName()
		{
			return EngineApplicationInterface.ISkeleton.GetName(this);
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0000B23F File Offset: 0x0000943F
		public string GetBoneName(sbyte boneIndex)
		{
			return EngineApplicationInterface.ISkeleton.GetBoneName(this, boneIndex);
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0000B24D File Offset: 0x0000944D
		public sbyte GetBoneChildAtIndex(sbyte boneIndex, sbyte childIndex)
		{
			return EngineApplicationInterface.ISkeleton.GetBoneChildAtIndex(this, boneIndex, childIndex);
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0000B25C File Offset: 0x0000945C
		public sbyte GetBoneChildCount(sbyte boneIndex)
		{
			return EngineApplicationInterface.ISkeleton.GetBoneChildCount(this, boneIndex);
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x0000B26A File Offset: 0x0000946A
		public sbyte GetParentBoneIndex(sbyte boneIndex)
		{
			return EngineApplicationInterface.ISkeleton.GetParentBoneIndex(this, boneIndex);
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0000B278 File Offset: 0x00009478
		public void AddMeshToBone(UIntPtr mesh, sbyte boneIndex)
		{
			EngineApplicationInterface.ISkeleton.AddMeshToBone(base.Pointer, mesh, boneIndex);
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x0000B28C File Offset: 0x0000948C
		public void Freeze(bool p)
		{
			EngineApplicationInterface.ISkeleton.Freeze(base.Pointer, p);
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x0000B29F File Offset: 0x0000949F
		public bool IsFrozen()
		{
			return EngineApplicationInterface.ISkeleton.IsFrozen(base.Pointer);
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x0000B2B1 File Offset: 0x000094B1
		public void SetBoneLocalFrame(sbyte boneIndex, MatrixFrame localFrame)
		{
			EngineApplicationInterface.ISkeleton.SetBoneLocalFrame(base.Pointer, boneIndex, ref localFrame);
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0000B2C6 File Offset: 0x000094C6
		public sbyte GetBoneCount()
		{
			return EngineApplicationInterface.ISkeleton.GetBoneCount(base.Pointer);
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x0000B2D8 File Offset: 0x000094D8
		public void GetBoneBody(sbyte boneIndex, ref CapsuleData data)
		{
			EngineApplicationInterface.ISkeleton.GetBoneBody(base.Pointer, boneIndex, ref data);
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x0000B2EC File Offset: 0x000094EC
		public static bool SkeletonModelExist(string skeletonModelName)
		{
			return EngineApplicationInterface.ISkeleton.SkeletonModelExist(skeletonModelName);
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x0000B2F9 File Offset: 0x000094F9
		public void ForceUpdateBoneFrames()
		{
			EngineApplicationInterface.ISkeleton.ForceUpdateBoneFrames(base.Pointer);
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x0000B30C File Offset: 0x0000950C
		public MatrixFrame GetBoneEntitialFrameWithIndex(sbyte boneIndex)
		{
			MatrixFrame result = default(MatrixFrame);
			EngineApplicationInterface.ISkeleton.GetBoneEntitialFrameWithIndex(base.Pointer, boneIndex, ref result);
			return result;
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x0000B338 File Offset: 0x00009538
		public MatrixFrame GetBoneEntitialFrameWithName(string boneName)
		{
			MatrixFrame result = default(MatrixFrame);
			EngineApplicationInterface.ISkeleton.GetBoneEntitialFrameWithName(base.Pointer, boneName, ref result);
			return result;
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x0000B361 File Offset: 0x00009561
		public RagdollState GetCurrentRagdollState()
		{
			return EngineApplicationInterface.ISkeleton.GetCurrentRagdollState(base.Pointer);
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0000B373 File Offset: 0x00009573
		public void ActivateRagdoll()
		{
			EngineApplicationInterface.ISkeleton.ActivateRagdoll(base.Pointer);
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x0000B385 File Offset: 0x00009585
		public sbyte GetSkeletonBoneMapping(sbyte boneIndex)
		{
			return EngineApplicationInterface.ISkeleton.GetSkeletonBoneMapping(base.Pointer, boneIndex);
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x0000B398 File Offset: 0x00009598
		public void AddMesh(Mesh mesh)
		{
			EngineApplicationInterface.ISkeleton.AddMesh(base.Pointer, mesh.Pointer);
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x0000B3B0 File Offset: 0x000095B0
		public void ClearComponents()
		{
			EngineApplicationInterface.ISkeleton.ClearComponents(base.Pointer);
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x0000B3C2 File Offset: 0x000095C2
		public void AddComponent(GameEntityComponent component)
		{
			EngineApplicationInterface.ISkeleton.AddComponent(base.Pointer, component.Pointer);
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x0000B3DA File Offset: 0x000095DA
		public bool HasComponent(GameEntityComponent component)
		{
			return EngineApplicationInterface.ISkeleton.HasComponent(base.Pointer, component.Pointer);
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x0000B3F2 File Offset: 0x000095F2
		public void RemoveComponent(GameEntityComponent component)
		{
			EngineApplicationInterface.ISkeleton.RemoveComponent(base.Pointer, component.Pointer);
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x0000B40A File Offset: 0x0000960A
		public void ClearMeshes(bool clearBoneComponents = true)
		{
			EngineApplicationInterface.ISkeleton.ClearMeshes(base.Pointer, clearBoneComponents);
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0000B41D File Offset: 0x0000961D
		public int GetComponentCount(GameEntity.ComponentType componentType)
		{
			return EngineApplicationInterface.ISkeleton.GetComponentCount(base.Pointer, componentType);
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x0000B430 File Offset: 0x00009630
		public void UpdateEntitialFramesFromLocalFrames()
		{
			EngineApplicationInterface.ISkeleton.UpdateEntitialFramesFromLocalFrames(base.Pointer);
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x0000B442 File Offset: 0x00009642
		public void ResetFrames()
		{
			EngineApplicationInterface.ISkeleton.ResetFrames(base.Pointer);
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x0000B454 File Offset: 0x00009654
		public GameEntityComponent GetComponentAtIndex(GameEntity.ComponentType componentType, int index)
		{
			return EngineApplicationInterface.ISkeleton.GetComponentAtIndex(base.Pointer, componentType, index);
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x0000B468 File Offset: 0x00009668
		public void SetUsePreciseBoundingVolume(bool value)
		{
			EngineApplicationInterface.ISkeleton.SetUsePreciseBoundingVolume(base.Pointer, value);
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x0000B47C File Offset: 0x0000967C
		public MatrixFrame GetBoneEntitialRestFrame(sbyte boneIndex, bool useBoneMapping)
		{
			MatrixFrame result = default(MatrixFrame);
			EngineApplicationInterface.ISkeleton.GetBoneEntitialRestFrame(base.Pointer, boneIndex, useBoneMapping, ref result);
			return result;
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x0000B4A8 File Offset: 0x000096A8
		public MatrixFrame GetBoneLocalRestFrame(sbyte boneIndex, bool useBoneMapping = true)
		{
			MatrixFrame result = default(MatrixFrame);
			EngineApplicationInterface.ISkeleton.GetBoneLocalRestFrame(base.Pointer, boneIndex, useBoneMapping, ref result);
			return result;
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x0000B4D4 File Offset: 0x000096D4
		public MatrixFrame GetBoneEntitialRestFrame(sbyte boneIndex)
		{
			MatrixFrame result = default(MatrixFrame);
			EngineApplicationInterface.ISkeleton.GetBoneEntitialRestFrame(base.Pointer, boneIndex, true, ref result);
			return result;
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x0000B500 File Offset: 0x00009700
		public MatrixFrame GetBoneEntitialFrameAtChannel(int channelNo, sbyte boneIndex)
		{
			MatrixFrame result = default(MatrixFrame);
			EngineApplicationInterface.ISkeleton.GetBoneEntitialFrameAtChannel(base.Pointer, channelNo, boneIndex, ref result);
			return result;
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x0000B52C File Offset: 0x0000972C
		public MatrixFrame GetBoneEntitialFrame(sbyte boneIndex)
		{
			MatrixFrame result = default(MatrixFrame);
			EngineApplicationInterface.ISkeleton.GetBoneEntitialFrame(base.Pointer, boneIndex, ref result);
			return result;
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0000B555 File Offset: 0x00009755
		public int GetBoneComponentCount(sbyte boneIndex)
		{
			return EngineApplicationInterface.ISkeleton.GetBoneComponentCount(base.Pointer, boneIndex);
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0000B568 File Offset: 0x00009768
		public GameEntityComponent GetBoneComponentAtIndex(sbyte boneIndex, int componentIndex)
		{
			return EngineApplicationInterface.ISkeleton.GetBoneComponentAtIndex(base.Pointer, boneIndex, componentIndex);
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0000B57C File Offset: 0x0000977C
		public bool HasBoneComponent(sbyte boneIndex, GameEntityComponent component)
		{
			return EngineApplicationInterface.ISkeleton.HasBoneComponent(base.Pointer, boneIndex, component);
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x0000B590 File Offset: 0x00009790
		public void AddComponentToBone(sbyte boneIndex, GameEntityComponent component)
		{
			EngineApplicationInterface.ISkeleton.AddComponentToBone(base.Pointer, boneIndex, component);
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x0000B5A4 File Offset: 0x000097A4
		public void RemoveBoneComponent(sbyte boneIndex, GameEntityComponent component)
		{
			EngineApplicationInterface.ISkeleton.RemoveBoneComponent(base.Pointer, boneIndex, component);
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0000B5B8 File Offset: 0x000097B8
		public void ClearMeshesAtBone(sbyte boneIndex)
		{
			EngineApplicationInterface.ISkeleton.ClearMeshesAtBone(base.Pointer, boneIndex);
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0000B5CB File Offset: 0x000097CB
		public void TickAnimations(float dt, MatrixFrame globalFrame, bool tickAnimsForChildren)
		{
			EngineApplicationInterface.ISkeleton.TickAnimations(base.Pointer, ref globalFrame, dt, tickAnimsForChildren);
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x0000B5E1 File Offset: 0x000097E1
		public void TickAnimationsAndForceUpdate(float dt, MatrixFrame globalFrame, bool tickAnimsForChildren)
		{
			EngineApplicationInterface.ISkeleton.TickAnimationsAndForceUpdate(base.Pointer, ref globalFrame, dt, tickAnimsForChildren);
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x0000B5F7 File Offset: 0x000097F7
		public float GetAnimationParameterAtChannel(int channelNo)
		{
			return EngineApplicationInterface.ISkeleton.GetSkeletonAnimationParameterAtChannel(base.Pointer, channelNo);
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x0000B60A File Offset: 0x0000980A
		public void SetAnimationParameterAtChannel(int channelNo, float parameter)
		{
			EngineApplicationInterface.ISkeleton.SetSkeletonAnimationParameterAtChannel(base.Pointer, channelNo, parameter);
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x0000B61E File Offset: 0x0000981E
		public float GetAnimationSpeedAtChannel(int channelNo)
		{
			return EngineApplicationInterface.ISkeleton.GetSkeletonAnimationSpeedAtChannel(base.Pointer, channelNo);
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x0000B631 File Offset: 0x00009831
		public void SetAnimationSpeedAtChannel(int channelNo, float speed)
		{
			EngineApplicationInterface.ISkeleton.SetSkeletonAnimationSpeedAtChannel(base.Pointer, channelNo, speed);
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x0000B645 File Offset: 0x00009845
		public void SetUptoDate(bool value)
		{
			EngineApplicationInterface.ISkeleton.SetSkeletonUptoDate(base.Pointer, value);
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x0000B658 File Offset: 0x00009858
		public string GetAnimationAtChannel(int channelNo)
		{
			return EngineApplicationInterface.ISkeleton.GetAnimationAtChannel(base.Pointer, channelNo);
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x0000B66B File Offset: 0x0000986B
		public int GetAnimationIndexAtChannel(int channelNo)
		{
			return EngineApplicationInterface.ISkeleton.GetAnimationIndexAtChannel(base.Pointer, channelNo);
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x0000B67E File Offset: 0x0000987E
		public void ResetCloths()
		{
			EngineApplicationInterface.ISkeleton.ResetCloths(base.Pointer);
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x0000B690 File Offset: 0x00009890
		public IEnumerable<Mesh> GetAllMeshes()
		{
			NativeObjectArray nativeObjectArray = NativeObjectArray.Create();
			EngineApplicationInterface.ISkeleton.GetAllMeshes(this, nativeObjectArray);
			foreach (NativeObject nativeObject in ((IEnumerable<NativeObject>)nativeObjectArray))
			{
				Mesh mesh = (Mesh)nativeObject;
				yield return mesh;
			}
			IEnumerator<NativeObject> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x0000B6A0 File Offset: 0x000098A0
		public static sbyte GetBoneIndexFromName(string skeletonModelName, string boneName)
		{
			return EngineApplicationInterface.ISkeleton.GetBoneIndexFromName(skeletonModelName, boneName);
		}

		// Token: 0x040001AC RID: 428
		public const sbyte MaxBoneCount = 64;
	}
}
