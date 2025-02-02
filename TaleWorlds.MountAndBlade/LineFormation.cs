using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000141 RID: 321
	public class LineFormation : IFormationArrangement
	{
		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000F7E RID: 3966 RVA: 0x0002AF1A File Offset: 0x0002911A
		protected int FileCount
		{
			get
			{
				return this._units2D.Count1;
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000F7F RID: 3967 RVA: 0x0002AF27 File Offset: 0x00029127
		public int RankCount
		{
			get
			{
				return this._units2D.Count2;
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000F80 RID: 3968 RVA: 0x0002AF34 File Offset: 0x00029134
		// (set) Token: 0x06000F81 RID: 3969 RVA: 0x0002AF3C File Offset: 0x0002913C
		public bool AreLocalPositionsDirty { protected get; set; }

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000F82 RID: 3970 RVA: 0x0002AF45 File Offset: 0x00029145
		protected float Interval
		{
			get
			{
				return this.owner.Interval;
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000F83 RID: 3971 RVA: 0x0002AF52 File Offset: 0x00029152
		protected float Distance
		{
			get
			{
				return this.owner.Distance;
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000F84 RID: 3972 RVA: 0x0002AF5F File Offset: 0x0002915F
		protected float UnitDiameter
		{
			get
			{
				return this.owner.UnitDiameter;
			}
		}

		// Token: 0x06000F85 RID: 3973 RVA: 0x0002AF6C File Offset: 0x0002916C
		public int GetFileCountFromWidth(float width)
		{
			return MathF.Max(MathF.Max(0, (int)((width - this.UnitDiameter) / (this.Interval + this.UnitDiameter) + 1E-05f)) + 1, this.MinimumFileCount);
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000F86 RID: 3974 RVA: 0x0002AF9E File Offset: 0x0002919E
		// (set) Token: 0x06000F87 RID: 3975 RVA: 0x0002AFA6 File Offset: 0x000291A6
		public virtual float Width
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

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000F88 RID: 3976 RVA: 0x0002AFAF File Offset: 0x000291AF
		public virtual float Depth
		{
			get
			{
				return this.RankDepth;
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000F89 RID: 3977 RVA: 0x0002AFB7 File Offset: 0x000291B7
		// (set) Token: 0x06000F8A RID: 3978 RVA: 0x0002AFD8 File Offset: 0x000291D8
		public float FlankWidth
		{
			get
			{
				return (float)(this.FileCount - 1) * (this.Interval + this.UnitDiameter) + this.UnitDiameter;
			}
			set
			{
				int fileCountFromWidth = this.GetFileCountFromWidth(value);
				if (fileCountFromWidth > this.FileCount)
				{
					LineFormation.WidenFormation(this, fileCountFromWidth - this.FileCount);
				}
				else if (fileCountFromWidth < this.FileCount)
				{
					LineFormation.NarrowFormation(this, this.FileCount - fileCountFromWidth);
				}
				Action onWidthChanged = this.OnWidthChanged;
				if (onWidthChanged != null)
				{
					onWidthChanged();
				}
				Action onShapeChanged = this.OnShapeChanged;
				if (onShapeChanged == null)
				{
					return;
				}
				onShapeChanged();
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000F8B RID: 3979 RVA: 0x0002B040 File Offset: 0x00029240
		private int MinimumFileCount
		{
			get
			{
				if (this.IsTransforming)
				{
					return 1;
				}
				int unitCountWithOverride = this.GetUnitCountWithOverride();
				return MathF.Max(1, (int)MathF.Sqrt((float)unitCountWithOverride));
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000F8C RID: 3980 RVA: 0x0002B06C File Offset: 0x0002926C
		public float RankDepth
		{
			get
			{
				return (float)(this.RankCount - 1) * (this.Distance + this.UnitDiameter) + this.UnitDiameter;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000F8D RID: 3981 RVA: 0x0002B08C File Offset: 0x0002928C
		public float MinimumFlankWidth
		{
			get
			{
				return (float)(this.MinimumFileCount - 1) * (this.MinimumInterval + this.UnitDiameter) + this.UnitDiameter;
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000F8E RID: 3982 RVA: 0x0002B0AC File Offset: 0x000292AC
		public virtual float MinimumWidth
		{
			get
			{
				return this.MinimumFlankWidth;
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000F8F RID: 3983 RVA: 0x0002B0B4 File Offset: 0x000292B4
		private float MinimumInterval
		{
			get
			{
				return this.owner.MinimumInterval;
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000F90 RID: 3984 RVA: 0x0002B0C4 File Offset: 0x000292C4
		public virtual float MaximumWidth
		{
			get
			{
				float num = this.UnitDiameter;
				int unitCountWithOverride = this.GetUnitCountWithOverride();
				if (unitCountWithOverride > 0)
				{
					num += (float)(unitCountWithOverride - 1) * (this.owner.MaximumInterval + this.UnitDiameter);
				}
				return num;
			}
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x0002B100 File Offset: 0x00029300
		protected int GetUnitCountWithOverride()
		{
			int? overridenUnitCount = this.owner.OverridenUnitCount;
			if (overridenUnitCount == null)
			{
				return this.UnitCount;
			}
			return overridenUnitCount.GetValueOrDefault();
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000F92 RID: 3986 RVA: 0x0002B130 File Offset: 0x00029330
		// (set) Token: 0x06000F93 RID: 3987 RVA: 0x0002B138 File Offset: 0x00029338
		public bool IsStaggered
		{
			get
			{
				return this._isStaggered;
			}
			set
			{
				if (this._isStaggered != value)
				{
					this._isStaggered = value;
					Action onShapeChanged = this.OnShapeChanged;
					if (onShapeChanged == null)
					{
						return;
					}
					onShapeChanged();
				}
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000F94 RID: 3988 RVA: 0x0002B15C File Offset: 0x0002935C
		public virtual bool? IsLoose
		{
			get
			{
				return null;
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000F95 RID: 3989 RVA: 0x0002B174 File Offset: 0x00029374
		// (remove) Token: 0x06000F96 RID: 3990 RVA: 0x0002B1AC File Offset: 0x000293AC
		public event Action OnWidthChanged;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000F97 RID: 3991 RVA: 0x0002B1E4 File Offset: 0x000293E4
		// (remove) Token: 0x06000F98 RID: 3992 RVA: 0x0002B21C File Offset: 0x0002941C
		public event Action OnShapeChanged;

		// Token: 0x06000F99 RID: 3993 RVA: 0x0002B254 File Offset: 0x00029454
		public LineFormation(IFormation ownerFormation, bool isStaggered = true)
		{
			this.owner = ownerFormation;
			this.IsStaggered = isStaggered;
			this._units2D = new MBList2D<IFormationUnit>(1, 1);
			this.UnitPositionAvailabilities = new MBList2D<int>(1, 1);
			this._globalPositions = new MBList2D<WorldPosition>(1, 1);
			this._units2DWorkspace = new MBList2D<IFormationUnit>(1, 1);
			this._unitPositionAvailabilitiesWorkspace = new MBList2D<int>(1, 1);
			this._globalPositionsWorkspace = new MBList2D<WorldPosition>(1, 1);
			this._cachedOrderedUnitPositionIndices = new MBArrayList<Vec2i>();
			this._cachedOrderedAndAvailableUnitPositionIndices = new MBArrayList<Vec2i>();
			this._cachedOrderedLocalPositions = new MBArrayList<Vec2>();
			this._unpositionedUnits = new MBList<IFormationUnit>();
			this._displacedUnitsWorkspace = new MBWorkspace<MBQueue<ValueTuple<IFormationUnit, int, int>>>();
			this._finalOccupationsWorkspace = new MBWorkspace<MBArrayList<Vec2i>>();
			this._filledInUnitPositionsWorkspace = new MBWorkspace<MBArrayList<Vec2i>>();
			this._toBeFilledInGapsWorkspace = new MBWorkspace<MBQueue<Vec2i>>();
			this._finalVacanciesWorkspace = new MBWorkspace<MBArrayList<Vec2i>>();
			this._filledInGapsWorkspace = new MBWorkspace<MBArrayList<Vec2i>>();
			this._toBeEmptiedOutUnitPositionsWorkspace = new MBWorkspace<MBArrayList<Vec2i>>();
			this.ReconstructUnitsFromUnits2D();
			Action onShapeChanged = this.OnShapeChanged;
			if (onShapeChanged == null)
			{
				return;
			}
			onShapeChanged();
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x0002B364 File Offset: 0x00029564
		protected LineFormation(IFormation ownerFormation, bool isDeformingOnWidthChange, bool isStaggered = true) : this(ownerFormation, isStaggered)
		{
			this._isDeformingOnWidthChange = isDeformingOnWidthChange;
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x0002B375 File Offset: 0x00029575
		public virtual IFormationArrangement Clone(IFormation formation)
		{
			return new LineFormation(formation, this._isDeformingOnWidthChange, this.IsStaggered);
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x0002B38C File Offset: 0x0002958C
		public virtual void DeepCopyFrom(IFormationArrangement arrangement)
		{
			LineFormation lineFormation = arrangement as LineFormation;
			this.IsStaggered = lineFormation.IsStaggered;
			this.IsTransforming = lineFormation.IsTransforming;
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x0002B3B8 File Offset: 0x000295B8
		public void Reset()
		{
			this._units2D = new MBList2D<IFormationUnit>(1, 1);
			this.UnitPositionAvailabilities = new MBList2D<int>(1, 1);
			this._globalPositions = new MBList2D<WorldPosition>(1, 1);
			this._units2DWorkspace = new MBList2D<IFormationUnit>(1, 1);
			this._unitPositionAvailabilitiesWorkspace = new MBList2D<int>(1, 1);
			this._globalPositionsWorkspace = new MBList2D<WorldPosition>(1, 1);
			this._cachedOrderedUnitPositionIndices = new MBArrayList<Vec2i>();
			this._cachedOrderedAndAvailableUnitPositionIndices = new MBArrayList<Vec2i>();
			this._cachedOrderedLocalPositions = new MBArrayList<Vec2>();
			this._unpositionedUnits.Clear();
			this.ReconstructUnitsFromUnits2D();
			Action onShapeChanged = this.OnShapeChanged;
			if (onShapeChanged == null)
			{
				return;
			}
			onShapeChanged();
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x0002B458 File Offset: 0x00029658
		protected virtual bool IsUnitPositionRestrained(int fileIndex, int rankIndex)
		{
			if (this._isMiddleFrontUnitPositionReserved)
			{
				Vec2i middleFrontUnitPosition = this.GetMiddleFrontUnitPosition();
				return fileIndex == middleFrontUnitPosition.Item1 && rankIndex == middleFrontUnitPosition.Item2;
			}
			return false;
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x0002B490 File Offset: 0x00029690
		protected virtual void MakeRestrainedPositionsUnavailable()
		{
			if (this._isMiddleFrontUnitPositionReserved)
			{
				Vec2i middleFrontUnitPosition = this.GetMiddleFrontUnitPosition();
				this.UnitPositionAvailabilities[middleFrontUnitPosition.Item1, middleFrontUnitPosition.Item2] = 1;
			}
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x0002B4C6 File Offset: 0x000296C6
		protected IFormationUnit GetUnitAt(int fileIndex, int rankIndex)
		{
			return this._units2D[fileIndex, rankIndex];
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x0002B4D5 File Offset: 0x000296D5
		public bool IsUnitPositionAvailable(int fileIndex, int rankIndex)
		{
			return this.UnitPositionAvailabilities[fileIndex, rankIndex] == 2;
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x0002B4E8 File Offset: 0x000296E8
		private Vec2i GetNearestAvailableNeighbourPositionIndex(int fileIndex, int rankIndex)
		{
			for (int i = 1; i < this.FileCount + this.RankCount; i++)
			{
				bool flag = true;
				bool flag2 = true;
				bool flag3 = true;
				bool flag4 = true;
				int num = 0;
				int num2 = 0;
				while (num2 <= i && (flag || flag2 || flag3 || flag4))
				{
					int num3 = i - num2;
					num = num2;
					int num4 = fileIndex - num2;
					int num5 = fileIndex + num2;
					int num6 = rankIndex - num3;
					int num7 = rankIndex + num3;
					if (flag && (num4 < 0 || num6 < 0))
					{
						flag = false;
					}
					if (flag3 && (num4 < 0 || num7 >= this.RankCount))
					{
						flag3 = false;
					}
					if (flag2 && (num5 >= this.FileCount || num6 < 0))
					{
						flag2 = false;
					}
					if (flag4 && (num5 >= this.FileCount || num7 >= this.RankCount))
					{
						flag4 = false;
					}
					if (flag && this.UnitPositionAvailabilities[num4, num6] == 2)
					{
						return new Vec2i(num4, num6);
					}
					if (flag3 && this.UnitPositionAvailabilities[num4, num7] == 2)
					{
						return new Vec2i(num4, num7);
					}
					if (flag2 && this.UnitPositionAvailabilities[num5, num6] == 2)
					{
						return new Vec2i(num5, num6);
					}
					if (flag4 && this.UnitPositionAvailabilities[num5, num7] == 2)
					{
						return new Vec2i(num5, num7);
					}
					num2++;
				}
				flag2 = (flag = (flag3 = (flag4 = true)));
				int num8 = 0;
				while (num8 < i - num && (flag || flag2 || flag3 || flag4))
				{
					int num9 = i - num8;
					int num10 = fileIndex - num9;
					int num11 = fileIndex + num9;
					int num12 = rankIndex - num8;
					int num13 = rankIndex + num8;
					if (flag && (num10 < 0 || num12 < 0))
					{
						flag = false;
					}
					if (flag3 && (num10 < 0 || num13 >= this.RankCount))
					{
						flag3 = false;
					}
					if (flag2 && (num11 >= this.FileCount || num12 < 0))
					{
						flag2 = false;
					}
					if (flag4 && (num11 >= this.FileCount || num13 >= this.RankCount))
					{
						flag4 = false;
					}
					if (flag && this.UnitPositionAvailabilities[num10, num12] == 2)
					{
						return new Vec2i(num10, num12);
					}
					if (flag3 && this.UnitPositionAvailabilities[num10, num13] == 2)
					{
						return new Vec2i(num10, num13);
					}
					if (flag2 && this.UnitPositionAvailabilities[num11, num12] == 2)
					{
						return new Vec2i(num11, num12);
					}
					if (flag4 && this.UnitPositionAvailabilities[num11, num13] == 2)
					{
						return new Vec2i(num11, num13);
					}
					num8++;
				}
			}
			return LineFormation.InvalidPositionIndex;
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x0002B750 File Offset: 0x00029950
		private bool GetNextVacancy(out int fileIndex, out int rankIndex)
		{
			int num = this.FileCount * this.RankCount;
			for (int i = 0; i < num; i++)
			{
				Vec2i orderedUnitPositionIndex = this.GetOrderedUnitPositionIndex(i);
				fileIndex = orderedUnitPositionIndex.Item1;
				rankIndex = orderedUnitPositionIndex.Item2;
				if (this._units2D[fileIndex, rankIndex] == null && this.IsUnitPositionAvailable(fileIndex, rankIndex))
				{
					return true;
				}
			}
			fileIndex = -1;
			rankIndex = -1;
			return false;
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x0002B7B8 File Offset: 0x000299B8
		private IFormationUnit GetLastUnit()
		{
			int num = -1;
			IFormationUnit result = null;
			foreach (IFormationUnit formationUnit in this._allUnits)
			{
				int formationFileIndex = formationUnit.FormationFileIndex;
				int formationRankIndex = formationUnit.FormationRankIndex;
				int num2 = formationFileIndex + formationRankIndex;
				if (num2 > num)
				{
					num = num2;
					result = formationUnit;
				}
			}
			return result;
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x0002B82C File Offset: 0x00029A2C
		private static Vec2i GetOrderedUnitPositionIndexAux(int fileIndexBegin, int fileIndexEnd, int rankIndexBegin, int rankIndexEnd, int unitIndex)
		{
			int num = fileIndexEnd - fileIndexBegin + 1;
			int num2 = unitIndex / num;
			int num3 = unitIndex - num2 * num;
			if (num % 2 == 1)
			{
				num3 = num / 2 + ((num3 % 2 == 0) ? 1 : -1) * (num3 + 1) / 2;
			}
			else
			{
				num3 = num / 2 - 1 + ((num3 % 2 == 0) ? -1 : 1) * (num3 + 1) / 2;
			}
			return new Vec2i(num3 + fileIndexBegin, num2 + rankIndexBegin);
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x0002B888 File Offset: 0x00029A88
		private Vec2i GetOrderedUnitPositionIndex(int unitIndex)
		{
			return LineFormation.GetOrderedUnitPositionIndexAux(0, this.FileCount - 1, 0, this.RankCount - 1, unitIndex);
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x0002B8A2 File Offset: 0x00029AA2
		private static IEnumerable<Vec2i> GetOrderedUnitPositionIndicesAux(int fileIndexBegin, int fileIndexEnd, int rankIndexBegin, int rankIndexEnd)
		{
			int fileCount = fileIndexEnd - fileIndexBegin + 1;
			if (fileCount % 2 == 1)
			{
				int centerFileIndex = fileCount / 2;
				int num;
				for (int rankIndex = rankIndexBegin; rankIndex <= rankIndexEnd; rankIndex = num + 1)
				{
					yield return new Vec2i(fileIndexBegin + centerFileIndex, rankIndex);
					for (int fileIndexOffset = 1; fileIndexOffset <= centerFileIndex; fileIndexOffset = num + 1)
					{
						yield return new Vec2i(fileIndexBegin + centerFileIndex - fileIndexOffset, rankIndex);
						if (centerFileIndex + fileIndexOffset < fileCount)
						{
							yield return new Vec2i(fileIndexBegin + centerFileIndex + fileIndexOffset, rankIndex);
						}
						num = fileIndexOffset;
					}
					num = rankIndex;
				}
			}
			else
			{
				int centerFileIndex = fileCount / 2 - 1;
				int num;
				for (int rankIndex = rankIndexBegin; rankIndex <= rankIndexEnd; rankIndex = num + 1)
				{
					yield return new Vec2i(fileIndexBegin + centerFileIndex, rankIndex);
					for (int fileIndexOffset = 1; fileIndexOffset <= centerFileIndex + 1; fileIndexOffset = num + 1)
					{
						yield return new Vec2i(fileIndexBegin + centerFileIndex + fileIndexOffset, rankIndex);
						if (centerFileIndex - fileIndexOffset >= 0)
						{
							yield return new Vec2i(fileIndexBegin + centerFileIndex - fileIndexOffset, rankIndex);
						}
						num = fileIndexOffset;
					}
					num = rankIndex;
				}
			}
			yield break;
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x0002B8C7 File Offset: 0x00029AC7
		private IEnumerable<Vec2i> GetOrderedUnitPositionIndices()
		{
			return LineFormation.GetOrderedUnitPositionIndicesAux(0, this.FileCount - 1, 0, this.RankCount - 1);
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x0002B8E0 File Offset: 0x00029AE0
		public Vec2? GetLocalPositionOfUnitOrDefault(int unitIndex)
		{
			Vec2i a = (unitIndex < this._cachedOrderedAndAvailableUnitPositionIndices.Count) ? this._cachedOrderedAndAvailableUnitPositionIndices.ElementAt(unitIndex) : LineFormation.InvalidPositionIndex;
			Vec2? result;
			if (a != LineFormation.InvalidPositionIndex)
			{
				int item = a.Item1;
				int item2 = a.Item2;
				result = new Vec2?(this.GetLocalPositionOfUnit(item, item2));
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x0002B946 File Offset: 0x00029B46
		public Vec2? GetLocalDirectionOfUnitOrDefault(int unitIndex)
		{
			return new Vec2?(Vec2.Forward);
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x0002B954 File Offset: 0x00029B54
		public WorldPosition? GetWorldPositionOfUnitOrDefault(int unitIndex)
		{
			Vec2i a = (unitIndex < this._cachedOrderedAndAvailableUnitPositionIndices.Count) ? this._cachedOrderedAndAvailableUnitPositionIndices.ElementAt(unitIndex) : LineFormation.InvalidPositionIndex;
			WorldPosition? result;
			if (a != LineFormation.InvalidPositionIndex)
			{
				int item = a.Item1;
				int item2 = a.Item2;
				result = new WorldPosition?(this._globalPositions[item, item2]);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x0002B9BF File Offset: 0x00029BBF
		public IEnumerable<Vec2> GetUnavailableUnitPositions()
		{
			int num;
			for (int fileIndex = 0; fileIndex < this.FileCount; fileIndex = num + 1)
			{
				for (int rankIndex = 0; rankIndex < this.RankCount; rankIndex = num + 1)
				{
					if (this.UnitPositionAvailabilities[fileIndex, rankIndex] == 1 && !this.IsUnitPositionRestrained(fileIndex, rankIndex))
					{
						yield return this.GetLocalPositionOfUnit(fileIndex, rankIndex);
					}
					num = rankIndex;
				}
				num = fileIndex;
			}
			yield break;
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x0002B9CF File Offset: 0x00029BCF
		private void InsertUnit(IFormationUnit unit, int fileIndex, int rankIndex)
		{
			unit.FormationFileIndex = fileIndex;
			unit.FormationRankIndex = rankIndex;
			this._units2D[fileIndex, rankIndex] = unit;
			this.ReconstructUnitsFromUnits2D();
			Action onShapeChanged = this.OnShapeChanged;
			if (onShapeChanged == null)
			{
				return;
			}
			onShapeChanged();
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x0002BA04 File Offset: 0x00029C04
		public bool AddUnit(IFormationUnit unit)
		{
			bool flag = false;
			while (!flag && !this.AreLastRanksCompletelyUnavailable(3))
			{
				int num;
				int num2;
				if (this.GetNextVacancy(out num, out num2))
				{
					unit.FormationFileIndex = num;
					unit.FormationRankIndex = num2;
					this._units2D[num, num2] = unit;
					this.ReconstructUnitsFromUnits2D();
					flag = true;
				}
				else
				{
					if (!this.IsDeepenApplicable())
					{
						break;
					}
					this.Deepen();
				}
			}
			if (!flag)
			{
				this._unpositionedUnits.Add(unit);
				this.ReconstructUnitsFromUnits2D();
			}
			if (flag)
			{
				if (this.FileCount < this.MinimumFileCount)
				{
					LineFormation.WidenFormation(this, this.MinimumFileCount - this.FileCount);
				}
				Action onShapeChanged = this.OnShapeChanged;
				if (onShapeChanged != null)
				{
					onShapeChanged();
				}
				if (unit is Agent)
				{
					bool hasMount = (unit as Agent).HasMount;
					if ((this.owner is Formation && (this.owner as Formation).CalculateHasSignificantNumberOfMounted) != this._isCavalry)
					{
						this.BatchUnitPositionAvailabilities(true);
					}
					else if (this._isCavalry != hasMount && this.owner is Formation)
					{
						(this.owner as Formation).QuerySystem.ForceExpireCavalryUnitRatio();
						if ((this.owner as Formation).CalculateHasSignificantNumberOfMounted != this._isCavalry)
						{
							this.BatchUnitPositionAvailabilities(true);
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x0002BB41 File Offset: 0x00029D41
		public void RemoveUnit(IFormationUnit unit)
		{
			if (this._unpositionedUnits.Remove(unit))
			{
				this.ReconstructUnitsFromUnits2D();
				return;
			}
			this.RemoveUnit(unit, true, false);
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x0002BB61 File Offset: 0x00029D61
		public IFormationUnit GetUnit(int fileIndex, int rankIndex)
		{
			return this._units2D[fileIndex, rankIndex];
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x0002BB72 File Offset: 0x00029D72
		public void OnBatchRemoveStart()
		{
			if (this._isBatchRemovingUnits)
			{
				return;
			}
			this._isBatchRemovingUnits = true;
			this._gapFillMinRanksPerFileForBatchRemove.Clear();
			this._batchRemoveInvolvesUnavailablePositions = false;
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x0002BB98 File Offset: 0x00029D98
		public void OnBatchRemoveEnd()
		{
			if (!this._isBatchRemovingUnits)
			{
				return;
			}
			if (this._gapFillMinRanksPerFileForBatchRemove.Count > 0)
			{
				for (int i = 0; i < this._gapFillMinRanksPerFileForBatchRemove.Count; i++)
				{
					int num = this._gapFillMinRanksPerFileForBatchRemove[i];
					if (i < this.FileCount && num < this.RankCount)
					{
						LineFormation.FillInTheGapsOfFile(this, i, num, true);
					}
				}
				this.FillInTheGapsOfFormationAfterRemove(this._batchRemoveInvolvesUnavailablePositions);
				this._gapFillMinRanksPerFileForBatchRemove.Clear();
			}
			this._isBatchRemovingUnits = false;
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x0002BC18 File Offset: 0x00029E18
		public List<IFormationUnit> GetUnitsToPop(int count)
		{
			List<IFormationUnit> list = new List<IFormationUnit>();
			if (this._unpositionedUnits.Count > 0)
			{
				int num = Math.Min(count, this._unpositionedUnits.Count);
				list.AddRange(this._unpositionedUnits.Take(num));
				count -= num;
			}
			if (count > 0)
			{
				for (int i = this.FileCount * this.RankCount - 1; i >= 0; i--)
				{
					Vec2i orderedUnitPositionIndex = this.GetOrderedUnitPositionIndex(i);
					int item = orderedUnitPositionIndex.Item1;
					int item2 = orderedUnitPositionIndex.Item2;
					if (this._units2D[item, item2] != null)
					{
						list.Add(this._units2D[item, item2]);
						count--;
						if (count == 0)
						{
							break;
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x0002BCCC File Offset: 0x00029ECC
		private void PickUnitsWithRespectToPosition(Agent agent, float distanceSquared, ref LinkedList<Tuple<IFormationUnit, float>> collection, ref List<IFormationUnit> chosenUnits, int countToChoose, bool chooseClosest)
		{
			if (collection.Count < countToChoose)
			{
				LinkedListNode<Tuple<IFormationUnit, float>> linkedListNode = null;
				for (LinkedListNode<Tuple<IFormationUnit, float>> linkedListNode2 = collection.First; linkedListNode2 != null; linkedListNode2 = linkedListNode2.Next)
				{
					if (chooseClosest ? (linkedListNode2.Value.Item2 < distanceSquared) : (linkedListNode2.Value.Item2 > distanceSquared))
					{
						linkedListNode = linkedListNode2;
						break;
					}
				}
				if (linkedListNode != null)
				{
					collection.AddBefore(linkedListNode, new LinkedListNode<Tuple<IFormationUnit, float>>(new Tuple<IFormationUnit, float>(agent, distanceSquared)));
					return;
				}
				collection.AddLast(new LinkedListNode<Tuple<IFormationUnit, float>>(new Tuple<IFormationUnit, float>(agent, distanceSquared)));
				return;
			}
			else
			{
				if (chooseClosest ? (distanceSquared < collection.First<Tuple<IFormationUnit, float>>().Item2) : (distanceSquared > collection.First<Tuple<IFormationUnit, float>>().Item2))
				{
					LinkedListNode<Tuple<IFormationUnit, float>> linkedListNode3 = null;
					for (LinkedListNode<Tuple<IFormationUnit, float>> next = collection.First.Next; next != null; next = next.Next)
					{
						if (chooseClosest ? (next.Value.Item2 < distanceSquared) : (next.Value.Item2 > distanceSquared))
						{
							linkedListNode3 = next;
							break;
						}
					}
					if (linkedListNode3 != null)
					{
						collection.AddBefore(linkedListNode3, new LinkedListNode<Tuple<IFormationUnit, float>>(new Tuple<IFormationUnit, float>(agent, distanceSquared)));
					}
					else
					{
						collection.AddLast(new LinkedListNode<Tuple<IFormationUnit, float>>(new Tuple<IFormationUnit, float>(agent, distanceSquared)));
					}
					if (!chooseClosest)
					{
						chosenUnits.Add(collection.First<Tuple<IFormationUnit, float>>().Item1);
					}
					collection.RemoveFirst();
					return;
				}
				if (!chooseClosest)
				{
					chosenUnits.Add(agent);
				}
				return;
			}
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x0002BE13 File Offset: 0x0002A013
		public IEnumerable<IFormationUnit> GetUnitsToPopWithCondition(int count, Func<IFormationUnit, bool> currentCondition)
		{
			IEnumerable<IFormationUnit> unpositionedUnits = this._unpositionedUnits;
			Func<IFormationUnit, bool> <>9__0;
			Func<IFormationUnit, bool> predicate;
			if ((predicate = <>9__0) == null)
			{
				predicate = (<>9__0 = ((IFormationUnit uu) => currentCondition(uu)));
			}
			int num;
			foreach (IFormationUnit formationUnit in unpositionedUnits.Where(predicate))
			{
				yield return formationUnit;
				num = count;
				count = num - 1;
				if (count == 0)
				{
					yield break;
				}
			}
			IEnumerator<IFormationUnit> enumerator = null;
			for (int i = this.FileCount * this.RankCount - 1; i >= 0; i = num - 1)
			{
				Vec2i orderedUnitPositionIndex = this.GetOrderedUnitPositionIndex(i);
				int item = orderedUnitPositionIndex.Item1;
				int item2 = orderedUnitPositionIndex.Item2;
				if (this._units2D[item, item2] != null && currentCondition(this._units2D[item, item2]))
				{
					yield return this._units2D[item, item2];
					num = count;
					count = num - 1;
					if (count == 0)
					{
						yield break;
					}
				}
				num = i;
			}
			yield break;
			yield break;
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x0002BE34 File Offset: 0x0002A034
		private void TryToKeepDepth()
		{
			if (this.FileCount > this.MinimumFileCount)
			{
				int num = this.CountUnitsAtRank(this.RankCount - 1);
				int num2 = this.RankCount - 1;
				if (num + num2 <= this.FileCount && MBRandom.RandomInt(this.RankCount * 2) == 0 && this.IsNarrowApplicable((this.FileCount > 2) ? 2 : 1))
				{
					LineFormation.NarrowFormation(this, (this.FileCount > 2) ? 2 : 1);
				}
			}
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x0002BEA8 File Offset: 0x0002A0A8
		public List<IFormationUnit> GetUnitsToPop(int count, Vec3 targetPosition)
		{
			List<IFormationUnit> list = new List<IFormationUnit>();
			if (this._unpositionedUnits.Count > 0)
			{
				int num = Math.Min(count, this._unpositionedUnits.Count);
				if (num < this._unpositionedUnits.Count)
				{
					LinkedList<Tuple<IFormationUnit, float>> linkedList = new LinkedList<Tuple<IFormationUnit, float>>();
					bool flag = (float)num <= (float)this._unpositionedUnits.Count * 0.5f;
					int num2 = flag ? num : (this._unpositionedUnits.Count - num);
					for (int i = 0; i < this._unpositionedUnits.Count; i++)
					{
						Agent agent = this._unpositionedUnits[i] as Agent;
						if (agent == null)
						{
							if (flag)
							{
								linkedList.AddFirst(new Tuple<IFormationUnit, float>(this._unpositionedUnits[i], float.MinValue));
								if (linkedList.Count > num)
								{
									linkedList.RemoveLast();
								}
							}
							else if (linkedList.Count < num2)
							{
								linkedList.AddLast(new Tuple<IFormationUnit, float>(this._unpositionedUnits[i], float.MinValue));
							}
							else
							{
								list.Add(this._unpositionedUnits[i]);
							}
						}
						else
						{
							float distanceSquared = agent.Position.DistanceSquared(targetPosition);
							this.PickUnitsWithRespectToPosition(agent, distanceSquared, ref linkedList, ref list, num2, flag);
						}
					}
					if (flag)
					{
						list.AddRange(from tuple in linkedList
						select tuple.Item1);
					}
					count -= num;
				}
				else
				{
					list.AddRange(this._unpositionedUnits.Take(num));
					count -= num;
				}
			}
			if (count > 0)
			{
				int num3 = count;
				int num4 = this.UnitCount - this._unpositionedUnits.Count;
				bool flag2 = num4 == num3;
				bool flag3 = (float)count <= (float)num4 * 0.5f;
				LinkedList<Tuple<IFormationUnit, float>> linkedList2 = flag2 ? null : new LinkedList<Tuple<IFormationUnit, float>>();
				int num5 = flag3 ? num3 : (num4 - num3);
				for (int j = this.FileCount * this.RankCount - 1; j >= 0; j--)
				{
					Vec2i orderedUnitPositionIndex = this.GetOrderedUnitPositionIndex(j);
					int item = orderedUnitPositionIndex.Item1;
					int item2 = orderedUnitPositionIndex.Item2;
					if (this._units2D[item, item2] != null)
					{
						if (flag2)
						{
							list.Add(this._units2D[item, item2]);
							count--;
							if (count == 0)
							{
								break;
							}
						}
						else
						{
							Agent agent2 = this._units2D[item, item2] as Agent;
							if (agent2 == null)
							{
								if (flag3)
								{
									linkedList2.AddFirst(new Tuple<IFormationUnit, float>(this._unpositionedUnits[j], float.MinValue));
									if (linkedList2.Count > num3)
									{
										linkedList2.RemoveLast();
									}
								}
								else if (linkedList2.Count < num5)
								{
									linkedList2.AddLast(new Tuple<IFormationUnit, float>(this._unpositionedUnits[j], float.MinValue));
								}
								else
								{
									list.Add(this._unpositionedUnits[j]);
								}
							}
							else
							{
								float distanceSquared2 = agent2.Position.DistanceSquared(targetPosition);
								this.PickUnitsWithRespectToPosition(agent2, distanceSquared2, ref linkedList2, ref list, num5, flag3);
							}
						}
					}
				}
				if (!flag2 && flag3)
				{
					list.AddRange(from tuple in linkedList2
					select tuple.Item1);
				}
			}
			return list;
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x0002C200 File Offset: 0x0002A400
		private void RemoveUnit(IFormationUnit unit, bool fillInTheGap, bool isRemovingFromAnUnavailablePosition = false)
		{
			if (fillInTheGap)
			{
			}
			int formationFileIndex = unit.FormationFileIndex;
			int formationRankIndex = unit.FormationRankIndex;
			if (unit.FormationFileIndex < 0 || unit.FormationRankIndex < 0 || unit.FormationFileIndex >= this.FileCount || unit.FormationRankIndex >= this.RankCount)
			{
				object[] array = new object[12];
				array[0] = "Unit removed has file-rank indices: ";
				array[1] = unit.FormationFileIndex;
				array[2] = " ";
				array[3] = unit.FormationRankIndex;
				array[4] = " while line formation has file-rank counts of ";
				array[5] = this.FileCount;
				array[6] = " ";
				array[7] = this.RankCount;
				array[8] = " agent state is ";
				int num = 9;
				Agent agent = unit as Agent;
				array[num] = ((agent != null) ? new AgentState?(agent.State) : null);
				array[10] = " unit detachment is ";
				int num2 = 11;
				Agent agent2 = unit as Agent;
				array[num2] = ((agent2 != null) ? agent2.Detachment : null);
				Debug.Print(string.Concat(array), 0, Debug.DebugColor.White, 17592186044416UL);
			}
			this._units2D[unit.FormationFileIndex, unit.FormationRankIndex] = null;
			this.ReconstructUnitsFromUnits2D();
			unit.FormationFileIndex = -1;
			unit.FormationRankIndex = -1;
			if (fillInTheGap)
			{
				if (this._isBatchRemovingUnits)
				{
					int num3 = formationFileIndex - this._gapFillMinRanksPerFileForBatchRemove.Count + 1;
					for (int i = 0; i < num3; i++)
					{
						this._gapFillMinRanksPerFileForBatchRemove.Add(int.MaxValue);
					}
					this._gapFillMinRanksPerFileForBatchRemove[formationFileIndex] = MathF.Min(formationRankIndex, this._gapFillMinRanksPerFileForBatchRemove[formationFileIndex]);
					this._batchRemoveInvolvesUnavailablePositions = (this._batchRemoveInvolvesUnavailablePositions || isRemovingFromAnUnavailablePosition);
				}
				else
				{
					LineFormation.FillInTheGapsOfFile(this, formationFileIndex, formationRankIndex, true);
					this.FillInTheGapsOfFormationAfterRemove(isRemovingFromAnUnavailablePosition);
				}
			}
			Action onShapeChanged = this.OnShapeChanged;
			if (onShapeChanged == null)
			{
				return;
			}
			onShapeChanged();
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x0002C3CC File Offset: 0x0002A5CC
		protected virtual bool TryGetUnitPositionIndexFromLocalPosition(Vec2 localPosition, out int fileIndex, out int rankIndex)
		{
			rankIndex = MathF.Round(-localPosition.y / (this.Distance + this.UnitDiameter));
			if (rankIndex >= this.RankCount)
			{
				fileIndex = -1;
				return false;
			}
			if (this.IsStaggered && rankIndex % 2 == 1)
			{
				localPosition.x -= (this.Interval + this.UnitDiameter) * 0.5f;
			}
			float num = (float)(this.FileCount - 1) * (this.Interval + this.UnitDiameter);
			fileIndex = MathF.Round((localPosition.x + num / 2f) / (this.Interval + this.UnitDiameter));
			return fileIndex >= 0 && fileIndex < this.FileCount;
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x0002C480 File Offset: 0x0002A680
		protected virtual Vec2 GetLocalPositionOfUnit(int fileIndex, int rankIndex)
		{
			float num = (float)(this.FileCount - 1) * (this.Interval + this.UnitDiameter);
			Vec2 result = new Vec2((float)fileIndex * (this.Interval + this.UnitDiameter) - num / 2f, (float)(-(float)rankIndex) * (this.Distance + this.UnitDiameter));
			if (this.IsStaggered && rankIndex % 2 == 1)
			{
				result.x += (this.Interval + this.UnitDiameter) * 0.5f;
			}
			return result;
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x0002C504 File Offset: 0x0002A704
		protected virtual Vec2 GetLocalPositionOfUnitWithAdjustment(int fileIndex, int rankIndex, float distanceBetweenAgentsAdjustment)
		{
			float num = this.Interval + distanceBetweenAgentsAdjustment;
			float num2 = (float)(this.FileCount - 1) * (num + this.UnitDiameter);
			Vec2 result = new Vec2((float)fileIndex * (num + this.UnitDiameter) - num2 / 2f, (float)(-(float)rankIndex) * (this.Distance + this.UnitDiameter));
			if (this.IsStaggered && rankIndex % 2 == 1)
			{
				result.x += (num + this.UnitDiameter) * 0.5f;
			}
			return result;
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x0002C580 File Offset: 0x0002A780
		protected virtual Vec2 GetLocalDirectionOfUnit(int fileIndex, int rankIndex)
		{
			return Vec2.Forward;
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x0002C588 File Offset: 0x0002A788
		public Vec2? GetLocalPositionOfUnitOrDefault(IFormationUnit unit)
		{
			if (this._unpositionedUnits.Contains(unit))
			{
				return null;
			}
			return new Vec2?(this.GetLocalPositionOfUnit(unit.FormationFileIndex, unit.FormationRankIndex));
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x0002C5C4 File Offset: 0x0002A7C4
		public Vec2? GetLocalPositionOfUnitOrDefaultWithAdjustment(IFormationUnit unit, float distanceBetweenAgentsAdjustment)
		{
			if (this._unpositionedUnits.Contains(unit))
			{
				return null;
			}
			return new Vec2?(this.GetLocalPositionOfUnitWithAdjustment(unit.FormationFileIndex, unit.FormationRankIndex, distanceBetweenAgentsAdjustment));
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x0002C601 File Offset: 0x0002A801
		public virtual Vec2? GetLocalDirectionOfUnitOrDefault(IFormationUnit unit)
		{
			return new Vec2?(Vec2.Forward);
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x0002C610 File Offset: 0x0002A810
		public WorldPosition? GetWorldPositionOfUnitOrDefault(IFormationUnit unit)
		{
			if (this._unpositionedUnits.Contains(unit))
			{
				return null;
			}
			return new WorldPosition?(this._globalPositions[unit.FormationFileIndex, unit.FormationRankIndex]);
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x0002C654 File Offset: 0x0002A854
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
			for (int k = 0; k < this._unpositionedUnits.Count; k++)
			{
				this._allUnits.Add(this._unpositionedUnits[k]);
			}
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x0002C6FF File Offset: 0x0002A8FF
		private void FillInTheGapsOfFormationAfterRemove(bool hasUnavailablePositions)
		{
			this.TryReaddingUnpositionedUnits();
			LineFormation.FillInTheGapsOfMiddleRanks(this, null);
			this.TryToKeepDepth();
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x0002C717 File Offset: 0x0002A917
		private static void WidenFormation(LineFormation formation, int fileCountFromBothFlanks)
		{
			if (fileCountFromBothFlanks % 2 == 0)
			{
				LineFormation.WidenFormation(formation, fileCountFromBothFlanks / 2, fileCountFromBothFlanks / 2);
				return;
			}
			if (formation.FileCount % 2 == 0)
			{
				LineFormation.WidenFormation(formation, fileCountFromBothFlanks / 2 + 1, fileCountFromBothFlanks / 2);
				return;
			}
			LineFormation.WidenFormation(formation, fileCountFromBothFlanks / 2, fileCountFromBothFlanks / 2 + 1);
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x0002C754 File Offset: 0x0002A954
		private static void WidenFormation(LineFormation formation, int fileCountFromLeftFlank, int fileCountFromRightFlank)
		{
			formation._units2DWorkspace.ResetWithNewCount(formation.FileCount + fileCountFromLeftFlank + fileCountFromRightFlank, formation.RankCount);
			formation._unitPositionAvailabilitiesWorkspace.ResetWithNewCount(formation.FileCount + fileCountFromLeftFlank + fileCountFromRightFlank, formation.RankCount);
			formation._globalPositionsWorkspace.ResetWithNewCount(formation.FileCount + fileCountFromLeftFlank + fileCountFromRightFlank, formation.RankCount);
			for (int i = 0; i < formation.FileCount; i++)
			{
				int destinationIndex = i + fileCountFromLeftFlank;
				formation._units2D.CopyRowTo(i, 0, formation._units2DWorkspace, destinationIndex, 0, formation.RankCount);
				formation.UnitPositionAvailabilities.CopyRowTo(i, 0, formation._unitPositionAvailabilitiesWorkspace, destinationIndex, 0, formation.RankCount);
				formation._globalPositions.CopyRowTo(i, 0, formation._globalPositionsWorkspace, destinationIndex, 0, formation.RankCount);
				if (fileCountFromLeftFlank > 0)
				{
					for (int j = 0; j < formation.RankCount; j++)
					{
						if (formation._units2D[i, j] != null)
						{
							formation._units2D[i, j].FormationFileIndex += fileCountFromLeftFlank;
						}
					}
				}
			}
			MBList2D<IFormationUnit> units2D = formation._units2D;
			formation._units2D = formation._units2DWorkspace;
			formation._units2DWorkspace = units2D;
			formation.ReconstructUnitsFromUnits2D();
			MBList2D<int> unitPositionAvailabilities = formation.UnitPositionAvailabilities;
			formation.UnitPositionAvailabilities = formation._unitPositionAvailabilitiesWorkspace;
			formation._unitPositionAvailabilitiesWorkspace = unitPositionAvailabilities;
			MBList2D<WorldPosition> globalPositions = formation._globalPositions;
			formation._globalPositions = formation._globalPositionsWorkspace;
			formation._globalPositionsWorkspace = globalPositions;
			formation.BatchUnitPositionAvailabilities(true);
			if (formation._isDeformingOnWidthChange || (fileCountFromLeftFlank + fileCountFromRightFlank) % 2 == 1)
			{
				formation.OnFormationFrameChanged();
			}
			else
			{
				LineFormation.ShiftUnitsForwardsForWideningFormation(formation);
				formation.TryReaddingUnpositionedUnits();
				while (formation.RankCount > 1 && formation.IsRankEmpty(formation.RankCount - 1))
				{
					formation.Shorten();
				}
			}
			Action onShapeChanged = formation.OnShapeChanged;
			if (onShapeChanged == null)
			{
				return;
			}
			onShapeChanged();
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x0002C918 File Offset: 0x0002AB18
		private static void GetToBeFilledInAndToBeEmptiedOutUnitPositions(LineFormation formation, MBQueue<Vec2i> toBeFilledInUnitPositions, MBArrayList<Vec2i> toBeEmptiedOutUnitPositions)
		{
			int num = 0;
			int num2 = formation.FileCount * formation.RankCount - 1;
			for (;;)
			{
				Vec2i orderedUnitPositionIndex = formation.GetOrderedUnitPositionIndex(num);
				int item = orderedUnitPositionIndex.Item1;
				int item2 = orderedUnitPositionIndex.Item2;
				Vec2i orderedUnitPositionIndex2 = formation.GetOrderedUnitPositionIndex(num2);
				int item3 = orderedUnitPositionIndex2.Item1;
				int item4 = orderedUnitPositionIndex2.Item2;
				if (item2 >= item4)
				{
					break;
				}
				if (formation._units2D[item, item2] != null || !formation.IsUnitPositionAvailable(item, item2))
				{
					num++;
				}
				else if (formation._units2D[item3, item4] == null)
				{
					num2--;
				}
				else
				{
					toBeFilledInUnitPositions.Enqueue(new Vec2i(item, item2));
					toBeEmptiedOutUnitPositions.Add(new Vec2i(item3, item4));
					num++;
					num2--;
				}
			}
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x0002C9D3 File Offset: 0x0002ABD3
		private static Vec2i GetUnitPositionForFillInFromNearby(LineFormation formation, int relocationFileIndex, int relocationRankIndex, Func<LineFormation, int, int, bool> predicate, bool isRelocationUnavailable = false)
		{
			return LineFormation.GetUnitPositionForFillInFromNearby(formation, relocationFileIndex, relocationRankIndex, predicate, LineFormation.InvalidPositionIndex, isRelocationUnavailable);
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x0002C9E8 File Offset: 0x0002ABE8
		private static Vec2i GetUnitPositionForFillInFromNearby(LineFormation formation, int relocationFileIndex, int relocationRankIndex, Func<LineFormation, int, int, bool> predicate, Vec2i lastFinalOccupation, bool isRelocationUnavailable = false)
		{
			int fileCount = formation.FileCount;
			int rankCount = formation.RankCount;
			bool flag = relocationFileIndex >= fileCount / 2;
			if (lastFinalOccupation != LineFormation.InvalidPositionIndex)
			{
				flag = (lastFinalOccupation.Item1 <= relocationFileIndex);
			}
			for (int i = 1; i <= fileCount + rankCount; i++)
			{
				for (int j = MathF.Min(i, rankCount - 1 - relocationRankIndex); j >= 0; j--)
				{
					int num = i - j;
					if (flag && relocationFileIndex - num >= 0 && predicate(formation, relocationFileIndex - num, relocationRankIndex + j))
					{
						return new Vec2i(relocationFileIndex - num, relocationRankIndex + j);
					}
					if (relocationFileIndex + num < fileCount && predicate(formation, relocationFileIndex + num, relocationRankIndex + j))
					{
						return new Vec2i(relocationFileIndex + num, relocationRankIndex + j);
					}
					if (!flag && relocationFileIndex - num >= 0 && predicate(formation, relocationFileIndex - num, relocationRankIndex + j))
					{
						return new Vec2i(relocationFileIndex - num, relocationRankIndex + j);
					}
				}
			}
			return LineFormation.InvalidPositionIndex;
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x0002CAE0 File Offset: 0x0002ACE0
		private static void ShiftUnitsForwardsForWideningFormation(LineFormation formation)
		{
			MBQueue<Vec2i> mbqueue = formation._toBeFilledInGapsWorkspace.StartUsingWorkspace();
			MBArrayList<Vec2i> mbarrayList = formation._finalVacanciesWorkspace.StartUsingWorkspace();
			MBArrayList<Vec2i> mbarrayList2 = formation._filledInGapsWorkspace.StartUsingWorkspace();
			LineFormation.GetToBeFilledInAndToBeEmptiedOutUnitPositions(formation, mbqueue, mbarrayList);
			if (formation._shiftUnitsForwardsPredicateDelegate == null)
			{
				formation._shiftUnitsForwardsPredicateDelegate = new Func<LineFormation, int, int, bool>(LineFormation.<>c.<>9.<ShiftUnitsForwardsForWideningFormation>g__ShiftUnitForwardsPredicate|127_0);
			}
			while (mbqueue.Count > 0)
			{
				Vec2i item = mbqueue.Dequeue();
				Vec2i unitPositionForFillInFromNearby = LineFormation.GetUnitPositionForFillInFromNearby(formation, item.Item1, item.Item2, formation._shiftUnitsForwardsPredicateDelegate, false);
				if (unitPositionForFillInFromNearby != LineFormation.InvalidPositionIndex)
				{
					int item2 = unitPositionForFillInFromNearby.Item1;
					int item3 = unitPositionForFillInFromNearby.Item2;
					IFormationUnit unit = formation._units2D[item2, item3];
					formation.RelocateUnit(unit, item.Item1, item.Item2);
					mbarrayList2.Add(item);
					Vec2i item4 = new Vec2i(item2, item3);
					if (!mbarrayList.Contains(item4))
					{
						mbqueue.Enqueue(item4);
					}
				}
			}
			formation._toBeFilledInGapsWorkspace.StopUsingWorkspace();
			formation._finalVacanciesWorkspace.StopUsingWorkspace();
			formation._filledInGapsWorkspace.StopUsingWorkspace();
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x0002CBFC File Offset: 0x0002ADFC
		private static void DeepenFormation(LineFormation formation, int rankCountFromFront, int rankCountFromRear)
		{
			formation._units2DWorkspace.ResetWithNewCount(formation.FileCount, formation.RankCount + rankCountFromFront + rankCountFromRear);
			formation._unitPositionAvailabilitiesWorkspace.ResetWithNewCount(formation.FileCount, formation.RankCount + rankCountFromFront + rankCountFromRear);
			formation._globalPositionsWorkspace.ResetWithNewCount(formation.FileCount, formation.RankCount + rankCountFromFront + rankCountFromRear);
			for (int i = 0; i < formation.FileCount; i++)
			{
				formation._units2D.CopyRowTo(i, 0, formation._units2DWorkspace, i, rankCountFromFront, formation.RankCount);
				formation.UnitPositionAvailabilities.CopyRowTo(i, 0, formation._unitPositionAvailabilitiesWorkspace, i, rankCountFromFront, formation.RankCount);
				formation._globalPositions.CopyRowTo(i, 0, formation._globalPositionsWorkspace, i, rankCountFromFront, formation.RankCount);
				if (rankCountFromFront > 0)
				{
					for (int j = 0; j < formation.RankCount; j++)
					{
						if (formation._units2D[i, j] != null)
						{
							formation._units2D[i, j].FormationRankIndex += rankCountFromFront;
						}
					}
				}
			}
			MBList2D<IFormationUnit> units2D = formation._units2D;
			formation._units2D = formation._units2DWorkspace;
			formation._units2DWorkspace = units2D;
			formation.ReconstructUnitsFromUnits2D();
			MBList2D<int> unitPositionAvailabilities = formation.UnitPositionAvailabilities;
			formation.UnitPositionAvailabilities = formation._unitPositionAvailabilitiesWorkspace;
			formation._unitPositionAvailabilitiesWorkspace = unitPositionAvailabilities;
			MBList2D<WorldPosition> globalPositions = formation._globalPositions;
			formation._globalPositions = formation._globalPositionsWorkspace;
			formation._globalPositionsWorkspace = globalPositions;
			formation.BatchUnitPositionAvailabilities(true);
			Action onShapeChanged = formation.OnShapeChanged;
			if (onShapeChanged == null)
			{
				return;
			}
			onShapeChanged();
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x0002CD71 File Offset: 0x0002AF71
		protected virtual bool IsDeepenApplicable()
		{
			return true;
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x0002CD74 File Offset: 0x0002AF74
		private void Deepen()
		{
			LineFormation.DeepenFormation(this, 0, 1);
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x0002CD80 File Offset: 0x0002AF80
		private static bool DeepenForVacancy(LineFormation formation, int requestedVacancyCount, int fileOffsetFromLeftFlank, int fileOffsetFromRightFlank)
		{
			int num = 0;
			bool? flag = null;
			while (flag == null)
			{
				int num2 = formation.RankCount - 1;
				for (int i = fileOffsetFromLeftFlank; i < formation.FileCount - fileOffsetFromRightFlank; i++)
				{
					if (formation._units2D[i, num2] == null && formation.IsUnitPositionAvailable(i, num2))
					{
						num++;
					}
				}
				if (num >= requestedVacancyCount)
				{
					flag = new bool?(true);
				}
				else if (!formation.AreLastRanksCompletelyUnavailable(3))
				{
					if (formation.IsDeepenApplicable())
					{
						formation.Deepen();
					}
					else
					{
						flag = new bool?(false);
					}
				}
				else
				{
					flag = new bool?(false);
				}
			}
			return flag.Value;
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x0002CE1B File Offset: 0x0002B01B
		protected virtual bool IsNarrowApplicable(int amount)
		{
			return true;
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x0002CE20 File Offset: 0x0002B020
		private static void NarrowFormation(LineFormation formation, int fileCountFromBothFlanks)
		{
			int num = fileCountFromBothFlanks / 2;
			int num2 = fileCountFromBothFlanks / 2;
			if (fileCountFromBothFlanks % 2 != 0)
			{
				if (formation.FileCount % 2 == 0)
				{
					num2++;
				}
				else
				{
					num++;
				}
			}
			if (formation.IsNarrowApplicable(num + num2))
			{
				LineFormation.NarrowFormation(formation, num, num2);
			}
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x0002CE64 File Offset: 0x0002B064
		private static bool ShiftUnitsBackwardsForNewUnavailableUnitPositions(LineFormation formation)
		{
			MBArrayList<Vec2i> mbarrayList = formation._toBeEmptiedOutUnitPositionsWorkspace.StartUsingWorkspace();
			for (int i = 0; i < formation.FileCount * formation.RankCount; i++)
			{
				Vec2i orderedUnitPositionIndex = formation.GetOrderedUnitPositionIndex(i);
				if (formation._units2D[orderedUnitPositionIndex.Item1, orderedUnitPositionIndex.Item2] != null && !formation.IsUnitPositionAvailable(orderedUnitPositionIndex.Item1, orderedUnitPositionIndex.Item2))
				{
					mbarrayList.Add(orderedUnitPositionIndex);
				}
			}
			bool flag = mbarrayList.Count > 0;
			if (flag)
			{
				MBQueue<ValueTuple<IFormationUnit, int, int>> mbqueue = formation._displacedUnitsWorkspace.StartUsingWorkspace();
				for (int j = mbarrayList.Count - 1; j >= 0; j--)
				{
					Vec2i vec2i = mbarrayList[j];
					IFormationUnit formationUnit = formation._units2D[vec2i.Item1, vec2i.Item2];
					if (formationUnit != null)
					{
						formation.RemoveUnit(formationUnit, false, true);
						mbqueue.Enqueue(ValueTuple.Create<IFormationUnit, int, int>(formationUnit, vec2i.Item1, vec2i.Item2));
					}
				}
				LineFormation.DeepenForVacancy(formation, mbqueue.Count, 0, 0);
				MBArrayList<Vec2i> mbarrayList2 = formation._finalOccupationsWorkspace.StartUsingWorkspace();
				int num = 0;
				int num2 = 0;
				while (num2 < formation.FileCount * formation.RankCount && num < mbqueue.Count)
				{
					Vec2i orderedUnitPositionIndex2 = formation.GetOrderedUnitPositionIndex(num2);
					if (formation._units2D[orderedUnitPositionIndex2.Item1, orderedUnitPositionIndex2.Item2] == null && formation.IsUnitPositionAvailable(orderedUnitPositionIndex2.Item1, orderedUnitPositionIndex2.Item2))
					{
						mbarrayList2.Add(orderedUnitPositionIndex2);
						num++;
					}
					num2++;
				}
				LineFormation.ShiftUnitsBackwardsAux(formation, mbqueue, mbarrayList2);
				formation._displacedUnitsWorkspace.StopUsingWorkspace();
				formation._finalOccupationsWorkspace.StopUsingWorkspace();
			}
			formation._toBeEmptiedOutUnitPositionsWorkspace.StopUsingWorkspace();
			return flag;
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x0002D018 File Offset: 0x0002B218
		private static void ShiftUnitsBackwardsForNarrowingFormation(LineFormation formation, int fileCountFromLeftFlank, int fileCountFromRightFlank)
		{
			MBQueue<ValueTuple<IFormationUnit, int, int>> mbqueue = formation._displacedUnitsWorkspace.StartUsingWorkspace();
			foreach (Vec2i vec2i in (from p in formation.GetOrderedUnitPositionIndices()
			where p.Item1 < fileCountFromLeftFlank || p.Item1 >= formation.FileCount - fileCountFromRightFlank
			select p).Reverse<Vec2i>())
			{
				IFormationUnit formationUnit = formation._units2D[vec2i.Item1, vec2i.Item2];
				if (formationUnit != null)
				{
					formation.RemoveUnit(formationUnit, false, false);
					mbqueue.Enqueue(ValueTuple.Create<IFormationUnit, int, int>(formationUnit, vec2i.Item1, vec2i.Item2));
				}
			}
			LineFormation.DeepenForVacancy(formation, mbqueue.Count, fileCountFromLeftFlank, fileCountFromRightFlank);
			IEnumerable<Vec2i> list = (from p in LineFormation.GetOrderedUnitPositionIndicesAux(fileCountFromLeftFlank, formation.FileCount - 1 - fileCountFromRightFlank, 0, formation.RankCount - 1)
			where formation._units2D[p.Item1, p.Item2] == null && formation.IsUnitPositionAvailable(p.Item1, p.Item2)
			select p).Take(mbqueue.Count);
			MBArrayList<Vec2i> mbarrayList = formation._finalOccupationsWorkspace.StartUsingWorkspace();
			mbarrayList.AddRange(list);
			LineFormation.ShiftUnitsBackwardsAux(formation, mbqueue, mbarrayList);
			formation._displacedUnitsWorkspace.StopUsingWorkspace();
			formation._finalOccupationsWorkspace.StopUsingWorkspace();
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x0002D1A8 File Offset: 0x0002B3A8
		private static void ShiftUnitsBackwardsAux(LineFormation formation, MBQueue<ValueTuple<IFormationUnit, int, int>> displacedUnits, MBArrayList<Vec2i> finalOccupations)
		{
			MBArrayList<Vec2i> mbarrayList = formation._filledInUnitPositionsWorkspace.StartUsingWorkspace();
			if (formation._shiftUnitsBackwardsPredicateDelegate == null)
			{
				formation._shiftUnitsBackwardsPredicateDelegate = new Func<LineFormation, int, int, bool>(LineFormation.<>c.<>9.<ShiftUnitsBackwardsAux>g__ShiftUnitsBackwardsPredicate|136_0);
			}
			while (!displacedUnits.IsEmpty<ValueTuple<IFormationUnit, int, int>>())
			{
				ValueTuple<IFormationUnit, int, int> valueTuple = displacedUnits.Dequeue();
				IFormationUnit item = valueTuple.Item1;
				int item2 = valueTuple.Item2;
				int item3 = valueTuple.Item3;
				Vec2i unitPositionForFillInFromNearby = LineFormation.GetUnitPositionForFillInFromNearby(formation, item2, item3, formation._shiftUnitsBackwardsPredicateDelegate, (finalOccupations.Count == 1) ? finalOccupations[0] : LineFormation.InvalidPositionIndex, true);
				if (unitPositionForFillInFromNearby != LineFormation.InvalidPositionIndex)
				{
					IFormationUnit formationUnit = formation._units2D[unitPositionForFillInFromNearby.Item1, unitPositionForFillInFromNearby.Item2];
					if (formationUnit != null)
					{
						formation.RemoveUnit(formationUnit, false, false);
						displacedUnits.Enqueue(ValueTuple.Create<IFormationUnit, int, int>(formationUnit, unitPositionForFillInFromNearby.Item1, unitPositionForFillInFromNearby.Item2));
					}
					mbarrayList.Add(unitPositionForFillInFromNearby);
					formation.InsertUnit(item, unitPositionForFillInFromNearby.Item1, unitPositionForFillInFromNearby.Item2);
				}
				else
				{
					float num = float.MaxValue;
					Vec2i vec2i = LineFormation.InvalidPositionIndex;
					for (int i = 0; i < finalOccupations.Count; i++)
					{
						if (mbarrayList.IndexOf(finalOccupations[i]) < 0)
						{
							float num2 = (float)(MathF.Abs(finalOccupations[i].Item1 - item2) + MathF.Abs(finalOccupations[i].Item2 - item3));
							if (num2 < num)
							{
								num = num2;
								vec2i = finalOccupations[i];
							}
						}
					}
					if (vec2i != LineFormation.InvalidPositionIndex)
					{
						mbarrayList.Add(vec2i);
						formation.InsertUnit(item, vec2i.Item1, vec2i.Item2);
					}
					else
					{
						formation._unpositionedUnits.Add(item);
						formation.ReconstructUnitsFromUnits2D();
					}
				}
			}
			formation._filledInUnitPositionsWorkspace.StopUsingWorkspace();
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x0002D36E File Offset: 0x0002B56E
		private static void NarrowFormation(LineFormation formation, int fileCountFromLeftFlank, int fileCountFromRightFlank)
		{
			LineFormation.ShiftUnitsBackwardsForNarrowingFormation(formation, fileCountFromLeftFlank, fileCountFromRightFlank);
			LineFormation.NarrowFormationAux(formation, fileCountFromLeftFlank, fileCountFromRightFlank);
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x0002D380 File Offset: 0x0002B580
		private static void NarrowFormationAux(LineFormation formation, int fileCountFromLeftFlank, int fileCountFromRightFlank)
		{
			formation._units2DWorkspace.ResetWithNewCount(formation.FileCount - fileCountFromLeftFlank - fileCountFromRightFlank, formation.RankCount);
			formation._unitPositionAvailabilitiesWorkspace.ResetWithNewCount(formation.FileCount - fileCountFromLeftFlank - fileCountFromRightFlank, formation.RankCount);
			formation._globalPositionsWorkspace.ResetWithNewCount(formation.FileCount - fileCountFromLeftFlank - fileCountFromRightFlank, formation.RankCount);
			for (int i = fileCountFromLeftFlank; i < formation.FileCount - fileCountFromRightFlank; i++)
			{
				int destinationIndex = i - fileCountFromLeftFlank;
				formation._units2D.CopyRowTo(i, 0, formation._units2DWorkspace, destinationIndex, 0, formation.RankCount);
				formation.UnitPositionAvailabilities.CopyRowTo(i, 0, formation._unitPositionAvailabilitiesWorkspace, destinationIndex, 0, formation.RankCount);
				formation._globalPositions.CopyRowTo(i, 0, formation._globalPositionsWorkspace, destinationIndex, 0, formation.RankCount);
				if (fileCountFromLeftFlank > 0)
				{
					for (int j = 0; j < formation.RankCount; j++)
					{
						if (formation._units2D[i, j] != null)
						{
							formation._units2D[i, j].FormationFileIndex -= fileCountFromLeftFlank;
						}
					}
				}
			}
			MBList2D<IFormationUnit> units2D = formation._units2D;
			formation._units2D = formation._units2DWorkspace;
			formation._units2DWorkspace = units2D;
			formation.ReconstructUnitsFromUnits2D();
			MBList2D<int> unitPositionAvailabilities = formation.UnitPositionAvailabilities;
			formation.UnitPositionAvailabilities = formation._unitPositionAvailabilitiesWorkspace;
			formation._unitPositionAvailabilitiesWorkspace = unitPositionAvailabilities;
			MBList2D<WorldPosition> globalPositions = formation._globalPositions;
			formation._globalPositions = formation._globalPositionsWorkspace;
			formation._globalPositionsWorkspace = globalPositions;
			formation.BatchUnitPositionAvailabilities(true);
			Action onShapeChanged = formation.OnShapeChanged;
			if (onShapeChanged != null)
			{
				onShapeChanged();
			}
			if (formation._isDeformingOnWidthChange || (fileCountFromLeftFlank + fileCountFromRightFlank) % 2 == 1)
			{
				formation.OnFormationFrameChanged();
			}
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x0002D518 File Offset: 0x0002B718
		private static void ShortenFormation(LineFormation formation, int front, int rear)
		{
			formation._units2DWorkspace.ResetWithNewCount(formation.FileCount, formation.RankCount - front - rear);
			formation._unitPositionAvailabilitiesWorkspace.ResetWithNewCount(formation.FileCount, formation.RankCount - front - rear);
			formation._globalPositionsWorkspace.ResetWithNewCount(formation.FileCount, formation.RankCount - front - rear);
			for (int i = 0; i < formation.FileCount; i++)
			{
				formation._units2D.CopyRowTo(i, front, formation._units2DWorkspace, i, 0, formation.RankCount - rear - front);
				formation.UnitPositionAvailabilities.CopyRowTo(i, front, formation._unitPositionAvailabilitiesWorkspace, i, 0, formation.RankCount - rear - front);
				formation._globalPositions.CopyRowTo(i, front, formation._globalPositionsWorkspace, i, 0, formation.RankCount - rear - front);
				if (front > 0)
				{
					for (int j = front; j < formation.RankCount - rear; j++)
					{
						if (formation._units2D[i, j] != null)
						{
							formation._units2D[i, j].FormationRankIndex -= front;
						}
					}
				}
			}
			MBList2D<IFormationUnit> units2D = formation._units2D;
			formation._units2D = formation._units2DWorkspace;
			formation._units2DWorkspace = units2D;
			formation.ReconstructUnitsFromUnits2D();
			MBList2D<int> unitPositionAvailabilities = formation.UnitPositionAvailabilities;
			formation.UnitPositionAvailabilities = formation._unitPositionAvailabilitiesWorkspace;
			formation._unitPositionAvailabilitiesWorkspace = unitPositionAvailabilities;
			MBList2D<WorldPosition> globalPositions = formation._globalPositions;
			formation._globalPositions = formation._globalPositionsWorkspace;
			formation._globalPositionsWorkspace = globalPositions;
			formation.BatchUnitPositionAvailabilities(true);
			Action onShapeChanged = formation.OnShapeChanged;
			if (onShapeChanged == null)
			{
				return;
			}
			onShapeChanged();
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x0002D69B File Offset: 0x0002B89B
		private void Shorten()
		{
			LineFormation.ShortenFormation(this, 0, 1);
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x0002D6A8 File Offset: 0x0002B8A8
		private void GetFrontAndRearOfFile(int fileIndex, out bool isFileEmtpy, out int rankIndexOfFront, out int rankIndexOfRear, bool includeUnavailablePositions = false)
		{
			rankIndexOfFront = -1;
			rankIndexOfRear = this.RankCount;
			for (int i = 0; i < this.RankCount; i++)
			{
				if (this._units2D[fileIndex, i] != null)
				{
					rankIndexOfFront = i;
					break;
				}
			}
			if (includeUnavailablePositions)
			{
				if (rankIndexOfFront != -1)
				{
					for (int j = rankIndexOfFront - 1; j >= 0; j--)
					{
						if (this.IsUnitPositionAvailable(fileIndex, j))
						{
							break;
						}
						rankIndexOfFront = j;
					}
				}
				else
				{
					bool flag = true;
					for (int k = 0; k < this.RankCount; k++)
					{
						if (this.IsUnitPositionAvailable(fileIndex, k))
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						rankIndexOfFront = 0;
					}
				}
			}
			for (int l = this.RankCount - 1; l >= 0; l--)
			{
				if (this._units2D[fileIndex, l] != null)
				{
					rankIndexOfRear = l;
					break;
				}
			}
			if (includeUnavailablePositions)
			{
				if (rankIndexOfRear != this.RankCount)
				{
					for (int m = rankIndexOfRear + 1; m < this.RankCount; m++)
					{
						if (this.IsUnitPositionAvailable(fileIndex, m))
						{
							break;
						}
						rankIndexOfRear = m;
					}
				}
				else
				{
					bool flag2 = true;
					for (int n = 0; n < this.RankCount; n++)
					{
						if (this.IsUnitPositionAvailable(fileIndex, n))
						{
							flag2 = false;
							break;
						}
					}
					if (flag2)
					{
						rankIndexOfRear = this.RankCount - 1;
					}
				}
			}
			if (rankIndexOfFront == -1 && rankIndexOfRear == this.RankCount)
			{
				isFileEmtpy = true;
				return;
			}
			isFileEmtpy = false;
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x0002D7EC File Offset: 0x0002B9EC
		private void GetFlanksOfRank(int rankIndex, out bool isRankEmpty, out int fileIndexOfLeftFlank, out int fileIndexOfRightFlank, bool includeUnavailablePositions = false)
		{
			fileIndexOfLeftFlank = -1;
			fileIndexOfRightFlank = this.FileCount;
			for (int i = 0; i < this.FileCount; i++)
			{
				if (this._units2D[i, rankIndex] != null)
				{
					fileIndexOfLeftFlank = i;
					break;
				}
			}
			if (includeUnavailablePositions)
			{
				if (fileIndexOfLeftFlank != -1)
				{
					for (int j = fileIndexOfLeftFlank - 1; j >= 0; j--)
					{
						if (this.IsUnitPositionAvailable(j, rankIndex))
						{
							break;
						}
						fileIndexOfLeftFlank = j;
					}
				}
				else
				{
					bool flag = true;
					for (int k = 0; k < this.FileCount; k++)
					{
						if (this.IsUnitPositionAvailable(k, rankIndex))
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						fileIndexOfLeftFlank = 0;
					}
				}
			}
			for (int l = this.FileCount - 1; l >= 0; l--)
			{
				if (this._units2D[l, rankIndex] != null)
				{
					fileIndexOfRightFlank = l;
					break;
				}
			}
			if (includeUnavailablePositions)
			{
				if (fileIndexOfRightFlank != this.FileCount)
				{
					for (int m = fileIndexOfRightFlank + 1; m < this.FileCount; m++)
					{
						if (this.IsUnitPositionAvailable(m, rankIndex))
						{
							break;
						}
						fileIndexOfRightFlank = m;
					}
				}
				else
				{
					bool flag2 = true;
					for (int n = 0; n < this.FileCount; n++)
					{
						if (this.IsUnitPositionAvailable(n, rankIndex))
						{
							flag2 = false;
							break;
						}
					}
					if (flag2)
					{
						fileIndexOfRightFlank = this.FileCount - 1;
					}
				}
			}
			if (fileIndexOfLeftFlank == -1 && fileIndexOfRightFlank == this.FileCount)
			{
				isRankEmpty = true;
				return;
			}
			isRankEmpty = false;
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x0002D92E File Offset: 0x0002BB2E
		private static void FillInTheGapsOfFile(LineFormation formation, int fileIndex, int rankIndex = 0, bool isCheckingLastRankForEmptiness = true)
		{
			LineFormation.FillInTheGapsOfFileAux(formation, fileIndex, rankIndex);
			while (isCheckingLastRankForEmptiness && formation.RankCount > 1 && formation.IsRankEmpty(formation.RankCount - 1))
			{
				formation.Shorten();
			}
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x0002D95C File Offset: 0x0002BB5C
		private static void FillInTheGapsOfFileAux(LineFormation formation, int fileIndex, int rankIndex)
		{
			for (;;)
			{
				int num = -1;
				while (rankIndex < formation.RankCount - 1)
				{
					if (formation._units2D[fileIndex, rankIndex] == null && formation.IsUnitPositionAvailable(fileIndex, rankIndex))
					{
						num = rankIndex;
						break;
					}
					rankIndex++;
				}
				int num2 = -1;
				while (rankIndex < formation.RankCount)
				{
					if (formation._units2D[fileIndex, rankIndex] != null)
					{
						num2 = rankIndex;
						break;
					}
					rankIndex++;
				}
				if (num == -1 || num2 == -1)
				{
					break;
				}
				formation.RelocateUnit(formation._units2D[fileIndex, num2], fileIndex, num);
				rankIndex = num + 1;
			}
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x0002D9E4 File Offset: 0x0002BBE4
		private static void FillInTheGapsOfMiddleRanks(LineFormation formation, List<IFormationUnit> relocatedUnits = null)
		{
			int num = formation.RankCount - 1;
			for (int i = 0; i < formation.FileCount; i++)
			{
				if (formation._units2D[i, num] == null && !formation.IsFileFullyOccupied(i))
				{
					int num4;
					for (;;)
					{
						bool flag;
						int num2;
						int num3;
						formation.GetFrontAndRearOfFile(i, out flag, out num2, out num3, true);
						if (num3 == num)
						{
							goto IL_D7;
						}
						num4 = num3 + 1;
						if (flag)
						{
							num4 = -1;
							for (int j = 0; j < formation.RankCount; j++)
							{
								if (formation.IsUnitPositionAvailable(i, j))
								{
									num4 = j;
									break;
								}
							}
						}
						IFormationUnit unitToFillIn = LineFormation.GetUnitToFillIn(formation, i, num4);
						if (unitToFillIn == null)
						{
							break;
						}
						formation.RelocateUnit(unitToFillIn, i, num4);
						if (relocatedUnits != null)
						{
							relocatedUnits.Add(unitToFillIn);
						}
						if (formation.IsRankEmpty(num))
						{
							formation.Shorten();
							num = formation.RankCount - 1;
						}
					}
					for (int k = num4 + 1; k < formation.RankCount; k++)
					{
					}
				}
				IL_D7:;
			}
			while (formation.RankCount > 1 && formation.IsRankEmpty(formation.RankCount - 1))
			{
				formation.Shorten();
			}
			LineFormation.AlignLastRank(formation);
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x0002DB00 File Offset: 0x0002BD00
		private static void AlignRankToLeft(LineFormation formation, int fileIndex, int rankIndex)
		{
			int num = -1;
			while (fileIndex < formation.FileCount - 1)
			{
				if (formation._units2D[fileIndex, rankIndex] == null && formation.IsUnitPositionAvailable(fileIndex, rankIndex))
				{
					num = fileIndex;
					break;
				}
				fileIndex++;
			}
			int num2 = -1;
			while (fileIndex < formation.FileCount)
			{
				if (formation._units2D[fileIndex, rankIndex] != null)
				{
					num2 = fileIndex;
					break;
				}
				fileIndex++;
			}
			if (num != -1 && num2 != -1)
			{
				formation.RelocateUnit(formation._units2D[num2, rankIndex], num, rankIndex);
				LineFormation.AlignRankToLeft(formation, num + 1, rankIndex);
			}
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x0002DB8C File Offset: 0x0002BD8C
		private static void AlignRankToRight(LineFormation formation, int fileIndex, int rankIndex)
		{
			int num = -1;
			while (fileIndex > 0)
			{
				if (formation._units2D[fileIndex, rankIndex] == null && formation.IsUnitPositionAvailable(fileIndex, rankIndex))
				{
					num = fileIndex;
					break;
				}
				fileIndex--;
			}
			int num2 = -1;
			while (fileIndex >= 0)
			{
				if (formation._units2D[fileIndex, rankIndex] != null)
				{
					num2 = fileIndex;
					break;
				}
				fileIndex--;
			}
			if (num != -1 && num2 != -1)
			{
				formation.RelocateUnit(formation._units2D[num2, rankIndex], num, rankIndex);
				LineFormation.AlignRankToRight(formation, num - 1, rankIndex);
			}
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x0002DC0C File Offset: 0x0002BE0C
		private static void AlignLastRank(LineFormation formation)
		{
			int num = formation.RankCount - 1;
			bool flag;
			int num2;
			int num3;
			formation.GetFlanksOfRank(num, out flag, out num2, out num3, true);
			if (num == 0 && flag)
			{
				return;
			}
			LineFormation.AlignRankToLeft(formation, num2, num);
			bool flag2 = false;
			bool flag3 = false;
			for (;;)
			{
				formation.GetFlanksOfRank(num, out flag, out num2, out num3, true);
				if (!flag2 && num2 < formation.FileCount - num3 - 1 - 1)
				{
					int num4;
					int index;
					formation.GetFlanksOfRank(num, out flag, out num4, out index, false);
					formation.RelocateUnit(formation._units2D[index, num], num3 + 1, num);
					LineFormation.AlignRankToRight(formation, num3 + 1, num);
					flag3 = true;
				}
				else
				{
					if (flag3 || num2 - 1 <= formation.FileCount - num3 - 1)
					{
						break;
					}
					int index2;
					int num5;
					formation.GetFlanksOfRank(num, out flag, out index2, out num5, false);
					formation.RelocateUnit(formation._units2D[index2, num], num2 - 1, num);
					LineFormation.AlignRankToLeft(formation, num2 - 1, num);
					flag2 = true;
				}
			}
		}

		// Token: 0x06000FDE RID: 4062 RVA: 0x0002DCE8 File Offset: 0x0002BEE8
		private int CountUnitsAtRank(int rankIndex)
		{
			int num = 0;
			for (int i = 0; i < this.FileCount; i++)
			{
				if (this._units2D[i, rankIndex] != null)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06000FDF RID: 4063 RVA: 0x0002DD1C File Offset: 0x0002BF1C
		private bool IsRankEmpty(int rankIndex)
		{
			for (int i = 0; i < this.FileCount; i++)
			{
				if (this._units2D[i, rankIndex] != null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x0002DD4C File Offset: 0x0002BF4C
		private bool IsFileFullyOccupied(int fileIndex)
		{
			bool result = true;
			for (int i = 0; i < this.RankCount; i++)
			{
				if (this._units2D[fileIndex, i] == null && this.IsUnitPositionAvailable(fileIndex, i))
				{
					result = false;
					break;
				}
			}
			return result;
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x0002DD8C File Offset: 0x0002BF8C
		private bool IsRankFullyOccupied(int rankIndex)
		{
			bool result = true;
			for (int i = 0; i < this.FileCount; i++)
			{
				if (this._units2D[i, rankIndex] == null && this.IsUnitPositionAvailable(i, rankIndex))
				{
					result = false;
					break;
				}
			}
			return result;
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x0002DDCC File Offset: 0x0002BFCC
		private static IFormationUnit GetUnitToFillIn(LineFormation formation, int relocationFileIndex, int relocationRankIndex)
		{
			int i = formation.RankCount - 1;
			while (i >= 0)
			{
				if (relocationRankIndex == i)
				{
					return null;
				}
				bool flag;
				int num;
				int num2;
				formation.GetFlanksOfRank(i, out flag, out num, out num2, false);
				if (!flag)
				{
					if (relocationFileIndex > num2)
					{
						return formation._units2D[num2, i];
					}
					if (relocationFileIndex < num)
					{
						return formation._units2D[num, i];
					}
					if (num2 - relocationFileIndex > relocationFileIndex - num)
					{
						return formation._units2D[num, i];
					}
					return formation._units2D[num2, i];
				}
				else
				{
					i--;
				}
			}
			Debug.FailedAssert("This line should not be reached.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\AI\\Formation\\LineFormation.cs", "GetUnitToFillIn", 3161);
			return null;
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x0002DE68 File Offset: 0x0002C068
		protected void RelocateUnit(IFormationUnit unit, int fileIndex, int rankIndex)
		{
			this._units2D[unit.FormationFileIndex, unit.FormationRankIndex] = null;
			this._units2D[fileIndex, rankIndex] = unit;
			this.ReconstructUnitsFromUnits2D();
			unit.FormationFileIndex = fileIndex;
			unit.FormationRankIndex = rankIndex;
			Action onShapeChanged = this.OnShapeChanged;
			if (onShapeChanged == null)
			{
				return;
			}
			onShapeChanged();
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000FE4 RID: 4068 RVA: 0x0002DEBF File Offset: 0x0002C0BF
		public int UnitCount
		{
			get
			{
				return this.GetAllUnits().Count;
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000FE5 RID: 4069 RVA: 0x0002DECC File Offset: 0x0002C0CC
		public int PositionedUnitCount
		{
			get
			{
				return this.UnitCount - this._unpositionedUnits.Count;
			}
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x0002DEE0 File Offset: 0x0002C0E0
		public IFormationUnit GetPlayerUnit()
		{
			return this._allUnits.FirstOrDefault((IFormationUnit unit) => unit.IsPlayerUnit);
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x0002DF0C File Offset: 0x0002C10C
		public MBReadOnlyList<IFormationUnit> GetAllUnits()
		{
			return this._allUnits;
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x0002DF14 File Offset: 0x0002C114
		public MBList<IFormationUnit> GetUnpositionedUnits()
		{
			return this._unpositionedUnits;
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x0002DF1C File Offset: 0x0002C11C
		public Vec2? GetLocalDirectionOfRelativeFormationLocation(IFormationUnit unit)
		{
			if (this._unpositionedUnits.Contains(unit))
			{
				return null;
			}
			Vec2 value = new Vec2((float)unit.FormationFileIndex, (float)(-(float)unit.FormationRankIndex)) - new Vec2((float)(this.FileCount - 1) * 0.5f, (float)(this.RankCount - 1) * -0.5f);
			value.Normalize();
			return new Vec2?(value);
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x0002DF8C File Offset: 0x0002C18C
		public Vec2? GetLocalWallDirectionOfRelativeFormationLocation(IFormationUnit unit)
		{
			if (this._unpositionedUnits.Contains(unit))
			{
				return null;
			}
			Vec2 value = new Vec2((float)unit.FormationFileIndex, (float)(-(float)unit.FormationRankIndex)) - new Vec2((float)(this.FileCount - 1) * 0.5f, (float)(-(float)this.RankCount));
			value.Normalize();
			return new Vec2?(value);
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x0002DFF5 File Offset: 0x0002C1F5
		public void GetFormationInfo(out int fileCount, out int rankCount)
		{
			fileCount = this.FileCount;
			rankCount = this.RankCount;
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x0002E008 File Offset: 0x0002C208
		[Conditional("DEBUG")]
		private void AssertUnit(IFormationUnit unit, bool isAssertingUnitPositionAvailability = true)
		{
			if (isAssertingUnitPositionAvailability)
			{
				this.IsUnitPositionRestrained(unit.FormationFileIndex, unit.FormationRankIndex);
				if (this._isMiddleFrontUnitPositionReserved && this.GetMiddleFrontUnitPosition().Item1 == unit.FormationFileIndex)
				{
					bool flag = this.GetMiddleFrontUnitPosition().Item2 == unit.FormationRankIndex;
				}
				this.IsUnitPositionAvailable(unit.FormationFileIndex, unit.FormationRankIndex);
			}
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x0002E076 File Offset: 0x0002C276
		[Conditional("DEBUG")]
		private void AssertUnpositionedUnit(IFormationUnit unit)
		{
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x0002E078 File Offset: 0x0002C278
		public float GetUnitsDistanceToFrontLine(IFormationUnit unit)
		{
			if (this._unpositionedUnits.Contains(unit))
			{
				return -1f;
			}
			return (float)unit.FormationRankIndex * (this.Distance + this.UnitDiameter) + this.UnitDiameter * 0.5f;
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x0002E0B0 File Offset: 0x0002C2B0
		public IFormationUnit GetNeighborUnitOfLeftSide(IFormationUnit unit)
		{
			if (this._unpositionedUnits.Contains(unit))
			{
				return null;
			}
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

		// Token: 0x06000FF0 RID: 4080 RVA: 0x0002E108 File Offset: 0x0002C308
		public IFormationUnit GetNeighborUnitOfRightSide(IFormationUnit unit)
		{
			if (this._unpositionedUnits.Contains(unit))
			{
				return null;
			}
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

		// Token: 0x06000FF1 RID: 4081 RVA: 0x0002E164 File Offset: 0x0002C364
		public void SwitchUnitLocationsWithUnpositionedUnit(IFormationUnit firstUnit, IFormationUnit secondUnit)
		{
			int formationFileIndex = firstUnit.FormationFileIndex;
			int formationRankIndex = firstUnit.FormationRankIndex;
			this._unpositionedUnits.Remove(secondUnit);
			this._units2D[formationFileIndex, formationRankIndex] = secondUnit;
			secondUnit.FormationFileIndex = formationFileIndex;
			secondUnit.FormationRankIndex = formationRankIndex;
			firstUnit.FormationFileIndex = -1;
			firstUnit.FormationRankIndex = -1;
			this._unpositionedUnits.Add(firstUnit);
			this.ReconstructUnitsFromUnits2D();
			Action onShapeChanged = this.OnShapeChanged;
			if (onShapeChanged == null)
			{
				return;
			}
			onShapeChanged();
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x0002E1D8 File Offset: 0x0002C3D8
		public void SwitchUnitLocations(IFormationUnit firstUnit, IFormationUnit secondUnit)
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

		// Token: 0x06000FF3 RID: 4083 RVA: 0x0002E250 File Offset: 0x0002C450
		public void SwitchUnitLocationsWithBackMostUnit(IFormationUnit unit)
		{
			IFormationUnit lastUnit = this.GetLastUnit();
			if (lastUnit != null && unit != null && unit != lastUnit)
			{
				this.SwitchUnitLocations(unit, lastUnit);
			}
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x0002E276 File Offset: 0x0002C476
		public void BeforeFormationFrameChange()
		{
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x0002E278 File Offset: 0x0002C478
		public void BatchUnitPositionAvailabilities(bool isUpdatingCachedOrderedLocalPositions = true)
		{
			if (isUpdatingCachedOrderedLocalPositions)
			{
				this.AreLocalPositionsDirty = true;
			}
			bool areLocalPositionsDirty = this.AreLocalPositionsDirty;
			this.AreLocalPositionsDirty = false;
			if (areLocalPositionsDirty)
			{
				this._cachedOrderedUnitPositionIndices.Clear();
				for (int i = 0; i < this.FileCount * this.RankCount; i++)
				{
					this._cachedOrderedUnitPositionIndices.Add(this.GetOrderedUnitPositionIndex(i));
				}
				this._cachedOrderedLocalPositions.Clear();
				for (int j = 0; j < this._cachedOrderedUnitPositionIndices.Count; j++)
				{
					Vec2i vec2i = this._cachedOrderedUnitPositionIndices[j];
					this._cachedOrderedLocalPositions.Add(this.GetLocalPositionOfUnit(vec2i.Item1, vec2i.Item2));
				}
			}
			this.MakeRestrainedPositionsUnavailable();
			if (!this.owner.BatchUnitPositions(this._cachedOrderedUnitPositionIndices, this._cachedOrderedLocalPositions, this.UnitPositionAvailabilities, this._globalPositions, this.FileCount, this.RankCount))
			{
				for (int k = 0; k < this.FileCount; k++)
				{
					for (int l = 0; l < this.RankCount; l++)
					{
						this.UnitPositionAvailabilities[k, l] = 1;
					}
				}
			}
			if (areLocalPositionsDirty)
			{
				this._cachedOrderedAndAvailableUnitPositionIndices.Clear();
				for (int m = 0; m < this._cachedOrderedUnitPositionIndices.Count; m++)
				{
					Vec2i item = this._cachedOrderedUnitPositionIndices[m];
					if (this.UnitPositionAvailabilities[item.Item1, item.Item2] == 2)
					{
						this._cachedOrderedAndAvailableUnitPositionIndices.Add(item);
					}
				}
			}
			Formation formation;
			this._isCavalry = ((formation = (this.owner as Formation)) != null && formation.CalculateHasSignificantNumberOfMounted);
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x0002E418 File Offset: 0x0002C618
		public void OnFormationFrameChanged()
		{
			this.UnitPositionAvailabilities.Clear();
			this.BatchUnitPositionAvailabilities(false);
			bool flag = LineFormation.ShiftUnitsBackwardsForNewUnavailableUnitPositions(this);
			for (int i = 0; i < this.FileCount; i++)
			{
				LineFormation.FillInTheGapsOfFile(this, i, 0, i == this.FileCount - 1);
			}
			bool flag2 = flag;
			bool flag3 = this.TryReaddingUnpositionedUnits();
			if (flag2 && !flag3)
			{
				this.owner.OnUnitAddedOrRemoved();
			}
			LineFormation.FillInTheGapsOfMiddleRanks(this, null);
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x0002E484 File Offset: 0x0002C684
		private bool TryReaddingUnpositionedUnits()
		{
			bool flag = this._unpositionedUnits.Count > 0;
			for (int i = this._unpositionedUnits.Count - 1; i >= 0; i--)
			{
				i = MathF.Min(i, this._unpositionedUnits.Count - 1);
				if (i < 0)
				{
					break;
				}
				IFormationUnit unit = this._unpositionedUnits[i];
				this.RemoveUnit(unit);
				if (!this.AddUnit(unit))
				{
					break;
				}
			}
			if (flag)
			{
				this.owner.OnUnitAddedOrRemoved();
			}
			return flag;
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x0002E4FC File Offset: 0x0002C6FC
		private bool AreLastRanksCompletelyUnavailable(int numberOfRanksToCheck = 3)
		{
			bool result = true;
			if (this.RankCount < numberOfRanksToCheck)
			{
				result = false;
			}
			else
			{
				for (int i = 0; i < this.FileCount; i++)
				{
					for (int j = this.RankCount - numberOfRanksToCheck; j < this.RankCount; j++)
					{
						if (this.IsUnitPositionAvailable(i, j))
						{
							i = 2147483646;
							result = false;
							break;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x0002E558 File Offset: 0x0002C758
		[Conditional("DEBUG")]
		private void AssertUnitPositions()
		{
			for (int i = 0; i < this._units2D.Count1; i++)
			{
				for (int j = 0; j < this._units2D.Count2; j++)
				{
					IFormationUnit formationUnit = this._units2D[i, j];
				}
			}
			foreach (IFormationUnit formationUnit2 in this._unpositionedUnits)
			{
			}
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x0002E5E0 File Offset: 0x0002C7E0
		[Conditional("DEBUG")]
		private void AssertFilePositions(int fileIndex)
		{
			bool flag;
			int num;
			int num2;
			this.GetFrontAndRearOfFile(fileIndex, out flag, out num, out num2, true);
			if (!flag)
			{
				for (int i = num; i <= num2; i++)
				{
				}
			}
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x0002E60C File Offset: 0x0002C80C
		[Conditional("DEBUG")]
		private void AssertRankPositions(int rankIndex)
		{
			bool flag;
			int num;
			int num2;
			this.GetFlanksOfRank(rankIndex, out flag, out num, out num2, true);
			if (!flag)
			{
				for (int i = num; i <= num2; i++)
				{
				}
			}
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x0002E638 File Offset: 0x0002C838
		public void OnFormationDispersed()
		{
			IEnumerable<Vec2i> enumerable = from i in this.GetOrderedUnitPositionIndices()
			where this.IsUnitPositionAvailable(i.Item1, i.Item2)
			select i;
			MBList<IFormationUnit> mblist = this.GetAllUnits().ToMBList<IFormationUnit>();
			foreach (Vec2i vec2i in enumerable)
			{
				int item = vec2i.Item1;
				int item2 = vec2i.Item2;
				IFormationUnit formationUnit = this._units2D[item, item2];
				if (formationUnit != null)
				{
					IFormationUnit closestUnitTo = this.owner.GetClosestUnitTo(this.GetLocalPositionOfUnit(item, item2), mblist, null);
					mblist[mblist.IndexOf(closestUnitTo)] = null;
					if (formationUnit != closestUnitTo)
					{
						if (closestUnitTo.FormationFileIndex == -1)
						{
							this.SwitchUnitLocationsWithUnpositionedUnit(formationUnit, closestUnitTo);
						}
						else
						{
							this.SwitchUnitLocations(formationUnit, closestUnitTo);
						}
					}
				}
			}
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x0002E71C File Offset: 0x0002C91C
		public void OnUnitLostMount(IFormationUnit unit)
		{
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x0002E720 File Offset: 0x0002C920
		public bool IsTurnBackwardsNecessary(Vec2 previousPosition, WorldPosition? newPosition, Vec2 previousDirection, bool hasNewDirection, Vec2? newDirection)
		{
			return hasNewDirection && MathF.Abs(MBMath.GetSmallestDifferenceBetweenTwoAngles(newDirection.Value.RotationInRadians, previousDirection.RotationInRadians)) >= 2.3561945f;
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x0002E760 File Offset: 0x0002C960
		public virtual void TurnBackwards()
		{
			for (int i = 0; i <= this.FileCount / 2; i++)
			{
				int num = i;
				int num2 = this.FileCount - 1 - i;
				for (int j = 0; j < this.RankCount; j++)
				{
					int num3 = j;
					int num4 = this.RankCount - 1 - j;
					IFormationUnit formationUnit = this._units2D[num, num3];
					IFormationUnit formationUnit2 = this._units2D[num2, num4];
					if (formationUnit != formationUnit2)
					{
						if (formationUnit != null && formationUnit2 != null)
						{
							this.SwitchUnitLocations(formationUnit, formationUnit2);
						}
						else if (formationUnit != null)
						{
							if (this.IsUnitPositionAvailable(num2, num4))
							{
								this.RelocateUnit(formationUnit, num2, num4);
							}
						}
						else if (formationUnit2 != null && this.IsUnitPositionAvailable(num, num3))
						{
							this.RelocateUnit(formationUnit2, num, num3);
						}
					}
				}
			}
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x0002E82C File Offset: 0x0002CA2C
		public float GetOccupationWidth(int unitCount)
		{
			if (unitCount < 1)
			{
				return 0f;
			}
			int num = this.FileCount - 1;
			int num2 = 0;
			for (int i = 0; i < this.FileCount * this.RankCount; i++)
			{
				Vec2i orderedUnitPositionIndex = this.GetOrderedUnitPositionIndex(i);
				if (orderedUnitPositionIndex.Item1 < num)
				{
					num = orderedUnitPositionIndex.Item1;
				}
				if (orderedUnitPositionIndex.Item1 > num2)
				{
					num2 = orderedUnitPositionIndex.Item1;
				}
				if (this.IsUnitPositionAvailable(orderedUnitPositionIndex.Item1, orderedUnitPositionIndex.Item2))
				{
					unitCount--;
					if (unitCount == 0)
					{
						break;
					}
				}
			}
			return (float)(num2 - num) * (this.Interval + this.UnitDiameter) + this.UnitDiameter;
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x0002E8CC File Offset: 0x0002CACC
		public void InvalidateCacheOfUnitAux(Vec2 roundedLocalPosition)
		{
			int index;
			int index2;
			if (this.TryGetUnitPositionIndexFromLocalPosition(roundedLocalPosition, out index, out index2))
			{
				this.UnitPositionAvailabilities[index, index2] = 0;
			}
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x0002E8F4 File Offset: 0x0002CAF4
		public Vec2? CreateNewPosition(int unitIndex)
		{
			Vec2? result = null;
			int num = 100;
			while (result == null && num > 0 && !this.AreLastRanksCompletelyUnavailable(3) && this.IsDeepenApplicable())
			{
				this.Deepen();
				result = this.GetLocalPositionOfUnitOrDefault(unitIndex);
				num--;
			}
			return result;
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x0002E93F File Offset: 0x0002CB3F
		public virtual void RearrangeFrom(IFormationArrangement arrangement)
		{
			this.BatchUnitPositionAvailabilities(true);
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x0002E948 File Offset: 0x0002CB48
		public virtual void RearrangeTo(IFormationArrangement arrangement)
		{
			if (arrangement is ColumnFormation)
			{
				this.IsTransforming = true;
				this.ReleaseMiddleFrontUnitPosition();
			}
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x0002E960 File Offset: 0x0002CB60
		public virtual void RearrangeTransferUnits(IFormationArrangement arrangement)
		{
			LineFormation lineFormation;
			if ((lineFormation = (arrangement as LineFormation)) != null)
			{
				lineFormation._units2D = this._units2D;
				lineFormation._allUnits = this._allUnits;
				lineFormation.UnitPositionAvailabilities = this.UnitPositionAvailabilities;
				lineFormation._globalPositions = this._globalPositions;
				lineFormation._unpositionedUnits = this._unpositionedUnits;
				lineFormation.AreLocalPositionsDirty = true;
				lineFormation.OnFormationFrameChanged();
				return;
			}
			for (int i = 0; i < this.FileCount * this.RankCount; i++)
			{
				Vec2i orderedUnitPositionIndex = this.GetOrderedUnitPositionIndex(i);
				int item = orderedUnitPositionIndex.Item1;
				int item2 = orderedUnitPositionIndex.Item2;
				IFormationUnit formationUnit = this._units2D[item, item2];
				if (formationUnit != null)
				{
					formationUnit.FormationFileIndex = -1;
					formationUnit.FormationRankIndex = -1;
					arrangement.AddUnit(formationUnit);
				}
			}
			foreach (IFormationUnit unit in this._unpositionedUnits)
			{
				arrangement.AddUnit(unit);
			}
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x0002EA6C File Offset: 0x0002CC6C
		public static float CalculateWidth(float interval, float unitDiameter, int unitCountOnLine)
		{
			return (float)MathF.Max(0, unitCountOnLine - 1) * (interval + unitDiameter) + unitDiameter;
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x0002EA7E File Offset: 0x0002CC7E
		public void FormFromFlankWidth(int unitCountOnLine, bool skipSingleFileChangesForPerformance = false)
		{
			if (skipSingleFileChangesForPerformance && MathF.Abs(this.FileCount - unitCountOnLine) <= 1)
			{
				return;
			}
			this.FlankWidth = LineFormation.CalculateWidth(this.Interval, this.UnitDiameter, unitCountOnLine);
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x0002EAAC File Offset: 0x0002CCAC
		public void ReserveMiddleFrontUnitPosition(IFormationUnit vanguard)
		{
			this._isMiddleFrontUnitPositionReserved = true;
			this.OnFormationFrameChanged();
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x0002EABB File Offset: 0x0002CCBB
		public void ReleaseMiddleFrontUnitPosition()
		{
			this._isMiddleFrontUnitPositionReserved = false;
			this.OnFormationFrameChanged();
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x0002EACA File Offset: 0x0002CCCA
		private Vec2i GetMiddleFrontUnitPosition()
		{
			return this.GetOrderedUnitPositionIndex(0);
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x0002EAD4 File Offset: 0x0002CCD4
		public Vec2 GetLocalPositionOfReservedUnitPosition()
		{
			Vec2i middleFrontUnitPosition = this.GetMiddleFrontUnitPosition();
			return this.GetLocalPositionOfUnit(middleFrontUnitPosition.Item1, middleFrontUnitPosition.Item2);
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x0002EAFC File Offset: 0x0002CCFC
		public virtual void OnTickOccasionallyOfUnit(IFormationUnit unit, bool arrangementChangeAllowed)
		{
			Agent agent;
			if (!arrangementChangeAllowed || (agent = (unit as Agent)) == null)
			{
				return;
			}
			Agent agent2;
			if (unit.FormationRankIndex > 0 && agent.HasShieldCached && (agent2 = (this._units2D[unit.FormationFileIndex, unit.FormationRankIndex - 1] as Agent)) != null && agent2.Banner == null)
			{
				if (!agent2.HasShieldCached)
				{
					this.SwitchUnitLocations(unit, agent2);
					return;
				}
				int num = 1;
				while (unit.FormationFileIndex - num >= 0 || unit.FormationFileIndex + num < this.FileCount)
				{
					Agent agent3;
					if (unit.FormationFileIndex - num >= 0 && (agent3 = (this._units2D[unit.FormationFileIndex - num, unit.FormationRankIndex - 1] as Agent)) != null && !agent3.HasShieldCached && agent3.Banner == null)
					{
						this.SwitchUnitLocations(unit, agent3);
						return;
					}
					Agent agent4;
					if (unit.FormationFileIndex + num < this.FileCount && (agent4 = (this._units2D[unit.FormationFileIndex + num, unit.FormationRankIndex - 1] as Agent)) != null && !agent4.HasShieldCached && agent4.Banner == null)
					{
						this.SwitchUnitLocations(unit, agent4);
						return;
					}
					num++;
				}
			}
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x0002EC34 File Offset: 0x0002CE34
		public virtual float GetDirectionChangeTendencyOfUnit(IFormationUnit unit)
		{
			if (this.RankCount == 1 || unit.FormationRankIndex == -1)
			{
				return 0f;
			}
			return (float)unit.FormationRankIndex * 1f / (float)(this.RankCount - 1);
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x0002EC65 File Offset: 0x0002CE65
		public int GetCachedOrderedAndAvailableUnitPositionIndicesCount()
		{
			return this._cachedOrderedAndAvailableUnitPositionIndices.Count;
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x0002EC72 File Offset: 0x0002CE72
		public Vec2i GetCachedOrderedAndAvailableUnitPositionIndexAt(int i)
		{
			return this._cachedOrderedAndAvailableUnitPositionIndices[i];
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x0002EC80 File Offset: 0x0002CE80
		public WorldPosition GetGlobalPositionAtIndex(int indexX, int indexY)
		{
			return this._globalPositions[indexX, indexY];
		}

		// Token: 0x040003AA RID: 938
		protected const int UnitPositionAvailabilityValueOfUnprocessed = 0;

		// Token: 0x040003AB RID: 939
		protected const int UnitPositionAvailabilityValueOfUnavailable = 1;

		// Token: 0x040003AC RID: 940
		protected const int UnitPositionAvailabilityValueOfAvailable = 2;

		// Token: 0x040003AD RID: 941
		private static readonly Vec2i InvalidPositionIndex = new Vec2i(-1, -1);

		// Token: 0x040003AE RID: 942
		protected readonly IFormation owner;

		// Token: 0x040003AF RID: 943
		private MBList2D<IFormationUnit> _units2D;

		// Token: 0x040003B0 RID: 944
		private MBList2D<IFormationUnit> _units2DWorkspace;

		// Token: 0x040003B1 RID: 945
		private MBList<IFormationUnit> _allUnits;

		// Token: 0x040003B2 RID: 946
		private bool _isBatchRemovingUnits;

		// Token: 0x040003B3 RID: 947
		private readonly List<int> _gapFillMinRanksPerFileForBatchRemove = new List<int>();

		// Token: 0x040003B4 RID: 948
		private bool _batchRemoveInvolvesUnavailablePositions;

		// Token: 0x040003B5 RID: 949
		private MBList<IFormationUnit> _unpositionedUnits;

		// Token: 0x040003B6 RID: 950
		protected MBList2D<int> UnitPositionAvailabilities;

		// Token: 0x040003B7 RID: 951
		private MBList2D<int> _unitPositionAvailabilitiesWorkspace;

		// Token: 0x040003B8 RID: 952
		private MBList2D<WorldPosition> _globalPositions;

		// Token: 0x040003B9 RID: 953
		private MBList2D<WorldPosition> _globalPositionsWorkspace;

		// Token: 0x040003BA RID: 954
		private readonly MBWorkspace<MBQueue<ValueTuple<IFormationUnit, int, int>>> _displacedUnitsWorkspace;

		// Token: 0x040003BB RID: 955
		private readonly MBWorkspace<MBArrayList<Vec2i>> _finalOccupationsWorkspace;

		// Token: 0x040003BC RID: 956
		private readonly MBWorkspace<MBArrayList<Vec2i>> _filledInUnitPositionsWorkspace;

		// Token: 0x040003BD RID: 957
		private readonly MBWorkspace<MBQueue<Vec2i>> _toBeFilledInGapsWorkspace;

		// Token: 0x040003BE RID: 958
		private readonly MBWorkspace<MBArrayList<Vec2i>> _finalVacanciesWorkspace;

		// Token: 0x040003BF RID: 959
		private readonly MBWorkspace<MBArrayList<Vec2i>> _filledInGapsWorkspace;

		// Token: 0x040003C0 RID: 960
		private readonly MBWorkspace<MBArrayList<Vec2i>> _toBeEmptiedOutUnitPositionsWorkspace;

		// Token: 0x040003C1 RID: 961
		private MBArrayList<Vec2i> _cachedOrderedUnitPositionIndices;

		// Token: 0x040003C2 RID: 962
		private MBArrayList<Vec2i> _cachedOrderedAndAvailableUnitPositionIndices;

		// Token: 0x040003C3 RID: 963
		private MBArrayList<Vec2> _cachedOrderedLocalPositions;

		// Token: 0x040003C4 RID: 964
		private Func<LineFormation, int, int, bool> _shiftUnitsBackwardsPredicateDelegate;

		// Token: 0x040003C5 RID: 965
		private Func<LineFormation, int, int, bool> _shiftUnitsForwardsPredicateDelegate;

		// Token: 0x040003C7 RID: 967
		private bool _isCavalry;

		// Token: 0x040003C8 RID: 968
		private bool _isStaggered = true;

		// Token: 0x040003CB RID: 971
		private readonly bool _isDeformingOnWidthChange;

		// Token: 0x040003CC RID: 972
		private bool _isMiddleFrontUnitPositionReserved;

		// Token: 0x040003CD RID: 973
		protected bool IsTransforming;
	}
}
