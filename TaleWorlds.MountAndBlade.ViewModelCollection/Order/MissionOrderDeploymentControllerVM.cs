using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.Missions.Handlers;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.Order
{
	// Token: 0x02000017 RID: 23
	public class MissionOrderDeploymentControllerVM : ViewModel
	{
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x000071A5 File Offset: 0x000053A5
		private Mission Mission
		{
			get
			{
				return Mission.Current;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x000071AC File Offset: 0x000053AC
		private Team Team
		{
			get
			{
				return Mission.Current.PlayerTeam;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x000071B8 File Offset: 0x000053B8
		public OrderController OrderController
		{
			get
			{
				return this.Team.PlayerOrderController;
			}
		}

		// Token: 0x060001CA RID: 458 RVA: 0x000071C8 File Offset: 0x000053C8
		public MissionOrderDeploymentControllerVM(List<DeploymentPoint> deploymentPoints, MissionOrderVM missionOrder, Camera deploymentCamera, Action<bool> toggleMissionInputs, OnRefreshVisualsDelegate onRefreshVisuals)
		{
			this._deploymentPoints = deploymentPoints;
			this._missionOrder = missionOrder;
			this._deploymentCamera = deploymentCamera;
			this._toggleMissionInputs = toggleMissionInputs;
			this._onRefreshVisuals = onRefreshVisuals;
			this.SiegeMachineList = new MBBindingList<OrderSiegeMachineVM>();
			this.SiegeDeploymentList = new MBBindingList<DeploymentSiegeMachineVM>();
			this.DeploymentTargets = new MBBindingList<DeploymentSiegeMachineVM>();
			MBTextManager.SetTextVariable("UNDEPLOYED_SIEGE_MACHINE_COUNT", this.SiegeMachineList.Count((OrderSiegeMachineVM s) => !s.SiegeWeapon.IsUsed).ToString(), false);
			this._siegeDeployQueryData = new InquiryData(new TextObject("{=TxphX8Uk}Deployment", null).ToString(), new TextObject("{=LlrlE199}You can still deploy siege engines.\nBegin anyway?", null).ToString(), true, true, GameTexts.FindText("str_ok", null).ToString(), GameTexts.FindText("str_cancel", null).ToString(), delegate()
			{
				this._siegeDeploymentHandler.FinishDeployment();
				this._missionOrder.TryCloseToggleOrder(false);
			}, null, "", 0f, null, null, null);
			this.SiegeMachineList.Clear();
			this.OrderController.SiegeWeaponController.OnSelectedSiegeWeaponsChanged += this.OnSelectedSiegeWeaponsChanged;
			this._siegeDeploymentHandler = this.Mission.GetMissionBehavior<SiegeDeploymentHandler>();
			if (this._siegeDeploymentHandler != null)
			{
				this._siegeDeploymentHandler.OnDeploymentReady += this.ExecuteDeployAll;
				this._siegeDeploymentHandler.OnAIDeploymentReady += this.ExecuteDeployAI;
			}
			else
			{
				this._battleDeploymentHandler = this.Mission.GetMissionBehavior<BattleDeploymentHandler>();
				if (this._battleDeploymentHandler != null)
				{
					this._battleDeploymentHandler.OnDeploymentReady += this.ExecuteDeployAll;
				}
			}
			this.SiegeDeploymentList.Clear();
			if (this._siegeDeploymentHandler != null)
			{
				int num = 1;
				foreach (DeploymentPoint deploymentPoint in this._deploymentPoints)
				{
					OrderSiegeMachineVM item = new OrderSiegeMachineVM(deploymentPoint, new Action<OrderSiegeMachineVM>(this.OnSelectOrderSiegeMachine), num++);
					this.SiegeMachineList.Add(item);
					if (deploymentPoint.DeployableWeapons.Any((SynchedMissionObject x) => this._siegeDeploymentHandler.GetMaxDeployableWeaponCountOfPlayer(x.GetType()) > 0))
					{
						DeploymentSiegeMachineVM item2 = new DeploymentSiegeMachineVM(deploymentPoint, null, this._deploymentCamera, new Action<DeploymentSiegeMachineVM>(this.OnRefreshSelectedDeploymentPoint), new Action<DeploymentPoint>(this.OnEntityHover), deploymentPoint.IsDeployed);
						this.DeploymentTargets.Add(item2);
					}
				}
			}
			this.RefreshValues();
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00007444 File Offset: 0x00005644
		public override void RefreshValues()
		{
			base.RefreshValues();
			this._siegeMachineList.ApplyActionOnAllItems(delegate(OrderSiegeMachineVM x)
			{
				x.RefreshValues();
			});
			this._siegeDeploymentList.ApplyActionOnAllItems(delegate(DeploymentSiegeMachineVM x)
			{
				x.RefreshValues();
			});
			this._deploymentTargets.ApplyActionOnAllItems(delegate(DeploymentSiegeMachineVM x)
			{
				x.RefreshValues();
			});
		}

		// Token: 0x060001CC RID: 460 RVA: 0x000074D5 File Offset: 0x000056D5
		public void SetIsOrderPreconfigured(bool isPreconfigured)
		{
			this._isOrderPreconfigured = isPreconfigured;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x000074E0 File Offset: 0x000056E0
		internal void Update()
		{
			for (int i = 0; i < this.DeploymentTargets.Count; i++)
			{
				this.DeploymentTargets[i].Update();
			}
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00007514 File Offset: 0x00005714
		internal void DeployFormationsOfPlayer()
		{
			if (this.Mission.IsSiegeBattle)
			{
				this.Mission.AutoDeployTeamUsingTeamAI(this.Mission.PlayerTeam, false);
			}
			else
			{
				this.Mission.AutoDeployTeamUsingDeploymentPlan(this.Mission.PlayerTeam);
			}
			AssignPlayerRoleInTeamMissionController missionBehavior = Mission.Current.GetMissionBehavior<AssignPlayerRoleInTeamMissionController>();
			if (missionBehavior != null)
			{
				missionBehavior.OnPlayerTeamDeployed();
			}
			if (this.Mission.IsSiegeBattle)
			{
				this.Mission.AutoAssignDetachmentsForDeployment(this.Mission.PlayerTeam);
			}
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00007595 File Offset: 0x00005795
		internal void DeployFormationsOfAI()
		{
			this.Mission.AutoDeployTeamUsingTeamAI(this.Mission.PlayerEnemyTeam, true);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x000075AE File Offset: 0x000057AE
		internal void SetSiegeMachineActiveOrders(OrderSiegeMachineVM siegeItemVM)
		{
			siegeItemVM.ActiveOrders.Clear();
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x000075BC File Offset: 0x000057BC
		internal void ProcessSiegeMachines()
		{
			foreach (OrderSiegeMachineVM orderSiegeMachineVM in this.SiegeMachineList)
			{
				orderSiegeMachineVM.RefreshSiegeWeapon();
				if (!orderSiegeMachineVM.IsSelectable && orderSiegeMachineVM.IsSelected)
				{
					this.OnDeselectSiegeMachine(orderSiegeMachineVM);
				}
			}
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00007620 File Offset: 0x00005820
		internal void SelectAllSiegeMachines()
		{
			this.OrderController.SiegeWeaponController.SelectAll();
			foreach (OrderSiegeMachineVM orderSiegeMachineVM in this.SiegeMachineList)
			{
				orderSiegeMachineVM.IsSelected = orderSiegeMachineVM.IsSelectable;
				if (orderSiegeMachineVM.IsSelectable)
				{
					this.SetSiegeMachineActiveOrders(orderSiegeMachineVM);
				}
			}
			this._missionOrder.SetActiveOrders();
			this._onRefreshVisuals();
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x000076A8 File Offset: 0x000058A8
		internal void AddSelectedSiegeMachine(OrderSiegeMachineVM item)
		{
			if (!item.IsSelectable)
			{
				return;
			}
			this.OrderController.SiegeWeaponController.Select(item.SiegeWeapon);
			item.IsSelected = true;
			this._missionOrder.SetActiveOrders();
			this._onRefreshVisuals();
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x000076E8 File Offset: 0x000058E8
		internal void SetSelectedSiegeMachine(OrderSiegeMachineVM item)
		{
			this.ProcessSiegeMachines();
			if (!item.IsSelectable)
			{
				return;
			}
			this.SetSiegeMachineActiveOrders(item);
			this.OrderController.SiegeWeaponController.ClearSelectedWeapons();
			foreach (OrderSiegeMachineVM orderSiegeMachineVM in this.SiegeMachineList)
			{
				orderSiegeMachineVM.IsSelected = false;
			}
			this.AddSelectedSiegeMachine(item);
			this._onRefreshVisuals();
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000776C File Offset: 0x0000596C
		internal void OnDeselectSiegeMachine(OrderSiegeMachineVM item)
		{
			if (this.OrderController.SiegeWeaponController.SelectedWeapons.Contains(item.SiegeWeapon))
			{
				this.OrderController.SiegeWeaponController.Deselect(item.SiegeWeapon);
			}
			item.IsSelected = false;
			this._missionOrder.SetActiveOrders();
			this._onRefreshVisuals();
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x000077CC File Offset: 0x000059CC
		internal void OnSelectOrderSiegeMachine(OrderSiegeMachineVM item)
		{
			this.ProcessSiegeMachines();
			this._missionOrder.IsTroopPlacingActive = false;
			if (item.IsSelectable)
			{
				if (Input.DebugInput.IsControlDown())
				{
					if (item.IsSelected)
					{
						this.OnDeselectSiegeMachine(item);
					}
					else
					{
						this.AddSelectedSiegeMachine(item);
					}
				}
				else
				{
					this.SetSelectedSiegeMachine(item);
				}
				this._onRefreshVisuals();
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000782C File Offset: 0x00005A2C
		internal void OnSelectDeploymentSiegeMachine(DeploymentSiegeMachineVM item)
		{
			this.IsSiegeDeploymentListActive = false;
			GameEntity currentSelectedEntity = this._currentSelectedEntity;
			if (currentSelectedEntity != null)
			{
				currentSelectedEntity.SetContourColor(null, true);
			}
			this._currentSelectedEntity = null;
			this._selectedDeploymentPointVM = null;
			this.SiegeDeploymentList.Clear();
			bool flag = false;
			if (item != null && (!(item.MachineType != null) || this._siegeDeploymentHandler.GetDeployableWeaponCountOfPlayer(item.MachineType) != 0) && (item.DeploymentPoint.DeployedWeapon == null || !(item.DeploymentPoint.DeployedWeapon.GetType() == item.MachineType)))
			{
				bool flag2 = !item.DeploymentPoint.IsDeployed || item.DeploymentPoint.DeployedWeapon != item.SiegeWeapon;
				if (item.DeploymentPoint.IsDeployed)
				{
					if (item.SiegeWeapon == null)
					{
						SoundEvent.PlaySound2D("event:/ui/dropdown");
					}
					item.DeploymentPoint.Disband();
				}
				flag = !this.SiegeMachineList.Any((OrderSiegeMachineVM s) => s.DeploymentPoint.IsDeployed);
				if (flag2 && item.SiegeWeapon != null)
				{
					SiegeEngineType machine = item.Machine;
					if (machine == DefaultSiegeEngineTypes.Catapult || machine == DefaultSiegeEngineTypes.FireCatapult || machine == DefaultSiegeEngineTypes.Onager || machine == DefaultSiegeEngineTypes.FireOnager)
					{
						SoundEvent.PlaySound2D("event:/ui/mission/catapult");
					}
					else if (machine == DefaultSiegeEngineTypes.Ram)
					{
						SoundEvent.PlaySound2D("event:/ui/mission/batteringram");
					}
					else if (machine == DefaultSiegeEngineTypes.SiegeTower)
					{
						SoundEvent.PlaySound2D("event:/ui/mission/siegetower");
					}
					else if (machine == DefaultSiegeEngineTypes.Trebuchet || machine == DefaultSiegeEngineTypes.Bricole)
					{
						SoundEvent.PlaySound2D("event:/ui/mission/catapult");
					}
					else if (machine == DefaultSiegeEngineTypes.Ballista || machine == DefaultSiegeEngineTypes.FireBallista)
					{
						SoundEvent.PlaySound2D("event:/ui/mission/ballista");
					}
					item.DeploymentPoint.Deploy(item.SiegeWeapon);
				}
			}
			this.ProcessSiegeMachines();
			if (flag && this._missionOrder.IsToggleOrderShown)
			{
				this._missionOrder.SetActiveOrders();
			}
			this._onRefreshVisuals();
			foreach (DeploymentSiegeMachineVM deploymentSiegeMachineVM in this.DeploymentTargets)
			{
				deploymentSiegeMachineVM.RefreshWithDeployedWeapon();
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00007A70 File Offset: 0x00005C70
		internal void OnSelectedSiegeWeaponsChanged()
		{
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00007A72 File Offset: 0x00005C72
		public void OnRefreshSelectedDeploymentPoint(DeploymentSiegeMachineVM item)
		{
			this.RefreshSelectedDeploymentPoint(item.DeploymentPoint);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00007A80 File Offset: 0x00005C80
		public void OnEntityHover(GameEntity hoveredEntity)
		{
			if (this._currentHoveredEntity == hoveredEntity)
			{
				return;
			}
			DeploymentPoint deploymentPoint = null;
			if (hoveredEntity != null)
			{
				if (hoveredEntity.HasScriptOfType<DeploymentPoint>())
				{
					deploymentPoint = hoveredEntity.GetFirstScriptOfType<DeploymentPoint>();
				}
				else if (this._siegeDeploymentHandler != null)
				{
					deploymentPoint = this._siegeDeploymentHandler.PlayerDeploymentPoints.SingleOrDefault((DeploymentPoint dp) => dp.IsDeployed && hoveredEntity.GetScriptComponents().Any((ScriptComponentBehavior sc) => dp.DeployedWeapon == sc));
				}
			}
			this.OnEntityHover(deploymentPoint);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00007B08 File Offset: 0x00005D08
		public void OnEntityHover(DeploymentPoint deploymentPoint)
		{
			if (this._currentSelectedEntity != this._currentHoveredEntity)
			{
				GameEntity currentHoveredEntity = this._currentHoveredEntity;
				if (currentHoveredEntity != null)
				{
					currentHoveredEntity.SetContourColor(null, true);
				}
			}
			if (deploymentPoint != null)
			{
				this._currentHoveredEntity = (deploymentPoint.IsDeployed ? deploymentPoint.DeployedWeapon.GameEntity : deploymentPoint.GameEntity);
			}
			else
			{
				this._currentHoveredEntity = null;
			}
			if (this._currentSelectedEntity != this._currentHoveredEntity)
			{
				GameEntity currentHoveredEntity2 = this._currentHoveredEntity;
				if (currentHoveredEntity2 == null)
				{
					return;
				}
				currentHoveredEntity2.SetContourColor(new uint?(4289622555U), true);
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00007BA0 File Offset: 0x00005DA0
		public void OnEntitySelect(GameEntity selectedEntity)
		{
			if (this._currentSelectedEntity == selectedEntity)
			{
				return;
			}
			DeploymentPoint deploymentPoint = null;
			if (selectedEntity != null && this._siegeDeploymentHandler != null)
			{
				if (selectedEntity.HasScriptOfType<DeploymentPoint>())
				{
					deploymentPoint = selectedEntity.GetFirstScriptOfType<DeploymentPoint>();
				}
				else if (this._siegeDeploymentHandler != null)
				{
					deploymentPoint = this._siegeDeploymentHandler.PlayerDeploymentPoints.SingleOrDefault((DeploymentPoint dp) => dp.IsDeployed && selectedEntity.GetScriptComponents().Any((ScriptComponentBehavior sc) => dp.DeployedWeapon == sc));
				}
			}
			if (deploymentPoint != null)
			{
				this._missionOrder.IsTroopPlacingActive = false;
				this.RefreshSelectedDeploymentPoint(deploymentPoint);
				return;
			}
			this.ExecuteCancelSelectedDeploymentPoint();
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00007C44 File Offset: 0x00005E44
		public void RefreshSelectedDeploymentPoint(DeploymentPoint selectedDeploymentPoint)
		{
			this.IsSiegeDeploymentListActive = false;
			foreach (DeploymentSiegeMachineVM deploymentSiegeMachineVM in this.DeploymentTargets)
			{
				if (deploymentSiegeMachineVM.DeploymentPoint == selectedDeploymentPoint)
				{
					this._selectedDeploymentPointVM = deploymentSiegeMachineVM;
				}
			}
			if (!this._selectedDeploymentPointVM.IsSelected)
			{
				this._selectedDeploymentPointVM.IsSelected = true;
			}
			this.SiegeDeploymentList.Clear();
			DeploymentSiegeMachineVM deploymentSiegeMachineVM2;
			foreach (SynchedMissionObject synchedMissionObject in selectedDeploymentPoint.DeployableWeapons)
			{
				Type type = synchedMissionObject.GetType();
				if (this._siegeDeploymentHandler.GetMaxDeployableWeaponCountOfPlayer(type) > 0)
				{
					deploymentSiegeMachineVM2 = new DeploymentSiegeMachineVM(selectedDeploymentPoint, synchedMissionObject as SiegeWeapon, this._deploymentCamera, new Action<DeploymentSiegeMachineVM>(this.OnSelectDeploymentSiegeMachine), null, selectedDeploymentPoint.IsDeployed && selectedDeploymentPoint.DeployedWeapon == synchedMissionObject);
					this.SiegeDeploymentList.Add(deploymentSiegeMachineVM2);
					deploymentSiegeMachineVM2.RemainingCount = this._siegeDeploymentHandler.GetDeployableWeaponCountOfPlayer(type);
				}
			}
			deploymentSiegeMachineVM2 = new DeploymentSiegeMachineVM(selectedDeploymentPoint, null, this._deploymentCamera, new Action<DeploymentSiegeMachineVM>(this.OnSelectDeploymentSiegeMachine), null, !selectedDeploymentPoint.IsDeployed);
			this.SiegeDeploymentList.Add(deploymentSiegeMachineVM2);
			selectedDeploymentPoint.GameEntity.SetContourColor(new uint?(4293481743U), true);
			this.IsSiegeDeploymentListActive = true;
			GameEntity currentSelectedEntity = this._currentSelectedEntity;
			if (currentSelectedEntity != null)
			{
				currentSelectedEntity.SetContourColor(null, true);
			}
			this._currentSelectedEntity = selectedDeploymentPoint.GameEntity;
			GameEntity currentSelectedEntity2 = this._currentSelectedEntity;
			if (currentSelectedEntity2 == null)
			{
				return;
			}
			currentSelectedEntity2.SetContourColor(new uint?(4293481743U), true);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00007DFC File Offset: 0x00005FFC
		public void ExecuteCancelSelectedDeploymentPoint()
		{
			this.OnSelectDeploymentSiegeMachine(null);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00007E08 File Offset: 0x00006008
		public void ExecuteBeginSiege()
		{
			this.IsSiegeDeploymentListActive = false;
			if (this._siegeDeploymentHandler != null && this._siegeDeploymentHandler.PlayerDeploymentPoints.Any((DeploymentPoint d) => !d.IsDeployed && d.DeployableWeaponTypes.Any((Type type) => this._siegeDeploymentHandler.GetDeployableWeaponCountOfPlayer(type) > 0)))
			{
				InformationManager.ShowInquiry(this._siegeDeployQueryData, false, false);
				return;
			}
			this._missionOrder.TryCloseToggleOrder(false);
			if (this._siegeDeploymentHandler != null)
			{
				this._siegeDeploymentHandler.FinishDeployment();
				return;
			}
			this._battleDeploymentHandler.FinishDeployment();
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00007E80 File Offset: 0x00006080
		public void ExecuteAutoDeploy()
		{
			this.Mission.TryRemakeInitialDeploymentPlanForBattleSide(this.Mission.PlayerTeam.Side);
			if (this.Mission.IsSiegeBattle)
			{
				this.Mission.AutoDeployTeamUsingTeamAI(this.Mission.PlayerTeam, true);
				this.AutoDeploySiegeMachines();
				return;
			}
			this.Mission.AutoDeployTeamUsingDeploymentPlan(this.Mission.PlayerTeam);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00007EEC File Offset: 0x000060EC
		private void AutoDeploySiegeMachines()
		{
			this.IsSiegeDeploymentListActive = false;
			foreach (DeploymentSiegeMachineVM deploymentSiegeMachineVM in this.DeploymentTargets)
			{
				if (!(deploymentSiegeMachineVM.MachineType != null))
				{
					deploymentSiegeMachineVM.ExecuteAction();
					DeploymentSiegeMachineVM deploymentSiegeMachineVM2 = this.SiegeDeploymentList.FirstOrDefault((DeploymentSiegeMachineVM d) => d.Machine != null && d.RemainingCount > 0);
					if (deploymentSiegeMachineVM2 != null)
					{
						deploymentSiegeMachineVM2.ExecuteAction();
					}
				}
			}
			this.IsSiegeDeploymentListActive = false;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00007F8C File Offset: 0x0000618C
		public void ExecuteDeployAll()
		{
			if (this._siegeDeploymentHandler != null)
			{
				this.Mission.ForceTickOccasionally = true;
				bool isTeleportingAgents = Mission.Current.IsTeleportingAgents;
				this.Mission.IsTeleportingAgents = true;
				this.DeployFormationsOfPlayer();
				this._siegeDeploymentHandler.ForceUpdateAllUnits();
				this._missionOrder.OnDeployAll();
				foreach (OrderSiegeMachineVM orderSiegeMachineVM in this.SiegeMachineList)
				{
					orderSiegeMachineVM.RefreshSiegeWeapon();
				}
				foreach (DeploymentSiegeMachineVM deploymentSiegeMachineVM in this.DeploymentTargets)
				{
					deploymentSiegeMachineVM.RefreshWithDeployedWeapon();
				}
				this.Mission.IsTeleportingAgents = isTeleportingAgents;
				this.Mission.ForceTickOccasionally = false;
				this.SelectAllSiegeMachines();
				return;
			}
			if (this._battleDeploymentHandler != null)
			{
				this.DeployFormationsOfPlayer();
				this._battleDeploymentHandler.ForceUpdateAllUnits();
				this._missionOrder.OnDeployAll();
			}
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000809C File Offset: 0x0000629C
		private void ExecuteDeployAI()
		{
			if (this._siegeDeploymentHandler != null)
			{
				this.Mission.ForceTickOccasionally = true;
				bool isTeleportingAgents = Mission.Current.IsTeleportingAgents;
				this.Mission.IsTeleportingAgents = true;
				this.DeployFormationsOfAI();
				this._siegeDeploymentHandler.ForceUpdateAllUnits();
				this._missionOrder.OnDeployAll();
				foreach (OrderSiegeMachineVM orderSiegeMachineVM in this.SiegeMachineList)
				{
					orderSiegeMachineVM.RefreshSiegeWeapon();
				}
				foreach (DeploymentSiegeMachineVM deploymentSiegeMachineVM in this.DeploymentTargets)
				{
					deploymentSiegeMachineVM.RefreshWithDeployedWeapon();
				}
				this.Mission.IsTeleportingAgents = isTeleportingAgents;
				this.Mission.ForceTickOccasionally = false;
				this.SelectAllSiegeMachines();
				return;
			}
			if (this._battleDeploymentHandler != null)
			{
				this._battleDeploymentHandler.ForceUpdateAllUnits();
				this._missionOrder.OnDeployAll();
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x000081A8 File Offset: 0x000063A8
		public void FinalizeDeployment()
		{
			this._missionOrder.IsDeployment = false;
			foreach (OrderSiegeMachineVM orderSiegeMachineVM in this.SiegeMachineList.ToList<OrderSiegeMachineVM>())
			{
				if (orderSiegeMachineVM.DeploymentPoint.IsDeployed)
				{
					this.SetSiegeMachineActiveOrders(orderSiegeMachineVM);
				}
				else
				{
					this.SiegeMachineList.Remove(orderSiegeMachineVM);
				}
			}
			this.DeploymentTargets.Clear();
			this.SiegeDeploymentList.Clear();
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00008240 File Offset: 0x00006440
		internal void OnSelectFormationWithIndex(int formationTroopIndex)
		{
			OrderSiegeMachineVM orderSiegeMachineVM = this.SiegeMachineList.ElementAtOrDefault(formationTroopIndex);
			if (orderSiegeMachineVM != null)
			{
				this.OnSelectOrderSiegeMachine(orderSiegeMachineVM);
				return;
			}
			this.SelectAllSiegeMachines();
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000826C File Offset: 0x0000646C
		internal void SetCurrentActiveOrders()
		{
			List<OrderSubjectVM> list = (from item in this.SiegeMachineList.Cast<OrderSubjectVM>().ToList<OrderSubjectVM>()
			where item.IsSelected && item.IsSelectable
			select item).ToList<OrderSubjectVM>();
			if (list.IsEmpty<OrderSubjectVM>())
			{
				this.OrderController.SiegeWeaponController.SelectAll();
				foreach (OrderSiegeMachineVM orderSiegeMachineVM in from s in this.SiegeMachineList
				where s.IsSelectable && s.DeploymentPoint.IsDeployed
				select s)
				{
					orderSiegeMachineVM.IsSelected = true;
					this.SetSiegeMachineActiveOrders(orderSiegeMachineVM);
					list.Add(orderSiegeMachineVM);
				}
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00008340 File Offset: 0x00006540
		public override void OnFinalize()
		{
			base.OnFinalize();
			this.OrderController.SiegeWeaponController.OnSelectedSiegeWeaponsChanged -= this.OnSelectedSiegeWeaponsChanged;
			this.SiegeDeploymentList.Clear();
			foreach (OrderSiegeMachineVM orderSiegeMachineVM in this.SiegeMachineList.ToList<OrderSiegeMachineVM>())
			{
				if (!orderSiegeMachineVM.DeploymentPoint.IsDeployed)
				{
					this.SiegeMachineList.Remove(orderSiegeMachineVM);
				}
			}
			this._siegeDeploymentHandler = null;
			this._siegeDeployQueryData = null;
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x000083E8 File Offset: 0x000065E8
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x000083F0 File Offset: 0x000065F0
		[DataSourceProperty]
		public MBBindingList<OrderSiegeMachineVM> SiegeMachineList
		{
			get
			{
				return this._siegeMachineList;
			}
			set
			{
				if (value != this._siegeMachineList)
				{
					this._siegeMachineList = value;
					base.OnPropertyChanged("SiegeMachineList");
				}
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0000840D File Offset: 0x0000660D
		// (set) Token: 0x060001EB RID: 491 RVA: 0x00008415 File Offset: 0x00006615
		[DataSourceProperty]
		public MBBindingList<DeploymentSiegeMachineVM> DeploymentTargets
		{
			get
			{
				return this._deploymentTargets;
			}
			set
			{
				if (value != this._deploymentTargets)
				{
					this._deploymentTargets = value;
					base.OnPropertyChanged("DeploymentTargets");
				}
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00008432 File Offset: 0x00006632
		// (set) Token: 0x060001ED RID: 493 RVA: 0x0000843C File Offset: 0x0000663C
		[DataSourceProperty]
		public bool IsSiegeDeploymentListActive
		{
			get
			{
				return this._isSiegeDeploymentListActive;
			}
			set
			{
				if (value != this._isSiegeDeploymentListActive)
				{
					this._isSiegeDeploymentListActive = value;
					base.OnPropertyChanged("IsSiegeDeploymentListActive");
					this._toggleMissionInputs(value);
					this._onRefreshVisuals();
					if (this._selectedDeploymentPointVM != null)
					{
						this._selectedDeploymentPointVM.IsSelected = value;
					}
				}
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001EE RID: 494 RVA: 0x0000848F File Offset: 0x0000668F
		// (set) Token: 0x060001EF RID: 495 RVA: 0x00008497 File Offset: 0x00006697
		[DataSourceProperty]
		public MBBindingList<DeploymentSiegeMachineVM> SiegeDeploymentList
		{
			get
			{
				return this._siegeDeploymentList;
			}
			set
			{
				if (value != this._siegeDeploymentList)
				{
					this._siegeDeploymentList = value;
					base.OnPropertyChanged("SiegeDeploymentList");
				}
			}
		}

		// Token: 0x040000D5 RID: 213
		public const uint ENTITYHIGHLIGHTCOLOR = 4289622555U;

		// Token: 0x040000D6 RID: 214
		public const uint ENTITYSELECTEDCOLOR = 4293481743U;

		// Token: 0x040000D7 RID: 215
		private GameEntity _currentSelectedEntity;

		// Token: 0x040000D8 RID: 216
		private GameEntity _currentHoveredEntity;

		// Token: 0x040000D9 RID: 217
		private InquiryData _siegeDeployQueryData;

		// Token: 0x040000DA RID: 218
		private SiegeDeploymentHandler _siegeDeploymentHandler;

		// Token: 0x040000DB RID: 219
		private BattleDeploymentHandler _battleDeploymentHandler;

		// Token: 0x040000DC RID: 220
		private readonly List<DeploymentPoint> _deploymentPoints;

		// Token: 0x040000DD RID: 221
		internal DeploymentSiegeMachineVM _selectedDeploymentPointVM;

		// Token: 0x040000DE RID: 222
		private readonly MissionOrderVM _missionOrder;

		// Token: 0x040000DF RID: 223
		private readonly Camera _deploymentCamera;

		// Token: 0x040000E0 RID: 224
		private readonly Action<bool> _toggleMissionInputs;

		// Token: 0x040000E1 RID: 225
		private readonly OnRefreshVisualsDelegate _onRefreshVisuals;

		// Token: 0x040000E2 RID: 226
		private bool _isOrderPreconfigured;

		// Token: 0x040000E3 RID: 227
		private MBBindingList<OrderSiegeMachineVM> _siegeMachineList;

		// Token: 0x040000E4 RID: 228
		private MBBindingList<DeploymentSiegeMachineVM> _siegeDeploymentList;

		// Token: 0x040000E5 RID: 229
		private MBBindingList<DeploymentSiegeMachineVM> _deploymentTargets;

		// Token: 0x040000E6 RID: 230
		private bool _isSiegeDeploymentListActive;
	}
}
