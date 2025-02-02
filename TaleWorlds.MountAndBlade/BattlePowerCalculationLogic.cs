using System;
using System.Collections.Generic;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200026E RID: 622
	public class BattlePowerCalculationLogic : MissionLogic
	{
		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x060020E2 RID: 8418 RVA: 0x00076056 File Offset: 0x00074256
		// (set) Token: 0x060020E3 RID: 8419 RVA: 0x0007605E File Offset: 0x0007425E
		public bool IsTeamPowersCalculated { get; private set; }

		// Token: 0x060020E4 RID: 8420 RVA: 0x00076068 File Offset: 0x00074268
		public BattlePowerCalculationLogic()
		{
			this._sidePowerData = new Dictionary<Team, float>[2];
			for (int i = 0; i < 2; i++)
			{
				this._sidePowerData[i] = new Dictionary<Team, float>();
			}
			this.IsTeamPowersCalculated = false;
		}

		// Token: 0x060020E5 RID: 8421 RVA: 0x000760A7 File Offset: 0x000742A7
		public float GetTotalTeamPower(Team team)
		{
			if (!this.IsTeamPowersCalculated)
			{
				this.CalculateTeamPowers();
			}
			return this._sidePowerData[(int)team.Side][team];
		}

		// Token: 0x060020E6 RID: 8422 RVA: 0x000760CC File Offset: 0x000742CC
		private void CalculateTeamPowers()
		{
			Mission.TeamCollection teams = base.Mission.Teams;
			foreach (Team team in teams)
			{
				this._sidePowerData[(int)team.Side].Add(team, 0f);
			}
			MissionAgentSpawnLogic missionBehavior = base.Mission.GetMissionBehavior<MissionAgentSpawnLogic>();
			for (int i = 0; i < 2; i++)
			{
				BattleSideEnum battleSideEnum = (BattleSideEnum)i;
				IEnumerable<IAgentOriginBase> allTroopsForSide = missionBehavior.GetAllTroopsForSide(battleSideEnum);
				Dictionary<Team, float> dictionary = this._sidePowerData[i];
				bool isPlayerSide = base.Mission.PlayerTeam != null && base.Mission.PlayerTeam.Side == battleSideEnum;
				foreach (IAgentOriginBase agentOriginBase in allTroopsForSide)
				{
					Team agentTeam = Mission.GetAgentTeam(agentOriginBase, isPlayerSide);
					BasicCharacterObject troop = agentOriginBase.Troop;
					Dictionary<Team, float> dictionary2 = dictionary;
					Team key = agentTeam;
					dictionary2[key] += troop.GetPower();
				}
			}
			foreach (Team team2 in teams)
			{
				team2.QuerySystem.Expire();
			}
			this.IsTeamPowersCalculated = true;
		}

		// Token: 0x04000C33 RID: 3123
		private Dictionary<Team, float>[] _sidePowerData;
	}
}
