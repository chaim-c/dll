using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.LinQuick;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000165 RID: 357
	public abstract class TacticComponent
	{
		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x060011F4 RID: 4596 RVA: 0x0003B5A4 File Offset: 0x000397A4
		// (set) Token: 0x060011F5 RID: 4597 RVA: 0x0003B5AC File Offset: 0x000397AC
		public Team Team { get; protected set; }

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x060011F6 RID: 4598 RVA: 0x0003B5B5 File Offset: 0x000397B5
		protected MBList<Formation> FormationsIncludingSpecialAndEmpty
		{
			get
			{
				return this.Team.FormationsIncludingSpecialAndEmpty;
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x060011F7 RID: 4599 RVA: 0x0003B5C2 File Offset: 0x000397C2
		protected MBList<Formation> FormationsIncludingEmpty
		{
			get
			{
				return this.Team.FormationsIncludingEmpty;
			}
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x0003B5CF File Offset: 0x000397CF
		protected TacticComponent(Team team)
		{
			this.Team = team;
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x0003B60D File Offset: 0x0003980D
		protected internal virtual void OnCancel()
		{
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x0003B60F File Offset: 0x0003980F
		protected internal virtual void OnApply()
		{
			this.IsTacticReapplyNeeded = true;
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x0003B618 File Offset: 0x00039818
		protected internal virtual void TickOccasionally()
		{
			TeamAIComponent teamAI = this.Team.TeamAI;
			if (teamAI.GetIsFirstTacticChosen)
			{
				teamAI.OnTacticAppliedForFirstTime();
			}
			if (Mission.Current.IsMissionEnding)
			{
				this.StopUsingAllMachines();
				if (teamAI.HasStrategicAreas)
				{
					teamAI.RemoveAllStrategicAreas();
				}
			}
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x0003B660 File Offset: 0x00039860
		private static void GetUnitCountByAttackType(Formation formation, out int unitCount, out int rangedCount, out int semiRangedCount, out int nonRangedCount)
		{
			FormationQuerySystem querySystem = formation.QuerySystem;
			unitCount = formation.CountOfUnits;
			rangedCount = (int)(querySystem.RangedUnitRatio * (float)unitCount);
			semiRangedCount = 0;
			nonRangedCount = unitCount - rangedCount;
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x0003B694 File Offset: 0x00039894
		protected static float GetFormationGroupEffectivenessOverOrder(IEnumerable<Formation> formationGroup, OrderType orderType, IOrderable targetObject = null)
		{
			int num;
			int num2;
			int num3;
			int num4;
			TacticComponent.GetUnitCountByAttackType(formationGroup.FirstOrDefault<Formation>(), out num, out num2, out num3, out num4);
			if (orderType == OrderType.PointDefence)
			{
				float num5 = ((float)num4 * 0.1f + (float)num3 * 0.3f + (float)num2 * 1f) / (float)num;
				int num6 = (targetObject as IPointDefendable).DefencePoints.Count<DefencePoint>() * 3;
				float num7 = MathF.Min((float)num * 1f / (float)num6, 1f);
				return num5 * num7;
			}
			if (orderType == OrderType.Use)
			{
				float num8 = ((float)num4 * 1f + (float)num3 * 0.9f + (float)num2 * 0.3f) / (float)num;
				int maxUserCount = (targetObject as UsableMachine).MaxUserCount;
				float num9 = MathF.Min((float)num * 1f / (float)maxUserCount, 1f);
				return num8 * num9;
			}
			if (orderType == OrderType.Charge)
			{
				return ((float)num4 * 1f + (float)num3 * 0.9f + (float)num2 * 0.3f) / (float)num;
			}
			return 1f;
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x0003B778 File Offset: 0x00039978
		protected static float GetFormationEffectivenessOverOrder(Formation formation, OrderType orderType, IOrderable targetObject = null)
		{
			int num;
			int num2;
			int num3;
			int num4;
			TacticComponent.GetUnitCountByAttackType(formation, out num, out num2, out num3, out num4);
			if (orderType == OrderType.PointDefence)
			{
				float num5 = ((float)num4 * 0.1f + (float)num3 * 0.3f + (float)num2 * 1f) / (float)num;
				int num6 = (targetObject as IPointDefendable).DefencePoints.Count<DefencePoint>() * 3;
				float num7 = MathF.Min((float)num * 1f / (float)num6, 1f);
				return num5 * num7;
			}
			if (orderType == OrderType.Use)
			{
				float minDistanceSquared = float.MaxValue;
				formation.ApplyActionOnEachUnit(delegate(Agent agent)
				{
					minDistanceSquared = MathF.Min(agent.Position.DistanceSquared((targetObject as UsableMachine).GameEntity.GlobalPosition), minDistanceSquared);
				}, null);
				return 1f / MBMath.ClampFloat(minDistanceSquared, 1f, float.MaxValue);
			}
			if (orderType == OrderType.Charge)
			{
				return ((float)num4 * 1f + (float)num3 * 0.9f + (float)num2 * 0.3f) / (float)num;
			}
			return 1f;
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x0003B871 File Offset: 0x00039A71
		[Conditional("DEBUG")]
		protected internal virtual void DebugTick(float dt)
		{
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x0003B874 File Offset: 0x00039A74
		private static int GetAvailableUnitCount(Formation formation, IEnumerable<ValueTuple<Formation, UsableMachine, int>> appliedCombinations)
		{
			int num = (from c in appliedCombinations
			where c.Item1 == formation
			select c).Sum((ValueTuple<Formation, UsableMachine, int> c) => c.Item3);
			int num2 = 0;
			return formation.CountOfUnits - (num + num2);
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x0003B8D8 File Offset: 0x00039AD8
		private static int GetVacantSlotCount(UsableMachine weapon, IEnumerable<ValueTuple<Formation, UsableMachine, int>> appliedCombinations)
		{
			int num = (from c in appliedCombinations
			where c.Item2 == weapon
			select c).Sum((ValueTuple<Formation, UsableMachine, int> c) => c.Item3);
			return weapon.MaxUserCount - num;
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x0003B938 File Offset: 0x00039B38
		protected List<Formation> ConsolidateFormations(List<Formation> formationsToBeConsolidated, int neededCount)
		{
			if (formationsToBeConsolidated.Count <= neededCount || neededCount <= 0)
			{
				return formationsToBeConsolidated;
			}
			List<Formation> list = (from f in formationsToBeConsolidated
			orderby f.CountOfUnits + ((!f.IsAIControlled) ? 10000 : 0) descending
			select f).ToList<Formation>();
			List<Formation> list2 = list.Take(neededCount).Reverse<Formation>().ToList<Formation>();
			list.RemoveRange(0, neededCount);
			Queue<Formation> queue = new Queue<Formation>(list2);
			List<Formation> list3 = new List<Formation>();
			foreach (Formation formation in list)
			{
				if (!formation.IsAIControlled)
				{
					list3.Add(formation);
				}
				else
				{
					if (queue.IsEmpty<Formation>())
					{
						queue = new Queue<Formation>(list2);
					}
					Formation target = queue.Dequeue();
					formation.TransferUnits(target, formation.CountOfUnits);
				}
			}
			return list2.Concat(list3).ToList<Formation>();
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x0003BA28 File Offset: 0x00039C28
		protected static float CalculateNotEngagingTacticalAdvantage(TeamQuerySystem team)
		{
			float num = team.CavalryRatio + team.RangedCavalryRatio;
			float num2 = team.EnemyCavalryRatio + team.EnemyRangedCavalryRatio;
			return MathF.Pow(MBMath.ClampFloat((num > 0f) ? (num2 / num) : 1.5f, 1f, 1.5f), 1.5f * MathF.Max(num, num2));
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x0003BA84 File Offset: 0x00039C84
		protected void SplitFormationClassIntoGivenNumber(Func<Formation, bool> formationClass, int count)
		{
			List<Formation> list = new List<Formation>();
			List<int> list2 = new List<int>();
			int num = 0;
			List<int> list3 = new List<int>();
			int num2 = 0;
			int i = 0;
			int num3 = 0;
			foreach (Formation formation in this.Team.FormationsIncludingEmpty)
			{
				if (formation.CountOfUnits == 0)
				{
					list.Add(formation);
					i++;
				}
				else if (formationClass(formation))
				{
					list.Add(formation);
					if (formation.IsAIOwned)
					{
						list2.Add(i);
						num += formation.CountOfUnits;
						if (formation.IsConvenientForTransfer)
						{
							list3.Add(i);
							num2 += formation.CountOfUnits;
						}
					}
					else
					{
						num3++;
					}
					i++;
				}
			}
			int num4 = count - num3;
			int count2 = list3.Count;
			int count3 = list2.Count;
			if (count3 > 0)
			{
				List<int> list4 = new List<int>();
				if (num4 > count3 && count2 > 0)
				{
					int num5 = num4 - count3;
					float num6 = (float)num / (float)num4;
					double num7 = Math.Ceiling((double)((float)num2 / (float)(list3.Count + num5)));
					List<int> list5 = new List<int>();
					int num8 = 0;
					for (i = 0; i < count3; i++)
					{
						if (list[list2[i]].IsConvenientForTransfer || (double)list[list2[i]].CountOfUnits <= num7)
						{
							list5.Add(i);
							num8 += list[list2[i]].CountOfUnits;
						}
					}
					double num9 = Math.Ceiling((double)((float)num8 / (float)(list5.Count + num5)));
					List<int> list6 = new List<int>();
					for (i = 0; i < list.Count; i++)
					{
						if (list[i].CountOfUnits == 0 && list6.Count < num5)
						{
							list6.Add(i);
						}
					}
					for (i = 0; i < count2; i++)
					{
						Formation formation2 = list[list3[i]];
						int num10 = (int)((double)formation2.CountOfUnits - num9);
						int num11 = 0;
						while (num10 >= 1 && num11 < list5.Count)
						{
							Formation formation3 = list[list5[num11]];
							if (formation2 != formation3)
							{
								int num12 = (int)(num9 - (double)formation3.CountOfUnits);
								if (num12 >= 1)
								{
									int num13 = MathF.Min(num10, num12);
									formation2.TransferUnits(formation3, num13);
									if (!list4.Contains(list3[i]))
									{
										list4.Add(list3[i]);
									}
									if (!list4.Contains(list5[num11]))
									{
										list4.Add(list5[num11]);
									}
									num10 -= num13;
								}
							}
							num11++;
						}
						if (num10 >= 1)
						{
							int num14 = 0;
							while (num10 >= 1 && num14 < list6.Count)
							{
								Formation formation4 = list[list6[num14]];
								int num15 = (int)(num7 - (double)formation4.CountOfUnits);
								if (num15 >= 1)
								{
									int num16 = MathF.Min(num10, num15);
									formation2.TransferUnits(formation4, num16);
									if (!list4.Contains(list3[i]))
									{
										list4.Add(list3[i]);
									}
									if (!list4.Contains(list6[num14]))
									{
										list4.Add(list6[num14]);
									}
									num10 -= num16;
								}
								num14++;
							}
						}
					}
				}
				else if (num4 < count3 && count3 != 1)
				{
					if (num4 < 1)
					{
						num4 = 1;
					}
					float num17 = (float)num / (float)num4;
					int num18 = 0;
					List<int> list7 = new List<int>();
					int num19 = 0;
					int num20 = 0;
					for (i = 0; i < list2.Count; i++)
					{
						Formation formation5 = list[list2[i]];
						bool flag = false;
						if (!formation5.IsConvenientForTransfer)
						{
							num18++;
							if ((float)formation5.CountOfUnits < num17)
							{
								flag = true;
							}
							else
							{
								num20++;
							}
						}
						else
						{
							flag = true;
						}
						if (flag)
						{
							list7.Add(list2[i]);
							num19 += formation5.CountOfUnits;
						}
					}
					if (num4 < 1)
					{
						num4 = 1;
					}
					else if (num4 < num18)
					{
						num4 = num18;
					}
					float num21 = (float)num19 / (float)(num4 - num20);
					List<int> list8 = new List<int>();
					int num22 = count3 - num4;
					while (list8.Count < num22 && count2 > 0)
					{
						int num23 = int.MaxValue;
						int item = -1;
						for (i = 0; i < list3.Count; i++)
						{
							Formation formation6 = list[list3[i]];
							if (formation6.CountOfUnits < num23)
							{
								num23 = formation6.CountOfUnits;
								item = list3[i];
							}
						}
						list3.Remove(item);
						list8.Add(item);
					}
					for (i = 0; i < list8.Count + list3.Count; i++)
					{
						bool flag2 = i < list8.Count;
						int num24;
						if (flag2)
						{
							num24 = list8[i];
						}
						else
						{
							num24 = list3[i - list8.Count];
						}
						Formation formation7 = list[num24];
						int num25;
						if (flag2)
						{
							num25 = formation7.CountOfUnits;
						}
						else
						{
							num25 = (int)((float)formation7.CountOfUnits - num21);
						}
						int num26 = 0;
						while (num25 >= 1 && num26 < list7.Count)
						{
							Formation formation8 = list[list7[num26]];
							if (formation7 != formation8 && formation8.CountOfUnits != 0)
							{
								int num27 = (int)Math.Ceiling((double)(num21 - (float)formation8.CountOfUnits));
								if (num27 >= 1)
								{
									int num28 = MathF.Min(num25, num27);
									formation7.TransferUnits(formation8, num28);
									if (!list4.Contains(num24))
									{
										list4.Add(num24);
									}
									if (!list4.Contains(list7[num26]))
									{
										list4.Add(list7[num26]);
									}
									num25 -= num28;
								}
							}
							num26++;
						}
					}
				}
				if (num4 > 1 && list4.Count > 1)
				{
					List<Formation> list9 = new List<Formation>();
					for (i = 0; i < list4.Count; i++)
					{
						Formation formation9 = list[list4[i]];
						if (formation9.CountOfUnits > 0)
						{
							formation9.AI.Side = FormationAI.BehaviorSide.BehaviorSideNotSet;
							formation9.AI.IsMainFormation = false;
							formation9.AI.ResetBehaviorWeights();
							TacticComponent.SetDefaultBehaviorWeights(formation9);
							list9.Add(formation9);
						}
					}
					this.IsTacticReapplyNeeded = true;
				}
			}
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x0003C0FC File Offset: 0x0003A2FC
		protected virtual bool CheckAndSetAvailableFormationsChanged()
		{
			return false;
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06001207 RID: 4615 RVA: 0x0003C100 File Offset: 0x0003A300
		protected bool AreFormationsCreated
		{
			get
			{
				if (this._areFormationsCreated)
				{
					return true;
				}
				if (this.FormationsIncludingEmpty.AnyQ((Formation f) => f.CountOfUnits > 0))
				{
					this._areFormationsCreated = true;
					this.ManageFormationCounts();
					this.CheckAndSetAvailableFormationsChanged();
					this.IsTacticReapplyNeeded = true;
					return true;
				}
				return false;
			}
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x0003C161 File Offset: 0x0003A361
		public void ResetTactic()
		{
			this.ManageFormationCounts();
			this.CheckAndSetAvailableFormationsChanged();
			this.IsTacticReapplyNeeded = true;
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x0003C178 File Offset: 0x0003A378
		protected void AssignTacticFormations1121()
		{
			this.ManageFormationCounts(1, 1, 2, 1);
			this._mainInfantry = TacticComponent.ChooseAndSortByPriority(this.FormationsIncludingEmpty, (Formation f) => f.CountOfUnits > 0 && f.QuerySystem.IsInfantryFormation, (Formation f) => f.IsAIControlled, (Formation f) => f.QuerySystem.FormationPower).FirstOrDefault<Formation>();
			if (this._mainInfantry != null)
			{
				this._mainInfantry.AI.IsMainFormation = true;
				this._mainInfantry.AI.Side = FormationAI.BehaviorSide.Middle;
			}
			this._archers = TacticComponent.ChooseAndSortByPriority(this.FormationsIncludingEmpty, (Formation f) => f.CountOfUnits > 0 && f.QuerySystem.IsRangedFormation, (Formation f) => f.IsAIControlled, (Formation f) => f.QuerySystem.FormationPower).FirstOrDefault<Formation>();
			List<Formation> list = TacticComponent.ChooseAndSortByPriority(this.FormationsIncludingEmpty, (Formation f) => f.CountOfUnits > 0 && f.QuerySystem.IsCavalryFormation, (Formation f) => f.IsAIControlled, (Formation f) => f.QuerySystem.FormationPower);
			if (list.Count > 0)
			{
				this._leftCavalry = list[0];
				this._leftCavalry.AI.Side = FormationAI.BehaviorSide.Left;
				if (list.Count > 1)
				{
					this._rightCavalry = list[1];
					this._rightCavalry.AI.Side = FormationAI.BehaviorSide.Right;
				}
				else
				{
					this._rightCavalry = null;
				}
			}
			else
			{
				this._leftCavalry = null;
				this._rightCavalry = null;
			}
			this._rangedCavalry = TacticComponent.ChooseAndSortByPriority(this.FormationsIncludingEmpty, (Formation f) => f.CountOfUnits > 0 && f.QuerySystem.IsRangedCavalryFormation, (Formation f) => f.IsAIControlled, (Formation f) => f.QuerySystem.FormationPower).FirstOrDefault<Formation>();
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x0003C3E4 File Offset: 0x0003A5E4
		protected static List<Formation> ChooseAndSortByPriority(IEnumerable<Formation> formations, Func<Formation, bool> isEligible, Func<Formation, bool> isPrioritized, Func<Formation, float> score)
		{
			formations = formations.Where(isEligible);
			IOrderedEnumerable<Formation> orderedEnumerable = formations.Where(isPrioritized).OrderByDescending(score);
			IOrderedEnumerable<Formation> second = formations.Except(orderedEnumerable).OrderByDescending(score);
			return orderedEnumerable.Concat(second).ToList<Formation>();
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x0003C422 File Offset: 0x0003A622
		protected virtual void ManageFormationCounts()
		{
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x0003C424 File Offset: 0x0003A624
		protected void ManageFormationCounts(int infantryCount, int rangedCount, int cavalryCount, int rangedCavalryCount)
		{
			this.SplitFormationClassIntoGivenNumber((Formation f) => f.QuerySystem.IsInfantryFormation, infantryCount);
			this.SplitFormationClassIntoGivenNumber((Formation f) => f.QuerySystem.IsRangedFormation, rangedCount);
			this.SplitFormationClassIntoGivenNumber((Formation f) => f.QuerySystem.IsCavalryFormation, cavalryCount);
			this.SplitFormationClassIntoGivenNumber((Formation f) => f.QuerySystem.IsRangedCavalryFormation, rangedCavalryCount);
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x0003C4CC File Offset: 0x0003A6CC
		protected virtual void StopUsingAllMachines()
		{
			TeamAISiegeComponent teamAISiegeComponent;
			if ((teamAISiegeComponent = (this.Team.TeamAI as TeamAISiegeComponent)) != null)
			{
				foreach (SiegeWeapon siegeWeapon in teamAISiegeComponent.SceneSiegeWeapons)
				{
					if (siegeWeapon.Side == this.Team.Side)
					{
						siegeWeapon.SetForcedUse(false);
						for (int i = siegeWeapon.UserFormations.Count - 1; i >= 0; i--)
						{
							Formation formation = siegeWeapon.UserFormations[i];
							if (formation.Team == this.Team)
							{
								formation.StopUsingMachine(siegeWeapon, false);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x0003C588 File Offset: 0x0003A788
		protected void StopUsingAllRangedSiegeWeapons()
		{
			foreach (Formation formation in this.FormationsIncludingSpecialAndEmpty)
			{
				if (formation.CountOfUnits > 0)
				{
					for (int i = formation.Detachments.Count - 1; i >= 0; i--)
					{
						RangedSiegeWeapon rangedSiegeWeapon;
						if ((rangedSiegeWeapon = (formation.Detachments[i] as RangedSiegeWeapon)) != null)
						{
							formation.StopUsingMachine(rangedSiegeWeapon, false);
							rangedSiegeWeapon.SetForcedUse(false);
						}
					}
				}
			}
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x0003C61C File Offset: 0x0003A81C
		protected void SoundTacticalHorn(int soundCode)
		{
			float currentTime = Mission.Current.CurrentTime;
			if (currentTime > this._hornCooldownExpireTime)
			{
				this._hornCooldownExpireTime = currentTime + 10f;
				SoundEvent.PlaySound2D(soundCode);
			}
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x0003C654 File Offset: 0x0003A854
		public static void SetDefaultBehaviorWeights(Formation f)
		{
			f.AI.SetBehaviorWeight<BehaviorCharge>(1f);
			f.AI.SetBehaviorWeight<BehaviorPullBack>(1f);
			f.AI.SetBehaviorWeight<BehaviorStop>(1f);
			f.AI.SetBehaviorWeight<BehaviorReserve>(1f);
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x0003C6A5 File Offset: 0x0003A8A5
		protected internal virtual float GetTacticWeight()
		{
			return 0f;
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x0003C6AC File Offset: 0x0003A8AC
		protected bool CheckAndDetermineFormation(ref Formation formation, Func<Formation, bool> isEligible)
		{
			if (formation != null && formation.CountOfUnits != 0 && isEligible(formation))
			{
				return true;
			}
			List<Formation> list = this.FormationsIncludingEmpty.Where(isEligible).ToList<Formation>();
			if (list.Count > 0)
			{
				formation = list.MaxBy((Formation f) => f.CountOfUnits);
				this.IsTacticReapplyNeeded = true;
				return true;
			}
			if (formation != null)
			{
				formation = null;
				this.IsTacticReapplyNeeded = true;
			}
			return false;
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x0003C72C File Offset: 0x0003A92C
		protected internal virtual bool ResetTacticalPositions()
		{
			return false;
		}

		// Token: 0x0400049E RID: 1182
		public static readonly int MoveHornSoundIndex = SoundEvent.GetEventIdFromString("event:/ui/mission/horns/move");

		// Token: 0x0400049F RID: 1183
		public static readonly int AttackHornSoundIndex = SoundEvent.GetEventIdFromString("event:/ui/mission/horns/attack");

		// Token: 0x040004A0 RID: 1184
		public static readonly int RetreatHornSoundIndex = SoundEvent.GetEventIdFromString("event:/ui/mission/horns/retreat");

		// Token: 0x040004A2 RID: 1186
		protected int _AIControlledFormationCount;

		// Token: 0x040004A3 RID: 1187
		protected bool IsTacticReapplyNeeded;

		// Token: 0x040004A4 RID: 1188
		private bool _areFormationsCreated;

		// Token: 0x040004A5 RID: 1189
		protected Formation _mainInfantry;

		// Token: 0x040004A6 RID: 1190
		protected Formation _archers;

		// Token: 0x040004A7 RID: 1191
		protected Formation _leftCavalry;

		// Token: 0x040004A8 RID: 1192
		protected Formation _rightCavalry;

		// Token: 0x040004A9 RID: 1193
		protected Formation _rangedCavalry;

		// Token: 0x040004AA RID: 1194
		private float _hornCooldownExpireTime;

		// Token: 0x040004AB RID: 1195
		private const float HornCooldownTime = 10f;
	}
}
