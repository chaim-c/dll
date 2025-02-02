using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TaleWorlds.Engine;
using TaleWorlds.Engine.Options;
using TaleWorlds.Engine.Screens;
using TaleWorlds.Library;
using TaleWorlds.ScreenSystem;

namespace TaleWorlds.MountAndBlade.View.Screens
{
	// Token: 0x02000037 RID: 55
	public class VisualTestsScreen : ScreenBase
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0001709D File Offset: 0x0001529D
		private int CamPointCount
		{
			get
			{
				return this.CamPoints.Count;
			}
		}

		// Token: 0x06000277 RID: 631 RVA: 0x000170AA File Offset: 0x000152AA
		public bool StartedRendering()
		{
			return this._sceneLayer.SceneView.ReadyToRender();
		}

		// Token: 0x06000278 RID: 632 RVA: 0x000170BC File Offset: 0x000152BC
		public string GetSubTestName(VisualTestsScreen.CameraPointTestType type)
		{
			if (type == VisualTestsScreen.CameraPointTestType.Albedo)
			{
				return "_albedo";
			}
			if (type == VisualTestsScreen.CameraPointTestType.Normal)
			{
				return "_normal";
			}
			if (type == VisualTestsScreen.CameraPointTestType.Specular)
			{
				return "_specular";
			}
			if (type == VisualTestsScreen.CameraPointTestType.AO)
			{
				return "_ao";
			}
			if (type == VisualTestsScreen.CameraPointTestType.OnlyAmbient)
			{
				return "_onlyambient";
			}
			if (type == VisualTestsScreen.CameraPointTestType.OnlyDirect)
			{
				return "_onlydirect";
			}
			if (type == VisualTestsScreen.CameraPointTestType.Final)
			{
				return "_final";
			}
			return "";
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00017113 File Offset: 0x00015313
		public Utilities.EngineRenderDisplayMode GetRenderMode(VisualTestsScreen.CameraPointTestType type)
		{
			if (type == VisualTestsScreen.CameraPointTestType.Albedo)
			{
				return Utilities.EngineRenderDisplayMode.ShowAlbedo;
			}
			if (type == VisualTestsScreen.CameraPointTestType.Normal)
			{
				return Utilities.EngineRenderDisplayMode.ShowNormals;
			}
			if (type == VisualTestsScreen.CameraPointTestType.Specular)
			{
				return Utilities.EngineRenderDisplayMode.ShowSpecular;
			}
			if (type == VisualTestsScreen.CameraPointTestType.AO)
			{
				return Utilities.EngineRenderDisplayMode.ShowOcclusion;
			}
			if (type == VisualTestsScreen.CameraPointTestType.OnlyAmbient)
			{
				return Utilities.EngineRenderDisplayMode.ShowDisableSunLighting;
			}
			if (type == VisualTestsScreen.CameraPointTestType.OnlyDirect)
			{
				return Utilities.EngineRenderDisplayMode.ShowDisableAmbientLighting;
			}
			return Utilities.EngineRenderDisplayMode.ShowNone;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0001713C File Offset: 0x0001533C
		public VisualTestsScreen(bool isValidTest, NativeOptions.ConfigQuality preset, string sceneName, DateTime testTime, List<string> testTypesToCheck)
		{
			this.isValidTest_ = isValidTest;
			this.preset_ = preset;
			this.scene_name = sceneName;
			this.testTime = testTime;
			VisualTestsScreen.isSceneSuccess = true;
			this._failDirectory = string.Concat(new object[]
			{
				this._failDirectory,
				"/",
				sceneName,
				"_",
				preset
			});
			this.testTypesToCheck_ = testTypesToCheck;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0001720F File Offset: 0x0001540F
		protected override void OnInitialize()
		{
			base.OnInitialize();
			this._sceneLayer = new SceneLayer("SceneLayer", true, true);
			base.AddLayer(this._sceneLayer);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00017238 File Offset: 0x00015438
		protected override void OnActivate()
		{
			base.OnActivate();
			if (!this.isValidTest_)
			{
				this.date = this.testTime.ToString("dd-MM-yyyy hh-mmtt");
				this._pathDirectory = this._pathDirectory + this.date + "/";
				Directory.CreateDirectory(this._pathDirectory);
				this._reportFile = this._pathDirectory + "report.txt";
			}
			this.CreateScene();
			this._scene.Tick(0f);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x000172BD File Offset: 0x000154BD
		protected override void OnDeactivate()
		{
			base.OnDeactivate();
		}

		// Token: 0x0600027E RID: 638 RVA: 0x000172C8 File Offset: 0x000154C8
		protected override void OnFrameTick(float dt)
		{
			base.OnFrameTick(dt);
			LoadingWindow.DisableGlobalLoadingWindow();
			MessageManager.EraseMessageLines();
			if (!this._sceneLayer.ReadyToRender())
			{
				return;
			}
			this.SetTestCamera();
			if (Utilities.GetNumberOfShaderCompilationsInProgress() > 0)
			{
				return;
			}
			float dt2 = (this._scene.GetName() == "visualtestmorph") ? 0.01f : 0f;
			this._scene.Tick(dt2);
			int num = 5;
			this.frameCounter++;
			if (this.frameCounter < num)
			{
				return;
			}
			this.TakeScreenshotAndAnalyze();
			if (this.CurCameraIndex >= this.CamPointCount)
			{
				ScreenManager.PopScreen();
				return;
			}
			this.frameCounter = 0;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00017370 File Offset: 0x00015570
		private void CreateScene()
		{
			this._scene = Scene.CreateNewScene(true, true, DecalAtlasGroup.All, "mono_renderscene");
			this._scene.SetName("VisualTestScreen");
			this._agentRendererSceneController = MBAgentRendererSceneController.CreateNewAgentRendererSceneController(this._scene, 32);
			this._scene.Read(this.scene_name);
			this._scene.SetUseConstantTime(true);
			this._scene.SetOcclusionMode(true);
			this._scene.OptimizeScene(true, true);
			this._sceneLayer.SetScene(this._scene);
			this._sceneLayer.SceneView.SetSceneUsesShadows(true);
			this._sceneLayer.SceneView.SetForceShaderCompilation(true);
			this._sceneLayer.SceneView.SetClearGbuffer(true);
			this._camera = Camera.CreateCamera();
			this.GetCameraPoints();
			MessageManager.EraseMessageLines();
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00017443 File Offset: 0x00015643
		private bool ShouldCheckTestModeWithTag(string mode, GameEntity entity)
		{
			if (this.testTypesToCheck_.Count > 0)
			{
				return this.testTypesToCheck_.Contains(mode) && entity.HasTag(mode);
			}
			return entity.HasTag(mode);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00017472 File Offset: 0x00015672
		private bool ShouldCheckTestMode(string mode)
		{
			return this.testTypesToCheck_.Contains(mode);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00017480 File Offset: 0x00015680
		private void GetCameraPoints()
		{
			this.CamPoints = new List<VisualTestsScreen.CameraPoint>();
			foreach (GameEntity gameEntity in (from o in this._scene.FindEntitiesWithTag("test_camera")
			orderby o.Name
			select o).ToList<GameEntity>())
			{
				if (!gameEntity.HasTag("exclude_" + (int)this.preset_))
				{
					VisualTestsScreen.CameraPoint cameraPoint = new VisualTestsScreen.CameraPoint();
					cameraPoint.CamFrame = gameEntity.GetFrame();
					cameraPoint.CameraName = gameEntity.Name;
					HashSet<VisualTestsScreen.CameraPointTestType> hashSet = new HashSet<VisualTestsScreen.CameraPointTestType>();
					if (this.ShouldCheckTestModeWithTag("gbuffer", gameEntity))
					{
						hashSet.Add(VisualTestsScreen.CameraPointTestType.Albedo);
						hashSet.Add(VisualTestsScreen.CameraPointTestType.Normal);
						hashSet.Add(VisualTestsScreen.CameraPointTestType.Specular);
						hashSet.Add(VisualTestsScreen.CameraPointTestType.AO);
					}
					if (this.ShouldCheckTestMode("albedo"))
					{
						hashSet.Add(VisualTestsScreen.CameraPointTestType.Albedo);
					}
					if (this.ShouldCheckTestMode("normal"))
					{
						hashSet.Add(VisualTestsScreen.CameraPointTestType.Normal);
					}
					if (this.ShouldCheckTestMode("specular"))
					{
						hashSet.Add(VisualTestsScreen.CameraPointTestType.Specular);
					}
					if (this.ShouldCheckTestMode("ao"))
					{
						hashSet.Add(VisualTestsScreen.CameraPointTestType.AO);
					}
					if (this.ShouldCheckTestModeWithTag("only_ambient", gameEntity))
					{
						hashSet.Add(VisualTestsScreen.CameraPointTestType.OnlyAmbient);
					}
					foreach (VisualTestsScreen.CameraPointTestType item in hashSet)
					{
						cameraPoint.TestTypes.Add(item);
					}
					cameraPoint.TestTypes.Add(VisualTestsScreen.CameraPointTestType.Final);
					this.CamPoints.Add(cameraPoint);
				}
			}
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00017664 File Offset: 0x00015864
		private void SetTestCamera()
		{
			VisualTestsScreen.CameraPoint cameraPoint = this.CamPoints[this.CurCameraIndex];
			MatrixFrame camFrame = cameraPoint.CamFrame;
			this._camera.Frame = camFrame;
			float aspectRatio = Screen.AspectRatio;
			this._camera.SetFovVertical(1.0471976f, aspectRatio, 0.1f, 500f);
			this._sceneLayer.SetCamera(this._camera);
			VisualTestsScreen.CameraPointTestType type = cameraPoint.TestTypes[this.TestSubIndex];
			Utilities.SetRenderMode(this.GetRenderMode(type));
		}

		// Token: 0x06000284 RID: 644 RVA: 0x000176E4 File Offset: 0x000158E4
		protected override void OnFinalize()
		{
			MBDebug.Print("On finalized called for scene: " + this.scene_name, 0, Debug.DebugColor.White, 17592186044416UL);
			base.OnFinalize();
			this._sceneLayer.ClearAll();
			MBAgentRendererSceneController.DestructAgentRendererSceneController(this._scene, this._agentRendererSceneController, false);
			this._agentRendererSceneController = null;
			this._scene = null;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00017743 File Offset: 0x00015943
		public void Reset()
		{
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00017748 File Offset: 0x00015948
		private void TakeScreenshotAndAnalyze()
		{
			VisualTestsScreen.CameraPoint cameraPoint = this.CamPoints[this.CurCameraIndex];
			VisualTestsScreen.CameraPointTestType type = cameraPoint.TestTypes[this.TestSubIndex];
			this.GetRenderMode(type);
			bool flag = true;
			string text;
			if (this.isValidTest_)
			{
				text = string.Concat(new string[]
				{
					this._validReadDirectory,
					this.scene_name,
					"_",
					cameraPoint.CameraName,
					"_",
					this.GetSubTestName(type),
					"_preset_",
					NativeOptions.GetGFXPresetName(this.preset_),
					".bmp"
				});
			}
			else
			{
				text = string.Concat(new string[]
				{
					this._validReadDirectory,
					this.scene_name,
					"_",
					cameraPoint.CameraName,
					"_",
					this.GetSubTestName(type),
					"_preset_",
					NativeOptions.GetGFXPresetName(this.preset_),
					".bmp"
				});
			}
			string str = string.Concat(new string[]
			{
				this.scene_name,
				"_",
				cameraPoint.CameraName,
				"_",
				this.GetSubTestName(type),
				"_preset_",
				NativeOptions.GetGFXPresetName(this.preset_),
				".bmp"
			});
			string text2 = this._pathDirectory + str;
			MBDebug.Print(text, 0, Debug.DebugColor.White, 17592186044416UL);
			MBDebug.Print(text2, 0, Debug.DebugColor.White, 17592186044416UL);
			if (this.isValidTest_)
			{
				Utilities.TakeScreenshot(text);
			}
			else
			{
				Utilities.TakeScreenshot(text2);
			}
			NativeOptions.GetGFXPresetName(this.preset_);
			if (!this.isValidTest_)
			{
				if (File.Exists(text))
				{
					if (!this.AnalyzeImageDifferences(text, text2))
					{
						flag = false;
					}
				}
				else
				{
					flag = false;
				}
			}
			if (!flag)
			{
				if (!Directory.Exists(this._failDirectory))
				{
					Directory.CreateDirectory(TestCommonBase.GetAttachmentsFolderPath());
				}
				if (!Directory.Exists(this._failDirectory))
				{
					Directory.CreateDirectory(this._failDirectory);
				}
				string text3 = this._failDirectory + "/" + cameraPoint.CameraName + this.GetSubTestName(type);
				if (!Directory.Exists(text3))
				{
					Directory.CreateDirectory(text3);
				}
				string text4 = text3 + "/branch_result.bmp";
				string text5 = text3 + "/valid.bmp";
				if (File.Exists(text4))
				{
					File.Delete(text4);
				}
				if (File.Exists(text5))
				{
					File.Delete(text5);
				}
				File.Copy(text2, text4);
				if (File.Exists(text))
				{
					if (File.Exists(text5))
					{
						File.Delete(text5);
					}
					File.Copy(text, text5);
				}
				VisualTestsScreen.isSceneSuccess = false;
			}
			this.TestSubIndex++;
			if (cameraPoint.TestTypes.Count == this.TestSubIndex)
			{
				this.CurCameraIndex++;
				this.TestSubIndex = 0;
			}
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00017A2C File Offset: 0x00015C2C
		private bool AnalyzeImageDifferences(string path1, string path2)
		{
			byte[] array = File.ReadAllBytes(path1);
			byte[] array2 = File.ReadAllBytes(path2);
			if (array.Length != array2.Length)
			{
				return false;
			}
			float num = 0f;
			for (int i = 0; i < array.Length; i++)
			{
				float num2 = (float)array[i];
				float num3 = (float)array2[i];
				float num4 = MathF.Max(MathF.Abs(num2 - num3), 0f);
				num += num4;
			}
			num /= (float)array.Length;
			return num < 0.5f;
		}

		// Token: 0x0400019E RID: 414
		private Scene _scene;

		// Token: 0x0400019F RID: 415
		private MBAgentRendererSceneController _agentRendererSceneController;

		// Token: 0x040001A0 RID: 416
		private Camera _camera;

		// Token: 0x040001A1 RID: 417
		private SceneLayer _sceneLayer;

		// Token: 0x040001A2 RID: 418
		private List<VisualTestsScreen.CameraPoint> CamPoints;

		// Token: 0x040001A3 RID: 419
		private DateTime testTime;

		// Token: 0x040001A4 RID: 420
		private string _validWriteDirectory = Utilities.GetVisualTestsValidatePath();

		// Token: 0x040001A5 RID: 421
		private string _validReadDirectory = Utilities.GetBasePath() + "ValidVisuals/";

		// Token: 0x040001A6 RID: 422
		private string _pathDirectory = Utilities.GetVisualTestsTestFilesPath();

		// Token: 0x040001A7 RID: 423
		private string _failDirectory = TestCommonBase.GetAttachmentsFolderPath();

		// Token: 0x040001A8 RID: 424
		private string _reportFile = "report.txt";

		// Token: 0x040001A9 RID: 425
		private int CurCameraIndex;

		// Token: 0x040001AA RID: 426
		private int TestSubIndex;

		// Token: 0x040001AB RID: 427
		private bool isValidTest_ = true;

		// Token: 0x040001AC RID: 428
		private NativeOptions.ConfigQuality preset_;

		// Token: 0x040001AD RID: 429
		public static bool isSceneSuccess = true;

		// Token: 0x040001AE RID: 430
		private string date;

		// Token: 0x040001AF RID: 431
		private string scene_name;

		// Token: 0x040001B0 RID: 432
		private int frameCounter = -200;

		// Token: 0x040001B1 RID: 433
		private List<string> testTypesToCheck_ = new List<string>();

		// Token: 0x0200009C RID: 156
		public enum CameraPointTestType
		{
			// Token: 0x04000343 RID: 835
			Final,
			// Token: 0x04000344 RID: 836
			Albedo,
			// Token: 0x04000345 RID: 837
			Normal,
			// Token: 0x04000346 RID: 838
			Specular,
			// Token: 0x04000347 RID: 839
			AO,
			// Token: 0x04000348 RID: 840
			OnlyAmbient,
			// Token: 0x04000349 RID: 841
			OnlyDirect
		}

		// Token: 0x0200009D RID: 157
		public class CameraPoint
		{
			// Token: 0x060004E1 RID: 1249 RVA: 0x00026431 File Offset: 0x00024631
			public CameraPoint()
			{
				this.TestTypes = new List<VisualTestsScreen.CameraPointTestType>();
				this.CamFrame = MatrixFrame.Identity;
				this.CameraName = "";
			}

			// Token: 0x0400034A RID: 842
			public MatrixFrame CamFrame;

			// Token: 0x0400034B RID: 843
			public string CameraName;

			// Token: 0x0400034C RID: 844
			public List<VisualTestsScreen.CameraPointTestType> TestTypes;
		}
	}
}
