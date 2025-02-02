using System;
using System.Diagnostics;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000282 RID: 642
	public class RecordMissionLogic : MissionLogic
	{
		// Token: 0x060021C3 RID: 8643 RVA: 0x0007B096 File Offset: 0x00079296
		public override void OnBehaviorInitialize()
		{
			base.Mission.Recorder.StartRecording();
		}

		// Token: 0x060021C4 RID: 8644 RVA: 0x0007B0A8 File Offset: 0x000792A8
		public override void OnMissionTick(float dt)
		{
			base.OnMissionTick(dt);
			if (this._lastRecordedTime + 0.02f < base.Mission.CurrentTime)
			{
				this._lastRecordedTime = base.Mission.CurrentTime;
				base.Mission.Recorder.RecordCurrentState();
			}
		}

		// Token: 0x060021C5 RID: 8645 RVA: 0x0007B0F8 File Offset: 0x000792F8
		public override void OnEndMissionInternal()
		{
			base.OnEndMissionInternal();
			base.Mission.Recorder.BackupRecordToFile("Mission_record_" + string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt}_", DateTime.Now) + Process.GetCurrentProcess().Id, Game.Current.GameType.GetType().Name, base.Mission.SceneLevels);
			GameNetwork.ResetMissionData();
		}

		// Token: 0x04000C95 RID: 3221
		private float _lastRecordedTime = -1f;
	}
}
