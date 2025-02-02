using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.ExtraWidgets;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.KillFeed
{
	// Token: 0x020000B7 RID: 183
	public class MultiplayerDuelKillFeedItemWidget : MultiplayerGeneralKillFeedItemWidget
	{
		// Token: 0x06000998 RID: 2456 RVA: 0x0001B39E File Offset: 0x0001959E
		public MultiplayerDuelKillFeedItemWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000999 RID: 2457 RVA: 0x0001B3A7 File Offset: 0x000195A7
		// (set) Token: 0x0600099A RID: 2458 RVA: 0x0001B3B0 File Offset: 0x000195B0
		[Editor(false)]
		public bool IsEndOfDuel
		{
			get
			{
				return this._isEndOfDuel;
			}
			set
			{
				if (value != this._isEndOfDuel)
				{
					this._isEndOfDuel = value;
					base.OnPropertyChanged(value, "IsEndOfDuel");
					if (value)
					{
						BrushWidget background = this.Background;
						if (background != null)
						{
							background.SetState("EndOfDuel");
						}
						BrushWidget victimCompassBackground = this.VictimCompassBackground;
						if (victimCompassBackground != null)
						{
							victimCompassBackground.SetState("EndOfDuel");
						}
						BrushWidget murdererCompassBackground = this.MurdererCompassBackground;
						if (murdererCompassBackground != null)
						{
							murdererCompassBackground.SetState("EndOfDuel");
						}
						ScrollingRichTextWidget victimNameText = this.VictimNameText;
						if (victimNameText != null)
						{
							victimNameText.SetState("EndOfDuel");
						}
						ScrollingRichTextWidget murdererNameText = this.MurdererNameText;
						if (murdererNameText != null)
						{
							murdererNameText.SetState("EndOfDuel");
						}
						TextWidget victimScoreText = this.VictimScoreText;
						if (victimScoreText != null)
						{
							victimScoreText.SetState("EndOfDuel");
						}
						TextWidget murdererScoreText = this.MurdererScoreText;
						if (murdererScoreText == null)
						{
							return;
						}
						murdererScoreText.SetState("EndOfDuel");
					}
				}
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x0600099B RID: 2459 RVA: 0x0001B47B File Offset: 0x0001967B
		// (set) Token: 0x0600099C RID: 2460 RVA: 0x0001B483 File Offset: 0x00019683
		[Editor(false)]
		public BrushWidget Background
		{
			get
			{
				return this._background;
			}
			set
			{
				if (value != this._background)
				{
					this._background = value;
					base.OnPropertyChanged<BrushWidget>(value, "Background");
				}
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x0600099D RID: 2461 RVA: 0x0001B4A1 File Offset: 0x000196A1
		// (set) Token: 0x0600099E RID: 2462 RVA: 0x0001B4A9 File Offset: 0x000196A9
		[Editor(false)]
		public BrushWidget VictimCompassBackground
		{
			get
			{
				return this._victimCompassBackground;
			}
			set
			{
				if (value != this._victimCompassBackground)
				{
					this._victimCompassBackground = value;
					base.OnPropertyChanged<BrushWidget>(value, "VictimCompassBackground");
				}
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x0600099F RID: 2463 RVA: 0x0001B4C7 File Offset: 0x000196C7
		// (set) Token: 0x060009A0 RID: 2464 RVA: 0x0001B4CF File Offset: 0x000196CF
		[Editor(false)]
		public BrushWidget MurdererCompassBackground
		{
			get
			{
				return this._murdererCompassBackground;
			}
			set
			{
				if (value != this._murdererCompassBackground)
				{
					this._murdererCompassBackground = value;
					base.OnPropertyChanged<BrushWidget>(value, "MurdererCompassBackground");
				}
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x060009A1 RID: 2465 RVA: 0x0001B4ED File Offset: 0x000196ED
		// (set) Token: 0x060009A2 RID: 2466 RVA: 0x0001B4F5 File Offset: 0x000196F5
		[Editor(false)]
		public ScrollingRichTextWidget VictimNameText
		{
			get
			{
				return this._victimNameText;
			}
			set
			{
				if (value != this._victimNameText)
				{
					this._victimNameText = value;
					base.OnPropertyChanged<ScrollingRichTextWidget>(value, "VictimNameText");
				}
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x060009A3 RID: 2467 RVA: 0x0001B513 File Offset: 0x00019713
		// (set) Token: 0x060009A4 RID: 2468 RVA: 0x0001B51B File Offset: 0x0001971B
		[Editor(false)]
		public ScrollingRichTextWidget MurdererNameText
		{
			get
			{
				return this._murdererNameText;
			}
			set
			{
				if (value != this._murdererNameText)
				{
					this._murdererNameText = value;
					base.OnPropertyChanged<ScrollingRichTextWidget>(value, "MurdererNameText");
				}
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x060009A5 RID: 2469 RVA: 0x0001B539 File Offset: 0x00019739
		// (set) Token: 0x060009A6 RID: 2470 RVA: 0x0001B541 File Offset: 0x00019741
		[Editor(false)]
		public TextWidget VictimScoreText
		{
			get
			{
				return this._victimScoreText;
			}
			set
			{
				if (value != this._victimScoreText)
				{
					this._victimScoreText = value;
					base.OnPropertyChanged<TextWidget>(value, "VictimScoreText");
				}
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x060009A7 RID: 2471 RVA: 0x0001B55F File Offset: 0x0001975F
		// (set) Token: 0x060009A8 RID: 2472 RVA: 0x0001B567 File Offset: 0x00019767
		[Editor(false)]
		public TextWidget MurdererScoreText
		{
			get
			{
				return this._murdererScoreText;
			}
			set
			{
				if (value != this._murdererScoreText)
				{
					this._murdererScoreText = value;
					base.OnPropertyChanged<TextWidget>(value, "MurdererScoreText");
				}
			}
		}

		// Token: 0x0400045D RID: 1117
		private const string EndOfDuelState = "EndOfDuel";

		// Token: 0x0400045E RID: 1118
		private bool _isEndOfDuel;

		// Token: 0x0400045F RID: 1119
		private BrushWidget _background;

		// Token: 0x04000460 RID: 1120
		private BrushWidget _victimCompassBackground;

		// Token: 0x04000461 RID: 1121
		private BrushWidget _murdererCompassBackground;

		// Token: 0x04000462 RID: 1122
		private ScrollingRichTextWidget _victimNameText;

		// Token: 0x04000463 RID: 1123
		private ScrollingRichTextWidget _murdererNameText;

		// Token: 0x04000464 RID: 1124
		private TextWidget _victimScoreText;

		// Token: 0x04000465 RID: 1125
		private TextWidget _murdererScoreText;
	}
}
