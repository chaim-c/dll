using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace ManagedCallbacks
{
	// Token: 0x0200001B RID: 27
	internal class ScriptingInterfaceOfIMetaMesh : IMetaMesh
	{
		// Token: 0x060002B8 RID: 696 RVA: 0x00011733 File Offset: 0x0000F933
		public void AddEditDataUser(UIntPtr meshPointer)
		{
			ScriptingInterfaceOfIMetaMesh.call_AddEditDataUserDelegate(meshPointer);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00011740 File Offset: 0x0000F940
		public void AddMesh(UIntPtr multiMeshPointer, UIntPtr meshPointer, uint lodLevel)
		{
			ScriptingInterfaceOfIMetaMesh.call_AddMeshDelegate(multiMeshPointer, meshPointer, lodLevel);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0001174F File Offset: 0x0000F94F
		public void AddMetaMesh(UIntPtr metaMeshPtr, UIntPtr otherMetaMeshPointer)
		{
			ScriptingInterfaceOfIMetaMesh.call_AddMetaMeshDelegate(metaMeshPtr, otherMetaMeshPointer);
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0001175D File Offset: 0x0000F95D
		public void AssignClothBodyFrom(UIntPtr multiMeshPointer, UIntPtr multiMeshToMergePointer)
		{
			ScriptingInterfaceOfIMetaMesh.call_AssignClothBodyFromDelegate(multiMeshPointer, multiMeshToMergePointer);
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0001176B File Offset: 0x0000F96B
		public void BatchMultiMeshes(UIntPtr multiMeshPointer, UIntPtr multiMeshToMergePointer)
		{
			ScriptingInterfaceOfIMetaMesh.call_BatchMultiMeshesDelegate(multiMeshPointer, multiMeshToMergePointer);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0001177C File Offset: 0x0000F97C
		public void BatchMultiMeshesMultiple(UIntPtr multiMeshPointer, UIntPtr[] multiMeshToMergePointers, int metaMeshCount)
		{
			PinnedArrayData<UIntPtr> pinnedArrayData = new PinnedArrayData<UIntPtr>(multiMeshToMergePointers, false);
			IntPtr pointer = pinnedArrayData.Pointer;
			ScriptingInterfaceOfIMetaMesh.call_BatchMultiMeshesMultipleDelegate(multiMeshPointer, pointer, metaMeshCount);
			pinnedArrayData.Dispose();
		}

		// Token: 0x060002BE RID: 702 RVA: 0x000117B0 File Offset: 0x0000F9B0
		public void CheckMetaMeshExistence(string multiMeshPrefixName, int lod_count_check)
		{
			byte[] array = null;
			if (multiMeshPrefixName != null)
			{
				int byteCount = ScriptingInterfaceOfIMetaMesh._utf8.GetByteCount(multiMeshPrefixName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMetaMesh._utf8.GetBytes(multiMeshPrefixName, 0, multiMeshPrefixName.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIMetaMesh.call_CheckMetaMeshExistenceDelegate(array, lod_count_check);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0001180B File Offset: 0x0000FA0B
		public int CheckResources(UIntPtr meshPointer)
		{
			return ScriptingInterfaceOfIMetaMesh.call_CheckResourcesDelegate(meshPointer);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00011818 File Offset: 0x0000FA18
		public void ClearEditData(UIntPtr multiMeshPointer)
		{
			ScriptingInterfaceOfIMetaMesh.call_ClearEditDataDelegate(multiMeshPointer);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00011825 File Offset: 0x0000FA25
		public void ClearMeshes(UIntPtr multiMeshPointer)
		{
			ScriptingInterfaceOfIMetaMesh.call_ClearMeshesDelegate(multiMeshPointer);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00011832 File Offset: 0x0000FA32
		public void ClearMeshesForLod(UIntPtr multiMeshPointer, int lodToClear)
		{
			ScriptingInterfaceOfIMetaMesh.call_ClearMeshesForLodDelegate(multiMeshPointer, lodToClear);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00011840 File Offset: 0x0000FA40
		public void ClearMeshesForLowerLods(UIntPtr multiMeshPointer, int lod)
		{
			ScriptingInterfaceOfIMetaMesh.call_ClearMeshesForLowerLodsDelegate(multiMeshPointer, lod);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0001184E File Offset: 0x0000FA4E
		public void ClearMeshesForOtherLods(UIntPtr multiMeshPointer, int lodToKeep)
		{
			ScriptingInterfaceOfIMetaMesh.call_ClearMeshesForOtherLodsDelegate(multiMeshPointer, lodToKeep);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0001185C File Offset: 0x0000FA5C
		public void CopyTo(UIntPtr metaMesh, UIntPtr targetMesh, bool copyMeshes)
		{
			ScriptingInterfaceOfIMetaMesh.call_CopyToDelegate(metaMesh, targetMesh, copyMeshes);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0001186C File Offset: 0x0000FA6C
		public MetaMesh CreateCopy(UIntPtr ptr)
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIMetaMesh.call_CreateCopyDelegate(ptr);
			MetaMesh result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new MetaMesh(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x000118B8 File Offset: 0x0000FAB8
		public MetaMesh CreateCopyFromName(string multiMeshPrefixName, bool showErrors, bool mayReturnNull)
		{
			byte[] array = null;
			if (multiMeshPrefixName != null)
			{
				int byteCount = ScriptingInterfaceOfIMetaMesh._utf8.GetByteCount(multiMeshPrefixName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMetaMesh._utf8.GetBytes(multiMeshPrefixName, 0, multiMeshPrefixName.Length, array, 0);
				array[byteCount] = 0;
			}
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIMetaMesh.call_CreateCopyFromNameDelegate(array, showErrors, mayReturnNull);
			MetaMesh result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new MetaMesh(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00011948 File Offset: 0x0000FB48
		public MetaMesh CreateMetaMesh(string name)
		{
			byte[] array = null;
			if (name != null)
			{
				int byteCount = ScriptingInterfaceOfIMetaMesh._utf8.GetByteCount(name);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMetaMesh._utf8.GetBytes(name, 0, name.Length, array, 0);
				array[byteCount] = 0;
			}
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIMetaMesh.call_CreateMetaMeshDelegate(array);
			MetaMesh result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new MetaMesh(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x000119D4 File Offset: 0x0000FBD4
		public void DrawTextWithDefaultFont(UIntPtr multiMeshPointer, string text, Vec2 textPositionMin, Vec2 textPositionMax, Vec2 size, uint color, TextFlags flags)
		{
			byte[] array = null;
			if (text != null)
			{
				int byteCount = ScriptingInterfaceOfIMetaMesh._utf8.GetByteCount(text);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMetaMesh._utf8.GetBytes(text, 0, text.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIMetaMesh.call_DrawTextWithDefaultFontDelegate(multiMeshPointer, array, textPositionMin, textPositionMax, size, color, flags);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00011A38 File Offset: 0x0000FC38
		public int GetAllMultiMeshes(UIntPtr[] gameEntitiesTemp)
		{
			PinnedArrayData<UIntPtr> pinnedArrayData = new PinnedArrayData<UIntPtr>(gameEntitiesTemp, false);
			IntPtr pointer = pinnedArrayData.Pointer;
			int result = ScriptingInterfaceOfIMetaMesh.call_GetAllMultiMeshesDelegate(pointer);
			pinnedArrayData.Dispose();
			return result;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00011A68 File Offset: 0x0000FC68
		public void GetBoundingBox(UIntPtr multiMeshPointer, ref BoundingBox outBoundingBox)
		{
			ScriptingInterfaceOfIMetaMesh.call_GetBoundingBoxDelegate(multiMeshPointer, ref outBoundingBox);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00011A76 File Offset: 0x0000FC76
		public uint GetFactor1(UIntPtr multiMeshPointer)
		{
			return ScriptingInterfaceOfIMetaMesh.call_GetFactor1Delegate(multiMeshPointer);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00011A83 File Offset: 0x0000FC83
		public uint GetFactor2(UIntPtr multiMeshPointer)
		{
			return ScriptingInterfaceOfIMetaMesh.call_GetFactor2Delegate(multiMeshPointer);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00011A90 File Offset: 0x0000FC90
		public void GetFrame(UIntPtr multiMeshPointer, ref MatrixFrame outFrame)
		{
			ScriptingInterfaceOfIMetaMesh.call_GetFrameDelegate(multiMeshPointer, ref outFrame);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00011A9E File Offset: 0x0000FC9E
		public int GetLodMaskForMeshAtIndex(UIntPtr multiMeshPointer, int meshIndex)
		{
			return ScriptingInterfaceOfIMetaMesh.call_GetLodMaskForMeshAtIndexDelegate(multiMeshPointer, meshIndex);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00011AAC File Offset: 0x0000FCAC
		public Mesh GetMeshAtIndex(UIntPtr multiMeshPointer, int meshIndex)
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIMetaMesh.call_GetMeshAtIndexDelegate(multiMeshPointer, meshIndex);
			Mesh result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new Mesh(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00011AF7 File Offset: 0x0000FCF7
		public int GetMeshCount(UIntPtr multiMeshPointer)
		{
			return ScriptingInterfaceOfIMetaMesh.call_GetMeshCountDelegate(multiMeshPointer);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00011B04 File Offset: 0x0000FD04
		public int GetMeshCountWithTag(UIntPtr multiMeshPointer, string tag)
		{
			byte[] array = null;
			if (tag != null)
			{
				int byteCount = ScriptingInterfaceOfIMetaMesh._utf8.GetByteCount(tag);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMetaMesh._utf8.GetBytes(tag, 0, tag.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfIMetaMesh.call_GetMeshCountWithTagDelegate(multiMeshPointer, array);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00011B60 File Offset: 0x0000FD60
		public MetaMesh GetMorphedCopy(string multiMeshName, float morphTarget, bool showErrors)
		{
			byte[] array = null;
			if (multiMeshName != null)
			{
				int byteCount = ScriptingInterfaceOfIMetaMesh._utf8.GetByteCount(multiMeshName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMetaMesh._utf8.GetBytes(multiMeshName, 0, multiMeshName.Length, array, 0);
				array[byteCount] = 0;
			}
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIMetaMesh.call_GetMorphedCopyDelegate(array, morphTarget, showErrors);
			MetaMesh result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new MetaMesh(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00011BF0 File Offset: 0x0000FDF0
		public MetaMesh GetMultiMesh(string name)
		{
			byte[] array = null;
			if (name != null)
			{
				int byteCount = ScriptingInterfaceOfIMetaMesh._utf8.GetByteCount(name);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMetaMesh._utf8.GetBytes(name, 0, name.Length, array, 0);
				array[byteCount] = 0;
			}
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIMetaMesh.call_GetMultiMeshDelegate(array);
			MetaMesh result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new MetaMesh(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00011C7C File Offset: 0x0000FE7C
		public int GetMultiMeshCount()
		{
			return ScriptingInterfaceOfIMetaMesh.call_GetMultiMeshCountDelegate();
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00011C88 File Offset: 0x0000FE88
		public string GetName(UIntPtr multiMeshPointer)
		{
			if (ScriptingInterfaceOfIMetaMesh.call_GetNameDelegate(multiMeshPointer) != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00011C9F File Offset: 0x0000FE9F
		public int GetTotalGpuSize(UIntPtr multiMeshPointer)
		{
			return ScriptingInterfaceOfIMetaMesh.call_GetTotalGpuSizeDelegate(multiMeshPointer);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00011CAC File Offset: 0x0000FEAC
		public Vec3 GetVectorArgument2(UIntPtr multiMeshPointer)
		{
			return ScriptingInterfaceOfIMetaMesh.call_GetVectorArgument2Delegate(multiMeshPointer);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00011CB9 File Offset: 0x0000FEB9
		public Vec3 GetVectorUserData(UIntPtr multiMeshPointer)
		{
			return ScriptingInterfaceOfIMetaMesh.call_GetVectorUserDataDelegate(multiMeshPointer);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00011CC6 File Offset: 0x0000FEC6
		public VisibilityMaskFlags GetVisibilityMask(UIntPtr multiMeshPointer)
		{
			return ScriptingInterfaceOfIMetaMesh.call_GetVisibilityMaskDelegate(multiMeshPointer);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00011CD3 File Offset: 0x0000FED3
		public bool HasAnyGeneratedLods(UIntPtr multiMeshPointer)
		{
			return ScriptingInterfaceOfIMetaMesh.call_HasAnyGeneratedLodsDelegate(multiMeshPointer);
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00011CE0 File Offset: 0x0000FEE0
		public bool HasAnyLods(UIntPtr multiMeshPointer)
		{
			return ScriptingInterfaceOfIMetaMesh.call_HasAnyLodsDelegate(multiMeshPointer);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00011CED File Offset: 0x0000FEED
		public bool HasClothData(UIntPtr multiMeshPointer)
		{
			return ScriptingInterfaceOfIMetaMesh.call_HasClothDataDelegate(multiMeshPointer);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00011CFA File Offset: 0x0000FEFA
		public bool HasVertexBufferOrEditDataOrPackageItem(UIntPtr multiMeshPointer)
		{
			return ScriptingInterfaceOfIMetaMesh.call_HasVertexBufferOrEditDataOrPackageItemDelegate(multiMeshPointer);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00011D07 File Offset: 0x0000FF07
		public void MergeMultiMeshes(UIntPtr multiMeshPointer, UIntPtr multiMeshToMergePointer)
		{
			ScriptingInterfaceOfIMetaMesh.call_MergeMultiMeshesDelegate(multiMeshPointer, multiMeshToMergePointer);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00011D15 File Offset: 0x0000FF15
		public void PreloadForRendering(UIntPtr multiMeshPointer)
		{
			ScriptingInterfaceOfIMetaMesh.call_PreloadForRenderingDelegate(multiMeshPointer);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00011D22 File Offset: 0x0000FF22
		public void PreloadShaders(UIntPtr multiMeshPointer, bool useTableau, bool useTeamColor)
		{
			ScriptingInterfaceOfIMetaMesh.call_PreloadShadersDelegate(multiMeshPointer, useTableau, useTeamColor);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00011D31 File Offset: 0x0000FF31
		public void RecomputeBoundingBox(UIntPtr multiMeshPointer, bool recomputeMeshes)
		{
			ScriptingInterfaceOfIMetaMesh.call_RecomputeBoundingBoxDelegate(multiMeshPointer, recomputeMeshes);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00011D3F File Offset: 0x0000FF3F
		public void Release(UIntPtr multiMeshPointer)
		{
			ScriptingInterfaceOfIMetaMesh.call_ReleaseDelegate(multiMeshPointer);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00011D4C File Offset: 0x0000FF4C
		public void ReleaseEditDataUser(UIntPtr meshPointer)
		{
			ScriptingInterfaceOfIMetaMesh.call_ReleaseEditDataUserDelegate(meshPointer);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00011D5C File Offset: 0x0000FF5C
		public int RemoveMeshesWithoutTag(UIntPtr multiMeshPointer, string tag)
		{
			byte[] array = null;
			if (tag != null)
			{
				int byteCount = ScriptingInterfaceOfIMetaMesh._utf8.GetByteCount(tag);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMetaMesh._utf8.GetBytes(tag, 0, tag.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfIMetaMesh.call_RemoveMeshesWithoutTagDelegate(multiMeshPointer, array);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00011DB8 File Offset: 0x0000FFB8
		public int RemoveMeshesWithTag(UIntPtr multiMeshPointer, string tag)
		{
			byte[] array = null;
			if (tag != null)
			{
				int byteCount = ScriptingInterfaceOfIMetaMesh._utf8.GetByteCount(tag);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMetaMesh._utf8.GetBytes(tag, 0, tag.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfIMetaMesh.call_RemoveMeshesWithTagDelegate(multiMeshPointer, array);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00011E13 File Offset: 0x00010013
		public void SetBillboarding(UIntPtr multiMeshPointer, BillboardType billboard)
		{
			ScriptingInterfaceOfIMetaMesh.call_SetBillboardingDelegate(multiMeshPointer, billboard);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00011E21 File Offset: 0x00010021
		public void SetContourColor(UIntPtr meshPointer, uint color)
		{
			ScriptingInterfaceOfIMetaMesh.call_SetContourColorDelegate(meshPointer, color);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x00011E2F File Offset: 0x0001002F
		public void SetContourState(UIntPtr meshPointer, bool alwaysVisible)
		{
			ScriptingInterfaceOfIMetaMesh.call_SetContourStateDelegate(meshPointer, alwaysVisible);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00011E3D File Offset: 0x0001003D
		public void SetCullMode(UIntPtr metaMeshPtr, MBMeshCullingMode cullMode)
		{
			ScriptingInterfaceOfIMetaMesh.call_SetCullModeDelegate(metaMeshPtr, cullMode);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00011E4B File Offset: 0x0001004B
		public void SetEditDataPolicy(UIntPtr meshPointer, EditDataPolicy policy)
		{
			ScriptingInterfaceOfIMetaMesh.call_SetEditDataPolicyDelegate(meshPointer, policy);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00011E59 File Offset: 0x00010059
		public void SetFactor1(UIntPtr multiMeshPointer, uint factorColor1)
		{
			ScriptingInterfaceOfIMetaMesh.call_SetFactor1Delegate(multiMeshPointer, factorColor1);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00011E67 File Offset: 0x00010067
		public void SetFactor1Linear(UIntPtr multiMeshPointer, uint linearFactorColor1)
		{
			ScriptingInterfaceOfIMetaMesh.call_SetFactor1LinearDelegate(multiMeshPointer, linearFactorColor1);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00011E75 File Offset: 0x00010075
		public void SetFactor2(UIntPtr multiMeshPointer, uint factorColor2)
		{
			ScriptingInterfaceOfIMetaMesh.call_SetFactor2Delegate(multiMeshPointer, factorColor2);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00011E83 File Offset: 0x00010083
		public void SetFactor2Linear(UIntPtr multiMeshPointer, uint linearFactorColor2)
		{
			ScriptingInterfaceOfIMetaMesh.call_SetFactor2LinearDelegate(multiMeshPointer, linearFactorColor2);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00011E94 File Offset: 0x00010094
		public void SetFactorColorToSubMeshesWithTag(UIntPtr meshPointer, uint color, string tag)
		{
			byte[] array = null;
			if (tag != null)
			{
				int byteCount = ScriptingInterfaceOfIMetaMesh._utf8.GetByteCount(tag);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMetaMesh._utf8.GetBytes(tag, 0, tag.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIMetaMesh.call_SetFactorColorToSubMeshesWithTagDelegate(meshPointer, color, array);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00011EF0 File Offset: 0x000100F0
		public void SetFrame(UIntPtr multiMeshPointer, ref MatrixFrame meshFrame)
		{
			ScriptingInterfaceOfIMetaMesh.call_SetFrameDelegate(multiMeshPointer, ref meshFrame);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00011EFE File Offset: 0x000100FE
		public void SetGlossMultiplier(UIntPtr multiMeshPointer, float value)
		{
			ScriptingInterfaceOfIMetaMesh.call_SetGlossMultiplierDelegate(multiMeshPointer, value);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00011F0C File Offset: 0x0001010C
		public void SetLodBias(UIntPtr multiMeshPointer, int lod_bias)
		{
			ScriptingInterfaceOfIMetaMesh.call_SetLodBiasDelegate(multiMeshPointer, lod_bias);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00011F1A File Offset: 0x0001011A
		public void SetMaterial(UIntPtr multiMeshPointer, UIntPtr materialPointer)
		{
			ScriptingInterfaceOfIMetaMesh.call_SetMaterialDelegate(multiMeshPointer, materialPointer);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00011F28 File Offset: 0x00010128
		public void SetMaterialToSubMeshesWithTag(UIntPtr meshPointer, UIntPtr materialPointer, string tag)
		{
			byte[] array = null;
			if (tag != null)
			{
				int byteCount = ScriptingInterfaceOfIMetaMesh._utf8.GetByteCount(tag);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMetaMesh._utf8.GetBytes(tag, 0, tag.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIMetaMesh.call_SetMaterialToSubMeshesWithTagDelegate(meshPointer, materialPointer, array);
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00011F84 File Offset: 0x00010184
		public void SetNumLods(UIntPtr multiMeshPointer, int num_lod)
		{
			ScriptingInterfaceOfIMetaMesh.call_SetNumLodsDelegate(multiMeshPointer, num_lod);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00011F92 File Offset: 0x00010192
		public void SetVectorArgument(UIntPtr multiMeshPointer, float vectorArgument0, float vectorArgument1, float vectorArgument2, float vectorArgument3)
		{
			ScriptingInterfaceOfIMetaMesh.call_SetVectorArgumentDelegate(multiMeshPointer, vectorArgument0, vectorArgument1, vectorArgument2, vectorArgument3);
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00011FA5 File Offset: 0x000101A5
		public void SetVectorArgument2(UIntPtr multiMeshPointer, float vectorArgument0, float vectorArgument1, float vectorArgument2, float vectorArgument3)
		{
			ScriptingInterfaceOfIMetaMesh.call_SetVectorArgument2Delegate(multiMeshPointer, vectorArgument0, vectorArgument1, vectorArgument2, vectorArgument3);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00011FB8 File Offset: 0x000101B8
		public void SetVectorUserData(UIntPtr multiMeshPointer, ref Vec3 vectorArg)
		{
			ScriptingInterfaceOfIMetaMesh.call_SetVectorUserDataDelegate(multiMeshPointer, ref vectorArg);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00011FC6 File Offset: 0x000101C6
		public void SetVisibilityMask(UIntPtr multiMeshPointer, VisibilityMaskFlags visibilityMask)
		{
			ScriptingInterfaceOfIMetaMesh.call_SetVisibilityMaskDelegate(multiMeshPointer, visibilityMask);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00011FD4 File Offset: 0x000101D4
		public void UseHeadBoneFaceGenScaling(UIntPtr multiMeshPointer, UIntPtr skeleton, sbyte headLookDirectionBoneIndex, ref MatrixFrame frame)
		{
			ScriptingInterfaceOfIMetaMesh.call_UseHeadBoneFaceGenScalingDelegate(multiMeshPointer, skeleton, headLookDirectionBoneIndex, ref frame);
		}

		// Token: 0x04000252 RID: 594
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x04000253 RID: 595
		public static ScriptingInterfaceOfIMetaMesh.AddEditDataUserDelegate call_AddEditDataUserDelegate;

		// Token: 0x04000254 RID: 596
		public static ScriptingInterfaceOfIMetaMesh.AddMeshDelegate call_AddMeshDelegate;

		// Token: 0x04000255 RID: 597
		public static ScriptingInterfaceOfIMetaMesh.AddMetaMeshDelegate call_AddMetaMeshDelegate;

		// Token: 0x04000256 RID: 598
		public static ScriptingInterfaceOfIMetaMesh.AssignClothBodyFromDelegate call_AssignClothBodyFromDelegate;

		// Token: 0x04000257 RID: 599
		public static ScriptingInterfaceOfIMetaMesh.BatchMultiMeshesDelegate call_BatchMultiMeshesDelegate;

		// Token: 0x04000258 RID: 600
		public static ScriptingInterfaceOfIMetaMesh.BatchMultiMeshesMultipleDelegate call_BatchMultiMeshesMultipleDelegate;

		// Token: 0x04000259 RID: 601
		public static ScriptingInterfaceOfIMetaMesh.CheckMetaMeshExistenceDelegate call_CheckMetaMeshExistenceDelegate;

		// Token: 0x0400025A RID: 602
		public static ScriptingInterfaceOfIMetaMesh.CheckResourcesDelegate call_CheckResourcesDelegate;

		// Token: 0x0400025B RID: 603
		public static ScriptingInterfaceOfIMetaMesh.ClearEditDataDelegate call_ClearEditDataDelegate;

		// Token: 0x0400025C RID: 604
		public static ScriptingInterfaceOfIMetaMesh.ClearMeshesDelegate call_ClearMeshesDelegate;

		// Token: 0x0400025D RID: 605
		public static ScriptingInterfaceOfIMetaMesh.ClearMeshesForLodDelegate call_ClearMeshesForLodDelegate;

		// Token: 0x0400025E RID: 606
		public static ScriptingInterfaceOfIMetaMesh.ClearMeshesForLowerLodsDelegate call_ClearMeshesForLowerLodsDelegate;

		// Token: 0x0400025F RID: 607
		public static ScriptingInterfaceOfIMetaMesh.ClearMeshesForOtherLodsDelegate call_ClearMeshesForOtherLodsDelegate;

		// Token: 0x04000260 RID: 608
		public static ScriptingInterfaceOfIMetaMesh.CopyToDelegate call_CopyToDelegate;

		// Token: 0x04000261 RID: 609
		public static ScriptingInterfaceOfIMetaMesh.CreateCopyDelegate call_CreateCopyDelegate;

		// Token: 0x04000262 RID: 610
		public static ScriptingInterfaceOfIMetaMesh.CreateCopyFromNameDelegate call_CreateCopyFromNameDelegate;

		// Token: 0x04000263 RID: 611
		public static ScriptingInterfaceOfIMetaMesh.CreateMetaMeshDelegate call_CreateMetaMeshDelegate;

		// Token: 0x04000264 RID: 612
		public static ScriptingInterfaceOfIMetaMesh.DrawTextWithDefaultFontDelegate call_DrawTextWithDefaultFontDelegate;

		// Token: 0x04000265 RID: 613
		public static ScriptingInterfaceOfIMetaMesh.GetAllMultiMeshesDelegate call_GetAllMultiMeshesDelegate;

		// Token: 0x04000266 RID: 614
		public static ScriptingInterfaceOfIMetaMesh.GetBoundingBoxDelegate call_GetBoundingBoxDelegate;

		// Token: 0x04000267 RID: 615
		public static ScriptingInterfaceOfIMetaMesh.GetFactor1Delegate call_GetFactor1Delegate;

		// Token: 0x04000268 RID: 616
		public static ScriptingInterfaceOfIMetaMesh.GetFactor2Delegate call_GetFactor2Delegate;

		// Token: 0x04000269 RID: 617
		public static ScriptingInterfaceOfIMetaMesh.GetFrameDelegate call_GetFrameDelegate;

		// Token: 0x0400026A RID: 618
		public static ScriptingInterfaceOfIMetaMesh.GetLodMaskForMeshAtIndexDelegate call_GetLodMaskForMeshAtIndexDelegate;

		// Token: 0x0400026B RID: 619
		public static ScriptingInterfaceOfIMetaMesh.GetMeshAtIndexDelegate call_GetMeshAtIndexDelegate;

		// Token: 0x0400026C RID: 620
		public static ScriptingInterfaceOfIMetaMesh.GetMeshCountDelegate call_GetMeshCountDelegate;

		// Token: 0x0400026D RID: 621
		public static ScriptingInterfaceOfIMetaMesh.GetMeshCountWithTagDelegate call_GetMeshCountWithTagDelegate;

		// Token: 0x0400026E RID: 622
		public static ScriptingInterfaceOfIMetaMesh.GetMorphedCopyDelegate call_GetMorphedCopyDelegate;

		// Token: 0x0400026F RID: 623
		public static ScriptingInterfaceOfIMetaMesh.GetMultiMeshDelegate call_GetMultiMeshDelegate;

		// Token: 0x04000270 RID: 624
		public static ScriptingInterfaceOfIMetaMesh.GetMultiMeshCountDelegate call_GetMultiMeshCountDelegate;

		// Token: 0x04000271 RID: 625
		public static ScriptingInterfaceOfIMetaMesh.GetNameDelegate call_GetNameDelegate;

		// Token: 0x04000272 RID: 626
		public static ScriptingInterfaceOfIMetaMesh.GetTotalGpuSizeDelegate call_GetTotalGpuSizeDelegate;

		// Token: 0x04000273 RID: 627
		public static ScriptingInterfaceOfIMetaMesh.GetVectorArgument2Delegate call_GetVectorArgument2Delegate;

		// Token: 0x04000274 RID: 628
		public static ScriptingInterfaceOfIMetaMesh.GetVectorUserDataDelegate call_GetVectorUserDataDelegate;

		// Token: 0x04000275 RID: 629
		public static ScriptingInterfaceOfIMetaMesh.GetVisibilityMaskDelegate call_GetVisibilityMaskDelegate;

		// Token: 0x04000276 RID: 630
		public static ScriptingInterfaceOfIMetaMesh.HasAnyGeneratedLodsDelegate call_HasAnyGeneratedLodsDelegate;

		// Token: 0x04000277 RID: 631
		public static ScriptingInterfaceOfIMetaMesh.HasAnyLodsDelegate call_HasAnyLodsDelegate;

		// Token: 0x04000278 RID: 632
		public static ScriptingInterfaceOfIMetaMesh.HasClothDataDelegate call_HasClothDataDelegate;

		// Token: 0x04000279 RID: 633
		public static ScriptingInterfaceOfIMetaMesh.HasVertexBufferOrEditDataOrPackageItemDelegate call_HasVertexBufferOrEditDataOrPackageItemDelegate;

		// Token: 0x0400027A RID: 634
		public static ScriptingInterfaceOfIMetaMesh.MergeMultiMeshesDelegate call_MergeMultiMeshesDelegate;

		// Token: 0x0400027B RID: 635
		public static ScriptingInterfaceOfIMetaMesh.PreloadForRenderingDelegate call_PreloadForRenderingDelegate;

		// Token: 0x0400027C RID: 636
		public static ScriptingInterfaceOfIMetaMesh.PreloadShadersDelegate call_PreloadShadersDelegate;

		// Token: 0x0400027D RID: 637
		public static ScriptingInterfaceOfIMetaMesh.RecomputeBoundingBoxDelegate call_RecomputeBoundingBoxDelegate;

		// Token: 0x0400027E RID: 638
		public static ScriptingInterfaceOfIMetaMesh.ReleaseDelegate call_ReleaseDelegate;

		// Token: 0x0400027F RID: 639
		public static ScriptingInterfaceOfIMetaMesh.ReleaseEditDataUserDelegate call_ReleaseEditDataUserDelegate;

		// Token: 0x04000280 RID: 640
		public static ScriptingInterfaceOfIMetaMesh.RemoveMeshesWithoutTagDelegate call_RemoveMeshesWithoutTagDelegate;

		// Token: 0x04000281 RID: 641
		public static ScriptingInterfaceOfIMetaMesh.RemoveMeshesWithTagDelegate call_RemoveMeshesWithTagDelegate;

		// Token: 0x04000282 RID: 642
		public static ScriptingInterfaceOfIMetaMesh.SetBillboardingDelegate call_SetBillboardingDelegate;

		// Token: 0x04000283 RID: 643
		public static ScriptingInterfaceOfIMetaMesh.SetContourColorDelegate call_SetContourColorDelegate;

		// Token: 0x04000284 RID: 644
		public static ScriptingInterfaceOfIMetaMesh.SetContourStateDelegate call_SetContourStateDelegate;

		// Token: 0x04000285 RID: 645
		public static ScriptingInterfaceOfIMetaMesh.SetCullModeDelegate call_SetCullModeDelegate;

		// Token: 0x04000286 RID: 646
		public static ScriptingInterfaceOfIMetaMesh.SetEditDataPolicyDelegate call_SetEditDataPolicyDelegate;

		// Token: 0x04000287 RID: 647
		public static ScriptingInterfaceOfIMetaMesh.SetFactor1Delegate call_SetFactor1Delegate;

		// Token: 0x04000288 RID: 648
		public static ScriptingInterfaceOfIMetaMesh.SetFactor1LinearDelegate call_SetFactor1LinearDelegate;

		// Token: 0x04000289 RID: 649
		public static ScriptingInterfaceOfIMetaMesh.SetFactor2Delegate call_SetFactor2Delegate;

		// Token: 0x0400028A RID: 650
		public static ScriptingInterfaceOfIMetaMesh.SetFactor2LinearDelegate call_SetFactor2LinearDelegate;

		// Token: 0x0400028B RID: 651
		public static ScriptingInterfaceOfIMetaMesh.SetFactorColorToSubMeshesWithTagDelegate call_SetFactorColorToSubMeshesWithTagDelegate;

		// Token: 0x0400028C RID: 652
		public static ScriptingInterfaceOfIMetaMesh.SetFrameDelegate call_SetFrameDelegate;

		// Token: 0x0400028D RID: 653
		public static ScriptingInterfaceOfIMetaMesh.SetGlossMultiplierDelegate call_SetGlossMultiplierDelegate;

		// Token: 0x0400028E RID: 654
		public static ScriptingInterfaceOfIMetaMesh.SetLodBiasDelegate call_SetLodBiasDelegate;

		// Token: 0x0400028F RID: 655
		public static ScriptingInterfaceOfIMetaMesh.SetMaterialDelegate call_SetMaterialDelegate;

		// Token: 0x04000290 RID: 656
		public static ScriptingInterfaceOfIMetaMesh.SetMaterialToSubMeshesWithTagDelegate call_SetMaterialToSubMeshesWithTagDelegate;

		// Token: 0x04000291 RID: 657
		public static ScriptingInterfaceOfIMetaMesh.SetNumLodsDelegate call_SetNumLodsDelegate;

		// Token: 0x04000292 RID: 658
		public static ScriptingInterfaceOfIMetaMesh.SetVectorArgumentDelegate call_SetVectorArgumentDelegate;

		// Token: 0x04000293 RID: 659
		public static ScriptingInterfaceOfIMetaMesh.SetVectorArgument2Delegate call_SetVectorArgument2Delegate;

		// Token: 0x04000294 RID: 660
		public static ScriptingInterfaceOfIMetaMesh.SetVectorUserDataDelegate call_SetVectorUserDataDelegate;

		// Token: 0x04000295 RID: 661
		public static ScriptingInterfaceOfIMetaMesh.SetVisibilityMaskDelegate call_SetVisibilityMaskDelegate;

		// Token: 0x04000296 RID: 662
		public static ScriptingInterfaceOfIMetaMesh.UseHeadBoneFaceGenScalingDelegate call_UseHeadBoneFaceGenScalingDelegate;

		// Token: 0x020002B1 RID: 689
		// (Invoke) Token: 0x06000FC7 RID: 4039
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddEditDataUserDelegate(UIntPtr meshPointer);

		// Token: 0x020002B2 RID: 690
		// (Invoke) Token: 0x06000FCB RID: 4043
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddMeshDelegate(UIntPtr multiMeshPointer, UIntPtr meshPointer, uint lodLevel);

		// Token: 0x020002B3 RID: 691
		// (Invoke) Token: 0x06000FCF RID: 4047
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddMetaMeshDelegate(UIntPtr metaMeshPtr, UIntPtr otherMetaMeshPointer);

		// Token: 0x020002B4 RID: 692
		// (Invoke) Token: 0x06000FD3 RID: 4051
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AssignClothBodyFromDelegate(UIntPtr multiMeshPointer, UIntPtr multiMeshToMergePointer);

		// Token: 0x020002B5 RID: 693
		// (Invoke) Token: 0x06000FD7 RID: 4055
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void BatchMultiMeshesDelegate(UIntPtr multiMeshPointer, UIntPtr multiMeshToMergePointer);

		// Token: 0x020002B6 RID: 694
		// (Invoke) Token: 0x06000FDB RID: 4059
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void BatchMultiMeshesMultipleDelegate(UIntPtr multiMeshPointer, IntPtr multiMeshToMergePointers, int metaMeshCount);

		// Token: 0x020002B7 RID: 695
		// (Invoke) Token: 0x06000FDF RID: 4063
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void CheckMetaMeshExistenceDelegate(byte[] multiMeshPrefixName, int lod_count_check);

		// Token: 0x020002B8 RID: 696
		// (Invoke) Token: 0x06000FE3 RID: 4067
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int CheckResourcesDelegate(UIntPtr meshPointer);

		// Token: 0x020002B9 RID: 697
		// (Invoke) Token: 0x06000FE7 RID: 4071
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ClearEditDataDelegate(UIntPtr multiMeshPointer);

		// Token: 0x020002BA RID: 698
		// (Invoke) Token: 0x06000FEB RID: 4075
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ClearMeshesDelegate(UIntPtr multiMeshPointer);

		// Token: 0x020002BB RID: 699
		// (Invoke) Token: 0x06000FEF RID: 4079
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ClearMeshesForLodDelegate(UIntPtr multiMeshPointer, int lodToClear);

		// Token: 0x020002BC RID: 700
		// (Invoke) Token: 0x06000FF3 RID: 4083
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ClearMeshesForLowerLodsDelegate(UIntPtr multiMeshPointer, int lod);

		// Token: 0x020002BD RID: 701
		// (Invoke) Token: 0x06000FF7 RID: 4087
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ClearMeshesForOtherLodsDelegate(UIntPtr multiMeshPointer, int lodToKeep);

		// Token: 0x020002BE RID: 702
		// (Invoke) Token: 0x06000FFB RID: 4091
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void CopyToDelegate(UIntPtr metaMesh, UIntPtr targetMesh, [MarshalAs(UnmanagedType.U1)] bool copyMeshes);

		// Token: 0x020002BF RID: 703
		// (Invoke) Token: 0x06000FFF RID: 4095
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer CreateCopyDelegate(UIntPtr ptr);

		// Token: 0x020002C0 RID: 704
		// (Invoke) Token: 0x06001003 RID: 4099
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer CreateCopyFromNameDelegate(byte[] multiMeshPrefixName, [MarshalAs(UnmanagedType.U1)] bool showErrors, [MarshalAs(UnmanagedType.U1)] bool mayReturnNull);

		// Token: 0x020002C1 RID: 705
		// (Invoke) Token: 0x06001007 RID: 4103
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer CreateMetaMeshDelegate(byte[] name);

		// Token: 0x020002C2 RID: 706
		// (Invoke) Token: 0x0600100B RID: 4107
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void DrawTextWithDefaultFontDelegate(UIntPtr multiMeshPointer, byte[] text, Vec2 textPositionMin, Vec2 textPositionMax, Vec2 size, uint color, TextFlags flags);

		// Token: 0x020002C3 RID: 707
		// (Invoke) Token: 0x0600100F RID: 4111
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetAllMultiMeshesDelegate(IntPtr gameEntitiesTemp);

		// Token: 0x020002C4 RID: 708
		// (Invoke) Token: 0x06001013 RID: 4115
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetBoundingBoxDelegate(UIntPtr multiMeshPointer, ref BoundingBox outBoundingBox);

		// Token: 0x020002C5 RID: 709
		// (Invoke) Token: 0x06001017 RID: 4119
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate uint GetFactor1Delegate(UIntPtr multiMeshPointer);

		// Token: 0x020002C6 RID: 710
		// (Invoke) Token: 0x0600101B RID: 4123
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate uint GetFactor2Delegate(UIntPtr multiMeshPointer);

		// Token: 0x020002C7 RID: 711
		// (Invoke) Token: 0x0600101F RID: 4127
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetFrameDelegate(UIntPtr multiMeshPointer, ref MatrixFrame outFrame);

		// Token: 0x020002C8 RID: 712
		// (Invoke) Token: 0x06001023 RID: 4131
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetLodMaskForMeshAtIndexDelegate(UIntPtr multiMeshPointer, int meshIndex);

		// Token: 0x020002C9 RID: 713
		// (Invoke) Token: 0x06001027 RID: 4135
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer GetMeshAtIndexDelegate(UIntPtr multiMeshPointer, int meshIndex);

		// Token: 0x020002CA RID: 714
		// (Invoke) Token: 0x0600102B RID: 4139
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetMeshCountDelegate(UIntPtr multiMeshPointer);

		// Token: 0x020002CB RID: 715
		// (Invoke) Token: 0x0600102F RID: 4143
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetMeshCountWithTagDelegate(UIntPtr multiMeshPointer, byte[] tag);

		// Token: 0x020002CC RID: 716
		// (Invoke) Token: 0x06001033 RID: 4147
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer GetMorphedCopyDelegate(byte[] multiMeshName, float morphTarget, [MarshalAs(UnmanagedType.U1)] bool showErrors);

		// Token: 0x020002CD RID: 717
		// (Invoke) Token: 0x06001037 RID: 4151
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer GetMultiMeshDelegate(byte[] name);

		// Token: 0x020002CE RID: 718
		// (Invoke) Token: 0x0600103B RID: 4155
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetMultiMeshCountDelegate();

		// Token: 0x020002CF RID: 719
		// (Invoke) Token: 0x0600103F RID: 4159
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetNameDelegate(UIntPtr multiMeshPointer);

		// Token: 0x020002D0 RID: 720
		// (Invoke) Token: 0x06001043 RID: 4163
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetTotalGpuSizeDelegate(UIntPtr multiMeshPointer);

		// Token: 0x020002D1 RID: 721
		// (Invoke) Token: 0x06001047 RID: 4167
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate Vec3 GetVectorArgument2Delegate(UIntPtr multiMeshPointer);

		// Token: 0x020002D2 RID: 722
		// (Invoke) Token: 0x0600104B RID: 4171
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate Vec3 GetVectorUserDataDelegate(UIntPtr multiMeshPointer);

		// Token: 0x020002D3 RID: 723
		// (Invoke) Token: 0x0600104F RID: 4175
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate VisibilityMaskFlags GetVisibilityMaskDelegate(UIntPtr multiMeshPointer);

		// Token: 0x020002D4 RID: 724
		// (Invoke) Token: 0x06001053 RID: 4179
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool HasAnyGeneratedLodsDelegate(UIntPtr multiMeshPointer);

		// Token: 0x020002D5 RID: 725
		// (Invoke) Token: 0x06001057 RID: 4183
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool HasAnyLodsDelegate(UIntPtr multiMeshPointer);

		// Token: 0x020002D6 RID: 726
		// (Invoke) Token: 0x0600105B RID: 4187
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool HasClothDataDelegate(UIntPtr multiMeshPointer);

		// Token: 0x020002D7 RID: 727
		// (Invoke) Token: 0x0600105F RID: 4191
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool HasVertexBufferOrEditDataOrPackageItemDelegate(UIntPtr multiMeshPointer);

		// Token: 0x020002D8 RID: 728
		// (Invoke) Token: 0x06001063 RID: 4195
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void MergeMultiMeshesDelegate(UIntPtr multiMeshPointer, UIntPtr multiMeshToMergePointer);

		// Token: 0x020002D9 RID: 729
		// (Invoke) Token: 0x06001067 RID: 4199
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void PreloadForRenderingDelegate(UIntPtr multiMeshPointer);

		// Token: 0x020002DA RID: 730
		// (Invoke) Token: 0x0600106B RID: 4203
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void PreloadShadersDelegate(UIntPtr multiMeshPointer, [MarshalAs(UnmanagedType.U1)] bool useTableau, [MarshalAs(UnmanagedType.U1)] bool useTeamColor);

		// Token: 0x020002DB RID: 731
		// (Invoke) Token: 0x0600106F RID: 4207
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RecomputeBoundingBoxDelegate(UIntPtr multiMeshPointer, [MarshalAs(UnmanagedType.U1)] bool recomputeMeshes);

		// Token: 0x020002DC RID: 732
		// (Invoke) Token: 0x06001073 RID: 4211
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ReleaseDelegate(UIntPtr multiMeshPointer);

		// Token: 0x020002DD RID: 733
		// (Invoke) Token: 0x06001077 RID: 4215
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ReleaseEditDataUserDelegate(UIntPtr meshPointer);

		// Token: 0x020002DE RID: 734
		// (Invoke) Token: 0x0600107B RID: 4219
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int RemoveMeshesWithoutTagDelegate(UIntPtr multiMeshPointer, byte[] tag);

		// Token: 0x020002DF RID: 735
		// (Invoke) Token: 0x0600107F RID: 4223
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int RemoveMeshesWithTagDelegate(UIntPtr multiMeshPointer, byte[] tag);

		// Token: 0x020002E0 RID: 736
		// (Invoke) Token: 0x06001083 RID: 4227
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetBillboardingDelegate(UIntPtr multiMeshPointer, BillboardType billboard);

		// Token: 0x020002E1 RID: 737
		// (Invoke) Token: 0x06001087 RID: 4231
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetContourColorDelegate(UIntPtr meshPointer, uint color);

		// Token: 0x020002E2 RID: 738
		// (Invoke) Token: 0x0600108B RID: 4235
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetContourStateDelegate(UIntPtr meshPointer, [MarshalAs(UnmanagedType.U1)] bool alwaysVisible);

		// Token: 0x020002E3 RID: 739
		// (Invoke) Token: 0x0600108F RID: 4239
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetCullModeDelegate(UIntPtr metaMeshPtr, MBMeshCullingMode cullMode);

		// Token: 0x020002E4 RID: 740
		// (Invoke) Token: 0x06001093 RID: 4243
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetEditDataPolicyDelegate(UIntPtr meshPointer, EditDataPolicy policy);

		// Token: 0x020002E5 RID: 741
		// (Invoke) Token: 0x06001097 RID: 4247
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetFactor1Delegate(UIntPtr multiMeshPointer, uint factorColor1);

		// Token: 0x020002E6 RID: 742
		// (Invoke) Token: 0x0600109B RID: 4251
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetFactor1LinearDelegate(UIntPtr multiMeshPointer, uint linearFactorColor1);

		// Token: 0x020002E7 RID: 743
		// (Invoke) Token: 0x0600109F RID: 4255
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetFactor2Delegate(UIntPtr multiMeshPointer, uint factorColor2);

		// Token: 0x020002E8 RID: 744
		// (Invoke) Token: 0x060010A3 RID: 4259
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetFactor2LinearDelegate(UIntPtr multiMeshPointer, uint linearFactorColor2);

		// Token: 0x020002E9 RID: 745
		// (Invoke) Token: 0x060010A7 RID: 4263
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetFactorColorToSubMeshesWithTagDelegate(UIntPtr meshPointer, uint color, byte[] tag);

		// Token: 0x020002EA RID: 746
		// (Invoke) Token: 0x060010AB RID: 4267
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetFrameDelegate(UIntPtr multiMeshPointer, ref MatrixFrame meshFrame);

		// Token: 0x020002EB RID: 747
		// (Invoke) Token: 0x060010AF RID: 4271
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetGlossMultiplierDelegate(UIntPtr multiMeshPointer, float value);

		// Token: 0x020002EC RID: 748
		// (Invoke) Token: 0x060010B3 RID: 4275
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetLodBiasDelegate(UIntPtr multiMeshPointer, int lod_bias);

		// Token: 0x020002ED RID: 749
		// (Invoke) Token: 0x060010B7 RID: 4279
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetMaterialDelegate(UIntPtr multiMeshPointer, UIntPtr materialPointer);

		// Token: 0x020002EE RID: 750
		// (Invoke) Token: 0x060010BB RID: 4283
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetMaterialToSubMeshesWithTagDelegate(UIntPtr meshPointer, UIntPtr materialPointer, byte[] tag);

		// Token: 0x020002EF RID: 751
		// (Invoke) Token: 0x060010BF RID: 4287
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetNumLodsDelegate(UIntPtr multiMeshPointer, int num_lod);

		// Token: 0x020002F0 RID: 752
		// (Invoke) Token: 0x060010C3 RID: 4291
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetVectorArgumentDelegate(UIntPtr multiMeshPointer, float vectorArgument0, float vectorArgument1, float vectorArgument2, float vectorArgument3);

		// Token: 0x020002F1 RID: 753
		// (Invoke) Token: 0x060010C7 RID: 4295
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetVectorArgument2Delegate(UIntPtr multiMeshPointer, float vectorArgument0, float vectorArgument1, float vectorArgument2, float vectorArgument3);

		// Token: 0x020002F2 RID: 754
		// (Invoke) Token: 0x060010CB RID: 4299
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetVectorUserDataDelegate(UIntPtr multiMeshPointer, ref Vec3 vectorArg);

		// Token: 0x020002F3 RID: 755
		// (Invoke) Token: 0x060010CF RID: 4303
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetVisibilityMaskDelegate(UIntPtr multiMeshPointer, VisibilityMaskFlags visibilityMask);

		// Token: 0x020002F4 RID: 756
		// (Invoke) Token: 0x060010D3 RID: 4307
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void UseHeadBoneFaceGenScalingDelegate(UIntPtr multiMeshPointer, UIntPtr skeleton, sbyte headLookDirectionBoneIndex, ref MatrixFrame frame);
	}
}
