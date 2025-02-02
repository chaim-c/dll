using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000143 RID: 323
	public class RectilinearSchiltronFormation : SquareFormation
	{
		// Token: 0x06001016 RID: 4118 RVA: 0x0002ED3F File Offset: 0x0002CF3F
		public RectilinearSchiltronFormation(IFormation owner) : base(owner)
		{
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x0002ED48 File Offset: 0x0002CF48
		public override IFormationArrangement Clone(IFormation formation)
		{
			return new RectilinearSchiltronFormation(formation);
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06001018 RID: 4120 RVA: 0x0002ED50 File Offset: 0x0002CF50
		public override float MaximumWidth
		{
			get
			{
				int num;
				int maximumRankCount = SquareFormation.GetMaximumRankCount(base.GetUnitCountWithOverride(), out num);
				return SquareFormation.GetSideWidthFromUnitCount(base.GetUnitsPerSideFromRankCount(maximumRankCount), this.owner.MaximumInterval, base.UnitDiameter);
			}
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x0002ED88 File Offset: 0x0002CF88
		public void Form()
		{
			int num;
			int maximumRankCount = SquareFormation.GetMaximumRankCount(base.GetUnitCountWithOverride(), out num);
			base.FormFromRankCount(maximumRankCount);
		}
	}
}
