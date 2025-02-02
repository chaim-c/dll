using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000144 RID: 324
	public class SkeinFormation : LineFormation
	{
		// Token: 0x0600101A RID: 4122 RVA: 0x0002EDAA File Offset: 0x0002CFAA
		public SkeinFormation(IFormation owner) : base(owner, true)
		{
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x0002EDB4 File Offset: 0x0002CFB4
		public override IFormationArrangement Clone(IFormation formation)
		{
			return new SkeinFormation(formation);
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x0002EDBC File Offset: 0x0002CFBC
		protected override Vec2 GetLocalPositionOfUnit(int fileIndex, int rankIndex)
		{
			float num = (float)(base.FileCount - 1) * (base.Interval + base.UnitDiameter);
			Vec2 result = new Vec2((float)fileIndex * (base.Interval + base.UnitDiameter) - num / 2f, (float)(-(float)rankIndex) * (base.Distance + base.UnitDiameter));
			float offsetOfFile = this.GetOffsetOfFile(fileIndex);
			result.y -= offsetOfFile;
			return result;
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x0002EE28 File Offset: 0x0002D028
		protected override Vec2 GetLocalPositionOfUnitWithAdjustment(int fileIndex, int rankIndex, float distanceBetweenAgentsAdjustment)
		{
			float num = base.Interval + distanceBetweenAgentsAdjustment;
			float num2 = (float)(base.FileCount - 1) * (num + base.UnitDiameter);
			Vec2 result = new Vec2((float)fileIndex * (num + base.UnitDiameter) - num2 / 2f, (float)(-(float)rankIndex) * (base.Distance + base.UnitDiameter));
			float offsetOfFile = this.GetOffsetOfFile(fileIndex);
			result.y -= offsetOfFile;
			return result;
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x0002EE94 File Offset: 0x0002D094
		private float GetOffsetOfFile(int fileIndex)
		{
			int num = base.FileCount / 2;
			return (float)MathF.Abs(fileIndex - num) * (base.Interval + base.UnitDiameter) / 2f;
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x0002EEC8 File Offset: 0x0002D0C8
		protected override bool TryGetUnitPositionIndexFromLocalPosition(Vec2 localPosition, out int fileIndex, out int rankIndex)
		{
			float num = (float)(base.FileCount - 1) * (base.Interval + base.UnitDiameter);
			fileIndex = MathF.Round((localPosition.x + num / 2f) / (base.Interval + base.UnitDiameter));
			if (fileIndex < 0 || fileIndex >= base.FileCount)
			{
				rankIndex = -1;
				return false;
			}
			float offsetOfFile = this.GetOffsetOfFile(fileIndex);
			localPosition.y += offsetOfFile;
			rankIndex = MathF.Round(-localPosition.y / (base.Distance + base.UnitDiameter));
			if (rankIndex < 0 || rankIndex >= base.RankCount)
			{
				fileIndex = -1;
				return false;
			}
			return true;
		}
	}
}
