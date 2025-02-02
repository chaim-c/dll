using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000240 RID: 576
	public class VideoPlaybackState : GameState
	{
		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06001F3E RID: 7998 RVA: 0x0006F023 File Offset: 0x0006D223
		// (set) Token: 0x06001F3F RID: 7999 RVA: 0x0006F02B File Offset: 0x0006D22B
		public string VideoPath { get; private set; }

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06001F40 RID: 8000 RVA: 0x0006F034 File Offset: 0x0006D234
		// (set) Token: 0x06001F41 RID: 8001 RVA: 0x0006F03C File Offset: 0x0006D23C
		public string AudioPath { get; private set; }

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06001F42 RID: 8002 RVA: 0x0006F045 File Offset: 0x0006D245
		// (set) Token: 0x06001F43 RID: 8003 RVA: 0x0006F04D File Offset: 0x0006D24D
		public float FrameRate { get; private set; }

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06001F44 RID: 8004 RVA: 0x0006F056 File Offset: 0x0006D256
		// (set) Token: 0x06001F45 RID: 8005 RVA: 0x0006F05E File Offset: 0x0006D25E
		public string SubtitleFileBasePath { get; private set; }

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06001F46 RID: 8006 RVA: 0x0006F067 File Offset: 0x0006D267
		// (set) Token: 0x06001F47 RID: 8007 RVA: 0x0006F06F File Offset: 0x0006D26F
		public bool CanUserSkip { get; private set; }

		// Token: 0x06001F48 RID: 8008 RVA: 0x0006F078 File Offset: 0x0006D278
		public void SetStartingParameters(string videoPath, string audioPath, string subtitleFileBasePath, float frameRate = 30f, bool canUserSkip = true)
		{
			this.VideoPath = videoPath;
			this.AudioPath = audioPath;
			this.FrameRate = frameRate;
			this.SubtitleFileBasePath = subtitleFileBasePath;
			this.CanUserSkip = canUserSkip;
		}

		// Token: 0x06001F49 RID: 8009 RVA: 0x0006F09F File Offset: 0x0006D29F
		public void SetOnVideoFinisedDelegate(Action onVideoFinised)
		{
			this._onVideoFinised = onVideoFinised;
		}

		// Token: 0x06001F4A RID: 8010 RVA: 0x0006F0A8 File Offset: 0x0006D2A8
		public void OnVideoFinished()
		{
			Action onVideoFinised = this._onVideoFinised;
			if (onVideoFinised == null)
			{
				return;
			}
			onVideoFinised();
		}

		// Token: 0x04000B7D RID: 2941
		private Action _onVideoFinised;
	}
}
