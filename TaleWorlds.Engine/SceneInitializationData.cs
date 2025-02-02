using System;
using System.Runtime.InteropServices;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x0200007B RID: 123
	[EngineStruct("rglScene_initialization_data", false)]
	public struct SceneInitializationData
	{
		// Token: 0x060008F1 RID: 2289 RVA: 0x00009154 File Offset: 0x00007354
		public SceneInitializationData(bool initializeWithDefaults)
		{
			if (initializeWithDefaults)
			{
				this.CamPosFromScene = MatrixFrame.Identity;
				this.InitPhysicsWorld = true;
				this.LoadNavMesh = true;
				this.InitFloraNodes = true;
				this.UsePhysicsMaterials = true;
				this.EnableFloraPhysics = true;
				this.UseTerrainMeshBlending = true;
				this.DoNotUseLoadingScreen = false;
				this.CreateOros = false;
				this.ForTerrainShaderCompile = false;
				return;
			}
			this.CamPosFromScene = MatrixFrame.Identity;
			this.InitPhysicsWorld = false;
			this.LoadNavMesh = false;
			this.InitFloraNodes = false;
			this.UsePhysicsMaterials = false;
			this.EnableFloraPhysics = false;
			this.UseTerrainMeshBlending = false;
			this.DoNotUseLoadingScreen = false;
			this.CreateOros = false;
			this.ForTerrainShaderCompile = false;
		}

		// Token: 0x04000185 RID: 389
		public MatrixFrame CamPosFromScene;

		// Token: 0x04000186 RID: 390
		[MarshalAs(UnmanagedType.U1)]
		public bool InitPhysicsWorld;

		// Token: 0x04000187 RID: 391
		[MarshalAs(UnmanagedType.U1)]
		public bool LoadNavMesh;

		// Token: 0x04000188 RID: 392
		[MarshalAs(UnmanagedType.U1)]
		public bool InitFloraNodes;

		// Token: 0x04000189 RID: 393
		[MarshalAs(UnmanagedType.U1)]
		public bool UsePhysicsMaterials;

		// Token: 0x0400018A RID: 394
		[MarshalAs(UnmanagedType.U1)]
		public bool EnableFloraPhysics;

		// Token: 0x0400018B RID: 395
		[MarshalAs(UnmanagedType.U1)]
		public bool UseTerrainMeshBlending;

		// Token: 0x0400018C RID: 396
		[MarshalAs(UnmanagedType.U1)]
		public bool DoNotUseLoadingScreen;

		// Token: 0x0400018D RID: 397
		[MarshalAs(UnmanagedType.U1)]
		public bool CreateOros;

		// Token: 0x0400018E RID: 398
		[MarshalAs(UnmanagedType.U1)]
		public bool ForTerrainShaderCompile;
	}
}
