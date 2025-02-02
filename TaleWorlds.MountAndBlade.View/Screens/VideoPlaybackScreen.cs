using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.ScreenSystem;

namespace TaleWorlds.MountAndBlade.View.Screens
{
	// Token: 0x02000036 RID: 54
	public class VideoPlaybackScreen : ScreenBase, IGameStateListener
	{
		// Token: 0x0600026F RID: 623 RVA: 0x00016F7E File Offset: 0x0001517E
		public VideoPlaybackScreen(VideoPlaybackState videoPlaybackState)
		{
			this._videoPlaybackState = videoPlaybackState;
			this._videoPlayerView = VideoPlayerView.CreateVideoPlayerView();
			this._videoPlayerView.SetRenderOrder(-10000);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00016FA8 File Offset: 0x000151A8
		protected sealed override void OnFrameTick(float dt)
		{
			this._totalElapsedTimeSinceVideoStart += dt;
			base.OnFrameTick(dt);
			if (this._videoPlayerView != null && this._videoPlaybackState != null)
			{
				if (this._videoPlaybackState.CanUserSkip && (Input.IsKeyReleased(InputKey.Escape) || Input.IsKeyReleased(InputKey.ControllerROption)))
				{
					this._videoPlayerView.StopVideo();
				}
				if (this._videoPlayerView.IsVideoFinished())
				{
					this._videoPlaybackState.OnVideoFinished();
					this._videoPlayerView.SetEnable(false);
					this._videoPlayerView = null;
				}
				if (ScreenManager.TopScreen == this)
				{
					this.OnVideoPlaybackTick(dt);
				}
			}
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00017046 File Offset: 0x00015246
		protected virtual void OnVideoPlaybackTick(float dt)
		{
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00017048 File Offset: 0x00015248
		void IGameStateListener.OnInitialize()
		{
			this._videoPlayerView.PlayVideo(this._videoPlaybackState.VideoPath, this._videoPlaybackState.AudioPath, this._videoPlaybackState.FrameRate);
			LoadingWindow.DisableGlobalLoadingWindow();
			Utilities.DisableGlobalLoadingWindow();
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00017080 File Offset: 0x00015280
		void IGameStateListener.OnFinalize()
		{
			this._videoPlayerView.FinalizePlayer();
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0001708D File Offset: 0x0001528D
		void IGameStateListener.OnActivate()
		{
			base.OnActivate();
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00017095 File Offset: 0x00015295
		void IGameStateListener.OnDeactivate()
		{
			base.OnDeactivate();
		}

		// Token: 0x0400019B RID: 411
		protected VideoPlaybackState _videoPlaybackState;

		// Token: 0x0400019C RID: 412
		protected VideoPlayerView _videoPlayerView;

		// Token: 0x0400019D RID: 413
		protected float _totalElapsedTimeSinceVideoStart;
	}
}
