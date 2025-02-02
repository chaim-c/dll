using System;
using TaleWorlds.DotNet;

namespace TaleWorlds.Engine
{
	// Token: 0x02000091 RID: 145
	[EngineClass("rglTwo_dimension_view")]
	public sealed class TwoDimensionView : View
	{
		// Token: 0x06000AEC RID: 2796 RVA: 0x0000BFF9 File Offset: 0x0000A1F9
		internal TwoDimensionView(UIntPtr pointer) : base(pointer)
		{
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0000C002 File Offset: 0x0000A202
		public static TwoDimensionView CreateTwoDimension()
		{
			return EngineApplicationInterface.ITwoDimensionView.CreateTwoDimensionView();
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x0000C00E File Offset: 0x0000A20E
		public void BeginFrame()
		{
			EngineApplicationInterface.ITwoDimensionView.BeginFrame(base.Pointer);
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x0000C020 File Offset: 0x0000A220
		public void EndFrame()
		{
			EngineApplicationInterface.ITwoDimensionView.EndFrame(base.Pointer);
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x0000C032 File Offset: 0x0000A232
		public void Clear()
		{
			EngineApplicationInterface.ITwoDimensionView.Clear(base.Pointer);
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x0000C044 File Offset: 0x0000A244
		public void CreateMeshFromDescription(float[] vertices, float[] uvs, uint[] indices, int indexCount, Material material, TwoDimensionMeshDrawData meshDrawData)
		{
			EngineApplicationInterface.ITwoDimensionView.AddNewMesh(base.Pointer, vertices, uvs, indices, vertices.Length / 2, indexCount, material.Pointer, ref meshDrawData);
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x0000C074 File Offset: 0x0000A274
		public void CreateMeshFromDescription(Material material, TwoDimensionMeshDrawData meshDrawData)
		{
			EngineApplicationInterface.ITwoDimensionView.AddNewQuadMesh(base.Pointer, material.Pointer, ref meshDrawData);
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x0000C08E File Offset: 0x0000A28E
		public bool CreateTextMeshFromCache(Material material, TwoDimensionTextMeshDrawData meshDrawData)
		{
			return EngineApplicationInterface.ITwoDimensionView.AddCachedTextMesh(base.Pointer, material.Pointer, ref meshDrawData);
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x0000C0A8 File Offset: 0x0000A2A8
		public void CreateTextMeshFromDescription(float[] vertices, float[] uvs, uint[] indices, int indexCount, Material material, TwoDimensionTextMeshDrawData meshDrawData)
		{
			EngineApplicationInterface.ITwoDimensionView.AddNewTextMesh(base.Pointer, vertices, uvs, indices, vertices.Length / 2, indexCount, material.Pointer, ref meshDrawData);
		}
	}
}
