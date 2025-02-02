using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000171 RID: 369
	public class TacticSallyOutHitAndRun : TacticComponent
	{
		// Token: 0x060012A1 RID: 4769 RVA: 0x00045A90 File Offset: 0x00043C90
		protected override void ManageFormationCounts()
		{
			List<IPrimarySiegeWeapon> list = this._teamAISallyOutAttacker.PrimarySiegeWeapons.Where(delegate(IPrimarySiegeWeapon psw)
			{
				SiegeWeapon siegeWeapon;
				return (siegeWeapon = (psw as SiegeWeapon)) != null && !siegeWeapon.IsDisabled && siegeWeapon.IsDestructible;
			}).ToList<IPrimarySiegeWeapon>();
			int num = list.Count;
			bool flag = false;
			foreach (UsableMachine usableMachine in this._teamAISallyOutAttacker.BesiegerRangedSiegeWeapons)
			{
				if (!usableMachine.IsDisabled && !usableMachine.IsDestroyed)
				{
					flag = true;
					break;
				}
			}
			num = MathF.Max(num, 1 + ((list.Count > 0 && flag) ? 1 : 0));
			int count = MathF.Min(this._teamAISallyOutAttacker.ArcherPositions.Count, 7 - num);
			object obj = base.FormationsIncludingEmpty.Count((Formation f) => f.CountOfUnits > 0 && (f.QuerySystem.IsCavalryFormation || f.QuerySystem.IsRangedCavalryFormation)) > 0 && base.Team.QuerySystem.CavalryRatio + base.Team.QuerySystem.RangedCavalryRatio > 0.1f;
			bool flag2 = true;
			object obj2 = obj;
			if (obj2 == null)
			{
				num = 1;
				flag2 = (base.FormationsIncludingEmpty.Count((Formation f) => f.CountOfUnits > 0 && f.QuerySystem.IsInfantryFormation) > 0 && base.Team.QuerySystem.CavalryRatio + base.Team.QuerySystem.RangedCavalryRatio + base.Team.QuerySystem.InfantryRatio > 0.15f);
				if (!flag2)
				{
					count = 1;
				}
			}
			base.SplitFormationClassIntoGivenNumber((Formation f) => f.QuerySystem.IsInfantryFormation, 1);
			base.SplitFormationClassIntoGivenNumber((Formation f) => f.QuerySystem.IsRangedFormation, count);
			base.SplitFormationClassIntoGivenNumber((Formation f) => f.QuerySystem.IsCavalryFormation || f.QuerySystem.IsRangedCavalryFormation, num);
			this._mainInfantryFormation = base.FormationsIncludingEmpty.FirstOrDefault((Formation f) => f.CountOfUnits > 0 && f.QuerySystem.IsInfantryFormation);
			if (this._mainInfantryFormation != null)
			{
				this._mainInfantryFormation.AI.IsMainFormation = true;
			}
			this._archerFormations = (from f in base.FormationsIncludingEmpty
			where f.CountOfUnits > 0 && f.QuerySystem.IsRangedFormation
			select f).ToMBList<Formation>();
			this._cavalryFormations.Clear();
			this._cavalryFormations = (from f in base.FormationsIncludingEmpty
			where f.CountOfUnits > 0 && (f.QuerySystem.IsCavalryFormation || f.QuerySystem.IsRangedCavalryFormation)
			select f).ToMBList<Formation>();
			if (obj2 == null)
			{
				if (this._mainInfantryFormation != null)
				{
					this._cavalryFormations.Add(this._mainInfantryFormation);
					this._mainInfantryFormation.AI.IsMainFormation = false;
					this._mainInfantryFormation = null;
				}
				if (!flag2)
				{
					this._cavalryFormations.AddRange(this._archerFormations);
					this._archerFormations.Clear();
				}
			}
			bool flag3 = list.Count == 0 || (list.Count == 1 && flag && this._cavalryFormations.Count + ((this._mainInfantryFormation != null) ? 1 : 0) > 1);
			for (int i = 0; i < this._cavalryFormations.Count - (flag3 ? 1 : 0); i++)
			{
				if (list.Count > 0)
				{
					this._cavalryFormations[i].AI.Side = list[i % list.Count].WeaponSide;
				}
				else
				{
					this._cavalryFormations[i].AI.Side = FormationAI.BehaviorSide.Middle;
				}
			}
			for (int j = 0; j < this._archerFormations.Count - (flag3 ? 1 : 0); j++)
			{
				if (list.Count > 0)
				{
					this._archerFormations[j].AI.Side = list[j % list.Count].WeaponSide;
				}
				else
				{
					this._archerFormations[j].AI.Side = FormationAI.BehaviorSide.Middle;
				}
			}
			if (this._cavalryFormations.Count > 0 && flag3)
			{
				if (list.Any((IPrimarySiegeWeapon psw) => psw != null && psw.WeaponSide == FormationAI.BehaviorSide.Middle))
				{
					this._cavalryFormations[0].AI.Side = FormationAI.BehaviorSide.Left;
				}
				else
				{
					this._cavalryFormations[this._cavalryFormations.Count - 1].AI.Side = FormationAI.BehaviorSide.Middle;
				}
			}
			if (this._archerFormations.Count > 0 && flag3)
			{
				if (list.Any((IPrimarySiegeWeapon psw) => psw != null && psw.WeaponSide == FormationAI.BehaviorSide.Middle))
				{
					this._archerFormations[0].AI.Side = FormationAI.BehaviorSide.Left;
				}
				else
				{
					this._archerFormations[this._archerFormations.Count - 1].AI.Side = FormationAI.BehaviorSide.Middle;
				}
			}
			this._AIControlledFormationCount = base.Team.GetAIControlledFormationCount();
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x00045FD8 File Offset: 0x000441D8
		private void DestroySiegeWeapons()
		{
			if (this._mainInfantryFormation != null)
			{
				this._mainInfantryFormation.AI.ResetBehaviorWeights();
				TacticComponent.SetDefaultBehaviorWeights(this._mainInfantryFormation);
				BehaviorDefend behaviorDefend = this._mainInfantryFormation.AI.SetBehaviorWeight<BehaviorDefend>(1f);
				Vec2 v = (this._teamAISallyOutAttacker.OuterGate.GameEntity.GlobalPosition.AsVec2 - this._teamAISallyOutAttacker.InnerGate.GameEntity.GlobalPosition.AsVec2).Normalized();
				WorldPosition defensePosition = new WorldPosition(this._mainInfantryFormation.Team.Mission.Scene, UIntPtr.Zero, this._teamAISallyOutAttacker.OuterGate.GameEntity.GlobalPosition, false);
				defensePosition.SetVec2(defensePosition.AsVec2 + (3f + this._mainInfantryFormation.Depth) * v);
				behaviorDefend.DefensePosition = defensePosition;
				this._mainInfantryFormation.AI.SetBehaviorWeight<BehaviorDestroySiegeWeapons>(1f);
				this._mainInfantryFormation.AI.SetBehaviorWeight<BehaviorCharge>(0.1f);
			}
			if (this._teamAISallyOutAttacker.ArcherPositions.Count > 0)
			{
				for (int i = 0; i < this._archerFormations.Count; i++)
				{
					Formation formation = this._archerFormations[i];
					formation.AI.ResetBehaviorWeights();
					TacticComponent.SetDefaultBehaviorWeights(formation);
					formation.AI.SetBehaviorWeight<BehaviorShootFromCastleWalls>(0.1f);
					formation.AI.GetBehavior<BehaviorShootFromCastleWalls>().ArcherPosition = this._teamAISallyOutAttacker.ArcherPositions[i % this._teamAISallyOutAttacker.ArcherPositions.Count];
					formation.AI.SetBehaviorWeight<BehaviorDestroySiegeWeapons>(1f);
					formation.AI.SetBehaviorWeight<BehaviorCharge>(0.1f);
				}
			}
			foreach (Formation formation2 in this._cavalryFormations)
			{
				formation2.AI.ResetBehaviorWeights();
				TacticComponent.SetDefaultBehaviorWeights(formation2);
				formation2.AI.SetBehaviorWeight<BehaviorDestroySiegeWeapons>(1f);
				formation2.AI.SetBehaviorWeight<BehaviorCharge>(0.1f);
			}
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x00046228 File Offset: 0x00044428
		private void CavalryRetreat()
		{
			if (this._mainInfantryFormation != null)
			{
				this._mainInfantryFormation.AI.ResetBehaviorWeights();
				TacticComponent.SetDefaultBehaviorWeights(this._mainInfantryFormation);
				BehaviorDefend behaviorDefend = this._mainInfantryFormation.AI.SetBehaviorWeight<BehaviorDefend>(1f);
				Vec2 v = (this._teamAISallyOutAttacker.OuterGate.GameEntity.GlobalPosition.AsVec2 - this._teamAISallyOutAttacker.InnerGate.GameEntity.GlobalPosition.AsVec2).Normalized();
				WorldPosition defensePosition = new WorldPosition(this._mainInfantryFormation.Team.Mission.Scene, UIntPtr.Zero, this._teamAISallyOutAttacker.OuterGate.GameEntity.GlobalPosition, false);
				defensePosition.SetVec2(defensePosition.AsVec2 + (3f + this._mainInfantryFormation.Depth) * v);
				behaviorDefend.DefensePosition = defensePosition;
			}
			if (this._teamAISallyOutAttacker.ArcherPositions.Count > 0)
			{
				for (int i = 0; i < this._archerFormations.Count; i++)
				{
					Formation formation = this._archerFormations[i];
					formation.AI.ResetBehaviorWeights();
					TacticComponent.SetDefaultBehaviorWeights(formation);
					formation.AI.SetBehaviorWeight<BehaviorShootFromCastleWalls>(1f);
					formation.AI.GetBehavior<BehaviorShootFromCastleWalls>().ArcherPosition = this._teamAISallyOutAttacker.ArcherPositions[i % this._teamAISallyOutAttacker.ArcherPositions.Count];
				}
			}
			foreach (Formation formation2 in this._cavalryFormations)
			{
				formation2.AI.ResetBehaviorWeights();
				TacticComponent.SetDefaultBehaviorWeights(formation2);
				formation2.AI.SetBehaviorWeight<BehaviorRetreatToCastle>(3f);
				formation2.AI.SetBehaviorWeight<BehaviorCharge>(0.1f);
			}
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x00046420 File Offset: 0x00044620
		private void InfantryRetreat()
		{
			if (this._mainInfantryFormation != null)
			{
				for (int i = 0; i < TeamAISiegeComponent.SiegeLanes.Count; i++)
				{
					SiegeLane siegeLane = TeamAISiegeComponent.SiegeLanes[i];
					if (siegeLane.HasGate)
					{
						this._mainInfantryFormation.AI.Side = siegeLane.LaneSide;
						break;
					}
				}
				this._mainInfantryFormation.AI.ResetBehaviorWeights();
				TacticComponent.SetDefaultBehaviorWeights(this._mainInfantryFormation);
				this._mainInfantryFormation.AI.SetBehaviorWeight<BehaviorDefendCastleKeyPosition>(1f);
			}
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x000464A8 File Offset: 0x000446A8
		private void HeadOutFromTheCastle()
		{
			if (this._mainInfantryFormation != null)
			{
				this._mainInfantryFormation.AI.ResetBehaviorWeights();
				TacticComponent.SetDefaultBehaviorWeights(this._mainInfantryFormation);
				this._mainInfantryFormation.AI.SetBehaviorWeight<BehaviorStop>(1000f);
			}
			if (this._teamAISallyOutAttacker.ArcherPositions.Count > 0)
			{
				for (int i = 0; i < this._archerFormations.Count; i++)
				{
					Formation formation = this._archerFormations[i];
					formation.AI.ResetBehaviorWeights();
					TacticComponent.SetDefaultBehaviorWeights(formation);
					formation.AI.SetBehaviorWeight<BehaviorShootFromCastleWalls>(1f);
					formation.AI.GetBehavior<BehaviorShootFromCastleWalls>().ArcherPosition = this._teamAISallyOutAttacker.ArcherPositions[i % this._teamAISallyOutAttacker.ArcherPositions.Count];
				}
			}
			foreach (Formation formation2 in this._cavalryFormations)
			{
				formation2.AI.ResetBehaviorWeights();
				TacticComponent.SetDefaultBehaviorWeights(formation2);
				formation2.AI.SetBehaviorWeight<BehaviorDestroySiegeWeapons>(1f);
				formation2.AI.SetBehaviorWeight<BehaviorCharge>(0.1f);
			}
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x000465E8 File Offset: 0x000447E8
		public TacticSallyOutHitAndRun(Team team) : base(team)
		{
			this._archerFormations = new MBList<Formation>();
			this._cavalryFormations = new MBList<Formation>();
			this._teamAISallyOutAttacker = (team.TeamAI as TeamAISallyOutAttacker);
			this._state = TacticSallyOutHitAndRun.TacticState.HeadingOutFromCastle;
			this._destructibleEnemySiegeWeapons = (from sw in Mission.Current.ActiveMissionObjects.FindAllWithType<SiegeWeapon>()
			where sw.Side != team.Side && sw.IsDestructible
			select sw).ToList<SiegeWeapon>();
			this.ManageFormationCounts();
			this.HeadOutFromTheCastle();
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x00046678 File Offset: 0x00044878
		protected override bool CheckAndSetAvailableFormationsChanged()
		{
			int aicontrolledFormationCount = base.Team.GetAIControlledFormationCount();
			bool flag = aicontrolledFormationCount != this._AIControlledFormationCount;
			if (flag)
			{
				this._AIControlledFormationCount = aicontrolledFormationCount;
				this.IsTacticReapplyNeeded = true;
			}
			if (!flag && (this._mainInfantryFormation == null || (this._mainInfantryFormation.CountOfUnits != 0 && this._mainInfantryFormation.QuerySystem.IsInfantryFormation)))
			{
				if (this._archerFormations.Count > 0)
				{
					if (this._archerFormations.Any((Formation af) => af.CountOfUnits == 0 || !af.QuerySystem.IsRangedFormation))
					{
						return true;
					}
				}
				if (this._cavalryFormations.Count > 0)
				{
					return this._cavalryFormations.Any((Formation cf) => cf.CountOfUnits == 0);
				}
				return false;
			}
			return true;
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x00046754 File Offset: 0x00044954
		private void CheckAndChangeState()
		{
			switch (this._state)
			{
			case TacticSallyOutHitAndRun.TacticState.HeadingOutFromCastle:
				if (this._cavalryFormations.All((Formation cf) => !TeamAISiegeComponent.IsFormationInsideCastle(cf, false, 0.4f)))
				{
					this._state = TacticSallyOutHitAndRun.TacticState.DestroyingSiegeWeapons;
					this.DestroySiegeWeapons();
					return;
				}
				break;
			case TacticSallyOutHitAndRun.TacticState.DestroyingSiegeWeapons:
			{
				bool flag = true;
				using (List<SiegeWeapon>.Enumerator enumerator = this._destructibleEnemySiegeWeapons.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (!enumerator.Current.IsDestroyed)
						{
							flag = false;
							break;
						}
					}
				}
				bool flag2 = true;
				if (!flag)
				{
					foreach (Formation formation in this._cavalryFormations)
					{
						if (formation.AI.ActiveBehavior == null)
						{
							flag2 = false;
							break;
						}
						BehaviorDestroySiegeWeapons behaviorDestroySiegeWeapons;
						if (!(formation.GetReadonlyMovementOrderReference() == MovementOrder.MovementOrderRetreat) && (behaviorDestroySiegeWeapons = (formation.AI.ActiveBehavior as BehaviorDestroySiegeWeapons)) != null)
						{
							if (behaviorDestroySiegeWeapons.LastTargetWeapon == null)
							{
								flag2 = false;
								break;
							}
							Vec2 asVec = behaviorDestroySiegeWeapons.LastTargetWeapon.GameEntity.GlobalPosition.AsVec2;
							if (base.Team.QuerySystem.GetLocalEnemyPower(asVec) <= base.Team.QuerySystem.GetLocalAllyPower(asVec) + formation.QuerySystem.FormationPower)
							{
								flag2 = false;
								break;
							}
						}
					}
				}
				if (flag || flag2)
				{
					this._state = TacticSallyOutHitAndRun.TacticState.CavalryRetreating;
					this.CavalryRetreat();
					TeamAIComponent teamAI = base.Team.TeamAI;
					TacticalDecision tacticalDecision = new TacticalDecision(this, 31, null, null, null, null);
					teamAI.NotifyTacticalDecision(tacticalDecision);
					return;
				}
				break;
			}
			case TacticSallyOutHitAndRun.TacticState.CavalryRetreating:
				if (this._cavalryFormations.IsEmpty<Formation>() || TeamAISiegeComponent.IsFormationGroupInsideCastle(this._cavalryFormations, false, 0.4f))
				{
					this._state = TacticSallyOutHitAndRun.TacticState.InfantryRetreating;
					this.InfantryRetreat();
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x00046958 File Offset: 0x00044B58
		protected internal override void TickOccasionally()
		{
			if (!base.AreFormationsCreated)
			{
				return;
			}
			if (this.CheckAndSetAvailableFormationsChanged() || this.IsTacticReapplyNeeded)
			{
				this.ManageFormationCounts();
				switch (this._state)
				{
				case TacticSallyOutHitAndRun.TacticState.HeadingOutFromCastle:
					this.HeadOutFromTheCastle();
					break;
				case TacticSallyOutHitAndRun.TacticState.DestroyingSiegeWeapons:
					this.DestroySiegeWeapons();
					break;
				case TacticSallyOutHitAndRun.TacticState.CavalryRetreating:
					this.CavalryRetreat();
					break;
				case TacticSallyOutHitAndRun.TacticState.InfantryRetreating:
					this.InfantryRetreat();
					break;
				}
				this.IsTacticReapplyNeeded = false;
			}
			this.CheckAndChangeState();
			base.TickOccasionally();
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x000469D4 File Offset: 0x00044BD4
		protected internal override float GetTacticWeight()
		{
			return 10f;
		}

		// Token: 0x040004E1 RID: 1249
		private TacticSallyOutHitAndRun.TacticState _state;

		// Token: 0x040004E2 RID: 1250
		private Formation _mainInfantryFormation;

		// Token: 0x040004E3 RID: 1251
		private MBList<Formation> _archerFormations;

		// Token: 0x040004E4 RID: 1252
		private MBList<Formation> _cavalryFormations;

		// Token: 0x040004E5 RID: 1253
		private readonly TeamAISallyOutAttacker _teamAISallyOutAttacker;

		// Token: 0x040004E6 RID: 1254
		private readonly List<SiegeWeapon> _destructibleEnemySiegeWeapons;

		// Token: 0x02000493 RID: 1171
		private enum TacticState
		{
			// Token: 0x04001A3D RID: 6717
			HeadingOutFromCastle,
			// Token: 0x04001A3E RID: 6718
			DestroyingSiegeWeapons,
			// Token: 0x04001A3F RID: 6719
			CavalryRetreating,
			// Token: 0x04001A40 RID: 6720
			InfantryRetreating
		}
	}
}
