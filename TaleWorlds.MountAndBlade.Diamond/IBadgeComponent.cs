using System;
using System.Collections.Generic;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000122 RID: 290
	public interface IBadgeComponent
	{
		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000674 RID: 1652
		Dictionary<ValueTuple<PlayerId, string, string>, int> DataDictionary { get; }

		// Token: 0x06000675 RID: 1653
		void OnPlayerJoin(PlayerData playerData);

		// Token: 0x06000676 RID: 1654
		void OnStartingNextBattle();
	}
}
