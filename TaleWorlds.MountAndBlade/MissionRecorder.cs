using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200028C RID: 652
	public class MissionRecorder
	{
		// Token: 0x06002228 RID: 8744 RVA: 0x0007CB54 File Offset: 0x0007AD54
		public MissionRecorder(Mission mission)
		{
			this._mission = mission;
		}

		// Token: 0x06002229 RID: 8745 RVA: 0x0007CB63 File Offset: 0x0007AD63
		public void RestartRecord()
		{
			MBAPI.IMBMission.RestartRecord(this._mission.Pointer);
		}

		// Token: 0x0600222A RID: 8746 RVA: 0x0007CB7A File Offset: 0x0007AD7A
		public void ProcessRecordUntilTime(float time)
		{
			MBAPI.IMBMission.ProcessRecordUntilTime(this._mission.Pointer, time);
		}

		// Token: 0x0600222B RID: 8747 RVA: 0x0007CB92 File Offset: 0x0007AD92
		public bool IsEndOfRecord()
		{
			return MBAPI.IMBMission.EndOfRecord(this._mission.Pointer);
		}

		// Token: 0x0600222C RID: 8748 RVA: 0x0007CBA9 File Offset: 0x0007ADA9
		public void StartRecording()
		{
			MBAPI.IMBMission.StartRecording();
		}

		// Token: 0x0600222D RID: 8749 RVA: 0x0007CBB5 File Offset: 0x0007ADB5
		public void RecordCurrentState()
		{
			MBAPI.IMBMission.RecordCurrentState(this._mission.Pointer);
		}

		// Token: 0x0600222E RID: 8750 RVA: 0x0007CBCC File Offset: 0x0007ADCC
		public void BackupRecordToFile(string fileName, string gameType, string sceneLevels)
		{
			MBAPI.IMBMission.BackupRecordToFile(this._mission.Pointer, fileName, gameType, sceneLevels);
		}

		// Token: 0x0600222F RID: 8751 RVA: 0x0007CBE6 File Offset: 0x0007ADE6
		public void RestoreRecordFromFile(string fileName)
		{
			MBAPI.IMBMission.RestoreRecordFromFile(this._mission.Pointer, fileName);
		}

		// Token: 0x06002230 RID: 8752 RVA: 0x0007CBFE File Offset: 0x0007ADFE
		public void ClearRecordBuffers()
		{
			MBAPI.IMBMission.ClearRecordBuffers(this._mission.Pointer);
		}

		// Token: 0x06002231 RID: 8753 RVA: 0x0007CC15 File Offset: 0x0007AE15
		public static string GetSceneNameForReplay(PlatformFilePath fileName)
		{
			return MBAPI.IMBMission.GetSceneNameForReplay(fileName);
		}

		// Token: 0x06002232 RID: 8754 RVA: 0x0007CC22 File Offset: 0x0007AE22
		public static string GetGameTypeForReplay(PlatformFilePath fileName)
		{
			return MBAPI.IMBMission.GetGameTypeForReplay(fileName);
		}

		// Token: 0x06002233 RID: 8755 RVA: 0x0007CC2F File Offset: 0x0007AE2F
		public static string GetSceneLevelsForReplay(PlatformFilePath fileName)
		{
			return MBAPI.IMBMission.GetSceneLevelsForReplay(fileName);
		}

		// Token: 0x06002234 RID: 8756 RVA: 0x0007CC3C File Offset: 0x0007AE3C
		public static string GetAtmosphereNameForReplay(PlatformFilePath fileName)
		{
			return MBAPI.IMBMission.GetAtmosphereNameForReplay(fileName);
		}

		// Token: 0x06002235 RID: 8757 RVA: 0x0007CC49 File Offset: 0x0007AE49
		public static int GetAtmosphereSeasonForReplay(PlatformFilePath fileName)
		{
			return MBAPI.IMBMission.GetAtmosphereSeasonForReplay(fileName);
		}

		// Token: 0x04000CC6 RID: 3270
		private readonly Mission _mission;
	}
}
