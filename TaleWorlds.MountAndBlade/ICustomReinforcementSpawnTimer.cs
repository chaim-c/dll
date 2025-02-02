using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000277 RID: 631
	public interface ICustomReinforcementSpawnTimer
	{
		// Token: 0x0600213B RID: 8507
		bool Check(BattleSideEnum side);

		// Token: 0x0600213C RID: 8508
		void ResetTimer(BattleSideEnum side);
	}
}
