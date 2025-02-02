using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.MissionLogics
{
	// Token: 0x02000051 RID: 81
	public class IndoorMissionController : MissionLogic
	{
		// Token: 0x0600033D RID: 829 RVA: 0x00014A97 File Offset: 0x00012C97
		public override void OnCreated()
		{
			base.OnCreated();
			base.Mission.DoesMissionRequireCivilianEquipment = true;
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00014AAB File Offset: 0x00012CAB
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			this._missionAgentHandler = base.Mission.GetMissionBehavior<MissionAgentHandler>();
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00014AC4 File Offset: 0x00012CC4
		public override void AfterStart()
		{
			base.AfterStart();
			base.Mission.SetMissionMode(MissionMode.StartUp, true);
			base.Mission.IsInventoryAccessible = !Campaign.Current.IsMainHeroDisguised;
			base.Mission.IsQuestScreenAccessible = true;
			this._missionAgentHandler.SpawnPlayer(base.Mission.DoesMissionRequireCivilianEquipment, true, false, false, false, "");
			this._missionAgentHandler.SpawnLocationCharacters(null);
		}

		// Token: 0x0400018F RID: 399
		private MissionAgentHandler _missionAgentHandler;
	}
}
