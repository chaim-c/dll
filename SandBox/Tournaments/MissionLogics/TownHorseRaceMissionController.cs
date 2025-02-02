using System;
using System.Collections.Generic;
using System.Linq;
using SandBox.Tournaments.AgentControllers;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.TournamentGames;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace SandBox.Tournaments.MissionLogics
{
	// Token: 0x02000031 RID: 49
	public class TownHorseRaceMissionController : MissionLogic, ITournamentGameBehavior
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600019D RID: 413 RVA: 0x0000B4B0 File Offset: 0x000096B0
		// (set) Token: 0x0600019E RID: 414 RVA: 0x0000B4B8 File Offset: 0x000096B8
		public List<TownHorseRaceMissionController.CheckPoint> CheckPoints { get; private set; }

		// Token: 0x0600019F RID: 415 RVA: 0x0000B4C1 File Offset: 0x000096C1
		public TownHorseRaceMissionController(CultureObject culture)
		{
			this._culture = culture;
			this._agents = new List<TownHorseRaceAgentController>();
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000B4DC File Offset: 0x000096DC
		public override void AfterStart()
		{
			base.AfterStart();
			this.CollectCheckPointsAndStartPoints();
			foreach (TownHorseRaceAgentController townHorseRaceAgentController in this._agents)
			{
				townHorseRaceAgentController.DisableMovement();
			}
			this._startTimer = new BasicMissionTimer();
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000B544 File Offset: 0x00009744
		public override void OnMissionTick(float dt)
		{
			base.OnMissionTick(dt);
			if (this._startTimer != null && this._startTimer.ElapsedTime > 3f)
			{
				foreach (TownHorseRaceAgentController townHorseRaceAgentController in this._agents)
				{
					townHorseRaceAgentController.Start();
				}
			}
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000B5B8 File Offset: 0x000097B8
		private void CollectCheckPointsAndStartPoints()
		{
			this.CheckPoints = new List<TownHorseRaceMissionController.CheckPoint>();
			foreach (GameEntity gameEntity in from amo in base.Mission.ActiveMissionObjects
			select amo.GameEntity)
			{
				VolumeBox firstScriptOfType = gameEntity.GetFirstScriptOfType<VolumeBox>();
				if (firstScriptOfType != null)
				{
					this.CheckPoints.Add(new TownHorseRaceMissionController.CheckPoint(firstScriptOfType));
				}
			}
			this.CheckPoints = (from x in this.CheckPoints
			orderby x.Name
			select x).ToList<TownHorseRaceMissionController.CheckPoint>();
			this._startPoints = base.Mission.Scene.FindEntitiesWithTag("sp_horse_race").ToList<GameEntity>();
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000B6A0 File Offset: 0x000098A0
		private MatrixFrame GetStartFrame(int index)
		{
			MatrixFrame result;
			if (index < this._startPoints.Count)
			{
				result = this._startPoints[index].GetGlobalFrame();
			}
			else
			{
				result = ((this._startPoints.Count > 0) ? this._startPoints[0].GetGlobalFrame() : MatrixFrame.Identity);
			}
			result.rotation.OrthonormalizeAccordingToForwardAndKeepUpAsZAxis();
			return result;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000B704 File Offset: 0x00009904
		private void SetItemsAndSpawnCharacter(CharacterObject troop)
		{
			int count = this._agents.Count;
			Equipment equipment = new Equipment();
			equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.ArmorItemEndSlot, new EquipmentElement(Game.Current.ObjectManager.GetObject<ItemObject>("charger"), null, null, false));
			equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.HorseHarness, new EquipmentElement(Game.Current.ObjectManager.GetObject<ItemObject>("horse_harness_e"), null, null, false));
			equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.WeaponItemBeginSlot, new EquipmentElement(Game.Current.ObjectManager.GetObject<ItemObject>("horse_whip"), null, null, false));
			equipment.AddEquipmentToSlotWithoutAgent(EquipmentIndex.Body, new EquipmentElement(Game.Current.ObjectManager.GetObject<ItemObject>("short_padded_robe"), null, null, false));
			MatrixFrame startFrame = this.GetStartFrame(count);
			AgentBuildData agentBuildData = new AgentBuildData(troop).Team(this._teams[count]).InitialPosition(startFrame.origin);
			Vec2 vec = startFrame.rotation.f.AsVec2;
			vec = vec.Normalized();
			AgentBuildData agentBuildData2 = agentBuildData.InitialDirection(vec).Equipment(equipment).Controller((troop == CharacterObject.PlayerCharacter) ? Agent.ControllerType.Player : Agent.ControllerType.AI);
			Agent agent = base.Mission.SpawnAgent(agentBuildData2, false);
			agent.Health = (float)agent.Monster.HitPoints;
			agent.WieldInitialWeapons(Agent.WeaponWieldActionType.InstantAfterPickUp, Equipment.InitialWeaponEquipPreference.Any);
			this._agents.Add(this.AddHorseRaceAgentController(agent));
			if (troop == CharacterObject.PlayerCharacter)
			{
				base.Mission.PlayerTeam = this._teams[count];
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000B875 File Offset: 0x00009A75
		private TownHorseRaceAgentController AddHorseRaceAgentController(Agent agent)
		{
			return agent.AddController(typeof(TownHorseRaceAgentController)) as TownHorseRaceAgentController;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000B88C File Offset: 0x00009A8C
		private void InitializeTeams(int count)
		{
			this._teams = new List<Team>();
			for (int i = 0; i < count; i++)
			{
				this._teams.Add(base.Mission.Teams.Add(BattleSideEnum.None, uint.MaxValue, uint.MaxValue, null, true, false, true));
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000B8D2 File Offset: 0x00009AD2
		public void StartMatch(TournamentMatch match, bool isLastRound)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000B8D9 File Offset: 0x00009AD9
		public void SkipMatch(TournamentMatch match)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000B8E0 File Offset: 0x00009AE0
		public bool IsMatchEnded()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000B8E7 File Offset: 0x00009AE7
		public void OnMatchEnded()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000096 RID: 150
		public const int TourCount = 2;

		// Token: 0x04000098 RID: 152
		private readonly List<TownHorseRaceAgentController> _agents;

		// Token: 0x04000099 RID: 153
		private List<Team> _teams;

		// Token: 0x0400009A RID: 154
		private List<GameEntity> _startPoints;

		// Token: 0x0400009B RID: 155
		private BasicMissionTimer _startTimer;

		// Token: 0x0400009C RID: 156
		private CultureObject _culture;

		// Token: 0x02000113 RID: 275
		public class CheckPoint
		{
			// Token: 0x170000E8 RID: 232
			// (get) Token: 0x06000B8F RID: 2959 RVA: 0x0005218B File Offset: 0x0005038B
			public string Name
			{
				get
				{
					return this._volumeBox.GameEntity.Name;
				}
			}

			// Token: 0x06000B90 RID: 2960 RVA: 0x000521A0 File Offset: 0x000503A0
			public CheckPoint(VolumeBox volumeBox)
			{
				this._volumeBox = volumeBox;
				this._bestTargetList = this._volumeBox.GameEntity.CollectChildrenEntitiesWithTag("best_target_point");
				this._volumeBox.SetIsOccupiedDelegate(new VolumeBox.VolumeBoxDelegate(this.OnAgentsEnterCheckBox));
			}

			// Token: 0x06000B91 RID: 2961 RVA: 0x000521EC File Offset: 0x000503EC
			public Vec3 GetBestTargetPosition()
			{
				Vec3 origin;
				if (this._bestTargetList.Count > 0)
				{
					origin = this._bestTargetList[MBRandom.RandomInt(this._bestTargetList.Count)].GetGlobalFrame().origin;
				}
				else
				{
					origin = this._volumeBox.GameEntity.GetGlobalFrame().origin;
				}
				return origin;
			}

			// Token: 0x06000B92 RID: 2962 RVA: 0x00052246 File Offset: 0x00050446
			public void AddToCheckList(Agent agent)
			{
				this._volumeBox.AddToCheckList(agent);
			}

			// Token: 0x06000B93 RID: 2963 RVA: 0x00052254 File Offset: 0x00050454
			public void RemoveFromCheckList(Agent agent)
			{
				this._volumeBox.RemoveFromCheckList(agent);
			}

			// Token: 0x06000B94 RID: 2964 RVA: 0x00052264 File Offset: 0x00050464
			private void OnAgentsEnterCheckBox(VolumeBox volumeBox, List<Agent> agentsInVolume)
			{
				foreach (Agent agent in agentsInVolume)
				{
					agent.GetController<TownHorseRaceAgentController>().OnEnterCheckPoint(volumeBox);
				}
			}

			// Token: 0x040004E1 RID: 1249
			private readonly VolumeBox _volumeBox;

			// Token: 0x040004E2 RID: 1250
			private readonly List<GameEntity> _bestTargetList;
		}
	}
}
