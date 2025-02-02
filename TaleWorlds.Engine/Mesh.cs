using System;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000065 RID: 101
	[EngineClass("rglMesh")]
	public sealed class Mesh : Resource
	{
		// Token: 0x060007E1 RID: 2017 RVA: 0x0000766E File Offset: 0x0000586E
		internal Mesh(UIntPtr meshPointer) : base(meshPointer)
		{
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00007677 File Offset: 0x00005877
		public static Mesh CreateMeshWithMaterial(Material material)
		{
			return EngineApplicationInterface.IMesh.CreateMeshWithMaterial(material.Pointer);
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00007689 File Offset: 0x00005889
		public static Mesh CreateMesh(bool editable = true)
		{
			return EngineApplicationInterface.IMesh.CreateMesh(editable);
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x00007696 File Offset: 0x00005896
		public Mesh GetBaseMesh()
		{
			return EngineApplicationInterface.IMesh.GetBaseMesh(base.Pointer);
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x000076A8 File Offset: 0x000058A8
		public static Mesh GetFromResource(string meshName)
		{
			return EngineApplicationInterface.IMesh.GetMeshFromResource(meshName);
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x000076B5 File Offset: 0x000058B5
		public static Mesh GetRandomMeshWithVdecl(int inputLayout)
		{
			return EngineApplicationInterface.IMesh.GetRandomMeshWithVdecl(inputLayout);
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x000076C2 File Offset: 0x000058C2
		public void SetColorAndStroke(uint color, uint strokeColor, bool drawStroke)
		{
			this.Color = color;
			this.Color2 = strokeColor;
			EngineApplicationInterface.IMesh.SetColorAndStroke(base.Pointer, drawStroke);
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x000076E3 File Offset: 0x000058E3
		public void SetMeshRenderOrder(int renderOrder)
		{
			EngineApplicationInterface.IMesh.SetMeshRenderOrder(base.Pointer, renderOrder);
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x000076F6 File Offset: 0x000058F6
		public bool HasTag(string str)
		{
			return EngineApplicationInterface.IMesh.HasTag(base.Pointer, str);
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x00007709 File Offset: 0x00005909
		public Mesh CreateCopy()
		{
			return EngineApplicationInterface.IMesh.CreateMeshCopy(base.Pointer);
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x0000771B File Offset: 0x0000591B
		public void SetMaterial(string newMaterialName)
		{
			EngineApplicationInterface.IMesh.SetMaterialByName(base.Pointer, newMaterialName);
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x0000772E File Offset: 0x0000592E
		public void SetVectorArgument(float vectorArgument0, float vectorArgument1, float vectorArgument2, float vectorArgument3)
		{
			EngineApplicationInterface.IMesh.SetVectorArgument(base.Pointer, vectorArgument0, vectorArgument1, vectorArgument2, vectorArgument3);
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x00007745 File Offset: 0x00005945
		public void SetVectorArgument2(float vectorArgument0, float vectorArgument1, float vectorArgument2, float vectorArgument3)
		{
			EngineApplicationInterface.IMesh.SetVectorArgument2(base.Pointer, vectorArgument0, vectorArgument1, vectorArgument2, vectorArgument3);
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x0000775C File Offset: 0x0000595C
		public void SetMaterial(Material material)
		{
			EngineApplicationInterface.IMesh.SetMaterial(base.Pointer, material.Pointer);
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x00007774 File Offset: 0x00005974
		public Material GetMaterial()
		{
			return EngineApplicationInterface.IMesh.GetMaterial(base.Pointer);
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x00007786 File Offset: 0x00005986
		public Material GetSecondMaterial()
		{
			return EngineApplicationInterface.IMesh.GetSecondMaterial(base.Pointer);
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x00007798 File Offset: 0x00005998
		public int AddFaceCorner(Vec3 position, Vec3 normal, Vec2 uvCoord, uint color, UIntPtr lockHandle)
		{
			if (base.IsValid)
			{
				return EngineApplicationInterface.IMesh.AddFaceCorner(base.Pointer, position, normal, uvCoord, color, lockHandle);
			}
			return -1;
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x000077BB File Offset: 0x000059BB
		public int AddFace(int patchNode0, int patchNode1, int patchNode2, UIntPtr lockHandle)
		{
			if (base.IsValid)
			{
				return EngineApplicationInterface.IMesh.AddFace(base.Pointer, patchNode0, patchNode1, patchNode2, lockHandle);
			}
			return -1;
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x000077DC File Offset: 0x000059DC
		public void ClearMesh()
		{
			if (base.IsValid)
			{
				EngineApplicationInterface.IMesh.ClearMesh(base.Pointer);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060007F4 RID: 2036 RVA: 0x000077F6 File Offset: 0x000059F6
		// (set) Token: 0x060007F5 RID: 2037 RVA: 0x00007816 File Offset: 0x00005A16
		public string Name
		{
			get
			{
				if (base.IsValid)
				{
					return EngineApplicationInterface.IMesh.GetName(base.Pointer);
				}
				return string.Empty;
			}
			set
			{
				EngineApplicationInterface.IMesh.SetName(base.Pointer, value);
			}
		}

		// Token: 0x1700004E RID: 78
		// (set) Token: 0x060007F6 RID: 2038 RVA: 0x00007829 File Offset: 0x00005A29
		public MBMeshCullingMode CullingMode
		{
			set
			{
				if (base.IsValid)
				{
					EngineApplicationInterface.IMesh.SetCullingMode(base.Pointer, (uint)value);
				}
			}
		}

		// Token: 0x1700004F RID: 79
		// (set) Token: 0x060007F7 RID: 2039 RVA: 0x00007844 File Offset: 0x00005A44
		public float MorphTime
		{
			set
			{
				if (base.IsValid)
				{
					EngineApplicationInterface.IMesh.SetMorphTime(base.Pointer, value);
				}
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060007F9 RID: 2041 RVA: 0x00007894 File Offset: 0x00005A94
		// (set) Token: 0x060007F8 RID: 2040 RVA: 0x0000785F File Offset: 0x00005A5F
		public uint Color
		{
			get
			{
				return EngineApplicationInterface.IMesh.GetColor(base.Pointer);
			}
			set
			{
				if (base.IsValid)
				{
					EngineApplicationInterface.IMesh.SetColor(base.Pointer, value);
					return;
				}
				Debug.FailedAssert("false", "C:\\Develop\\MB3\\Source\\Engine\\TaleWorlds.Engine\\Mesh.cs", "Color", 301);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060007FB RID: 2043 RVA: 0x000078DB File Offset: 0x00005ADB
		// (set) Token: 0x060007FA RID: 2042 RVA: 0x000078A6 File Offset: 0x00005AA6
		public uint Color2
		{
			get
			{
				return EngineApplicationInterface.IMesh.GetColor2(base.Pointer);
			}
			set
			{
				if (base.IsValid)
				{
					EngineApplicationInterface.IMesh.SetColor2(base.Pointer, value);
					return;
				}
				Debug.FailedAssert("false", "C:\\Develop\\MB3\\Source\\Engine\\TaleWorlds.Engine\\Mesh.cs", "Color2", 324);
			}
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x000078ED File Offset: 0x00005AED
		public void SetColorAlpha(uint newAlpha)
		{
			if (base.IsValid)
			{
				EngineApplicationInterface.IMesh.SetColorAlpha(base.Pointer, newAlpha);
			}
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x00007908 File Offset: 0x00005B08
		public uint GetFaceCount()
		{
			if (!base.IsValid)
			{
				return 0U;
			}
			return EngineApplicationInterface.IMesh.GetFaceCount(base.Pointer);
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x00007924 File Offset: 0x00005B24
		public uint GetFaceCornerCount()
		{
			if (!base.IsValid)
			{
				return 0U;
			}
			return EngineApplicationInterface.IMesh.GetFaceCornerCount(base.Pointer);
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x00007940 File Offset: 0x00005B40
		public void ComputeNormals()
		{
			if (base.IsValid)
			{
				EngineApplicationInterface.IMesh.ComputeNormals(base.Pointer);
			}
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x0000795A File Offset: 0x00005B5A
		public void ComputeTangents()
		{
			if (base.IsValid)
			{
				EngineApplicationInterface.IMesh.ComputeTangents(base.Pointer);
			}
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x00007974 File Offset: 0x00005B74
		public void AddMesh(string meshResourceName, MatrixFrame meshFrame)
		{
			if (base.IsValid)
			{
				Mesh fromResource = Mesh.GetFromResource(meshResourceName);
				EngineApplicationInterface.IMesh.AddMeshToMesh(base.Pointer, fromResource.Pointer, ref meshFrame);
			}
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x000079A8 File Offset: 0x00005BA8
		public void AddMesh(Mesh mesh, MatrixFrame meshFrame)
		{
			if (base.IsValid)
			{
				EngineApplicationInterface.IMesh.AddMeshToMesh(base.Pointer, mesh.Pointer, ref meshFrame);
			}
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x000079CC File Offset: 0x00005BCC
		public MatrixFrame GetLocalFrame()
		{
			if (base.IsValid)
			{
				MatrixFrame result = default(MatrixFrame);
				EngineApplicationInterface.IMesh.GetLocalFrame(base.Pointer, ref result);
				return result;
			}
			return default(MatrixFrame);
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x00007A06 File Offset: 0x00005C06
		public void SetLocalFrame(MatrixFrame meshFrame)
		{
			if (base.IsValid)
			{
				EngineApplicationInterface.IMesh.SetLocalFrame(base.Pointer, ref meshFrame);
			}
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x00007A22 File Offset: 0x00005C22
		public void SetVisibilityMask(VisibilityMaskFlags visibilityMask)
		{
			EngineApplicationInterface.IMesh.SetVisibilityMask(base.Pointer, visibilityMask);
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x00007A35 File Offset: 0x00005C35
		public void UpdateBoundingBox()
		{
			if (base.IsValid)
			{
				EngineApplicationInterface.IMesh.UpdateBoundingBox(base.Pointer);
			}
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x00007A4F File Offset: 0x00005C4F
		public void SetAsNotEffectedBySeason()
		{
			EngineApplicationInterface.IMesh.SetAsNotEffectedBySeason(base.Pointer);
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x00007A61 File Offset: 0x00005C61
		public float GetBoundingBoxWidth()
		{
			if (!base.IsValid)
			{
				return 0f;
			}
			return EngineApplicationInterface.IMesh.GetBoundingBoxWidth(base.Pointer);
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x00007A81 File Offset: 0x00005C81
		public float GetBoundingBoxHeight()
		{
			if (!base.IsValid)
			{
				return 0f;
			}
			return EngineApplicationInterface.IMesh.GetBoundingBoxHeight(base.Pointer);
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x00007AA1 File Offset: 0x00005CA1
		public Vec3 GetBoundingBoxMin()
		{
			return EngineApplicationInterface.IMesh.GetBoundingBoxMin(base.Pointer);
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x00007AB3 File Offset: 0x00005CB3
		public Vec3 GetBoundingBoxMax()
		{
			return EngineApplicationInterface.IMesh.GetBoundingBoxMax(base.Pointer);
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x00007AC8 File Offset: 0x00005CC8
		public void AddTriangle(Vec3 p1, Vec3 p2, Vec3 p3, Vec2 uv1, Vec2 uv2, Vec2 uv3, uint color, UIntPtr lockHandle)
		{
			EngineApplicationInterface.IMesh.AddTriangle(base.Pointer, p1, p2, p3, uv1, uv2, uv3, color, lockHandle);
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x00007AF4 File Offset: 0x00005CF4
		public void AddTriangleWithVertexColors(Vec3 p1, Vec3 p2, Vec3 p3, Vec2 uv1, Vec2 uv2, Vec2 uv3, uint c1, uint c2, uint c3, UIntPtr lockHandle)
		{
			EngineApplicationInterface.IMesh.AddTriangleWithVertexColors(base.Pointer, p1, p2, p3, uv1, uv2, uv3, c1, c2, c3, lockHandle);
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x00007B22 File Offset: 0x00005D22
		public void HintIndicesDynamic()
		{
			EngineApplicationInterface.IMesh.HintIndicesDynamic(base.Pointer);
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x00007B34 File Offset: 0x00005D34
		public void HintVerticesDynamic()
		{
			EngineApplicationInterface.IMesh.HintVerticesDynamic(base.Pointer);
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x00007B46 File Offset: 0x00005D46
		public void RecomputeBoundingBox()
		{
			EngineApplicationInterface.IMesh.RecomputeBoundingBox(base.Pointer);
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000811 RID: 2065 RVA: 0x00007B58 File Offset: 0x00005D58
		// (set) Token: 0x06000812 RID: 2066 RVA: 0x00007B6A File Offset: 0x00005D6A
		public BillboardType Billboard
		{
			get
			{
				return EngineApplicationInterface.IMesh.GetBillboard(base.Pointer);
			}
			set
			{
				EngineApplicationInterface.IMesh.SetBillboard(base.Pointer, value);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000813 RID: 2067 RVA: 0x00007B7D File Offset: 0x00005D7D
		// (set) Token: 0x06000814 RID: 2068 RVA: 0x00007B8F File Offset: 0x00005D8F
		public VisibilityMaskFlags VisibilityMask
		{
			get
			{
				return EngineApplicationInterface.IMesh.GetVisibilityMask(base.Pointer);
			}
			set
			{
				EngineApplicationInterface.IMesh.SetVisibilityMask(base.Pointer, value);
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000815 RID: 2069 RVA: 0x00007BA2 File Offset: 0x00005DA2
		public int EditDataFaceCornerCount
		{
			get
			{
				return EngineApplicationInterface.IMesh.GetEditDataFaceCornerCount(base.Pointer);
			}
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x00007BB4 File Offset: 0x00005DB4
		public void SetEditDataFaceCornerVertexColor(int index, uint color)
		{
			EngineApplicationInterface.IMesh.SetEditDataFaceCornerVertexColor(base.Pointer, index, color);
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x00007BC8 File Offset: 0x00005DC8
		public uint GetEditDataFaceCornerVertexColor(int index)
		{
			return EngineApplicationInterface.IMesh.GetEditDataFaceCornerVertexColor(base.Pointer, index);
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x00007BDB File Offset: 0x00005DDB
		public void PreloadForRendering()
		{
			EngineApplicationInterface.IMesh.PreloadForRendering(base.Pointer);
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x00007BED File Offset: 0x00005DED
		public void SetContourColor(Vec3 color, bool alwaysVisible, bool maskMesh)
		{
			EngineApplicationInterface.IMesh.SetContourColor(base.Pointer, color, alwaysVisible, maskMesh);
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x00007C02 File Offset: 0x00005E02
		public void DisableContour()
		{
			EngineApplicationInterface.IMesh.DisableContour(base.Pointer);
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x00007C14 File Offset: 0x00005E14
		public void SetExternalBoundingBox(BoundingBox bbox)
		{
			EngineApplicationInterface.IMesh.SetExternalBoundingBox(base.Pointer, ref bbox);
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x00007C28 File Offset: 0x00005E28
		public void AddEditDataUser()
		{
			EngineApplicationInterface.IMesh.AddEditDataUser(base.Pointer);
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x00007C3A File Offset: 0x00005E3A
		public void ReleaseEditDataUser()
		{
			EngineApplicationInterface.IMesh.ReleaseEditDataUser(base.Pointer);
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x00007C4C File Offset: 0x00005E4C
		public void SetEditDataPolicy(EditDataPolicy policy)
		{
			EngineApplicationInterface.IMesh.SetEditDataPolicy(base.Pointer, policy);
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x00007C5F File Offset: 0x00005E5F
		public UIntPtr LockEditDataWrite()
		{
			return EngineApplicationInterface.IMesh.LockEditDataWrite(base.Pointer);
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x00007C71 File Offset: 0x00005E71
		public void UnlockEditDataWrite(UIntPtr handle)
		{
			EngineApplicationInterface.IMesh.UnlockEditDataWrite(base.Pointer, handle);
		}
	}
}
