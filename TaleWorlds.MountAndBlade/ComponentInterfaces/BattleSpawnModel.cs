using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade.ComponentInterfaces
{
	// Token: 0x020003CA RID: 970
	public abstract class BattleSpawnModel : GameModel
	{
		// Token: 0x06003377 RID: 13175 RVA: 0x000D582A File Offset: 0x000D3A2A
		public virtual void OnMissionStart()
		{
		}

		// Token: 0x06003378 RID: 13176 RVA: 0x000D582C File Offset: 0x000D3A2C
		public virtual void OnMissionEnd()
		{
		}

		// Token: 0x06003379 RID: 13177
		[return: TupleElementNames(new string[]
		{
			"origin",
			"formationIndex"
		})]
		public abstract List<ValueTuple<IAgentOriginBase, int>> GetInitialSpawnAssignments(BattleSideEnum battleSide, List<IAgentOriginBase> troopOrigins);

		// Token: 0x0600337A RID: 13178
		[return: TupleElementNames(new string[]
		{
			"origin",
			"formationIndex"
		})]
		public abstract List<ValueTuple<IAgentOriginBase, int>> GetReinforcementAssignments(BattleSideEnum battleSide, List<IAgentOriginBase> troopOrigins);
	}
}
