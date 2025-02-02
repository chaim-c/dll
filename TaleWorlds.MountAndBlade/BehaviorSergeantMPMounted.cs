using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Objects;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000128 RID: 296
	public class BehaviorSergeantMPMounted : BehaviorComponent
	{
		// Token: 0x06000DEF RID: 3567 RVA: 0x0002258C File Offset: 0x0002078C
		public BehaviorSergeantMPMounted(Formation formation) : base(formation)
		{
			this._flagpositions = base.Formation.Team.Mission.ActiveMissionObjects.FindAllWithType<FlagCapturePoint>().ToList<FlagCapturePoint>();
			this._flagDominationGameMode = base.Formation.Team.Mission.GetMissionBehavior<MissionMultiplayerFlagDomination>();
			this.CalculateCurrentOrder();
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x000225E8 File Offset: 0x000207E8
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

		// Token: 0x06000DF1 RID: 3569 RVA: 0x000226D8 File Offset: 0x000208D8
		protected override void CalculateCurrentOrder()
		{
			if (base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation == null || base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.MedianPosition.AsVec2.DistanceSquared(base.Formation.QuerySystem.AveragePosition) > 2500f)
			{
				base.CurrentOrder = this.UncapturedFlagMoveOrder();
				return;
			}
			FlagCapturePoint flagCapturePoint = null;
			if (this._flagpositions.Any((FlagCapturePoint fp) => this._flagDominationGameMode.GetFlagOwnerTeam(fp) != base.Formation.Team && !fp.IsContested))
			{
				flagCapturePoint = (from fp in this._flagpositions
				where this._flagDominationGameMode.GetFlagOwnerTeam(fp) != base.Formation.Team && !fp.IsContested
				select fp).MinBy((FlagCapturePoint fp) => base.Formation.QuerySystem.AveragePosition.DistanceSquared(fp.Position.AsVec2));
			}
			if ((!base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.IsRangedFormation || base.Formation.QuerySystem.FormationPower / base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.FormationPower / base.Formation.Team.QuerySystem.RemainingPowerRatio <= 0.7f) && flagCapturePoint != null)
			{
				base.CurrentOrder = MovementOrder.MovementOrderMove(new WorldPosition(base.Formation.Team.Mission.Scene, UIntPtr.Zero, flagCapturePoint.Position, false));
				return;
			}
			base.CurrentOrder = MovementOrder.MovementOrderChargeToTarget(base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.Formation);
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x00022838 File Offset: 0x00020A38
		public override void TickOccasionally()
		{
			this._flagpositions.RemoveAll((FlagCapturePoint fp) => fp.IsDeactivated);
			this.CalculateCurrentOrder();
			base.Formation.SetMovementOrder(base.CurrentOrder);
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x00022888 File Offset: 0x00020A88
		protected override void OnBehaviorActivatedAux()
		{
			this.CalculateCurrentOrder();
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.ArrangementOrder = ArrangementOrder.ArrangementOrderLine;
			base.Formation.FacingOrder = FacingOrder.FacingOrderLookAtEnemy;
			base.Formation.FiringOrder = FiringOrder.FiringOrderFireAtWill;
			base.Formation.FormOrder = FormOrder.FormOrderDeep;
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x000228EC File Offset: 0x00020AEC
		protected override float GetAiWeight()
		{
			if (base.Formation.QuerySystem.IsCavalryFormation)
			{
				return 1.2f;
			}
			return 0f;
		}

		// Token: 0x0400035E RID: 862
		private List<FlagCapturePoint> _flagpositions;

		// Token: 0x0400035F RID: 863
		private MissionMultiplayerFlagDomination _flagDominationGameMode;
	}
}
