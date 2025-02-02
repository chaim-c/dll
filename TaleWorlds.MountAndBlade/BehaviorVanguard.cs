using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.LinQuick;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000135 RID: 309
	public class BehaviorVanguard : BehaviorComponent
	{
		// Token: 0x06000E5C RID: 3676 RVA: 0x00026BC0 File Offset: 0x00024DC0
		public BehaviorVanguard(Formation formation) : base(formation)
		{
			this._behaviorSide = formation.AI.Side;
			this._mainFormation = formation.Team.FormationsIncludingEmpty.FirstOrDefaultQ((Formation f) => f.CountOfUnits > 0 && f.AI.IsMainFormation);
			this.CalculateCurrentOrder();
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x00026C20 File Offset: 0x00024E20
		protected override void CalculateCurrentOrder()
		{
			if (this._mainFormation != null && this._mainFormation.CountOfUnits == 0)
			{
				this._mainFormation = base.Formation.Team.FormationsIncludingEmpty.FirstOrDefaultQ((Formation f) => f.CountOfUnits > 0 && f.AI.IsMainFormation);
			}
			Vec2 direction;
			WorldPosition medianPosition;
			if (this._mainFormation != null)
			{
				direction = this._mainFormation.Direction;
				Vec2 v = (base.Formation.QuerySystem.Team.MedianTargetFormationPosition.AsVec2 - this._mainFormation.QuerySystem.MedianPosition.AsVec2).Normalized();
				medianPosition = this._mainFormation.QuerySystem.MedianPosition;
				medianPosition.SetVec2(this._mainFormation.CurrentPosition + v * ((this._mainFormation.Depth + base.Formation.Depth) * 0.5f + 10f));
			}
			else
			{
				direction = base.Formation.Direction;
				medianPosition = base.Formation.QuerySystem.MedianPosition;
				medianPosition.SetVec2(base.Formation.QuerySystem.AveragePosition);
			}
			base.CurrentOrder = MovementOrder.MovementOrderMove(medianPosition);
			this.CurrentFacingOrder = FacingOrder.FacingOrderLookAtDirection(direction);
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x00026D78 File Offset: 0x00024F78
		public override void OnValidBehaviorSideChanged()
		{
			base.OnValidBehaviorSideChanged();
			this._mainFormation = base.Formation.Team.FormationsIncludingEmpty.FirstOrDefaultQ((Formation f) => f.CountOfUnits > 0 && f.AI.IsMainFormation);
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x00026DC8 File Offset: 0x00024FC8
		public override void TickOccasionally()
		{
			this.CalculateCurrentOrder();
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.FacingOrder = this.CurrentFacingOrder;
			if (base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation != null && base.Formation.QuerySystem.AveragePosition.DistanceSquared(base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.MedianPosition.AsVec2) > 1600f && base.Formation.QuerySystem.UnderRangedAttackRatio > 0.2f - ((base.Formation.ArrangementOrder.OrderEnum == ArrangementOrder.ArrangementOrderEnum.Loose) ? 0.1f : 0f))
			{
				base.Formation.ArrangementOrder = ArrangementOrder.ArrangementOrderLoose;
				return;
			}
			base.Formation.ArrangementOrder = ArrangementOrder.ArrangementOrderLine;
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x00026EA8 File Offset: 0x000250A8
		protected override void OnBehaviorActivatedAux()
		{
			this.CalculateCurrentOrder();
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.FacingOrder = this.CurrentFacingOrder;
			base.Formation.ArrangementOrder = ArrangementOrder.ArrangementOrderLine;
			base.Formation.FiringOrder = FiringOrder.FiringOrderFireAtWill;
			base.Formation.FormOrder = FormOrder.FormOrderWide;
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x00026F10 File Offset: 0x00025110
		public override TextObject GetBehaviorString()
		{
			TextObject behaviorString = base.GetBehaviorString();
			TextObject variable = GameTexts.FindText("str_formation_ai_side_strings", base.Formation.AI.Side.ToString());
			behaviorString.SetTextVariable("SIDE_STRING", variable);
			if (this._mainFormation != null)
			{
				behaviorString.SetTextVariable("AI_SIDE", GameTexts.FindText("str_formation_ai_side_strings", this._mainFormation.AI.Side.ToString()));
				behaviorString.SetTextVariable("CLASS", GameTexts.FindText("str_formation_class_string", this._mainFormation.PhysicalClass.GetName()));
			}
			return behaviorString;
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x00026FC0 File Offset: 0x000251C0
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

		// Token: 0x04000387 RID: 903
		private Formation _mainFormation;
	}
}
