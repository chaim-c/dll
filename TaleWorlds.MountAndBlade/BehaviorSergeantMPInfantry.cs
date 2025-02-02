using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Objects;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000126 RID: 294
	public class BehaviorSergeantMPInfantry : BehaviorComponent
	{
		// Token: 0x06000DDF RID: 3551 RVA: 0x00021D38 File Offset: 0x0001FF38
		public BehaviorSergeantMPInfantry(Formation formation) : base(formation)
		{
			this._behaviorState = BehaviorSergeantMPInfantry.BehaviorState.Unset;
			this._flagpositions = base.Formation.Team.Mission.ActiveMissionObjects.FindAllWithType<FlagCapturePoint>().ToList<FlagCapturePoint>();
			this._flagDominationGameMode = base.Formation.Team.Mission.GetMissionBehavior<MissionMultiplayerFlagDomination>();
			this.CalculateCurrentOrder();
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x00021D9C File Offset: 0x0001FF9C
		protected override void CalculateCurrentOrder()
		{
			BehaviorSergeantMPInfantry.BehaviorState behaviorState;
			if (base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation != null && ((base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.IsRangedFormation && base.Formation.QuerySystem.AveragePosition.DistanceSquared(base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.MedianPosition.AsVec2) <= ((this._behaviorState == BehaviorSergeantMPInfantry.BehaviorState.Attacking) ? 3600f : 2500f)) || (base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.IsInfantryFormation && base.Formation.QuerySystem.AveragePosition.DistanceSquared(base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.MedianPosition.AsVec2) <= ((this._behaviorState == BehaviorSergeantMPInfantry.BehaviorState.Attacking) ? 900f : 400f))))
			{
				behaviorState = BehaviorSergeantMPInfantry.BehaviorState.Attacking;
			}
			else
			{
				behaviorState = BehaviorSergeantMPInfantry.BehaviorState.GoingToFlag;
			}
			if (behaviorState == BehaviorSergeantMPInfantry.BehaviorState.Attacking && (this._behaviorState != BehaviorSergeantMPInfantry.BehaviorState.Attacking || base.CurrentOrder.OrderEnum != MovementOrder.MovementOrderEnum.ChargeToTarget || base.CurrentOrder.TargetFormation.QuerySystem != base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation))
			{
				this._behaviorState = BehaviorSergeantMPInfantry.BehaviorState.Attacking;
				base.CurrentOrder = MovementOrder.MovementOrderChargeToTarget(base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.Formation);
			}
			if (behaviorState == BehaviorSergeantMPInfantry.BehaviorState.GoingToFlag)
			{
				this._behaviorState = behaviorState;
				WorldPosition medianPosition;
				if (this._flagpositions.Any((FlagCapturePoint fp) => this._flagDominationGameMode.GetFlagOwnerTeam(fp) != base.Formation.Team))
				{
					medianPosition = new WorldPosition(base.Formation.Team.Mission.Scene, UIntPtr.Zero, (from fp in this._flagpositions
					where this._flagDominationGameMode.GetFlagOwnerTeam(fp) != base.Formation.Team
					select fp).MinBy((FlagCapturePoint fp) => fp.Position.AsVec2.DistanceSquared(base.Formation.QuerySystem.AveragePosition)).Position, false);
				}
				else if (this._flagpositions.Any((FlagCapturePoint fp) => this._flagDominationGameMode.GetFlagOwnerTeam(fp) == base.Formation.Team))
				{
					medianPosition = new WorldPosition(base.Formation.Team.Mission.Scene, UIntPtr.Zero, (from fp in this._flagpositions
					where this._flagDominationGameMode.GetFlagOwnerTeam(fp) == base.Formation.Team
					select fp).MinBy((FlagCapturePoint fp) => fp.Position.AsVec2.DistanceSquared(base.Formation.QuerySystem.AveragePosition)).Position, false);
				}
				else
				{
					medianPosition = base.Formation.QuerySystem.MedianPosition;
					medianPosition.SetVec2(base.Formation.QuerySystem.AveragePosition);
				}
				if (base.CurrentOrder.OrderEnum == MovementOrder.MovementOrderEnum.Invalid || base.CurrentOrder.GetPosition(base.Formation) != medianPosition.AsVec2)
				{
					Vec2 direction;
					if (base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation == null)
					{
						direction = base.Formation.Direction;
					}
					else
					{
						direction = (base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.MedianPosition.AsVec2 - base.Formation.QuerySystem.AveragePosition).Normalized();
					}
					base.CurrentOrder = MovementOrder.MovementOrderMove(medianPosition);
					this.CurrentFacingOrder = FacingOrder.FacingOrderLookAtDirection(direction);
				}
			}
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x000220A4 File Offset: 0x000202A4
		public override void TickOccasionally()
		{
			this._flagpositions.RemoveAll((FlagCapturePoint fp) => fp.IsDeactivated);
			this.CalculateCurrentOrder();
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.FacingOrder = this.CurrentFacingOrder;
			if (base.Formation.QuerySystem.HasShield && (this._behaviorState == BehaviorSergeantMPInfantry.BehaviorState.Attacking || (this._behaviorState == BehaviorSergeantMPInfantry.BehaviorState.GoingToFlag && base.CurrentOrder.GetPosition(base.Formation).IsValid && base.Formation.QuerySystem.AveragePosition.DistanceSquared(base.CurrentOrder.GetPosition(base.Formation)) <= 225f)))
			{
				base.Formation.ArrangementOrder = ArrangementOrder.ArrangementOrderShieldWall;
				return;
			}
			base.Formation.ArrangementOrder = ArrangementOrder.ArrangementOrderLine;
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x0002219C File Offset: 0x0002039C
		protected override void OnBehaviorActivatedAux()
		{
			this.CalculateCurrentOrder();
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.FacingOrder = this.CurrentFacingOrder;
			base.Formation.ArrangementOrder = ArrangementOrder.ArrangementOrderLine;
			base.Formation.FiringOrder = FiringOrder.FiringOrderFireAtWill;
			base.Formation.FormOrder = FormOrder.FormOrderDeep;
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x00022201 File Offset: 0x00020401
		protected override float GetAiWeight()
		{
			if (base.Formation.QuerySystem.IsInfantryFormation)
			{
				return 1.2f;
			}
			return 0f;
		}

		// Token: 0x04000358 RID: 856
		private BehaviorSergeantMPInfantry.BehaviorState _behaviorState;

		// Token: 0x04000359 RID: 857
		private List<FlagCapturePoint> _flagpositions;

		// Token: 0x0400035A RID: 858
		private MissionMultiplayerFlagDomination _flagDominationGameMode;

		// Token: 0x02000413 RID: 1043
		private enum BehaviorState
		{
			// Token: 0x040017F2 RID: 6130
			GoingToFlag,
			// Token: 0x040017F3 RID: 6131
			Attacking,
			// Token: 0x040017F4 RID: 6132
			Unset
		}
	}
}
