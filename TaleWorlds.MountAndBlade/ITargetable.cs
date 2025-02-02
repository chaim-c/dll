using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000363 RID: 867
	public interface ITargetable
	{
		// Token: 0x06002F39 RID: 12089
		TargetFlags GetTargetFlags();

		// Token: 0x06002F3A RID: 12090
		float GetTargetValue(List<Vec3> referencePositions);

		// Token: 0x06002F3B RID: 12091
		GameEntity GetTargetEntity();

		// Token: 0x06002F3C RID: 12092
		Vec3 GetTargetingOffset();

		// Token: 0x06002F3D RID: 12093
		BattleSideEnum GetSide();

		// Token: 0x06002F3E RID: 12094
		GameEntity Entity();
	}
}
