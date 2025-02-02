using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200015D RID: 349
	public sealed class SiegeTowerAI : UsableMachineAIBase
	{
		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06001180 RID: 4480 RVA: 0x00037C19 File Offset: 0x00035E19
		private SiegeTower SiegeTower
		{
			get
			{
				return this.UsableMachine as SiegeTower;
			}
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x00037C26 File Offset: 0x00035E26
		public SiegeTowerAI(SiegeTower siegeTower) : base(siegeTower)
		{
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06001182 RID: 4482 RVA: 0x00037C2F File Offset: 0x00035E2F
		public override bool HasActionCompleted
		{
			get
			{
				return this.SiegeTower.MovementComponent.HasArrivedAtTarget && this.SiegeTower.State == SiegeTower.GateState.Open;
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06001183 RID: 4483 RVA: 0x00037C53 File Offset: 0x00035E53
		protected override MovementOrder NextOrder
		{
			get
			{
				return MovementOrder.MovementOrderCharge;
			}
		}
	}
}
