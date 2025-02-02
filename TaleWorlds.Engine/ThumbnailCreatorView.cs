using System;
using TaleWorlds.DotNet;

namespace TaleWorlds.Engine
{
	// Token: 0x0200008F RID: 143
	[EngineClass("rglThumbnail_creator_view")]
	public sealed class ThumbnailCreatorView : View
	{
		// Token: 0x06000AE1 RID: 2785 RVA: 0x0000BEFF File Offset: 0x0000A0FF
		internal ThumbnailCreatorView(UIntPtr pointer) : base(pointer)
		{
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x0000BF08 File Offset: 0x0000A108
		[EngineCallback]
		internal static void OnThumbnailRenderComplete(string renderId, Texture renderTarget)
		{
			ThumbnailCreatorView.renderCallback(renderId, renderTarget);
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x0000BF16 File Offset: 0x0000A116
		public static ThumbnailCreatorView CreateThumbnailCreatorView()
		{
			return EngineApplicationInterface.IThumbnailCreatorView.CreateThumbnailCreatorView();
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x0000BF22 File Offset: 0x0000A122
		public void RegisterScene(Scene scene, bool usePostFx = true)
		{
			EngineApplicationInterface.IThumbnailCreatorView.RegisterScene(base.Pointer, scene.Pointer, usePostFx);
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x0000BF3B File Offset: 0x0000A13B
		public void RegisterEntity(Scene scene, Camera cam, Texture texture, GameEntity itemEntity, int allocationGroupIndex, string renderId = "")
		{
			EngineApplicationInterface.IThumbnailCreatorView.RegisterEntity(base.Pointer, scene.Pointer, cam.Pointer, texture.Pointer, itemEntity.Pointer, renderId, allocationGroupIndex);
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x0000BF6A File Offset: 0x0000A16A
		public void ClearRequests()
		{
			EngineApplicationInterface.IThumbnailCreatorView.ClearRequests(base.Pointer);
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x0000BF7C File Offset: 0x0000A17C
		public void CancelRequest(string renderID)
		{
			EngineApplicationInterface.IThumbnailCreatorView.CancelRequest(base.Pointer, renderID);
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0000BF90 File Offset: 0x0000A190
		public void RegisterEntityWithoutTexture(Scene scene, Camera camera, GameEntity entity, int width, int height, int allocationGroupIndex, string renderId = "", string debugName = "")
		{
			EngineApplicationInterface.IThumbnailCreatorView.RegisterEntityWithoutTexture(base.Pointer, scene.Pointer, camera.Pointer, entity.Pointer, width, height, renderId, debugName, allocationGroupIndex);
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x0000BFC9 File Offset: 0x0000A1C9
		public int GetNumberOfPendingRequests()
		{
			return EngineApplicationInterface.IThumbnailCreatorView.GetNumberOfPendingRequests(base.Pointer);
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x0000BFDB File Offset: 0x0000A1DB
		public bool IsMemoryCleared()
		{
			return EngineApplicationInterface.IThumbnailCreatorView.IsMemoryCleared(base.Pointer);
		}

		// Token: 0x040001B3 RID: 435
		public static ThumbnailCreatorView.OnThumbnailRenderCompleteDelegate renderCallback;

		// Token: 0x020000C8 RID: 200
		// (Invoke) Token: 0x06000CAA RID: 3242
		public delegate void OnThumbnailRenderCompleteDelegate(string renderId, Texture renderTarget);
	}
}
