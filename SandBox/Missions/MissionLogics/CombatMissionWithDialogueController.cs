using System;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.AI.AgentComponents;

namespace SandBox.Missions.MissionLogics
{
	// Token: 0x0200004C RID: 76
	public class CombatMissionWithDialogueController : MissionLogic, IMissionAgentSpawnLogic, IMissionBehavior
	{
		// Token: 0x060002EE RID: 750 RVA: 0x0001278A File Offset: 0x0001098A
		public CombatMissionWithDialogueController(IMissionTroopSupplier[] suppliers, BasicCharacterObject characterToTalkTo)
		{
			this._troopSuppliers = suppliers;
			this._characterToTalkTo = characterToTalkTo;
		}

		// Token: 0x060002EF RID: 751 RVA: 0x000127A0 File Offset: 0x000109A0
		public override void OnCreated()
		{
			base.OnCreated();
			base.Mission.DoesMissionRequireCivilianEquipment = true;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x000127B4 File Offset: 0x000109B4
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			this._battleAgentLogic = Mission.Current.GetMissionBehavior<BattleAgentLogic>();
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x000127CC File Offset: 0x000109CC
		public override void AfterStart()
		{
			base.AfterStart();
			base.Mission.MakeDefaultDeploymentPlans();
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x000127E0 File Offset: 0x000109E0
		public override void OnMissionTick(float dt)
		{
			if (!this._isMissionInitialized)
			{
				this.SpawnAgents();
				this._isMissionInitialized = true;
				return;
			}
			if (!this._troopsInitialized)
			{
				this._troopsInitialized = true;
				foreach (Agent agent in base.Mission.Agents)
				{
					this._battleAgentLogic.OnAgentBuild(agent, null);
				}
			}
			if (!this._conversationInitialized && Agent.Main != null && Agent.Main.IsActive())
			{
				foreach (Agent agent2 in base.Mission.Agents)
				{
					ScriptedMovementComponent component = agent2.GetComponent<ScriptedMovementComponent>();
					if (component != null && component.ShouldConversationStartWithAgent())
					{
						this.StartConversation(agent2, true);
						this._conversationInitialized = true;
					}
				}
			}
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x000128E0 File Offset: 0x00010AE0
		public override void OnAgentHit(Agent affectedAgent, Agent affectorAgent, in MissionWeapon affectorWeapon, in Blow blow, in AttackCollisionData attackCollisionData)
		{
			if (!this._conversationInitialized && affectedAgent.Team != Mission.Current.PlayerTeam && affectorAgent != null && affectorAgent == Agent.Main)
			{
				this._conversationInitialized = true;
				this.StartFight(false);
			}
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00012918 File Offset: 0x00010B18
		public void StartFight(bool hasPlayerChangedSide)
		{
			base.Mission.SetMissionMode(MissionMode.Battle, false);
			if (hasPlayerChangedSide)
			{
				Agent.Main.SetTeam((Agent.Main.Team == base.Mission.AttackerTeam) ? base.Mission.DefenderTeam : base.Mission.AttackerTeam, true);
				Mission.Current.PlayerTeam = Agent.Main.Team;
			}
			foreach (Agent agent in base.Mission.Agents)
			{
				if (Agent.Main != agent)
				{
					if (hasPlayerChangedSide && agent.Team != Mission.Current.PlayerTeam && agent.Origin.BattleCombatant as PartyBase == PartyBase.MainParty)
					{
						agent.SetTeam(Mission.Current.PlayerTeam, true);
					}
					AgentFlag agentFlags = agent.GetAgentFlags();
					agent.SetAgentFlags(agentFlags | AgentFlag.CanGetAlarmed);
				}
			}
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00012A20 File Offset: 0x00010C20
		public void StartConversation(Agent agent, bool setActionsInstantly)
		{
			Campaign.Current.ConversationManager.SetupAndStartMissionConversation(agent, base.Mission.MainAgent, setActionsInstantly);
			foreach (IAgent agent2 in Campaign.Current.ConversationManager.ConversationAgents)
			{
				Agent agent3 = (Agent)agent2;
				agent3.ForceAiBehaviorSelection();
				agent3.AgentVisuals.SetClothComponentKeepStateOfAllMeshes(true);
			}
			base.Mission.MainAgentServer.AgentVisuals.SetClothComponentKeepStateOfAllMeshes(true);
			base.Mission.SetMissionMode(MissionMode.Conversation, setActionsInstantly);
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00012AC4 File Offset: 0x00010CC4
		private void SpawnAgents()
		{
			Agent targetAgent = null;
			IMissionTroopSupplier[] troopSuppliers = this._troopSuppliers;
			for (int i = 0; i < troopSuppliers.Length; i++)
			{
				foreach (IAgentOriginBase agentOriginBase in troopSuppliers[i].SupplyTroops(25).ToList<IAgentOriginBase>())
				{
					Agent agent = Mission.Current.SpawnTroop(agentOriginBase, agentOriginBase.BattleCombatant.Side == BattleSideEnum.Attacker, false, false, false, 0, 0, false, true, true, null, null, null, null, FormationClass.NumberOfAllFormations, false);
					this._numSpawnedTroops++;
					if (!agent.IsMainAgent)
					{
						agent.AddComponent(new ScriptedMovementComponent(agent, agent.Character == this._characterToTalkTo, (float)(agentOriginBase.IsUnderPlayersCommand ? 5 : 2)));
						if (agent.Character == this._characterToTalkTo)
						{
							targetAgent = agent;
						}
					}
				}
			}
			foreach (Agent agent2 in base.Mission.Agents)
			{
				ScriptedMovementComponent component = agent2.GetComponent<ScriptedMovementComponent>();
				if (component != null)
				{
					if (agent2.Team.Side == Mission.Current.PlayerTeam.Side)
					{
						component.SetTargetAgent(targetAgent);
					}
					else
					{
						component.SetTargetAgent(Agent.Main);
					}
				}
				agent2.SetFiringOrder(FiringOrder.RangedWeaponUsageOrderEnum.HoldYourFire);
			}
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00012C5C File Offset: 0x00010E5C
		public void StartSpawner(BattleSideEnum side)
		{
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00012C5E File Offset: 0x00010E5E
		public void StopSpawner(BattleSideEnum side)
		{
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00012C60 File Offset: 0x00010E60
		public bool IsSideSpawnEnabled(BattleSideEnum side)
		{
			return false;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00012C63 File Offset: 0x00010E63
		public float GetReinforcementInterval()
		{
			return 0f;
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00012C6C File Offset: 0x00010E6C
		public bool IsSideDepleted(BattleSideEnum side)
		{
			int num = this._numSpawnedTroops - this._troopSuppliers[(int)side].NumRemovedTroops;
			if (Mission.Current.PlayerTeam == base.Mission.DefenderTeam)
			{
				if (side == BattleSideEnum.Attacker)
				{
					num--;
				}
				else if (Agent.Main != null && Agent.Main.IsActive())
				{
					num++;
				}
			}
			return num == 0;
		}

		// Token: 0x04000153 RID: 339
		private BattleAgentLogic _battleAgentLogic;

		// Token: 0x04000154 RID: 340
		private readonly BasicCharacterObject _characterToTalkTo;

		// Token: 0x04000155 RID: 341
		private bool _isMissionInitialized;

		// Token: 0x04000156 RID: 342
		private bool _troopsInitialized;

		// Token: 0x04000157 RID: 343
		private bool _conversationInitialized;

		// Token: 0x04000158 RID: 344
		private int _numSpawnedTroops;

		// Token: 0x04000159 RID: 345
		private readonly IMissionTroopSupplier[] _troopSuppliers;
	}
}
