using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.LinQuick;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200012F RID: 303
	public class BehaviorSkirmishLine : BehaviorComponent
	{
		// Token: 0x06000E33 RID: 3635 RVA: 0x00024D20 File Offset: 0x00022F20
		public BehaviorSkirmishLine(Formation formation) : base(formation)
		{
			this._behaviorSide = FormationAI.BehaviorSide.BehaviorSideNotSet;
			this._mainFormation = formation.Team.FormationsIncludingEmpty.FirstOrDefaultQ((Formation f) => f.CountOfUnits > 0 && f.AI.IsMainFormation);
			this.CalculateCurrentOrder();
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x00024D78 File Offset: 0x00022F78
		protected override void CalculateCurrentOrder()
		{
			this._targetFormation = (base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation ?? base.Formation.QuerySystem.ClosestEnemyFormation);
			Vec2 vec;
			WorldPosition medianPosition;
			if (this._targetFormation == null || this._mainFormation == null)
			{
				vec = base.Formation.Direction;
				medianPosition = base.Formation.QuerySystem.MedianPosition;
				medianPosition.SetVec2(base.Formation.QuerySystem.AveragePosition);
			}
			else
			{
				if (this._mainFormation.AI.ActiveBehavior is BehaviorCautiousAdvance)
				{
					vec = this._mainFormation.Direction;
				}
				else
				{
					vec = ((base.Formation.Direction.DotProduct((this._targetFormation.MedianPosition.AsVec2 - this._mainFormation.QuerySystem.MedianPosition.AsVec2).Normalized()) < 0.5f) ? (this._targetFormation.MedianPosition.AsVec2 - this._mainFormation.QuerySystem.MedianPosition.AsVec2) : base.Formation.Direction).Normalized();
				}
				Vec2 vec2 = this._mainFormation.OrderPosition - this._mainFormation.QuerySystem.MedianPosition.AsVec2;
				float num = this._mainFormation.QuerySystem.MovementSpeed * 7f;
				float length = vec2.Length;
				if (length > 0f)
				{
					float num2 = num / length;
					if (num2 < 1f)
					{
						vec2 *= num2;
					}
				}
				medianPosition = this._mainFormation.QuerySystem.MedianPosition;
				medianPosition.SetVec2(medianPosition.AsVec2 + vec * 8f + vec2);
			}
			base.CurrentOrder = MovementOrder.MovementOrderMove(medianPosition);
			if (!this.CurrentFacingOrder.GetDirection(base.Formation, null).IsValid || this.CurrentFacingOrder.OrderEnum == FacingOrder.FacingOrderEnum.LookAtEnemy || (this._targetFormation != null && (base.Formation.QuerySystem.AveragePosition.DistanceSquared(this._targetFormation.MedianPosition.GetNavMeshVec3().AsVec2) >= base.Formation.QuerySystem.MissileRangeAdjusted * base.Formation.QuerySystem.MissileRangeAdjusted || (!this._targetFormation.IsRangedCavalryFormation && this.CurrentFacingOrder.GetDirection(base.Formation, null).DotProduct(vec) <= MBMath.Lerp(0.5f, 1f, 1f - MBMath.ClampFloat(base.Formation.Width, 1f, 20f) * 0.05f, 1E-05f)))))
			{
				this.CurrentFacingOrder = FacingOrder.FacingOrderLookAtDirection(vec);
			}
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x00025074 File Offset: 0x00023274
		public override TextObject GetBehaviorString()
		{
			TextObject behaviorString = base.GetBehaviorString();
			if (this._mainFormation != null)
			{
				behaviorString.SetTextVariable("AI_SIDE", GameTexts.FindText("str_formation_ai_side_strings", this._mainFormation.AI.Side.ToString()));
				behaviorString.SetTextVariable("CLASS", GameTexts.FindText("str_formation_class_string", this._mainFormation.PhysicalClass.GetName()));
			}
			return behaviorString;
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x000250EC File Offset: 0x000232EC
		public override void OnValidBehaviorSideChanged()
		{
			base.OnValidBehaviorSideChanged();
			this._mainFormation = base.Formation.Team.FormationsIncludingEmpty.FirstOrDefaultQ((Formation f) => f.CountOfUnits > 0 && f.AI.IsMainFormation);
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x0002513C File Offset: 0x0002333C
		public override void TickOccasionally()
		{
			this.CalculateCurrentOrder();
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.FacingOrder = this.CurrentFacingOrder;
			if (this._mainFormation != null && base.Formation.Width > this._mainFormation.Width * 1.5f)
			{
				base.Formation.FormOrder = FormOrder.FormOrderCustom(this._mainFormation.Width * 1.2f);
			}
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x000251B8 File Offset: 0x000233B8
		protected override void OnBehaviorActivatedAux()
		{
			this.CalculateCurrentOrder();
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.FacingOrder = this.CurrentFacingOrder;
			base.Formation.ArrangementOrder = ArrangementOrder.ArrangementOrderLine;
			base.Formation.FiringOrder = FiringOrder.FiringOrderFireAtWill;
			base.Formation.FormOrder = FormOrder.FormOrderWider;
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x00025220 File Offset: 0x00023420
		protected override float GetAiWeight()
		{
			if (this._mainFormation == null || !this._mainFormation.AI.IsMainFormation)
			{
				this._mainFormation = base.Formation.Team.FormationsIncludingEmpty.FirstOrDefaultQ((Formation f) => f.CountOfUnits > 0 && f.AI.IsMainFormation);
			}
			if (this._behaviorSide != base.Formation.AI.Side)
			{
				this._behaviorSide = base.Formation.AI.Side;
			}
			if (this._mainFormation == null || base.Formation.AI.IsMainFormation || base.Formation.QuerySystem.ClosestEnemyFormation == null)
			{
				return 0f;
			}
			FormationQuerySystem querySystem = base.Formation.QuerySystem;
			float num = MBMath.Lerp(0.1f, 1f, MBMath.ClampFloat(querySystem.RangedUnitRatio + querySystem.RangedCavalryUnitRatio, 0f, 0.5f) * 2f, 1E-05f);
			float value = base.Formation.QuerySystem.AveragePosition.Distance((querySystem.ClosestSignificantlyLargeEnemyFormation ?? querySystem.ClosestEnemyFormation).MedianPosition.AsVec2) / (querySystem.ClosestSignificantlyLargeEnemyFormation ?? querySystem.ClosestEnemyFormation).MovementSpeedMaximum;
			float num2 = MBMath.Lerp(0.5f, 1.2f, (MBMath.ClampFloat(value, 4f, 8f) - 4f) / 4f, 1E-05f);
			return num * querySystem.MainFormationReliabilityFactor * num2;
		}

		// Token: 0x04000372 RID: 882
		private Formation _mainFormation;

		// Token: 0x04000373 RID: 883
		private FormationQuerySystem _targetFormation;
	}
}
