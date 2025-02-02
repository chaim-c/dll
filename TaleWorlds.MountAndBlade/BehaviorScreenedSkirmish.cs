using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.LinQuick;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000125 RID: 293
	public class BehaviorScreenedSkirmish : BehaviorComponent
	{
		// Token: 0x06000DD9 RID: 3545 RVA: 0x0002149C File Offset: 0x0001F69C
		public BehaviorScreenedSkirmish(Formation formation) : base(formation)
		{
			this._behaviorSide = formation.AI.Side;
			this._mainFormation = formation.Team.FormationsIncludingEmpty.FirstOrDefaultQ((Formation f) => f.CountOfUnits > 0 && f.AI.IsMainFormation);
			this.CalculateCurrentOrder();
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x00021504 File Offset: 0x0001F704
		protected override void CalculateCurrentOrder()
		{
			Vec2 vec2;
			if (base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation != null && this._mainFormation != null)
			{
				Vec2 vec = (base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.MedianPosition.AsVec2 - base.Formation.QuerySystem.AveragePosition).Normalized();
				Vec2 v = (this._mainFormation.QuerySystem.MedianPosition.AsVec2 - base.Formation.QuerySystem.AveragePosition).Normalized();
				if (vec.DotProduct(v) > 0.5f)
				{
					vec2 = this._mainFormation.FacingOrder.GetDirection(this._mainFormation, null);
				}
				else
				{
					vec2 = vec;
				}
			}
			else
			{
				vec2 = base.Formation.Direction;
			}
			WorldPosition medianPosition;
			if (this._mainFormation == null)
			{
				medianPosition = base.Formation.QuerySystem.MedianPosition;
				medianPosition.SetVec2(base.Formation.QuerySystem.AveragePosition);
			}
			else
			{
				medianPosition = this._mainFormation.QuerySystem.MedianPosition;
				medianPosition.SetVec2(medianPosition.AsVec2 - vec2 * ((this._mainFormation.Depth + base.Formation.Depth) * 0.5f));
			}
			if (!base.CurrentOrder.CreateNewOrderWorldPosition(base.Formation, WorldPosition.WorldPositionEnforcedCache.None).IsValid || (base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation != null && (!base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.IsRangedCavalryFormation || base.Formation.QuerySystem.AveragePosition.DistanceSquared(base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.MedianPosition.GetNavMeshVec3().AsVec2) >= base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.MissileRangeAdjusted * base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.MissileRangeAdjusted || base.CurrentOrder.CreateNewOrderWorldPosition(base.Formation, WorldPosition.WorldPositionEnforcedCache.NavMeshVec3).GetNavMeshVec3().DistanceSquared(medianPosition.GetNavMeshVec3()) >= base.Formation.Depth * base.Formation.Depth)))
			{
				base.CurrentOrder = MovementOrder.MovementOrderMove(medianPosition);
			}
			if (!this.CurrentFacingOrder.GetDirection(base.Formation, null).IsValid || this.CurrentFacingOrder.OrderEnum == FacingOrder.FacingOrderEnum.LookAtEnemy || base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation == null || base.Formation.QuerySystem.AveragePosition.DistanceSquared(base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.MedianPosition.GetNavMeshVec3().AsVec2) >= base.Formation.QuerySystem.MissileRangeAdjusted * base.Formation.QuerySystem.MissileRangeAdjusted || (!base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.IsRangedCavalryFormation && this.CurrentFacingOrder.GetDirection(base.Formation, null).DotProduct(vec2) <= MBMath.Lerp(0.5f, 1f, 1f - MBMath.ClampFloat(base.Formation.Width, 1f, 20f) * 0.05f, 1E-05f)))
			{
				this.CurrentFacingOrder = FacingOrder.FacingOrderLookAtDirection(vec2);
			}
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x00021894 File Offset: 0x0001FA94
		public override void TickOccasionally()
		{
			this.CalculateCurrentOrder();
			bool flag = base.Formation.QuerySystem.ClosestEnemyFormation == null || this._mainFormation.QuerySystem.MedianPosition.AsVec2.DistanceSquared(base.Formation.QuerySystem.ClosestEnemyFormation.MedianPosition.AsVec2) <= base.Formation.QuerySystem.AveragePosition.DistanceSquared(base.Formation.QuerySystem.ClosestEnemyFormation.MedianPosition.AsVec2) || base.Formation.QuerySystem.AveragePosition.DistanceSquared(base.CurrentOrder.GetPosition(base.Formation)) <= (this._mainFormation.Depth + base.Formation.Depth) * (this._mainFormation.Depth + base.Formation.Depth) * 0.25f;
			if (flag != this._isFireAtWill)
			{
				this._isFireAtWill = flag;
				base.Formation.FiringOrder = (this._isFireAtWill ? FiringOrder.FiringOrderFireAtWill : FiringOrder.FiringOrderHoldYourFire);
			}
			if (this._mainFormation != null && MathF.Abs(this._mainFormation.Width - base.Formation.Width) > 10f)
			{
				base.Formation.FormOrder = FormOrder.FormOrderCustom(this._mainFormation.Width);
			}
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.FacingOrder = this.CurrentFacingOrder;
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x00021A34 File Offset: 0x0001FC34
		protected override void OnBehaviorActivatedAux()
		{
			this.CalculateCurrentOrder();
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.FacingOrder = this.CurrentFacingOrder;
			base.Formation.ArrangementOrder = ArrangementOrder.ArrangementOrderLine;
			base.Formation.FiringOrder = FiringOrder.FiringOrderFireAtWill;
			base.Formation.FormOrder = FormOrder.FormOrderWide;
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x00021A9C File Offset: 0x0001FC9C
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

		// Token: 0x06000DDE RID: 3550 RVA: 0x00021B14 File Offset: 0x0001FD14
		protected override float GetAiWeight()
		{
			MovementOrder currentOrder = base.CurrentOrder;
			if (currentOrder == MovementOrder.MovementOrderStop)
			{
				this.CalculateCurrentOrder();
			}
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
			float num2 = this._mainFormation.Direction.Normalized().DotProduct((base.Formation.QuerySystem.ClosestEnemyFormation.MedianPosition.AsVec2 - this._mainFormation.QuerySystem.MedianPosition.AsVec2).Normalized());
			float num3 = MBMath.LinearExtrapolation(0.5f, 1.1f, (num2 + 1f) / 2f);
			float value = base.Formation.QuerySystem.AveragePosition.Distance(querySystem.ClosestEnemyFormation.MedianPosition.AsVec2) / querySystem.ClosestEnemyFormation.MovementSpeedMaximum;
			float num4 = MBMath.Lerp(0.5f, 1.2f, (8f - MBMath.ClampFloat(value, 4f, 8f)) / 4f, 1E-05f);
			return num * base.Formation.QuerySystem.MainFormationReliabilityFactor * num3 * num4;
		}

		// Token: 0x04000356 RID: 854
		private Formation _mainFormation;

		// Token: 0x04000357 RID: 855
		private bool _isFireAtWill = true;
	}
}
