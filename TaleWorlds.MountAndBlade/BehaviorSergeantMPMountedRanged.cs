using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Objects;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000129 RID: 297
	public class BehaviorSergeantMPMountedRanged : BehaviorComponent
	{
		// Token: 0x06000DFE RID: 3582 RVA: 0x00022A64 File Offset: 0x00020C64
		public BehaviorSergeantMPMountedRanged(Formation formation) : base(formation)
		{
			this._flagpositions = base.Formation.Team.Mission.ActiveMissionObjects.FindAllWithType<FlagCapturePoint>().ToList<FlagCapturePoint>();
			this._flagDominationGameMode = base.Formation.Team.Mission.GetMissionBehavior<MissionMultiplayerFlagDomination>();
			this.CalculateCurrentOrder();
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x00022AC0 File Offset: 0x00020CC0
		private MovementOrder UncapturedFlagMoveOrder()
		{
			if (this._flagpositions.Any((FlagCapturePoint fp) => this._flagDominationGameMode.GetFlagOwnerTeam(fp) != base.Formation.Team))
			{
				FlagCapturePoint flagCapturePoint = (from fp in this._flagpositions
				where this._flagDominationGameMode.GetFlagOwnerTeam(fp) != base.Formation.Team
				select fp).MinBy((FlagCapturePoint fp) => base.Formation.Team.QuerySystem.GetLocalEnemyPower(fp.Position.AsVec2));
				return MovementOrder.MovementOrderMove(new WorldPosition(base.Formation.Team.Mission.Scene, UIntPtr.Zero, flagCapturePoint.Position, false));
			}
			if (this._flagpositions.Any((FlagCapturePoint fp) => this._flagDominationGameMode.GetFlagOwnerTeam(fp) == base.Formation.Team))
			{
				Vec3 position = (from fp in this._flagpositions
				where this._flagDominationGameMode.GetFlagOwnerTeam(fp) == base.Formation.Team
				select fp).MinBy((FlagCapturePoint fp) => fp.Position.AsVec2.DistanceSquared(base.Formation.QuerySystem.AveragePosition)).Position;
				return MovementOrder.MovementOrderMove(new WorldPosition(base.Formation.Team.Mission.Scene, UIntPtr.Zero, position, false));
			}
			return MovementOrder.MovementOrderStop;
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x00022BB0 File Offset: 0x00020DB0
		protected override void CalculateCurrentOrder()
		{
			if (base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation == null || base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.MedianPosition.AsVec2.DistanceSquared(base.Formation.QuerySystem.AveragePosition) > 2500f)
			{
				base.CurrentOrder = this.UncapturedFlagMoveOrder();
				this.CurrentFacingOrder = FacingOrder.FacingOrderLookAtEnemy;
				return;
			}
			FlagCapturePoint flagCapturePoint = null;
			if (this._flagpositions.Any((FlagCapturePoint fp) => this._flagDominationGameMode.GetFlagOwnerTeam(fp) != base.Formation.Team && !fp.IsContested))
			{
				flagCapturePoint = (from fp in this._flagpositions
				where this._flagDominationGameMode.GetFlagOwnerTeam(fp) != base.Formation.Team && !fp.IsContested
				select fp).MinBy((FlagCapturePoint fp) => base.Formation.QuerySystem.AveragePosition.DistanceSquared(fp.Position.AsVec2));
			}
			if (!base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.IsInfantryFormation && flagCapturePoint != null)
			{
				WorldPosition position = new WorldPosition(base.Formation.Team.Mission.Scene, UIntPtr.Zero, flagCapturePoint.Position, false);
				base.CurrentOrder = MovementOrder.MovementOrderMove(position);
				this.CurrentFacingOrder = FacingOrder.FacingOrderLookAtEnemy;
				return;
			}
			if (base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.IsRangedFormation)
			{
				base.CurrentOrder = MovementOrder.MovementOrderChargeToTarget(base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.Formation);
				this.CurrentFacingOrder = FacingOrder.FacingOrderLookAtEnemy;
				return;
			}
			Vec2 vec = base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.MedianPosition.AsVec2 - base.Formation.QuerySystem.AveragePosition;
			float num = vec.Normalize();
			WorldPosition medianPosition = base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.MedianPosition;
			if (num > base.Formation.QuerySystem.MissileRangeAdjusted)
			{
				medianPosition.SetVec2(medianPosition.AsVec2 - vec * (base.Formation.QuerySystem.MissileRangeAdjusted - base.Formation.Depth * 0.5f));
			}
			else if (num < base.Formation.QuerySystem.MissileRangeAdjusted * 0.4f)
			{
				medianPosition.SetVec2(medianPosition.AsVec2 - vec * (base.Formation.QuerySystem.MissileRangeAdjusted * 0.7f));
			}
			else
			{
				vec = vec.RightVec();
				medianPosition.SetVec2(base.Formation.QuerySystem.AveragePosition + vec * 20f);
			}
			base.CurrentOrder = MovementOrder.MovementOrderMove(medianPosition);
			this.CurrentFacingOrder = FacingOrder.FacingOrderLookAtDirection(vec);
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x00022E44 File Offset: 0x00021044
		public override void TickOccasionally()
		{
			this.CalculateCurrentOrder();
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.FacingOrder = this.CurrentFacingOrder;
			if (base.CurrentOrder.OrderEnum == MovementOrder.MovementOrderEnum.ChargeToTarget && base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation != null && base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.IsRangedFormation)
			{
				base.Formation.FiringOrder = FiringOrder.FiringOrderHoldYourFire;
				return;
			}
			base.Formation.FiringOrder = FiringOrder.FiringOrderFireAtWill;
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x00022ED4 File Offset: 0x000210D4
		protected override void OnBehaviorActivatedAux()
		{
			this._flagpositions.RemoveAll((FlagCapturePoint fp) => fp.IsDeactivated);
			this.CalculateCurrentOrder();
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.FacingOrder = this.CurrentFacingOrder;
			base.Formation.ArrangementOrder = ArrangementOrder.ArrangementOrderLine;
			base.Formation.FiringOrder = FiringOrder.FiringOrderFireAtWill;
			base.Formation.FormOrder = FormOrder.FormOrderDeep;
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x00022F64 File Offset: 0x00021164
		protected override float GetAiWeight()
		{
			if (base.Formation.QuerySystem.IsRangedCavalryFormation)
			{
				return 1.2f;
			}
			return 0f;
		}

		// Token: 0x04000360 RID: 864
		private List<FlagCapturePoint> _flagpositions;

		// Token: 0x04000361 RID: 865
		private MissionMultiplayerFlagDomination _flagDominationGameMode;
	}
}
