using System;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200012B RID: 299
	public class BehaviorShootFromCastleWalls : BehaviorComponent
	{
		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000E1B RID: 3611 RVA: 0x00023993 File Offset: 0x00021B93
		// (set) Token: 0x06000E1C RID: 3612 RVA: 0x0002399B File Offset: 0x00021B9B
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
					this.OnArcherPositionSet(value);
				}
			}
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x000239B2 File Offset: 0x00021BB2
		public BehaviorShootFromCastleWalls(Formation formation) : base(formation)
		{
			this.OnArcherPositionSet(this._archerPosition);
			base.BehaviorCoherence = 0f;
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x000239D4 File Offset: 0x00021BD4
		private void OnArcherPositionSet(GameEntity value)
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
			this.CurrentFacingOrder = FacingOrder.FacingOrderLookAtDirection(this._archerPosition.GetGlobalFrame().rotation.f.AsVec2);
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x00023AC4 File Offset: 0x00021CC4
		public override void TickOccasionally()
		{
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.FacingOrder = this.CurrentFacingOrder;
			if (this._tacticalArcherPosition != null)
			{
				base.Formation.FormOrder = FormOrder.FormOrderCustom(this._tacticalArcherPosition.Width);
			}
			foreach (Team team in base.Formation.Team.Mission.Teams)
			{
				if (team.IsEnemyOf(base.Formation.Team))
				{
					if (!this._areStrategicArcherAreasAbandoned)
					{
						if (team.QuerySystem.InsideWallsRatio > 0.6f)
						{
							base.Formation.ArrangementOrder = ArrangementOrder.ArrangementOrderLine;
							this._areStrategicArcherAreasAbandoned = true;
							break;
						}
						break;
					}
					else
					{
						if (team.QuerySystem.InsideWallsRatio <= 0.4f)
						{
							base.Formation.ArrangementOrder = ArrangementOrder.ArrangementOrderScatter;
							this._areStrategicArcherAreasAbandoned = false;
							break;
						}
						break;
					}
				}
			}
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x00023BD8 File Offset: 0x00021DD8
		protected override void OnBehaviorActivatedAux()
		{
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.FacingOrder = this.CurrentFacingOrder;
			base.Formation.ArrangementOrder = ArrangementOrder.ArrangementOrderScatter;
			base.Formation.FiringOrder = FiringOrder.FiringOrderFireAtWill;
			base.Formation.FormOrder = FormOrder.FormOrderWide;
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000E21 RID: 3617 RVA: 0x00023C37 File Offset: 0x00021E37
		public override float NavmeshlessTargetPositionPenalty
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x00023C3E File Offset: 0x00021E3E
		protected override float GetAiWeight()
		{
			return 10f * (base.Formation.QuerySystem.RangedCavalryUnitRatio + base.Formation.QuerySystem.RangedUnitRatio);
		}

		// Token: 0x04000365 RID: 869
		private GameEntity _archerPosition;

		// Token: 0x04000366 RID: 870
		private TacticalPosition _tacticalArcherPosition;

		// Token: 0x04000367 RID: 871
		private bool _areStrategicArcherAreasAbandoned;
	}
}
