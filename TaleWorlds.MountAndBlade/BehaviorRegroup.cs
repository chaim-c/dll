using System;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200011E RID: 286
	public class BehaviorRegroup : BehaviorComponent
	{
		// Token: 0x06000DA9 RID: 3497 RVA: 0x00020407 File Offset: 0x0001E607
		public BehaviorRegroup(Formation formation) : base(formation)
		{
			base.BehaviorCoherence = 1f;
			this.CalculateCurrentOrder();
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x00020424 File Offset: 0x0001E624
		protected override void CalculateCurrentOrder()
		{
			Vec2 direction;
			if (base.Formation.QuerySystem.ClosestEnemyFormation != null)
			{
				direction = (base.Formation.QuerySystem.ClosestEnemyFormation.MedianPosition.AsVec2 - base.Formation.QuerySystem.AveragePosition).Normalized();
			}
			else
			{
				direction = base.Formation.Direction;
			}
			WorldPosition medianPosition = base.Formation.QuerySystem.MedianPosition;
			medianPosition.SetVec2(base.Formation.QuerySystem.AveragePosition);
			base.CurrentOrder = MovementOrder.MovementOrderMove(medianPosition);
			this.CurrentFacingOrder = FacingOrder.FacingOrderLookAtDirection(direction);
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x000204CC File Offset: 0x0001E6CC
		public override void TickOccasionally()
		{
			this.CalculateCurrentOrder();
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.FacingOrder = this.CurrentFacingOrder;
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x000204F8 File Offset: 0x0001E6F8
		protected override float GetAiWeight()
		{
			FormationQuerySystem querySystem = base.Formation.QuerySystem;
			if (base.Formation.AI.ActiveBehavior == null)
			{
				return 0f;
			}
			float behaviorCoherence = base.Formation.AI.ActiveBehavior.BehaviorCoherence;
			return MBMath.Lerp(0.1f, 1.2f, MBMath.ClampFloat(behaviorCoherence * (querySystem.FormationIntegrityData.DeviationOfPositionsExcludeFarAgents + 1f) / (querySystem.IdealAverageDisplacement + 1f), 0f, 3f) / 3f, 1E-05f);
		}
	}
}
