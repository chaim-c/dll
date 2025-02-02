using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001E1 RID: 481
	public class BasicMissionTimer
	{
		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001AEB RID: 6891 RVA: 0x0005D4FF File Offset: 0x0005B6FF
		public float ElapsedTime
		{
			get
			{
				return MBCommon.GetTotalMissionTime() - this._startTime;
			}
		}

		// Token: 0x06001AEC RID: 6892 RVA: 0x0005D50D File Offset: 0x0005B70D
		public BasicMissionTimer()
		{
			this._startTime = MBCommon.GetTotalMissionTime();
		}

		// Token: 0x06001AED RID: 6893 RVA: 0x0005D520 File Offset: 0x0005B720
		public void Reset()
		{
			this._startTime = MBCommon.GetTotalMissionTime();
		}

		// Token: 0x04000861 RID: 2145
		private float _startTime;
	}
}
