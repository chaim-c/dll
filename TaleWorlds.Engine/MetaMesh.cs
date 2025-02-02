using System;
using System.Collections.Generic;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000068 RID: 104
	[EngineClass("rglMeta_mesh")]
	public sealed class MetaMesh : GameEntityComponent
	{
		// Token: 0x0600082E RID: 2094 RVA: 0x00007F86 File Offset: 0x00006186
		internal MetaMesh(UIntPtr pointer) : base(pointer)
		{
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x00007F8F File Offset: 0x0000618F
		public static MetaMesh CreateMetaMesh(string name = null)
		{
			return EngineApplicationInterface.IMetaMesh.CreateMetaMesh(name);
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000830 RID: 2096 RVA: 0x00007F9C File Offset: 0x0000619C
		public bool IsValid
		{
			get
			{
				return base.Pointer != UIntPtr.Zero;
			}
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00007FAE File Offset: 0x000061AE
		public int GetLodMaskForMeshAtIndex(int index)
		{
			return EngineApplicationInterface.IMetaMesh.GetLodMaskForMeshAtIndex(base.Pointer, index);
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x00007FC1 File Offset: 0x000061C1
		public int GetTotalGpuSize()
		{
			return EngineApplicationInterface.IMetaMesh.GetTotalGpuSize(base.Pointer);
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00007FD3 File Offset: 0x000061D3
		public int RemoveMeshesWithTag(string tag)
		{
			return EngineApplicationInterface.IMetaMesh.RemoveMeshesWithTag(base.Pointer, tag);
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x00007FE6 File Offset: 0x000061E6
		public int RemoveMeshesWithoutTag(string tag)
		{
			return EngineApplicationInterface.IMetaMesh.RemoveMeshesWithoutTag(base.Pointer, tag);
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x00007FF9 File Offset: 0x000061F9
		public int GetMeshCountWithTag(string tag)
		{
			return EngineApplicationInterface.IMetaMesh.GetMeshCountWithTag(base.Pointer, tag);
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x0000800C File Offset: 0x0000620C
		public bool HasVertexBufferOrEditDataOrPackageItem()
		{
			return EngineApplicationInterface.IMetaMesh.HasVertexBufferOrEditDataOrPackageItem(base.Pointer);
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x0000801E File Offset: 0x0000621E
		public bool HasAnyGeneratedLods()
		{
			return EngineApplicationInterface.IMetaMesh.HasAnyGeneratedLods(base.Pointer);
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00008030 File Offset: 0x00006230
		public bool HasAnyLods()
		{
			return EngineApplicationInterface.IMetaMesh.HasAnyLods(base.Pointer);
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00008042 File Offset: 0x00006242
		public static MetaMesh GetCopy(string metaMeshName, bool showErrors = true, bool mayReturnNull = false)
		{
			return EngineApplicationInterface.IMetaMesh.CreateCopyFromName(metaMeshName, showErrors, mayReturnNull);
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x00008051 File Offset: 0x00006251
		public void CopyTo(MetaMesh res, bool copyMeshes = true)
		{
			EngineApplicationInterface.IMetaMesh.CopyTo(base.Pointer, res.Pointer, copyMeshes);
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x0000806A File Offset: 0x0000626A
		public void ClearMeshesForOtherLods(int lodToKeep)
		{
			EngineApplicationInterface.IMetaMesh.ClearMeshesForOtherLods(base.Pointer, lodToKeep);
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x0000807D File Offset: 0x0000627D
		public void ClearMeshesForLod(int lodToClear)
		{
			EngineApplicationInterface.IMetaMesh.ClearMeshesForLod(base.Pointer, lodToClear);
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x00008090 File Offset: 0x00006290
		public void ClearMeshesForLowerLods(int lodToClear)
		{
			EngineApplicationInterface.IMetaMesh.ClearMeshesForLowerLods(base.Pointer, lodToClear);
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x000080A3 File Offset: 0x000062A3
		public void ClearMeshes()
		{
			EngineApplicationInterface.IMetaMesh.ClearMeshes(base.Pointer);
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x000080B5 File Offset: 0x000062B5
		public void SetNumLods(int lodToClear)
		{
			EngineApplicationInterface.IMetaMesh.SetNumLods(base.Pointer, lodToClear);
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x000080C8 File Offset: 0x000062C8
		public static void CheckMetaMeshExistence(string metaMeshName, int lod_count_check)
		{
			EngineApplicationInterface.IMetaMesh.CheckMetaMeshExistence(metaMeshName, lod_count_check);
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x000080D6 File Offset: 0x000062D6
		public static MetaMesh GetMorphedCopy(string metaMeshName, float morphTarget, bool showErrors)
		{
			return EngineApplicationInterface.IMetaMesh.GetMorphedCopy(metaMeshName, morphTarget, showErrors);
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x000080E5 File Offset: 0x000062E5
		public MetaMesh CreateCopy()
		{
			return EngineApplicationInterface.IMetaMesh.CreateCopy(base.Pointer);
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x000080F7 File Offset: 0x000062F7
		public void AddMesh(Mesh mesh)
		{
			EngineApplicationInterface.IMetaMesh.AddMesh(base.Pointer, mesh.Pointer, 0U);
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x00008110 File Offset: 0x00006310
		public void AddMesh(Mesh mesh, uint lodLevel)
		{
			EngineApplicationInterface.IMetaMesh.AddMesh(base.Pointer, mesh.Pointer, lodLevel);
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x00008129 File Offset: 0x00006329
		public void AddMetaMesh(MetaMesh metaMesh)
		{
			EngineApplicationInterface.IMetaMesh.AddMetaMesh(base.Pointer, metaMesh.Pointer);
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00008141 File Offset: 0x00006341
		public void SetCullMode(MBMeshCullingMode cullMode)
		{
			EngineApplicationInterface.IMetaMesh.SetCullMode(base.Pointer, cullMode);
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00008154 File Offset: 0x00006354
		public void AddMaterialShaderFlag(string materialShaderFlag)
		{
			for (int i = 0; i < this.MeshCount; i++)
			{
				Mesh meshAtIndex = this.GetMeshAtIndex(i);
				Material material = meshAtIndex.GetMaterial();
				material = material.CreateCopy();
				material.AddMaterialShaderFlag(materialShaderFlag, false);
				meshAtIndex.SetMaterial(material);
			}
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00008195 File Offset: 0x00006395
		public void MergeMultiMeshes(MetaMesh metaMesh)
		{
			EngineApplicationInterface.IMetaMesh.MergeMultiMeshes(base.Pointer, metaMesh.Pointer);
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x000081AD File Offset: 0x000063AD
		public void AssignClothBodyFrom(MetaMesh metaMesh)
		{
			EngineApplicationInterface.IMetaMesh.AssignClothBodyFrom(base.Pointer, metaMesh.Pointer);
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x000081C5 File Offset: 0x000063C5
		public void BatchMultiMeshes(MetaMesh metaMesh)
		{
			EngineApplicationInterface.IMetaMesh.BatchMultiMeshes(base.Pointer, metaMesh.Pointer);
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x000081DD File Offset: 0x000063DD
		public bool HasClothData()
		{
			return EngineApplicationInterface.IMetaMesh.HasClothData(base.Pointer);
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x000081F0 File Offset: 0x000063F0
		public void BatchMultiMeshesMultiple(List<MetaMesh> metaMeshes)
		{
			UIntPtr[] array = new UIntPtr[metaMeshes.Count];
			for (int i = 0; i < metaMeshes.Count; i++)
			{
				array[i] = metaMeshes[i].Pointer;
			}
			EngineApplicationInterface.IMetaMesh.BatchMultiMeshesMultiple(base.Pointer, array, metaMeshes.Count);
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x00008240 File Offset: 0x00006440
		public void ClearEditData()
		{
			EngineApplicationInterface.IMetaMesh.ClearEditData(base.Pointer);
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600084E RID: 2126 RVA: 0x00008252 File Offset: 0x00006452
		public int MeshCount
		{
			get
			{
				return EngineApplicationInterface.IMetaMesh.GetMeshCount(base.Pointer);
			}
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x00008264 File Offset: 0x00006464
		public Mesh GetMeshAtIndex(int meshIndex)
		{
			return EngineApplicationInterface.IMetaMesh.GetMeshAtIndex(base.Pointer, meshIndex);
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x00008278 File Offset: 0x00006478
		public Mesh GetFirstMeshWithTag(string tag)
		{
			for (int i = 0; i < this.MeshCount; i++)
			{
				Mesh meshAtIndex = this.GetMeshAtIndex(i);
				if (meshAtIndex.HasTag(tag))
				{
					return meshAtIndex;
				}
			}
			return null;
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x000082AA File Offset: 0x000064AA
		private void Release()
		{
			EngineApplicationInterface.IMetaMesh.Release(base.Pointer);
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x000082BC File Offset: 0x000064BC
		public uint GetFactor1()
		{
			return EngineApplicationInterface.IMetaMesh.GetFactor1(base.Pointer);
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x000082CE File Offset: 0x000064CE
		public void SetGlossMultiplier(float value)
		{
			EngineApplicationInterface.IMetaMesh.SetGlossMultiplier(base.Pointer, value);
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x000082E1 File Offset: 0x000064E1
		public uint GetFactor2()
		{
			return EngineApplicationInterface.IMetaMesh.GetFactor2(base.Pointer);
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x000082F3 File Offset: 0x000064F3
		public void SetFactor1Linear(uint linearFactorColor1)
		{
			EngineApplicationInterface.IMetaMesh.SetFactor1Linear(base.Pointer, linearFactorColor1);
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x00008306 File Offset: 0x00006506
		public void SetFactor2Linear(uint linearFactorColor2)
		{
			EngineApplicationInterface.IMetaMesh.SetFactor2Linear(base.Pointer, linearFactorColor2);
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x00008319 File Offset: 0x00006519
		public void SetFactor1(uint factorColor1)
		{
			EngineApplicationInterface.IMetaMesh.SetFactor1(base.Pointer, factorColor1);
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x0000832C File Offset: 0x0000652C
		public void SetFactor2(uint factorColor2)
		{
			EngineApplicationInterface.IMetaMesh.SetFactor2(base.Pointer, factorColor2);
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0000833F File Offset: 0x0000653F
		public void SetVectorArgument(float vectorArgument0, float vectorArgument1, float vectorArgument2, float vectorArgument3)
		{
			EngineApplicationInterface.IMetaMesh.SetVectorArgument(base.Pointer, vectorArgument0, vectorArgument1, vectorArgument2, vectorArgument3);
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x00008356 File Offset: 0x00006556
		public void SetVectorArgument2(float vectorArgument0, float vectorArgument1, float vectorArgument2, float vectorArgument3)
		{
			EngineApplicationInterface.IMetaMesh.SetVectorArgument2(base.Pointer, vectorArgument0, vectorArgument1, vectorArgument2, vectorArgument3);
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0000836D File Offset: 0x0000656D
		public Vec3 GetVectorArgument2()
		{
			return EngineApplicationInterface.IMetaMesh.GetVectorArgument2(base.Pointer);
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0000837F File Offset: 0x0000657F
		public void SetMaterial(Material material)
		{
			EngineApplicationInterface.IMetaMesh.SetMaterial(base.Pointer, material.Pointer);
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x00008397 File Offset: 0x00006597
		public void SetLodBias(int lodBias)
		{
			EngineApplicationInterface.IMetaMesh.SetLodBias(base.Pointer, lodBias);
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x000083AA File Offset: 0x000065AA
		public void SetBillboarding(BillboardType billboard)
		{
			EngineApplicationInterface.IMetaMesh.SetBillboarding(base.Pointer, billboard);
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x000083BD File Offset: 0x000065BD
		public void UseHeadBoneFaceGenScaling(Skeleton skeleton, sbyte headLookDirectionBoneIndex, MatrixFrame frame)
		{
			EngineApplicationInterface.IMetaMesh.UseHeadBoneFaceGenScaling(base.Pointer, skeleton.Pointer, headLookDirectionBoneIndex, ref frame);
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x000083D8 File Offset: 0x000065D8
		public void DrawTextWithDefaultFont(string text, Vec2 textPositionMin, Vec2 textPositionMax, Vec2 size, uint color, TextFlags flags)
		{
			EngineApplicationInterface.IMetaMesh.DrawTextWithDefaultFont(base.Pointer, text, textPositionMin, textPositionMax, size, color, flags);
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000861 RID: 2145 RVA: 0x000083F4 File Offset: 0x000065F4
		// (set) Token: 0x06000862 RID: 2146 RVA: 0x0000841C File Offset: 0x0000661C
		public MatrixFrame Frame
		{
			get
			{
				MatrixFrame result = default(MatrixFrame);
				EngineApplicationInterface.IMetaMesh.GetFrame(base.Pointer, ref result);
				return result;
			}
			set
			{
				EngineApplicationInterface.IMetaMesh.SetFrame(base.Pointer, ref value);
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000863 RID: 2147 RVA: 0x00008430 File Offset: 0x00006630
		// (set) Token: 0x06000864 RID: 2148 RVA: 0x00008442 File Offset: 0x00006642
		public Vec3 VectorUserData
		{
			get
			{
				return EngineApplicationInterface.IMetaMesh.GetVectorUserData(base.Pointer);
			}
			set
			{
				EngineApplicationInterface.IMetaMesh.SetVectorUserData(base.Pointer, ref value);
			}
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x00008456 File Offset: 0x00006656
		public void PreloadForRendering()
		{
			EngineApplicationInterface.IMetaMesh.PreloadForRendering(base.Pointer);
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x00008468 File Offset: 0x00006668
		public int CheckResources()
		{
			return EngineApplicationInterface.IMetaMesh.CheckResources(base.Pointer);
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0000847A File Offset: 0x0000667A
		public void PreloadShaders(bool useTableau, bool useTeamColor)
		{
			EngineApplicationInterface.IMetaMesh.PreloadShaders(base.Pointer, useTableau, useTeamColor);
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0000848E File Offset: 0x0000668E
		public void RecomputeBoundingBox(bool recomputeMeshes)
		{
			EngineApplicationInterface.IMetaMesh.RecomputeBoundingBox(base.Pointer, recomputeMeshes);
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x000084A1 File Offset: 0x000066A1
		public void AddEditDataUser()
		{
			EngineApplicationInterface.IMetaMesh.AddEditDataUser(base.Pointer);
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x000084B3 File Offset: 0x000066B3
		public void ReleaseEditDataUser()
		{
			EngineApplicationInterface.IMetaMesh.ReleaseEditDataUser(base.Pointer);
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x000084C5 File Offset: 0x000066C5
		public void SetEditDataPolicy(EditDataPolicy policy)
		{
			EngineApplicationInterface.IMetaMesh.SetEditDataPolicy(base.Pointer, policy);
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x000084D8 File Offset: 0x000066D8
		public MatrixFrame Fit()
		{
			MatrixFrame identity = MatrixFrame.Identity;
			Vec3 vec = new Vec3(1000000f, 1000000f, 1000000f, -1f);
			Vec3 vec2 = new Vec3(-1000000f, -1000000f, -1000000f, -1f);
			for (int num = 0; num != this.MeshCount; num++)
			{
				Vec3 boundingBoxMin = this.GetMeshAtIndex(num).GetBoundingBoxMin();
				Vec3 boundingBoxMax = this.GetMeshAtIndex(num).GetBoundingBoxMax();
				vec = Vec3.Vec3Min(vec, boundingBoxMin);
				vec2 = Vec3.Vec3Max(vec2, boundingBoxMax);
			}
			Vec3 v = (vec + vec2) * 0.5f;
			float num2 = MathF.Max(vec2.x - vec.x, vec2.y - vec.y);
			float num3 = 0.95f / num2;
			identity.origin -= v * num3;
			identity.rotation.ApplyScaleLocal(num3);
			return identity;
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x000085D4 File Offset: 0x000067D4
		public BoundingBox GetBoundingBox()
		{
			BoundingBox result = default(BoundingBox);
			EngineApplicationInterface.IMetaMesh.GetBoundingBox(base.Pointer, ref result);
			return result;
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x000085FC File Offset: 0x000067FC
		public VisibilityMaskFlags GetVisibilityMask()
		{
			return EngineApplicationInterface.IMetaMesh.GetVisibilityMask(base.Pointer);
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0000860E File Offset: 0x0000680E
		public void SetVisibilityMask(VisibilityMaskFlags visibilityMask)
		{
			EngineApplicationInterface.IMetaMesh.SetVisibilityMask(base.Pointer, visibilityMask);
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x00008621 File Offset: 0x00006821
		public string GetName()
		{
			return EngineApplicationInterface.IMetaMesh.GetName(base.Pointer);
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x00008634 File Offset: 0x00006834
		public static void GetAllMultiMeshes(ref List<MetaMesh> multiMeshList)
		{
			int multiMeshCount = EngineApplicationInterface.IMetaMesh.GetMultiMeshCount();
			UIntPtr[] array = new UIntPtr[multiMeshCount];
			EngineApplicationInterface.IMetaMesh.GetAllMultiMeshes(array);
			for (int i = 0; i < multiMeshCount; i++)
			{
				multiMeshList.Add(new MetaMesh(array[i]));
			}
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x0000867A File Offset: 0x0000687A
		public static MetaMesh GetMultiMesh(string name)
		{
			return EngineApplicationInterface.IMetaMesh.GetMultiMesh(name);
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00008687 File Offset: 0x00006887
		public void SetContourState(bool alwaysVisible)
		{
			EngineApplicationInterface.IMetaMesh.SetContourState(base.Pointer, alwaysVisible);
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x0000869A File Offset: 0x0000689A
		public void SetContourColor(uint color)
		{
			EngineApplicationInterface.IMetaMesh.SetContourColor(base.Pointer, color);
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x000086AD File Offset: 0x000068AD
		public void SetMaterialToSubMeshesWithTag(Material bodyMaterial, string tag)
		{
			EngineApplicationInterface.IMetaMesh.SetMaterialToSubMeshesWithTag(base.Pointer, bodyMaterial.Pointer, tag);
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x000086C6 File Offset: 0x000068C6
		public void SetFactorColorToSubMeshesWithTag(uint color, string tag)
		{
			EngineApplicationInterface.IMetaMesh.SetFactorColorToSubMeshesWithTag(base.Pointer, color, tag);
		}
	}
}
