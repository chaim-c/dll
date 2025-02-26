﻿using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200012D RID: 301
	public class BehaviorSkirmish : BehaviorComponent
	{
		// Token: 0x06000E28 RID: 3624 RVA: 0x00023D60 File Offset: 0x00021F60
		public BehaviorSkirmish(Formation formation) : base(formation)
		{
			base.BehaviorCoherence = 0.5f;
			this._cantShootTimer = new Timer(0f, 0f, true);
			this._pullBackTimer = new Timer(0f, 0f, true);
			this.CalculateCurrentOrder();
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x00023DD0 File Offset: 0x00021FD0
		protected override void CalculateCurrentOrder()
		{
			WorldPosition position = base.Formation.QuerySystem.MedianPosition;
			bool flag = false;
			Vec2 vec;
			if (base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation == null)
			{
				vec = base.Formation.Direction;
				position.SetVec2(base.Formation.QuerySystem.AveragePosition);
			}
			else
			{
				vec = base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.MedianPosition.AsVec2 - base.Formation.QuerySystem.AveragePosition;
				float num = vec.Normalize();
				float num2 = base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.CurrentVelocity.DotProduct(vec);
				float num3 = MBMath.Lerp(5f, 10f, (MBMath.ClampFloat((float)base.Formation.CountOfUnits, 10f, 60f) - 10f) * 0.02f, 1E-05f) * num2;
				num += num3;
				float num4 = MBMath.Lerp(0.1f, 0.33f, 1f - MBMath.ClampFloat((float)base.Formation.CountOfUnits, 1f, 50f) * 0.02f, 1E-05f) * base.Formation.QuerySystem.RangedUnitRatio;
				switch (this._behaviorState)
				{
				case BehaviorSkirmish.BehaviorState.Approaching:
					if (num < this._cantShootDistance * 0.8f)
					{
						this._behaviorState = BehaviorSkirmish.BehaviorState.Shooting;
						this._cantShoot = false;
						flag = true;
					}
					else if (base.Formation.QuerySystem.MakingRangedAttackRatio >= num4 * 1.2f)
					{
						this._behaviorState = BehaviorSkirmish.BehaviorState.Shooting;
						this._cantShoot = false;
						flag = true;
					}
					break;
				case BehaviorSkirmish.BehaviorState.Shooting:
					if (base.Formation.QuerySystem.MakingRangedAttackRatio <= num4)
					{
						if (num > base.Formation.QuerySystem.MaximumMissileRange)
						{
							this._behaviorState = BehaviorSkirmish.BehaviorState.Approaching;
							this._cantShootDistance = MathF.Min(this._cantShootDistance, base.Formation.QuerySystem.MaximumMissileRange * 0.9f);
						}
						else if (!this._cantShoot)
						{
							this._cantShoot = true;
							this._cantShootTimer.Reset(Mission.Current.CurrentTime, MBMath.Lerp(5f, 10f, (MBMath.ClampFloat((float)base.Formation.CountOfUnits, 10f, 60f) - 10f) * 0.02f, 1E-05f));
						}
						else if (this._cantShootTimer.Check(Mission.Current.CurrentTime))
						{
							this._behaviorState = BehaviorSkirmish.BehaviorState.Approaching;
							this._cantShootDistance = MathF.Min(this._cantShootDistance, num);
						}
					}
					else
					{
						this._cantShootDistance = MathF.Max(this._cantShootDistance, num);
						this._cantShoot = false;
						if (this._pullBackTimer.Check(Mission.Current.CurrentTime) && base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.IsInfantryFormation && num < MathF.Min(base.Formation.QuerySystem.MissileRangeAdjusted * 0.4f, this._cantShootDistance * 0.666f))
						{
							this._behaviorState = BehaviorSkirmish.BehaviorState.PullingBack;
							this._pullBackTimer.Reset(Mission.Current.CurrentTime, 10f);
						}
					}
					break;
				case BehaviorSkirmish.BehaviorState.PullingBack:
					if (num > MathF.Min(this._cantShootDistance, base.Formation.QuerySystem.MissileRangeAdjusted) * 0.8f)
					{
						this._behaviorState = BehaviorSkirmish.BehaviorState.Shooting;
						this._cantShoot = false;
						flag = true;
					}
					else if (this._pullBackTimer.Check(Mission.Current.CurrentTime) && base.Formation.QuerySystem.MakingRangedAttackRatio <= num4 * 0.5f)
					{
						this._behaviorState = BehaviorSkirmish.BehaviorState.Shooting;
						this._cantShoot = false;
						flag = true;
						this._pullBackTimer.Reset(Mission.Current.CurrentTime, 5f);
					}
					break;
				}
				switch (this._behaviorState)
				{
				case BehaviorSkirmish.BehaviorState.Approaching:
				{
					bool flag2 = false;
					if (this._alternatePositionUsed)
					{
						float num5 = base.Formation.QuerySystem.AveragePosition.DistanceSquared(base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.AveragePosition);
						Vec2 v = (base.Formation.QuerySystem.AveragePosition + base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.AveragePosition) * 0.5f;
						bool flag3 = (double)this._alternatePosition.AsVec2.DistanceSquared(v) > (double)num5 * 0.0625;
						if (!flag3)
						{
							int num6 = -1;
							Vec3 navMeshVec = this._alternatePosition.GetNavMeshVec3();
							Mission.Current.Scene.GetNavigationMeshForPosition(ref navMeshVec, out num6, 1.5f);
							Agent medianAgent = base.Formation.GetMedianAgent(true, true, base.Formation.QuerySystem.AveragePosition);
							flag3 = ((medianAgent != null && medianAgent.GetCurrentNavigationFaceId() % 10 == 1) != (num6 % 10 == 1));
						}
						if (flag3)
						{
							Agent medianAgent2 = base.Formation.GetMedianAgent(true, true, base.Formation.QuerySystem.AveragePosition);
							bool flag4 = medianAgent2 != null && medianAgent2.GetCurrentNavigationFaceId() % 10 == 1;
							Agent medianAgent3 = base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.Formation.GetMedianAgent(true, true, base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.AveragePosition);
							if (flag4 == (medianAgent3 != null && medianAgent3.GetCurrentNavigationFaceId() % 10 == 1))
							{
								this._alternatePositionUsed = false;
								this._alternatePosition = WorldPosition.Invalid;
							}
							else
							{
								flag2 = true;
							}
						}
					}
					else if (Mission.Current.MissionTeamAIType == Mission.MissionTeamAITypeEnum.Siege || Mission.Current.MissionTeamAIType == Mission.MissionTeamAITypeEnum.SallyOut)
					{
						Agent medianAgent4 = base.Formation.GetMedianAgent(true, true, base.Formation.QuerySystem.AveragePosition);
						bool flag5 = medianAgent4 != null && medianAgent4.GetCurrentNavigationFaceId() % 10 == 1;
						Agent medianAgent5 = base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.Formation.GetMedianAgent(true, true, base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.AveragePosition);
						if (flag5 != (medianAgent5 != null && medianAgent5.GetCurrentNavigationFaceId() % 10 == 1))
						{
							this._alternatePositionUsed = true;
							flag2 = true;
						}
					}
					if (this._alternatePositionUsed)
					{
						if (flag2)
						{
							this._alternatePosition = new WorldPosition(Mission.Current.Scene, new Vec3((base.Formation.QuerySystem.AveragePosition + base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.AveragePosition) * 0.5f, base.Formation.QuerySystem.MedianPosition.GetNavMeshZ(), -1f));
						}
						position = this._alternatePosition;
					}
					else
					{
						position = base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.MedianPosition;
						position.SetVec2(base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.AveragePosition);
					}
					break;
				}
				case BehaviorSkirmish.BehaviorState.Shooting:
					position.SetVec2(base.Formation.QuerySystem.AveragePosition + base.Formation.QuerySystem.CurrentVelocity.Normalized() * (base.Formation.Depth * 0.5f));
					break;
				case BehaviorSkirmish.BehaviorState.PullingBack:
					position = base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.MedianPosition;
					position.SetVec2(position.AsVec2 - vec * (base.Formation.QuerySystem.MissileRangeAdjusted - base.Formation.Depth * 0.5f - 10f));
					break;
				}
			}
			if (!base.CurrentOrder.GetPosition(base.Formation).IsValid || this._behaviorState != BehaviorSkirmish.BehaviorState.Shooting || flag)
			{
				base.CurrentOrder = MovementOrder.MovementOrderMove(position);
			}
			if (!this.CurrentFacingOrder.GetDirection(base.Formation, null).IsValid || this._behaviorState != BehaviorSkirmish.BehaviorState.Shooting || flag)
			{
				this.CurrentFacingOrder = FacingOrder.FacingOrderLookAtDirection(vec);
			}
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x000245ED File Offset: 0x000227ED
		public override void TickOccasionally()
		{
			this.CalculateCurrentOrder();
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.FacingOrder = this.CurrentFacingOrder;
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x00024618 File Offset: 0x00022818
		protected override void OnBehaviorActivatedAux()
		{
			this._cantShoot = false;
			this._cantShootDistance = float.MaxValue;
			this._behaviorState = BehaviorSkirmish.BehaviorState.Shooting;
			this._cantShootTimer.Reset(Mission.Current.CurrentTime, MBMath.Lerp(5f, 10f, (MBMath.ClampFloat((float)base.Formation.CountOfUnits, 10f, 60f) - 10f) * 0.02f, 1E-05f));
			this._pullBackTimer.Reset(0f, 0f);
			this.CalculateCurrentOrder();
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.FacingOrder = this.CurrentFacingOrder;
			base.Formation.ArrangementOrder = ArrangementOrder.ArrangementOrderLoose;
			base.Formation.FiringOrder = FiringOrder.FiringOrderFireAtWill;
			base.Formation.FormOrder = FormOrder.FormOrderWide;
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x000246FC File Offset: 0x000228FC
		protected override float GetAiWeight()
		{
			FormationQuerySystem querySystem = base.Formation.QuerySystem;
			return MBMath.Lerp(0.1f, 1f, MBMath.ClampFloat(querySystem.RangedUnitRatio + querySystem.RangedCavalryUnitRatio, 0f, 0.5f) * 2f, 1E-05f);
		}

		// Token: 0x04000369 RID: 873
		private bool _cantShoot;

		// Token: 0x0400036A RID: 874
		private float _cantShootDistance = float.MaxValue;

		// Token: 0x0400036B RID: 875
		private bool _alternatePositionUsed;

		// Token: 0x0400036C RID: 876
		private WorldPosition _alternatePosition = WorldPosition.Invalid;

		// Token: 0x0400036D RID: 877
		private BehaviorSkirmish.BehaviorState _behaviorState = BehaviorSkirmish.BehaviorState.Shooting;

		// Token: 0x0400036E RID: 878
		private Timer _cantShootTimer;

		// Token: 0x0400036F RID: 879
		private Timer _pullBackTimer;

		// Token: 0x02000419 RID: 1049
		private enum BehaviorState
		{
			// Token: 0x04001802 RID: 6146
			Approaching,
			// Token: 0x04001803 RID: 6147
			Shooting,
			// Token: 0x04001804 RID: 6148
			PullingBack
		}
	}
}
