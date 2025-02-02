using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200025A RID: 602
	public interface IFlagRemoved : IMissionBehavior
	{
		// Token: 0x06002009 RID: 8201
		void OnFlagsRemoved(int remainingFlagIndex);
	}
}
