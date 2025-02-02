using System;
using System.Collections.Generic;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000152 RID: 338
	public interface IPointDefendable
	{
		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x0600111B RID: 4379
		IEnumerable<DefencePoint> DefencePoints { get; }

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x0600111C RID: 4380
		FormationAI.BehaviorSide DefenseSide { get; }

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x0600111D RID: 4381
		WorldFrame MiddleFrame { get; }

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x0600111E RID: 4382
		WorldFrame DefenseWaitFrame { get; }
	}
}
