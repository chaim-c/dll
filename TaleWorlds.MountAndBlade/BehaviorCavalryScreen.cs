using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.LinQuick;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200010B RID: 267
	public class BehaviorCavalryScreen : BehaviorComponent
	{
		// Token: 0x06000D0A RID: 3338 RVA: 0x0001A45C File Offset: 0x0001865C
		public BehaviorCavalryScreen(Formation formation) : base(formation)
		{
			this._behaviorSide = formation.AI.Side;
			this._mainFormation = formation.Team.FormationsIncludingEmpty.FirstOrDefaultQ((Formation f) => f.CountOfUnits > 0 && f.AI.IsMainFormation);
			this.CalculateCurrentOrder();
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x0001A4BC File Offset: 0x000186BC
		public override void OnValidBehaviorSideChanged()
		{
			base.OnValidBehaviorSideChanged();
			this._mainFormation = base.Formation.Team.FormationsIncludingEmpty.FirstOrDefaultQ((Formation f) => f.CountOfUnits > 0 && f.AI.IsMainFormation);
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x0001A50C File Offset: 0x0001870C
		protected override void CalculateCurrentOrder()
		{
			if (this._mainFormation == null || base.Formation.AI.IsMainFormation || (base.Formation.AI.Side != FormationAI.BehaviorSide.Left && base.Formation.AI.Side != FormationAI.BehaviorSide.Right))
			{
				this._flankingEnemyCavalryFormation = null;
				WorldPosition medianPosition = base.Formation.QuerySystem.MedianPosition;
				medianPosition.SetVec2(base.Formation.QuerySystem.AveragePosition);
				base.CurrentOrder = MovementOrder.MovementOrderMove(medianPosition);
				return;
			}
			float currentTime = Mission.Current.CurrentTime;
			if (this._threatFormationCacheTime + 5f < currentTime)
			{
				this._threatFormationCacheTime = currentTime;
				Vec2 vec = ((base.Formation.AI.Side == FormationAI.BehaviorSide.Left) ? this._mainFormation.Direction.LeftVec() : this._mainFormation.Direction.RightVec()).Normalized() - this._mainFormation.Direction.Normalized();
				this._flankingEnemyCavalryFormation = null;
				float num = float.MinValue;
				foreach (Team team in Mission.Current.Teams)
				{
					if (team.IsEnemyOf(base.Formation.Team))
					{
						foreach (Formation formation in team.FormationsIncludingSpecialAndEmpty)
						{
							if (formation.CountOfUnits > 0)
							{
								Vec2 vec2 = formation.QuerySystem.MedianPosition.AsVec2 - this._mainFormation.QuerySystem.MedianPosition.AsVec2;
								if (vec.Normalized().DotProduct(vec2.Normalized()) > 0.9238795f)
								{
									float formationPower = formation.QuerySystem.FormationPower;
									if (formationPower > num)
									{
										num = formationPower;
										this._flankingEnemyCavalryFormation = formation;
									}
								}
							}
						}
					}
				}
			}
			WorldPosition medianPosition2;
			if (this._flankingEnemyCavalryFormation == null)
			{
				medianPosition2 = base.Formation.QuerySystem.MedianPosition;
				medianPosition2.SetVec2(base.Formation.QuerySystem.AveragePosition);
			}
			else
			{
				Vec2 v = this._flankingEnemyCavalryFormation.QuerySystem.MedianPosition.AsVec2 - this._mainFormation.QuerySystem.MedianPosition.AsVec2;
				float f = v.Normalize() * 0.5f;
				medianPosition2 = this._mainFormation.QuerySystem.MedianPosition;
				medianPosition2.SetVec2(medianPosition2.AsVec2 + f * v);
			}
			base.CurrentOrder = MovementOrder.MovementOrderMove(medianPosition2);
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x0001A7FC File Offset: 0x000189FC
		public override void TickOccasionally()
		{
			this.CalculateCurrentOrder();
			base.Formation.SetMovementOrder(base.CurrentOrder);
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x0001A818 File Offset: 0x00018A18
		protected override void OnBehaviorActivatedAux()
		{
			this.CalculateCurrentOrder();
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.ArrangementOrder = ArrangementOrder.ArrangementOrderSkein;
			base.Formation.FacingOrder = FacingOrder.FacingOrderLookAtEnemy;
			base.Formation.FiringOrder = FiringOrder.FiringOrderFireAtWill;
			base.Formation.FormOrder = FormOrder.FormOrderDeep;
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x0001A87C File Offset: 0x00018A7C
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

		// Token: 0x06000D10 RID: 3344 RVA: 0x0001A92C File Offset: 0x00018B2C
		protected override float GetAiWeight()
		{
			if (this._mainFormation == null || !this._mainFormation.AI.IsMainFormation)
			{
				this._mainFormation = base.Formation.Team.FormationsIncludingEmpty.FirstOrDefaultQ((Formation f) => f.CountOfUnits > 0 && f.AI.IsMainFormation);
			}
			if (this._flankingEnemyCavalryFormation == null)
			{
				return 0f;
			}
			return 1.2f;
		}

		// Token: 0x0400030A RID: 778
		private Formation _mainFormation;

		// Token: 0x0400030B RID: 779
		private Formation _flankingEnemyCavalryFormation;

		// Token: 0x0400030C RID: 780
		private float _threatFormationCacheTime;

		// Token: 0x0400030D RID: 781
		private const float _threatFormationCacheExpireTime = 5f;
	}
}
