using System;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200011A RID: 282
	public class BehaviorMountedSkirmish : BehaviorComponent
	{
		// Token: 0x06000D91 RID: 3473 RVA: 0x0001EEA5 File Offset: 0x0001D0A5
		public BehaviorMountedSkirmish(Formation formation) : base(formation)
		{
			this.CalculateCurrentOrder();
			base.BehaviorCoherence = 0.5f;
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x0001EED0 File Offset: 0x0001D0D0
		protected override void CalculateCurrentOrder()
		{
			WorldPosition medianPosition = base.Formation.QuerySystem.MedianPosition;
			this._isEnemyReachable = (base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation != null && (!(base.Formation.Team.TeamAI is TeamAISiegeComponent) || !TeamAISiegeComponent.IsFormationInsideCastle(base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.Formation, false, 0.4f)));
			if (!this._isEnemyReachable)
			{
				medianPosition.SetVec2(base.Formation.QuerySystem.AveragePosition);
			}
			else
			{
				bool flag = (base.Formation.QuerySystem.AverageAllyPosition - base.Formation.Team.QuerySystem.AverageEnemyPosition).LengthSquared <= 3600f;
				bool engaging = this._engaging;
				if (flag)
				{
					engaging = true;
				}
				else if (!this._engaging)
				{
					engaging = ((base.Formation.QuerySystem.AveragePosition - base.Formation.QuerySystem.AverageAllyPosition).LengthSquared <= 3600f);
				}
				else
				{
					engaging = (base.Formation.QuerySystem.UnderRangedAttackRatio <= base.Formation.QuerySystem.MakingRangedAttackRatio && ((!base.Formation.QuerySystem.FastestSignificantlyLargeEnemyFormation.IsCavalryFormation && !base.Formation.QuerySystem.FastestSignificantlyLargeEnemyFormation.IsRangedCavalryFormation) || (base.Formation.QuerySystem.AveragePosition - base.Formation.QuerySystem.FastestSignificantlyLargeEnemyFormation.MedianPosition.AsVec2).LengthSquared / (base.Formation.QuerySystem.FastestSignificantlyLargeEnemyFormation.MovementSpeed * base.Formation.QuerySystem.FastestSignificantlyLargeEnemyFormation.MovementSpeed) >= 16f));
				}
				this._engaging = engaging;
				if (this._engaging)
				{
					Vec2 vec = (base.Formation.QuerySystem.AveragePosition - base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.AveragePosition).Normalized().LeftVec();
					FormationQuerySystem closestSignificantlyLargeEnemyFormation = base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation;
					float num = 50f + (base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.Formation.Width + base.Formation.Depth) * 0.5f;
					float num2 = 0f;
					Formation formation = base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.Formation;
					foreach (Team team in Mission.Current.Teams)
					{
						if (team.IsEnemyOf(base.Formation.Team))
						{
							foreach (Formation formation2 in team.FormationsIncludingSpecialAndEmpty)
							{
								if (formation2.CountOfUnits > 0 && formation2.QuerySystem != closestSignificantlyLargeEnemyFormation)
								{
									Vec2 v = formation2.QuerySystem.AveragePosition - closestSignificantlyLargeEnemyFormation.AveragePosition;
									float num3 = v.Normalize();
									if (vec.DotProduct(v) > 0.8f && num3 < num && num3 > num2)
									{
										num2 = num3;
										formation = formation2;
									}
								}
							}
						}
					}
					if (!(base.Formation.Team.TeamAI is TeamAISiegeComponent) && base.Formation.QuerySystem.RangedCavalryUnitRatio > 0.95f && base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.Formation == formation)
					{
						base.CurrentOrder = MovementOrder.MovementOrderCharge;
						return;
					}
					bool flag2 = formation.QuerySystem.IsCavalryFormation || formation.QuerySystem.IsRangedCavalryFormation;
					float num4 = flag2 ? 35f : 20f;
					num4 += (formation.Depth + base.Formation.Width) * 0.5f;
					num4 = MathF.Min(num4, base.Formation.QuerySystem.MissileRangeAdjusted - base.Formation.Width * 0.5f);
					BehaviorMountedSkirmish.Ellipse ellipse = new BehaviorMountedSkirmish.Ellipse(formation.QuerySystem.MedianPosition.AsVec2, num4, formation.Width * 0.5f * (flag2 ? 1.5f : 1f), formation.Direction);
					medianPosition.SetVec2(ellipse.GetTargetPos(base.Formation.QuerySystem.AveragePosition, 20f));
				}
				else
				{
					medianPosition = new WorldPosition(Mission.Current.Scene, new Vec3(base.Formation.QuerySystem.AverageAllyPosition, base.Formation.Team.QuerySystem.MedianPosition.GetNavMeshZ() + 100f, -1f));
				}
			}
			base.CurrentOrder = MovementOrder.MovementOrderMove(medianPosition);
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x0001F40C File Offset: 0x0001D60C
		public override void TickOccasionally()
		{
			this.CalculateCurrentOrder();
			base.Formation.SetMovementOrder(base.CurrentOrder);
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x0001F428 File Offset: 0x0001D628
		protected override void OnBehaviorActivatedAux()
		{
			this.CalculateCurrentOrder();
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.ArrangementOrder = ArrangementOrder.ArrangementOrderLine;
			base.Formation.FacingOrder = FacingOrder.FacingOrderLookAtEnemy;
			base.Formation.FiringOrder = FiringOrder.FiringOrderFireAtWill;
			base.Formation.FormOrder = FormOrder.FormOrderDeep;
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x0001F48C File Offset: 0x0001D68C
		protected override float GetAiWeight()
		{
			if (!this._isEnemyReachable)
			{
				return 0.1f;
			}
			return 1f;
		}

		// Token: 0x04000344 RID: 836
		private bool _engaging = true;

		// Token: 0x04000345 RID: 837
		private bool _isEnemyReachable = true;

		// Token: 0x0200040B RID: 1035
		private struct Ellipse
		{
			// Token: 0x06003411 RID: 13329 RVA: 0x000D6837 File Offset: 0x000D4A37
			public Ellipse(Vec2 center, float radius, float halfLength, Vec2 direction)
			{
				this._center = center;
				this._radius = radius;
				this._halfLength = halfLength;
				this._direction = direction;
			}

			// Token: 0x06003412 RID: 13330 RVA: 0x000D6858 File Offset: 0x000D4A58
			public Vec2 GetTargetPos(Vec2 position, float distance)
			{
				Vec2 v = this._direction.LeftVec();
				Vec2 vec = this._center + v * this._halfLength;
				Vec2 vec2 = this._center - v * this._halfLength;
				Vec2 vec3 = position - this._center;
				bool flag = vec3.Normalized().DotProduct(this._direction) > 0f;
				Vec2 v2 = vec3.DotProduct(v) * v;
				bool flag2 = v2.Length < this._halfLength;
				bool flag3 = true;
				if (flag2)
				{
					position = this._center + v2 + this._direction * (this._radius * (float)(flag ? 1 : -1));
				}
				else
				{
					flag3 = (v2.DotProduct(v) > 0f);
					Vec2 v3 = (position - (flag3 ? vec : vec2)).Normalized();
					position = (flag3 ? vec : vec2) + v3 * this._radius;
				}
				Vec2 vec4 = this._center + v2;
				float num = 6.2831855f * this._radius;
				while (distance > 0f)
				{
					if (flag2 && flag)
					{
						float num2 = ((vec - vec4).Length < distance) ? (vec - vec4).Length : distance;
						position = vec4 + (vec - vec4).Normalized() * num2;
						position += this._direction * this._radius;
						distance -= num2;
						flag2 = false;
						flag3 = true;
					}
					else if (!flag2 && flag3)
					{
						Vec2 v4 = (position - vec).Normalized();
						float num3 = MathF.Acos(MBMath.ClampFloat(this._direction.DotProduct(v4), -1f, 1f));
						float num4 = 6.2831855f * (distance / num);
						float num5 = (num3 + num4 < 3.1415927f) ? (num3 + num4) : 3.1415927f;
						float num6 = (num5 - num3) / 3.1415927f * (num / 2f);
						Vec2 direction = this._direction;
						direction.RotateCCW(num5);
						position = vec + direction * this._radius;
						distance -= num6;
						flag2 = true;
						flag = false;
					}
					else if (flag2)
					{
						float num7 = ((vec2 - vec4).Length < distance) ? (vec2 - vec4).Length : distance;
						position = vec4 + (vec2 - vec4).Normalized() * num7;
						position -= this._direction * this._radius;
						distance -= num7;
						flag2 = false;
						flag3 = false;
					}
					else
					{
						Vec2 vec5 = (position - vec2).Normalized();
						float num8 = MathF.Acos(MBMath.ClampFloat(this._direction.DotProduct(vec5), -1f, 1f));
						float num9 = 6.2831855f * (distance / num);
						float num10 = (num8 - num9 > 0f) ? (num8 - num9) : 0f;
						float num11 = num8 - num10;
						float num12 = num11 / 3.1415927f * (num / 2f);
						Vec2 v5 = vec5;
						v5.RotateCCW(num11);
						position = vec2 + v5 * this._radius;
						distance -= num12;
						flag2 = true;
						flag = true;
					}
				}
				return position;
			}

			// Token: 0x040017D4 RID: 6100
			private readonly Vec2 _center;

			// Token: 0x040017D5 RID: 6101
			private readonly float _radius;

			// Token: 0x040017D6 RID: 6102
			private readonly float _halfLength;

			// Token: 0x040017D7 RID: 6103
			private readonly Vec2 _direction;
		}
	}
}
