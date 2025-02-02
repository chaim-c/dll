using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets
{
	// Token: 0x02000038 RID: 56
	public class SceneWidget : TextureWidget
	{
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600030A RID: 778 RVA: 0x00009E6D File Offset: 0x0000806D
		private bool _isClickToContinueActive
		{
			get
			{
				return this._clickToContinueStartTime != -1f && base.EventManager.Time - this._clickToContinueStartTime >= this._clickToContinueDelayInSeconds;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600030B RID: 779 RVA: 0x00009E9B File Offset: 0x0000809B
		private float _clickToContinueDelayInSeconds
		{
			get
			{
				return 2f;
			}
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00009EA4 File Offset: 0x000080A4
		public SceneWidget(UIContext context) : base(context)
		{
			base.TextureProviderName = "SceneTextureProvider";
			this._isRenderRequestedPreviousFrame = true;
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00009EF8 File Offset: 0x000080F8
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this.Scene != null && !this._initialized)
			{
				this.DetermineInitContinueState();
				this._initialized = true;
			}
			if (this.Scene != null && !this.IsReady)
			{
				bool? flag = (bool?)base.GetTextureProviderProperty("IsReady");
				bool flag2 = true;
				this.IsReady = (flag.GetValueOrDefault() == flag2 & flag != null);
			}
			if (this._isInClickToContinueState)
			{
				if (this._isClickToContinueActive)
				{
					this.ClickToContinueTextWidget.SetGlobalAlphaRecursively(Mathf.Lerp(this.ClickToContinueTextWidget.ReadOnlyBrush.GlobalAlphaFactor, 1f, dt * 10f));
					if (!this._prevIsClickToContinueActive)
					{
						this.ClickToContinueTextWidget.BrushRenderer.RestartAnimation();
					}
				}
				this.CancelButton.SetGlobalAlphaRecursively(Mathf.Lerp(this.CancelButton.ReadOnlyBrush.GlobalAlphaFactor, 0f, dt * 10f));
				this.AffirmativeButton.SetGlobalAlphaRecursively(Mathf.Lerp(this.AffirmativeButton.ReadOnlyBrush.GlobalAlphaFactor, 0f, dt * 10f));
			}
			else
			{
				this.ClickToContinueTextWidget.SetGlobalAlphaRecursively(Mathf.Lerp(this.ClickToContinueTextWidget.ReadOnlyBrush.GlobalAlphaFactor, 0f, dt * 10f));
				this.CancelButton.SetGlobalAlphaRecursively(Mathf.Lerp(this.CancelButton.ReadOnlyBrush.GlobalAlphaFactor, (float)(this.IsCancelShown ? 1 : 0), dt * 10f));
				this.AffirmativeButton.SetGlobalAlphaRecursively(Mathf.Lerp(this.AffirmativeButton.ReadOnlyBrush.GlobalAlphaFactor, (float)(this.IsOkShown ? 1 : 0), dt * 10f));
			}
			this.UpdateVisibilityOfWidgetBasedOnAlpha(this.ClickToContinueTextWidget);
			this.UpdateVisibilityOfWidgetBasedOnAlpha(this.CancelButton);
			this.UpdateVisibilityOfWidgetBasedOnAlpha(this.AffirmativeButton);
			this.HandleTitleTextChange();
			this.FadeImageWidget.AlphaFactor = (this.IsReady ? this.EndProgress : 1f);
			this.PreparingVisualWidget.IsVisible = !this.IsReady;
			this._prevIsClickToContinueActive = this._isClickToContinueActive;
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000A116 File Offset: 0x00008316
		private void UpdateVisibilityOfWidgetBasedOnAlpha(BrushWidget widget)
		{
			widget.IsVisible = !widget.ReadOnlyBrush.GlobalAlphaFactor.ApproximatelyEqualsTo(0f, 0.01f);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000A13B File Offset: 0x0000833B
		protected override void OnMousePressed()
		{
			base.OnMousePressed();
			if (this._isClickToContinueActive)
			{
				base.EventFired("Close", Array.Empty<object>());
				this.ResetStates();
			}
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000A161 File Offset: 0x00008361
		private void OnAnyActionButtonClick()
		{
			this._isInClickToContinueState = true;
			base.DoNotAcceptEvents = false;
			base.DoNotPassEventsToChildren = true;
			this.ClickToContinueTextWidget.BrushRenderer.RestartAnimation();
			this._clickToContinueStartTime = base.EventManager.Time;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000A19C File Offset: 0x0000839C
		private void ResetStates()
		{
			this._isInClickToContinueState = false;
			base.DoNotAcceptEvents = true;
			base.DoNotPassEventsToChildren = false;
			this._initialized = false;
			this.IsReady = false;
			this._titleChangeStartTime = -1f;
			this._currentTitleTextToUpdateTo = string.Empty;
			this.TitleTextWidget.SetAlpha(1f);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000A1F2 File Offset: 0x000083F2
		private void OnAffirmativeButtonClick(Widget obj)
		{
			this.SetNewTitleText(this.AffirmativeTitleText);
			this.OnAnyActionButtonClick();
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000A206 File Offset: 0x00008406
		private void OnCancelButtonClick(Widget obj)
		{
			this.SetNewTitleText(this.NegativeTitleText);
			this.OnAnyActionButtonClick();
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000A21A File Offset: 0x0000841A
		private void SetNewTitleText(string newText)
		{
			if (!string.IsNullOrEmpty(newText))
			{
				this._currentTitleTextToUpdateTo = newText;
				this._titleChangeStartTime = base.EventManager.Time;
			}
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000A23C File Offset: 0x0000843C
		private void DetermineInitContinueState()
		{
			this.CancelButton.IsVisible = this.IsCancelShown;
			this.CancelButton.SetGlobalAlphaRecursively((float)(this.IsCancelShown ? 1 : 0));
			this.AffirmativeButton.IsVisible = this.IsOkShown;
			this.AffirmativeButton.SetGlobalAlphaRecursively((float)(this.IsOkShown ? 1 : 0));
			this.ClickToContinueTextWidget.SetGlobalAlphaRecursively(0f);
			this._isInClickToContinueState = (!this.IsCancelShown && !this.IsOkShown);
			if (this._isInClickToContinueState)
			{
				this._clickToContinueStartTime = base.EventManager.Time;
			}
			base.DoNotAcceptEvents = !this._isInClickToContinueState;
			base.DoNotPassEventsToChildren = this._isInClickToContinueState;
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000A2FC File Offset: 0x000084FC
		private void HandleTitleTextChange()
		{
			if (this._titleChangeStartTime != -1f)
			{
				if (!string.IsNullOrEmpty(this._currentTitleTextToUpdateTo) && this.TitleTextWidget != null && base.EventManager.Time - this._titleChangeStartTime < this._titleChangeTotalTimeInSeconds)
				{
					if (base.EventManager.Time - this._titleChangeStartTime >= this._titleChangeTotalTimeInSeconds / 2f)
					{
						this.TitleTextWidget.Text = this._currentTitleTextToUpdateTo;
					}
					float alphaFactor = 1f - MathF.PingPong(0f, 1f, base.EventManager.Time - this._titleChangeStartTime);
					this.TitleTextWidget.SetAlpha(alphaFactor);
					return;
				}
				this.TitleTextWidget.SetAlpha(1f);
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000317 RID: 791 RVA: 0x0000A3C4 File Offset: 0x000085C4
		// (set) Token: 0x06000318 RID: 792 RVA: 0x0000A3CC File Offset: 0x000085CC
		[Editor(false)]
		public object Scene
		{
			get
			{
				return this._scene;
			}
			set
			{
				if (value != this._scene)
				{
					this._scene = value;
					base.OnPropertyChanged<object>(value, "Scene");
					base.SetTextureProviderProperty("Scene", value);
					if (value != null)
					{
						this._isTargetSizeDirty = true;
						this.ResetStates();
					}
				}
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000319 RID: 793 RVA: 0x0000A406 File Offset: 0x00008606
		// (set) Token: 0x0600031A RID: 794 RVA: 0x0000A40E File Offset: 0x0000860E
		[Editor(false)]
		public ButtonWidget AffirmativeButton
		{
			get
			{
				return this._affirmativeButton;
			}
			set
			{
				if (value != this._affirmativeButton)
				{
					this._affirmativeButton = value;
					base.OnPropertyChanged<ButtonWidget>(value, "AffirmativeButton");
					ButtonWidget affirmativeButton = this._affirmativeButton;
					if (affirmativeButton == null)
					{
						return;
					}
					affirmativeButton.ClickEventHandlers.Add(new Action<Widget>(this.OnAffirmativeButtonClick));
				}
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600031B RID: 795 RVA: 0x0000A44D File Offset: 0x0000864D
		// (set) Token: 0x0600031C RID: 796 RVA: 0x0000A455 File Offset: 0x00008655
		[Editor(false)]
		public ButtonWidget CancelButton
		{
			get
			{
				return this._cancelButton;
			}
			set
			{
				if (value != this._cancelButton)
				{
					this._cancelButton = value;
					base.OnPropertyChanged<ButtonWidget>(value, "CancelButton");
					ButtonWidget cancelButton = this._cancelButton;
					if (cancelButton == null)
					{
						return;
					}
					cancelButton.ClickEventHandlers.Add(new Action<Widget>(this.OnCancelButtonClick));
				}
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600031D RID: 797 RVA: 0x0000A494 File Offset: 0x00008694
		// (set) Token: 0x0600031E RID: 798 RVA: 0x0000A49C File Offset: 0x0000869C
		[Editor(false)]
		public RichTextWidget ClickToContinueTextWidget
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
					base.OnPropertyChanged<RichTextWidget>(value, "ClickToContinueTextWidget");
				}
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600031F RID: 799 RVA: 0x0000A4BA File Offset: 0x000086BA
		// (set) Token: 0x06000320 RID: 800 RVA: 0x0000A4C2 File Offset: 0x000086C2
		[Editor(false)]
		public TextWidget TitleTextWidget
		{
			get
			{
				return this._titleTextWidget;
			}
			set
			{
				if (value != this._titleTextWidget)
				{
					this._titleTextWidget = value;
					base.OnPropertyChanged<TextWidget>(value, "TitleTextWidget");
				}
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000321 RID: 801 RVA: 0x0000A4E0 File Offset: 0x000086E0
		// (set) Token: 0x06000322 RID: 802 RVA: 0x0000A4E8 File Offset: 0x000086E8
		[Editor(false)]
		public Widget FadeImageWidget
		{
			get
			{
				return this._fadeImageWidget;
			}
			set
			{
				if (value != this._fadeImageWidget)
				{
					this._fadeImageWidget = value;
					base.OnPropertyChanged<Widget>(value, "FadeImageWidget");
				}
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0000A506 File Offset: 0x00008706
		// (set) Token: 0x06000324 RID: 804 RVA: 0x0000A50E File Offset: 0x0000870E
		[Editor(false)]
		public Widget PreparingVisualWidget
		{
			get
			{
				return this._preparingVisualWidget;
			}
			set
			{
				if (value != this._preparingVisualWidget)
				{
					this._preparingVisualWidget = value;
					base.OnPropertyChanged<Widget>(value, "PreparingVisualWidget");
				}
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000325 RID: 805 RVA: 0x0000A52C File Offset: 0x0000872C
		// (set) Token: 0x06000326 RID: 806 RVA: 0x0000A534 File Offset: 0x00008734
		[Editor(false)]
		public float EndProgress
		{
			get
			{
				return this._endProgress;
			}
			set
			{
				if (value != this._endProgress)
				{
					this._endProgress = value;
					base.OnPropertyChanged(value, "EndProgress");
				}
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000327 RID: 807 RVA: 0x0000A552 File Offset: 0x00008752
		// (set) Token: 0x06000328 RID: 808 RVA: 0x0000A55A File Offset: 0x0000875A
		[Editor(false)]
		public bool IsOkShown
		{
			get
			{
				return this._isOkShown;
			}
			set
			{
				if (value != this._isOkShown)
				{
					this._isOkShown = value;
					base.OnPropertyChanged(value, "IsOkShown");
					this.DetermineInitContinueState();
				}
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000329 RID: 809 RVA: 0x0000A57E File Offset: 0x0000877E
		// (set) Token: 0x0600032A RID: 810 RVA: 0x0000A586 File Offset: 0x00008786
		[Editor(false)]
		public bool IsCancelShown
		{
			get
			{
				return this._isCancelShown;
			}
			set
			{
				if (value != this._isCancelShown)
				{
					this._isCancelShown = value;
					base.OnPropertyChanged(value, "IsCancelShown");
					this.DetermineInitContinueState();
				}
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600032B RID: 811 RVA: 0x0000A5AA File Offset: 0x000087AA
		// (set) Token: 0x0600032C RID: 812 RVA: 0x0000A5B2 File Offset: 0x000087B2
		[Editor(false)]
		public bool IsReady
		{
			get
			{
				return this._isReady;
			}
			set
			{
				if (value != this._isReady)
				{
					this._isReady = value;
					base.OnPropertyChanged(value, "IsReady");
				}
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600032D RID: 813 RVA: 0x0000A5D0 File Offset: 0x000087D0
		// (set) Token: 0x0600032E RID: 814 RVA: 0x0000A5D8 File Offset: 0x000087D8
		[Editor(false)]
		public string AffirmativeTitleText
		{
			get
			{
				return this._affirmativeTitleText;
			}
			set
			{
				if (value != this._affirmativeTitleText)
				{
					this._affirmativeTitleText = value;
					base.OnPropertyChanged<string>(value, "AffirmativeTitleText");
				}
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600032F RID: 815 RVA: 0x0000A5FB File Offset: 0x000087FB
		// (set) Token: 0x06000330 RID: 816 RVA: 0x0000A603 File Offset: 0x00008803
		[Editor(false)]
		public string NegativeTitleText
		{
			get
			{
				return this._negativeTitleText;
			}
			set
			{
				if (value != this._negativeTitleText)
				{
					this._negativeTitleText = value;
					base.OnPropertyChanged<string>(value, "NegativeTitleText");
				}
			}
		}

		// Token: 0x0400013D RID: 317
		private bool _isInClickToContinueState;

		// Token: 0x0400013E RID: 318
		private bool _prevIsClickToContinueActive;

		// Token: 0x0400013F RID: 319
		private bool _initialized;

		// Token: 0x04000140 RID: 320
		private float _clickToContinueStartTime = -1f;

		// Token: 0x04000141 RID: 321
		private string _currentTitleTextToUpdateTo = string.Empty;

		// Token: 0x04000142 RID: 322
		private float _titleChangeStartTime = -1f;

		// Token: 0x04000143 RID: 323
		private float _titleChangeTotalTimeInSeconds = 2f;

		// Token: 0x04000144 RID: 324
		private object _scene;

		// Token: 0x04000145 RID: 325
		private ButtonWidget _cancelButton;

		// Token: 0x04000146 RID: 326
		private ButtonWidget _affirmativeButton;

		// Token: 0x04000147 RID: 327
		private RichTextWidget _clickToContinueTextWidget;

		// Token: 0x04000148 RID: 328
		private Widget _fadeImageWidget;

		// Token: 0x04000149 RID: 329
		private TextWidget _titleTextWidget;

		// Token: 0x0400014A RID: 330
		private float _endProgress;

		// Token: 0x0400014B RID: 331
		private string _affirmativeTitleText;

		// Token: 0x0400014C RID: 332
		private string _negativeTitleText;

		// Token: 0x0400014D RID: 333
		private Widget _preparingVisualWidget;

		// Token: 0x0400014E RID: 334
		private bool _isCancelShown;

		// Token: 0x0400014F RID: 335
		private bool _isOkShown;

		// Token: 0x04000150 RID: 336
		private bool _isReady;
	}
}
