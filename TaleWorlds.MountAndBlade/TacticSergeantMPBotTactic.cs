using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000172 RID: 370
	public class TacticSergeantMPBotTactic : TacticComponent
	{
		// Token: 0x060012AB RID: 4779 RVA: 0x000469DB File Offset: 0x00044BDB
		public TacticSergeantMPBotTactic(Team team) : base(team)
		{
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x000469E4 File Offset: 0x00044BE4
		protected internal override void TickOccasionally()
		{
			foreach (Formation formation in base.FormationsIncludingEmpty)
			{
				if (formation.CountOfUnits > 0)
				{
					formation.AI.ResetBehaviorWeights();
					formation.AI.SetBehaviorWeight<BehaviorCharge>(1f);
					formation.AI.SetBehaviorWeight<BehaviorTacticalCharge>(1f);
					formation.AI.SetBehaviorWeight<BehaviorSergeantMPInfantry>(1f);
					formation.AI.SetBehaviorWeight<BehaviorSergeantMPRanged>(1f);
					formation.AI.SetBehaviorWeight<BehaviorSergeantMPMounted>(1f);
					formation.AI.SetBehaviorWeight<BehaviorSergeantMPMountedRanged>(1f);
					formation.AI.SetBehaviorWeight<BehaviorSergeantMPLastFlagLastStand>(1f);
				}
			}
		}
	}
}
