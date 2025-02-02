using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.ExtraWidgets;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Scoreboard
{
	// Token: 0x02000051 RID: 81
	public class ScoreboardScreenWidget : Widget
	{
		// Token: 0x06000447 RID: 1095 RVA: 0x0000DED8 File Offset: 0x0000C0D8
		public ScoreboardScreenWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0000DEE4 File Offset: 0x0000C0E4
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			if (this.ScrollablePanel != null && this.ScrollGradient != null)
			{
				this.ScrollGradient.IsVisible = (this.ScrollablePanel.InnerPanel.Size.Y > this.ScrollablePanel.ClipRect.Size.Y);
			}
			if (!this._isAnimationActive && this.ShowScoreboard && this.IsOver)
			{
				this.StartBattleResultAnimation();
			}
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0000DF60 File Offset: 0x0000C160
		private void UpdateControlButtonsPanel()
		{
			this._controlButtonsPanel.IsVisible = (this.ShowScoreboard || this.IsMainCharacterDead);
			this.InputKeysPanel.IsVisible = (!this.ShowScoreboard && !this.IsSimulation && this.IsMainCharacterDead);
			this.ShowMouseIconWidget.IsVisible = (!this.IsMouseEnabled && this.ShowScoreboard && !this.IsSimulation && !this.IsMainCharacterDead);
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0000DFDC File Offset: 0x0000C1DC
		private void UpdateControlButtons()
		{
			this._fastForwardWidget.IsVisible = ((this.IsMainCharacterDead || this.IsSimulation) && this.ShowScoreboard);
			this._quitButton.IsVisible = this.ShowScoreboard;
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0000E014 File Offset: 0x0000C214
		private void StartBattleResultAnimation()
		{
			this._isAnimationActive = true;
			ScoreboardBattleRewardsWidget battleRewardsWidget = this.BattleRewardsWidget;
			if (battleRewardsWidget != null)
			{
				battleRewardsWidget.StartAnimation();
			}
			DelayedStateChanger shieldStateChanger = this.ShieldStateChanger;
			if (shieldStateChanger != null)
			{
				shieldStateChanger.Start();
			}
			DelayedStateChanger titleStateChanger = this.TitleStateChanger;
			if (titleStateChanger != null)
			{
				titleStateChanger.Start();
			}
			DelayedStateChanger titleBackgroundStateChanger = this.TitleBackgroundStateChanger;
			if (titleBackgroundStateChanger != null)
			{
				titleBackgroundStateChanger.Start();
			}
			if (this.BattleResult == 0)
			{
				DelayedStateChanger flagsDefeat = this.FlagsDefeat;
				if (flagsDefeat == null)
				{
					return;
				}
				flagsDefeat.Start();
				return;
			}
			else
			{
				if (this.BattleResult != 1)
				{
					if (this.BattleResult == 2)
					{
						DelayedStateChanger flagsRetreat = this.FlagsRetreat;
						if (flagsRetreat == null)
						{
							return;
						}
						flagsRetreat.Start();
					}
					return;
				}
				DelayedStateChanger flagsSuccess = this.FlagsSuccess;
				if (flagsSuccess == null)
				{
					return;
				}
				flagsSuccess.Start();
				return;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x0000E0B8 File Offset: 0x0000C2B8
		// (set) Token: 0x0600044D RID: 1101 RVA: 0x0000E0C0 File Offset: 0x0000C2C0
		[Editor(false)]
		public bool ShowScoreboard
		{
			get
			{
				return this._showScoreboard;
			}
			set
			{
				if (this._showScoreboard != value)
				{
					this._showScoreboard = value;
					base.OnPropertyChanged(value, "ShowScoreboard");
					this.UpdateControlButtonsPanel();
					this.UpdateControlButtons();
				}
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x0000E0EA File Offset: 0x0000C2EA
		// (set) Token: 0x0600044F RID: 1103 RVA: 0x0000E0F2 File Offset: 0x0000C2F2
		[Editor(false)]
		public bool IsOver
		{
			get
			{
				return this._isOver;
			}
			set
			{
				if (this._isOver != value)
				{
					this._isOver = value;
					base.OnPropertyChanged(value, "IsOver");
					this.UpdateControlButtons();
				}
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x0000E116 File Offset: 0x0000C316
		// (set) Token: 0x06000451 RID: 1105 RVA: 0x0000E11E File Offset: 0x0000C31E
		[Editor(false)]
		public int BattleResult
		{
			get
			{
				return this._battleResult;
			}
			set
			{
				if (this._battleResult != value)
				{
					this._battleResult = value;
					base.OnPropertyChanged(value, "BattleResult");
				}
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x0000E13C File Offset: 0x0000C33C
		// (set) Token: 0x06000453 RID: 1107 RVA: 0x0000E144 File Offset: 0x0000C344
		[Editor(false)]
		public bool IsMainCharacterDead
		{
			get
			{
				return this._isMainCharacterDead;
			}
			set
			{
				if (this._isMainCharacterDead != value)
				{
					this._isMainCharacterDead = value;
					base.OnPropertyChanged(value, "IsMainCharacterDead");
					this.UpdateControlButtonsPanel();
					this.UpdateControlButtons();
				}
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x0000E16E File Offset: 0x0000C36E
		// (set) Token: 0x06000455 RID: 1109 RVA: 0x0000E176 File Offset: 0x0000C376
		[Editor(false)]
		public bool IsSimulation
		{
			get
			{
				return this._isSimulation;
			}
			set
			{
				if (this._isSimulation != value)
				{
					this._isSimulation = value;
					base.OnPropertyChanged(value, "IsSimulation");
					this.UpdateControlButtonsPanel();
					this.UpdateControlButtons();
				}
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x0000E1A0 File Offset: 0x0000C3A0
		// (set) Token: 0x06000457 RID: 1111 RVA: 0x0000E1A8 File Offset: 0x0000C3A8
		[Editor(false)]
		public bool IsMouseEnabled
		{
			get
			{
				return this._isMouseEnabled;
			}
			set
			{
				if (this._isMouseEnabled != value)
				{
					this._isMouseEnabled = value;
					base.OnPropertyChanged(value, "IsMouseEnabled");
					this.UpdateControlButtonsPanel();
				}
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x0000E1CC File Offset: 0x0000C3CC
		// (set) Token: 0x06000459 RID: 1113 RVA: 0x0000E1D4 File Offset: 0x0000C3D4
		[Editor(false)]
		public ScrollablePanel ScrollablePanel
		{
			get
			{
				return this._scrollablePanel;
			}
			set
			{
				if (this._scrollablePanel != value)
				{
					this._scrollablePanel = value;
					base.OnPropertyChanged<ScrollablePanel>(value, "ScrollablePanel");
				}
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x0000E1F2 File Offset: 0x0000C3F2
		// (set) Token: 0x0600045B RID: 1115 RVA: 0x0000E1FA File Offset: 0x0000C3FA
		[Editor(false)]
		public Widget ScrollGradient
		{
			get
			{
				return this._scrollGradient;
			}
			set
			{
				if (this._scrollGradient != value)
				{
					this._scrollGradient = value;
					base.OnPropertyChanged<Widget>(value, "ScrollGradient");
				}
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x0000E218 File Offset: 0x0000C418
		// (set) Token: 0x0600045D RID: 1117 RVA: 0x0000E220 File Offset: 0x0000C420
		[Editor(false)]
		public Widget ControlButtonsPanel
		{
			get
			{
				return this._controlButtonsPanel;
			}
			set
			{
				if (this._controlButtonsPanel != value)
				{
					this._controlButtonsPanel = value;
					base.OnPropertyChanged<Widget>(value, "ControlButtonsPanel");
				}
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x0000E23E File Offset: 0x0000C43E
		// (set) Token: 0x0600045F RID: 1119 RVA: 0x0000E246 File Offset: 0x0000C446
		[Editor(false)]
		public ListPanel InputKeysPanel
		{
			get
			{
				return this._inputKeysPanel;
			}
			set
			{
				if (this._inputKeysPanel != value)
				{
					this._inputKeysPanel = value;
					base.OnPropertyChanged<ListPanel>(value, "InputKeysPanel");
				}
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x0000E264 File Offset: 0x0000C464
		// (set) Token: 0x06000461 RID: 1121 RVA: 0x0000E26C File Offset: 0x0000C46C
		[Editor(false)]
		public Widget ShowMouseIconWidget
		{
			get
			{
				return this._showMouseIconWidget;
			}
			set
			{
				if (this._showMouseIconWidget != value)
				{
					this._showMouseIconWidget = value;
					base.OnPropertyChanged<Widget>(value, "ShowMouseIconWidget");
				}
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x0000E28A File Offset: 0x0000C48A
		// (set) Token: 0x06000463 RID: 1123 RVA: 0x0000E292 File Offset: 0x0000C492
		[Editor(false)]
		public Widget FastForwardWidget
		{
			get
			{
				return this._fastForwardWidget;
			}
			set
			{
				if (this._fastForwardWidget != value)
				{
					this._fastForwardWidget = value;
					base.OnPropertyChanged<Widget>(value, "FastForwardWidget");
				}
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x0000E2B0 File Offset: 0x0000C4B0
		// (set) Token: 0x06000465 RID: 1125 RVA: 0x0000E2B8 File Offset: 0x0000C4B8
		[Editor(false)]
		public ButtonWidget QuitButton
		{
			get
			{
				return this._quitButton;
			}
			set
			{
				if (this._quitButton != value)
				{
					this._quitButton = value;
					base.OnPropertyChanged<ButtonWidget>(value, "QuitButton");
				}
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x0000E2D6 File Offset: 0x0000C4D6
		// (set) Token: 0x06000467 RID: 1127 RVA: 0x0000E2DE File Offset: 0x0000C4DE
		[Editor(false)]
		public ButtonWidget ShowScoreboardToggle
		{
			get
			{
				return this._showScoreboardToggle;
			}
			set
			{
				if (this._showScoreboardToggle != value)
				{
					this._showScoreboardToggle = value;
					base.OnPropertyChanged<ButtonWidget>(value, "ShowScoreboardToggle");
				}
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x0000E2FC File Offset: 0x0000C4FC
		// (set) Token: 0x06000469 RID: 1129 RVA: 0x0000E304 File Offset: 0x0000C504
		[Editor(false)]
		public ScoreboardBattleRewardsWidget BattleRewardsWidget
		{
			get
			{
				return this._battleRewardsWidget;
			}
			set
			{
				if (this._battleRewardsWidget != value)
				{
					this._battleRewardsWidget = value;
					base.OnPropertyChanged<ScoreboardBattleRewardsWidget>(value, "BattleRewardsWidget");
				}
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x0000E322 File Offset: 0x0000C522
		// (set) Token: 0x0600046B RID: 1131 RVA: 0x0000E32A File Offset: 0x0000C52A
		[Editor(false)]
		public DelayedStateChanger FlagsSuccess
		{
			get
			{
				return this._flagsSuccess;
			}
			set
			{
				if (this._flagsSuccess != value)
				{
					this._flagsSuccess = value;
					base.OnPropertyChanged<DelayedStateChanger>(value, "FlagsSuccess");
				}
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x0000E348 File Offset: 0x0000C548
		// (set) Token: 0x0600046D RID: 1133 RVA: 0x0000E350 File Offset: 0x0000C550
		[Editor(false)]
		public DelayedStateChanger FlagsRetreat
		{
			get
			{
				return this._flagsRetreat;
			}
			set
			{
				if (this._flagsRetreat != value)
				{
					this._flagsRetreat = value;
					base.OnPropertyChanged<DelayedStateChanger>(value, "FlagsRetreat");
				}
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x0000E36E File Offset: 0x0000C56E
		// (set) Token: 0x0600046F RID: 1135 RVA: 0x0000E376 File Offset: 0x0000C576
		[Editor(false)]
		public DelayedStateChanger FlagsDefeat
		{
			get
			{
				return this._flagsDefeat;
			}
			set
			{
				if (this._flagsDefeat != value)
				{
					this._flagsDefeat = value;
					base.OnPropertyChanged<DelayedStateChanger>(value, "FlagsDefeat");
				}
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x0000E394 File Offset: 0x0000C594
		// (set) Token: 0x06000471 RID: 1137 RVA: 0x0000E39C File Offset: 0x0000C59C
		[Editor(false)]
		public DelayedStateChanger ShieldStateChanger
		{
			get
			{
				return this._shieldStateChanger;
			}
			set
			{
				if (this._shieldStateChanger != value)
				{
					this._shieldStateChanger = value;
					base.OnPropertyChanged<DelayedStateChanger>(value, "ShieldStateChanger");
				}
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x0000E3BA File Offset: 0x0000C5BA
		// (set) Token: 0x06000473 RID: 1139 RVA: 0x0000E3C2 File Offset: 0x0000C5C2
		[Editor(false)]
		public DelayedStateChanger TitleStateChanger
		{
			get
			{
				return this._titleStateChanger;
			}
			set
			{
				if (this._titleStateChanger != value)
				{
					this._titleStateChanger = value;
					base.OnPropertyChanged<DelayedStateChanger>(value, "TitleStateChanger");
				}
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x0000E3E0 File Offset: 0x0000C5E0
		// (set) Token: 0x06000475 RID: 1141 RVA: 0x0000E3E8 File Offset: 0x0000C5E8
		[Editor(false)]
		public DelayedStateChanger TitleBackgroundStateChanger
		{
			get
			{
				return this._titleBackgroundStateChanger;
			}
			set
			{
				if (this._titleBackgroundStateChanger != value)
				{
					this._titleBackgroundStateChanger = value;
					base.OnPropertyChanged<DelayedStateChanger>(value, "TitleBackgroundStateChanger");
				}
			}
		}

		// Token: 0x040001DB RID: 475
		private bool _isAnimationActive;

		// Token: 0x040001DC RID: 476
		private bool _showScoreboard;

		// Token: 0x040001DD RID: 477
		private bool _isOver;

		// Token: 0x040001DE RID: 478
		private int _battleResult;

		// Token: 0x040001DF RID: 479
		private bool _isMainCharacterDead;

		// Token: 0x040001E0 RID: 480
		private bool _isSimulation;

		// Token: 0x040001E1 RID: 481
		private bool _isMouseEnabled;

		// Token: 0x040001E2 RID: 482
		private ScrollablePanel _scrollablePanel;

		// Token: 0x040001E3 RID: 483
		private Widget _scrollGradient;

		// Token: 0x040001E4 RID: 484
		private Widget _controlButtonsPanel;

		// Token: 0x040001E5 RID: 485
		private Widget _showMouseIconWidget;

		// Token: 0x040001E6 RID: 486
		private ListPanel _inputKeysPanel;

		// Token: 0x040001E7 RID: 487
		private Widget _fastForwardWidget;

		// Token: 0x040001E8 RID: 488
		private ButtonWidget _showScoreboardToggle;

		// Token: 0x040001E9 RID: 489
		private ButtonWidget _quitButton;

		// Token: 0x040001EA RID: 490
		private ScoreboardBattleRewardsWidget _battleRewardsWidget;

		// Token: 0x040001EB RID: 491
		private DelayedStateChanger _flagsSuccess;

		// Token: 0x040001EC RID: 492
		private DelayedStateChanger _flagsDefeat;

		// Token: 0x040001ED RID: 493
		private DelayedStateChanger _flagsRetreat;

		// Token: 0x040001EE RID: 494
		private DelayedStateChanger _shieldStateChanger;

		// Token: 0x040001EF RID: 495
		private DelayedStateChanger _titleStateChanger;

		// Token: 0x040001F0 RID: 496
		private DelayedStateChanger _titleBackgroundStateChanger;
	}
}
