using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.MissionLogics
{
	// Token: 0x02000050 RID: 80
	public class HouseMissionController : MissionLogic
	{
		// Token: 0x06000339 RID: 825 RVA: 0x000149F7 File Offset: 0x00012BF7
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			this._missionAgentHandler = base.Mission.GetMissionBehavior<MissionAgentHandler>();
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00014A10 File Offset: 0x00012C10
		public override void OnCreated()
		{
			base.OnCreated();
			base.Mission.DoesMissionRequireCivilianEquipment = true;
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00014A24 File Offset: 0x00012C24
		public override void EarlyStart()
		{
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00014A28 File Offset: 0x00012C28
		public override void AfterStart()
		{
			base.AfterStart();
			base.Mission.SetMissionMode(MissionMode.StartUp, true);
			base.Mission.IsInventoryAccessible = !Campaign.Current.IsMainHeroDisguised;
			base.Mission.IsQuestScreenAccessible = true;
			this._missionAgentHandler.SpawnPlayer(base.Mission.DoesMissionRequireCivilianEquipment, true, true, false, false, "");
			this._missionAgentHandler.SpawnLocationCharacters(null);
		}

		// Token: 0x0400018E RID: 398
		private MissionAgentHandler _missionAgentHandler;
	}
}
