using System;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000057 RID: 87
	[EngineClass("rglManaged_mesh_edit_operations")]
	public sealed class ManagedMeshEditOperations : NativeObject
	{
		// Token: 0x0600071F RID: 1823 RVA: 0x00005D54 File Offset: 0x00003F54
		internal ManagedMeshEditOperations(UIntPtr pointer)
		{
			base.Construct(pointer);
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00005D63 File Offset: 0x00003F63
		public static ManagedMeshEditOperations Create(Mesh meshToEdit)
		{
			return EngineApplicationInterface.IManagedMeshEditOperations.Create(meshToEdit.Pointer);
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00005D75 File Offset: 0x00003F75
		public void Weld()
		{
			EngineApplicationInterface.IManagedMeshEditOperations.Weld(base.Pointer);
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x00005D87 File Offset: 0x00003F87
		public int AddVertex(Vec3 vertexPos)
		{
			return EngineApplicationInterface.IManagedMeshEditOperations.AddVertex(base.Pointer, ref vertexPos);
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00005D9B File Offset: 0x00003F9B
		public int AddFaceCorner(int vertexIndex, Vec2 uv0, Vec3 color, Vec3 normal)
		{
			return EngineApplicationInterface.IManagedMeshEditOperations.AddFaceCorner1(base.Pointer, vertexIndex, ref uv0, ref color, ref normal);
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00005DB4 File Offset: 0x00003FB4
		public int AddFaceCorner(int vertexIndex, Vec2 uv0, Vec2 uv1, Vec3 color, Vec3 normal)
		{
			return EngineApplicationInterface.IManagedMeshEditOperations.AddFaceCorner2(base.Pointer, vertexIndex, ref uv0, ref uv1, ref color, ref normal);
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x00005DCF File Offset: 0x00003FCF
		public int AddFace(int patchNode0, int patchNode1, int patchNode2)
		{
			return EngineApplicationInterface.IManagedMeshEditOperations.AddFace(base.Pointer, patchNode0, patchNode1, patchNode2);
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x00005DE4 File Offset: 0x00003FE4
		public void AddTriangle(Vec3 p1, Vec3 p2, Vec3 p3, Vec2 uv1, Vec2 uv2, Vec2 uv3, Vec3 color)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.AddTriangle1(base.Pointer, ref p1, ref p2, ref p3, ref uv1, ref uv2, ref uv3, ref color);
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x00005E10 File Offset: 0x00004010
		public void AddTriangle(Vec3 p1, Vec3 p2, Vec3 p3, Vec3 n1, Vec3 n2, Vec3 n3, Vec2 uv1, Vec2 uv2, Vec2 uv3, Vec3 c1, Vec3 c2, Vec3 c3)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.AddTriangle2(base.Pointer, ref p1, ref p2, ref p3, ref n1, ref n2, ref n3, ref uv1, ref uv2, ref uv3, ref c1, ref c2, ref c3);
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x00005E45 File Offset: 0x00004045
		public void AddRectangle3(Vec3 o, Vec2 size, Vec2 uv_origin, Vec2 uvSize, Vec3 color)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.AddRectangle3(base.Pointer, ref o, ref size, ref uv_origin, ref uvSize, ref color);
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x00005E61 File Offset: 0x00004061
		public void AddRectangleWithInverseUV(Vec3 o, Vec2 size, Vec2 uv_origin, Vec2 uvSize, Vec3 color)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.AddRectangleWithInverseUV(base.Pointer, ref o, ref size, ref uv_origin, ref uvSize, ref color);
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x00005E7D File Offset: 0x0000407D
		public void AddRect(Vec3 originBegin, Vec3 originEnd, Vec2 uvBegin, Vec2 uvEnd, Vec3 color)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.AddRect(base.Pointer, ref originBegin, ref originEnd, ref uvBegin, ref uvEnd, ref color);
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x00005E99 File Offset: 0x00004099
		public void AddRectWithZUp(Vec3 originBegin, Vec3 originEnd, Vec2 uvBegin, Vec2 uvEnd, Vec3 color)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.AddRectWithZUp(base.Pointer, ref originBegin, ref originEnd, ref uvBegin, ref uvEnd, ref color);
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x00005EB5 File Offset: 0x000040B5
		public void InvertFacesWindingOrder()
		{
			EngineApplicationInterface.IManagedMeshEditOperations.InvertFacesWindingOrder(base.Pointer);
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x00005EC7 File Offset: 0x000040C7
		public void ScaleVertices(float newScale)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.ScaleVertices1(base.Pointer, newScale);
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x00005EDA File Offset: 0x000040DA
		public void MoveVerticesAlongNormal(float moveAmount)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.MoveVerticesAlongNormal(base.Pointer, moveAmount);
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x00005EED File Offset: 0x000040ED
		public void ScaleVertices(Vec3 newScale, bool keepUvX = false, float maxUvSize = 1f)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.ScaleVertices2(base.Pointer, ref newScale, keepUvX, maxUvSize);
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x00005F03 File Offset: 0x00004103
		public void TranslateVertices(Vec3 newOrigin)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.TranslateVertices(base.Pointer, ref newOrigin);
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x00005F18 File Offset: 0x00004118
		public void AddMeshAux(Mesh mesh, MatrixFrame frame, sbyte boneNo, Vec3 color, bool transformNormal, bool heightGradient, bool addSkinData, bool useDoublePrecision = true)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.AddMeshAux(base.Pointer, mesh.Pointer, ref frame, boneNo, ref color, transformNormal, heightGradient, addSkinData, useDoublePrecision);
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x00005F48 File Offset: 0x00004148
		public int ComputeTangents(bool checkFixedNormals)
		{
			return EngineApplicationInterface.IManagedMeshEditOperations.ComputeTangents(base.Pointer, checkFixedNormals);
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x00005F5B File Offset: 0x0000415B
		public void GenerateGrid(Vec2i numEdges, Vec2 edgeScale)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.GenerateGrid(base.Pointer, ref numEdges, ref edgeScale);
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x00005F71 File Offset: 0x00004171
		public void RescaleMesh2d(Vec2 scaleSizeMin, Vec2 scaleSizeMax)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.RescaleMesh2d(base.Pointer, ref scaleSizeMin, ref scaleSizeMax);
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x00005F87 File Offset: 0x00004187
		public void RescaleMesh2dRepeatX(Vec2 scaleSizeMin, Vec2 scaleSizeMax, float frameThickness = 0f, int frameSide = 0)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.RescaleMesh2dRepeatX(base.Pointer, ref scaleSizeMin, ref scaleSizeMax, frameThickness, frameSide);
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x00005FA0 File Offset: 0x000041A0
		public void RescaleMesh2dRepeatY(Vec2 scaleSizeMin, Vec2 scaleSizeMax, float frameThickness = 0f, int frameSide = 0)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.RescaleMesh2dRepeatY(base.Pointer, ref scaleSizeMin, ref scaleSizeMax, frameThickness, frameSide);
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x00005FB9 File Offset: 0x000041B9
		public void RescaleMesh2dRepeatXWithTiling(Vec2 scaleSizeMin, Vec2 scaleSizeMax, float frameThickness = 0f, int frameSide = 0, float xyRatio = 0f)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.RescaleMesh2dRepeatXWithTiling(base.Pointer, ref scaleSizeMin, ref scaleSizeMax, frameThickness, frameSide, xyRatio);
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x00005FD4 File Offset: 0x000041D4
		public void RescaleMesh2dRepeatYWithTiling(Vec2 scaleSizeMin, Vec2 scaleSizeMax, float frameThickness = 0f, int frameSide = 0, float xyRatio = 0f)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.RescaleMesh2dRepeatYWithTiling(base.Pointer, ref scaleSizeMin, ref scaleSizeMax, frameThickness, frameSide, xyRatio);
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x00005FEF File Offset: 0x000041EF
		public void RescaleMesh2dWithoutChangingUV(Vec2 scaleSizeMin, Vec2 scaleSizeMax, float remaining)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.RescaleMesh2dRepeatYWithTiling(base.Pointer, ref scaleSizeMin, ref scaleSizeMax, remaining, 0, 0f);
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x0000600C File Offset: 0x0000420C
		public void AddLine(Vec3 start, Vec3 end, Vec3 color, float lineWidth = 0.004f)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.AddLine(base.Pointer, ref start, ref end, ref color, lineWidth);
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x00006026 File Offset: 0x00004226
		public void ComputeCornerNormals(bool checkFixedNormals = false, bool smoothCornerNormals = true)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.ComputeCornerNormals(base.Pointer, checkFixedNormals, smoothCornerNormals);
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x0000603A File Offset: 0x0000423A
		public void ComputeCornerNormalsWithSmoothingData()
		{
			EngineApplicationInterface.IManagedMeshEditOperations.ComputeCornerNormalsWithSmoothingData(base.Pointer);
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x0000604C File Offset: 0x0000424C
		public void AddMesh(Mesh mesh, MatrixFrame frame)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.AddMesh(base.Pointer, mesh.Pointer, ref frame);
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x00006066 File Offset: 0x00004266
		public void AddMeshWithSkinData(Mesh mesh, MatrixFrame frame, sbyte boneIndex)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.AddMeshWithSkinData(base.Pointer, mesh.Pointer, ref frame, boneIndex);
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x00006081 File Offset: 0x00004281
		public void AddMeshWithColor(Mesh mesh, MatrixFrame frame, Vec3 vertexColor, bool useDoublePrecision = true)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.AddMeshWithColor(base.Pointer, mesh.Pointer, ref frame, ref vertexColor, useDoublePrecision);
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0000609F File Offset: 0x0000429F
		public void AddMeshToBone(Mesh mesh, MatrixFrame frame, sbyte boneIndex)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.AddMeshToBone(base.Pointer, mesh.Pointer, ref frame, boneIndex);
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x000060BA File Offset: 0x000042BA
		public void AddMeshWithFixedNormals(Mesh mesh, MatrixFrame frame)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.AddMeshWithFixedNormals(base.Pointer, mesh.Pointer, ref frame);
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x000060D4 File Offset: 0x000042D4
		public void AddMeshWithFixedNormalsWithHeightGradientColor(Mesh mesh, MatrixFrame frame)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.AddMeshWithFixedNormalsWithHeightGradientColor(base.Pointer, mesh.Pointer, ref frame);
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x000060EE File Offset: 0x000042EE
		public void AddSkinnedMeshWithColor(Mesh mesh, MatrixFrame frame, Vec3 vertexColor, bool useDoublePrecision = true)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.AddSkinnedMeshWithColor(base.Pointer, mesh.Pointer, ref frame, ref vertexColor, useDoublePrecision);
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0000610C File Offset: 0x0000430C
		public void SetCornerVertexColor(int cornerNo, Vec3 vertexColor)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.SetCornerVertexColor(base.Pointer, cornerNo, ref vertexColor);
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x00006121 File Offset: 0x00004321
		public void SetCornerUV(int cornerNo, Vec2 newUV, int uvNumber = 0)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.SetCornerUV(base.Pointer, cornerNo, ref newUV, uvNumber);
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x00006137 File Offset: 0x00004337
		public void ReserveVertices(int count)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.ReserveVertices(base.Pointer, count);
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x0000614A File Offset: 0x0000434A
		public void ReserveFaceCorners(int count)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.ReserveFaceCorners(base.Pointer, count);
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x0000615D File Offset: 0x0000435D
		public void ReserveFaces(int count)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.ReserveFaces(base.Pointer, count);
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x00006170 File Offset: 0x00004370
		public int RemoveDuplicatedCorners()
		{
			return EngineApplicationInterface.IManagedMeshEditOperations.RemoveDuplicatedCorners(base.Pointer);
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00006182 File Offset: 0x00004382
		public void TransformVerticesToParent(MatrixFrame frame)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.TransformVerticesToParent(base.Pointer, ref frame);
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x00006196 File Offset: 0x00004396
		public void TransformVerticesToLocal(MatrixFrame frame)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.TransformVerticesToLocal(base.Pointer, ref frame);
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x000061AA File Offset: 0x000043AA
		public void SetVertexColor(Vec3 color)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.SetVertexColor(base.Pointer, ref color);
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x000061BE File Offset: 0x000043BE
		public Vec3 GetVertexColor(int faceCornerIndex)
		{
			return EngineApplicationInterface.IManagedMeshEditOperations.GetVertexColor(base.Pointer, faceCornerIndex);
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x000061D1 File Offset: 0x000043D1
		public void SetVertexColorAlpha(float newAlpha)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.SetVertexColorAlpha(base.Pointer, newAlpha);
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x000061E4 File Offset: 0x000043E4
		public float GetVertexColorAlpha()
		{
			return EngineApplicationInterface.IManagedMeshEditOperations.GetVertexColorAlpha(base.Pointer);
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x000061F6 File Offset: 0x000043F6
		public void EnsureTransformedVertices()
		{
			EngineApplicationInterface.IManagedMeshEditOperations.EnsureTransformedVertices(base.Pointer);
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x00006208 File Offset: 0x00004408
		public void ApplyCPUSkinning(Skeleton skeleton)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.ApplyCPUSkinning(base.Pointer, skeleton.Pointer);
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x00006220 File Offset: 0x00004420
		public void UpdateOverlappedVertexNormals(Mesh attachedToMesh, MatrixFrame attachFrame, float mergeRadiusSQ = 0.0025f)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.UpdateOverlappedVertexNormals(base.Pointer, attachedToMesh.Pointer, ref attachFrame, mergeRadiusSQ);
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x0000623B File Offset: 0x0000443B
		public void ClearAll()
		{
			EngineApplicationInterface.IManagedMeshEditOperations.ClearAll(base.Pointer);
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x0000624D File Offset: 0x0000444D
		public void SetTangentsOfFaceCorner(int faceCornerIndex, Vec3 tangent, Vec3 binormal)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.SetTangentsOfFaceCorner(base.Pointer, faceCornerIndex, ref tangent, ref binormal);
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x00006264 File Offset: 0x00004464
		public void SetPositionOfVertex(int vertexIndex, Vec3 position)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.SetPositionOfVertex(base.Pointer, vertexIndex, ref position);
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x00006279 File Offset: 0x00004479
		public Vec3 GetPositionOfVertex(int vertexIndex)
		{
			return EngineApplicationInterface.IManagedMeshEditOperations.GetPositionOfVertex(base.Pointer, vertexIndex);
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0000628C File Offset: 0x0000448C
		public void RemoveFace(int faceIndex)
		{
			EngineApplicationInterface.IManagedMeshEditOperations.RemoveFace(base.Pointer, faceIndex);
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x0000629F File Offset: 0x0000449F
		public void FinalizeEditing()
		{
			EngineApplicationInterface.IManagedMeshEditOperations.FinalizeEditing(base.Pointer);
		}
	}
}
