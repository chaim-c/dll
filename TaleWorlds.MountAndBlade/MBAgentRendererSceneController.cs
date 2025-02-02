using System;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000196 RID: 406
	public class MBAgentRendererSceneController
	{
		// Token: 0x060014E3 RID: 5347 RVA: 0x0004F164 File Offset: 0x0004D364
		internal MBAgentRendererSceneController(UIntPtr pointer)
		{
			this._pointer = pointer;
		}

		// Token: 0x060014E4 RID: 5348 RVA: 0x0004F174 File Offset: 0x0004D374
		~MBAgentRendererSceneController()
		{
		}

		// Token: 0x060014E5 RID: 5349 RVA: 0x0004F19C File Offset: 0x0004D39C
		public void SetEnforcedVisibilityForAllAgents(Scene scene)
		{
			MBAPI.IMBAgentVisuals.SetEnforcedVisibilityForAllAgents(scene.Pointer, this._pointer);
		}

		// Token: 0x060014E6 RID: 5350 RVA: 0x0004F1B4 File Offset: 0x0004D3B4
		public static MBAgentRendererSceneController CreateNewAgentRendererSceneController(Scene scene, int maxRenderCount)
		{
			return new MBAgentRendererSceneController(MBAPI.IMBAgentVisuals.CreateAgentRendererSceneController(scene.Pointer, maxRenderCount));
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x0004F1CC File Offset: 0x0004D3CC
		public void SetDoTimerBasedForcedSkeletonUpdates(bool value)
		{
			MBAPI.IMBAgentVisuals.SetDoTimerBasedForcedSkeletonUpdates(this._pointer, value);
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x0004F1DF File Offset: 0x0004D3DF
		public static void DestructAgentRendererSceneController(Scene scene, MBAgentRendererSceneController rendererSceneController, bool deleteThisFrame)
		{
			MBAPI.IMBAgentVisuals.DestructAgentRendererSceneController(scene.Pointer, rendererSceneController._pointer, deleteThisFrame);
			rendererSceneController._pointer = UIntPtr.Zero;
		}

		// Token: 0x060014E9 RID: 5353 RVA: 0x0004F203 File Offset: 0x0004D403
		public static void ValidateAgentVisualsReseted(Scene scene, MBAgentRendererSceneController rendererSceneController)
		{
			MBAPI.IMBAgentVisuals.ValidateAgentVisualsReseted(scene.Pointer, rendererSceneController._pointer);
		}

		// Token: 0x040006F2 RID: 1778
		private UIntPtr _pointer;
	}
}
