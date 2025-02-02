using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.LinQuick;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000112 RID: 274
	public class BehaviorDefensiveRing : BehaviorComponent
	{
		// Token: 0x06000D5A RID: 3418 RVA: 0x0001CDBA File Offset: 0x0001AFBA
		public BehaviorDefensiveRing(Formation formation) : base(formation)
		{
			this.CalculateCurrentOrder();
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x0001CDCC File Offset: 0x0001AFCC
		protected override void CalculateCurrentOrder()
		{
			Vec2 direction;
			if (this.TacticalDefendPosition != null)
			{
				direction = this.TacticalDefendPosition.Direction;
			}
			else if (base.Formation.QuerySystem.ClosestEnemyFormation == null)
			{
				direction = base.Formation.Direction;
			}
			else
			{
				direction = ((base.Formation.Direction.DotProduct((base.Formation.QuerySystem.ClosestEnemyFormation.MedianPosition.AsVec2 - base.Formation.QuerySystem.AveragePosition).Normalized()) < 0.5f) ? (base.Formation.QuerySystem.ClosestEnemyFormation.MedianPosition.AsVec2 - base.Formation.QuerySystem.AveragePosition) : base.Formation.Direction).Normalized();
			}
			if (this.TacticalDefendPosition != null)
			{
				base.CurrentOrder = MovementOrder.MovementOrderMove(this.TacticalDefendPosition.Position);
				this.CurrentFacingOrder = FacingOrder.FacingOrderLookAtDirection(direction);
				return;
			}
			WorldPosition medianPosition = base.Formation.QuerySystem.MedianPosition;
			medianPosition.SetVec2(base.Formation.QuerySystem.AveragePosition);
			base.CurrentOrder = MovementOrder.MovementOrderMove(medianPosition);
			this.CurrentFacingOrder = FacingOrder.FacingOrderLookAtDirection(direction);
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x0001CF20 File Offset: 0x0001B120
		public override void TickOccasionally()
		{
			this.CalculateCurrentOrder();
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.FacingOrder = this.CurrentFacingOrder;
			if (base.Formation.QuerySystem.AveragePosition.Distance(base.CurrentOrder.GetPosition(base.Formation)) - base.Formation.Arrangement.Depth * 0.5f < 10f)
			{
				base.Formation.ArrangementOrder = ArrangementOrder.ArrangementOrderCircle;
				if (base.Formation.Team.FormationsIncludingEmpty.AnyQ((Formation f) => f.CountOfUnits > 0 && f.QuerySystem.IsRangedFormation))
				{
					Formation formation = base.Formation.Team.FormationsIncludingEmpty.WhereQ((Formation f) => f.CountOfUnits > 0 && f.QuerySystem.IsRangedFormation).MaxBy((Formation f) => f.CountOfUnits);
					int num = (int)MathF.Sqrt((float)formation.CountOfUnits);
					float num2 = ((float)num * formation.UnitDiameter + (float)(num - 1) * formation.Interval) * 0.5f * 1.414213f;
					int i = base.Formation.Arrangement.UnitCount;
					int num3 = 0;
					while (i > 0)
					{
						double a = (double)(num2 + base.Formation.Distance * (float)num3 + base.Formation.UnitDiameter * (float)(num3 + 1)) * 3.141592653589793 * 2.0 / (double)(base.Formation.UnitDiameter + base.Formation.Interval);
						i -= (int)Math.Ceiling(a);
						num3++;
					}
					float num4 = num2 + (float)num3 * base.Formation.UnitDiameter + (float)(num3 - 1) * base.Formation.Distance;
					base.Formation.FormOrder = FormOrder.FormOrderCustom(num4 * 2f);
					return;
				}
			}
			else
			{
				base.Formation.ArrangementOrder = ArrangementOrder.ArrangementOrderLoose;
			}
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x0001D148 File Offset: 0x0001B348
		protected override void OnBehaviorActivatedAux()
		{
			this.CalculateCurrentOrder();
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.FacingOrder = this.CurrentFacingOrder;
			base.Formation.ArrangementOrder = ArrangementOrder.ArrangementOrderLoose;
			base.Formation.FiringOrder = FiringOrder.FiringOrderFireAtWill;
			base.Formation.FormOrder = FormOrder.FormOrderDeep;
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x0001D1AD File Offset: 0x0001B3AD
		public override void ResetBehavior()
		{
			base.ResetBehavior();
			this.TacticalDefendPosition = null;
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x0001D1BC File Offset: 0x0001B3BC
		protected override float GetAiWeight()
		{
			if (this.TacticalDefendPosition == null)
			{
				return 0f;
			}
			return 1f;
		}

		// Token: 0x04000330 RID: 816
		public TacticalPosition TacticalDefendPosition;
	}
}
