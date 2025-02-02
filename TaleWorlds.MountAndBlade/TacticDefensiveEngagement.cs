using System;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000168 RID: 360
	public class TacticDefensiveEngagement : TacticComponent
	{
		// Token: 0x06001233 RID: 4659 RVA: 0x0003FD55 File Offset: 0x0003DF55
		public TacticDefensiveEngagement(Team team) : base(team)
		{
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x0003FD5E File Offset: 0x0003DF5E
		protected override void ManageFormationCounts()
		{
			base.AssignTacticFormations1121();
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x0003FD68 File Offset: 0x0003DF68
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
				this._mainInfantry.AI.SetBehaviorWeight<BehaviorHoldHighGround>(1f).RangedAllyFormation = this._archers;
				this._mainInfantry.AI.SetBehaviorWeight<BehaviorTacticalCharge>(1f);
			}
			if (this._archers != null)
			{
				this._archers.AI.ResetBehaviorWeights();
				TacticComponent.SetDefaultBehaviorWeights(this._archers);
				this._archers.AI.SetBehaviorWeight<BehaviorSkirmishLine>(1f);
				this._archers.AI.SetBehaviorWeight<BehaviorScreenedSkirmish>(1f);
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

		// Token: 0x06001236 RID: 4662 RVA: 0x0003FF48 File Offset: 0x0003E148
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
				this._mainInfantry.AI.SetBehaviorWeight<BehaviorHoldHighGround>(1f).RangedAllyFormation = this._archers;
				this._mainInfantry.AI.SetBehaviorWeight<BehaviorTacticalCharge>(1f);
			}
			if (this._archers != null)
			{
				this._archers.AI.ResetBehaviorWeights();
				TacticComponent.SetDefaultBehaviorWeights(this._archers);
				this._archers.AI.SetBehaviorWeight<BehaviorSkirmish>(1f);
				this._archers.AI.SetBehaviorWeight<BehaviorScreenedSkirmish>(1f);
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

		// Token: 0x06001237 RID: 4663 RVA: 0x0004011C File Offset: 0x0003E31C
		private bool HasBattleBeenJoined()
		{
			Formation mainInfantry = this._mainInfantry;
			return ((mainInfantry != null) ? mainInfantry.QuerySystem.ClosestEnemyFormation : null) == null || this._mainInfantry.AI.ActiveBehavior is BehaviorCharge || this._mainInfantry.AI.ActiveBehavior is BehaviorTacticalCharge || this._mainInfantry.QuerySystem.MedianPosition.AsVec2.Distance(this._mainInfantry.QuerySystem.ClosestEnemyFormation.MedianPosition.AsVec2) / this._mainInfantry.QuerySystem.ClosestEnemyFormation.MovementSpeedMaximum <= 5f + (this._hasBattleBeenJoined ? 5f : 0f);
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x000401EC File Offset: 0x0003E3EC
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

		// Token: 0x06001239 RID: 4665 RVA: 0x000402FC File Offset: 0x0003E4FC
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

		// Token: 0x0600123A RID: 4666 RVA: 0x00040388 File Offset: 0x0003E588
		protected internal override float GetTacticWeight()
		{
			if (base.Team.TeamAI.IsDefenseApplicable)
			{
				if (!base.FormationsIncludingEmpty.All((Formation f) => f.CountOfUnits == 0 || !f.QuerySystem.IsInfantryFormation))
				{
					Formation formation;
					if ((formation = this._mainInfantry) == null)
					{
						formation = (from f in base.FormationsIncludingEmpty
						where f.CountOfUnits > 0 && f.QuerySystem.IsInfantryFormation
						select f).MaxBy((Formation f) => f.CountOfUnits);
					}
					Formation formation2 = formation;
					if (formation2 == null)
					{
						return 0f;
					}
					if (this._mainInfantry == null)
					{
						this._mainInfantry = formation2;
					}
					float num = base.Team.QuerySystem.InfantryRatio + base.Team.QuerySystem.RangedRatio;
					float value = this._mainInfantry.QuerySystem.AveragePosition.Distance(this._mainInfantry.QuerySystem.HighGroundCloseToForeseenBattleGround);
					float num2 = MBMath.Lerp(0.7f, 1f, (150f - MBMath.ClampFloat(value, 50f, 150f)) / 100f, 1E-05f);
					return num * 1.1f * TacticComponent.CalculateNotEngagingTacticalAdvantage(base.Team.QuerySystem) * num2 / MathF.Sqrt(base.Team.QuerySystem.RemainingPowerRatio);
				}
			}
			return 0f;
		}

		// Token: 0x040004C5 RID: 1221
		private const float DefendersAdvantage = 1.1f;

		// Token: 0x040004C6 RID: 1222
		private bool _hasBattleBeenJoined;
	}
}
