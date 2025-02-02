using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Engine;
using TaleWorlds.MountAndBlade.Objects;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000127 RID: 295
	public class BehaviorSergeantMPLastFlagLastStand : BehaviorComponent
	{
		// Token: 0x06000DEA RID: 3562 RVA: 0x000222FB File Offset: 0x000204FB
		public BehaviorSergeantMPLastFlagLastStand(Formation formation) : base(formation)
		{
			this._flagpositions = Mission.Current.ActiveMissionObjects.FindAllWithType<FlagCapturePoint>().ToList<FlagCapturePoint>();
			this._flagDominationGameMode = Mission.Current.GetMissionBehavior<MissionMultiplayerFlagDomination>();
			this.CalculateCurrentOrder();
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x00022334 File Offset: 0x00020534
		protected override void CalculateCurrentOrder()
		{
			base.CurrentOrder = ((this._flagpositions.Count > 0) ? MovementOrder.MovementOrderMove(new WorldPosition(Mission.Current.Scene, UIntPtr.Zero, this._flagpositions[0].Position, false)) : MovementOrder.MovementOrderStop);
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x00022388 File Offset: 0x00020588
		public override void TickOccasionally()
		{
			this._flagpositions.RemoveAll((FlagCapturePoint fp) => fp.IsDeactivated);
			this.CalculateCurrentOrder();
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.FiringOrder = FiringOrder.FiringOrderHoldYourFire;
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x000223E8 File Offset: 0x000205E8
		protected override void OnBehaviorActivatedAux()
		{
			this._flagpositions.RemoveAll((FlagCapturePoint fp) => fp.IsDeactivated);
			this.CalculateCurrentOrder();
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.ArrangementOrder = ArrangementOrder.ArrangementOrderLine;
			base.Formation.FacingOrder = FacingOrder.FacingOrderLookAtEnemy;
			base.Formation.FiringOrder = FiringOrder.FiringOrderHoldYourFire;
			base.Formation.FormOrder = FormOrder.FormOrderDeep;
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x00022478 File Offset: 0x00020678
		protected override float GetAiWeight()
		{
			if (this._lastEffort)
			{
				return 10f;
			}
			this._flagpositions.RemoveAll((FlagCapturePoint fp) => fp.IsDeactivated);
			FlagCapturePoint flagCapturePoint = this._flagpositions.FirstOrDefault<FlagCapturePoint>();
			if (this._flagpositions.Count != 1 || this._flagDominationGameMode.GetFlagOwnerTeam(flagCapturePoint) == null || !this._flagDominationGameMode.GetFlagOwnerTeam(flagCapturePoint).IsEnemyOf(base.Formation.Team))
			{
				return 0f;
			}
			float timeUntilBattleSideVictory = this._flagDominationGameMode.GetTimeUntilBattleSideVictory(this._flagDominationGameMode.GetFlagOwnerTeam(flagCapturePoint).Side);
			if (timeUntilBattleSideVictory <= 60f)
			{
				return 10f;
			}
			float num = base.Formation.QuerySystem.AveragePosition.Distance(flagCapturePoint.Position.AsVec2);
			float movementSpeedMaximum = base.Formation.QuerySystem.MovementSpeedMaximum;
			if (num / movementSpeedMaximum * 8f > timeUntilBattleSideVictory)
			{
				this._lastEffort = true;
				return 10f;
			}
			return 0f;
		}

		// Token: 0x0400035B RID: 859
		private List<FlagCapturePoint> _flagpositions;

		// Token: 0x0400035C RID: 860
		private bool _lastEffort;

		// Token: 0x0400035D RID: 861
		private MissionMultiplayerFlagDomination _flagDominationGameMode;
	}
}
