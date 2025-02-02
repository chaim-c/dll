using System;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000080 RID: 128
	[EngineClass("rglScene_view")]
	public class SceneView : View
	{
		// Token: 0x060009DB RID: 2523 RVA: 0x0000A91D File Offset: 0x00008B1D
		internal SceneView(UIntPtr meshPointer) : base(meshPointer)
		{
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0000A926 File Offset: 0x00008B26
		public static SceneView CreateSceneView()
		{
			return EngineApplicationInterface.ISceneView.CreateSceneView();
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0000A932 File Offset: 0x00008B32
		public void SetScene(Scene scene)
		{
			EngineApplicationInterface.ISceneView.SetScene(base.Pointer, scene.Pointer);
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x0000A94A File Offset: 0x00008B4A
		public void SetAcceptGlobalDebugRenderObjects(bool value)
		{
			EngineApplicationInterface.ISceneView.SetAcceptGlobalDebugRenderObjects(base.Pointer, value);
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x0000A95D File Offset: 0x00008B5D
		public void SetRenderWithPostfx(bool value)
		{
			EngineApplicationInterface.ISceneView.SetRenderWithPostfx(base.Pointer, value);
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x0000A970 File Offset: 0x00008B70
		public void SetPostfxConfigParams(int value)
		{
			EngineApplicationInterface.ISceneView.SetPostfxConfigParams(base.Pointer, value);
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x0000A983 File Offset: 0x00008B83
		public void SetForceShaderCompilation(bool value)
		{
			EngineApplicationInterface.ISceneView.SetForceShaderCompilation(base.Pointer, value);
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x0000A996 File Offset: 0x00008B96
		public bool CheckSceneReadyToRender()
		{
			return EngineApplicationInterface.ISceneView.CheckSceneReadyToRender(base.Pointer);
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x0000A9A8 File Offset: 0x00008BA8
		public void SetCamera(Camera camera)
		{
			EngineApplicationInterface.ISceneView.SetCamera(base.Pointer, camera.Pointer);
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x0000A9C0 File Offset: 0x00008BC0
		public void SetResolutionScaling(bool value)
		{
			EngineApplicationInterface.ISceneView.SetResolutionScaling(base.Pointer, value);
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x0000A9D3 File Offset: 0x00008BD3
		public void SetPostfxFromConfig()
		{
			EngineApplicationInterface.ISceneView.SetPostfxFromConfig(base.Pointer);
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x0000A9E5 File Offset: 0x00008BE5
		public Vec2 WorldPointToScreenPoint(Vec3 position)
		{
			return EngineApplicationInterface.ISceneView.WorldPointToScreenPoint(base.Pointer, position);
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x0000A9F8 File Offset: 0x00008BF8
		public Vec2 ScreenPointToViewportPoint(Vec2 position)
		{
			return EngineApplicationInterface.ISceneView.ScreenPointToViewportPoint(base.Pointer, position.x, position.y);
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x0000AA16 File Offset: 0x00008C16
		public bool ProjectedMousePositionOnGround(out Vec3 groundPosition, out Vec3 groundNormal, bool mouseVisible, BodyFlags excludeBodyOwnerFlags, bool checkOccludedSurface)
		{
			return EngineApplicationInterface.ISceneView.ProjectedMousePositionOnGround(base.Pointer, out groundPosition, out groundNormal, mouseVisible, excludeBodyOwnerFlags, checkOccludedSurface);
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x0000AA2F File Offset: 0x00008C2F
		public void TranslateMouse(ref Vec3 worldMouseNear, ref Vec3 worldMouseFar, float maxDistance = -1f)
		{
			EngineApplicationInterface.ISceneView.TranslateMouse(base.Pointer, ref worldMouseNear, ref worldMouseFar, maxDistance);
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x0000AA44 File Offset: 0x00008C44
		public void SetSceneUsesSkybox(bool value)
		{
			EngineApplicationInterface.ISceneView.SetSceneUsesSkybox(base.Pointer, value);
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x0000AA57 File Offset: 0x00008C57
		public void SetSceneUsesShadows(bool value)
		{
			EngineApplicationInterface.ISceneView.SetSceneUsesShadows(base.Pointer, value);
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x0000AA6A File Offset: 0x00008C6A
		public void SetSceneUsesContour(bool value)
		{
			EngineApplicationInterface.ISceneView.SetSceneUsesContour(base.Pointer, value);
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x0000AA7D File Offset: 0x00008C7D
		public void DoNotClear(bool value)
		{
			EngineApplicationInterface.ISceneView.DoNotClear(base.Pointer, value);
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x0000AA90 File Offset: 0x00008C90
		public void AddClearTask(bool clearOnlySceneview = false)
		{
			EngineApplicationInterface.ISceneView.AddClearTask(base.Pointer, clearOnlySceneview);
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x0000AAA3 File Offset: 0x00008CA3
		public bool ReadyToRender()
		{
			return EngineApplicationInterface.ISceneView.ReadyToRender(base.Pointer);
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x0000AAB5 File Offset: 0x00008CB5
		public void SetClearAndDisableAfterSucessfullRender(bool value)
		{
			EngineApplicationInterface.ISceneView.SetClearAndDisableAfterSucessfullRender(base.Pointer, value);
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x0000AAC8 File Offset: 0x00008CC8
		public void SetClearGbuffer(bool value)
		{
			EngineApplicationInterface.ISceneView.SetClearGbuffer(base.Pointer, value);
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x0000AADB File Offset: 0x00008CDB
		public void SetShadowmapResolutionMultiplier(float value)
		{
			EngineApplicationInterface.ISceneView.SetShadowmapResolutionMultiplier(base.Pointer, value);
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x0000AAEE File Offset: 0x00008CEE
		public void SetPointlightResolutionMultiplier(float value)
		{
			EngineApplicationInterface.ISceneView.SetPointlightResolutionMultiplier(base.Pointer, value);
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x0000AB01 File Offset: 0x00008D01
		public void SetCleanScreenUntilLoadingDone(bool value)
		{
			EngineApplicationInterface.ISceneView.SetCleanScreenUntilLoadingDone(base.Pointer, value);
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x0000AB14 File Offset: 0x00008D14
		public void ClearAll(bool clearScene, bool removeTerrain)
		{
			EngineApplicationInterface.ISceneView.ClearAll(base.Pointer, clearScene, removeTerrain);
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x0000AB28 File Offset: 0x00008D28
		public void SetFocusedShadowmap(bool enable, ref Vec3 center, float radius)
		{
			EngineApplicationInterface.ISceneView.SetFocusedShadowmap(base.Pointer, enable, ref center, radius);
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x0000AB3D File Offset: 0x00008D3D
		public Scene GetScene()
		{
			return EngineApplicationInterface.ISceneView.GetScene(base.Pointer);
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x0000AB50 File Offset: 0x00008D50
		public bool RayCastForClosestEntityOrTerrain(Vec3 sourcePoint, Vec3 targetPoint, out float collisionDistance, out Vec3 closestPoint, float rayThickness = 0.01f, BodyFlags excludeBodyFlags = BodyFlags.CommonFocusRayCastExcludeFlags)
		{
			collisionDistance = float.NaN;
			closestPoint = Vec3.Invalid;
			UIntPtr zero = UIntPtr.Zero;
			return EngineApplicationInterface.ISceneView.RayCastForClosestEntityOrTerrain(base.Pointer, ref sourcePoint, ref targetPoint, rayThickness, ref collisionDistance, ref closestPoint, ref zero, excludeBodyFlags);
		}
	}
}
