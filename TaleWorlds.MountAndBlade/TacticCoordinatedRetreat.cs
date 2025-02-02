using System;
using System.Linq;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000166 RID: 358
	public class TacticCoordinatedRetreat : TacticComponent
	{
		// Token: 0x06001214 RID: 4628 RVA: 0x0003C72F File Offset: 0x0003A92F
		public TacticCoordinatedRetreat(Team team) : base(team)
		{
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x0003C743 File Offset: 0x0003A943
		protected override void ManageFormationCounts()
		{
			if (!this._canWeSafelyRunAway)
			{
				base.AssignTacticFormations1121();
			}
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x0003C754 File Offset: 0x0003A954
		private void OrganizedRetreat()
		{
			if (base.Team.IsPlayerTeam && !base.Team.IsPlayerGeneral && base.Team.IsPlayerSergeant)
			{
				base.SoundTacticalHorn(TacticComponent.RetreatHornSoundIndex);
			}
			if (this._mainInfantry != null)
			{
				this._mainInfantry.AI.ResetBehaviorWeights();
				TacticComponent.SetDefaultBehaviorWeights(this._mainInfantry);
				BehaviorDefend behaviorDefend = this._mainInfantry.AI.SetBehaviorWeight<BehaviorDefend>(1f);
				WorldPosition closestFleePositionForFormation = Mission.Current.GetClosestFleePositionForFormation(this._mainInfantry);
				closestFleePositionForFormation.SetVec2(Mission.Current.GetClosestBoundaryPosition(closestFleePositionForFormation.AsVec2));
				this._retreatPosition = closestFleePositionForFormation.AsVec2;
				behaviorDefend.DefensePosition = closestFleePositionForFormation;
				this._mainInfantry.AI.SetBehaviorWeight<BehaviorPullBack>(1f);
			}
			if (this._archers != null)
			{
				this._archers.AI.ResetBehaviorWeights();
				TacticComponent.SetDefaultBehaviorWeights(this._archers);
				this._archers.AI.SetBehaviorWeight<BehaviorScreenedSkirmish>(1f);
				this._archers.AI.SetBehaviorWeight<BehaviorPullBack>(1.5f);
			}
			if (this._leftCavalry != null)
			{
				this._leftCavalry.AI.ResetBehaviorWeights();
				TacticComponent.SetDefaultBehaviorWeights(this._leftCavalry);
				this._leftCavalry.AI.SetBehaviorWeight<BehaviorProtectFlank>(1f).FlankSide = FormationAI.BehaviorSide.Left;
				this._leftCavalry.AI.SetBehaviorWeight<BehaviorCavalryScreen>(1f);
				this._leftCavalry.AI.SetBehaviorWeight<BehaviorPullBack>(1.5f);
			}
			if (this._rightCavalry != null)
			{
				this._rightCavalry.AI.ResetBehaviorWeights();
				TacticComponent.SetDefaultBehaviorWeights(this._rightCavalry);
				this._rightCavalry.AI.SetBehaviorWeight<BehaviorProtectFlank>(1f).FlankSide = FormationAI.BehaviorSide.Right;
				this._rightCavalry.AI.SetBehaviorWeight<BehaviorCavalryScreen>(1f);
				this._rightCavalry.AI.SetBehaviorWeight<BehaviorPullBack>(1.5f);
			}
			if (this._rangedCavalry != null)
			{
				this._rangedCavalry.AI.ResetBehaviorWeights();
				TacticComponent.SetDefaultBehaviorWeights(this._rangedCavalry);
				this._rangedCavalry.AI.SetBehaviorWeight<BehaviorMountedSkirmish>(1f);
				this._rangedCavalry.AI.SetBehaviorWeight<BehaviorPullBack>(1.5f);
				this._rangedCavalry.AI.SetBehaviorWeight<BehaviorHorseArcherSkirmish>(1f);
			}
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x0003C9A8 File Offset: 0x0003ABA8
		private void RunForTheBorder()
		{
			if (base.Team.IsPlayerTeam && !base.Team.IsPlayerGeneral && base.Team.IsPlayerSergeant)
			{
				base.SoundTacticalHorn(TacticComponent.RetreatHornSoundIndex);
			}
			foreach (Formation formation in base.FormationsIncludingSpecialAndEmpty)
			{
				if (formation.CountOfUnits > 0)
				{
					formation.AI.ResetBehaviorWeights();
					formation.AI.SetBehaviorWeight<BehaviorRetreat>(1f);
				}
			}
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x0003CA4C File Offset: 0x0003AC4C
		private bool HasRetreatDestinationBeenReached()
		{
			return base.FormationsIncludingEmpty.All((Formation f) => f.CountOfUnits == 0 || !f.QuerySystem.IsInfantryFormation || f.QuerySystem.AveragePosition.DistanceSquared(this._retreatPosition) < 5625f);
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x0003CA68 File Offset: 0x0003AC68
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

		// Token: 0x0600121A RID: 4634 RVA: 0x0003CB78 File Offset: 0x0003AD78
		protected internal override void TickOccasionally()
		{
			if (!base.AreFormationsCreated)
			{
				return;
			}
			bool flag = this.HasRetreatDestinationBeenReached();
			if (this.CheckAndSetAvailableFormationsChanged())
			{
				this._canWeSafelyRunAway = flag;
				this.ManageFormationCounts();
				if (this._canWeSafelyRunAway)
				{
					this.RunForTheBorder();
				}
				else
				{
					this.OrganizedRetreat();
				}
				this.IsTacticReapplyNeeded = false;
			}
			if (flag != this._canWeSafelyRunAway || this.IsTacticReapplyNeeded)
			{
				this._canWeSafelyRunAway = flag;
				if (this._canWeSafelyRunAway)
				{
					this.RunForTheBorder();
				}
				else
				{
					this.OrganizedRetreat();
				}
				this.IsTacticReapplyNeeded = false;
			}
			base.TickOccasionally();
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x0003CC04 File Offset: 0x0003AE04
		protected internal override float GetTacticWeight()
		{
			float value = base.Team.QuerySystem.TotalPowerRatio / base.Team.QuerySystem.RemainingPowerRatio;
			float valueTo = MathF.Max(base.Team.QuerySystem.InfantryRatio, MathF.Max(base.Team.QuerySystem.RangedRatio, base.Team.QuerySystem.CavalryRatio));
			float num = MBMath.LinearExtrapolation(0f, valueTo, MBMath.ClampFloat(value, 0f, 4f) / 2f);
			float num2 = 0f;
			int num3 = 0;
			foreach (Team team in base.Team.Mission.Teams)
			{
				if (team.IsEnemyOf(base.Team))
				{
					num3++;
					num2 += team.QuerySystem.CavalryRatio + team.QuerySystem.RangedCavalryRatio;
				}
			}
			if (num3 > 0)
			{
				num2 /= (float)num3;
			}
			float num4 = (num2 == 0f) ? 1.2f : MBMath.Lerp(0.8f, 1.2f, MBMath.ClampFloat((base.Team.QuerySystem.CavalryRatio + base.Team.QuerySystem.RangedCavalryRatio) / num2, 0f, 2f) / 2f, 1E-05f);
			return num * num4 * MathF.Min(1f, MathF.Sqrt(base.Team.QuerySystem.RemainingPowerRatio));
		}

		// Token: 0x040004AC RID: 1196
		private bool _canWeSafelyRunAway;

		// Token: 0x040004AD RID: 1197
		private Vec2 _retreatPosition = Vec2.Invalid;

		// Token: 0x040004AE RID: 1198
		private const float RetreatThresholdValue = 2f;
	}
}
