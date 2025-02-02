using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.Lobby
{
	// Token: 0x02000093 RID: 147
	public class MultiplayerLobbyAfterBattlePopupWidget : Widget
	{
		// Token: 0x060007D6 RID: 2006 RVA: 0x00016EDD File Offset: 0x000150DD
		public MultiplayerLobbyAfterBattlePopupWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x00016EE8 File Offset: 0x000150E8
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			this.IsActive = base.IsVisible;
			if (this._isActive)
			{
				this._timePassed += dt;
			}
			if (this._isFinished)
			{
				return;
			}
			if (this._timePassed >= this.AnimationDuration + this.AnimationDelay + (float)this._currentRewardIndex * this.RewardRevealDuration && this._currentRewardIndex < this.RewardsListPanel.Children.Count)
			{
				(this.RewardsListPanel.Children[this._currentRewardIndex] as MultiplayerLobbyBattleRewardWidget).StartAnimation();
				this._currentRewardIndex++;
			}
			if (this._timePassed >= this.AnimationDelay + this.AnimationDuration + (float)this.RewardsListPanel.Children.Count * this.RewardRevealDuration)
			{
				this._isFinished = true;
				this.ClickToContinueTextWidget.IsVisible = true;
			}
			if (!this._isStarted)
			{
				return;
			}
			if (this._timePassed >= this.AnimationDelay)
			{
				this._isStarted = false;
				this.ExperiencePanel.StartAnimation();
			}
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x00016FFC File Offset: 0x000151FC
		public void StartAnimation()
		{
			foreach (Widget widget in this.RewardsListPanel.Children)
			{
				(widget as MultiplayerLobbyBattleRewardWidget).StartPreAnimation();
			}
			this._isStarted = true;
			this._isFinished = false;
			this._timePassed = 0f;
			this._currentRewardIndex = 0;
			this.ClickToContinueTextWidget.IsVisible = false;
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x00017084 File Offset: 0x00015284
		private void Reset()
		{
			this.ExperiencePanel.Reset();
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x00017091 File Offset: 0x00015291
		private void IsActiveUpdated()
		{
			if (this.IsActive)
			{
				this.StartAnimation();
				return;
			}
			this.Reset();
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x060007DB RID: 2011 RVA: 0x000170A8 File Offset: 0x000152A8
		// (set) Token: 0x060007DC RID: 2012 RVA: 0x000170B0 File Offset: 0x000152B0
		[Editor(false)]
		public bool IsActive
		{
			get
			{
				return this._isActive;
			}
			set
			{
				if (value != this._isActive)
				{
					this._isActive = value;
					base.OnPropertyChanged(value, "IsActive");
					this.IsActiveUpdated();
				}
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x060007DD RID: 2013 RVA: 0x000170D4 File Offset: 0x000152D4
		// (set) Token: 0x060007DE RID: 2014 RVA: 0x000170DC File Offset: 0x000152DC
		[Editor(false)]
		public float AnimationDelay
		{
			get
			{
				return this._animationDelay;
			}
			set
			{
				if (value != this._animationDelay)
				{
					this._animationDelay = value;
					base.OnPropertyChanged(value, "AnimationDelay");
				}
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x060007DF RID: 2015 RVA: 0x000170FA File Offset: 0x000152FA
		// (set) Token: 0x060007E0 RID: 2016 RVA: 0x00017102 File Offset: 0x00015302
		[Editor(false)]
		public float AnimationDuration
		{
			get
			{
				return this._animationDuration;
			}
			set
			{
				if (value != this._animationDuration)
				{
					this._animationDuration = value;
					base.OnPropertyChanged(value, "AnimationDuration");
				}
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x060007E1 RID: 2017 RVA: 0x00017120 File Offset: 0x00015320
		// (set) Token: 0x060007E2 RID: 2018 RVA: 0x00017128 File Offset: 0x00015328
		[Editor(false)]
		public float RewardRevealDuration
		{
			get
			{
				return this._rewardRevealDuration;
			}
			set
			{
				if (value != this._rewardRevealDuration)
				{
					this._rewardRevealDuration = value;
					base.OnPropertyChanged(value, "RewardRevealDuration");
				}
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x060007E3 RID: 2019 RVA: 0x00017146 File Offset: 0x00015346
		// (set) Token: 0x060007E4 RID: 2020 RVA: 0x0001714E File Offset: 0x0001534E
		[Editor(false)]
		public MultiplayerLobbyAfterBattleExperiencePanelWidget ExperiencePanel
		{
			get
			{
				return this._experiencePanel;
			}
			set
			{
				if (value != this._experiencePanel)
				{
					this._experiencePanel = value;
					base.OnPropertyChanged<MultiplayerLobbyAfterBattleExperiencePanelWidget>(value, "ExperiencePanel");
				}
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x060007E5 RID: 2021 RVA: 0x0001716C File Offset: 0x0001536C
		// (set) Token: 0x060007E6 RID: 2022 RVA: 0x00017174 File Offset: 0x00015374
		[Editor(false)]
		public TextWidget ClickToContinueTextWidget
		{
			get
			{
				return this._clickToContinueTextWidget;
			}
			set
			{
				if (value != this._clickToContinueTextWidget)
				{
					this._clickToContinueTextWidget = value;
					base.OnPropertyChanged<TextWidget>(value, "ClickToContinueTextWidget");
				}
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x060007E7 RID: 2023 RVA: 0x00017192 File Offset: 0x00015392
		// (set) Token: 0x060007E8 RID: 2024 RVA: 0x0001719A File Offset: 0x0001539A
		[Editor(false)]
		public ListPanel RewardsListPanel
		{
			get
			{
				return this._rewardsListPanel;
			}
			set
			{
				if (value != this._rewardsListPanel)
				{
					this._rewardsListPanel = value;
					base.OnPropertyChanged<ListPanel>(value, "RewardsListPanel");
				}
			}
		}

		// Token: 0x04000384 RID: 900
		private bool _isStarted;

		// Token: 0x04000385 RID: 901
		private bool _isFinished;

		// Token: 0x04000386 RID: 902
		private float _timePassed;

		// Token: 0x04000387 RID: 903
		private int _currentRewardIndex;

		// Token: 0x04000388 RID: 904
		private bool _isActive;

		// Token: 0x04000389 RID: 905
		private float _animationDelay;

		// Token: 0x0400038A RID: 906
		private float _animationDuration;

		// Token: 0x0400038B RID: 907
		private float _rewardRevealDuration;

		// Token: 0x0400038C RID: 908
		private MultiplayerLobbyAfterBattleExperiencePanelWidget _experiencePanel;

		// Token: 0x0400038D RID: 909
		private TextWidget _clickToContinueTextWidget;

		// Token: 0x0400038E RID: 910
		private ListPanel _rewardsListPanel;
	}
}
