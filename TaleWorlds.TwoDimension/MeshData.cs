using System;

namespace TaleWorlds.TwoDimension
{
	// Token: 0x0200001C RID: 28
	[Serializable]
	internal struct MeshData
	{
		// Token: 0x040000AD RID: 173
		public MeshTopology Topology;

		// Token: 0x040000AE RID: 174
		public float[] Vertices;

		// Token: 0x040000AF RID: 175
		public float[] TextureCoordinates;

		// Token: 0x040000B0 RID: 176
		public uint[] Indices;

		// Token: 0x040000B1 RID: 177
		public int VertexCount;
	}
}
