using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.Scoreboard;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.Lobby
{
	// Token: 0x02000092 RID: 146
	public class MultiplayerLobbyAfterBattleExperiencePanelWidget : Widget
	{
		// Token: 0x060007C7 RID: 1991 RVA: 0x00016D37 File Offset: 0x00014F37
		public MultiplayerLobbyAfterBattleExperiencePanelWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x00016D40 File Offset: 0x00014F40
		public void StartAnimation()
		{
			this.ExperienceFillBar.StartAnimation();
			this.EarnedExperienceCounterTextWidget.IntTarget = this.GainedExperience;
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x00016D5E File Offset: 0x00014F5E
		public void Reset()
		{
			MultiplayerScoreboardAnimatedFillBarWidget experienceFillBar = this.ExperienceFillBar;
			if (experienceFillBar != null)
			{
				experienceFillBar.Reset();
			}
			CounterTextBrushWidget earnedExperienceCounterTextWidget = this.EarnedExperienceCounterTextWidget;
			if (earnedExperienceCounterTextWidget == null)
			{
				return;
			}
			earnedExperienceCounterTextWidget.SetInitialValue(0f);
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x00016D86 File Offset: 0x00014F86
		private void OnFillBarFill()
		{
			this.CurrentLevelTextWidget.IntText++;
			this.NextLevelTextWidget.IntText++;
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x00016DAE File Offset: 0x00014FAE
		protected override void RefreshState()
		{
			if (base.IsHidden)
			{
				MultiplayerScoreboardAnimatedFillBarWidget experienceFillBar = this.ExperienceFillBar;
				if (experienceFillBar == null)
				{
					return;
				}
				experienceFillBar.Reset();
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x060007CC RID: 1996 RVA: 0x00016DC8 File Offset: 0x00014FC8
		// (set) Token: 0x060007CD RID: 1997 RVA: 0x00016DD0 File Offset: 0x00014FD0
		[Editor(false)]
		public int GainedExperience
		{
			get
			{
				return this._gainedExperience;
			}
			set
			{
				if (value != this._gainedExperience)
				{
					this._gainedExperience = value;
					base.OnPropertyChanged(value, "GainedExperience");
				}
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x060007CE RID: 1998 RVA: 0x00016DEE File Offset: 0x00014FEE
		// (set) Token: 0x060007CF RID: 1999 RVA: 0x00016DF8 File Offset: 0x00014FF8
		[Editor(false)]
		public MultiplayerScoreboardAnimatedFillBarWidget ExperienceFillBar
		{
			get
			{
				return this._experienceFillBar;
			}
			set
			{
				if (value != this._experienceFillBar)
				{
					if (this._experienceFillBar != null)
					{
						this._experienceFillBar.OnFullFillFinished -= this.OnFillBarFill;
					}
					this._experienceFillBar = value;
					if (this._experienceFillBar != null)
					{
						this._experienceFillBar.OnFullFillFinished += this.OnFillBarFill;
					}
					base.OnPropertyChanged<MultiplayerScoreboardAnimatedFillBarWidget>(value, "ExperienceFillBar");
					this.Reset();
				}
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x060007D0 RID: 2000 RVA: 0x00016E65 File Offset: 0x00015065
		// (set) Token: 0x060007D1 RID: 2001 RVA: 0x00016E6D File Offset: 0x0001506D
		[Editor(false)]
		public CounterTextBrushWidget EarnedExperienceCounterTextWidget
		{
			get
			{
				return this._earnedExperienceCounterTextWidget;
			}
			set
			{
				if (value != this._earnedExperienceCounterTextWidget)
				{
					this._earnedExperienceCounterTextWidget = value;
					base.OnPropertyChanged<CounterTextBrushWidget>(value, "EarnedExperienceCounterTextWidget");
					this.Reset();
				}
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x00016E91 File Offset: 0x00015091
		// (set) Token: 0x060007D3 RID: 2003 RVA: 0x00016E99 File Offset: 0x00015099
		[Editor(false)]
		public TextWidget CurrentLevelTextWidget
		{
			get
			{
				return this._currentLevelTextWidget;
			}
			set
			{
				if (value != this._currentLevelTextWidget)
				{
					this._currentLevelTextWidget = value;
					base.OnPropertyChanged<TextWidget>(value, "CurrentLevelTextWidget");
				}
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x060007D4 RID: 2004 RVA: 0x00016EB7 File Offset: 0x000150B7
		// (set) Token: 0x060007D5 RID: 2005 RVA: 0x00016EBF File Offset: 0x000150BF
		public TextWidget NextLevelTextWidget
		{
			get
			{
				return this._nextLevelTextWidget;
			}
			set
			{
				if (value != this._nextLevelTextWidget)
				{
					this._nextLevelTextWidget = value;
					base.OnPropertyChanged<TextWidget>(value, "NextLevelTextWidget");
				}
			}
		}

		// Token: 0x0400037F RID: 895
		private int _gainedExperience;

		// Token: 0x04000380 RID: 896
		private MultiplayerScoreboardAnimatedFillBarWidget _experienceFillBar;

		// Token: 0x04000381 RID: 897
		private CounterTextBrushWidget _earnedExperienceCounterTextWidget;

		// Token: 0x04000382 RID: 898
		private TextWidget _currentLevelTextWidget;

		// Token: 0x04000383 RID: 899
		private TextWidget _nextLevelTextWidget;
	}
}
