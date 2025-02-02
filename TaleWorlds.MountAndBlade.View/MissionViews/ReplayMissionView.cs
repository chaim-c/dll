using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.View.MissionViews
{
	// Token: 0x02000059 RID: 89
	public class ReplayMissionView : MissionView
	{
		// Token: 0x060003E5 RID: 997 RVA: 0x00021533 File Offset: 0x0001F733
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			this._resetTime = 0f;
			this._replayMissionLogic = base.Mission.GetMissionBehavior<ReplayMissionLogic>();
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00021558 File Offset: 0x0001F758
		public override void OnPreMissionTick(float dt)
		{
			base.OnPreMissionTick(dt);
			base.Mission.Recorder.ProcessRecordUntilTime(base.Mission.CurrentTime - this._resetTime);
			bool isInputOverridden = this._isInputOverridden;
			if (base.Mission.CurrentState == Mission.State.Continuing && base.Mission.Recorder.IsEndOfRecord())
			{
				if (MBEditor._isEditorMissionOn)
				{
					MBEditor.LeaveEditMissionMode();
					return;
				}
				base.Mission.EndMission();
			}
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x000215CD File Offset: 0x0001F7CD
		public void OverrideInput(bool isOverridden)
		{
			this._isInputOverridden = isOverridden;
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x000215D8 File Offset: 0x0001F7D8
		public void ResetReplay()
		{
			this._resetTime = base.Mission.CurrentTime;
			base.Mission.ResetMission();
			base.Mission.Teams.Clear();
			base.Mission.Recorder.RestartRecord();
			MBCommon.UnPauseGameEngine();
			base.Mission.Scene.TimeSpeed = 1f;
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0002163C File Offset: 0x0001F83C
		public void Rewind(float time)
		{
			this._resetTime = MathF.Min(this._resetTime + time, base.Mission.CurrentTime);
			base.Mission.ResetMission();
			base.Mission.Teams.Clear();
			base.Mission.Recorder.RestartRecord();
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00021692 File Offset: 0x0001F892
		public void FastForward(float time)
		{
			this._resetTime -= time;
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x000216A2 File Offset: 0x0001F8A2
		public void Pause()
		{
			if (!MBCommon.IsPaused && base.Mission.Scene.TimeSpeed.ApproximatelyEqualsTo(1f, 1E-05f))
			{
				MBCommon.PauseGameEngine();
			}
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x000216D4 File Offset: 0x0001F8D4
		public void Resume()
		{
			if (MBCommon.IsPaused || !base.Mission.Scene.TimeSpeed.ApproximatelyEqualsTo(1f, 1E-05f))
			{
				MBCommon.UnPauseGameEngine();
				base.Mission.Scene.TimeSpeed = 1f;
			}
		}

		// Token: 0x04000296 RID: 662
		private float _resetTime;

		// Token: 0x04000297 RID: 663
		private bool _isInputOverridden;

		// Token: 0x04000298 RID: 664
		private ReplayMissionLogic _replayMissionLogic;
	}
}
