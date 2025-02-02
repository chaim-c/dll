using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200012E RID: 302
	public class BehaviorSkirmishBehindFormation : BehaviorComponent
	{
		// Token: 0x06000E2D RID: 3629 RVA: 0x0002474B File Offset: 0x0002294B
		public BehaviorSkirmishBehindFormation(Formation formation) : base(formation)
		{
			this._behaviorSide = formation.AI.Side;
			this.CalculateCurrentOrder();
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x00024774 File Offset: 0x00022974
		protected override void CalculateCurrentOrder()
		{
			Vec2 vec;
			if (base.Formation.QuerySystem.ClosestEnemyFormation != null)
			{
				vec = ((base.Formation.Direction.DotProduct((base.Formation.QuerySystem.ClosestEnemyFormation.MedianPosition.AsVec2 - base.Formation.QuerySystem.AveragePosition).Normalized()) > 0.5f) ? (base.Formation.QuerySystem.ClosestEnemyFormation.MedianPosition.AsVec2 - base.Formation.QuerySystem.AveragePosition) : base.Formation.Direction).Normalized();
			}
			else
			{
				vec = base.Formation.Direction;
			}
			WorldPosition medianPosition;
			if (this.ReferenceFormation == null)
			{
				medianPosition = base.Formation.QuerySystem.MedianPosition;
				medianPosition.SetVec2(base.Formation.QuerySystem.AveragePosition);
			}
			else
			{
				medianPosition = this.ReferenceFormation.QuerySystem.MedianPosition;
				medianPosition.SetVec2(medianPosition.AsVec2 - vec * ((this.ReferenceFormation.Depth + base.Formation.Depth) * 0.5f));
			}
			if (base.CurrentOrder.GetPosition(base.Formation).IsValid)
			{
				base.CurrentOrder = MovementOrder.MovementOrderMove(medianPosition);
			}
			else
			{
				FormationQuerySystem closestSignificantlyLargeEnemyFormation = base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation;
				if ((closestSignificantlyLargeEnemyFormation != null && (!closestSignificantlyLargeEnemyFormation.IsRangedCavalryFormation || base.Formation.QuerySystem.AveragePosition.DistanceSquared(base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.MedianPosition.GetNavMeshVec3().AsVec2) >= closestSignificantlyLargeEnemyFormation.MissileRangeAdjusted * closestSignificantlyLargeEnemyFormation.MissileRangeAdjusted)) || base.CurrentOrder.CreateNewOrderWorldPosition(base.Formation, WorldPosition.WorldPositionEnforcedCache.NavMeshVec3).GetNavMeshVec3().DistanceSquared(medianPosition.GetNavMeshVec3()) >= base.Formation.Depth * base.Formation.Depth)
				{
					base.CurrentOrder = MovementOrder.MovementOrderMove(medianPosition);
				}
			}
			if (!this.CurrentFacingOrder.GetDirection(base.Formation, null).IsValid || this.CurrentFacingOrder.OrderEnum == FacingOrder.FacingOrderEnum.LookAtEnemy || base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation == null || base.Formation.QuerySystem.AveragePosition.DistanceSquared(base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.MedianPosition.GetNavMeshVec3().AsVec2) >= base.Formation.QuerySystem.MissileRangeAdjusted * base.Formation.QuerySystem.MissileRangeAdjusted || (!base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.IsRangedCavalryFormation && this.CurrentFacingOrder.GetDirection(base.Formation, null).DotProduct(vec) <= MBMath.Lerp(0.5f, 1f, 1f - MBMath.ClampFloat(base.Formation.Width, 1f, 20f) * 0.05f, 1E-05f)))
			{
				this.CurrentFacingOrder = FacingOrder.FacingOrderLookAtDirection(vec);
			}
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x00024AD4 File Offset: 0x00022CD4
		public override void TickOccasionally()
		{
			this.CalculateCurrentOrder();
			bool flag = base.Formation.QuerySystem.ClosestEnemyFormation == null || this.ReferenceFormation.QuerySystem.MedianPosition.AsVec2.DistanceSquared(base.Formation.QuerySystem.ClosestEnemyFormation.MedianPosition.AsVec2) <= base.Formation.QuerySystem.AveragePosition.DistanceSquared(base.Formation.QuerySystem.ClosestEnemyFormation.MedianPosition.AsVec2) || base.Formation.QuerySystem.AveragePosition.DistanceSquared(base.CurrentOrder.GetPosition(base.Formation)) <= (this.ReferenceFormation.Depth + base.Formation.Depth) * (this.ReferenceFormation.Depth + base.Formation.Depth) * 0.25f;
			if (flag != this._isFireAtWill)
			{
				this._isFireAtWill = flag;
				if (this._isFireAtWill)
				{
					base.Formation.FiringOrder = FiringOrder.FiringOrderFireAtWill;
				}
				else
				{
					base.Formation.FiringOrder = FiringOrder.FiringOrderHoldYourFire;
				}
			}
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.FacingOrder = this.CurrentFacingOrder;
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x00024C38 File Offset: 0x00022E38
		protected override void OnBehaviorActivatedAux()
		{
			this.CalculateCurrentOrder();
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.FacingOrder = this.CurrentFacingOrder;
			base.Formation.ArrangementOrder = ArrangementOrder.ArrangementOrderLine;
			base.Formation.FiringOrder = FiringOrder.FiringOrderFireAtWill;
			base.Formation.FormOrder = FormOrder.FormOrderWider;
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x00024CA0 File Offset: 0x00022EA0
		public override TextObject GetBehaviorString()
		{
			TextObject behaviorString = base.GetBehaviorString();
			if (this.ReferenceFormation != null)
			{
				behaviorString.SetTextVariable("AI_SIDE", GameTexts.FindText("str_formation_ai_side_strings", this.ReferenceFormation.AI.Side.ToString()));
				behaviorString.SetTextVariable("CLASS", GameTexts.FindText("str_formation_class_string", this.ReferenceFormation.PhysicalClass.GetName()));
			}
			return behaviorString;
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x00024D17 File Offset: 0x00022F17
		protected override float GetAiWeight()
		{
			return 10f;
		}

		// Token: 0x04000370 RID: 880
		public Formation ReferenceFormation;

		// Token: 0x04000371 RID: 881
		private bool _isFireAtWill = true;
	}
}
