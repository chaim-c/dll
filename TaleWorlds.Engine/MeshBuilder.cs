using System;
using System.Collections.Generic;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000066 RID: 102
	public class MeshBuilder
	{
		// Token: 0x06000821 RID: 2081 RVA: 0x00007C84 File Offset: 0x00005E84
		public MeshBuilder()
		{
			this.vertices = new List<Vec3>();
			this.faceCorners = new List<MeshBuilder.FaceCorner>();
			this.faces = new List<MeshBuilder.Face>();
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x00007CB0 File Offset: 0x00005EB0
		public int AddFaceCorner(Vec3 position, Vec3 normal, Vec2 uvCoord, uint color)
		{
			this.vertices.Add(new Vec3(position, -1f));
			MeshBuilder.FaceCorner item;
			item.vertexIndex = this.vertices.Count - 1;
			item.color = color;
			item.uvCoord = uvCoord;
			item.normal = normal;
			this.faceCorners.Add(item);
			return this.faceCorners.Count - 1;
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x00007D1C File Offset: 0x00005F1C
		public int AddFace(int patchNode0, int patchNode1, int patchNode2)
		{
			MeshBuilder.Face item;
			item.fc0 = patchNode0;
			item.fc1 = patchNode1;
			item.fc2 = patchNode2;
			this.faces.Add(item);
			return this.faces.Count - 1;
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x00007D5A File Offset: 0x00005F5A
		public void Clear()
		{
			this.vertices.Clear();
			this.faceCorners.Clear();
			this.faces.Clear();
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x00007D80 File Offset: 0x00005F80
		public new Mesh Finalize()
		{
			Vec3[] array = this.vertices.ToArray();
			MeshBuilder.FaceCorner[] array2 = this.faceCorners.ToArray();
			MeshBuilder.Face[] array3 = this.faces.ToArray();
			Mesh result = EngineApplicationInterface.IMeshBuilder.FinalizeMeshBuilder(this.vertices.Count, array, this.faceCorners.Count, array2, this.faces.Count, array3);
			this.vertices.Clear();
			this.faceCorners.Clear();
			this.faces.Clear();
			return result;
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x00007E00 File Offset: 0x00006000
		public static Mesh CreateUnitMesh()
		{
			Mesh mesh = Mesh.CreateMeshWithMaterial(Material.GetDefaultMaterial());
			Vec3 position = new Vec3(0f, -1f, 0f, -1f);
			Vec3 position2 = new Vec3(1f, -1f, 0f, -1f);
			Vec3 position3 = new Vec3(1f, 0f, 0f, -1f);
			Vec3 position4 = new Vec3(0f, 0f, 0f, -1f);
			Vec3 normal = new Vec3(0f, 0f, 1f, -1f);
			Vec2 uvCoord = new Vec2(0f, 0f);
			Vec2 uvCoord2 = new Vec2(1f, 0f);
			Vec2 uvCoord3 = new Vec2(1f, 1f);
			Vec2 uvCoord4 = new Vec2(0f, 1f);
			UIntPtr uintPtr = mesh.LockEditDataWrite();
			int num = mesh.AddFaceCorner(position, normal, uvCoord, uint.MaxValue, uintPtr);
			int patchNode = mesh.AddFaceCorner(position2, normal, uvCoord2, uint.MaxValue, uintPtr);
			int num2 = mesh.AddFaceCorner(position3, normal, uvCoord3, uint.MaxValue, uintPtr);
			int patchNode2 = mesh.AddFaceCorner(position4, normal, uvCoord4, uint.MaxValue, uintPtr);
			mesh.AddFace(num, patchNode, num2, uintPtr);
			mesh.AddFace(num2, patchNode2, num, uintPtr);
			mesh.UpdateBoundingBox();
			mesh.UnlockEditDataWrite(uintPtr);
			return mesh;
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x00007F56 File Offset: 0x00006156
		public static Mesh CreateTilingWindowMesh(string baseMeshName, Vec2 meshSizeMin, Vec2 meshSizeMax, Vec2 borderThickness, Vec2 bgBorderThickness)
		{
			return EngineApplicationInterface.IMeshBuilder.CreateTilingWindowMesh(baseMeshName, ref meshSizeMin, ref meshSizeMax, ref borderThickness, ref bgBorderThickness);
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x00007F6B File Offset: 0x0000616B
		public static Mesh CreateTilingButtonMesh(string baseMeshName, Vec2 meshSizeMin, Vec2 meshSizeMax, Vec2 borderThickness)
		{
			return EngineApplicationInterface.IMeshBuilder.CreateTilingButtonMesh(baseMeshName, ref meshSizeMin, ref meshSizeMax, ref borderThickness);
		}

		// Token: 0x04000137 RID: 311
		private List<Vec3> vertices;

		// Token: 0x04000138 RID: 312
		private List<MeshBuilder.FaceCorner> faceCorners;

		// Token: 0x04000139 RID: 313
		private List<MeshBuilder.Face> faces;

		// Token: 0x020000BF RID: 191
		[EngineStruct("rglMeshBuilder_face_corner", false)]
		public struct FaceCorner
		{
			// Token: 0x040003FB RID: 1019
			public int vertexIndex;

			// Token: 0x040003FC RID: 1020
			public Vec2 uvCoord;

			// Token: 0x040003FD RID: 1021
			public Vec3 normal;

			// Token: 0x040003FE RID: 1022
			public uint color;
		}

		// Token: 0x020000C0 RID: 192
		[EngineStruct("rglMeshBuilder_face", false)]
		public struct Face
		{
			// Token: 0x040003FF RID: 1023
			public int fc0;

			// Token: 0x04000400 RID: 1024
			public int fc1;

			// Token: 0x04000401 RID: 1025
			public int fc2;
		}
	}
}
