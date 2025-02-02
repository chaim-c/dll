using System;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000119 RID: 281
	public class BehaviorHorseArcherSkirmish : BehaviorComponent
	{
		// Token: 0x06000D8C RID: 3468 RVA: 0x0001EA18 File Offset: 0x0001CC18
		public BehaviorHorseArcherSkirmish(Formation formation) : base(formation)
		{
			this.CalculateCurrentOrder();
			base.BehaviorCoherence = 0.5f;
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x0001EA39 File Offset: 0x0001CC39
		protected override float GetAiWeight()
		{
			if (!this._isEnemyReachable)
			{
				return 0.09f;
			}
			return 0.9f;
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x0001EA50 File Offset: 0x0001CC50
		protected override void OnBehaviorActivatedAux()
		{
			this.CalculateCurrentOrder();
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.ArrangementOrder = ArrangementOrder.ArrangementOrderLine;
			base.Formation.FacingOrder = FacingOrder.FacingOrderLookAtEnemy;
			base.Formation.FiringOrder = FiringOrder.FiringOrderFireAtWill;
			base.Formation.FormOrder = FormOrder.FormOrderDeep;
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x0001EAB4 File Offset: 0x0001CCB4
		protected override void CalculateCurrentOrder()
		{
			WorldPosition position = base.Formation.QuerySystem.MedianPosition;
			this._isEnemyReachable = (base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation != null && (!(base.Formation.Team.TeamAI is TeamAISiegeComponent) || !TeamAISiegeComponent.IsFormationInsideCastle(base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.Formation, false, 0.4f)));
			Vec2 averagePosition = base.Formation.QuerySystem.AveragePosition;
			if (!this._isEnemyReachable)
			{
				position.SetVec2(averagePosition);
			}
			else
			{
				WorldPosition medianPosition = base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.MedianPosition;
				int num = 0;
				Vec2 vec = Vec2.Zero;
				foreach (Formation formation in base.Formation.Team.FormationsIncludingSpecialAndEmpty)
				{
					if (formation != base.Formation && formation.CountOfUnits > 0)
					{
						num++;
						vec += formation.QuerySystem.MedianPosition.AsVec2;
					}
				}
				if (num > 0)
				{
					vec /= (float)num;
				}
				else
				{
					vec = averagePosition;
				}
				WorldPosition medianTargetFormationPosition = base.Formation.QuerySystem.Team.MedianTargetFormationPosition;
				Vec2 v = (medianTargetFormationPosition.AsVec2 - vec).Normalized();
				float missileRangeAdjusted = base.Formation.QuerySystem.MissileRangeAdjusted;
				if (this._rushMode)
				{
					float num2 = averagePosition.DistanceSquared(medianPosition.AsVec2);
					if (num2 > base.Formation.QuerySystem.MissileRangeAdjusted * base.Formation.QuerySystem.MissileRangeAdjusted)
					{
						position = medianTargetFormationPosition;
						position.SetVec2(position.AsVec2 - v * (missileRangeAdjusted - (10f + base.Formation.Depth * 0.5f)));
					}
					else if (base.Formation.QuerySystem.ClosestEnemyFormation.IsCavalryFormation || num2 <= 400f || base.Formation.QuerySystem.UnderRangedAttackRatio >= 0.4f)
					{
						position = base.Formation.QuerySystem.Team.MedianPosition;
						position.SetVec2(vec - (((num > 0) ? 30f : 80f) + base.Formation.Depth) * v);
						this._rushMode = false;
					}
					else
					{
						position = base.Formation.QuerySystem.Team.MedianPosition;
						Vec2 v2 = (medianPosition.AsVec2 - averagePosition).Normalized();
						position.SetVec2(medianPosition.AsVec2 - v2 * (missileRangeAdjusted - (10f + base.Formation.Depth * 0.5f)));
					}
				}
				else
				{
					if (num > 0)
					{
						position = base.Formation.QuerySystem.Team.MedianPosition;
						position.SetVec2(vec - (30f + base.Formation.Depth) * v);
					}
					else
					{
						position = base.Formation.QuerySystem.ClosestEnemyFormation.MedianPosition;
						position.SetVec2(position.AsVec2 - 80f * v);
					}
					if (position.AsVec2.DistanceSquared(averagePosition) <= 400f)
					{
						position = medianTargetFormationPosition;
						position.SetVec2(position.AsVec2 - v * (missileRangeAdjusted - (10f + base.Formation.Depth * 0.5f)));
						this._rushMode = true;
					}
				}
			}
			base.CurrentOrder = MovementOrder.MovementOrderMove(position);
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x0001EE8C File Offset: 0x0001D08C
		public override void TickOccasionally()
		{
			this.CalculateCurrentOrder();
			base.Formation.SetMovementOrder(base.CurrentOrder);
		}

		// Token: 0x04000342 RID: 834
		private bool _rushMode;

		// Token: 0x04000343 RID: 835
		private bool _isEnemyReachable = true;
	}
}
