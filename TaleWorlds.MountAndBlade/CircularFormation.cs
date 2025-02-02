using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000139 RID: 313
	public class CircularFormation : LineFormation
	{
		// Token: 0x06000E8B RID: 3723 RVA: 0x0002814C File Offset: 0x0002634C
		public CircularFormation(IFormation owner) : base(owner, true, true)
		{
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x00028157 File Offset: 0x00026357
		public override IFormationArrangement Clone(IFormation formation)
		{
			return new CircularFormation(formation);
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x00028160 File Offset: 0x00026360
		private float GetDistanceFromCenterOfRank(int rankIndex)
		{
			float num = this.Radius - (float)rankIndex * (base.Distance + base.UnitDiameter);
			if (num >= 0f)
			{
				return num;
			}
			return 0f;
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x00028194 File Offset: 0x00026394
		protected override bool IsDeepenApplicable()
		{
			return this.Radius - (float)base.RankCount * (base.Distance + base.UnitDiameter) >= 0f;
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x000281BC File Offset: 0x000263BC
		protected override bool IsNarrowApplicable(int amount)
		{
			return ((float)(base.FileCount - 1 - amount) * (base.Interval + base.UnitDiameter) + base.UnitDiameter) / 6.2831855f - (float)base.RankCount * (base.Distance + base.UnitDiameter) >= 0f;
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x00028210 File Offset: 0x00026410
		private int GetUnitCountOfRank(int rankIndex)
		{
			if (rankIndex == 0)
			{
				return base.FileCount;
			}
			float distanceFromCenterOfRank = this.GetDistanceFromCenterOfRank(rankIndex);
			int b = MathF.Floor(6.2831855f * distanceFromCenterOfRank / (base.Interval + base.UnitDiameter));
			return MathF.Max(1, b);
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000E91 RID: 3729 RVA: 0x00028251 File Offset: 0x00026451
		// (set) Token: 0x06000E92 RID: 3730 RVA: 0x0002825C File Offset: 0x0002645C
		public override float Width
		{
			get
			{
				return this.Diameter;
			}
			set
			{
				float circumference = 3.1415927f * value;
				this.FormFromCircumference(circumference);
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000E93 RID: 3731 RVA: 0x0002827A File Offset: 0x0002647A
		public override float Depth
		{
			get
			{
				return this.Diameter;
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000E94 RID: 3732 RVA: 0x00028282 File Offset: 0x00026482
		private float Diameter
		{
			get
			{
				return 2f * this.Radius;
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000E95 RID: 3733 RVA: 0x00028290 File Offset: 0x00026490
		private float Radius
		{
			get
			{
				return (base.FlankWidth + base.Interval) / 6.2831855f;
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000E96 RID: 3734 RVA: 0x000282A8 File Offset: 0x000264A8
		public override float MinimumWidth
		{
			get
			{
				int unitCountWithOverride = base.GetUnitCountWithOverride();
				int currentMaximumRankCount = this.GetCurrentMaximumRankCount(unitCountWithOverride);
				float radialInterval = this.owner.MinimumInterval + base.UnitDiameter;
				float distanceInterval = this.owner.MinimumDistance + base.UnitDiameter;
				return this.GetCircumferenceAux(unitCountWithOverride, currentMaximumRankCount, radialInterval, distanceInterval) / 3.1415927f;
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000E97 RID: 3735 RVA: 0x000282FC File Offset: 0x000264FC
		public override float MaximumWidth
		{
			get
			{
				int unitCountWithOverride = base.GetUnitCountWithOverride();
				float num = this.owner.MaximumInterval + base.UnitDiameter;
				return MathF.Max(0f, (float)unitCountWithOverride * num) / 3.1415927f;
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000E98 RID: 3736 RVA: 0x00028337 File Offset: 0x00026537
		private int MaxRank
		{
			get
			{
				return MathF.Floor(this.Radius / (base.Distance + base.UnitDiameter));
			}
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x00028354 File Offset: 0x00026554
		protected override bool IsUnitPositionRestrained(int fileIndex, int rankIndex)
		{
			if (base.IsUnitPositionRestrained(fileIndex, rankIndex))
			{
				return true;
			}
			if (rankIndex > this.MaxRank)
			{
				return true;
			}
			int unitCountOfRank = this.GetUnitCountOfRank(rankIndex);
			int num = (base.FileCount - unitCountOfRank) / 2;
			return fileIndex < num || fileIndex >= num + unitCountOfRank;
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x00028398 File Offset: 0x00026598
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

		// Token: 0x06000E9B RID: 3739 RVA: 0x000283E0 File Offset: 0x000265E0
		protected override Vec2 GetLocalDirectionOfUnit(int fileIndex, int rankIndex)
		{
			int unitCountOfRank = this.GetUnitCountOfRank(rankIndex);
			int num = (base.FileCount - unitCountOfRank) / 2;
			Vec2 result = Vec2.FromRotation((float)((fileIndex - num) * 2) * 3.1415927f / (float)unitCountOfRank + 3.1415927f);
			result.x *= -1f;
			return result;
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x0002842C File Offset: 0x0002662C
		public override Vec2? GetLocalDirectionOfUnitOrDefault(IFormationUnit unit)
		{
			if (unit.FormationFileIndex < 0 || unit.FormationRankIndex < 0)
			{
				return null;
			}
			return new Vec2?(this.GetLocalDirectionOfUnit(unit.FormationFileIndex, unit.FormationRankIndex));
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x0002846C File Offset: 0x0002666C
		protected override Vec2 GetLocalPositionOfUnit(int fileIndex, int rankIndex)
		{
			Vec2 v = new Vec2(0f, -this.Radius);
			Vec2 localDirectionOfUnit = this.GetLocalDirectionOfUnit(fileIndex, rankIndex);
			float distanceFromCenterOfRank = this.GetDistanceFromCenterOfRank(rankIndex);
			return v + localDirectionOfUnit * distanceFromCenterOfRank;
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x000284A7 File Offset: 0x000266A7
		protected override Vec2 GetLocalPositionOfUnitWithAdjustment(int fileIndex, int rankIndex, float distanceBetweenAgentsAdjustment)
		{
			return this.GetLocalPositionOfUnit(fileIndex, rankIndex);
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x000284B4 File Offset: 0x000266B4
		protected override bool TryGetUnitPositionIndexFromLocalPosition(Vec2 localPosition, out int fileIndex, out int rankIndex)
		{
			Vec2 v = new Vec2(0f, -this.Radius);
			Vec2 vec = localPosition - v;
			float length = vec.Length;
			rankIndex = MathF.Round((length - this.Radius) / (base.Distance + base.UnitDiameter) * -1f);
			if (rankIndex < 0 || rankIndex >= base.RankCount)
			{
				fileIndex = -1;
				return false;
			}
			if (this.Radius - (float)rankIndex * (base.Distance + base.UnitDiameter) < 0f)
			{
				fileIndex = -1;
				return false;
			}
			int unitCountOfRank = this.GetUnitCountOfRank(rankIndex);
			int num = (base.FileCount - unitCountOfRank) / 2;
			vec.x *= -1f;
			float num2 = vec.RotationInRadians;
			num2 -= 3.1415927f;
			if (num2 < 0f)
			{
				num2 += 6.2831855f;
			}
			int num3 = MathF.Round(num2 / 2f / 3.1415927f * (float)unitCountOfRank);
			fileIndex = num3 + num;
			return fileIndex >= 0 && fileIndex < base.FileCount;
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x000285BC File Offset: 0x000267BC
		protected int GetCurrentMaximumRankCount(int unitCount)
		{
			int num = 0;
			int i = 0;
			float num2 = base.Interval + base.UnitDiameter;
			float num3 = base.Distance + base.UnitDiameter;
			while (i < unitCount)
			{
				float num4 = (float)num * num3;
				int b = (int)(6.2831855f * num4 / num2);
				i += MathF.Max(1, b);
				num++;
			}
			return MathF.Max(num, 1);
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x00028618 File Offset: 0x00026818
		public float GetCircumferenceFromRankCount(int rankCount)
		{
			int unitCountWithOverride = base.GetUnitCountWithOverride();
			rankCount = MathF.Min(this.GetCurrentMaximumRankCount(unitCountWithOverride), rankCount);
			float radialInterval = base.Interval + base.UnitDiameter;
			float distanceInterval = base.Distance + base.UnitDiameter;
			return this.GetCircumferenceAux(unitCountWithOverride, rankCount, radialInterval, distanceInterval);
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x00028664 File Offset: 0x00026864
		public void FormFromCircumference(float circumference)
		{
			int unitCountWithOverride = base.GetUnitCountWithOverride();
			int currentMaximumRankCount = this.GetCurrentMaximumRankCount(unitCountWithOverride);
			float num = base.Interval + base.UnitDiameter;
			float distanceInterval = base.Distance + base.UnitDiameter;
			float circumferenceAux = this.GetCircumferenceAux(unitCountWithOverride, currentMaximumRankCount, num, distanceInterval);
			float maxValue = MathF.Max(0f, (float)unitCountWithOverride * num);
			circumference = MBMath.ClampFloat(circumference, circumferenceAux, maxValue);
			base.FlankWidth = Math.Max(circumference - base.Interval, base.UnitDiameter);
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x000286E0 File Offset: 0x000268E0
		protected float GetCircumferenceAux(int unitCount, int rankCount, float radialInterval, float distanceInterval)
		{
			float num = (float)(6.283185307179586 * (double)distanceInterval);
			float num2 = MathF.Max(0f, (float)unitCount * radialInterval);
			float num3;
			int unitCountAux;
			do
			{
				num3 = num2;
				num2 = MathF.Max(0f, num3 - num);
				unitCountAux = CircularFormation.GetUnitCountAux(num2, rankCount, radialInterval, distanceInterval);
			}
			while (unitCountAux > unitCount && num3 > 0f);
			return num3;
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x00028734 File Offset: 0x00026934
		private static int GetUnitCountAux(float circumference, int rankCount, float radialInterval, float distanceInterval)
		{
			int num = 0;
			double num2 = 6.283185307179586 * (double)distanceInterval;
			for (int i = 1; i <= rankCount; i++)
			{
				num += (int)(Math.Max(0.0, (double)circumference - (double)(rankCount - i) * num2) / (double)radialInterval);
			}
			return num;
		}
	}
}
