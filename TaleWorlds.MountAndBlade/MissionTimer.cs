using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200028F RID: 655
	public class MissionTimer
	{
		// Token: 0x0600225F RID: 8799 RVA: 0x0007D2CD File Offset: 0x0007B4CD
		private MissionTimer()
		{
		}

		// Token: 0x06002260 RID: 8800 RVA: 0x0007D2D5 File Offset: 0x0007B4D5
		public MissionTimer(float duration)
		{
			this._startTime = MissionTime.Now;
			this._duration = duration;
		}

		// Token: 0x06002261 RID: 8801 RVA: 0x0007D2EF File Offset: 0x0007B4EF
		public MissionTime GetStartTime()
		{
			return this._startTime;
		}

		// Token: 0x06002262 RID: 8802 RVA: 0x0007D2F7 File Offset: 0x0007B4F7
		public float GetTimerDuration()
		{
			return this._duration;
		}

		// Token: 0x06002263 RID: 8803 RVA: 0x0007D300 File Offset: 0x0007B500
		public float GetRemainingTimeInSeconds(bool synched = false)
		{
			if (this._duration < 0f)
			{
				return 0f;
			}
			float num = this._duration - this._startTime.ElapsedSeconds;
			if (synched && GameNetwork.IsClientOrReplay)
			{
				num -= Mission.Current.MissionTimeTracker.GetLastSyncDifference();
			}
			if (num <= 0f)
			{
				return 0f;
			}
			return num;
		}

		// Token: 0x06002264 RID: 8804 RVA: 0x0007D35E File Offset: 0x0007B55E
		public bool Check(bool reset = false)
		{
			bool flag = this.GetRemainingTimeInSeconds(false) <= 0f;
			if (flag && reset)
			{
				this._startTime = MissionTime.Now;
			}
			return flag;
		}

		// Token: 0x06002265 RID: 8805 RVA: 0x0007D381 File Offset: 0x0007B581
		public static MissionTimer CreateSynchedTimerClient(float startTimeInSeconds, float duration)
		{
			return new MissionTimer
			{
				_startTime = new MissionTime((long)(startTimeInSeconds * 10000000f)),
				_duration = duration
			};
		}

		// Token: 0x04000CD1 RID: 3281
		private MissionTime _startTime;

		// Token: 0x04000CD2 RID: 3282
		private float _duration;
	}
}
