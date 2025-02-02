using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000111 RID: 273
	public class BehaviorDefendSiegeWeapon : BehaviorComponent
	{
		// Token: 0x06000D51 RID: 3409 RVA: 0x0001C7C4 File Offset: 0x0001A9C4
		public BehaviorDefendSiegeWeapon(Formation formation) : base(formation)
		{
			this.CalculateCurrentOrder();
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x0001C7DE File Offset: 0x0001A9DE
		public void SetDefensePositionFromTactic(WorldPosition defensePosition)
		{
			this._defensePosition = defensePosition;
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x0001C7E7 File Offset: 0x0001A9E7
		public void SetDefendedSiegeWeaponFromTactic(SiegeWeapon siegeWeapon)
		{
			this._defendedSiegeWeapon = siegeWeapon;
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x0001C7F0 File Offset: 0x0001A9F0
		public override TextObject GetBehaviorString()
		{
			TextObject behaviorString = base.GetBehaviorString();
			TextObject variable = GameTexts.FindText("str_formation_ai_side_strings", base.Formation.AI.Side.ToString());
			behaviorString.SetTextVariable("SIDE_STRING", variable);
			behaviorString.SetTextVariable("IS_GENERAL_SIDE", "0");
			return behaviorString;
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x0001C84C File Offset: 0x0001AA4C
		protected override void CalculateCurrentOrder()
		{
			float num = 5f;
			Vec2 vec;
			if (this._tacticalDefendPosition != null)
			{
				if (!this._tacticalDefendPosition.IsInsurmountable)
				{
					vec = this._tacticalDefendPosition.Direction;
				}
				else
				{
					vec = (base.Formation.Team.QuerySystem.AverageEnemyPosition - this._tacticalDefendPosition.Position.AsVec2).Normalized();
				}
			}
			else if (base.Formation.QuerySystem.ClosestEnemyFormation == null)
			{
				vec = base.Formation.Direction;
			}
			else if (this._defendedSiegeWeapon != null)
			{
				vec = base.Formation.QuerySystem.ClosestEnemyFormation.MedianPosition.AsVec2 - this._defendedSiegeWeapon.GameEntity.GlobalPosition.AsVec2;
				num = vec.Normalize();
				num = MathF.Min(num, 5f);
				float b;
				if (this._defendedSiegeWeapon.WaitEntity != null)
				{
					b = (this._defendedSiegeWeapon.WaitEntity.GlobalPosition - this._defendedSiegeWeapon.GameEntity.GlobalPosition).Length;
				}
				else
				{
					b = 3f;
				}
				num = MathF.Max(num, b);
			}
			else
			{
				vec = ((base.Formation.Direction.DotProduct((base.Formation.QuerySystem.ClosestEnemyFormation.MedianPosition.AsVec2 - base.Formation.QuerySystem.AveragePosition).Normalized()) < 0.5f) ? (base.Formation.QuerySystem.ClosestEnemyFormation.MedianPosition.AsVec2 - base.Formation.QuerySystem.AveragePosition) : base.Formation.Direction).Normalized();
			}
			if (this._tacticalDefendPosition != null)
			{
				if (!this._tacticalDefendPosition.IsInsurmountable)
				{
					base.CurrentOrder = MovementOrder.MovementOrderMove(this._tacticalDefendPosition.Position);
				}
				else
				{
					Vec2 vec2 = this._tacticalDefendPosition.Position.AsVec2 + this._tacticalDefendPosition.Width * 0.5f * vec;
					WorldPosition position = this._tacticalDefendPosition.Position;
					position.SetVec2(vec2);
					base.CurrentOrder = MovementOrder.MovementOrderMove(position);
				}
				if (!this._tacticalDefendPosition.IsInsurmountable)
				{
					this.CurrentFacingOrder = FacingOrder.FacingOrderLookAtDirection(vec);
					return;
				}
				this.CurrentFacingOrder = FacingOrder.FacingOrderLookAtEnemy;
				return;
			}
			else
			{
				if (this._defensePosition.IsValid)
				{
					WorldPosition defensePosition = this._defensePosition;
					defensePosition.SetVec2(this._defensePosition.AsVec2 + vec * num);
					base.CurrentOrder = MovementOrder.MovementOrderMove(defensePosition);
					this.CurrentFacingOrder = FacingOrder.FacingOrderLookAtDirection(vec);
					return;
				}
				WorldPosition medianPosition = base.Formation.QuerySystem.MedianPosition;
				medianPosition.SetVec2(base.Formation.QuerySystem.AveragePosition);
				base.CurrentOrder = MovementOrder.MovementOrderMove(medianPosition);
				this.CurrentFacingOrder = FacingOrder.FacingOrderLookAtDirection(vec);
				return;
			}
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x0001CB78 File Offset: 0x0001AD78
		public override void TickOccasionally()
		{
			this.CalculateCurrentOrder();
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.FacingOrder = this.CurrentFacingOrder;
			if (base.Formation.QuerySystem.AveragePosition.DistanceSquared(base.CurrentOrder.GetPosition(base.Formation)) < 100f)
			{
				if (base.Formation.QuerySystem.HasShield)
				{
					base.Formation.ArrangementOrder = ArrangementOrder.ArrangementOrderLine;
				}
				else if (base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation != null && base.Formation.QuerySystem.AveragePosition.DistanceSquared(base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.MedianPosition.AsVec2) > 100f && base.Formation.QuerySystem.UnderRangedAttackRatio > 0.2f - ((base.Formation.ArrangementOrder.OrderEnum == ArrangementOrder.ArrangementOrderEnum.Loose) ? 0.1f : 0f))
				{
					base.Formation.ArrangementOrder = ArrangementOrder.ArrangementOrderLoose;
				}
				if (this._tacticalDefendPosition != null)
				{
					float customWidth;
					if (this._tacticalDefendPosition.TacticalPositionType == TacticalPosition.TacticalPositionTypeEnum.ChokePoint)
					{
						customWidth = this._tacticalDefendPosition.Width;
					}
					else
					{
						int countOfUnits = base.Formation.CountOfUnits;
						float num = base.Formation.Interval * (float)(countOfUnits - 1) + base.Formation.UnitDiameter * (float)countOfUnits;
						customWidth = MathF.Min(this._tacticalDefendPosition.Width, num / 3f);
					}
					base.Formation.FormOrder = FormOrder.FormOrderCustom(customWidth);
					return;
				}
			}
			else
			{
				base.Formation.ArrangementOrder = ArrangementOrder.ArrangementOrderLoose;
			}
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x0001CD34 File Offset: 0x0001AF34
		protected override void OnBehaviorActivatedAux()
		{
			this.CalculateCurrentOrder();
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.FacingOrder = this.CurrentFacingOrder;
			base.Formation.ArrangementOrder = ArrangementOrder.ArrangementOrderLoose;
			base.Formation.FiringOrder = FiringOrder.FiringOrderFireAtWill;
			base.Formation.FormOrder = FormOrder.FormOrderWide;
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x0001CD99 File Offset: 0x0001AF99
		public override void ResetBehavior()
		{
			base.ResetBehavior();
			this._defensePosition = WorldPosition.Invalid;
			this._tacticalDefendPosition = null;
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x0001CDB3 File Offset: 0x0001AFB3
		protected override float GetAiWeight()
		{
			return 1f;
		}

		// Token: 0x0400032D RID: 813
		private WorldPosition _defensePosition = WorldPosition.Invalid;

		// Token: 0x0400032E RID: 814
		private TacticalPosition _tacticalDefendPosition;

		// Token: 0x0400032F RID: 815
		private SiegeWeapon _defendedSiegeWeapon;
	}
}
