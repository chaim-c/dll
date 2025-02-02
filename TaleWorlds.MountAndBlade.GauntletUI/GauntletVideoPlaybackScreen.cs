using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.View.Screens;
using TaleWorlds.MountAndBlade.ViewModelCollection.VideoPlayback;

namespace TaleWorlds.MountAndBlade.GauntletUI
{
	// Token: 0x02000011 RID: 17
	[GameStateScreen(typeof(VideoPlaybackState))]
	public class GauntletVideoPlaybackScreen : VideoPlaybackScreen
	{
		// Token: 0x0600008D RID: 141 RVA: 0x00006052 File Offset: 0x00004252
		public GauntletVideoPlaybackScreen(VideoPlaybackState videoPlaybackState) : base(videoPlaybackState)
		{
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000605C File Offset: 0x0000425C
		protected override void OnInitialize()
		{
			base.OnInitialize();
			string subtitleExtensionOfLanguage = LocalizedTextManager.GetSubtitleExtensionOfLanguage(BannerlordConfig.Language);
			string text = this._videoPlaybackState.SubtitleFileBasePath + "_" + subtitleExtensionOfLanguage + ".srt";
			List<SRTHelper.SubtitleItem> subtitles = null;
			if (!string.IsNullOrEmpty(this._videoPlaybackState.SubtitleFileBasePath))
			{
				if (File.Exists(text))
				{
					subtitles = SRTHelper.SrtParser.ParseStream(new FileStream(text, FileMode.Open, FileAccess.Read), Encoding.UTF8);
				}
				else
				{
					Debug.FailedAssert("No Subtitle file exists in path: " + text, "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.GauntletUI\\GauntletVideoPlaybackScreen.cs", "OnInitialize", 41);
				}
			}
			this._layer = new GauntletLayer(100002, "GauntletLayer", false);
			this._dataSource = new VideoPlaybackVM();
			this._layer.LoadMovie("VideoPlayer", this._dataSource);
			this._dataSource.SetSubtitles(subtitles);
			this._layer.InputRestrictions.SetInputRestrictions(false, InputUsageMask.All);
			base.AddLayer(this._layer);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00006145 File Offset: 0x00004345
		protected override void OnVideoPlaybackTick(float dt)
		{
			base.OnVideoPlaybackTick(dt);
			this._dataSource.Tick(this._totalElapsedTimeSinceVideoStart);
		}

		// Token: 0x0400006A RID: 106
		private GauntletLayer _layer;

		// Token: 0x0400006B RID: 107
		private VideoPlaybackVM _dataSource;
	}
}
