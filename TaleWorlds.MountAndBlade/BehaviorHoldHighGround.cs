using System;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000118 RID: 280
	public class BehaviorHoldHighGround : BehaviorComponent
	{
		// Token: 0x06000D87 RID: 3463 RVA: 0x0001E70A File Offset: 0x0001C90A
		public BehaviorHoldHighGround(Formation formation) : base(formation)
		{
			this._isAllowedToChangePosition = true;
			this.RangedAllyFormation = null;
			this.CalculateCurrentOrder();
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x0001E728 File Offset: 0x0001C928
		protected override void CalculateCurrentOrder()
		{
			WorldPosition worldPosition;
			Vec2 direction;
			if (base.Formation.QuerySystem.ClosestEnemyFormation != null)
			{
				worldPosition = base.Formation.QuerySystem.MedianPosition;
				if (base.Formation.AI.ActiveBehavior != this)
				{
					this._isAllowedToChangePosition = true;
				}
				else
				{
					float num = Math.Max((this.RangedAllyFormation != null) ? (this.RangedAllyFormation.QuerySystem.MissileRangeAdjusted * 0.8f) : 0f, 30f);
					this._isAllowedToChangePosition = (base.Formation.QuerySystem.AveragePosition.DistanceSquared(base.Formation.QuerySystem.ClosestEnemyFormation.MedianPosition.AsVec2) > num * num);
				}
				if (this._isAllowedToChangePosition)
				{
					worldPosition.SetVec2(base.Formation.QuerySystem.HighGroundCloseToForeseenBattleGround);
					this._lastChosenPosition = worldPosition;
				}
				else
				{
					worldPosition = this._lastChosenPosition;
				}
				direction = ((base.Formation.QuerySystem.AveragePosition.DistanceSquared(base.Formation.QuerySystem.HighGroundCloseToForeseenBattleGround) > 25f) ? (base.Formation.QuerySystem.Team.MedianTargetFormationPosition.AsVec2 - worldPosition.AsVec2).Normalized() : ((base.Formation.Direction.DotProduct((base.Formation.QuerySystem.ClosestEnemyFormation.MedianPosition.AsVec2 - base.Formation.QuerySystem.AveragePosition).Normalized()) < 0.5f) ? (base.Formation.QuerySystem.ClosestEnemyFormation.MedianPosition.AsVec2 - base.Formation.QuerySystem.AveragePosition) : base.Formation.Direction).Normalized());
			}
			else
			{
				direction = base.Formation.Direction;
				worldPosition = base.Formation.QuerySystem.MedianPosition;
				worldPosition.SetVec2(base.Formation.QuerySystem.AveragePosition);
			}
			base.CurrentOrder = MovementOrder.MovementOrderMove(worldPosition);
			this.CurrentFacingOrder = FacingOrder.FacingOrderLookAtDirection(direction);
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x0001E96A File Offset: 0x0001CB6A
		public override void TickOccasionally()
		{
			this.CalculateCurrentOrder();
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.FacingOrder = this.CurrentFacingOrder;
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x0001E994 File Offset: 0x0001CB94
		protected override void OnBehaviorActivatedAux()
		{
			this.CalculateCurrentOrder();
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.FacingOrder = this.CurrentFacingOrder;
			base.Formation.ArrangementOrder = ArrangementOrder.ArrangementOrderLine;
			base.Formation.FiringOrder = FiringOrder.FiringOrderFireAtWill;
			base.Formation.FormOrder = FormOrder.FormOrderDeep;
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x0001E9F9 File Offset: 0x0001CBF9
		protected override float GetAiWeight()
		{
			if (base.Formation.QuerySystem.ClosestEnemyFormation == null)
			{
				return 0f;
			}
			return 1f;
		}

		// Token: 0x0400033F RID: 831
		public Formation RangedAllyFormation;

		// Token: 0x04000340 RID: 832
		private bool _isAllowedToChangePosition;

		// Token: 0x04000341 RID: 833
		private WorldPosition _lastChosenPosition;
	}
}
