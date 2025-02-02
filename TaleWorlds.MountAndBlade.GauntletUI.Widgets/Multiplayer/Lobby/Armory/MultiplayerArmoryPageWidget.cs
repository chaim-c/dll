using System;
using System.Linq;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.GamepadNavigation;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Multiplayer.Lobby.Armory
{
	// Token: 0x020000AD RID: 173
	public class MultiplayerArmoryPageWidget : Widget
	{
		// Token: 0x0600091E RID: 2334 RVA: 0x00019FC1 File Offset: 0x000181C1
		public MultiplayerArmoryPageWidget(UIContext context) : base(context)
		{
			base.EventManager.AddLateUpdateAction(this, new Action<float>(this.Update), 1);
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x00019FE4 File Offset: 0x000181E4
		private void Update(float dt)
		{
			if (this.IsTauntAssignmentActive && !Input.IsGamepadActive)
			{
				Widget latestMouseUpWidget = base.EventManager.LatestMouseUpWidget;
				Widget latestMouseDownWidget = base.EventManager.LatestMouseDownWidget;
				if (latestMouseUpWidget != null && latestMouseUpWidget == latestMouseDownWidget && !this.IsWidgetUsedForTauntSelection(latestMouseUpWidget))
				{
					base.EventFired("ReleaseTauntSelections", Array.Empty<object>());
				}
			}
			if (this.TauntSlotsContainer != null && this.TauntCircleActionSelector != null)
			{
				this.TauntCircleActionSelector.IsCircularInputEnabled = (this.IsTauntControlsOpen && this.TauntSlotsContainer.IsPointInsideMeasuredArea(base.EventManager.MousePosition));
			}
			if (this._cosmeticPanelScrollTarget != null && this._cosmeticsScrollablePanel != null)
			{
				this._cosmeticsScrollablePanel.ScrollToChild(this._cosmeticPanelScrollTarget, -1f, 0.5f, 0, 0, 0.3f, 0f);
				this._cosmeticPanelScrollTarget = null;
			}
			base.EventManager.AddLateUpdateAction(this, new Action<float>(this.Update), 1);
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0001A0CC File Offset: 0x000182CC
		private bool IsWidgetUsedForTauntSelection(Widget widget)
		{
			CircleActionSelectorWidget tauntCircleActionSelector = this.TauntCircleActionSelector;
			MultiplayerLobbyArmoryCosmeticItemButtonWidget multiplayerLobbyArmoryCosmeticItemButtonWidget;
			return (tauntCircleActionSelector != null && tauntCircleActionSelector.CheckIsMyChildRecursive(widget)) || ((multiplayerLobbyArmoryCosmeticItemButtonWidget = (widget as MultiplayerLobbyArmoryCosmeticItemButtonWidget)) != null && multiplayerLobbyArmoryCosmeticItemButtonWidget.IsSelected);
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0001A105 File Offset: 0x00018305
		private void RegisterForStateUpdate()
		{
			if (this._isTauntStateDirty)
			{
				return;
			}
			this._isTauntStateDirty = true;
			base.EventManager.AddLateUpdateAction(this, new Action<float>(this.UpdateTauntControlStates), 1);
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0001A130 File Offset: 0x00018330
		private void UpdateTauntControlStates(float dt)
		{
			string state = this.IsTauntControlsOpen ? "TauntEnabled" : "Default";
			if (this.TauntCircleActionSelector != null)
			{
				this.TauntCircleActionSelector.AnimateDistanceFromCenterTo((float)(this.IsTauntControlsOpen ? this.TauntEnabledRadialDistance : this.TauntDisabledRadialDistance), this.TauntStateAnimationDuration);
				this.TauntCircleActionSelector.IsEnabled = this.IsTauntControlsOpen;
				this.TauntCircleActionSelector.SetGlobalAlphaRecursively(this.IsTauntControlsOpen ? 1f : 0.6f);
			}
			Widget tauntSlotsContainer = this.TauntSlotsContainer;
			if (tauntSlotsContainer != null)
			{
				tauntSlotsContainer.SetState(state);
			}
			Widget manageTauntsButton = this.ManageTauntsButton;
			if (manageTauntsButton != null)
			{
				manageTauntsButton.SetState(state);
			}
			Widget leftSideParent = this.LeftSideParent;
			if (leftSideParent != null)
			{
				leftSideParent.SetState(state);
			}
			Widget gameModesDropdownParent = this.GameModesDropdownParent;
			if (gameModesDropdownParent != null)
			{
				gameModesDropdownParent.SetState(state);
			}
			Widget heroPreviewParent = this.HeroPreviewParent;
			if (heroPreviewParent != null)
			{
				heroPreviewParent.SetState(state);
			}
			if (this.RightPanelTabControl != null && this.IsTauntControlsOpen)
			{
				this.RightPanelTabControl.SelectedIndex = 1;
			}
			this._isTauntStateDirty = false;
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0001A230 File Offset: 0x00018430
		private void OnTauntAssignmentStateChanged(bool isTauntAssignmentActive)
		{
			if (isTauntAssignmentActive && this.TauntCircleActionSelector != null)
			{
				if (this.TauntCircleActionSelector.AllChildren.FirstOrDefault(delegate(Widget c)
				{
					ButtonWidget buttonWidget = c as ButtonWidget;
					return buttonWidget != null && buttonWidget.IsSelected;
				}) != null)
				{
					if (this._cosmeticsScrollablePanel == null)
					{
						this._cosmeticsScrollablePanel = (this.RightPanelTabControl.AllChildren.FirstOrDefault((Widget c) => c is ScrollablePanel) as ScrollablePanel);
					}
					if (this._cosmeticsScrollablePanel != null)
					{
						Widget widget = this._cosmeticsScrollablePanel.AllChildren.FirstOrDefault(delegate(Widget c)
						{
							MultiplayerLobbyArmoryCosmeticItemButtonWidget multiplayerLobbyArmoryCosmeticItemButtonWidget;
							return (multiplayerLobbyArmoryCosmeticItemButtonWidget = (c as MultiplayerLobbyArmoryCosmeticItemButtonWidget)) != null && multiplayerLobbyArmoryCosmeticItemButtonWidget.IsSelectable;
						});
						if (widget != null)
						{
							this._cosmeticPanelScrollTarget = widget;
						}
					}
				}
			}
			if (Input.IsGamepadActive && isTauntAssignmentActive)
			{
				GauntletGamepadNavigationManager.Instance.TryNavigateTo(this.ManageTauntsButton);
			}
			base.EventManager.AddLateUpdateAction(this, new Action<float>(this.AnimateTauntAssignmentStates), 1);
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0001A33C File Offset: 0x0001853C
		private void AnimateTauntAssignmentStates(float dt)
		{
			this._tauntAssignmentStateTimer += dt;
			float amount;
			if (this._tauntAssignmentStateTimer < this.TauntStateAnimationDuration)
			{
				amount = this._tauntAssignmentStateTimer / this.TauntStateAnimationDuration;
				base.EventManager.AddLateUpdateAction(this, new Action<float>(this.AnimateTauntAssignmentStates), 1);
			}
			else
			{
				amount = 1f;
			}
			float valueFrom = this.IsTauntAssignmentActive ? 0f : this.TauntAssignmentOverlayAlpha;
			float valueTo = this.IsTauntAssignmentActive ? this.TauntAssignmentOverlayAlpha : 0f;
			float num = MathF.Lerp(valueFrom, valueTo, amount, 1E-05f);
			if (this.TauntAssignmentOverlay != null)
			{
				this.TauntAssignmentOverlay.IsVisible = (num != 0f);
				this.TauntAssignmentOverlay.SetGlobalAlphaRecursively(num);
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000925 RID: 2341 RVA: 0x0001A3F6 File Offset: 0x000185F6
		// (set) Token: 0x06000926 RID: 2342 RVA: 0x0001A3FE File Offset: 0x000185FE
		public bool IsTauntAssignmentActive
		{
			get
			{
				return this._isTauntAssignmentActive;
			}
			set
			{
				if (value != this._isTauntAssignmentActive)
				{
					this._isTauntAssignmentActive = value;
					base.OnPropertyChanged(value, "IsTauntAssignmentActive");
					this._tauntAssignmentStateTimer = 0f;
					this.OnTauntAssignmentStateChanged(value);
				}
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000927 RID: 2343 RVA: 0x0001A42E File Offset: 0x0001862E
		// (set) Token: 0x06000928 RID: 2344 RVA: 0x0001A436 File Offset: 0x00018636
		public bool IsTauntControlsOpen
		{
			get
			{
				return this._isTauntControlsOpen;
			}
			set
			{
				if (value != this._isTauntControlsOpen)
				{
					this._isTauntControlsOpen = value;
					base.OnPropertyChanged(value, "IsTauntControlsOpen");
					this.RegisterForStateUpdate();
				}
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000929 RID: 2345 RVA: 0x0001A45A File Offset: 0x0001865A
		// (set) Token: 0x0600092A RID: 2346 RVA: 0x0001A462 File Offset: 0x00018662
		public int TauntEnabledRadialDistance
		{
			get
			{
				return this._tauntEnabledRadialDistance;
			}
			set
			{
				if (value != this._tauntEnabledRadialDistance)
				{
					this._tauntEnabledRadialDistance = value;
					base.OnPropertyChanged(value, "TauntEnabledRadialDistance");
					this.RegisterForStateUpdate();
				}
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x0001A486 File Offset: 0x00018686
		// (set) Token: 0x0600092C RID: 2348 RVA: 0x0001A48E File Offset: 0x0001868E
		public int TauntDisabledRadialDistance
		{
			get
			{
				return this._tauntDisabledRadialDistance;
			}
			set
			{
				if (value != this._tauntDisabledRadialDistance)
				{
					this._tauntDisabledRadialDistance = value;
					base.OnPropertyChanged(value, "TauntDisabledRadialDistance");
					this.RegisterForStateUpdate();
				}
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x0001A4B2 File Offset: 0x000186B2
		// (set) Token: 0x0600092E RID: 2350 RVA: 0x0001A4BA File Offset: 0x000186BA
		public float TauntStateAnimationDuration
		{
			get
			{
				return this._tauntStateAnimationDuration;
			}
			set
			{
				if (value != this._tauntStateAnimationDuration)
				{
					this._tauntStateAnimationDuration = value;
					base.OnPropertyChanged(value, "TauntStateAnimationDuration");
					this.RegisterForStateUpdate();
				}
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x0001A4DE File Offset: 0x000186DE
		// (set) Token: 0x06000930 RID: 2352 RVA: 0x0001A4E6 File Offset: 0x000186E6
		public float TauntAssignmentOverlayAlpha
		{
			get
			{
				return this._tauntAssignmentOverlayAlpha;
			}
			set
			{
				if (value != this._tauntAssignmentOverlayAlpha)
				{
					this._tauntAssignmentOverlayAlpha = value;
					base.OnPropertyChanged(value, "TauntAssignmentOverlayAlpha");
					this.RegisterForStateUpdate();
				}
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000931 RID: 2353 RVA: 0x0001A50A File Offset: 0x0001870A
		// (set) Token: 0x06000932 RID: 2354 RVA: 0x0001A512 File Offset: 0x00018712
		public Widget LeftSideParent
		{
			get
			{
				return this._leftSideParent;
			}
			set
			{
				if (value != this._leftSideParent)
				{
					this._leftSideParent = value;
					base.OnPropertyChanged<Widget>(value, "LeftSideParent");
					this.RegisterForStateUpdate();
				}
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x0001A536 File Offset: 0x00018736
		// (set) Token: 0x06000934 RID: 2356 RVA: 0x0001A53E File Offset: 0x0001873E
		public Widget GameModesDropdownParent
		{
			get
			{
				return this._gameModesDropdownParent;
			}
			set
			{
				if (value != this._gameModesDropdownParent)
				{
					this._gameModesDropdownParent = value;
					base.OnPropertyChanged<Widget>(value, "GameModesDropdownParent");
					this.RegisterForStateUpdate();
				}
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000935 RID: 2357 RVA: 0x0001A562 File Offset: 0x00018762
		// (set) Token: 0x06000936 RID: 2358 RVA: 0x0001A56A File Offset: 0x0001876A
		public Widget HeroPreviewParent
		{
			get
			{
				return this._heroPreviewParent;
			}
			set
			{
				if (value != this._heroPreviewParent)
				{
					this._heroPreviewParent = value;
					base.OnPropertyChanged<Widget>(value, "HeroPreviewParent");
					this.RegisterForStateUpdate();
				}
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x0001A58E File Offset: 0x0001878E
		// (set) Token: 0x06000938 RID: 2360 RVA: 0x0001A596 File Offset: 0x00018796
		public Widget TauntAssignmentOverlay
		{
			get
			{
				return this._tauntAssignmentOverlay;
			}
			set
			{
				if (value != this._tauntAssignmentOverlay)
				{
					this._tauntAssignmentOverlay = value;
					base.OnPropertyChanged<Widget>(value, "TauntAssignmentOverlay");
				}
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000939 RID: 2361 RVA: 0x0001A5B4 File Offset: 0x000187B4
		// (set) Token: 0x0600093A RID: 2362 RVA: 0x0001A5BC File Offset: 0x000187BC
		public Widget ManageTauntsButton
		{
			get
			{
				return this._manageTauntsButton;
			}
			set
			{
				if (value != this._manageTauntsButton)
				{
					this._manageTauntsButton = value;
					base.OnPropertyChanged<Widget>(value, "ManageTauntsButton");
				}
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x0600093B RID: 2363 RVA: 0x0001A5DA File Offset: 0x000187DA
		// (set) Token: 0x0600093C RID: 2364 RVA: 0x0001A5E2 File Offset: 0x000187E2
		public Widget TauntSlotsContainer
		{
			get
			{
				return this._tauntSlotsContainer;
			}
			set
			{
				if (value != this._tauntSlotsContainer)
				{
					this._tauntSlotsContainer = value;
					base.OnPropertyChanged<Widget>(value, "TauntSlotsContainer");
				}
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x0600093D RID: 2365 RVA: 0x0001A600 File Offset: 0x00018800
		// (set) Token: 0x0600093E RID: 2366 RVA: 0x0001A608 File Offset: 0x00018808
		public TabControl RightPanelTabControl
		{
			get
			{
				return this._rightPanelTabControl;
			}
			set
			{
				if (value != this._rightPanelTabControl)
				{
					this._rightPanelTabControl = value;
					base.OnPropertyChanged<TabControl>(value, "RightPanelTabControl");
					this.RegisterForStateUpdate();
				}
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x0600093F RID: 2367 RVA: 0x0001A62C File Offset: 0x0001882C
		// (set) Token: 0x06000940 RID: 2368 RVA: 0x0001A634 File Offset: 0x00018834
		public CircleActionSelectorWidget TauntCircleActionSelector
		{
			get
			{
				return this._tauntCircleActionSelector;
			}
			set
			{
				if (value != this._tauntCircleActionSelector)
				{
					this._tauntCircleActionSelector = value;
					base.OnPropertyChanged<CircleActionSelectorWidget>(value, "TauntCircleActionSelector");
					if (this._tauntCircleActionSelector != null)
					{
						this._tauntCircleActionSelector.DistanceFromCenterModifier = (float)(this.IsTauntControlsOpen ? this.TauntEnabledRadialDistance : this.TauntDisabledRadialDistance);
					}
					this.RegisterForStateUpdate();
				}
			}
		}

		// Token: 0x04000427 RID: 1063
		private bool _isTauntStateDirty;

		// Token: 0x04000428 RID: 1064
		private float _tauntAssignmentStateTimer;

		// Token: 0x04000429 RID: 1065
		private ScrollablePanel _cosmeticsScrollablePanel;

		// Token: 0x0400042A RID: 1066
		private Widget _cosmeticPanelScrollTarget;

		// Token: 0x0400042B RID: 1067
		private bool _isTauntAssignmentActive;

		// Token: 0x0400042C RID: 1068
		private bool _isTauntControlsOpen;

		// Token: 0x0400042D RID: 1069
		private int _tauntEnabledRadialDistance;

		// Token: 0x0400042E RID: 1070
		private int _tauntDisabledRadialDistance;

		// Token: 0x0400042F RID: 1071
		private float _tauntStateAnimationDuration;

		// Token: 0x04000430 RID: 1072
		private float _tauntAssignmentOverlayAlpha;

		// Token: 0x04000431 RID: 1073
		private Widget _leftSideParent;

		// Token: 0x04000432 RID: 1074
		private Widget _gameModesDropdownParent;

		// Token: 0x04000433 RID: 1075
		private Widget _heroPreviewParent;

		// Token: 0x04000434 RID: 1076
		private Widget _tauntAssignmentOverlay;

		// Token: 0x04000435 RID: 1077
		private Widget _manageTauntsButton;

		// Token: 0x04000436 RID: 1078
		private Widget _tauntSlotsContainer;

		// Token: 0x04000437 RID: 1079
		private TabControl _rightPanelTabControl;

		// Token: 0x04000438 RID: 1080
		private CircleActionSelectorWidget _tauntCircleActionSelector;
	}
}
