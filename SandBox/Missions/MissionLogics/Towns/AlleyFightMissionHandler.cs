using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.AgentOrigins;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.MissionLogics.Towns
{
	// Token: 0x0200006B RID: 107
	public class AlleyFightMissionHandler : MissionLogic, IMissionAgentSpawnLogic, IMissionBehavior
	{
		// Token: 0x06000408 RID: 1032 RVA: 0x0001B87A File Offset: 0x00019A7A
		public AlleyFightMissionHandler(TroopRoster playerSideTroops, TroopRoster rivalSideTroops)
		{
			this._playerSideTroops = playerSideTroops;
			this._rivalSideTroops = rivalSideTroops;
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0001B8A8 File Offset: 0x00019AA8
		public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow blow)
		{
			base.OnAgentRemoved(affectedAgent, affectorAgent, agentState, blow);
			if (this._playerSideAliveAgents.Contains(affectedAgent))
			{
				this._playerSideAliveAgents.Remove(affectedAgent);
				this._playerSideTroops.RemoveTroop(affectedAgent.Character as CharacterObject, 1, default(UniqueTroopDescriptor), 0);
			}
			else if (this._rivalSideAliveAgents.Contains(affectedAgent))
			{
				this._rivalSideAliveAgents.Remove(affectedAgent);
				this._rivalSideTroops.RemoveTroop(affectedAgent.Character as CharacterObject, 1, default(UniqueTroopDescriptor), 0);
			}
			if (affectedAgent == Agent.Main)
			{
				Campaign.Current.GetCampaignBehavior<IAlleyCampaignBehavior>().OnPlayerDiedInMission();
			}
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0001B954 File Offset: 0x00019B54
		public override void AfterStart()
		{
			base.Mission.Teams.Add(BattleSideEnum.Defender, Clan.PlayerClan.Color, Clan.PlayerClan.Color2, Clan.PlayerClan.Banner, true, false, true);
			base.Mission.Teams.Add(BattleSideEnum.Attacker, Clan.BanditFactions.First<Clan>().Color, Clan.BanditFactions.First<Clan>().Color2, Clan.BanditFactions.First<Clan>().Banner, true, false, true);
			base.Mission.PlayerTeam = base.Mission.DefenderTeam;
			base.Mission.AddTroopsToDeploymentPlan(BattleSideEnum.Defender, DeploymentPlanType.Initial, FormationClass.Infantry, this._playerSideTroops.TotalManCount, 0);
			base.Mission.AddTroopsToDeploymentPlan(BattleSideEnum.Attacker, DeploymentPlanType.Initial, FormationClass.Infantry, this._rivalSideTroops.TotalManCount, 0);
			base.Mission.MakeDefaultDeploymentPlans();
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0001BA2C File Offset: 0x00019C2C
		public override InquiryData OnEndMissionRequest(out bool canLeave)
		{
			canLeave = true;
			return new InquiryData("", GameTexts.FindText("str_give_up_fight", null).ToString(), true, true, GameTexts.FindText("str_ok", null).ToString(), GameTexts.FindText("str_cancel", null).ToString(), new Action(base.Mission.OnEndMissionResult), null, "", 0f, null, null, null);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0001BA97 File Offset: 0x00019C97
		public override void OnRetreatMission()
		{
			Campaign.Current.GetCampaignBehavior<IAlleyCampaignBehavior>().OnPlayerRetreatedFromMission();
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0001BAA8 File Offset: 0x00019CA8
		public override void OnRenderingStarted()
		{
			Mission.Current.SetMissionMode(MissionMode.Battle, true);
			this.SpawnAgentsForBothSides();
			base.Mission.PlayerTeam.PlayerOrderController.SelectAllFormations(false);
			base.Mission.PlayerTeam.PlayerOrderController.SetOrder(OrderType.Charge);
			base.Mission.PlayerEnemyTeam.MasterOrderController.SelectAllFormations(false);
			base.Mission.PlayerEnemyTeam.MasterOrderController.SetOrder(OrderType.Charge);
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0001BB20 File Offset: 0x00019D20
		private void SpawnAgentsForBothSides()
		{
			Mission.Current.PlayerEnemyTeam.SetIsEnemyOf(Mission.Current.PlayerTeam, true);
			foreach (TroopRosterElement troopRosterElement in this._playerSideTroops.GetTroopRoster())
			{
				for (int i = 0; i < troopRosterElement.Number; i++)
				{
					this.SpawnATroop(troopRosterElement.Character, true);
				}
			}
			foreach (TroopRosterElement troopRosterElement2 in this._rivalSideTroops.GetTroopRoster())
			{
				for (int j = 0; j < troopRosterElement2.Number; j++)
				{
					this.SpawnATroop(troopRosterElement2.Character, false);
				}
			}
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0001BC10 File Offset: 0x00019E10
		private void SpawnATroop(CharacterObject character, bool isPlayerSide)
		{
			SimpleAgentOrigin troopOrigin = new SimpleAgentOrigin(character, -1, null, default(UniqueTroopDescriptor));
			Agent agent = Mission.Current.SpawnTroop(troopOrigin, isPlayerSide, true, false, false, 0, 0, true, true, true, null, null, null, null, FormationClass.NumberOfAllFormations, false);
			if (isPlayerSide)
			{
				this._playerSideAliveAgents.Add(agent);
			}
			else
			{
				this._rivalSideAliveAgents.Add(agent);
			}
			AgentFlag agentFlags = agent.GetAgentFlags();
			agent.SetAgentFlags((agentFlags | AgentFlag.CanGetAlarmed) & ~AgentFlag.CanRetreat);
			if (agent.IsAIControlled)
			{
				agent.SetWatchState(Agent.WatchState.Alarmed);
			}
			if (isPlayerSide)
			{
				agent.SetTeam(Mission.Current.PlayerTeam, true);
				return;
			}
			agent.SetTeam(Mission.Current.PlayerEnemyTeam, true);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0001BCC9 File Offset: 0x00019EC9
		public void StartSpawner(BattleSideEnum side)
		{
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0001BCCB File Offset: 0x00019ECB
		public void StopSpawner(BattleSideEnum side)
		{
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0001BCCD File Offset: 0x00019ECD
		public bool IsSideSpawnEnabled(BattleSideEnum side)
		{
			return true;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0001BCD0 File Offset: 0x00019ED0
		public bool IsSideDepleted(BattleSideEnum side)
		{
			if (side != BattleSideEnum.Attacker)
			{
				return this._playerSideAliveAgents.Count == 0;
			}
			return this._rivalSideAliveAgents.Count == 0;
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0001BCF3 File Offset: 0x00019EF3
		public float GetReinforcementInterval()
		{
			return float.MaxValue;
		}

		// Token: 0x040001D5 RID: 469
		private TroopRoster _playerSideTroops;

		// Token: 0x040001D6 RID: 470
		private TroopRoster _rivalSideTroops;

		// Token: 0x040001D7 RID: 471
		private List<Agent> _playerSideAliveAgents = new List<Agent>();

		// Token: 0x040001D8 RID: 472
		private List<Agent> _rivalSideAliveAgents = new List<Agent>();
	}
}
