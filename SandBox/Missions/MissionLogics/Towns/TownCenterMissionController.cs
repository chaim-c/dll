using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.MissionLogics.Towns
{
	// Token: 0x0200006D RID: 109
	public class TownCenterMissionController : MissionLogic
	{
		// Token: 0x0600042C RID: 1068 RVA: 0x0001C7DA File Offset: 0x0001A9DA
		public override void OnCreated()
		{
			base.OnCreated();
			base.Mission.DoesMissionRequireCivilianEquipment = true;
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0001C7F0 File Offset: 0x0001A9F0
		public override void AfterStart()
		{
			bool isNight = Campaign.Current.IsNight;
			base.Mission.SetMissionMode(MissionMode.StartUp, true);
			base.Mission.IsInventoryAccessible = !Campaign.Current.IsMainHeroDisguised;
			base.Mission.IsQuestScreenAccessible = true;
			MissionAgentHandler missionBehavior = base.Mission.GetMissionBehavior<MissionAgentHandler>();
			missionBehavior.SpawnPlayer(base.Mission.DoesMissionRequireCivilianEquipment, true, false, false, false, "");
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
