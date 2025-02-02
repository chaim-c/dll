using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200016E RID: 366
	public class TacticPerimeterDefense : TacticComponent
	{
		// Token: 0x0600127F RID: 4735 RVA: 0x000435BC File Offset: 0x000417BC
		public TacticPerimeterDefense(Team team) : base(team)
		{
			Scene scene = Mission.Current.Scene;
			FleePosition fleePosition = Mission.Current.GetFleePositionsForSide(BattleSideEnum.Defender).FirstOrDefault((FleePosition fp) => fp.GetSide() == BattleSideEnum.Defender);
			if (fleePosition != null)
			{
				this._defendPosition = fleePosition.GameEntity.GlobalPosition.ToWorldPosition();
			}
			else
			{
				this._defendPosition = WorldPosition.Invalid;
			}
			this._enemyClusters = new List<TacticPerimeterDefense.EnemyCluster>();
			this._defenseFronts = new List<TacticPerimeterDefense.DefenseFront>();
		}

		// Token: 0x06001280 RID: 4736 RVA: 0x00043648 File Offset: 0x00041848
		private void DetermineEnemyClusters()
		{
			List<Formation> list = new List<Formation>();
			float num = 0f;
			foreach (Team team in base.Team.Mission.Teams)
			{
				if (team.IsEnemyOf(base.Team))
				{
					num += team.QuerySystem.TeamPower;
				}
			}
			foreach (Team team2 in base.Team.Mission.Teams)
			{
				if (team2.IsEnemyOf(base.Team))
				{
					for (int i = 0; i < Math.Min(team2.FormationsIncludingSpecialAndEmpty.Count, 8); i++)
					{
						Formation enemyFormation = team2.FormationsIncludingSpecialAndEmpty[i];
						if (enemyFormation.CountOfUnits > 0 && enemyFormation.QuerySystem.FormationPower < MathF.Min(base.Team.QuerySystem.TeamPower, num) / 4f)
						{
							if (!this._enemyClusters.Any((TacticPerimeterDefense.EnemyCluster ec) => ec.enemyFormations.IndexOf(enemyFormation) >= 0))
							{
								list.Add(enemyFormation);
							}
						}
						else
						{
							TacticPerimeterDefense.EnemyCluster enemyCluster = this._enemyClusters.FirstOrDefault((TacticPerimeterDefense.EnemyCluster ec) => ec.enemyFormations.IndexOf(enemyFormation) >= 0);
							if (enemyCluster != null)
							{
								if ((double)(this._defendPosition.AsVec2 - enemyCluster.AggregatePosition).DotProduct(this._defendPosition.AsVec2 - enemyFormation.QuerySystem.AveragePosition) >= 0.70710678118)
								{
									goto IL_216;
								}
								enemyCluster.RemoveFromCluster(enemyFormation);
							}
							List<TacticPerimeterDefense.EnemyCluster> list2 = (from c in this._enemyClusters
							where (double)(this._defendPosition.AsVec2 - c.AggregatePosition).DotProduct(this._defendPosition.AsVec2 - enemyFormation.QuerySystem.MedianPosition.AsVec2) >= 0.70710678118
							select c).ToList<TacticPerimeterDefense.EnemyCluster>();
							if (list2.Count > 0)
							{
								list2.MaxBy((TacticPerimeterDefense.EnemyCluster ec) => (this._defendPosition.AsVec2 - ec.AggregatePosition).DotProduct(this._defendPosition.AsVec2 - enemyFormation.QuerySystem.MedianPosition.AsVec2)).AddToCluster(enemyFormation);
							}
							else
							{
								TacticPerimeterDefense.EnemyCluster enemyCluster2 = new TacticPerimeterDefense.EnemyCluster();
								enemyCluster2.AddToCluster(enemyFormation);
								this._enemyClusters.Add(enemyCluster2);
							}
						}
						IL_216:;
					}
				}
			}
			if (this._enemyClusters.Count > 0)
			{
				using (List<Formation>.Enumerator enumerator2 = list.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						Formation skippedFormation = enumerator2.Current;
						this._enemyClusters.MaxBy((TacticPerimeterDefense.EnemyCluster ec) => (this._defendPosition.AsVec2 - ec.AggregatePosition).DotProduct(this._defendPosition.AsVec2 - skippedFormation.QuerySystem.MedianPosition.AsVec2)).AddToCluster(skippedFormation);
					}
				}
			}
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x00043964 File Offset: 0x00041B64
		private bool MustRetreatToCastle()
		{
			return base.Team.QuerySystem.TotalPowerRatio / base.Team.QuerySystem.RemainingPowerRatio > 2f;
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x00043990 File Offset: 0x00041B90
		private void StartRetreatToKeep()
		{
			foreach (Formation formation in base.FormationsIncludingEmpty)
			{
				if (formation.CountOfUnits > 0)
				{
					formation.AI.ResetBehaviorWeights();
					TacticComponent.SetDefaultBehaviorWeights(formation);
					formation.AI.SetBehaviorWeight<BehaviorRetreatToKeep>(1f);
				}
			}
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x00043A08 File Offset: 0x00041C08
		private void CheckAndChangeState()
		{
			if (this.MustRetreatToCastle())
			{
				if (this._isRetreatingToKeep)
				{
					return;
				}
				this._isRetreatingToKeep = true;
				this.StartRetreatToKeep();
			}
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x00043A28 File Offset: 0x00041C28
		private void ArrangeDefenseFronts()
		{
			this._meleeFormations = (from f in base.FormationsIncludingEmpty
			where f.CountOfUnits > 0 && (f.QuerySystem.IsInfantryFormation || f.QuerySystem.IsCavalryFormation)
			select f).ToList<Formation>();
			this._rangedFormations = (from f in base.FormationsIncludingEmpty
			where f.CountOfUnits > 0 && (f.QuerySystem.IsRangedFormation || f.QuerySystem.IsRangedCavalryFormation)
			select f).ToList<Formation>();
			int num = MathF.Min(8 - this._rangedFormations.Count, this._enemyClusters.Count);
			if (this._meleeFormations.Count != num)
			{
				base.SplitFormationClassIntoGivenNumber((Formation f) => f.QuerySystem.IsInfantryFormation || f.QuerySystem.IsCavalryFormation, num);
				this._meleeFormations = (from f in base.FormationsIncludingEmpty
				where f.CountOfUnits > 0 && (f.QuerySystem.IsInfantryFormation || f.QuerySystem.IsCavalryFormation)
				select f).ToList<Formation>();
			}
			int num2 = MathF.Min(8 - num, this._enemyClusters.Count);
			if (this._rangedFormations.Count != num2)
			{
				base.SplitFormationClassIntoGivenNumber((Formation f) => f.QuerySystem.IsRangedFormation || f.QuerySystem.IsRangedCavalryFormation, num2);
				this._rangedFormations = (from f in base.FormationsIncludingEmpty
				where f.CountOfUnits > 0 && (f.QuerySystem.IsRangedFormation || f.QuerySystem.IsRangedCavalryFormation)
				select f).ToList<Formation>();
			}
			foreach (TacticPerimeterDefense.DefenseFront defenseFront in this._defenseFronts)
			{
				defenseFront.MatchedEnemyCluster.UpdateClusterData();
				BehaviorDefendKeyPosition behaviorDefendKeyPosition = defenseFront.MeleeFormation.AI.SetBehaviorWeight<BehaviorDefendKeyPosition>(1f);
				behaviorDefendKeyPosition.EnemyClusterPosition = defenseFront.MatchedEnemyCluster.MedianAggregatePosition;
				behaviorDefendKeyPosition.EnemyClusterPosition.SetVec2(defenseFront.MatchedEnemyCluster.AggregatePosition);
			}
			IEnumerable<TacticPerimeterDefense.EnemyCluster> enumerable = from ec in this._enemyClusters
			where this._defenseFronts.All((TacticPerimeterDefense.DefenseFront df) => df.MatchedEnemyCluster != ec)
			select ec;
			List<Formation> list = (from mf in this._meleeFormations
			where this._defenseFronts.All((TacticPerimeterDefense.DefenseFront df) => df.MeleeFormation != mf)
			select mf).ToList<Formation>();
			List<Formation> list2 = (from rf in this._rangedFormations
			where this._defenseFronts.All((TacticPerimeterDefense.DefenseFront df) => df.RangedFormation != rf)
			select rf).ToList<Formation>();
			foreach (TacticPerimeterDefense.EnemyCluster enemyCluster in enumerable)
			{
				if (list.IsEmpty<Formation>())
				{
					break;
				}
				Formation formation = list[list.Count - 1];
				TacticPerimeterDefense.DefenseFront defenseFront2 = new TacticPerimeterDefense.DefenseFront(enemyCluster, formation);
				formation.AI.ResetBehaviorWeights();
				TacticComponent.SetDefaultBehaviorWeights(formation);
				BehaviorDefendKeyPosition behaviorDefendKeyPosition2 = formation.AI.SetBehaviorWeight<BehaviorDefendKeyPosition>(1f);
				behaviorDefendKeyPosition2.DefensePosition = this._defendPosition;
				behaviorDefendKeyPosition2.EnemyClusterPosition = enemyCluster.MedianAggregatePosition;
				behaviorDefendKeyPosition2.EnemyClusterPosition.SetVec2(enemyCluster.AggregatePosition);
				list.Remove(formation);
				if (!list2.IsEmpty<Formation>())
				{
					Formation formation2 = list2[list2.Count - 1];
					formation2.AI.ResetBehaviorWeights();
					TacticComponent.SetDefaultBehaviorWeights(formation2);
					formation2.AI.SetBehaviorWeight<BehaviorSkirmishBehindFormation>(1f).ReferenceFormation = formation;
					defenseFront2.RangedFormation = formation2;
					list2.Remove(formation2);
					this._defenseFronts.Add(defenseFront2);
				}
			}
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x00043D9C File Offset: 0x00041F9C
		protected internal override void TickOccasionally()
		{
			if (!base.AreFormationsCreated)
			{
				return;
			}
			this.CheckAndChangeState();
			if (!this._isRetreatingToKeep)
			{
				this.DetermineEnemyClusters();
				this.ArrangeDefenseFronts();
			}
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x00043DC1 File Offset: 0x00041FC1
		protected internal override float GetTacticWeight()
		{
			if (this._defendPosition.IsValid)
			{
				return 10f;
			}
			return 0f;
		}

		// Token: 0x040004D3 RID: 1235
		private WorldPosition _defendPosition;

		// Token: 0x040004D4 RID: 1236
		private readonly List<TacticPerimeterDefense.EnemyCluster> _enemyClusters;

		// Token: 0x040004D5 RID: 1237
		private readonly List<TacticPerimeterDefense.DefenseFront> _defenseFronts;

		// Token: 0x040004D6 RID: 1238
		private const float RetreatThresholdValue = 2f;

		// Token: 0x040004D7 RID: 1239
		private List<Formation> _meleeFormations;

		// Token: 0x040004D8 RID: 1240
		private List<Formation> _rangedFormations;

		// Token: 0x040004D9 RID: 1241
		private bool _isRetreatingToKeep;

		// Token: 0x02000486 RID: 1158
		private class DefenseFront
		{
			// Token: 0x060035EE RID: 13806 RVA: 0x000DA6FB File Offset: 0x000D88FB
			public DefenseFront(TacticPerimeterDefense.EnemyCluster matchedEnemyCluster, Formation meleeFormation)
			{
				this.MatchedEnemyCluster = matchedEnemyCluster;
				this.MeleeFormation = meleeFormation;
				this.RangedFormation = null;
			}

			// Token: 0x040019FA RID: 6650
			public Formation MeleeFormation;

			// Token: 0x040019FB RID: 6651
			public Formation RangedFormation;

			// Token: 0x040019FC RID: 6652
			public TacticPerimeterDefense.EnemyCluster MatchedEnemyCluster;
		}

		// Token: 0x02000487 RID: 1159
		private class EnemyCluster
		{
			// Token: 0x1700094F RID: 2383
			// (get) Token: 0x060035EF RID: 13807 RVA: 0x000DA718 File Offset: 0x000D8918
			// (set) Token: 0x060035F0 RID: 13808 RVA: 0x000DA720 File Offset: 0x000D8920
			public Vec2 AggregatePosition { get; private set; }

			// Token: 0x17000950 RID: 2384
			// (get) Token: 0x060035F1 RID: 13809 RVA: 0x000DA729 File Offset: 0x000D8929
			// (set) Token: 0x060035F2 RID: 13810 RVA: 0x000DA731 File Offset: 0x000D8931
			public WorldPosition MedianAggregatePosition { get; private set; }

			// Token: 0x060035F3 RID: 13811 RVA: 0x000DA73A File Offset: 0x000D893A
			public EnemyCluster()
			{
				this.enemyFormations = new List<Formation>();
			}

			// Token: 0x060035F4 RID: 13812 RVA: 0x000DA750 File Offset: 0x000D8950
			public void UpdateClusterData()
			{
				this.totalPower = this.enemyFormations.Sum((Formation ef) => ef.QuerySystem.FormationPower);
				this.AggregatePosition = Vec2.Zero;
				foreach (Formation formation in this.enemyFormations)
				{
					this.AggregatePosition += formation.QuerySystem.AveragePosition * (formation.QuerySystem.FormationPower / this.totalPower);
				}
				this.UpdateMedianPosition();
			}

			// Token: 0x060035F5 RID: 13813 RVA: 0x000DA810 File Offset: 0x000D8A10
			public void AddToCluster(Formation formation)
			{
				this.enemyFormations.Add(formation);
				float num = this.totalPower;
				this.totalPower += formation.QuerySystem.FormationPower;
				this.AggregatePosition = this.AggregatePosition * (num / this.totalPower) + formation.QuerySystem.AveragePosition * (formation.QuerySystem.FormationPower / this.totalPower);
				this.UpdateMedianPosition();
			}

			// Token: 0x060035F6 RID: 13814 RVA: 0x000DA890 File Offset: 0x000D8A90
			public void RemoveFromCluster(Formation formation)
			{
				this.enemyFormations.Remove(formation);
				float num = this.totalPower;
				this.totalPower -= formation.QuerySystem.FormationPower;
				this.AggregatePosition -= formation.QuerySystem.AveragePosition * (formation.QuerySystem.FormationPower / num);
				this.AggregatePosition *= num / this.totalPower;
				this.UpdateMedianPosition();
			}

			// Token: 0x060035F7 RID: 13815 RVA: 0x000DA918 File Offset: 0x000D8B18
			private void UpdateMedianPosition()
			{
				float num = float.MaxValue;
				foreach (Formation formation in this.enemyFormations)
				{
					float num2 = formation.QuerySystem.MedianPosition.AsVec2.DistanceSquared(this.AggregatePosition);
					if (num2 < num)
					{
						num = num2;
						this.MedianAggregatePosition = formation.QuerySystem.MedianPosition;
					}
				}
			}

			// Token: 0x040019FD RID: 6653
			public List<Formation> enemyFormations;

			// Token: 0x040019FE RID: 6654
			public float totalPower;
		}
	}
}
