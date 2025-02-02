using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002CA RID: 714
	public static class ModuleExtensions
	{
		// Token: 0x060027A3 RID: 10147 RVA: 0x000989FC File Offset: 0x00096BFC
		public static IEnumerable<UsableMachine> GetUsedMachines(this Formation formation)
		{
			return from d in formation.Detachments
			select d as UsableMachine into u
			where u != null
			select u;
		}

		// Token: 0x060027A4 RID: 10148 RVA: 0x00098A57 File Offset: 0x00096C57
		public static void StartUsingMachine(this Formation formation, UsableMachine usable, bool isPlayerOrder = false)
		{
			if (isPlayerOrder || (formation.IsAIControlled && !Mission.Current.IsMissionEnding))
			{
				formation.JoinDetachment(usable);
			}
		}

		// Token: 0x060027A5 RID: 10149 RVA: 0x00098A77 File Offset: 0x00096C77
		public static void StopUsingMachine(this Formation formation, UsableMachine usable, bool isPlayerOrder = false)
		{
			if (isPlayerOrder || formation.IsAIControlled)
			{
				formation.LeaveDetachment(usable);
			}
		}

		// Token: 0x060027A6 RID: 10150 RVA: 0x00098A8B File Offset: 0x00096C8B
		public static WorldPosition ToWorldPosition(this Vec3 rawPosition)
		{
			return new WorldPosition(Mission.Current.Scene, rawPosition);
		}
	}
}
