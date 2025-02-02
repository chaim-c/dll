using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200035B RID: 859
	public class ClearHandInverseKinematicsOnStopUsageComponent : UsableMissionObjectComponent
	{
		// Token: 0x06002F26 RID: 12070 RVA: 0x000C122B File Offset: 0x000BF42B
		protected internal override void OnUseStopped(Agent userAgent, bool isSuccessful = true)
		{
			userAgent.ClearHandInverseKinematics();
		}
	}
}
