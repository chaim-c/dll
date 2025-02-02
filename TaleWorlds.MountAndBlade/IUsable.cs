using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000365 RID: 869
	public interface IUsable
	{
		// Token: 0x06002F3F RID: 12095
		void OnUse(Agent userAgent);

		// Token: 0x06002F40 RID: 12096
		void OnUseStopped(Agent userAgent, bool isSuccessful, int preferenceIndex);
	}
}
