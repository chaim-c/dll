using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200011C RID: 284
	public class BehaviorProtectGeneral : BehaviorComponent
	{
		// Token: 0x06000D9E RID: 3486 RVA: 0x0001FBC8 File Offset: 0x0001DDC8
		public BehaviorProtectGeneral(Formation formation) : base(formation)
		{
			base.CurrentOrder = MovementOrder.MovementOrderFollow((formation.Team.GeneralsFormation != null && formation.Team.GeneralsFormation.CountOfUnits > 0) ? formation.Team.GeneralsFormation.GetFirstUnit() : Mission.Current.MainAgent);
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x0001FC23 File Offset: 0x0001DE23
		public override void TickOccasionally()
		{
			base.Formation.SetMovementOrder(base.CurrentOrder);
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000DA0 RID: 3488 RVA: 0x0001FC36 File Offset: 0x0001DE36
		public override float NavmeshlessTargetPositionPenalty
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x0001FC40 File Offset: 0x0001DE40
		protected override float GetAiWeight()
		{
			if ((base.Formation.Team.GeneralsFormation != null && base.Formation.Team.GeneralsFormation.CountOfUnits > 0) || (base.Formation.Team.IsPlayerTeam && base.Formation.Team.IsPlayerGeneral && Mission.Current.MainAgent != null))
			{
				return 1f;
			}
			return 0f;
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x0001FCB4 File Offset: 0x0001DEB4
		public override void OnAgentRemoved(Agent agent)
		{
			if (base.CurrentOrder._targetAgent == agent)
			{
				base.CurrentOrder = MovementOrder.MovementOrderNull;
			}
		}
	}
}
