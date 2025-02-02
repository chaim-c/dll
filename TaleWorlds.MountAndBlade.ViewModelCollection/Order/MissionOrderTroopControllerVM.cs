using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.ViewModelCollection.Input;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.Order
{
	// Token: 0x02000018 RID: 24
	public class MissionOrderTroopControllerVM : ViewModel
	{
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00008518 File Offset: 0x00006718
		private Mission Mission
		{
			get
			{
				return Mission.Current;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x0000851F File Offset: 0x0000671F
		private Team Team
		{
			get
			{
				return Mission.Current.PlayerTeam;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x0000852B File Offset: 0x0000672B
		public OrderController OrderController
		{
			get
			{
				return this.Team.PlayerOrderController;
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00008538 File Offset: 0x00006738
		public MissionOrderTroopControllerVM(MissionOrderVM missionOrder, bool isMultiplayer, bool isDeployment, Action onTransferFinised)
		{
			this._missionOrder = missionOrder;
			this._onTransferFinised = onTransferFinised;
			this._isMultiplayer = isMultiplayer;
			this._isDeployment = isDeployment;
			this.TroopList = new MBBindingList<OrderTroopItemVM>();
			this.TransferTargetList = new MBBindingList<OrderTroopItemVM>();
			this.TroopList.Clear();
			this.TransferTargetList.Clear();
			for (int i = 0; i < 8; i++)
			{
				OrderTroopItemVM orderTroopItemVM = new OrderTroopItemVM(this.Team.GetFormation((FormationClass)i), new Action<OrderTroopItemVM>(this.ExecuteSelectTransferTroop), new Func<Formation, int>(this.GetFormationMorale));
				this.TransferTargetList.Add(orderTroopItemVM);
				orderTroopItemVM.IsSelected = false;
			}
			this.Team.OnOrderIssued += new OnOrderIssuedDelegate(this.OrderController_OnTroopOrderIssued);
			if (this.TroopList.Count > 0)
			{
				this.OnSelectFormation(this.TroopList[0]);
			}
			this._formationIndexComparer = new MissionOrderTroopControllerVM.TroopItemFormationIndexComparer();
			this.SortFormations();
			this.RefreshValues();
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000862C File Offset: 0x0000682C
		public override void RefreshValues()
		{
			base.RefreshValues();
			this._troopList.ApplyActionOnAllItems(delegate(OrderTroopItemVM x)
			{
				x.RefreshValues();
			});
			this.AcceptText = GameTexts.FindText("str_selection_widget_accept", null).ToString();
			this.CancelText = GameTexts.FindText("str_selection_widget_cancel", null).ToString();
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00008695 File Offset: 0x00006895
		public void ExecuteSelectAll()
		{
			this.SelectAllFormations(true);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x000086A0 File Offset: 0x000068A0
		public void ExecuteSelectTransferTroop(OrderTroopItemVM targetTroop)
		{
			foreach (OrderTroopItemVM orderTroopItemVM in this.TransferTargetList)
			{
				orderTroopItemVM.IsSelected = false;
			}
			targetTroop.IsSelected = targetTroop.IsSelectable;
			this.IsTransferValid = targetTroop.IsSelectable;
			GameTexts.SetVariable("FORMATION_INDEX", Common.ToRoman(targetTroop.Formation.Index + 1));
			this.TransferTitleText = new TextObject("{=DvnRkWQg}Transfer Troops To {FORMATION_INDEX}", null).ToString();
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00008738 File Offset: 0x00006938
		public void ExecuteConfirmTransfer()
		{
			this.IsTransferActive = false;
			OrderTroopItemVM orderTroopItemVM = this.TransferTargetList.Single((OrderTroopItemVM t) => t.IsSelected);
			int num = this.TransferValue;
			int b = (from t in this.TroopList
			where t.IsSelected
			select t).Sum((OrderTroopItemVM t) => t.CurrentMemberCount);
			num = MathF.Min(num, b);
			this.OrderController.SetOrderWithFormationAndNumber(OrderType.Transfer, orderTroopItemVM.Formation, num);
			for (int i = 0; i < this.TroopList.Count; i++)
			{
				OrderTroopItemVM orderTroopItemVM2 = this.TroopList[i];
				if (!orderTroopItemVM2.ContainsDeadTroop && orderTroopItemVM2.CurrentMemberCount == 0)
				{
					this.TroopList.RemoveAt(i);
					i--;
				}
			}
			Action onTransferFinised = this._onTransferFinised;
			if (onTransferFinised == null)
			{
				return;
			}
			onTransferFinised.DynamicInvokeWithLog(Array.Empty<object>());
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00008845 File Offset: 0x00006A45
		public void ExecuteCancelTransfer()
		{
			this.IsTransferActive = false;
			Action onTransferFinised = this._onTransferFinised;
			if (onTransferFinised == null)
			{
				return;
			}
			onTransferFinised.DynamicInvokeWithLog(Array.Empty<object>());
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00008864 File Offset: 0x00006A64
		public void ExecuteReset()
		{
			this.RefreshValues();
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000886C File Offset: 0x00006A6C
		internal void SetTroopActiveOrders(OrderTroopItemVM item)
		{
			bool flag = BannerlordConfig.OrderLayoutType == 1;
			item.ActiveOrders.Clear();
			List<OrderSubType> list = new List<OrderSubType>();
			OrderType orderType = OrderUIHelper.GetOrderOverrideForUI(item.Formation, OrderSetType.Movement);
			if (orderType == OrderType.None)
			{
				orderType = OrderController.GetActiveMovementOrderOf(item.Formation);
			}
			switch (orderType)
			{
			case OrderType.Move:
				list.Add(OrderSubType.MoveToPosition);
				goto IL_D9;
			case OrderType.Charge:
				list.Add(OrderSubType.Charge);
				goto IL_D9;
			case OrderType.ChargeWithTarget:
				list.Add(OrderSubType.Charge);
				goto IL_D9;
			case OrderType.StandYourGround:
				list.Add(OrderSubType.Stop);
				goto IL_D9;
			case OrderType.FollowMe:
				list.Add(OrderSubType.FollowMe);
				goto IL_D9;
			case OrderType.Retreat:
				list.Add(OrderSubType.Retreat);
				goto IL_D9;
			case OrderType.Advance:
				list.Add(OrderSubType.Advance);
				goto IL_D9;
			case OrderType.FallBack:
				list.Add(OrderSubType.Fallback);
				goto IL_D9;
			}
			Debug.FailedAssert("false", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.ViewModelCollection\\Order\\MissionOrderTroopControllerVM.cs", "SetTroopActiveOrders", 175);
			IL_D9:
			OrderType orderType2 = OrderUIHelper.GetOrderOverrideForUI(item.Formation, OrderSetType.Form);
			if (orderType2 == OrderType.None)
			{
				orderType2 = OrderController.GetActiveArrangementOrderOf(item.Formation);
			}
			switch (orderType2)
			{
			case OrderType.ArrangementLine:
				list.Add(OrderSubType.FormLine);
				break;
			case OrderType.ArrangementCloseOrder:
				list.Add(OrderSubType.FormClose);
				break;
			case OrderType.ArrangementLoose:
				list.Add(OrderSubType.FormLoose);
				break;
			case OrderType.ArrangementCircular:
				list.Add(OrderSubType.FormCircular);
				break;
			case OrderType.ArrangementSchiltron:
				list.Add(OrderSubType.FormSchiltron);
				break;
			case OrderType.ArrangementVee:
				list.Add(OrderSubType.FormV);
				break;
			case OrderType.ArrangementColumn:
				list.Add(OrderSubType.FormColumn);
				break;
			case OrderType.ArrangementScatter:
				list.Add(OrderSubType.FormScatter);
				break;
			default:
				Debug.FailedAssert("false", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.ViewModelCollection\\Order\\MissionOrderTroopControllerVM.cs", "SetTroopActiveOrders", 220);
				break;
			}
			OrderType orderType3 = OrderController.GetActiveRidingOrderOf(item.Formation);
			if (orderType3 != OrderType.Mount)
			{
				if (orderType3 == OrderType.Dismount)
				{
					list.Add(OrderSubType.ToggleMount);
				}
				else
				{
					Debug.FailedAssert("false", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.ViewModelCollection\\Order\\MissionOrderTroopControllerVM.cs", "SetTroopActiveOrders", 234);
				}
			}
			orderType3 = OrderController.GetActiveFiringOrderOf(item.Formation);
			if (orderType3 != OrderType.HoldFire)
			{
				if (orderType3 != OrderType.FireAtWill)
				{
					Debug.FailedAssert("false", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.ViewModelCollection\\Order\\MissionOrderTroopControllerVM.cs", "SetTroopActiveOrders", 248);
				}
			}
			else
			{
				list.Add(OrderSubType.ToggleFire);
			}
			if (!this._isMultiplayer)
			{
				orderType3 = OrderController.GetActiveAIControlOrderOf(item.Formation);
				if (orderType3 != OrderType.AIControlOn)
				{
					if (orderType3 != OrderType.AIControlOff)
					{
						Debug.FailedAssert("false", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.ViewModelCollection\\Order\\MissionOrderTroopControllerVM.cs", "SetTroopActiveOrders", 264);
					}
				}
				else
				{
					list.Add(OrderSubType.ToggleAI);
				}
			}
			orderType3 = OrderController.GetActiveFacingOrderOf(item.Formation);
			if (orderType3 != OrderType.LookAtEnemy)
			{
				if (orderType3 == OrderType.LookAtDirection)
				{
					list.Add(OrderSubType.ActivationFaceDirection);
				}
				else
				{
					Debug.FailedAssert("false", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.ViewModelCollection\\Order\\MissionOrderTroopControllerVM.cs", "SetTroopActiveOrders", 280);
				}
			}
			else
			{
				list.Add(flag ? OrderSubType.FaceEnemy : OrderSubType.ToggleFacing);
			}
			foreach (OrderSubType orderSubType in list)
			{
				item.ActiveOrders.AddRange(this._missionOrder.GetAllOrderItemsForSubType(orderSubType));
			}
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00008B64 File Offset: 0x00006D64
		internal void SelectAllFormations(bool uiFeedback = true)
		{
			foreach (OrderSetVM orderSetVM in this._missionOrder.OrderSetsWithOrdersByType.Values)
			{
				orderSetVM.ShowOrders = false;
			}
			if (this.TroopList.Any((OrderTroopItemVM t) => t.IsSelectable))
			{
				this.OrderController.SelectAllFormations(uiFeedback);
				if (uiFeedback && this.OrderController.SelectedFormations.Count > 0)
				{
					InformationManager.DisplayMessage(new InformationMessage(new TextObject("{=xTv4tCbZ}Everybody!! Listen to me", null).ToString()));
				}
			}
			foreach (OrderTroopItemVM orderTroopItemVM in this.TroopList)
			{
				orderTroopItemVM.IsSelected = orderTroopItemVM.IsSelectable;
			}
			this._missionOrder.SetActiveOrders();
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00008C74 File Offset: 0x00006E74
		internal void AddSelectedFormation(OrderTroopItemVM item)
		{
			if (!item.IsSelectable)
			{
				return;
			}
			Formation formation = this.Team.GetFormation(item.InitialFormationClass);
			this.OrderController.SelectFormation(formation);
			item.IsSelected = true;
			this._missionOrder.SetActiveOrders();
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00008CBC File Offset: 0x00006EBC
		internal void SetSelectedFormation(OrderTroopItemVM item)
		{
			this.UpdateTroops();
			if (!item.IsSelectable)
			{
				return;
			}
			this.OrderController.ClearSelectedFormations();
			foreach (OrderTroopItemVM orderTroopItemVM in this.TroopList)
			{
				orderTroopItemVM.IsSelected = false;
			}
			this.AddSelectedFormation(item);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00008D28 File Offset: 0x00006F28
		public void OnDeselectFormation(int index)
		{
			OrderTroopItemVM item = this.TroopList.FirstOrDefault((OrderTroopItemVM t) => t.Formation.Index == index);
			this.OnDeselectFormation(item);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00008D64 File Offset: 0x00006F64
		internal void OnDeselectFormation(OrderTroopItemVM item)
		{
			if (item != null)
			{
				Formation formation = this.Team.GetFormation(item.InitialFormationClass);
				if (this.OrderController.SelectedFormations.Contains(formation))
				{
					this.OrderController.DeselectFormation(formation);
				}
				item.IsSelected = false;
				if (this._isDeployment)
				{
					if (this.TroopList.Count((OrderTroopItemVM t) => t.IsSelected) != 0)
					{
						this._missionOrder.SetActiveOrders();
						return;
					}
					this._missionOrder.TryCloseToggleOrder(true);
					this._missionOrder.IsTroopPlacingActive = false;
					return;
				}
				else
				{
					this._missionOrder.SetActiveOrders();
				}
			}
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00008E14 File Offset: 0x00007014
		internal void OnSelectFormation(OrderTroopItemVM item)
		{
			foreach (OrderSetVM orderSetVM in this._missionOrder.OrderSetsWithOrdersByType.Values)
			{
				orderSetVM.ShowOrders = false;
			}
			this.UpdateTroops();
			this._missionOrder.IsTroopPlacingActive = true;
			if (Input.IsKeyDown(InputKey.LeftControl))
			{
				if (item.IsSelected)
				{
					this.OnDeselectFormation(item);
				}
				else
				{
					this.AddSelectedFormation(item);
				}
			}
			else
			{
				this.SetSelectedFormation(item);
			}
			if (this.IsTransferActive)
			{
				foreach (OrderTroopItemVM orderTroopItemVM in this.TransferTargetList)
				{
					orderTroopItemVM.IsSelectable = !this.OrderController.IsFormationListening(orderTroopItemVM.Formation);
				}
				this.IsTransferValid = this.TransferTargetList.Any((OrderTroopItemVM t) => t.IsSelected && t.IsSelectable);
				this.TransferMaxValue = (from t in this.TroopList
				where t.IsSelected
				select t).Sum((OrderTroopItemVM t) => t.CurrentMemberCount);
				this.TransferValue = this.TransferMaxValue;
			}
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00008F98 File Offset: 0x00007198
		internal void CheckSelectableFormations()
		{
			foreach (OrderTroopItemVM orderTroopItemVM in this.TroopList)
			{
				Formation formation = this.Team.GetFormation(orderTroopItemVM.InitialFormationClass);
				if (formation != null)
				{
					bool isSelectable = this.OrderController.IsFormationSelectable(formation);
					orderTroopItemVM.IsSelectable = isSelectable;
					if (!orderTroopItemVM.IsSelectable && orderTroopItemVM.IsSelected)
					{
						this.OnDeselectFormation(orderTroopItemVM);
					}
				}
			}
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00009020 File Offset: 0x00007220
		internal void UpdateTroops()
		{
			List<Formation> list;
			if (this.Mission.MainAgent != null && this.Mission.MainAgent.Controller != Agent.ControllerType.Player)
			{
				list = (from f in this.Team.FormationsIncludingEmpty
				where f.CountOfUnits > 0
				select f).ToList<Formation>();
			}
			else
			{
				list = (from f in this.Team.FormationsIncludingEmpty
				where f.CountOfUnits > 0 && (!f.IsPlayerTroopInFormation || f.CountOfUnits > 1)
				select f).ToList<Formation>();
			}
			foreach (OrderTroopItemVM orderTroopItemVM in this.TroopList)
			{
				this.SetTroopActiveOrders(orderTroopItemVM);
				orderTroopItemVM.IsSelectable = this.OrderController.IsFormationSelectable(orderTroopItemVM.Formation);
				if (orderTroopItemVM.IsSelectable && this.OrderController.IsFormationListening(orderTroopItemVM.Formation))
				{
					orderTroopItemVM.IsSelected = true;
				}
			}
			for (int i = 0; i < list.Count; i++)
			{
				Formation formation = list[i];
				if (formation != null && this.TroopList.All((OrderTroopItemVM item) => item.Formation != formation))
				{
					OrderTroopItemVM orderTroopItemVM2 = new OrderTroopItemVM(formation, new Action<OrderTroopItemVM>(this.OnSelectFormation), new Func<Formation, int>(this.GetFormationMorale));
					orderTroopItemVM2 = this.AddTroopItemIfNotExist(orderTroopItemVM2, -1);
					this.SetTroopActiveOrders(orderTroopItemVM2);
					orderTroopItemVM2.IsSelectable = this.OrderController.IsFormationSelectable(formation);
					if (orderTroopItemVM2.IsSelectable && this.OrderController.IsFormationListening(formation))
					{
						orderTroopItemVM2.IsSelected = true;
					}
					this.SortFormations();
				}
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00009204 File Offset: 0x00007404
		public void AddTroops(Agent agent)
		{
			if (agent.Team != this.Team || agent.Formation == null || agent.IsPlayerControlled)
			{
				return;
			}
			Formation formation = agent.Formation;
			OrderTroopItemVM orderTroopItemVM = this.TroopList.FirstOrDefault((OrderTroopItemVM item) => item.Formation.FormationIndex == formation.FormationIndex);
			if (orderTroopItemVM == null)
			{
				OrderTroopItemVM orderTroopItemVM2 = new OrderTroopItemVM(formation, new Action<OrderTroopItemVM>(this.OnSelectFormation), new Func<Formation, int>(this.GetFormationMorale));
				orderTroopItemVM2 = this.AddTroopItemIfNotExist(orderTroopItemVM2, -1);
				this.SetTroopActiveOrders(orderTroopItemVM2);
				orderTroopItemVM2.IsSelectable = this.OrderController.IsFormationSelectable(formation);
				if (orderTroopItemVM2.IsSelectable && this.OrderController.IsFormationListening(formation))
				{
					orderTroopItemVM2.IsSelected = true;
					return;
				}
			}
			else
			{
				orderTroopItemVM.SetFormationClassFromFormation(formation);
				bool isSelectable = this.OrderController.IsFormationSelectable(formation);
				orderTroopItemVM.IsSelectable = isSelectable;
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x000092F4 File Offset: 0x000074F4
		public void RemoveTroops(Agent agent)
		{
			if (agent.Team != this.Team || agent.Formation == null)
			{
				return;
			}
			Formation formation = agent.Formation;
			OrderTroopItemVM orderTroopItemVM = this.TroopList.FirstOrDefault((OrderTroopItemVM item) => item.Formation.FormationIndex == formation.FormationIndex);
			if (orderTroopItemVM != null)
			{
				orderTroopItemVM.OnFormationAgentRemoved(agent);
				orderTroopItemVM.SetFormationClassFromFormation(formation);
				orderTroopItemVM.IsSelectable = this.OrderController.IsFormationSelectable(formation);
				if (!orderTroopItemVM.IsSelectable && orderTroopItemVM.IsSelected)
				{
					this.OnDeselectFormation(orderTroopItemVM);
				}
			}
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00009388 File Offset: 0x00007588
		private void OrderController_OnTroopOrderIssued(OrderType orderType, IEnumerable<Formation> appliedFormations, OrderController orderController, params object[] delegateParams)
		{
			foreach (OrderSetVM orderSetVM in this._missionOrder.OrderSetsWithOrdersByType.Values)
			{
				orderSetVM.TitleOrder.IsActive = (orderSetVM.TitleOrder.SelectionState != 0);
			}
			this._missionOrder.OrderSetsWithOrdersByType[OrderSetType.Movement].ShowOrders = false;
			if (orderType == OrderType.Transfer)
			{
				if (!(delegateParams[1] is object[]))
				{
					int num = (int)delegateParams[1];
				}
				Formation formation = delegateParams[0] as Formation;
				OrderTroopItemVM orderTroopItemVM = this.TroopList.FirstOrDefault((OrderTroopItemVM item) => item.Formation == formation);
				if (orderTroopItemVM == null)
				{
					int index = -1;
					for (int i = 0; i < this.TroopList.Count; i++)
					{
						if (this.TroopList[i].Formation.Index > formation.Index)
						{
							index = i;
							break;
						}
					}
					OrderTroopItemVM orderTroopItemVM2 = new OrderTroopItemVM(formation, new Action<OrderTroopItemVM>(this.OnSelectFormation), new Func<Formation, int>(this.GetFormationMorale));
					orderTroopItemVM2 = this.AddTroopItemIfNotExist(orderTroopItemVM2, index);
					this.SetTroopActiveOrders(orderTroopItemVM2);
					orderTroopItemVM2.IsSelectable = this.OrderController.IsFormationSelectable(formation);
					if (orderTroopItemVM2.IsSelectable && this.OrderController.IsFormationListening(formation))
					{
						orderTroopItemVM2.IsSelected = true;
					}
					this.OnFiltersSet(this._filterData);
				}
				else
				{
					orderTroopItemVM.SetFormationClassFromFormation(formation);
				}
				using (IEnumerator<Formation> enumerator2 = appliedFormations.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						Formation sourceFormation = enumerator2.Current;
						OrderTroopItemVM orderTroopItemVM3 = this.TroopList.FirstOrDefault((OrderTroopItemVM item) => item.Formation == sourceFormation);
						if (orderTroopItemVM3 == null)
						{
							int index2 = -1;
							for (int j = 0; j < this.TroopList.Count; j++)
							{
								if (this.TroopList[j].Formation.Index > sourceFormation.Index)
								{
									index2 = j;
									break;
								}
							}
							OrderTroopItemVM orderTroopItemVM4 = new OrderTroopItemVM(sourceFormation, new Action<OrderTroopItemVM>(this.OnSelectFormation), new Func<Formation, int>(this.GetFormationMorale));
							orderTroopItemVM4 = this.AddTroopItemIfNotExist(orderTroopItemVM4, index2);
							this.SetTroopActiveOrders(orderTroopItemVM4);
							orderTroopItemVM4.IsSelectable = this.OrderController.IsFormationSelectable(sourceFormation);
							if (orderTroopItemVM4.IsSelectable && this.OrderController.IsFormationListening(sourceFormation))
							{
								orderTroopItemVM4.IsSelected = true;
							}
							this.OnFiltersSet(this._filterData);
						}
						else
						{
							orderTroopItemVM3.SetFormationClassFromFormation(sourceFormation);
						}
					}
				}
				int num2 = 1;
				using (IEnumerator<Formation> enumerator2 = appliedFormations.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						Formation sourceFormation = enumerator2.Current;
						this.TroopList.FirstOrDefault((OrderTroopItemVM item) => item.Formation.Index == sourceFormation.Index).SetFormationClassFromFormation(sourceFormation);
						num2++;
					}
				}
			}
			this.UpdateTroops();
			this.SortFormations();
			foreach (OrderTroopItemVM troopActiveOrders in from item in this.TroopList
			where item.IsSelected
			select item)
			{
				this.SetTroopActiveOrders(troopActiveOrders);
			}
			this._missionOrder.SetActiveOrders();
			this.CheckSelectableFormations();
		}

		// Token: 0x0600020A RID: 522 RVA: 0x000097A0 File Offset: 0x000079A0
		public override void OnFinalize()
		{
			base.OnFinalize();
			this.Team.OnOrderIssued -= new OnOrderIssuedDelegate(this.OrderController_OnTroopOrderIssued);
			foreach (OrderTroopItemVM orderTroopItemVM in this.TroopList)
			{
				orderTroopItemVM.OnFinalize();
			}
			this._transferTargetList = null;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00009810 File Offset: 0x00007A10
		internal void IntervalUpdate()
		{
			for (int i = this.TroopList.Count - 1; i >= 0; i--)
			{
				OrderTroopItemVM orderTroopItemVM = this.TroopList[i];
				Formation formation = this.Team.GetFormation(orderTroopItemVM.InitialFormationClass);
				if (formation != null && formation.CountOfUnits > 0)
				{
					orderTroopItemVM.UnderAttackOfType = (int)formation.GetUnderAttackTypeOfUnits(3f);
					orderTroopItemVM.BehaviorType = (int)formation.GetMovementTypeOfUnits();
					if (!this._isDeployment)
					{
						orderTroopItemVM.Morale = (int)MissionGameModels.Current.BattleMoraleModel.GetAverageMorale(formation);
						if (orderTroopItemVM.SetFormationClassFromFormation(formation))
						{
							this.UpdateTroops();
						}
						orderTroopItemVM.IsAmmoAvailable = (formation.QuerySystem.RangedUnitRatio > 0f || formation.QuerySystem.RangedCavalryUnitRatio > 0f);
						if (orderTroopItemVM.IsAmmoAvailable)
						{
							int totalCurrentAmmo = 0;
							int totalMaxAmmo = 0;
							orderTroopItemVM.Formation.ApplyActionOnEachUnit(delegate(Agent agent)
							{
								if (!agent.IsMainAgent)
								{
									int num;
									int num2;
									this.GetMaxAndCurrentAmmoOfAgent(agent, out num, out num2);
									totalCurrentAmmo += num;
									totalMaxAmmo += num2;
								}
							}, null);
							orderTroopItemVM.AmmoPercentage = (float)totalCurrentAmmo / (float)totalMaxAmmo;
						}
					}
				}
				else if (formation != null && formation.CountOfUnits == 0)
				{
					orderTroopItemVM.Morale = 0;
					orderTroopItemVM.SetFormationClassFromFormation(formation);
				}
			}
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00009954 File Offset: 0x00007B54
		internal void RefreshTroopFormationTargetVisuals()
		{
			for (int i = 0; i < this.TroopList.Count; i++)
			{
				this.TroopList[i].RefreshTargetedOrderVisual();
			}
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00009988 File Offset: 0x00007B88
		internal void OnSelectFormationWithIndex(int formationTroopIndex)
		{
			this.UpdateTroops();
			OrderTroopItemVM orderTroopItemVM = this.TroopList.SingleOrDefault((OrderTroopItemVM t) => t.Formation.Index == formationTroopIndex);
			if (orderTroopItemVM != null)
			{
				if (orderTroopItemVM.IsSelectable)
				{
					this.OnSelectFormation(orderTroopItemVM);
					return;
				}
			}
			else
			{
				this.SelectAllFormations(true);
			}
		}

		// Token: 0x0600020E RID: 526 RVA: 0x000099DC File Offset: 0x00007BDC
		internal void SetCurrentActiveOrders()
		{
			List<OrderSubjectVM> list = (from item in this.TroopList.Cast<OrderSubjectVM>().ToList<OrderSubjectVM>()
			where item.IsSelected && item.IsSelectable
			select item).ToList<OrderSubjectVM>();
			if (list.IsEmpty<OrderSubjectVM>())
			{
				this.OrderController.SelectAllFormations(false);
				foreach (OrderTroopItemVM orderTroopItemVM in from s in this.TroopList
				where this.OrderController.SelectedFormations.Contains(s.Formation)
				select s)
				{
					orderTroopItemVM.IsSelected = true;
					orderTroopItemVM.IsSelectable = true;
					this.SetTroopActiveOrders(orderTroopItemVM);
					list.Add(orderTroopItemVM);
				}
			}
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00009AA0 File Offset: 0x00007CA0
		private void GetMaxAndCurrentAmmoOfAgent(Agent agent, out int currentAmmo, out int maxAmmo)
		{
			currentAmmo = 0;
			maxAmmo = 0;
			for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.ExtraWeaponSlot; equipmentIndex++)
			{
				if (!agent.Equipment[equipmentIndex].IsEmpty && agent.Equipment[equipmentIndex].CurrentUsageItem.IsRangedWeapon)
				{
					currentAmmo = agent.Equipment.GetAmmoAmount(equipmentIndex);
					maxAmmo = agent.Equipment.GetMaxAmmo(equipmentIndex);
					return;
				}
			}
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00009B10 File Offset: 0x00007D10
		public void OnFiltersSet(List<ValueTuple<int, List<int>>> filterData)
		{
			if (filterData == null)
			{
				return;
			}
			this._filterData = filterData;
			using (List<ValueTuple<int, List<int>>>.Enumerator enumerator = filterData.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ValueTuple<int, List<int>> filter = enumerator.Current;
					OrderTroopItemVM orderTroopItemVM = this.TroopList.FirstOrDefault((OrderTroopItemVM f) => f.Formation.Index == filter.Item1);
					if (orderTroopItemVM != null)
					{
						orderTroopItemVM.UpdateFilterData(filter.Item2);
					}
					OrderTroopItemVM orderTroopItemVM2 = this.TransferTargetList.FirstOrDefault((OrderTroopItemVM f) => f.Formation.Index == filter.Item1);
					if (orderTroopItemVM2 != null)
					{
						orderTroopItemVM2.UpdateFilterData(filter.Item2);
					}
				}
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00009BC8 File Offset: 0x00007DC8
		public void OnDeploymentFinished()
		{
			this._isDeployment = false;
			this.SortFormations();
			for (int i = this.TroopList.Count - 1; i >= 0; i--)
			{
				if (this.TroopList[i].CurrentMemberCount <= 0)
				{
					this.TroopList.RemoveAt(i);
				}
			}
			this.SelectAllFormations(false);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00009C21 File Offset: 0x00007E21
		private void SortFormations()
		{
			this.TroopList.Sort(this._formationIndexComparer);
			this.TransferTargetList.Sort(this._formationIndexComparer);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00009C45 File Offset: 0x00007E45
		private int GetFormationMorale(Formation formation)
		{
			if (!this._isDeployment)
			{
				return (int)MissionGameModels.Current.BattleMoraleModel.GetAverageMorale(formation);
			}
			return 0;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00009C64 File Offset: 0x00007E64
		private OrderTroopItemVM AddTroopItemIfNotExist(OrderTroopItemVM troopItem, int index = -1)
		{
			OrderTroopItemVM orderTroopItemVM = null;
			if (troopItem != null)
			{
				bool flag = true;
				orderTroopItemVM = this.TroopList.FirstOrDefault((OrderTroopItemVM t) => t.Formation.Index == troopItem.Formation.Index);
				if (orderTroopItemVM == null)
				{
					flag = false;
					orderTroopItemVM = troopItem;
				}
				if (flag)
				{
					this.TroopList.Remove(orderTroopItemVM);
				}
				if (index == -1)
				{
					this.TroopList.Add(troopItem);
				}
				else
				{
					this.TroopList.Insert(index, troopItem);
				}
			}
			else
			{
				Debug.FailedAssert("Added troop item is null!", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade.ViewModelCollection\\Order\\MissionOrderTroopControllerVM.cs", "AddTroopItemIfNotExist", 882);
			}
			return orderTroopItemVM;
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000215 RID: 533 RVA: 0x00009D03 File Offset: 0x00007F03
		// (set) Token: 0x06000216 RID: 534 RVA: 0x00009D0C File Offset: 0x00007F0C
		[DataSourceProperty]
		public bool IsTransferActive
		{
			get
			{
				return this._isTransferActive;
			}
			set
			{
				if (value != this._isTransferActive)
				{
					this._isTransferActive = value;
					base.OnPropertyChanged("IsTransferActive");
					this._missionOrder.IsTroopPlacingActive = !value;
					if (this._missionOrder.OrderSetsWithOrdersByType.ContainsKey(OrderSetType.Toggle))
					{
						this._missionOrder.OrderSetsWithOrdersByType[OrderSetType.Toggle].GetOrder(OrderSubType.ToggleTransfer).SelectionState = (value ? 3 : 1);
						this._missionOrder.OrderSetsWithOrdersByType[OrderSetType.Toggle].FinalizeActiveStatus(false);
					}
					if (this._isTransferActive)
					{
						foreach (OrderTroopItemVM orderTroopItemVM in this.TransferTargetList)
						{
							orderTroopItemVM.SetFormationClassFromFormation(orderTroopItemVM.Formation);
							orderTroopItemVM.Morale = (int)MissionGameModels.Current.BattleMoraleModel.GetAverageMorale(orderTroopItemVM.Formation);
							orderTroopItemVM.IsAmmoAvailable = (orderTroopItemVM.Formation.QuerySystem.RangedUnitRatio > 0f || orderTroopItemVM.Formation.QuerySystem.RangedCavalryUnitRatio > 0f);
						}
					}
					if (this.Mission != null)
					{
						this.Mission.IsTransferMenuOpen = value;
					}
				}
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00009E4C File Offset: 0x0000804C
		// (set) Token: 0x06000218 RID: 536 RVA: 0x00009E54 File Offset: 0x00008054
		[DataSourceProperty]
		public bool IsTransferValid
		{
			get
			{
				return this._isTransferValid;
			}
			set
			{
				if (value != this._isTransferValid)
				{
					this._isTransferValid = value;
					base.OnPropertyChanged("IsTransferValid");
					if (!value)
					{
						this.TransferTitleText = "";
					}
				}
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000219 RID: 537 RVA: 0x00009E7F File Offset: 0x0000807F
		// (set) Token: 0x0600021A RID: 538 RVA: 0x00009E87 File Offset: 0x00008087
		[DataSourceProperty]
		public MBBindingList<OrderTroopItemVM> TransferTargetList
		{
			get
			{
				return this._transferTargetList;
			}
			set
			{
				if (value != this._transferTargetList)
				{
					this._transferTargetList = value;
					base.OnPropertyChanged("TransferTargetList");
				}
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00009EA4 File Offset: 0x000080A4
		// (set) Token: 0x0600021C RID: 540 RVA: 0x00009EAC File Offset: 0x000080AC
		[DataSourceProperty]
		public int TransferMaxValue
		{
			get
			{
				return this._transferMaxValue;
			}
			set
			{
				if (value != this._transferMaxValue)
				{
					this._transferMaxValue = value;
					base.OnPropertyChanged("TransferMaxValue");
				}
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600021D RID: 541 RVA: 0x00009EC9 File Offset: 0x000080C9
		// (set) Token: 0x0600021E RID: 542 RVA: 0x00009ED1 File Offset: 0x000080D1
		[DataSourceProperty]
		public int TransferValue
		{
			get
			{
				return this._transferValue;
			}
			set
			{
				if (value != this._transferValue)
				{
					this._transferValue = value;
					base.OnPropertyChanged("TransferValue");
				}
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600021F RID: 543 RVA: 0x00009EEE File Offset: 0x000080EE
		// (set) Token: 0x06000220 RID: 544 RVA: 0x00009EF6 File Offset: 0x000080F6
		[DataSourceProperty]
		public string TransferTitleText
		{
			get
			{
				return this._transferTitleText;
			}
			set
			{
				if (value != this._transferTitleText)
				{
					this._transferTitleText = value;
					base.OnPropertyChanged("TransferTitleText");
				}
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000221 RID: 545 RVA: 0x00009F18 File Offset: 0x00008118
		// (set) Token: 0x06000222 RID: 546 RVA: 0x00009F20 File Offset: 0x00008120
		[DataSourceProperty]
		public string AcceptText
		{
			get
			{
				return this._acceptText;
			}
			set
			{
				if (value != this._acceptText)
				{
					this._acceptText = value;
					base.OnPropertyChanged("AcceptText");
				}
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000223 RID: 547 RVA: 0x00009F42 File Offset: 0x00008142
		// (set) Token: 0x06000224 RID: 548 RVA: 0x00009F4A File Offset: 0x0000814A
		[DataSourceProperty]
		public string CancelText
		{
			get
			{
				return this._cancelText;
			}
			set
			{
				if (value != this._cancelText)
				{
					this._cancelText = value;
					base.OnPropertyChanged("CancelText");
				}
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000225 RID: 549 RVA: 0x00009F6C File Offset: 0x0000816C
		// (set) Token: 0x06000226 RID: 550 RVA: 0x00009F74 File Offset: 0x00008174
		[DataSourceProperty]
		public MBBindingList<OrderTroopItemVM> TroopList
		{
			get
			{
				return this._troopList;
			}
			set
			{
				if (value != this._troopList)
				{
					this._troopList = value;
					base.OnPropertyChanged("TroopList");
				}
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000227 RID: 551 RVA: 0x00009F91 File Offset: 0x00008191
		// (set) Token: 0x06000228 RID: 552 RVA: 0x00009F99 File Offset: 0x00008199
		[DataSourceProperty]
		public InputKeyItemVM DoneInputKey
		{
			get
			{
				return this._doneInputKey;
			}
			set
			{
				if (value != this._doneInputKey)
				{
					this._doneInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "DoneInputKey");
				}
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00009FB7 File Offset: 0x000081B7
		// (set) Token: 0x0600022A RID: 554 RVA: 0x00009FBF File Offset: 0x000081BF
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

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00009FDD File Offset: 0x000081DD
		// (set) Token: 0x0600022C RID: 556 RVA: 0x00009FE5 File Offset: 0x000081E5
		[DataSourceProperty]
		public InputKeyItemVM ResetInputKey
		{
			get
			{
				return this._resetInputKey;
			}
			set
			{
				if (value != this._resetInputKey)
				{
					this._resetInputKey = value;
					base.OnPropertyChangedWithValue<InputKeyItemVM>(value, "ResetInputKey");
				}
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000A003 File Offset: 0x00008203
		public void SetDoneInputKey(HotKey hotKey)
		{
			this.DoneInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000A012 File Offset: 0x00008212
		public void SetCancelInputKey(HotKey hotKey)
		{
			this.CancelInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000A021 File Offset: 0x00008221
		public void SetResetInputKey(HotKey hotKey)
		{
			this.ResetInputKey = InputKeyItemVM.CreateFromHotKey(hotKey, true);
		}

		// Token: 0x040000E7 RID: 231
		private readonly MissionOrderVM _missionOrder;

		// Token: 0x040000E8 RID: 232
		private readonly Action _onTransferFinised;

		// Token: 0x040000E9 RID: 233
		private readonly bool _isMultiplayer;

		// Token: 0x040000EA RID: 234
		private List<ValueTuple<int, List<int>>> _filterData;

		// Token: 0x040000EB RID: 235
		private bool _isDeployment;

		// Token: 0x040000EC RID: 236
		private MissionOrderTroopControllerVM.TroopItemFormationIndexComparer _formationIndexComparer;

		// Token: 0x040000ED RID: 237
		private MBBindingList<OrderTroopItemVM> _troopList;

		// Token: 0x040000EE RID: 238
		private bool _isTransferActive;

		// Token: 0x040000EF RID: 239
		private MBBindingList<OrderTroopItemVM> _transferTargetList;

		// Token: 0x040000F0 RID: 240
		private int _transferValue;

		// Token: 0x040000F1 RID: 241
		private int _transferMaxValue;

		// Token: 0x040000F2 RID: 242
		private string _transferTitleText;

		// Token: 0x040000F3 RID: 243
		private string _acceptText;

		// Token: 0x040000F4 RID: 244
		private string _cancelText;

		// Token: 0x040000F5 RID: 245
		private bool _isTransferValid;

		// Token: 0x040000F6 RID: 246
		private InputKeyItemVM _doneInputKey;

		// Token: 0x040000F7 RID: 247
		private InputKeyItemVM _cancelInputKey;

		// Token: 0x040000F8 RID: 248
		private InputKeyItemVM _resetInputKey;

		// Token: 0x0200009A RID: 154
		private class TroopItemFormationIndexComparer : IComparer<OrderTroopItemVM>
		{
			// Token: 0x06000A8E RID: 2702 RVA: 0x00027948 File Offset: 0x00025B48
			public int Compare(OrderTroopItemVM x, OrderTroopItemVM y)
			{
				return x.Formation.Index.CompareTo(y.Formation.Index);
			}
		}
	}
}
