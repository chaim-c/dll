using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000109 RID: 265
	public class BehaviorAssaultWalls : BehaviorComponent
	{
		// Token: 0x06000CF5 RID: 3317 RVA: 0x00018F90 File Offset: 0x00017190
		private void ResetOrderPositions()
		{
			this._primarySiegeWeapons = this._teamAISiegeComponent.PrimarySiegeWeapons.ToList<IPrimarySiegeWeapon>();
			this._primarySiegeWeapons.RemoveAll((IPrimarySiegeWeapon uM) => uM.WeaponSide != this._behaviorSide);
			IEnumerable<ICastleKeyPosition> source = (from sl in TeamAISiegeComponent.SiegeLanes
			where sl.LaneSide == this._behaviorSide
			select sl).SelectMany((SiegeLane sila) => sila.DefensePoints);
			this._innerGate = this._teamAISiegeComponent.InnerGate;
			this._isGateLane = (this._teamAISiegeComponent.OuterGate.DefenseSide == this._behaviorSide);
			if (this._isGateLane)
			{
				this._wallSegment = null;
			}
			else
			{
				WallSegment wallSegment = source.FirstOrDefault((ICastleKeyPosition dp) => dp is WallSegment && (dp as WallSegment).IsBreachedWall) as WallSegment;
				if (wallSegment != null)
				{
					this._wallSegment = wallSegment;
				}
				else
				{
					IPrimarySiegeWeapon primarySiegeWeapon = this._primarySiegeWeapons.MaxBy((IPrimarySiegeWeapon psw) => psw.SiegeWeaponPriority);
					this._wallSegment = (primarySiegeWeapon.TargetCastlePosition as WallSegment);
				}
			}
			this._stopOrder = MovementOrder.MovementOrderStop;
			this._chargeOrder = MovementOrder.MovementOrderCharge;
			bool flag = this._teamAISiegeComponent.OuterGate != null && this._behaviorSide == this._teamAISiegeComponent.OuterGate.DefenseSide;
			this._attackEntityOrderOuterGate = ((flag && !this._teamAISiegeComponent.OuterGate.IsDeactivated && this._teamAISiegeComponent.OuterGate.State != CastleGate.GateState.Open) ? MovementOrder.MovementOrderAttackEntity(this._teamAISiegeComponent.OuterGate.GameEntity, false) : MovementOrder.MovementOrderStop);
			this._attackEntityOrderInnerGate = ((flag && this._teamAISiegeComponent.InnerGate != null && !this._teamAISiegeComponent.InnerGate.IsDeactivated && this._teamAISiegeComponent.InnerGate.State != CastleGate.GateState.Open) ? MovementOrder.MovementOrderAttackEntity(this._teamAISiegeComponent.InnerGate.GameEntity, false) : MovementOrder.MovementOrderStop);
			WorldPosition origin = this._teamAISiegeComponent.OuterGate.MiddleFrame.Origin;
			this._castleGateMoveOrder = MovementOrder.MovementOrderMove(origin);
			if (this._isGateLane)
			{
				this._wallSegmentMoveOrder = this._castleGateMoveOrder;
			}
			else
			{
				WorldPosition origin2 = this._wallSegment.MiddleFrame.Origin;
				this._wallSegmentMoveOrder = MovementOrder.MovementOrderMove(origin2);
			}
			this._facingOrder = FacingOrder.FacingOrderLookAtEnemy;
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x00019200 File Offset: 0x00017400
		public BehaviorAssaultWalls(Formation formation) : base(formation)
		{
			base.BehaviorCoherence = 0f;
			this._behaviorSide = formation.AI.Side;
			this._teamAISiegeComponent = (TeamAISiegeComponent)formation.Team.TeamAI;
			this._behaviorState = BehaviorAssaultWalls.BehaviorState.Deciding;
			this.ResetOrderPositions();
			base.CurrentOrder = this._stopOrder;
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x00019260 File Offset: 0x00017460
		public override TextObject GetBehaviorString()
		{
			TextObject behaviorString = base.GetBehaviorString();
			TextObject variable = GameTexts.FindText("str_formation_ai_side_strings", base.Formation.AI.Side.ToString());
			behaviorString.SetTextVariable("SIDE_STRING", variable);
			behaviorString.SetTextVariable("IS_GENERAL_SIDE", "0");
			return behaviorString;
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x000192BC File Offset: 0x000174BC
		private BehaviorAssaultWalls.BehaviorState CheckAndChangeState()
		{
			switch (this._behaviorState)
			{
			case BehaviorAssaultWalls.BehaviorState.Deciding:
				if (!this._isGateLane && this._wallSegment == null)
				{
					return BehaviorAssaultWalls.BehaviorState.Charging;
				}
				if (!this._isGateLane)
				{
					return BehaviorAssaultWalls.BehaviorState.ClimbWall;
				}
				if (this._teamAISiegeComponent.OuterGate.IsGateOpen && this._teamAISiegeComponent.InnerGate.IsGateOpen)
				{
					return BehaviorAssaultWalls.BehaviorState.Charging;
				}
				return BehaviorAssaultWalls.BehaviorState.AttackEntity;
			case BehaviorAssaultWalls.BehaviorState.ClimbWall:
			{
				if (this._wallSegment == null)
				{
					return BehaviorAssaultWalls.BehaviorState.Charging;
				}
				bool flag = false;
				if (this._behaviorSide < FormationAI.BehaviorSide.BehaviorSideNotSet)
				{
					SiegeLane siegeLane = TeamAISiegeComponent.SiegeLanes[(int)this._behaviorSide];
					flag = (siegeLane.IsUnderAttack() && !siegeLane.IsDefended());
				}
				flag = (flag || base.Formation.QuerySystem.MedianPosition.GetNavMeshVec3().DistanceSquared(this._wallSegment.MiddleFrame.Origin.GetNavMeshVec3()) < base.Formation.Depth * base.Formation.Depth);
				if (flag)
				{
					return BehaviorAssaultWalls.BehaviorState.TakeControl;
				}
				return BehaviorAssaultWalls.BehaviorState.ClimbWall;
			}
			case BehaviorAssaultWalls.BehaviorState.AttackEntity:
				if (this._teamAISiegeComponent.OuterGate.IsGateOpen && this._teamAISiegeComponent.InnerGate.IsGateOpen)
				{
					return BehaviorAssaultWalls.BehaviorState.Charging;
				}
				return BehaviorAssaultWalls.BehaviorState.AttackEntity;
			case BehaviorAssaultWalls.BehaviorState.TakeControl:
				if (base.Formation.QuerySystem.ClosestEnemyFormation == null)
				{
					return BehaviorAssaultWalls.BehaviorState.Deciding;
				}
				if (TeamAISiegeComponent.SiegeLanes.FirstOrDefault((SiegeLane sl) => sl.LaneSide == this._behaviorSide).IsDefended())
				{
					return BehaviorAssaultWalls.BehaviorState.TakeControl;
				}
				if (!this._teamAISiegeComponent.OuterGate.IsGateOpen || !this._teamAISiegeComponent.InnerGate.IsGateOpen)
				{
					return BehaviorAssaultWalls.BehaviorState.MoveToGate;
				}
				return BehaviorAssaultWalls.BehaviorState.Charging;
			case BehaviorAssaultWalls.BehaviorState.MoveToGate:
				if (this._teamAISiegeComponent.OuterGate.IsGateOpen && this._teamAISiegeComponent.InnerGate.IsGateOpen)
				{
					return BehaviorAssaultWalls.BehaviorState.Charging;
				}
				return BehaviorAssaultWalls.BehaviorState.MoveToGate;
			case BehaviorAssaultWalls.BehaviorState.Charging:
				if ((!this._isGateLane || !this._teamAISiegeComponent.OuterGate.IsGateOpen || !this._teamAISiegeComponent.InnerGate.IsGateOpen) && this._behaviorSide < FormationAI.BehaviorSide.BehaviorSideNotSet)
				{
					if (!TeamAISiegeComponent.SiegeLanes[(int)this._behaviorSide].IsOpen && !TeamAISiegeComponent.IsFormationInsideCastle(base.Formation, true, 0.4f))
					{
						return BehaviorAssaultWalls.BehaviorState.Deciding;
					}
					if (base.Formation.QuerySystem.ClosestEnemyFormation == null)
					{
						return BehaviorAssaultWalls.BehaviorState.Stop;
					}
				}
				return BehaviorAssaultWalls.BehaviorState.Charging;
			default:
				if (base.Formation.QuerySystem.ClosestEnemyFormation != null)
				{
					return BehaviorAssaultWalls.BehaviorState.Deciding;
				}
				return BehaviorAssaultWalls.BehaviorState.Stop;
			}
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x0001950C File Offset: 0x0001770C
		protected override void CalculateCurrentOrder()
		{
			switch (this._behaviorState)
			{
			case BehaviorAssaultWalls.BehaviorState.Deciding:
				base.CurrentOrder = this._stopOrder;
				return;
			case BehaviorAssaultWalls.BehaviorState.ClimbWall:
			{
				base.CurrentOrder = this._wallSegmentMoveOrder;
				WorldFrame middleFrame = this._wallSegment.MiddleFrame;
				this.CurrentFacingOrder = FacingOrder.FacingOrderLookAtDirection(-middleFrame.Rotation.f.AsVec2.Normalized());
				this.CurrentArrangementOrder = ArrangementOrder.ArrangementOrderLine;
				return;
			}
			case BehaviorAssaultWalls.BehaviorState.AttackEntity:
				if (!this._teamAISiegeComponent.OuterGate.IsGateOpen)
				{
					base.CurrentOrder = this._attackEntityOrderOuterGate;
				}
				else
				{
					base.CurrentOrder = this._attackEntityOrderInnerGate;
				}
				this.CurrentFacingOrder = FacingOrder.FacingOrderLookAtEnemy;
				this.CurrentArrangementOrder = ArrangementOrder.ArrangementOrderLine;
				return;
			case BehaviorAssaultWalls.BehaviorState.TakeControl:
			{
				if (base.Formation.QuerySystem.ClosestEnemyFormation != null)
				{
					base.CurrentOrder = MovementOrder.MovementOrderChargeToTarget(base.Formation.QuerySystem.ClosestEnemyFormation.Formation);
				}
				else
				{
					base.CurrentOrder = MovementOrder.MovementOrderCharge;
				}
				WorldFrame middleFrame = this._wallSegment.MiddleFrame;
				this.CurrentFacingOrder = FacingOrder.FacingOrderLookAtDirection(-middleFrame.Rotation.f.AsVec2.Normalized());
				this.CurrentArrangementOrder = ArrangementOrder.ArrangementOrderLine;
				return;
			}
			case BehaviorAssaultWalls.BehaviorState.MoveToGate:
			{
				base.CurrentOrder = this._castleGateMoveOrder;
				WorldFrame middleFrame = this._innerGate.MiddleFrame;
				this.CurrentFacingOrder = FacingOrder.FacingOrderLookAtDirection(-middleFrame.Rotation.f.AsVec2.Normalized());
				this.CurrentArrangementOrder = ArrangementOrder.ArrangementOrderLine;
				return;
			}
			case BehaviorAssaultWalls.BehaviorState.Charging:
				base.CurrentOrder = this._chargeOrder;
				this.CurrentFacingOrder = FacingOrder.FacingOrderLookAtEnemy;
				this.CurrentArrangementOrder = ArrangementOrder.ArrangementOrderLoose;
				return;
			case BehaviorAssaultWalls.BehaviorState.Stop:
				base.CurrentOrder = this._stopOrder;
				return;
			default:
				return;
			}
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x000196DC File Offset: 0x000178DC
		public override void OnValidBehaviorSideChanged()
		{
			base.OnValidBehaviorSideChanged();
			this.ResetOrderPositions();
			this._behaviorState = BehaviorAssaultWalls.BehaviorState.Deciding;
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x000196F4 File Offset: 0x000178F4
		public override void TickOccasionally()
		{
			BehaviorAssaultWalls.BehaviorState behaviorState = this.CheckAndChangeState();
			this._behaviorState = behaviorState;
			this.CalculateCurrentOrder();
			foreach (IPrimarySiegeWeapon primarySiegeWeapon in this._primarySiegeWeapons)
			{
				UsableMachine usableMachine = primarySiegeWeapon as UsableMachine;
				if (!usableMachine.IsDeactivated && !primarySiegeWeapon.HasCompletedAction() && !usableMachine.IsUsedByFormation(base.Formation))
				{
					base.Formation.StartUsingMachine(primarySiegeWeapon as UsableMachine, false);
				}
			}
			if (this._behaviorState == BehaviorAssaultWalls.BehaviorState.MoveToGate)
			{
				CastleGate castleGate = this._teamAISiegeComponent.InnerGate;
				if (castleGate != null && !castleGate.IsGateOpen && !castleGate.IsDestroyed)
				{
					if (!castleGate.IsUsedByFormation(base.Formation))
					{
						base.Formation.StartUsingMachine(castleGate, false);
					}
				}
				else
				{
					castleGate = this._teamAISiegeComponent.OuterGate;
					if (castleGate != null && !castleGate.IsGateOpen && !castleGate.IsDestroyed && !castleGate.IsUsedByFormation(base.Formation))
					{
						base.Formation.StartUsingMachine(castleGate, false);
					}
				}
			}
			else
			{
				if (base.Formation.Detachments.Contains(this._teamAISiegeComponent.OuterGate))
				{
					base.Formation.StopUsingMachine(this._teamAISiegeComponent.OuterGate, false);
				}
				if (base.Formation.Detachments.Contains(this._teamAISiegeComponent.InnerGate))
				{
					base.Formation.StopUsingMachine(this._teamAISiegeComponent.InnerGate, false);
				}
			}
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.FacingOrder = this.CurrentFacingOrder;
			base.Formation.ArrangementOrder = this.CurrentArrangementOrder;
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x000198C8 File Offset: 0x00017AC8
		protected override void OnBehaviorActivatedAux()
		{
			this.CalculateCurrentOrder();
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.ArrangementOrder = ArrangementOrder.ArrangementOrderLine;
			base.Formation.FacingOrder = this.CurrentFacingOrder;
			base.Formation.FiringOrder = FiringOrder.FiringOrderHoldYourFire;
			base.Formation.FormOrder = FormOrder.FormOrderDeep;
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000CFD RID: 3325 RVA: 0x0001992D File Offset: 0x00017B2D
		public override float NavmeshlessTargetPositionPenalty
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x00019934 File Offset: 0x00017B34
		protected override float GetAiWeight()
		{
			float result = 0f;
			if (this._teamAISiegeComponent != null)
			{
				if (this._primarySiegeWeapons.Any((IPrimarySiegeWeapon psw) => psw.HasCompletedAction()) || this._wallSegment != null)
				{
					if (this._teamAISiegeComponent.IsCastleBreached())
					{
						result = 0.75f;
					}
					else
					{
						result = 0.25f;
					}
				}
				else if (this._teamAISiegeComponent.OuterGate.DefenseSide == this._behaviorSide)
				{
					result = 0.1f;
				}
			}
			return result;
		}

		// Token: 0x040002F1 RID: 753
		private BehaviorAssaultWalls.BehaviorState _behaviorState;

		// Token: 0x040002F2 RID: 754
		private List<IPrimarySiegeWeapon> _primarySiegeWeapons;

		// Token: 0x040002F3 RID: 755
		private WallSegment _wallSegment;

		// Token: 0x040002F4 RID: 756
		private CastleGate _innerGate;

		// Token: 0x040002F5 RID: 757
		private TeamAISiegeComponent _teamAISiegeComponent;

		// Token: 0x040002F6 RID: 758
		private MovementOrder _attackEntityOrderInnerGate;

		// Token: 0x040002F7 RID: 759
		private MovementOrder _attackEntityOrderOuterGate;

		// Token: 0x040002F8 RID: 760
		private MovementOrder _chargeOrder;

		// Token: 0x040002F9 RID: 761
		private MovementOrder _stopOrder;

		// Token: 0x040002FA RID: 762
		private MovementOrder _castleGateMoveOrder;

		// Token: 0x040002FB RID: 763
		private MovementOrder _wallSegmentMoveOrder;

		// Token: 0x040002FC RID: 764
		private FacingOrder _facingOrder;

		// Token: 0x040002FD RID: 765
		protected ArrangementOrder CurrentArrangementOrder;

		// Token: 0x040002FE RID: 766
		private bool _isGateLane;

		// Token: 0x020003FC RID: 1020
		private enum BehaviorState
		{
			// Token: 0x040017A2 RID: 6050
			Deciding,
			// Token: 0x040017A3 RID: 6051
			ClimbWall,
			// Token: 0x040017A4 RID: 6052
			AttackEntity,
			// Token: 0x040017A5 RID: 6053
			TakeControl,
			// Token: 0x040017A6 RID: 6054
			MoveToGate,
			// Token: 0x040017A7 RID: 6055
			Charging,
			// Token: 0x040017A8 RID: 6056
			Stop
		}
	}
}
