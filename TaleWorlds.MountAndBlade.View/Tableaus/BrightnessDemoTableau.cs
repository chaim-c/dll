using System;
using TaleWorlds.Engine;
using TaleWorlds.Engine.Options;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.View.Tableaus
{
	// Token: 0x02000022 RID: 34
	public class BrightnessDemoTableau
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600010E RID: 270 RVA: 0x000095FD File Offset: 0x000077FD
		// (set) Token: 0x0600010F RID: 271 RVA: 0x00009605 File Offset: 0x00007805
		public Texture Texture { get; private set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000110 RID: 272 RVA: 0x0000960E File Offset: 0x0000780E
		private TableauView View
		{
			get
			{
				if (this.Texture != null)
				{
					return this.Texture.TableauView;
				}
				return null;
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00009645 File Offset: 0x00007845
		private void SetEnabled(bool enabled)
		{
			this._isEnabled = enabled;
			TableauView view = this.View;
			if (!this._initialized)
			{
				this.SetScene();
			}
			if (view == null)
			{
				return;
			}
			view.SetEnable(this._isEnabled);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00009672 File Offset: 0x00007872
		public void SetDemoType(int demoType)
		{
			this._demoType = demoType;
			this._initialized = false;
			this.RefreshDemoTableau();
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00009688 File Offset: 0x00007888
		public void SetTargetSize(int width, int height)
		{
			int num;
			int num2;
			if (width <= 0 || height <= 0)
			{
				num = 10;
				num2 = 10;
			}
			else
			{
				this.RenderScale = NativeOptions.GetConfig(NativeOptions.NativeOptionsType.ResolutionScale) / 100f;
				num = (int)((float)width * this.RenderScale);
				num2 = (int)((float)height * this.RenderScale);
			}
			if (num != this._tableauSizeX || num2 != this._tableauSizeY)
			{
				this._tableauSizeX = num;
				this._tableauSizeY = num2;
				TableauView view = this.View;
				if (view != null)
				{
					view.SetEnable(false);
				}
				TableauView view2 = this.View;
				if (view2 != null)
				{
					view2.AddClearTask(true);
				}
				Texture texture = this.Texture;
				if (texture != null)
				{
					texture.ReleaseNextFrame();
				}
				this.Texture = TableauView.AddTableau("BrightnessDemo", new RenderTargetComponent.TextureUpdateEventHandler(this.SceneTableauContinuousRenderFunction), this._tableauScene, this._tableauSizeX, this._tableauSizeY);
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00009758 File Offset: 0x00007958
		public void OnFinalize()
		{
			if (this._continuousRenderCamera != null)
			{
				this._continuousRenderCamera.ReleaseCameraEntity();
				this._continuousRenderCamera = null;
			}
			TableauView view = this.View;
			if (view != null)
			{
				view.SetEnable(false);
			}
			TableauView view2 = this.View;
			if (view2 != null)
			{
				view2.AddClearTask(false);
			}
			this.Texture = null;
			this._tableauScene = null;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000097B8 File Offset: 0x000079B8
		public void SetScene()
		{
			this._tableauScene = Scene.CreateNewScene(true, true, DecalAtlasGroup.All, "mono_renderscene");
			switch (this._demoType)
			{
			case 0:
				this._demoTexture = Texture.GetFromResource("brightness_calibration_wide");
				this._tableauScene.SetAtmosphereWithName("brightness_calibration_screen");
				break;
			case 1:
				this._demoTexture = Texture.GetFromResource("calibration_image_1");
				this._tableauScene.SetAtmosphereWithName("TOD_11_00_SemiCloudy");
				break;
			case 2:
				this._demoTexture = Texture.GetFromResource("calibration_image_2");
				this._tableauScene.SetAtmosphereWithName("TOD_05_00_SemiCloudy");
				break;
			case 3:
				this._demoTexture = Texture.GetFromResource("calibration_image_3");
				this._tableauScene.SetAtmosphereWithName("exposure_calibration_interior");
				break;
			default:
				Debug.FailedAssert(string.Format("Undefined Brightness demo type({0})", this._demoType), "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.View\\Tableaus\\BrightnessDemoTableau.cs", "SetScene", 130);
				break;
			}
			this._tableauScene.SetDepthOfFieldParameters(0f, 0f, false);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000098C4 File Offset: 0x00007AC4
		private void RefreshDemoTableau()
		{
			if (!this._initialized)
			{
				this.SetEnabled(true);
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000098D5 File Offset: 0x00007AD5
		public void OnTick(float dt)
		{
			if (this._continuousRenderCamera == null)
			{
				this._continuousRenderCamera = Camera.CreateCamera();
			}
			TableauView view = this.View;
			if (view == null)
			{
				return;
			}
			view.SetDoNotRenderThisFrame(false);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00009904 File Offset: 0x00007B04
		internal void SceneTableauContinuousRenderFunction(Texture sender, EventArgs e)
		{
			Scene scene = (Scene)sender.UserData;
			TableauView tableauView = sender.TableauView;
			tableauView.SetEnable(true);
			if (scene == null)
			{
				tableauView.SetContinuousRendering(false);
				tableauView.SetDeleteAfterRendering(true);
				return;
			}
			scene.SetShadow(false);
			scene.EnsurePostfxSystem();
			scene.SetDofMode(false);
			scene.SetMotionBlurMode(false);
			scene.SetBloom(true);
			scene.SetDynamicShadowmapCascadesRadiusMultiplier(0.31f);
			scene.SetExternalInjectionTexture(this._demoTexture);
			tableauView.SetRenderWithPostfx(true);
			if (this._continuousRenderCamera != null)
			{
				Camera continuousRenderCamera = this._continuousRenderCamera;
				tableauView.SetCamera(continuousRenderCamera);
				tableauView.SetScene(scene);
				tableauView.SetSceneUsesSkybox(false);
				tableauView.SetDeleteAfterRendering(false);
				tableauView.SetContinuousRendering(true);
				tableauView.SetDoNotRenderThisFrame(true);
				tableauView.SetClearColor(4278190080U);
				tableauView.SetFocusedShadowmap(true, ref this._frame.origin, 1.55f);
			}
		}

		// Token: 0x0400008F RID: 143
		private MatrixFrame _frame;

		// Token: 0x04000090 RID: 144
		private Scene _tableauScene;

		// Token: 0x04000091 RID: 145
		private Texture _demoTexture;

		// Token: 0x04000092 RID: 146
		private Camera _continuousRenderCamera;

		// Token: 0x04000093 RID: 147
		private bool _initialized;

		// Token: 0x04000094 RID: 148
		private int _tableauSizeX;

		// Token: 0x04000095 RID: 149
		private int _tableauSizeY;

		// Token: 0x04000096 RID: 150
		private int _demoType = -1;

		// Token: 0x04000097 RID: 151
		private bool _isEnabled;

		// Token: 0x04000098 RID: 152
		private float RenderScale = 1f;
	}
}
