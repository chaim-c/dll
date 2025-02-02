using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200013B RID: 315
	public class ColumnFormation : IFormationArrangement
	{
		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000EA9 RID: 3753 RVA: 0x0002880B File Offset: 0x00026A0B
		// (set) Token: 0x06000EAA RID: 3754 RVA: 0x00028813 File Offset: 0x00026A13
		public IFormationUnit Vanguard
		{
			get
			{
				return this._vanguard;
			}
			private set
			{
				this.SetVanguard(value);
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000EAB RID: 3755 RVA: 0x0002881C File Offset: 0x00026A1C
		// (set) Token: 0x06000EAC RID: 3756 RVA: 0x00028824 File Offset: 0x00026A24
		public int ColumnCount
		{
			get
			{
				return this.FileCount;
			}
			set
			{
				this.SetColumnCount(value);
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000EAD RID: 3757 RVA: 0x0002882D File Offset: 0x00026A2D
		protected int FileCount
		{
			get
			{
				return this._units2D.Count1;
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000EAE RID: 3758 RVA: 0x0002883A File Offset: 0x00026A3A
		public int RankCount
		{
			get
			{
				return this._units2D.Count2;
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000EAF RID: 3759 RVA: 0x00028847 File Offset: 0x00026A47
		private int VanguardFileIndex
		{
			get
			{
				if (this.FileCount % 2 != 0)
				{
					return this.FileCount / 2;
				}
				if (this.isExpandingFromRightSide)
				{
					return this.FileCount / 2 - 1;
				}
				return this.FileCount / 2;
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000EB0 RID: 3760 RVA: 0x00028877 File Offset: 0x00026A77
		protected float Distance
		{
			get
			{
				return this.owner.Distance * 1f + 0.5f;
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000EB1 RID: 3761 RVA: 0x00028890 File Offset: 0x00026A90
		protected float Interval
		{
			get
			{
				return this.owner.Interval * 1.5f;
			}
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x000288A4 File Offset: 0x00026AA4
		public ColumnFormation(IFormation ownerFormation, IFormationUnit vanguard = null, int columnCount = 1)
		{
			this.owner = ownerFormation;
			this._units2D = new MBList2D<IFormationUnit>(columnCount, 1);
			this._units2DWorkspace = new MBList2D<IFormationUnit>(columnCount, 1);
			this.ReconstructUnitsFromUnits2D();
			this._vanguard = vanguard;
			Action onShapeChanged = this.OnShapeChanged;
			if (onShapeChanged == null)
			{
				return;
			}
			onShapeChanged();
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x000288FC File Offset: 0x00026AFC
		public IFormationArrangement Clone(IFormation formation)
		{
			return new ColumnFormation(formation, this.Vanguard, this.ColumnCount);
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x00028910 File Offset: 0x00026B10
		public void DeepCopyFrom(IFormationArrangement arrangement)
		{
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000EB5 RID: 3765 RVA: 0x00028912 File Offset: 0x00026B12
		// (set) Token: 0x06000EB6 RID: 3766 RVA: 0x0002891A File Offset: 0x00026B1A
		public float Width
		{
			get
			{
				return this.FlankWidth;
			}
			set
			{
				this.FlankWidth = value;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000EB7 RID: 3767 RVA: 0x00028923 File Offset: 0x00026B23
		// (set) Token: 0x06000EB8 RID: 3768 RVA: 0x00028954 File Offset: 0x00026B54
		public float FlankWidth
		{
			get
			{
				return (float)(this.FileCount - 1) * (this.owner.Interval + this.owner.UnitDiameter) + this.owner.UnitDiameter;
			}
			set
			{
				int num = MathF.Max(0, (int)((value - this.owner.UnitDiameter) / (this.owner.Interval + this.owner.UnitDiameter) + 1E-05f)) + 1;
				num = MathF.Max(num, 1);
				this.SetColumnCount(num);
				Action onWidthChanged = this.OnWidthChanged;
				if (onWidthChanged == null)
				{
					return;
				}
				onWidthChanged();
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000EB9 RID: 3769 RVA: 0x000289B5 File Offset: 0x00026BB5
		public float Depth
		{
			get
			{
				return this.RankDepth;
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000EBA RID: 3770 RVA: 0x000289BD File Offset: 0x00026BBD
		public float RankDepth
		{
			get
			{
				return (float)(this.RankCount - 1) * (this.Distance + this.owner.UnitDiameter) + this.owner.UnitDiameter;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000EBB RID: 3771 RVA: 0x000289E7 File Offset: 0x00026BE7
		public float MinimumWidth
		{
			get
			{
				return this.MinimumFlankWidth;
			}
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x000289EF File Offset: 0x00026BEF
		public IFormationUnit GetPlayerUnit()
		{
			return this._allUnits.FirstOrDefault((IFormationUnit unit) => unit.IsPlayerUnit);
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000EBD RID: 3773 RVA: 0x00028A1B File Offset: 0x00026C1B
		public float MaximumWidth
		{
			get
			{
				return (float)(this.UnitCount - 1) * (this.owner.UnitDiameter + this.owner.Interval) + this.owner.UnitDiameter;
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000EBE RID: 3774 RVA: 0x00028A4C File Offset: 0x00026C4C
		public float MinimumFlankWidth
		{
			get
			{
				return (float)(MathF.Max(1, MathF.Ceiling(MathF.Sqrt((float)(this.UnitCount / ColumnFormation.ArrangementAspectRatio)))) - 1) * (this.owner.UnitDiameter + this.owner.Interval) + this.owner.UnitDiameter;
			}
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x00028A9D File Offset: 0x00026C9D
		public MBReadOnlyList<IFormationUnit> GetAllUnits()
		{
			return this._allUnits;
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x00028AA5 File Offset: 0x00026CA5
		public MBList<IFormationUnit> GetUnpositionedUnits()
		{
			return null;
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000EC1 RID: 3777 RVA: 0x00028AA8 File Offset: 0x00026CA8
		public bool? IsLoose
		{
			get
			{
				return new bool?(false);
			}
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x00028AB0 File Offset: 0x00026CB0
		private bool IsUnitPositionAvailable(int fileIndex, int rankIndex)
		{
			if (this.IsMiddleFrontUnitPositionReserved)
			{
				ValueTuple<int, int> middleFrontUnitPosition = this.GetMiddleFrontUnitPosition();
				if (fileIndex == middleFrontUnitPosition.Item1 && rankIndex == middleFrontUnitPosition.Item2)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x00028AE4 File Offset: 0x00026CE4
		private bool GetNextVacancy(out int fileIndex, out int rankIndex)
		{
			if (this.RankCount == 0)
			{
				fileIndex = -1;
				rankIndex = -1;
				return false;
			}
			rankIndex = this.RankCount - 1;
			for (int i = 0; i < this.ColumnCount; i++)
			{
				int columnOffsetFromColumnIndex = ColumnFormation.GetColumnOffsetFromColumnIndex(i, this.isExpandingFromRightSide ^ this.ColumnCount % 2 == 1);
				fileIndex = this.VanguardFileIndex + columnOffsetFromColumnIndex;
				if (this._units2D[fileIndex, rankIndex] == null && this.IsUnitPositionAvailable(fileIndex, rankIndex))
				{
					return true;
				}
			}
			fileIndex = -1;
			rankIndex = -1;
			return false;
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x00028B68 File Offset: 0x00026D68
		private IFormationUnit GetLastUnit()
		{
			if (this.RankCount == 0)
			{
				return null;
			}
			int index = this.RankCount - 1;
			for (int i = this.ColumnCount - 1; i >= 0; i--)
			{
				int columnOffsetFromColumnIndex = ColumnFormation.GetColumnOffsetFromColumnIndex(i, this.isExpandingFromRightSide);
				int index2 = this.VanguardFileIndex + columnOffsetFromColumnIndex;
				IFormationUnit formationUnit = this._units2D[index2, index];
				if (formationUnit != null)
				{
					return formationUnit;
				}
			}
			return null;
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x00028BC8 File Offset: 0x00026DC8
		private void Deepen()
		{
			ColumnFormation.Deepen(this);
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x00028BD0 File Offset: 0x00026DD0
		private void ReconstructUnitsFromUnits2D()
		{
			if (this._allUnits == null)
			{
				this._allUnits = new MBList<IFormationUnit>();
			}
			this._allUnits.Clear();
			for (int i = 0; i < this._units2D.Count1; i++)
			{
				for (int j = 0; j < this._units2D.Count2; j++)
				{
					if (this._units2D[i, j] != null)
					{
						this._allUnits.Add(this._units2D[i, j]);
					}
				}
			}
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x00028C50 File Offset: 0x00026E50
		private static void Deepen(ColumnFormation formation)
		{
			formation._units2DWorkspace.ResetWithNewCount(formation.FileCount, formation.RankCount + 1);
			for (int i = 0; i < formation.FileCount; i++)
			{
				formation._units2D.CopyRowTo(i, 0, formation._units2DWorkspace, i, 0, formation.RankCount);
			}
			MBList2D<IFormationUnit> units2D = formation._units2D;
			formation._units2D = formation._units2DWorkspace;
			formation._units2DWorkspace = units2D;
			formation.ReconstructUnitsFromUnits2D();
			Action onShapeChanged = formation.OnShapeChanged;
			if (onShapeChanged == null)
			{
				return;
			}
			onShapeChanged();
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x00028CD2 File Offset: 0x00026ED2
		private void Shorten()
		{
			ColumnFormation.Shorten(this);
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x00028CDC File Offset: 0x00026EDC
		private static void Shorten(ColumnFormation formation)
		{
			formation._units2DWorkspace.ResetWithNewCount(formation.FileCount, formation.RankCount - 1);
			for (int i = 0; i < formation.FileCount; i++)
			{
				formation._units2D.CopyRowTo(i, 0, formation._units2DWorkspace, i, 0, formation.RankCount - 1);
			}
			MBList2D<IFormationUnit> units2D = formation._units2D;
			formation._units2D = formation._units2DWorkspace;
			formation._units2DWorkspace = units2D;
			formation.ReconstructUnitsFromUnits2D();
			Action onShapeChanged = formation.OnShapeChanged;
			if (onShapeChanged == null)
			{
				return;
			}
			onShapeChanged();
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x00028D60 File Offset: 0x00026F60
		public bool AddUnit(IFormationUnit unit)
		{
			int num = 0;
			bool flag = false;
			while (!flag && num < 100)
			{
				num++;
				if (num > 10)
				{
					Debug.FailedAssert("false", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\AI\\Formation\\ColumnFormation.cs", "AddUnit", 371);
				}
				int num2;
				int num3;
				if (this.GetNextVacancy(out num2, out num3))
				{
					unit.FormationFileIndex = num2;
					unit.FormationRankIndex = num3;
					this._units2D[num2, num3] = unit;
					this.ReconstructUnitsFromUnits2D();
					flag = true;
				}
				else
				{
					this.Deepen();
				}
			}
			if (flag)
			{
				int columnOffset;
				IFormationUnit unitToFollow = this.GetUnitToFollow(unit, out columnOffset);
				this.SetUnitToFollow(unit, unitToFollow, columnOffset);
				Action onShapeChanged = this.OnShapeChanged;
				if (onShapeChanged != null)
				{
					onShapeChanged();
				}
			}
			return flag;
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x00028E02 File Offset: 0x00027002
		private IFormationUnit TryGetUnit(int fileIndex, int rankIndex)
		{
			if (fileIndex >= 0 && fileIndex < this.FileCount && rankIndex >= 0 && rankIndex < this.RankCount)
			{
				return this._units2D[fileIndex, rankIndex];
			}
			return null;
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x00028E30 File Offset: 0x00027030
		private void AdjustFollowDataOfUnitPosition(int fileIndex, int rankIndex)
		{
			IFormationUnit formationUnit = this._units2D[fileIndex, rankIndex];
			if (fileIndex == this.VanguardFileIndex)
			{
				if (formationUnit != null)
				{
					IFormationUnit formationUnit2 = this.TryGetUnit(fileIndex, rankIndex - 1);
					this.SetUnitToFollow(formationUnit, formationUnit2 ?? this.Vanguard, 0);
				}
				for (int i = 1; i < this.ColumnCount; i++)
				{
					int columnOffsetFromColumnIndex = ColumnFormation.GetColumnOffsetFromColumnIndex(i, this.isExpandingFromRightSide);
					IFormationUnit formationUnit3 = this._units2D[fileIndex + columnOffsetFromColumnIndex, rankIndex];
					if (formationUnit3 != null)
					{
						this.SetUnitToFollow(formationUnit3, formationUnit ?? this.Vanguard, columnOffsetFromColumnIndex);
					}
				}
				IFormationUnit formationUnit4 = this.TryGetUnit(fileIndex, rankIndex + 1);
				if (formationUnit4 != null)
				{
					this.SetUnitToFollow(formationUnit4, formationUnit ?? this.Vanguard, 0);
					return;
				}
			}
			else if (formationUnit != null)
			{
				IFormationUnit formationUnit5 = this._units2D[this.VanguardFileIndex, rankIndex];
				int columnOffsetFromColumnIndex2 = ColumnFormation.GetColumnOffsetFromColumnIndex(fileIndex, this.isExpandingFromRightSide);
				this.SetUnitToFollow(formationUnit, formationUnit5 ?? this.Vanguard, columnOffsetFromColumnIndex2);
			}
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x00028F20 File Offset: 0x00027120
		private void ShiftUnitsForward(int fileIndex, int rankIndex)
		{
			for (;;)
			{
				IFormationUnit formationUnit = this.TryGetUnit(fileIndex, rankIndex + 1);
				if (formationUnit == null)
				{
					break;
				}
				IFormationUnit formationUnit2 = formationUnit;
				int formationRankIndex = formationUnit2.FormationRankIndex;
				formationUnit2.FormationRankIndex = formationRankIndex - 1;
				this._units2D[fileIndex, rankIndex] = formationUnit;
				this._units2D[fileIndex, rankIndex + 1] = null;
				this.ReconstructUnitsFromUnits2D();
				this.AdjustFollowDataOfUnitPosition(fileIndex, rankIndex);
				rankIndex++;
			}
			int num = 0;
			if (rankIndex == this.RankCount - 1)
			{
				for (int i = 0; i < this.ColumnCount; i++)
				{
					int columnOffsetFromColumnIndex = ColumnFormation.GetColumnOffsetFromColumnIndex(i, this.isExpandingFromRightSide);
					if (this.VanguardFileIndex + columnOffsetFromColumnIndex == fileIndex)
					{
						num = i + 1;
					}
				}
			}
			IFormationUnit formationUnit3 = null;
			for (int j = this.ColumnCount - 1; j >= num; j--)
			{
				int columnOffsetFromColumnIndex2 = ColumnFormation.GetColumnOffsetFromColumnIndex(j, this.isExpandingFromRightSide);
				int index = this.VanguardFileIndex + columnOffsetFromColumnIndex2;
				formationUnit3 = this._units2D[index, this.RankCount - 1];
				if (formationUnit3 != null)
				{
					break;
				}
			}
			if (formationUnit3 != null)
			{
				this._units2D[formationUnit3.FormationFileIndex, formationUnit3.FormationRankIndex] = null;
				formationUnit3.FormationFileIndex = fileIndex;
				formationUnit3.FormationRankIndex = rankIndex;
				this._units2D[fileIndex, rankIndex] = formationUnit3;
				this.ReconstructUnitsFromUnits2D();
				this.AdjustFollowDataOfUnitPosition(fileIndex, rankIndex);
			}
			if (this.IsLastRankEmpty())
			{
				this.Shorten();
			}
			Action onShapeChanged = this.OnShapeChanged;
			if (onShapeChanged == null)
			{
				return;
			}
			onShapeChanged();
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x00029070 File Offset: 0x00027270
		private void ShiftUnitsBackwardForMakingRoomForVanguard(int fileIndex, int rankIndex)
		{
			if (this.RankCount == 1)
			{
				bool flag = false;
				int num = -1;
				for (int i = 0; i < this.ColumnCount; i++)
				{
					int columnOffsetFromColumnIndex = ColumnFormation.GetColumnOffsetFromColumnIndex(i, this.isExpandingFromRightSide);
					if (this._units2D[this.VanguardFileIndex + columnOffsetFromColumnIndex, 0] == null)
					{
						flag = true;
						num = this.VanguardFileIndex + columnOffsetFromColumnIndex;
						break;
					}
				}
				if (flag)
				{
					IFormationUnit formationUnit = this._units2D[fileIndex, rankIndex];
					this._units2D[fileIndex, rankIndex] = null;
					this._units2D[num, 0] = formationUnit;
					this.ReconstructUnitsFromUnits2D();
					formationUnit.FormationFileIndex = num;
					formationUnit.FormationRankIndex = 0;
					return;
				}
				ColumnFormation.Deepen(this);
				IFormationUnit formationUnit2 = this._units2D[fileIndex, rankIndex];
				this._units2D[fileIndex, rankIndex] = null;
				this._units2D[fileIndex, rankIndex + 1] = formationUnit2;
				this.ReconstructUnitsFromUnits2D();
				IFormationUnit formationUnit3 = formationUnit2;
				int formationRankIndex = formationUnit3.FormationRankIndex;
				formationUnit3.FormationRankIndex = formationRankIndex + 1;
				return;
			}
			else
			{
				int num2 = rankIndex;
				IFormationUnit formationUnit4 = null;
				for (rankIndex = this.RankCount - 1; rankIndex >= num2; rankIndex--)
				{
					IFormationUnit formationUnit5 = this._units2D[fileIndex, rankIndex];
					this.TryGetUnit(fileIndex, rankIndex + 1);
					this._units2D[fileIndex, rankIndex] = null;
					if (rankIndex + 1 < this.RankCount)
					{
						IFormationUnit formationUnit6 = formationUnit5;
						int formationRankIndex = formationUnit6.FormationRankIndex;
						formationUnit6.FormationRankIndex = formationRankIndex + 1;
						this._units2D[fileIndex, rankIndex + 1] = formationUnit5;
					}
					else
					{
						formationUnit4 = formationUnit5;
						if (formationUnit4 != null)
						{
							formationUnit4.FormationFileIndex = -1;
							formationUnit4.FormationRankIndex = -1;
						}
					}
					this.ReconstructUnitsFromUnits2D();
				}
				for (rankIndex = this.RankCount - 1; rankIndex >= num2; rankIndex--)
				{
					this.AdjustFollowDataOfUnitPosition(fileIndex, rankIndex);
				}
				if (formationUnit4 != null)
				{
					this.AddUnit(formationUnit4);
				}
				Action onShapeChanged = this.OnShapeChanged;
				if (onShapeChanged == null)
				{
					return;
				}
				onShapeChanged();
				return;
			}
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x00029234 File Offset: 0x00027434
		private bool IsLastRankEmpty()
		{
			if (this.RankCount == 0)
			{
				return false;
			}
			for (int i = 0; i < this.FileCount; i++)
			{
				if (this._units2D[i, this.RankCount - 1] != null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x00029278 File Offset: 0x00027478
		public void RemoveUnit(IFormationUnit unit)
		{
			int formationFileIndex = unit.FormationFileIndex;
			int formationRankIndex = unit.FormationRankIndex;
			if (GameNetwork.IsServer)
			{
				MBDebug.Print(string.Concat(new object[]
				{
					"Removing unit at ",
					formationFileIndex,
					" ",
					formationRankIndex,
					" from column arrangement\nFileCount&RankCount: ",
					this.FileCount,
					" ",
					this.RankCount
				}), 0, Debug.DebugColor.White, 17592186044416UL);
			}
			this._units2D[unit.FormationFileIndex, unit.FormationRankIndex] = null;
			this.ReconstructUnitsFromUnits2D();
			this.ShiftUnitsForward(unit.FormationFileIndex, unit.FormationRankIndex);
			if (this.IsLastRankEmpty())
			{
				this.Shorten();
			}
			unit.FormationFileIndex = -1;
			unit.FormationRankIndex = -1;
			this.SetUnitToFollow(unit, null, 0);
			Action onShapeChanged = this.OnShapeChanged;
			if (onShapeChanged != null)
			{
				onShapeChanged();
			}
			if (this.Vanguard == unit && !((Agent)unit).IsActive())
			{
				this._vanguard = null;
				if (this.FileCount > 0 && this.RankCount > 0)
				{
					this.AdjustFollowDataOfUnitPosition(formationFileIndex, formationRankIndex);
				}
			}
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x000293A1 File Offset: 0x000275A1
		public IFormationUnit GetUnit(int fileIndex, int rankIndex)
		{
			return this._units2D[fileIndex, rankIndex];
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x000293B0 File Offset: 0x000275B0
		public void OnBatchRemoveStart()
		{
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x000293B2 File Offset: 0x000275B2
		public void OnBatchRemoveEnd()
		{
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x000293B4 File Offset: 0x000275B4
		[Conditional("DEBUG")]
		private void AssertUnitPositions()
		{
			for (int i = 0; i < this.FileCount; i++)
			{
				for (int j = 0; j < this.RankCount; j++)
				{
					IFormationUnit formationUnit = this._units2D[i, j];
				}
			}
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x000293F4 File Offset: 0x000275F4
		[Conditional("DEBUG")]
		private void AssertUnit(IFormationUnit unit, bool isAssertingFollowed = true)
		{
			if (unit == null)
			{
				return;
			}
			if (isAssertingFollowed)
			{
				int num;
				this.GetUnitToFollow(unit, out num);
			}
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x00029414 File Offset: 0x00027614
		private static int GetColumnOffsetFromColumnIndex(int columnIndex, bool isExpandingFromRightSide)
		{
			int result;
			if (isExpandingFromRightSide)
			{
				result = (columnIndex + 1) / 2 * ((columnIndex % 2 == 0) ? -1 : 1);
			}
			else
			{
				result = (columnIndex + 1) / 2 * ((columnIndex % 2 == 0) ? 1 : -1);
			}
			return result;
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x00029448 File Offset: 0x00027648
		private IFormationUnit GetUnitToFollow(IFormationUnit unit, out int columnOffset)
		{
			IFormationUnit formationUnit;
			if (unit.FormationFileIndex == this.VanguardFileIndex)
			{
				columnOffset = 0;
				if (unit.FormationRankIndex > 0)
				{
					formationUnit = this._units2D[unit.FormationFileIndex, unit.FormationRankIndex - 1];
				}
				else
				{
					formationUnit = null;
				}
			}
			else
			{
				columnOffset = unit.FormationFileIndex - this.VanguardFileIndex;
				formationUnit = this._units2D[this.VanguardFileIndex, unit.FormationRankIndex];
			}
			if (formationUnit == null)
			{
				formationUnit = this.Vanguard;
			}
			return formationUnit;
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x000294C1 File Offset: 0x000276C1
		private IEnumerable<ValueTuple<int, int>> GetOrderedUnitPositionIndices()
		{
			int num2;
			for (int rankIndex = 0; rankIndex < this.RankCount; rankIndex = num2 + 1)
			{
				for (int columnIndex = 0; columnIndex < this.ColumnCount; columnIndex = num2 + 1)
				{
					int columnOffsetFromColumnIndex = ColumnFormation.GetColumnOffsetFromColumnIndex(columnIndex, this.isExpandingFromRightSide);
					int num = this.VanguardFileIndex + columnOffsetFromColumnIndex;
					if (this.IsUnitPositionAvailable(num, rankIndex))
					{
						yield return new ValueTuple<int, int>(num, rankIndex);
					}
					num2 = columnIndex;
				}
				num2 = rankIndex;
			}
			yield break;
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x000294D4 File Offset: 0x000276D4
		private Vec2 GetLocalPositionOfUnit(int fileIndex, int rankIndex)
		{
			float num = (float)(this.FileCount - 1) * (this.owner.Interval + this.owner.UnitDiameter);
			return new Vec2((float)fileIndex * (this.owner.Interval + this.owner.UnitDiameter) - num / 2f, (float)(-(float)rankIndex) * (this.owner.Distance + this.owner.UnitDiameter));
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x00029545 File Offset: 0x00027745
		private Vec2 GetLocalDirectionOfUnit(int fileIndex, int rankIndex)
		{
			return Vec2.Forward;
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x0002954C File Offset: 0x0002774C
		private WorldPosition? GetWorldPositionOfUnit(int fileIndex, int rankIndex)
		{
			return null;
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x00029564 File Offset: 0x00027764
		public Vec2? GetLocalPositionOfUnitOrDefault(int unitIndex)
		{
			ValueTuple<int, int> valueTuple = (from i in this.GetOrderedUnitPositionIndices()
			where this.IsUnitPositionAvailable(i.Item1, i.Item2)
			select i).ElementAtOrValue(unitIndex, new ValueTuple<int, int>(-1, -1));
			Vec2? result;
			if (valueTuple.Item1 != -1 && valueTuple.Item2 != -1)
			{
				int item = valueTuple.Item1;
				int item2 = valueTuple.Item2;
				result = new Vec2?(this.GetLocalPositionOfUnit(item, item2));
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x000295D0 File Offset: 0x000277D0
		public Vec2? GetLocalDirectionOfUnitOrDefault(int unitIndex)
		{
			ValueTuple<int, int> valueTuple = (from i in this.GetOrderedUnitPositionIndices()
			where this.IsUnitPositionAvailable(i.Item1, i.Item2)
			select i).ElementAtOrValue(unitIndex, new ValueTuple<int, int>(-1, -1));
			Vec2? result;
			if (valueTuple.Item1 != -1 && valueTuple.Item2 != -1)
			{
				int item = valueTuple.Item1;
				int item2 = valueTuple.Item2;
				result = new Vec2?(this.GetLocalDirectionOfUnit(item, item2));
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x0002963C File Offset: 0x0002783C
		public WorldPosition? GetWorldPositionOfUnitOrDefault(int unitIndex)
		{
			ValueTuple<int, int> valueTuple = (from i in this.GetOrderedUnitPositionIndices()
			where this.IsUnitPositionAvailable(i.Item1, i.Item2)
			select i).ElementAtOrValue(unitIndex, new ValueTuple<int, int>(-1, -1));
			WorldPosition? result;
			if (valueTuple.Item1 != -1 && valueTuple.Item2 != -1)
			{
				int item = valueTuple.Item1;
				int item2 = valueTuple.Item2;
				result = this.GetWorldPositionOfUnit(item, item2);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x000296A2 File Offset: 0x000278A2
		public Vec2? GetLocalPositionOfUnitOrDefault(IFormationUnit unit)
		{
			return new Vec2?(this.GetLocalPositionOfUnit(unit.FormationFileIndex, unit.FormationRankIndex));
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x000296BB File Offset: 0x000278BB
		public Vec2? GetLocalPositionOfUnitOrDefaultWithAdjustment(IFormationUnit unit, float distanceBetweenAgentsAdjustment)
		{
			return new Vec2?(this.GetLocalPositionOfUnit(unit.FormationFileIndex, unit.FormationRankIndex));
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x000296D4 File Offset: 0x000278D4
		public WorldPosition? GetWorldPositionOfUnitOrDefault(IFormationUnit unit)
		{
			return this.GetWorldPositionOfUnit(unit.FormationFileIndex, unit.FormationRankIndex);
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x000296E8 File Offset: 0x000278E8
		public Vec2? GetLocalDirectionOfUnitOrDefault(IFormationUnit unit)
		{
			return new Vec2?(this.GetLocalDirectionOfUnit(unit.FormationFileIndex, unit.FormationRankIndex));
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x00029704 File Offset: 0x00027904
		public List<IFormationUnit> GetUnitsToPop(int count)
		{
			List<IFormationUnit> list = new List<IFormationUnit>();
			for (int i = this.RankCount - 1; i >= 0; i--)
			{
				for (int j = this.ColumnCount - 1; j >= 0; j--)
				{
					int columnOffsetFromColumnIndex = ColumnFormation.GetColumnOffsetFromColumnIndex(j, this.isExpandingFromRightSide);
					int index = this.VanguardFileIndex + columnOffsetFromColumnIndex;
					IFormationUnit formationUnit = this._units2D[index, i];
					if (formationUnit != null)
					{
						list.Add(formationUnit);
						count--;
						if (count == 0)
						{
							return list;
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x0002977B File Offset: 0x0002797B
		public List<IFormationUnit> GetUnitsToPop(int count, Vec3 targetPosition)
		{
			return this.GetUnitsToPop(count);
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x00029784 File Offset: 0x00027984
		public IEnumerable<IFormationUnit> GetUnitsToPopWithCondition(int count, Func<IFormationUnit, bool> currentCondition)
		{
			int num;
			for (int rankIndex = this.RankCount - 1; rankIndex >= 0; rankIndex = num - 1)
			{
				for (int columnIndex = this.ColumnCount - 1; columnIndex >= 0; columnIndex = num - 1)
				{
					int columnOffsetFromColumnIndex = ColumnFormation.GetColumnOffsetFromColumnIndex(columnIndex, this.isExpandingFromRightSide);
					int index = this.VanguardFileIndex + columnOffsetFromColumnIndex;
					IFormationUnit formationUnit = this._units2D[index, rankIndex];
					if (formationUnit != null && currentCondition(formationUnit))
					{
						yield return formationUnit;
						num = count;
						count = num - 1;
						if (count == 0)
						{
							yield break;
						}
					}
					num = columnIndex;
				}
				num = rankIndex;
			}
			yield break;
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x000297A2 File Offset: 0x000279A2
		public void SwitchUnitLocations(IFormationUnit firstUnit, IFormationUnit secondUnit)
		{
			this.SwitchUnitLocationsAux(firstUnit, secondUnit);
			this.AdjustFollowDataOfUnitPosition(firstUnit.FormationFileIndex, firstUnit.FormationRankIndex);
			this.AdjustFollowDataOfUnitPosition(secondUnit.FormationFileIndex, secondUnit.FormationRankIndex);
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x000297D0 File Offset: 0x000279D0
		private void SwitchUnitLocationsAux(IFormationUnit firstUnit, IFormationUnit secondUnit)
		{
			int formationFileIndex = firstUnit.FormationFileIndex;
			int formationRankIndex = firstUnit.FormationRankIndex;
			int formationFileIndex2 = secondUnit.FormationFileIndex;
			int formationRankIndex2 = secondUnit.FormationRankIndex;
			this._units2D[formationFileIndex, formationRankIndex] = secondUnit;
			this._units2D[formationFileIndex2, formationRankIndex2] = firstUnit;
			this.ReconstructUnitsFromUnits2D();
			firstUnit.FormationFileIndex = formationFileIndex2;
			firstUnit.FormationRankIndex = formationRankIndex2;
			secondUnit.FormationFileIndex = formationFileIndex;
			secondUnit.FormationRankIndex = formationRankIndex;
			Action onShapeChanged = this.OnShapeChanged;
			if (onShapeChanged == null)
			{
				return;
			}
			onShapeChanged();
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x00029847 File Offset: 0x00027A47
		public void SwitchUnitLocationsWithUnpositionedUnit(IFormationUnit firstUnit, IFormationUnit secondUnit)
		{
			Debug.FailedAssert("Column formation should NOT have an unpositioned unit", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\AI\\Formation\\ColumnFormation.cs", "SwitchUnitLocationsWithUnpositionedUnit", 1169);
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x00029864 File Offset: 0x00027A64
		public void SwitchUnitLocationsWithBackMostUnit(IFormationUnit unit)
		{
			Agent agent;
			if (this.Vanguard == null || (agent = (this.Vanguard as Agent)) == null || agent != unit)
			{
				IFormationUnit lastUnit = this.GetLastUnit();
				if (lastUnit != null && unit != null && unit != lastUnit)
				{
					this.SwitchUnitLocations(unit, lastUnit);
				}
			}
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x000298A5 File Offset: 0x00027AA5
		public float GetUnitsDistanceToFrontLine(IFormationUnit unit)
		{
			return -1f;
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x000298AC File Offset: 0x00027AAC
		public Vec2? GetLocalDirectionOfRelativeFormationLocation(IFormationUnit unit)
		{
			return null;
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x000298C4 File Offset: 0x00027AC4
		public Vec2? GetLocalWallDirectionOfRelativeFormationLocation(IFormationUnit unit)
		{
			return null;
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x000298DA File Offset: 0x00027ADA
		public IEnumerable<Vec2> GetUnavailableUnitPositions()
		{
			yield break;
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x000298E3 File Offset: 0x00027AE3
		public float GetOccupationWidth(int unitCount)
		{
			return this.FlankWidth;
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x000298EC File Offset: 0x00027AEC
		public Vec2? CreateNewPosition(int unitIndex)
		{
			int num = MathF.Ceiling((float)unitIndex * 1f / (float)this.ColumnCount);
			if (num > this.RankCount)
			{
				this._units2D.ResetWithNewCount(this.ColumnCount, num);
				this.ReconstructUnitsFromUnits2D();
			}
			Vec2? localPositionOfUnitOrDefault = this.GetLocalPositionOfUnitOrDefault(unitIndex);
			Action onShapeChanged = this.OnShapeChanged;
			if (onShapeChanged == null)
			{
				return localPositionOfUnitOrDefault;
			}
			onShapeChanged();
			return localPositionOfUnitOrDefault;
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x00029947 File Offset: 0x00027B47
		public void InvalidateCacheOfUnitAux(Vec2 roundedLocalPosition)
		{
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x00029949 File Offset: 0x00027B49
		public void BeforeFormationFrameChange()
		{
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x0002994B File Offset: 0x00027B4B
		public void OnFormationFrameChanged()
		{
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x00029950 File Offset: 0x00027B50
		private Vec2 CalculateArrangementOrientation()
		{
			IFormationUnit formationUnit = this.Vanguard ?? this._units2D[this.GetMiddleFrontUnitPosition().Item1, this.GetMiddleFrontUnitPosition().Item2];
			if (formationUnit is Agent && this.owner is Formation)
			{
				return ((formationUnit as Agent).Position.AsVec2 - (this.owner as Formation).QuerySystem.MedianPosition.AsVec2).Normalized();
			}
			Debug.FailedAssert("Unexpected case", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\AI\\Formation\\ColumnFormation.cs", "CalculateArrangementOrientation", 1254);
			return this.GetLocalDirectionOfUnit(formationUnit.FormationFileIndex, formationUnit.FormationRankIndex);
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x00029A07 File Offset: 0x00027C07
		public void OnUnitLostMount(IFormationUnit unit)
		{
			this.RemoveUnit(unit);
			this.AddUnit(unit);
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x00029A18 File Offset: 0x00027C18
		public bool IsTurnBackwardsNecessary(Vec2 previousPosition, WorldPosition? newPosition, Vec2 previousDirection, bool hasNewDirection, Vec2? newDirection)
		{
			return newPosition != null && this.UnitCount > 0 && this.RankCount > 0 && (newPosition.Value.AsVec2 - previousPosition).LengthSquared >= this.RankDepth * this.RankDepth && MathF.Abs(MBMath.GetSmallestDifferenceBetweenTwoAngles(this.CalculateArrangementOrientation().RotationInRadians, (newPosition.Value.AsVec2 - previousPosition).Normalized().RotationInRadians)) >= 2.3561945f;
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x00029AB8 File Offset: 0x00027CB8
		public void TurnBackwards()
		{
			if (!this.IsMiddleFrontUnitPositionReserved && !this._isMiddleFrontUnitPositionUsedByVanguardInFormation && this.RankCount > 1)
			{
				bool isMiddleFrontUnitPositionReserved = this.IsMiddleFrontUnitPositionReserved;
				IFormationUnit vanguard = this._vanguard;
				if (isMiddleFrontUnitPositionReserved)
				{
					this.ReleaseMiddleFrontUnitPosition();
				}
				int rankCount = this.RankCount;
				for (int i = 0; i < rankCount / 2; i++)
				{
					for (int j = 0; j < this.FileCount; j++)
					{
						IFormationUnit formationUnit = this._units2D[j, i];
						int num = rankCount - i - 1;
						int num2 = this.FileCount - j - 1;
						IFormationUnit formationUnit2 = this._units2D[num2, num];
						if (formationUnit2 == null)
						{
							this._units2D[num2, num] = formationUnit;
							this._units2D[j, i] = null;
							if (formationUnit != null)
							{
								formationUnit.FormationFileIndex = num2;
								formationUnit.FormationRankIndex = num;
							}
						}
						else if (formationUnit != null && formationUnit != formationUnit2)
						{
							this.SwitchUnitLocationsAux(formationUnit, formationUnit2);
						}
					}
				}
				for (int k = 0; k < this.FileCount; k++)
				{
					if (this._units2D[k, 0] == null && this._units2D[k, 1] != null)
					{
						for (int l = 1; l < rankCount; l++)
						{
							IFormationUnit formationUnit3 = this._units2D[k, l];
							IFormationUnit formationUnit4 = formationUnit3;
							int formationRankIndex = formationUnit4.FormationRankIndex;
							formationUnit4.FormationRankIndex = formationRankIndex - 1;
							this._units2D[k, l - 1] = formationUnit3;
							this._units2D[k, l] = null;
						}
					}
				}
				this.isExpandingFromRightSide = !this.isExpandingFromRightSide;
				this.ReconstructUnitsFromUnits2D();
				foreach (IFormationUnit unit in this.GetAllUnits())
				{
					int columnOffset;
					IFormationUnit unitToFollow = this.GetUnitToFollow(unit, out columnOffset);
					this.SetUnitToFollow(unit, unitToFollow, columnOffset);
				}
				Action onShapeChanged = this.OnShapeChanged;
				if (onShapeChanged != null)
				{
					onShapeChanged();
				}
				if (isMiddleFrontUnitPositionReserved)
				{
					this.ReserveMiddleFrontUnitPosition(vanguard);
				}
				Action onShapeChanged2 = this.OnShapeChanged;
				if (onShapeChanged2 == null)
				{
					return;
				}
				onShapeChanged2();
			}
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x00029CE4 File Offset: 0x00027EE4
		public void OnFormationDispersed()
		{
			foreach (IFormationUnit unit in this.GetAllUnits().ToArray())
			{
				this.SwitchUnitIfLeftBehind(unit);
			}
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x00029D16 File Offset: 0x00027F16
		public void Reset()
		{
			this._units2D.ResetWithNewCount(this.ColumnCount, 1);
			this.ReconstructUnitsFromUnits2D();
			Action onShapeChanged = this.OnShapeChanged;
			if (onShapeChanged == null)
			{
				return;
			}
			onShapeChanged();
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000EF9 RID: 3833 RVA: 0x00029D40 File Offset: 0x00027F40
		// (remove) Token: 0x06000EFA RID: 3834 RVA: 0x00029D78 File Offset: 0x00027F78
		public event Action OnWidthChanged;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000EFB RID: 3835 RVA: 0x00029DB0 File Offset: 0x00027FB0
		// (remove) Token: 0x06000EFC RID: 3836 RVA: 0x00029DE8 File Offset: 0x00027FE8
		public event Action OnShapeChanged;

		// Token: 0x06000EFD RID: 3837 RVA: 0x00029E20 File Offset: 0x00028020
		public virtual void RearrangeFrom(IFormationArrangement arrangement)
		{
			if (arrangement is LineFormation)
			{
				this.FlankWidth = (float)MathF.Max(0, MathF.Ceiling(MathF.Sqrt((float)(arrangement.UnitCount / 5))) - 1) * (this.owner.UnitDiameter + this.owner.Interval) + this.owner.UnitDiameter;
			}
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x00029E7B File Offset: 0x0002807B
		public virtual void RearrangeTo(IFormationArrangement arrangement)
		{
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x00029E80 File Offset: 0x00028080
		public virtual void RearrangeTransferUnits(IFormationArrangement arrangement)
		{
			foreach (ValueTuple<int, int> valueTuple in this.GetOrderedUnitPositionIndices().ToList<ValueTuple<int, int>>())
			{
				IFormationUnit formationUnit = this._units2D[valueTuple.Item1, valueTuple.Item2];
				if (formationUnit != null)
				{
					formationUnit.FormationFileIndex = -1;
					formationUnit.FormationRankIndex = -1;
					this.SetUnitToFollow(formationUnit, null, 0);
					arrangement.AddUnit(formationUnit);
				}
			}
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x00029F0C File Offset: 0x0002810C
		private void SetVanguard(IFormationUnit vanguard)
		{
			if (this.Vanguard != null || vanguard != null)
			{
				bool flag = false;
				bool flag2 = false;
				if (this.UnitCount > 0)
				{
					if (this.Vanguard == null && vanguard != null)
					{
						flag2 = true;
					}
					else if (this.Vanguard != null && vanguard == null)
					{
						flag = true;
					}
				}
				ValueTuple<int, int> middleFrontUnitPosition = this.GetMiddleFrontUnitPosition();
				if (flag)
				{
					Agent agent = this.Vanguard as Agent;
					if (((agent != null) ? agent.Formation : null) == this.owner)
					{
						this.RemoveUnit(this.Vanguard);
						this.AddUnit(this.Vanguard);
					}
					else if (this.RankCount > 0)
					{
						this.ShiftUnitsForward(middleFrontUnitPosition.Item1, middleFrontUnitPosition.Item2);
					}
				}
				else if (flag2)
				{
					Agent agent2 = vanguard as Agent;
					if (((agent2 != null) ? agent2.Formation : null) == this.owner)
					{
						this.RemoveUnit(vanguard);
						this.ShiftUnitsBackwardForMakingRoomForVanguard(middleFrontUnitPosition.Item1, middleFrontUnitPosition.Item2);
						if (this.RankCount > 0)
						{
							this._units2D[middleFrontUnitPosition.Item1, middleFrontUnitPosition.Item2] = vanguard;
							this.ReconstructUnitsFromUnits2D();
							vanguard.FormationFileIndex = middleFrontUnitPosition.Item1;
							vanguard.FormationRankIndex = middleFrontUnitPosition.Item2;
							if (this.RankCount == 2)
							{
								this.AdjustFollowDataOfUnitPosition(middleFrontUnitPosition.Item1, middleFrontUnitPosition.Item2);
								this.AdjustFollowDataOfUnitPosition(middleFrontUnitPosition.Item1, middleFrontUnitPosition.Item2 + 1);
								Action onShapeChanged = this.OnShapeChanged;
								if (onShapeChanged != null)
								{
									onShapeChanged();
								}
							}
						}
						else
						{
							this.AddUnit(vanguard);
						}
					}
					else
					{
						this.ShiftUnitsBackwardForMakingRoomForVanguard(middleFrontUnitPosition.Item1, middleFrontUnitPosition.Item2);
					}
				}
				this._vanguard = vanguard;
				if (this.RankCount > 0)
				{
					this.AdjustFollowDataOfUnitPosition(middleFrontUnitPosition.Item1, middleFrontUnitPosition.Item2);
				}
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000F01 RID: 3841 RVA: 0x0002A0B5 File Offset: 0x000282B5
		public int UnitCount
		{
			get
			{
				return this.GetAllUnits().Count;
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000F02 RID: 3842 RVA: 0x0002A0C2 File Offset: 0x000282C2
		public int PositionedUnitCount
		{
			get
			{
				return this.UnitCount;
			}
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x0002A0CC File Offset: 0x000282CC
		protected int GetUnitCountWithOverride()
		{
			int result;
			if (this.owner.OverridenUnitCount != null)
			{
				result = this.owner.OverridenUnitCount.Value;
			}
			else
			{
				result = this.UnitCount;
			}
			return result;
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x0002A10C File Offset: 0x0002830C
		private void SetColumnCount(int columnCount)
		{
			if (this.ColumnCount != columnCount)
			{
				IFormationUnit[] array = this.GetAllUnits().ToArray();
				this._units2D.ResetWithNewCount(columnCount, 1);
				this.ReconstructUnitsFromUnits2D();
				foreach (IFormationUnit formationUnit in array)
				{
					formationUnit.FormationFileIndex = -1;
					formationUnit.FormationRankIndex = -1;
					this.AddUnit(formationUnit);
				}
				Action onShapeChanged = this.OnShapeChanged;
				if (onShapeChanged == null)
				{
					return;
				}
				onShapeChanged();
			}
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x0002A179 File Offset: 0x00028379
		public void FormFromWidth(float width)
		{
			this.ColumnCount = MathF.Ceiling(width);
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x0002A188 File Offset: 0x00028388
		public IFormationUnit GetNeighborUnitOfLeftSide(IFormationUnit unit)
		{
			int formationRankIndex = unit.FormationRankIndex;
			for (int i = unit.FormationFileIndex - 1; i >= 0; i--)
			{
				if (this._units2D[i, formationRankIndex] != null)
				{
					return this._units2D[i, formationRankIndex];
				}
			}
			return null;
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x0002A1D0 File Offset: 0x000283D0
		public IFormationUnit GetNeighborUnitOfRightSide(IFormationUnit unit)
		{
			int formationRankIndex = unit.FormationRankIndex;
			for (int i = unit.FormationFileIndex + 1; i < this.FileCount; i++)
			{
				if (this._units2D[i, formationRankIndex] != null)
				{
					return this._units2D[i, formationRankIndex];
				}
			}
			return null;
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x0002A21A File Offset: 0x0002841A
		public void ReserveMiddleFrontUnitPosition(IFormationUnit vanguard)
		{
			Agent agent = vanguard as Agent;
			if (((agent != null) ? agent.Formation : null) != this.owner)
			{
				this.IsMiddleFrontUnitPositionReserved = true;
			}
			else
			{
				this._isMiddleFrontUnitPositionUsedByVanguardInFormation = true;
			}
			this.Vanguard = vanguard;
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x0002A24D File Offset: 0x0002844D
		public void ReleaseMiddleFrontUnitPosition()
		{
			this.IsMiddleFrontUnitPositionReserved = false;
			this.Vanguard = null;
			this._isMiddleFrontUnitPositionUsedByVanguardInFormation = false;
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x0002A264 File Offset: 0x00028464
		private ValueTuple<int, int> GetMiddleFrontUnitPosition()
		{
			return new ValueTuple<int, int>(this.VanguardFileIndex, 0);
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x0002A272 File Offset: 0x00028472
		public Vec2 GetLocalPositionOfReservedUnitPosition()
		{
			return Vec2.Zero;
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x0002A27C File Offset: 0x0002847C
		public void OnTickOccasionallyOfUnit(IFormationUnit unit, bool arrangementChangeAllowed)
		{
			if (arrangementChangeAllowed && unit.FollowedUnit != this._vanguard && unit.FollowedUnit is Agent && !((Agent)unit.FollowedUnit).IsAIControlled && unit.FollowedUnit.FormationFileIndex >= 0 && unit.FollowedUnit.FormationRankIndex >= 0)
			{
				if (unit.FollowedUnit.FormationFileIndex * this._units2D.Count2 + unit.FollowedUnit.FormationRankIndex >= this._units2D.RawArray.Length || unit.FollowedUnit.FormationFileIndex * this._units2D.Count2 + unit.FollowedUnit.FormationRankIndex < 0)
				{
					Debug.Print("Followed unit has illegal formation indices!", 0, Debug.DebugColor.White, 17592186044416UL);
					Debug.Print(string.Concat(new object[]
					{
						"RankIndex: ",
						unit.FormationRankIndex,
						" FileIndex: ",
						unit.FormationFileIndex
					}), 0, Debug.DebugColor.White, 17592186044416UL);
					Debug.Print(string.Concat(new object[]
					{
						"_units2D.Capacity: ",
						this._units2D.RawArray.Length,
						" _units2D.Count1: ",
						this._units2D.Count1,
						" _units2D.Count2: ",
						this._units2D.Count2
					}), 0, Debug.DebugColor.White, 17592186044416UL);
					Debug.Print(string.Concat(new object[]
					{
						"FollowedUnit.RankIndex: ",
						unit.FollowedUnit.FormationRankIndex,
						" FollowedUnit.FileIndex: ",
						unit.FollowedUnit.FormationFileIndex
					}), 0, Debug.DebugColor.White, 17592186044416UL);
					if (!(unit.FollowedUnit.Formation is ColumnFormation))
					{
						Debug.Print("Followed unit is not in column formation", 0, Debug.DebugColor.White, 17592186044416UL);
					}
					if (((Agent)unit.FollowedUnit).IsPlayerControlled)
					{
						Debug.Print("Followed unit is player", 0, Debug.DebugColor.White, 17592186044416UL);
					}
					if (((Agent)unit).Formation.Captain == (Agent)unit.FollowedUnit)
					{
						Debug.Print("Followed unit is the captain", 0, Debug.DebugColor.White, 17592186044416UL);
					}
					Debug.Print("-------------------------------------", 0, Debug.DebugColor.White, 17592186044416UL);
					foreach (IFormationUnit formationUnit in unit.FollowedUnit.Formation.GetAllUnits())
					{
						Debug.Print(string.Concat(new object[]
						{
							"R: ",
							formationUnit.FormationRankIndex,
							" F: ",
							formationUnit.FormationFileIndex,
							" AI: ",
							((Agent)formationUnit).IsAIControlled ? "1" : "0"
						}), 0, Debug.DebugColor.White, 17592186044416UL);
					}
					Debug.Print("-------------------------------------", 0, Debug.DebugColor.White, 17592186044416UL);
				}
				IFormationUnit followedUnit = unit.FollowedUnit;
				this.RemoveUnit(unit.FollowedUnit);
				this.AddUnit(followedUnit);
			}
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x0002A5E0 File Offset: 0x000287E0
		private MBList<IFormationUnit> GetUnitsBehind(IFormationUnit unit)
		{
			MBList<IFormationUnit> mblist = new MBList<IFormationUnit>();
			bool flag = false;
			for (int i = 0; i < this.ColumnCount; i++)
			{
				int columnOffsetFromColumnIndex = ColumnFormation.GetColumnOffsetFromColumnIndex(i, this.isExpandingFromRightSide);
				int num = this.VanguardFileIndex + columnOffsetFromColumnIndex;
				if (num == unit.FormationFileIndex)
				{
					flag = true;
				}
				if (flag && this._units2D[num, unit.FormationRankIndex] != null)
				{
					mblist.Add(this._units2D[num, unit.FormationRankIndex]);
				}
			}
			for (int j = 0; j < this.FileCount; j++)
			{
				for (int k = unit.FormationRankIndex + 1; k < this.RankCount; k++)
				{
					if (this._units2D[j, k] != null)
					{
						mblist.Add(this._units2D[j, k]);
					}
				}
			}
			return mblist;
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x0002A6B4 File Offset: 0x000288B4
		private void SwitchUnitIfLeftBehind(IFormationUnit unit)
		{
			int columnOffset;
			IFormationUnit unitToFollow = this.GetUnitToFollow(unit, out columnOffset);
			if (unitToFollow == null)
			{
				float value = this.owner.UnitDiameter * 2f;
				IFormationUnit closestUnitTo = this.owner.GetClosestUnitTo(Vec2.Zero, new MBList<IFormationUnit>
				{
					unit
				}, new float?(value));
				if (closestUnitTo == null)
				{
					closestUnitTo = this.owner.GetClosestUnitTo(Vec2.Zero, this.GetUnitsAtRanks(0, this.RankCount - 1), null);
				}
				if (closestUnitTo != null && closestUnitTo != unit && closestUnitTo is Agent && (closestUnitTo as Agent).IsAIControlled)
				{
					this.SwitchUnitLocations(unit, closestUnitTo);
					return;
				}
			}
			else
			{
				float value2 = this.GetFollowVector(columnOffset).Length * 1.5f;
				IFormationUnit closestUnitTo2 = this.owner.GetClosestUnitTo(unitToFollow, new MBList<IFormationUnit>
				{
					unit
				}, new float?(value2));
				if (closestUnitTo2 == null)
				{
					closestUnitTo2 = this.owner.GetClosestUnitTo(unitToFollow, this.GetUnitsBehind(unit), null);
				}
				Agent agent;
				if (closestUnitTo2 != null && closestUnitTo2 != unit && (agent = (closestUnitTo2 as Agent)) != null && agent.IsAIControlled)
				{
					this.SwitchUnitLocations(unit, agent);
				}
			}
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x0002A7E8 File Offset: 0x000289E8
		private void SetUnitToFollow(IFormationUnit unit, IFormationUnit unitToFollow, int columnOffset = 0)
		{
			Vec2 followVector = this.GetFollowVector(columnOffset);
			this.owner.SetUnitToFollow(unit, unitToFollow, followVector);
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x0002A80C File Offset: 0x00028A0C
		private Vec2 GetFollowVector(int columnOffset)
		{
			Vec2 result;
			if (columnOffset == 0)
			{
				result = -Vec2.Forward * (this.Distance + this.owner.UnitDiameter);
			}
			else
			{
				result = Vec2.Side * (float)columnOffset * (this.owner.UnitDiameter + this.Interval);
			}
			return result;
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x0002A865 File Offset: 0x00028A65
		public float GetDirectionChangeTendencyOfUnit(IFormationUnit unit)
		{
			if (this.RankCount == 1 || unit.FormationRankIndex == -1)
			{
				return 0f;
			}
			return (float)unit.FormationRankIndex * 1f / (float)(this.RankCount - 1);
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x0002A898 File Offset: 0x00028A98
		private MBList<IFormationUnit> GetUnitsAtRanks(int rankIndex1, int rankIndex2)
		{
			MBList<IFormationUnit> mblist = new MBList<IFormationUnit>();
			for (int i = 0; i < this.ColumnCount; i++)
			{
				int columnOffsetFromColumnIndex = ColumnFormation.GetColumnOffsetFromColumnIndex(i, this.isExpandingFromRightSide);
				int index = this.VanguardFileIndex + columnOffsetFromColumnIndex;
				if (this._units2D[index, rankIndex1] != null)
				{
					mblist.Add(this._units2D[index, rankIndex1]);
				}
			}
			for (int j = 0; j < this.ColumnCount; j++)
			{
				int columnOffsetFromColumnIndex2 = ColumnFormation.GetColumnOffsetFromColumnIndex(j, this.isExpandingFromRightSide);
				int index2 = this.VanguardFileIndex + columnOffsetFromColumnIndex2;
				if (this._units2D[index2, rankIndex2] != null)
				{
					mblist.Add(this._units2D[index2, rankIndex2]);
				}
			}
			return mblist;
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x0002A948 File Offset: 0x00028B48
		public IEnumerable<T> GetUnitsAtVanguardFile<T>() where T : IFormationUnit
		{
			int fileIndex = this.VanguardFileIndex;
			int num;
			for (int rankIndex = 0; rankIndex < this.RankCount; rankIndex = num + 1)
			{
				if (this._units2D[fileIndex, rankIndex] != null)
				{
					yield return (T)((object)this._units2D[fileIndex, rankIndex]);
				}
				num = rankIndex;
			}
			yield break;
		}

		// Token: 0x1700035B RID: 859
		// (set) Token: 0x06000F14 RID: 3860 RVA: 0x0002A958 File Offset: 0x00028B58
		bool IFormationArrangement.AreLocalPositionsDirty
		{
			set
			{
			}
		}

		// Token: 0x0400039F RID: 927
		public static readonly int ArrangementAspectRatio = 5;

		// Token: 0x040003A0 RID: 928
		private readonly IFormation owner;

		// Token: 0x040003A1 RID: 929
		private IFormationUnit _vanguard;

		// Token: 0x040003A2 RID: 930
		private MBList2D<IFormationUnit> _units2D;

		// Token: 0x040003A3 RID: 931
		private MBList2D<IFormationUnit> _units2DWorkspace;

		// Token: 0x040003A4 RID: 932
		private MBList<IFormationUnit> _allUnits;

		// Token: 0x040003A5 RID: 933
		private bool isExpandingFromRightSide = true;

		// Token: 0x040003A6 RID: 934
		private bool IsMiddleFrontUnitPositionReserved;

		// Token: 0x040003A7 RID: 935
		private bool _isMiddleFrontUnitPositionUsedByVanguardInFormation;
	}
}
