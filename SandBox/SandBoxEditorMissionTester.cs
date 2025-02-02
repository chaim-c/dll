using System;
using TaleWorlds.MountAndBlade;

namespace SandBox
{
	// Token: 0x02000024 RID: 36
	internal class SandBoxEditorMissionTester : IEditorMissionTester
	{
		// Token: 0x060000F7 RID: 247 RVA: 0x00006E1F File Offset: 0x0000501F
		void IEditorMissionTester.StartMissionForEditor(string missionName, string sceneName, string levels)
		{
			MBGameManager.StartNewGame(new EditorSceneMissionManager(missionName, sceneName, levels, false, "", false, 0f, 0f));
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00006E3F File Offset: 0x0000503F
		void IEditorMissionTester.StartMissionForReplayEditor(string missionName, string sceneName, string levels, string fileName, bool record, float startTime, float endTime)
		{
			MBGameManager.StartNewGame(new EditorSceneMissionManager(missionName, sceneName, levels, true, fileName, record, startTime, endTime));
		}
	}
}
