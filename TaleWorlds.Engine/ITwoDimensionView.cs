using System;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000035 RID: 53
	[ApplicationInterfaceBase]
	internal interface ITwoDimensionView
	{
		// Token: 0x0600047C RID: 1148
		[EngineMethod("create_twodimension_view", false)]
		TwoDimensionView CreateTwoDimensionView();

		// Token: 0x0600047D RID: 1149
		[EngineMethod("begin_frame", false)]
		void BeginFrame(UIntPtr pointer);

		// Token: 0x0600047E RID: 1150
		[EngineMethod("end_frame", false)]
		void EndFrame(UIntPtr pointer);

		// Token: 0x0600047F RID: 1151
		[EngineMethod("clear", false)]
		void Clear(UIntPtr pointer);

		// Token: 0x06000480 RID: 1152
		[EngineMethod("add_new_mesh", false)]
		void AddNewMesh(UIntPtr pointer, float[] vertices, float[] uvs, uint[] indices, int vertexCount, int indexCount, UIntPtr material, ref TwoDimensionMeshDrawData meshDrawData);

		// Token: 0x06000481 RID: 1153
		[EngineMethod("add_new_quad_mesh", false)]
		void AddNewQuadMesh(UIntPtr pointer, UIntPtr material, ref TwoDimensionMeshDrawData meshDrawData);

		// Token: 0x06000482 RID: 1154
		[EngineMethod("add_cached_text_mesh", false)]
		bool AddCachedTextMesh(UIntPtr pointer, UIntPtr material, ref TwoDimensionTextMeshDrawData meshDrawData);

		// Token: 0x06000483 RID: 1155
		[EngineMethod("add_new_text_mesh", false)]
		void AddNewTextMesh(UIntPtr pointer, float[] vertices, float[] uvs, uint[] indices, int vertexCount, int indexCount, UIntPtr material, ref TwoDimensionTextMeshDrawData meshDrawData);
	}
}
