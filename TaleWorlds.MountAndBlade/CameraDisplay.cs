using System;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000319 RID: 793
	public class CameraDisplay : ScriptComponentBehavior
	{
		// Token: 0x06002AE4 RID: 10980 RVA: 0x000A633C File Offset: 0x000A453C
		private void BuildView()
		{
			this._sceneView = SceneView.CreateSceneView();
			this._myCamera = Camera.CreateCamera();
			this._sceneView.SetScene(base.GameEntity.Scene);
			this._sceneView.SetPostfxFromConfig();
			this._sceneView.SetRenderOption(View.ViewRenderOptions.ClearColor, false);
			this._sceneView.SetRenderOption(View.ViewRenderOptions.ClearDepth, true);
			this._sceneView.SetScale(new Vec2(0.2f, 0.2f));
		}

		// Token: 0x06002AE5 RID: 10981 RVA: 0x000A63B4 File Offset: 0x000A45B4
		private void SetCamera()
		{
			Vec2 realScreenResolution = Screen.RealScreenResolution;
			float aspectRatioXY = realScreenResolution.x / realScreenResolution.y;
			MatrixFrame globalFrame = base.GameEntity.GetGlobalFrame();
			this._myCamera.SetFovVertical(0.7853982f, aspectRatioXY, 0.2f, 200f);
			this._myCamera.Frame = globalFrame;
			this._sceneView.SetCamera(this._myCamera);
		}

		// Token: 0x06002AE6 RID: 10982 RVA: 0x000A6419 File Offset: 0x000A4619
		private void RenderCameraFrustrum()
		{
			this._myCamera.RenderFrustrum();
		}

		// Token: 0x06002AE7 RID: 10983 RVA: 0x000A6426 File Offset: 0x000A4626
		protected internal override void OnEditorInit()
		{
			this.BuildView();
		}

		// Token: 0x06002AE8 RID: 10984 RVA: 0x000A642E File Offset: 0x000A462E
		protected internal override void OnInit()
		{
			this.BuildView();
		}

		// Token: 0x06002AE9 RID: 10985 RVA: 0x000A6436 File Offset: 0x000A4636
		protected internal override void OnEditorTick(float dt)
		{
			base.OnEditorTick(dt);
			if (MBEditor.IsEntitySelected(base.GameEntity))
			{
				this.RenderCameraFrustrum();
				this._sceneView.SetEnable(true);
				return;
			}
			this._sceneView.SetEnable(false);
		}

		// Token: 0x06002AEA RID: 10986 RVA: 0x000A646B File Offset: 0x000A466B
		protected override void OnRemoved(int removeReason)
		{
			base.OnRemoved(removeReason);
			this._sceneView = null;
			this._myCamera = null;
		}

		// Token: 0x04001084 RID: 4228
		private Camera _myCamera;

		// Token: 0x04001085 RID: 4229
		private SceneView _sceneView;

		// Token: 0x04001086 RID: 4230
		public int renderOrder;
	}
}
