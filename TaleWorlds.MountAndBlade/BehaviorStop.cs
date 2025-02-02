using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000131 RID: 305
	public class BehaviorStop : BehaviorComponent
	{
		// Token: 0x06000E42 RID: 3650 RVA: 0x0002557D File Offset: 0x0002377D
		public BehaviorStop(Formation formation) : base(formation)
		{
			base.CurrentOrder = MovementOrder.MovementOrderStop;
			base.BehaviorCoherence = 0f;
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x0002559C File Offset: 0x0002379C
		public override void TickOccasionally()
		{
			base.Formation.SetMovementOrder(base.CurrentOrder);
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x000255B0 File Offset: 0x000237B0
		protected override void OnBehaviorActivatedAux()
		{
			base.Formation.ArrangementOrder = (base.Formation.QuerySystem.HasShield ? ArrangementOrder.ArrangementOrderShieldWall : ArrangementOrder.ArrangementOrderLine);
			base.Formation.FiringOrder = FiringOrder.FiringOrderFireAtWill;
			this._lastPlayerInformTime = Mission.Current.CurrentTime;
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000E45 RID: 3653 RVA: 0x00025606 File Offset: 0x00023806
		public override float NavmeshlessTargetPositionPenalty
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x0002560D File Offset: 0x0002380D
		protected override float GetAiWeight()
		{
			return 0.01f;
		}
	}
}
