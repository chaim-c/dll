using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.LinQuick;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200011B RID: 283
	public class BehaviorProtectFlank : BehaviorComponent
	{
		// Token: 0x06000D96 RID: 3478 RVA: 0x0001F4A4 File Offset: 0x0001D6A4
		public BehaviorProtectFlank(Formation formation) : base(formation)
		{
			this._protectFlankState = BehaviorProtectFlank.BehaviorState.HoldingFlank;
			this._behaviorSide = formation.AI.Side;
			this._mainFormation = formation.Team.FormationsIncludingEmpty.FirstOrDefaultQ((Formation f) => f.CountOfUnits > 0 && f.AI.IsMainFormation);
			this.CalculateCurrentOrder();
			base.CurrentOrder = this._movementOrder;
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x0001F518 File Offset: 0x0001D718
		protected override void CalculateCurrentOrder()
		{
			if (this._mainFormation == null || base.Formation.AI.IsMainFormation || base.Formation.QuerySystem.ClosestEnemyFormation == null)
			{
				base.CurrentOrder = MovementOrder.MovementOrderStop;
				this.CurrentFacingOrder = FacingOrder.FacingOrderLookAtEnemy;
				return;
			}
			if (this._protectFlankState == BehaviorProtectFlank.BehaviorState.HoldingFlank || this._protectFlankState == BehaviorProtectFlank.BehaviorState.Returning)
			{
				Vec2 direction = this._mainFormation.Direction;
				Vec2 v = (base.Formation.QuerySystem.Team.MedianTargetFormationPosition.AsVec2 - this._mainFormation.QuerySystem.MedianPosition.AsVec2).Normalized();
				Vec2 vec;
				if (this._behaviorSide == FormationAI.BehaviorSide.Right || this.FlankSide == FormationAI.BehaviorSide.Right)
				{
					vec = this._mainFormation.CurrentPosition + v.RightVec().Normalized() * (this._mainFormation.Width + base.Formation.Width + 10f);
					vec -= v * (this._mainFormation.Depth + base.Formation.Depth);
				}
				else if (this._behaviorSide == FormationAI.BehaviorSide.Left || this.FlankSide == FormationAI.BehaviorSide.Left)
				{
					vec = this._mainFormation.CurrentPosition + v.LeftVec().Normalized() * (this._mainFormation.Width + base.Formation.Width + 10f);
					vec -= v * (this._mainFormation.Depth + base.Formation.Depth);
				}
				else
				{
					vec = this._mainFormation.CurrentPosition + v * ((this._mainFormation.Depth + base.Formation.Depth) * 0.5f + 10f);
				}
				WorldPosition medianPosition = this._mainFormation.QuerySystem.MedianPosition;
				medianPosition.SetVec2(vec);
				this._movementOrder = MovementOrder.MovementOrderMove(medianPosition);
				base.CurrentOrder = this._movementOrder;
				this.CurrentFacingOrder = FacingOrder.FacingOrderLookAtDirection(direction);
			}
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x0001F73C File Offset: 0x0001D93C
		private void CheckAndChangeState()
		{
			Vec2 position = this._movementOrder.GetPosition(base.Formation);
			switch (this._protectFlankState)
			{
			case BehaviorProtectFlank.BehaviorState.HoldingFlank:
				if (base.Formation.QuerySystem.ClosestEnemyFormation != null)
				{
					float num = 50f + (base.Formation.Depth + base.Formation.QuerySystem.ClosestEnemyFormation.Formation.Depth) / 2f;
					if (base.Formation.QuerySystem.ClosestEnemyFormation.MedianPosition.AsVec2.DistanceSquared(position) < num * num)
					{
						this._chargeToTargetOrder = MovementOrder.MovementOrderChargeToTarget(base.Formation.QuerySystem.ClosestEnemyFormation.Formation);
						base.CurrentOrder = this._chargeToTargetOrder;
						this._protectFlankState = BehaviorProtectFlank.BehaviorState.Charging;
						return;
					}
				}
				break;
			case BehaviorProtectFlank.BehaviorState.Charging:
			{
				if (base.Formation.QuerySystem.ClosestEnemyFormation == null)
				{
					base.CurrentOrder = this._movementOrder;
					this._protectFlankState = BehaviorProtectFlank.BehaviorState.Returning;
					return;
				}
				float num2 = 60f + (base.Formation.Depth + base.Formation.QuerySystem.ClosestEnemyFormation.Formation.Depth) / 2f;
				if (base.Formation.QuerySystem.AveragePosition.DistanceSquared(position) > num2 * num2)
				{
					base.CurrentOrder = this._movementOrder;
					this._protectFlankState = BehaviorProtectFlank.BehaviorState.Returning;
					return;
				}
				break;
			}
			case BehaviorProtectFlank.BehaviorState.Returning:
				if (base.Formation.QuerySystem.AveragePosition.DistanceSquared(position) < 400f)
				{
					this._protectFlankState = BehaviorProtectFlank.BehaviorState.HoldingFlank;
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x0001F8D8 File Offset: 0x0001DAD8
		public override void OnValidBehaviorSideChanged()
		{
			base.OnValidBehaviorSideChanged();
			this._mainFormation = base.Formation.Team.FormationsIncludingEmpty.FirstOrDefaultQ((Formation f) => f.CountOfUnits > 0 && f.AI.IsMainFormation);
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x0001F928 File Offset: 0x0001DB28
		public override void TickOccasionally()
		{
			this.CheckAndChangeState();
			this.CalculateCurrentOrder();
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.FacingOrder = this.CurrentFacingOrder;
			if (this._protectFlankState == BehaviorProtectFlank.BehaviorState.HoldingFlank && base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation != null && base.Formation.QuerySystem.AveragePosition.DistanceSquared(base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.MedianPosition.AsVec2) > 1600f && base.Formation.QuerySystem.UnderRangedAttackRatio > 0.2f - ((base.Formation.ArrangementOrder.OrderEnum == ArrangementOrder.ArrangementOrderEnum.Loose) ? 0.1f : 0f))
			{
				base.Formation.ArrangementOrder = ArrangementOrder.ArrangementOrderLoose;
				return;
			}
			base.Formation.ArrangementOrder = ArrangementOrder.ArrangementOrderLine;
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x0001FA18 File Offset: 0x0001DC18
		protected override void OnBehaviorActivatedAux()
		{
			this.CalculateCurrentOrder();
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.FacingOrder = this.CurrentFacingOrder;
			base.Formation.ArrangementOrder = ArrangementOrder.ArrangementOrderLine;
			base.Formation.FiringOrder = FiringOrder.FiringOrderFireAtWill;
			base.Formation.FormOrder = FormOrder.FormOrderDeep;
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x0001FA80 File Offset: 0x0001DC80
		public override TextObject GetBehaviorString()
		{
			TextObject behaviorString = base.GetBehaviorString();
			TextObject variable = GameTexts.FindText("str_formation_ai_side_strings", base.Formation.AI.Side.ToString());
			behaviorString.SetTextVariable("IS_GENERAL_SIDE", "0");
			behaviorString.SetTextVariable("SIDE_STRING", variable);
			if (this._mainFormation != null)
			{
				behaviorString.SetTextVariable("AI_SIDE", GameTexts.FindText("str_formation_ai_side_strings", this._mainFormation.AI.Side.ToString()));
				behaviorString.SetTextVariable("CLASS", GameTexts.FindText("str_formation_class_string", this._mainFormation.PhysicalClass.GetName()));
			}
			return behaviorString;
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x0001FB40 File Offset: 0x0001DD40
		protected override float GetAiWeight()
		{
			if (this._mainFormation == null || !this._mainFormation.AI.IsMainFormation)
			{
				this._mainFormation = base.Formation.Team.FormationsIncludingEmpty.FirstOrDefaultQ((Formation f) => f.CountOfUnits > 0 && f.AI.IsMainFormation);
			}
			if (this._mainFormation == null || base.Formation.AI.IsMainFormation)
			{
				return 0f;
			}
			return 1.2f;
		}

		// Token: 0x04000346 RID: 838
		private Formation _mainFormation;

		// Token: 0x04000347 RID: 839
		public FormationAI.BehaviorSide FlankSide;

		// Token: 0x04000348 RID: 840
		private BehaviorProtectFlank.BehaviorState _protectFlankState;

		// Token: 0x04000349 RID: 841
		private MovementOrder _movementOrder;

		// Token: 0x0400034A RID: 842
		private MovementOrder _chargeToTargetOrder;

		// Token: 0x0200040C RID: 1036
		private enum BehaviorState
		{
			// Token: 0x040017D9 RID: 6105
			HoldingFlank,
			// Token: 0x040017DA RID: 6106
			Charging,
			// Token: 0x040017DB RID: 6107
			Returning
		}
	}
}
