using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000145 RID: 325
	public class SquareFormation : LineFormation
	{
		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06001020 RID: 4128 RVA: 0x0002EF6A File Offset: 0x0002D16A
		// (set) Token: 0x06001021 RID: 4129 RVA: 0x0002EF84 File Offset: 0x0002D184
		public override float Width
		{
			get
			{
				return SquareFormation.GetSideWidthFromUnitCount(this.UnitCountOfOuterSide, base.Interval, base.UnitDiameter);
			}
			set
			{
				this.FormFromBorderSideWidth(value);
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06001022 RID: 4130 RVA: 0x0002EF9A File Offset: 0x0002D19A
		public override float Depth
		{
			get
			{
				return SquareFormation.GetSideWidthFromUnitCount(this.UnitCountOfOuterSide, base.Interval, base.UnitDiameter);
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06001023 RID: 4131 RVA: 0x0002EFB4 File Offset: 0x0002D1B4
		public override float MinimumWidth
		{
			get
			{
				int num;
				int maximumRankCount = SquareFormation.GetMaximumRankCount(base.GetUnitCountWithOverride(), out num);
				return SquareFormation.GetSideWidthFromUnitCount(this.GetUnitsPerSideFromRankCount(maximumRankCount), this.owner.MinimumInterval, base.UnitDiameter);
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06001024 RID: 4132 RVA: 0x0002EFEC File Offset: 0x0002D1EC
		public override float MaximumWidth
		{
			get
			{
				return SquareFormation.GetSideWidthFromUnitCount(this.GetUnitsPerSideFromRankCount(1), this.owner.MaximumInterval, base.UnitDiameter);
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06001025 RID: 4133 RVA: 0x0002F00B File Offset: 0x0002D20B
		private int UnitCountOfOuterSide
		{
			get
			{
				return MathF.Ceiling((float)base.FileCount / 4f) + 1;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06001026 RID: 4134 RVA: 0x0002F021 File Offset: 0x0002D221
		private int MaxRank
		{
			get
			{
				return (this.UnitCountOfOuterSide + 1) / 2;
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06001027 RID: 4135 RVA: 0x0002F02D File Offset: 0x0002D22D
		private new float Distance
		{
			get
			{
				return base.Interval;
			}
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x0002F035 File Offset: 0x0002D235
		public SquareFormation(IFormation owner) : base(owner, true, true)
		{
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x0002F040 File Offset: 0x0002D240
		public override IFormationArrangement Clone(IFormation formation)
		{
			return new SquareFormation(formation);
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x0002F048 File Offset: 0x0002D248
		public override void DeepCopyFrom(IFormationArrangement arrangement)
		{
			base.DeepCopyFrom(arrangement);
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x0002F054 File Offset: 0x0002D254
		public void FormFromBorderSideWidth(float borderSideWidth)
		{
			int unitCountPerSide = MathF.Max(1, (int)((borderSideWidth - base.UnitDiameter) / (base.Interval + base.UnitDiameter) + 1E-05f)) + 1;
			this.FormFromBorderUnitCountPerSide(unitCountPerSide);
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x0002F08E File Offset: 0x0002D28E
		public void FormFromBorderUnitCountPerSide(int unitCountPerSide)
		{
			if (unitCountPerSide == 1)
			{
				base.FlankWidth = base.UnitDiameter;
				return;
			}
			base.FlankWidth = (float)(4 * (unitCountPerSide - 1) - 1) * (base.Interval + base.UnitDiameter) + base.UnitDiameter;
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x0002F0C4 File Offset: 0x0002D2C4
		public int GetUnitsPerSideFromRankCount(int rankCount)
		{
			int unitCountWithOverride = base.GetUnitCountWithOverride();
			int num;
			rankCount = MathF.Min(SquareFormation.GetMaximumRankCount(unitCountWithOverride, out num), rankCount);
			float f = (float)unitCountWithOverride / (4f * (float)rankCount) + (float)rankCount;
			int num2 = MathF.Ceiling(f);
			int num3 = MathF.Round(f);
			if (num3 < num2 && num3 * num3 == unitCountWithOverride)
			{
				num2 = num3;
			}
			if (num2 == 0)
			{
				num2 = 1;
			}
			return num2;
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x0002F118 File Offset: 0x0002D318
		protected static int GetMaximumRankCount(int unitCount, out int minimumFlankCount)
		{
			int num = (int)MathF.Sqrt((float)unitCount);
			if (num * num != unitCount)
			{
				num++;
			}
			minimumFlankCount = num;
			return MathF.Max(1, (num + 1) / 2);
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x0002F148 File Offset: 0x0002D348
		public void FormFromRankCount(int rankCount)
		{
			int unitsPerSideFromRankCount = this.GetUnitsPerSideFromRankCount(rankCount);
			this.FormFromBorderUnitCountPerSide(unitsPerSideFromRankCount);
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x0002F164 File Offset: 0x0002D364
		private SquareFormation.Side GetSideOfUnitPosition(int fileIndex)
		{
			return (SquareFormation.Side)(fileIndex / (this.UnitCountOfOuterSide - 1));
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x0002F170 File Offset: 0x0002D370
		private SquareFormation.Side? GetSideOfUnitPosition(int fileIndex, int rankIndex)
		{
			SquareFormation.Side sideOfUnitPosition = this.GetSideOfUnitPosition(fileIndex);
			if (rankIndex == 0)
			{
				return new SquareFormation.Side?(sideOfUnitPosition);
			}
			int num = this.UnitCountOfOuterSide - 2 * rankIndex;
			if (num == 1 && sideOfUnitPosition != SquareFormation.Side.Front)
			{
				return null;
			}
			int num2 = fileIndex % (this.UnitCountOfOuterSide - 1);
			int num3 = this.UnitCountOfOuterSide - num;
			num3 /= 2;
			if (num2 >= num3 && this.UnitCountOfOuterSide - num2 - 1 > num3)
			{
				return new SquareFormation.Side?(sideOfUnitPosition);
			}
			return null;
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x0002F1EC File Offset: 0x0002D3EC
		private Vec2 GetLocalPositionOfUnitAux(int fileIndex, int rankIndex, float usedInterval)
		{
			if (this.UnitCountOfOuterSide == 1)
			{
				return Vec2.Zero;
			}
			SquareFormation.Side sideOfUnitPosition = this.GetSideOfUnitPosition(fileIndex);
			float num = (float)(this.UnitCountOfOuterSide - 1) * (usedInterval + base.UnitDiameter);
			float num2 = (float)(fileIndex % (this.UnitCountOfOuterSide - 1)) * (usedInterval + base.UnitDiameter);
			float num3 = (float)rankIndex * (this.Distance + base.UnitDiameter);
			Vec2 vec;
			switch (sideOfUnitPosition)
			{
			case SquareFormation.Side.Front:
				vec = new Vec2(-num / 2f, 0f);
				vec += new Vec2(num2, -num3);
				break;
			case SquareFormation.Side.Right:
				vec = new Vec2(num / 2f, 0f);
				vec += new Vec2(-num3, -num2);
				break;
			case SquareFormation.Side.Rear:
				vec = new Vec2(num / 2f, -num);
				vec += new Vec2(-num2, num3);
				break;
			case SquareFormation.Side.Left:
				vec = new Vec2(-num / 2f, -num);
				vec += new Vec2(num3, num2);
				break;
			default:
				Debug.FailedAssert("false", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\AI\\Formation\\SquareFormation.cs", "GetLocalPositionOfUnitAux", 369);
				vec = Vec2.Zero;
				break;
			}
			return vec;
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x0002F320 File Offset: 0x0002D520
		protected override Vec2 GetLocalPositionOfUnit(int fileIndex, int rankIndex)
		{
			int fileIndex2 = this.ShiftFileIndex(fileIndex);
			return this.GetLocalPositionOfUnitAux(fileIndex2, rankIndex, base.Interval);
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x0002F344 File Offset: 0x0002D544
		protected override Vec2 GetLocalPositionOfUnitWithAdjustment(int fileIndex, int rankIndex, float distanceBetweenAgentsAdjustment)
		{
			int fileIndex2 = this.ShiftFileIndex(fileIndex);
			return this.GetLocalPositionOfUnitAux(fileIndex2, rankIndex, base.Interval + distanceBetweenAgentsAdjustment);
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x0002F36C File Offset: 0x0002D56C
		protected override Vec2 GetLocalDirectionOfUnit(int fileIndex, int rankIndex)
		{
			int fileIndex2 = this.ShiftFileIndex(fileIndex);
			switch (this.GetSideOfUnitPosition(fileIndex2))
			{
			case SquareFormation.Side.Front:
				return Vec2.Forward;
			case SquareFormation.Side.Right:
				return Vec2.Side;
			case SquareFormation.Side.Rear:
				return -Vec2.Forward;
			case SquareFormation.Side.Left:
				return -Vec2.Side;
			default:
				Debug.FailedAssert("false", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\AI\\Formation\\SquareFormation.cs", "GetLocalDirectionOfUnit", 448);
				return Vec2.Forward;
			}
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x0002F3E4 File Offset: 0x0002D5E4
		public override Vec2? GetLocalDirectionOfUnitOrDefault(IFormationUnit unit)
		{
			if (unit.FormationFileIndex < 0 || unit.FormationRankIndex < 0)
			{
				return null;
			}
			return new Vec2?(this.GetLocalDirectionOfUnit(unit.FormationFileIndex, unit.FormationRankIndex));
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x0002F424 File Offset: 0x0002D624
		protected override bool IsUnitPositionRestrained(int fileIndex, int rankIndex)
		{
			if (base.IsUnitPositionRestrained(fileIndex, rankIndex))
			{
				return true;
			}
			if (rankIndex >= this.MaxRank)
			{
				return true;
			}
			int fileIndex2 = this.ShiftFileIndex(fileIndex);
			return this.GetSideOfUnitPosition(fileIndex2, rankIndex) == null;
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x0002F464 File Offset: 0x0002D664
		protected override void MakeRestrainedPositionsUnavailable()
		{
			for (int i = 0; i < base.FileCount; i++)
			{
				for (int j = 0; j < base.RankCount; j++)
				{
					if (this.IsUnitPositionRestrained(i, j))
					{
						this.UnitPositionAvailabilities[i, j] = 1;
					}
				}
			}
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x0002F4AC File Offset: 0x0002D6AC
		private SquareFormation.Side GetSideOfLocalPosition(Vec2 localPosition)
		{
			float num = (float)(this.UnitCountOfOuterSide - 1) * (base.Interval + base.UnitDiameter);
			Vec2 v = new Vec2(0f, -num / 2f);
			Vec2 vec = localPosition - v;
			vec.y *= (base.Interval + base.UnitDiameter) / (this.Distance + base.UnitDiameter);
			float num2 = vec.RotationInRadians;
			if (num2 < 0f)
			{
				num2 += 6.2831855f;
			}
			if (num2 <= 0.7863982f || num2 > 5.4987874f)
			{
				return SquareFormation.Side.Front;
			}
			if (num2 <= 2.3571944f)
			{
				return SquareFormation.Side.Left;
			}
			if (num2 <= 3.927991f)
			{
				return SquareFormation.Side.Rear;
			}
			return SquareFormation.Side.Right;
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x0002F554 File Offset: 0x0002D754
		protected override bool TryGetUnitPositionIndexFromLocalPosition(Vec2 localPosition, out int fileIndex, out int rankIndex)
		{
			SquareFormation.Side sideOfLocalPosition = this.GetSideOfLocalPosition(localPosition);
			float num = (float)(this.UnitCountOfOuterSide - 1) * (base.Interval + base.UnitDiameter);
			float num2;
			float num3;
			switch (sideOfLocalPosition)
			{
			case SquareFormation.Side.Front:
			{
				Vec2 vec = localPosition - new Vec2(-num / 2f, 0f);
				num2 = vec.x;
				num3 = -vec.y;
				break;
			}
			case SquareFormation.Side.Right:
			{
				Vec2 vec2 = localPosition - new Vec2(num / 2f, 0f);
				num2 = -vec2.y;
				num3 = -vec2.x;
				break;
			}
			case SquareFormation.Side.Rear:
			{
				Vec2 vec3 = localPosition - new Vec2(num / 2f, -num);
				num2 = -vec3.x;
				num3 = vec3.y;
				break;
			}
			case SquareFormation.Side.Left:
			{
				Vec2 vec4 = localPosition - new Vec2(-num / 2f, -num);
				num2 = vec4.y;
				num3 = vec4.x;
				break;
			}
			default:
				Debug.FailedAssert("false", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\AI\\Formation\\SquareFormation.cs", "TryGetUnitPositionIndexFromLocalPosition", 575);
				num2 = 0f;
				num3 = 0f;
				break;
			}
			rankIndex = MathF.Round(num3 / (this.Distance + base.UnitDiameter));
			if (rankIndex < 0 || rankIndex >= base.RankCount || rankIndex >= this.MaxRank)
			{
				fileIndex = -1;
				return false;
			}
			int num4 = MathF.Round(num2 / (base.Interval + base.UnitDiameter));
			if (num4 >= this.UnitCountOfOuterSide - 1)
			{
				fileIndex = 1;
				return false;
			}
			int shiftedFileIndex = num4 + (this.UnitCountOfOuterSide - 1) * (int)sideOfLocalPosition;
			fileIndex = this.UnshiftFileIndex(shiftedFileIndex);
			return fileIndex >= 0 && fileIndex < base.FileCount;
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x0002F6E4 File Offset: 0x0002D8E4
		private int ShiftFileIndex(int fileIndex)
		{
			int num = this.UnitCountOfOuterSide + this.UnitCountOfOuterSide / 2 - 2;
			int num2 = fileIndex - num;
			if (num2 < 0)
			{
				num2 += (this.UnitCountOfOuterSide - 1) * 4;
			}
			return num2;
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x0002F71C File Offset: 0x0002D91C
		private int UnshiftFileIndex(int shiftedFileIndex)
		{
			int num = this.UnitCountOfOuterSide + this.UnitCountOfOuterSide / 2 - 2;
			int num2 = shiftedFileIndex + num;
			if (num2 >= (this.UnitCountOfOuterSide - 1) * 4)
			{
				num2 -= (this.UnitCountOfOuterSide - 1) * 4;
			}
			return num2;
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x0002F75A File Offset: 0x0002D95A
		protected static float GetSideWidthFromUnitCount(int sideUnitCount, float interval, float unitDiameter)
		{
			if (sideUnitCount > 0)
			{
				return (float)(sideUnitCount - 1) * (interval + unitDiameter) + unitDiameter;
			}
			return 0f;
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x0002F770 File Offset: 0x0002D970
		public override void TurnBackwards()
		{
			int num = base.FileCount / 2;
			for (int i = 0; i <= base.FileCount / 2; i++)
			{
				for (int j = 0; j < base.RankCount; j++)
				{
					int num2 = i + num;
					if (num2 < base.FileCount)
					{
						IFormationUnit unitAt = base.GetUnitAt(i, j);
						IFormationUnit unitAt2 = base.GetUnitAt(num2, j);
						if (unitAt != unitAt2)
						{
							if (unitAt != null && unitAt2 != null)
							{
								base.SwitchUnitLocations(unitAt, unitAt2);
							}
							else if (unitAt != null)
							{
								if (base.IsUnitPositionAvailable(num2, j))
								{
									base.RelocateUnit(unitAt, num2, j);
								}
							}
							else if (unitAt2 != null && base.IsUnitPositionAvailable(i, j))
							{
								base.RelocateUnit(unitAt2, i, j);
							}
						}
					}
				}
			}
		}

		// Token: 0x02000432 RID: 1074
		private enum Side
		{
			// Token: 0x04001883 RID: 6275
			Front,
			// Token: 0x04001884 RID: 6276
			Right,
			// Token: 0x04001885 RID: 6277
			Rear,
			// Token: 0x04001886 RID: 6278
			Left
		}
	}
}
