using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000243 RID: 579
	public class IncrementalTimer
	{
		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06001F5A RID: 8026 RVA: 0x0006F0C2 File Offset: 0x0006D2C2
		// (set) Token: 0x06001F5B RID: 8027 RVA: 0x0006F0CA File Offset: 0x0006D2CA
		public float TimerCounter { get; private set; }

		// Token: 0x06001F5C RID: 8028 RVA: 0x0006F0D4 File Offset: 0x0006D2D4
		public IncrementalTimer(float totalDuration, float tickInterval)
		{
			this._tickInterval = MathF.Max(tickInterval, 0.01f);
			this._totalDuration = MathF.Max(totalDuration, 0.01f);
			this.TimerCounter = 0f;
			this._timer = new Timer(MBCommon.GetTotalMissionTime(), this._tickInterval, true);
		}

		// Token: 0x06001F5D RID: 8029 RVA: 0x0006F12B File Offset: 0x0006D32B
		public bool Check()
		{
			if (this._timer.Check(MBCommon.GetTotalMissionTime()))
			{
				this.TimerCounter += this._tickInterval / this._totalDuration;
				return true;
			}
			return false;
		}

		// Token: 0x06001F5E RID: 8030 RVA: 0x0006F15C File Offset: 0x0006D35C
		public bool HasEnded()
		{
			return this.TimerCounter >= 1f;
		}

		// Token: 0x04000B8C RID: 2956
		private readonly float _totalDuration;

		// Token: 0x04000B8D RID: 2957
		private readonly float _tickInterval;

		// Token: 0x04000B8E RID: 2958
		private readonly Timer _timer;
	}
}
