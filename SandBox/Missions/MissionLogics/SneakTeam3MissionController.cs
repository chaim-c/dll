using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.MissionLogics
{
	// Token: 0x02000067 RID: 103
	public class SneakTeam3MissionController : MissionLogic
	{
		// Token: 0x060003E8 RID: 1000 RVA: 0x0001AFA8 File Offset: 0x000191A8
		public SneakTeam3MissionController()
		{
			this._game = Game.Current;
			this._townRegionProps = new List<List<GameEntity>>();
			this._isScrollObtained = false;
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0001AFD0 File Offset: 0x000191D0
		public override void AfterStart()
		{
			base.AfterStart();
			base.Mission.SetMissionMode(MissionMode.Stealth, true);
			base.Mission.Scene.TimeOfDay = 20.5f;
			this.GetAllProps();
			this.RandomizeScrollPosition();
			GameEntity gameEntity = base.Mission.Scene.FindEntityWithTag("spawnpoint_player");
			MatrixFrame matrixFrame = (gameEntity != null) ? gameEntity.GetGlobalFrame() : MatrixFrame.Identity;
			if (gameEntity != null)
			{
				matrixFrame.rotation.OrthonormalizeAccordingToForwardAndKeepUpAsZAxis();
			}
			Mission mission = base.Mission;
			AgentBuildData agentBuildData = new AgentBuildData(this._game.PlayerTroop).Team(base.Mission.PlayerTeam).InitialPosition(matrixFrame.origin);
			Vec2 vec = matrixFrame.rotation.f.AsVec2;
			vec = vec.Normalized();
			this._playerAgent = mission.SpawnAgent(agentBuildData.InitialDirection(vec).NoHorses(true).Controller(Agent.ControllerType.Player), false);
			this._playerAgent.WieldInitialWeapons(Agent.WeaponWieldActionType.InstantAfterPickUp, Equipment.InitialWeaponEquipPreference.Any);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0001B0CC File Offset: 0x000192CC
		private void GetAllProps()
		{
			for (int i = 0; i < 5; i++)
			{
				List<GameEntity> list = new List<GameEntity>();
				IEnumerable<GameEntity> collection = base.Mission.Scene.FindEntitiesWithTag("patrol_region_" + i);
				list.AddRange(collection);
				this._townRegionProps.Add(list);
			}
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0001B120 File Offset: 0x00019320
		private void RandomizeScrollPosition()
		{
			int num = MBRandom.RandomInt(3);
			GameEntity gameEntity = base.Mission.Scene.FindEntityWithTag("scroll_" + num);
			if (gameEntity != null)
			{
				GameEntity gameEntity2 = base.Mission.Scene.FindEntityWithTag("khuzait_scroll");
				if (gameEntity2 != null)
				{
					MatrixFrame frame = gameEntity.GetFrame();
					frame.origin.z = frame.origin.z + 0.9f;
					gameEntity2.SetFrame(ref frame);
				}
			}
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0001B1A1 File Offset: 0x000193A1
		public override void OnMissionTick(float dt)
		{
			base.OnMissionTick(dt);
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0001B1AA File Offset: 0x000193AA
		private bool IsPlayerDead()
		{
			return base.Mission.MainAgent == null || !base.Mission.MainAgent.IsActive();
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0001B1CE File Offset: 0x000193CE
		public override void OnObjectUsed(Agent userAgent, UsableMissionObject usedObject)
		{
			if (usedObject.GameEntity.HasTag("khuzait_scroll"))
			{
				this._isScrollObtained = true;
			}
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0001B1E9 File Offset: 0x000193E9
		public override bool MissionEnded(ref MissionResult missionResult)
		{
			return this._isScrollObtained || this.IsPlayerDead();
		}

		// Token: 0x040001C4 RID: 452
		private Game _game;

		// Token: 0x040001C5 RID: 453
		private List<List<GameEntity>> _townRegionProps;

		// Token: 0x040001C6 RID: 454
		private Agent _playerAgent;

		// Token: 0x040001C7 RID: 455
		private const string _targetEntityTag = "khuzait_scroll";

		// Token: 0x040001C8 RID: 456
		private bool _isScrollObtained;
	}
}
