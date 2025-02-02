using System;
using System.Collections.Generic;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200013F RID: 319
	public interface IFormationArrangement
	{
		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000F38 RID: 3896
		// (set) Token: 0x06000F39 RID: 3897
		float Width { get; set; }

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000F3A RID: 3898
		float Depth { get; }

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000F3B RID: 3899
		// (set) Token: 0x06000F3C RID: 3900
		float FlankWidth { get; set; }

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000F3D RID: 3901
		float RankDepth { get; }

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000F3E RID: 3902
		float MinimumWidth { get; }

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000F3F RID: 3903
		float MaximumWidth { get; }

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000F40 RID: 3904
		float MinimumFlankWidth { get; }

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000F41 RID: 3905
		bool? IsLoose { get; }

		// Token: 0x06000F42 RID: 3906
		IFormationUnit GetPlayerUnit();

		// Token: 0x06000F43 RID: 3907
		MBReadOnlyList<IFormationUnit> GetAllUnits();

		// Token: 0x06000F44 RID: 3908
		MBList<IFormationUnit> GetUnpositionedUnits();

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000F45 RID: 3909
		int UnitCount { get; }

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000F46 RID: 3910
		int RankCount { get; }

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000F47 RID: 3911
		int PositionedUnitCount { get; }

		// Token: 0x06000F48 RID: 3912
		bool AddUnit(IFormationUnit unit);

		// Token: 0x06000F49 RID: 3913
		void RemoveUnit(IFormationUnit unit);

		// Token: 0x06000F4A RID: 3914
		IFormationUnit GetUnit(int fileIndex, int rankIndex);

		// Token: 0x06000F4B RID: 3915
		void OnBatchRemoveStart();

		// Token: 0x06000F4C RID: 3916
		void OnBatchRemoveEnd();

		// Token: 0x06000F4D RID: 3917
		Vec2? GetLocalPositionOfUnitOrDefault(int unitIndex);

		// Token: 0x06000F4E RID: 3918
		Vec2? GetLocalPositionOfUnitOrDefault(IFormationUnit unit);

		// Token: 0x06000F4F RID: 3919
		Vec2? GetLocalPositionOfUnitOrDefaultWithAdjustment(IFormationUnit unit, float distanceBetweenAgentsAdjustment);

		// Token: 0x06000F50 RID: 3920
		Vec2? GetLocalDirectionOfUnitOrDefault(int unitIndex);

		// Token: 0x06000F51 RID: 3921
		Vec2? GetLocalDirectionOfUnitOrDefault(IFormationUnit unit);

		// Token: 0x06000F52 RID: 3922
		WorldPosition? GetWorldPositionOfUnitOrDefault(int unitIndex);

		// Token: 0x06000F53 RID: 3923
		WorldPosition? GetWorldPositionOfUnitOrDefault(IFormationUnit unit);

		// Token: 0x06000F54 RID: 3924
		List<IFormationUnit> GetUnitsToPop(int count);

		// Token: 0x06000F55 RID: 3925
		List<IFormationUnit> GetUnitsToPop(int count, Vec3 targetPosition);

		// Token: 0x06000F56 RID: 3926
		IEnumerable<IFormationUnit> GetUnitsToPopWithCondition(int count, Func<IFormationUnit, bool> conditionFunction);

		// Token: 0x06000F57 RID: 3927
		void SwitchUnitLocations(IFormationUnit firstUnit, IFormationUnit secondUnit);

		// Token: 0x06000F58 RID: 3928
		void SwitchUnitLocationsWithUnpositionedUnit(IFormationUnit firstUnit, IFormationUnit secondUnit);

		// Token: 0x06000F59 RID: 3929
		void SwitchUnitLocationsWithBackMostUnit(IFormationUnit unit);

		// Token: 0x06000F5A RID: 3930
		IFormationUnit GetNeighborUnitOfLeftSide(IFormationUnit unit);

		// Token: 0x06000F5B RID: 3931
		IFormationUnit GetNeighborUnitOfRightSide(IFormationUnit unit);

		// Token: 0x06000F5C RID: 3932
		Vec2? GetLocalWallDirectionOfRelativeFormationLocation(IFormationUnit unit);

		// Token: 0x06000F5D RID: 3933
		IEnumerable<Vec2> GetUnavailableUnitPositions();

		// Token: 0x06000F5E RID: 3934
		float GetOccupationWidth(int unitCount);

		// Token: 0x06000F5F RID: 3935
		Vec2? CreateNewPosition(int unitIndex);

		// Token: 0x06000F60 RID: 3936
		void BeforeFormationFrameChange();

		// Token: 0x06000F61 RID: 3937
		void OnFormationFrameChanged();

		// Token: 0x06000F62 RID: 3938
		bool IsTurnBackwardsNecessary(Vec2 previousPosition, WorldPosition? newPosition, Vec2 previousDirection, bool hasNewDirection, Vec2? newDirection);

		// Token: 0x06000F63 RID: 3939
		void TurnBackwards();

		// Token: 0x06000F64 RID: 3940
		void OnFormationDispersed();

		// Token: 0x06000F65 RID: 3941
		void Reset();

		// Token: 0x06000F66 RID: 3942
		IFormationArrangement Clone(IFormation formation);

		// Token: 0x06000F67 RID: 3943
		void DeepCopyFrom(IFormationArrangement arrangement);

		// Token: 0x06000F68 RID: 3944
		void RearrangeTo(IFormationArrangement arrangement);

		// Token: 0x06000F69 RID: 3945
		void RearrangeFrom(IFormationArrangement arrangement);

		// Token: 0x06000F6A RID: 3946
		void RearrangeTransferUnits(IFormationArrangement arrangement);

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000F6B RID: 3947
		// (remove) Token: 0x06000F6C RID: 3948
		event Action OnWidthChanged;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000F6D RID: 3949
		// (remove) Token: 0x06000F6E RID: 3950
		event Action OnShapeChanged;

		// Token: 0x06000F6F RID: 3951
		void ReserveMiddleFrontUnitPosition(IFormationUnit vanguard);

		// Token: 0x06000F70 RID: 3952
		void ReleaseMiddleFrontUnitPosition();

		// Token: 0x06000F71 RID: 3953
		Vec2 GetLocalPositionOfReservedUnitPosition();

		// Token: 0x06000F72 RID: 3954
		void OnTickOccasionallyOfUnit(IFormationUnit unit, bool arrangementChangeAllowed);

		// Token: 0x06000F73 RID: 3955
		void OnUnitLostMount(IFormationUnit unit);

		// Token: 0x06000F74 RID: 3956
		float GetDirectionChangeTendencyOfUnit(IFormationUnit unit);

		// Token: 0x1700036F RID: 879
		// (set) Token: 0x06000F75 RID: 3957
		bool AreLocalPositionsDirty { set; }
	}
}
