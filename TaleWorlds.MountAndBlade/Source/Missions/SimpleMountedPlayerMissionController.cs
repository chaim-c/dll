using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.Source.Missions
{
	// Token: 0x020003B6 RID: 950
	public class SimpleMountedPlayerMissionController : MissionLogic
	{
		// Token: 0x060032D7 RID: 13015 RVA: 0x000D3416 File Offset: 0x000D1616
		public SimpleMountedPlayerMissionController()
		{
			this._game = Game.Current;
		}

		// Token: 0x060032D8 RID: 13016 RVA: 0x000D342C File Offset: 0x000D162C
		public override void AfterStart()
		{
			BasicCharacterObject @object = this._game.ObjectManager.GetObject<BasicCharacterObject>("aserai_tribal_horseman");
			GameEntity gameEntity = Mission.Current.Scene.FindEntityWithTag("sp_play");
			MatrixFrame matrixFrame = (gameEntity != null) ? gameEntity.GetGlobalFrame() : MatrixFrame.Identity;
			AgentBuildData agentBuildData = new AgentBuildData(new BasicBattleAgentOrigin(@object));
			AgentBuildData agentBuildData2 = agentBuildData.InitialPosition(matrixFrame.origin);
			Vec2 vec = matrixFrame.rotation.f.AsVec2;
			vec = vec.Normalized();
			agentBuildData2.InitialDirection(vec).Controller(Agent.ControllerType.Player);
			base.Mission.SpawnAgent(agentBuildData, false).WieldInitialWeapons(Agent.WeaponWieldActionType.InstantAfterPickUp, Equipment.InitialWeaponEquipPreference.Any);
		}

		// Token: 0x060032D9 RID: 13017 RVA: 0x000D34C7 File Offset: 0x000D16C7
		public override bool MissionEnded(ref MissionResult missionResult)
		{
			return base.Mission.InputManager.IsGameKeyPressed(4);
		}

		// Token: 0x0400160B RID: 5643
		private Game _game;
	}
}
