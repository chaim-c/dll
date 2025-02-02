using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.ScreenSystem;

namespace TaleWorlds.MountAndBlade.View.Screens
{
	// Token: 0x0200002B RID: 43
	public class BenchmarkScreen : ScreenBase
	{
		// Token: 0x060001BE RID: 446 RVA: 0x0000F7EC File Offset: 0x0000D9EC
		protected override void OnActivate()
		{
			base.OnActivate();
			this._scene = Scene.CreateNewScene(true, true, DecalAtlasGroup.All, "mono_renderscene");
			this._scene.SetName("BenchmarkScreen");
			this._scene.Read("benchmark");
			this._cameraFrame = this._scene.ReadAndCalculateInitialCamera();
			this._scene.SetUseConstantTime(true);
			this._sceneView = SceneView.CreateSceneView();
			this._sceneView.SetScene(this._scene);
			this._sceneView.SetSceneUsesShadows(true);
			this._camera = Camera.CreateCamera();
			this.UpdateCamera();
			this._cameraTimer = new Timer(MBCommon.GetApplicationTime() - 5f, 5f, true);
			GameEntity gameEntity = this._scene.FindEntityWithName("LocationEntityParent");
			this._cameraLocationEntities = gameEntity.GetChildren().ToList<GameEntity>();
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000F8C6 File Offset: 0x0000DAC6
		public void UpdateCamera()
		{
			this._camera.Frame = this._cameraFrame;
			this._sceneView.SetCamera(this._camera);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000F8EA File Offset: 0x0000DAEA
		protected override void OnDeactivate()
		{
			base.OnDeactivate();
			this._scene = null;
			this._analyzer = null;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000F900 File Offset: 0x0000DB00
		protected override void OnFrameTick(float dt)
		{
			base.OnFrameTick(dt);
			if (this._cameraTimer.Check(MBCommon.GetApplicationTime()))
			{
				this._currentEntityIndex++;
				if (this._currentEntityIndex >= this._cameraLocationEntities.Count)
				{
					this._analyzer.FinalizeAndWrite("../../../Tools/TestAutomation/Attachments/benchmark_scene_performance.xml");
					ScreenManager.PopScreen();
					return;
				}
				GameEntity gameEntity = this._cameraLocationEntities[this._currentEntityIndex];
				this._cameraFrame = gameEntity.GetGlobalFrame();
				this.UpdateCamera();
				this._analyzer.Start(gameEntity.Name);
				this._cameraTimer.Reset(MBCommon.GetApplicationTime());
			}
			this._analyzer.Tick(dt);
		}

		// Token: 0x0400012E RID: 302
		private SceneView _sceneView;

		// Token: 0x0400012F RID: 303
		private Scene _scene;

		// Token: 0x04000130 RID: 304
		private Camera _camera;

		// Token: 0x04000131 RID: 305
		private MatrixFrame _cameraFrame;

		// Token: 0x04000132 RID: 306
		private Timer _cameraTimer;

		// Token: 0x04000133 RID: 307
		private const string _parentEntityName = "LocationEntityParent";

		// Token: 0x04000134 RID: 308
		private const string _sceneName = "benchmark";

		// Token: 0x04000135 RID: 309
		private const string _xmlPath = "../../../Tools/TestAutomation/Attachments/benchmark_scene_performance.xml";

		// Token: 0x04000136 RID: 310
		private List<GameEntity> _cameraLocationEntities;

		// Token: 0x04000137 RID: 311
		private int _currentEntityIndex = -1;

		// Token: 0x04000138 RID: 312
		private PerformanceAnalyzer _analyzer = new PerformanceAnalyzer();
	}
}
