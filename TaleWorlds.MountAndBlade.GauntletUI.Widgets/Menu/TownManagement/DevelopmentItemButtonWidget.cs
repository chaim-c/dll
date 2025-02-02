using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Menu.TownManagement
{
	// Token: 0x020000FA RID: 250
	public class DevelopmentItemButtonWidget : ButtonWidget
	{
		// Token: 0x06000D21 RID: 3361 RVA: 0x000245BD File Offset: 0x000227BD
		public DevelopmentItemButtonWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x000245C8 File Offset: 0x000227C8
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			ButtonWidget buttonWidget;
			if (!this._isParentInitialized && (buttonWidget = (base.ParentWidget as ButtonWidget)) != null)
			{
				buttonWidget.ClickEventHandlers.Add(new Action<Widget>(this.OnParentClick));
				this._isParentInitialized = true;
			}
			if (!this.IsDaily)
			{
				this.HandleFocus();
				this.HandleEnabledStates();
				this.DevelopmentFrontVisualWidget.HeightSizePolicy = SizePolicy.Fixed;
				this.DevelopmentFrontVisualWidget.WidthSizePolicy = SizePolicy.Fixed;
				this.DevelopmentFrontVisualWidget.ScaledSuggestedHeight = this.DevelopmentBackVisualWidget.Size.Y;
				this.DevelopmentFrontVisualWidget.ScaledSuggestedWidth = this.DevelopmentBackVisualWidget.Size.X;
				if (this.IsProgressShown)
				{
					if (this.Progress > 0 || this.Level == 0)
					{
						this.ProgressClipWidget.HeightSizePolicy = SizePolicy.Fixed;
						this.ProgressClipWidget.ScaledSuggestedHeight = this.DevelopmentBackVisualWidget.Size.Y * ((float)this.Progress / 100f);
					}
					if (this.Level == 0)
					{
						this.DevelopmentBackVisualWidget.AlphaFactor = 0.8f;
						this.DevelopmentBackVisualWidget.SaturationFactor = -80f;
					}
					else
					{
						this.DevelopmentBackVisualWidget.AlphaFactor = 0.2f;
					}
				}
				else
				{
					this.ProgressClipWidget.HeightSizePolicy = SizePolicy.StretchToParent;
				}
				this.HandleChildrenAlphaValues();
			}
			this.DevelopmentBackVisualWidget.CircularClipEnabled = true;
			this.DevelopmentBackVisualWidget.CircularClipRadius = this.DevelopmentBackVisualWidget.Size.X / 2f * base._inverseScaleToUse - 10f * base._scaleToUse;
			this.DevelopmentBackVisualWidget.CircularClipSmoothingRadius = 3f;
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x00024766 File Offset: 0x00022966
		private void HandleFocus()
		{
			if (base.EventManager.LatestMouseUpWidget != base.ParentWidget && !base.ParentWidget.CheckIsMyChildRecursive(base.EventManager.LatestMouseUpWidget))
			{
				this.IsSelectedItem = false;
			}
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x0002479C File Offset: 0x0002299C
		private void HandleChildrenAlphaValues()
		{
			this.SetAsActiveButtonWidget.Brush.AlphaFactor = (float)(this.IsSelectedItem ? 1 : 0);
			this.AddToQueueButtonWidget.Brush.AlphaFactor = (float)(this.IsSelectedItem ? 1 : 0);
			this.SelectedBlackOverlayWidget.AlphaFactor = (this.IsSelectedItem ? 0.7f : 0f);
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x00024802 File Offset: 0x00022A02
		private void HandleEnabledStates()
		{
			base.ParentWidget.DoNotPassEventsToChildren = !this.IsSelectedItem;
			base.DoNotPassEventsToChildren = !this.IsSelectedItem;
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x00024827 File Offset: 0x00022A27
		private void OnParentClick(Widget widget)
		{
			if (!this.IsSelectedItem && this.CanBuild)
			{
				this.IsSelectedItem = true;
			}
			if (!this.CanBuild)
			{
				DevelopmentNameTextWidget nameTextWidget = this.NameTextWidget;
				if (nameTextWidget == null)
				{
					return;
				}
				nameTextWidget.StartMaxTextAnimation();
			}
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x00024858 File Offset: 0x00022A58
		private void OnAddToQueueClick(Widget widget)
		{
			this.IsSelectedItem = false;
			base.EventFired("OnAddToQueue", Array.Empty<object>());
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x00024871 File Offset: 0x00022A71
		private void OnSetAsActiveDevelopmentClick(Widget widget)
		{
			this.IsSelectedItem = false;
			base.EventFired("SetAsActive", Array.Empty<object>());
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x0002488A File Offset: 0x00022A8A
		private void UpdateDevelopmentLevelVisual(int level)
		{
			if (!this.IsDaily)
			{
				this.DevelopmentLevelVisualWidget.SetState(level.ToString());
				this.DevelopmentLevelVisualWidget.IsVisible = (level >= 0);
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06000D2A RID: 3370 RVA: 0x000248B8 File Offset: 0x00022AB8
		// (set) Token: 0x06000D2B RID: 3371 RVA: 0x000248C0 File Offset: 0x00022AC0
		[Editor(false)]
		public bool IsSelectedItem
		{
			get
			{
				return this._isSelectedItem;
			}
			set
			{
				if (this._isSelectedItem != value)
				{
					this._isSelectedItem = value;
					base.OnPropertyChanged(value, "IsSelectedItem");
					if (this.ActionButtonsForcedScopeTargeter != null && this.ActionButtonsNavigationScopeTargeter != null)
					{
						this.ActionButtonsForcedScopeTargeter.IsCollectionEnabled = value;
						this.ActionButtonsNavigationScopeTargeter.IsScopeEnabled = value;
					}
				}
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06000D2C RID: 3372 RVA: 0x00024911 File Offset: 0x00022B11
		// (set) Token: 0x06000D2D RID: 3373 RVA: 0x00024919 File Offset: 0x00022B19
		[Editor(false)]
		public NavigationScopeTargeter ActionButtonsNavigationScopeTargeter
		{
			get
			{
				return this._actionButtonsNavigationScopeTargeter;
			}
			set
			{
				if (this._actionButtonsNavigationScopeTargeter != value)
				{
					this._actionButtonsNavigationScopeTargeter = value;
					base.OnPropertyChanged<NavigationScopeTargeter>(value, "ActionButtonsNavigationScopeTargeter");
				}
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06000D2E RID: 3374 RVA: 0x00024937 File Offset: 0x00022B37
		// (set) Token: 0x06000D2F RID: 3375 RVA: 0x0002493F File Offset: 0x00022B3F
		[Editor(false)]
		public NavigationForcedScopeCollectionTargeter ActionButtonsForcedScopeTargeter
		{
			get
			{
				return this._actionButtonsForcedScopeTargeter;
			}
			set
			{
				if (this._actionButtonsForcedScopeTargeter != value)
				{
					this._actionButtonsForcedScopeTargeter = value;
					base.OnPropertyChanged<NavigationForcedScopeCollectionTargeter>(value, "ActionButtonsForcedScopeTargeter");
				}
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06000D30 RID: 3376 RVA: 0x0002495D File Offset: 0x00022B5D
		// (set) Token: 0x06000D31 RID: 3377 RVA: 0x00024965 File Offset: 0x00022B65
		[Editor(false)]
		public Widget SelectedBlackOverlayWidget
		{
			get
			{
				return this._selectedBlackOverlayWidget;
			}
			set
			{
				if (this._selectedBlackOverlayWidget != value)
				{
					this._selectedBlackOverlayWidget = value;
					base.OnPropertyChanged<Widget>(value, "SelectedBlackOverlayWidget");
				}
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06000D32 RID: 3378 RVA: 0x00024983 File Offset: 0x00022B83
		// (set) Token: 0x06000D33 RID: 3379 RVA: 0x0002498B File Offset: 0x00022B8B
		[Editor(false)]
		public DevelopmentNameTextWidget NameTextWidget
		{
			get
			{
				return this._nameTextWidget;
			}
			set
			{
				if (this._nameTextWidget != value)
				{
					this._nameTextWidget = value;
					base.OnPropertyChanged<DevelopmentNameTextWidget>(value, "NameTextWidget");
				}
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06000D34 RID: 3380 RVA: 0x000249A9 File Offset: 0x00022BA9
		// (set) Token: 0x06000D35 RID: 3381 RVA: 0x000249B1 File Offset: 0x00022BB1
		[Editor(false)]
		public ButtonWidget AddToQueueButtonWidget
		{
			get
			{
				return this._addToQueueButtonWidget;
			}
			set
			{
				if (this._addToQueueButtonWidget != value)
				{
					this._addToQueueButtonWidget = value;
					base.OnPropertyChanged<ButtonWidget>(value, "AddToQueueButtonWidget");
					value.ClickEventHandlers.Add(new Action<Widget>(this.OnAddToQueueClick));
				}
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06000D36 RID: 3382 RVA: 0x000249E6 File Offset: 0x00022BE6
		// (set) Token: 0x06000D37 RID: 3383 RVA: 0x000249EE File Offset: 0x00022BEE
		[Editor(false)]
		public ButtonWidget SetAsActiveButtonWidget
		{
			get
			{
				return this._setAsActiveButtonWidget;
			}
			set
			{
				if (this._setAsActiveButtonWidget != value)
				{
					this._setAsActiveButtonWidget = value;
					base.OnPropertyChanged<ButtonWidget>(value, "SetAsActiveButtonWidget");
					value.ClickEventHandlers.Add(new Action<Widget>(this.OnSetAsActiveDevelopmentClick));
				}
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06000D38 RID: 3384 RVA: 0x00024A23 File Offset: 0x00022C23
		// (set) Token: 0x06000D39 RID: 3385 RVA: 0x00024A2B File Offset: 0x00022C2B
		[Editor(false)]
		public Widget DevelopmentLevelVisualWidget
		{
			get
			{
				return this._developmentLevelVisualWidget;
			}
			set
			{
				if (this._developmentLevelVisualWidget != value)
				{
					this._developmentLevelVisualWidget = value;
					base.OnPropertyChanged<Widget>(value, "DevelopmentLevelVisualWidget");
					this.UpdateDevelopmentLevelVisual(this.Level);
				}
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06000D3A RID: 3386 RVA: 0x00024A55 File Offset: 0x00022C55
		// (set) Token: 0x06000D3B RID: 3387 RVA: 0x00024A5D File Offset: 0x00022C5D
		[Editor(false)]
		public Widget ProgressClipWidget
		{
			get
			{
				return this._progressClipWidget;
			}
			set
			{
				if (this._progressClipWidget != value)
				{
					this._progressClipWidget = value;
					base.OnPropertyChanged<Widget>(value, "ProgressClipWidget");
				}
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06000D3C RID: 3388 RVA: 0x00024A7B File Offset: 0x00022C7B
		// (set) Token: 0x06000D3D RID: 3389 RVA: 0x00024A83 File Offset: 0x00022C83
		[Editor(false)]
		public bool IsProgressShown
		{
			get
			{
				return this._isProgressShown;
			}
			set
			{
				if (this._isProgressShown != value)
				{
					this._isProgressShown = value;
					base.OnPropertyChanged(value, "IsProgressShown");
				}
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06000D3E RID: 3390 RVA: 0x00024AA1 File Offset: 0x00022CA1
		// (set) Token: 0x06000D3F RID: 3391 RVA: 0x00024AA9 File Offset: 0x00022CA9
		[Editor(false)]
		public bool CanBuild
		{
			get
			{
				return this._canBuild;
			}
			set
			{
				if (this._canBuild != value)
				{
					this._canBuild = value;
					base.OnPropertyChanged(value, "CanBuild");
				}
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06000D40 RID: 3392 RVA: 0x00024AC7 File Offset: 0x00022CC7
		// (set) Token: 0x06000D41 RID: 3393 RVA: 0x00024ACF File Offset: 0x00022CCF
		[Editor(false)]
		public Widget DevelopmentBackVisualWidget
		{
			get
			{
				return this._developmentBackVisualWidget;
			}
			set
			{
				if (this._developmentBackVisualWidget != value)
				{
					this._developmentBackVisualWidget = value;
					base.OnPropertyChanged<Widget>(value, "DevelopmentBackVisualWidget");
				}
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06000D42 RID: 3394 RVA: 0x00024AED File Offset: 0x00022CED
		// (set) Token: 0x06000D43 RID: 3395 RVA: 0x00024AF5 File Offset: 0x00022CF5
		[Editor(false)]
		public Widget DevelopmentFrontVisualWidget
		{
			get
			{
				return this._developmentFrontVisualWidget;
			}
			set
			{
				if (this._developmentFrontVisualWidget != value)
				{
					this._developmentFrontVisualWidget = value;
					base.OnPropertyChanged<Widget>(value, "DevelopmentFrontVisualWidget");
				}
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06000D44 RID: 3396 RVA: 0x00024B13 File Offset: 0x00022D13
		// (set) Token: 0x06000D45 RID: 3397 RVA: 0x00024B1B File Offset: 0x00022D1B
		[Editor(false)]
		public bool IsProgressIndicatorsEnabled
		{
			get
			{
				return this._isProgressIndicatorsEnabled;
			}
			set
			{
				if (this._isProgressIndicatorsEnabled != value)
				{
					this._isProgressIndicatorsEnabled = value;
					base.OnPropertyChanged(value, "IsProgressIndicatorsEnabled");
				}
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06000D46 RID: 3398 RVA: 0x00024B39 File Offset: 0x00022D39
		// (set) Token: 0x06000D47 RID: 3399 RVA: 0x00024B41 File Offset: 0x00022D41
		[Editor(false)]
		public bool IsDaily
		{
			get
			{
				return this._isDaily;
			}
			set
			{
				if (this._isDaily != value)
				{
					this._isDaily = value;
					base.OnPropertyChanged(value, "IsDaily");
				}
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06000D48 RID: 3400 RVA: 0x00024B5F File Offset: 0x00022D5F
		// (set) Token: 0x06000D49 RID: 3401 RVA: 0x00024B67 File Offset: 0x00022D67
		[Editor(false)]
		public int Level
		{
			get
			{
				return this._level;
			}
			set
			{
				if (this._level != value)
				{
					this._level = value;
					base.OnPropertyChanged(value, "Level");
					this.UpdateDevelopmentLevelVisual(value);
				}
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06000D4A RID: 3402 RVA: 0x00024B8C File Offset: 0x00022D8C
		// (set) Token: 0x06000D4B RID: 3403 RVA: 0x00024B94 File Offset: 0x00022D94
		[Editor(false)]
		public int Progress
		{
			get
			{
				return this._progress;
			}
			set
			{
				if (this._progress != value)
				{
					this._progress = value;
					base.OnPropertyChanged(value, "Progress");
				}
			}
		}

		// Token: 0x04000609 RID: 1545
		private bool _isParentInitialized;

		// Token: 0x0400060A RID: 1546
		private bool _isSelectedItem;

		// Token: 0x0400060B RID: 1547
		private int _level;

		// Token: 0x0400060C RID: 1548
		private int _progress;

		// Token: 0x0400060D RID: 1549
		private bool _isDaily;

		// Token: 0x0400060E RID: 1550
		private bool _isProgressIndicatorsEnabled;

		// Token: 0x0400060F RID: 1551
		private Widget _developmentLevelVisualWidget;

		// Token: 0x04000610 RID: 1552
		private Widget _developmentBackVisualWidget;

		// Token: 0x04000611 RID: 1553
		private Widget _developmentFrontVisualWidget;

		// Token: 0x04000612 RID: 1554
		private Widget _selectedBlackOverlayWidget;

		// Token: 0x04000613 RID: 1555
		private NavigationScopeTargeter _actionButtonsNavigationScopeTargeter;

		// Token: 0x04000614 RID: 1556
		private NavigationForcedScopeCollectionTargeter _actionButtonsForcedScopeTargeter;

		// Token: 0x04000615 RID: 1557
		private ButtonWidget _addToQueueButtonWidget;

		// Token: 0x04000616 RID: 1558
		private ButtonWidget _setAsActiveButtonWidget;

		// Token: 0x04000617 RID: 1559
		private DevelopmentNameTextWidget _nameTextWidget;

		// Token: 0x04000618 RID: 1560
		private Widget _progressClipWidget;

		// Token: 0x04000619 RID: 1561
		private bool _isProgressShown;

		// Token: 0x0400061A RID: 1562
		private bool _canBuild;
	}
}
