﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000169 RID: 361
	public class TacticDefensiveLine : TacticComponent
	{
		// Token: 0x0600123B RID: 4667 RVA: 0x000404F4 File Offset: 0x0003E6F4
		public TacticDefensiveLine(Team team) : base(team)
		{
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x000404FD File Offset: 0x0003E6FD
		protected override void ManageFormationCounts()
		{
			base.AssignTacticFormations1121();
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x00040508 File Offset: 0x0003E708
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
				this._mainInfantry.AI.SetBehaviorWeight<BehaviorDefend>(1f).TacticalDefendPosition = this._mainDefensiveLineObject;
				this._mainInfantry.AI.SetBehaviorWeight<BehaviorTacticalCharge>(1f);
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

		// Token: 0x0600123E RID: 4670 RVA: 0x00040710 File Offset: 0x0003E910
		private void Engage()
		{
			if (base.Team.IsPlayerTeam && !base.Team.IsPlayerGeneral && base.Team.IsPlayerSergeant)
			{
				base.SoundTacticalHorn(TacticComponent.AttackHornSoundIndex);
			}
			if (this._mainInfantry != null)
			{
				this._mainInfantry.AI.ResetBehaviorWeights();
				TacticComponent.SetDefaultBehaviorWeights(this._mainInfantry);
				this._mainInfantry.AI.SetBehaviorWeight<BehaviorDefend>(1f).TacticalDefendPosition = this._mainDefensiveLineObject;
				this._mainInfantry.AI.SetBehaviorWeight<BehaviorTacticalCharge>(1f);
			}
			if (this._archers != null)
			{
				this._archers.AI.ResetBehaviorWeights();
				TacticComponent.SetDefaultBehaviorWeights(this._archers);
				this._archers.AI.SetBehaviorWeight<BehaviorSkirmish>(1f);
				this._archers.AI.SetBehaviorWeight<BehaviorScreenedSkirmish>(1f);
				if (this._linkedRangedDefensivePosition != null)
				{
					this._archers.AI.SetBehaviorWeight<BehaviorDefend>(1f).TacticalDefendPosition = this._linkedRangedDefensivePosition;
				}
			}
			if (this._leftCavalry != null)
			{
				this._leftCavalry.AI.ResetBehaviorWeights();
				TacticComponent.SetDefaultBehaviorWeights(this._leftCavalry);
				this._leftCavalry.AI.SetBehaviorWeight<BehaviorFlank>(1f);
				this._leftCavalry.AI.SetBehaviorWeight<BehaviorTacticalCharge>(1f);
			}
			if (this._rightCavalry != null)
			{
				this._rightCavalry.AI.ResetBehaviorWeights();
				TacticComponent.SetDefaultBehaviorWeights(this._rightCavalry);
				this._rightCavalry.AI.SetBehaviorWeight<BehaviorFlank>(1f);
				this._rightCavalry.AI.SetBehaviorWeight<BehaviorTacticalCharge>(1f);
			}
			if (this._rangedCavalry != null)
			{
				this._rangedCavalry.AI.ResetBehaviorWeights();
				TacticComponent.SetDefaultBehaviorWeights(this._rangedCavalry);
				this._rangedCavalry.AI.SetBehaviorWeight<BehaviorMountedSkirmish>(1f);
				this._rangedCavalry.AI.SetBehaviorWeight<BehaviorHorseArcherSkirmish>(1f);
			}
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x0004090C File Offset: 0x0003EB0C
		private bool HasBattleBeenJoined()
		{
			Formation mainInfantry = this._mainInfantry;
			return ((mainInfantry != null) ? mainInfantry.QuerySystem.ClosestEnemyFormation : null) == null || this._mainInfantry.AI.ActiveBehavior is BehaviorCharge || this._mainInfantry.AI.ActiveBehavior is BehaviorTacticalCharge || this._mainInfantry.QuerySystem.MedianPosition.AsVec2.Distance(this._mainInfantry.QuerySystem.ClosestEnemyFormation.MedianPosition.AsVec2) / this._mainInfantry.QuerySystem.ClosestEnemyFormation.MovementSpeedMaximum <= 5f + (this._hasBattleBeenJoined ? 5f : 0f);
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x000409DC File Offset: 0x0003EBDC
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

		// Token: 0x06001241 RID: 4673 RVA: 0x00040AEC File Offset: 0x0003ECEC
		protected internal override void TickOccasionally()
		{
			if (!base.AreFormationsCreated)
			{
				return;
			}
			bool flag = this.HasBattleBeenJoined();
			if (this.CheckAndSetAvailableFormationsChanged())
			{
				this._hasBattleBeenJoined = flag;
				this.ManageFormationCounts();
				if (this._hasBattleBeenJoined)
				{
					this.Engage();
				}
				else
				{
					this.Defend();
				}
				this.IsTacticReapplyNeeded = false;
			}
			if (flag != this._hasBattleBeenJoined || this.IsTacticReapplyNeeded)
			{
				this._hasBattleBeenJoined = flag;
				if (this._hasBattleBeenJoined)
				{
					this.Engage();
				}
				else
				{
					this.Defend();
				}
				this.IsTacticReapplyNeeded = false;
			}
			base.TickOccasionally();
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x00040B76 File Offset: 0x0003ED76
		protected internal override bool ResetTacticalPositions()
		{
			this.DetermineMainDefensiveLine();
			return true;
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x00040B80 File Offset: 0x0003ED80
		protected internal override float GetTacticWeight()
		{
			if (base.Team.TeamAI.IsDefenseApplicable)
			{
				if (base.CheckAndDetermineFormation(ref this._mainInfantry, (Formation f) => f.CountOfUnits > 0 && f.QuerySystem.IsInfantryFormation))
				{
					if (!base.Team.TeamAI.IsCurrentTactic(this) || this._mainDefensiveLineObject == null || !this.IsTacticalPositionEligible(this._mainDefensiveLineObject))
					{
						this.DetermineMainDefensiveLine();
					}
					if (this._mainDefensiveLineObject == null)
					{
						return 0f;
					}
					return (base.Team.QuerySystem.InfantryRatio + base.Team.QuerySystem.RangedRatio) * 1.2f * this.GetTacticalPositionScore(this._mainDefensiveLineObject) * TacticComponent.CalculateNotEngagingTacticalAdvantage(base.Team.QuerySystem) / MathF.Sqrt(base.Team.QuerySystem.RemainingPowerRatio);
				}
			}
			return 0f;
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x00040C6C File Offset: 0x0003EE6C
		private bool IsTacticalPositionEligible(TacticalPosition tacticalPosition)
		{
			if (tacticalPosition.TacticalPositionType == TacticalPosition.TacticalPositionTypeEnum.SpecialMissionPosition)
			{
				return true;
			}
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
			if (num2 > 20f && num2 > num3 * 0.5f)
			{
				return false;
			}
			if (!tacticalPosition.IsInsurmountable)
			{
				vec = base.Team.QuerySystem.AverageEnemyPosition - tacticalPosition.Position.AsVec2;
				vec = vec.Normalized();
				return vec.DotProduct(tacticalPosition.Direction) > 0.5f;
			}
			return true;
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x00040D74 File Offset: 0x0003EF74
		private float GetTacticalPositionScore(TacticalPosition tacticalPosition)
		{
			if (tacticalPosition.TacticalPositionType == TacticalPosition.TacticalPositionTypeEnum.SpecialMissionPosition)
			{
				return 100f;
			}
			if (base.CheckAndDetermineFormation(ref this._mainInfantry, (Formation f) => f.CountOfUnits > 0 && f.QuerySystem.IsInfantryFormation))
			{
				float num = MBMath.Lerp(1f, 1.5f, MBMath.ClampFloat(tacticalPosition.Slope, 0f, 60f) / 60f, 1E-05f);
				float maximumWidth = this._mainInfantry.MaximumWidth;
				float num2 = MBMath.Lerp(0.67f, 1f, (6f - MBMath.ClampFloat(maximumWidth / tacticalPosition.Width, 3f, 6f)) / 3f, 1E-05f);
				float num3 = tacticalPosition.IsInsurmountable ? 1.3f : 1f;
				float num4 = 1f;
				if (this._archers != null)
				{
					if ((from lcp in tacticalPosition.LinkedTacticalPositions
					where lcp.TacticalPositionType == TacticalPosition.TacticalPositionTypeEnum.Cliff
					select lcp).ToList<TacticalPosition>().Count > 0)
					{
						num4 = MBMath.Lerp(1f, 1.5f, (MBMath.ClampFloat(base.Team.QuerySystem.RangedRatio, 0.05f, 0.25f) - 0.05f) * 5f, 1E-05f);
					}
				}
				float rangedFactor = this.GetRangedFactor(tacticalPosition);
				float cavalryFactor = this.GetCavalryFactor(tacticalPosition);
				float value = this._mainInfantry.QuerySystem.AveragePosition.Distance(tacticalPosition.Position.AsVec2);
				float num5 = MBMath.Lerp(0.7f, 1f, (150f - MBMath.ClampFloat(value, 50f, 150f)) / 100f, 1E-05f);
				return num * num2 * num4 * rangedFactor * cavalryFactor * num5 * num3;
			}
			return 0f;
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x00040F54 File Offset: 0x0003F154
		private List<TacticalPosition> ExtractPossibleTacticalPositionsFromTacticalRegion(TacticalRegion tacticalRegion)
		{
			List<TacticalPosition> list = new List<TacticalPosition>();
			if (tacticalRegion.tacticalRegionType == TacticalRegion.TacticalRegionTypeEnum.Forest)
			{
				Vec2 vec = (base.Team.QuerySystem.AverageEnemyPosition - tacticalRegion.Position.AsVec2).Normalized();
				TacticalPosition item = new TacticalPosition(tacticalRegion.Position, vec, tacticalRegion.radius, 0f, false, TacticalPosition.TacticalPositionTypeEnum.Regional, TacticalRegion.TacticalRegionTypeEnum.Forest);
				list.Add(item);
				float f = tacticalRegion.radius * 0.87f;
				TacticalPosition item2 = new TacticalPosition(new WorldPosition(Mission.Current.Scene, UIntPtr.Zero, tacticalRegion.Position.GetNavMeshVec3() + new Vec3(f * vec, 0f, -1f), false), vec, tacticalRegion.radius, 0f, false, TacticalPosition.TacticalPositionTypeEnum.Regional, TacticalRegion.TacticalRegionTypeEnum.Forest);
				list.Add(item2);
				TacticalPosition item3 = new TacticalPosition(new WorldPosition(Mission.Current.Scene, UIntPtr.Zero, tacticalRegion.Position.GetNavMeshVec3() - new Vec3(f * vec, 0f, -1f), false), vec, tacticalRegion.radius, 0f, false, TacticalPosition.TacticalPositionTypeEnum.Regional, TacticalRegion.TacticalRegionTypeEnum.Forest);
				list.Add(item3);
			}
			return list;
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x0004108C File Offset: 0x0003F28C
		private float GetCavalryFactor(TacticalPosition tacticalPosition)
		{
			if (tacticalPosition.TacticalRegionMembership != TacticalRegion.TacticalRegionTypeEnum.Forest)
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
			if (num2 <= 0f)
			{
				num2 = 0.01f;
			}
			return num / num2 / base.Team.QuerySystem.RemainingPowerRatio;
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x000411A0 File Offset: 0x0003F3A0
		private float GetRangedFactor(TacticalPosition tacticalPosition)
		{
			bool isOuterEdge = tacticalPosition.IsOuterEdge;
			if (tacticalPosition.TacticalRegionMembership != TacticalRegion.TacticalRegionTypeEnum.Forest)
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
			num2 -= num2 * (base.Team.QuerySystem.EnemyRangedRatio + base.Team.QuerySystem.EnemyRangedCavalryRatio) * 0.5f;
			if (num2 <= 0f)
			{
				num2 = 0.01f;
			}
			if (!isOuterEdge)
			{
				num -= num * (base.Team.QuerySystem.RangedRatio + base.Team.QuerySystem.RangedCavalryRatio) * 0.5f;
			}
			return num / num2 / base.Team.QuerySystem.RemainingPowerRatio;
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x000412C0 File Offset: 0x0003F4C0
		private void DetermineMainDefensiveLine()
		{
			IEnumerable<ValueTuple<TacticalPosition, float>> first = from tp in base.Team.TeamAI.TacticalPositions
			where (tp.TacticalPositionType == TacticalPosition.TacticalPositionTypeEnum.SpecialMissionPosition || tp.TacticalPositionType == TacticalPosition.TacticalPositionTypeEnum.HighGround) && this.IsTacticalPositionEligible(tp)
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
				if (item != this._mainDefensiveLineObject)
				{
					this._mainDefensiveLineObject = item;
					this.IsTacticReapplyNeeded = true;
				}
				if (this._mainDefensiveLineObject.LinkedTacticalPositions.Count <= 0)
				{
					this._linkedRangedDefensivePosition = null;
					return;
				}
				TacticalPosition tacticalPosition = this._mainDefensiveLineObject.LinkedTacticalPositions.FirstOrDefault<TacticalPosition>();
				if (tacticalPosition != this._linkedRangedDefensivePosition)
				{
					this._linkedRangedDefensivePosition = tacticalPosition;
					this.IsTacticReapplyNeeded = true;
					return;
				}
			}
			else
			{
				this._mainDefensiveLineObject = null;
				this._linkedRangedDefensivePosition = null;
			}
		}

		// Token: 0x040004C7 RID: 1223
		private bool _hasBattleBeenJoined;

		// Token: 0x040004C8 RID: 1224
		private const float DefendersAdvantage = 1.2f;

		// Token: 0x040004C9 RID: 1225
		private TacticalPosition _mainDefensiveLineObject;

		// Token: 0x040004CA RID: 1226
		private TacticalPosition _linkedRangedDefensivePosition;
	}
}
