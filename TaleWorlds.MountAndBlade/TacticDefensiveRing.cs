using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200016A RID: 362
	public class TacticDefensiveRing : TacticComponent
	{
		// Token: 0x0600124F RID: 4687 RVA: 0x0004144F File Offset: 0x0003F64F
		public TacticDefensiveRing(Team team) : base(team)
		{
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x00041458 File Offset: 0x0003F658
		protected override void ManageFormationCounts()
		{
			base.AssignTacticFormations1121();
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x00041460 File Offset: 0x0003F660
		private void Defend()
		{
			if (base.Team.IsPlayerTeam && !base.Team.IsPlayerGeneral && base.Team.IsPlayerSergeant)
			{
				base.SoundTacticalHorn(TacticComponent.MoveHornSoundIndex);
			}
			if (this._mainInfantry != null)
			{
				this._mainInfantry.AI.ResetBehaviorWeights();
				TacticComponent.SetDefaultBehaviorWeights(this._mainInfantry);
				this._mainInfantry.AI.SetBehaviorWeight<BehaviorDefensiveRing>(1f).TacticalDefendPosition = this._mainRingPosition;
			}
			if (this._archers != null && this._mainInfantry != null)
			{
				this._archers.AI.ResetBehaviorWeights();
				TacticComponent.SetDefaultBehaviorWeights(this._archers);
				this._archers.AI.SetBehaviorWeight<BehaviorFireFromInfantryCover>(1f);
				this._archers.AI.SetBehaviorWeight<BehaviorSkirmish>(1f);
			}
			if (this._leftCavalry != null)
			{
				this._leftCavalry.AI.ResetBehaviorWeights();
				TacticComponent.SetDefaultBehaviorWeights(this._leftCavalry);
				this._leftCavalry.AI.SetBehaviorWeight<BehaviorProtectFlank>(1f).FlankSide = FormationAI.BehaviorSide.Left;
				this._leftCavalry.AI.SetBehaviorWeight<BehaviorCavalryScreen>(1f);
			}
			if (this._rightCavalry != null)
			{
				this._rightCavalry.AI.ResetBehaviorWeights();
				TacticComponent.SetDefaultBehaviorWeights(this._rightCavalry);
				this._rightCavalry.AI.SetBehaviorWeight<BehaviorProtectFlank>(1f).FlankSide = FormationAI.BehaviorSide.Right;
				this._rightCavalry.AI.SetBehaviorWeight<BehaviorCavalryScreen>(1f);
			}
			if (this._rangedCavalry != null)
			{
				this._rangedCavalry.AI.ResetBehaviorWeights();
				TacticComponent.SetDefaultBehaviorWeights(this._rangedCavalry);
				this._rangedCavalry.AI.SetBehaviorWeight<BehaviorMountedSkirmish>(1f);
				this._rangedCavalry.AI.SetBehaviorWeight<BehaviorHorseArcherSkirmish>(1f);
			}
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x00041630 File Offset: 0x0003F830
		protected override bool CheckAndSetAvailableFormationsChanged()
		{
			int aicontrolledFormationCount = base.Team.GetAIControlledFormationCount();
			bool flag = aicontrolledFormationCount != this._AIControlledFormationCount;
			if (flag)
			{
				this._AIControlledFormationCount = aicontrolledFormationCount;
				this.IsTacticReapplyNeeded = true;
			}
			return flag || (this._mainInfantry != null && (this._mainInfantry.CountOfUnits == 0 || !this._mainInfantry.QuerySystem.IsInfantryFormation)) || (this._archers != null && (this._archers.CountOfUnits == 0 || !this._archers.QuerySystem.IsRangedFormation)) || (this._leftCavalry != null && (this._leftCavalry.CountOfUnits == 0 || !this._leftCavalry.QuerySystem.IsCavalryFormation)) || (this._rightCavalry != null && (this._rightCavalry.CountOfUnits == 0 || !this._rightCavalry.QuerySystem.IsCavalryFormation)) || (this._rangedCavalry != null && (this._rangedCavalry.CountOfUnits == 0 || !this._rangedCavalry.QuerySystem.IsRangedCavalryFormation));
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x00041740 File Offset: 0x0003F940
		protected internal override void TickOccasionally()
		{
			if (!base.AreFormationsCreated)
			{
				return;
			}
			if (this.CheckAndSetAvailableFormationsChanged() || this.IsTacticReapplyNeeded)
			{
				this.ManageFormationCounts();
				this.Defend();
				this.IsTacticReapplyNeeded = false;
			}
			base.TickOccasionally();
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x00041774 File Offset: 0x0003F974
		protected internal override bool ResetTacticalPositions()
		{
			this.DetermineRingPosition();
			return true;
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x00041780 File Offset: 0x0003F980
		protected internal override float GetTacticWeight()
		{
			if (base.Team.TeamAI.IsDefenseApplicable)
			{
				if (base.CheckAndDetermineFormation(ref this._mainInfantry, (Formation f) => f.CountOfUnits > 0 && f.QuerySystem.IsInfantryFormation))
				{
					if (base.CheckAndDetermineFormation(ref this._archers, (Formation f) => f.CountOfUnits > 0 && f.QuerySystem.IsRangedFormation))
					{
						float num = (float)MathF.Max(0, this._mainInfantry.CountOfUnits) * (this._mainInfantry.MaximumInterval + this._mainInfantry.UnitDiameter) / 6.2831855f;
						float num2 = MathF.Sqrt((float)this._archers.CountOfUnits);
						float num3 = this._archers.UnitDiameter * num2 + this._archers.Interval * (num2 - 1f);
						if (num < num3)
						{
							return 0f;
						}
						if (!base.Team.TeamAI.IsCurrentTactic(this) || this._mainRingPosition == null || !this.IsTacticalPositionEligible(this._mainRingPosition))
						{
							this.DetermineRingPosition();
						}
						if (this._mainRingPosition == null)
						{
							return 0f;
						}
						return MathF.Min(base.Team.QuerySystem.InfantryRatio, base.Team.QuerySystem.RangedRatio) * 2f * 1.5f * this.GetTacticalPositionScore(this._mainRingPosition) * TacticComponent.CalculateNotEngagingTacticalAdvantage(base.Team.QuerySystem) / MathF.Sqrt(base.Team.QuerySystem.RemainingPowerRatio);
					}
				}
			}
			return 0f;
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x00041910 File Offset: 0x0003FB10
		private bool IsTacticalPositionEligible(TacticalPosition tacticalPosition)
		{
			Formation mainInfantry = this._mainInfantry;
			Vec2 vec;
			float num;
			if (mainInfantry == null)
			{
				vec = base.Team.QuerySystem.AveragePosition;
				num = vec.Distance(tacticalPosition.Position.AsVec2);
			}
			else
			{
				vec = mainInfantry.QuerySystem.AveragePosition;
				num = vec.Distance(tacticalPosition.Position.AsVec2);
			}
			float num2 = num;
			vec = base.Team.QuerySystem.AverageEnemyPosition;
			Formation mainInfantry2 = this._mainInfantry;
			float num3 = vec.Distance((mainInfantry2 != null) ? mainInfantry2.QuerySystem.AveragePosition : base.Team.QuerySystem.AveragePosition);
			return (num2 <= 20f || num2 <= num3 * 0.5f) && tacticalPosition.IsInsurmountable;
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x000419CC File Offset: 0x0003FBCC
		private float GetTacticalPositionScore(TacticalPosition tacticalPosition)
		{
			if (base.CheckAndDetermineFormation(ref this._mainInfantry, (Formation f) => f.CountOfUnits > 0 && f.QuerySystem.IsInfantryFormation))
			{
				if (base.CheckAndDetermineFormation(ref this._archers, (Formation f) => f.CountOfUnits > 0 && f.QuerySystem.IsRangedFormation))
				{
					float num = MBMath.Lerp(1f, 1.5f, MBMath.ClampFloat(tacticalPosition.Slope, 0f, 60f) / 60f, 1E-05f);
					Formation formation = (from f in base.Team.FormationsIncludingEmpty
					where f.CountOfUnits > 0 && f.QuerySystem.IsRangedFormation
					select f).MaxBy((Formation f) => f.CountOfUnits);
					float num2 = MathF.Max(formation.Arrangement.RankDepth, formation.Arrangement.FlankWidth);
					float num3 = MBMath.ClampFloat(tacticalPosition.Width / num2, 0.7f, 1f);
					float num4 = tacticalPosition.IsInsurmountable ? 1.5f : 1f;
					float cavalryFactor = this.GetCavalryFactor(tacticalPosition);
					float value = this._mainInfantry.QuerySystem.AveragePosition.Distance(tacticalPosition.Position.AsVec2);
					float num5 = MBMath.Lerp(0.7f, 1f, (150f - MBMath.ClampFloat(value, 50f, 150f)) / 100f, 1E-05f);
					return num * num3 * num4 * cavalryFactor * num5;
				}
			}
			return 0f;
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x00041B7C File Offset: 0x0003FD7C
		private List<TacticalPosition> ExtractPossibleTacticalPositionsFromTacticalRegion(TacticalRegion tacticalRegion)
		{
			List<TacticalPosition> list = new List<TacticalPosition>();
			foreach (TacticalPosition tacticalPosition in tacticalRegion.LinkedTacticalPositions)
			{
				if (tacticalPosition.TacticalPositionType == TacticalPosition.TacticalPositionTypeEnum.HighGround)
				{
					list.Add(tacticalPosition);
				}
			}
			if (tacticalRegion.tacticalRegionType == TacticalRegion.TacticalRegionTypeEnum.DifficultTerrain || tacticalRegion.tacticalRegionType == TacticalRegion.TacticalRegionTypeEnum.Opening)
			{
				Vec2 direction = (base.Team.QuerySystem.AverageEnemyPosition - tacticalRegion.Position.AsVec2).Normalized();
				TacticalPosition item = new TacticalPosition(tacticalRegion.Position, direction, tacticalRegion.radius, 0f, true, TacticalPosition.TacticalPositionTypeEnum.Regional, tacticalRegion.tacticalRegionType);
				list.Add(item);
			}
			return list;
		}

		// Token: 0x06001259 RID: 4697 RVA: 0x00041C48 File Offset: 0x0003FE48
		private float GetCavalryFactor(TacticalPosition tacticalPosition)
		{
			if (tacticalPosition.TacticalRegionMembership == TacticalRegion.TacticalRegionTypeEnum.Opening)
			{
				return 1f;
			}
			float num = base.Team.QuerySystem.TeamPower;
			float num2 = 0f;
			foreach (Team team in base.Team.Mission.Teams)
			{
				if (team.IsEnemyOf(base.Team))
				{
					num2 += team.QuerySystem.TeamPower;
				}
			}
			num -= num * (base.Team.QuerySystem.CavalryRatio + base.Team.QuerySystem.RangedCavalryRatio) * 0.5f;
			num2 -= num2 * (base.Team.QuerySystem.EnemyCavalryRatio + base.Team.QuerySystem.EnemyRangedCavalryRatio) * 0.5f;
			return num / num2 / base.Team.QuerySystem.RemainingPowerRatio;
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x00041D4C File Offset: 0x0003FF4C
		private void DetermineRingPosition()
		{
			IEnumerable<ValueTuple<TacticalPosition, float>> first = from tp in base.Team.TeamAI.TacticalPositions
			where tp.TacticalPositionType == TacticalPosition.TacticalPositionTypeEnum.HighGround && this.IsTacticalPositionEligible(tp)
			select new ValueTuple<TacticalPosition, float>(tp, this.GetTacticalPositionScore(tp));
			IEnumerable<ValueTuple<TacticalPosition, float>> second = from tpftr in base.Team.TeamAI.TacticalRegions.SelectMany((TacticalRegion r) => this.ExtractPossibleTacticalPositionsFromTacticalRegion(r))
			where (tpftr.TacticalPositionType == TacticalPosition.TacticalPositionTypeEnum.Regional || tpftr.TacticalPositionType == TacticalPosition.TacticalPositionTypeEnum.HighGround) && this.IsTacticalPositionEligible(tpftr)
			select tpftr into tp
			select new ValueTuple<TacticalPosition, float>(tp, this.GetTacticalPositionScore(tp));
			List<ValueTuple<TacticalPosition, float>> list = first.Concat(second).ToList<ValueTuple<TacticalPosition, float>>();
			if (list.Count > 0)
			{
				TacticalPosition item = list.MaxBy(([TupleElementNames(new string[]
				{
					"tp",
					null
				})] ValueTuple<TacticalPosition, float> pst) => pst.Item2).Item1;
				if (item != this._mainRingPosition)
				{
					this._mainRingPosition = item;
					this.IsTacticReapplyNeeded = true;
					return;
				}
			}
			else
			{
				this._mainRingPosition = null;
			}
		}

		// Token: 0x040004CB RID: 1227
		private const float DefendersAdvantage = 1.5f;

		// Token: 0x040004CC RID: 1228
		private TacticalPosition _mainRingPosition;
	}
}
