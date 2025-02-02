using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200013A RID: 314
	public class CircularSchiltronFormation : CircularFormation
	{
		// Token: 0x06000EA5 RID: 3749 RVA: 0x0002877B File Offset: 0x0002697B
		public CircularSchiltronFormation(IFormation owner) : base(owner)
		{
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x00028784 File Offset: 0x00026984
		public override IFormationArrangement Clone(IFormation formation)
		{
			return new CircularSchiltronFormation(formation);
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000EA7 RID: 3751 RVA: 0x0002878C File Offset: 0x0002698C
		public override float MaximumWidth
		{
			get
			{
				int unitCountWithOverride = base.GetUnitCountWithOverride();
				int currentMaximumRankCount = base.GetCurrentMaximumRankCount(unitCountWithOverride);
				float radialInterval = this.owner.MaximumInterval + base.UnitDiameter;
				float distanceInterval = this.owner.MaximumDistance + base.UnitDiameter;
				return base.GetCircumferenceAux(unitCountWithOverride, currentMaximumRankCount, radialInterval, distanceInterval) / 3.1415927f;
			}
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x000287E0 File Offset: 0x000269E0
		public void Form()
		{
			int unitCountWithOverride = base.GetUnitCountWithOverride();
			int currentMaximumRankCount = base.GetCurrentMaximumRankCount(unitCountWithOverride);
			float circumferenceFromRankCount = base.GetCircumferenceFromRankCount(currentMaximumRankCount);
			base.FormFromCircumference(circumferenceFromRankCount);
		}
	}
}
