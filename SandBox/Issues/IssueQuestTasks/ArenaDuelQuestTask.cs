using System;
using System.Collections.Generic;
using System.Linq;
using SandBox.Missions.MissionLogics.Arena;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.AgentOrigins;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace SandBox.Issues.IssueQuestTasks
{
	// Token: 0x0200008F RID: 143
	public class ArenaDuelQuestTask : QuestTaskBase
	{
		// Token: 0x06000585 RID: 1413 RVA: 0x00024EC4 File Offset: 0x000230C4
		public ArenaDuelQuestTask(CharacterObject duelOpponentCharacter, Settlement settlement, Action onSucceededAction, Action onFailedAction, DialogFlow dialogFlow = null) : base(dialogFlow, onSucceededAction, onFailedAction, null)
		{
			this._opponentCharacter = duelOpponentCharacter;
			this._settlement = settlement;
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00024EE0 File Offset: 0x000230E0
		public void AfterStart(IMission mission)
		{
			if (Mission.Current.HasMissionBehavior<ArenaDuelMissionBehavior>() && PlayerEncounter.LocationEncounter.Settlement == this._settlement)
			{
				this.InitializeTeams();
				List<MatrixFrame> list = (from e in Mission.Current.Scene.FindEntitiesWithTag("sp_arena_respawn")
				select e.GetGlobalFrame()).ToList<MatrixFrame>();
				MatrixFrame matrixFrame = list[MBRandom.RandomInt(list.Count)];
				float maxValue = float.MaxValue;
				MatrixFrame frame = matrixFrame;
				foreach (MatrixFrame matrixFrame2 in list)
				{
					if (matrixFrame != matrixFrame2)
					{
						Vec3 origin = matrixFrame2.origin;
						if (origin.DistanceSquared(matrixFrame.origin) < maxValue)
						{
							frame = matrixFrame2;
						}
					}
				}
				matrixFrame.rotation.OrthonormalizeAccordingToForwardAndKeepUpAsZAxis();
				frame.rotation.OrthonormalizeAccordingToForwardAndKeepUpAsZAxis();
				this._playerAgent = this.SpawnArenaAgent(CharacterObject.PlayerCharacter, Mission.Current.PlayerTeam, matrixFrame);
				this._opponentAgent = this.SpawnArenaAgent(this._opponentCharacter, Mission.Current.PlayerEnemyTeam, frame);
			}
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00025020 File Offset: 0x00023220
		public override void SetReferences()
		{
			CampaignEvents.AfterMissionStarted.AddNonSerializedListener(this, new Action<IMission>(this.AfterStart));
			CampaignEvents.GameMenuOpened.AddNonSerializedListener(this, new Action<MenuCallbackArgs>(this.OnGameMenuOpened));
			CampaignEvents.MissionTickEvent.AddNonSerializedListener(this, new Action<float>(this.MissionTick));
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00025072 File Offset: 0x00023272
		public void OnGameMenuOpened(MenuCallbackArgs args)
		{
			if (Hero.MainHero.CurrentSettlement == this._settlement)
			{
				if (this._duelStarted)
				{
					if (this._opponentAgent.IsActive())
					{
						base.Finish(QuestTaskBase.FinishStates.Fail);
						return;
					}
					base.Finish(QuestTaskBase.FinishStates.Success);
					return;
				}
				else
				{
					this.OpenArenaDuelMission();
				}
			}
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x000250B4 File Offset: 0x000232B4
		public void MissionTick(float dt)
		{
			if (Mission.Current.HasMissionBehavior<ArenaDuelMissionBehavior>() && PlayerEncounter.LocationEncounter.Settlement == this._settlement && ((this._playerAgent != null && !this._playerAgent.IsActive()) || (this._opponentAgent != null && !this._opponentAgent.IsActive())))
			{
				if (this._missionEndTimer != null && this._missionEndTimer.ElapsedTime > 4f)
				{
					Mission.Current.EndMission();
					return;
				}
				if (this._missionEndTimer == null && ((this._playerAgent != null && !this._playerAgent.IsActive()) || (this._opponentAgent != null && !this._opponentAgent.IsActive())))
				{
					this._missionEndTimer = new BasicMissionTimer();
				}
			}
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00025174 File Offset: 0x00023374
		private void OpenArenaDuelMission()
		{
			Location locationWithId = this._settlement.LocationComplex.GetLocationWithId("arena");
			int upgradeLevel = this._settlement.IsTown ? this._settlement.Town.GetWallLevel() : 1;
			SandBoxMissions.OpenArenaDuelMission(locationWithId.GetSceneName(upgradeLevel), locationWithId);
			this._duelStarted = true;
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x000251D0 File Offset: 0x000233D0
		private void InitializeTeams()
		{
			Mission.Current.Teams.Add(BattleSideEnum.Defender, Hero.MainHero.MapFaction.Color, Hero.MainHero.MapFaction.Color2, null, true, false, true);
			Mission.Current.Teams.Add(BattleSideEnum.Attacker, Hero.MainHero.MapFaction.Color2, Hero.MainHero.MapFaction.Color, null, true, false, true);
			Mission.Current.PlayerTeam = Mission.Current.DefenderTeam;
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00025258 File Offset: 0x00023458
		private Agent SpawnArenaAgent(CharacterObject character, Team team, MatrixFrame frame)
		{
			if (team == Mission.Current.PlayerTeam)
			{
				character = CharacterObject.PlayerCharacter;
			}
			Equipment randomElement = this._settlement.Culture.DuelPresetEquipmentRoster.AllEquipments.GetRandomElement<Equipment>();
			Mission mission = Mission.Current;
			AgentBuildData agentBuildData = new AgentBuildData(character).Team(team).ClothingColor1(team.Color).ClothingColor2(team.Color2).InitialPosition(frame.origin);
			Vec2 vec = frame.rotation.f.AsVec2;
			vec = vec.Normalized();
			Agent agent = mission.SpawnAgent(agentBuildData.InitialDirection(vec).NoHorses(true).Equipment(randomElement).TroopOrigin(new SimpleAgentOrigin(character, -1, null, default(UniqueTroopDescriptor))).Controller((character == CharacterObject.PlayerCharacter) ? Agent.ControllerType.Player : Agent.ControllerType.AI), false);
			if (agent.IsAIControlled)
			{
				agent.SetWatchState(Agent.WatchState.Alarmed);
			}
			return agent;
		}

		// Token: 0x0400029A RID: 666
		private Settlement _settlement;

		// Token: 0x0400029B RID: 667
		private CharacterObject _opponentCharacter;

		// Token: 0x0400029C RID: 668
		private Agent _playerAgent;

		// Token: 0x0400029D RID: 669
		private Agent _opponentAgent;

		// Token: 0x0400029E RID: 670
		private bool _duelStarted;

		// Token: 0x0400029F RID: 671
		private BasicMissionTimer _missionEndTimer;
	}
}
