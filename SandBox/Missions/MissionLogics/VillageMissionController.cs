using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.MissionLogics
{
	// Token: 0x02000068 RID: 104
	public class VillageMissionController : MissionLogic
	{
		// Token: 0x060003F0 RID: 1008 RVA: 0x0001B1FB File Offset: 0x000193FB
		public override void OnCreated()
		{
			base.OnCreated();
			base.Mission.DoesMissionRequireCivilianEquipment = false;
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0001B210 File Offset: 0x00019410
		public override void AfterStart()
		{
			base.AfterStart();
			bool isNight = Campaign.Current.IsNight;
			base.Mission.IsInventoryAccessible = true;
			base.Mission.IsQuestScreenAccessible = true;
			MissionAgentHandler missionBehavior = base.Mission.GetMissionBehavior<MissionAgentHandler>();
			missionBehavior.SpawnPlayer(base.Mission.DoesMissionRequireCivilianEquipment, false, false, false, false, "");
			missionBehavior.SpawnLocationCharacters(null);
			MissionAgentHandler.SpawnHorses();
			if (!isNight)
			{
				MissionAgentHandler.SpawnSheeps();
				MissionAgentHandler.SpawnCows();
				MissionAgentHandler.SpawnHogs();
				MissionAgentHandler.SpawnGeese();
				MissionAgentHandler.SpawnChicken();
			}
		}
	}
}
