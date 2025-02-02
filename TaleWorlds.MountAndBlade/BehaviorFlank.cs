using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000116 RID: 278
	public class BehaviorFlank : BehaviorComponent
	{
		// Token: 0x06000D7C RID: 3452 RVA: 0x0001E00E File Offset: 0x0001C20E
		public BehaviorFlank(Formation formation) : base(formation)
		{
			base.BehaviorCoherence = 0.5f;
			this.CalculateCurrentOrder();
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x0001E028 File Offset: 0x0001C228
		protected override void CalculateCurrentOrder()
		{
			WorldPosition position = (base.Formation.AI.Side == FormationAI.BehaviorSide.Right) ? base.Formation.QuerySystem.Team.RightFlankEdgePosition : base.Formation.QuerySystem.Team.LeftFlankEdgePosition;
			Vec2 direction = (position.AsVec2 - base.Formation.QuerySystem.AveragePosition).Normalized();
			base.CurrentOrder = MovementOrder.MovementOrderMove(position);
			this.CurrentFacingOrder = FacingOrder.FacingOrderLookAtDirection(direction);
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x0001E0B2 File Offset: 0x0001C2B2
		public override void TickOccasionally()
		{
			this.CalculateCurrentOrder();
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.FacingOrder = this.CurrentFacingOrder;
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x0001E0DC File Offset: 0x0001C2DC
		public override TextObject GetBehaviorString()
		{
			TextObject behaviorString = base.GetBehaviorString();
			behaviorString.SetTextVariable("IS_GENERAL_SIDE", "0");
			TextObject variable = GameTexts.FindText("str_formation_ai_side_strings", base.Formation.AI.Side.ToString());
			behaviorString.SetTextVariable("SIDE_STRING", variable);
			return behaviorString;
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x0001E138 File Offset: 0x0001C338
		protected override void OnBehaviorActivatedAux()
		{
			this.CalculateCurrentOrder();
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.FacingOrder = this.CurrentFacingOrder;
			base.Formation.ArrangementOrder = ArrangementOrder.ArrangementOrderLine;
			base.Formation.FiringOrder = FiringOrder.FiringOrderFireAtWill;
			base.Formation.FormOrder = FormOrder.FormOrderDeep;
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x0001E1A0 File Offset: 0x0001C3A0
		protected override float GetAiWeight()
		{
			FormationQuerySystem querySystem = base.Formation.QuerySystem;
			if (querySystem.ClosestEnemyFormation == null || querySystem.ClosestEnemyFormation.ClosestEnemyFormation == querySystem)
			{
				return 0f;
			}
			Vec2 vec = (querySystem.ClosestEnemyFormation.MedianPosition.AsVec2 - querySystem.AveragePosition).Normalized();
			Vec2 v = (querySystem.ClosestEnemyFormation.ClosestEnemyFormation.MedianPosition.AsVec2 - querySystem.ClosestEnemyFormation.MedianPosition.AsVec2).Normalized();
			if (vec.DotProduct(v) > -0.5f)
			{
				return 0f;
			}
			if (Mission.Current.MissionTeamAIType != Mission.MissionTeamAITypeEnum.FieldBattle)
			{
				int num = -1;
				Vec3 navMeshVec = ((base.Formation.AI.Side == FormationAI.BehaviorSide.Right) ? base.Formation.QuerySystem.Team.RightFlankEdgePosition : base.Formation.QuerySystem.Team.LeftFlankEdgePosition).GetNavMeshVec3();
				Mission.Current.Scene.GetNavigationMeshForPosition(ref navMeshVec, out num, 1.5f);
				if (num >= 0)
				{
					Agent medianAgent = base.Formation.GetMedianAgent(true, true, base.Formation.QuerySystem.AveragePosition);
					if ((medianAgent != null && medianAgent.GetCurrentNavigationFaceId() % 10 == 1) == (num % 10 == 1))
					{
						goto IL_158;
					}
				}
				return 0f;
			}
			IL_158:
			return 1.2f;
		}
	}
}
