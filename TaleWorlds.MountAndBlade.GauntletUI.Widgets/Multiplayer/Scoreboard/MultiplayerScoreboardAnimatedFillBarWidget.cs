using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.ExtraWidgets;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.Scoreboard
{
	// Token: 0x02000088 RID: 136
	public class MultiplayerScoreboardAnimatedFillBarWidget : FillBarWidget
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000761 RID: 1889 RVA: 0x00015B88 File Offset: 0x00013D88
		// (remove) Token: 0x06000762 RID: 1890 RVA: 0x00015BC0 File Offset: 0x00013DC0
		public event MultiplayerScoreboardAnimatedFillBarWidget.FullFillFinishedHandler OnFullFillFinished;

		// Token: 0x06000763 RID: 1891 RVA: 0x00015BF5 File Offset: 0x00013DF5
		public MultiplayerScoreboardAnimatedFillBarWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x00015C1C File Offset: 0x00013E1C
		public void StartAnimation()
		{
			if (base.FillWidget == null || base.ChangeWidget == null || MathF.Abs(this.AnimationDuration) <= 1E-45f)
			{
				return;
			}
			float num = Mathf.Clamp(Mathf.Clamp((float)base.InitialAmount, 0f, (float)base.MaxAmount) / (float)base.MaxAmount, 0f, 1f);
			float num2 = Mathf.Clamp((float)(base.CurrentAmount - base.InitialAmount), (float)(-(float)base.MaxAmount), (float)base.MaxAmount);
			this._finalRatio = num + Mathf.Clamp(num2 / (float)base.MaxAmount, -1f, 1f);
			if (!this._isStarted)
			{
				base.Context.TwoDimensionContext.CreateSoundEvent(this._xpBarSoundEventName);
				base.Context.TwoDimensionContext.PlaySoundEvent(this._xpBarSoundEventName);
			}
			if (this.TimesOfFullFill > 0)
			{
				this._currentTargetRatioOfChange = 1f;
			}
			else
			{
				this._currentTargetRatioOfChange = this._finalRatio;
				this._inFinalFillState = true;
			}
			this._inFirstFillState = true;
			this._ratioOfChange = num;
			this._isStarted = true;
			this._timePassed = 0f;
			this._ratioOfChangePerTick = this.AnimationDuration / ((float)this._timesOfFullFill + this._finalRatio);
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x00015D58 File Offset: 0x00013F58
		public void Reset()
		{
			this._timePassed = 0f;
			this._ratioOfChange = 0f;
			this._currentTargetRatioOfChange = 0f;
			this._ratioOfChangePerTick = 0f;
			this._inFirstFillState = true;
			this._isStarted = false;
			base.Context.TwoDimensionContext.StopAndRemoveSoundEvent(this._xpBarSoundEventName);
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x00015DB8 File Offset: 0x00013FB8
		protected override void OnUpdate(float dt)
		{
			if (this.IsStartRequested && !this._isStarted && !this._inFinalFillState)
			{
				this.StartAnimation();
			}
			if (!this._isStarted)
			{
				return;
			}
			this._timePassed += dt;
			if (this._timePassed >= this.AnimationDelay)
			{
				this._ratioOfChange += dt / this._ratioOfChangePerTick;
				if (this._ratioOfChange > this._currentTargetRatioOfChange)
				{
					if (this._timesOfFullFill > 0)
					{
						this._currentTargetRatioOfChange = 1f;
						this._ratioOfChange = 0f;
						this._timesOfFullFill--;
						this._inFirstFillState = false;
						if (this._timesOfFullFill == 0)
						{
							this._currentTargetRatioOfChange = this._finalRatio;
							this._inFinalFillState = true;
						}
						MultiplayerScoreboardAnimatedFillBarWidget.FullFillFinishedHandler onFullFillFinished = this.OnFullFillFinished;
						if (onFullFillFinished == null)
						{
							return;
						}
						onFullFillFinished();
						return;
					}
					else if (this._inFinalFillState || this._timesOfFullFill == 0)
					{
						this._inFinalFillState = true;
						this._ratioOfChange = this._finalRatio;
						this._isStarted = false;
						base.Context.TwoDimensionContext.StopAndRemoveSoundEvent(this._xpBarSoundEventName);
						base.Context.TwoDimensionContext.PlaySound(this._xpBarStopSoundEventName);
					}
				}
			}
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00015EE8 File Offset: 0x000140E8
		protected override void OnRender(TwoDimensionContext twoDimensionContext, TwoDimensionDrawContext drawContext)
		{
			if (base.FillWidget != null)
			{
				float x = base.FillWidget.ParentWidget.Size.X;
				float num = Mathf.Clamp(Mathf.Clamp((float)base.InitialAmount, 0f, (float)base.MaxAmount) / (float)base.MaxAmount, 0f, 1f);
				base.FillWidget.ScaledSuggestedWidth = num * x;
				base.FillWidget.IsVisible = this._inFirstFillState;
				if (base.ChangeWidget != null)
				{
					if (this._ratioOfChange >= 0f)
					{
						base.ChangeWidget.ScaledSuggestedWidth = this._ratioOfChange * x;
						base.ChangeWidget.Color = new Color(1f, 1f, 1f, 1f);
					}
					if (base.DividerWidget != null)
					{
						if (this._ratioOfChange > 0f)
						{
							base.DividerWidget.ScaledPositionXOffset = base.FillWidget.ScaledSuggestedWidth - base.DividerWidget.Size.X;
						}
						base.DividerWidget.IsVisible = (this._ratioOfChange != 0f);
					}
					if (this.ChangeOverlayWidget != null)
					{
						this.ChangeOverlayWidget.ScaledPositionXOffset = base.ChangeWidget.ScaledMarginLeft + base.ChangeWidget.ScaledSuggestedWidth - this.ChangeOverlayWidget.Size.X;
						this.ChangeOverlayWidget.IsVisible = (this._ratioOfChange != 0f);
					}
				}
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000768 RID: 1896 RVA: 0x00016061 File Offset: 0x00014261
		// (set) Token: 0x06000769 RID: 1897 RVA: 0x00016069 File Offset: 0x00014269
		[Editor(false)]
		public bool IsStartRequested
		{
			get
			{
				return this._isStartRequested;
			}
			set
			{
				if (value != this._isStartRequested)
				{
					this._isStartRequested = value;
					base.OnPropertyChanged(value, "IsStartRequested");
				}
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x0600076A RID: 1898 RVA: 0x00016087 File Offset: 0x00014287
		// (set) Token: 0x0600076B RID: 1899 RVA: 0x0001608F File Offset: 0x0001428F
		[Editor(false)]
		public float AnimationDelay
		{
			get
			{
				return this._animationDelay;
			}
			set
			{
				if (this._animationDelay != value)
				{
					this._animationDelay = value;
					base.OnPropertyChanged(value, "AnimationDelay");
				}
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x0600076C RID: 1900 RVA: 0x000160AD File Offset: 0x000142AD
		// (set) Token: 0x0600076D RID: 1901 RVA: 0x000160B5 File Offset: 0x000142B5
		[Editor(false)]
		public float AnimationDuration
		{
			get
			{
				return this._animationDuration;
			}
			set
			{
				if (this._animationDuration != value)
				{
					this._animationDuration = value;
					base.OnPropertyChanged(value, "AnimationDuration");
				}
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x0600076E RID: 1902 RVA: 0x000160D3 File Offset: 0x000142D3
		// (set) Token: 0x0600076F RID: 1903 RVA: 0x000160DB File Offset: 0x000142DB
		[Editor(false)]
		public int TimesOfFullFill
		{
			get
			{
				return this._timesOfFullFill;
			}
			set
			{
				if (this._timesOfFullFill != value)
				{
					this._timesOfFullFill = value;
					base.OnPropertyChanged(value, "TimesOfFullFill");
				}
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000770 RID: 1904 RVA: 0x000160F9 File Offset: 0x000142F9
		// (set) Token: 0x06000771 RID: 1905 RVA: 0x00016101 File Offset: 0x00014301
		[Editor(false)]
		public Widget ChangeOverlayWidget
		{
			get
			{
				return this._changeOverlayWidget;
			}
			set
			{
				if (this._changeOverlayWidget != value)
				{
					this._changeOverlayWidget = value;
					base.OnPropertyChanged<Widget>(value, "ChangeOverlayWidget");
				}
			}
		}

		// Token: 0x04000346 RID: 838
		private float _currentTargetRatioOfChange;

		// Token: 0x04000347 RID: 839
		private float _finalRatio;

		// Token: 0x04000348 RID: 840
		private float _ratioOfChange;

		// Token: 0x04000349 RID: 841
		private bool _isStarted;

		// Token: 0x0400034A RID: 842
		private bool _inFinalFillState;

		// Token: 0x0400034B RID: 843
		private bool _inFirstFillState = true;

		// Token: 0x0400034C RID: 844
		private float _timePassed;

		// Token: 0x0400034D RID: 845
		private float _ratioOfChangePerTick;

		// Token: 0x0400034E RID: 846
		private string _xpBarSoundEventName = "multiplayer/xpbar";

		// Token: 0x0400034F RID: 847
		private string _xpBarStopSoundEventName = "multiplayer/xpbar_stop";

		// Token: 0x04000351 RID: 849
		private bool _isStartRequested;

		// Token: 0x04000352 RID: 850
		private float _animationDelay;

		// Token: 0x04000353 RID: 851
		private float _animationDuration;

		// Token: 0x04000354 RID: 852
		private Widget _changeOverlayWidget;

		// Token: 0x04000355 RID: 853
		private int _timesOfFullFill;

		// Token: 0x020001A3 RID: 419
		// (Invoke) Token: 0x06001466 RID: 5222
		public delegate void FullFillFinishedHandler();
	}
}
