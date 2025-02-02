using System;
using System.Linq;
using SandBox.Tournaments.MissionLogics;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.AgentOrigins;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.MissionLogics.Arena
{
	// Token: 0x02000071 RID: 113
	public class ArenaDuelMissionController : MissionLogic
	{
		// Token: 0x0600043C RID: 1084 RVA: 0x0001D022 File Offset: 0x0001B222
		public ArenaDuelMissionController(CharacterObject duelCharacter, bool requireCivilianEquipment, bool spawnBothSideWithHorses, Action<CharacterObject> onDuelEnd, float customAgentHealth)
		{
			this._duelCharacter = duelCharacter;
			this._requireCivilianEquipment = requireCivilianEquipment;
			this._spawnBothSideWithHorses = spawnBothSideWithHorses;
			this._customAgentHealth = customAgentHealth;
			ArenaDuelMissionController._onDuelEnd = onDuelEnd;
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0001D050 File Offset: 0x0001B250
		public override void AfterStart()
		{
			this._duelHasEnded = false;
			this._duelEndTimer = new BasicMissionTimer();
			this.DeactivateOtherTournamentSets();
			this.InitializeMissionTeams();
			this._initialSpawnFrames = (from e in base.Mission.Scene.FindEntitiesWithTag("sp_arena")
			select e.GetGlobalFrame()).ToMBList<MatrixFrame>();
			for (int i = 0; i < this._initialSpawnFrames.Count; i++)
			{
				MatrixFrame value = this._initialSpawnFrames[i];
				value.rotation.OrthonormalizeAccordingToForwardAndKeepUpAsZAxis();
				this._initialSpawnFrames[i] = value;
			}
			MatrixFrame randomElement = this._initialSpawnFrames.GetRandomElement<MatrixFrame>();
			this._initialSpawnFrames.Remove(randomElement);
			MatrixFrame randomElement2 = this._initialSpawnFrames.GetRandomElement<MatrixFrame>();
			this.SpawnAgent(CharacterObject.PlayerCharacter, randomElement);
			this._duelAgent = this.SpawnAgent(this._duelCharacter, randomElement2);
			this._duelAgent.Defensiveness = 1f;
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0001D150 File Offset: 0x0001B350
		private void InitializeMissionTeams()
		{
			base.Mission.Teams.Add(BattleSideEnum.Defender, Hero.MainHero.MapFaction.Color, Hero.MainHero.MapFaction.Color2, null, true, false, true);
			base.Mission.Teams.Add(BattleSideEnum.Attacker, this._duelCharacter.Culture.Color, this._duelCharacter.Culture.Color2, null, true, false, true);
			base.Mission.PlayerTeam = base.Mission.Teams.Defender;
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0001D1E2 File Offset: 0x0001B3E2
		private void DeactivateOtherTournamentSets()
		{
			TournamentBehavior.DeleteTournamentSetsExcept(base.Mission.Scene.FindEntityWithTag("tournament_fight"));
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0001D200 File Offset: 0x0001B400
		private Agent SpawnAgent(CharacterObject character, MatrixFrame spawnFrame)
		{
			AgentBuildData agentBuildData = new AgentBuildData(character);
			agentBuildData.BodyProperties(character.GetBodyPropertiesMax());
			Mission mission = base.Mission;
			AgentBuildData agentBuildData2 = agentBuildData.Team((character == CharacterObject.PlayerCharacter) ? base.Mission.PlayerTeam : base.Mission.PlayerEnemyTeam).InitialPosition(spawnFrame.origin);
			Vec2 vec = spawnFrame.rotation.f.AsVec2;
			vec = vec.Normalized();
			Agent agent = mission.SpawnAgent(agentBuildData2.InitialDirection(vec).NoHorses(!this._spawnBothSideWithHorses).Equipment(this._requireCivilianEquipment ? character.FirstCivilianEquipment : character.FirstBattleEquipment).TroopOrigin(new SimpleAgentOrigin(character, -1, null, default(UniqueTroopDescriptor))), false);
			agent.FadeIn();
			if (character == CharacterObject.PlayerCharacter)
			{
				agent.Controller = Agent.ControllerType.Player;
			}
			if (agent.IsAIControlled)
			{
				agent.SetWatchState(Agent.WatchState.Alarmed);
			}
			agent.Health = this._customAgentHealth;
			agent.BaseHealthLimit = this._customAgentHealth;
			agent.HealthLimit = this._customAgentHealth;
			return agent;
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0001D30C File Offset: 0x0001B50C
		public override void OnMissionTick(float dt)
		{
			if (this._duelHasEnded && this._duelEndTimer.ElapsedTime > 4f)
			{
				GameTexts.SetVariable("leave_key", HyperlinkTexts.GetKeyHyperlinkText(HotKeyManager.GetHotKeyId("Generic", 4)));
				MBInformationManager.AddQuickInformation(GameTexts.FindText("str_duel_has_ended", null), 0, null, "");
				this._duelEndTimer.Reset();
			}
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0001D370 File Offset: 0x0001B570
		public override void OnAgentRemoved(Agent affectedAgent, Agent affectorAgent, AgentState agentState, KillingBlow killingBlow)
		{
			if (ArenaDuelMissionController._onDuelEnd != null)
			{
				ArenaDuelMissionController._onDuelEnd((affectedAgent == this._duelAgent) ? CharacterObject.PlayerCharacter : this._duelCharacter);
				ArenaDuelMissionController._onDuelEnd = null;
				this._duelHasEnded = true;
				this._duelEndTimer.Reset();
			}
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0001D3BC File Offset: 0x0001B5BC
		public override InquiryData OnEndMissionRequest(out bool canPlayerLeave)
		{
			canPlayerLeave = true;
			if (!this._duelHasEnded)
			{
				canPlayerLeave = false;
				MBInformationManager.AddQuickInformation(GameTexts.FindText("str_can_not_retreat_duel_ongoing", null), 0, null, "");
			}
			return null;
		}

		// Token: 0x040001E8 RID: 488
		private CharacterObject _duelCharacter;

		// Token: 0x040001E9 RID: 489
		private bool _requireCivilianEquipment;

		// Token: 0x040001EA RID: 490
		private bool _spawnBothSideWithHorses;

		// Token: 0x040001EB RID: 491
		private bool _duelHasEnded;

		// Token: 0x040001EC RID: 492
		private Agent _duelAgent;

		// Token: 0x040001ED RID: 493
		private float _customAgentHealth;

		// Token: 0x040001EE RID: 494
		private BasicMissionTimer _duelEndTimer;

		// Token: 0x040001EF RID: 495
		private MBList<MatrixFrame> _initialSpawnFrames;

		// Token: 0x040001F0 RID: 496
		private static Action<CharacterObject> _onDuelEnd;
	}
}
