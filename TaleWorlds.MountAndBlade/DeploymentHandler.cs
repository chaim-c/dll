using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000272 RID: 626
	public class DeploymentHandler : MissionLogic
	{
		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x060020F9 RID: 8441 RVA: 0x000767C1 File Offset: 0x000749C1
		public Team team
		{
			get
			{
				return base.Mission.PlayerTeam;
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x060020FA RID: 8442 RVA: 0x000767CE File Offset: 0x000749CE
		public bool IsPlayerAttacker
		{
			get
			{
				return this.isPlayerAttacker;
			}
		}

		// Token: 0x060020FB RID: 8443 RVA: 0x000767D6 File Offset: 0x000749D6
		public DeploymentHandler(bool isPlayerAttacker)
		{
			this.isPlayerAttacker = isPlayerAttacker;
		}

		// Token: 0x060020FC RID: 8444 RVA: 0x000767E5 File Offset: 0x000749E5
		public override void EarlyStart()
		{
		}

		// Token: 0x060020FD RID: 8445 RVA: 0x000767E7 File Offset: 0x000749E7
		public override void AfterStart()
		{
			base.AfterStart();
			this.previousMissionMode = base.Mission.Mode;
			base.Mission.SetMissionMode(MissionMode.Deployment, true);
			this.team.OnOrderIssued += this.OrderController_OnOrderIssued;
		}

		// Token: 0x060020FE RID: 8446 RVA: 0x00076824 File Offset: 0x00074A24
		private void OrderController_OnOrderIssued(OrderType orderType, MBReadOnlyList<Formation> appliedFormations, OrderController orderController, params object[] delegateParams)
		{
			DeploymentHandler.OrderController_OnOrderIssued_Aux(orderType, appliedFormations, orderController, delegateParams);
		}

		// Token: 0x060020FF RID: 8447 RVA: 0x00076830 File Offset: 0x00074A30
		internal static void OrderController_OnOrderIssued_Aux(OrderType orderType, MBReadOnlyList<Formation> appliedFormations, OrderController orderController = null, params object[] delegateParams)
		{
			DeploymentHandler.<>c__DisplayClass10_0 CS$<>8__locals1;
			CS$<>8__locals1.appliedFormations = appliedFormations;
			CS$<>8__locals1.orderController = orderController;
			bool flag = false;
			using (List<Formation>.Enumerator enumerator = CS$<>8__locals1.appliedFormations.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.CountOfUnits > 0)
					{
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				return;
			}
			switch (orderType)
			{
			case OrderType.None:
				Debug.FailedAssert("false", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Missions\\MissionLogics\\DeploymentHandler.cs", "OrderController_OnOrderIssued_Aux", 107);
				return;
			case OrderType.Move:
			case OrderType.MoveToLineSegment:
			case OrderType.MoveToLineSegmentWithHorizontalLayout:
			case OrderType.FollowMe:
			case OrderType.FollowEntity:
			case OrderType.Advance:
			case OrderType.FallBack:
				DeploymentHandler.<OrderController_OnOrderIssued_Aux>g__ForcePositioning|10_1(ref CS$<>8__locals1);
				DeploymentHandler.<OrderController_OnOrderIssued_Aux>g__ForceUpdateFormationParams|10_0(ref CS$<>8__locals1);
				return;
			case OrderType.Charge:
			case OrderType.ChargeWithTarget:
			case OrderType.GuardMe:
				DeploymentHandler.<OrderController_OnOrderIssued_Aux>g__ForcePositioning|10_1(ref CS$<>8__locals1);
				DeploymentHandler.<OrderController_OnOrderIssued_Aux>g__ForceUpdateFormationParams|10_0(ref CS$<>8__locals1);
				return;
			case OrderType.StandYourGround:
				DeploymentHandler.<OrderController_OnOrderIssued_Aux>g__ForcePositioning|10_1(ref CS$<>8__locals1);
				DeploymentHandler.<OrderController_OnOrderIssued_Aux>g__ForceUpdateFormationParams|10_0(ref CS$<>8__locals1);
				return;
			case OrderType.Retreat:
				DeploymentHandler.<OrderController_OnOrderIssued_Aux>g__ForcePositioning|10_1(ref CS$<>8__locals1);
				DeploymentHandler.<OrderController_OnOrderIssued_Aux>g__ForceUpdateFormationParams|10_0(ref CS$<>8__locals1);
				return;
			case OrderType.LookAtEnemy:
			case OrderType.LookAtDirection:
				DeploymentHandler.<OrderController_OnOrderIssued_Aux>g__ForcePositioning|10_1(ref CS$<>8__locals1);
				DeploymentHandler.<OrderController_OnOrderIssued_Aux>g__ForceUpdateFormationParams|10_0(ref CS$<>8__locals1);
				return;
			case OrderType.ArrangementLine:
			case OrderType.ArrangementCloseOrder:
			case OrderType.ArrangementLoose:
			case OrderType.ArrangementCircular:
			case OrderType.ArrangementSchiltron:
			case OrderType.ArrangementVee:
			case OrderType.ArrangementColumn:
			case OrderType.ArrangementScatter:
				DeploymentHandler.<OrderController_OnOrderIssued_Aux>g__ForceUpdateFormationParams|10_0(ref CS$<>8__locals1);
				return;
			case OrderType.FormCustom:
			case OrderType.FormDeep:
			case OrderType.FormWide:
			case OrderType.FormWider:
				DeploymentHandler.<OrderController_OnOrderIssued_Aux>g__ForceUpdateFormationParams|10_0(ref CS$<>8__locals1);
				return;
			case OrderType.CohesionHigh:
			case OrderType.CohesionMedium:
			case OrderType.CohesionLow:
			case OrderType.HoldFire:
			case OrderType.FireAtWill:
				return;
			case OrderType.Mount:
			case OrderType.Dismount:
				DeploymentHandler.<OrderController_OnOrderIssued_Aux>g__ForceUpdateFormationParams|10_0(ref CS$<>8__locals1);
				return;
			case OrderType.AIControlOn:
			case OrderType.AIControlOff:
				DeploymentHandler.<OrderController_OnOrderIssued_Aux>g__ForcePositioning|10_1(ref CS$<>8__locals1);
				DeploymentHandler.<OrderController_OnOrderIssued_Aux>g__ForceUpdateFormationParams|10_0(ref CS$<>8__locals1);
				return;
			case OrderType.Transfer:
			case OrderType.Use:
			case OrderType.AttackEntity:
				DeploymentHandler.<OrderController_OnOrderIssued_Aux>g__ForceUpdateFormationParams|10_0(ref CS$<>8__locals1);
				return;
			case OrderType.PointDefence:
				Debug.FailedAssert("will be removed", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Missions\\MissionLogics\\DeploymentHandler.cs", "OrderController_OnOrderIssued_Aux", 180);
				return;
			}
			Debug.FailedAssert("false", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Missions\\MissionLogics\\DeploymentHandler.cs", "OrderController_OnOrderIssued_Aux", 183);
		}

		// Token: 0x06002100 RID: 8448 RVA: 0x00076A18 File Offset: 0x00074C18
		public void ForceUpdateAllUnits()
		{
			DeploymentHandler.OrderController_OnOrderIssued_Aux(OrderType.Move, this.team.FormationsIncludingSpecialAndEmpty, null, Array.Empty<object>());
		}

		// Token: 0x06002101 RID: 8449 RVA: 0x00076A31 File Offset: 0x00074C31
		public virtual void FinishDeployment()
		{
		}

		// Token: 0x06002102 RID: 8450 RVA: 0x00076A33 File Offset: 0x00074C33
		public override void OnRemoveBehavior()
		{
			if (this.team != null)
			{
				this.team.OnOrderIssued -= this.OrderController_OnOrderIssued;
			}
			base.Mission.SetMissionMode(this.previousMissionMode, false);
			base.OnRemoveBehavior();
		}

		// Token: 0x06002103 RID: 8451 RVA: 0x00076A6C File Offset: 0x00074C6C
		public void InitializeDeploymentPoints()
		{
			if (!this.areDeploymentPointsInitialized)
			{
				foreach (DeploymentPoint deploymentPoint in base.Mission.ActiveMissionObjects.FindAllWithType<DeploymentPoint>())
				{
					deploymentPoint.Hide();
				}
				this.areDeploymentPointsInitialized = true;
			}
		}

		// Token: 0x06002104 RID: 8452 RVA: 0x00076AD0 File Offset: 0x00074CD0
		[CompilerGenerated]
		internal unsafe static void <OrderController_OnOrderIssued_Aux>g__ForceUpdateFormationParams|10_0(ref DeploymentHandler.<>c__DisplayClass10_0 A_0)
		{
			foreach (Formation formation in A_0.appliedFormations)
			{
				if (formation.CountOfUnits > 0 && (A_0.orderController == null || A_0.orderController.FormationUpdateEnabledAfterSetOrder))
				{
					bool flag = false;
					if (formation.IsPlayerTroopInFormation)
					{
						flag = (formation.GetReadonlyMovementOrderReference()->OrderEnum == MovementOrder.MovementOrderEnum.Follow);
					}
					formation.ApplyActionOnEachUnit(delegate(Agent agent)
					{
						agent.UpdateCachedAndFormationValues(true, false);
					}, flag ? Mission.Current.MainAgent : null);
				}
			}
		}

		// Token: 0x06002105 RID: 8453 RVA: 0x00076B90 File Offset: 0x00074D90
		[CompilerGenerated]
		internal unsafe static void <OrderController_OnOrderIssued_Aux>g__ForcePositioning|10_1(ref DeploymentHandler.<>c__DisplayClass10_0 A_0)
		{
			foreach (Formation formation in A_0.appliedFormations)
			{
				if (formation.CountOfUnits > 0)
				{
					Vec2 direction = formation.FacingOrder.GetDirection(formation, null);
					Formation formation2 = formation;
					MovementOrder movementOrder = *formation.GetReadonlyMovementOrderReference();
					formation2.SetPositioning(new WorldPosition?(movementOrder.CreateNewOrderWorldPosition(formation, WorldPosition.WorldPositionEnforcedCache.None)), new Vec2?(direction), null);
				}
			}
		}

		// Token: 0x04000C39 RID: 3129
		protected MissionMode previousMissionMode;

		// Token: 0x04000C3A RID: 3130
		protected readonly bool isPlayerAttacker;

		// Token: 0x04000C3B RID: 3131
		private bool areDeploymentPointsInitialized;
	}
}
