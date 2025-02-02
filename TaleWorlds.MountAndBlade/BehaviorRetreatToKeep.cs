using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000123 RID: 291
	public class BehaviorRetreatToKeep : BehaviorComponent
	{
		// Token: 0x06000DCC RID: 3532 RVA: 0x000210F0 File Offset: 0x0001F2F0
		public BehaviorRetreatToKeep(Formation formation) : base(formation)
		{
			base.CurrentOrder = MovementOrder.MovementOrderRetreat;
			base.BehaviorCoherence = 0f;
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x0002110F File Offset: 0x0001F30F
		public override void TickOccasionally()
		{
			base.TickOccasionally();
			if (base.Formation.AI.ActiveBehavior == this)
			{
				base.Formation.SetMovementOrder(base.CurrentOrder);
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000DCE RID: 3534 RVA: 0x0002113B File Offset: 0x0001F33B
		public override float NavmeshlessTargetPositionPenalty
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x00021142 File Offset: 0x0001F342
		protected override float GetAiWeight()
		{
			return 1f;
		}
	}
}
