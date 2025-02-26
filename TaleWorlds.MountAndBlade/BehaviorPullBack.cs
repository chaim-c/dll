﻿using System;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200011D RID: 285
	public class BehaviorPullBack : BehaviorComponent
	{
		// Token: 0x06000DA3 RID: 3491 RVA: 0x0001FCDD File Offset: 0x0001DEDD
		public BehaviorPullBack(Formation formation) : base(formation)
		{
			this.CalculateCurrentOrder();
			base.BehaviorCoherence = 0.2f;
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x0001FCF8 File Offset: 0x0001DEF8
		protected override void CalculateCurrentOrder()
		{
			WorldPosition medianPosition = base.Formation.QuerySystem.MedianPosition;
			if (base.Formation.QuerySystem.ClosestEnemyFormation == null)
			{
				medianPosition.SetVec2(base.Formation.QuerySystem.AveragePosition);
				base.CurrentOrder = MovementOrder.MovementOrderMove(medianPosition);
				return;
			}
			Vec2 v = (base.Formation.QuerySystem.AveragePosition - base.Formation.QuerySystem.ClosestEnemyFormation.MedianPosition.AsVec2).Normalized();
			medianPosition.SetVec2(base.Formation.QuerySystem.AveragePosition + 50f * v);
			base.CurrentOrder = MovementOrder.MovementOrderMove(medianPosition);
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x0001FDBA File Offset: 0x0001DFBA
		public override void TickOccasionally()
		{
			this.CalculateCurrentOrder();
			base.Formation.SetMovementOrder(base.CurrentOrder);
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x0001FDD4 File Offset: 0x0001DFD4
		protected override void OnBehaviorActivatedAux()
		{
			this.CalculateCurrentOrder();
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.ArrangementOrder = ArrangementOrder.ArrangementOrderLoose;
			base.Formation.FacingOrder = FacingOrder.FacingOrderLookAtEnemy;
			base.Formation.FiringOrder = FiringOrder.FiringOrderFireAtWill;
			base.Formation.FormOrder = FormOrder.FormOrderWide;
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x0001FE38 File Offset: 0x0001E038
		protected override float GetAiWeight()
		{
			if (base.Formation.Team.TeamAI is TeamAISiegeComponent && !(base.Formation.Team.TeamAI is TeamAISallyOutAttacker) && !(base.Formation.Team.TeamAI is TeamAISallyOutDefender))
			{
				return this.GetSiegeAIWeight();
			}
			FormationQuerySystem querySystem = base.Formation.QuerySystem;
			FormationQuerySystem formationQuerySystem = querySystem.ClosestSignificantlyLargeEnemyFormation;
			if (formationQuerySystem == null || formationQuerySystem.ClosestEnemyFormation != querySystem || formationQuerySystem.MovementSpeedMaximum - querySystem.MovementSpeedMaximum > 2f)
			{
				formationQuerySystem = querySystem.ClosestEnemyFormation;
				if (formationQuerySystem == null || formationQuerySystem.ClosestEnemyFormation != querySystem || formationQuerySystem.MovementSpeedMaximum - querySystem.MovementSpeedMaximum > 2f)
				{
					return 0f;
				}
			}
			float num = querySystem.AveragePosition.Distance(formationQuerySystem.MedianPosition.AsVec2) / formationQuerySystem.MovementSpeedMaximum;
			float num2 = MBMath.ClampFloat(num, 4f, 10f);
			float num3 = MBMath.Lerp(0.1f, 1f, 1f - (num2 - 4f) / 6f, 1E-05f);
			float num4 = 0f;
			foreach (Team team in Mission.Current.Teams)
			{
				if (team.IsEnemyOf(base.Formation.Team))
				{
					foreach (Formation formation in team.FormationsIncludingSpecialAndEmpty)
					{
						if (formation.CountOfUnits > 0 && formation != formationQuerySystem.Formation)
						{
							float num5 = formation.QuerySystem.MedianPosition.AsVec2.Distance(formationQuerySystem.MedianPosition.AsVec2) / formation.QuerySystem.MovementSpeedMaximum;
							if (num5 <= num + 4f && (num > 8f || formation.QuerySystem.ClosestEnemyFormation == base.Formation.QuerySystem))
							{
								bool flag = false;
								if (num <= 8f)
								{
									foreach (Team team2 in base.Formation.Team.Mission.Teams)
									{
										if (team2.IsFriendOf(base.Formation.Team))
										{
											foreach (Formation formation2 in team2.FormationsIncludingSpecialAndEmpty)
											{
												if (formation2.CountOfUnits > 0 && formation2 != base.Formation && formation2.QuerySystem.ClosestEnemyFormation == formation.QuerySystem && formation2.QuerySystem.MedianPosition.AsVec2.DistanceSquared(querySystem.AveragePosition) / formation2.QuerySystem.MovementSpeedMaximum < num5 + 4f)
												{
													flag = true;
													break;
												}
											}
											if (flag)
											{
												break;
											}
										}
									}
								}
								if (!flag)
								{
									num4 += formation.QuerySystem.FormationMeleeFightingPower * formation.QuerySystem.GetClassWeightedFactor(1f, 1f, 1f, 1f);
								}
							}
						}
					}
				}
			}
			float num6 = 0f;
			foreach (Team team3 in Mission.Current.Teams)
			{
				if (team3.IsFriendOf(base.Formation.Team))
				{
					foreach (Formation formation3 in team3.FormationsIncludingSpecialAndEmpty)
					{
						if (formation3.CountOfUnits > 0 && formation3 != base.Formation && formation3.QuerySystem.ClosestEnemyFormation == formationQuerySystem && formation3.QuerySystem.MedianPosition.AsVec2.Distance(formation3.QuerySystem.ClosestEnemyFormation.MedianPosition.AsVec2) / formation3.QuerySystem.MovementSpeedMaximum < 4f)
						{
							num6 += formation3.QuerySystem.FormationMeleeFightingPower * formation3.QuerySystem.GetClassWeightedFactor(1f, 1f, 1f, 1f);
						}
					}
				}
			}
			return MBMath.ClampFloat((1f + num4 + formationQuerySystem.Formation.QuerySystem.FormationMeleeFightingPower * formationQuerySystem.GetClassWeightedFactor(1f, 1f, 1f, 1f)) / (base.Formation.GetFormationMeleeFightingPower() * querySystem.GetClassWeightedFactor(1f, 1f, 1f, 1f) + num6 + 1f) * querySystem.Team.RemainingPowerRatio / 3f, 0.1f, 1.21f) * num3;
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x00020400 File Offset: 0x0001E600
		private float GetSiegeAIWeight()
		{
			return 0f;
		}
	}
}
