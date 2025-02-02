using System;
using System.Collections.Generic;
using System.Numerics;
using TaleWorlds.Library;

namespace TaleWorlds.TwoDimension
{
	// Token: 0x0200001B RID: 27
	public sealed class DrawObject2D
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000101 RID: 257 RVA: 0x000064D2 File Offset: 0x000046D2
		// (set) Token: 0x06000102 RID: 258 RVA: 0x000064DA File Offset: 0x000046DA
		public MeshTopology Topology { get; private set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000103 RID: 259 RVA: 0x000064E3 File Offset: 0x000046E3
		// (set) Token: 0x06000104 RID: 260 RVA: 0x000064EB File Offset: 0x000046EB
		public float[] Vertices { get; private set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000105 RID: 261 RVA: 0x000064F4 File Offset: 0x000046F4
		// (set) Token: 0x06000106 RID: 262 RVA: 0x000064FC File Offset: 0x000046FC
		public float[] TextureCoordinates { get; private set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00006505 File Offset: 0x00004705
		// (set) Token: 0x06000108 RID: 264 RVA: 0x0000650D File Offset: 0x0000470D
		public uint[] Indices { get; private set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00006516 File Offset: 0x00004716
		// (set) Token: 0x0600010A RID: 266 RVA: 0x0000651E File Offset: 0x0000471E
		public int VertexCount { get; set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00006527 File Offset: 0x00004727
		// (set) Token: 0x0600010C RID: 268 RVA: 0x0000652F File Offset: 0x0000472F
		public ulong HashCode1 { get; private set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00006538 File Offset: 0x00004738
		// (set) Token: 0x0600010E RID: 270 RVA: 0x00006540 File Offset: 0x00004740
		public ulong HashCode2 { get; private set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00006549 File Offset: 0x00004749
		// (set) Token: 0x06000110 RID: 272 RVA: 0x00006551 File Offset: 0x00004751
		public Rectangle BoundingRectangle { get; private set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000111 RID: 273 RVA: 0x0000655A File Offset: 0x0000475A
		// (set) Token: 0x06000112 RID: 274 RVA: 0x00006562 File Offset: 0x00004762
		public DrawObjectType DrawObjectType { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000113 RID: 275 RVA: 0x0000656B File Offset: 0x0000476B
		// (set) Token: 0x06000114 RID: 276 RVA: 0x00006573 File Offset: 0x00004773
		public float Width { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000115 RID: 277 RVA: 0x0000657C File Offset: 0x0000477C
		// (set) Token: 0x06000116 RID: 278 RVA: 0x00006584 File Offset: 0x00004784
		public float Height { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000117 RID: 279 RVA: 0x0000658D File Offset: 0x0000478D
		// (set) Token: 0x06000118 RID: 280 RVA: 0x00006595 File Offset: 0x00004795
		public float MinU { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000119 RID: 281 RVA: 0x0000659E File Offset: 0x0000479E
		// (set) Token: 0x0600011A RID: 282 RVA: 0x000065A6 File Offset: 0x000047A6
		public float MinV { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600011B RID: 283 RVA: 0x000065AF File Offset: 0x000047AF
		// (set) Token: 0x0600011C RID: 284 RVA: 0x000065B7 File Offset: 0x000047B7
		public float MaxU { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600011D RID: 285 RVA: 0x000065C0 File Offset: 0x000047C0
		// (set) Token: 0x0600011E RID: 286 RVA: 0x000065C8 File Offset: 0x000047C8
		public float MaxV { get; set; }

		// Token: 0x0600011F RID: 287 RVA: 0x000065D4 File Offset: 0x000047D4
		static DrawObject2D()
		{
			for (int i = 0; i < 16; i++)
			{
				Vector2 item = default(Vector2);
				float num = (float)i;
				num *= 22.5f;
				item.X = Mathf.Cos(num * 0.017453292f);
				item.Y = Mathf.Sin(num * 0.017453292f);
				DrawObject2D._referenceCirclePoints.Add(item);
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00006660 File Offset: 0x00004860
		public DrawObject2D(MeshTopology topology, float[] vertices, float[] uvs, uint[] indices, int vertexCount)
		{
			this.Topology = topology;
			this.Vertices = vertices;
			this.TextureCoordinates = uvs;
			this.Indices = indices;
			this.VertexCount = vertexCount;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000668D File Offset: 0x0000488D
		public DrawObject2D(MeshTopology topology, int vertexCount)
		{
			this.Topology = topology;
			this.Vertices = new float[vertexCount * 2];
			this.TextureCoordinates = new float[vertexCount * 2];
			this.Indices = new uint[vertexCount];
			this.VertexCount = vertexCount;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000066CB File Offset: 0x000048CB
		public void SetVertexAt(int index, Vector2 vertex)
		{
			this.Vertices[2 * index] = vertex.X;
			this.Vertices[2 * index + 1] = vertex.Y;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000066F0 File Offset: 0x000048F0
		public static DrawObject2D CreateTriangleTopologyMeshWithPolygonCoordinates(List<Vector2> vertices)
		{
			int num = 3 * (vertices.Count - 2);
			float[] array = new float[num * 2];
			float[] uvs = new float[num * 2];
			uint[] array2 = new uint[num];
			for (int i = 0; i < num / 3; i++)
			{
				array[6 * i] = vertices[0].X;
				array[6 * i + 1] = vertices[0].Y;
				array[6 * i + 2] = vertices[i + 1].X;
				array[6 * i + 3] = vertices[i + 1].Y;
				array[6 * i + 4] = vertices[i + 2].X;
				array[6 * i + 5] = vertices[i + 2].Y;
			}
			uint num2 = 0U;
			while ((ulong)num2 < (ulong)((long)num))
			{
				array2[(int)num2] = num2;
				num2 += 1U;
			}
			return new DrawObject2D(MeshTopology.Triangles, array, uvs, array2, num);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000067E0 File Offset: 0x000049E0
		public static DrawObject2D CreateLineTopologyMeshWithPolygonCoordinates(List<Vector2> vertices)
		{
			int num = 2 * vertices.Count;
			float[] array = new float[num * 2];
			float[] uvs = new float[num * 2];
			uint[] indices = new uint[num];
			DrawObject2D.FillLineTopologyMeshWithPolygonCoordinates(array, indices, vertices);
			return new DrawObject2D(MeshTopology.Lines, array, uvs, indices, num);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00006824 File Offset: 0x00004A24
		private static void FillLineTopologyMeshWithPolygonCoordinates(float[] lineTopologyVertices, uint[] indices, List<Vector2> vertices)
		{
			for (int i = 0; i < vertices.Count; i++)
			{
				int index = i;
				int index2 = (i + 1 == vertices.Count) ? 0 : (i + 1);
				lineTopologyVertices[i * 4] = vertices[index].X;
				lineTopologyVertices[i * 4 + 1] = vertices[index].Y;
				lineTopologyVertices[i * 4 + 2] = vertices[index2].X;
				lineTopologyVertices[i * 4 + 3] = vertices[index2].Y;
				indices[i] = (uint)i;
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000068A4 File Offset: 0x00004AA4
		public static DrawObject2D CreateLineTopologyMeshWithQuadVertices(float[] quadVertices, uint[] indices, int vertexCount)
		{
			float[] array = new float[vertexCount * 2 * 2];
			float[] uvs = new float[vertexCount * 2 * 2];
			DrawObject2D.QuadVerticesToLineVertices(quadVertices, vertexCount, array);
			return new DrawObject2D(MeshTopology.Lines, array, uvs, indices, vertexCount);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x000068DC File Offset: 0x00004ADC
		public static void QuadVerticesToLineVertices(float[] quadVertices, int vertexCount, float[] lineVertices)
		{
			for (int i = 0; i < vertexCount; i++)
			{
				int num = 2 * i;
				int num2 = (i + 1 == vertexCount) ? 0 : (2 * (i + 1));
				lineVertices[i * 4] = quadVertices[num];
				lineVertices[i * 4 + 1] = quadVertices[num + 1];
				lineVertices[i * 4 + 2] = quadVertices[num2];
				lineVertices[i * 4 + 3] = quadVertices[num2 + 1];
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00006934 File Offset: 0x00004B34
		public static DrawObject2D CreateTriangleTopologyMeshWithCircleRadius(float radius)
		{
			DrawObject2D._circlePolygonPoints.Clear();
			for (int i = 0; i < DrawObject2D._referenceCirclePoints.Count; i++)
			{
				Vector2 item = DrawObject2D._referenceCirclePoints[i];
				item.X *= radius;
				item.Y *= radius;
				DrawObject2D._circlePolygonPoints.Add(item);
			}
			return DrawObject2D.CreateTriangleTopologyMeshWithPolygonCoordinates(DrawObject2D._circlePolygonPoints);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000699C File Offset: 0x00004B9C
		public static DrawObject2D CreateLineTopologyMeshWithCircleRadius(float radius)
		{
			DrawObject2D._circlePolygonPoints.Clear();
			for (int i = 0; i < DrawObject2D._referenceCirclePoints.Count; i++)
			{
				Vector2 item = DrawObject2D._referenceCirclePoints[i];
				item.X *= radius;
				item.Y *= radius;
				DrawObject2D._circlePolygonPoints.Add(item);
			}
			int num = 2 * DrawObject2D._circlePolygonPoints.Count + 2;
			float[] array = new float[num * 2];
			float[] uvs = new float[num * 2];
			uint[] indices = new uint[num];
			DrawObject2D.FillLineTopologyMeshWithPolygonCoordinates(array, indices, DrawObject2D._circlePolygonPoints);
			Vector2 vector = new Vector2(1f, 0f);
			vector.X *= radius;
			vector.Y *= radius;
			array[array.Length - 4] = 0f;
			array[array.Length - 3] = 0f;
			array[array.Length - 2] = vector.X;
			array[array.Length - 1] = vector.Y;
			return new DrawObject2D(MeshTopology.Lines, array, uvs, indices, num);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00006A9C File Offset: 0x00004C9C
		public void RecalculateProperties()
		{
			ulong hashCode;
			ulong hashCode2;
			this.ConvertToHashInPlace(out hashCode, out hashCode2);
			this.HashCode1 = hashCode;
			this.HashCode2 = hashCode2;
			float num = float.MaxValue;
			float num2 = float.MaxValue;
			float num3 = float.MinValue;
			float num4 = float.MinValue;
			for (int i = 0; i < this.VertexCount; i++)
			{
				float num5 = this.Vertices[2 * i];
				float num6 = this.Vertices[2 * i + 1];
				if (num5 < num)
				{
					num = num5;
				}
				if (num6 < num2)
				{
					num2 = num6;
				}
				if (num5 > num3)
				{
					num3 = num5;
				}
				if (num6 > num4)
				{
					num4 = num6;
				}
			}
			this.BoundingRectangle = new Rectangle(num, num2, num3 - num, num4 - num2);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00006B48 File Offset: 0x00004D48
		public byte[] AsByteArray()
		{
			return Common.SerializeObject(new MeshData
			{
				Topology = this.Topology,
				Vertices = this.Vertices,
				TextureCoordinates = this.TextureCoordinates,
				Indices = this.Indices,
				VertexCount = this.VertexCount
			});
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00006BAC File Offset: 0x00004DAC
		public void ConvertToHashInPlace(out ulong hash1, out ulong hash2)
		{
			ulong num = 5381UL;
			ulong num2 = 5381UL;
			int num3 = this.Vertices.Length / 2;
			int num4 = this.TextureCoordinates.Length / 2;
			int num5 = this.Indices.Length / 2;
			for (int i = 0; i < num3; i++)
			{
				Buffer.BlockCopy(this.Vertices, i * 4, DrawObject2D.floatTemporaryHolder, 0, 4);
				num = (num << 5) + num + (ulong)DrawObject2D.floatTemporaryHolder[0];
				num = (num << 5) + num + (ulong)DrawObject2D.floatTemporaryHolder[1];
				num = (num << 5) + num + (ulong)DrawObject2D.floatTemporaryHolder[2];
				num = (num << 5) + num + (ulong)DrawObject2D.floatTemporaryHolder[3];
			}
			for (int j = num3; j < this.Vertices.Length; j++)
			{
				Buffer.BlockCopy(this.Vertices, j * 4, DrawObject2D.floatTemporaryHolder, 0, 4);
				num2 = (num2 << 5) + num2 + (ulong)DrawObject2D.floatTemporaryHolder[0];
				num2 = (num2 << 5) + num2 + (ulong)DrawObject2D.floatTemporaryHolder[1];
				num2 = (num2 << 5) + num2 + (ulong)DrawObject2D.floatTemporaryHolder[2];
				num2 = (num2 << 5) + num2 + (ulong)DrawObject2D.floatTemporaryHolder[3];
			}
			for (int k = 0; k < num4; k++)
			{
				Buffer.BlockCopy(this.TextureCoordinates, k * 4, DrawObject2D.floatTemporaryHolder, 0, 4);
				num = (num << 5) + num + (ulong)DrawObject2D.floatTemporaryHolder[0];
				num = (num << 5) + num + (ulong)DrawObject2D.floatTemporaryHolder[1];
				num = (num << 5) + num + (ulong)DrawObject2D.floatTemporaryHolder[2];
				num = (num << 5) + num + (ulong)DrawObject2D.floatTemporaryHolder[3];
			}
			for (int l = num4; l < this.TextureCoordinates.Length; l++)
			{
				Buffer.BlockCopy(this.TextureCoordinates, l * 4, DrawObject2D.floatTemporaryHolder, 0, 4);
				num2 = (num2 << 5) + num2 + (ulong)DrawObject2D.floatTemporaryHolder[0];
				num2 = (num2 << 5) + num2 + (ulong)DrawObject2D.floatTemporaryHolder[1];
				num2 = (num2 << 5) + num2 + (ulong)DrawObject2D.floatTemporaryHolder[2];
				num2 = (num2 << 5) + num2 + (ulong)DrawObject2D.floatTemporaryHolder[3];
			}
			for (int m = 0; m < num5; m++)
			{
				Buffer.BlockCopy(this.Indices, m * 4, DrawObject2D.floatTemporaryHolder, 0, 4);
				num = (num << 5) + num + (ulong)DrawObject2D.floatTemporaryHolder[0];
				num = (num << 5) + num + (ulong)DrawObject2D.floatTemporaryHolder[1];
				num = (num << 5) + num + (ulong)DrawObject2D.floatTemporaryHolder[2];
				num = (num << 5) + num + (ulong)DrawObject2D.floatTemporaryHolder[3];
			}
			for (int n = num5; n < this.Indices.Length; n++)
			{
				Buffer.BlockCopy(this.Indices, n * 4, DrawObject2D.floatTemporaryHolder, 0, 4);
				num2 = (num2 << 5) + num2 + (ulong)DrawObject2D.floatTemporaryHolder[0];
				num2 = (num2 << 5) + num2 + (ulong)DrawObject2D.floatTemporaryHolder[1];
				num2 = (num2 << 5) + num2 + (ulong)DrawObject2D.floatTemporaryHolder[2];
				num2 = (num2 << 5) + num2 + (ulong)DrawObject2D.floatTemporaryHolder[3];
			}
			num = (num << 5) + num + (ulong)((byte)this.Topology);
			DrawObject2D.uintTemporaryHolder[0] = (uint)this.VertexCount;
			Buffer.BlockCopy(DrawObject2D.uintTemporaryHolder, 0, DrawObject2D.floatTemporaryHolder, 0, 4);
			num2 = (num2 << 5) + num2 + (ulong)DrawObject2D.floatTemporaryHolder[0];
			num2 = (num2 << 5) + num2 + (ulong)DrawObject2D.floatTemporaryHolder[1];
			num2 = (num2 << 5) + num2 + (ulong)DrawObject2D.floatTemporaryHolder[2];
			num2 = (num2 << 5) + num2 + (ulong)DrawObject2D.floatTemporaryHolder[3];
			hash1 = num;
			hash2 = num2;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00006EBC File Offset: 0x000050BC
		public static DrawObject2D CreateQuad(Vector2 size)
		{
			DrawObject2D drawObject2D = DrawObject2D.CreateTriangleTopologyMeshWithPolygonCoordinates(new List<Vector2>
			{
				new Vector2(0f, 0f),
				new Vector2(0f, size.Y),
				new Vector2(size.X, size.Y),
				new Vector2(size.X, 0f)
			});
			drawObject2D.DrawObjectType = DrawObjectType.Quad;
			drawObject2D.TextureCoordinates[0] = 0f;
			drawObject2D.TextureCoordinates[1] = 0f;
			drawObject2D.TextureCoordinates[2] = 0f;
			drawObject2D.TextureCoordinates[3] = 1f;
			drawObject2D.TextureCoordinates[4] = 1f;
			drawObject2D.TextureCoordinates[5] = 1f;
			drawObject2D.TextureCoordinates[6] = 0f;
			drawObject2D.TextureCoordinates[7] = 0f;
			drawObject2D.TextureCoordinates[8] = 1f;
			drawObject2D.TextureCoordinates[9] = 1f;
			drawObject2D.TextureCoordinates[10] = 1f;
			drawObject2D.TextureCoordinates[11] = 0f;
			drawObject2D.Width = size.X;
			drawObject2D.Height = size.Y;
			drawObject2D.MinU = 0f;
			drawObject2D.MaxU = 1f;
			drawObject2D.MinV = 0f;
			drawObject2D.MaxV = 1f;
			return drawObject2D;
		}

		// Token: 0x0400009A RID: 154
		private static byte[] floatTemporaryHolder = new byte[4];

		// Token: 0x0400009B RID: 155
		private static uint[] uintTemporaryHolder = new uint[1];

		// Token: 0x040000A4 RID: 164
		private static List<Vector2> _referenceCirclePoints = new List<Vector2>(64);

		// Token: 0x040000A5 RID: 165
		private static List<Vector2> _circlePolygonPoints = new List<Vector2>(64);
	}
}
