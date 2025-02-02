using System;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x02000027 RID: 39
	[ApplicationInterfaceBase]
	internal interface IGameEntityComponent
	{
		// Token: 0x06000257 RID: 599
		[EngineMethod("get_entity", false)]
		GameEntity GetEntity(GameEntityComponent entityComponent);

		// Token: 0x06000258 RID: 600
		[EngineMethod("get_first_meta_mesh", false)]
		MetaMesh GetFirstMetaMesh(GameEntityComponent entityComponent);
	}
}
