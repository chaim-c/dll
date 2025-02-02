using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Engine.Options;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.View.Tableaus
{
	// Token: 0x02000020 RID: 32
	public class BannerTableau
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00007E5E File Offset: 0x0000605E
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x00007E66 File Offset: 0x00006066
		public Texture Texture { get; private set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00007E6F File Offset: 0x0000606F
		internal Camera CurrentCamera
		{
			get
			{
				if (!this._isNineGrid)
				{
					return this._defaultCamera;
				}
				return this._nineGridCamera;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00007E86 File Offset: 0x00006086
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

		// Token: 0x060000EC RID: 236 RVA: 0x00007EA3 File Offset: 0x000060A3
		public BannerTableau()
		{
			this.SetEnabled(true);
			this.FirstTimeInit();
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00007EDC File Offset: 0x000060DC
		public void OnTick(float dt)
		{
			if (this._isEnabled && !this._isFinalized)
			{
				this.Refresh();
				TableauView view = this.View;
				if (view == null)
				{
					return;
				}
				view.SetDoNotRenderThisFrame(false);
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00007F08 File Offset: 0x00006108
		private void FirstTimeInit()
		{
			this._scene = Scene.CreateNewScene(true, false, DecalAtlasGroup.All, "mono_renderscene");
			this._scene.DisableStaticShadows(true);
			this._scene.SetName("BannerTableau.Scene");
			this._scene.SetDefaultLighting();
			this._defaultCamera = TableauCacheManager.CreateDefaultBannerCamera();
			this._nineGridCamera = TableauCacheManager.CreateNineGridBannerCamera();
			this._isDirty = true;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00007F6C File Offset: 0x0000616C
		private void Refresh()
		{
			if (this._isDirty)
			{
				if (this._currentMeshEntity != null)
				{
					this._scene.RemoveEntity(this._currentMeshEntity, 111);
				}
				if (this._banner != null)
				{
					MatrixFrame identity = MatrixFrame.Identity;
					this._currentMultiMesh = this._banner.ConvertToMultiMesh();
					this._currentMeshEntity = this._scene.AddItemEntity(ref identity, this._currentMultiMesh);
					this._currentMeshEntity.ManualInvalidate();
					this._currentMultiMesh.ManualInvalidate();
					this._isDirty = false;
				}
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00007FF7 File Offset: 0x000061F7
		private void SetEnabled(bool enabled)
		{
			this._isEnabled = enabled;
			TableauView view = this.View;
			if (view == null)
			{
				return;
			}
			view.SetEnable(this._isEnabled);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00008018 File Offset: 0x00006218
		public void SetTargetSize(int width, int height)
		{
			this._latestWidth = width;
			this._latestHeight = height;
			if (width <= 0 || height <= 0)
			{
				this._tableauSizeX = 10;
				this._tableauSizeY = 10;
			}
			else
			{
				this.RenderScale = NativeOptions.GetConfig(NativeOptions.NativeOptionsType.ResolutionScale) / 100f;
				this._tableauSizeX = (int)((float)width * this._customRenderScale * this.RenderScale);
				this._tableauSizeY = (int)((float)height * this._customRenderScale * this.RenderScale);
			}
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
			this.Texture = TableauView.AddTableau("BannerTableau", new RenderTargetComponent.TextureUpdateEventHandler(this.BannerTableauContinuousRenderFunction), this._scene, this._tableauSizeX, this._tableauSizeY);
			this.Texture.TableauView.SetSceneUsesContour(false);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00008102 File Offset: 0x00006302
		public void SetBannerCode(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				this._banner = null;
			}
			else
			{
				this._banner = BannerCode.CreateFrom(value).CalculateBanner();
			}
			this._isDirty = true;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00008130 File Offset: 0x00006330
		public void OnFinalize()
		{
			if (!this._isFinalized)
			{
				Scene scene = this._scene;
				if (scene != null)
				{
					scene.ClearDecals();
				}
				Scene scene2 = this._scene;
				if (scene2 != null)
				{
					scene2.ClearAll();
				}
				Scene scene3 = this._scene;
				if (scene3 != null)
				{
					scene3.ManualInvalidate();
				}
				this._scene = null;
				TableauView view = this.View;
				if (view != null)
				{
					view.SetEnable(false);
				}
				Texture texture = this.Texture;
				if (texture != null)
				{
					texture.ReleaseNextFrame();
				}
				this.Texture = null;
				Camera defaultCamera = this._defaultCamera;
				if (defaultCamera != null)
				{
					defaultCamera.ReleaseCamera();
				}
				this._defaultCamera = null;
				Camera nineGridCamera = this._nineGridCamera;
				if (nineGridCamera != null)
				{
					nineGridCamera.ReleaseCamera();
				}
				this._nineGridCamera = null;
			}
			this._isFinalized = true;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000081E3 File Offset: 0x000063E3
		public void SetCustomRenderScale(float value)
		{
			if (!this._customRenderScale.ApproximatelyEqualsTo(value, 1E-05f))
			{
				this._customRenderScale = value;
				if (this._latestWidth != -1 && this._latestHeight != -1)
				{
					this.SetTargetSize(this._latestWidth, this._latestHeight);
				}
			}
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00008224 File Offset: 0x00006424
		internal void BannerTableauContinuousRenderFunction(Texture sender, EventArgs e)
		{
			Scene scene = (Scene)sender.UserData;
			TableauView tableauView = sender.TableauView;
			if (scene == null)
			{
				tableauView.SetContinuousRendering(false);
				tableauView.SetDeleteAfterRendering(true);
				return;
			}
			scene.EnsurePostfxSystem();
			scene.SetDofMode(false);
			scene.SetMotionBlurMode(false);
			scene.SetBloom(false);
			scene.SetDynamicShadowmapCascadesRadiusMultiplier(0.31f);
			tableauView.SetRenderWithPostfx(false);
			tableauView.SetScene(scene);
			tableauView.SetCamera(this.CurrentCamera);
			tableauView.SetSceneUsesSkybox(false);
			tableauView.SetDeleteAfterRendering(false);
			tableauView.SetContinuousRendering(true);
			tableauView.SetDoNotRenderThisFrame(true);
			tableauView.SetClearColor(0U);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000082BF File Offset: 0x000064BF
		public void SetIsNineGrid(bool value)
		{
			this._isNineGrid = value;
			this._isDirty = true;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000082CF File Offset: 0x000064CF
		public void SetMeshIndexToUpdate(int value)
		{
			this._meshIndexToUpdate = value;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000082D8 File Offset: 0x000064D8
		public void SetUpdatePositionValueManual(Vec2 value)
		{
			if (this._currentMultiMesh.MeshCount >= 1 && this._meshIndexToUpdate >= 0 && this._meshIndexToUpdate < this._currentMultiMesh.MeshCount)
			{
				Mesh meshAtIndex = this._currentMultiMesh.GetMeshAtIndex(this._meshIndexToUpdate);
				MatrixFrame localFrame = meshAtIndex.GetLocalFrame();
				localFrame.origin.x = 0f;
				localFrame.origin.y = 0f;
				localFrame.origin.x = localFrame.origin.x + value.X / 1528f;
				localFrame.origin.y = localFrame.origin.y - value.Y / 1528f;
				meshAtIndex.SetLocalFrame(localFrame);
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00008390 File Offset: 0x00006590
		public void SetUpdateSizeValueManual(Vec2 value)
		{
			if (this._currentMultiMesh.MeshCount >= 1 && this._meshIndexToUpdate >= 0 && this._meshIndexToUpdate < this._currentMultiMesh.MeshCount)
			{
				Mesh meshAtIndex = this._currentMultiMesh.GetMeshAtIndex(this._meshIndexToUpdate);
				MatrixFrame localFrame = meshAtIndex.GetLocalFrame();
				float x = value.X / 1528f / meshAtIndex.GetBoundingBoxWidth();
				float y = value.Y / 1528f / meshAtIndex.GetBoundingBoxHeight();
				Vec3 eulerAngles = localFrame.rotation.GetEulerAngles();
				localFrame.rotation = Mat3.Identity;
				localFrame.rotation.ApplyEulerAngles(eulerAngles);
				localFrame.rotation.ApplyScaleLocal(new Vec3(x, y, 1f, -1f));
				meshAtIndex.SetLocalFrame(localFrame);
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00008460 File Offset: 0x00006660
		public void SetUpdateRotationValueManual(ValueTuple<float, bool> value)
		{
			if (this._currentMultiMesh.MeshCount >= 1 && this._meshIndexToUpdate >= 0 && this._meshIndexToUpdate < this._currentMultiMesh.MeshCount)
			{
				Mesh meshAtIndex = this._currentMultiMesh.GetMeshAtIndex(this._meshIndexToUpdate);
				MatrixFrame localFrame = meshAtIndex.GetLocalFrame();
				float a = value.Item1 * 2f * 3.1415927f;
				Vec3 scaleVector = localFrame.rotation.GetScaleVector();
				localFrame.rotation = Mat3.Identity;
				localFrame.rotation.RotateAboutUp(a);
				localFrame.rotation.ApplyScaleLocal(scaleVector);
				if (value.Item2)
				{
					localFrame.rotation.RotateAboutForward(3.1415927f);
				}
				meshAtIndex.SetLocalFrame(localFrame);
			}
		}

		// Token: 0x0400004C RID: 76
		private bool _isFinalized;

		// Token: 0x0400004D RID: 77
		private bool _isEnabled;

		// Token: 0x0400004E RID: 78
		private bool _isNineGrid;

		// Token: 0x0400004F RID: 79
		private bool _isDirty;

		// Token: 0x04000050 RID: 80
		private Banner _banner;

		// Token: 0x04000051 RID: 81
		private int _latestWidth = -1;

		// Token: 0x04000052 RID: 82
		private int _latestHeight = -1;

		// Token: 0x04000053 RID: 83
		private int _tableauSizeX;

		// Token: 0x04000054 RID: 84
		private int _tableauSizeY;

		// Token: 0x04000055 RID: 85
		private float RenderScale = 1f;

		// Token: 0x04000056 RID: 86
		private float _customRenderScale = 1f;

		// Token: 0x04000057 RID: 87
		private Scene _scene;

		// Token: 0x04000058 RID: 88
		private Camera _defaultCamera;

		// Token: 0x04000059 RID: 89
		private Camera _nineGridCamera;

		// Token: 0x0400005A RID: 90
		private MetaMesh _currentMultiMesh;

		// Token: 0x0400005B RID: 91
		private GameEntity _currentMeshEntity;

		// Token: 0x0400005C RID: 92
		private int _meshIndexToUpdate;
	}
}
