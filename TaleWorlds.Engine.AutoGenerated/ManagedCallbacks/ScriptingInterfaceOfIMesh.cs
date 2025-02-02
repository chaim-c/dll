using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace ManagedCallbacks
{
	// Token: 0x02000019 RID: 25
	internal class ScriptingInterfaceOfIMesh : IMesh
	{
		// Token: 0x06000273 RID: 627 RVA: 0x00010E82 File Offset: 0x0000F082
		public void AddEditDataUser(UIntPtr meshPointer)
		{
			ScriptingInterfaceOfIMesh.call_AddEditDataUserDelegate(meshPointer);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00010E8F File Offset: 0x0000F08F
		public int AddFace(UIntPtr meshPointer, int faceCorner0, int faceCorner1, int faceCorner2, UIntPtr lockHandle)
		{
			return ScriptingInterfaceOfIMesh.call_AddFaceDelegate(meshPointer, faceCorner0, faceCorner1, faceCorner2, lockHandle);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00010EA2 File Offset: 0x0000F0A2
		public int AddFaceCorner(UIntPtr meshPointer, Vec3 vertexPosition, Vec3 vertexNormal, Vec2 vertexUVCoordinates, uint vertexColor, UIntPtr lockHandle)
		{
			return ScriptingInterfaceOfIMesh.call_AddFaceCornerDelegate(meshPointer, vertexPosition, vertexNormal, vertexUVCoordinates, vertexColor, lockHandle);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00010EB7 File Offset: 0x0000F0B7
		public void AddMeshToMesh(UIntPtr meshPointer, UIntPtr newMeshPointer, ref MatrixFrame meshFrame)
		{
			ScriptingInterfaceOfIMesh.call_AddMeshToMeshDelegate(meshPointer, newMeshPointer, ref meshFrame);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00010EC8 File Offset: 0x0000F0C8
		public void AddTriangle(UIntPtr meshPointer, Vec3 p1, Vec3 p2, Vec3 p3, Vec2 uv1, Vec2 uv2, Vec2 uv3, uint color, UIntPtr lockHandle)
		{
			ScriptingInterfaceOfIMesh.call_AddTriangleDelegate(meshPointer, p1, p2, p3, uv1, uv2, uv3, color, lockHandle);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00010EF0 File Offset: 0x0000F0F0
		public void AddTriangleWithVertexColors(UIntPtr meshPointer, Vec3 p1, Vec3 p2, Vec3 p3, Vec2 uv1, Vec2 uv2, Vec2 uv3, uint c1, uint c2, uint c3, UIntPtr lockHandle)
		{
			ScriptingInterfaceOfIMesh.call_AddTriangleWithVertexColorsDelegate(meshPointer, p1, p2, p3, uv1, uv2, uv3, c1, c2, c3, lockHandle);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00010F1A File Offset: 0x0000F11A
		public void ClearMesh(UIntPtr meshPointer)
		{
			ScriptingInterfaceOfIMesh.call_ClearMeshDelegate(meshPointer);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00010F27 File Offset: 0x0000F127
		public void ComputeNormals(UIntPtr meshPointer)
		{
			ScriptingInterfaceOfIMesh.call_ComputeNormalsDelegate(meshPointer);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00010F34 File Offset: 0x0000F134
		public void ComputeTangents(UIntPtr meshPointer)
		{
			ScriptingInterfaceOfIMesh.call_ComputeTangentsDelegate(meshPointer);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00010F44 File Offset: 0x0000F144
		public Mesh CreateMesh(bool editable)
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIMesh.call_CreateMeshDelegate(editable);
			Mesh result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new Mesh(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00010F90 File Offset: 0x0000F190
		public Mesh CreateMeshCopy(UIntPtr meshPointer)
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIMesh.call_CreateMeshCopyDelegate(meshPointer);
			Mesh result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new Mesh(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00010FDC File Offset: 0x0000F1DC
		public Mesh CreateMeshWithMaterial(UIntPtr ptr)
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIMesh.call_CreateMeshWithMaterialDelegate(ptr);
			Mesh result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new Mesh(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00011026 File Offset: 0x0000F226
		public void DisableContour(UIntPtr meshPointer)
		{
			ScriptingInterfaceOfIMesh.call_DisableContourDelegate(meshPointer);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00011034 File Offset: 0x0000F234
		public Mesh GetBaseMesh(UIntPtr ptr)
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIMesh.call_GetBaseMeshDelegate(ptr);
			Mesh result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new Mesh(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0001107E File Offset: 0x0000F27E
		public BillboardType GetBillboard(UIntPtr meshPointer)
		{
			return ScriptingInterfaceOfIMesh.call_GetBillboardDelegate(meshPointer);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0001108B File Offset: 0x0000F28B
		public float GetBoundingBoxHeight(UIntPtr meshPointer)
		{
			return ScriptingInterfaceOfIMesh.call_GetBoundingBoxHeightDelegate(meshPointer);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00011098 File Offset: 0x0000F298
		public Vec3 GetBoundingBoxMax(UIntPtr meshPointer)
		{
			return ScriptingInterfaceOfIMesh.call_GetBoundingBoxMaxDelegate(meshPointer);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x000110A5 File Offset: 0x0000F2A5
		public Vec3 GetBoundingBoxMin(UIntPtr meshPointer)
		{
			return ScriptingInterfaceOfIMesh.call_GetBoundingBoxMinDelegate(meshPointer);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x000110B2 File Offset: 0x0000F2B2
		public float GetBoundingBoxWidth(UIntPtr meshPointer)
		{
			return ScriptingInterfaceOfIMesh.call_GetBoundingBoxWidthDelegate(meshPointer);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x000110BF File Offset: 0x0000F2BF
		public uint GetColor(UIntPtr meshPointer)
		{
			return ScriptingInterfaceOfIMesh.call_GetColorDelegate(meshPointer);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x000110CC File Offset: 0x0000F2CC
		public uint GetColor2(UIntPtr meshPointer)
		{
			return ScriptingInterfaceOfIMesh.call_GetColor2Delegate(meshPointer);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x000110D9 File Offset: 0x0000F2D9
		public int GetEditDataFaceCornerCount(UIntPtr meshPointer)
		{
			return ScriptingInterfaceOfIMesh.call_GetEditDataFaceCornerCountDelegate(meshPointer);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x000110E6 File Offset: 0x0000F2E6
		public uint GetEditDataFaceCornerVertexColor(UIntPtr meshPointer, int index)
		{
			return ScriptingInterfaceOfIMesh.call_GetEditDataFaceCornerVertexColorDelegate(meshPointer, index);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x000110F4 File Offset: 0x0000F2F4
		public uint GetFaceCornerCount(UIntPtr meshPointer)
		{
			return ScriptingInterfaceOfIMesh.call_GetFaceCornerCountDelegate(meshPointer);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00011101 File Offset: 0x0000F301
		public uint GetFaceCount(UIntPtr meshPointer)
		{
			return ScriptingInterfaceOfIMesh.call_GetFaceCountDelegate(meshPointer);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0001110E File Offset: 0x0000F30E
		public void GetLocalFrame(UIntPtr meshPointer, ref MatrixFrame outFrame)
		{
			ScriptingInterfaceOfIMesh.call_GetLocalFrameDelegate(meshPointer, ref outFrame);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0001111C File Offset: 0x0000F31C
		public Material GetMaterial(UIntPtr meshPointer)
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIMesh.call_GetMaterialDelegate(meshPointer);
			Material result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new Material(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00011168 File Offset: 0x0000F368
		public Mesh GetMeshFromResource(string materialName)
		{
			byte[] array = null;
			if (materialName != null)
			{
				int byteCount = ScriptingInterfaceOfIMesh._utf8.GetByteCount(materialName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMesh._utf8.GetBytes(materialName, 0, materialName.Length, array, 0);
				array[byteCount] = 0;
			}
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIMesh.call_GetMeshFromResourceDelegate(array);
			Mesh result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new Mesh(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x000111F4 File Offset: 0x0000F3F4
		public string GetName(UIntPtr meshPointer)
		{
			if (ScriptingInterfaceOfIMesh.call_GetNameDelegate(meshPointer) != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0001120C File Offset: 0x0000F40C
		public Mesh GetRandomMeshWithVdecl(int vdecl)
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIMesh.call_GetRandomMeshWithVdeclDelegate(vdecl);
			Mesh result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new Mesh(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00011258 File Offset: 0x0000F458
		public Material GetSecondMaterial(UIntPtr meshPointer)
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIMesh.call_GetSecondMaterialDelegate(meshPointer);
			Material result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new Material(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x06000292 RID: 658 RVA: 0x000112A2 File Offset: 0x0000F4A2
		public VisibilityMaskFlags GetVisibilityMask(UIntPtr meshPointer)
		{
			return ScriptingInterfaceOfIMesh.call_GetVisibilityMaskDelegate(meshPointer);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x000112B0 File Offset: 0x0000F4B0
		public bool HasTag(UIntPtr meshPointer, string tag)
		{
			byte[] array = null;
			if (tag != null)
			{
				int byteCount = ScriptingInterfaceOfIMesh._utf8.GetByteCount(tag);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMesh._utf8.GetBytes(tag, 0, tag.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfIMesh.call_HasTagDelegate(meshPointer, array);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0001130B File Offset: 0x0000F50B
		public void HintIndicesDynamic(UIntPtr meshPointer)
		{
			ScriptingInterfaceOfIMesh.call_HintIndicesDynamicDelegate(meshPointer);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00011318 File Offset: 0x0000F518
		public void HintVerticesDynamic(UIntPtr meshPointer)
		{
			ScriptingInterfaceOfIMesh.call_HintVerticesDynamicDelegate(meshPointer);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00011325 File Offset: 0x0000F525
		public UIntPtr LockEditDataWrite(UIntPtr meshPointer)
		{
			return ScriptingInterfaceOfIMesh.call_LockEditDataWriteDelegate(meshPointer);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00011332 File Offset: 0x0000F532
		public void PreloadForRendering(UIntPtr meshPointer)
		{
			ScriptingInterfaceOfIMesh.call_PreloadForRenderingDelegate(meshPointer);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0001133F File Offset: 0x0000F53F
		public void RecomputeBoundingBox(UIntPtr meshPointer)
		{
			ScriptingInterfaceOfIMesh.call_RecomputeBoundingBoxDelegate(meshPointer);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0001134C File Offset: 0x0000F54C
		public void ReleaseEditDataUser(UIntPtr meshPointer)
		{
			ScriptingInterfaceOfIMesh.call_ReleaseEditDataUserDelegate(meshPointer);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00011359 File Offset: 0x0000F559
		public void ReleaseResources(UIntPtr meshPointer)
		{
			ScriptingInterfaceOfIMesh.call_ReleaseResourcesDelegate(meshPointer);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00011366 File Offset: 0x0000F566
		public void SetAsNotEffectedBySeason(UIntPtr meshPointer)
		{
			ScriptingInterfaceOfIMesh.call_SetAsNotEffectedBySeasonDelegate(meshPointer);
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00011373 File Offset: 0x0000F573
		public void SetBillboard(UIntPtr meshPointer, BillboardType value)
		{
			ScriptingInterfaceOfIMesh.call_SetBillboardDelegate(meshPointer, value);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00011381 File Offset: 0x0000F581
		public void SetColor(UIntPtr meshPointer, uint newColor)
		{
			ScriptingInterfaceOfIMesh.call_SetColorDelegate(meshPointer, newColor);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0001138F File Offset: 0x0000F58F
		public void SetColor2(UIntPtr meshPointer, uint newColor2)
		{
			ScriptingInterfaceOfIMesh.call_SetColor2Delegate(meshPointer, newColor2);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0001139D File Offset: 0x0000F59D
		public void SetColorAlpha(UIntPtr meshPointer, uint newColorAlpha)
		{
			ScriptingInterfaceOfIMesh.call_SetColorAlphaDelegate(meshPointer, newColorAlpha);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x000113AB File Offset: 0x0000F5AB
		public void SetColorAndStroke(UIntPtr meshPointer, bool drawStroke)
		{
			ScriptingInterfaceOfIMesh.call_SetColorAndStrokeDelegate(meshPointer, drawStroke);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x000113B9 File Offset: 0x0000F5B9
		public void SetContourColor(UIntPtr meshPointer, Vec3 color, bool alwaysVisible, bool maskMesh)
		{
			ScriptingInterfaceOfIMesh.call_SetContourColorDelegate(meshPointer, color, alwaysVisible, maskMesh);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x000113CA File Offset: 0x0000F5CA
		public void SetCullingMode(UIntPtr meshPointer, uint newCullingMode)
		{
			ScriptingInterfaceOfIMesh.call_SetCullingModeDelegate(meshPointer, newCullingMode);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x000113D8 File Offset: 0x0000F5D8
		public void SetEditDataFaceCornerVertexColor(UIntPtr meshPointer, int index, uint color)
		{
			ScriptingInterfaceOfIMesh.call_SetEditDataFaceCornerVertexColorDelegate(meshPointer, index, color);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x000113E7 File Offset: 0x0000F5E7
		public void SetEditDataPolicy(UIntPtr meshPointer, EditDataPolicy policy)
		{
			ScriptingInterfaceOfIMesh.call_SetEditDataPolicyDelegate(meshPointer, policy);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x000113F5 File Offset: 0x0000F5F5
		public void SetExternalBoundingBox(UIntPtr meshPointer, ref BoundingBox bbox)
		{
			ScriptingInterfaceOfIMesh.call_SetExternalBoundingBoxDelegate(meshPointer, ref bbox);
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00011403 File Offset: 0x0000F603
		public void SetLocalFrame(UIntPtr meshPointer, ref MatrixFrame meshFrame)
		{
			ScriptingInterfaceOfIMesh.call_SetLocalFrameDelegate(meshPointer, ref meshFrame);
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00011411 File Offset: 0x0000F611
		public void SetMaterial(UIntPtr meshPointer, UIntPtr materialpointer)
		{
			ScriptingInterfaceOfIMesh.call_SetMaterialDelegate(meshPointer, materialpointer);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00011420 File Offset: 0x0000F620
		public void SetMaterialByName(UIntPtr meshPointer, string materialName)
		{
			byte[] array = null;
			if (materialName != null)
			{
				int byteCount = ScriptingInterfaceOfIMesh._utf8.GetByteCount(materialName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMesh._utf8.GetBytes(materialName, 0, materialName.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIMesh.call_SetMaterialByNameDelegate(meshPointer, array);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0001147B File Offset: 0x0000F67B
		public void SetMeshRenderOrder(UIntPtr meshPointer, int renderorder)
		{
			ScriptingInterfaceOfIMesh.call_SetMeshRenderOrderDelegate(meshPointer, renderorder);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00011489 File Offset: 0x0000F689
		public void SetMorphTime(UIntPtr meshPointer, float newTime)
		{
			ScriptingInterfaceOfIMesh.call_SetMorphTimeDelegate(meshPointer, newTime);
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00011498 File Offset: 0x0000F698
		public void SetName(UIntPtr meshPointer, string name)
		{
			byte[] array = null;
			if (name != null)
			{
				int byteCount = ScriptingInterfaceOfIMesh._utf8.GetByteCount(name);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMesh._utf8.GetBytes(name, 0, name.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIMesh.call_SetNameDelegate(meshPointer, array);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x000114F3 File Offset: 0x0000F6F3
		public void SetVectorArgument(UIntPtr meshPointer, float vectorArgument0, float vectorArgument1, float vectorArgument2, float vectorArgument3)
		{
			ScriptingInterfaceOfIMesh.call_SetVectorArgumentDelegate(meshPointer, vectorArgument0, vectorArgument1, vectorArgument2, vectorArgument3);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00011506 File Offset: 0x0000F706
		public void SetVectorArgument2(UIntPtr meshPointer, float vectorArgument0, float vectorArgument1, float vectorArgument2, float vectorArgument3)
		{
			ScriptingInterfaceOfIMesh.call_SetVectorArgument2Delegate(meshPointer, vectorArgument0, vectorArgument1, vectorArgument2, vectorArgument3);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00011519 File Offset: 0x0000F719
		public void SetVisibilityMask(UIntPtr meshPointer, VisibilityMaskFlags value)
		{
			ScriptingInterfaceOfIMesh.call_SetVisibilityMaskDelegate(meshPointer, value);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00011527 File Offset: 0x0000F727
		public void UnlockEditDataWrite(UIntPtr meshPointer, UIntPtr handle)
		{
			ScriptingInterfaceOfIMesh.call_UnlockEditDataWriteDelegate(meshPointer, handle);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00011535 File Offset: 0x0000F735
		public void UpdateBoundingBox(UIntPtr meshPointer)
		{
			ScriptingInterfaceOfIMesh.call_UpdateBoundingBoxDelegate(meshPointer);
		}

		// Token: 0x0400020F RID: 527
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x04000210 RID: 528
		public static ScriptingInterfaceOfIMesh.AddEditDataUserDelegate call_AddEditDataUserDelegate;

		// Token: 0x04000211 RID: 529
		public static ScriptingInterfaceOfIMesh.AddFaceDelegate call_AddFaceDelegate;

		// Token: 0x04000212 RID: 530
		public static ScriptingInterfaceOfIMesh.AddFaceCornerDelegate call_AddFaceCornerDelegate;

		// Token: 0x04000213 RID: 531
		public static ScriptingInterfaceOfIMesh.AddMeshToMeshDelegate call_AddMeshToMeshDelegate;

		// Token: 0x04000214 RID: 532
		public static ScriptingInterfaceOfIMesh.AddTriangleDelegate call_AddTriangleDelegate;

		// Token: 0x04000215 RID: 533
		public static ScriptingInterfaceOfIMesh.AddTriangleWithVertexColorsDelegate call_AddTriangleWithVertexColorsDelegate;

		// Token: 0x04000216 RID: 534
		public static ScriptingInterfaceOfIMesh.ClearMeshDelegate call_ClearMeshDelegate;

		// Token: 0x04000217 RID: 535
		public static ScriptingInterfaceOfIMesh.ComputeNormalsDelegate call_ComputeNormalsDelegate;

		// Token: 0x04000218 RID: 536
		public static ScriptingInterfaceOfIMesh.ComputeTangentsDelegate call_ComputeTangentsDelegate;

		// Token: 0x04000219 RID: 537
		public static ScriptingInterfaceOfIMesh.CreateMeshDelegate call_CreateMeshDelegate;

		// Token: 0x0400021A RID: 538
		public static ScriptingInterfaceOfIMesh.CreateMeshCopyDelegate call_CreateMeshCopyDelegate;

		// Token: 0x0400021B RID: 539
		public static ScriptingInterfaceOfIMesh.CreateMeshWithMaterialDelegate call_CreateMeshWithMaterialDelegate;

		// Token: 0x0400021C RID: 540
		public static ScriptingInterfaceOfIMesh.DisableContourDelegate call_DisableContourDelegate;

		// Token: 0x0400021D RID: 541
		public static ScriptingInterfaceOfIMesh.GetBaseMeshDelegate call_GetBaseMeshDelegate;

		// Token: 0x0400021E RID: 542
		public static ScriptingInterfaceOfIMesh.GetBillboardDelegate call_GetBillboardDelegate;

		// Token: 0x0400021F RID: 543
		public static ScriptingInterfaceOfIMesh.GetBoundingBoxHeightDelegate call_GetBoundingBoxHeightDelegate;

		// Token: 0x04000220 RID: 544
		public static ScriptingInterfaceOfIMesh.GetBoundingBoxMaxDelegate call_GetBoundingBoxMaxDelegate;

		// Token: 0x04000221 RID: 545
		public static ScriptingInterfaceOfIMesh.GetBoundingBoxMinDelegate call_GetBoundingBoxMinDelegate;

		// Token: 0x04000222 RID: 546
		public static ScriptingInterfaceOfIMesh.GetBoundingBoxWidthDelegate call_GetBoundingBoxWidthDelegate;

		// Token: 0x04000223 RID: 547
		public static ScriptingInterfaceOfIMesh.GetColorDelegate call_GetColorDelegate;

		// Token: 0x04000224 RID: 548
		public static ScriptingInterfaceOfIMesh.GetColor2Delegate call_GetColor2Delegate;

		// Token: 0x04000225 RID: 549
		public static ScriptingInterfaceOfIMesh.GetEditDataFaceCornerCountDelegate call_GetEditDataFaceCornerCountDelegate;

		// Token: 0x04000226 RID: 550
		public static ScriptingInterfaceOfIMesh.GetEditDataFaceCornerVertexColorDelegate call_GetEditDataFaceCornerVertexColorDelegate;

		// Token: 0x04000227 RID: 551
		public static ScriptingInterfaceOfIMesh.GetFaceCornerCountDelegate call_GetFaceCornerCountDelegate;

		// Token: 0x04000228 RID: 552
		public static ScriptingInterfaceOfIMesh.GetFaceCountDelegate call_GetFaceCountDelegate;

		// Token: 0x04000229 RID: 553
		public static ScriptingInterfaceOfIMesh.GetLocalFrameDelegate call_GetLocalFrameDelegate;

		// Token: 0x0400022A RID: 554
		public static ScriptingInterfaceOfIMesh.GetMaterialDelegate call_GetMaterialDelegate;

		// Token: 0x0400022B RID: 555
		public static ScriptingInterfaceOfIMesh.GetMeshFromResourceDelegate call_GetMeshFromResourceDelegate;

		// Token: 0x0400022C RID: 556
		public static ScriptingInterfaceOfIMesh.GetNameDelegate call_GetNameDelegate;

		// Token: 0x0400022D RID: 557
		public static ScriptingInterfaceOfIMesh.GetRandomMeshWithVdeclDelegate call_GetRandomMeshWithVdeclDelegate;

		// Token: 0x0400022E RID: 558
		public static ScriptingInterfaceOfIMesh.GetSecondMaterialDelegate call_GetSecondMaterialDelegate;

		// Token: 0x0400022F RID: 559
		public static ScriptingInterfaceOfIMesh.GetVisibilityMaskDelegate call_GetVisibilityMaskDelegate;

		// Token: 0x04000230 RID: 560
		public static ScriptingInterfaceOfIMesh.HasTagDelegate call_HasTagDelegate;

		// Token: 0x04000231 RID: 561
		public static ScriptingInterfaceOfIMesh.HintIndicesDynamicDelegate call_HintIndicesDynamicDelegate;

		// Token: 0x04000232 RID: 562
		public static ScriptingInterfaceOfIMesh.HintVerticesDynamicDelegate call_HintVerticesDynamicDelegate;

		// Token: 0x04000233 RID: 563
		public static ScriptingInterfaceOfIMesh.LockEditDataWriteDelegate call_LockEditDataWriteDelegate;

		// Token: 0x04000234 RID: 564
		public static ScriptingInterfaceOfIMesh.PreloadForRenderingDelegate call_PreloadForRenderingDelegate;

		// Token: 0x04000235 RID: 565
		public static ScriptingInterfaceOfIMesh.RecomputeBoundingBoxDelegate call_RecomputeBoundingBoxDelegate;

		// Token: 0x04000236 RID: 566
		public static ScriptingInterfaceOfIMesh.ReleaseEditDataUserDelegate call_ReleaseEditDataUserDelegate;

		// Token: 0x04000237 RID: 567
		public static ScriptingInterfaceOfIMesh.ReleaseResourcesDelegate call_ReleaseResourcesDelegate;

		// Token: 0x04000238 RID: 568
		public static ScriptingInterfaceOfIMesh.SetAsNotEffectedBySeasonDelegate call_SetAsNotEffectedBySeasonDelegate;

		// Token: 0x04000239 RID: 569
		public static ScriptingInterfaceOfIMesh.SetBillboardDelegate call_SetBillboardDelegate;

		// Token: 0x0400023A RID: 570
		public static ScriptingInterfaceOfIMesh.SetColorDelegate call_SetColorDelegate;

		// Token: 0x0400023B RID: 571
		public static ScriptingInterfaceOfIMesh.SetColor2Delegate call_SetColor2Delegate;

		// Token: 0x0400023C RID: 572
		public static ScriptingInterfaceOfIMesh.SetColorAlphaDelegate call_SetColorAlphaDelegate;

		// Token: 0x0400023D RID: 573
		public static ScriptingInterfaceOfIMesh.SetColorAndStrokeDelegate call_SetColorAndStrokeDelegate;

		// Token: 0x0400023E RID: 574
		public static ScriptingInterfaceOfIMesh.SetContourColorDelegate call_SetContourColorDelegate;

		// Token: 0x0400023F RID: 575
		public static ScriptingInterfaceOfIMesh.SetCullingModeDelegate call_SetCullingModeDelegate;

		// Token: 0x04000240 RID: 576
		public static ScriptingInterfaceOfIMesh.SetEditDataFaceCornerVertexColorDelegate call_SetEditDataFaceCornerVertexColorDelegate;

		// Token: 0x04000241 RID: 577
		public static ScriptingInterfaceOfIMesh.SetEditDataPolicyDelegate call_SetEditDataPolicyDelegate;

		// Token: 0x04000242 RID: 578
		public static ScriptingInterfaceOfIMesh.SetExternalBoundingBoxDelegate call_SetExternalBoundingBoxDelegate;

		// Token: 0x04000243 RID: 579
		public static ScriptingInterfaceOfIMesh.SetLocalFrameDelegate call_SetLocalFrameDelegate;

		// Token: 0x04000244 RID: 580
		public static ScriptingInterfaceOfIMesh.SetMaterialDelegate call_SetMaterialDelegate;

		// Token: 0x04000245 RID: 581
		public static ScriptingInterfaceOfIMesh.SetMaterialByNameDelegate call_SetMaterialByNameDelegate;

		// Token: 0x04000246 RID: 582
		public static ScriptingInterfaceOfIMesh.SetMeshRenderOrderDelegate call_SetMeshRenderOrderDelegate;

		// Token: 0x04000247 RID: 583
		public static ScriptingInterfaceOfIMesh.SetMorphTimeDelegate call_SetMorphTimeDelegate;

		// Token: 0x04000248 RID: 584
		public static ScriptingInterfaceOfIMesh.SetNameDelegate call_SetNameDelegate;

		// Token: 0x04000249 RID: 585
		public static ScriptingInterfaceOfIMesh.SetVectorArgumentDelegate call_SetVectorArgumentDelegate;

		// Token: 0x0400024A RID: 586
		public static ScriptingInterfaceOfIMesh.SetVectorArgument2Delegate call_SetVectorArgument2Delegate;

		// Token: 0x0400024B RID: 587
		public static ScriptingInterfaceOfIMesh.SetVisibilityMaskDelegate call_SetVisibilityMaskDelegate;

		// Token: 0x0400024C RID: 588
		public static ScriptingInterfaceOfIMesh.UnlockEditDataWriteDelegate call_UnlockEditDataWriteDelegate;

		// Token: 0x0400024D RID: 589
		public static ScriptingInterfaceOfIMesh.UpdateBoundingBoxDelegate call_UpdateBoundingBoxDelegate;

		// Token: 0x02000270 RID: 624
		// (Invoke) Token: 0x06000EC3 RID: 3779
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddEditDataUserDelegate(UIntPtr meshPointer);

		// Token: 0x02000271 RID: 625
		// (Invoke) Token: 0x06000EC7 RID: 3783
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int AddFaceDelegate(UIntPtr meshPointer, int faceCorner0, int faceCorner1, int faceCorner2, UIntPtr lockHandle);

		// Token: 0x02000272 RID: 626
		// (Invoke) Token: 0x06000ECB RID: 3787
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int AddFaceCornerDelegate(UIntPtr meshPointer, Vec3 vertexPosition, Vec3 vertexNormal, Vec2 vertexUVCoordinates, uint vertexColor, UIntPtr lockHandle);

		// Token: 0x02000273 RID: 627
		// (Invoke) Token: 0x06000ECF RID: 3791
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddMeshToMeshDelegate(UIntPtr meshPointer, UIntPtr newMeshPointer, ref MatrixFrame meshFrame);

		// Token: 0x02000274 RID: 628
		// (Invoke) Token: 0x06000ED3 RID: 3795
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddTriangleDelegate(UIntPtr meshPointer, Vec3 p1, Vec3 p2, Vec3 p3, Vec2 uv1, Vec2 uv2, Vec2 uv3, uint color, UIntPtr lockHandle);

		// Token: 0x02000275 RID: 629
		// (Invoke) Token: 0x06000ED7 RID: 3799
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddTriangleWithVertexColorsDelegate(UIntPtr meshPointer, Vec3 p1, Vec3 p2, Vec3 p3, Vec2 uv1, Vec2 uv2, Vec2 uv3, uint c1, uint c2, uint c3, UIntPtr lockHandle);

		// Token: 0x02000276 RID: 630
		// (Invoke) Token: 0x06000EDB RID: 3803
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ClearMeshDelegate(UIntPtr meshPointer);

		// Token: 0x02000277 RID: 631
		// (Invoke) Token: 0x06000EDF RID: 3807
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ComputeNormalsDelegate(UIntPtr meshPointer);

		// Token: 0x02000278 RID: 632
		// (Invoke) Token: 0x06000EE3 RID: 3811
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ComputeTangentsDelegate(UIntPtr meshPointer);

		// Token: 0x02000279 RID: 633
		// (Invoke) Token: 0x06000EE7 RID: 3815
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer CreateMeshDelegate([MarshalAs(UnmanagedType.U1)] bool editable);

		// Token: 0x0200027A RID: 634
		// (Invoke) Token: 0x06000EEB RID: 3819
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer CreateMeshCopyDelegate(UIntPtr meshPointer);

		// Token: 0x0200027B RID: 635
		// (Invoke) Token: 0x06000EEF RID: 3823
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer CreateMeshWithMaterialDelegate(UIntPtr ptr);

		// Token: 0x0200027C RID: 636
		// (Invoke) Token: 0x06000EF3 RID: 3827
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void DisableContourDelegate(UIntPtr meshPointer);

		// Token: 0x0200027D RID: 637
		// (Invoke) Token: 0x06000EF7 RID: 3831
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer GetBaseMeshDelegate(UIntPtr ptr);

		// Token: 0x0200027E RID: 638
		// (Invoke) Token: 0x06000EFB RID: 3835
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate BillboardType GetBillboardDelegate(UIntPtr meshPointer);

		// Token: 0x0200027F RID: 639
		// (Invoke) Token: 0x06000EFF RID: 3839
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetBoundingBoxHeightDelegate(UIntPtr meshPointer);

		// Token: 0x02000280 RID: 640
		// (Invoke) Token: 0x06000F03 RID: 3843
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate Vec3 GetBoundingBoxMaxDelegate(UIntPtr meshPointer);

		// Token: 0x02000281 RID: 641
		// (Invoke) Token: 0x06000F07 RID: 3847
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate Vec3 GetBoundingBoxMinDelegate(UIntPtr meshPointer);

		// Token: 0x02000282 RID: 642
		// (Invoke) Token: 0x06000F0B RID: 3851
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetBoundingBoxWidthDelegate(UIntPtr meshPointer);

		// Token: 0x02000283 RID: 643
		// (Invoke) Token: 0x06000F0F RID: 3855
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate uint GetColorDelegate(UIntPtr meshPointer);

		// Token: 0x02000284 RID: 644
		// (Invoke) Token: 0x06000F13 RID: 3859
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate uint GetColor2Delegate(UIntPtr meshPointer);

		// Token: 0x02000285 RID: 645
		// (Invoke) Token: 0x06000F17 RID: 3863
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetEditDataFaceCornerCountDelegate(UIntPtr meshPointer);

		// Token: 0x02000286 RID: 646
		// (Invoke) Token: 0x06000F1B RID: 3867
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate uint GetEditDataFaceCornerVertexColorDelegate(UIntPtr meshPointer, int index);

		// Token: 0x02000287 RID: 647
		// (Invoke) Token: 0x06000F1F RID: 3871
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate uint GetFaceCornerCountDelegate(UIntPtr meshPointer);

		// Token: 0x02000288 RID: 648
		// (Invoke) Token: 0x06000F23 RID: 3875
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate uint GetFaceCountDelegate(UIntPtr meshPointer);

		// Token: 0x02000289 RID: 649
		// (Invoke) Token: 0x06000F27 RID: 3879
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetLocalFrameDelegate(UIntPtr meshPointer, ref MatrixFrame outFrame);

		// Token: 0x0200028A RID: 650
		// (Invoke) Token: 0x06000F2B RID: 3883
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer GetMaterialDelegate(UIntPtr meshPointer);

		// Token: 0x0200028B RID: 651
		// (Invoke) Token: 0x06000F2F RID: 3887
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer GetMeshFromResourceDelegate(byte[] materialName);

		// Token: 0x0200028C RID: 652
		// (Invoke) Token: 0x06000F33 RID: 3891
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetNameDelegate(UIntPtr meshPointer);

		// Token: 0x0200028D RID: 653
		// (Invoke) Token: 0x06000F37 RID: 3895
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer GetRandomMeshWithVdeclDelegate(int vdecl);

		// Token: 0x0200028E RID: 654
		// (Invoke) Token: 0x06000F3B RID: 3899
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer GetSecondMaterialDelegate(UIntPtr meshPointer);

		// Token: 0x0200028F RID: 655
		// (Invoke) Token: 0x06000F3F RID: 3903
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate VisibilityMaskFlags GetVisibilityMaskDelegate(UIntPtr meshPointer);

		// Token: 0x02000290 RID: 656
		// (Invoke) Token: 0x06000F43 RID: 3907
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool HasTagDelegate(UIntPtr meshPointer, byte[] tag);

		// Token: 0x02000291 RID: 657
		// (Invoke) Token: 0x06000F47 RID: 3911
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void HintIndicesDynamicDelegate(UIntPtr meshPointer);

		// Token: 0x02000292 RID: 658
		// (Invoke) Token: 0x06000F4B RID: 3915
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void HintVerticesDynamicDelegate(UIntPtr meshPointer);

		// Token: 0x02000293 RID: 659
		// (Invoke) Token: 0x06000F4F RID: 3919
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate UIntPtr LockEditDataWriteDelegate(UIntPtr meshPointer);

		// Token: 0x02000294 RID: 660
		// (Invoke) Token: 0x06000F53 RID: 3923
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void PreloadForRenderingDelegate(UIntPtr meshPointer);

		// Token: 0x02000295 RID: 661
		// (Invoke) Token: 0x06000F57 RID: 3927
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RecomputeBoundingBoxDelegate(UIntPtr meshPointer);

		// Token: 0x02000296 RID: 662
		// (Invoke) Token: 0x06000F5B RID: 3931
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ReleaseEditDataUserDelegate(UIntPtr meshPointer);

		// Token: 0x02000297 RID: 663
		// (Invoke) Token: 0x06000F5F RID: 3935
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ReleaseResourcesDelegate(UIntPtr meshPointer);

		// Token: 0x02000298 RID: 664
		// (Invoke) Token: 0x06000F63 RID: 3939
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetAsNotEffectedBySeasonDelegate(UIntPtr meshPointer);

		// Token: 0x02000299 RID: 665
		// (Invoke) Token: 0x06000F67 RID: 3943
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetBillboardDelegate(UIntPtr meshPointer, BillboardType value);

		// Token: 0x0200029A RID: 666
		// (Invoke) Token: 0x06000F6B RID: 3947
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetColorDelegate(UIntPtr meshPointer, uint newColor);

		// Token: 0x0200029B RID: 667
		// (Invoke) Token: 0x06000F6F RID: 3951
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetColor2Delegate(UIntPtr meshPointer, uint newColor2);

		// Token: 0x0200029C RID: 668
		// (Invoke) Token: 0x06000F73 RID: 3955
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetColorAlphaDelegate(UIntPtr meshPointer, uint newColorAlpha);

		// Token: 0x0200029D RID: 669
		// (Invoke) Token: 0x06000F77 RID: 3959
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetColorAndStrokeDelegate(UIntPtr meshPointer, [MarshalAs(UnmanagedType.U1)] bool drawStroke);

		// Token: 0x0200029E RID: 670
		// (Invoke) Token: 0x06000F7B RID: 3963
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetContourColorDelegate(UIntPtr meshPointer, Vec3 color, [MarshalAs(UnmanagedType.U1)] bool alwaysVisible, [MarshalAs(UnmanagedType.U1)] bool maskMesh);

		// Token: 0x0200029F RID: 671
		// (Invoke) Token: 0x06000F7F RID: 3967
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetCullingModeDelegate(UIntPtr meshPointer, uint newCullingMode);

		// Token: 0x020002A0 RID: 672
		// (Invoke) Token: 0x06000F83 RID: 3971
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetEditDataFaceCornerVertexColorDelegate(UIntPtr meshPointer, int index, uint color);

		// Token: 0x020002A1 RID: 673
		// (Invoke) Token: 0x06000F87 RID: 3975
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetEditDataPolicyDelegate(UIntPtr meshPointer, EditDataPolicy policy);

		// Token: 0x020002A2 RID: 674
		// (Invoke) Token: 0x06000F8B RID: 3979
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetExternalBoundingBoxDelegate(UIntPtr meshPointer, ref BoundingBox bbox);

		// Token: 0x020002A3 RID: 675
		// (Invoke) Token: 0x06000F8F RID: 3983
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetLocalFrameDelegate(UIntPtr meshPointer, ref MatrixFrame meshFrame);

		// Token: 0x020002A4 RID: 676
		// (Invoke) Token: 0x06000F93 RID: 3987
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetMaterialDelegate(UIntPtr meshPointer, UIntPtr materialpointer);

		// Token: 0x020002A5 RID: 677
		// (Invoke) Token: 0x06000F97 RID: 3991
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetMaterialByNameDelegate(UIntPtr meshPointer, byte[] materialName);

		// Token: 0x020002A6 RID: 678
		// (Invoke) Token: 0x06000F9B RID: 3995
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetMeshRenderOrderDelegate(UIntPtr meshPointer, int renderorder);

		// Token: 0x020002A7 RID: 679
		// (Invoke) Token: 0x06000F9F RID: 3999
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetMorphTimeDelegate(UIntPtr meshPointer, float newTime);

		// Token: 0x020002A8 RID: 680
		// (Invoke) Token: 0x06000FA3 RID: 4003
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetNameDelegate(UIntPtr meshPointer, byte[] name);

		// Token: 0x020002A9 RID: 681
		// (Invoke) Token: 0x06000FA7 RID: 4007
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetVectorArgumentDelegate(UIntPtr meshPointer, float vectorArgument0, float vectorArgument1, float vectorArgument2, float vectorArgument3);

		// Token: 0x020002AA RID: 682
		// (Invoke) Token: 0x06000FAB RID: 4011
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetVectorArgument2Delegate(UIntPtr meshPointer, float vectorArgument0, float vectorArgument1, float vectorArgument2, float vectorArgument3);

		// Token: 0x020002AB RID: 683
		// (Invoke) Token: 0x06000FAF RID: 4015
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetVisibilityMaskDelegate(UIntPtr meshPointer, VisibilityMaskFlags value);

		// Token: 0x020002AC RID: 684
		// (Invoke) Token: 0x06000FB3 RID: 4019
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void UnlockEditDataWriteDelegate(UIntPtr meshPointer, UIntPtr handle);

		// Token: 0x020002AD RID: 685
		// (Invoke) Token: 0x06000FB7 RID: 4023
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void UpdateBoundingBoxDelegate(UIntPtr meshPointer);
	}
}
