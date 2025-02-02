using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.AgentOrigins;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.MissionLogics
{
	// Token: 0x0200005E RID: 94
	public class RetirementMissionLogic : MissionLogic
	{
		// Token: 0x060003CF RID: 975 RVA: 0x0001A864 File Offset: 0x00018A64
		public override void AfterStart()
		{
			base.AfterStart();
			this.SpawnHermit();
			((LeaveMissionLogic)base.Mission.MissionLogics.FirstOrDefault((MissionLogic x) => x is LeaveMissionLogic)).UnconsciousGameMenuID = "retirement_after_player_knockedout";
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0001A8BC File Offset: 0x00018ABC
		private void SpawnHermit()
		{
			List<GameEntity> list = base.Mission.Scene.FindEntitiesWithTag("sp_hermit").ToList<GameEntity>();
			MatrixFrame globalFrame = list[MBRandom.RandomInt(list.Count<GameEntity>())].GetGlobalFrame();
			CharacterObject @object = Campaign.Current.ObjectManager.GetObject<CharacterObject>("sp_hermit");
			AgentBuildData agentBuildData = new AgentBuildData(@object).TroopOrigin(new SimpleAgentOrigin(@object, -1, null, default(UniqueTroopDescriptor))).Team(base.Mission.SpectatorTeam).InitialPosition(globalFrame.origin);
			Vec2 vec = globalFrame.rotation.f.AsVec2;
			vec = vec.Normalized();
			AgentBuildData agentBuildData2 = agentBuildData.InitialDirection(vec).CivilianEquipment(true).NoHorses(true).NoWeapons(true).ClothingColor1(base.Mission.PlayerTeam.Color).ClothingColor2(base.Mission.PlayerTeam.Color2);
			base.Mission.SpawnAgent(agentBuildData2, false).SetMortalityState(Agent.MortalityState.Invulnerable);
		}
	}
}
