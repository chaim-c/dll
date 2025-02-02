using System;
using System.Linq;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000124 RID: 292
	public class BehaviorSallyOut : BehaviorComponent
	{
		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000DD0 RID: 3536 RVA: 0x0002114C File Offset: 0x0001F34C
		private bool _calculateAreGatesOutsideOpen
		{
			get
			{
				return (this._teamAISiegeDefender.OuterGate == null || this._teamAISiegeDefender.OuterGate.IsGateOpen) && (this._teamAISiegeDefender.InnerGate == null || this._teamAISiegeDefender.InnerGate.IsGateOpen);
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000DD1 RID: 3537 RVA: 0x00021199 File Offset: 0x0001F399
		private bool _calculateShouldStartAttacking
		{
			get
			{
				return this._calculateAreGatesOutsideOpen || !TeamAISiegeComponent.IsFormationInsideCastle(base.Formation, true, 0.4f);
			}
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x000211B9 File Offset: 0x0001F3B9
		public BehaviorSallyOut(Formation formation) : base(formation)
		{
			this._teamAISiegeDefender = (formation.Team.TeamAI as TeamAISiegeDefender);
			this._behaviorSide = formation.AI.Side;
			this.ResetOrderPositions();
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x000211EF File Offset: 0x0001F3EF
		protected override void CalculateCurrentOrder()
		{
			base.CalculateCurrentOrder();
			base.CurrentOrder = (this._calculateShouldStartAttacking ? this._attackOrder : this._gatherOrder);
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x00021214 File Offset: 0x0001F414
		private void ResetOrderPositions()
		{
			SiegeLane siegeLane = TeamAISiegeComponent.SiegeLanes.FirstOrDefault((SiegeLane sl) => sl.LaneSide == FormationAI.BehaviorSide.Middle);
			WorldFrame? worldFrame;
			if (siegeLane == null)
			{
				worldFrame = null;
			}
			else
			{
				ICastleKeyPosition castleKeyPosition = siegeLane.DefensePoints.FirstOrDefault((ICastleKeyPosition dp) => dp.AttackerSiegeWeapon is UsableMachine && !(dp.AttackerSiegeWeapon as UsableMachine).IsDisabled);
				worldFrame = ((castleKeyPosition != null) ? new WorldFrame?(castleKeyPosition.DefenseWaitFrame) : null);
			}
			WorldFrame worldFrame2 = worldFrame ?? WorldFrame.Invalid;
			TacticalPosition gatheringTacticalPos;
			if (siegeLane == null)
			{
				gatheringTacticalPos = null;
			}
			else
			{
				ICastleKeyPosition castleKeyPosition2 = siegeLane.DefensePoints.FirstOrDefault((ICastleKeyPosition dp) => dp.AttackerSiegeWeapon is UsableMachine && !(dp.AttackerSiegeWeapon as UsableMachine).IsDisabled);
				gatheringTacticalPos = ((castleKeyPosition2 != null) ? castleKeyPosition2.WaitPosition : null);
			}
			this._gatheringTacticalPos = gatheringTacticalPos;
			if (this._gatheringTacticalPos != null)
			{
				this._gatherOrder = MovementOrder.MovementOrderMove(this._gatheringTacticalPos.Position);
			}
			else if (worldFrame2.Origin.IsValid)
			{
				worldFrame2.Rotation.f.Normalize();
				this._gatherOrder = MovementOrder.MovementOrderMove(worldFrame2.Origin);
			}
			else
			{
				this._gatherOrder = MovementOrder.MovementOrderMove(base.Formation.QuerySystem.MedianPosition);
			}
			this._attackOrder = MovementOrder.MovementOrderCharge;
			base.CurrentOrder = (this._calculateShouldStartAttacking ? this._attackOrder : this._gatherOrder);
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x0002138C File Offset: 0x0001F58C
		public override void TickOccasionally()
		{
			base.TickOccasionally();
			this.CalculateCurrentOrder();
			base.Formation.SetMovementOrder(base.CurrentOrder);
			if (!this._calculateAreGatesOutsideOpen)
			{
				CastleGate castleGate = (this._teamAISiegeDefender.InnerGate != null && !this._teamAISiegeDefender.InnerGate.IsGateOpen) ? this._teamAISiegeDefender.InnerGate : this._teamAISiegeDefender.OuterGate;
				if (!castleGate.IsUsedByFormation(base.Formation))
				{
					base.Formation.StartUsingMachine(castleGate, false);
				}
			}
		}

		// Token: 0x06000DD6 RID: 3542 RVA: 0x00021414 File Offset: 0x0001F614
		protected override void OnBehaviorActivatedAux()
		{
			this._behaviorSide = base.Formation.AI.Side;
			this.ResetOrderPositions();
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.FacingOrder = FacingOrder.FacingOrderLookAtEnemy;
			base.Formation.ArrangementOrder = ArrangementOrder.ArrangementOrderLine;
			base.Formation.FiringOrder = FiringOrder.FiringOrderFireAtWill;
			base.Formation.FormOrder = FormOrder.FormOrderWide;
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000DD7 RID: 3543 RVA: 0x0002148E File Offset: 0x0001F68E
		public override float NavmeshlessTargetPositionPenalty
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x00021495 File Offset: 0x0001F695
		protected override float GetAiWeight()
		{
			return 10f;
		}

		// Token: 0x04000352 RID: 850
		private readonly TeamAISiegeDefender _teamAISiegeDefender;

		// Token: 0x04000353 RID: 851
		private MovementOrder _gatherOrder;

		// Token: 0x04000354 RID: 852
		private MovementOrder _attackOrder;

		// Token: 0x04000355 RID: 853
		private TacticalPosition _gatheringTacticalPos;
	}
}
