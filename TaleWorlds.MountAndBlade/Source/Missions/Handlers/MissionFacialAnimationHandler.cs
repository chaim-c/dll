using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade.Source.Missions.Handlers
{
	// Token: 0x020003BA RID: 954
	public class MissionFacialAnimationHandler : MissionLogic
	{
		// Token: 0x06003300 RID: 13056 RVA: 0x000D416D File Offset: 0x000D236D
		public override void EarlyStart()
		{
			this._animRefreshTimer = new Timer(base.Mission.CurrentTime, 5f, true);
		}

		// Token: 0x06003301 RID: 13057 RVA: 0x000D418B File Offset: 0x000D238B
		public override void AfterStart()
		{
		}

		// Token: 0x06003302 RID: 13058 RVA: 0x000D418D File Offset: 0x000D238D
		public override void OnMissionTick(float dt)
		{
		}

		// Token: 0x06003303 RID: 13059 RVA: 0x000D4190 File Offset: 0x000D2390
		private void SetDefaultFacialAnimationsForAllAgents()
		{
			foreach (Agent agent in base.Mission.Agents)
			{
				if (agent.IsActive() && agent.IsHuman)
				{
					agent.SetAgentFacialAnimation(Agent.FacialAnimChannel.Low, "idle_tired", true);
				}
			}
		}

		// Token: 0x0400161C RID: 5660
		private Timer _animRefreshTimer;
	}
}
