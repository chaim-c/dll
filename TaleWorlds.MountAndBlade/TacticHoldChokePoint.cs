using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200016D RID: 365
	public class TacticHoldChokePoint : TacticComponent
	{
		// Token: 0x06001270 RID: 4720 RVA: 0x00042CD0 File Offset: 0x00040ED0
		public TacticHoldChokePoint(Team team) : base(team)
		{
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x00042CD9 File Offset: 0x00040ED9
		protected override void ManageFormationCounts()
		{
			base.AssignTacticFormations1121();
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x00042CE4 File Offset: 0x00040EE4
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
				this._mainInfantry.AI.SetBehaviorWeight<BehaviorDefend>(1f).TacticalDefendPosition = this._chokePointTacticalPosition;
			}
			if (this._archers != null)
			{
				this._archers.AI.ResetBehaviorWeights();
				TacticComponent.SetDefaultBehaviorWeights(this._archers);
				this._archers.AI.SetBehaviorWeight<BehaviorSkirmishLine>(1f);
				this._archers.AI.SetBehaviorWeight<BehaviorScreenedSkirmish>(1f);
				if (this._linkedRangedDefensivePosition != null)
				{
					this._archers.AI.SetBehaviorWeight<BehaviorDefend>(10f).TacticalDefendPosition = this._linkedRangedDefensivePosition;
				}
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

		// Token: 0x06001273 RID: 4723 RVA: 0x00042ED4 File Offset: 0x000410D4
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

		// Token: 0x06001274 RID: 4724 RVA: 0x00042FE4 File Offset: 0x000411E4
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

		// Token: 0x06001275 RID: 4725 RVA: 0x00043018 File Offset: 0x00041218
		protected internal override float GetTacticWeight()
		{
			if (base.Team.TeamAI.IsDefenseApplicable)
			{
				if (base.CheckAndDetermineFormation(ref this._mainInfantry, (Formation f) => f.CountOfUnits > 0 && f.QuerySystem.IsInfantryFormation))
				{
					if (!base.Team.TeamAI.IsCurrentTactic(this) || this._chokePointTacticalPosition == null || !this.IsTacticalPositionEligible(this._chokePointTacticalPosition))
					{
						this.DetermineChokePoints();
					}
					if (this._chokePointTacticalPosition == null)
					{
						return 0f;
					}
					float infantryRatio = base.Team.QuerySystem.InfantryRatio;
					float num = MathF.Min(infantryRatio, base.Team.QuerySystem.RangedRatio);
					float num2 = infantryRatio + num;
					float num3 = MBMath.ClampFloat((float)base.Team.QuerySystem.EnemyUnitCount / (float)base.Team.QuerySystem.MemberCount, 0.33f, 3f);
					return num2 * num3 * this.GetTacticalPositionScore(this._chokePointTacticalPosition) * TacticComponent.CalculateNotEngagingTacticalAdvantage(base.Team.QuerySystem) * 1.3f / MathF.Sqrt(base.Team.QuerySystem.RemainingPowerRatio);
				}
			}
			return 0f;
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x00043140 File Offset: 0x00041340
		private bool IsTacticalPositionEligible(TacticalPosition tacticalPosition)
		{
			if (!base.CheckAndDetermineFormation(ref this._mainInfantry, (Formation f) => f.CountOfUnits > 0 && f.QuerySystem.IsInfantryFormation))
			{
				return false;
			}
			float num = base.Team.QuerySystem.AveragePosition.Distance(tacticalPosition.Position.AsVec2);
			float num2 = base.Team.QuerySystem.AverageEnemyPosition.Distance(this._mainInfantry.QuerySystem.AveragePosition);
			if (num > 20f && num > num2 * 0.5f)
			{
				return false;
			}
			if (this._mainInfantry.MaximumWidth < tacticalPosition.Width)
			{
				return false;
			}
			float num3 = (base.Team.QuerySystem.AverageEnemyPosition - tacticalPosition.Position.AsVec2).Normalized().DotProduct(tacticalPosition.Direction);
			if (tacticalPosition.IsInsurmountable)
			{
				return MathF.Abs(num3) >= 0.5f;
			}
			return num3 >= 0.5f;
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x0004325C File Offset: 0x0004145C
		private float GetTacticalPositionScore(TacticalPosition tacticalPosition)
		{
			if (base.CheckAndDetermineFormation(ref this._mainInfantry, (Formation f) => f.CountOfUnits > 0 && f.QuerySystem.IsInfantryFormation))
			{
				float num = MBMath.Lerp(1f, 1.5f, MBMath.ClampFloat(tacticalPosition.Slope, 0f, 60f) / 60f, 1E-05f);
				int countOfUnits = this._mainInfantry.CountOfUnits;
				float num2 = this._mainInfantry.Interval * (float)(countOfUnits - 1) + this._mainInfantry.UnitDiameter * (float)countOfUnits;
				float num3 = MBMath.Lerp(0.67f, 1.5f, (MBMath.ClampFloat(num2 / tacticalPosition.Width, 0.5f, 3f) - 0.5f) / 2.5f, 1E-05f);
				float num4 = 1f;
				if (base.CheckAndDetermineFormation(ref this._archers, (Formation f) => f.CountOfUnits > 0 && f.QuerySystem.IsRangedFormation))
				{
					if ((from lcp in tacticalPosition.LinkedTacticalPositions
					where lcp.TacticalPositionType == TacticalPosition.TacticalPositionTypeEnum.Cliff
					select lcp).ToList<TacticalPosition>().Count > 0)
					{
						num4 = MBMath.Lerp(1f, 1.5f, (MBMath.ClampFloat(base.Team.QuerySystem.RangedRatio, 0.05f, 0.25f) - 0.05f) * 5f, 1E-05f);
					}
				}
				float value = this._mainInfantry.QuerySystem.AveragePosition.Distance(tacticalPosition.Position.AsVec2);
				float num5 = MBMath.Lerp(0.7f, 1f, (150f - MBMath.ClampFloat(value, 50f, 150f)) / 100f, 1E-05f);
				return num * num3 * num4 * num5;
			}
			return 0f;
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x00043440 File Offset: 0x00041640
		protected internal override bool ResetTacticalPositions()
		{
			this.DetermineChokePoints();
			return true;
		}

		// Token: 0x06001279 RID: 4729 RVA: 0x0004344C File Offset: 0x0004164C
		private void DetermineChokePoints()
		{
			IEnumerable<ValueTuple<TacticalPosition, float>> first = from tp in base.Team.TeamAI.TacticalPositions
			where tp.TacticalPositionType == TacticalPosition.TacticalPositionTypeEnum.ChokePoint && this.IsTacticalPositionEligible(tp)
			select new ValueTuple<TacticalPosition, float>(tp, this.GetTacticalPositionScore(tp));
			IEnumerable<ValueTuple<TacticalPosition, float>> second = from tp in base.Team.TeamAI.TacticalRegions.SelectMany((TacticalRegion r) => from tpftr in r.LinkedTacticalPositions
			where tpftr.TacticalPositionType == TacticalPosition.TacticalPositionTypeEnum.ChokePoint && this.IsTacticalPositionEligible(tpftr)
			select tpftr)
			select new ValueTuple<TacticalPosition, float>(tp, this.GetTacticalPositionScore(tp));
			IEnumerable<ValueTuple<TacticalPosition, float>> source = first.Concat(second);
			if (source.Any<ValueTuple<TacticalPosition, float>>())
			{
				TacticalPosition item = source.MaxBy(([TupleElementNames(new string[]
				{
					"tp",
					null
				})] ValueTuple<TacticalPosition, float> pst) => pst.Item2).Item1;
				if (item != this._chokePointTacticalPosition)
				{
					this._chokePointTacticalPosition = item;
					this.IsTacticReapplyNeeded = true;
				}
				if (this._chokePointTacticalPosition.LinkedTacticalPositions.Count <= 0)
				{
					this._linkedRangedDefensivePosition = null;
					return;
				}
				TacticalPosition tacticalPosition = this._chokePointTacticalPosition.LinkedTacticalPositions.FirstOrDefault<TacticalPosition>();
				if (tacticalPosition != this._linkedRangedDefensivePosition)
				{
					this._linkedRangedDefensivePosition = tacticalPosition;
					this.IsTacticReapplyNeeded = true;
					return;
				}
			}
			else
			{
				this._chokePointTacticalPosition = null;
			}
		}

		// Token: 0x040004D0 RID: 1232
		private const float DefendersAdvantage = 1.3f;

		// Token: 0x040004D1 RID: 1233
		private TacticalPosition _chokePointTacticalPosition;

		// Token: 0x040004D2 RID: 1234
		private TacticalPosition _linkedRangedDefensivePosition;
	}
}
