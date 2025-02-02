using System;
using System.Linq;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.GamepadNavigation;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Inventory
{
	// Token: 0x02000131 RID: 305
	public class InventoryTupleExtensionControlsWidget : Widget
	{
		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06000FB7 RID: 4023 RVA: 0x0002B44A File Offset: 0x0002964A
		// (set) Token: 0x06000FB8 RID: 4024 RVA: 0x0002B452 File Offset: 0x00029652
		public Widget NavigationParent { get; set; }

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06000FB9 RID: 4025 RVA: 0x0002B45B File Offset: 0x0002965B
		// (set) Token: 0x06000FBA RID: 4026 RVA: 0x0002B463 File Offset: 0x00029663
		private GamepadNavigationScope _parentScope { get; set; }

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06000FBB RID: 4027 RVA: 0x0002B46C File Offset: 0x0002966C
		// (set) Token: 0x06000FBC RID: 4028 RVA: 0x0002B474 File Offset: 0x00029674
		private GamepadNavigationScope _extensionSliderScope { get; set; }

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06000FBD RID: 4029 RVA: 0x0002B47D File Offset: 0x0002967D
		// (set) Token: 0x06000FBE RID: 4030 RVA: 0x0002B485 File Offset: 0x00029685
		private GamepadNavigationScope _extensionIncreaseDecreaseScope { get; set; }

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06000FBF RID: 4031 RVA: 0x0002B48E File Offset: 0x0002968E
		// (set) Token: 0x06000FC0 RID: 4032 RVA: 0x0002B496 File Offset: 0x00029696
		private GamepadNavigationScope _extensionButtonsScope { get; set; }

		// Token: 0x06000FC1 RID: 4033 RVA: 0x0002B49F File Offset: 0x0002969F
		public InventoryTupleExtensionControlsWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x0002B4A8 File Offset: 0x000296A8
		public void BuildNavigationData()
		{
			if (this._isNavigationActive)
			{
				return;
			}
			if (this.TransferSlider != null)
			{
				this._extensionSliderScope = new GamepadNavigationScope
				{
					ScopeID = "ExtensionSliderScope",
					ParentWidget = this.TransferSlider,
					IsEnabled = false,
					NavigateFromScopeEdges = true
				};
			}
			if (this.IncreaseDecreaseButtonsParent != null)
			{
				this._extensionIncreaseDecreaseScope = new GamepadNavigationScope
				{
					ScopeID = "ExtensionIncreaseDecreaseScope",
					ParentWidget = this.IncreaseDecreaseButtonsParent,
					IsEnabled = false,
					ScopeMovements = GamepadNavigationTypes.Horizontal,
					ExtendDiscoveryAreaTop = -40f,
					ExtendDiscoveryAreaBottom = -10f,
					ExtendDiscoveryAreaRight = -350f
				};
			}
			if (this.ButtonCarrier != null)
			{
				this._extensionButtonsScope = new GamepadNavigationScope
				{
					ScopeID = "ExtensionButtonsScope",
					ParentWidget = this.ButtonCarrier,
					IsEnabled = false,
					ScopeMovements = GamepadNavigationTypes.Horizontal
				};
			}
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x0002B58C File Offset: 0x0002978C
		private void TransitionTick(float dt)
		{
			if (this._currentVisualStateAnimationState == VisualStateAnimationState.None)
			{
				if (!this._isNavigationActive)
				{
					this.AddGamepadNavigationControls();
					base.EventManager.AddLateUpdateAction(this, delegate(float _dt)
					{
						this.NavigateToBestChildScope();
					}, 1);
					return;
				}
			}
			else
			{
				base.EventManager.AddLateUpdateAction(this, new Action<float>(this.TransitionTick), 1);
			}
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x0002B5E4 File Offset: 0x000297E4
		private void AddGamepadNavigationControls()
		{
			if (this.ValidateParentScope() && !this._isNavigationActive)
			{
				if (this._extensionIncreaseDecreaseScope != null)
				{
					base.GamepadNavigationContext.AddNavigationScope(this._extensionIncreaseDecreaseScope, false);
				}
				if (this._extensionSliderScope != null)
				{
					base.GamepadNavigationContext.AddNavigationScope(this._extensionSliderScope, false);
				}
				if (this._extensionButtonsScope != null)
				{
					base.GamepadNavigationContext.AddNavigationScope(this._extensionButtonsScope, false);
				}
				this.SetEnabledAllScopes(true);
				if (this._extensionSliderScope != null)
				{
					this._extensionSliderScope.SetParentScope(this._parentScope);
				}
				if (this._extensionIncreaseDecreaseScope != null)
				{
					this._extensionIncreaseDecreaseScope.SetParentScope(this._parentScope);
				}
				if (this._extensionButtonsScope != null)
				{
					this._extensionButtonsScope.SetParentScope(this._parentScope);
				}
				base.DoNotAcceptNavigation = false;
				this._isNavigationActive = true;
			}
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x0002B6B8 File Offset: 0x000298B8
		private void RemoveGamepadNavigationControls()
		{
			if (this.ValidateParentScope() && this._isNavigationActive)
			{
				this.SetEnabledAllScopes(false);
				if (this._extensionSliderScope != null)
				{
					this._extensionSliderScope.SetParentScope(null);
					base.GamepadNavigationContext.RemoveNavigationScope(this._extensionSliderScope);
					this._extensionSliderScope = null;
				}
				if (this._extensionIncreaseDecreaseScope != null)
				{
					this._extensionIncreaseDecreaseScope.SetParentScope(null);
					base.GamepadNavigationContext.RemoveNavigationScope(this._extensionIncreaseDecreaseScope);
					this._extensionIncreaseDecreaseScope = null;
				}
				if (this._extensionButtonsScope != null)
				{
					this._extensionButtonsScope.SetParentScope(null);
					base.GamepadNavigationContext.RemoveNavigationScope(this._extensionButtonsScope);
					this._extensionButtonsScope = null;
				}
				base.DoNotAcceptNavigation = true;
				this._isNavigationActive = false;
			}
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x0002B774 File Offset: 0x00029974
		private void SetEnabledAllScopes(bool isEnabled)
		{
			if (this._extensionSliderScope != null)
			{
				this._extensionSliderScope.IsEnabled = isEnabled;
			}
			if (this._extensionIncreaseDecreaseScope != null)
			{
				this._extensionIncreaseDecreaseScope.IsEnabled = isEnabled;
			}
			if (this._extensionButtonsScope != null)
			{
				this._extensionButtonsScope.IsEnabled = isEnabled;
			}
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x0002B7B4 File Offset: 0x000299B4
		private void NavigateToBestChildScope()
		{
			if (this._parentScope.IsActiveScope)
			{
				GamepadNavigationScope[] array = new GamepadNavigationScope[]
				{
					this._extensionSliderScope,
					this._extensionButtonsScope,
					this._extensionIncreaseDecreaseScope
				};
				for (int i = 0; i < array.Length; i++)
				{
					if (GauntletGamepadNavigationManager.Instance.TryNavigateTo(array[i]))
					{
						return;
					}
				}
			}
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x0002B80E File Offset: 0x00029A0E
		private bool ValidateParentScope()
		{
			if (this._parentScope == null)
			{
				this._parentScope = this.GetParentScope();
			}
			return this._parentScope != null;
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x0002B830 File Offset: 0x00029A30
		private GamepadNavigationScope GetParentScope()
		{
			Widget navigationParent = this.NavigationParent;
			for (Widget widget = (navigationParent != null) ? navigationParent.ParentWidget : null; widget != null; widget = widget.ParentWidget)
			{
				NavigationScopeTargeter navigationScopeTargeter;
				if ((navigationScopeTargeter = (widget as NavigationScopeTargeter)) != null)
				{
					return navigationScopeTargeter.NavigationScope;
				}
				NavigationScopeTargeter navigationScopeTargeter2 = widget.Children.FirstOrDefault((Widget x) => x is NavigationScopeTargeter) as NavigationScopeTargeter;
				if (navigationScopeTargeter2 != null)
				{
					return navigationScopeTargeter2.NavigationScope;
				}
			}
			return null;
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06000FCA RID: 4042 RVA: 0x0002B8A8 File Offset: 0x00029AA8
		// (set) Token: 0x06000FCB RID: 4043 RVA: 0x0002B8B0 File Offset: 0x00029AB0
		public bool IsExtended
		{
			get
			{
				return this._isExtended;
			}
			set
			{
				if (value != this._isExtended)
				{
					this._isExtended = value;
					base.IsEnabled = this._isExtended;
					this.SetEnabledAllScopes(false);
					if (this._isExtended)
					{
						this.BuildNavigationData();
						base.EventManager.AddLateUpdateAction(this, new Action<float>(this.TransitionTick), 1);
						return;
					}
					this.RemoveGamepadNavigationControls();
				}
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06000FCC RID: 4044 RVA: 0x0002B90E File Offset: 0x00029B0E
		// (set) Token: 0x06000FCD RID: 4045 RVA: 0x0002B916 File Offset: 0x00029B16
		[Editor(false)]
		public Widget TransferSlider
		{
			get
			{
				return this._transferSlider;
			}
			set
			{
				if (this._transferSlider != value)
				{
					this._transferSlider = value;
					base.OnPropertyChanged<Widget>(value, "TransferSlider");
				}
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x06000FCE RID: 4046 RVA: 0x0002B934 File Offset: 0x00029B34
		// (set) Token: 0x06000FCF RID: 4047 RVA: 0x0002B93C File Offset: 0x00029B3C
		[Editor(false)]
		public Widget IncreaseDecreaseButtonsParent
		{
			get
			{
				return this._increaseDecreaseButtonsParent;
			}
			set
			{
				if (this._increaseDecreaseButtonsParent != value)
				{
					this._increaseDecreaseButtonsParent = value;
					base.OnPropertyChanged<Widget>(value, "IncreaseDecreaseButtonsParent");
				}
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x06000FD0 RID: 4048 RVA: 0x0002B95A File Offset: 0x00029B5A
		// (set) Token: 0x06000FD1 RID: 4049 RVA: 0x0002B962 File Offset: 0x00029B62
		[Editor(false)]
		public Widget ButtonCarrier
		{
			get
			{
				return this._buttonCarrier;
			}
			set
			{
				if (this._buttonCarrier != value)
				{
					this._buttonCarrier = value;
					base.OnPropertyChanged<Widget>(value, "ButtonCarrier");
				}
			}
		}

		// Token: 0x0400072C RID: 1836
		private bool _isNavigationActive;

		// Token: 0x0400072D RID: 1837
		private bool _isExtended;

		// Token: 0x0400072E RID: 1838
		private Widget _transferSlider;

		// Token: 0x0400072F RID: 1839
		private Widget _increaseDecreaseButtonsParent;

		// Token: 0x04000730 RID: 1840
		private Widget _buttonCarrier;
	}
}
