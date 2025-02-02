using System;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000110 RID: 272
	public class BehaviorDefendKeyPosition : BehaviorComponent
	{
		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000D49 RID: 3401 RVA: 0x0001C527 File Offset: 0x0001A727
		// (set) Token: 0x06000D4A RID: 3402 RVA: 0x0001C534 File Offset: 0x0001A734
		public WorldPosition DefensePosition
		{
			get
			{
				return this._behaviorPosition.Value;
			}
			set
			{
				this._defensePosition = value;
			}
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x0001C540 File Offset: 0x0001A740
		public BehaviorDefendKeyPosition(Formation formation) : base(formation)
		{
			this._behaviorPosition = new QueryData<WorldPosition>(() => Mission.Current.FindBestDefendingPosition(this.EnemyClusterPosition, this._defensePosition), 5f);
			this.CalculateCurrentOrder();
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x0001C58C File Offset: 0x0001A78C
		protected override void CalculateCurrentOrder()
		{
			Vec2 direction;
			if (base.Formation.QuerySystem.ClosestEnemyFormation == null)
			{
				direction = base.Formation.Direction;
			}
			else
			{
				direction = ((base.Formation.Direction.DotProduct((base.Formation.QuerySystem.ClosestEnemyFormation.MedianPosition.AsVec2 - base.Formation.QuerySystem.AveragePosition).Normalized()) < 0.5f) ? (base.Formation.QuerySystem.ClosestEnemyFormation.MedianPosition.AsVec2 - base.Formation.QuerySystem.AveragePosition) : base.Formation.Direction).Normalized();
			}
			if (this.DefensePosition.IsValid)
			{
				base.CurrentOrder = MovementOrder.MovementOrderMove(this.DefensePosition);
			}
			else
			{
				WorldPosition medianPosition = base.Formation.QuerySystem.MedianPosition;
				medianPosition.SetVec2(base.Formation.QuerySystem.AveragePosition);
				base.CurrentOrder = MovementOrder.MovementOrderMove(medianPosition);
			}
			this.CurrentFacingOrder = FacingOrder.FacingOrderLookAtDirection(direction);
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x0001C6C0 File Offset: 0x0001A8C0
		public override void TickOccasionally()
		{
			this.CalculateCurrentOrder();
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.FacingOrder = this.CurrentFacingOrder;
			if (base.Formation.QuerySystem.HasShield && base.Formation.QuerySystem.AveragePosition.DistanceSquared(base.CurrentOrder.GetPosition(base.Formation)) < base.Formation.Depth * base.Formation.Depth * 4f)
			{
				base.Formation.ArrangementOrder = ArrangementOrder.ArrangementOrderShieldWall;
				return;
			}
			base.Formation.ArrangementOrder = ArrangementOrder.ArrangementOrderLoose;
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x0001C773 File Offset: 0x0001A973
		protected override void OnBehaviorActivatedAux()
		{
			base.Formation.ArrangementOrder = ArrangementOrder.ArrangementOrderLoose;
			base.Formation.FiringOrder = FiringOrder.FiringOrderFireAtWill;
			base.Formation.FormOrder = FormOrder.FormOrderWide;
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x0001C7A5 File Offset: 0x0001A9A5
		protected override float GetAiWeight()
		{
			return 10f;
		}

		// Token: 0x0400032A RID: 810
		private WorldPosition _defensePosition = WorldPosition.Invalid;

		// Token: 0x0400032B RID: 811
		public WorldPosition EnemyClusterPosition = WorldPosition.Invalid;

		// Token: 0x0400032C RID: 812
		private readonly QueryData<WorldPosition> _behaviorPosition;
	}
}
