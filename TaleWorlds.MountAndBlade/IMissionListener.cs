using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000257 RID: 599
	public interface IMissionListener
	{
		// Token: 0x06002001 RID: 8193
		void OnEquipItemsFromSpawnEquipmentBegin(Agent agent, Agent.CreationType creationType);

		// Token: 0x06002002 RID: 8194
		void OnEquipItemsFromSpawnEquipment(Agent agent, Agent.CreationType creationType);

		// Token: 0x06002003 RID: 8195
		void OnEndMission();

		// Token: 0x06002004 RID: 8196
		void OnMissionModeChange(MissionMode oldMissionMode, bool atStart);

		// Token: 0x06002005 RID: 8197
		void OnConversationCharacterChanged();

		// Token: 0x06002006 RID: 8198
		void OnResetMission();

		// Token: 0x06002007 RID: 8199
		void OnInitialDeploymentPlanMade(BattleSideEnum battleSide, bool isFirstPlan);
	}
}
