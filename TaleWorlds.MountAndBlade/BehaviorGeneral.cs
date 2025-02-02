using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.LinQuick;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000117 RID: 279
	public class BehaviorGeneral : BehaviorComponent
	{
		// Token: 0x06000D82 RID: 3458 RVA: 0x0001E30C File Offset: 0x0001C50C
		public BehaviorGeneral(Formation formation) : base(formation)
		{
			this._mainFormation = formation.Team.FormationsIncludingEmpty.FirstOrDefaultQ((Formation f) => f.CountOfUnits > 0 && f.AI.IsMainFormation);
			this.CalculateCurrentOrder();
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x0001E35C File Offset: 0x0001C55C
		protected override void CalculateCurrentOrder()
		{
			bool flag = false;
			bool flag2 = false;
			foreach (Formation formation in base.Formation.Team.FormationsIncludingEmpty)
			{
				if (formation.CountOfUnits > 0)
				{
					flag = true;
					if (formation.GetReadonlyMovementOrderReference().OrderEnum != MovementOrder.MovementOrderEnum.Retreat)
					{
						flag2 = false;
						break;
					}
					flag2 = true;
				}
			}
			if (!flag)
			{
				base.CurrentOrder = MovementOrder.MovementOrderCharge;
				return;
			}
			if (flag2)
			{
				base.CurrentOrder = MovementOrder.MovementOrderRetreat;
				return;
			}
			bool flag3 = false;
			foreach (Team team in Mission.Current.Teams)
			{
				if (team.IsEnemyOf(base.Formation.Team) && team.HasAnyFormationsIncludingSpecialThatIsNotEmpty())
				{
					flag3 = true;
					break;
				}
			}
			WorldPosition medianPosition;
			if (flag3 && base.Formation.Team.HasAnyFormationsIncludingSpecialThatIsNotEmpty())
			{
				float num = (base.Formation.PhysicalClass.IsMounted() && base.Formation.Team.QuerySystem.CavalryRatio + base.Formation.Team.QuerySystem.RangedCavalryRatio >= 33.3f) ? 40f : 3f;
				if (this._mainFormation != null && this._mainFormation.CountOfUnits > 0)
				{
					float f = this._mainFormation.Depth + num;
					medianPosition = this._mainFormation.QuerySystem.MedianPosition;
					medianPosition.SetVec2(medianPosition.AsVec2 - (base.Formation.QuerySystem.Team.MedianTargetFormationPosition.AsVec2 - this._mainFormation.QuerySystem.MedianPosition.AsVec2).Normalized() * f);
				}
				else
				{
					medianPosition = base.Formation.QuerySystem.Team.MedianPosition;
					medianPosition.SetVec2(base.Formation.QuerySystem.Team.AveragePosition - (base.Formation.QuerySystem.Team.MedianTargetFormationPosition.AsVec2 - base.Formation.QuerySystem.Team.AveragePosition).Normalized() * num);
				}
			}
			else
			{
				medianPosition = base.Formation.QuerySystem.MedianPosition;
				medianPosition.SetVec2(base.Formation.QuerySystem.AveragePosition);
			}
			base.CurrentOrder = MovementOrder.MovementOrderMove(medianPosition);
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x0001E624 File Offset: 0x0001C824
		public override void TickOccasionally()
		{
			this.CalculateCurrentOrder();
			base.Formation.SetMovementOrder(base.CurrentOrder);
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x0001E640 File Offset: 0x0001C840
		protected override void OnBehaviorActivatedAux()
		{
			this.CalculateCurrentOrder();
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.ArrangementOrder = ArrangementOrder.ArrangementOrderLine;
			base.Formation.FacingOrder = FacingOrder.FacingOrderLookAtEnemy;
			base.Formation.FiringOrder = FiringOrder.FiringOrderFireAtWill;
			base.Formation.FormOrder = FormOrder.FormOrderDeep;
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x0001E6A4 File Offset: 0x0001C8A4
		protected override float GetAiWeight()
		{
			if (this._mainFormation == null || !this._mainFormation.AI.IsMainFormation)
			{
				this._mainFormation = base.Formation.Team.FormationsIncludingEmpty.FirstOrDefaultQ((Formation f) => f.CountOfUnits > 0 && f.AI.IsMainFormation);
			}
			return 1.2f;
		}

		// Token: 0x0400033E RID: 830
		private Formation _mainFormation;
	}
}
