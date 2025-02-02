using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000173 RID: 371
	public class TacticStop : TacticComponent
	{
		// Token: 0x060012AD RID: 4781 RVA: 0x00046AC4 File Offset: 0x00044CC4
		public TacticStop(Team team) : base(team)
		{
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x00046AD0 File Offset: 0x00044CD0
		protected internal override void TickOccasionally()
		{
			foreach (Formation formation in base.FormationsIncludingEmpty)
			{
				if (formation.CountOfUnits > 0)
				{
					formation.AI.ResetBehaviorWeights();
					formation.AI.SetBehaviorWeight<BehaviorStop>(1f);
				}
			}
			base.TickOccasionally();
		}
	}
}
