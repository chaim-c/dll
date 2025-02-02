using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000121 RID: 289
	public class BehaviorRetreat : BehaviorComponent
	{
		// Token: 0x06000DC3 RID: 3523 RVA: 0x00020F9B File Offset: 0x0001F19B
		public BehaviorRetreat(Formation formation) : base(formation)
		{
			base.CurrentOrder = MovementOrder.MovementOrderRetreat;
			base.BehaviorCoherence = 0f;
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x00020FBA File Offset: 0x0001F1BA
		public override void TickOccasionally()
		{
			base.Formation.SetMovementOrder(base.CurrentOrder);
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x00020FCD File Offset: 0x0001F1CD
		protected override void OnBehaviorActivatedAux()
		{
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000DC6 RID: 3526 RVA: 0x00020FCF File Offset: 0x0001F1CF
		public override float NavmeshlessTargetPositionPenalty
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x00020FD8 File Offset: 0x0001F1D8
		protected override float GetAiWeight()
		{
			float casualtyPowerLossOfFormation = Mission.Current.GetMissionBehavior<CasualtyHandler>().GetCasualtyPowerLossOfFormation(base.Formation);
			float num = MathF.Sqrt(casualtyPowerLossOfFormation / (base.Formation.QuerySystem.FormationPower + casualtyPowerLossOfFormation));
			return MBMath.ClampFloat(base.Formation.Team.QuerySystem.TotalPowerRatio, 0.1f, 3f) / MBMath.ClampFloat(base.Formation.Team.QuerySystem.RemainingPowerRatio, 0.1f, 3f) * (0.05f + num);
		}
	}
}
