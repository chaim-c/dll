using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000146 RID: 326
	public class WedgeFormation : LineFormation
	{
		// Token: 0x0600103F RID: 4159 RVA: 0x0002F81D File Offset: 0x0002DA1D
		public WedgeFormation(IFormation owner) : base(owner, true)
		{
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x0002F827 File Offset: 0x0002DA27
		public override IFormationArrangement Clone(IFormation formation)
		{
			return new WedgeFormation(formation);
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x0002F830 File Offset: 0x0002DA30
		private int GetUnitCountOfRank(int rankIndex)
		{
			int b = rankIndex * 2 * 3 + 3;
			return MathF.Min(base.FileCount, b);
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x0002F854 File Offset: 0x0002DA54
		protected override bool IsUnitPositionRestrained(int fileIndex, int rankIndex)
		{
			if (base.IsUnitPositionRestrained(fileIndex, rankIndex))
			{
				return true;
			}
			int unitCountOfRank = this.GetUnitCountOfRank(rankIndex);
			int num = (base.FileCount - unitCountOfRank) / 2;
			return fileIndex < num || fileIndex >= num + unitCountOfRank;
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x0002F890 File Offset: 0x0002DA90
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
	}
}
