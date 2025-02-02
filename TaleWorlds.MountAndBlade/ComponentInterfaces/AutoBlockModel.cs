using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade.ComponentInterfaces
{
	// Token: 0x020003CE RID: 974
	public abstract class AutoBlockModel : GameModel
	{
		// Token: 0x06003395 RID: 13205
		public abstract Agent.UsageDirection GetBlockDirection(Mission mission);
	}
}
