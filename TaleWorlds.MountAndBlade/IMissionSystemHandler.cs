using System;
using System.Collections.Generic;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200023C RID: 572
	public interface IMissionSystemHandler
	{
		// Token: 0x06001F2B RID: 7979
		void OnMissionAfterStarting(Mission mission);

		// Token: 0x06001F2C RID: 7980
		void OnMissionLoadingFinished(Mission mission);

		// Token: 0x06001F2D RID: 7981
		void BeforeMissionTick(Mission mission, float realDt);

		// Token: 0x06001F2E RID: 7982
		void AfterMissionTick(Mission mission, float realDt);

		// Token: 0x06001F2F RID: 7983
		void UpdateCamera(Mission mission, float realDt);

		// Token: 0x06001F30 RID: 7984
		bool RenderIsReady();

		// Token: 0x06001F31 RID: 7985
		IEnumerable<MissionBehavior> OnAddBehaviors(IEnumerable<MissionBehavior> behaviors, Mission mission, string missionName, bool addDefaultMissionBehaviors);
	}
}
