using System;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200010E RID: 270
	public class BehaviorDefend : BehaviorComponent
	{
		// Token: 0x06000D37 RID: 3383 RVA: 0x0001B735 File Offset: 0x00019935
		public BehaviorDefend(Formation formation) : base(formation)
		{
			this.CalculateCurrentOrder();
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x0001B750 File Offset: 0x00019950
		protected override void CalculateCurrentOrder()
		{
			Vec2 vec;
			if (this.TacticalDefendPosition != null)
			{
				vec = ((!this.TacticalDefendPosition.IsInsurmountable) ? this.TacticalDefendPosition.Direction : (base.Formation.Team.QuerySystem.AverageEnemyPosition - this.TacticalDefendPosition.Position.AsVec2).Normalized());
			}
			else if (base.Formation.QuerySystem.ClosestEnemyFormation == null)
			{
				vec = base.Formation.Direction;
			}
			else
			{
				vec = ((base.Formation.Direction.DotProduct((base.Formation.QuerySystem.ClosestEnemyFormation.MedianPosition.AsVec2 - base.Formation.QuerySystem.AveragePosition).Normalized()) < 0.5f) ? (base.Formation.QuerySystem.ClosestEnemyFormation.MedianPosition.AsVec2 - base.Formation.QuerySystem.AveragePosition) : base.Formation.Direction).Normalized();
			}
			if (this.TacticalDefendPosition != null)
			{
				if (!this.TacticalDefendPosition.IsInsurmountable)
				{
					base.CurrentOrder = MovementOrder.MovementOrderMove(this.TacticalDefendPosition.Position);
				}
				else
				{
					Vec2 vec2 = this.TacticalDefendPosition.Position.AsVec2 + this.TacticalDefendPosition.Width * 0.5f * vec;
					WorldPosition position = this.TacticalDefendPosition.Position;
					position.SetVec2(vec2);
					base.CurrentOrder = MovementOrder.MovementOrderMove(position);
				}
				if (!this.TacticalDefendPosition.IsInsurmountable)
				{
					this.CurrentFacingOrder = FacingOrder.FacingOrderLookAtDirection(vec);
					return;
				}
				this.CurrentFacingOrder = FacingOrder.FacingOrderLookAtEnemy;
				return;
			}
			else
			{
				if (this.DefensePosition.IsValid)
				{
					base.CurrentOrder = MovementOrder.MovementOrderMove(this.DefensePosition);
					this.CurrentFacingOrder = FacingOrder.FacingOrderLookAtDirection(vec);
					return;
				}
				WorldPosition medianPosition = base.Formation.QuerySystem.MedianPosition;
				medianPosition.SetVec2(base.Formation.QuerySystem.AveragePosition);
				base.CurrentOrder = MovementOrder.MovementOrderMove(medianPosition);
				this.CurrentFacingOrder = FacingOrder.FacingOrderLookAtDirection(vec);
				return;
			}
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x0001B994 File Offset: 0x00019B94
		public override void TickOccasionally()
		{
			this.CalculateCurrentOrder();
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.FacingOrder = this.CurrentFacingOrder;
			if (base.Formation.QuerySystem.AveragePosition.DistanceSquared(base.CurrentOrder.GetPosition(base.Formation)) < 100f)
			{
				if (base.Formation.QuerySystem.HasShield)
				{
					base.Formation.ArrangementOrder = ArrangementOrder.ArrangementOrderShieldWall;
				}
				else if (base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation != null && base.Formation.QuerySystem.AveragePosition.DistanceSquared(base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.MedianPosition.AsVec2) > 100f && base.Formation.QuerySystem.UnderRangedAttackRatio > 0.2f - ((base.Formation.ArrangementOrder.OrderEnum == ArrangementOrder.ArrangementOrderEnum.Loose) ? 0.1f : 0f))
				{
					base.Formation.ArrangementOrder = ArrangementOrder.ArrangementOrderLoose;
				}
				if (this.TacticalDefendPosition != null)
				{
					float customWidth;
					if (this.TacticalDefendPosition.TacticalPositionType == TacticalPosition.TacticalPositionTypeEnum.ChokePoint)
					{
						customWidth = this.TacticalDefendPosition.Width;
					}
					else
					{
						int countOfUnits = base.Formation.CountOfUnits;
						float num = base.Formation.Interval * (float)(countOfUnits - 1) + base.Formation.UnitDiameter * (float)countOfUnits;
						customWidth = MathF.Min(this.TacticalDefendPosition.Width, num / 3f);
					}
					base.Formation.FormOrder = FormOrder.FormOrderCustom(customWidth);
					return;
				}
			}
			else
			{
				base.Formation.ArrangementOrder = ArrangementOrder.ArrangementOrderLoose;
			}
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x0001BB50 File Offset: 0x00019D50
		protected override void OnBehaviorActivatedAux()
		{
			this.CalculateCurrentOrder();
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.FacingOrder = this.CurrentFacingOrder;
			base.Formation.ArrangementOrder = ArrangementOrder.ArrangementOrderLoose;
			base.Formation.FiringOrder = FiringOrder.FiringOrderFireAtWill;
			base.Formation.FormOrder = FormOrder.FormOrderWide;
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x0001BBB5 File Offset: 0x00019DB5
		public override void ResetBehavior()
		{
			base.ResetBehavior();
			this.DefensePosition = WorldPosition.Invalid;
			this.TacticalDefendPosition = null;
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x0001BBCF File Offset: 0x00019DCF
		protected override float GetAiWeight()
		{
			return 1f;
		}

		// Token: 0x0400031B RID: 795
		public WorldPosition DefensePosition = WorldPosition.Invalid;

		// Token: 0x0400031C RID: 796
		public TacticalPosition TacticalDefendPosition;
	}
}
