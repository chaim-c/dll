using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000136 RID: 310
	public class BehaviorWaitForLadders : BehaviorComponent
	{
		// Token: 0x06000E63 RID: 3683 RVA: 0x00027048 File Offset: 0x00025248
		public BehaviorWaitForLadders(Formation formation) : base(formation)
		{
			this._behaviorSide = formation.AI.Side;
			this._ladders = Mission.Current.ActiveMissionObjects.OfType<SiegeLadder>().ToList<SiegeLadder>();
			this._ladders.RemoveAll((SiegeLadder l) => l.IsDeactivated || l.WeaponSide != this._behaviorSide);
			this._teamAISiegeComponent = (TeamAISiegeComponent)formation.Team.TeamAI;
			SiegeLane siegeLane = TeamAISiegeComponent.SiegeLanes.FirstOrDefault((SiegeLane sl) => sl.LaneSide == this._behaviorSide);
			object obj;
			if (siegeLane == null)
			{
				obj = null;
			}
			else
			{
				obj = siegeLane.DefensePoints.FirstOrDefault((ICastleKeyPosition dp) => dp is WallSegment && (dp as WallSegment).IsBreachedWall);
			}
			this._breachedWallSegment = (obj as WallSegment);
			this.ResetFollowOrder();
			this._stopOrder = MovementOrder.MovementOrderStop;
			if (this._followOrder.OrderEnum != MovementOrder.MovementOrderEnum.Invalid)
			{
				base.CurrentOrder = this._followOrder;
				this._behaviorState = BehaviorWaitForLadders.BehaviorState.Follow;
				return;
			}
			base.CurrentOrder = this._stopOrder;
			this._behaviorState = BehaviorWaitForLadders.BehaviorState.Stop;
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x0002714C File Offset: 0x0002534C
		private void ResetFollowOrder()
		{
			this._followedEntity = null;
			this._followTacticalPosition = null;
			if (this._ladders.Count > 0)
			{
				SiegeLadder siegeLadder;
				if ((siegeLadder = this._ladders.FirstOrDefault((SiegeLadder l) => !l.IsDeactivated && l.InitialWaitPosition.HasScriptOfType<TacticalPosition>())) == null)
				{
					siegeLadder = this._ladders.FirstOrDefault((SiegeLadder l) => !l.IsDeactivated);
				}
				this._followedEntity = siegeLadder.InitialWaitPosition;
				if (this._followedEntity == null)
				{
					this._followedEntity = this._ladders.FirstOrDefault((SiegeLadder l) => !l.IsDeactivated).InitialWaitPosition;
				}
				this._followOrder = MovementOrder.MovementOrderFollowEntity(this._followedEntity);
			}
			else if (this._breachedWallSegment != null)
			{
				this._followedEntity = this._breachedWallSegment.GameEntity.CollectChildrenEntitiesWithTag("attacker_wait_pos").FirstOrDefault<GameEntity>();
				this._followOrder = MovementOrder.MovementOrderFollowEntity(this._followedEntity);
			}
			else
			{
				this._followOrder = MovementOrder.MovementOrderNull;
			}
			if (this._followedEntity != null)
			{
				this._followTacticalPosition = this._followedEntity.GetFirstScriptOfType<TacticalPosition>();
			}
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x00027298 File Offset: 0x00025498
		public override void OnValidBehaviorSideChanged()
		{
			base.OnValidBehaviorSideChanged();
			this._ladders = Mission.Current.ActiveMissionObjects.OfType<SiegeLadder>().ToList<SiegeLadder>();
			this._ladders.RemoveAll((SiegeLadder l) => l.IsDeactivated || l.WeaponSide != this._behaviorSide);
			SiegeLane siegeLane = TeamAISiegeComponent.SiegeLanes.FirstOrDefault((SiegeLane sl) => sl.LaneSide == this._behaviorSide);
			object obj;
			if (siegeLane == null)
			{
				obj = null;
			}
			else
			{
				obj = siegeLane.DefensePoints.FirstOrDefault((ICastleKeyPosition dp) => dp is WallSegment && (dp as WallSegment).IsBreachedWall);
			}
			this._breachedWallSegment = (obj as WallSegment);
			this.ResetFollowOrder();
			this._behaviorState = BehaviorWaitForLadders.BehaviorState.Unset;
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x0002733C File Offset: 0x0002553C
		protected override void CalculateCurrentOrder()
		{
			BehaviorWaitForLadders.BehaviorState behaviorState = (this._followOrder.OrderEnum != MovementOrder.MovementOrderEnum.Invalid) ? BehaviorWaitForLadders.BehaviorState.Follow : BehaviorWaitForLadders.BehaviorState.Stop;
			if (behaviorState != this._behaviorState)
			{
				if (behaviorState == BehaviorWaitForLadders.BehaviorState.Follow)
				{
					base.CurrentOrder = this._followOrder;
					if (this._followTacticalPosition != null)
					{
						this.CurrentFacingOrder = FacingOrder.FacingOrderLookAtDirection(this._followTacticalPosition.Direction);
					}
					else
					{
						this.CurrentFacingOrder = FacingOrder.FacingOrderLookAtEnemy;
					}
				}
				else
				{
					base.CurrentOrder = this._stopOrder;
					this.CurrentFacingOrder = FacingOrder.FacingOrderLookAtEnemy;
				}
				this._behaviorState = behaviorState;
			}
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x000273C0 File Offset: 0x000255C0
		public override void TickOccasionally()
		{
			base.TickOccasionally();
			if (this._ladders.RemoveAll((SiegeLadder l) => l.IsDeactivated) > 0)
			{
				this.ResetFollowOrder();
				this.CalculateCurrentOrder();
			}
			base.Formation.SetMovementOrder(base.CurrentOrder);
			base.Formation.FacingOrder = this.CurrentFacingOrder;
			if (this._behaviorState == BehaviorWaitForLadders.BehaviorState.Follow && this._followTacticalPosition != null)
			{
				base.Formation.FormOrder = FormOrder.FormOrderCustom(this._followTacticalPosition.Width);
			}
			foreach (SiegeLadder siegeLadder in this._ladders)
			{
				if (siegeLadder.IsUsedByFormation(base.Formation))
				{
					base.Formation.StopUsingMachine(siegeLadder, false);
				}
			}
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x000274B4 File Offset: 0x000256B4
		protected override void OnBehaviorActivatedAux()
		{
			base.Formation.ArrangementOrder = (base.Formation.QuerySystem.HasShield ? ArrangementOrder.ArrangementOrderShieldWall : ArrangementOrder.ArrangementOrderLine);
			base.Formation.FacingOrder = FacingOrder.FacingOrderLookAtEnemy;
			base.Formation.FiringOrder = FiringOrder.FiringOrderFireAtWill;
			base.Formation.FormOrder = FormOrder.FormOrderWide;
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000E69 RID: 3689 RVA: 0x0002751A File Offset: 0x0002571A
		public override float NavmeshlessTargetPositionPenalty
		{
			get
			{
				return 1f;
			}
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x00027524 File Offset: 0x00025724
		protected override float GetAiWeight()
		{
			float result = 0f;
			if (this._followOrder.OrderEnum != MovementOrder.MovementOrderEnum.Invalid && !this._teamAISiegeComponent.AreLaddersReady)
			{
				result = ((!this._teamAISiegeComponent.IsCastleBreached()) ? 1f : 0.5f);
			}
			return result;
		}

		// Token: 0x04000388 RID: 904
		private const string WallWaitPositionTag = "attacker_wait_pos";

		// Token: 0x04000389 RID: 905
		private List<SiegeLadder> _ladders;

		// Token: 0x0400038A RID: 906
		private WallSegment _breachedWallSegment;

		// Token: 0x0400038B RID: 907
		private TeamAISiegeComponent _teamAISiegeComponent;

		// Token: 0x0400038C RID: 908
		private MovementOrder _stopOrder;

		// Token: 0x0400038D RID: 909
		private MovementOrder _followOrder;

		// Token: 0x0400038E RID: 910
		private BehaviorWaitForLadders.BehaviorState _behaviorState;

		// Token: 0x0400038F RID: 911
		private GameEntity _followedEntity;

		// Token: 0x04000390 RID: 912
		private TacticalPosition _followTacticalPosition;

		// Token: 0x0200041F RID: 1055
		private enum BehaviorState
		{
			// Token: 0x0400181D RID: 6173
			Unset,
			// Token: 0x0400181E RID: 6174
			Stop,
			// Token: 0x0400181F RID: 6175
			Follow
		}
	}
}
