using System;
using System.Numerics;
using TaleWorlds.Library;
using TaleWorlds.ScreenSystem;

namespace TaleWorlds.Engine.Screens
{
	// Token: 0x0200009B RID: 155
	public class SceneLayer : ScreenLayer
	{
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000BBB RID: 3003 RVA: 0x0000D140 File Offset: 0x0000B340
		// (set) Token: 0x06000BBC RID: 3004 RVA: 0x0000D148 File Offset: 0x0000B348
		public bool ClearSceneOnFinalize { get; private set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000BBD RID: 3005 RVA: 0x0000D151 File Offset: 0x0000B351
		// (set) Token: 0x06000BBE RID: 3006 RVA: 0x0000D159 File Offset: 0x0000B359
		public bool AutoToggleSceneView { get; private set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000BBF RID: 3007 RVA: 0x0000D162 File Offset: 0x0000B362
		public SceneView SceneView
		{
			get
			{
				return this._sceneView;
			}
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x0000D16C File Offset: 0x0000B36C
		public SceneLayer(string categoryId = "SceneLayer", bool clearSceneOnFinalize = true, bool autoToggleSceneView = true) : base(-100, categoryId)
		{
			base.Name = "SceneLayer";
			this.ClearSceneOnFinalize = clearSceneOnFinalize;
			base.InputRestrictions.SetInputRestrictions(false, InputUsageMask.All);
			this._sceneView = SceneView.CreateSceneView();
			this.AutoToggleSceneView = autoToggleSceneView;
			base.IsFocusLayer = true;
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x0000D1BA File Offset: 0x0000B3BA
		protected override void OnActivate()
		{
			base.OnActivate();
			if (this.AutoToggleSceneView)
			{
				this._sceneView.SetEnable(true);
			}
			ScreenManager.TrySetFocus(this);
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x0000D1DC File Offset: 0x0000B3DC
		protected override void OnDeactivate()
		{
			base.OnDeactivate();
			if (this.AutoToggleSceneView)
			{
				this._sceneView.SetEnable(false);
			}
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x0000D1F8 File Offset: 0x0000B3F8
		protected override void OnFinalize()
		{
			if (this.ClearSceneOnFinalize)
			{
				this._sceneView.ClearAll(true, true);
			}
			base.OnFinalize();
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x0000D215 File Offset: 0x0000B415
		public void SetScene(Scene scene)
		{
			this._sceneView.SetScene(scene);
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x0000D223 File Offset: 0x0000B423
		public void SetRenderWithPostfx(bool value)
		{
			this._sceneView.SetRenderWithPostfx(value);
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x0000D231 File Offset: 0x0000B431
		public void SetPostfxConfigParams(int value)
		{
			this._sceneView.SetPostfxConfigParams(value);
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x0000D23F File Offset: 0x0000B43F
		public void SetCamera(Camera camera)
		{
			this._sceneView.SetCamera(camera);
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x0000D24D File Offset: 0x0000B44D
		public void SetPostfxFromConfig()
		{
			this._sceneView.SetPostfxFromConfig();
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x0000D25A File Offset: 0x0000B45A
		public Vec2 WorldPointToScreenPoint(Vec3 position)
		{
			return this._sceneView.WorldPointToScreenPoint(position);
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x0000D268 File Offset: 0x0000B468
		public Vec2 ScreenPointToViewportPoint(Vec2 position)
		{
			return this._sceneView.ScreenPointToViewportPoint(position);
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x0000D276 File Offset: 0x0000B476
		public bool ProjectedMousePositionOnGround(out Vec3 groundPosition, out Vec3 groundNormal, bool mouseVisible, BodyFlags excludeBodyOwnerFlags, bool checkOccludedSurface)
		{
			return this._sceneView.ProjectedMousePositionOnGround(out groundPosition, out groundNormal, mouseVisible, excludeBodyOwnerFlags, checkOccludedSurface);
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x0000D28A File Offset: 0x0000B48A
		public void TranslateMouse(ref Vec3 worldMouseNear, ref Vec3 worldMouseFar, float maxDistance = -1f)
		{
			this._sceneView.TranslateMouse(ref worldMouseNear, ref worldMouseFar, maxDistance);
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x0000D29A File Offset: 0x0000B49A
		public void SetSceneUsesSkybox(bool value)
		{
			this._sceneView.SetSceneUsesSkybox(value);
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x0000D2A8 File Offset: 0x0000B4A8
		public void SetSceneUsesShadows(bool value)
		{
			this._sceneView.SetSceneUsesShadows(value);
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x0000D2B6 File Offset: 0x0000B4B6
		public void SetSceneUsesContour(bool value)
		{
			this._sceneView.SetSceneUsesContour(value);
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x0000D2C4 File Offset: 0x0000B4C4
		public void SetShadowmapResolutionMultiplier(float value)
		{
			this._sceneView.SetShadowmapResolutionMultiplier(value);
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x0000D2D2 File Offset: 0x0000B4D2
		public void SetFocusedShadowmap(bool enable, ref Vec3 center, float radius)
		{
			this._sceneView.SetFocusedShadowmap(enable, ref center, radius);
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x0000D2E2 File Offset: 0x0000B4E2
		public void DoNotClear(bool value)
		{
			this._sceneView.DoNotClear(value);
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x0000D2F0 File Offset: 0x0000B4F0
		public bool ReadyToRender()
		{
			return this._sceneView.ReadyToRender();
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x0000D2FD File Offset: 0x0000B4FD
		public void SetCleanScreenUntilLoadingDone(bool value)
		{
			this._sceneView.SetCleanScreenUntilLoadingDone(value);
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x0000D30B File Offset: 0x0000B50B
		public void ClearAll()
		{
			this._sceneView.ClearAll(true, true);
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x0000D31A File Offset: 0x0000B51A
		public void ClearRuntimeGPUMemory(bool remove_terrain)
		{
			this._sceneView.ClearAll(false, remove_terrain);
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x0000D329 File Offset: 0x0000B529
		protected override void RefreshGlobalOrder(ref int currentOrder)
		{
			this._sceneView.SetRenderOrder(currentOrder);
			currentOrder++;
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x0000D33E File Offset: 0x0000B53E
		public override bool HitTest(Vector2 position)
		{
			return true;
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x0000D341 File Offset: 0x0000B541
		public override bool HitTest()
		{
			return true;
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x0000D344 File Offset: 0x0000B544
		public override bool FocusTest()
		{
			return true;
		}

		// Token: 0x040001FD RID: 509
		private SceneView _sceneView;
	}
}
