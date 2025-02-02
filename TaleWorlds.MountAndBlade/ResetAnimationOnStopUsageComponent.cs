using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000366 RID: 870
	public class ResetAnimationOnStopUsageComponent : UsableMissionObjectComponent
	{
		// Token: 0x06002F41 RID: 12097 RVA: 0x000C13DA File Offset: 0x000BF5DA
		public ResetAnimationOnStopUsageComponent(ActionIndexCache successfulResetActionCode)
		{
			this._successfulResetAction = successfulResetActionCode;
		}

		// Token: 0x06002F42 RID: 12098 RVA: 0x000C13F4 File Offset: 0x000BF5F4
		protected internal override void OnUseStopped(Agent userAgent, bool isSuccessful = true)
		{
			ActionIndexCache actionIndexCache = isSuccessful ? this._successfulResetAction : ActionIndexCache.act_none;
			if (actionIndexCache == ActionIndexCache.act_none)
			{
				userAgent.SetActionChannel(1, actionIndexCache, false, 72UL, 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true);
			}
			userAgent.SetActionChannel(0, actionIndexCache, false, 72UL, 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true);
		}

		// Token: 0x040013FF RID: 5119
		private readonly ActionIndexCache _successfulResetAction = ActionIndexCache.act_none;
	}
}
