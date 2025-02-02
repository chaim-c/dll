using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.LinQuick;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000265 RID: 613
	public class AssignPlayerRoleInTeamMissionController : MissionLogic
	{
		// Token: 0x1400002D RID: 45
		// (add) Token: 0x0600208A RID: 8330 RVA: 0x00074294 File Offset: 0x00072494
		// (remove) Token: 0x0600208B RID: 8331 RVA: 0x000742CC File Offset: 0x000724CC
		public event PlayerTurnToChooseFormationToLeadEvent OnPlayerTurnToChooseFormationToLead;

		// Token: 0x1400002E RID: 46
		// (add) Token: 0x0600208C RID: 8332 RVA: 0x00074304 File Offset: 0x00072504
		// (remove) Token: 0x0600208D RID: 8333 RVA: 0x0007433C File Offset: 0x0007253C
		public event AllFormationsAssignedSergeantsEvent OnAllFormationsAssignedSergeants;

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x0600208E RID: 8334 RVA: 0x00074371 File Offset: 0x00072571
		public bool IsPlayerInArmy { get; }

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x0600208F RID: 8335 RVA: 0x00074379 File Offset: 0x00072579
		public bool IsPlayerGeneral { get; }

		// Token: 0x06002090 RID: 8336 RVA: 0x00074381 File Offset: 0x00072581
		public AssignPlayerRoleInTeamMissionController(bool isPlayerGeneral, bool isPlayerSergeant, bool isPlayerInArmy, List<string> charactersInPlayerSideByPriority = null, FormationClass preassignedFormationClass = FormationClass.NumberOfRegularFormations)
		{
			this.IsPlayerGeneral = isPlayerGeneral;
			this._isPlayerSergeant = isPlayerSergeant;
			this.IsPlayerInArmy = isPlayerInArmy;
			this._charactersInPlayerSideByPriority = charactersInPlayerSideByPriority;
			this._preassignedFormationClass = preassignedFormationClass;
		}

		// Token: 0x06002091 RID: 8337 RVA: 0x000743C0 File Offset: 0x000725C0
		public override void AfterStart()
		{
			Mission.Current.PlayerTeam.SetPlayerRole(this.IsPlayerGeneral, this._isPlayerSergeant);
		}

		// Token: 0x06002092 RID: 8338 RVA: 0x000743E0 File Offset: 0x000725E0
		private Formation ChooseFormationToLead(IEnumerable<Formation> formationsToChooseFrom, Agent agent)
		{
			bool hasMount = agent.HasMount;
			bool flag = agent.HasRangedWeapon(false);
			List<Formation> list = formationsToChooseFrom.ToList<Formation>();
			while (list.Count > 0)
			{
				Formation formation = list.MaxBy((Formation ftcf) => ftcf.QuerySystem.FormationPower);
				list.Remove(formation);
				if ((flag || (!formation.QuerySystem.IsRangedFormation && !formation.QuerySystem.IsRangedCavalryFormation)) && (hasMount || (!formation.QuerySystem.IsCavalryFormation && !formation.QuerySystem.IsRangedCavalryFormation)))
				{
					return formation;
				}
			}
			return null;
		}

		// Token: 0x06002093 RID: 8339 RVA: 0x00074479 File Offset: 0x00072679
		private void AssignSergeant(Formation formationToLead, Agent sergeant)
		{
			sergeant.Formation = formationToLead;
			if (!sergeant.IsAIControlled || sergeant == Agent.Main)
			{
				formationToLead.PlayerOwner = sergeant;
			}
			formationToLead.Captain = sergeant;
		}

		// Token: 0x06002094 RID: 8340 RVA: 0x000744A0 File Offset: 0x000726A0
		public void OnPlayerChoiceMade(int chosenIndex, bool isFinal)
		{
			if (this._playerChosenIndex != chosenIndex)
			{
				this._playerChosenIndex = chosenIndex;
				this._formationsWithLooselyChosenSergeants.Clear();
				List<Formation> list = base.Mission.PlayerTeam.FormationsIncludingEmpty.WhereQ((Formation f) => f.CountOfUnits > 0 && !this._formationsLockedWithSergeants.ContainsKey(f.Index)).ToList<Formation>();
				if (chosenIndex != -1)
				{
					Formation item = list.FirstOrDefault((Formation fr) => fr.Index == chosenIndex);
					this._formationsWithLooselyChosenSergeants.Add(chosenIndex, base.Mission.PlayerTeam.PlayerOrderController.Owner);
					list.Remove(item);
				}
				Queue<string> queue = new Queue<string>(this._characterNamesInPlayerSideByPriorityQueue);
				while (list.Count > 0 && queue.Count > 0)
				{
					string nextAgentNameToProcess = queue.Dequeue();
					Agent agent = base.Mission.PlayerTeam.ActiveAgents.FirstOrDefault((Agent aa) => aa.Character.StringId.Equals(nextAgentNameToProcess));
					if (agent != null)
					{
						Formation formation = this.ChooseFormationToLead(list, agent);
						if (formation != null)
						{
							this._formationsWithLooselyChosenSergeants.Add(formation.Index, agent);
							list.Remove(formation);
						}
					}
				}
				if (this.OnAllFormationsAssignedSergeants != null)
				{
					this.OnAllFormationsAssignedSergeants(this._formationsWithLooselyChosenSergeants);
					return;
				}
			}
			else if (isFinal)
			{
				foreach (KeyValuePair<int, Agent> keyValuePair in this._formationsLockedWithSergeants)
				{
					this.AssignSergeant(keyValuePair.Value.Team.GetFormation((FormationClass)keyValuePair.Key), keyValuePair.Value);
				}
				foreach (KeyValuePair<int, Agent> keyValuePair2 in this._formationsWithLooselyChosenSergeants)
				{
					this.AssignSergeant(keyValuePair2.Value.Team.GetFormation((FormationClass)keyValuePair2.Key), keyValuePair2.Value);
				}
			}
		}

		// Token: 0x06002095 RID: 8341 RVA: 0x000746D0 File Offset: 0x000728D0
		public void OnPlayerTeamDeployed()
		{
			if (MissionGameModels.Current.BattleInitializationModel.CanPlayerSideDeployWithOrderOfBattle())
			{
				Team playerTeam = Mission.Current.PlayerTeam;
				this._formationsLockedWithSergeants = new Dictionary<int, Agent>();
				this._formationsWithLooselyChosenSergeants = new Dictionary<int, Agent>();
				if (playerTeam.IsPlayerGeneral)
				{
					this._characterNamesInPlayerSideByPriorityQueue = new Queue<string>();
					this._remainingFormationsToAssignSergeantsTo = new List<Formation>();
				}
				else
				{
					this._characterNamesInPlayerSideByPriorityQueue = ((this._charactersInPlayerSideByPriority != null) ? new Queue<string>(this._charactersInPlayerSideByPriority) : new Queue<string>());
					this._remainingFormationsToAssignSergeantsTo = playerTeam.FormationsIncludingSpecialAndEmpty.WhereQ((Formation f) => f.CountOfUnits > 0).ToList<Formation>();
					while (this._remainingFormationsToAssignSergeantsTo.Count > 0 && this._characterNamesInPlayerSideByPriorityQueue.Count > 0)
					{
						string nextAgentNameToProcess = this._characterNamesInPlayerSideByPriorityQueue.Dequeue();
						Agent agent = playerTeam.ActiveAgents.FirstOrDefault((Agent aa) => aa.Character.StringId.Equals(nextAgentNameToProcess));
						if (agent != null)
						{
							if (agent == Agent.Main)
							{
								break;
							}
							Formation formation = this.ChooseFormationToLead(this._remainingFormationsToAssignSergeantsTo, agent);
							if (formation != null)
							{
								this._formationsLockedWithSergeants.Add(formation.Index, agent);
								this._remainingFormationsToAssignSergeantsTo.Remove(formation);
							}
						}
					}
				}
				PlayerTurnToChooseFormationToLeadEvent onPlayerTurnToChooseFormationToLead = this.OnPlayerTurnToChooseFormationToLead;
				if (onPlayerTurnToChooseFormationToLead == null)
				{
					return;
				}
				onPlayerTurnToChooseFormationToLead(this._formationsLockedWithSergeants, (from ftcsf in this._remainingFormationsToAssignSergeantsTo
				select ftcsf.Index).ToList<int>());
			}
		}

		// Token: 0x06002096 RID: 8342 RVA: 0x0007485C File Offset: 0x00072A5C
		public override void OnTeamDeployed(Team team)
		{
			base.OnTeamDeployed(team);
			if (team == base.Mission.PlayerTeam)
			{
				team.PlayerOrderController.Owner = Agent.Main;
				if (team.IsPlayerGeneral)
				{
					foreach (Formation formation in team.FormationsIncludingEmpty)
					{
						formation.PlayerOwner = Agent.Main;
					}
				}
				team.PlayerOrderController.SelectAllFormations(false);
			}
		}

		// Token: 0x06002097 RID: 8343 RVA: 0x000748EC File Offset: 0x00072AEC
		public void OnPlayerChoiceMade(FormationClass chosenFormationClass, FormationAI.BehaviorSide formationBehaviorSide = FormationAI.BehaviorSide.Middle)
		{
			Team playerTeam = base.Mission.PlayerTeam;
			Formation formation = playerTeam.FormationsIncludingEmpty.WhereQ((Formation f) => f.CountOfUnits > 0 && f.PhysicalClass == chosenFormationClass && f.AI.Side == formationBehaviorSide).MaxBy((Formation f) => f.QuerySystem.FormationPower);
			if (playerTeam.IsPlayerSergeant)
			{
				formation.PlayerOwner = Agent.Main;
				formation.SetControlledByAI(false, false);
			}
			if (formation != null && formation != Agent.Main.Formation)
			{
				MBTextManager.SetTextVariable("SIDE_STRING", formation.AI.Side.ToString(), false);
				MBTextManager.SetTextVariable("CLASS_NAME", formation.PhysicalClass.GetName(), false);
				MBInformationManager.AddQuickInformation(GameTexts.FindText("str_formation_soldier_join_text", null), 0, null, "");
			}
			Agent.Main.Formation = formation;
			playerTeam.TriggerOnFormationsChanged(formation);
		}

		// Token: 0x04000C05 RID: 3077
		private bool _isPlayerSergeant;

		// Token: 0x04000C06 RID: 3078
		private FormationClass _preassignedFormationClass;

		// Token: 0x04000C07 RID: 3079
		private List<string> _charactersInPlayerSideByPriority = new List<string>();

		// Token: 0x04000C08 RID: 3080
		private Queue<string> _characterNamesInPlayerSideByPriorityQueue;

		// Token: 0x04000C09 RID: 3081
		private List<Formation> _remainingFormationsToAssignSergeantsTo;

		// Token: 0x04000C0A RID: 3082
		private Dictionary<int, Agent> _formationsLockedWithSergeants;

		// Token: 0x04000C0B RID: 3083
		private Dictionary<int, Agent> _formationsWithLooselyChosenSergeants;

		// Token: 0x04000C0C RID: 3084
		private int _playerChosenIndex = -1;
	}
}
