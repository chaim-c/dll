using System;
using System.Collections.Generic;
using System.Threading;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.ScreenSystem;

namespace TaleWorlds.MountAndBlade.View.Screens.Scripts
{
	// Token: 0x02000038 RID: 56
	public class MultiThreadedStressTestsScreen : ScreenBase
	{
		// Token: 0x0600028A RID: 650 RVA: 0x00017AAC File Offset: 0x00015CAC
		protected override void OnActivate()
		{
			base.OnActivate();
			this._scene = Scene.CreateNewScene(true, true, DecalAtlasGroup.All, "mono_renderscene");
			this._scene.Read("mp_ruins_2");
			this._sceneView = SceneView.CreateSceneView();
			this._sceneView.SetScene(this._scene);
			this._sceneView.SetSceneUsesShadows(true);
			Camera camera = Camera.CreateCamera();
			camera.Frame = this._scene.ReadAndCalculateInitialCamera();
			this._sceneView.SetCamera(camera);
			this._workerThreads = new List<Thread>();
			Thread thread = new Thread(delegate()
			{
				MultiThreadedStressTestsScreen.MultiThreadedTestFunctions.MeshMerger(InputLayout.Input_layout_regular);
			});
			thread.Name = "StressTester|Mesh Merger Thread";
			this._workerThreads.Add(thread);
			Thread thread2 = new Thread(delegate()
			{
				MultiThreadedStressTestsScreen.MultiThreadedTestFunctions.MeshMerger(InputLayout.Input_layout_normal_map);
			});
			thread2.Name = "StressTester|Mesh Merger Thread";
			this._workerThreads.Add(thread2);
			Thread thread3 = new Thread(delegate()
			{
				MultiThreadedStressTestsScreen.MultiThreadedTestFunctions.MeshMerger(InputLayout.Input_layout_skinning);
			});
			thread3.Name = "StressTester|Mesh Merger Thread";
			this._workerThreads.Add(thread3);
			for (int i = 0; i < this._workerThreads.Count; i++)
			{
				this._workerThreads[i].Start();
			}
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00017C18 File Offset: 0x00015E18
		protected override void OnDeactivate()
		{
			base.OnDeactivate();
			this._sceneView = null;
			this._scene = null;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00017C30 File Offset: 0x00015E30
		protected override void OnFrameTick(float dt)
		{
			base.OnFrameTick(dt);
			bool flag = true;
			for (int i = 0; i < this._workerThreads.Count; i++)
			{
				if (this._workerThreads[i].IsAlive)
				{
					flag = false;
				}
			}
			if (flag)
			{
				ScreenManager.PopScreen();
			}
		}

		// Token: 0x040001B2 RID: 434
		private List<Thread> _workerThreads;

		// Token: 0x040001B3 RID: 435
		private Scene _scene;

		// Token: 0x040001B4 RID: 436
		private SceneView _sceneView;

		// Token: 0x0200009F RID: 159
		public static class MultiThreadedTestFunctions
		{
			// Token: 0x060004E5 RID: 1253 RVA: 0x00026478 File Offset: 0x00024678
			public static void MeshMerger(InputLayout layout)
			{
				Mesh mesh = Mesh.GetRandomMeshWithVdecl((int)layout);
				mesh = mesh.CreateCopy();
				UIntPtr uintPtr = mesh.LockEditDataWrite();
				Mesh mesh2 = Mesh.GetRandomMeshWithVdecl((int)layout);
				mesh2 = mesh2.CreateCopy();
				Mesh randomMeshWithVdecl = Mesh.GetRandomMeshWithVdecl((int)layout);
				Mesh randomMeshWithVdecl2 = Mesh.GetRandomMeshWithVdecl((int)layout);
				mesh.AddMesh(randomMeshWithVdecl, MatrixFrame.Identity);
				mesh2.AddMesh(randomMeshWithVdecl2, MatrixFrame.Identity);
				mesh.AddMesh(mesh2, MatrixFrame.Identity);
				int patchNode = mesh.AddFaceCorner(new Vec3(0f, 0f, 1f, -1f), new Vec3(0f, 0f, 1f, -1f), new Vec2(0f, 1f), 268435455U, uintPtr);
				int patchNode2 = mesh.AddFaceCorner(new Vec3(0f, 1f, 0f, -1f), new Vec3(0f, 0f, 1f, -1f), new Vec2(1f, 0f), 268435455U, uintPtr);
				int patchNode3 = mesh.AddFaceCorner(new Vec3(0f, 1f, 1f, -1f), new Vec3(0f, 0f, 1f, -1f), new Vec2(1f, 1f), 268435455U, uintPtr);
				mesh.AddFace(patchNode, patchNode2, patchNode3, uintPtr);
				mesh.UnlockEditDataWrite(uintPtr);
			}

			// Token: 0x060004E6 RID: 1254 RVA: 0x000265E0 File Offset: 0x000247E0
			public static void SceneHandler(SceneView view)
			{
				int i = 0;
				while (i < 500)
				{
					view.SetSceneUsesShadows(false);
					view.SetRenderWithPostfx(false);
					Thread.Sleep(5000);
					view.SetSceneUsesShadows(true);
					view.SetRenderWithPostfx(true);
					Thread.Sleep(5000);
					view.SetSceneUsesContour(true);
					Thread.Sleep(5000);
				}
			}
		}
	}
}
