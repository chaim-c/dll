using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000142 RID: 322
	public class TransposedLineFormation : LineFormation
	{
		// Token: 0x06001013 RID: 4115 RVA: 0x0002ECB3 File Offset: 0x0002CEB3
		public TransposedLineFormation(IFormation owner) : base(owner, true)
		{
			base.IsStaggered = false;
			this.IsTransforming = true;
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x0002ECCB File Offset: 0x0002CECB
		public override IFormationArrangement Clone(IFormation formation)
		{
			return new TransposedLineFormation(formation);
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x0002ECD4 File Offset: 0x0002CED4
		public override void RearrangeFrom(IFormationArrangement arrangement)
		{
			if (arrangement is ColumnFormation)
			{
				int unitCount = arrangement.UnitCount;
				if (unitCount > 0)
				{
					int? fileCountStatic = FormOrder.GetFileCountStatic(((Formation)this.owner).FormOrder.OrderEnum, unitCount);
					if (fileCountStatic != null)
					{
						int unitCountOnLine = MathF.Ceiling((float)unitCount * 1f / (float)fileCountStatic.Value);
						base.FormFromFlankWidth(unitCountOnLine, false);
					}
				}
			}
			base.RearrangeFrom(arrangement);
		}
	}
}
