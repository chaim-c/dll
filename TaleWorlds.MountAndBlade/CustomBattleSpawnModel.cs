using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade.ComponentInterfaces;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001F3 RID: 499
	public class CustomBattleSpawnModel : BattleSpawnModel
	{
		// Token: 0x06001BFE RID: 7166 RVA: 0x00060F48 File Offset: 0x0005F148
		public override void OnMissionStart()
		{
			MissionReinforcementsHelper.OnMissionStart();
		}

		// Token: 0x06001BFF RID: 7167 RVA: 0x00060F4F File Offset: 0x0005F14F
		public override void OnMissionEnd()
		{
			MissionReinforcementsHelper.OnMissionEnd();
		}

		// Token: 0x06001C00 RID: 7168 RVA: 0x00060F58 File Offset: 0x0005F158
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

		// Token: 0x06001C01 RID: 7169 RVA: 0x00060FC8 File Offset: 0x0005F1C8
		[return: TupleElementNames(new string[]
		{
			"origin",
			"formationIndex"
		})]
		public override List<ValueTuple<IAgentOriginBase, int>> GetReinforcementAssignments(BattleSideEnum battleSide, List<IAgentOriginBase> troopOrigins)
		{
			return MissionReinforcementsHelper.GetReinforcementAssignments(battleSide, troopOrigins);
		}
	}
}
