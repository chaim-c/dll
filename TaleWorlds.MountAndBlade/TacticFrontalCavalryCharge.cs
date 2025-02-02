using System;
using System.Linq;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200016B RID: 363
	public class TacticFrontalCavalryCharge : TacticComponent
	{
		// Token: 0x06001260 RID: 4704 RVA: 0x00041E85 File Offset: 0x00040085
		public TacticFrontalCavalryCharge(Team team) : base(team)
		{
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x00041E90 File Offset: 0x00040090
		protected override void ManageFormationCounts()
		{
			base.ManageFormationCounts(1, 1, 1, 1);
			this._mainInfantry = TacticComponent.ChooseAndSortByPriority(base.FormationsIncludingEmpty, (Formation f) => f.CountOfUnits > 0 && f.QuerySystem.IsInfantryFormation, (Formation f) => f.IsAIControlled, (Formation f) => f.QuerySystem.FormationPower).FirstOrDefault<Formation>();
			if (this._mainInfantry != null)
			{
				this._mainInfantry.AI.IsMainFormation = true;
			}
			this._archers = TacticComponent.ChooseAndSortByPriority(base.FormationsIncludingEmpty, (Formation f) => f.CountOfUnits > 0 && f.QuerySystem.IsRangedFormation, (Formation f) => f.IsAIControlled, (Formation f) => f.QuerySystem.FormationPower).FirstOrDefault<Formation>();
			this._cavalry = TacticComponent.ChooseAndSortByPriority(base.FormationsIncludingEmpty, (Formation f) => f.CountOfUnits > 0 && f.QuerySystem.IsCavalryFormation, (Formation f) => f.IsAIControlled, (Formation f) => f.QuerySystem.FormationPower).FirstOrDefault<Formation>();
			this._rangedCavalry = TacticComponent.ChooseAndSortByPriority(base.FormationsIncludingEmpty, (Formation f) => f.CountOfUnits > 0 && f.QuerySystem.IsRangedCavalryFormation, (Formation f) => f.IsAIControlled, (Formation f) => f.QuerySystem.FormationPower).FirstOrDefault<Formation>();
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x0004208C File Offset: 0x0004028C
		private void Advance()
		{
			if (base.Team.IsPlayerTeam && !base.Team.IsPlayerGeneral && base.Team.IsPlayerSergeant)
			{
				base.SoundTacticalHorn(TacticComponent.MoveHornSoundIndex);
			}
			if (this._mainInfantry != null)
			{
				this._mainInfantry.AI.ResetBehaviorWeights();
				TacticComponent.SetDefaultBehaviorWeights(this._mainInfantry);
				this._mainInfantry.AI.SetBehaviorWeight<BehaviorAdvance>(1f);
			}
			if (this._archers != null)
			{
				this._archers.AI.ResetBehaviorWeights();
				TacticComponent.SetDefaultBehaviorWeights(this._archers);
				this._archers.AI.SetBehaviorWeight<BehaviorSkirmishLine>(1f);
				this._archers.AI.SetBehaviorWeight<BehaviorScreenedSkirmish>(1f);
				this._archers.AI.SetBehaviorWeight<BehaviorSkirmish>(1f);
			}
			if (this._cavalry != null)
			{
				this._cavalry.AI.ResetBehaviorWeights();
				TacticComponent.SetDefaultBehaviorWeights(this._cavalry);
				this._cavalry.AI.SetBehaviorWeight<BehaviorAdvance>(1f);
				this._cavalry.AI.SetBehaviorWeight<BehaviorVanguard>(1f);
			}
			if (this._rangedCavalry != null)
			{
				this._rangedCavalry.AI.ResetBehaviorWeights();
				TacticComponent.SetDefaultBehaviorWeights(this._rangedCavalry);
				this._rangedCavalry.AI.SetBehaviorWeight<BehaviorMountedSkirmish>(1f);
				this._rangedCavalry.AI.SetBehaviorWeight<BehaviorHorseArcherSkirmish>(1f);
			}
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x00042208 File Offset: 0x00040408
		private void Attack()
		{
			if (base.Team.IsPlayerTeam && !base.Team.IsPlayerGeneral && base.Team.IsPlayerSergeant)
			{
				base.SoundTacticalHorn(TacticComponent.AttackHornSoundIndex);
			}
			if (this._mainInfantry != null)
			{
				this._mainInfantry.AI.ResetBehaviorWeights();
				TacticComponent.SetDefaultBehaviorWeights(this._mainInfantry);
				this._mainInfantry.AI.SetBehaviorWeight<BehaviorAdvance>(1f);
				this._mainInfantry.AI.SetBehaviorWeight<BehaviorTacticalCharge>(1f);
			}
			if (this._archers != null)
			{
				this._archers.AI.ResetBehaviorWeights();
				TacticComponent.SetDefaultBehaviorWeights(this._archers);
				this._archers.AI.SetBehaviorWeight<BehaviorScreenedSkirmish>(1f);
				this._archers.AI.SetBehaviorWeight<BehaviorSkirmish>(1f);
			}
			if (this._cavalry != null)
			{
				this._cavalry.AI.ResetBehaviorWeights();
				TacticComponent.SetDefaultBehaviorWeights(this._cavalry);
				this._cavalry.AI.SetBehaviorWeight<BehaviorFlank>(1f);
				this._cavalry.AI.SetBehaviorWeight<BehaviorTacticalCharge>(1f);
			}
			if (this._rangedCavalry != null)
			{
				this._rangedCavalry.AI.ResetBehaviorWeights();
				TacticComponent.SetDefaultBehaviorWeights(this._rangedCavalry);
				this._rangedCavalry.AI.SetBehaviorWeight<BehaviorMountedSkirmish>(1f);
				this._rangedCavalry.AI.SetBehaviorWeight<BehaviorHorseArcherSkirmish>(1f);
			}
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x00042384 File Offset: 0x00040584
		private bool HasBattleBeenJoined()
		{
			Formation cavalry = this._cavalry;
			return ((cavalry != null) ? cavalry.QuerySystem.ClosestEnemyFormation : null) == null || this._cavalry.AI.ActiveBehavior is BehaviorCharge || this._cavalry.AI.ActiveBehavior is BehaviorTacticalCharge || this._cavalry.QuerySystem.MedianPosition.AsVec2.Distance(this._cavalry.QuerySystem.ClosestEnemyFormation.MedianPosition.AsVec2) / this._cavalry.QuerySystem.ClosestEnemyFormation.MovementSpeedMaximum <= 7f + (this._hasBattleBeenJoined ? 7f : 0f);
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x00042454 File Offset: 0x00040654
		protected override bool CheckAndSetAvailableFormationsChanged()
		{
			int aicontrolledFormationCount = base.Team.GetAIControlledFormationCount();
			bool flag = aicontrolledFormationCount != this._AIControlledFormationCount;
			if (flag)
			{
				this._AIControlledFormationCount = aicontrolledFormationCount;
				this.IsTacticReapplyNeeded = true;
			}
			return flag || (this._mainInfantry != null && (this._mainInfantry.CountOfUnits == 0 || !this._mainInfantry.QuerySystem.IsInfantryFormation)) || (this._archers != null && (this._archers.CountOfUnits == 0 || !this._archers.QuerySystem.IsRangedFormation)) || (this._cavalry != null && (this._cavalry.CountOfUnits == 0 || !this._cavalry.QuerySystem.IsCavalryFormation)) || (this._rangedCavalry != null && (this._rangedCavalry.CountOfUnits == 0 || !this._rangedCavalry.QuerySystem.IsRangedCavalryFormation));
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x00042538 File Offset: 0x00040738
		protected internal override void TickOccasionally()
		{
			if (!base.AreFormationsCreated)
			{
				return;
			}
			if (this.CheckAndSetAvailableFormationsChanged())
			{
				this.ManageFormationCounts();
				if (this._hasBattleBeenJoined)
				{
					this.Attack();
				}
				else
				{
					this.Advance();
				}
				this.IsTacticReapplyNeeded = false;
			}
			bool flag = this.HasBattleBeenJoined();
			if (flag != this._hasBattleBeenJoined || this.IsTacticReapplyNeeded)
			{
				this._hasBattleBeenJoined = flag;
				if (this._hasBattleBeenJoined)
				{
					this.Attack();
				}
				else
				{
					this.Advance();
				}
				this.IsTacticReapplyNeeded = false;
			}
			base.TickOccasionally();
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x000425BC File Offset: 0x000407BC
		protected internal override float GetTacticWeight()
		{
			float num = base.Team.QuerySystem.RangedCavalryRatio * (float)base.Team.QuerySystem.MemberCount;
			return base.Team.QuerySystem.CavalryRatio * (float)base.Team.QuerySystem.MemberCount / ((float)base.Team.QuerySystem.MemberCount - num) * MathF.Sqrt(base.Team.QuerySystem.RemainingPowerRatio);
		}

		// Token: 0x040004CD RID: 1229
		private Formation _cavalry;

		// Token: 0x040004CE RID: 1230
		private bool _hasBattleBeenJoined;
	}
}
