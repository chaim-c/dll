using System;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000032 RID: 50
	[ApplicationInterfaceBase]
	internal interface IVideoPlayerView
	{
		// Token: 0x0600044F RID: 1103
		[EngineMethod("create_video_player_view", false)]
		VideoPlayerView CreateVideoPlayerView();

		// Token: 0x06000450 RID: 1104
		[EngineMethod("play_video", false)]
		void PlayVideo(UIntPtr pointer, string videoFileName, string soundFileName, float framerate);

		// Token: 0x06000451 RID: 1105
		[EngineMethod("stop_video", false)]
		void StopVideo(UIntPtr pointer);

		// Token: 0x06000452 RID: 1106
		[EngineMethod("is_video_finished", false)]
		bool IsVideoFinished(UIntPtr pointer);

		// Token: 0x06000453 RID: 1107
		[EngineMethod("finalize", false)]
		void Finalize(UIntPtr pointer);
	}
}
