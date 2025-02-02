using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000159 RID: 345
	public sealed class BatteringRamAI : UsableMachineAIBase
	{
		// Token: 0x06001171 RID: 4465 RVA: 0x00037817 File Offset: 0x00035A17
		public BatteringRamAI(BatteringRam batteringRam) : base(batteringRam)
		{
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06001172 RID: 4466 RVA: 0x00037820 File Offset: 0x00035A20
		private BatteringRam BatteringRam
		{
			get
			{
				return this.UsableMachine as BatteringRam;
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06001173 RID: 4467 RVA: 0x0003782D File Offset: 0x00035A2D
		public override bool HasActionCompleted
		{
			get
			{
				return this.BatteringRam.IsDeactivated;
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06001174 RID: 4468 RVA: 0x0003783C File Offset: 0x00035A3C
		protected override MovementOrder NextOrder
		{
			get
			{
				TeamAISiegeComponent teamAISiegeComponent;
				if ((teamAISiegeComponent = (Mission.Current.Teams[0].TeamAI as TeamAISiegeComponent)) != null && teamAISiegeComponent.InnerGate != null && !teamAISiegeComponent.InnerGate.IsDestroyed)
				{
					return MovementOrder.MovementOrderAttackEntity(teamAISiegeComponent.InnerGate.GameEntity, false);
				}
				return MovementOrder.MovementOrderCharge;
			}
		}
	}
}
