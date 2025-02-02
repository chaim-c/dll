using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Engine.Options;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.View.Tableaus
{
	// Token: 0x02000025 RID: 37
	public class SceneTableau
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000172 RID: 370 RVA: 0x0000C78F File Offset: 0x0000A98F
		// (set) Token: 0x06000173 RID: 371 RVA: 0x0000C797 File Offset: 0x0000A997
		public Texture _texture { get; private set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000174 RID: 372 RVA: 0x0000C7A0 File Offset: 0x0000A9A0
		public bool? IsReady
		{
			get
			{
				SceneView view = this.View;
				if (view == null)
				{
					return null;
				}
				return new bool?(view.ReadyToRender());
			}
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000C7CB File Offset: 0x0000A9CB
		public SceneTableau()
		{
			this.SetEnabled(true);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000C7F0 File Offset: 0x0000A9F0
		private void SetEnabled(bool enabled)
		{
			this._isEnabled = enabled;
			SceneView view = this.View;
			if (view == null)
			{
				return;
			}
			view.SetEnable(this._isEnabled);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000C810 File Offset: 0x0000AA10
		private void CreateTexture()
		{
			this._texture = Texture.CreateRenderTarget("SceneTableau", this._tableauSizeX, this._tableauSizeY, true, false, false, false);
			this.View = SceneView.CreateSceneView();
			this.View.SetScene(this._tableauScene);
			this.View.SetRenderTarget(this._texture);
			this.View.SetAutoDepthTargetCreation(true);
			this.View.SetSceneUsesSkybox(true);
			this.View.SetClearColor(4294902015U);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000C894 File Offset: 0x0000AA94
		public void SetTargetSize(int width, int height)
		{
			this._isRotatingCharacter = false;
			if (width <= 0 || height <= 0)
			{
				this._tableauSizeX = 10;
				this._tableauSizeY = 10;
			}
			else
			{
				this.RenderScale = NativeOptions.GetConfig(NativeOptions.NativeOptionsType.ResolutionScale) / 100f;
				this._tableauSizeX = (int)((float)width * this.RenderScale);
				this._tableauSizeY = (int)((float)height * this.RenderScale);
			}
			this._cameraRatio = (float)this._tableauSizeX / (float)this._tableauSizeY;
			SceneView view = this.View;
			SceneView view2 = this.View;
			if (view2 != null)
			{
				view2.SetEnable(false);
			}
			SceneView view3 = this.View;
			if (view3 != null)
			{
				view3.AddClearTask(true);
			}
			this.CreateTexture();
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000C93C File Offset: 0x0000AB3C
		public void OnFinalize()
		{
			if (this._continuousRenderCamera != null)
			{
				this._continuousRenderCamera.ReleaseCameraEntity();
				this._continuousRenderCamera = null;
				this._cameraEntity = null;
			}
			SceneView view = this.View;
			if (view != null)
			{
				view.SetEnable(false);
			}
			SceneView view2 = this.View;
			if (view2 != null)
			{
				view2.AddClearTask(false);
			}
			this._texture.ReleaseNextFrame();
			this._texture = null;
			this._tableauScene = null;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000C9B0 File Offset: 0x0000ABB0
		public void SetScene(object scene)
		{
			Scene tableauScene;
			if ((tableauScene = (scene as Scene)) != null)
			{
				this._tableauScene = tableauScene;
				if (this._tableauSizeX != 0 && this._tableauSizeY != 0)
				{
					this.CreateTexture();
					return;
				}
			}
			else
			{
				Debug.FailedAssert("Given scene object is not Scene type", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.View\\Tableaus\\SceneTableau.cs", "SetScene", 120);
			}
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000C9FB File Offset: 0x0000ABFB
		public void SetBannerCode(string value)
		{
			this.RefreshCharacterTableau(null);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000CA04 File Offset: 0x0000AC04
		private void RefreshCharacterTableau(Equipment oldEquipment = null)
		{
			if (!this._initialized)
			{
				this.FirstTimeInit();
			}
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000CA14 File Offset: 0x0000AC14
		public void RotateCharacter(bool value)
		{
			this._isRotatingCharacter = value;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000CA20 File Offset: 0x0000AC20
		public void OnTick(float dt)
		{
			if (this._animationFrequencyThreshold > this._animationGap)
			{
				this._animationGap += dt;
			}
			if (this.View != null)
			{
				if (this._continuousRenderCamera == null)
				{
					GameEntity gameEntity = this._tableauScene.FindEntityWithTag("customcamera");
					if (gameEntity != null)
					{
						this._continuousRenderCamera = Camera.CreateCamera();
						Vec3 vec = default(Vec3);
						gameEntity.GetCameraParamsFromCameraScript(this._continuousRenderCamera, ref vec);
						this._cameraEntity = gameEntity;
					}
				}
				this.PopupSceneContinuousRenderFunction();
			}
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000CAAD File Offset: 0x0000ACAD
		private void FirstTimeInit()
		{
			this._initialized = true;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000CAB8 File Offset: 0x0000ACB8
		private void PopupSceneContinuousRenderFunction()
		{
			GameEntity gameEntity = this._tableauScene.FindEntityWithTag("customcamera");
			this._tableauScene.SetShadow(true);
			this._tableauScene.EnsurePostfxSystem();
			this._tableauScene.SetMotionBlurMode(true);
			this._tableauScene.SetBloom(true);
			this._tableauScene.SetDynamicShadowmapCascadesRadiusMultiplier(1f);
			this.View.SetRenderWithPostfx(true);
			this.View.SetSceneUsesShadows(true);
			this.View.SetScene(this._tableauScene);
			this.View.SetSceneUsesSkybox(true);
			this.View.SetClearColor(4278190080U);
			this.View.SetFocusedShadowmap(false, ref this._frame.origin, 1.55f);
			this.View.SetEnable(true);
			if (gameEntity != null)
			{
				Vec3 vec = default(Vec3);
				gameEntity.GetCameraParamsFromCameraScript(this._continuousRenderCamera, ref vec);
				if (this._continuousRenderCamera != null)
				{
					Camera continuousRenderCamera = this._continuousRenderCamera;
					this.View.SetCamera(continuousRenderCamera);
				}
			}
		}

		// Token: 0x040000FE RID: 254
		private float _animationFrequencyThreshold = 2.5f;

		// Token: 0x040000FF RID: 255
		private MatrixFrame _frame;

		// Token: 0x04000100 RID: 256
		private Scene _tableauScene;

		// Token: 0x04000101 RID: 257
		private Camera _continuousRenderCamera;

		// Token: 0x04000102 RID: 258
		private GameEntity _cameraEntity;

		// Token: 0x04000103 RID: 259
		private float _cameraRatio;

		// Token: 0x04000104 RID: 260
		private bool _initialized;

		// Token: 0x04000105 RID: 261
		private int _tableauSizeX;

		// Token: 0x04000106 RID: 262
		private int _tableauSizeY;

		// Token: 0x04000107 RID: 263
		private SceneView View;

		// Token: 0x04000108 RID: 264
		private bool _isRotatingCharacter;

		// Token: 0x04000109 RID: 265
		private float _animationGap;

		// Token: 0x0400010A RID: 266
		private bool _isEnabled;

		// Token: 0x0400010B RID: 267
		private float RenderScale = 1f;
	}
}
