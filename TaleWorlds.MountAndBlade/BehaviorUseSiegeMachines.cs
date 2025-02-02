using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000134 RID: 308
	public class BehaviorUseSiegeMachines : BehaviorComponent
	{
		// Token: 0x06000E54 RID: 3668 RVA: 0x000264C0 File Offset: 0x000246C0
		public BehaviorUseSiegeMachines(Formation formation) : base(formation)
		{
			this._behaviorSide = formation.AI.Side;
			this._primarySiegeWeapons = new List<SiegeWeapon>();
			foreach (MissionObject missionObject in Mission.Current.ActiveMissionObjects)
			{
				IPrimarySiegeWeapon primarySiegeWeapon;
				if ((primarySiegeWeapon = (missionObject as IPrimarySiegeWeapon)) != null && primarySiegeWeapon.WeaponSide == this._behaviorSide)
				{
					this._primarySiegeWeapons.Add(missionObject as SiegeWeapon);
				}
			}
			this._teamAISiegeComponent = (TeamAISiegeComponent)formation.Team.TeamAI;
			base.BehaviorCoherence = 0f;
			this._stopOrder = MovementOrder.MovementOrderStop;
			this.RecreateFollowEntityOrder();
			if (this._followEntityOrder.OrderEnum != MovementOrder.MovementOrderEnum.Invalid)
			{
				this._behaviorState = BehaviorUseSiegeMachines.BehaviorState.Follow;
				base.CurrentOrder = this._followEntityOrder;
				return;
			}
			this._behaviorState = BehaviorUseSiegeMachines.BehaviorState.Stop;
			base.CurrentOrder = this._stopOrder;
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x000265C4 File Offset: 0x000247C4
		public override TextObject GetBehaviorString()
		{
			TextObject behaviorString = base.GetBehaviorString();
			TextObject variable = GameTexts.FindText("str_formation_ai_side_strings", base.Formation.AI.Side.ToString());
			behaviorString.SetTextVariable("SIDE_STRING", variable);
			behaviorString.SetTextVariable("IS_GENERAL_SIDE", "0");
			return behaviorString;
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x00026620 File Offset: 0x00024820
		private void RecreateFollowEntityOrder()
		{
			this._followEntityOrder = MovementOrder.MovementOrderStop;
			SiegeWeapon siegeWeapon = this._primarySiegeWeapons.FirstOrDefault(delegate(SiegeWeapon psw)
			{
				IPrimarySiegeWeapon primarySiegeWeapon;
				return !psw.IsDeactivated && (primarySiegeWeapon = (psw as IPrimarySiegeWeapon)) != null && !primarySiegeWeapon.HasCompletedAction();
			});
			this._followedEntity = ((siegeWeapon != null) ? siegeWeapon.WaitEntity : null);
			if (this._followedEntity != null)
			{
				this._followEntityOrder = MovementOrder.MovementOrderFollowEntity(this._followedEntity);
			}
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x00026694 File Offset: 0x00024894
		public override void OnValidBehaviorSideChanged()
		{
			base.OnValidBehaviorSideChanged();
			this._primarySiegeWeapons.Clear();
			foreach (MissionObject missionObject in Mission.Current.ActiveMissionObjects)
			{
				IPrimarySiegeWeapon primarySiegeWeapon;
				if ((primarySiegeWeapon = (missionObject as IPrimarySiegeWeapon)) != null && primarySiegeWeapon.WeaponSide == this._behaviorSide && !((SiegeWeapon)missionObject).IsDeactivated)
				{
					this._primarySiegeWeapons.Add(missionObject as SiegeWeapon);
				}
			}
			this.RecreateFollowEntityOrder();
			this._behaviorState = BehaviorUseSiegeMachines.BehaviorState.Unset;
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x00026738 File Offset: 0x00024938
		public override void TickOccasionally()
		{
			base.TickOccasionally();
			bool flag = false;
			for (int i = this._primarySiegeWeapons.Count - 1; i >= 0; i--)
			{
				SiegeWeapon siegeWeapon = this._primarySiegeWeapons[i];
				if (siegeWeapon.IsDestroyed || siegeWeapon.IsDeactivated)
				{
					this._primarySiegeWeapons.RemoveAt(i);
					flag = true;
				}
			}
			if (flag)
			{
				this.RecreateFollowEntityOrder();
			}
			int num = 0;
			SiegeTower siegeTower = null;
			foreach (SiegeWeapon siegeWeapon2 in this._primarySiegeWeapons)
			{
				if (!((IPrimarySiegeWeapon)siegeWeapon2).HasCompletedAction())
				{
					num++;
					SiegeTower siegeTower2;
					if ((siegeTower2 = (siegeWeapon2 as SiegeTower)) != null)
					{
						siegeTower = siegeTower2;
					}
				}
			}
			if (num == 0)
			{
				base.CurrentOrder = this._stopOrder;
				return;
			}
			if (this._behaviorState == BehaviorUseSiegeMachines.BehaviorState.Follow)
			{
				if (this._followEntityOrder.OrderEnum == MovementOrder.MovementOrderEnum.Stop)
				{
					this.RecreateFollowEntityOrder();
				}
				base.CurrentOrder = this._followEntityOrder;
			}
			BehaviorUseSiegeMachines.BehaviorState behaviorState = (siegeTower != null && siegeTower.HasArrivedAtTarget) ? BehaviorUseSiegeMachines.BehaviorState.ClimbSiegeTower : ((this._followEntityOrder.OrderEnum != MovementOrder.MovementOrderEnum.Invalid) ? BehaviorUseSiegeMachines.BehaviorState.Follow : BehaviorUseSiegeMachines.BehaviorState.Stop);
			if (behaviorState != this._behaviorState)
			{
				if (behaviorState == BehaviorUseSiegeMachines.BehaviorState.Follow)
				{
					base.CurrentOrder = this._followEntityOrder;
				}
				else if (behaviorState == BehaviorUseSiegeMachines.BehaviorState.ClimbSiegeTower)
				{
					this.RecreateFollowEntityOrder();
					base.CurrentOrder = this._followEntityOrder;
				}
				else
				{
					base.CurrentOrder = this._stopOrder;
				}
				this._behaviorState = behaviorState;
				bool flag2 = this._behaviorState == BehaviorUseSiegeMachines.BehaviorState.ClimbSiegeTower;
				if (!flag2)
				{
					using (List<SiegeWeapon>.Enumerator enumerator = this._primarySiegeWeapons.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							SiegeLadder siegeLadder;
							if ((siegeLadder = (enumerator.Current as SiegeLadder)) != null && !siegeLadder.IsDisabled)
							{
								flag2 = true;
								break;
							}
						}
					}
				}
				if (flag2)
				{
					base.Formation.ArrangementOrder = ArrangementOrder.ArrangementOrderLine;
				}
				else if (base.Formation.QuerySystem.IsRangedFormation)
				{
					base.Formation.ArrangementOrder = ArrangementOrder.ArrangementOrderScatter;
				}
				else
				{
					base.Formation.ArrangementOrder = ArrangementOrder.ArrangementOrderShieldWall;
				}
			}
			if (this._followedEntity != null && (this._behaviorState == BehaviorUseSiegeMachines.BehaviorState.Follow || this._behaviorState == BehaviorUseSiegeMachines.BehaviorState.ClimbSiegeTower))
			{
				base.Formation.FacingOrder = FacingOrder.FacingOrderLookAtDirection(this._followedEntity.GetGlobalFrame().rotation.f.AsVec2.Normalized());
			}
			else
			{
				base.Formation.FacingOrder = FacingOrder.FacingOrderLookAtEnemy;
			}
			if (base.Formation.AI.ActiveBehavior == this)
			{
				foreach (SiegeWeapon siegeWeapon3 in this._primarySiegeWeapons)
				{
					if (!((IPrimarySiegeWeapon)siegeWeapon3).HasCompletedAction())
					{
						if (!siegeWeapon3.IsUsedByFormation(base.Formation))
						{
							base.Formation.StartUsingMachine(siegeWeapon3, false);
						}
						for (int j = siegeWeapon3.UserFormations.Count - 1; j >= 0; j--)
						{
							Formation formation = siegeWeapon3.UserFormations[j];
							if (formation != base.Formation && formation.IsAIControlled && (formation.AI.Side != this._behaviorSide || !(formation.AI.ActiveBehavior is BehaviorUseSiegeMachines)) && formation.Team == base.Formation.Team)
							{
								formation.StopUsingMachine(siegeWeapon3, false);
							}
						}
					}
				}
			}
			base.Formation.SetMovementOrder(base.CurrentOrder);
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x00026AE0 File Offset: 0x00024CE0
		protected override void OnBehaviorActivatedAux()
		{
			base.Formation.ArrangementOrder = (base.Formation.QuerySystem.IsRangedFormation ? ArrangementOrder.ArrangementOrderScatter : ArrangementOrder.ArrangementOrderShieldWall);
			base.Formation.FacingOrder = FacingOrder.FacingOrderLookAtEnemy;
			base.Formation.FiringOrder = FiringOrder.FiringOrderFireAtWill;
			base.Formation.FormOrder = FormOrder.FormOrderDeep;
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000E5A RID: 3674 RVA: 0x00026B46 File Offset: 0x00024D46
		public override float NavmeshlessTargetPositionPenalty
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x00026B50 File Offset: 0x00024D50
		protected override float GetAiWeight()
		{
			float result = 0f;
			if (this._teamAISiegeComponent != null && this._primarySiegeWeapons.Count > 0)
			{
				if (this._primarySiegeWeapons.All((SiegeWeapon psw) => !(psw as IPrimarySiegeWeapon).HasCompletedAction()))
				{
					result = ((!this._teamAISiegeComponent.IsCastleBreached()) ? 0.75f : 0.25f);
				}
			}
			return result;
		}

		// Token: 0x04000381 RID: 897
		private List<SiegeWeapon> _primarySiegeWeapons;

		// Token: 0x04000382 RID: 898
		private TeamAISiegeComponent _teamAISiegeComponent;

		// Token: 0x04000383 RID: 899
		private MovementOrder _followEntityOrder;

		// Token: 0x04000384 RID: 900
		private GameEntity _followedEntity;

		// Token: 0x04000385 RID: 901
		private MovementOrder _stopOrder;

		// Token: 0x04000386 RID: 902
		private BehaviorUseSiegeMachines.BehaviorState _behaviorState;

		// Token: 0x0200041C RID: 1052
		private enum BehaviorState
		{
			// Token: 0x04001810 RID: 6160
			Unset,
			// Token: 0x04001811 RID: 6161
			Follow,
			// Token: 0x04001812 RID: 6162
			ClimbSiegeTower,
			// Token: 0x04001813 RID: 6163
			Stop
		}
	}
}
