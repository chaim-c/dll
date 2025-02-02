using System;
using TaleWorlds.DotNet;

namespace TaleWorlds.Engine
{
	// Token: 0x02000095 RID: 149
	[EngineClass("rglVideo_player_view")]
	public sealed class VideoPlayerView : View
	{
		// Token: 0x06000B87 RID: 2951 RVA: 0x0000CB46 File Offset: 0x0000AD46
		internal VideoPlayerView(UIntPtr meshPointer) : base(meshPointer)
		{
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x0000CB4F File Offset: 0x0000AD4F
		public static VideoPlayerView CreateVideoPlayerView()
		{
			return EngineApplicationInterface.IVideoPlayerView.CreateVideoPlayerView();
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x0000CB5B File Offset: 0x0000AD5B
		public void PlayVideo(string videoFileName, string soundFileName, float framerate)
		{
			EngineApplicationInterface.IVideoPlayerView.PlayVideo(base.Pointer, videoFileName, soundFileName, framerate);
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x0000CB70 File Offset: 0x0000AD70
		public void StopVideo()
		{
			EngineApplicationInterface.IVideoPlayerView.StopVideo(base.Pointer);
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x0000CB82 File Offset: 0x0000AD82
		public bool IsVideoFinished()
		{
			return EngineApplicationInterface.IVideoPlayerView.IsVideoFinished(base.Pointer);
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x0000CB94 File Offset: 0x0000AD94
		public void FinalizePlayer()
		{
			EngineApplicationInterface.IVideoPlayerView.Finalize(base.Pointer);
		}
	}
}
