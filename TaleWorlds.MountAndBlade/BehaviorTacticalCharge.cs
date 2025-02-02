using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000132 RID: 306
	public class BehaviorTacticalCharge : BehaviorComponent
	{
		// Token: 0x06000E47 RID: 3655 RVA: 0x00025614 File Offset: 0x00023814
		public BehaviorTacticalCharge(Formation formation) : base(formation)
		{
			this._lastTarget = null;
			base.CurrentOrder = MovementOrder.MovementOrderCharge;
			this.CurrentFacingOrder = FacingOrder.FacingOrderLookAtEnemy;
			this._chargeState = BehaviorTacticalCharge.ChargeState.Undetermined;
			base.BehaviorCoherence = 0.5f;
			this._desiredChargeStopDistance = 20f;
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x00025670 File Offset: 0x00023870
		public override void TickOccasionally()
		{
			base.TickOccasionally();
			if (base.Formation.AI.ActiveBehavior != this)
			{
				return;
			}
			this.CalculateCurrentOrder();
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.FacingOrder = this.CurrentFacingOrder;
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x000256C0 File Offset: 0x000238C0
		private BehaviorTacticalCharge.ChargeState CheckAndChangeState()
		{
			BehaviorTacticalCharge.ChargeState result = this._chargeState;
			if (base.Formation.QuerySystem.ClosestEnemyFormation == null)
			{
				result = BehaviorTacticalCharge.ChargeState.Undetermined;
			}
			else
			{
				switch (this._chargeState)
				{
				case BehaviorTacticalCharge.ChargeState.Undetermined:
					if (base.Formation.QuerySystem.ClosestEnemyFormation != null && ((!base.Formation.QuerySystem.IsCavalryFormation && !base.Formation.QuerySystem.IsRangedCavalryFormation) || base.Formation.QuerySystem.AveragePosition.Distance(base.Formation.QuerySystem.ClosestEnemyFormation.MedianPosition.AsVec2) / base.Formation.QuerySystem.MovementSpeedMaximum <= 5f))
					{
						result = BehaviorTacticalCharge.ChargeState.Charging;
					}
					break;
				case BehaviorTacticalCharge.ChargeState.Charging:
					if (this._lastTarget == null || this._lastTarget.Formation.CountOfUnits == 0)
					{
						result = BehaviorTacticalCharge.ChargeState.Undetermined;
					}
					else if (!base.Formation.QuerySystem.IsCavalryFormation && !base.Formation.QuerySystem.IsRangedCavalryFormation)
					{
						if (!base.Formation.QuerySystem.IsInfantryFormation || !base.Formation.QuerySystem.ClosestEnemyFormation.IsCavalryFormation)
						{
							result = BehaviorTacticalCharge.ChargeState.Charging;
						}
						else
						{
							Vec2 vec = base.Formation.QuerySystem.AveragePosition - base.Formation.QuerySystem.ClosestEnemyFormation.AveragePosition;
							float num = vec.Normalize();
							Vec2 currentVelocity = base.Formation.QuerySystem.ClosestEnemyFormation.CurrentVelocity;
							float num2 = currentVelocity.Normalize();
							if (num / num2 <= 6f && vec.DotProduct(currentVelocity) > 0.5f)
							{
								this._chargeState = BehaviorTacticalCharge.ChargeState.Bracing;
							}
						}
					}
					else if (this._initialChargeDirection.DotProduct(base.Formation.QuerySystem.ClosestEnemyFormation.MedianPosition.AsVec2 - base.Formation.QuerySystem.AveragePosition) <= 0f)
					{
						result = BehaviorTacticalCharge.ChargeState.ChargingPast;
					}
					break;
				case BehaviorTacticalCharge.ChargeState.ChargingPast:
					if (this._chargingPastTimer.Check(Mission.Current.CurrentTime) || base.Formation.QuerySystem.AveragePosition.Distance(base.Formation.QuerySystem.ClosestEnemyFormation.MedianPosition.AsVec2) >= this._desiredChargeStopDistance)
					{
						result = BehaviorTacticalCharge.ChargeState.Reforming;
					}
					break;
				case BehaviorTacticalCharge.ChargeState.Reforming:
					if (this._reformTimer.Check(Mission.Current.CurrentTime) || base.Formation.QuerySystem.AveragePosition.Distance(base.Formation.QuerySystem.ClosestEnemyFormation.MedianPosition.AsVec2) <= 30f)
					{
						result = BehaviorTacticalCharge.ChargeState.Charging;
					}
					break;
				case BehaviorTacticalCharge.ChargeState.Bracing:
				{
					bool flag = false;
					if (base.Formation.QuerySystem.IsInfantryFormation && base.Formation.QuerySystem.ClosestEnemyFormation.IsCavalryFormation)
					{
						Vec2 vec2 = base.Formation.QuerySystem.AveragePosition - base.Formation.QuerySystem.ClosestEnemyFormation.AveragePosition;
						float num3 = vec2.Normalize();
						Vec2 currentVelocity2 = base.Formation.QuerySystem.ClosestEnemyFormation.CurrentVelocity;
						float num4 = currentVelocity2.Normalize();
						if (num3 / num4 <= 8f && vec2.DotProduct(currentVelocity2) > 0.33f)
						{
							flag = true;
						}
					}
					if (!flag)
					{
						this._bracePosition = Vec2.Invalid;
						this._chargeState = BehaviorTacticalCharge.ChargeState.Charging;
					}
					break;
				}
				}
			}
			return result;
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x00025A5C File Offset: 0x00023C5C
		protected override void CalculateCurrentOrder()
		{
			if (base.Formation.QuerySystem.ClosestEnemyFormation == null || ((base.Formation.QuerySystem.IsCavalryFormation || base.Formation.QuerySystem.IsRangedCavalryFormation) && (base.Formation.QuerySystem.ClosestEnemyFormation.IsCavalryFormation || base.Formation.QuerySystem.ClosestEnemyFormation.IsRangedCavalryFormation)))
			{
				base.CurrentOrder = MovementOrder.MovementOrderCharge;
				return;
			}
			BehaviorTacticalCharge.ChargeState chargeState = this.CheckAndChangeState();
			if (chargeState != this._chargeState)
			{
				this._chargeState = chargeState;
				switch (this._chargeState)
				{
				case BehaviorTacticalCharge.ChargeState.Undetermined:
					base.CurrentOrder = MovementOrder.MovementOrderCharge;
					break;
				case BehaviorTacticalCharge.ChargeState.Charging:
					this._lastTarget = base.Formation.QuerySystem.ClosestEnemyFormation;
					if (base.Formation.QuerySystem.IsCavalryFormation || base.Formation.QuerySystem.IsRangedCavalryFormation)
					{
						this._initialChargeDirection = this._lastTarget.MedianPosition.AsVec2 - base.Formation.QuerySystem.AveragePosition;
						float value = this._initialChargeDirection.Normalize();
						this._desiredChargeStopDistance = MBMath.ClampFloat(value, 20f, 50f);
					}
					break;
				case BehaviorTacticalCharge.ChargeState.ChargingPast:
					this._chargingPastTimer = new Timer(Mission.Current.CurrentTime, 5f, true);
					break;
				case BehaviorTacticalCharge.ChargeState.Reforming:
					this._reformTimer = new Timer(Mission.Current.CurrentTime, 2f, true);
					break;
				case BehaviorTacticalCharge.ChargeState.Bracing:
				{
					Vec2 v = (base.Formation.QuerySystem.Team.MedianTargetFormationPosition.AsVec2 - base.Formation.QuerySystem.AveragePosition).Normalized();
					this._bracePosition = base.Formation.QuerySystem.AveragePosition + v * 5f;
					break;
				}
				}
			}
			switch (this._chargeState)
			{
			case BehaviorTacticalCharge.ChargeState.Undetermined:
				if (base.Formation.QuerySystem.ClosestEnemyFormation != null && (base.Formation.QuerySystem.IsCavalryFormation || base.Formation.QuerySystem.IsRangedCavalryFormation))
				{
					base.CurrentOrder = MovementOrder.MovementOrderMove(base.Formation.QuerySystem.ClosestEnemyFormation.MedianPosition);
				}
				else
				{
					base.CurrentOrder = MovementOrder.MovementOrderCharge;
				}
				this.CurrentFacingOrder = FacingOrder.FacingOrderLookAtEnemy;
				return;
			case BehaviorTacticalCharge.ChargeState.Charging:
			{
				if (base.Formation.QuerySystem.IsCavalryFormation || base.Formation.QuerySystem.IsRangedCavalryFormation)
				{
					Vec2 vec = (this._lastTarget.MedianPosition.AsVec2 - base.Formation.QuerySystem.AveragePosition).Normalized();
					WorldPosition medianPosition = this._lastTarget.MedianPosition;
					Vec2 vec2 = medianPosition.AsVec2 + vec * this._desiredChargeStopDistance;
					medianPosition.SetVec2(vec2);
					base.CurrentOrder = MovementOrder.MovementOrderMove(medianPosition);
					this.CurrentFacingOrder = FacingOrder.FacingOrderLookAtDirection(vec);
					return;
				}
				if (base.Formation.Width >= base.Formation.QuerySystem.ClosestEnemyFormation.Formation.Width * (1f + ((base.Formation.GetReadonlyMovementOrderReference().OrderEnum != MovementOrder.MovementOrderEnum.Charge) ? 0.1f : 0f)))
				{
					base.CurrentOrder = MovementOrder.MovementOrderCharge;
					this.CurrentFacingOrder = FacingOrder.FacingOrderLookAtEnemy;
					return;
				}
				WorldPosition medianPosition2 = base.Formation.QuerySystem.ClosestEnemyFormation.MedianPosition;
				base.CurrentOrder = MovementOrder.MovementOrderMove(medianPosition2);
				this.CurrentFacingOrder = FacingOrder.FacingOrderLookAtEnemy;
				return;
			}
			case BehaviorTacticalCharge.ChargeState.ChargingPast:
			{
				Vec2 vec3 = (base.Formation.QuerySystem.AveragePosition - this._lastTarget.MedianPosition.AsVec2).Normalized();
				this._lastReformDestination = this._lastTarget.MedianPosition;
				Vec2 vec4 = this._lastTarget.MedianPosition.AsVec2 + vec3 * this._desiredChargeStopDistance;
				this._lastReformDestination.SetVec2(vec4);
				base.CurrentOrder = MovementOrder.MovementOrderMove(this._lastReformDestination);
				this.CurrentFacingOrder = FacingOrder.FacingOrderLookAtDirection(vec3);
				return;
			}
			case BehaviorTacticalCharge.ChargeState.Reforming:
				base.CurrentOrder = MovementOrder.MovementOrderMove(this._lastReformDestination);
				this.CurrentFacingOrder = FacingOrder.FacingOrderLookAtEnemy;
				return;
			case BehaviorTacticalCharge.ChargeState.Bracing:
			{
				WorldPosition medianPosition3 = base.Formation.QuerySystem.MedianPosition;
				medianPosition3.SetVec2(this._bracePosition);
				base.CurrentOrder = MovementOrder.MovementOrderMove(medianPosition3);
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x00025F0C File Offset: 0x0002410C
		protected override void OnBehaviorActivatedAux()
		{
			this.CalculateCurrentOrder();
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.FacingOrder = this.CurrentFacingOrder;
			if (base.Formation.QuerySystem.IsCavalryFormation || base.Formation.QuerySystem.IsRangedCavalryFormation)
			{
				base.Formation.ArrangementOrder = ArrangementOrder.ArrangementOrderSkein;
			}
			else
			{
				base.Formation.ArrangementOrder = ArrangementOrder.ArrangementOrderLine;
			}
			base.Formation.FiringOrder = FiringOrder.FiringOrderFireAtWill;
			base.Formation.FormOrder = FormOrder.FormOrderWide;
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x00025FA8 File Offset: 0x000241A8
		public override TextObject GetBehaviorString()
		{
			TextObject behaviorString = base.GetBehaviorString();
			if (base.Formation.QuerySystem.ClosestEnemyFormation != null)
			{
				behaviorString.SetTextVariable("AI_SIDE", GameTexts.FindText("str_formation_ai_side_strings", base.Formation.QuerySystem.ClosestEnemyFormation.Formation.AI.Side.ToString()));
				behaviorString.SetTextVariable("CLASS", GameTexts.FindText("str_formation_class_string", base.Formation.QuerySystem.ClosestEnemyFormation.Formation.PhysicalClass.GetName()));
			}
			return behaviorString;
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000E4D RID: 3661 RVA: 0x00026047 File Offset: 0x00024247
		public override float NavmeshlessTargetPositionPenalty
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x00026050 File Offset: 0x00024250
		private float CalculateAIWeight()
		{
			FormationQuerySystem querySystem = base.Formation.QuerySystem;
			if (querySystem.ClosestEnemyFormation == null)
			{
				return 0f;
			}
			float num = querySystem.AveragePosition.Distance(querySystem.ClosestEnemyFormation.MedianPosition.AsVec2) / querySystem.MovementSpeedMaximum;
			float num3;
			if (!querySystem.IsCavalryFormation && !querySystem.IsRangedCavalryFormation)
			{
				float num2 = MBMath.ClampFloat(num, 4f, 10f);
				num3 = MBMath.Lerp(0.8f, 1f, 1f - (num2 - 4f) / 6f, 1E-05f);
			}
			else if (num <= 4f)
			{
				float num4 = MBMath.ClampFloat(num, 0f, 4f);
				num3 = MBMath.Lerp(0.8f, 1.2f, num4 / 4f, 1E-05f);
			}
			else
			{
				float num5 = MBMath.ClampFloat(num, 4f, 10f);
				num3 = MBMath.Lerp(0.8f, 1.2f, 1f - (num5 - 4f) / 6f, 1E-05f);
			}
			float num6 = 1f;
			if (num <= 4f)
			{
				float length = (querySystem.AveragePosition - querySystem.ClosestEnemyFormation.MedianPosition.AsVec2).Length;
				if (length > 1E-45f)
				{
					WorldPosition medianPosition = querySystem.MedianPosition;
					medianPosition.SetVec2(querySystem.AveragePosition);
					float navMeshZ = medianPosition.GetNavMeshZ();
					if (!float.IsNaN(navMeshZ))
					{
						float value = (navMeshZ - querySystem.ClosestEnemyFormation.MedianPosition.GetNavMeshZ()) / length;
						num6 = MBMath.Lerp(0.9f, 1.1f, (MBMath.ClampFloat(value, -0.58f, 0.58f) + 0.58f) / 1.16f, 1E-05f);
					}
				}
			}
			float num7 = 1f;
			if (num <= 4f && num >= 1.5f)
			{
				num7 = 1.2f;
			}
			float num8 = 1f;
			if (num <= 4f && querySystem.ClosestEnemyFormation.ClosestEnemyFormation != querySystem)
			{
				num8 = 1.2f;
			}
			float num9 = querySystem.GetClassWeightedFactor(1f, 1f, 1.5f, 1.5f) * querySystem.ClosestEnemyFormation.GetClassWeightedFactor(1f, 1f, 0.5f, 0.5f);
			return num3 * num6 * num7 * num8 * num9;
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x000262A8 File Offset: 0x000244A8
		protected override float GetAiWeight()
		{
			float result = 0f;
			if (base.Formation.QuerySystem.ClosestEnemyFormation == null)
			{
				return 0f;
			}
			bool flag;
			if (!(base.Formation.Team.TeamAI is TeamAISiegeComponent))
			{
				flag = true;
			}
			else if ((base.Formation.Team.TeamAI as TeamAISiegeComponent).CalculateIsChargePastWallsApplicable(base.Formation.AI.Side))
			{
				flag = true;
			}
			else
			{
				bool flag2 = TeamAISiegeComponent.IsFormationInsideCastle(base.Formation.QuerySystem.ClosestEnemyFormation.Formation, true, 0.51f);
				flag = (flag2 == TeamAISiegeComponent.IsFormationInsideCastle(base.Formation, true, flag2 ? 0.9f : 0.1f));
			}
			if (flag)
			{
				result = this.CalculateAIWeight();
			}
			return result;
		}

		// Token: 0x04000376 RID: 886
		private BehaviorTacticalCharge.ChargeState _chargeState;

		// Token: 0x04000377 RID: 887
		private FormationQuerySystem _lastTarget;

		// Token: 0x04000378 RID: 888
		private Vec2 _initialChargeDirection;

		// Token: 0x04000379 RID: 889
		private float _desiredChargeStopDistance;

		// Token: 0x0400037A RID: 890
		private WorldPosition _lastReformDestination;

		// Token: 0x0400037B RID: 891
		private Timer _chargingPastTimer;

		// Token: 0x0400037C RID: 892
		private Timer _reformTimer;

		// Token: 0x0400037D RID: 893
		private Vec2 _bracePosition = Vec2.Invalid;

		// Token: 0x0200041B RID: 1051
		private enum ChargeState
		{
			// Token: 0x0400180A RID: 6154
			Undetermined,
			// Token: 0x0400180B RID: 6155
			Charging,
			// Token: 0x0400180C RID: 6156
			ChargingPast,
			// Token: 0x0400180D RID: 6157
			Reforming,
			// Token: 0x0400180E RID: 6158
			Bracing
		}
	}
}
