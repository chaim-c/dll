using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Missions.Handlers;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.MissionViews;
using TaleWorlds.MountAndBlade.View.MissionViews.Order;
using TaleWorlds.MountAndBlade.View.MissionViews.Singleplayer;
using TaleWorlds.MountAndBlade.View.Screens;
using TaleWorlds.MountAndBlade.ViewModelCollection;
using TaleWorlds.MountAndBlade.ViewModelCollection.Order;
using TaleWorlds.ScreenSystem;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Mission.Singleplayer
{
	// Token: 0x02000034 RID: 52
	[OverrideView(typeof(MissionOrderUIHandler))]
	public class MissionGauntletSingleplayerOrderUIHandler : MissionView, ISiegeDeploymentView
	{
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0000DEEF File Offset: 0x0000C0EF
		private float _minHoldTimeForActivation
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0000DEF6 File Offset: 0x0000C0F6
		// (set) Token: 0x06000246 RID: 582 RVA: 0x0000DEFE File Offset: 0x0000C0FE
		public bool IsSiegeDeployment { get; private set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000DF07 File Offset: 0x0000C107
		// (set) Token: 0x06000248 RID: 584 RVA: 0x0000DF0F File Offset: 0x0000C10F
		public bool IsBattleDeployment { get; private set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0000DF18 File Offset: 0x0000C118
		private bool _isAnyDeployment
		{
			get
			{
				return this.IsSiegeDeployment || this.IsBattleDeployment;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600024A RID: 586 RVA: 0x0000DF2A File Offset: 0x0000C12A
		public bool IsOrderMenuActive
		{
			get
			{
				MissionOrderVM dataSource = this._dataSource;
				return dataSource != null && dataSource.IsToggleOrderShown;
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600024B RID: 587 RVA: 0x0000DF40 File Offset: 0x0000C140
		// (remove) Token: 0x0600024C RID: 588 RVA: 0x0000DF78 File Offset: 0x0000C178
		public event Action<bool> OnCameraControlsToggled;

		// Token: 0x0600024D RID: 589 RVA: 0x0000DFAD File Offset: 0x0000C1AD
		public MissionGauntletSingleplayerOrderUIHandler()
		{
			this.ViewOrderPriority = 14;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000DFC0 File Offset: 0x0000C1C0
		public override void OnMissionScreenTick(float dt)
		{
			base.OnMissionScreenTick(dt);
			this._latestDt = dt;
			this._isReceivingInput = false;
			if (!base.MissionScreen.IsPhotoModeEnabled && (!base.MissionScreen.IsRadialMenuActive || this._dataSource.IsToggleOrderShown))
			{
				this.TickInput(dt);
				this._dataSource.Update();
				if (this._dataSource.IsToggleOrderShown)
				{
					if (this._targetFormationOrderGivenWithActionButton)
					{
						this.SetSuspendTroopPlacer(false);
						this._targetFormationOrderGivenWithActionButton = false;
					}
					this._orderTroopPlacer.IsDrawingForced = this._dataSource.IsMovementSubOrdersShown;
					this._orderTroopPlacer.IsDrawingFacing = this._dataSource.IsFacingSubOrdersShown;
					this._orderTroopPlacer.IsDrawingForming = false;
					if (this.cursorState == MissionOrderVM.CursorState.Face)
					{
						Vec2 orderLookAtDirection = OrderController.GetOrderLookAtDirection(base.Mission.MainAgent.Team.PlayerOrderController.SelectedFormations, base.MissionScreen.OrderFlag.Position.AsVec2);
						base.MissionScreen.OrderFlag.SetArrowVisibility(true, orderLookAtDirection);
					}
					else
					{
						base.MissionScreen.OrderFlag.SetArrowVisibility(false, Vec2.Invalid);
					}
					if (this.cursorState == MissionOrderVM.CursorState.Form)
					{
						float orderFormCustomWidth = OrderController.GetOrderFormCustomWidth(base.Mission.MainAgent.Team.PlayerOrderController.SelectedFormations, base.MissionScreen.OrderFlag.Position);
						base.MissionScreen.OrderFlag.SetWidthVisibility(true, orderFormCustomWidth);
					}
					else
					{
						base.MissionScreen.OrderFlag.SetWidthVisibility(false, -1f);
					}
					if (TaleWorlds.InputSystem.Input.IsGamepadActive)
					{
						OrderItemVM lastSelectedOrderItem = this._dataSource.LastSelectedOrderItem;
						if (lastSelectedOrderItem == null || lastSelectedOrderItem.IsTitle)
						{
							if (this._orderTroopPlacer.SuspendTroopPlacer && this._dataSource.ActiveTargetState == 0)
							{
								this._orderTroopPlacer.SuspendTroopPlacer = false;
							}
						}
						else if (!this._orderTroopPlacer.SuspendTroopPlacer)
						{
							this._orderTroopPlacer.SuspendTroopPlacer = true;
						}
					}
				}
				else if (this._dataSource.TroopController.IsTransferActive || this._isAnyDeployment)
				{
					this._gauntletLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
				}
				else
				{
					if (!this._dataSource.TroopController.IsTransferActive && !this._orderTroopPlacer.SuspendTroopPlacer)
					{
						this._orderTroopPlacer.SuspendTroopPlacer = true;
					}
					this._gauntletLayer.InputRestrictions.ResetInputRestrictions();
				}
				if (this._isAnyDeployment)
				{
					if (base.MissionScreen.SceneLayer.Input.IsKeyDown(InputKey.RightMouseButton) || base.MissionScreen.SceneLayer.Input.IsKeyDown(InputKey.ControllerLTrigger))
					{
						Action<bool> onCameraControlsToggled = this.OnCameraControlsToggled;
						if (onCameraControlsToggled != null)
						{
							onCameraControlsToggled(true);
						}
					}
					else
					{
						Action<bool> onCameraControlsToggled2 = this.OnCameraControlsToggled;
						if (onCameraControlsToggled2 != null)
						{
							onCameraControlsToggled2(false);
						}
					}
				}
				base.MissionScreen.OrderFlag.IsTroop = (this._dataSource.ActiveTargetState == 0);
				this.TickOrderFlag(this._latestDt, false);
				bool flag;
				if (this._dataSource.IsToggleOrderShown)
				{
					if (this._dataSource.OrderSets.Any((OrderSetVM x) => x.ShowOrders))
					{
						flag = (this._dataSource.IsHolding || base.Mission.Mode == MissionMode.Deployment);
						goto IL_348;
					}
				}
				flag = false;
				IL_348:
				bool flag2 = flag;
				if (flag2 != base.MissionScreen.IsRadialMenuActive)
				{
					base.MissionScreen.SetRadialMenuActiveState(flag2);
				}
			}
			this._targetFormationOrderGivenWithActionButton = false;
			this._dataSource.UpdateCanUseShortcuts(this._isReceivingInput);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000E348 File Offset: 0x0000C548
		public override bool OnEscape()
		{
			bool isToggleOrderShown = this._dataSource.IsToggleOrderShown;
			this._dataSource.OnEscape();
			return isToggleOrderShown;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000E360 File Offset: 0x0000C560
		public override void OnMissionScreenActivate()
		{
			base.OnMissionScreenActivate();
			this._dataSource.AfterInitialize();
			this._isInitialized = true;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000E37A File Offset: 0x0000C57A
		public override void OnAgentBuild(Agent agent, Banner banner)
		{
			if (!this._isInitialized)
			{
				return;
			}
			if (agent.IsHuman)
			{
				this._dataSource.TroopController.AddTroops(agent);
			}
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000E39E File Offset: 0x0000C59E
		public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow killingBlow)
		{
			base.OnAgentRemoved(affectedAgent, affectorAgent, agentState, killingBlow);
			if (affectedAgent.IsHuman)
			{
				this._dataSource.TroopController.RemoveTroops(affectedAgent);
			}
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000E3C4 File Offset: 0x0000C5C4
		public override void OnMissionScreenInitialize()
		{
			base.OnMissionScreenInitialize();
			base.MissionScreen.SceneLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("MissionOrderHotkeyCategory"));
			base.MissionScreen.OrderFlag = new OrderFlag(base.Mission, base.MissionScreen);
			this._orderTroopPlacer = base.Mission.GetMissionBehavior<OrderTroopPlacer>();
			base.MissionScreen.SetOrderFlagVisibility(false);
			this._siegeDeploymentHandler = base.Mission.GetMissionBehavior<SiegeDeploymentHandler>();
			this._battleDeploymentHandler = base.Mission.GetMissionBehavior<BattleDeploymentHandler>();
			this._formationTargetHandler = base.Mission.GetMissionBehavior<MissionFormationTargetSelectionHandler>();
			if (this._formationTargetHandler != null)
			{
				this._formationTargetHandler.OnFormationFocused += this.OnFormationFocused;
			}
			this.IsSiegeDeployment = (this._siegeDeploymentHandler != null);
			this.IsBattleDeployment = (this._battleDeploymentHandler != null);
			if (this._isAnyDeployment)
			{
				this._deploymentMissionView = base.Mission.GetMissionBehavior<DeploymentMissionView>();
				if (this._deploymentMissionView != null)
				{
					DeploymentMissionView deploymentMissionView = this._deploymentMissionView;
					deploymentMissionView.OnDeploymentFinish = (OnPlayerDeploymentFinishDelegate)Delegate.Combine(deploymentMissionView.OnDeploymentFinish, new OnPlayerDeploymentFinishDelegate(this.OnDeploymentFinish));
				}
				this._deploymentPointDataSources = new List<DeploymentSiegeMachineVM>();
			}
			this._dataSource = new MissionOrderVM(base.MissionScreen.CombatCamera, this.IsSiegeDeployment ? this._siegeDeploymentHandler.PlayerDeploymentPoints.ToList<DeploymentPoint>() : new List<DeploymentPoint>(), new Action<bool>(this.ToggleScreenRotation), this._isAnyDeployment, new GetOrderFlagPositionDelegate(base.MissionScreen.GetOrderFlagPosition), new OnRefreshVisualsDelegate(this.RefreshVisuals), new ToggleOrderPositionVisibilityDelegate(this.SetSuspendTroopPlacer), new OnToggleActivateOrderStateDelegate(this.OnActivateToggleOrder), new OnToggleActivateOrderStateDelegate(this.OnDeactivateToggleOrder), new OnToggleActivateOrderStateDelegate(this.OnTransferFinished), new OnBeforeOrderDelegate(this.OnBeforeOrder), false);
			this._dataSource.SetCancelInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("ToggleEscapeMenu"));
			this._dataSource.TroopController.SetDoneInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Confirm"));
			this._dataSource.TroopController.SetCancelInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Exit"));
			this._dataSource.TroopController.SetResetInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Reset"));
			if (this.IsSiegeDeployment)
			{
				foreach (DeploymentPoint deploymentPoint in this._siegeDeploymentHandler.PlayerDeploymentPoints)
				{
					DeploymentSiegeMachineVM deploymentSiegeMachineVM = new DeploymentSiegeMachineVM(deploymentPoint, null, base.MissionScreen.CombatCamera, new Action<DeploymentSiegeMachineVM>(this._dataSource.DeploymentController.OnRefreshSelectedDeploymentPoint), new Action<DeploymentPoint>(this._dataSource.DeploymentController.OnEntityHover), false);
					Vec3 v = deploymentPoint.GameEntity.GetFrame().origin;
					for (int i = 0; i < deploymentPoint.GameEntity.ChildCount; i++)
					{
						if (deploymentPoint.GameEntity.GetChild(i).HasTag("deployment_point_icon_target"))
						{
							v += deploymentPoint.GameEntity.GetChild(i).GetFrame().origin;
							break;
						}
					}
					this._deploymentPointDataSources.Add(deploymentSiegeMachineVM);
					deploymentSiegeMachineVM.RemainingCount = 0;
				}
			}
			this._gauntletLayer = new GauntletLayer(this.ViewOrderPriority, "GauntletLayer", false);
			this._gauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericPanelGameKeyCategory"));
			string movieName = (BannerlordConfig.OrderType == 0) ? "OrderBar" : "OrderRadial";
			SpriteData spriteData = UIResourceManager.SpriteData;
			TwoDimensionEngineResourceContext resourceContext = UIResourceManager.ResourceContext;
			ResourceDepot uiresourceDepot = UIResourceManager.UIResourceDepot;
			this._spriteCategory = spriteData.SpriteCategories["ui_order"];
			this._spriteCategory.Load(resourceContext, uiresourceDepot);
			this._movie = this._gauntletLayer.LoadMovie(movieName, this._dataSource);
			base.MissionScreen.AddLayer(this._gauntletLayer);
			if (!this._isAnyDeployment && BannerlordConfig.HideBattleUI)
			{
				this._gauntletLayer.UIContext.ContextAlpha = 0f;
			}
			if (!this._isAnyDeployment && !this._dataSource.IsToggleOrderShown)
			{
				ScreenManager.SetSuspendLayer(this._gauntletLayer, true);
			}
			this._dataSource.InputRestrictions = this._gauntletLayer.InputRestrictions;
			ManagedOptions.OnManagedOptionChanged = (ManagedOptions.OnManagedOptionChangedDelegate)Delegate.Combine(ManagedOptions.OnManagedOptionChanged, new ManagedOptions.OnManagedOptionChangedDelegate(this.OnManagedOptionChanged));
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000E854 File Offset: 0x0000CA54
		public override bool IsReady()
		{
			return this._spriteCategory.IsCategoryFullyLoaded();
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000E864 File Offset: 0x0000CA64
		private void OnManagedOptionChanged(ManagedOptions.ManagedOptionsType changedManagedOptionsType)
		{
			if (changedManagedOptionsType == ManagedOptions.ManagedOptionsType.OrderType)
			{
				this._gauntletLayer.ReleaseMovie(this._movie);
				string movieName = (BannerlordConfig.OrderType == 0) ? "OrderBar" : "OrderRadial";
				this._movie = this._gauntletLayer.LoadMovie(movieName, this._dataSource);
				return;
			}
			if (changedManagedOptionsType != ManagedOptions.ManagedOptionsType.OrderLayoutType)
			{
				if (changedManagedOptionsType == ManagedOptions.ManagedOptionsType.HideBattleUI)
				{
					if (!this._isAnyDeployment)
					{
						this._gauntletLayer.UIContext.ContextAlpha = (BannerlordConfig.HideBattleUI ? 0f : 1f);
						return;
					}
				}
				else if (changedManagedOptionsType == ManagedOptions.ManagedOptionsType.SlowDownOnOrder && !BannerlordConfig.SlowDownOnOrder && this._slowedDownMission)
				{
					base.Mission.RemoveTimeSpeedRequest(864);
				}
				return;
			}
			MissionOrderVM dataSource = this._dataSource;
			if (dataSource == null)
			{
				return;
			}
			dataSource.OnOrderLayoutTypeChanged();
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000E920 File Offset: 0x0000CB20
		public override void OnMissionScreenFinalize()
		{
			base.OnMissionScreenFinalize();
			ManagedOptions.OnManagedOptionChanged = (ManagedOptions.OnManagedOptionChangedDelegate)Delegate.Remove(ManagedOptions.OnManagedOptionChanged, new ManagedOptions.OnManagedOptionChangedDelegate(this.OnManagedOptionChanged));
			if (this._formationTargetHandler != null)
			{
				this._formationTargetHandler.OnFormationFocused -= this.OnFormationFocused;
			}
			this._deploymentPointDataSources = null;
			this._orderTroopPlacer = null;
			this._movie = null;
			this._gauntletLayer = null;
			this._dataSource.OnFinalize();
			this._dataSource = null;
			this._siegeDeploymentHandler = null;
			this._spriteCategory.Unload();
			this._battleDeploymentHandler = null;
			this._formationTargetHandler = null;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000E9C0 File Offset: 0x0000CBC0
		public OrderSetVM GetLastSelectedOrderSet()
		{
			return this._dataSource.LastSelectedOrderSet;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000E9CD File Offset: 0x0000CBCD
		public override void OnConversationBegin()
		{
			base.OnConversationBegin();
			MissionOrderVM dataSource = this._dataSource;
			if (dataSource == null)
			{
				return;
			}
			dataSource.TryCloseToggleOrder(true);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000E9E7 File Offset: 0x0000CBE7
		public void OnActivateToggleOrder()
		{
			this.SetLayerEnabled(true);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000E9F0 File Offset: 0x0000CBF0
		public void OnDeactivateToggleOrder()
		{
			if (!this._dataSource.TroopController.IsTransferActive)
			{
				this.SetLayerEnabled(false);
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000EA0B File Offset: 0x0000CC0B
		private void OnTransferFinished()
		{
			if (!this._isAnyDeployment)
			{
				this.SetLayerEnabled(false);
			}
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000EA1C File Offset: 0x0000CC1C
		public void OnAutoDeploy()
		{
			this._dataSource.DeploymentController.ExecuteAutoDeploy();
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000EA2E File Offset: 0x0000CC2E
		public void OnBeginMission()
		{
			this._dataSource.DeploymentController.ExecuteBeginSiege();
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000EA40 File Offset: 0x0000CC40
		private void SetLayerEnabled(bool isEnabled)
		{
			if (isEnabled)
			{
				if (!base.MissionScreen.IsRadialMenuActive)
				{
					if (this._dataSource == null || this._dataSource.ActiveTargetState == 0)
					{
						this._orderTroopPlacer.SuspendTroopPlacer = false;
					}
					if (!this._slowedDownMission && BannerlordConfig.SlowDownOnOrder)
					{
						base.Mission.AddTimeSpeedRequest(new Mission.TimeSpeedRequest(0.25f, 864));
						this._slowedDownMission = true;
					}
					base.MissionScreen.SetOrderFlagVisibility(true);
					if (this._gauntletLayer != null)
					{
						ScreenManager.SetSuspendLayer(this._gauntletLayer, false);
					}
					Game.Current.EventManager.TriggerEvent<MissionPlayerToggledOrderViewEvent>(new MissionPlayerToggledOrderViewEvent(true));
					return;
				}
			}
			else
			{
				this.SetSuspendTroopPlacer(true);
				if (this._gauntletLayer != null)
				{
					ScreenManager.SetSuspendLayer(this._gauntletLayer, true);
				}
				if (this._slowedDownMission)
				{
					base.Mission.RemoveTimeSpeedRequest(864);
					this._slowedDownMission = false;
				}
				Game.Current.EventManager.TriggerEvent<MissionPlayerToggledOrderViewEvent>(new MissionPlayerToggledOrderViewEvent(false));
			}
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000EB3C File Offset: 0x0000CD3C
		private void OnDeploymentFinish()
		{
			this.IsSiegeDeployment = false;
			this.IsBattleDeployment = false;
			this._dataSource.OnDeploymentFinished();
			this._deploymentPointDataSources.Clear();
			this.SetSuspendTroopPlacer(true);
			this._gauntletLayer.IsFocusLayer = false;
			ScreenManager.TryLoseFocus(this._gauntletLayer);
			base.MissionScreen.SetRadialMenuActiveState(false);
			this._gauntletLayer.UIContext.ContextAlpha = (BannerlordConfig.HideBattleUI ? 0f : 1f);
			if (this._deploymentMissionView != null)
			{
				DeploymentMissionView deploymentMissionView = this._deploymentMissionView;
				deploymentMissionView.OnDeploymentFinish = (OnPlayerDeploymentFinishDelegate)Delegate.Remove(deploymentMissionView.OnDeploymentFinish, new OnPlayerDeploymentFinishDelegate(this.OnDeploymentFinish));
			}
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000EBEC File Offset: 0x0000CDEC
		private void RefreshVisuals()
		{
			if (this.IsSiegeDeployment)
			{
				foreach (DeploymentSiegeMachineVM deploymentSiegeMachineVM in this._deploymentPointDataSources)
				{
					deploymentSiegeMachineVM.RefreshWithDeployedWeapon();
				}
			}
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000EC44 File Offset: 0x0000CE44
		private IOrderable GetFocusedOrderableObject()
		{
			return base.MissionScreen.OrderFlag.FocusedOrderableObject;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000EC56 File Offset: 0x0000CE56
		private void SetSuspendTroopPlacer(bool value)
		{
			this._orderTroopPlacer.SuspendTroopPlacer = value;
			base.MissionScreen.SetOrderFlagVisibility(!value);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000EC73 File Offset: 0x0000CE73
		public void SelectFormationAtIndex(int index)
		{
			this._dataSource.OnSelect(index);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000EC81 File Offset: 0x0000CE81
		public void DeselectFormationAtIndex(int index)
		{
			this._dataSource.TroopController.OnDeselectFormation(index);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000EC94 File Offset: 0x0000CE94
		public void ClearFormationSelection()
		{
			MissionOrderVM dataSource = this._dataSource;
			if (dataSource != null)
			{
				dataSource.DeploymentController.ExecuteCancelSelectedDeploymentPoint();
			}
			MissionOrderVM dataSource2 = this._dataSource;
			if (dataSource2 != null)
			{
				dataSource2.OrderController.ClearSelectedFormations();
			}
			MissionOrderVM dataSource3 = this._dataSource;
			if (dataSource3 == null)
			{
				return;
			}
			dataSource3.TryCloseToggleOrder(false);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000ECD4 File Offset: 0x0000CED4
		public void OnFiltersSet(List<ValueTuple<int, List<int>>> filterData)
		{
			this._dataSource.OnFiltersSet(filterData);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000ECE2 File Offset: 0x0000CEE2
		public void SetIsOrderPreconfigured(bool isOrderPreconfigured)
		{
			this._dataSource.DeploymentController.SetIsOrderPreconfigured(isOrderPreconfigured);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000ECF5 File Offset: 0x0000CEF5
		private void OnBeforeOrder()
		{
			this.TickOrderFlag(this._latestDt, true);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000ED04 File Offset: 0x0000CF04
		private void TickOrderFlag(float dt, bool forceUpdate)
		{
			if ((base.MissionScreen.OrderFlag.IsVisible || forceUpdate) && Utilities.EngineFrameNo != base.MissionScreen.OrderFlag.LatestUpdateFrameNo)
			{
				base.MissionScreen.OrderFlag.Tick(this._latestDt);
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000ED52 File Offset: 0x0000CF52
		private void OnFormationFocused(MBReadOnlyList<Formation> focusedFormations)
		{
			this._focusedFormationsCache = focusedFormations;
			this._dataSource.SetFocusedFormations(this._focusedFormationsCache);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000ED6C File Offset: 0x0000CF6C
		void ISiegeDeploymentView.OnEntityHover(GameEntity hoveredEntity)
		{
			if (!this._gauntletLayer.IsHitThisFrame)
			{
				this._dataSource.DeploymentController.OnEntityHover(hoveredEntity);
			}
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000ED8C File Offset: 0x0000CF8C
		void ISiegeDeploymentView.OnEntitySelection(GameEntity selectedEntity)
		{
			this._dataSource.DeploymentController.OnEntitySelect(selectedEntity);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000ED9F File Offset: 0x0000CF9F
		private void ToggleScreenRotation(bool isLocked)
		{
			MissionScreen.SetFixedMissionCameraActive(isLocked);
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000EDA7 File Offset: 0x0000CFA7
		public MissionOrderVM.CursorState cursorState
		{
			get
			{
				if (this._dataSource.IsFacingSubOrdersShown)
				{
					return MissionOrderVM.CursorState.Face;
				}
				return MissionOrderVM.CursorState.Move;
			}
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000EDBC File Offset: 0x0000CFBC
		private void TickInput(float dt)
		{
			bool displayDialog = ((IMissionScreen)base.MissionScreen).GetDisplayDialog();
			bool flag = base.MissionScreen.SceneLayer.IsHitThisFrame || this._gauntletLayer.IsHitThisFrame;
			if (displayDialog || (TaleWorlds.InputSystem.Input.IsGamepadActive && !flag))
			{
				this._isReceivingInput = false;
				this._dataSource.UpdateCanUseShortcuts(false);
				return;
			}
			this._isReceivingInput = true;
			if (!this.IsSiegeDeployment && !this.IsBattleDeployment)
			{
				if (base.Input.IsGameKeyDown(86) && !this._dataSource.IsToggleOrderShown)
				{
					this._holdTime += dt;
					if (this._holdTime >= this._minHoldTimeForActivation)
					{
						this._dataSource.OpenToggleOrder(true, !this._dataSource.IsHolding);
						this._dataSource.IsHolding = true;
					}
				}
				else if (!base.Input.IsGameKeyDown(86))
				{
					if (this._dataSource.IsHolding && this._dataSource.IsToggleOrderShown)
					{
						this._dataSource.TryCloseToggleOrder(false);
					}
					this._dataSource.IsHolding = false;
					this._holdTime = 0f;
				}
			}
			if (this._dataSource.IsToggleOrderShown)
			{
				if (this._dataSource.ActiveTargetState == 0 && (base.Input.IsKeyReleased(InputKey.LeftMouseButton) || base.Input.IsKeyReleased(InputKey.ControllerRTrigger)))
				{
					OrderItemVM lastSelectedOrderItem = this._dataSource.LastSelectedOrderItem;
					if (lastSelectedOrderItem != null && !lastSelectedOrderItem.IsTitle && TaleWorlds.InputSystem.Input.IsGamepadActive)
					{
						this._dataSource.ApplySelectedOrder();
					}
					else
					{
						switch (this.cursorState)
						{
						case MissionOrderVM.CursorState.Move:
						{
							MBReadOnlyList<Formation> focusedFormationsCache = this._focusedFormationsCache;
							if (focusedFormationsCache != null && focusedFormationsCache.Count > 0)
							{
								this._dataSource.OrderController.SetOrderWithFormation(OrderType.Charge, this._focusedFormationsCache[0]);
								this.SetSuspendTroopPlacer(true);
								this._targetFormationOrderGivenWithActionButton = true;
							}
							else
							{
								IOrderable focusedOrderableObject = this.GetFocusedOrderableObject();
								if (focusedOrderableObject != null)
								{
									if (this._dataSource.OrderController.SelectedFormations.Count > 0)
									{
										this._dataSource.OrderController.SetOrderWithOrderableObject(focusedOrderableObject);
									}
									else
									{
										Debug.FailedAssert("No selected formations when issuing order", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.GauntletUI\\Mission\\Singleplayer\\MissionGauntletSingleplayerOrderUIHandler.cs", "TickInput", 681);
									}
								}
							}
							break;
						}
						case MissionOrderVM.CursorState.Face:
							this._dataSource.OrderController.SetOrderWithPosition(OrderType.LookAtDirection, new WorldPosition(Mission.Current.Scene, UIntPtr.Zero, base.MissionScreen.GetOrderFlagPosition(), false));
							break;
						case MissionOrderVM.CursorState.Form:
							this._dataSource.OrderController.SetOrderWithPosition(OrderType.FormCustom, new WorldPosition(Mission.Current.Scene, UIntPtr.Zero, base.MissionScreen.GetOrderFlagPosition(), false));
							break;
						default:
							Debug.FailedAssert("false", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.GauntletUI\\Mission\\Singleplayer\\MissionGauntletSingleplayerOrderUIHandler.cs", "TickInput", 696);
							break;
						}
					}
				}
				if (base.Input.IsKeyReleased(InputKey.RightMouseButton) && !this._isAnyDeployment)
				{
					this._dataSource.OnEscape();
				}
			}
			else if (this._dataSource.TroopController.IsTransferActive != this._isTransferEnabled)
			{
				this._isTransferEnabled = this._dataSource.TroopController.IsTransferActive;
				if (!this._isTransferEnabled)
				{
					this._gauntletLayer.UIContext.ContextAlpha = (BannerlordConfig.HideBattleUI ? 0f : 1f);
					this._gauntletLayer.IsFocusLayer = false;
					ScreenManager.TryLoseFocus(this._gauntletLayer);
				}
				else
				{
					this._gauntletLayer.UIContext.ContextAlpha = 1f;
					this._gauntletLayer.IsFocusLayer = true;
					ScreenManager.TrySetFocus(this._gauntletLayer);
				}
			}
			else if (this._dataSource.TroopController.IsTransferActive)
			{
				if (this._gauntletLayer.Input.IsHotKeyReleased("Exit"))
				{
					UISoundsHelper.PlayUISound("event:/ui/default");
					this._dataSource.TroopController.ExecuteCancelTransfer();
				}
				else if (this._gauntletLayer.Input.IsHotKeyReleased("Confirm"))
				{
					UISoundsHelper.PlayUISound("event:/ui/default");
					this._dataSource.TroopController.ExecuteConfirmTransfer();
				}
				else if (this._gauntletLayer.Input.IsHotKeyReleased("Reset"))
				{
					UISoundsHelper.PlayUISound("event:/ui/default");
					this._dataSource.TroopController.ExecuteReset();
				}
			}
			int num = -1;
			if ((!TaleWorlds.InputSystem.Input.IsGamepadActive || this._dataSource.IsToggleOrderShown) && !base.DebugInput.IsControlDown())
			{
				if (base.Input.IsGameKeyPressed(68))
				{
					num = 0;
				}
				else if (base.Input.IsGameKeyPressed(69))
				{
					num = 1;
				}
				else if (base.Input.IsGameKeyPressed(70))
				{
					num = 2;
				}
				else if (base.Input.IsGameKeyPressed(71))
				{
					num = 3;
				}
				else if (base.Input.IsGameKeyPressed(72))
				{
					num = 4;
				}
				else if (base.Input.IsGameKeyPressed(73))
				{
					num = 5;
				}
				else if (base.Input.IsGameKeyPressed(74))
				{
					num = 6;
				}
				else if (base.Input.IsGameKeyPressed(75))
				{
					num = 7;
				}
				else if (base.Input.IsGameKeyPressed(76))
				{
					num = 8;
				}
			}
			if (num > -1)
			{
				this._dataSource.OnGiveOrder(num);
			}
			int num2 = -1;
			if (base.Input.IsGameKeyPressed(77))
			{
				num2 = 100;
			}
			else if (base.Input.IsGameKeyPressed(78))
			{
				num2 = 0;
			}
			else if (base.Input.IsGameKeyPressed(79))
			{
				num2 = 1;
			}
			else if (base.Input.IsGameKeyPressed(80))
			{
				num2 = 2;
			}
			else if (base.Input.IsGameKeyPressed(81))
			{
				num2 = 3;
			}
			else if (base.Input.IsGameKeyPressed(82))
			{
				num2 = 4;
			}
			else if (base.Input.IsGameKeyPressed(83))
			{
				num2 = 5;
			}
			else if (base.Input.IsGameKeyPressed(84))
			{
				num2 = 6;
			}
			else if (base.Input.IsGameKeyPressed(85))
			{
				num2 = 7;
			}
			if (!this.IsBattleDeployment && !this.IsSiegeDeployment && this._dataSource.IsToggleOrderShown)
			{
				if (base.Input.IsGameKeyPressed(87))
				{
					this._dataSource.SelectNextTroop(1);
				}
				else if (base.Input.IsGameKeyPressed(88))
				{
					this._dataSource.SelectNextTroop(-1);
				}
				else if (base.Input.IsGameKeyPressed(89))
				{
					this._dataSource.ToggleSelectionForCurrentTroop();
				}
			}
			if (num2 != -1)
			{
				this._dataSource.OnSelect(num2);
			}
			if (base.Input.IsGameKeyPressed(67))
			{
				this._dataSource.ViewOrders();
			}
		}

		// Token: 0x0400014E RID: 334
		private const string _radialOrderMovieName = "OrderRadial";

		// Token: 0x0400014F RID: 335
		private const string _barOrderMovieName = "OrderBar";

		// Token: 0x04000150 RID: 336
		private const float _slowDownAmountWhileOrderIsOpen = 0.25f;

		// Token: 0x04000151 RID: 337
		private const int _missionTimeSpeedRequestID = 864;

		// Token: 0x04000152 RID: 338
		private float _holdTime;

		// Token: 0x04000153 RID: 339
		private DeploymentMissionView _deploymentMissionView;

		// Token: 0x04000154 RID: 340
		private List<DeploymentSiegeMachineVM> _deploymentPointDataSources;

		// Token: 0x04000155 RID: 341
		private OrderTroopPlacer _orderTroopPlacer;

		// Token: 0x04000156 RID: 342
		private GauntletLayer _gauntletLayer;

		// Token: 0x04000157 RID: 343
		private IGauntletMovie _movie;

		// Token: 0x04000158 RID: 344
		private SpriteCategory _spriteCategory;

		// Token: 0x04000159 RID: 345
		private MissionOrderVM _dataSource;

		// Token: 0x0400015A RID: 346
		private SiegeDeploymentHandler _siegeDeploymentHandler;

		// Token: 0x0400015B RID: 347
		private BattleDeploymentHandler _battleDeploymentHandler;

		// Token: 0x0400015C RID: 348
		private MissionFormationTargetSelectionHandler _formationTargetHandler;

		// Token: 0x0400015D RID: 349
		private MBReadOnlyList<Formation> _focusedFormationsCache;

		// Token: 0x04000160 RID: 352
		private bool _isReceivingInput;

		// Token: 0x04000161 RID: 353
		private bool _isInitialized;

		// Token: 0x04000162 RID: 354
		private bool _slowedDownMission;

		// Token: 0x04000163 RID: 355
		private float _latestDt;

		// Token: 0x04000164 RID: 356
		private bool _targetFormationOrderGivenWithActionButton;

		// Token: 0x04000166 RID: 358
		private bool _isTransferEnabled;
	}
}
