using System;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200013E RID: 318
	public interface IFormation
	{
		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000F2A RID: 3882
		float Interval { get; }

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000F2B RID: 3883
		float Distance { get; }

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000F2C RID: 3884
		float UnitDiameter { get; }

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000F2D RID: 3885
		float MinimumInterval { get; }

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000F2E RID: 3886
		float MaximumInterval { get; }

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000F2F RID: 3887
		float MinimumDistance { get; }

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000F30 RID: 3888
		float MaximumDistance { get; }

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000F31 RID: 3889
		int? OverridenUnitCount { get; }

		// Token: 0x06000F32 RID: 3890
		bool GetIsLocalPositionAvailable(Vec2 localPosition, Vec2? nearestAvailableUnitPositionLocal);

		// Token: 0x06000F33 RID: 3891
		bool BatchUnitPositions(MBArrayList<Vec2i> orderedPositionIndices, MBArrayList<Vec2> orderedLocalPositions, MBList2D<int> availabilityTable, MBList2D<WorldPosition> globalPositionTable, int fileCount, int rankCount);

		// Token: 0x06000F34 RID: 3892
		IFormationUnit GetClosestUnitTo(Vec2 localPosition, MBList<IFormationUnit> unitsWithSpaces = null, float? maxDistance = null);

		// Token: 0x06000F35 RID: 3893
		IFormationUnit GetClosestUnitTo(IFormationUnit targetUnit, MBList<IFormationUnit> unitsWithSpaces = null, float? maxDistance = null);

		// Token: 0x06000F36 RID: 3894
		void OnUnitAddedOrRemoved();

		// Token: 0x06000F37 RID: 3895
		void SetUnitToFollow(IFormationUnit unit, IFormationUnit toFollow, Vec2 vector);
	}
}
