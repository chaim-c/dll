using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade.ComponentInterfaces;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000200 RID: 512
	public class MultiplayerBattleSpawnModel : BattleSpawnModel
	{
		// Token: 0x06001C60 RID: 7264 RVA: 0x00062F84 File Offset: 0x00061184
		[return: TupleElementNames(new string[]
		{
			"origin",
			"formationIndex"
		})]
		public override List<ValueTuple<IAgentOriginBase, int>> GetInitialSpawnAssignments(BattleSideEnum battleSide, List<IAgentOriginBase> troopOrigins)
		{
			List<ValueTuple<IAgentOriginBase, int>> list = new List<ValueTuple<IAgentOriginBase, int>>();
			foreach (IAgentOriginBase agentOriginBase in troopOrigins)
			{
				ValueTuple<IAgentOriginBase, int> item = new ValueTuple<IAgentOriginBase, int>(agentOriginBase, (int)Mission.Current.GetAgentTroopClass(battleSide, agentOriginBase.Troop));
				list.Add(item);
			}
			return list;
		}

		// Token: 0x06001C61 RID: 7265 RVA: 0x00062FF4 File Offset: 0x000611F4
		[return: TupleElementNames(new string[]
		{
			"origin",
			"formationIndex"
		})]
		public override List<ValueTuple<IAgentOriginBase, int>> GetReinforcementAssignments(BattleSideEnum battleSide, List<IAgentOriginBase> troopOrigins)
		{
			List<ValueTuple<IAgentOriginBase, int>> list = new List<ValueTuple<IAgentOriginBase, int>>();
			foreach (IAgentOriginBase agentOriginBase in troopOrigins)
			{
				ValueTuple<IAgentOriginBase, int> item = new ValueTuple<IAgentOriginBase, int>(agentOriginBase, (int)Mission.Current.GetAgentTroopClass(battleSide, agentOriginBase.Troop));
				list.Add(item);
			}
			return list;
		}
	}
}
