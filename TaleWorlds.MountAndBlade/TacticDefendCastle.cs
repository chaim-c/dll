using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000167 RID: 359
	public class TacticDefendCastle : TacticComponent
	{
		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x0600121D RID: 4637 RVA: 0x0003CDE8 File Offset: 0x0003AFE8
		// (set) Token: 0x0600121E RID: 4638 RVA: 0x0003CDF0 File Offset: 0x0003AFF0
		public TacticDefendCastle.TacticState CurrentTacticState { get; private set; }

		// Token: 0x0600121F RID: 4639 RVA: 0x0003CDFC File Offset: 0x0003AFFC
		public TacticDefendCastle(Team team) : base(team)
		{
			Mission mission = Mission.Current;
			this._castleKeyPositions = new List<MissionObject>();
			IEnumerable<CastleGate> enumerable = mission.ActiveMissionObjects.FindAllWithType<CastleGate>();
			IEnumerable<WallSegment> collection = mission.ActiveMissionObjects.FindAllWithType<WallSegment>();
			enumerable.FirstOrDefault<CastleGate>();
			this._castleKeyPositions.AddRange(enumerable);
			this._castleKeyPositions.AddRange(collection);
			this._teamAISiegeDefender = (team.TeamAI as TeamAISiegeDefender);
			this._lanes = TeamAISiegeComponent.SiegeLanes;
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x0003CE88 File Offset: 0x0003B088
		private static float GetFormationSallyOutPower(Formation formation)
		{
			if (formation.CountOfUnits > 0)
			{
				float typeMultiplier = formation.PhysicalClass.IsMeleeCavalry() ? 2f : (formation.PhysicalClass.IsRanged() ? 0.3f : 1f);
				float sum = 0f;
				formation.ApplyActionOnEachUnit(delegate(Agent agent)
				{
					sum += agent.CharacterPowerCached * typeMultiplier;
				}, null);
				return sum;
			}
			return 0f;
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x0003CF04 File Offset: 0x0003B104
		private Formation GetStrongestSallyOutFormation()
		{
			float num = 0f;
			Formation result = null;
			foreach (Formation formation in this._meleeFormations)
			{
				if (TeamAISiegeComponent.IsFormationInsideCastle(formation, true, 0.4f))
				{
					float formationSallyOutPower = TacticDefendCastle.GetFormationSallyOutPower(formation);
					if (formationSallyOutPower > num)
					{
						result = formation;
						num = formationSallyOutPower;
					}
				}
			}
			return result;
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x0003CF7C File Offset: 0x0003B17C
		private bool MustRetreatToCastle()
		{
			return false;
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x0003CF80 File Offset: 0x0003B180
		private bool IsSallyOutApplicable()
		{
			float num = base.FormationsIncludingEmpty.Sum((Formation formation) => TacticDefendCastle.GetFormationSallyOutPower(formation));
			float num2 = 0f;
			foreach (Team team in Mission.Current.Teams)
			{
				if (team.Side.GetOppositeSide() == BattleSideEnum.Defender)
				{
					foreach (Formation formation2 in team.FormationsIncludingSpecialAndEmpty)
					{
						if (formation2.CountOfUnits > 0)
						{
							num2 += TacticDefendCastle.GetFormationSallyOutPower(formation2);
						}
					}
				}
			}
			return num > num2 * 3f && base.Team.QuerySystem.RemainingPowerRatio / this._startingPowerRatio > 3f;
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x0003D08C File Offset: 0x0003B28C
		private void BalanceLaneDefenders(List<Formation> defenderFormations, out bool transferOccurred)
		{
			transferOccurred = false;
			int num = 3;
			SiegeLane[] array = new SiegeLane[num];
			int i;
			int n;
			for (i = 0; i < num; i = n + 1)
			{
				array[i] = this._lanes.FirstOrDefault((SiegeLane l) => l.LaneSide == (FormationAI.BehaviorSide)i);
				n = i;
			}
			float[] array2 = new float[num];
			for (int j = 0; j < array.Length; j++)
			{
				SiegeLane siegeLane = array[j];
				array2[j] = ((siegeLane == null || siegeLane.GetDefenseState() == SiegeLane.LaneDefenseStates.Token) ? 0f : siegeLane.CalculateLaneCapacity());
			}
			float num2 = array2.Sum();
			float[] array3 = new float[num];
			for (int k = 0; k < num; k++)
			{
				array3[k] = array2[k] / num2;
			}
			int num3 = array.Count((SiegeLane l) => l != null && l.GetDefenseState() == SiegeLane.LaneDefenseStates.Token);
			int num4 = 15;
			int num5 = num3 * 15;
			int num6 = defenderFormations.Sum((Formation f) => f.CountOfUnitsWithoutDetachedOnes);
			int num7 = num6 - num5;
			IEnumerable<float> source = from ltp in array3
			where ltp > 0f
			select ltp;
			if (source.Any<float>() && (float)num7 * source.Min() <= (float)num4)
			{
				num7 = num6;
				num4 = MathF.Max((int)((float)num7 * 0.1f), 1);
			}
			int[] array4 = new int[num];
			for (int m = 0; m < num; m++)
			{
				int num8 = (int)(array3[m] * (float)num7);
				array4[m] = ((num8 == 0) ? num4 : num8);
			}
			int[] array5 = new int[num];
			foreach (Formation formation in defenderFormations)
			{
				int side = (int)formation.AI.Side;
				array5[side] = formation.UnitsWithoutLooseDetachedOnes.Count - array4[side];
			}
			int num9 = MathF.Max((int)((float)defenderFormations.Sum((Formation df) => df.CountOfUnits) * 0.2f), 1);
			using (List<Formation>.Enumerator enumerator = defenderFormations.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Formation receiverDefenderFormation = enumerator.Current;
					int side2 = (int)receiverDefenderFormation.AI.Side;
					if (array5[side2] < -num9)
					{
						Func<Formation, bool> predicate;
						Func<Formation, bool> <>9__5;
						if ((predicate = <>9__5) == null)
						{
							predicate = (<>9__5 = ((Formation df) => df != receiverDefenderFormation));
						}
						foreach (Formation formation2 in defenderFormations.Where(predicate))
						{
							int side3 = (int)formation2.AI.Side;
							if (array5[side3] > num9)
							{
								int num10 = MathF.Min(-array5[side2], array5[side3]);
								array5[side2] += num10;
								array5[side3] -= num10;
								formation2.TransferUnits(receiverDefenderFormation, num10);
								transferOccurred = true;
								if (array5[side2] == 0)
								{
									break;
								}
							}
						}
						if (!transferOccurred)
						{
							break;
						}
					}
				}
			}
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x0003D438 File Offset: 0x0003B638
		private void ArcherShiftAround(List<Formation> p_RangedFormations)
		{
			List<Formation> list = (from rf in p_RangedFormations
			where rf.AI.ActiveBehavior is BehaviorShootFromCastleWalls
			select rf).ToList<Formation>();
			if (list.Count < 2)
			{
				return;
			}
			float smallerFormationUnitPercentage = 0.1f;
			float mediumFormationUnitPercentage = 0.2f;
			float largerFormationUnitPercentage = 0.4f;
			float num = list.Sum(delegate(Formation f)
			{
				if ((f.AI.ActiveBehavior as BehaviorShootFromCastleWalls).ArcherPosition.HasTag("many"))
				{
					return largerFormationUnitPercentage;
				}
				if (!(f.AI.ActiveBehavior as BehaviorShootFromCastleWalls).ArcherPosition.HasTag("few"))
				{
					return mediumFormationUnitPercentage;
				}
				return smallerFormationUnitPercentage;
			});
			smallerFormationUnitPercentage /= num;
			mediumFormationUnitPercentage /= num;
			largerFormationUnitPercentage /= num;
			int num2 = list.Sum((Formation f) => f.CountOfUnitsWithoutDetachedOnes);
			int smallFormationCount = MathF.Max((int)((float)num2 * smallerFormationUnitPercentage), 1);
			int mediumFormationCount = MathF.Max((int)((float)num2 * mediumFormationUnitPercentage), 1);
			int largeFormationCount = MathF.Max((int)((float)num2 * largerFormationUnitPercentage), 1);
			int num3 = MathF.Max((int)((float)num2 * 0.1f), 1);
			Func<Formation, int> <>9__4;
			Func<Formation, bool> <>9__3;
			foreach (Formation formation in list)
			{
				int num4 = (formation.AI.ActiveBehavior as BehaviorShootFromCastleWalls).ArcherPosition.HasTag("many") ? largeFormationCount : ((formation.AI.ActiveBehavior as BehaviorShootFromCastleWalls).ArcherPosition.HasTag("few") ? smallFormationCount : mediumFormationCount);
				int num5 = 0;
				while (num4 - formation.CountOfUnitsWithoutDetachedOnes > num3)
				{
					IEnumerable<Formation> source = list;
					Func<Formation, bool> predicate;
					if ((predicate = <>9__3) == null)
					{
						predicate = (<>9__3 = ((Formation rf) => rf.CountOfUnitsWithoutDetachedOnes > ((rf.AI.ActiveBehavior as BehaviorShootFromCastleWalls).ArcherPosition.HasTag("many") ? largeFormationCount : ((rf.AI.ActiveBehavior as BehaviorShootFromCastleWalls).ArcherPosition.HasTag("few") ? smallFormationCount : mediumFormationCount))));
					}
					if (!source.Any(predicate) || num5 >= list.Count)
					{
						break;
					}
					int num6 = num4 - formation.CountOfUnitsWithoutDetachedOnes;
					IEnumerable<Formation> source2 = list;
					Func<Formation, int> selector;
					if ((selector = <>9__4) == null)
					{
						selector = (<>9__4 = ((Formation rf) => rf.CountOfUnitsWithoutDetachedOnes - ((rf.AI.ActiveBehavior as BehaviorShootFromCastleWalls).ArcherPosition.HasTag("many") ? largeFormationCount : ((rf.AI.ActiveBehavior as BehaviorShootFromCastleWalls).ArcherPosition.HasTag("few") ? smallFormationCount : mediumFormationCount))));
					}
					Formation formation2 = source2.MaxBy(selector);
					num6 = MathF.Min(num6, formation2.CountOfUnitsWithoutDetachedOnes - ((formation2.AI.ActiveBehavior as BehaviorShootFromCastleWalls).ArcherPosition.HasTag("many") ? largeFormationCount : ((formation2.AI.ActiveBehavior as BehaviorShootFromCastleWalls).ArcherPosition.HasTag("few") ? smallFormationCount : mediumFormationCount)));
					formation2.TransferUnits(formation, num6);
					num5++;
				}
			}
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x0003D700 File Offset: 0x0003B900
		protected override bool CheckAndSetAvailableFormationsChanged()
		{
			bool flag = false;
			if (this._laneCount != this._lanes.Count)
			{
				this._laneCount = this._lanes.Count;
				flag = true;
			}
			int aicontrolledFormationCount = base.Team.GetAIControlledFormationCount();
			bool flag2 = aicontrolledFormationCount != this._AIControlledFormationCount;
			if (flag2)
			{
				this._AIControlledFormationCount = aicontrolledFormationCount;
				this.IsTacticReapplyNeeded = true;
			}
			return flag || flag2;
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x0003D762 File Offset: 0x0003B962
		private int GetRequiredMeleeDefenderCount()
		{
			return this._lanes.Count(delegate(SiegeLane l)
			{
				if (!l.IsOpen)
				{
					if (!l.PrimarySiegeWeapons.Any((IPrimarySiegeWeapon lsw) => lsw is SiegeLadder))
					{
						return l.PrimarySiegeWeapons.Any((IPrimarySiegeWeapon psw) => psw.HasCompletedAction() || (psw as SiegeWeapon).IsUsed);
					}
				}
				return true;
			});
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x0003D790 File Offset: 0x0003B990
		protected override void ManageFormationCounts()
		{
			if (this._startingPowerRatio == 0f)
			{
				this._startingPowerRatio = base.Team.QuerySystem.RemainingPowerRatio;
			}
			switch (this.CurrentTacticState)
			{
			case TacticDefendCastle.TacticState.ProperDefense:
			{
				int requiredMeleeDefenderCount = this.GetRequiredMeleeDefenderCount();
				int rangedCount = MathF.Min(this._teamAISiegeDefender.ArcherPositions.Count, 8 - requiredMeleeDefenderCount);
				base.ManageFormationCounts(requiredMeleeDefenderCount, rangedCount, 0, 0);
				this._meleeFormations = (from f in base.FormationsIncludingEmpty
				where f.CountOfUnits > 0 && f.QuerySystem.IsInfantryFormation
				select f).ToList<Formation>();
				this._rangedFormations = (from f in base.FormationsIncludingEmpty
				where f.CountOfUnits > 0 && f.QuerySystem.IsRangedFormation
				select f).ToList<Formation>();
				break;
			}
			case TacticDefendCastle.TacticState.DesperateDefense:
			{
				int formationCount = base.Team.GetFormationCount();
				int aicontrolledFormationCount = base.Team.GetAIControlledFormationCount();
				int num = formationCount - aicontrolledFormationCount;
				int requiredMeleeDefenderCount2 = this.GetRequiredMeleeDefenderCount();
				if (aicontrolledFormationCount > 0 && formationCount != requiredMeleeDefenderCount2 && num <= requiredMeleeDefenderCount2)
				{
					List<Formation> list = (from f in base.FormationsIncludingEmpty
					where f.CountOfUnits > 0 && f.IsAIControlled
					select f).ToList<Formation>();
					Formation biggestFormation = list.MaxBy((Formation f) => f.CountOfUnitsWithoutDetachedOnes);
					IEnumerable<Formation> source = list;
					Func<Formation, bool> predicate;
					Func<Formation, bool> <>9__4;
					if ((predicate = <>9__4) == null)
					{
						predicate = (<>9__4 = ((Formation f) => f != biggestFormation));
					}
					foreach (Formation formation in source.Where(predicate))
					{
						formation.TransferUnits(biggestFormation, formation.CountOfUnits);
					}
					if (aicontrolledFormationCount > 1)
					{
						biggestFormation.Split(aicontrolledFormationCount);
					}
				}
				break;
			}
			case TacticDefendCastle.TacticState.SallyOut:
			{
				int num2 = 1;
				int rangedCount2 = MathF.Min(this._teamAISiegeDefender.ArcherPositions.Count, 8 - num2);
				base.ManageFormationCounts(num2, rangedCount2, 0, 0);
				this._meleeFormations = (from f in base.FormationsIncludingEmpty
				where f.CountOfUnits > 0 && f.QuerySystem.IsInfantryFormation
				select f).ToList<Formation>();
				this._rangedFormations = (from f in base.FormationsIncludingEmpty
				where f.CountOfUnits > 0 && f.QuerySystem.IsRangedFormation
				select f).ToList<Formation>();
				break;
			}
			}
			if (this._initialLaneDefensePowerRatio == -1f)
			{
				this._meleeDefenderPower = (from f in base.FormationsIncludingEmpty
				where f.CountOfUnits > 0
				select f).Sum((Formation f) => f.QuerySystem.FormationMeleeFightingPower);
				this._laneThreatCapacity = this._lanes.Sum((SiegeLane l) => l.CalculateLaneCapacity());
				float num3 = 0f;
				foreach (Team team in base.Team.Mission.Teams)
				{
					if (team.IsEnemyOf(base.Team))
					{
						foreach (Formation formation2 in team.FormationsIncludingSpecialAndEmpty)
						{
							if (formation2.CountOfUnits > 0)
							{
								num3 += formation2.QuerySystem.FormationPower;
							}
						}
					}
				}
				int enemyUnitCount = base.Team.QuerySystem.EnemyUnitCount;
				float num4 = (enemyUnitCount == 0) ? 0f : (num3 / (float)enemyUnitCount);
				this._laneThreatCapacity = MathF.Min(this._lanes.Where(delegate(SiegeLane l)
				{
					if (!l.IsOpen)
					{
						return l.PrimarySiegeWeapons.Any((IPrimarySiegeWeapon psw) => !(psw as SiegeWeapon).IsDeactivated);
					}
					return true;
				}).Sum((SiegeLane l) => l.CalculateLaneCapacity()) * num4, num3);
				this._initialLaneDefensePowerRatio = this._meleeDefenderPower / this._laneThreatCapacity;
			}
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x0003DC2C File Offset: 0x0003BE2C
		protected override void StopUsingAllMachines()
		{
			base.StopUsingAllMachines();
			this.StopUsingStrategicAreas();
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x0003DC3C File Offset: 0x0003BE3C
		private void StopUsingStrategicAreas()
		{
			foreach (ValueTuple<IDetachment, DetachmentData> valueTuple in (from d in base.Team.DetachmentManager.Detachments
			where d.Item1 is StrategicArea
			select d).ToList<ValueTuple<IDetachment, DetachmentData>>())
			{
				base.Team.DetachmentManager.DestroyDetachment(valueTuple.Item1);
			}
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x0003DCD4 File Offset: 0x0003BED4
		private void StartRetreatToKeep()
		{
			this.StopUsingAllMachines();
			foreach (Formation formation in base.FormationsIncludingSpecialAndEmpty)
			{
				if (formation.CountOfUnits > 0)
				{
					formation.AI.ResetBehaviorWeights();
					TacticComponent.SetDefaultBehaviorWeights(formation);
					formation.AI.SetBehaviorWeight<BehaviorRetreatToKeep>(1f);
				}
			}
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x0003DD54 File Offset: 0x0003BF54
		private void DistributeRangedFormations()
		{
			List<Tuple<Formation, ArcherPosition>> list = this._rangedFormations.CombineWith(this._teamAISiegeDefender.ArcherPositions);
			while (list.Count > 0)
			{
				Tuple<Formation, ArcherPosition> tuple = list.MinBy((Tuple<Formation, ArcherPosition> c) => c.Item1.QuerySystem.MedianPosition.AsVec2.DistanceSquared(c.Item2.Entity.GlobalPosition.AsVec2));
				Formation bestFormation = tuple.Item1;
				ArcherPosition bestArcherPosition = tuple.Item2;
				bestFormation.AI.ResetBehaviorWeights();
				TacticComponent.SetDefaultBehaviorWeights(bestFormation);
				bestFormation.AI.SetBehaviorWeight<BehaviorShootFromCastleWalls>(1f);
				bestFormation.AI.GetBehavior<BehaviorShootFromCastleWalls>().ArcherPosition = bestArcherPosition.Entity;
				list.RemoveAll((Tuple<Formation, ArcherPosition> c) => c.Item1 == bestFormation || c.Item2 == bestArcherPosition);
			}
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x0003DE34 File Offset: 0x0003C034
		private void ManageGatesForSallyingOut()
		{
			if (!this._teamAISiegeDefender.InnerGate.IsGateOpen || !this._teamAISiegeDefender.OuterGate.IsGateOpen)
			{
				if (this._meleeFormations.Any((Formation mf) => TeamAISiegeComponent.IsFormationInsideCastle(mf, true, 0.4f)))
				{
					CastleGate castleGate = (!this._teamAISiegeDefender.InnerGate.IsGateOpen) ? this._teamAISiegeDefender.InnerGate : this._teamAISiegeDefender.OuterGate;
					bool flag = false;
					foreach (Formation formation in base.FormationsIncludingEmpty)
					{
						if (formation.CountOfUnits != 0 && castleGate.IsUsedByFormation(formation))
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						Formation strongestSallyOutFormation = this.GetStrongestSallyOutFormation();
						if (strongestSallyOutFormation != null)
						{
							strongestSallyOutFormation.StartUsingMachine(castleGate, false);
						}
					}
				}
			}
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x0003DF30 File Offset: 0x0003C130
		private void StartSallyOut()
		{
			this.DistributeRangedFormations();
			foreach (Formation formation in this._meleeFormations)
			{
				formation.AI.ResetBehaviorWeights();
				TacticComponent.SetDefaultBehaviorWeights(formation);
				formation.AI.SetBehaviorWeight<BehaviorSallyOut>(1000f);
			}
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x0003DFA4 File Offset: 0x0003C1A4
		private void CarryOutDefense(List<SiegeLane> defendedLanes, List<SiegeLane> lanesToBeRetaken, bool isEnemyInside, bool doRangedJoinMelee, out bool hasTransferOccurred)
		{
			hasTransferOccurred = false;
			List<Formation> list = new List<Formation>();
			List<Formation> list2 = new List<Formation>();
			int num = defendedLanes.Count + MathF.Max(lanesToBeRetaken.Count, isEnemyInside ? 1 : 0);
			List<Formation> list3 = new List<Formation>();
			foreach (Formation formation in this._meleeFormations)
			{
				if (formation.CountOfUnitsWithoutDetachedOnes > 0)
				{
					list3.Add(formation);
				}
			}
			if (list3.Count <= 0)
			{
				foreach (Formation formation2 in base.FormationsIncludingEmpty)
				{
					if (formation2.CountOfUnits > 0 && formation2.QuerySystem.IsMeleeFormation)
					{
						list3.Add(formation2);
					}
				}
			}
			int num2 = list3.Count;
			List<ArcherPosition> list4 = new List<ArcherPosition>();
			foreach (ArcherPosition archerPosition in this._teamAISiegeDefender.ArcherPositions)
			{
				foreach (SiegeLane siegeLane in this._lanes)
				{
					if (archerPosition.IsArcherPositionRelatedToSide(siegeLane.LaneSide) && siegeLane.LaneState > SiegeLane.LaneStateEnum.Used && siegeLane.LaneState < SiegeLane.LaneStateEnum.Conceited)
					{
						list4.Add(archerPosition);
					}
				}
			}
			List<Formation> list5 = new List<Formation>();
			foreach (Formation formation3 in this._rangedFormations)
			{
				if (formation3.CountOfUnitsWithoutDetachedOnes > 0 && !list3.Contains(formation3))
				{
					list5.Add(formation3);
				}
			}
			if (list5.Count <= 0)
			{
				foreach (Formation formation4 in base.FormationsIncludingEmpty)
				{
					if (formation4.CountOfUnits > 0 && formation4.QuerySystem.IsRangedFormation && !list3.Contains(formation4))
					{
						list5.Add(formation4);
					}
				}
			}
			foreach (Formation formation5 in base.FormationsIncludingEmpty)
			{
				if (formation5.CountOfUnits != 0 && !list3.Contains(formation5) && !list5.Contains(formation5) && formation5.CountOfUnitsWithoutDetachedOnes > 0)
				{
					if (formation5.QuerySystem.IsRangedFormation)
					{
						list5.Add(formation5);
					}
					else
					{
						list3.Add(formation5);
						num2++;
					}
				}
			}
			List<ArcherPosition> list6 = new List<ArcherPosition>();
			if (doRangedJoinMelee)
			{
				using (List<ArcherPosition>.Enumerator enumerator2 = list4.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						ArcherPosition archerPosition2 = enumerator2.Current;
						foreach (SiegeLane siegeLane2 in this._lanes)
						{
							if (siegeLane2.LaneState > SiegeLane.LaneStateEnum.Unused && siegeLane2.IsUnderAttack() && archerPosition2.IsArcherPositionRelatedToSide(siegeLane2.LaneSide))
							{
								list6.Add(archerPosition2);
							}
						}
					}
					goto IL_4C5;
				}
			}
			foreach (ArcherPosition archerPosition3 in list4)
			{
				bool flag = false;
				foreach (SiegeLane siegeLane3 in this._lanes)
				{
					if (siegeLane3.LaneSide == archerPosition3.GetArcherPositionClosestSide() && siegeLane3.LaneState >= SiegeLane.LaneStateEnum.Conceited)
					{
						list6.Add(archerPosition3);
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					bool flag2 = false;
					foreach (SiegeLane siegeLane4 in this._lanes)
					{
						if (archerPosition3.IsArcherPositionRelatedToSide(siegeLane4.LaneSide) && siegeLane4.LaneState >= SiegeLane.LaneStateEnum.Conceited)
						{
							list6.Add(archerPosition3);
							flag2 = true;
							break;
						}
					}
					if (!flag2 && this._lanes.Count > 0)
					{
						int num3 = int.MaxValue;
						SiegeLane siegeLane5 = null;
						foreach (SiegeLane siegeLane6 in this._lanes)
						{
							int num4 = SiegeQuerySystem.SideDistance(archerPosition3.ConnectedSides, 1 << (int)siegeLane6.LaneSide);
							if (num4 < num3)
							{
								siegeLane5 = siegeLane6;
								num3 = num4;
							}
						}
						if (siegeLane5.LaneState >= SiegeLane.LaneStateEnum.Conceited)
						{
							list6.Add(archerPosition3);
							break;
						}
					}
				}
			}
			IL_4C5:
			List<Formation> list7 = new List<Formation>();
			foreach (ArcherPosition archerPosition4 in list6)
			{
				Formation lastAssignedFormation = archerPosition4.GetLastAssignedFormation(base.Team.TeamIndex);
				if (lastAssignedFormation != null && list5.Contains(lastAssignedFormation))
				{
					list7.Add(lastAssignedFormation);
				}
			}
			int count = list7.Count;
			if (num2 > num)
			{
				List<Formation> list8 = new List<Formation>();
				foreach (SiegeLane siegeLane7 in defendedLanes)
				{
					if (siegeLane7.GetLastAssignedFormation(base.Team.TeamIndex) != null)
					{
						list8.Add(siegeLane7.GetLastAssignedFormation(base.Team.TeamIndex));
					}
				}
				foreach (SiegeLane siegeLane8 in lanesToBeRetaken)
				{
					if (siegeLane8.GetLastAssignedFormation(base.Team.TeamIndex) != null)
					{
						list8.Add(siegeLane8.GetLastAssignedFormation(base.Team.TeamIndex));
					}
				}
				if (list8.Count > 0)
				{
					Formation formation6 = null;
					using (List<Formation>.Enumerator enumerator = list3.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Formation excessFormation = enumerator.Current;
							if (excessFormation.IsAIControlled && !list8.Contains(excessFormation))
							{
								Formation formation7 = list8.FirstOrDefault((Formation aff) => aff.AI.Side == excessFormation.AI.Side);
								if (formation7 != null)
								{
									excessFormation.TransferUnits(formation7, excessFormation.CountOfUnits);
									hasTransferOccurred = true;
									formation6 = excessFormation;
									break;
								}
								float num5 = (float)(list8.Sum((Formation aff) => aff.CountOfUnits) + excessFormation.CountOfUnits) / (float)list8.Count;
								foreach (Formation formation8 in list8)
								{
									int b = (int)Math.Ceiling((double)(num5 - (float)formation8.CountOfUnits));
									int num6 = MathF.Min(excessFormation.CountOfUnits, b);
									if (num6 > 0)
									{
										excessFormation.TransferUnits(formation8, num6);
									}
								}
								hasTransferOccurred = true;
								formation6 = excessFormation;
								break;
							}
						}
					}
					if (formation6 != null)
					{
						list3.Remove(formation6);
					}
				}
				else
				{
					list3 = base.ConsolidateFormations(list3, num);
					hasTransferOccurred = true;
				}
				num2 = list3.Count;
			}
			List<Formation> list9 = list3.Concat(list7).ToList<Formation>();
			if (list9.Count <= 0)
			{
				list9 = (from f in base.FormationsIncludingEmpty
				where f.CountOfUnits > 0
				select f).ToList<Formation>();
				list5.Clear();
			}
			if (num2 + count < num)
			{
				List<Formation> list10 = new List<Formation>();
				foreach (Formation formation9 in list9)
				{
					if (formation9.IsSplittableByAI)
					{
						list10.Add(formation9);
					}
				}
				if (list10.Count > 0)
				{
					int num7 = 0;
					while (num2 + count + num7 < num && !hasTransferOccurred)
					{
						Formation largestFormation = list10.MaxBy((Formation rf) => rf.UnitsWithoutLooseDetachedOnes.Count);
						List<Formation> list11 = largestFormation.Split(2).ToList<Formation>();
						hasTransferOccurred = true;
						if (list11.Count < 2)
						{
							break;
						}
						num7++;
						Formation item = list11.FirstOrDefault((Formation rf) => rf != largestFormation);
						list10.Add(item);
						list9.Add(item);
					}
				}
			}
			List<SiegeLane> list12 = new List<SiegeLane>();
			List<Formation> list13 = new List<Formation>();
			using (List<SiegeLane>.Enumerator enumerator3 = defendedLanes.GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					SiegeLane toBeDefendedLane = enumerator3.Current;
					Formation formation10 = list9.FirstOrDefault((Formation affml) => affml == toBeDefendedLane.GetLastAssignedFormation(this.Team.TeamIndex));
					if (formation10 != null)
					{
						formation10.AI.Side = toBeDefendedLane.LaneSide;
						formation10.AI.ResetBehaviorWeights();
						TacticComponent.SetDefaultBehaviorWeights(formation10);
						formation10.AI.SetBehaviorWeight<BehaviorDefendCastleKeyPosition>(1f);
						toBeDefendedLane.SetLastAssignedFormation(base.Team.TeamIndex, formation10);
						list9.Remove(formation10);
						list12.Add(toBeDefendedLane);
						list13.Add(formation10);
						list.Add(formation10);
					}
				}
			}
			List<SiegeLane> list14 = defendedLanes.Except(list12).ToList<SiegeLane>();
			List<SiegeLane> list15 = new List<SiegeLane>();
			using (List<SiegeLane>.Enumerator enumerator3 = lanesToBeRetaken.GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					SiegeLane toBeRetakenLane = enumerator3.Current;
					Formation formation11 = list9.FirstOrDefault((Formation affml) => affml == toBeRetakenLane.GetLastAssignedFormation(this.Team.TeamIndex));
					if (formation11 != null)
					{
						formation11.AI.Side = toBeRetakenLane.LaneSide;
						formation11.AI.ResetBehaviorWeights();
						TacticComponent.SetDefaultBehaviorWeights(formation11);
						formation11.AI.SetBehaviorWeight<BehaviorRetakeCastleKeyPosition>(1f);
						toBeRetakenLane.SetLastAssignedFormation(base.Team.TeamIndex, formation11);
						list9.Remove(formation11);
						list15.Add(toBeRetakenLane);
						list13.Add(formation11);
						list.Add(formation11);
						list12.Add(toBeRetakenLane);
					}
				}
			}
			bool flag3 = false;
			while (list14.Count > 0)
			{
				SiegeLane firstToDefend = list14.MaxBy((SiegeLane tbdl) => tbdl.CalculateLaneCapacity());
				Formation formation12 = list9.FirstOrDefault((Formation affml) => affml.AI.Side == firstToDefend.LaneSide);
				if (formation12 != null)
				{
					formation12.AI.ResetBehaviorWeights();
					TacticComponent.SetDefaultBehaviorWeights(formation12);
					formation12.AI.SetBehaviorWeight<BehaviorDefendCastleKeyPosition>(1f);
					firstToDefend.SetLastAssignedFormation(base.Team.TeamIndex, formation12);
					list9.Remove(formation12);
					list13.Add(formation12);
					list.Add(formation12);
					list12.Add(firstToDefend);
				}
				else
				{
					if (list9.Count <= 0)
					{
						flag3 = true;
						list14.Clear();
						break;
					}
					Formation formation13 = list9.MaxBy((Formation f) => f.QuerySystem.FormationPower);
					formation13.AI.Side = firstToDefend.LaneSide;
					formation13.AI.ResetBehaviorWeights();
					TacticComponent.SetDefaultBehaviorWeights(formation13);
					formation13.AI.SetBehaviorWeight<BehaviorDefendCastleKeyPosition>(1f);
					firstToDefend.SetLastAssignedFormation(base.Team.TeamIndex, formation13);
					list9.Remove(formation13);
					list13.Add(formation13);
					list.Add(formation13);
					list12.Add(firstToDefend);
				}
				list14.Remove(firstToDefend);
			}
			List<SiegeLane> list16 = flag3 ? new List<SiegeLane>() : lanesToBeRetaken.Except(list15).ToList<SiegeLane>();
			while (list16.Count > 0 && list9.Count > 0)
			{
				SiegeLane firstToRetake = lanesToBeRetaken.MaxBy((SiegeLane ltbr) => ltbr.CalculateLaneCapacity());
				Formation formation14 = list9.FirstOrDefault((Formation affml) => affml.AI.Side == firstToRetake.LaneSide);
				if (formation14 != null)
				{
					formation14.AI.ResetBehaviorWeights();
					TacticComponent.SetDefaultBehaviorWeights(formation14);
					formation14.AI.SetBehaviorWeight<BehaviorRetakeCastleKeyPosition>(1f);
					firstToRetake.SetLastAssignedFormation(base.Team.TeamIndex, formation14);
					list9.Remove(formation14);
					list13.Add(formation14);
					list.Add(formation14);
					list12.Add(firstToRetake);
				}
				else
				{
					if (list9.Count <= 0)
					{
						break;
					}
					Formation formation15 = list9.MaxBy((Formation f) => f.QuerySystem.FormationPower);
					formation15.AI.Side = firstToRetake.LaneSide;
					formation15.AI.ResetBehaviorWeights();
					TacticComponent.SetDefaultBehaviorWeights(formation15);
					formation15.AI.SetBehaviorWeight<BehaviorRetakeCastleKeyPosition>(1f);
					firstToRetake.SetLastAssignedFormation(base.Team.TeamIndex, formation15);
					list9.Remove(formation15);
					list13.Add(formation15);
					list.Add(formation15);
					list12.Add(firstToRetake);
				}
				list16.Remove(firstToRetake);
			}
			Formation formation16 = null;
			if (isEnemyInside && list9.Count > 0)
			{
				Formation formation17;
				if (this._emergencyFormation != null && list9.Contains(this._emergencyFormation))
				{
					formation17 = this._emergencyFormation;
				}
				else
				{
					formation17 = list9.MaxBy((Formation affml) => affml.QuerySystem.FormationPower);
				}
				formation17.AI.Side = FormationAI.BehaviorSide.BehaviorSideNotSet;
				formation17.AI.ResetBehaviorWeights();
				TacticComponent.SetDefaultBehaviorWeights(formation17);
				formation17.AI.SetBehaviorWeight<BehaviorEliminateEnemyInsideCastle>(1f);
				list9.Remove(formation17);
				list13.Add(formation17);
				list.Add(formation17);
				formation16 = formation17;
			}
			IEnumerable<Formation> enumerable = list7.Except(list13);
			if (!hasTransferOccurred)
			{
				using (IEnumerator<Formation> enumerator5 = enumerable.GetEnumerator())
				{
					if (enumerator5.MoveNext())
					{
						Formation rangedFormation = enumerator5.Current;
						ArcherPosition archerPosition5 = list6.FirstOrDefault((ArcherPosition aptba) => aptba.GetLastAssignedFormation(this.Team.TeamIndex) == rangedFormation);
						List<SiegeLane> list17 = new List<SiegeLane>();
						foreach (SiegeLane siegeLane9 in defendedLanes.Union(lanesToBeRetaken))
						{
							if (siegeLane9.GetLastAssignedFormation(base.Team.TeamIndex) != null)
							{
								list17.Add(siegeLane9);
							}
						}
						bool flag4 = list17.Count > 0;
						SiegeLane siegeLane10 = null;
						if (archerPosition5 == null)
						{
							if (flag4)
							{
								siegeLane10 = list17.MaxBy((SiegeLane arl) => arl.LaneState);
							}
						}
						else if (flag4)
						{
							List<SiegeLane> list18 = new List<SiegeLane>();
							foreach (SiegeLane siegeLane11 in list17)
							{
								if (archerPosition5.IsArcherPositionRelatedToSide(siegeLane11.LaneSide))
								{
									list18.Add(siegeLane11);
								}
							}
							if (list18.Count > 0)
							{
								siegeLane10 = list18.MaxBy((SiegeLane rl) => rl.LaneState);
							}
							else
							{
								siegeLane10 = list17.MaxBy((SiegeLane arl) => arl.LaneState);
							}
						}
						Formation target;
						if (siegeLane10 == null)
						{
							target = formation16;
						}
						else
						{
							target = siegeLane10.GetLastAssignedFormation(base.Team.TeamIndex);
						}
						rangedFormation.TransferUnits(target, rangedFormation.CountOfUnits);
						hasTransferOccurred = true;
					}
				}
			}
			List<ArcherPosition> list19 = list4.Except(list6).ToList<ArcherPosition>();
			List<Formation> list20 = list5.Except(list13).Except(enumerable).ToList<Formation>();
			List<ArcherPosition> list21 = new List<ArcherPosition>();
			if (list20.Count > list19.Count)
			{
				if (list19.Count > 0 && !hasTransferOccurred)
				{
					list20 = base.ConsolidateFormations(list20, list19.Count);
					hasTransferOccurred = true;
				}
			}
			else if (list20.Count < list19.Count && list20.Count > 0 && !hasTransferOccurred)
			{
				int num8 = list19.Count - list20.Count;
				Formation formation18 = list20.MaxBy((Formation rrf) => rrf.CountOfUnits);
				List<Formation> list22 = formation18.Split(num8 + 1).ToList<Formation>();
				list22.Remove(formation18);
				hasTransferOccurred = true;
				list20.AddRange(list22);
			}
			using (List<ArcherPosition>.Enumerator enumerator2 = list19.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					ArcherPosition remainingArcherPosition = enumerator2.Current;
					if (remainingArcherPosition.GetLastAssignedFormation(base.Team.TeamIndex) != null)
					{
						if (list20.Contains(remainingArcherPosition.GetLastAssignedFormation(base.Team.TeamIndex)))
						{
							Formation lastAssignedFormation2 = remainingArcherPosition.GetLastAssignedFormation(base.Team.TeamIndex);
							lastAssignedFormation2.AI.Side = remainingArcherPosition.GetArcherPositionClosestSide();
							lastAssignedFormation2.AI.ResetBehaviorWeights();
							TacticComponent.SetDefaultBehaviorWeights(lastAssignedFormation2);
							lastAssignedFormation2.AI.SetBehaviorWeight<BehaviorShootFromCastleWalls>(1f).ArcherPosition = remainingArcherPosition.Entity;
							remainingArcherPosition.SetLastAssignedFormation(base.Team.TeamIndex, lastAssignedFormation2);
							list20.Remove(lastAssignedFormation2);
							list13.Add(remainingArcherPosition.GetLastAssignedFormation(base.Team.TeamIndex));
							list2.Add(remainingArcherPosition.GetLastAssignedFormation(base.Team.TeamIndex));
							list21.Add(remainingArcherPosition);
						}
						else
						{
							Formation formation19 = list20.FirstOrDefault((Formation rrf) => remainingArcherPosition.IsArcherPositionRelatedToSide(rrf.AI.Side));
							if (formation19 == null)
							{
								formation19 = list20.FirstOrDefault<Formation>();
							}
							if (formation19 == null)
							{
								break;
							}
							formation19.AI.Side = remainingArcherPosition.GetArcherPositionClosestSide();
							formation19.AI.ResetBehaviorWeights();
							TacticComponent.SetDefaultBehaviorWeights(formation19);
							formation19.AI.SetBehaviorWeight<BehaviorShootFromCastleWalls>(1f).ArcherPosition = remainingArcherPosition.Entity;
							remainingArcherPosition.SetLastAssignedFormation(base.Team.TeamIndex, formation19);
							list20.Remove(formation19);
							list13.Add(formation19);
							list2.Add(formation19);
							list21.Add(remainingArcherPosition);
						}
					}
				}
			}
			list19 = list19.Except(list21).ToList<ArcherPosition>();
			using (List<ArcherPosition>.Enumerator enumerator2 = list19.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					ArcherPosition remainingArcherPosition = enumerator2.Current;
					Formation formation20 = list20.FirstOrDefault((Formation rrf) => remainingArcherPosition.IsArcherPositionRelatedToSide(rrf.AI.Side));
					if (formation20 == null)
					{
						formation20 = list20.FirstOrDefault<Formation>();
					}
					if (formation20 == null)
					{
						break;
					}
					formation20.AI.Side = remainingArcherPosition.GetArcherPositionClosestSide();
					formation20.AI.ResetBehaviorWeights();
					TacticComponent.SetDefaultBehaviorWeights(formation20);
					formation20.AI.SetBehaviorWeight<BehaviorShootFromCastleWalls>(1f).ArcherPosition = remainingArcherPosition.Entity;
					remainingArcherPosition.SetLastAssignedFormation(base.Team.TeamIndex, formation20);
					list20.Remove(formation20);
					list13.Add(formation20);
					list2.Add(formation20);
					list21.Add(remainingArcherPosition);
				}
			}
			this._meleeFormations = list;
			this._laneDefendingFormations = new List<Formation>();
			foreach (Formation formation21 in list)
			{
				if (formation21.AI.Side < FormationAI.BehaviorSide.BehaviorSideNotSet)
				{
					this._laneDefendingFormations.Add(formation21);
				}
			}
			this._rangedFormations = list2;
			foreach (SiegeLane siegeLane12 in this._lanes.Except(list12))
			{
				siegeLane12.SetLastAssignedFormation(base.Team.TeamIndex, null);
			}
			this._emergencyFormation = formation16;
			foreach (ArcherPosition archerPosition6 in this._teamAISiegeDefender.ArcherPositions.Except(list21))
			{
				archerPosition6.SetLastAssignedFormation(base.Team.TeamIndex, null);
			}
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x0003F828 File Offset: 0x0003DA28
		private void CheckAndChangeState()
		{
			if (!this.MustRetreatToCastle())
			{
				if (this.IsSallyOutApplicable())
				{
					if (this.CurrentTacticState != TacticDefendCastle.TacticState.SallyOut)
					{
						this.CurrentTacticState = TacticDefendCastle.TacticState.SallyOut;
						return;
					}
					if (!this._isSallyingOut)
					{
						this.ManageGatesForSallyingOut();
						if (this._teamAISiegeDefender.InnerGate.IsGateOpen && this._teamAISiegeDefender.OuterGate.IsGateOpen)
						{
							this.StartSallyOut();
							this._isSallyingOut = true;
							return;
						}
					}
				}
				else
				{
					bool flag = false;
					if (this._invadingEnemyFormation != null)
					{
						flag = TeamAISiegeComponent.IsFormationInsideCastle(this._invadingEnemyFormation, true, 0.4f);
						if (!flag)
						{
							this._invadingEnemyFormation = null;
						}
					}
					if (!flag)
					{
						flag = (TeamAISiegeComponent.QuerySystem.InsideAttackerCount > 30);
						if (flag)
						{
							Formation formation = null;
							foreach (Team team in base.Team.Mission.Teams)
							{
								if (team.IsEnemyOf(base.Team))
								{
									for (int i = 0; i < Math.Min(team.FormationsIncludingSpecialAndEmpty.Count, 8); i++)
									{
										Formation formation2 = team.FormationsIncludingSpecialAndEmpty[i];
										if (formation2.CountOfUnits > 0 && TeamAISiegeComponent.IsFormationInsideCastle(formation2, true, 0.4f))
										{
											formation = formation2;
											break;
										}
									}
								}
							}
							if (formation != null)
							{
								this._invadingEnemyFormation = formation;
							}
							else
							{
								flag = false;
							}
						}
					}
					List<SiegeLane> list = (from l in this._lanes
					where l.LaneState == SiegeLane.LaneStateEnum.Conceited
					select l).ToList<SiegeLane>();
					List<SiegeLane> activeLanes = (from l in this._lanes.Except(list)
					where l.GetDefenseState() > SiegeLane.LaneDefenseStates.Empty
					select l).ToList<SiegeLane>();
					if (flag)
					{
						list.Clear();
					}
					bool flag2 = list.Count > 0;
					if (!flag2 && !flag && activeLanes.Count == 0)
					{
						activeLanes = (from l in this._lanes
						where l.HasGate
						select l).ToList<SiegeLane>();
					}
					if (flag2 && activeLanes.Count > 0)
					{
						SiegeLane item = list.MinBy((SiegeLane cl) => activeLanes.Min((SiegeLane al) => SiegeQuerySystem.SideDistance(1 << (int)al.LaneSide, 1 << (int)cl.LaneSide)));
						list.Clear();
						list.Add(item);
					}
					bool flag3 = flag2 || flag;
					this._meleeFormations = (from mf in this._meleeFormations
					where mf.CountOfUnits > 0
					select mf).ToList<Formation>();
					this._rangedFormations = (from rf in this._rangedFormations
					where rf.CountOfUnits > 0
					select rf).ToList<Formation>();
					int num = MathF.Max(this._meleeFormations.Sum((Formation mf) => mf.CountOfUnits), 1);
					int num2 = MathF.Max(this._rangedFormations.Sum((Formation rf) => rf.CountOfUnits), 1);
					int num3 = num + num2;
					if (!this._areRangedNeededForLaneDefense)
					{
						this._areRangedNeededForLaneDefense = ((float)num < (float)num3 * 0.33f);
					}
					int num4 = 0;
					if (flag3)
					{
						float num5 = (float)num - this._lanes.Sum((SiegeLane l) => l.CalculateLaneCapacity());
						if (flag)
						{
							num4 = ((num5 >= 15f) ? 1 : 0);
						}
						else
						{
							num4 = MathF.Min((int)num5 / 15, list.Count);
						}
					}
					if (activeLanes.Count + list.Count + num4 <= 0)
					{
						this._isTacticFailing = true;
						num4 = 1;
					}
					bool flag4;
					this.CarryOutDefense(activeLanes, list, flag && num4 > 0, this._areRangedNeededForLaneDefense, out flag4);
					if (!flag4)
					{
						this.BalanceLaneDefenders((from ldf in this._laneDefendingFormations
						where ldf.IsAIControlled && ldf.AI.ActiveBehavior is BehaviorDefendCastleKeyPosition
						select ldf).ToList<Formation>(), out flag4);
						if (!flag4)
						{
							this.ArcherShiftAround(this._rangedFormations);
						}
					}
				}
				return;
			}
			if (this.CurrentTacticState == TacticDefendCastle.TacticState.RetreatToKeep)
			{
				return;
			}
			this.CurrentTacticState = TacticDefendCastle.TacticState.RetreatToKeep;
			this.ManageFormationCounts();
			this.StartRetreatToKeep();
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x0003FC94 File Offset: 0x0003DE94
		protected internal override void TickOccasionally()
		{
			if (!base.AreFormationsCreated)
			{
				return;
			}
			if (!this._areSiegeWeaponsAbandoned)
			{
				foreach (Team team in base.Team.Mission.Teams)
				{
					if (team.IsEnemyOf(base.Team))
					{
						if (team.QuerySystem.InsideWallsRatio > 0.5f)
						{
							base.StopUsingAllRangedSiegeWeapons();
							this._areSiegeWeaponsAbandoned = true;
							break;
						}
						break;
					}
				}
			}
			this.CheckAndChangeState();
			this.CheckAndSetAvailableFormationsChanged();
			base.TickOccasionally();
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x0003FD40 File Offset: 0x0003DF40
		protected internal override float GetTacticWeight()
		{
			if (this._isTacticFailing)
			{
				return 5f;
			}
			return 10f;
		}

		// Token: 0x040004AF RID: 1199
		private const float InfantrySallyOutEffectiveness = 1f;

		// Token: 0x040004B0 RID: 1200
		private const float RangedSallyOutEffectiveness = 0.3f;

		// Token: 0x040004B1 RID: 1201
		private const float CavalrySallyOutEffectiveness = 2f;

		// Token: 0x040004B2 RID: 1202
		private const float SallyOutDecisionPenalty = 3f;

		// Token: 0x040004B3 RID: 1203
		private readonly TeamAISiegeDefender _teamAISiegeDefender;

		// Token: 0x040004B4 RID: 1204
		private readonly List<MissionObject> _castleKeyPositions;

		// Token: 0x040004B5 RID: 1205
		private readonly List<SiegeLane> _lanes;

		// Token: 0x040004B6 RID: 1206
		private float _startingPowerRatio;

		// Token: 0x040004B7 RID: 1207
		private float _meleeDefenderPower;

		// Token: 0x040004B8 RID: 1208
		private float _laneThreatCapacity;

		// Token: 0x040004B9 RID: 1209
		private float _initialLaneDefensePowerRatio = -1f;

		// Token: 0x040004BA RID: 1210
		private bool _isSallyingOut;

		// Token: 0x040004BB RID: 1211
		private bool _areRangedNeededForLaneDefense;

		// Token: 0x040004BC RID: 1212
		private bool _isTacticFailing;

		// Token: 0x040004BD RID: 1213
		private bool _areSiegeWeaponsAbandoned;

		// Token: 0x040004BE RID: 1214
		private Formation _invadingEnemyFormation;

		// Token: 0x040004BF RID: 1215
		private Formation _emergencyFormation;

		// Token: 0x040004C0 RID: 1216
		private List<Formation> _meleeFormations;

		// Token: 0x040004C1 RID: 1217
		private List<Formation> _laneDefendingFormations = new List<Formation>();

		// Token: 0x040004C2 RID: 1218
		private List<Formation> _rangedFormations;

		// Token: 0x040004C3 RID: 1219
		private int _laneCount;

		// Token: 0x0200046E RID: 1134
		public enum TacticState
		{
			// Token: 0x04001983 RID: 6531
			ProperDefense,
			// Token: 0x04001984 RID: 6532
			DesperateDefense,
			// Token: 0x04001985 RID: 6533
			RetreatToKeep,
			// Token: 0x04001986 RID: 6534
			SallyOut
		}
	}
}
