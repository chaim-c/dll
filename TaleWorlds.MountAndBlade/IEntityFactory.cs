using System;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000322 RID: 802
	public interface IEntityFactory
	{
		// Token: 0x06002B20 RID: 11040
		GameEntity MakeEntity(params object[] paramObjects);
	}
}
