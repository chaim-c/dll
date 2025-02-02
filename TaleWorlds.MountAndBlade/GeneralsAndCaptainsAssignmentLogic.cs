using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.LinQuick;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.ComponentInterfaces;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000275 RID: 629
	public class GeneralsAndCaptainsAssignmentLogic : MissionLogic
	{
		// Token: 0x0600211A RID: 8474 RVA: 0x0007734D File Offset: 0x0007554D
		public GeneralsAndCaptainsAssignmentLogic(TextObject attackerGeneralName, TextObject defenderGeneralName, TextObject attackerAllyGeneralName = null, TextObject defenderAllyGeneralName = null, bool createBodyguard = true)
		{
			this._attackerGeneralName = attackerGeneralName;
			this._defenderGeneralName = defenderGeneralName;
			this._attackerAllyGeneralName = attackerAllyGeneralName;
			this._defenderAllyGeneralName = defenderAllyGeneralName;
			this._createBodyguard = createBodyguard;
			this._isPlayerTeamGeneralFormationSet = false;
		}

		// Token: 0x0600211B RID: 8475 RVA: 0x00077388 File Offset: 0x00075588
		public override void AfterStart()
		{
			this._bannerLogic = base.Mission.GetMissionBehavior<BannerBearerLogic>();
		}

		// Token: 0x0600211C RID: 8476 RVA: 0x0007739C File Offset: 0x0007559C
		public override void OnTeamDeployed(Team team)
		{
			this.SetGeneralAgentOfTeam(team);
			if (team.IsPlayerTeam)
			{
				if (!MissionGameModels.Current.BattleInitializationModel.CanPlayerSideDeployWithOrderOfBattle())
				{
					if (this.CanTeamHaveGeneralsFormation(team))
					{
						this.CreateGeneralFormationForTeam(team);
						this._isPlayerTeamGeneralFormationSet = true;
					}
					this.AssignBestCaptainsForTeam(team);
					return;
				}
			}
			else
			{
				if (this.CanTeamHaveGeneralsFormation(team))
				{
					this.CreateGeneralFormationForTeam(team);
				}
				this.AssignBestCaptainsForTeam(team);
			}
		}

		// Token: 0x0600211D RID: 8477 RVA: 0x00077404 File Offset: 0x00075604
		public override void OnDeploymentFinished()
		{
			Team playerTeam = base.Mission.PlayerTeam;
			if (!this._isPlayerTeamGeneralFormationSet && this.CanTeamHaveGeneralsFormation(playerTeam))
			{
				this.CreateGeneralFormationForTeam(playerTeam);
				this._isPlayerTeamGeneralFormationSet = true;
			}
			Agent mainAgent;
			if (this._isPlayerTeamGeneralFormationSet && (mainAgent = base.Mission.MainAgent) != null && playerTeam.GeneralAgent != mainAgent)
			{
				mainAgent.SetCanLeadFormationsRemotely(true);
				Formation formation = playerTeam.GetFormation(FormationClass.NumberOfRegularFormations);
				mainAgent.Formation = formation;
				mainAgent.Team.TriggerOnFormationsChanged(formation);
				formation.QuerySystem.Expire();
			}
		}

		// Token: 0x0600211E RID: 8478 RVA: 0x0007748C File Offset: 0x0007568C
		protected virtual void SortCaptainsByPriority(Team team, ref List<Agent> captains)
		{
			captains = captains.OrderByDescending(delegate(Agent captain)
			{
				if (team.GeneralAgent != captain)
				{
					return captain.Character.GetPower();
				}
				return float.MaxValue;
			}).ToList<Agent>();
		}

		// Token: 0x0600211F RID: 8479 RVA: 0x000774C0 File Offset: 0x000756C0
		protected virtual Formation PickBestRegularFormationToLead(Agent agent, List<Formation> candidateFormations)
		{
			Formation result = null;
			int num = 0;
			foreach (Formation formation in candidateFormations)
			{
				if (!(agent.HasMount ^ formation.CalculateHasSignificantNumberOfMounted))
				{
					int countOfUnits = formation.CountOfUnits;
					if (countOfUnits > num)
					{
						num = countOfUnits;
						result = formation;
					}
				}
			}
			return result;
		}

		// Token: 0x06002120 RID: 8480 RVA: 0x00077530 File Offset: 0x00075730
		private bool CanTeamHaveGeneralsFormation(Team team)
		{
			Agent generalAgent = team.GeneralAgent;
			return generalAgent != null && (generalAgent == base.Mission.MainAgent || team.QuerySystem.MemberCount >= 50);
		}

		// Token: 0x06002121 RID: 8481 RVA: 0x0007756C File Offset: 0x0007576C
		private void AssignBestCaptainsForTeam(Team team)
		{
			List<Agent> list = (from agent in team.ActiveAgents
			where agent.IsHero
			select agent).ToList<Agent>();
			this.SortCaptainsByPriority(team, ref list);
			int numRegularFormations = 8;
			List<Formation> list2 = team.FormationsIncludingEmpty.WhereQ((Formation f) => f.CountOfUnits > 0 && f.FormationIndex < (FormationClass)numRegularFormations).ToList<Formation>();
			List<Agent> list3 = new List<Agent>();
			foreach (Agent agent2 in list)
			{
				Formation formation = null;
				BattleBannerBearersModel battleBannerBearersModel = MissionGameModels.Current.BattleBannerBearersModel;
				if (agent2 == team.GeneralAgent && team.BodyGuardFormation != null && team.BodyGuardFormation.CountOfUnits > 0)
				{
					formation = team.BodyGuardFormation;
				}
				if (formation == null)
				{
					formation = this.PickBestRegularFormationToLead(agent2, list2);
					if (formation != null)
					{
						list2.Remove(formation);
					}
				}
				if (formation != null)
				{
					list3.Add(agent2);
					this.OnCaptainAssignedToFormation(agent2, formation);
				}
			}
			foreach (Agent item in list3)
			{
				list.Remove(item);
			}
			using (List<Agent>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Agent candidate = enumerator.Current;
					if (list2.IsEmpty<Formation>())
					{
						break;
					}
					Formation formation2 = list2.FirstOrDefault((Formation f) => f.CalculateHasSignificantNumberOfMounted == candidate.HasMount);
					if (formation2 != null)
					{
						this.OnCaptainAssignedToFormation(candidate, formation2);
						list2.Remove(formation2);
					}
				}
			}
		}

		// Token: 0x06002122 RID: 8482 RVA: 0x00077748 File Offset: 0x00075948
		private void SetGeneralAgentOfTeam(Team team)
		{
			Agent agent = null;
			if (team.IsPlayerTeam && team.IsPlayerGeneral)
			{
				agent = base.Mission.MainAgent;
			}
			else
			{
				List<IFormationUnit> source = team.FormationsIncludingEmpty.SelectMany((Formation f) => f.UnitsWithoutLooseDetachedOnes).ToList<IFormationUnit>();
				TextObject generalName = (team == base.Mission.AttackerTeam) ? this._attackerGeneralName : ((team == base.Mission.DefenderTeam) ? this._defenderGeneralName : ((team == base.Mission.AttackerAllyTeam) ? this._attackerAllyGeneralName : ((team == base.Mission.DefenderAllyTeam) ? this._defenderAllyGeneralName : null)));
				if (generalName != null && source.Count((IFormationUnit ta) => ((Agent)ta).Character != null && ((Agent)ta).Character.GetName().Equals(generalName)) == 1)
				{
					agent = (Agent)source.First((IFormationUnit ta) => ((Agent)ta).Character != null && ((Agent)ta).Character.GetName().Equals(generalName));
				}
				else if (source.Any((IFormationUnit u) => !((Agent)u).IsMainAgent && ((Agent)u).IsHero))
				{
					agent = (Agent)(from u in source
					where !((Agent)u).IsMainAgent && ((Agent)u).IsHero
					select u).MaxBy((IFormationUnit u) => ((Agent)u).CharacterPowerCached);
				}
			}
			if (agent != null)
			{
				agent.SetCanLeadFormationsRemotely(true);
			}
			team.GeneralAgent = agent;
		}

		// Token: 0x06002123 RID: 8483 RVA: 0x000778CC File Offset: 0x00075ACC
		private void CreateGeneralFormationForTeam(Team team)
		{
			Agent generalAgent = team.GeneralAgent;
			Formation formation = team.GetFormation(FormationClass.NumberOfRegularFormations);
			base.Mission.SetFormationPositioningFromDeploymentPlan(formation);
			WorldPosition position = formation.CreateNewOrderWorldPosition(WorldPosition.WorldPositionEnforcedCache.GroundVec3);
			formation.SetMovementOrder(MovementOrder.MovementOrderMove(position));
			formation.SetControlledByAI(true, false);
			team.GeneralsFormation = formation;
			generalAgent.Formation = formation;
			generalAgent.Team.TriggerOnFormationsChanged(formation);
			formation.QuerySystem.Expire();
			TacticComponent.SetDefaultBehaviorWeights(formation);
			formation.AI.SetBehaviorWeight<BehaviorGeneral>(1f);
			formation.PlayerOwner = null;
			if (this._createBodyguard && generalAgent != base.Mission.MainAgent)
			{
				List<IFormationUnit> list = team.FormationsIncludingEmpty.SelectMany((Formation f) => f.UnitsWithoutLooseDetachedOnes).ToList<IFormationUnit>();
				list.Remove(generalAgent);
				List<IFormationUnit> list2 = list.Where(delegate(IFormationUnit u)
				{
					Agent agent;
					if ((agent = (u as Agent)) == null || (agent.Character != null && agent.Character.IsHero) || agent.Banner != null)
					{
						return false;
					}
					if (generalAgent.MountAgent == null)
					{
						return !agent.HasMount;
					}
					return agent.HasMount;
				}).ToList<IFormationUnit>();
				int num = MathF.Min((int)((float)list2.Count / 10f), 20);
				if (num != 0)
				{
					Formation formation2 = team.GetFormation(FormationClass.Bodyguard);
					formation2.SetMovementOrder(MovementOrder.MovementOrderMove(position));
					formation2.SetControlledByAI(true, false);
					List<IFormationUnit> list3 = (from u in list2
					orderby ((Agent)u).CharacterPowerCached descending
					select u).Take(num).ToList<IFormationUnit>();
					IEnumerable<Formation> enumerable = (from bu in list3
					select ((Agent)bu).Formation).Distinct<Formation>();
					foreach (IFormationUnit formationUnit in list3)
					{
						((Agent)formationUnit).Formation = formation2;
					}
					foreach (Formation formation3 in enumerable)
					{
						team.TriggerOnFormationsChanged(formation3);
						formation3.QuerySystem.Expire();
					}
					TacticComponent.SetDefaultBehaviorWeights(formation2);
					formation2.AI.SetBehaviorWeight<BehaviorProtectGeneral>(1f);
					formation2.PlayerOwner = null;
					formation2.QuerySystem.Expire();
					team.BodyGuardFormation = formation2;
					team.TriggerOnFormationsChanged(formation2);
				}
			}
		}

		// Token: 0x06002124 RID: 8484 RVA: 0x00077B44 File Offset: 0x00075D44
		private void OnCaptainAssignedToFormation(Agent captain, Formation formation)
		{
			if (captain.Formation != formation && captain != formation.Team.GeneralAgent)
			{
				captain.Formation = formation;
				formation.Team.TriggerOnFormationsChanged(formation);
				formation.QuerySystem.Expire();
			}
			formation.Captain = captain;
			if (this._bannerLogic != null && captain.FormationBanner != null)
			{
				this._bannerLogic.SetFormationBanner(formation, captain.FormationBanner);
			}
		}

		// Token: 0x04000C42 RID: 3138
		public int MinimumAgentCountToLeadGeneralFormation = 3;

		// Token: 0x04000C43 RID: 3139
		private BannerBearerLogic _bannerLogic;

		// Token: 0x04000C44 RID: 3140
		private readonly TextObject _attackerGeneralName;

		// Token: 0x04000C45 RID: 3141
		private readonly TextObject _defenderGeneralName;

		// Token: 0x04000C46 RID: 3142
		private readonly TextObject _attackerAllyGeneralName;

		// Token: 0x04000C47 RID: 3143
		private readonly TextObject _defenderAllyGeneralName;

		// Token: 0x04000C48 RID: 3144
		private readonly bool _createBodyguard;

		// Token: 0x04000C49 RID: 3145
		private bool _isPlayerTeamGeneralFormationSet;
	}
}
