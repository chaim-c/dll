using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.ExtraWidgets;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.Lobby
{
	// Token: 0x02000097 RID: 151
	public class MultiplayerLobbyBattleRewardWidget : Widget
	{
		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000807 RID: 2055 RVA: 0x000176F1 File Offset: 0x000158F1
		// (set) Token: 0x06000808 RID: 2056 RVA: 0x000176F9 File Offset: 0x000158F9
		public float AnimationDuration { get; set; } = 0.1f;

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000809 RID: 2057 RVA: 0x00017702 File Offset: 0x00015902
		// (set) Token: 0x0600080A RID: 2058 RVA: 0x0001770A File Offset: 0x0001590A
		public float TextRevealAnimationDuration { get; set; } = 0.05f;

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x0600080B RID: 2059 RVA: 0x00017713 File Offset: 0x00015913
		// (set) Token: 0x0600080C RID: 2060 RVA: 0x0001771B File Offset: 0x0001591B
		public float AnimationInitialScaleMultiplier { get; set; } = 2f;

		// Token: 0x0600080D RID: 2061 RVA: 0x00017724 File Offset: 0x00015924
		public MultiplayerLobbyBattleRewardWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x00017750 File Offset: 0x00015950
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this._isInPreAnimationState)
			{
				foreach (Widget widget in base.Children)
				{
					if (widget is ValueBasedVisibilityWidget)
					{
						widget.IsVisible = false;
					}
				}
			}
			bool flag = false;
			if (this._isAnimationStarted && base.EventManager.Time - this._animationStartTime < this.AnimationDuration)
			{
				float amount = (base.EventManager.Time - this._animationStartTime) / this.AnimationDuration;
				this._rewardIconButton.SuggestedWidth = Mathf.Lerp(this._buttonAnimationStartWidth, this._buttonAnimationEndWidth, amount);
				this._rewardIconButton.SuggestedHeight = Mathf.Lerp(this._buttonAnimationStartHeight, this._buttonAnimationEndHeight, amount);
				this._rewardIcon.SuggestedWidth = Mathf.Lerp(this._iconAnimationStartWidget, this._iconAnimationEndWidth, amount);
				this._rewardIcon.SuggestedHeight = Mathf.Lerp(this._iconAnimationStartHeight, this._iconAnimationEndHeight, amount);
				this._rewardIconButton.SetGlobalAlphaRecursively(Mathf.Lerp(0f, 1f, amount));
				this._rewardIcon.SetGlobalAlphaRecursively(Mathf.Lerp(0f, 1f, amount));
				this._rewardToShow.IsVisible = true;
				flag = true;
			}
			if (!this._isTextAnimationStarted && this._isAnimationStarted && base.EventManager.Time - this._animationStartTime >= this.AnimationDuration)
			{
				this._textAnimationStartTime = base.EventManager.Time;
				this._isTextAnimationStarted = true;
			}
			if (this._isTextAnimationStarted && base.EventManager.Time - this._textAnimationStartTime < this.TextRevealAnimationDuration)
			{
				float amount2 = (base.EventManager.Time - this._textAnimationStartTime) / this.TextRevealAnimationDuration;
				this._rewardTextDescription.SetGlobalAlphaRecursively(Mathf.Lerp(0f, 1f, amount2));
				flag = true;
			}
			if (this._isAnimationStarted && this._isTextAnimationStarted && !flag)
			{
				this.EndAnimation();
			}
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x0001796C File Offset: 0x00015B6C
		public void StartAnimation()
		{
			this._isInPreAnimationState = false;
			foreach (Widget widget in base.Children)
			{
				ValueBasedVisibilityWidget valueBasedVisibilityWidget;
				if ((valueBasedVisibilityWidget = (widget as ValueBasedVisibilityWidget)) != null && valueBasedVisibilityWidget.IndexToWatch == valueBasedVisibilityWidget.IndexToBeVisible)
				{
					this._rewardToShow = valueBasedVisibilityWidget;
					this._rewardIconButton = (widget.Children[0].Children[0] as ButtonWidget);
					this._rewardIcon = this._rewardIconButton.Children[0];
					this._rewardTextDescription = (widget.Children[0].Children[1] as TextWidget);
					this._buttonAnimationStartWidth = this._rewardIconButton.SuggestedWidth * this.AnimationInitialScaleMultiplier;
					this._buttonAnimationStartHeight = this._rewardIconButton.SuggestedHeight * this.AnimationInitialScaleMultiplier;
					this._buttonAnimationEndWidth = this._rewardIconButton.SuggestedWidth;
					this._buttonAnimationEndHeight = this._rewardIconButton.SuggestedHeight;
					this._iconAnimationStartWidget = this._rewardIcon.SuggestedWidth * this.AnimationInitialScaleMultiplier;
					this._iconAnimationStartHeight = this._rewardIcon.SuggestedHeight * this.AnimationInitialScaleMultiplier;
					this._iconAnimationEndWidth = this._rewardIcon.SuggestedWidth;
					this._iconAnimationEndHeight = this._rewardIcon.SuggestedHeight;
					this._rewardTextDescription.SetGlobalAlphaRecursively(0f);
				}
			}
			this._isAnimationStarted = true;
			this._animationStartTime = base.EventManager.Time;
			base.Context.TwoDimensionContext.PlaySound("inventory/perk");
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x00017B34 File Offset: 0x00015D34
		public void StartPreAnimation()
		{
			this._isInPreAnimationState = true;
			base.IsVisible = true;
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x00017B44 File Offset: 0x00015D44
		public void EndAnimation()
		{
			this._rewardIconButton.SetGlobalAlphaRecursively(1f);
			this._rewardIcon.SetGlobalAlphaRecursively(1f);
			this._rewardTextDescription.SetGlobalAlphaRecursively(1f);
			this._rewardIconButton.SuggestedWidth = this._buttonAnimationEndWidth;
			this._rewardIconButton.SuggestedHeight = this._buttonAnimationEndHeight;
			this._rewardIcon.SuggestedWidth = this._iconAnimationEndWidth;
			this._rewardIcon.SuggestedHeight = this._iconAnimationEndHeight;
			this._isAnimationStarted = false;
			this._isTextAnimationStarted = false;
		}

		// Token: 0x0400039E RID: 926
		private const string _rewardImpactSoundEventName = "inventory/perk";

		// Token: 0x0400039F RID: 927
		private float _buttonAnimationStartWidth;

		// Token: 0x040003A0 RID: 928
		private float _buttonAnimationStartHeight;

		// Token: 0x040003A1 RID: 929
		private float _buttonAnimationEndWidth;

		// Token: 0x040003A2 RID: 930
		private float _buttonAnimationEndHeight;

		// Token: 0x040003A3 RID: 931
		private float _iconAnimationStartWidget;

		// Token: 0x040003A4 RID: 932
		private float _iconAnimationStartHeight;

		// Token: 0x040003A5 RID: 933
		private float _iconAnimationEndWidth;

		// Token: 0x040003A6 RID: 934
		private float _iconAnimationEndHeight;

		// Token: 0x040003AA RID: 938
		private ButtonWidget _rewardIconButton;

		// Token: 0x040003AB RID: 939
		private Widget _rewardIcon;

		// Token: 0x040003AC RID: 940
		private TextWidget _rewardTextDescription;

		// Token: 0x040003AD RID: 941
		private ValueBasedVisibilityWidget _rewardToShow;

		// Token: 0x040003AE RID: 942
		private bool _isAnimationStarted;

		// Token: 0x040003AF RID: 943
		private bool _isTextAnimationStarted;

		// Token: 0x040003B0 RID: 944
		private bool _isInPreAnimationState;

		// Token: 0x040003B1 RID: 945
		private float _animationStartTime;

		// Token: 0x040003B2 RID: 946
		private float _textAnimationStartTime;
	}
}
