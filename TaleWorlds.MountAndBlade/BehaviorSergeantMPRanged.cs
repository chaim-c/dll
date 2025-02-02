using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.LinQuick;
using TaleWorlds.MountAndBlade.Objects;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200012A RID: 298
	public class BehaviorSergeantMPRanged : BehaviorComponent
	{
		// Token: 0x06000E0D RID: 3597 RVA: 0x000230DC File Offset: 0x000212DC
		public BehaviorSergeantMPRanged(Formation formation) : base(formation)
		{
			this._flagpositions = base.Formation.Team.Mission.ActiveMissionObjects.FindAllWithType<FlagCapturePoint>().ToList<FlagCapturePoint>();
			this._flagDominationGameMode = base.Formation.Team.Mission.GetMissionBehavior<MissionMultiplayerFlagDomination>();
			this.CalculateCurrentOrder();
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x00023138 File Offset: 0x00021338
		protected override void CalculateCurrentOrder()
		{
			bool flag = false;
			Formation formation = null;
			float num = float.MaxValue;
			foreach (Team team in base.Formation.Team.Mission.Teams)
			{
				if (team.IsEnemyOf(base.Formation.Team))
				{
					for (int i = 0; i < Math.Min(team.FormationsIncludingSpecialAndEmpty.Count, 8); i++)
					{
						Formation formation2 = team.FormationsIncludingSpecialAndEmpty[i];
						if (formation2.CountOfUnits > 0)
						{
							flag = true;
							if (formation2.QuerySystem.IsCavalryFormation || formation2.QuerySystem.IsRangedCavalryFormation)
							{
								float num2 = formation2.QuerySystem.MedianPosition.AsVec2.DistanceSquared(base.Formation.QuerySystem.AveragePosition);
								if (num2 < num)
								{
									num = num2;
									formation = formation2;
								}
							}
						}
					}
				}
			}
			if (base.Formation.Team.FormationsIncludingEmpty.AnyQ((Formation f) => f.CountOfUnits > 0 && f != base.Formation && f.QuerySystem.IsInfantryFormation))
			{
				this._attachedInfantry = (from f in base.Formation.Team.FormationsIncludingEmpty
				where f.CountOfUnits > 0 && f != base.Formation && f.QuerySystem.IsInfantryFormation
				select f).MinBy((Formation f) => f.QuerySystem.MedianPosition.AsVec2.DistanceSquared(base.Formation.QuerySystem.AveragePosition));
				Formation formation3 = null;
				if (flag)
				{
					if (base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.MedianPosition.AsVec2.DistanceSquared(base.Formation.QuerySystem.AveragePosition) <= 4900f)
					{
						formation3 = base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.Formation;
					}
					else if (formation != null)
					{
						formation3 = formation;
					}
				}
				Vec2 vec = (formation3 == null) ? this._attachedInfantry.Direction : (formation3.QuerySystem.MedianPosition.AsVec2 - this._attachedInfantry.QuerySystem.MedianPosition.AsVec2).Normalized();
				WorldPosition medianPosition = this._attachedInfantry.QuerySystem.MedianPosition;
				medianPosition.SetVec2(medianPosition.AsVec2 - vec * ((this._attachedInfantry.Depth + base.Formation.Depth) / 2f));
				base.CurrentOrder = MovementOrder.MovementOrderMove(medianPosition);
				this.CurrentFacingOrder = FacingOrder.FacingOrderLookAtDirection(vec);
				return;
			}
			if (base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation != null && base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.MedianPosition.AsVec2.DistanceSquared(base.Formation.QuerySystem.AveragePosition) <= 4900f)
			{
				Vec2 vec2 = (base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.MedianPosition.AsVec2 - base.Formation.QuerySystem.AveragePosition).Normalized();
				float num3 = base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.MedianPosition.AsVec2.Distance(base.Formation.QuerySystem.AveragePosition);
				WorldPosition medianPosition2 = base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.MedianPosition;
				if (num3 > base.Formation.QuerySystem.MissileRangeAdjusted)
				{
					medianPosition2.SetVec2(medianPosition2.AsVec2 - vec2 * (base.Formation.QuerySystem.MissileRangeAdjusted - base.Formation.Depth * 0.5f));
				}
				else if (num3 < base.Formation.QuerySystem.MissileRangeAdjusted * 0.4f)
				{
					medianPosition2.SetVec2(medianPosition2.AsVec2 - vec2 * (base.Formation.QuerySystem.MissileRangeAdjusted * 0.4f));
				}
				else
				{
					medianPosition2.SetVec2(base.Formation.QuerySystem.AveragePosition);
				}
				base.CurrentOrder = MovementOrder.MovementOrderMove(medianPosition2);
				this.CurrentFacingOrder = FacingOrder.FacingOrderLookAtDirection(vec2);
				return;
			}
			if (this._flagpositions.Any((FlagCapturePoint fp) => this._flagDominationGameMode.GetFlagOwnerTeam(fp) != base.Formation.Team))
			{
				Vec3 position = (from fp in this._flagpositions
				where this._flagDominationGameMode.GetFlagOwnerTeam(fp) != base.Formation.Team
				select fp).MinBy((FlagCapturePoint fp) => fp.Position.AsVec2.DistanceSquared(base.Formation.QuerySystem.AveragePosition)).Position;
				if (base.CurrentOrder.OrderEnum == MovementOrder.MovementOrderEnum.Invalid || base.CurrentOrder.GetPosition(base.Formation) != position.AsVec2)
				{
					Vec2 direction;
					if (base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation == null)
					{
						direction = base.Formation.Direction;
					}
					else
					{
						direction = (base.Formation.QuerySystem.ClosestSignificantlyLargeEnemyFormation.MedianPosition.AsVec2 - base.Formation.QuerySystem.AveragePosition).Normalized();
					}
					WorldPosition position2 = new WorldPosition(base.Formation.Team.Mission.Scene, UIntPtr.Zero, position, false);
					base.CurrentOrder = MovementOrder.MovementOrderMove(position2);
					this.CurrentFacingOrder = FacingOrder.FacingOrderLookAtDirection(direction);
					return;
				}
			}
			else
			{
				if (this._flagpositions.Any((FlagCapturePoint fp) => this._flagDominationGameMode.GetFlagOwnerTeam(fp) == base.Formation.Team))
				{
					Vec3 position3 = (from fp in this._flagpositions
					where this._flagDominationGameMode.GetFlagOwnerTeam(fp) == base.Formation.Team
					select fp).MinBy((FlagCapturePoint fp) => fp.Position.AsVec2.DistanceSquared(base.Formation.QuerySystem.AveragePosition)).Position;
					base.CurrentOrder = MovementOrder.MovementOrderMove(new WorldPosition(base.Formation.Team.Mission.Scene, UIntPtr.Zero, position3, false));
					this.CurrentFacingOrder = FacingOrder.FacingOrderLookAtEnemy;
					return;
				}
				WorldPosition medianPosition3 = base.Formation.QuerySystem.MedianPosition;
				medianPosition3.SetVec2(base.Formation.QuerySystem.AveragePosition);
				base.CurrentOrder = MovementOrder.MovementOrderMove(medianPosition3);
				this.CurrentFacingOrder = FacingOrder.FacingOrderLookAtEnemy;
			}
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x00023758 File Offset: 0x00021958
		public override void TickOccasionally()
		{
			this._flagpositions.RemoveAll((FlagCapturePoint fp) => fp.IsDeactivated);
			this.CalculateCurrentOrder();
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.FacingOrder = this.CurrentFacingOrder;
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x000237B8 File Offset: 0x000219B8
		protected override void OnBehaviorActivatedAux()
		{
			this.CalculateCurrentOrder();
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.FacingOrder = this.CurrentFacingOrder;
			base.Formation.ArrangementOrder = ArrangementOrder.ArrangementOrderLoose;
			base.Formation.FiringOrder = FiringOrder.FiringOrderFireAtWill;
			base.Formation.FormOrder = FormOrder.FormOrderWide;
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x0002381D File Offset: 0x00021A1D
		protected override float GetAiWeight()
		{
			if (base.Formation.QuerySystem.IsRangedFormation)
			{
				return 1.2f;
			}
			return 0f;
		}

		// Token: 0x04000362 RID: 866
		private List<FlagCapturePoint> _flagpositions;

		// Token: 0x04000363 RID: 867
		private Formation _attachedInfantry;

		// Token: 0x04000364 RID: 868
		private MissionMultiplayerFlagDomination _flagDominationGameMode;
	}
}
