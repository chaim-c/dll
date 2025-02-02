using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000113 RID: 275
	public class BehaviorDestroySiegeWeapons : BehaviorComponent
	{
		// Token: 0x06000D60 RID: 3424 RVA: 0x0001D1D4 File Offset: 0x0001B3D4
		private void DetermineTargetWeapons()
		{
			this._targetWeapons = (from w in this._allWeapons
			where w is IPrimarySiegeWeapon && (w as IPrimarySiegeWeapon).WeaponSide == this._behaviorSide && w.IsDestructible && !w.IsDestroyed && !w.IsDisabled
			select w).ToList<SiegeWeapon>();
			if (this._targetWeapons.IsEmpty<SiegeWeapon>())
			{
				this._targetWeapons = (from w in this._allWeapons
				where !(w is IPrimarySiegeWeapon) && w.IsDestructible && !w.IsDestroyed && !w.IsDisabled
				select w).ToList<SiegeWeapon>();
				this._isTargetPrimaryWeapon = false;
				return;
			}
			this._isTargetPrimaryWeapon = true;
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x0001D254 File Offset: 0x0001B454
		public BehaviorDestroySiegeWeapons(Formation formation) : base(formation)
		{
			base.BehaviorCoherence = 0.2f;
			this._behaviorSide = formation.AI.Side;
			this._allWeapons = (from sw in Mission.Current.ActiveMissionObjects.FindAllWithType<SiegeWeapon>()
			where sw.Side != formation.Team.Side
			select sw).ToList<SiegeWeapon>();
			this.DetermineTargetWeapons();
			base.CurrentOrder = MovementOrder.MovementOrderCharge;
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x0001D2D8 File Offset: 0x0001B4D8
		public override TextObject GetBehaviorString()
		{
			TextObject behaviorString = base.GetBehaviorString();
			TextObject variable = GameTexts.FindText("str_formation_ai_side_strings", base.Formation.AI.Side.ToString());
			behaviorString.SetTextVariable("SIDE_STRING", variable);
			behaviorString.SetTextVariable("IS_GENERAL_SIDE", "0");
			return behaviorString;
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x0001D332 File Offset: 0x0001B532
		public override void OnValidBehaviorSideChanged()
		{
			base.OnValidBehaviorSideChanged();
			this.DetermineTargetWeapons();
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x0001D340 File Offset: 0x0001B540
		public override void TickOccasionally()
		{
			base.TickOccasionally();
			this._targetWeapons.RemoveAll((SiegeWeapon tw) => tw.IsDestroyed);
			if (this._targetWeapons.Count == 0)
			{
				this.DetermineTargetWeapons();
			}
			if (base.Formation.AI.ActiveBehavior == this)
			{
				if (this._targetWeapons.Count == 0)
				{
					MovementOrder currentOrder = base.CurrentOrder;
					if (currentOrder != MovementOrder.MovementOrderCharge)
					{
						base.CurrentOrder = MovementOrder.MovementOrderCharge;
					}
					this._isTargetPrimaryWeapon = false;
				}
				else
				{
					SiegeWeapon siegeWeapon = this._targetWeapons.MinBy((SiegeWeapon tw) => base.Formation.QuerySystem.AveragePosition.DistanceSquared(tw.GameEntity.GlobalPosition.AsVec2));
					if (base.CurrentOrder.OrderEnum != MovementOrder.MovementOrderEnum.AttackEntity || this.LastTargetWeapon != siegeWeapon)
					{
						this.LastTargetWeapon = siegeWeapon;
						base.CurrentOrder = MovementOrder.MovementOrderAttackEntity(this.LastTargetWeapon.GameEntity, true);
					}
				}
				base.Formation.SetMovementOrder(base.CurrentOrder);
			}
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x0001D43C File Offset: 0x0001B63C
		protected override void OnBehaviorActivatedAux()
		{
			this.DetermineTargetWeapons();
			base.Formation.ArrangementOrder = ((base.Formation.QuerySystem.IsCavalryFormation || base.Formation.QuerySystem.IsRangedCavalryFormation) ? ArrangementOrder.ArrangementOrderSkein : ArrangementOrder.ArrangementOrderLine);
			base.Formation.FacingOrder = FacingOrder.FacingOrderLookAtEnemy;
			base.Formation.FiringOrder = FiringOrder.FiringOrderFireAtWill;
			base.Formation.FormOrder = FormOrder.FormOrderDeep;
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000D66 RID: 3430 RVA: 0x0001D4BA File Offset: 0x0001B6BA
		public override float NavmeshlessTargetPositionPenalty
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x0001D4C1 File Offset: 0x0001B6C1
		protected override float GetAiWeight()
		{
			if (this._targetWeapons.IsEmpty<SiegeWeapon>())
			{
				return 0f;
			}
			if (!this._isTargetPrimaryWeapon)
			{
				return 0.7f;
			}
			return 1f;
		}

		// Token: 0x04000331 RID: 817
		private readonly List<SiegeWeapon> _allWeapons;

		// Token: 0x04000332 RID: 818
		private List<SiegeWeapon> _targetWeapons;

		// Token: 0x04000333 RID: 819
		public SiegeWeapon LastTargetWeapon;

		// Token: 0x04000334 RID: 820
		private bool _isTargetPrimaryWeapon;
	}
}
