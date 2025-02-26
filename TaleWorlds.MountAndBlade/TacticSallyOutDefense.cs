﻿using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000170 RID: 368
	public class TacticSallyOutDefense : TacticComponent
	{
		// Token: 0x06001292 RID: 4754 RVA: 0x000445A8 File Offset: 0x000427A8
		protected override void ManageFormationCounts()
		{
			if (this._weaponsToBeDefendedState == TacticSallyOutDefense.WeaponsToBeDefended.TwoPrimary)
			{
				base.ManageFormationCounts(1, 1, 1, 1);
				this._mainInfantry = TacticComponent.ChooseAndSortByPriority(base.FormationsIncludingEmpty, (Formation f) => f.CountOfUnits > 0 && f.QuerySystem.IsInfantryFormation, (Formation f) => f.IsAIControlled, (Formation f) => f.QuerySystem.FormationPower).FirstOrDefault<Formation>();
				if (this._mainInfantry != null)
				{
					this._mainInfantry.AI.IsMainFormation = true;
				}
				this._archers = TacticComponent.ChooseAndSortByPriority(base.FormationsIncludingEmpty, (Formation f) => f.CountOfUnits > 0 && f.QuerySystem.IsRangedFormation, (Formation f) => f.IsAIControlled, (Formation f) => f.QuerySystem.FormationPower).FirstOrDefault<Formation>();
				this._cavalryFormation = TacticComponent.ChooseAndSortByPriority(base.FormationsIncludingEmpty, (Formation f) => f.CountOfUnits > 0 && f.QuerySystem.IsCavalryFormation, (Formation f) => f.IsAIControlled, (Formation f) => f.QuerySystem.FormationPower).FirstOrDefault<Formation>();
				this._rangedCavalry = TacticComponent.ChooseAndSortByPriority(base.FormationsIncludingEmpty, (Formation f) => f.CountOfUnits > 0 && f.QuerySystem.IsRangedCavalryFormation, (Formation f) => f.IsAIControlled, (Formation f) => f.QuerySystem.FormationPower).FirstOrDefault<Formation>();
				return;
			}
			base.AssignTacticFormations1121();
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x000447B8 File Offset: 0x000429B8
		private void Engage()
		{
			if (this._leftCavalry != null)
			{
				this._leftCavalry.AI.SetBehaviorWeight<BehaviorFlank>(1f);
				this._leftCavalry.AI.SetBehaviorWeight<BehaviorTacticalCharge>(1f);
			}
			if (this._rightCavalry != null)
			{
				this._rightCavalry.AI.SetBehaviorWeight<BehaviorFlank>(1f);
				this._rightCavalry.AI.SetBehaviorWeight<BehaviorTacticalCharge>(1f);
			}
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x00044830 File Offset: 0x00042A30
		private void DetermineState()
		{
			if (this._destructableSiegeWeapons.Count == 0)
			{
				this._weaponsToBeDefendedState = TacticSallyOutDefense.WeaponsToBeDefended.NoWeapons;
				return;
			}
			IEnumerable<SiegeWeapon> destructableSiegeWeapons = this._destructableSiegeWeapons;
			Func<SiegeWeapon, bool> predicate = (SiegeWeapon dsw) => dsw is IPrimarySiegeWeapon && !dsw.IsDisabled && !dsw.IsDestroyed;
			switch (destructableSiegeWeapons.Count(predicate))
			{
			case 0:
				this._weaponsToBeDefendedState = TacticSallyOutDefense.WeaponsToBeDefended.OnlyRangedWeapons;
				return;
			case 1:
				this._weaponsToBeDefendedState = TacticSallyOutDefense.WeaponsToBeDefended.OnePrimary;
				return;
			case 2:
				this._weaponsToBeDefendedState = TacticSallyOutDefense.WeaponsToBeDefended.TwoPrimary;
				return;
			case 3:
				this._weaponsToBeDefendedState = TacticSallyOutDefense.WeaponsToBeDefended.ThreePrimary;
				return;
			default:
				return;
			}
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x000448B4 File Offset: 0x00042AB4
		public TacticSallyOutDefense(Team team) : base(team)
		{
			this._teamAISallyOutDefender = (team.TeamAI as TeamAISallyOutDefender);
			this._destructableSiegeWeapons = (from sw in Mission.Current.ActiveMissionObjects.FindAllWithType<SiegeWeapon>()
			where sw.Side == team.Side && sw.IsDestructible
			select sw).ToList<SiegeWeapon>();
			this.SallyOutDefensePosition = ((team.TeamAI is TeamAISallyOutDefender) ? (team.TeamAI as TeamAISallyOutDefender).DefensePosition() : WorldPosition.Invalid);
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x00044954 File Offset: 0x00042B54
		private bool CalculateHasBattleBeenJoined()
		{
			Formation mainInfantry = this._mainInfantry;
			return ((mainInfantry != null) ? mainInfantry.QuerySystem.ClosestEnemyFormation : null) == null || this._mainInfantry.AI.ActiveBehavior is BehaviorCharge || this._mainInfantry.AI.ActiveBehavior is BehaviorTacticalCharge || this._mainInfantry.QuerySystem.MedianPosition.AsVec2.Distance(this._mainInfantry.QuerySystem.ClosestEnemyFormation.MedianPosition.AsVec2) / this._mainInfantry.QuerySystem.ClosestEnemyFormation.MovementSpeedMaximum <= 3f + (this._hasBattleBeenJoined ? 2f : 0f);
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x00044A24 File Offset: 0x00042C24
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

		// Token: 0x06001298 RID: 4760 RVA: 0x00044B34 File Offset: 0x00042D34
		private void DefendCenterLocation()
		{
			if (this._mainInfantry != null)
			{
				this._mainInfantry.AI.ResetBehaviorWeights();
				TacticComponent.SetDefaultBehaviorWeights(this._mainInfantry);
				this._mainInfantry.AI.SetBehaviorWeight<BehaviorDefendSiegeWeapon>(1f);
				BehaviorDefendSiegeWeapon behavior = this._mainInfantry.AI.GetBehavior<BehaviorDefendSiegeWeapon>();
				behavior.SetDefensePositionFromTactic(this._teamAISallyOutDefender.CalculateSallyOutReferencePosition(FormationAI.BehaviorSide.Middle).ToWorldPosition());
				behavior.SetDefendedSiegeWeaponFromTactic(this._teamAISallyOutDefender.PrimarySiegeWeapons.FirstOrDefault((IPrimarySiegeWeapon psw) => psw.WeaponSide == FormationAI.BehaviorSide.Middle && psw is SiegeWeapon) as SiegeWeapon);
			}
			if (this._archers != null)
			{
				this._archers.AI.ResetBehaviorWeights();
				TacticComponent.SetDefaultBehaviorWeights(this._archers);
				this._archers.AI.SetBehaviorWeight<BehaviorSkirmishLine>(1f);
				this._archers.AI.SetBehaviorWeight<BehaviorScreenedSkirmish>(1f);
				this._archers.AI.SetBehaviorWeight<BehaviorSkirmish>(1f);
			}
			if (this._leftCavalry != null)
			{
				this._leftCavalry.AI.ResetBehaviorWeights();
				TacticComponent.SetDefaultBehaviorWeights(this._leftCavalry);
				this._leftCavalry.AI.SetBehaviorWeight<BehaviorDefendSiegeWeapon>(1.5f);
				BehaviorDefendSiegeWeapon behavior2 = this._leftCavalry.AI.GetBehavior<BehaviorDefendSiegeWeapon>();
				behavior2.SetDefensePositionFromTactic(this._teamAISallyOutDefender.CalculateSallyOutReferencePosition(FormationAI.BehaviorSide.Left).ToWorldPosition());
				behavior2.SetDefendedSiegeWeaponFromTactic(this._teamAISallyOutDefender.PrimarySiegeWeapons.FirstOrDefault((IPrimarySiegeWeapon psw) => psw.WeaponSide == FormationAI.BehaviorSide.Left && psw is SiegeWeapon) as SiegeWeapon);
				BehaviorProtectFlank behaviorProtectFlank = this._leftCavalry.AI.SetBehaviorWeight<BehaviorProtectFlank>(1f);
				behaviorProtectFlank.FlankSide = FormationAI.BehaviorSide.Left;
				this._leftCavalry.AI.AddSpecialBehavior(behaviorProtectFlank, true);
			}
			if (this._rightCavalry != null)
			{
				this._rightCavalry.AI.ResetBehaviorWeights();
				TacticComponent.SetDefaultBehaviorWeights(this._rightCavalry);
				this._rightCavalry.AI.SetBehaviorWeight<BehaviorDefendSiegeWeapon>(1.5f);
				BehaviorDefendSiegeWeapon behavior3 = this._rightCavalry.AI.GetBehavior<BehaviorDefendSiegeWeapon>();
				behavior3.SetDefensePositionFromTactic(this._teamAISallyOutDefender.CalculateSallyOutReferencePosition(FormationAI.BehaviorSide.Right).ToWorldPosition());
				behavior3.SetDefendedSiegeWeaponFromTactic(this._teamAISallyOutDefender.PrimarySiegeWeapons.FirstOrDefault((IPrimarySiegeWeapon psw) => psw.WeaponSide == FormationAI.BehaviorSide.Right && psw is SiegeWeapon) as SiegeWeapon);
				this._rightCavalry.AI.SetBehaviorWeight<BehaviorTacticalCharge>(1f);
				BehaviorProtectFlank behaviorProtectFlank2 = this._leftCavalry.AI.SetBehaviorWeight<BehaviorProtectFlank>(1f);
				behaviorProtectFlank2.FlankSide = FormationAI.BehaviorSide.Right;
				this._rightCavalry.AI.AddSpecialBehavior(behaviorProtectFlank2, true);
			}
			if (this._rangedCavalry != null)
			{
				this._rangedCavalry.AI.ResetBehaviorWeights();
				TacticComponent.SetDefaultBehaviorWeights(this._rangedCavalry);
				this._rangedCavalry.AI.SetBehaviorWeight<BehaviorHorseArcherSkirmish>(1f);
				this._rangedCavalry.AI.SetBehaviorWeight<BehaviorScreenedSkirmish>(0.3f);
			}
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x00044E40 File Offset: 0x00043040
		private void DefendTwoMainPositions()
		{
			FormationAI.BehaviorSide infantrySide = FormationAI.BehaviorSide.BehaviorSideNotSet;
			if (this._mainInfantry != null)
			{
				this._mainInfantry.AI.ResetBehaviorWeights();
				TacticComponent.SetDefaultBehaviorWeights(this._mainInfantry);
				this._mainInfantry.AI.SetBehaviorWeight<BehaviorDefendSiegeWeapon>(1f);
				BehaviorDefendSiegeWeapon behavior = this._mainInfantry.AI.GetBehavior<BehaviorDefendSiegeWeapon>();
				SiegeWeapon siegeWeapon = this._destructableSiegeWeapons.FirstOrDefault((SiegeWeapon dsw) => dsw is IPrimarySiegeWeapon && dsw is IMoveableSiegeWeapon && (dsw as IPrimarySiegeWeapon).WeaponSide == FormationAI.BehaviorSide.Middle);
				if (siegeWeapon != null)
				{
					infantrySide = FormationAI.BehaviorSide.Middle;
				}
				else
				{
					siegeWeapon = (from dsw in this._destructableSiegeWeapons
					where dsw is IPrimarySiegeWeapon && dsw is IMoveableSiegeWeapon
					select dsw).MinBy((SiegeWeapon dsw) => dsw.GameEntity.GlobalPosition.AsVec2.DistanceSquared(this._mainInfantry.QuerySystem.AveragePosition));
					infantrySide = (siegeWeapon as IPrimarySiegeWeapon).WeaponSide;
				}
				behavior.SetDefensePositionFromTactic(this._teamAISallyOutDefender.CalculateSallyOutReferencePosition(infantrySide).ToWorldPosition());
				behavior.SetDefendedSiegeWeaponFromTactic(siegeWeapon);
				this._mainInfantry.AI.SetBehaviorWeight<BehaviorTacticalCharge>(1f);
				this._mainInfantry.AI.Side = infantrySide;
			}
			if (this._archers != null)
			{
				this._archers.AI.ResetBehaviorWeights();
				TacticComponent.SetDefaultBehaviorWeights(this._archers);
				this._archers.AI.SetBehaviorWeight<BehaviorSkirmishLine>(1f);
				this._archers.AI.SetBehaviorWeight<BehaviorScreenedSkirmish>(1f);
				this._archers.AI.SetBehaviorWeight<BehaviorSkirmish>(1f);
			}
			if (this._cavalryFormation != null)
			{
				if (infantrySide != FormationAI.BehaviorSide.BehaviorSideNotSet)
				{
					this._cavalryFormation.AI.ResetBehaviorWeights();
					TacticComponent.SetDefaultBehaviorWeights(this._cavalryFormation);
					this._cavalryFormation.AI.SetBehaviorWeight<BehaviorDefendSiegeWeapon>(1f);
					BehaviorDefendSiegeWeapon behavior2 = this._cavalryFormation.AI.GetBehavior<BehaviorDefendSiegeWeapon>();
					SiegeWeapon siegeWeapon2 = this._destructableSiegeWeapons.FirstOrDefault((SiegeWeapon dsw) => dsw is IPrimarySiegeWeapon && (dsw as IPrimarySiegeWeapon).WeaponSide != infantrySide);
					FormationAI.BehaviorSide weaponSide;
					if (siegeWeapon2 != null)
					{
						weaponSide = (siegeWeapon2 as IPrimarySiegeWeapon).WeaponSide;
					}
					else
					{
						weaponSide = ((from dsw in this._destructableSiegeWeapons
						where dsw is IPrimarySiegeWeapon && dsw is IMoveableSiegeWeapon
						select dsw).MinBy((SiegeWeapon dsw) => dsw.GameEntity.GlobalPosition.AsVec2.DistanceSquared(this._cavalryFormation.QuerySystem.AveragePosition)) as IPrimarySiegeWeapon).WeaponSide;
					}
					behavior2.SetDefensePositionFromTactic(this._teamAISallyOutDefender.CalculateSallyOutReferencePosition(weaponSide).ToWorldPosition());
					behavior2.SetDefendedSiegeWeaponFromTactic(siegeWeapon2);
					this._cavalryFormation.AI.Side = weaponSide;
				}
				else
				{
					this._cavalryFormation.AI.ResetBehaviorWeights();
					TacticComponent.SetDefaultBehaviorWeights(this._cavalryFormation);
					this._cavalryFormation.AI.SetBehaviorWeight<BehaviorDefendSiegeWeapon>(1f);
					BehaviorDefendSiegeWeapon behavior3 = this._cavalryFormation.AI.GetBehavior<BehaviorDefendSiegeWeapon>();
					SiegeWeapon siegeWeapon3 = this._destructableSiegeWeapons.FirstOrDefault((SiegeWeapon dsw) => dsw is IPrimarySiegeWeapon && (dsw as IPrimarySiegeWeapon).WeaponSide == FormationAI.BehaviorSide.Middle);
					FormationAI.BehaviorSide side;
					if (siegeWeapon3 != null)
					{
						side = FormationAI.BehaviorSide.Middle;
					}
					else
					{
						siegeWeapon3 = (from dsw in this._destructableSiegeWeapons
						where dsw is IPrimarySiegeWeapon && dsw is IMoveableSiegeWeapon
						select dsw).MinBy((SiegeWeapon dsw) => dsw.GameEntity.GlobalPosition.AsVec2.DistanceSquared(this._cavalryFormation.QuerySystem.AveragePosition));
						side = (siegeWeapon3 as IPrimarySiegeWeapon).WeaponSide;
					}
					behavior3.SetDefensePositionFromTactic(this._teamAISallyOutDefender.CalculateSallyOutReferencePosition(side).ToWorldPosition());
					behavior3.SetDefendedSiegeWeaponFromTactic(siegeWeapon3);
					this._cavalryFormation.AI.Side = side;
				}
			}
			if (this._rangedCavalry != null)
			{
				this._rangedCavalry.AI.ResetBehaviorWeights();
				TacticComponent.SetDefaultBehaviorWeights(this._rangedCavalry);
				this._rangedCavalry.AI.SetBehaviorWeight<BehaviorHorseArcherSkirmish>(1f);
				this._rangedCavalry.AI.SetBehaviorWeight<BehaviorScreenedSkirmish>(0.3f);
			}
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x00045224 File Offset: 0x00043424
		private void DefendSingleMainPosition()
		{
			if (this._mainInfantry != null)
			{
				this._mainInfantry.AI.ResetBehaviorWeights();
				TacticComponent.SetDefaultBehaviorWeights(this._mainInfantry);
				IPrimarySiegeWeapon primarySiegeWeapon = this._destructableSiegeWeapons.FirstOrDefault((SiegeWeapon dsw) => dsw is IPrimarySiegeWeapon && dsw is IMoveableSiegeWeapon) as IPrimarySiegeWeapon;
				if (primarySiegeWeapon != null)
				{
					this._mainInfantry.AI.SetBehaviorWeight<BehaviorDefendSiegeWeapon>(1f);
					BehaviorDefendSiegeWeapon behavior = this._mainInfantry.AI.GetBehavior<BehaviorDefendSiegeWeapon>();
					behavior.SetDefensePositionFromTactic(this._teamAISallyOutDefender.CalculateSallyOutReferencePosition(primarySiegeWeapon.WeaponSide).ToWorldPosition());
					behavior.SetDefendedSiegeWeaponFromTactic(primarySiegeWeapon as SiegeWeapon);
				}
				else if (this._destructableSiegeWeapons.Any((SiegeWeapon dsw) => !dsw.IsDisabled))
				{
					SiegeWeapon siegeWeapon = (from dsw in this._destructableSiegeWeapons
					where !dsw.IsDisabled
					select dsw).MinBy((SiegeWeapon dsw) => dsw.GameEntity.GlobalPosition.AsVec2.DistanceSquared(this._mainInfantry.QuerySystem.AveragePosition));
					this._mainInfantry.AI.ResetBehaviorWeights();
					TacticComponent.SetDefaultBehaviorWeights(this._mainInfantry);
					this._mainInfantry.AI.SetBehaviorWeight<BehaviorDefendSiegeWeapon>(1f);
					BehaviorDefendSiegeWeapon behavior2 = this._mainInfantry.AI.GetBehavior<BehaviorDefendSiegeWeapon>();
					behavior2.SetDefensePositionFromTactic(siegeWeapon.GameEntity.GlobalPosition.ToWorldPosition());
					behavior2.SetDefendedSiegeWeaponFromTactic(siegeWeapon);
				}
				else
				{
					this._mainInfantry.AI.ResetBehaviorWeights();
					TacticComponent.SetDefaultBehaviorWeights(this._mainInfantry);
					this._mainInfantry.AI.SetBehaviorWeight<BehaviorDefend>(1f);
					this._mainInfantry.AI.GetBehavior<BehaviorDefend>().DefensePosition = this._teamAISallyOutDefender.CalculateSallyOutReferencePosition(FormationAI.BehaviorSide.Middle).ToWorldPosition();
				}
				this._mainInfantry.AI.SetBehaviorWeight<BehaviorTacticalCharge>(1f);
			}
			if (this._archers != null)
			{
				this._archers.AI.ResetBehaviorWeights();
				TacticComponent.SetDefaultBehaviorWeights(this._archers);
				this._archers.AI.SetBehaviorWeight<BehaviorSkirmishLine>(1f);
				this._archers.AI.SetBehaviorWeight<BehaviorScreenedSkirmish>(1f);
				this._archers.AI.SetBehaviorWeight<BehaviorSkirmish>(1f);
			}
			if (this._leftCavalry != null)
			{
				this._leftCavalry.AI.ResetBehaviorWeights();
				TacticComponent.SetDefaultBehaviorWeights(this._leftCavalry);
				this._leftCavalry.AI.SetBehaviorWeight<BehaviorProtectFlank>(1f).FlankSide = FormationAI.BehaviorSide.Left;
				if (this._mainInfantry == null)
				{
					IPrimarySiegeWeapon primarySiegeWeapon2 = this._destructableSiegeWeapons.FirstOrDefault((SiegeWeapon dsw) => dsw is IPrimarySiegeWeapon && dsw is IMoveableSiegeWeapon) as IPrimarySiegeWeapon;
					if (primarySiegeWeapon2 != null)
					{
						this._leftCavalry.AI.SetBehaviorWeight<BehaviorDefendSiegeWeapon>(1f);
						BehaviorDefendSiegeWeapon behavior3 = this._leftCavalry.AI.GetBehavior<BehaviorDefendSiegeWeapon>();
						behavior3.SetDefensePositionFromTactic(this._teamAISallyOutDefender.CalculateSallyOutReferencePosition(primarySiegeWeapon2.WeaponSide).ToWorldPosition());
						behavior3.SetDefendedSiegeWeaponFromTactic(primarySiegeWeapon2 as SiegeWeapon);
					}
					else if (this._destructableSiegeWeapons.Any((SiegeWeapon dsw) => !dsw.IsDisabled))
					{
						SiegeWeapon siegeWeapon2 = (from dsw in this._destructableSiegeWeapons
						where !dsw.IsDisabled
						select dsw).MinBy((SiegeWeapon dsw) => dsw.GameEntity.GlobalPosition.AsVec2.DistanceSquared(this._leftCavalry.QuerySystem.AveragePosition));
						this._leftCavalry.AI.ResetBehaviorWeights();
						TacticComponent.SetDefaultBehaviorWeights(this._leftCavalry);
						this._leftCavalry.AI.SetBehaviorWeight<BehaviorDefendSiegeWeapon>(1f);
						BehaviorDefendSiegeWeapon behavior4 = this._leftCavalry.AI.GetBehavior<BehaviorDefendSiegeWeapon>();
						behavior4.SetDefensePositionFromTactic(siegeWeapon2.GameEntity.GlobalPosition.ToWorldPosition());
						behavior4.SetDefendedSiegeWeaponFromTactic(siegeWeapon2);
					}
					else
					{
						this._leftCavalry.AI.ResetBehaviorWeights();
						TacticComponent.SetDefaultBehaviorWeights(this._leftCavalry);
						this._leftCavalry.AI.SetBehaviorWeight<BehaviorDefend>(1f);
						this._leftCavalry.AI.GetBehavior<BehaviorDefend>().DefensePosition = this._teamAISallyOutDefender.CalculateSallyOutReferencePosition(FormationAI.BehaviorSide.Middle).ToWorldPosition();
					}
					this._leftCavalry.AI.SetBehaviorWeight<BehaviorTacticalCharge>(1f);
				}
			}
			if (this._rightCavalry != null)
			{
				this._rightCavalry.AI.ResetBehaviorWeights();
				TacticComponent.SetDefaultBehaviorWeights(this._rightCavalry);
				this._rightCavalry.AI.SetBehaviorWeight<BehaviorProtectFlank>(1f).FlankSide = FormationAI.BehaviorSide.Right;
				if (this._mainInfantry == null)
				{
					IPrimarySiegeWeapon primarySiegeWeapon3 = this._destructableSiegeWeapons.FirstOrDefault((SiegeWeapon dsw) => dsw is IPrimarySiegeWeapon && dsw is IMoveableSiegeWeapon) as IPrimarySiegeWeapon;
					if (primarySiegeWeapon3 != null)
					{
						this._rightCavalry.AI.SetBehaviorWeight<BehaviorDefendSiegeWeapon>(1f);
						BehaviorDefendSiegeWeapon behavior5 = this._rightCavalry.AI.GetBehavior<BehaviorDefendSiegeWeapon>();
						behavior5.SetDefensePositionFromTactic(this._teamAISallyOutDefender.CalculateSallyOutReferencePosition(primarySiegeWeapon3.WeaponSide).ToWorldPosition());
						behavior5.SetDefendedSiegeWeaponFromTactic(primarySiegeWeapon3 as SiegeWeapon);
					}
					else if (this._destructableSiegeWeapons.Any((SiegeWeapon dsw) => !dsw.IsDisabled))
					{
						SiegeWeapon siegeWeapon3 = (from dsw in this._destructableSiegeWeapons
						where !dsw.IsDisabled
						select dsw).MinBy((SiegeWeapon dsw) => dsw.GameEntity.GlobalPosition.AsVec2.DistanceSquared(this._rightCavalry.QuerySystem.AveragePosition));
						this._rightCavalry.AI.ResetBehaviorWeights();
						TacticComponent.SetDefaultBehaviorWeights(this._rightCavalry);
						this._rightCavalry.AI.SetBehaviorWeight<BehaviorDefendSiegeWeapon>(1f);
						BehaviorDefendSiegeWeapon behavior6 = this._rightCavalry.AI.GetBehavior<BehaviorDefendSiegeWeapon>();
						behavior6.SetDefensePositionFromTactic(siegeWeapon3.GameEntity.GlobalPosition.ToWorldPosition());
						behavior6.SetDefendedSiegeWeaponFromTactic(siegeWeapon3);
					}
					else
					{
						this._rightCavalry.AI.ResetBehaviorWeights();
						TacticComponent.SetDefaultBehaviorWeights(this._rightCavalry);
						this._rightCavalry.AI.SetBehaviorWeight<BehaviorDefend>(1f);
						this._rightCavalry.AI.GetBehavior<BehaviorDefend>().DefensePosition = this._teamAISallyOutDefender.CalculateSallyOutReferencePosition(FormationAI.BehaviorSide.Middle).ToWorldPosition();
					}
					this._rightCavalry.AI.SetBehaviorWeight<BehaviorTacticalCharge>(1f);
				}
			}
			if (this._rangedCavalry != null)
			{
				this._rangedCavalry.AI.ResetBehaviorWeights();
				TacticComponent.SetDefaultBehaviorWeights(this._rangedCavalry);
				this._rangedCavalry.AI.SetBehaviorWeight<BehaviorHorseArcherSkirmish>(1f);
				this._rangedCavalry.AI.SetBehaviorWeight<BehaviorScreenedSkirmish>(0.3f);
			}
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x000458E0 File Offset: 0x00043AE0
		private void ApplyDefenseBasedOnState()
		{
			this._destructableSiegeWeapons.RemoveAll((SiegeWeapon dsw) => dsw.IsDisabled || dsw.IsDestroyed);
			TacticSallyOutDefense.WeaponsToBeDefended weaponsToBeDefendedState = this._weaponsToBeDefendedState;
			if (weaponsToBeDefendedState == TacticSallyOutDefense.WeaponsToBeDefended.TwoPrimary)
			{
				this.DefendTwoMainPositions();
				return;
			}
			if (weaponsToBeDefendedState == TacticSallyOutDefense.WeaponsToBeDefended.ThreePrimary)
			{
				this.DefendCenterLocation();
				return;
			}
			this.DefendSingleMainPosition();
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x0004593C File Offset: 0x00043B3C
		protected internal override void TickOccasionally()
		{
			if (!base.AreFormationsCreated)
			{
				return;
			}
			if (this.CheckAndSetAvailableFormationsChanged())
			{
				this.ManageFormationCounts();
				this.ApplyDefenseBasedOnState();
				if (this._hasBattleBeenJoined)
				{
					this.Engage();
				}
				this.IsTacticReapplyNeeded = false;
			}
			TacticSallyOutDefense.WeaponsToBeDefended weaponsToBeDefendedState = this._weaponsToBeDefendedState;
			this.DetermineState();
			if (weaponsToBeDefendedState != this._weaponsToBeDefendedState)
			{
				this.ApplyDefenseBasedOnState();
				this.IsTacticReapplyNeeded = false;
			}
			bool flag = this.CalculateHasBattleBeenJoined();
			if (flag != this._hasBattleBeenJoined || this.IsTacticReapplyNeeded)
			{
				this._hasBattleBeenJoined = flag;
				if (this._hasBattleBeenJoined)
				{
					this.Engage();
				}
				else
				{
					this.ApplyDefenseBasedOnState();
				}
				this.IsTacticReapplyNeeded = false;
			}
			base.TickOccasionally();
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x000459DE File Offset: 0x00043BDE
		protected internal override float GetTacticWeight()
		{
			return 10f;
		}

		// Token: 0x040004DB RID: 1243
		private bool _hasBattleBeenJoined;

		// Token: 0x040004DC RID: 1244
		private WorldPosition SallyOutDefensePosition;

		// Token: 0x040004DD RID: 1245
		private Formation _cavalryFormation;

		// Token: 0x040004DE RID: 1246
		private readonly TeamAISallyOutDefender _teamAISallyOutDefender;

		// Token: 0x040004DF RID: 1247
		private List<SiegeWeapon> _destructableSiegeWeapons;

		// Token: 0x040004E0 RID: 1248
		private TacticSallyOutDefense.WeaponsToBeDefended _weaponsToBeDefendedState;

		// Token: 0x0200048F RID: 1167
		private enum WeaponsToBeDefended
		{
			// Token: 0x04001A13 RID: 6675
			Unset,
			// Token: 0x04001A14 RID: 6676
			NoWeapons,
			// Token: 0x04001A15 RID: 6677
			OnlyRangedWeapons,
			// Token: 0x04001A16 RID: 6678
			OnePrimary,
			// Token: 0x04001A17 RID: 6679
			TwoPrimary,
			// Token: 0x04001A18 RID: 6680
			ThreePrimary
		}
	}
}
