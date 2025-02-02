using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000361 RID: 865
	public interface IOrderable
	{
		// Token: 0x06002F37 RID: 12087
		OrderType GetOrder(BattleSideEnum side);
	}
}
