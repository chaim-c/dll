using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000122 RID: 290
	public class BehaviorRetreatToCastle : BehaviorComponent
	{
		// Token: 0x06000DC8 RID: 3528 RVA: 0x00021068 File Offset: 0x0001F268
		public BehaviorRetreatToCastle(Formation formation) : base(formation)
		{
			WorldPosition position = Mission.Current.DeploymentPlan.GetFormationPlan(formation.Team.Side, FormationClass.Cavalry, DeploymentPlanType.Initial).CreateNewDeploymentWorldPosition(WorldPosition.WorldPositionEnforcedCache.GroundVec3);
			base.CurrentOrder = MovementOrder.MovementOrderMove(position);
			base.BehaviorCoherence = 0f;
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x000210B6 File Offset: 0x0001F2B6
		public override void TickOccasionally()
		{
			base.TickOccasionally();
			if (base.Formation.AI.ActiveBehavior == this)
			{
				base.Formation.SetMovementOrder(base.CurrentOrder);
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000DCA RID: 3530 RVA: 0x000210E2 File Offset: 0x0001F2E2
		public override float NavmeshlessTargetPositionPenalty
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x000210E9 File Offset: 0x0001F2E9
		protected override float GetAiWeight()
		{
			return 1f;
		}
	}
}
