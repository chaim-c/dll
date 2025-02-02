using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000210 RID: 528
	public interface IMissionDeploymentPlan
	{
		// Token: 0x06001D1D RID: 7453
		bool IsPlanMadeForBattleSide(BattleSideEnum side, DeploymentPlanType planType);

		// Token: 0x06001D1E RID: 7454
		bool IsPositionInsideDeploymentBoundaries(BattleSideEnum battleSide, in Vec2 position);

		// Token: 0x06001D1F RID: 7455
		bool HasDeploymentBoundaries(BattleSideEnum side);

		// Token: 0x06001D20 RID: 7456
		[return: TupleElementNames(new string[]
		{
			"id",
			"points"
		})]
		MBReadOnlyList<ValueTuple<string, List<Vec2>>> GetDeploymentBoundaries(BattleSideEnum side);

		// Token: 0x06001D21 RID: 7457
		Vec2 GetClosestDeploymentBoundaryPosition(BattleSideEnum battleSide, in Vec2 position, bool withNavMesh = false, float positionZ = 0f);

		// Token: 0x06001D22 RID: 7458
		int GetTroopCountForSide(BattleSideEnum side, DeploymentPlanType planType);

		// Token: 0x06001D23 RID: 7459
		Vec3 GetMeanPositionOfPlan(BattleSideEnum battleSide, DeploymentPlanType planType);

		// Token: 0x06001D24 RID: 7460
		MatrixFrame GetBattleSideDeploymentFrame(BattleSideEnum side);

		// Token: 0x06001D25 RID: 7461
		IFormationDeploymentPlan GetFormationPlan(BattleSideEnum side, FormationClass fClass, DeploymentPlanType planType);
	}
}
