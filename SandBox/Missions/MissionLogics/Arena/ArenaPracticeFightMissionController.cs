using System;
using System.Collections.Generic;
using System.Linq;
using SandBox.Conversation.MissionLogics;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.AgentOrigins;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.MissionLogics.Arena
{
	// Token: 0x02000072 RID: 114
	public class ArenaPracticeFightMissionController : MissionLogic
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x0001D3E4 File Offset: 0x0001B5E4
		private int AISpawnIndex
		{
			get
			{
				return this._spawnedOpponentAgentCount;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x0001D3EC File Offset: 0x0001B5EC
		// (set) Token: 0x06000446 RID: 1094 RVA: 0x0001D3F4 File Offset: 0x0001B5F4
		public int RemainingOpponentCountFromLastPractice { get; private set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x0001D3FD File Offset: 0x0001B5FD
		// (set) Token: 0x06000448 RID: 1096 RVA: 0x0001D405 File Offset: 0x0001B605
		public bool IsPlayerPracticing { get; private set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x0001D40E File Offset: 0x0001B60E
		// (set) Token: 0x0600044A RID: 1098 RVA: 0x0001D416 File Offset: 0x0001B616
		public int OpponentCountBeatenByPlayer { get; private set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x0001D41F File Offset: 0x0001B61F
		public int RemainingOpponentCount
		{
			get
			{
				return 30 - this._spawnedOpponentAgentCount + this._aliveOpponentCount;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x0001D431 File Offset: 0x0001B631
		// (set) Token: 0x0600044D RID: 1101 RVA: 0x0001D439 File Offset: 0x0001B639
		public bool IsPlayerSurvived { get; private set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x0001D442 File Offset: 0x0001B642
		// (set) Token: 0x0600044F RID: 1103 RVA: 0x0001D44A File Offset: 0x0001B64A
		public bool AfterPractice { get; set; }

		// Token: 0x06000450 RID: 1104 RVA: 0x0001D454 File Offset: 0x0001B654
		public override void AfterStart()
		{
			this._settlement = PlayerEncounter.LocationEncounter.Settlement;
			this.InitializeTeams();
			GameEntity item = base.Mission.Scene.FindEntityWithTag("tournament_practice") ?? base.Mission.Scene.FindEntityWithTag("tournament_fight");
			List<GameEntity> list = Mission.Current.Scene.FindEntitiesWithTag("arena_set").ToList<GameEntity>();
			list.Remove(item);
			foreach (GameEntity gameEntity in list)
			{
				gameEntity.Remove(88);
			}
			this._initialSpawnFrames = (from e in base.Mission.Scene.FindEntitiesWithTag("sp_arena")
			select e.GetGlobalFrame()).ToList<MatrixFrame>();
			this._spawnFrames = (from e in base.Mission.Scene.FindEntitiesWithTag("sp_arena_respawn")
			select e.GetGlobalFrame()).ToList<MatrixFrame>();
			for (int i = 0; i < this._initialSpawnFrames.Count; i++)
			{
				MatrixFrame value = this._initialSpawnFrames[i];
				value.rotation.OrthonormalizeAccordingToForwardAndKeepUpAsZAxis();
				this._initialSpawnFrames[i] = value;
			}
			for (int j = 0; j < this._spawnFrames.Count; j++)
			{
				MatrixFrame value2 = this._spawnFrames[j];
				value2.rotation.OrthonormalizeAccordingToForwardAndKeepUpAsZAxis();
				this._spawnFrames[j] = value2;
			}
			this.IsPlayerPracticing = false;
			this._participantAgents = new List<Agent>();
			this.StartPractice();
			MissionAgentHandler missionBehavior = base.Mission.GetMissionBehavior<MissionAgentHandler>();
			missionBehavior.SpawnPlayer(true, true, false, false, false, "");
			missionBehavior.SpawnLocationCharacters(null);
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0001D648 File Offset: 0x0001B848
		private void SpawnPlayerNearTournamentMaster()
		{
			GameEntity entity = base.Mission.Scene.FindEntityWithTag("sp_player_near_arena_master");
			base.Mission.SpawnAgent(new AgentBuildData(CharacterObject.PlayerCharacter).Team(base.Mission.PlayerTeam).InitialFrameFromSpawnPointEntity(entity).NoHorses(true).CivilianEquipment(true).TroopOrigin(new SimpleAgentOrigin(CharacterObject.PlayerCharacter, -1, null, default(UniqueTroopDescriptor))).Controller(Agent.ControllerType.Player), false);
			Mission.Current.SetMissionMode(MissionMode.StartUp, false);
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0001D6D0 File Offset: 0x0001B8D0
		private Agent SpawnArenaAgent(Team team, MatrixFrame frame)
		{
			CharacterObject characterObject;
			int spawnIndex;
			if (team == base.Mission.PlayerTeam)
			{
				characterObject = CharacterObject.PlayerCharacter;
				spawnIndex = 0;
			}
			else
			{
				characterObject = this._participantCharacters[this.AISpawnIndex];
				spawnIndex = this.AISpawnIndex;
			}
			Equipment equipment = new Equipment();
			this.AddRandomWeapons(equipment, spawnIndex);
			this.AddRandomClothes(characterObject, equipment);
			Mission mission = base.Mission;
			AgentBuildData agentBuildData = new AgentBuildData(characterObject).Team(team).InitialPosition(frame.origin);
			Vec2 vec = frame.rotation.f.AsVec2;
			vec = vec.Normalized();
			Agent agent = mission.SpawnAgent(agentBuildData.InitialDirection(vec).NoHorses(true).Equipment(equipment).TroopOrigin(new SimpleAgentOrigin(characterObject, -1, null, default(UniqueTroopDescriptor))).Controller((characterObject == CharacterObject.PlayerCharacter) ? Agent.ControllerType.Player : Agent.ControllerType.AI), false);
			agent.FadeIn();
			if (characterObject != CharacterObject.PlayerCharacter)
			{
				this._aliveOpponentCount++;
				this._spawnedOpponentAgentCount++;
			}
			if (agent.IsAIControlled)
			{
				agent.SetWatchState(Agent.WatchState.Alarmed);
			}
			return agent;
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0001D7DC File Offset: 0x0001B9DC
		public override void OnScoreHit(Agent affectedAgent, Agent affectorAgent, WeaponComponentData attackerWeapon, bool isBlocked, bool isSiegeEngineHit, in Blow blow, in AttackCollisionData collisionData, float damagedHp, float hitDistance, float shotDifficulty)
		{
			if (affectorAgent == null)
			{
				return;
			}
			if (affectorAgent.IsMount && affectorAgent.RiderAgent != null)
			{
				affectorAgent = affectorAgent.RiderAgent;
			}
			if (affectorAgent.Character == null || affectedAgent.Character == null)
			{
				return;
			}
			float num = (float)blow.InflictedDamage;
			if (num > affectedAgent.HealthLimit)
			{
				num = affectedAgent.HealthLimit;
			}
			float num2 = num / affectedAgent.HealthLimit;
			this.EnemyHitReward(affectedAgent, affectorAgent, blow.MovementSpeedDamageModifier, shotDifficulty, attackerWeapon, blow.AttackType, 0.5f * num2, num);
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0001D85C File Offset: 0x0001BA5C
		private void EnemyHitReward(Agent affectedAgent, Agent affectorAgent, float lastSpeedBonus, float lastShotDifficulty, WeaponComponentData attackerWeapon, AgentAttackType attackType, float hitpointRatio, float damageAmount)
		{
			CharacterObject affectedCharacter = (CharacterObject)affectedAgent.Character;
			CharacterObject affectorCharacter = (CharacterObject)affectorAgent.Character;
			if (affectedAgent.Origin != null && affectorAgent != null && affectorAgent.Origin != null)
			{
				bool flag = affectorAgent.MountAgent != null;
				bool isHorseCharge = flag && attackType == AgentAttackType.Collision;
				SkillLevelingManager.OnCombatHit(affectorCharacter, affectedCharacter, null, null, lastSpeedBonus, lastShotDifficulty, attackerWeapon, hitpointRatio, CombatXpModel.MissionTypeEnum.PracticeFight, flag, affectorAgent.Team == affectedAgent.Team, false, damageAmount, affectedAgent.Health < 1f, false, isHorseCharge);
			}
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0001D8DC File Offset: 0x0001BADC
		public override void OnMissionTick(float dt)
		{
			base.OnMissionTick(dt);
			if (this._aliveOpponentCount < 6 && this._spawnedOpponentAgentCount < 30 && (this._aliveOpponentCount == 2 || this._nextSpawnTime < base.Mission.CurrentTime))
			{
				Team team = this.SelectRandomAiTeam();
				Agent item = this.SpawnArenaAgent(team, this.GetSpawnFrame(true, false));
				this._participantAgents.Add(item);
				this._nextSpawnTime = base.Mission.CurrentTime + 14f - (float)this._spawnedOpponentAgentCount / 3f;
				if (this._spawnedOpponentAgentCount == 30 && !this.IsPlayerPracticing)
				{
					this._spawnedOpponentAgentCount = 0;
				}
			}
			if (this._teleportTimer == null && this.IsPlayerPracticing && this.CheckPracticeEndedForPlayer())
			{
				this._teleportTimer = new BasicMissionTimer();
				this.IsPlayerSurvived = (base.Mission.MainAgent != null && base.Mission.MainAgent.IsActive());
				if (this.IsPlayerSurvived)
				{
					MBInformationManager.AddQuickInformation(new TextObject("{=seyti8xR}Victory!", null), 0, null, "event:/ui/mission/arena_victory");
				}
				this.AfterPractice = true;
			}
			if (this._teleportTimer != null && this._teleportTimer.ElapsedTime > (float)this.TeleportTime)
			{
				this._teleportTimer = null;
				this.RemainingOpponentCountFromLastPractice = this.RemainingOpponentCount;
				this.IsPlayerPracticing = false;
				this.StartPractice();
				this.SpawnPlayerNearTournamentMaster();
				Agent agent = base.Mission.Agents.FirstOrDefault((Agent x) => x.Character != null && ((CharacterObject)x.Character).Occupation == Occupation.ArenaMaster);
				MissionConversationLogic.Current.StartConversation(agent, true, false);
			}
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0001DA74 File Offset: 0x0001BC74
		private Team SelectRandomAiTeam()
		{
			Team team = null;
			foreach (Team team2 in this._AIParticipantTeams)
			{
				if (!team2.HasBots)
				{
					team = team2;
					break;
				}
			}
			if (team == null)
			{
				team = this._AIParticipantTeams[MBRandom.RandomInt(this._AIParticipantTeams.Count - 1) + 1];
			}
			return team;
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0001DAF4 File Offset: 0x0001BCF4
		public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow killingBlow)
		{
			if (affectedAgent != null && affectedAgent.IsHuman)
			{
				if (affectedAgent != Agent.Main)
				{
					this._aliveOpponentCount--;
				}
				if (affectorAgent != null && affectorAgent.IsHuman && affectorAgent == Agent.Main && affectedAgent != Agent.Main)
				{
					int opponentCountBeatenByPlayer = this.OpponentCountBeatenByPlayer;
					this.OpponentCountBeatenByPlayer = opponentCountBeatenByPlayer + 1;
				}
			}
			if (this._participantAgents.Contains(affectedAgent))
			{
				this._participantAgents.Remove(affectedAgent);
			}
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0001DB68 File Offset: 0x0001BD68
		public override bool MissionEnded(ref MissionResult missionResult)
		{
			return false;
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0001DB6C File Offset: 0x0001BD6C
		public override InquiryData OnEndMissionRequest(out bool canPlayerLeave)
		{
			canPlayerLeave = true;
			if (!this.IsPlayerPracticing)
			{
				return null;
			}
			return new InquiryData(new TextObject("{=zv49qE35}Practice Fight", null).ToString(), GameTexts.FindText("str_give_up_fight", null).ToString(), true, true, GameTexts.FindText("str_ok", null).ToString(), GameTexts.FindText("str_cancel", null).ToString(), new Action(base.Mission.OnEndMissionResult), null, "", 0f, null, null, null);
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0001DBEC File Offset: 0x0001BDEC
		public void StartPlayerPractice()
		{
			this.IsPlayerPracticing = true;
			this.AfterPractice = false;
			this.StartPractice();
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0001DC04 File Offset: 0x0001BE04
		private void StartPractice()
		{
			this.InitializeParticipantCharacters();
			SandBoxHelpers.MissionHelper.FadeOutAgents(from agent in base.Mission.Agents
			where this._participantAgents.Contains(agent) || agent.IsMount || agent.IsPlayerControlled
			select agent, true, false);
			this._spawnedOpponentAgentCount = 0;
			this._aliveOpponentCount = 0;
			this._participantAgents.Clear();
			Mission.Current.ClearCorpses(false);
			base.Mission.RemoveSpawnedItemsAndMissiles();
			this.ArrangePlayerTeamEnmity();
			if (this.IsPlayerPracticing)
			{
				Agent agent2 = this.SpawnArenaAgent(base.Mission.PlayerTeam, this.GetSpawnFrame(false, true));
				agent2.WieldInitialWeapons(Agent.WeaponWieldActionType.InstantAfterPickUp, Equipment.InitialWeaponEquipPreference.Any);
				this.OpponentCountBeatenByPlayer = 0;
				this._participantAgents.Add(agent2);
			}
			int count = this._AIParticipantTeams.Count;
			int num = 0;
			while (this._spawnedOpponentAgentCount < 6)
			{
				this._participantAgents.Add(this.SpawnArenaAgent(this._AIParticipantTeams[num % count], this.GetSpawnFrame(false, true)));
				num++;
			}
			this._nextSpawnTime = base.Mission.CurrentTime + 14f;
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0001DD07 File Offset: 0x0001BF07
		private bool CheckPracticeEndedForPlayer()
		{
			return base.Mission.MainAgent == null || !base.Mission.MainAgent.IsActive() || this.RemainingOpponentCount == 0;
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0001DD34 File Offset: 0x0001BF34
		private void AddRandomWeapons(Equipment equipment, int spawnIndex)
		{
			int num = 1 + spawnIndex * 3 / 30;
			List<Equipment> list = (Game.Current.ObjectManager.GetObject<CharacterObject>(string.Concat(new object[]
			{
				"weapon_practice_stage_",
				num,
				"_",
				this._settlement.MapFaction.Culture.StringId
			})) ?? Game.Current.ObjectManager.GetObject<CharacterObject>("weapon_practice_stage_" + num + "_empire")).BattleEquipments.ToList<Equipment>();
			int index = MBRandom.RandomInt(list.Count);
			for (int i = 0; i <= 3; i++)
			{
				EquipmentElement equipmentFromSlot = list[index].GetEquipmentFromSlot((EquipmentIndex)i);
				if (equipmentFromSlot.Item != null)
				{
					equipment.AddEquipmentToSlotWithoutAgent((EquipmentIndex)i, equipmentFromSlot);
				}
			}
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0001DE04 File Offset: 0x0001C004
		private void AddRandomClothes(CharacterObject troop, Equipment equipment)
		{
			Equipment participantArmor = Campaign.Current.Models.TournamentModel.GetParticipantArmor(troop);
			for (int i = 0; i < 12; i++)
			{
				if (i > 4 && i != 10 && i != 11)
				{
					EquipmentElement equipmentFromSlot = participantArmor.GetEquipmentFromSlot((EquipmentIndex)i);
					if (equipmentFromSlot.Item != null)
					{
						equipment.AddEquipmentToSlotWithoutAgent((EquipmentIndex)i, equipmentFromSlot);
					}
				}
			}
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0001DE5C File Offset: 0x0001C05C
		private void InitializeTeams()
		{
			this._AIParticipantTeams = new List<Team>();
			base.Mission.Teams.Add(BattleSideEnum.Defender, Hero.MainHero.MapFaction.Color, Hero.MainHero.MapFaction.Color2, null, true, false, true);
			base.Mission.PlayerTeam = base.Mission.DefenderTeam;
			this._tournamentMasterTeam = base.Mission.Teams.Add(BattleSideEnum.None, this._settlement.MapFaction.Color, this._settlement.MapFaction.Color2, null, true, false, true);
			while (this._AIParticipantTeams.Count < 6)
			{
				this._AIParticipantTeams.Add(base.Mission.Teams.Add(BattleSideEnum.Attacker, uint.MaxValue, uint.MaxValue, null, true, false, true));
			}
			for (int i = 0; i < this._AIParticipantTeams.Count; i++)
			{
				this._AIParticipantTeams[i].SetIsEnemyOf(this._tournamentMasterTeam, false);
				for (int j = i + 1; j < this._AIParticipantTeams.Count; j++)
				{
					this._AIParticipantTeams[i].SetIsEnemyOf(this._AIParticipantTeams[j], true);
				}
			}
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0001DF90 File Offset: 0x0001C190
		private void InitializeParticipantCharacters()
		{
			List<CharacterObject> participantCharacters = ArenaPracticeFightMissionController.GetParticipantCharacters(this._settlement);
			this._participantCharacters = (from x in participantCharacters
			orderby x.Level
			select x).ToList<CharacterObject>();
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0001DFDC File Offset: 0x0001C1DC
		public static List<CharacterObject> GetParticipantCharacters(Settlement settlement)
		{
			int num = 30;
			List<CharacterObject> list = new List<CharacterObject>();
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			if (list.Count < num && settlement.Town.GarrisonParty != null)
			{
				foreach (TroopRosterElement troopRosterElement in settlement.Town.GarrisonParty.MemberRoster.GetTroopRoster())
				{
					int num5 = num - list.Count;
					if (!list.Contains(troopRosterElement.Character) && troopRosterElement.Character.Tier == 3 && (float)num5 * 0.4f > (float)num2)
					{
						list.Add(troopRosterElement.Character);
						num2++;
					}
					else if (!list.Contains(troopRosterElement.Character) && troopRosterElement.Character.Tier == 4 && (float)num5 * 0.4f > (float)num3)
					{
						list.Add(troopRosterElement.Character);
						num3++;
					}
					else if (!list.Contains(troopRosterElement.Character) && troopRosterElement.Character.Tier == 5 && (float)num5 * 0.2f > (float)num4)
					{
						list.Add(troopRosterElement.Character);
						num4++;
					}
					if (list.Count >= num)
					{
						break;
					}
				}
			}
			if (list.Count < num)
			{
				List<CharacterObject> list2 = new List<CharacterObject>();
				ArenaPracticeFightMissionController.GetUpgradeTargets(((settlement != null) ? settlement.Culture : Game.Current.ObjectManager.GetObject<CultureObject>("empire")).BasicTroop, ref list2);
				int num6 = num - list.Count;
				using (List<CharacterObject>.Enumerator enumerator2 = list2.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						CharacterObject characterObject = enumerator2.Current;
						if (!list.Contains(characterObject) && characterObject.Tier == 3 && (float)num6 * 0.4f > (float)num2)
						{
							list.Add(characterObject);
							num2++;
						}
						else if (!list.Contains(characterObject) && characterObject.Tier == 4 && (float)num6 * 0.4f > (float)num3)
						{
							list.Add(characterObject);
							num3++;
						}
						else if (!list.Contains(characterObject) && characterObject.Tier == 5 && (float)num6 * 0.2f > (float)num4)
						{
							list.Add(characterObject);
							num4++;
						}
						if (list.Count >= num)
						{
							break;
						}
					}
					goto IL_284;
				}
				IL_256:
				int num7 = 0;
				while (num7 < list2.Count && list.Count < num)
				{
					list.Add(list2[num7]);
					num7++;
				}
				IL_284:
				if (list.Count < num)
				{
					goto IL_256;
				}
			}
			return list;
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0001E294 File Offset: 0x0001C494
		private static void GetUpgradeTargets(CharacterObject troop, ref List<CharacterObject> list)
		{
			if (!list.Contains(troop) && troop.Tier >= 3)
			{
				list.Add(troop);
			}
			CharacterObject[] upgradeTargets = troop.UpgradeTargets;
			for (int i = 0; i < upgradeTargets.Length; i++)
			{
				ArenaPracticeFightMissionController.GetUpgradeTargets(upgradeTargets[i], ref list);
			}
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0001E2DC File Offset: 0x0001C4DC
		private void ArrangePlayerTeamEnmity()
		{
			foreach (Team team in this._AIParticipantTeams)
			{
				team.SetIsEnemyOf(base.Mission.PlayerTeam, this.IsPlayerPracticing);
			}
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0001E340 File Offset: 0x0001C540
		private Team GetStrongestTeamExceptPlayerTeam()
		{
			Team result = null;
			int num = -1;
			foreach (Team team in this._AIParticipantTeams)
			{
				int num2 = this.CalculateTeamPower(team);
				if (num2 > num)
				{
					result = team;
					num = num2;
				}
			}
			return result;
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0001E3A4 File Offset: 0x0001C5A4
		private int CalculateTeamPower(Team team)
		{
			int num = 0;
			foreach (Agent agent in team.ActiveAgents)
			{
				num += agent.Character.Level * agent.KillCount + (int)MathF.Sqrt(agent.Health);
			}
			return num;
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0001E418 File Offset: 0x0001C618
		private MatrixFrame GetSpawnFrame(bool considerPlayerDistance, bool isInitialSpawn)
		{
			List<MatrixFrame> list = (isInitialSpawn || this._spawnFrames.IsEmpty<MatrixFrame>()) ? this._initialSpawnFrames : this._spawnFrames;
			if (list.Count == 1)
			{
				Debug.FailedAssert("Spawn point count is wrong! Arena practice spawn point set should be used in arena scenes.", "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox\\Missions\\MissionLogics\\Arena\\ArenaPracticeFightMissionController.cs", "GetSpawnFrame", 615);
				return list[0];
			}
			MatrixFrame result;
			if (considerPlayerDistance && Agent.Main != null && Agent.Main.IsActive())
			{
				int num = MBRandom.RandomInt(list.Count);
				result = list[num];
				float num2 = float.MinValue;
				for (int i = num + 1; i < num + list.Count; i++)
				{
					MatrixFrame matrixFrame = list[i % list.Count];
					float num3 = this.CalculateLocationScore(matrixFrame);
					if (num3 >= 100f)
					{
						result = matrixFrame;
						break;
					}
					if (num3 > num2)
					{
						result = matrixFrame;
						num2 = num3;
					}
				}
			}
			else
			{
				int num4 = this._spawnedOpponentAgentCount;
				if (this.IsPlayerPracticing && Agent.Main != null)
				{
					num4++;
				}
				result = list[num4 % list.Count];
			}
			return result;
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0001E51C File Offset: 0x0001C71C
		private float CalculateLocationScore(MatrixFrame matrixFrame)
		{
			float num = 100f;
			float num2 = 0.25f;
			float num3 = 0.75f;
			if (matrixFrame.origin.DistanceSquared(Agent.Main.Position) < 144f)
			{
				num *= num2;
			}
			for (int i = 0; i < this._participantAgents.Count; i++)
			{
				if (this._participantAgents[i].Position.DistanceSquared(matrixFrame.origin) < 144f)
				{
					num *= num3;
				}
			}
			return num;
		}

		// Token: 0x040001F1 RID: 497
		private const int AIParticipantCount = 30;

		// Token: 0x040001F2 RID: 498
		private const int MaxAliveAgentCount = 6;

		// Token: 0x040001F3 RID: 499
		private const int MaxSpawnInterval = 14;

		// Token: 0x040001F4 RID: 500
		private const int MinSpawnDistanceSquared = 144;

		// Token: 0x040001F5 RID: 501
		private const int TotalStageCount = 3;

		// Token: 0x040001F6 RID: 502
		private const int PracticeFightTroopTierLimit = 3;

		// Token: 0x040001F7 RID: 503
		public int TeleportTime = 5;

		// Token: 0x040001F8 RID: 504
		private Settlement _settlement;

		// Token: 0x040001F9 RID: 505
		private int _spawnedOpponentAgentCount;

		// Token: 0x040001FA RID: 506
		private int _aliveOpponentCount;

		// Token: 0x040001FB RID: 507
		private float _nextSpawnTime;

		// Token: 0x040001FC RID: 508
		private List<MatrixFrame> _initialSpawnFrames;

		// Token: 0x040001FD RID: 509
		private List<MatrixFrame> _spawnFrames;

		// Token: 0x040001FE RID: 510
		private List<Team> _AIParticipantTeams;

		// Token: 0x040001FF RID: 511
		private List<Agent> _participantAgents;

		// Token: 0x04000200 RID: 512
		private Team _tournamentMasterTeam;

		// Token: 0x04000201 RID: 513
		private BasicMissionTimer _teleportTimer;

		// Token: 0x04000202 RID: 514
		private List<CharacterObject> _participantCharacters;

		// Token: 0x04000208 RID: 520
		private const float XpShareForKill = 0.5f;

		// Token: 0x04000209 RID: 521
		private const float XpShareForDamage = 0.5f;
	}
}
