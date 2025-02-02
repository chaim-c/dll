using System;
using System.Collections.Generic;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.LinQuick;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200011F RID: 287
	public class BehaviorReserve : BehaviorComponent
	{
		// Token: 0x06000DAD RID: 3501 RVA: 0x00020588 File Offset: 0x0001E788
		public BehaviorReserve(Formation formation) : base(formation)
		{
			this._behaviorSide = formation.AI.Side;
			this.CalculateCurrentOrder();
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x000205A8 File Offset: 0x0001E7A8
		protected override void CalculateCurrentOrder()
		{
			Formation formation = base.Formation.Team.FormationsIncludingEmpty.FirstOrDefaultQ((Formation f) => f.CountOfUnits > 0 && f != base.Formation && f.AI.IsMainFormation);
			WorldPosition position;
			if (formation != null)
			{
				position = formation.QuerySystem.MedianPosition;
				Vec2 v = (base.Formation.QuerySystem.Team.AverageEnemyPosition - formation.QuerySystem.MedianPosition.AsVec2).Normalized();
				position.SetVec2(position.AsVec2 - v * (40f + base.Formation.Depth));
			}
			else
			{
				Vec2 vec = Vec2.Zero;
				int num = 0;
				foreach (Formation formation2 in base.Formation.Team.FormationsIncludingSpecialAndEmpty)
				{
					if (formation2.CountOfUnits > 0 && formation2 != base.Formation)
					{
						vec += formation2.QuerySystem.MedianPosition.AsVec2;
						num++;
					}
				}
				if (num <= 0)
				{
					base.CurrentOrder = MovementOrder.MovementOrderStop;
					return;
				}
				WorldPosition worldPosition = WorldPosition.Invalid;
				float num2 = float.MaxValue;
				vec *= 1f / (float)num;
				foreach (Formation formation3 in base.Formation.Team.FormationsIncludingSpecialAndEmpty)
				{
					if (formation3.CountOfUnits > 0 && formation3 != base.Formation)
					{
						float num3 = vec.DistanceSquared(formation3.QuerySystem.MedianPosition.AsVec2);
						if (num3 < num2)
						{
							num2 = num3;
							worldPosition = formation3.QuerySystem.MedianPosition;
						}
					}
				}
				Vec2 v2 = (base.Formation.QuerySystem.Team.AverageEnemyPosition - worldPosition.AsVec2).Normalized();
				position = worldPosition;
				position.SetVec2(position.AsVec2 - v2 * (20f + base.Formation.Depth));
			}
			base.CurrentOrder = MovementOrder.MovementOrderMove(position);
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x00020804 File Offset: 0x0001EA04
		public override void TickOccasionally()
		{
			this.CalculateCurrentOrder();
			base.Formation.SetMovementOrder(base.CurrentOrder);
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x00020820 File Offset: 0x0001EA20
		protected override void OnBehaviorActivatedAux()
		{
			this.CalculateCurrentOrder();
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.ArrangementOrder = ArrangementOrder.ArrangementOrderLine;
			base.Formation.FacingOrder = FacingOrder.FacingOrderLookAtEnemy;
			base.Formation.FiringOrder = FiringOrder.FiringOrderFireAtWill;
			base.Formation.FormOrder = FormOrder.FormOrderWider;
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x00020884 File Offset: 0x0001EA84
		protected override float GetAiWeight()
		{
			if (!base.Formation.AI.IsMainFormation)
			{
				foreach (Formation formation in base.Formation.Team.FormationsIncludingSpecialAndEmpty)
				{
					if (base.Formation != formation && formation.CountOfUnits > 0)
					{
						using (List<Team>.Enumerator enumerator2 = Mission.Current.Teams.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								Team team = enumerator2.Current;
								if (team.IsEnemyOf(base.Formation.Team))
								{
									using (List<Formation>.Enumerator enumerator3 = team.FormationsIncludingSpecialAndEmpty.GetEnumerator())
									{
										while (enumerator3.MoveNext())
										{
											if (enumerator3.Current.CountOfUnits > 0)
											{
												return 0.04f;
											}
										}
									}
								}
							}
							break;
						}
					}
				}
			}
			return 0f;
		}
	}
}
