using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200015C RID: 348
	public sealed class SiegeLadderAI : UsableMachineAIBase
	{
		// Token: 0x0600117C RID: 4476 RVA: 0x00037BF9 File Offset: 0x00035DF9
		public SiegeLadderAI(SiegeLadder ladder) : base(ladder)
		{
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x0600117D RID: 4477 RVA: 0x00037C02 File Offset: 0x00035E02
		public SiegeLadder Ladder
		{
			get
			{
				return this.UsableMachine as SiegeLadder;
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x0600117E RID: 4478 RVA: 0x00037C0F File Offset: 0x00035E0F
		public override bool HasActionCompleted
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x0600117F RID: 4479 RVA: 0x00037C12 File Offset: 0x00035E12
		protected override MovementOrder NextOrder
		{
			get
			{
				return MovementOrder.MovementOrderCharge;
			}
		}
	}
}
