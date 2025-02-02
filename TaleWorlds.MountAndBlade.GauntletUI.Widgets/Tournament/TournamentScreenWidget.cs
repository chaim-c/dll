using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.ExtraWidgets;
using TaleWorlds.MountAndBlade.GauntletUI.Widgets.Scoreboard;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Tournament
{
	// Token: 0x0200004D RID: 77
	public class TournamentScreenWidget : Widget
	{
		// Token: 0x0600041B RID: 1051 RVA: 0x0000D77E File Offset: 0x0000B97E
		public TournamentScreenWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000D787 File Offset: 0x0000B987
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			if (!this._isAnimationActive && this.IsOver)
			{
				this.StartBattleResultAnimation();
			}
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000D7A8 File Offset: 0x0000B9A8
		private void StartBattleResultAnimation()
		{
			this._isAnimationActive = true;
			DelayedStateChanger shieldStateChanger = this.ShieldStateChanger;
			if (shieldStateChanger != null)
			{
				shieldStateChanger.Start();
			}
			DelayedStateChanger winnerTextContainer = this.WinnerTextContainer1;
			if (winnerTextContainer != null)
			{
				winnerTextContainer.Start();
			}
			DelayedStateChanger characterContainer = this.CharacterContainer;
			if (characterContainer != null)
			{
				characterContainer.Start();
			}
			DelayedStateChanger rewardsContainer = this.RewardsContainer;
			if (rewardsContainer != null)
			{
				rewardsContainer.Start();
			}
			DelayedStateChanger flagsSuccess = this.FlagsSuccess;
			if (flagsSuccess != null)
			{
				flagsSuccess.Start();
			}
			ScoreboardBattleRewardsWidget scoreboardBattleRewardsWidget = this.ScoreboardBattleRewardsWidget;
			if (scoreboardBattleRewardsWidget == null)
			{
				return;
			}
			scoreboardBattleRewardsWidget.StartAnimation();
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x0000D821 File Offset: 0x0000BA21
		// (set) Token: 0x0600041F RID: 1055 RVA: 0x0000D829 File Offset: 0x0000BA29
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
				}
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x0000D847 File Offset: 0x0000BA47
		// (set) Token: 0x06000421 RID: 1057 RVA: 0x0000D84F File Offset: 0x0000BA4F
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

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x0000D86D File Offset: 0x0000BA6D
		// (set) Token: 0x06000423 RID: 1059 RVA: 0x0000D875 File Offset: 0x0000BA75
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

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x0000D893 File Offset: 0x0000BA93
		// (set) Token: 0x06000425 RID: 1061 RVA: 0x0000D89B File Offset: 0x0000BA9B
		[Editor(false)]
		public DelayedStateChanger WinnerTextContainer1
		{
			get
			{
				return this._winnerTextContainer1;
			}
			set
			{
				if (this._winnerTextContainer1 != value)
				{
					this._winnerTextContainer1 = value;
					base.OnPropertyChanged<DelayedStateChanger>(value, "WinnerTextContainer1");
				}
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x0000D8B9 File Offset: 0x0000BAB9
		// (set) Token: 0x06000427 RID: 1063 RVA: 0x0000D8C1 File Offset: 0x0000BAC1
		[Editor(false)]
		public DelayedStateChanger CharacterContainer
		{
			get
			{
				return this._characterContainer;
			}
			set
			{
				if (this._characterContainer != value)
				{
					this._characterContainer = value;
					base.OnPropertyChanged<DelayedStateChanger>(value, "CharacterContainer");
				}
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x0000D8DF File Offset: 0x0000BADF
		// (set) Token: 0x06000429 RID: 1065 RVA: 0x0000D8E7 File Offset: 0x0000BAE7
		[Editor(false)]
		public DelayedStateChanger RewardsContainer
		{
			get
			{
				return this._rewardsContainer;
			}
			set
			{
				if (this._rewardsContainer != value)
				{
					this._rewardsContainer = value;
					base.OnPropertyChanged<DelayedStateChanger>(value, "RewardsContainer");
				}
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x0000D905 File Offset: 0x0000BB05
		// (set) Token: 0x0600042B RID: 1067 RVA: 0x0000D90D File Offset: 0x0000BB0D
		[Editor(false)]
		public ScoreboardBattleRewardsWidget ScoreboardBattleRewardsWidget
		{
			get
			{
				return this._scoreboardBattleRewardsWidget;
			}
			set
			{
				if (this._scoreboardBattleRewardsWidget != value)
				{
					this._scoreboardBattleRewardsWidget = value;
					base.OnPropertyChanged<ScoreboardBattleRewardsWidget>(value, "ScoreboardBattleRewardsWidget");
				}
			}
		}

		// Token: 0x040001C7 RID: 455
		private bool _isAnimationActive;

		// Token: 0x040001C8 RID: 456
		private bool _isOver;

		// Token: 0x040001C9 RID: 457
		private DelayedStateChanger _flagsSuccess;

		// Token: 0x040001CA RID: 458
		private DelayedStateChanger _shieldStateChanger;

		// Token: 0x040001CB RID: 459
		private DelayedStateChanger _winnerTextContainer1;

		// Token: 0x040001CC RID: 460
		private DelayedStateChanger _characterContainer;

		// Token: 0x040001CD RID: 461
		private DelayedStateChanger _rewardsContainer;

		// Token: 0x040001CE RID: 462
		private ScoreboardBattleRewardsWidget _scoreboardBattleRewardsWidget;
	}
}
