using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000174 RID: 372
	public class FormationQuerySystem
	{
		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x060012AF RID: 4783 RVA: 0x00046B48 File Offset: 0x00044D48
		public TeamQuerySystem Team
		{
			get
			{
				return this.Formation.Team.QuerySystem;
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x060012B0 RID: 4784 RVA: 0x00046B5A File Offset: 0x00044D5A
		public float FormationPower
		{
			get
			{
				return this._formationPower.Value;
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x060012B1 RID: 4785 RVA: 0x00046B67 File Offset: 0x00044D67
		public float FormationMeleeFightingPower
		{
			get
			{
				return this._formationMeleeFightingPower.Value;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x060012B2 RID: 4786 RVA: 0x00046B74 File Offset: 0x00044D74
		public Vec2 AveragePosition
		{
			get
			{
				return this._averagePosition.Value;
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x060012B3 RID: 4787 RVA: 0x00046B81 File Offset: 0x00044D81
		public Vec2 CurrentVelocity
		{
			get
			{
				return this._currentVelocity.Value;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x060012B4 RID: 4788 RVA: 0x00046B8E File Offset: 0x00044D8E
		public Vec2 EstimatedDirection
		{
			get
			{
				return this._estimatedDirection.Value;
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x060012B5 RID: 4789 RVA: 0x00046B9B File Offset: 0x00044D9B
		public float EstimatedInterval
		{
			get
			{
				return this._estimatedInterval.Value;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x060012B6 RID: 4790 RVA: 0x00046BA8 File Offset: 0x00044DA8
		public WorldPosition MedianPosition
		{
			get
			{
				return this._medianPosition.Value;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x060012B7 RID: 4791 RVA: 0x00046BB5 File Offset: 0x00044DB5
		public Vec2 AverageAllyPosition
		{
			get
			{
				return this._averageAllyPosition.Value;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x060012B8 RID: 4792 RVA: 0x00046BC2 File Offset: 0x00044DC2
		public float IdealAverageDisplacement
		{
			get
			{
				return this._idealAverageDisplacement.Value;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x060012B9 RID: 4793 RVA: 0x00046BCF File Offset: 0x00044DCF
		public FormationQuerySystem.FormationIntegrityDataGroup FormationIntegrityData
		{
			get
			{
				return this._formationIntegrityData.Value;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x060012BA RID: 4794 RVA: 0x00046BDC File Offset: 0x00044DDC
		public MBList<Agent> LocalAllyUnits
		{
			get
			{
				return this._localAllyUnits.Value;
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x060012BB RID: 4795 RVA: 0x00046BE9 File Offset: 0x00044DE9
		public MBList<Agent> LocalEnemyUnits
		{
			get
			{
				return this._localEnemyUnits.Value;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x060012BC RID: 4796 RVA: 0x00046BF6 File Offset: 0x00044DF6
		public FormationClass MainClass
		{
			get
			{
				return this._mainClass.Value;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x060012BD RID: 4797 RVA: 0x00046C03 File Offset: 0x00044E03
		public float InfantryUnitRatio
		{
			get
			{
				return this._infantryUnitRatio.Value;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x060012BE RID: 4798 RVA: 0x00046C10 File Offset: 0x00044E10
		public float HasShieldUnitRatio
		{
			get
			{
				return this._hasShieldUnitRatio.Value;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x060012BF RID: 4799 RVA: 0x00046C1D File Offset: 0x00044E1D
		public float HasThrowingUnitRatio
		{
			get
			{
				return this._hasThrowingUnitRatio.Value;
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x060012C0 RID: 4800 RVA: 0x00046C2A File Offset: 0x00044E2A
		public float RangedUnitRatio
		{
			get
			{
				return this._rangedUnitRatio.Value;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x060012C1 RID: 4801 RVA: 0x00046C37 File Offset: 0x00044E37
		public int InsideCastleUnitCountIncludingUnpositioned
		{
			get
			{
				return this._insideCastleUnitCountIncludingUnpositioned.Value;
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x060012C2 RID: 4802 RVA: 0x00046C44 File Offset: 0x00044E44
		public int InsideCastleUnitCountPositioned
		{
			get
			{
				return this._insideCastleUnitCountPositioned.Value;
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x060012C3 RID: 4803 RVA: 0x00046C51 File Offset: 0x00044E51
		public float CavalryUnitRatio
		{
			get
			{
				return this._cavalryUnitRatio.Value;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x060012C4 RID: 4804 RVA: 0x00046C5E File Offset: 0x00044E5E
		public float GetCavalryUnitRatioWithoutExpiration
		{
			get
			{
				return this._cavalryUnitRatio.GetCachedValue();
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x060012C5 RID: 4805 RVA: 0x00046C6B File Offset: 0x00044E6B
		public float RangedCavalryUnitRatio
		{
			get
			{
				return this._rangedCavalryUnitRatio.Value;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x060012C6 RID: 4806 RVA: 0x00046C78 File Offset: 0x00044E78
		public float GetRangedCavalryUnitRatioWithoutExpiration
		{
			get
			{
				return this._rangedCavalryUnitRatio.GetCachedValue();
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x060012C7 RID: 4807 RVA: 0x00046C85 File Offset: 0x00044E85
		public bool IsMeleeFormation
		{
			get
			{
				return this._isMeleeFormation.Value;
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x060012C8 RID: 4808 RVA: 0x00046C92 File Offset: 0x00044E92
		public bool IsInfantryFormation
		{
			get
			{
				return this._isInfantryFormation.Value;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x060012C9 RID: 4809 RVA: 0x00046C9F File Offset: 0x00044E9F
		public bool HasShield
		{
			get
			{
				return this._hasShield.Value;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x060012CA RID: 4810 RVA: 0x00046CAC File Offset: 0x00044EAC
		public bool HasThrowing
		{
			get
			{
				return this._hasThrowing.Value;
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x060012CB RID: 4811 RVA: 0x00046CB9 File Offset: 0x00044EB9
		public bool IsRangedFormation
		{
			get
			{
				return this._isRangedFormation.Value;
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x060012CC RID: 4812 RVA: 0x00046CC6 File Offset: 0x00044EC6
		public bool IsCavalryFormation
		{
			get
			{
				return this._isCavalryFormation.Value;
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x060012CD RID: 4813 RVA: 0x00046CD3 File Offset: 0x00044ED3
		public bool IsRangedCavalryFormation
		{
			get
			{
				return this._isRangedCavalryFormation.Value;
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x060012CE RID: 4814 RVA: 0x00046CE0 File Offset: 0x00044EE0
		public float MovementSpeedMaximum
		{
			get
			{
				return this._movementSpeedMaximum.Value;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x060012CF RID: 4815 RVA: 0x00046CED File Offset: 0x00044EED
		public float MovementSpeed
		{
			get
			{
				return this._movementSpeed.Value;
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x060012D0 RID: 4816 RVA: 0x00046CFA File Offset: 0x00044EFA
		public float MaximumMissileRange
		{
			get
			{
				return this._maximumMissileRange.Value;
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x060012D1 RID: 4817 RVA: 0x00046D07 File Offset: 0x00044F07
		public float MissileRangeAdjusted
		{
			get
			{
				return this._missileRangeAdjusted.Value;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x060012D2 RID: 4818 RVA: 0x00046D14 File Offset: 0x00044F14
		public float LocalInfantryUnitRatio
		{
			get
			{
				return this._localInfantryUnitRatio.Value;
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x060012D3 RID: 4819 RVA: 0x00046D21 File Offset: 0x00044F21
		public float LocalRangedUnitRatio
		{
			get
			{
				return this._localRangedUnitRatio.Value;
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x060012D4 RID: 4820 RVA: 0x00046D2E File Offset: 0x00044F2E
		public float LocalCavalryUnitRatio
		{
			get
			{
				return this._localCavalryUnitRatio.Value;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x060012D5 RID: 4821 RVA: 0x00046D3B File Offset: 0x00044F3B
		public float LocalRangedCavalryUnitRatio
		{
			get
			{
				return this._localRangedCavalryUnitRatio.Value;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x060012D6 RID: 4822 RVA: 0x00046D48 File Offset: 0x00044F48
		public float LocalAllyPower
		{
			get
			{
				return this._localAllyPower.Value;
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x060012D7 RID: 4823 RVA: 0x00046D55 File Offset: 0x00044F55
		public float LocalEnemyPower
		{
			get
			{
				return this._localEnemyPower.Value;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x060012D8 RID: 4824 RVA: 0x00046D62 File Offset: 0x00044F62
		public float LocalPowerRatio
		{
			get
			{
				return this._localPowerRatio.Value;
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x060012D9 RID: 4825 RVA: 0x00046D6F File Offset: 0x00044F6F
		public float CasualtyRatio
		{
			get
			{
				return this._casualtyRatio.Value;
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x060012DA RID: 4826 RVA: 0x00046D7C File Offset: 0x00044F7C
		public bool IsUnderRangedAttack
		{
			get
			{
				return this._isUnderRangedAttack.Value;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x060012DB RID: 4827 RVA: 0x00046D89 File Offset: 0x00044F89
		public float UnderRangedAttackRatio
		{
			get
			{
				return this._underRangedAttackRatio.Value;
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x060012DC RID: 4828 RVA: 0x00046D96 File Offset: 0x00044F96
		public float MakingRangedAttackRatio
		{
			get
			{
				return this._makingRangedAttackRatio.Value;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x060012DD RID: 4829 RVA: 0x00046DA3 File Offset: 0x00044FA3
		public Formation MainFormation
		{
			get
			{
				return this._mainFormation.Value;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x060012DE RID: 4830 RVA: 0x00046DB0 File Offset: 0x00044FB0
		public float MainFormationReliabilityFactor
		{
			get
			{
				return this._mainFormationReliabilityFactor.Value;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x060012DF RID: 4831 RVA: 0x00046DBD File Offset: 0x00044FBD
		public Vec2 WeightedAverageEnemyPosition
		{
			get
			{
				return this._weightedAverageEnemyPosition.Value;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x060012E0 RID: 4832 RVA: 0x00046DCA File Offset: 0x00044FCA
		public Agent ClosestEnemyAgent
		{
			get
			{
				return this._closestEnemyAgent.Value;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x060012E1 RID: 4833 RVA: 0x00046DD8 File Offset: 0x00044FD8
		public FormationQuerySystem ClosestEnemyFormation
		{
			get
			{
				if (this._closestEnemyFormation.Value == null || this._closestEnemyFormation.Value.CountOfUnits == 0)
				{
					this._closestEnemyFormation.Expire();
				}
				Formation value = this._closestEnemyFormation.Value;
				if (value == null)
				{
					return null;
				}
				return value.QuerySystem;
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x060012E2 RID: 4834 RVA: 0x00046E28 File Offset: 0x00045028
		public FormationQuerySystem ClosestSignificantlyLargeEnemyFormation
		{
			get
			{
				if (this._closestSignificantlyLargeEnemyFormation.Value == null || this._closestSignificantlyLargeEnemyFormation.Value.CountOfUnits == 0)
				{
					this._closestSignificantlyLargeEnemyFormation.Expire();
				}
				Formation value = this._closestSignificantlyLargeEnemyFormation.Value;
				if (value == null)
				{
					return null;
				}
				return value.QuerySystem;
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x060012E3 RID: 4835 RVA: 0x00046E78 File Offset: 0x00045078
		public FormationQuerySystem FastestSignificantlyLargeEnemyFormation
		{
			get
			{
				if (this._fastestSignificantlyLargeEnemyFormation.Value == null || this._fastestSignificantlyLargeEnemyFormation.Value.CountOfUnits == 0)
				{
					this._fastestSignificantlyLargeEnemyFormation.Expire();
				}
				Formation value = this._fastestSignificantlyLargeEnemyFormation.Value;
				if (value == null)
				{
					return null;
				}
				return value.QuerySystem;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x060012E4 RID: 4836 RVA: 0x00046EC5 File Offset: 0x000450C5
		public Vec2 HighGroundCloseToForeseenBattleGround
		{
			get
			{
				return this._highGroundCloseToForeseenBattleGround.Value;
			}
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x00046ED4 File Offset: 0x000450D4
		public FormationQuerySystem(Formation formation)
		{
			FormationQuerySystem.<>c__DisplayClass162_0 CS$<>8__locals1 = new FormationQuerySystem.<>c__DisplayClass162_0();
			CS$<>8__locals1.formation = formation;
			base..ctor();
			CS$<>8__locals1.<>4__this = this;
			this.Formation = CS$<>8__locals1.formation;
			Mission mission = Mission.Current;
			this._formationPower = new QueryData<float>(new Func<float>(CS$<>8__locals1.formation.GetFormationPower), 2.5f);
			this._formationMeleeFightingPower = new QueryData<float>(new Func<float>(CS$<>8__locals1.formation.GetFormationMeleeFightingPower), 2.5f);
			this._averagePosition = new QueryData<Vec2>(delegate()
			{
				Vec2 vec = (CS$<>8__locals1.formation.CountOfUnitsWithoutDetachedOnes > 1) ? CS$<>8__locals1.formation.GetAveragePositionOfUnits(true, true) : ((CS$<>8__locals1.formation.CountOfUnitsWithoutDetachedOnes > 0) ? CS$<>8__locals1.formation.GetAveragePositionOfUnits(true, false) : CS$<>8__locals1.formation.OrderPosition);
				float currentTime = Mission.Current.CurrentTime;
				float num = currentTime - CS$<>8__locals1.<>4__this._lastAveragePositionCalculateTime;
				if (num > 0f)
				{
					CS$<>8__locals1.<>4__this._currentVelocity.SetValue((vec - CS$<>8__locals1.<>4__this._averagePosition.GetCachedValue()) * (1f / num), currentTime);
				}
				CS$<>8__locals1.<>4__this._lastAveragePositionCalculateTime = currentTime;
				return vec;
			}, 0.05f);
			this._currentVelocity = new QueryData<Vec2>(delegate()
			{
				CS$<>8__locals1.<>4__this._averagePosition.Evaluate(Mission.Current.CurrentTime);
				return CS$<>8__locals1.<>4__this._currentVelocity.GetCachedValue();
			}, 1f);
			this._estimatedDirection = new QueryData<Vec2>(delegate()
			{
				if (CS$<>8__locals1.formation.CountOfUnitsWithoutDetachedOnes > 0)
				{
					Vec2 averagePositionOfUnits = CS$<>8__locals1.formation.GetAveragePositionOfUnits(true, true);
					float num = 0f;
					float num2 = 0f;
					Vec2 orderLocalAveragePosition = CS$<>8__locals1.formation.OrderLocalAveragePosition;
					int num3 = 0;
					foreach (IFormationUnit formationUnit in CS$<>8__locals1.formation.UnitsWithoutLooseDetachedOnes)
					{
						Agent agent = (Agent)formationUnit;
						Vec2? localPositionOfUnitOrDefault = CS$<>8__locals1.formation.Arrangement.GetLocalPositionOfUnitOrDefault(agent);
						if (localPositionOfUnitOrDefault != null)
						{
							Vec2 value = localPositionOfUnitOrDefault.Value;
							Vec2 asVec = agent.Position.AsVec2;
							num += (value.x - orderLocalAveragePosition.x) * (asVec.x - averagePositionOfUnits.x) + (value.y - orderLocalAveragePosition.y) * (asVec.y - averagePositionOfUnits.y);
							num2 += (value.x - orderLocalAveragePosition.x) * (asVec.y - averagePositionOfUnits.y) - (value.y - orderLocalAveragePosition.y) * (asVec.x - averagePositionOfUnits.x);
							num3++;
						}
					}
					if (num3 > 0)
					{
						float num4 = 1f / (float)num3;
						num *= num4;
						num2 *= num4;
						float num5 = MathF.Sqrt(num * num + num2 * num2);
						if (num5 > 0f)
						{
							float num6 = MathF.Acos(MBMath.ClampFloat(num / num5, -1f, 1f));
							Vec2 result = Vec2.FromRotation(num6);
							Vec2 result2 = Vec2.FromRotation(-num6);
							float num7 = 0f;
							float num8 = 0f;
							foreach (IFormationUnit formationUnit2 in CS$<>8__locals1.formation.UnitsWithoutLooseDetachedOnes)
							{
								Agent agent2 = (Agent)formationUnit2;
								Vec2? localPositionOfUnitOrDefault2 = CS$<>8__locals1.formation.Arrangement.GetLocalPositionOfUnitOrDefault(agent2);
								if (localPositionOfUnitOrDefault2 != null)
								{
									Vec2 v = result.TransformToParentUnitF(localPositionOfUnitOrDefault2.Value - orderLocalAveragePosition);
									Vec2 v2 = result2.TransformToParentUnitF(localPositionOfUnitOrDefault2.Value - orderLocalAveragePosition);
									Vec2 asVec2 = agent2.Position.AsVec2;
									num7 += (v - asVec2 + averagePositionOfUnits).LengthSquared;
									num8 += (v2 - asVec2 + averagePositionOfUnits).LengthSquared;
								}
							}
							if (num7 >= num8)
							{
								return result2;
							}
							return result;
						}
					}
				}
				return new Vec2(0f, 1f);
			}, 0.2f);
			this._estimatedInterval = new QueryData<float>(delegate()
			{
				if (CS$<>8__locals1.formation.CountOfUnitsWithoutDetachedOnes > 0)
				{
					Vec2 estimatedDirection = CS$<>8__locals1.formation.QuerySystem.EstimatedDirection;
					Vec2 currentPosition = CS$<>8__locals1.formation.CurrentPosition;
					float num = 0f;
					float num2 = 0f;
					foreach (IFormationUnit formationUnit in CS$<>8__locals1.formation.UnitsWithoutLooseDetachedOnes)
					{
						Agent agent = (Agent)formationUnit;
						Vec2? localPositionOfUnitOrDefault = CS$<>8__locals1.formation.Arrangement.GetLocalPositionOfUnitOrDefault(agent);
						if (localPositionOfUnitOrDefault != null)
						{
							Vec2 v = estimatedDirection.TransformToLocalUnitF(agent.Position.AsVec2 - currentPosition);
							Vec2 va = localPositionOfUnitOrDefault.Value - v;
							Vec2 vb = CS$<>8__locals1.formation.Arrangement.GetLocalPositionOfUnitOrDefaultWithAdjustment(agent, 1f).Value - localPositionOfUnitOrDefault.Value;
							if (vb.IsNonZero())
							{
								float num3 = vb.Normalize();
								float num4 = Vec2.DotProduct(va, vb);
								num += num4 * num3;
								num2 += num3 * num3;
							}
						}
					}
					if (num2 != 0f)
					{
						return Math.Max(0f, -num / num2 + CS$<>8__locals1.formation.Interval);
					}
				}
				return CS$<>8__locals1.formation.Interval;
			}, 0.2f);
			this._medianPosition = new QueryData<WorldPosition>(delegate()
			{
				if (CS$<>8__locals1.formation.CountOfUnitsWithoutDetachedOnes != 0)
				{
					if (CS$<>8__locals1.formation.CountOfUnitsWithoutDetachedOnes != 1)
					{
						return CS$<>8__locals1.formation.GetMedianAgent(true, true, CS$<>8__locals1.<>4__this.AveragePosition).GetWorldPosition();
					}
					return CS$<>8__locals1.formation.GetMedianAgent(true, false, CS$<>8__locals1.<>4__this.AveragePosition).GetWorldPosition();
				}
				else
				{
					if (CS$<>8__locals1.formation.CountOfUnits == 0)
					{
						return CS$<>8__locals1.formation.CreateNewOrderWorldPosition(WorldPosition.WorldPositionEnforcedCache.None);
					}
					if (CS$<>8__locals1.formation.CountOfUnits != 1)
					{
						return CS$<>8__locals1.formation.GetMedianAgent(false, true, CS$<>8__locals1.<>4__this.AveragePosition).GetWorldPosition();
					}
					return CS$<>8__locals1.formation.GetFirstUnit().GetWorldPosition();
				}
			}, 0.05f);
			this._averageAllyPosition = new QueryData<Vec2>(delegate()
			{
				int num = 0;
				Vec2 vec = Vec2.Zero;
				using (List<Team>.Enumerator enumerator = mission.Teams.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.IsFriendOf(CS$<>8__locals1.formation.Team))
						{
							foreach (Formation formation2 in CS$<>8__locals1.<>4__this.Formation.Team.FormationsIncludingSpecialAndEmpty)
							{
								if (formation2.CountOfUnits > 0 && formation2 != CS$<>8__locals1.formation)
								{
									num += formation2.CountOfUnits;
									vec += formation2.GetAveragePositionOfUnits(false, false) * (float)formation2.CountOfUnits;
								}
							}
						}
					}
				}
				if (num > 0)
				{
					return vec * (1f / (float)num);
				}
				return CS$<>8__locals1.<>4__this.AveragePosition;
			}, 5f);
			this._idealAverageDisplacement = new QueryData<float>(() => MathF.Sqrt(CS$<>8__locals1.formation.Width * CS$<>8__locals1.formation.Width * 0.5f * 0.5f + CS$<>8__locals1.formation.Depth * CS$<>8__locals1.formation.Depth * 0.5f * 0.5f) / 2f, 5f);
			this._formationIntegrityData = new QueryData<FormationQuerySystem.FormationIntegrityDataGroup>(delegate()
			{
				FormationQuerySystem.FormationIntegrityDataGroup result;
				if (CS$<>8__locals1.formation.CountOfUnitsWithoutDetachedOnes > 0)
				{
					float num = 0f;
					MBReadOnlyList<IFormationUnit> allUnits = CS$<>8__locals1.formation.Arrangement.GetAllUnits();
					int num2 = 0;
					float distanceBetweenAgentsAdjustment = CS$<>8__locals1.formation.QuerySystem.EstimatedInterval - CS$<>8__locals1.formation.Interval;
					foreach (IFormationUnit formationUnit in allUnits)
					{
						Agent agent = (Agent)formationUnit;
						Vec2? localPositionOfUnitOrDefaultWithAdjustment = CS$<>8__locals1.formation.Arrangement.GetLocalPositionOfUnitOrDefaultWithAdjustment(agent, distanceBetweenAgentsAdjustment);
						if (localPositionOfUnitOrDefaultWithAdjustment != null)
						{
							Vec2 v = CS$<>8__locals1.formation.QuerySystem.EstimatedDirection.TransformToParentUnitF(localPositionOfUnitOrDefaultWithAdjustment.Value) + CS$<>8__locals1.formation.CurrentPosition;
							num2++;
							num += (v - agent.Position.AsVec2).LengthSquared;
						}
					}
					if (num2 > 0)
					{
						float num3 = num / (float)num2 * 4f;
						float num4 = 0f;
						Vec2 vec = Vec2.Zero;
						float num5 = 0f;
						num2 = 0;
						foreach (IFormationUnit formationUnit2 in allUnits)
						{
							Agent agent2 = (Agent)formationUnit2;
							Vec2? localPositionOfUnitOrDefaultWithAdjustment2 = CS$<>8__locals1.formation.Arrangement.GetLocalPositionOfUnitOrDefaultWithAdjustment(agent2, distanceBetweenAgentsAdjustment);
							if (localPositionOfUnitOrDefaultWithAdjustment2 != null)
							{
								float lengthSquared = (CS$<>8__locals1.formation.QuerySystem.EstimatedDirection.TransformToParentUnitF(localPositionOfUnitOrDefaultWithAdjustment2.Value) + CS$<>8__locals1.formation.CurrentPosition - agent2.Position.AsVec2).LengthSquared;
								if (lengthSquared < num3)
								{
									num4 += lengthSquared;
									vec += agent2.AverageVelocity.AsVec2;
									num5 += agent2.MaximumForwardUnlimitedSpeed;
									num2++;
								}
							}
						}
						if (num2 > 0)
						{
							vec *= 1f / (float)num2;
							num4 /= (float)num2;
							num5 /= (float)num2;
							result.AverageVelocityExcludeFarAgents = vec;
							result.DeviationOfPositionsExcludeFarAgents = MathF.Sqrt(num4);
							result.AverageMaxUnlimitedSpeedExcludeFarAgents = num5;
							return result;
						}
					}
				}
				result.AverageVelocityExcludeFarAgents = Vec2.Zero;
				result.DeviationOfPositionsExcludeFarAgents = 0f;
				result.AverageMaxUnlimitedSpeedExcludeFarAgents = 0f;
				return result;
			}, 1f);
			this._localAllyUnits = new QueryData<MBList<Agent>>(() => mission.GetNearbyAllyAgents(CS$<>8__locals1.<>4__this.AveragePosition, 30f, CS$<>8__locals1.formation.Team, CS$<>8__locals1.<>4__this._localAllyUnits.GetCachedValue()), 5f, new MBList<Agent>());
			this._localEnemyUnits = new QueryData<MBList<Agent>>(() => mission.GetNearbyEnemyAgents(CS$<>8__locals1.<>4__this.AveragePosition, 30f, CS$<>8__locals1.formation.Team, CS$<>8__locals1.<>4__this._localEnemyUnits.GetCachedValue()), 5f, new MBList<Agent>());
			this._infantryUnitRatio = new QueryData<float>(delegate()
			{
				if (CS$<>8__locals1.formation.CountOfUnits > 0)
				{
					return (float)CS$<>8__locals1.formation.GetCountOfUnitsBelongingToPhysicalClass(FormationClass.Infantry, false) / (float)CS$<>8__locals1.formation.CountOfUnits;
				}
				return 0f;
			}, 2.5f);
			this._hasShieldUnitRatio = new QueryData<float>(delegate()
			{
				if (CS$<>8__locals1.formation.CountOfUnits > 0)
				{
					return (float)CS$<>8__locals1.formation.GetCountOfUnitsWithCondition(new Func<Agent, bool>(QueryLibrary.HasShield)) / (float)CS$<>8__locals1.formation.CountOfUnits;
				}
				return 0f;
			}, 2.5f);
			this._hasThrowingUnitRatio = new QueryData<float>(delegate()
			{
				if (CS$<>8__locals1.formation.CountOfUnits > 0)
				{
					return (float)CS$<>8__locals1.formation.GetCountOfUnitsWithCondition(new Func<Agent, bool>(QueryLibrary.HasThrown)) / (float)CS$<>8__locals1.formation.CountOfUnits;
				}
				return 0f;
			}, 2.5f);
			this._rangedUnitRatio = new QueryData<float>(delegate()
			{
				if (CS$<>8__locals1.formation.CountOfUnits > 0)
				{
					return (float)CS$<>8__locals1.formation.GetCountOfUnitsBelongingToPhysicalClass(FormationClass.Ranged, false) / (float)CS$<>8__locals1.formation.CountOfUnits;
				}
				return 0f;
			}, 2.5f);
			this._cavalryUnitRatio = new QueryData<float>(delegate()
			{
				if (CS$<>8__locals1.formation.CountOfUnits > 0)
				{
					return (float)CS$<>8__locals1.formation.GetCountOfUnitsBelongingToPhysicalClass(FormationClass.Cavalry, false) / (float)CS$<>8__locals1.formation.CountOfUnits;
				}
				return 0f;
			}, 2.5f);
			this._rangedCavalryUnitRatio = new QueryData<float>(delegate()
			{
				if (CS$<>8__locals1.formation.CountOfUnits > 0)
				{
					return (float)CS$<>8__locals1.formation.GetCountOfUnitsBelongingToPhysicalClass(FormationClass.HorseArcher, false) / (float)CS$<>8__locals1.formation.CountOfUnits;
				}
				return 0f;
			}, 2.5f);
			this._isMeleeFormation = new QueryData<bool>(() => CS$<>8__locals1.<>4__this.InfantryUnitRatio + CS$<>8__locals1.<>4__this.CavalryUnitRatio > CS$<>8__locals1.<>4__this.RangedUnitRatio + CS$<>8__locals1.<>4__this.RangedCavalryUnitRatio, 5f);
			this._isInfantryFormation = new QueryData<bool>(() => CS$<>8__locals1.<>4__this.InfantryUnitRatio >= CS$<>8__locals1.<>4__this.RangedUnitRatio && CS$<>8__locals1.<>4__this.InfantryUnitRatio >= CS$<>8__locals1.<>4__this.CavalryUnitRatio && CS$<>8__locals1.<>4__this.InfantryUnitRatio >= CS$<>8__locals1.<>4__this.RangedCavalryUnitRatio, 5f);
			this._hasShield = new QueryData<bool>(() => CS$<>8__locals1.<>4__this.HasShieldUnitRatio >= 0.4f, 5f);
			this._hasThrowing = new QueryData<bool>(() => CS$<>8__locals1.<>4__this.HasThrowingUnitRatio >= 0.5f, 5f);
			this._isRangedFormation = new QueryData<bool>(() => CS$<>8__locals1.<>4__this.RangedUnitRatio > CS$<>8__locals1.<>4__this.InfantryUnitRatio && CS$<>8__locals1.<>4__this.RangedUnitRatio >= CS$<>8__locals1.<>4__this.CavalryUnitRatio && CS$<>8__locals1.<>4__this.RangedUnitRatio >= CS$<>8__locals1.<>4__this.RangedCavalryUnitRatio, 5f);
			this._isCavalryFormation = new QueryData<bool>(() => CS$<>8__locals1.<>4__this.CavalryUnitRatio > CS$<>8__locals1.<>4__this.InfantryUnitRatio && CS$<>8__locals1.<>4__this.CavalryUnitRatio > CS$<>8__locals1.<>4__this.RangedUnitRatio && CS$<>8__locals1.<>4__this.CavalryUnitRatio >= CS$<>8__locals1.<>4__this.RangedCavalryUnitRatio, 5f);
			this._isRangedCavalryFormation = new QueryData<bool>(() => CS$<>8__locals1.<>4__this.RangedCavalryUnitRatio > CS$<>8__locals1.<>4__this.InfantryUnitRatio && CS$<>8__locals1.<>4__this.RangedCavalryUnitRatio > CS$<>8__locals1.<>4__this.RangedUnitRatio && CS$<>8__locals1.<>4__this.RangedCavalryUnitRatio > CS$<>8__locals1.<>4__this.CavalryUnitRatio, 5f);
			QueryData<float>.SetupSyncGroup(new IQueryData[]
			{
				this._infantryUnitRatio,
				this._hasShieldUnitRatio,
				this._rangedUnitRatio,
				this._cavalryUnitRatio,
				this._rangedCavalryUnitRatio,
				this._isMeleeFormation,
				this._isInfantryFormation,
				this._hasShield,
				this._isRangedFormation,
				this._isCavalryFormation,
				this._isRangedCavalryFormation
			});
			this._movementSpeedMaximum = new QueryData<float>(new Func<float>(CS$<>8__locals1.formation.GetAverageMaximumMovementSpeedOfUnits), 10f);
			this._movementSpeed = new QueryData<float>(new Func<float>(CS$<>8__locals1.formation.GetMovementSpeedOfUnits), 2f);
			this._maximumMissileRange = new QueryData<float>(delegate()
			{
				if (CS$<>8__locals1.formation.CountOfUnits == 0)
				{
					return 0f;
				}
				float maximumRange = 0f;
				CS$<>8__locals1.formation.ApplyActionOnEachUnit(delegate(Agent agent)
				{
					if (agent.MaximumMissileRange > maximumRange)
					{
						maximumRange = agent.MaximumMissileRange;
					}
				}, null);
				return maximumRange;
			}, 10f);
			this._missileRangeAdjusted = new QueryData<float>(delegate()
			{
				if (CS$<>8__locals1.formation.CountOfUnits == 0)
				{
					return 0f;
				}
				float sum = 0f;
				CS$<>8__locals1.formation.ApplyActionOnEachUnit(delegate(Agent agent)
				{
					sum += agent.MissileRangeAdjusted;
				}, null);
				return sum / (float)CS$<>8__locals1.formation.CountOfUnits;
			}, 10f);
			this._localInfantryUnitRatio = new QueryData<float>(delegate()
			{
				if (CS$<>8__locals1.<>4__this.LocalAllyUnits.Count != 0)
				{
					return 1f * (float)CS$<>8__locals1.<>4__this.LocalAllyUnits.Count(new Func<Agent, bool>(QueryLibrary.IsInfantry)) / (float)CS$<>8__locals1.<>4__this.LocalAllyUnits.Count;
				}
				return 0f;
			}, 15f);
			this._localRangedUnitRatio = new QueryData<float>(delegate()
			{
				if (CS$<>8__locals1.<>4__this.LocalAllyUnits.Count != 0)
				{
					return 1f * (float)CS$<>8__locals1.<>4__this.LocalAllyUnits.Count(new Func<Agent, bool>(QueryLibrary.IsRanged)) / (float)CS$<>8__locals1.<>4__this.LocalAllyUnits.Count;
				}
				return 0f;
			}, 15f);
			this._localCavalryUnitRatio = new QueryData<float>(delegate()
			{
				if (CS$<>8__locals1.<>4__this.LocalAllyUnits.Count != 0)
				{
					return 1f * (float)CS$<>8__locals1.<>4__this.LocalAllyUnits.Count(new Func<Agent, bool>(QueryLibrary.IsCavalry)) / (float)CS$<>8__locals1.<>4__this.LocalAllyUnits.Count;
				}
				return 0f;
			}, 15f);
			this._localRangedCavalryUnitRatio = new QueryData<float>(delegate()
			{
				if (CS$<>8__locals1.<>4__this.LocalAllyUnits.Count != 0)
				{
					return 1f * (float)CS$<>8__locals1.<>4__this.LocalAllyUnits.Count(new Func<Agent, bool>(QueryLibrary.IsRangedCavalry)) / (float)CS$<>8__locals1.<>4__this.LocalAllyUnits.Count;
				}
				return 0f;
			}, 15f);
			QueryData<float>.SetupSyncGroup(new IQueryData[]
			{
				this._localInfantryUnitRatio,
				this._localRangedUnitRatio,
				this._localCavalryUnitRatio,
				this._localRangedCavalryUnitRatio
			});
			this._localAllyPower = new QueryData<float>(() => CS$<>8__locals1.<>4__this.LocalAllyUnits.Sum((Agent lau) => lau.CharacterPowerCached), 5f);
			this._localEnemyPower = new QueryData<float>(() => CS$<>8__locals1.<>4__this.LocalEnemyUnits.Sum((Agent leu) => leu.CharacterPowerCached), 5f);
			this._localPowerRatio = new QueryData<float>(() => MBMath.ClampFloat(MathF.Sqrt((CS$<>8__locals1.<>4__this.LocalAllyUnits.Sum((Agent lau) => lau.CharacterPowerCached) + 1f) * 1f / (CS$<>8__locals1.<>4__this.LocalEnemyUnits.Sum((Agent leu) => leu.CharacterPowerCached) + 1f)), 0.5f, 1.75f), 5f);
			this._casualtyRatio = new QueryData<float>(delegate()
			{
				if (CS$<>8__locals1.formation.CountOfUnits == 0)
				{
					return 0f;
				}
				CasualtyHandler missionBehavior = mission.GetMissionBehavior<CasualtyHandler>();
				int num = (missionBehavior != null) ? missionBehavior.GetCasualtyCountOfFormation(CS$<>8__locals1.formation) : 0;
				return 1f - (float)num * 1f / (float)(num + CS$<>8__locals1.formation.CountOfUnits);
			}, 10f);
			this._isUnderRangedAttack = new QueryData<bool>(() => CS$<>8__locals1.formation.GetUnderAttackTypeOfUnits(10f) == Agent.UnderAttackType.UnderRangedAttack, 3f);
			this._underRangedAttackRatio = new QueryData<float>(delegate()
			{
				float currentTime = MBCommon.GetTotalMissionTime();
				int countOfUnitsWithCondition = CS$<>8__locals1.formation.GetCountOfUnitsWithCondition((Agent agent) => currentTime - agent.LastRangedHitTime < 10f);
				if (CS$<>8__locals1.formation.CountOfUnits <= 0)
				{
					return 0f;
				}
				return (float)countOfUnitsWithCondition / (float)CS$<>8__locals1.formation.CountOfUnits;
			}, 3f);
			this._makingRangedAttackRatio = new QueryData<float>(delegate()
			{
				float currentTime = MBCommon.GetTotalMissionTime();
				int countOfUnitsWithCondition = CS$<>8__locals1.formation.GetCountOfUnitsWithCondition((Agent agent) => currentTime - agent.LastRangedAttackTime < 10f);
				if (CS$<>8__locals1.formation.CountOfUnits <= 0)
				{
					return 0f;
				}
				return (float)countOfUnitsWithCondition / (float)CS$<>8__locals1.formation.CountOfUnits;
			}, 3f);
			this._closestEnemyAgent = new QueryData<Agent>(delegate()
			{
				float num = float.MaxValue;
				Agent result = null;
				foreach (Team team in mission.Teams)
				{
					if (team.IsEnemyOf(CS$<>8__locals1.formation.Team))
					{
						foreach (Agent agent in team.ActiveAgents)
						{
							float num2 = agent.Position.DistanceSquared(new Vec3(CS$<>8__locals1.<>4__this.AveragePosition, CS$<>8__locals1.<>4__this.MedianPosition.GetNavMeshZ(), -1f));
							if (num2 < num)
							{
								num = num2;
								result = agent;
							}
						}
					}
				}
				return result;
			}, 1.5f);
			this._closestEnemyFormation = new QueryData<Formation>(delegate()
			{
				float num = float.MaxValue;
				Formation result = null;
				foreach (Team team in mission.Teams)
				{
					if (team.IsEnemyOf(CS$<>8__locals1.formation.Team))
					{
						foreach (Formation formation2 in team.FormationsIncludingSpecialAndEmpty)
						{
							if (formation2.CountOfUnits > 0)
							{
								float num2 = formation2.QuerySystem.MedianPosition.GetNavMeshVec3().DistanceSquared(new Vec3(CS$<>8__locals1.<>4__this.AveragePosition, CS$<>8__locals1.<>4__this.MedianPosition.GetNavMeshZ(), -1f));
								if (num2 < num)
								{
									num = num2;
									result = formation2;
								}
							}
						}
					}
				}
				return result;
			}, 1.5f);
			this._closestSignificantlyLargeEnemyFormation = new QueryData<Formation>(delegate()
			{
				float num = float.MaxValue;
				Formation formation2 = null;
				float num2 = float.MaxValue;
				Formation result = null;
				foreach (Team team in mission.Teams)
				{
					if (team.IsEnemyOf(CS$<>8__locals1.formation.Team))
					{
						foreach (Formation formation3 in team.FormationsIncludingSpecialAndEmpty)
						{
							if (formation3.CountOfUnits > 0)
							{
								if (formation3.QuerySystem.FormationPower / CS$<>8__locals1.<>4__this.FormationPower > 0.2f || formation3.QuerySystem.FormationPower * CS$<>8__locals1.<>4__this.Team.TeamPower / (formation3.Team.QuerySystem.TeamPower * CS$<>8__locals1.<>4__this.FormationPower) > 0.2f)
								{
									float num3 = formation3.QuerySystem.MedianPosition.GetNavMeshVec3().DistanceSquared(new Vec3(CS$<>8__locals1.<>4__this.AveragePosition, CS$<>8__locals1.<>4__this.MedianPosition.GetNavMeshZ(), -1f));
									if (num3 < num)
									{
										num = num3;
										formation2 = formation3;
									}
								}
								else if (formation2 == null)
								{
									float num4 = formation3.QuerySystem.MedianPosition.GetNavMeshVec3().DistanceSquared(new Vec3(CS$<>8__locals1.<>4__this.AveragePosition, CS$<>8__locals1.<>4__this.MedianPosition.GetNavMeshZ(), -1f));
									if (num4 < num2)
									{
										num2 = num4;
										result = formation3;
									}
								}
							}
						}
					}
				}
				if (formation2 != null)
				{
					return formation2;
				}
				return result;
			}, 1.5f);
			this._fastestSignificantlyLargeEnemyFormation = new QueryData<Formation>(delegate()
			{
				float num = float.MaxValue;
				Formation formation2 = null;
				float num2 = float.MaxValue;
				Formation result = null;
				foreach (Team team in mission.Teams)
				{
					if (team.IsEnemyOf(CS$<>8__locals1.formation.Team))
					{
						foreach (Formation formation3 in team.FormationsIncludingSpecialAndEmpty)
						{
							if (formation3.CountOfUnits > 0)
							{
								if (formation3.QuerySystem.FormationPower / CS$<>8__locals1.<>4__this.FormationPower > 0.2f || formation3.QuerySystem.FormationPower * CS$<>8__locals1.<>4__this.Team.TeamPower / (formation3.Team.QuerySystem.TeamPower * CS$<>8__locals1.<>4__this.FormationPower) > 0.2f)
								{
									float num3 = formation3.QuerySystem.MedianPosition.GetNavMeshVec3().DistanceSquared(new Vec3(CS$<>8__locals1.<>4__this.AveragePosition, CS$<>8__locals1.<>4__this.MedianPosition.GetNavMeshZ(), -1f)) / (formation3.QuerySystem.MovementSpeed * formation3.QuerySystem.MovementSpeed);
									if (num3 < num)
									{
										num = num3;
										formation2 = formation3;
									}
								}
								else if (formation2 == null)
								{
									float num4 = formation3.QuerySystem.MedianPosition.GetNavMeshVec3().DistanceSquared(new Vec3(CS$<>8__locals1.<>4__this.AveragePosition, CS$<>8__locals1.<>4__this.MedianPosition.GetNavMeshZ(), -1f)) / (formation3.QuerySystem.MovementSpeed * formation3.QuerySystem.MovementSpeed);
									if (num4 < num2)
									{
										num2 = num4;
										result = formation3;
									}
								}
							}
						}
					}
				}
				if (formation2 != null)
				{
					return formation2;
				}
				return result;
			}, 1.5f);
			this._mainClass = new QueryData<FormationClass>(delegate()
			{
				FormationClass result = FormationClass.Infantry;
				float num = CS$<>8__locals1.<>4__this.InfantryUnitRatio;
				if (CS$<>8__locals1.<>4__this.RangedUnitRatio > num)
				{
					result = FormationClass.Ranged;
					num = CS$<>8__locals1.<>4__this.RangedUnitRatio;
				}
				if (CS$<>8__locals1.<>4__this.CavalryUnitRatio > num)
				{
					result = FormationClass.Cavalry;
					num = CS$<>8__locals1.<>4__this.CavalryUnitRatio;
				}
				if (CS$<>8__locals1.<>4__this.RangedCavalryUnitRatio > num)
				{
					result = FormationClass.HorseArcher;
				}
				return result;
			}, 15f);
			this._mainFormation = new QueryData<Formation>(delegate()
			{
				IEnumerable<Formation> formationsIncludingSpecialAndEmpty = CS$<>8__locals1.formation.Team.FormationsIncludingSpecialAndEmpty;
				Func<Formation, bool> predicate;
				if ((predicate = CS$<>8__locals1.<>9__55) == null)
				{
					predicate = (CS$<>8__locals1.<>9__55 = ((Formation f) => f.CountOfUnits > 0 && f.AI.IsMainFormation && f != CS$<>8__locals1.formation));
				}
				return formationsIncludingSpecialAndEmpty.FirstOrDefault(predicate);
			}, 15f);
			this._mainFormationReliabilityFactor = new QueryData<float>(delegate()
			{
				if (CS$<>8__locals1.<>4__this.MainFormation == null)
				{
					return 0f;
				}
				float num = (CS$<>8__locals1.<>4__this.MainFormation.GetReadonlyMovementOrderReference().OrderEnum == MovementOrder.MovementOrderEnum.Charge || CS$<>8__locals1.<>4__this.MainFormation.GetReadonlyMovementOrderReference().OrderEnum == MovementOrder.MovementOrderEnum.ChargeToTarget || CS$<>8__locals1.<>4__this.MainFormation.GetReadonlyMovementOrderReference() == MovementOrder.MovementOrderRetreat) ? 0.5f : 1f;
				float num2 = (CS$<>8__locals1.<>4__this.MainFormation.GetUnderAttackTypeOfUnits(10f) == Agent.UnderAttackType.UnderMeleeAttack) ? 0.8f : 1f;
				return num * num2;
			}, 5f);
			this._weightedAverageEnemyPosition = new QueryData<Vec2>(() => CS$<>8__locals1.<>4__this.Formation.Team.GetWeightedAverageOfEnemies(CS$<>8__locals1.<>4__this.Formation.CurrentPosition), 0.5f);
			this._highGroundCloseToForeseenBattleGround = new QueryData<Vec2>(delegate()
			{
				WorldPosition medianPosition = CS$<>8__locals1.<>4__this.MedianPosition;
				medianPosition.SetVec2(CS$<>8__locals1.<>4__this.AveragePosition);
				WorldPosition medianTargetFormationPosition = CS$<>8__locals1.<>4__this.Team.MedianTargetFormationPosition;
				return mission.FindPositionWithBiggestSlopeTowardsDirectionInSquare(ref medianPosition, CS$<>8__locals1.<>4__this.AveragePosition.Distance(CS$<>8__locals1.<>4__this.Team.MedianTargetFormationPosition.AsVec2) * 0.5f, ref medianTargetFormationPosition).AsVec2;
			}, 10f);
			this._insideCastleUnitCountIncludingUnpositioned = new QueryData<int>(() => CS$<>8__locals1.<>4__this.Formation.CountUnitsOnNavMeshIDMod10(1, false), 3f);
			this._insideCastleUnitCountPositioned = new QueryData<int>(() => CS$<>8__locals1.<>4__this.Formation.CountUnitsOnNavMeshIDMod10(1, true), 3f);
			this.InitializeTelemetryScopeNames();
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x00047650 File Offset: 0x00045850
		public void EvaluateAllPreliminaryQueryData()
		{
			float currentTime = Mission.Current.CurrentTime;
			this._infantryUnitRatio.Evaluate(currentTime);
			this._hasShieldUnitRatio.Evaluate(currentTime);
			this._rangedUnitRatio.Evaluate(currentTime);
			this._cavalryUnitRatio.Evaluate(currentTime);
			this._rangedCavalryUnitRatio.Evaluate(currentTime);
			this._isInfantryFormation.Evaluate(currentTime);
			this._hasShield.Evaluate(currentTime);
			this._isRangedFormation.Evaluate(currentTime);
			this._isCavalryFormation.Evaluate(currentTime);
			this._isRangedCavalryFormation.Evaluate(currentTime);
			this._isMeleeFormation.Evaluate(currentTime);
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x000476EC File Offset: 0x000458EC
		public void ForceExpireCavalryUnitRatio()
		{
			this._cavalryUnitRatio.Expire();
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x000476FC File Offset: 0x000458FC
		public void Expire()
		{
			this._formationPower.Expire();
			this._formationMeleeFightingPower.Expire();
			this._averagePosition.Expire();
			this._currentVelocity.Expire();
			this._estimatedDirection.Expire();
			this._medianPosition.Expire();
			this._averageAllyPosition.Expire();
			this._idealAverageDisplacement.Expire();
			this._formationIntegrityData.Expire();
			this._localAllyUnits.Expire();
			this._localEnemyUnits.Expire();
			this._mainClass.Expire();
			this._infantryUnitRatio.Expire();
			this._hasShieldUnitRatio.Expire();
			this._rangedUnitRatio.Expire();
			this._cavalryUnitRatio.Expire();
			this._rangedCavalryUnitRatio.Expire();
			this._isMeleeFormation.Expire();
			this._isInfantryFormation.Expire();
			this._hasShield.Expire();
			this._isRangedFormation.Expire();
			this._isCavalryFormation.Expire();
			this._isRangedCavalryFormation.Expire();
			this._movementSpeedMaximum.Expire();
			this._movementSpeed.Expire();
			this._maximumMissileRange.Expire();
			this._missileRangeAdjusted.Expire();
			this._localInfantryUnitRatio.Expire();
			this._localRangedUnitRatio.Expire();
			this._localCavalryUnitRatio.Expire();
			this._localRangedCavalryUnitRatio.Expire();
			this._localAllyPower.Expire();
			this._localEnemyPower.Expire();
			this._localPowerRatio.Expire();
			this._casualtyRatio.Expire();
			this._isUnderRangedAttack.Expire();
			this._underRangedAttackRatio.Expire();
			this._makingRangedAttackRatio.Expire();
			this._mainFormation.Expire();
			this._mainFormationReliabilityFactor.Expire();
			this._weightedAverageEnemyPosition.Expire();
			this._closestEnemyFormation.Expire();
			this._closestSignificantlyLargeEnemyFormation.Expire();
			this._fastestSignificantlyLargeEnemyFormation.Expire();
			this._highGroundCloseToForeseenBattleGround.Expire();
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x000478F8 File Offset: 0x00045AF8
		public void ExpireAfterUnitAddRemove()
		{
			this._formationPower.Expire();
			float currentTime = Mission.Current.CurrentTime;
			this._infantryUnitRatio.Evaluate(currentTime);
			this._hasShieldUnitRatio.Evaluate(currentTime);
			this._rangedUnitRatio.Evaluate(currentTime);
			this._cavalryUnitRatio.Evaluate(currentTime);
			this._rangedCavalryUnitRatio.Evaluate(currentTime);
			this._isMeleeFormation.Evaluate(currentTime);
			this._isInfantryFormation.Evaluate(currentTime);
			this._hasShield.Evaluate(currentTime);
			this._isRangedFormation.Evaluate(currentTime);
			this._isCavalryFormation.Evaluate(currentTime);
			this._isRangedCavalryFormation.Evaluate(currentTime);
			this._mainClass.Evaluate(currentTime);
			if (this.Formation.CountOfUnits == 0)
			{
				this._infantryUnitRatio.SetValue(0f, currentTime);
				this._hasShieldUnitRatio.SetValue(0f, currentTime);
				this._rangedUnitRatio.SetValue(0f, currentTime);
				this._cavalryUnitRatio.SetValue(0f, currentTime);
				this._rangedCavalryUnitRatio.SetValue(0f, currentTime);
				this._isMeleeFormation.SetValue(false, currentTime);
				this._isInfantryFormation.SetValue(true, currentTime);
				this._hasShield.SetValue(false, currentTime);
				this._isRangedFormation.SetValue(false, currentTime);
				this._isCavalryFormation.SetValue(false, currentTime);
				this._isRangedCavalryFormation.SetValue(false, currentTime);
			}
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x00047A5E File Offset: 0x00045C5E
		private void InitializeTelemetryScopeNames()
		{
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x00047A60 File Offset: 0x00045C60
		public Vec2 GetAveragePositionWithMaxAge(float age)
		{
			return this._averagePosition.GetCachedValueWithMaxAge(age);
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x00047A6E File Offset: 0x00045C6E
		public float GetClassWeightedFactor(float infantryWeight, float rangedWeight, float cavalryWeight, float rangedCavalryWeight)
		{
			return this.InfantryUnitRatio * infantryWeight + this.RangedUnitRatio * rangedWeight + this.CavalryUnitRatio * cavalryWeight + this.RangedCavalryUnitRatio * rangedCavalryWeight;
		}

		// Token: 0x040004E7 RID: 1255
		public readonly Formation Formation;

		// Token: 0x040004E8 RID: 1256
		private readonly QueryData<float> _formationPower;

		// Token: 0x040004E9 RID: 1257
		private readonly QueryData<float> _formationMeleeFightingPower;

		// Token: 0x040004EA RID: 1258
		private readonly QueryData<Vec2> _averagePosition;

		// Token: 0x040004EB RID: 1259
		private readonly QueryData<Vec2> _currentVelocity;

		// Token: 0x040004EC RID: 1260
		private float _lastAveragePositionCalculateTime;

		// Token: 0x040004ED RID: 1261
		private readonly QueryData<Vec2> _estimatedDirection;

		// Token: 0x040004EE RID: 1262
		private readonly QueryData<float> _estimatedInterval;

		// Token: 0x040004EF RID: 1263
		private readonly QueryData<WorldPosition> _medianPosition;

		// Token: 0x040004F0 RID: 1264
		private readonly QueryData<Vec2> _averageAllyPosition;

		// Token: 0x040004F1 RID: 1265
		private readonly QueryData<float> _idealAverageDisplacement;

		// Token: 0x040004F2 RID: 1266
		private readonly QueryData<FormationQuerySystem.FormationIntegrityDataGroup> _formationIntegrityData;

		// Token: 0x040004F3 RID: 1267
		private readonly QueryData<MBList<Agent>> _localAllyUnits;

		// Token: 0x040004F4 RID: 1268
		private readonly QueryData<MBList<Agent>> _localEnemyUnits;

		// Token: 0x040004F5 RID: 1269
		private readonly QueryData<FormationClass> _mainClass;

		// Token: 0x040004F6 RID: 1270
		private readonly QueryData<float> _infantryUnitRatio;

		// Token: 0x040004F7 RID: 1271
		private readonly QueryData<float> _hasShieldUnitRatio;

		// Token: 0x040004F8 RID: 1272
		private readonly QueryData<float> _hasThrowingUnitRatio;

		// Token: 0x040004F9 RID: 1273
		private readonly QueryData<float> _rangedUnitRatio;

		// Token: 0x040004FA RID: 1274
		private readonly QueryData<int> _insideCastleUnitCountIncludingUnpositioned;

		// Token: 0x040004FB RID: 1275
		private readonly QueryData<int> _insideCastleUnitCountPositioned;

		// Token: 0x040004FC RID: 1276
		private readonly QueryData<float> _cavalryUnitRatio;

		// Token: 0x040004FD RID: 1277
		private readonly QueryData<float> _rangedCavalryUnitRatio;

		// Token: 0x040004FE RID: 1278
		private readonly QueryData<bool> _isMeleeFormation;

		// Token: 0x040004FF RID: 1279
		private readonly QueryData<bool> _isInfantryFormation;

		// Token: 0x04000500 RID: 1280
		private readonly QueryData<bool> _hasShield;

		// Token: 0x04000501 RID: 1281
		private readonly QueryData<bool> _hasThrowing;

		// Token: 0x04000502 RID: 1282
		private readonly QueryData<bool> _isRangedFormation;

		// Token: 0x04000503 RID: 1283
		private readonly QueryData<bool> _isCavalryFormation;

		// Token: 0x04000504 RID: 1284
		private readonly QueryData<bool> _isRangedCavalryFormation;

		// Token: 0x04000505 RID: 1285
		private readonly QueryData<float> _movementSpeedMaximum;

		// Token: 0x04000506 RID: 1286
		private readonly QueryData<float> _movementSpeed;

		// Token: 0x04000507 RID: 1287
		private readonly QueryData<float> _maximumMissileRange;

		// Token: 0x04000508 RID: 1288
		private readonly QueryData<float> _missileRangeAdjusted;

		// Token: 0x04000509 RID: 1289
		private readonly QueryData<float> _localInfantryUnitRatio;

		// Token: 0x0400050A RID: 1290
		private readonly QueryData<float> _localRangedUnitRatio;

		// Token: 0x0400050B RID: 1291
		private readonly QueryData<float> _localCavalryUnitRatio;

		// Token: 0x0400050C RID: 1292
		private readonly QueryData<float> _localRangedCavalryUnitRatio;

		// Token: 0x0400050D RID: 1293
		private readonly QueryData<float> _localAllyPower;

		// Token: 0x0400050E RID: 1294
		private readonly QueryData<float> _localEnemyPower;

		// Token: 0x0400050F RID: 1295
		private readonly QueryData<float> _localPowerRatio;

		// Token: 0x04000510 RID: 1296
		private readonly QueryData<float> _casualtyRatio;

		// Token: 0x04000511 RID: 1297
		private readonly QueryData<bool> _isUnderRangedAttack;

		// Token: 0x04000512 RID: 1298
		private readonly QueryData<float> _underRangedAttackRatio;

		// Token: 0x04000513 RID: 1299
		private readonly QueryData<float> _makingRangedAttackRatio;

		// Token: 0x04000514 RID: 1300
		private readonly QueryData<Formation> _mainFormation;

		// Token: 0x04000515 RID: 1301
		private readonly QueryData<float> _mainFormationReliabilityFactor;

		// Token: 0x04000516 RID: 1302
		private readonly QueryData<Vec2> _weightedAverageEnemyPosition;

		// Token: 0x04000517 RID: 1303
		private readonly QueryData<Agent> _closestEnemyAgent;

		// Token: 0x04000518 RID: 1304
		private readonly QueryData<Formation> _closestEnemyFormation;

		// Token: 0x04000519 RID: 1305
		private readonly QueryData<Formation> _closestSignificantlyLargeEnemyFormation;

		// Token: 0x0400051A RID: 1306
		private readonly QueryData<Formation> _fastestSignificantlyLargeEnemyFormation;

		// Token: 0x0400051B RID: 1307
		private readonly QueryData<Vec2> _highGroundCloseToForeseenBattleGround;

		// Token: 0x02000496 RID: 1174
		public struct FormationIntegrityDataGroup
		{
			// Token: 0x04001A51 RID: 6737
			public Vec2 AverageVelocityExcludeFarAgents;

			// Token: 0x04001A52 RID: 6738
			public float DeviationOfPositionsExcludeFarAgents;

			// Token: 0x04001A53 RID: 6739
			public float AverageMaxUnlimitedSpeedExcludeFarAgents;
		}
	}
}
