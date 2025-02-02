using System;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000130 RID: 304
	public class BehaviorSparseSkirmish : BehaviorComponent
	{
		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000E3A RID: 3642 RVA: 0x000253AA File Offset: 0x000235AA
		// (set) Token: 0x06000E3B RID: 3643 RVA: 0x000253B2 File Offset: 0x000235B2
		public GameEntity ArcherPosition
		{
			get
			{
				return this._archerPosition;
			}
			set
			{
				if (this._archerPosition != value)
				{
					this.SetArcherPosition(value);
				}
			}
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x000253CC File Offset: 0x000235CC
		private void SetArcherPosition(GameEntity value)
		{
			this._archerPosition = value;
			if (!(this._archerPosition != null))
			{
				this._tacticalArcherPosition = null;
				WorldPosition medianPosition = base.Formation.QuerySystem.MedianPosition;
				medianPosition.SetVec2(base.Formation.CurrentPosition);
				base.CurrentOrder = MovementOrder.MovementOrderMove(medianPosition);
				this.CurrentFacingOrder = FacingOrder.FacingOrderLookAtEnemy;
				return;
			}
			this._tacticalArcherPosition = this._archerPosition.GetFirstScriptOfType<TacticalPosition>();
			if (this._tacticalArcherPosition != null)
			{
				base.CurrentOrder = MovementOrder.MovementOrderMove(this._tacticalArcherPosition.Position);
				this.CurrentFacingOrder = FacingOrder.FacingOrderLookAtDirection(this._tacticalArcherPosition.Direction);
				return;
			}
			base.CurrentOrder = MovementOrder.MovementOrderMove(this._archerPosition.GlobalPosition.ToWorldPosition());
			this.CurrentFacingOrder = FacingOrder.FacingOrderLookAtEnemy;
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x0002549C File Offset: 0x0002369C
		public BehaviorSparseSkirmish(Formation formation) : base(formation)
		{
			this.SetArcherPosition(this._archerPosition);
			base.BehaviorCoherence = 0f;
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x000254BC File Offset: 0x000236BC
		public override void TickOccasionally()
		{
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.FacingOrder = this.CurrentFacingOrder;
			if (this._tacticalArcherPosition != null)
			{
				base.Formation.FormOrder = FormOrder.FormOrderCustom(this._tacticalArcherPosition.Width);
			}
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x00025510 File Offset: 0x00023710
		protected override void OnBehaviorActivatedAux()
		{
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.FacingOrder = this.CurrentFacingOrder;
			base.Formation.ArrangementOrder = ArrangementOrder.ArrangementOrderScatter;
			base.Formation.FiringOrder = FiringOrder.FiringOrderFireAtWill;
			base.Formation.FormOrder = FormOrder.FormOrderWider;
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000E40 RID: 3648 RVA: 0x0002556F File Offset: 0x0002376F
		public override float NavmeshlessTargetPositionPenalty
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x00025576 File Offset: 0x00023776
		protected override float GetAiWeight()
		{
			return 2f;
		}

		// Token: 0x04000374 RID: 884
		private GameEntity _archerPosition;

		// Token: 0x04000375 RID: 885
		private TacticalPosition _tacticalArcherPosition;
	}
}
