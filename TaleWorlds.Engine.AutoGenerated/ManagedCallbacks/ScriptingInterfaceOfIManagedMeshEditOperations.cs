using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace ManagedCallbacks
{
	// Token: 0x02000017 RID: 23
	internal class ScriptingInterfaceOfIManagedMeshEditOperations : IManagedMeshEditOperations
	{
		// Token: 0x0600021D RID: 541 RVA: 0x00010691 File Offset: 0x0000E891
		public int AddFace(UIntPtr Pointer, int patchNode0, int patchNode1, int patchNode2)
		{
			return ScriptingInterfaceOfIManagedMeshEditOperations.call_AddFaceDelegate(Pointer, patchNode0, patchNode1, patchNode2);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x000106A2 File Offset: 0x0000E8A2
		public int AddFaceCorner1(UIntPtr Pointer, int vertexIndex, ref Vec2 uv0, ref Vec3 color, ref Vec3 normal)
		{
			return ScriptingInterfaceOfIManagedMeshEditOperations.call_AddFaceCorner1Delegate(Pointer, vertexIndex, ref uv0, ref color, ref normal);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x000106B5 File Offset: 0x0000E8B5
		public int AddFaceCorner2(UIntPtr Pointer, int vertexIndex, ref Vec2 uv0, ref Vec2 uv1, ref Vec3 color, ref Vec3 normal)
		{
			return ScriptingInterfaceOfIManagedMeshEditOperations.call_AddFaceCorner2Delegate(Pointer, vertexIndex, ref uv0, ref uv1, ref color, ref normal);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x000106CA File Offset: 0x0000E8CA
		public void AddLine(UIntPtr Pointer, ref Vec3 start, ref Vec3 end, ref Vec3 color, float lineWidth)
		{
			ScriptingInterfaceOfIManagedMeshEditOperations.call_AddLineDelegate(Pointer, ref start, ref end, ref color, lineWidth);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x000106DD File Offset: 0x0000E8DD
		public void AddMesh(UIntPtr Pointer, UIntPtr meshPointer, ref MatrixFrame frame)
		{
			ScriptingInterfaceOfIManagedMeshEditOperations.call_AddMeshDelegate(Pointer, meshPointer, ref frame);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x000106EC File Offset: 0x0000E8EC
		public void AddMeshAux(UIntPtr Pointer, UIntPtr meshPointer, ref MatrixFrame frame, sbyte boneIndex, ref Vec3 color, bool transformNormal, bool heightGradient, bool addSkinData, bool useDoublePrecision)
		{
			ScriptingInterfaceOfIManagedMeshEditOperations.call_AddMeshAuxDelegate(Pointer, meshPointer, ref frame, boneIndex, ref color, transformNormal, heightGradient, addSkinData, useDoublePrecision);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00010712 File Offset: 0x0000E912
		public void AddMeshToBone(UIntPtr Pointer, UIntPtr meshPointer, ref MatrixFrame frame, sbyte boneIndex)
		{
			ScriptingInterfaceOfIManagedMeshEditOperations.call_AddMeshToBoneDelegate(Pointer, meshPointer, ref frame, boneIndex);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00010723 File Offset: 0x0000E923
		public void AddMeshWithColor(UIntPtr Pointer, UIntPtr meshPointer, ref MatrixFrame frame, ref Vec3 vertexColor, bool useDoublePrecision)
		{
			ScriptingInterfaceOfIManagedMeshEditOperations.call_AddMeshWithColorDelegate(Pointer, meshPointer, ref frame, ref vertexColor, useDoublePrecision);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00010736 File Offset: 0x0000E936
		public void AddMeshWithFixedNormals(UIntPtr Pointer, UIntPtr meshPointer, ref MatrixFrame frame)
		{
			ScriptingInterfaceOfIManagedMeshEditOperations.call_AddMeshWithFixedNormalsDelegate(Pointer, meshPointer, ref frame);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00010745 File Offset: 0x0000E945
		public void AddMeshWithFixedNormalsWithHeightGradientColor(UIntPtr Pointer, UIntPtr meshPointer, ref MatrixFrame frame)
		{
			ScriptingInterfaceOfIManagedMeshEditOperations.call_AddMeshWithFixedNormalsWithHeightGradientColorDelegate(Pointer, meshPointer, ref frame);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00010754 File Offset: 0x0000E954
		public void AddMeshWithSkinData(UIntPtr Pointer, UIntPtr meshPointer, ref MatrixFrame frame, sbyte boneIndex)
		{
			ScriptingInterfaceOfIManagedMeshEditOperations.call_AddMeshWithSkinDataDelegate(Pointer, meshPointer, ref frame, boneIndex);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00010765 File Offset: 0x0000E965
		public void AddRect(UIntPtr Pointer, ref Vec3 originBegin, ref Vec3 originEnd, ref Vec2 uvBegin, ref Vec2 uvEnd, ref Vec3 color)
		{
			ScriptingInterfaceOfIManagedMeshEditOperations.call_AddRectDelegate(Pointer, ref originBegin, ref originEnd, ref uvBegin, ref uvEnd, ref color);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0001077A File Offset: 0x0000E97A
		public void AddRectangle3(UIntPtr Pointer, ref Vec3 o, ref Vec2 size, ref Vec2 uv_origin, ref Vec2 uvSize, ref Vec3 color)
		{
			ScriptingInterfaceOfIManagedMeshEditOperations.call_AddRectangle3Delegate(Pointer, ref o, ref size, ref uv_origin, ref uvSize, ref color);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0001078F File Offset: 0x0000E98F
		public void AddRectangleWithInverseUV(UIntPtr Pointer, ref Vec3 o, ref Vec2 size, ref Vec2 uv_origin, ref Vec2 uvSize, ref Vec3 color)
		{
			ScriptingInterfaceOfIManagedMeshEditOperations.call_AddRectangleWithInverseUVDelegate(Pointer, ref o, ref size, ref uv_origin, ref uvSize, ref color);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x000107A4 File Offset: 0x0000E9A4
		public void AddRectWithZUp(UIntPtr Pointer, ref Vec3 originBegin, ref Vec3 originEnd, ref Vec2 uvBegin, ref Vec2 uvEnd, ref Vec3 color)
		{
			ScriptingInterfaceOfIManagedMeshEditOperations.call_AddRectWithZUpDelegate(Pointer, ref originBegin, ref originEnd, ref uvBegin, ref uvEnd, ref color);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x000107B9 File Offset: 0x0000E9B9
		public void AddSkinnedMeshWithColor(UIntPtr Pointer, UIntPtr meshPointer, ref MatrixFrame frame, ref Vec3 vertexColor, bool useDoublePrecision)
		{
			ScriptingInterfaceOfIManagedMeshEditOperations.call_AddSkinnedMeshWithColorDelegate(Pointer, meshPointer, ref frame, ref vertexColor, useDoublePrecision);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x000107CC File Offset: 0x0000E9CC
		public void AddTriangle1(UIntPtr Pointer, ref Vec3 p1, ref Vec3 p2, ref Vec3 p3, ref Vec2 uv1, ref Vec2 uv2, ref Vec2 uv3, ref Vec3 color)
		{
			ScriptingInterfaceOfIManagedMeshEditOperations.call_AddTriangle1Delegate(Pointer, ref p1, ref p2, ref p3, ref uv1, ref uv2, ref uv3, ref color);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x000107F0 File Offset: 0x0000E9F0
		public void AddTriangle2(UIntPtr Pointer, ref Vec3 p1, ref Vec3 p2, ref Vec3 p3, ref Vec3 n1, ref Vec3 n2, ref Vec3 n3, ref Vec2 uv1, ref Vec2 uv2, ref Vec2 uv3, ref Vec3 c1, ref Vec3 c2, ref Vec3 c3)
		{
			ScriptingInterfaceOfIManagedMeshEditOperations.call_AddTriangle2Delegate(Pointer, ref p1, ref p2, ref p3, ref n1, ref n2, ref n3, ref uv1, ref uv2, ref uv3, ref c1, ref c2, ref c3);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0001081E File Offset: 0x0000EA1E
		public int AddVertex(UIntPtr Pointer, ref Vec3 vertexPos)
		{
			return ScriptingInterfaceOfIManagedMeshEditOperations.call_AddVertexDelegate(Pointer, ref vertexPos);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0001082C File Offset: 0x0000EA2C
		public void ApplyCPUSkinning(UIntPtr Pointer, UIntPtr skeletonPointer)
		{
			ScriptingInterfaceOfIManagedMeshEditOperations.call_ApplyCPUSkinningDelegate(Pointer, skeletonPointer);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0001083A File Offset: 0x0000EA3A
		public void ClearAll(UIntPtr Pointer)
		{
			ScriptingInterfaceOfIManagedMeshEditOperations.call_ClearAllDelegate(Pointer);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00010847 File Offset: 0x0000EA47
		public void ComputeCornerNormals(UIntPtr Pointer, bool checkFixedNormals, bool smoothCornerNormals)
		{
			ScriptingInterfaceOfIManagedMeshEditOperations.call_ComputeCornerNormalsDelegate(Pointer, checkFixedNormals, smoothCornerNormals);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00010856 File Offset: 0x0000EA56
		public void ComputeCornerNormalsWithSmoothingData(UIntPtr Pointer)
		{
			ScriptingInterfaceOfIManagedMeshEditOperations.call_ComputeCornerNormalsWithSmoothingDataDelegate(Pointer);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00010863 File Offset: 0x0000EA63
		public int ComputeTangents(UIntPtr Pointer, bool checkFixedNormals)
		{
			return ScriptingInterfaceOfIManagedMeshEditOperations.call_ComputeTangentsDelegate(Pointer, checkFixedNormals);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00010874 File Offset: 0x0000EA74
		public ManagedMeshEditOperations Create(UIntPtr meshPointer)
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIManagedMeshEditOperations.call_CreateDelegate(meshPointer);
			ManagedMeshEditOperations result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new ManagedMeshEditOperations(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x000108BE File Offset: 0x0000EABE
		public void EnsureTransformedVertices(UIntPtr Pointer)
		{
			ScriptingInterfaceOfIManagedMeshEditOperations.call_EnsureTransformedVerticesDelegate(Pointer);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x000108CB File Offset: 0x0000EACB
		public void FinalizeEditing(UIntPtr Pointer)
		{
			ScriptingInterfaceOfIManagedMeshEditOperations.call_FinalizeEditingDelegate(Pointer);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x000108D8 File Offset: 0x0000EAD8
		public void GenerateGrid(UIntPtr Pointer, ref Vec2i numEdges, ref Vec2 edgeScale)
		{
			ScriptingInterfaceOfIManagedMeshEditOperations.call_GenerateGridDelegate(Pointer, ref numEdges, ref edgeScale);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x000108E7 File Offset: 0x0000EAE7
		public Vec3 GetPositionOfVertex(UIntPtr Pointer, int vertexIndex)
		{
			return ScriptingInterfaceOfIManagedMeshEditOperations.call_GetPositionOfVertexDelegate(Pointer, vertexIndex);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x000108F5 File Offset: 0x0000EAF5
		public Vec3 GetVertexColor(UIntPtr Pointer, int faceCornerIndex)
		{
			return ScriptingInterfaceOfIManagedMeshEditOperations.call_GetVertexColorDelegate(Pointer, faceCornerIndex);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00010903 File Offset: 0x0000EB03
		public float GetVertexColorAlpha(UIntPtr Pointer)
		{
			return ScriptingInterfaceOfIManagedMeshEditOperations.call_GetVertexColorAlphaDelegate(Pointer);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00010910 File Offset: 0x0000EB10
		public void InvertFacesWindingOrder(UIntPtr Pointer)
		{
			ScriptingInterfaceOfIManagedMeshEditOperations.call_InvertFacesWindingOrderDelegate(Pointer);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0001091D File Offset: 0x0000EB1D
		public void MoveVerticesAlongNormal(UIntPtr Pointer, float moveAmount)
		{
			ScriptingInterfaceOfIManagedMeshEditOperations.call_MoveVerticesAlongNormalDelegate(Pointer, moveAmount);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0001092B File Offset: 0x0000EB2B
		public int RemoveDuplicatedCorners(UIntPtr Pointer)
		{
			return ScriptingInterfaceOfIManagedMeshEditOperations.call_RemoveDuplicatedCornersDelegate(Pointer);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00010938 File Offset: 0x0000EB38
		public void RemoveFace(UIntPtr Pointer, int faceIndex)
		{
			ScriptingInterfaceOfIManagedMeshEditOperations.call_RemoveFaceDelegate(Pointer, faceIndex);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00010946 File Offset: 0x0000EB46
		public void RescaleMesh2d(UIntPtr Pointer, ref Vec2 scaleSizeMin, ref Vec2 scaleSizeMax)
		{
			ScriptingInterfaceOfIManagedMeshEditOperations.call_RescaleMesh2dDelegate(Pointer, ref scaleSizeMin, ref scaleSizeMax);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00010955 File Offset: 0x0000EB55
		public void RescaleMesh2dRepeatX(UIntPtr Pointer, ref Vec2 scaleSizeMin, ref Vec2 scaleSizeMax, float frameThickness, int frameSide)
		{
			ScriptingInterfaceOfIManagedMeshEditOperations.call_RescaleMesh2dRepeatXDelegate(Pointer, ref scaleSizeMin, ref scaleSizeMax, frameThickness, frameSide);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00010968 File Offset: 0x0000EB68
		public void RescaleMesh2dRepeatXWithTiling(UIntPtr Pointer, ref Vec2 scaleSizeMin, ref Vec2 scaleSizeMax, float frameThickness, int frameSide, float xyRatio)
		{
			ScriptingInterfaceOfIManagedMeshEditOperations.call_RescaleMesh2dRepeatXWithTilingDelegate(Pointer, ref scaleSizeMin, ref scaleSizeMax, frameThickness, frameSide, xyRatio);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0001097D File Offset: 0x0000EB7D
		public void RescaleMesh2dRepeatY(UIntPtr Pointer, ref Vec2 scaleSizeMin, ref Vec2 scaleSizeMax, float frameThickness, int frameSide)
		{
			ScriptingInterfaceOfIManagedMeshEditOperations.call_RescaleMesh2dRepeatYDelegate(Pointer, ref scaleSizeMin, ref scaleSizeMax, frameThickness, frameSide);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00010990 File Offset: 0x0000EB90
		public void RescaleMesh2dRepeatYWithTiling(UIntPtr Pointer, ref Vec2 scaleSizeMin, ref Vec2 scaleSizeMax, float frameThickness, int frameSide, float xyRatio)
		{
			ScriptingInterfaceOfIManagedMeshEditOperations.call_RescaleMesh2dRepeatYWithTilingDelegate(Pointer, ref scaleSizeMin, ref scaleSizeMax, frameThickness, frameSide, xyRatio);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x000109A5 File Offset: 0x0000EBA5
		public void RescaleMesh2dWithoutChangingUV(UIntPtr Pointer, ref Vec2 scaleSizeMin, ref Vec2 scaleSizeMax, float remaining)
		{
			ScriptingInterfaceOfIManagedMeshEditOperations.call_RescaleMesh2dWithoutChangingUVDelegate(Pointer, ref scaleSizeMin, ref scaleSizeMax, remaining);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x000109B6 File Offset: 0x0000EBB6
		public void ReserveFaceCorners(UIntPtr Pointer, int count)
		{
			ScriptingInterfaceOfIManagedMeshEditOperations.call_ReserveFaceCornersDelegate(Pointer, count);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x000109C4 File Offset: 0x0000EBC4
		public void ReserveFaces(UIntPtr Pointer, int count)
		{
			ScriptingInterfaceOfIManagedMeshEditOperations.call_ReserveFacesDelegate(Pointer, count);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x000109D2 File Offset: 0x0000EBD2
		public void ReserveVertices(UIntPtr Pointer, int count)
		{
			ScriptingInterfaceOfIManagedMeshEditOperations.call_ReserveVerticesDelegate(Pointer, count);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x000109E0 File Offset: 0x0000EBE0
		public void ScaleVertices1(UIntPtr Pointer, float newScale)
		{
			ScriptingInterfaceOfIManagedMeshEditOperations.call_ScaleVertices1Delegate(Pointer, newScale);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x000109EE File Offset: 0x0000EBEE
		public void ScaleVertices2(UIntPtr Pointer, ref Vec3 newScale, bool keepUvX, float maxUvSize)
		{
			ScriptingInterfaceOfIManagedMeshEditOperations.call_ScaleVertices2Delegate(Pointer, ref newScale, keepUvX, maxUvSize);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x000109FF File Offset: 0x0000EBFF
		public void SetCornerUV(UIntPtr Pointer, int cornerNo, ref Vec2 newUV, int uvNumber)
		{
			ScriptingInterfaceOfIManagedMeshEditOperations.call_SetCornerUVDelegate(Pointer, cornerNo, ref newUV, uvNumber);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00010A10 File Offset: 0x0000EC10
		public void SetCornerVertexColor(UIntPtr Pointer, int cornerNo, ref Vec3 vertexColor)
		{
			ScriptingInterfaceOfIManagedMeshEditOperations.call_SetCornerVertexColorDelegate(Pointer, cornerNo, ref vertexColor);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00010A1F File Offset: 0x0000EC1F
		public void SetPositionOfVertex(UIntPtr Pointer, int vertexIndex, ref Vec3 position)
		{
			ScriptingInterfaceOfIManagedMeshEditOperations.call_SetPositionOfVertexDelegate(Pointer, vertexIndex, ref position);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00010A2E File Offset: 0x0000EC2E
		public void SetTangentsOfFaceCorner(UIntPtr Pointer, int faceCornerIndex, ref Vec3 tangent, ref Vec3 binormal)
		{
			ScriptingInterfaceOfIManagedMeshEditOperations.call_SetTangentsOfFaceCornerDelegate(Pointer, faceCornerIndex, ref tangent, ref binormal);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00010A3F File Offset: 0x0000EC3F
		public void SetVertexColor(UIntPtr Pointer, ref Vec3 color)
		{
			ScriptingInterfaceOfIManagedMeshEditOperations.call_SetVertexColorDelegate(Pointer, ref color);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00010A4D File Offset: 0x0000EC4D
		public void SetVertexColorAlpha(UIntPtr Pointer, float newAlpha)
		{
			ScriptingInterfaceOfIManagedMeshEditOperations.call_SetVertexColorAlphaDelegate(Pointer, newAlpha);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00010A5B File Offset: 0x0000EC5B
		public void TransformVerticesToLocal(UIntPtr Pointer, ref MatrixFrame frame)
		{
			ScriptingInterfaceOfIManagedMeshEditOperations.call_TransformVerticesToLocalDelegate(Pointer, ref frame);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00010A69 File Offset: 0x0000EC69
		public void TransformVerticesToParent(UIntPtr Pointer, ref MatrixFrame frame)
		{
			ScriptingInterfaceOfIManagedMeshEditOperations.call_TransformVerticesToParentDelegate(Pointer, ref frame);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00010A77 File Offset: 0x0000EC77
		public void TranslateVertices(UIntPtr Pointer, ref Vec3 newOrigin)
		{
			ScriptingInterfaceOfIManagedMeshEditOperations.call_TranslateVerticesDelegate(Pointer, ref newOrigin);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00010A85 File Offset: 0x0000EC85
		public void UpdateOverlappedVertexNormals(UIntPtr Pointer, UIntPtr meshPointer, ref MatrixFrame attachFrame, float mergeRadiusSQ)
		{
			ScriptingInterfaceOfIManagedMeshEditOperations.call_UpdateOverlappedVertexNormalsDelegate(Pointer, meshPointer, ref attachFrame, mergeRadiusSQ);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00010A96 File Offset: 0x0000EC96
		public void Weld(UIntPtr Pointer)
		{
			ScriptingInterfaceOfIManagedMeshEditOperations.call_WeldDelegate(Pointer);
		}

		// Token: 0x040001BB RID: 443
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x040001BC RID: 444
		public static ScriptingInterfaceOfIManagedMeshEditOperations.AddFaceDelegate call_AddFaceDelegate;

		// Token: 0x040001BD RID: 445
		public static ScriptingInterfaceOfIManagedMeshEditOperations.AddFaceCorner1Delegate call_AddFaceCorner1Delegate;

		// Token: 0x040001BE RID: 446
		public static ScriptingInterfaceOfIManagedMeshEditOperations.AddFaceCorner2Delegate call_AddFaceCorner2Delegate;

		// Token: 0x040001BF RID: 447
		public static ScriptingInterfaceOfIManagedMeshEditOperations.AddLineDelegate call_AddLineDelegate;

		// Token: 0x040001C0 RID: 448
		public static ScriptingInterfaceOfIManagedMeshEditOperations.AddMeshDelegate call_AddMeshDelegate;

		// Token: 0x040001C1 RID: 449
		public static ScriptingInterfaceOfIManagedMeshEditOperations.AddMeshAuxDelegate call_AddMeshAuxDelegate;

		// Token: 0x040001C2 RID: 450
		public static ScriptingInterfaceOfIManagedMeshEditOperations.AddMeshToBoneDelegate call_AddMeshToBoneDelegate;

		// Token: 0x040001C3 RID: 451
		public static ScriptingInterfaceOfIManagedMeshEditOperations.AddMeshWithColorDelegate call_AddMeshWithColorDelegate;

		// Token: 0x040001C4 RID: 452
		public static ScriptingInterfaceOfIManagedMeshEditOperations.AddMeshWithFixedNormalsDelegate call_AddMeshWithFixedNormalsDelegate;

		// Token: 0x040001C5 RID: 453
		public static ScriptingInterfaceOfIManagedMeshEditOperations.AddMeshWithFixedNormalsWithHeightGradientColorDelegate call_AddMeshWithFixedNormalsWithHeightGradientColorDelegate;

		// Token: 0x040001C6 RID: 454
		public static ScriptingInterfaceOfIManagedMeshEditOperations.AddMeshWithSkinDataDelegate call_AddMeshWithSkinDataDelegate;

		// Token: 0x040001C7 RID: 455
		public static ScriptingInterfaceOfIManagedMeshEditOperations.AddRectDelegate call_AddRectDelegate;

		// Token: 0x040001C8 RID: 456
		public static ScriptingInterfaceOfIManagedMeshEditOperations.AddRectangle3Delegate call_AddRectangle3Delegate;

		// Token: 0x040001C9 RID: 457
		public static ScriptingInterfaceOfIManagedMeshEditOperations.AddRectangleWithInverseUVDelegate call_AddRectangleWithInverseUVDelegate;

		// Token: 0x040001CA RID: 458
		public static ScriptingInterfaceOfIManagedMeshEditOperations.AddRectWithZUpDelegate call_AddRectWithZUpDelegate;

		// Token: 0x040001CB RID: 459
		public static ScriptingInterfaceOfIManagedMeshEditOperations.AddSkinnedMeshWithColorDelegate call_AddSkinnedMeshWithColorDelegate;

		// Token: 0x040001CC RID: 460
		public static ScriptingInterfaceOfIManagedMeshEditOperations.AddTriangle1Delegate call_AddTriangle1Delegate;

		// Token: 0x040001CD RID: 461
		public static ScriptingInterfaceOfIManagedMeshEditOperations.AddTriangle2Delegate call_AddTriangle2Delegate;

		// Token: 0x040001CE RID: 462
		public static ScriptingInterfaceOfIManagedMeshEditOperations.AddVertexDelegate call_AddVertexDelegate;

		// Token: 0x040001CF RID: 463
		public static ScriptingInterfaceOfIManagedMeshEditOperations.ApplyCPUSkinningDelegate call_ApplyCPUSkinningDelegate;

		// Token: 0x040001D0 RID: 464
		public static ScriptingInterfaceOfIManagedMeshEditOperations.ClearAllDelegate call_ClearAllDelegate;

		// Token: 0x040001D1 RID: 465
		public static ScriptingInterfaceOfIManagedMeshEditOperations.ComputeCornerNormalsDelegate call_ComputeCornerNormalsDelegate;

		// Token: 0x040001D2 RID: 466
		public static ScriptingInterfaceOfIManagedMeshEditOperations.ComputeCornerNormalsWithSmoothingDataDelegate call_ComputeCornerNormalsWithSmoothingDataDelegate;

		// Token: 0x040001D3 RID: 467
		public static ScriptingInterfaceOfIManagedMeshEditOperations.ComputeTangentsDelegate call_ComputeTangentsDelegate;

		// Token: 0x040001D4 RID: 468
		public static ScriptingInterfaceOfIManagedMeshEditOperations.CreateDelegate call_CreateDelegate;

		// Token: 0x040001D5 RID: 469
		public static ScriptingInterfaceOfIManagedMeshEditOperations.EnsureTransformedVerticesDelegate call_EnsureTransformedVerticesDelegate;

		// Token: 0x040001D6 RID: 470
		public static ScriptingInterfaceOfIManagedMeshEditOperations.FinalizeEditingDelegate call_FinalizeEditingDelegate;

		// Token: 0x040001D7 RID: 471
		public static ScriptingInterfaceOfIManagedMeshEditOperations.GenerateGridDelegate call_GenerateGridDelegate;

		// Token: 0x040001D8 RID: 472
		public static ScriptingInterfaceOfIManagedMeshEditOperations.GetPositionOfVertexDelegate call_GetPositionOfVertexDelegate;

		// Token: 0x040001D9 RID: 473
		public static ScriptingInterfaceOfIManagedMeshEditOperations.GetVertexColorDelegate call_GetVertexColorDelegate;

		// Token: 0x040001DA RID: 474
		public static ScriptingInterfaceOfIManagedMeshEditOperations.GetVertexColorAlphaDelegate call_GetVertexColorAlphaDelegate;

		// Token: 0x040001DB RID: 475
		public static ScriptingInterfaceOfIManagedMeshEditOperations.InvertFacesWindingOrderDelegate call_InvertFacesWindingOrderDelegate;

		// Token: 0x040001DC RID: 476
		public static ScriptingInterfaceOfIManagedMeshEditOperations.MoveVerticesAlongNormalDelegate call_MoveVerticesAlongNormalDelegate;

		// Token: 0x040001DD RID: 477
		public static ScriptingInterfaceOfIManagedMeshEditOperations.RemoveDuplicatedCornersDelegate call_RemoveDuplicatedCornersDelegate;

		// Token: 0x040001DE RID: 478
		public static ScriptingInterfaceOfIManagedMeshEditOperations.RemoveFaceDelegate call_RemoveFaceDelegate;

		// Token: 0x040001DF RID: 479
		public static ScriptingInterfaceOfIManagedMeshEditOperations.RescaleMesh2dDelegate call_RescaleMesh2dDelegate;

		// Token: 0x040001E0 RID: 480
		public static ScriptingInterfaceOfIManagedMeshEditOperations.RescaleMesh2dRepeatXDelegate call_RescaleMesh2dRepeatXDelegate;

		// Token: 0x040001E1 RID: 481
		public static ScriptingInterfaceOfIManagedMeshEditOperations.RescaleMesh2dRepeatXWithTilingDelegate call_RescaleMesh2dRepeatXWithTilingDelegate;

		// Token: 0x040001E2 RID: 482
		public static ScriptingInterfaceOfIManagedMeshEditOperations.RescaleMesh2dRepeatYDelegate call_RescaleMesh2dRepeatYDelegate;

		// Token: 0x040001E3 RID: 483
		public static ScriptingInterfaceOfIManagedMeshEditOperations.RescaleMesh2dRepeatYWithTilingDelegate call_RescaleMesh2dRepeatYWithTilingDelegate;

		// Token: 0x040001E4 RID: 484
		public static ScriptingInterfaceOfIManagedMeshEditOperations.RescaleMesh2dWithoutChangingUVDelegate call_RescaleMesh2dWithoutChangingUVDelegate;

		// Token: 0x040001E5 RID: 485
		public static ScriptingInterfaceOfIManagedMeshEditOperations.ReserveFaceCornersDelegate call_ReserveFaceCornersDelegate;

		// Token: 0x040001E6 RID: 486
		public static ScriptingInterfaceOfIManagedMeshEditOperations.ReserveFacesDelegate call_ReserveFacesDelegate;

		// Token: 0x040001E7 RID: 487
		public static ScriptingInterfaceOfIManagedMeshEditOperations.ReserveVerticesDelegate call_ReserveVerticesDelegate;

		// Token: 0x040001E8 RID: 488
		public static ScriptingInterfaceOfIManagedMeshEditOperations.ScaleVertices1Delegate call_ScaleVertices1Delegate;

		// Token: 0x040001E9 RID: 489
		public static ScriptingInterfaceOfIManagedMeshEditOperations.ScaleVertices2Delegate call_ScaleVertices2Delegate;

		// Token: 0x040001EA RID: 490
		public static ScriptingInterfaceOfIManagedMeshEditOperations.SetCornerUVDelegate call_SetCornerUVDelegate;

		// Token: 0x040001EB RID: 491
		public static ScriptingInterfaceOfIManagedMeshEditOperations.SetCornerVertexColorDelegate call_SetCornerVertexColorDelegate;

		// Token: 0x040001EC RID: 492
		public static ScriptingInterfaceOfIManagedMeshEditOperations.SetPositionOfVertexDelegate call_SetPositionOfVertexDelegate;

		// Token: 0x040001ED RID: 493
		public static ScriptingInterfaceOfIManagedMeshEditOperations.SetTangentsOfFaceCornerDelegate call_SetTangentsOfFaceCornerDelegate;

		// Token: 0x040001EE RID: 494
		public static ScriptingInterfaceOfIManagedMeshEditOperations.SetVertexColorDelegate call_SetVertexColorDelegate;

		// Token: 0x040001EF RID: 495
		public static ScriptingInterfaceOfIManagedMeshEditOperations.SetVertexColorAlphaDelegate call_SetVertexColorAlphaDelegate;

		// Token: 0x040001F0 RID: 496
		public static ScriptingInterfaceOfIManagedMeshEditOperations.TransformVerticesToLocalDelegate call_TransformVerticesToLocalDelegate;

		// Token: 0x040001F1 RID: 497
		public static ScriptingInterfaceOfIManagedMeshEditOperations.TransformVerticesToParentDelegate call_TransformVerticesToParentDelegate;

		// Token: 0x040001F2 RID: 498
		public static ScriptingInterfaceOfIManagedMeshEditOperations.TranslateVerticesDelegate call_TranslateVerticesDelegate;

		// Token: 0x040001F3 RID: 499
		public static ScriptingInterfaceOfIManagedMeshEditOperations.UpdateOverlappedVertexNormalsDelegate call_UpdateOverlappedVertexNormalsDelegate;

		// Token: 0x040001F4 RID: 500
		public static ScriptingInterfaceOfIManagedMeshEditOperations.WeldDelegate call_WeldDelegate;

		// Token: 0x0200021E RID: 542
		// (Invoke) Token: 0x06000D7B RID: 3451
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int AddFaceDelegate(UIntPtr Pointer, int patchNode0, int patchNode1, int patchNode2);

		// Token: 0x0200021F RID: 543
		// (Invoke) Token: 0x06000D7F RID: 3455
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int AddFaceCorner1Delegate(UIntPtr Pointer, int vertexIndex, ref Vec2 uv0, ref Vec3 color, ref Vec3 normal);

		// Token: 0x02000220 RID: 544
		// (Invoke) Token: 0x06000D83 RID: 3459
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int AddFaceCorner2Delegate(UIntPtr Pointer, int vertexIndex, ref Vec2 uv0, ref Vec2 uv1, ref Vec3 color, ref Vec3 normal);

		// Token: 0x02000221 RID: 545
		// (Invoke) Token: 0x06000D87 RID: 3463
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddLineDelegate(UIntPtr Pointer, ref Vec3 start, ref Vec3 end, ref Vec3 color, float lineWidth);

		// Token: 0x02000222 RID: 546
		// (Invoke) Token: 0x06000D8B RID: 3467
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddMeshDelegate(UIntPtr Pointer, UIntPtr meshPointer, ref MatrixFrame frame);

		// Token: 0x02000223 RID: 547
		// (Invoke) Token: 0x06000D8F RID: 3471
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddMeshAuxDelegate(UIntPtr Pointer, UIntPtr meshPointer, ref MatrixFrame frame, sbyte boneIndex, ref Vec3 color, [MarshalAs(UnmanagedType.U1)] bool transformNormal, [MarshalAs(UnmanagedType.U1)] bool heightGradient, [MarshalAs(UnmanagedType.U1)] bool addSkinData, [MarshalAs(UnmanagedType.U1)] bool useDoublePrecision);

		// Token: 0x02000224 RID: 548
		// (Invoke) Token: 0x06000D93 RID: 3475
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddMeshToBoneDelegate(UIntPtr Pointer, UIntPtr meshPointer, ref MatrixFrame frame, sbyte boneIndex);

		// Token: 0x02000225 RID: 549
		// (Invoke) Token: 0x06000D97 RID: 3479
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddMeshWithColorDelegate(UIntPtr Pointer, UIntPtr meshPointer, ref MatrixFrame frame, ref Vec3 vertexColor, [MarshalAs(UnmanagedType.U1)] bool useDoublePrecision);

		// Token: 0x02000226 RID: 550
		// (Invoke) Token: 0x06000D9B RID: 3483
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddMeshWithFixedNormalsDelegate(UIntPtr Pointer, UIntPtr meshPointer, ref MatrixFrame frame);

		// Token: 0x02000227 RID: 551
		// (Invoke) Token: 0x06000D9F RID: 3487
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddMeshWithFixedNormalsWithHeightGradientColorDelegate(UIntPtr Pointer, UIntPtr meshPointer, ref MatrixFrame frame);

		// Token: 0x02000228 RID: 552
		// (Invoke) Token: 0x06000DA3 RID: 3491
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddMeshWithSkinDataDelegate(UIntPtr Pointer, UIntPtr meshPointer, ref MatrixFrame frame, sbyte boneIndex);

		// Token: 0x02000229 RID: 553
		// (Invoke) Token: 0x06000DA7 RID: 3495
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddRectDelegate(UIntPtr Pointer, ref Vec3 originBegin, ref Vec3 originEnd, ref Vec2 uvBegin, ref Vec2 uvEnd, ref Vec3 color);

		// Token: 0x0200022A RID: 554
		// (Invoke) Token: 0x06000DAB RID: 3499
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddRectangle3Delegate(UIntPtr Pointer, ref Vec3 o, ref Vec2 size, ref Vec2 uv_origin, ref Vec2 uvSize, ref Vec3 color);

		// Token: 0x0200022B RID: 555
		// (Invoke) Token: 0x06000DAF RID: 3503
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddRectangleWithInverseUVDelegate(UIntPtr Pointer, ref Vec3 o, ref Vec2 size, ref Vec2 uv_origin, ref Vec2 uvSize, ref Vec3 color);

		// Token: 0x0200022C RID: 556
		// (Invoke) Token: 0x06000DB3 RID: 3507
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddRectWithZUpDelegate(UIntPtr Pointer, ref Vec3 originBegin, ref Vec3 originEnd, ref Vec2 uvBegin, ref Vec2 uvEnd, ref Vec3 color);

		// Token: 0x0200022D RID: 557
		// (Invoke) Token: 0x06000DB7 RID: 3511
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddSkinnedMeshWithColorDelegate(UIntPtr Pointer, UIntPtr meshPointer, ref MatrixFrame frame, ref Vec3 vertexColor, [MarshalAs(UnmanagedType.U1)] bool useDoublePrecision);

		// Token: 0x0200022E RID: 558
		// (Invoke) Token: 0x06000DBB RID: 3515
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddTriangle1Delegate(UIntPtr Pointer, ref Vec3 p1, ref Vec3 p2, ref Vec3 p3, ref Vec2 uv1, ref Vec2 uv2, ref Vec2 uv3, ref Vec3 color);

		// Token: 0x0200022F RID: 559
		// (Invoke) Token: 0x06000DBF RID: 3519
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddTriangle2Delegate(UIntPtr Pointer, ref Vec3 p1, ref Vec3 p2, ref Vec3 p3, ref Vec3 n1, ref Vec3 n2, ref Vec3 n3, ref Vec2 uv1, ref Vec2 uv2, ref Vec2 uv3, ref Vec3 c1, ref Vec3 c2, ref Vec3 c3);

		// Token: 0x02000230 RID: 560
		// (Invoke) Token: 0x06000DC3 RID: 3523
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int AddVertexDelegate(UIntPtr Pointer, ref Vec3 vertexPos);

		// Token: 0x02000231 RID: 561
		// (Invoke) Token: 0x06000DC7 RID: 3527
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ApplyCPUSkinningDelegate(UIntPtr Pointer, UIntPtr skeletonPointer);

		// Token: 0x02000232 RID: 562
		// (Invoke) Token: 0x06000DCB RID: 3531
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ClearAllDelegate(UIntPtr Pointer);

		// Token: 0x02000233 RID: 563
		// (Invoke) Token: 0x06000DCF RID: 3535
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ComputeCornerNormalsDelegate(UIntPtr Pointer, [MarshalAs(UnmanagedType.U1)] bool checkFixedNormals, [MarshalAs(UnmanagedType.U1)] bool smoothCornerNormals);

		// Token: 0x02000234 RID: 564
		// (Invoke) Token: 0x06000DD3 RID: 3539
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ComputeCornerNormalsWithSmoothingDataDelegate(UIntPtr Pointer);

		// Token: 0x02000235 RID: 565
		// (Invoke) Token: 0x06000DD7 RID: 3543
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int ComputeTangentsDelegate(UIntPtr Pointer, [MarshalAs(UnmanagedType.U1)] bool checkFixedNormals);

		// Token: 0x02000236 RID: 566
		// (Invoke) Token: 0x06000DDB RID: 3547
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer CreateDelegate(UIntPtr meshPointer);

		// Token: 0x02000237 RID: 567
		// (Invoke) Token: 0x06000DDF RID: 3551
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void EnsureTransformedVerticesDelegate(UIntPtr Pointer);

		// Token: 0x02000238 RID: 568
		// (Invoke) Token: 0x06000DE3 RID: 3555
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void FinalizeEditingDelegate(UIntPtr Pointer);

		// Token: 0x02000239 RID: 569
		// (Invoke) Token: 0x06000DE7 RID: 3559
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GenerateGridDelegate(UIntPtr Pointer, ref Vec2i numEdges, ref Vec2 edgeScale);

		// Token: 0x0200023A RID: 570
		// (Invoke) Token: 0x06000DEB RID: 3563
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate Vec3 GetPositionOfVertexDelegate(UIntPtr Pointer, int vertexIndex);

		// Token: 0x0200023B RID: 571
		// (Invoke) Token: 0x06000DEF RID: 3567
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate Vec3 GetVertexColorDelegate(UIntPtr Pointer, int faceCornerIndex);

		// Token: 0x0200023C RID: 572
		// (Invoke) Token: 0x06000DF3 RID: 3571
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetVertexColorAlphaDelegate(UIntPtr Pointer);

		// Token: 0x0200023D RID: 573
		// (Invoke) Token: 0x06000DF7 RID: 3575
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void InvertFacesWindingOrderDelegate(UIntPtr Pointer);

		// Token: 0x0200023E RID: 574
		// (Invoke) Token: 0x06000DFB RID: 3579
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void MoveVerticesAlongNormalDelegate(UIntPtr Pointer, float moveAmount);

		// Token: 0x0200023F RID: 575
		// (Invoke) Token: 0x06000DFF RID: 3583
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int RemoveDuplicatedCornersDelegate(UIntPtr Pointer);

		// Token: 0x02000240 RID: 576
		// (Invoke) Token: 0x06000E03 RID: 3587
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RemoveFaceDelegate(UIntPtr Pointer, int faceIndex);

		// Token: 0x02000241 RID: 577
		// (Invoke) Token: 0x06000E07 RID: 3591
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RescaleMesh2dDelegate(UIntPtr Pointer, ref Vec2 scaleSizeMin, ref Vec2 scaleSizeMax);

		// Token: 0x02000242 RID: 578
		// (Invoke) Token: 0x06000E0B RID: 3595
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RescaleMesh2dRepeatXDelegate(UIntPtr Pointer, ref Vec2 scaleSizeMin, ref Vec2 scaleSizeMax, float frameThickness, int frameSide);

		// Token: 0x02000243 RID: 579
		// (Invoke) Token: 0x06000E0F RID: 3599
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RescaleMesh2dRepeatXWithTilingDelegate(UIntPtr Pointer, ref Vec2 scaleSizeMin, ref Vec2 scaleSizeMax, float frameThickness, int frameSide, float xyRatio);

		// Token: 0x02000244 RID: 580
		// (Invoke) Token: 0x06000E13 RID: 3603
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RescaleMesh2dRepeatYDelegate(UIntPtr Pointer, ref Vec2 scaleSizeMin, ref Vec2 scaleSizeMax, float frameThickness, int frameSide);

		// Token: 0x02000245 RID: 581
		// (Invoke) Token: 0x06000E17 RID: 3607
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RescaleMesh2dRepeatYWithTilingDelegate(UIntPtr Pointer, ref Vec2 scaleSizeMin, ref Vec2 scaleSizeMax, float frameThickness, int frameSide, float xyRatio);

		// Token: 0x02000246 RID: 582
		// (Invoke) Token: 0x06000E1B RID: 3611
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RescaleMesh2dWithoutChangingUVDelegate(UIntPtr Pointer, ref Vec2 scaleSizeMin, ref Vec2 scaleSizeMax, float remaining);

		// Token: 0x02000247 RID: 583
		// (Invoke) Token: 0x06000E1F RID: 3615
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ReserveFaceCornersDelegate(UIntPtr Pointer, int count);

		// Token: 0x02000248 RID: 584
		// (Invoke) Token: 0x06000E23 RID: 3619
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ReserveFacesDelegate(UIntPtr Pointer, int count);

		// Token: 0x02000249 RID: 585
		// (Invoke) Token: 0x06000E27 RID: 3623
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ReserveVerticesDelegate(UIntPtr Pointer, int count);

		// Token: 0x0200024A RID: 586
		// (Invoke) Token: 0x06000E2B RID: 3627
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ScaleVertices1Delegate(UIntPtr Pointer, float newScale);

		// Token: 0x0200024B RID: 587
		// (Invoke) Token: 0x06000E2F RID: 3631
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ScaleVertices2Delegate(UIntPtr Pointer, ref Vec3 newScale, [MarshalAs(UnmanagedType.U1)] bool keepUvX, float maxUvSize);

		// Token: 0x0200024C RID: 588
		// (Invoke) Token: 0x06000E33 RID: 3635
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetCornerUVDelegate(UIntPtr Pointer, int cornerNo, ref Vec2 newUV, int uvNumber);

		// Token: 0x0200024D RID: 589
		// (Invoke) Token: 0x06000E37 RID: 3639
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetCornerVertexColorDelegate(UIntPtr Pointer, int cornerNo, ref Vec3 vertexColor);

		// Token: 0x0200024E RID: 590
		// (Invoke) Token: 0x06000E3B RID: 3643
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetPositionOfVertexDelegate(UIntPtr Pointer, int vertexIndex, ref Vec3 position);

		// Token: 0x0200024F RID: 591
		// (Invoke) Token: 0x06000E3F RID: 3647
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetTangentsOfFaceCornerDelegate(UIntPtr Pointer, int faceCornerIndex, ref Vec3 tangent, ref Vec3 binormal);

		// Token: 0x02000250 RID: 592
		// (Invoke) Token: 0x06000E43 RID: 3651
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetVertexColorDelegate(UIntPtr Pointer, ref Vec3 color);

		// Token: 0x02000251 RID: 593
		// (Invoke) Token: 0x06000E47 RID: 3655
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetVertexColorAlphaDelegate(UIntPtr Pointer, float newAlpha);

		// Token: 0x02000252 RID: 594
		// (Invoke) Token: 0x06000E4B RID: 3659
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void TransformVerticesToLocalDelegate(UIntPtr Pointer, ref MatrixFrame frame);

		// Token: 0x02000253 RID: 595
		// (Invoke) Token: 0x06000E4F RID: 3663
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void TransformVerticesToParentDelegate(UIntPtr Pointer, ref MatrixFrame frame);

		// Token: 0x02000254 RID: 596
		// (Invoke) Token: 0x06000E53 RID: 3667
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void TranslateVerticesDelegate(UIntPtr Pointer, ref Vec3 newOrigin);

		// Token: 0x02000255 RID: 597
		// (Invoke) Token: 0x06000E57 RID: 3671
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void UpdateOverlappedVertexNormalsDelegate(UIntPtr Pointer, UIntPtr meshPointer, ref MatrixFrame attachFrame, float mergeRadiusSQ);

		// Token: 0x02000256 RID: 598
		// (Invoke) Token: 0x06000E5B RID: 3675
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void WeldDelegate(UIntPtr Pointer);
	}
}
