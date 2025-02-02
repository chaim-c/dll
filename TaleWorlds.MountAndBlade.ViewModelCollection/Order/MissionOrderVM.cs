using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.ViewModelCollection.Input;
using TaleWorlds.ScreenSystem;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.Order
{
	// Token: 0x02000020 RID: 32
	public class MissionOrderVM : ViewModel
	{
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600024D RID: 589 RVA: 0x0000A048 File Offset: 0x00008248
		private Team Team
		{
			get
			{
				return Mission.Current.PlayerTeam;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600024E RID: 590 RVA: 0x0000A054 File Offset: 0x00008254
		// (set) Token: 0x0600024F RID: 591 RVA: 0x0000A05C File Offset: 0x0000825C
		public OrderItemVM LastSelectedOrderItem { get; private set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000250 RID: 592 RVA: 0x0000A065 File Offset: 0x00008265
		public OrderController OrderController
		{
			get
			{
				return this.Team.PlayerOrderController;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000251 RID: 593 RVA: 0x0000A072 File Offset: 0x00008272
		public bool IsMovementSubOrdersShown
		{
			get
			{
				return this._movementSet.ShowOrders;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000252 RID: 594 RVA: 0x0000A07F File Offset: 0x0000827F
		public bool IsFacingSubOrdersShown
		{
			get
			{
				OrderSetVM facingSet = this._facingSet;
				return facingSet != null && facingSet.ShowOrders;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000253 RID: 595 RVA: 0x0000A092 File Offset: 0x00008292
		// (set) Token: 0x06000254 RID: 596 RVA: 0x0000A09A File Offset: 0x0000829A
		public bool IsTroopPlacingActive
		{
			get
			{
				return this._isTroopPlacingActive;
			}
			set
			{
				this._isTroopPlacingActive = value;
				this._toggleOrderPositionVisibility(!value);
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000255 RID: 597 RVA: 0x0000A0B2 File Offset: 0x000082B2
		// (set) Token: 0x06000256 RID: 598 RVA: 0x0000A0BA File Offset: 0x000082BA
		public OrderSetType LastSelectedOrderSetType
		{
			get
			{
				return this._lastSelectedOrderSetType;
			}
			set
			{
				if (value != this._lastSelectedOrderSetType)
				{
					this._lastSelectedOrderSetType = value;
					this.IsAnyOrderSetActive = (this._lastSelectedOrderSetType != OrderSetType.None);
				}
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000257 RID: 599 RVA: 0x0000A0DE File Offset: 0x000082DE
		public OrderSetVM LastSelectedOrderSet
		{
			get
			{
				return this.OrderSets.FirstOrDefault((OrderSetVM o) => o.OrderSetType == this.LastSelectedOrderSetType);
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0000A0F7 File Offset: 0x000082F7
		public bool PlayerHasAnyTroopUnderThem
		{
			get
			{
				return this.Team.FormationsIncludingEmpty.Any((Formation f) => f.PlayerOwner == Agent.Main && f.CountOfUnits > 0);
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000259 RID: 601 RVA: 0x0000A128 File Offset: 0x00008328
		private Mission Mission
		{
			get
			{
				return Mission.Current;
			}
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000A130 File Offset: 0x00008330
		public MissionOrderVM(Camera deploymentCamera, List<DeploymentPoint> deploymentPoints, Action<bool> toggleMissionInputs, bool isDeployment, GetOrderFlagPositionDelegate getOrderFlagPosition, OnRefreshVisualsDelegate refreshVisuals, ToggleOrderPositionVisibilityDelegate setSuspendTroopPlacer, OnToggleActivateOrderStateDelegate onActivateToggleOrder, OnToggleActivateOrderStateDelegate onDeactivateToggleOrder, OnToggleActivateOrderStateDelegate onTransferTroopsFinishedDelegate, OnBeforeOrderDelegate onBeforeOrderDelegate, bool isMultiplayer)
		{
			this._deploymentCamera = deploymentCamera;
			this._toggleMissionInputs = toggleMissionInputs;
			this._deploymentPoints = deploymentPoints;
			this._getOrderFlagPosition = getOrderFlagPosition;
			this._onRefreshVisuals = refreshVisuals;
			this._toggleOrderPositionVisibility = setSuspendTroopPlacer;
			this._onActivateToggleOrder = onActivateToggleOrder;
			this._onDeactivateToggleOrder = onDeactivateToggleOrder;
			this._onTransferTroopsFinishedDelegate = onTransferTroopsFinishedDelegate;
			this._onBeforeOrderDelegate = onBeforeOrderDelegate;
			this._isMultiplayer = isMultiplayer;
			this.IsDeployment = isDeployment;
			this.OrderSetsWithOrdersByType = new Dictionary<OrderSetType, OrderSetVM>();
			this.OrderSets = new MBBindingList<OrderSetVM>();
			this.DeploymentController = new MissionOrderDeploymentControllerVM(this._deploymentPoints, this, this._deploymentCamera, this._toggleMissionInputs, this._onRefreshVisuals);
			this.TroopController = new MissionOrderTroopControllerVM(this, this._isMultiplayer, this.IsDeployment, new Action(this.OnTransferFinished));
			this.PopulateOrderSets();
			this.Team.OnFormationAIActiveBehaviorChanged += this.TeamOnFormationAIActiveBehaviorChanged;
			OrderTroopItemVM.OnSelectionChange += this.OnTroopItemSelectionStateChanged;
			this.RefreshValues();
			this.Mission.OnMainAgentChanged += this.MissionOnMainAgentChanged;
			this.UpdateCanUseShortcuts(this._isMultiplayer);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000A260 File Offset: 0x00008460
		public override void RefreshValues()
		{
			base.RefreshValues();
			this.ReturnText = new TextObject("{=EmVbbIUc}Return", null).ToString();
			foreach (OrderSetVM orderSetVM in this.OrderSetsWithOrdersByType.Values)
			{
				orderSetVM.RefreshValues();
			}
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000A2D4 File Offset: 0x000084D4
		public override void OnFinalize()
		{
			base.OnFinalize();
			OrderTroopItemVM.OnSelectionChange -= this.OnTroopItemSelectionStateChanged;
			this.Mission.OnMainAgentChanged -= this.MissionOnMainAgentChanged;
			this.DeploymentController.OnFinalize();
			this.TroopController.OnFinalize();
			this._deploymentPoints.Clear();
			foreach (OrderSetVM orderSetVM in this._orderSets)
			{
				orderSetVM.OnFinalize();
			}
			this.InputRestrictions = null;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000A374 File Offset: 0x00008574
		private void PopulateOrderSets()
		{
			this._movementSet = new OrderSetVM(OrderSetType.Movement, new Action<OrderItemVM, OrderSetType, bool>(this.OnOrder), this._isMultiplayer);
			OrderSetVM orderSetVM = new OrderSetVM(OrderSetType.Form, new Action<OrderItemVM, OrderSetType, bool>(this.OnOrder), this._isMultiplayer);
			bool flag = BannerlordConfig.OrderLayoutType == 1;
			this.OrderSets.Add(this._movementSet);
			this.OrderSetsWithOrdersByType.Add(OrderSetType.Movement, this._movementSet);
			if (flag)
			{
				this._facingSet = new OrderSetVM(OrderSetType.Facing, new Action<OrderItemVM, OrderSetType, bool>(this.OnOrder), this._isMultiplayer);
				this.OrderSets.Add(this._facingSet);
				this.OrderSets.Add(orderSetVM);
				this.OrderSetsWithOrdersByType.Add(OrderSetType.Facing, this._facingSet);
				this.OrderSetsWithOrdersByType.Add(OrderSetType.Form, orderSetVM);
			}
			else
			{
				OrderSetVM orderSetVM2 = new OrderSetVM(OrderSetType.Toggle, new Action<OrderItemVM, OrderSetType, bool>(this.OnOrder), this._isMultiplayer);
				this.OrderSets.Add(orderSetVM);
				this.OrderSets.Add(orderSetVM2);
				this.OrderSetsWithOrdersByType.Add(OrderSetType.Form, orderSetVM);
				this.OrderSetsWithOrdersByType.Add(OrderSetType.Toggle, orderSetVM2);
			}
			OrderSetVM item = new OrderSetVM(OrderSubType.ToggleFire, this.OrderSets.Count, new Action<OrderItemVM, OrderSetType, bool>(this.OnOrder), this._isMultiplayer);
			this.OrderSets.Add(item);
			OrderSetVM item2 = new OrderSetVM(OrderSubType.ToggleMount, this.OrderSets.Count, new Action<OrderItemVM, OrderSetType, bool>(this.OnOrder), this._isMultiplayer);
			this.OrderSets.Add(item2);
			OrderSetVM item3 = new OrderSetVM(OrderSubType.ToggleAI, this.OrderSets.Count, new Action<OrderItemVM, OrderSetType, bool>(this.OnOrder), this._isMultiplayer);
			this.OrderSets.Add(item3);
			if (flag)
			{
				if (!this._isMultiplayer)
				{
					OrderSetVM item4 = new OrderSetVM(OrderSubType.ToggleTransfer, this.OrderSets.Count, new Action<OrderItemVM, OrderSetType, bool>(this.OnOrder), this._isMultiplayer);
					this.OrderSets.Add(item4);
					return;
				}
			}
			else
			{
				OrderSetVM item5 = new OrderSetVM(OrderSubType.ActivationFaceDirection, this.OrderSets.Count, new Action<OrderItemVM, OrderSetType, bool>(this.OnOrder), this._isMultiplayer);
				this.OrderSets.Add(item5);
				OrderSetVM item6 = new OrderSetVM(OrderSubType.FormClose, this.OrderSets.Count, new Action<OrderItemVM, OrderSetType, bool>(this.OnOrder), this._isMultiplayer);
				this.OrderSets.Add(item6);
				OrderSetVM item7 = new OrderSetVM(OrderSubType.FormLine, this.OrderSets.Count, new Action<OrderItemVM, OrderSetType, bool>(this.OnOrder), this._isMultiplayer);
				this.OrderSets.Add(item7);
			}
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000A5FB File Offset: 0x000087FB
		private void TeamOnFormationAIActiveBehaviorChanged(Formation formation)
		{
			if (formation.IsAIControlled)
			{
				if (this._modifiedAIFormations.IndexOf(formation) < 0)
				{
					this._modifiedAIFormations.Add(formation);
				}
				this._delayValueForAIFormationModifications = 3;
			}
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000A628 File Offset: 0x00008828
		private void DisplayFormationAIFeedback()
		{
			this._delayValueForAIFormationModifications = Math.Max(0, this._delayValueForAIFormationModifications - 1);
			if (this._delayValueForAIFormationModifications == 0 && this._modifiedAIFormations.Count > 0)
			{
				for (int i = 0; i < this._modifiedAIFormations.Count; i++)
				{
					Formation formation = this._modifiedAIFormations[i];
					if (((formation != null) ? formation.AI.ActiveBehavior : null) != null && formation.FormationIndex < FormationClass.NumberOfRegularFormations)
					{
						MissionOrderVM.DisplayFormationAIFeedbackAux(this._modifiedAIFormations);
					}
					else
					{
						this._modifiedAIFormations[i] = null;
					}
				}
				this._modifiedAIFormations.Clear();
			}
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000A6C4 File Offset: 0x000088C4
		private static void DisplayFormationAIFeedbackAux(List<Formation> formations)
		{
			Dictionary<FormationClass, TextObject> dictionary = new Dictionary<FormationClass, TextObject>();
			Type type = null;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			for (int i = 0; i < formations.Count; i++)
			{
				Formation formation = formations[i];
				if (((formation != null) ? formation.AI.ActiveBehavior : null) != null && (type == null || type == formation.AI.ActiveBehavior.GetType()))
				{
					type = formation.AI.ActiveBehavior.GetType();
					switch (formation.AI.Side)
					{
					case FormationAI.BehaviorSide.Left:
						flag = true;
						break;
					case FormationAI.BehaviorSide.Middle:
						flag3 = true;
						break;
					case FormationAI.BehaviorSide.Right:
						flag2 = true;
						break;
					}
					if (!dictionary.ContainsKey(formation.PhysicalClass))
					{
						TextObject localizedName = formation.PhysicalClass.GetLocalizedName();
						TextObject textObject = GameTexts.FindText("str_troop_group_name_definite", null);
						textObject.SetTextVariable("FORMATION_CLASS", localizedName);
						dictionary.Add(formation.PhysicalClass, textObject);
					}
					formations[i] = null;
				}
			}
			if (dictionary.Count == 1)
			{
				MBTextManager.SetTextVariable("IS_PLURAL", 0);
				MBTextManager.SetTextVariable("TROOP_NAMES_BEGIN", TextObject.Empty, false);
				MBTextManager.SetTextVariable("TROOP_NAMES_END", dictionary.First<KeyValuePair<FormationClass, TextObject>>().Value, false);
			}
			else
			{
				MBTextManager.SetTextVariable("IS_PLURAL", 1);
				TextObject value = dictionary.Last<KeyValuePair<FormationClass, TextObject>>().Value;
				TextObject textObject2;
				if (dictionary.Count == 2)
				{
					textObject2 = dictionary.First<KeyValuePair<FormationClass, TextObject>>().Value;
				}
				else
				{
					textObject2 = GameTexts.FindText("str_LEFT_comma_RIGHT", null);
					textObject2.SetTextVariable("LEFT", dictionary.First<KeyValuePair<FormationClass, TextObject>>().Value);
					textObject2.SetTextVariable("RIGHT", dictionary.Last<KeyValuePair<FormationClass, TextObject>>().Value);
					for (int j = 2; j < dictionary.Count - 1; j++)
					{
						TextObject textObject3 = GameTexts.FindText("str_LEFT_comma_RIGHT", null);
						textObject3.SetTextVariable("LEFT", textObject2);
						textObject3.SetTextVariable("RIGHT", dictionary.Values.ElementAt(j));
						textObject2 = textObject3;
					}
				}
				MBTextManager.SetTextVariable("TROOP_NAMES_BEGIN", textObject2, false);
				MBTextManager.SetTextVariable("TROOP_NAMES_END", value, false);
			}
			bool flag4 = (flag ? 1 : 0) + (flag3 ? 1 : 0) + (flag2 ? 1 : 0) > 1;
			MBTextManager.SetTextVariable("IS_LEFT", flag4 ? 2 : (flag ? 1 : 0));
			MBTextManager.SetTextVariable("IS_MIDDLE", (!flag4 && flag3) ? 1 : 0);
			MBTextManager.SetTextVariable("IS_RIGHT", (!flag4 && flag2) ? 1 : 0);
			string name = type.Name;
			InformationManager.DisplayMessage(new InformationMessage(GameTexts.FindText("str_formation_ai_behavior_text", name).ToString()));
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000A984 File Offset: 0x00008B84
		private void OnTroopItemSelectionStateChanged(OrderTroopItemVM troopItem, bool isSelected)
		{
			for (int i = 0; i < this.TroopController.TroopList.Count; i++)
			{
				this.TroopController.TroopList[i].IsTargetRelevant = this.TroopController.TroopList[i].IsSelected;
			}
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000A9D8 File Offset: 0x00008BD8
		public void OnOrderLayoutTypeChanged()
		{
			this.TroopController = new MissionOrderTroopControllerVM(this, this._isMultiplayer, this.IsDeployment, new Action(this.OnTransferFinished));
			this.OrderSets.Clear();
			this.OrderSetsWithOrdersByType.Clear();
			this.PopulateOrderSets();
			this.TroopController.UpdateTroops();
			this.TroopController.TroopList.ApplyActionOnAllItems(delegate(OrderTroopItemVM x)
			{
				this.TroopController.SetTroopActiveOrders(x);
			});
			this.TroopController.OnFiltersSet(this._filterData);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000AA60 File Offset: 0x00008C60
		public void OpenToggleOrder(bool fromHold, bool displayMessage = true)
		{
			if (this.IsToggleOrderShown)
			{
				return;
			}
			if (this.CheckCanBeOpened(displayMessage))
			{
				Mission.Current.IsOrderMenuOpen = true;
				this._currentActivationType = (fromHold ? MissionOrderVM.ActivationType.Hold : MissionOrderVM.ActivationType.Click);
				this.IsToggleOrderShown = true;
				this.TroopController.IsTransferActive = false;
				this.DeploymentController.ProcessSiegeMachines();
				if (this.OrderController.SelectedFormations.IsEmpty<Formation>())
				{
					this.TroopController.SelectAllFormations(true);
				}
				this.SetActiveOrders();
			}
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000AADC File Offset: 0x00008CDC
		private bool CheckCanBeOpened(bool displayMessage = false)
		{
			if (Agent.Main == null)
			{
				if (displayMessage)
				{
					InformationManager.DisplayMessage(new InformationMessage(new TextObject("{=GMhOZGnb}Cannot issue order while dead.", null).ToString()));
				}
				return false;
			}
			if (Mission.Current.Mode != MissionMode.Deployment && !Agent.Main.IsPlayerControlled)
			{
				if (displayMessage)
				{
					InformationManager.DisplayMessage(new InformationMessage(new TextObject("{=b1DHZsaH}Cannot issue order right now.", null).ToString()));
				}
				return false;
			}
			if (!this.Team.HasBots || !this.PlayerHasAnyTroopUnderThem || (!this.Team.IsPlayerGeneral && !this.Team.IsPlayerSergeant))
			{
				if (displayMessage)
				{
					InformationManager.DisplayMessage(new InformationMessage(new TextObject("{=DQvGNQ0g}There isn't any unit under command.", null).ToString()));
				}
				return false;
			}
			return !Mission.Current.IsMissionEnding || Mission.Current.CheckIfBattleInRetreat();
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000ABB0 File Offset: 0x00008DB0
		public bool TryCloseToggleOrder(bool dontApplySelected = false)
		{
			if (this.IsToggleOrderShown)
			{
				Mission.Current.IsOrderMenuOpen = false;
				if (this.LastSelectedOrderItem != null && !dontApplySelected)
				{
					this.ApplySelectedOrder();
				}
				this.LastSelectedOrderSetType = OrderSetType.None;
				this.LastSelectedOrderItem = null;
				this.OrderSets.ApplyActionOnAllItems(delegate(OrderSetVM s)
				{
					s.Orders.ApplyActionOnAllItems(delegate(OrderItemVM o)
					{
						o.IsSelected = false;
					});
				});
				bool isDeployment = this._isDeployment;
				this.IsToggleOrderShown = false;
				this.UpdateTitleOrdersKeyVisualVisibility();
				if (!this.IsDeployment)
				{
					this.InputRestrictions.ResetInputRestrictions();
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000AC44 File Offset: 0x00008E44
		internal void SetActiveOrders()
		{
			bool flag = this.ActiveTargetState == 1;
			if (flag)
			{
				this.DeploymentController.SetCurrentActiveOrders();
			}
			else
			{
				this.TroopController.SetCurrentActiveOrders();
			}
			List<OrderSubjectVM> list = (from item in flag ? this.DeploymentController.SiegeMachineList.Cast<OrderSubjectVM>().ToList<OrderSubjectVM>() : this.TroopController.TroopList.Cast<OrderSubjectVM>().ToList<OrderSubjectVM>()
			where item.IsSelected && item.IsSelectable
			select item).ToList<OrderSubjectVM>();
			this.OrderSetsWithOrdersByType[OrderSetType.Movement].ResetActiveStatus(false);
			this.OrderSetsWithOrdersByType[OrderSetType.Form].ResetActiveStatus(flag && list.IsEmpty<OrderSubjectVM>());
			if (this.OrderSetsWithOrdersByType.ContainsKey(OrderSetType.Toggle))
			{
				this.OrderSetsWithOrdersByType[OrderSetType.Toggle].ResetActiveStatus(flag);
			}
			if (this.OrderSetsWithOrdersByType.ContainsKey(OrderSetType.Facing))
			{
				this.OrderSetsWithOrdersByType[OrderSetType.Facing].ResetActiveStatus(flag);
			}
			foreach (OrderSetVM orderSetVM in from s in this.OrderSets
			where !s.ContainsOrders
			select s)
			{
				orderSetVM.ResetActiveStatus(false);
			}
			if (list.Count > 0)
			{
				List<OrderItemVM> list2 = new List<OrderItemVM>();
				foreach (OrderSubjectVM orderSubjectVM in list)
				{
					foreach (OrderItemVM item2 in orderSubjectVM.ActiveOrders)
					{
						if (!list2.Contains(item2))
						{
							list2.Add(item2);
						}
					}
				}
				foreach (OrderItemVM orderItemVM in list2)
				{
					orderItemVM.SelectionState = 2;
					if (orderItemVM.IsTitle)
					{
						orderItemVM.SetActiveState(true);
					}
					if (orderItemVM.OrderSetType != OrderSetType.None)
					{
						this.OrderSetsWithOrdersByType[orderItemVM.OrderSetType].SetActiveOrder(this.OrderSetsWithOrdersByType[orderItemVM.OrderSetType].TitleOrder);
					}
				}
				list2 = list[0].ActiveOrders;
				foreach (OrderSubjectVM orderSubjectVM2 in list)
				{
					list2 = list2.Intersect(orderSubjectVM2.ActiveOrders).ToList<OrderItemVM>();
					if (list2.IsEmpty<OrderItemVM>())
					{
						break;
					}
				}
				foreach (OrderItemVM orderItemVM2 in list2)
				{
					orderItemVM2.SelectionState = 3;
					if (orderItemVM2.OrderSetType != OrderSetType.None)
					{
						this.OrderSetsWithOrdersByType[orderItemVM2.OrderSetType].SetActiveOrder(orderItemVM2);
					}
				}
			}
			this.OrderSetsWithOrdersByType[OrderSetType.Movement].FinalizeActiveStatus(false);
			this.OrderSetsWithOrdersByType[OrderSetType.Form].FinalizeActiveStatus(flag && list.IsEmpty<OrderSubjectVM>());
			if (this.OrderSetsWithOrdersByType.ContainsKey(OrderSetType.Toggle))
			{
				this.OrderSetsWithOrdersByType[OrderSetType.Toggle].FinalizeActiveStatus(flag);
			}
			if (this.OrderSetsWithOrdersByType.ContainsKey(OrderSetType.Facing))
			{
				this.OrderSetsWithOrdersByType[OrderSetType.Facing].FinalizeActiveStatus(flag);
			}
			foreach (OrderSetVM orderSetVM2 in from s in this.OrderSets
			where !s.ContainsOrders
			select s)
			{
				orderSetVM2.FinalizeActiveStatus(false);
			}
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000B054 File Offset: 0x00009254
		private void OnOrder(OrderItemVM orderItem, OrderSetType orderSetType, bool fromSelection)
		{
			if (this.LastSelectedOrderItem != orderItem || !fromSelection)
			{
				this._onBeforeOrderDelegate();
				if (this.LastSelectedOrderItem != null)
				{
					this.LastSelectedOrderItem.IsSelected = false;
				}
				if (orderItem.IsTitle)
				{
					this.LastSelectedOrderSetType = orderSetType;
				}
				this.LastSelectedOrderItem = orderItem;
				if (this.LastSelectedOrderItem != null)
				{
					this.LastSelectedOrderItem.IsSelected = true;
					if (this.LastSelectedOrderItem.OrderSubType == OrderSubType.None)
					{
						this.OrderSetsWithOrdersByType[this.LastSelectedOrderSetType].ShowOrders = false;
						this.LastSelectedOrderSetType = orderSetType;
						this.OrderSetsWithOrdersByType[this.LastSelectedOrderSetType].ShowOrders = true;
					}
				}
				if (this.LastSelectedOrderItem != null && this.LastSelectedOrderItem.OrderSubType != OrderSubType.None && !fromSelection)
				{
					OrderSetVM orderSetVM;
					if (this.LastSelectedOrderItem.OrderSubType == OrderSubType.Return && this.OrderSetsWithOrdersByType.TryGetValue(this.LastSelectedOrderSetType, out orderSetVM))
					{
						orderSetVM.ShowOrders = false;
						this.UpdateTitleOrdersKeyVisualVisibility();
						this.LastSelectedOrderSetType = OrderSetType.None;
					}
					else if (this._currentActivationType == MissionOrderVM.ActivationType.Hold && this.LastSelectedOrderSetType != OrderSetType.None)
					{
						this.ApplySelectedOrder();
						if (this.LastSelectedOrderItem != null && this.LastSelectedOrderSetType != OrderSetType.None)
						{
							this.OrderSetsWithOrdersByType[this.LastSelectedOrderSetType].ShowOrders = false;
							this.LastSelectedOrderSetType = OrderSetType.None;
							this.LastSelectedOrderItem = null;
						}
						this.OrderSets.ApplyActionOnAllItems(delegate(OrderSetVM s)
						{
							s.Orders.ApplyActionOnAllItems(delegate(OrderItemVM o)
							{
								o.IsSelected = false;
							});
						});
					}
					else if (this.IsDeployment)
					{
						this.ApplySelectedOrder();
					}
					else
					{
						this.TryCloseToggleOrder(false);
					}
				}
				if (!fromSelection)
				{
					this.UpdateTitleOrdersKeyVisualVisibility();
				}
			}
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000B1F8 File Offset: 0x000093F8
		private void UpdateTitleOrdersKeyVisualVisibility()
		{
			bool flag = this.OrderSets.Any((OrderSetVM o) => o.ShowOrders && o.Orders.Count > 0);
			bool? forcedVisibility = null;
			if (flag)
			{
				forcedVisibility = new bool?(false);
			}
			for (int i = 0; i < this.OrderSets.Count; i++)
			{
				this.OrderSets[i].TitleOrder.ShortcutKey.SetForcedVisibility(forcedVisibility);
			}
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000B273 File Offset: 0x00009473
		public void SetFocusedFormations(MBReadOnlyList<Formation> focusedFormationsCache)
		{
			this._focusedFormationsCache = focusedFormationsCache;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000B27C File Offset: 0x0000947C
		public void ApplySelectedOrder()
		{
			bool flag = this._isPressedViewOrders || (this.LastSelectedOrderItem.OrderSetType == OrderSetType.None && this.LastSelectedOrderItem.IsTitle) || !this.LastSelectedOrderItem.IsTitle;
			if (this.LastSelectedOrderItem == null)
			{
				return;
			}
			if (this.LastSelectedOrderItem.OrderSubType == OrderSubType.Return)
			{
				OrderSetVM orderSetVM;
				if (this.OrderSetsWithOrdersByType.TryGetValue(this.LastSelectedOrderSetType, out orderSetVM))
				{
					orderSetVM.ShowOrders = false;
					this.UpdateTitleOrdersKeyVisualVisibility();
					this.LastSelectedOrderSetType = OrderSetType.None;
				}
				else
				{
					this.TryCloseToggleOrder(true);
				}
				this.LastSelectedOrderItem = null;
				return;
			}
			List<TextObject> list = new List<TextObject>();
			if (this.OrderController.SelectedFormations.Count == 0 && this.ActiveTargetState == 0)
			{
				this.TroopController.UpdateTroops();
				this.TroopController.SelectAllFormations(true);
			}
			if (this.LastSelectedOrderItem.OrderSubType != OrderSubType.ToggleTransfer && flag)
			{
				if (this.ActiveTargetState == 1)
				{
					using (IEnumerator<OrderSiegeMachineVM> enumerator = (from item in this.DeploymentController.SiegeMachineList
					where item.IsSelected && ((this.LastSelectedOrderItem.OrderSetType != OrderSetType.Toggle && this.LastSelectedOrderItem.OrderSubType != OrderSubType.ToggleFacing) || !item.IsPrimarySiegeMachine)
					select item).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							OrderSiegeMachineVM orderSiegeMachineVM = enumerator.Current;
							list.Add(GameTexts.FindText("str_siege_engine", orderSiegeMachineVM.MachineClass));
						}
						goto IL_1B4;
					}
				}
				foreach (OrderTroopItemVM orderTroopItemVM in from item in this.TroopController.TroopList
				where item.IsSelected
				select item)
				{
					list.Add(GameTexts.FindText("str_formation_class_string", orderTroopItemVM.Formation.PhysicalClass.GetName()));
				}
			}
			IL_1B4:
			if (!list.IsEmpty<TextObject>())
			{
				TextObject textObject = new TextObject("{=ApD0xQXT}{STR1}: {STR2}", null);
				textObject.SetTextVariable("STR1", GameTexts.GameTextHelper.MergeTextObjectsWithComma(list, false));
				textObject.SetTextVariable("STR2", this.LastSelectedOrderItem.MainTitle);
				InformationManager.DisplayMessage(new InformationMessage(textObject.ToString()));
			}
			if (this.LastSelectedOrderSetType != OrderSetType.None)
			{
				this.OrderSetsWithOrdersByType[this.LastSelectedOrderSetType].ShowOrders = false;
				foreach (OrderSetVM orderSetVM2 in this.OrderSetsWithOrdersByType.Values)
				{
					orderSetVM2.TitleOrder.IsActive = (orderSetVM2.TitleOrder.SelectionState != 0);
				}
			}
			if (this.ActiveTargetState == 0)
			{
				switch (this.LastSelectedOrderItem.OrderSubType)
				{
				case OrderSubType.None:
				case OrderSubType.Return:
					return;
				case OrderSubType.MoveToPosition:
				{
					Vec3 position = this._getOrderFlagPosition();
					WorldPosition worldPosition = new WorldPosition(this.Mission.Scene, UIntPtr.Zero, position, false);
					if (this.Mission.IsFormationUnitPositionAvailable(ref worldPosition, this.Team))
					{
						this.OrderController.SetOrderWithTwoPositions(OrderType.MoveToLineSegment, worldPosition, worldPosition);
						goto IL_830;
					}
					goto IL_830;
				}
				case OrderSubType.Charge:
				{
					MBReadOnlyList<Formation> focusedFormationsCache = this._focusedFormationsCache;
					if (focusedFormationsCache != null && focusedFormationsCache.Count > 0)
					{
						this.OrderController.SetOrderWithFormation(OrderType.Charge, this._focusedFormationsCache[0]);
						goto IL_830;
					}
					this.OrderController.SetOrder(OrderType.Charge);
					goto IL_830;
				}
				case OrderSubType.FollowMe:
					this.OrderController.SetOrderWithAgent(OrderType.FollowMe, Agent.Main);
					goto IL_830;
				case OrderSubType.Advance:
				{
					MBReadOnlyList<Formation> focusedFormationsCache2 = this._focusedFormationsCache;
					if (focusedFormationsCache2 != null && focusedFormationsCache2.Count > 0)
					{
						this.OrderController.SetOrderWithFormation(OrderType.Advance, this._focusedFormationsCache[0]);
						goto IL_830;
					}
					this.OrderController.SetOrder(OrderType.Advance);
					goto IL_830;
				}
				case OrderSubType.Fallback:
					this.OrderController.SetOrder(OrderType.FallBack);
					goto IL_830;
				case OrderSubType.Stop:
					this.OrderController.SetOrder(OrderType.StandYourGround);
					goto IL_830;
				case OrderSubType.Retreat:
					this.OrderController.SetOrder(OrderType.Retreat);
					goto IL_830;
				case OrderSubType.FormLine:
					this.OrderController.SetOrder(OrderType.ArrangementLine);
					goto IL_830;
				case OrderSubType.FormClose:
					this.OrderController.SetOrder(OrderType.ArrangementCloseOrder);
					goto IL_830;
				case OrderSubType.FormLoose:
					this.OrderController.SetOrder(OrderType.ArrangementLoose);
					goto IL_830;
				case OrderSubType.FormCircular:
					this.OrderController.SetOrder(OrderType.ArrangementCircular);
					goto IL_830;
				case OrderSubType.FormSchiltron:
					this.OrderController.SetOrder(OrderType.ArrangementSchiltron);
					goto IL_830;
				case OrderSubType.FormV:
					this.OrderController.SetOrder(OrderType.ArrangementVee);
					goto IL_830;
				case OrderSubType.FormColumn:
					this.OrderController.SetOrder(OrderType.ArrangementColumn);
					goto IL_830;
				case OrderSubType.FormScatter:
					this.OrderController.SetOrder(OrderType.ArrangementScatter);
					goto IL_830;
				case OrderSubType.ToggleStart:
				case OrderSubType.ToggleEnd:
					goto IL_830;
				case OrderSubType.ToggleFacing:
					if (OrderController.GetActiveFacingOrderOf(this.OrderController.SelectedFormations.FirstOrDefault<Formation>()) == OrderType.LookAtDirection)
					{
						this.OrderController.SetOrder(OrderType.LookAtEnemy);
						goto IL_830;
					}
					this.OrderController.SetOrderWithPosition(OrderType.LookAtDirection, new WorldPosition(this.Mission.Scene, UIntPtr.Zero, this._getOrderFlagPosition(), false));
					goto IL_830;
				case OrderSubType.ToggleFire:
					if (this.LastSelectedOrderItem.SelectionState == 3)
					{
						this.OrderController.SetOrder(OrderType.FireAtWill);
						goto IL_830;
					}
					this.OrderController.SetOrder(OrderType.HoldFire);
					goto IL_830;
				case OrderSubType.ToggleMount:
					if (this.LastSelectedOrderItem.SelectionState == 3)
					{
						this.OrderController.SetOrder(OrderType.Mount);
						goto IL_830;
					}
					this.OrderController.SetOrder(OrderType.Dismount);
					goto IL_830;
				case OrderSubType.ToggleAI:
					if (this.LastSelectedOrderItem.SelectionState == 3)
					{
						this.OrderController.SetOrder(OrderType.AIControlOff);
						goto IL_830;
					}
					this.OrderController.SetOrder(OrderType.AIControlOn);
					using (List<Formation>.Enumerator enumerator4 = this.OrderController.SelectedFormations.GetEnumerator())
					{
						while (enumerator4.MoveNext())
						{
							Formation formation = enumerator4.Current;
							this.TeamOnFormationAIActiveBehaviorChanged(formation);
						}
						goto IL_830;
					}
					break;
				case OrderSubType.ToggleTransfer:
					break;
				case OrderSubType.ActivationFaceDirection:
					this.OrderController.SetOrderWithPosition(OrderType.LookAtDirection, new WorldPosition(this.Mission.Scene, UIntPtr.Zero, this._getOrderFlagPosition(), false));
					goto IL_830;
				case OrderSubType.FaceEnemy:
					this.OrderController.SetOrder(OrderType.LookAtEnemy);
					goto IL_830;
				default:
					goto IL_830;
				}
				if (!this.IsDeployment)
				{
					foreach (OrderTroopItemVM orderTroopItemVM2 in this.TroopController.TransferTargetList)
					{
						orderTroopItemVM2.IsSelected = false;
						orderTroopItemVM2.IsSelectable = !this.OrderController.IsFormationListening(orderTroopItemVM2.Formation);
					}
					OrderTroopItemVM orderTroopItemVM3 = this.TroopController.TransferTargetList.FirstOrDefault((OrderTroopItemVM t) => t.IsSelectable);
					if (orderTroopItemVM3 != null)
					{
						orderTroopItemVM3.IsSelected = true;
						this.TroopController.IsTransferValid = true;
						GameTexts.SetVariable("{FORMATION_INDEX}", Common.ToRoman(orderTroopItemVM3.Formation.Index + 1));
						this.TroopController.TransferTitleText = new TextObject("{=DvnRkWQg}Transfer Troops To {FORMATION_INDEX}", null).ToString();
						this.TroopController.IsTransferActive = true;
						this.TroopController.IsTransferValid = false;
						this.TroopController.TransferMaxValue = (from t in this.TroopController.TroopList
						where t.IsSelected
						select t).Sum((OrderTroopItemVM t) => t.CurrentMemberCount);
						this.TroopController.TransferValue = this.TroopController.TransferMaxValue;
						this.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
					}
					else
					{
						MBInformationManager.AddQuickInformation(new TextObject("{=SLY8z9fP}All formations are selected!", null), 0, null, "");
					}
				}
			}
			else
			{
				OrderSubType orderSubType = this.LastSelectedOrderItem.OrderSubType;
				if (orderSubType <= OrderSubType.Stop)
				{
					if (orderSubType != OrderSubType.MoveToPosition)
					{
						if (orderSubType == OrderSubType.Stop)
						{
							this.OrderController.SiegeWeaponController.SetOrder(SiegeWeaponOrderType.Stop);
						}
					}
					else
					{
						this.OrderController.SiegeWeaponController.SetOrder(SiegeWeaponOrderType.Attack);
					}
				}
				else if (orderSubType != OrderSubType.ToggleFacing)
				{
					if (orderSubType == OrderSubType.Return)
					{
						return;
					}
				}
				else
				{
					this.OrderController.SiegeWeaponController.SetOrder(SiegeWeaponOrderType.FireAtWalls);
				}
			}
			IL_830:
			if (this.ActiveTargetState == 0)
			{
				IEnumerable<OrderTroopItemVM> enumerable = from item in this.TroopController.TroopList
				where item.IsSelected
				select item;
				enumerable.Count<OrderTroopItemVM>();
				using (IEnumerator<OrderTroopItemVM> enumerator2 = enumerable.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						OrderTroopItemVM orderTroopItemVM4 = enumerator2.Current;
						this.TroopController.SetTroopActiveOrders(orderTroopItemVM4);
						orderTroopItemVM4.IsTargetRelevant = orderTroopItemVM4.IsSelected;
					}
					goto IL_912;
				}
			}
			foreach (OrderSiegeMachineVM siegeMachineActiveOrders in from item in this.DeploymentController.SiegeMachineList
			where item.IsSelected
			select item)
			{
				this.DeploymentController.SetSiegeMachineActiveOrders(siegeMachineActiveOrders);
			}
			IL_912:
			this.UpdateTitleOrdersKeyVisualVisibility();
			this.LastSelectedOrderItem = null;
			this.LastSelectedOrderSetType = OrderSetType.None;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000BC08 File Offset: 0x00009E08
		public void AfterInitialize()
		{
			this.TroopController.UpdateTroops();
			if (!this.IsDeployment)
			{
				this.TroopController.SelectAllFormations(false);
			}
			this.DeploymentController.SetCurrentActiveOrders();
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000BC34 File Offset: 0x00009E34
		public void Update()
		{
			if (this.IsToggleOrderShown)
			{
				if (!this.CheckCanBeOpened(false))
				{
					if (this.IsToggleOrderShown)
					{
						this.TryCloseToggleOrder(false);
					}
				}
				else if (this._updateTroopsTimer.Check(MBCommon.GetApplicationTime()))
				{
					this.TroopController.IntervalUpdate();
				}
				this.TroopController.RefreshTroopFormationTargetVisuals();
			}
			this.DeploymentController.Update();
			this.DisplayFormationAIFeedback();
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000BCA0 File Offset: 0x00009EA0
		public void OnEscape()
		{
			if (this.IsToggleOrderShown)
			{
				if (this._currentActivationType == MissionOrderVM.ActivationType.Hold)
				{
					if (this.LastSelectedOrderItem != null)
					{
						this.UpdateTitleOrdersKeyVisualVisibility();
						this.OrderSetsWithOrdersByType[this.LastSelectedOrderSetType].ShowOrders = false;
						this.LastSelectedOrderItem = null;
						return;
					}
				}
				else if (this._currentActivationType == MissionOrderVM.ActivationType.Click)
				{
					this.LastSelectedOrderItem = null;
					if (this.LastSelectedOrderSetType != OrderSetType.None)
					{
						this.UpdateTitleOrdersKeyVisualVisibility();
						this.OrderSetsWithOrdersByType[this.LastSelectedOrderSetType].ShowOrders = false;
						this.LastSelectedOrderSetType = OrderSetType.None;
						return;
					}
					this.LastSelectedOrderSetType = OrderSetType.None;
					this.TryCloseToggleOrder(false);
				}
			}
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000BD3B File Offset: 0x00009F3B
		public void ViewOrders()
		{
			this._isPressedViewOrders = true;
			if (!this.IsToggleOrderShown)
			{
				this.TroopController.UpdateTroops();
				this.OpenToggleOrder(false, true);
			}
			else
			{
				this.TryCloseToggleOrder(false);
			}
			this._isPressedViewOrders = false;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000BD70 File Offset: 0x00009F70
		public void OnSelect(int formationTroopIndex)
		{
			if (!this.CheckCanBeOpened(true))
			{
				return;
			}
			if (this.ActiveTargetState == 0)
			{
				this.TroopController.OnSelectFormationWithIndex(formationTroopIndex);
			}
			else if (this.ActiveTargetState == 1)
			{
				this.DeploymentController.OnSelectFormationWithIndex(formationTroopIndex);
			}
			this.OpenToggleOrder(false, true);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000BDB0 File Offset: 0x00009FB0
		public void OnGiveOrder(int pressedIndex)
		{
			if (!this.CheckCanBeOpened(true))
			{
				return;
			}
			OrderSetVM orderSetVM = this.OrderSetsWithOrdersByType.Values.FirstOrDefault((OrderSetVM o) => o.ShowOrders);
			if (orderSetVM != null)
			{
				if (orderSetVM.Orders.Count > pressedIndex)
				{
					orderSetVM.Orders[pressedIndex].ExecuteAction();
					return;
				}
				if (pressedIndex == 8)
				{
					orderSetVM.Orders[orderSetVM.Orders.Count - 1].ExecuteAction();
					return;
				}
			}
			else
			{
				int num = (int)MathF.Clamp((float)pressedIndex, 0f, (float)(this.OrderSets.Count - 1));
				if (num >= 0 && this.OrderSets.Count > num && this.OrderSets[num].TitleOrder.SelectionState != 0)
				{
					this.OpenToggleOrder(false, true);
					this.OrderSets[num].TitleOrder.ExecuteAction();
				}
			}
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000BEA3 File Offset: 0x0000A0A3
		private void MissionOnMainAgentChanged(object sender, PropertyChangedEventArgs e)
		{
			if (this.Mission.MainAgent == null)
			{
				this.TryCloseToggleOrder(false);
				this.Mission.IsOrderMenuOpen = false;
			}
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000BEC8 File Offset: 0x0000A0C8
		internal void OnDeployAll()
		{
			this.TroopController.UpdateTroops();
			foreach (OrderTroopItemVM troopActiveOrders in this.TroopController.TroopList)
			{
				this.TroopController.SetTroopActiveOrders(troopActiveOrders);
			}
			if (!this.IsDeployment)
			{
				this.TroopController.SelectAllFormations(false);
			}
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000BF40 File Offset: 0x0000A140
		private void OnOrderShownToggle()
		{
			this.IsTroopListShown = (this.IsToggleOrderShown && !this.IsDeployment);
			if (!this._isDeployment)
			{
				if (this.IsToggleOrderShown)
				{
					this._onActivateToggleOrder();
				}
				else
				{
					this._onDeactivateToggleOrder();
				}
			}
			this._updateTroopsTimer = (this.IsToggleOrderShown ? new Timer(MBCommon.GetApplicationTime() - 2f, 2f, true) : null);
			this.IsTroopPlacingActive = (this.IsToggleOrderShown && this.ActiveTargetState == 0);
			foreach (OrderSetVM orderSetVM in this.OrderSetsWithOrdersByType.Values)
			{
				orderSetVM.ShowOrders = false;
				orderSetVM.TitleOrder.IsActive = (orderSetVM.TitleOrder.SelectionState != 0);
			}
			if (!this.IsDeployment && this.TroopController.TroopList.FirstOrDefault<OrderTroopItemVM>() != null)
			{
				this.TroopController.TroopList.ApplyActionOnAllItems(delegate(OrderTroopItemVM t)
				{
					t.IsSelectionActive = false;
				});
				this.TroopController.TroopList[0].IsSelectionActive = true;
			}
			this._onRefreshVisuals();
			if (!this.IsToggleOrderShown)
			{
				this._currentActivationType = MissionOrderVM.ActivationType.NotActive;
			}
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000C0B0 File Offset: 0x0000A2B0
		public void SelectNextTroop(int direction)
		{
			if (!this.CheckCanBeOpened(true))
			{
				return;
			}
			if (this.TroopController.TroopList.Count > 0)
			{
				OrderTroopItemVM orderTroopItemVM = this.TroopController.TroopList.FirstOrDefault((OrderTroopItemVM t) => t.IsSelectionActive);
				if (orderTroopItemVM != null)
				{
					int num = (direction > 0) ? 1 : -1;
					orderTroopItemVM.IsSelectionActive = false;
					int num2 = this.TroopController.TroopList.IndexOf(orderTroopItemVM) + num;
					for (int i = 0; i < this.TroopController.TroopList.Count; i++)
					{
						int num3 = (num2 + i * num) % this.TroopController.TroopList.Count;
						if (num3 < 0)
						{
							num3 += this.TroopController.TroopList.Count;
						}
						if (this.TroopController.TroopList[num3].IsSelectable)
						{
							this.TroopController.TroopList[num3].IsSelectionActive = true;
							return;
						}
					}
					return;
				}
				this.TroopController.TroopList.FirstOrDefault<OrderTroopItemVM>().IsSelectionActive = true;
			}
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000C1D0 File Offset: 0x0000A3D0
		public void ToggleSelectionForCurrentTroop()
		{
			if (!this.CheckCanBeOpened(true))
			{
				return;
			}
			OrderTroopItemVM orderTroopItemVM = this.TroopController.TroopList.FirstOrDefault((OrderTroopItemVM t) => t.IsSelectionActive);
			if (orderTroopItemVM != null)
			{
				if (orderTroopItemVM.IsSelected)
				{
					this.TroopController.OnDeselectFormation(orderTroopItemVM);
					return;
				}
				this.TroopController.OnSelectFormation(orderTroopItemVM);
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000C23B File Offset: 0x0000A43B
		private void OnTransferFinished()
		{
			this._onTransferTroopsFinishedDelegate.DynamicInvokeWithLog(Array.Empty<object>());
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000C24E File Offset: 0x0000A44E
		internal OrderSetType GetOrderSetWithShortcutIndex(int index)
		{
			switch (index)
			{
			case 0:
				return OrderSetType.Movement;
			case 1:
				return OrderSetType.Form;
			case 2:
				return OrderSetType.Toggle;
			case 3:
				return OrderSetType.Facing;
			default:
				return (OrderSetType)index;
			}
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000C274 File Offset: 0x0000A474
		internal IEnumerable<OrderItemVM> GetAllOrderItemsForSubType(OrderSubType orderSubType)
		{
			Func<OrderItemVM, bool> <>9__4;
			IEnumerable<OrderItemVM> first = (from s in this.OrderSets
			select s.Orders).SelectMany(delegate(MBBindingList<OrderItemVM> o)
			{
				Func<OrderItemVM, bool> predicate;
				if ((predicate = <>9__4) == null)
				{
					predicate = (<>9__4 = ((OrderItemVM l) => l.OrderSubType == orderSubType));
				}
				return o.Where(predicate);
			});
			IEnumerable<OrderItemVM> second = from s in this.OrderSets
			where s.TitleOrder.OrderSubType == orderSubType
			select s into t
			select t.TitleOrder;
			return first.Union(second);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000C30C File Offset: 0x0000A50C
		[Conditional("DEBUG")]
		private void DebugTick()
		{
			if (this.IsToggleOrderShown)
			{
				string str = "SelectedFormations (" + this.OrderController.SelectedFormations.Count + ") :";
				foreach (Formation formation in this.OrderController.SelectedFormations)
				{
					str = str + " " + formation.FormationIndex.GetName();
				}
			}
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000C3A4 File Offset: 0x0000A5A4
		public void OnDeploymentFinished()
		{
			this.TroopController.OnDeploymentFinished();
			this.DeploymentController.FinalizeDeployment();
			this.IsDeployment = false;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000C3C3 File Offset: 0x0000A5C3
		public void OnFiltersSet(List<ValueTuple<int, List<int>>> filterData)
		{
			this._filterData = filterData;
			this.TroopController.OnFiltersSet(filterData);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000C3D8 File Offset: 0x0000A5D8
		public void UpdateCanUseShortcuts(bool value)
		{
			this.CanUseShortcuts = value;
			for (int i = 0; i < this.OrderSets.Count; i++)
			{
				this.OrderSets[i].UpdateCanUseShortcuts(value);
			}
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000C414 File Offset: 0x0000A614
		public void SetCancelInputKey(HotKey hotKey)
		{
			this.CancelInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600027E RID: 638 RVA: 0x0000C423 File Offset: 0x0000A623
		// (set) Token: 0x0600027F RID: 639 RVA: 0x0000C42B File Offset: 0x0000A62B
		[DataSourceProperty]
		public InputKeyItemVM CancelInputKey
		{
			get
			{
				return this._cancelInputKey;
			}
			set
			{
				if (value != this._cancelInputKey)
				{
					this._cancelInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "CancelInputKey");
				}
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0000C449 File Offset: 0x0000A649
		// (set) Token: 0x06000281 RID: 641 RVA: 0x0000C451 File Offset: 0x0000A651
		[DataSourceProperty]
		public MBBindingList<OrderSetVM> OrderSets
		{
			get
			{
				return this._orderSets;
			}
			set
			{
				if (value == this._orderSets)
				{
					return;
				}
				this._orderSets = value;
				base.OnPropertyChangedWithValue<MBBindingList<OrderSetVM>>(value, "OrderSets");
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0000C470 File Offset: 0x0000A670
		// (set) Token: 0x06000283 RID: 643 RVA: 0x0000C478 File Offset: 0x0000A678
		[DataSourceProperty]
		public MissionOrderTroopControllerVM TroopController
		{
			get
			{
				return this._troopController;
			}
			set
			{
				if (value == this._troopController)
				{
					return;
				}
				this._troopController = value;
				base.OnPropertyChangedWithValue<MissionOrderTroopControllerVM>(value, "TroopController");
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0000C497 File Offset: 0x0000A697
		// (set) Token: 0x06000285 RID: 645 RVA: 0x0000C49F File Offset: 0x0000A69F
		[DataSourceProperty]
		public MissionOrderDeploymentControllerVM DeploymentController
		{
			get
			{
				return this._deploymentController;
			}
			set
			{
				if (value == this._deploymentController)
				{
					return;
				}
				this._deploymentController = value;
				base.OnPropertyChangedWithValue<MissionOrderDeploymentControllerVM>(value, "DeploymentController");
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000286 RID: 646 RVA: 0x0000C4BE File Offset: 0x0000A6BE
		// (set) Token: 0x06000287 RID: 647 RVA: 0x0000C4C8 File Offset: 0x0000A6C8
		[DataSourceProperty]
		public int ActiveTargetState
		{
			get
			{
				return this._activeTargetState;
			}
			set
			{
				if (value == this._activeTargetState)
				{
					return;
				}
				this._activeTargetState = value;
				base.OnPropertyChangedWithValue(value, "ActiveTargetState");
				this.IsTroopPlacingActive = (value == 0);
				foreach (OrderSetVM orderSetVM in this.OrderSetsWithOrdersByType.Values)
				{
					orderSetVM.ShowOrders = false;
					orderSetVM.TitleOrder.IsActive = (orderSetVM.TitleOrder.SelectionState != 0);
				}
				this._onRefreshVisuals();
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000288 RID: 648 RVA: 0x0000C56C File Offset: 0x0000A76C
		// (set) Token: 0x06000289 RID: 649 RVA: 0x0000C574 File Offset: 0x0000A774
		[DataSourceProperty]
		public bool IsDeployment
		{
			get
			{
				return this._isDeployment;
			}
			set
			{
				this._isDeployment = value;
				base.OnPropertyChangedWithValue(value, "IsDeployment");
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600028A RID: 650 RVA: 0x0000C589 File Offset: 0x0000A789
		// (set) Token: 0x0600028B RID: 651 RVA: 0x0000C591 File Offset: 0x0000A791
		[DataSourceProperty]
		public bool IsToggleOrderShown
		{
			get
			{
				return this._isToggleOrderShown;
			}
			set
			{
				if (value == this._isToggleOrderShown)
				{
					return;
				}
				this._isToggleOrderShown = value;
				this.OnOrderShownToggle();
				base.OnPropertyChangedWithValue(value, "IsToggleOrderShown");
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600028C RID: 652 RVA: 0x0000C5B6 File Offset: 0x0000A7B6
		// (set) Token: 0x0600028D RID: 653 RVA: 0x0000C5BE File Offset: 0x0000A7BE
		[DataSourceProperty]
		public bool IsTroopListShown
		{
			get
			{
				return this._isTroopListShown;
			}
			set
			{
				if (value == this._isTroopListShown)
				{
					return;
				}
				this._isTroopListShown = value;
				base.OnPropertyChangedWithValue(value, "IsTroopListShown");
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600028E RID: 654 RVA: 0x0000C5DD File Offset: 0x0000A7DD
		// (set) Token: 0x0600028F RID: 655 RVA: 0x0000C5E5 File Offset: 0x0000A7E5
		[DataSourceProperty]
		public bool CanUseShortcuts
		{
			get
			{
				return this._canUseShortcuts;
			}
			set
			{
				if (value != this._canUseShortcuts)
				{
					this._canUseShortcuts = value;
					base.OnPropertyChangedWithValue(value, "CanUseShortcuts");
				}
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000290 RID: 656 RVA: 0x0000C603 File Offset: 0x0000A803
		// (set) Token: 0x06000291 RID: 657 RVA: 0x0000C60B File Offset: 0x0000A80B
		[DataSourceProperty]
		public bool IsHolding
		{
			get
			{
				return this._isHolding;
			}
			set
			{
				if (value != this._isHolding)
				{
					this._isHolding = value;
					base.OnPropertyChangedWithValue(value, "IsHolding");
				}
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000292 RID: 658 RVA: 0x0000C629 File Offset: 0x0000A829
		// (set) Token: 0x06000293 RID: 659 RVA: 0x0000C631 File Offset: 0x0000A831
		[DataSourceProperty]
		public bool IsAnyOrderSetActive
		{
			get
			{
				return this._isAnyOrderSetActive;
			}
			set
			{
				if (value != this._isAnyOrderSetActive)
				{
					this._isAnyOrderSetActive = value;
					base.OnPropertyChangedWithValue(value, "IsAnyOrderSetActive");
				}
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000294 RID: 660 RVA: 0x0000C64F File Offset: 0x0000A84F
		// (set) Token: 0x06000295 RID: 661 RVA: 0x0000C657 File Offset: 0x0000A857
		[DataSourceProperty]
		public string ReturnText
		{
			get
			{
				return this._returnText;
			}
			set
			{
				if (value != this._returnText)
				{
					this._returnText = value;
					base.OnPropertyChangedWithValue<string>(value, "ReturnText");
				}
			}
		}

		// Token: 0x040000FA RID: 250
		public InputRestrictions InputRestrictions;

		// Token: 0x040000FB RID: 251
		private MissionOrderVM.ActivationType _currentActivationType;

		// Token: 0x040000FC RID: 252
		private Timer _updateTroopsTimer;

		// Token: 0x040000FD RID: 253
		internal readonly Dictionary<OrderSetType, OrderSetVM> OrderSetsWithOrdersByType;

		// Token: 0x040000FE RID: 254
		private readonly Camera _deploymentCamera;

		// Token: 0x040000FF RID: 255
		private bool _isTroopPlacingActive;

		// Token: 0x04000100 RID: 256
		private OrderSetType _lastSelectedOrderSetType;

		// Token: 0x04000101 RID: 257
		private bool _isPressedViewOrders;

		// Token: 0x04000102 RID: 258
		private readonly OnToggleActivateOrderStateDelegate _onActivateToggleOrder;

		// Token: 0x04000103 RID: 259
		private readonly OnToggleActivateOrderStateDelegate _onDeactivateToggleOrder;

		// Token: 0x04000104 RID: 260
		private readonly GetOrderFlagPositionDelegate _getOrderFlagPosition;

		// Token: 0x04000105 RID: 261
		private readonly ToggleOrderPositionVisibilityDelegate _toggleOrderPositionVisibility;

		// Token: 0x04000106 RID: 262
		private readonly OnRefreshVisualsDelegate _onRefreshVisuals;

		// Token: 0x04000107 RID: 263
		private readonly OnToggleActivateOrderStateDelegate _onTransferTroopsFinishedDelegate;

		// Token: 0x04000108 RID: 264
		private readonly OnBeforeOrderDelegate _onBeforeOrderDelegate;

		// Token: 0x04000109 RID: 265
		private readonly Action<bool> _toggleMissionInputs;

		// Token: 0x0400010A RID: 266
		private readonly List<DeploymentPoint> _deploymentPoints;

		// Token: 0x0400010B RID: 267
		private readonly bool _isMultiplayer;

		// Token: 0x0400010C RID: 268
		private OrderSetVM _movementSet;

		// Token: 0x0400010D RID: 269
		private MBReadOnlyList<Formation> _focusedFormationsCache;

		// Token: 0x0400010E RID: 270
		private OrderSetVM _facingSet;

		// Token: 0x0400010F RID: 271
		private int _delayValueForAIFormationModifications;

		// Token: 0x04000110 RID: 272
		private readonly List<Formation> _modifiedAIFormations = new List<Formation>();

		// Token: 0x04000111 RID: 273
		private List<ValueTuple<int, List<int>>> _filterData;

		// Token: 0x04000112 RID: 274
		private InputKeyItemVM _cancelInputKey;

		// Token: 0x04000113 RID: 275
		private MBBindingList<OrderSetVM> _orderSets;

		// Token: 0x04000114 RID: 276
		private MissionOrderTroopControllerVM _troopController;

		// Token: 0x04000115 RID: 277
		private MissionOrderDeploymentControllerVM _deploymentController;

		// Token: 0x04000116 RID: 278
		private bool _isDeployment;

		// Token: 0x04000117 RID: 279
		private int _activeTargetState;

		// Token: 0x04000118 RID: 280
		private bool _isToggleOrderShown;

		// Token: 0x04000119 RID: 281
		private bool _isTroopListShown;

		// Token: 0x0400011A RID: 282
		private bool _canUseShortcuts;

		// Token: 0x0400011B RID: 283
		private bool _isHolding;

		// Token: 0x0400011C RID: 284
		private bool _isAnyOrderSetActive;

		// Token: 0x0400011D RID: 285
		private string _returnText;

		// Token: 0x020000A7 RID: 167
		public enum CursorState
		{
			// Token: 0x04000530 RID: 1328
			Move,
			// Token: 0x04000531 RID: 1329
			Face,
			// Token: 0x04000532 RID: 1330
			Form
		}

		// Token: 0x020000A8 RID: 168
		public enum OrderTargets
		{
			// Token: 0x04000534 RID: 1332
			Troops,
			// Token: 0x04000535 RID: 1333
			SiegeMachines
		}

		// Token: 0x020000A9 RID: 169
		public enum ActivationType
		{
			// Token: 0x04000537 RID: 1335
			NotActive,
			// Token: 0x04000538 RID: 1336
			Hold,
			// Token: 0x04000539 RID: 1337
			Click
		}
	}
}
