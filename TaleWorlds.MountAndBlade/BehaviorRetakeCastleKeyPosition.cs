using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000120 RID: 288
	public class BehaviorRetakeCastleKeyPosition : BehaviorComponent
	{
		// Token: 0x06000DB3 RID: 3507 RVA: 0x000209D5 File Offset: 0x0001EBD5
		public BehaviorRetakeCastleKeyPosition(Formation formation) : base(formation)
		{
			this._behaviorState = BehaviorRetakeCastleKeyPosition.BehaviorState.UnSet;
			this._behaviorSide = formation.AI.Side;
			this.ResetOrderPositions();
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x000209FC File Offset: 0x0001EBFC
		protected override void CalculateCurrentOrder()
		{
			base.CalculateCurrentOrder();
			base.CurrentOrder = ((this._behaviorState == BehaviorRetakeCastleKeyPosition.BehaviorState.Attacking) ? this._attackOrder : this._gatherOrder);
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x00020A24 File Offset: 0x0001EC24
		private FormationAI.BehaviorSide DetermineGatheringSide()
		{
			IEnumerable<SiegeLane> source = from sl in TeamAISiegeComponent.SiegeLanes
			where sl.LaneSide != this._behaviorSide && sl.LaneState != SiegeLane.LaneStateEnum.Conceited && sl.DefenderOrigin.IsValid
			select sl;
			if (source.Any<SiegeLane>())
			{
				int nearestSafeSideDistance = source.Min((SiegeLane pgl) => SiegeQuerySystem.SideDistance(1 << (int)this._behaviorSide, 1 << (int)pgl.LaneSide));
				return (from pgl in source
				where SiegeQuerySystem.SideDistance(1 << (int)this._behaviorSide, 1 << (int)pgl.LaneSide) == nearestSafeSideDistance
				select pgl).MinBy((SiegeLane pgl) => pgl.DefenderOrigin.GetGroundVec3().DistanceSquared(base.Formation.QuerySystem.MedianPosition.GetGroundVec3())).LaneSide;
			}
			return this._behaviorSide;
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x00020AA4 File Offset: 0x0001ECA4
		private void ConfirmGatheringSide()
		{
			SiegeLane siegeLane = TeamAISiegeComponent.SiegeLanes.FirstOrDefault((SiegeLane sl) => sl.LaneSide == this._gatheringSide);
			if (siegeLane == null || siegeLane.LaneState >= SiegeLane.LaneStateEnum.Conceited)
			{
				this.ResetOrderPositions();
			}
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x00020ADC File Offset: 0x0001ECDC
		private void ResetOrderPositions()
		{
			this._behaviorState = BehaviorRetakeCastleKeyPosition.BehaviorState.UnSet;
			this._gatheringSide = this.DetermineGatheringSide();
			SiegeLane siegeLane = TeamAISiegeComponent.SiegeLanes.FirstOrDefault((SiegeLane sl) => sl.LaneSide == this._gatheringSide);
			ICastleKeyPosition castleKeyPosition = siegeLane.DefensePoints.FirstOrDefault((ICastleKeyPosition dp) => dp.AttackerSiegeWeapon is UsableMachine && !(dp.AttackerSiegeWeapon as UsableMachine).IsDisabled);
			WorldFrame worldFrame = (castleKeyPosition != null) ? castleKeyPosition.DefenseWaitFrame : siegeLane.DefensePoints.FirstOrDefault<ICastleKeyPosition>().DefenseWaitFrame;
			ICastleKeyPosition castleKeyPosition2 = siegeLane.DefensePoints.FirstOrDefault((ICastleKeyPosition dp) => dp.AttackerSiegeWeapon is UsableMachine && !(dp.AttackerSiegeWeapon as UsableMachine).IsDisabled);
			this._gatheringTacticalPos = ((castleKeyPosition2 != null) ? castleKeyPosition2.WaitPosition : null);
			if (this._gatheringTacticalPos != null)
			{
				this._gatherOrder = MovementOrder.MovementOrderMove(this._gatheringTacticalPos.Position);
				this._gatheringFacingOrder = FacingOrder.FacingOrderLookAtEnemy;
			}
			else if (worldFrame.Origin.IsValid)
			{
				worldFrame.Rotation.f.Normalize();
				this._gatherOrder = MovementOrder.MovementOrderMove(worldFrame.Origin);
				this._gatheringFacingOrder = FacingOrder.FacingOrderLookAtEnemy;
			}
			else
			{
				this._gatherOrder = MovementOrder.MovementOrderMove(base.Formation.QuerySystem.MedianPosition);
				this._gatheringFacingOrder = FacingOrder.FacingOrderLookAtEnemy;
			}
			SiegeLane siegeLane2 = TeamAISiegeComponent.SiegeLanes.FirstOrDefault((SiegeLane sl) => sl.LaneSide == this._behaviorSide);
			ICastleKeyPosition castleKeyPosition3 = siegeLane2.DefensePoints.FirstOrDefault((ICastleKeyPosition dp) => dp.AttackerSiegeWeapon is UsableMachine && !(dp.AttackerSiegeWeapon as UsableMachine).IsDisabled);
			this._attackOrder = MovementOrder.MovementOrderMove((castleKeyPosition3 != null) ? castleKeyPosition3.MiddleFrame.Origin : siegeLane2.DefensePoints.FirstOrDefault<ICastleKeyPosition>().MiddleFrame.Origin);
			this._attackFacingOrder = FacingOrder.FacingOrderLookAtEnemy;
			base.CurrentOrder = ((this._behaviorState == BehaviorRetakeCastleKeyPosition.BehaviorState.Attacking) ? this._attackOrder : this._gatherOrder);
			this.CurrentFacingOrder = ((this._behaviorState == BehaviorRetakeCastleKeyPosition.BehaviorState.Attacking) ? this._attackFacingOrder : this._gatheringFacingOrder);
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x00020CDA File Offset: 0x0001EEDA
		public override void OnValidBehaviorSideChanged()
		{
			base.OnValidBehaviorSideChanged();
			this.ResetOrderPositions();
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x00020CE8 File Offset: 0x0001EEE8
		public override void TickOccasionally()
		{
			base.TickOccasionally();
			if (this._behaviorState != BehaviorRetakeCastleKeyPosition.BehaviorState.Attacking)
			{
				this.ConfirmGatheringSide();
			}
			bool flag = true;
			if (this._behaviorState != BehaviorRetakeCastleKeyPosition.BehaviorState.Attacking)
			{
				flag = (base.Formation.QuerySystem.MedianPosition.GetNavMeshVec3().DistanceSquared(this._gatherOrder.CreateNewOrderWorldPosition(base.Formation, WorldPosition.WorldPositionEnforcedCache.NavMeshVec3).GetNavMeshVec3()) < 100f || base.Formation.QuerySystem.FormationIntegrityData.DeviationOfPositionsExcludeFarAgents / ((base.Formation.QuerySystem.IdealAverageDisplacement != 0f) ? base.Formation.QuerySystem.IdealAverageDisplacement : 1f) <= 3f);
			}
			BehaviorRetakeCastleKeyPosition.BehaviorState behaviorState = flag ? BehaviorRetakeCastleKeyPosition.BehaviorState.Attacking : BehaviorRetakeCastleKeyPosition.BehaviorState.Gathering;
			if (behaviorState != this._behaviorState)
			{
				this._behaviorState = behaviorState;
				base.CurrentOrder = ((this._behaviorState == BehaviorRetakeCastleKeyPosition.BehaviorState.Attacking) ? this._attackOrder : this._gatherOrder);
				this.CurrentFacingOrder = ((this._behaviorState == BehaviorRetakeCastleKeyPosition.BehaviorState.Attacking) ? this._attackFacingOrder : this._gatheringFacingOrder);
			}
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.FacingOrder = this.CurrentFacingOrder;
			if (this._behaviorState == BehaviorRetakeCastleKeyPosition.BehaviorState.Gathering && this._gatheringTacticalPos != null)
			{
				base.Formation.FormOrder = FormOrder.FormOrderCustom(this._gatheringTacticalPos.Width);
			}
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x00020E4C File Offset: 0x0001F04C
		protected override void OnBehaviorActivatedAux()
		{
			this._behaviorState = BehaviorRetakeCastleKeyPosition.BehaviorState.UnSet;
			this._behaviorSide = base.Formation.AI.Side;
			this.ResetOrderPositions();
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.FacingOrder = this.CurrentFacingOrder;
			base.Formation.ArrangementOrder = ArrangementOrder.ArrangementOrderLine;
			base.Formation.FiringOrder = FiringOrder.FiringOrderFireAtWill;
			base.Formation.FormOrder = FormOrder.FormOrderWide;
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000DBB RID: 3515 RVA: 0x00020ECE File Offset: 0x0001F0CE
		public override float NavmeshlessTargetPositionPenalty
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x00020ED5 File Offset: 0x0001F0D5
		protected override float GetAiWeight()
		{
			return 1f;
		}

		// Token: 0x0400034B RID: 843
		private BehaviorRetakeCastleKeyPosition.BehaviorState _behaviorState;

		// Token: 0x0400034C RID: 844
		private MovementOrder _gatherOrder;

		// Token: 0x0400034D RID: 845
		private MovementOrder _attackOrder;

		// Token: 0x0400034E RID: 846
		private FacingOrder _gatheringFacingOrder;

		// Token: 0x0400034F RID: 847
		private FacingOrder _attackFacingOrder;

		// Token: 0x04000350 RID: 848
		private TacticalPosition _gatheringTacticalPos;

		// Token: 0x04000351 RID: 849
		private FormationAI.BehaviorSide _gatheringSide;

		// Token: 0x0200040E RID: 1038
		private enum BehaviorState
		{
			// Token: 0x040017E1 RID: 6113
			UnSet,
			// Token: 0x040017E2 RID: 6114
			Gathering,
			// Token: 0x040017E3 RID: 6115
			Attacking
		}
	}
}
