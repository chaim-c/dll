using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000107 RID: 263
	public class VictoryComponent : AgentComponent
	{
		// Token: 0x06000CED RID: 3309 RVA: 0x000188D9 File Offset: 0x00016AD9
		public VictoryComponent(Agent agent, RandomTimer timer) : base(agent)
		{
			this._timer = timer;
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x000188E9 File Offset: 0x00016AE9
		public bool CheckTimer()
		{
			return this._timer.Check(Mission.Current.CurrentTime);
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x00018900 File Offset: 0x00016B00
		public void ChangeTimerDuration(float min, float max)
		{
			this._timer.ChangeDuration(min, max);
		}

		// Token: 0x040002EC RID: 748
		private readonly RandomTimer _timer;
	}
}
