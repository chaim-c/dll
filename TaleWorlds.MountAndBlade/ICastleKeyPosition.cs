using System;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000336 RID: 822
	public interface ICastleKeyPosition
	{
		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x06002C4E RID: 11342
		// (set) Token: 0x06002C4F RID: 11343
		IPrimarySiegeWeapon AttackerSiegeWeapon { get; set; }

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x06002C50 RID: 11344
		TacticalPosition MiddlePosition { get; }

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06002C51 RID: 11345
		TacticalPosition WaitPosition { get; }

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06002C52 RID: 11346
		WorldFrame MiddleFrame { get; }

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06002C53 RID: 11347
		WorldFrame DefenseWaitFrame { get; }

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06002C54 RID: 11348
		FormationAI.BehaviorSide DefenseSide { get; }

		// Token: 0x06002C55 RID: 11349
		Vec3 GetPosition();
	}
}
