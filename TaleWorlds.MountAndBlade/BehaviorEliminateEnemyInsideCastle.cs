using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000114 RID: 276
	public class BehaviorEliminateEnemyInsideCastle : BehaviorComponent
	{
		// Token: 0x06000D6A RID: 3434 RVA: 0x0001D55C File Offset: 0x0001B75C
		public BehaviorEliminateEnemyInsideCastle(Formation formation) : base(formation)
		{
			this._behaviorState = BehaviorEliminateEnemyInsideCastle.BehaviorState.UnSet;
			this._behaviorSide = formation.AI.Side;
			this.ResetOrderPositions();
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x0001D583 File Offset: 0x0001B783
		protected override void CalculateCurrentOrder()
		{
			base.CalculateCurrentOrder();
			base.CurrentOrder = ((this._behaviorState == BehaviorEliminateEnemyInsideCastle.BehaviorState.Attacking) ? this._attackOrder : this._gatherOrder);
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x0001D5A8 File Offset: 0x0001B7A8
		private void DetermineMostImportantInvadingEnemyFormation()
		{
			float num = float.MinValue;
			this._targetEnemyFormation = null;
			foreach (Team team in base.Formation.Team.Mission.Teams)
			{
				if (team.IsEnemyOf(base.Formation.Team))
				{
					for (int i = 0; i < Math.Min(team.FormationsIncludingSpecialAndEmpty.Count, 8); i++)
					{
						Formation formation = team.FormationsIncludingSpecialAndEmpty[i];
						if (formation.CountOfUnits > 0 && TeamAISiegeComponent.IsFormationInsideCastle(formation, true, 0.4f))
						{
							float formationPower = formation.QuerySystem.FormationPower;
							if (formationPower > num)
							{
								num = formationPower;
								this._targetEnemyFormation = formation;
							}
						}
					}
				}
			}
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x0001D688 File Offset: 0x0001B888
		private void ConfirmGatheringSide()
		{
			SiegeLane siegeLane = TeamAISiegeComponent.SiegeLanes.FirstOrDefault((SiegeLane sl) => sl.LaneSide == this._behaviorSide);
			if (siegeLane == null || siegeLane.LaneState >= SiegeLane.LaneStateEnum.Conceited)
			{
				this.ResetOrderPositions();
			}
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x0001D6C0 File Offset: 0x0001B8C0
		private FormationAI.BehaviorSide DetermineGatheringSide()
		{
			this.DetermineMostImportantInvadingEnemyFormation();
			if (this._targetEnemyFormation == null)
			{
				if (this._behaviorState == BehaviorEliminateEnemyInsideCastle.BehaviorState.Attacking)
				{
					this._behaviorState = BehaviorEliminateEnemyInsideCastle.BehaviorState.UnSet;
				}
				return this._behaviorSide;
			}
			int connectedSides = TeamAISiegeComponent.QuerySystem.DeterminePositionAssociatedSide(this._targetEnemyFormation.QuerySystem.MedianPosition.GetNavMeshVec3());
			IEnumerable<SiegeLane> source = from sl in TeamAISiegeComponent.SiegeLanes
			where sl.LaneState != SiegeLane.LaneStateEnum.Conceited && !SiegeQuerySystem.AreSidesRelated(sl.LaneSide, connectedSides)
			select sl;
			FormationAI.BehaviorSide result = this._behaviorSide;
			if (source.Any<SiegeLane>())
			{
				if (source.Count<SiegeLane>() > 1)
				{
					int leastDangerousLaneState = source.Min((SiegeLane pgl) => (int)pgl.LaneState);
					IEnumerable<SiegeLane> source2 = from pgl in source
					where pgl.LaneState == (SiegeLane.LaneStateEnum)leastDangerousLaneState
					select pgl;
					result = ((source2.Count<SiegeLane>() > 1) ? source2.MinBy((SiegeLane ldl) => SiegeQuerySystem.SideDistance(1 << connectedSides, 1 << (int)ldl.LaneSide)).LaneSide : source2.First<SiegeLane>().LaneSide);
				}
				else
				{
					result = source.First<SiegeLane>().LaneSide;
				}
			}
			return result;
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x0001D7DC File Offset: 0x0001B9DC
		private void ResetOrderPositions()
		{
			this._behaviorSide = this.DetermineGatheringSide();
			SiegeLane siegeLane = TeamAISiegeComponent.SiegeLanes.FirstOrDefault((SiegeLane sl) => sl.LaneSide == this._behaviorSide);
			WorldFrame? worldFrame;
			if (siegeLane == null)
			{
				worldFrame = null;
			}
			else
			{
				List<ICastleKeyPosition> defensePoints = siegeLane.DefensePoints;
				if (defensePoints == null)
				{
					worldFrame = null;
				}
				else
				{
					ICastleKeyPosition castleKeyPosition = defensePoints.FirstOrDefault((ICastleKeyPosition dp) => dp.AttackerSiegeWeapon is UsableMachine && !(dp.AttackerSiegeWeapon as UsableMachine).IsDisabled);
					worldFrame = ((castleKeyPosition != null) ? new WorldFrame?(castleKeyPosition.DefenseWaitFrame) : null);
				}
			}
			WorldFrame worldFrame2 = worldFrame ?? WorldFrame.Invalid;
			object obj;
			if (siegeLane == null)
			{
				obj = null;
			}
			else
			{
				List<ICastleKeyPosition> defensePoints2 = siegeLane.DefensePoints;
				if (defensePoints2 == null)
				{
					obj = null;
				}
				else
				{
					ICastleKeyPosition castleKeyPosition2 = defensePoints2.FirstOrDefault((ICastleKeyPosition dp) => dp.AttackerSiegeWeapon is UsableMachine && !(dp.AttackerSiegeWeapon as UsableMachine).IsDisabled);
					obj = ((castleKeyPosition2 != null) ? castleKeyPosition2.WaitPosition : null);
				}
			}
			object gatheringTacticalPos;
			if ((gatheringTacticalPos = obj) == null)
			{
				if (siegeLane == null)
				{
					gatheringTacticalPos = null;
				}
				else
				{
					List<ICastleKeyPosition> defensePoints3 = siegeLane.DefensePoints;
					gatheringTacticalPos = ((defensePoints3 != null) ? defensePoints3.FirstOrDefault<ICastleKeyPosition>().WaitPosition : null);
				}
			}
			this._gatheringTacticalPos = gatheringTacticalPos;
			if (this._gatheringTacticalPos != null)
			{
				this._gatherOrder = MovementOrder.MovementOrderMove(this._gatheringTacticalPos.Position);
				this._gatheringFacingOrder = FacingOrder.FacingOrderLookAtEnemy;
			}
			else if (worldFrame2.Origin.IsValid)
			{
				worldFrame2.Rotation.f.Normalize();
				this._gatherOrder = MovementOrder.MovementOrderMove(worldFrame2.Origin);
				this._gatheringFacingOrder = FacingOrder.FacingOrderLookAtEnemy;
			}
			else
			{
				this._gatherOrder = MovementOrder.MovementOrderMove(base.Formation.QuerySystem.MedianPosition);
				this._gatheringFacingOrder = FacingOrder.FacingOrderLookAtEnemy;
			}
			this._attackOrder = MovementOrder.MovementOrderChargeToTarget(this._targetEnemyFormation);
			this._attackFacingOrder = FacingOrder.FacingOrderLookAtEnemy;
			base.CurrentOrder = ((this._behaviorState == BehaviorEliminateEnemyInsideCastle.BehaviorState.Attacking) ? this._attackOrder : this._gatherOrder);
			this.CurrentFacingOrder = ((this._behaviorState == BehaviorEliminateEnemyInsideCastle.BehaviorState.Attacking) ? this._attackFacingOrder : this._gatheringFacingOrder);
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x0001D9D4 File Offset: 0x0001BBD4
		public override void OnValidBehaviorSideChanged()
		{
			base.OnValidBehaviorSideChanged();
			this.ResetOrderPositions();
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x0001D9E4 File Offset: 0x0001BBE4
		public override void TickOccasionally()
		{
			base.TickOccasionally();
			if (this._behaviorState != BehaviorEliminateEnemyInsideCastle.BehaviorState.Attacking)
			{
				this.ConfirmGatheringSide();
			}
			bool flag;
			if (this._behaviorState == BehaviorEliminateEnemyInsideCastle.BehaviorState.Attacking)
			{
				flag = (this._targetEnemyFormation != null);
			}
			else
			{
				flag = (this._targetEnemyFormation != null && (base.Formation.QuerySystem.MedianPosition.GetNavMeshVec3().DistanceSquared(this._gatherOrder.CreateNewOrderWorldPosition(base.Formation, WorldPosition.WorldPositionEnforcedCache.NavMeshVec3).GetNavMeshVec3()) < 100f || base.Formation.QuerySystem.FormationIntegrityData.DeviationOfPositionsExcludeFarAgents / ((base.Formation.QuerySystem.IdealAverageDisplacement != 0f) ? base.Formation.QuerySystem.IdealAverageDisplacement : 1f) <= 3f));
			}
			BehaviorEliminateEnemyInsideCastle.BehaviorState behaviorState = flag ? BehaviorEliminateEnemyInsideCastle.BehaviorState.Attacking : BehaviorEliminateEnemyInsideCastle.BehaviorState.Gathering;
			if (behaviorState != this._behaviorState)
			{
				this._behaviorState = behaviorState;
				base.CurrentOrder = ((this._behaviorState == BehaviorEliminateEnemyInsideCastle.BehaviorState.Attacking) ? this._attackOrder : this._gatherOrder);
				this.CurrentFacingOrder = ((this._behaviorState == BehaviorEliminateEnemyInsideCastle.BehaviorState.Attacking) ? this._attackFacingOrder : this._gatheringFacingOrder);
			}
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.FacingOrder = this.CurrentFacingOrder;
			if (this._behaviorState == BehaviorEliminateEnemyInsideCastle.BehaviorState.Gathering && this._gatheringTacticalPos != null)
			{
				base.Formation.FormOrder = FormOrder.FormOrderCustom(this._gatheringTacticalPos.Width);
			}
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x0001DB60 File Offset: 0x0001BD60
		protected override void OnBehaviorActivatedAux()
		{
			this._behaviorState = BehaviorEliminateEnemyInsideCastle.BehaviorState.UnSet;
			this._behaviorSide = base.Formation.AI.Side;
			this.ResetOrderPositions();
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.FacingOrder = this.CurrentFacingOrder;
			base.Formation.ArrangementOrder = ArrangementOrder.ArrangementOrderLine;
			base.Formation.FiringOrder = FiringOrder.FiringOrderFireAtWill;
			base.Formation.FormOrder = FormOrder.FormOrderWide;
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000D73 RID: 3443 RVA: 0x0001DBE2 File Offset: 0x0001BDE2
		public override float NavmeshlessTargetPositionPenalty
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x0001DBE9 File Offset: 0x0001BDE9
		protected override float GetAiWeight()
		{
			return 1f;
		}

		// Token: 0x04000335 RID: 821
		private BehaviorEliminateEnemyInsideCastle.BehaviorState _behaviorState;

		// Token: 0x04000336 RID: 822
		private MovementOrder _gatherOrder;

		// Token: 0x04000337 RID: 823
		private MovementOrder _attackOrder;

		// Token: 0x04000338 RID: 824
		private FacingOrder _gatheringFacingOrder;

		// Token: 0x04000339 RID: 825
		private FacingOrder _attackFacingOrder;

		// Token: 0x0400033A RID: 826
		private TacticalPosition _gatheringTacticalPos;

		// Token: 0x0400033B RID: 827
		private Formation _targetEnemyFormation;

		// Token: 0x02000405 RID: 1029
		private enum BehaviorState
		{
			// Token: 0x040017C5 RID: 6085
			UnSet,
			// Token: 0x040017C6 RID: 6086
			Gathering,
			// Token: 0x040017C7 RID: 6087
			Attacking
		}
	}
}
