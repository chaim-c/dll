using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000349 RID: 841
	public class StandingPointWithTeamLimit : StandingPoint
	{
		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x06002E14 RID: 11796 RVA: 0x000BBC0A File Offset: 0x000B9E0A
		// (set) Token: 0x06002E15 RID: 11797 RVA: 0x000BBC12 File Offset: 0x000B9E12
		public Team UsableTeam { get; set; }

		// Token: 0x06002E16 RID: 11798 RVA: 0x000BBC1B File Offset: 0x000B9E1B
		public override bool IsDisabledForAgent(Agent agent)
		{
			return agent.Team != this.UsableTeam || base.IsDisabledForAgent(agent);
		}

		// Token: 0x06002E17 RID: 11799 RVA: 0x000BBC34 File Offset: 0x000B9E34
		protected internal override bool IsUsableBySide(BattleSideEnum side)
		{
			return side == this.UsableTeam.Side && base.IsUsableBySide(side);
		}
	}
}
