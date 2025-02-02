using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002C8 RID: 712
	public interface IEditorMissionTester
	{
		// Token: 0x060027A0 RID: 10144
		void StartMissionForEditor(string missionName, string sceneName, string levels);

		// Token: 0x060027A1 RID: 10145
		void StartMissionForReplayEditor(string missionName, string sceneName, string levels, string fileName, bool record, float startTime, float endTime);
	}
}
