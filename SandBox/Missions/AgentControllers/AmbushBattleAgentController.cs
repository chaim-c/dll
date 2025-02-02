using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.AgentControllers
{
	// Token: 0x02000074 RID: 116
	public class AmbushBattleAgentController : AgentController
	{
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x0001E619 File Offset: 0x0001C819
		// (set) Token: 0x0600046C RID: 1132 RVA: 0x0001E5FC File Offset: 0x0001C7FC
		public bool Aggressive
		{
			get
			{
				return this._aggressive;
			}
			set
			{
				this._aggressive = value;
				if (this._aggressive)
				{
					base.Owner.SetWatchState(Agent.WatchState.Alarmed);
				}
			}
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0001E621 File Offset: 0x0001C821
		public override void OnInitialize()
		{
			this.Aggressive = false;
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0001E62A File Offset: 0x0001C82A
		public bool CheckArrivedAtWayPoint(GameEntity waypoint)
		{
			return waypoint.CheckPointWithOrientedBoundingBox(base.Owner.Position);
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0001E63D File Offset: 0x0001C83D
		public void UpdateState()
		{
			if (!this.IsAttacker)
			{
				this.UpdateDefendingAIAgentState();
				return;
			}
			this.UpdateAttackingAIAgentState();
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0001E654 File Offset: 0x0001C854
		private void UpdateDefendingAIAgentState()
		{
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0001E658 File Offset: 0x0001C858
		private void UpdateAttackingAIAgentState()
		{
			if (this._agentState == AmbushBattleAgentController.AgentState.MovingToBoulder || this._agentState == AmbushBattleAgentController.AgentState.SearchingForBoulder)
			{
				if (base.Owner.Character != Game.Current.PlayerTroop && !base.Owner.Character.IsPlayerCharacter && this._agentState != AmbushBattleAgentController.AgentState.SearchingForBoulder)
				{
					Vec3 origin = base.Owner.AgentVisuals.GetGlobalFrame().origin;
					Vec3 globalPosition = this.BoulderTarget.GlobalPosition;
					if (origin.DistanceSquared(globalPosition) < 0.16000001f)
					{
						MBDebug.Print("Picking up a boulder", 0, Debug.DebugColor.White, 17592186044416UL);
						this._agentState = AmbushBattleAgentController.AgentState.PickingUpBoulder;
						base.Owner.DisableScriptedMovement();
						MatrixFrame globalFrame = this.BoulderTarget.GetGlobalFrame();
						Vec2 asVec = globalFrame.origin.AsVec2;
						base.Owner.SetTargetPositionAndDirectionSynched(ref asVec, ref globalFrame.rotation.f);
					}
				}
			}
			else if (this._agentState == AmbushBattleAgentController.AgentState.PickingUpBoulder)
			{
				this.PickUpBoulderWithAnimation();
			}
			if (this._agentState == AmbushBattleAgentController.AgentState.MovingBackToSpawn)
			{
				base.Owner.DisableScriptedMovement();
				this._agentState = AmbushBattleAgentController.AgentState.None;
			}
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0001E76C File Offset: 0x0001C96C
		private void PickUpBoulderWithAnimation()
		{
			ActionIndexValueCache currentActionValue = base.Owner.GetCurrentActionValue(0);
			if (!this._boulderAddedToEquip && currentActionValue != this.act_pickup_boulder_begin)
			{
				base.Owner.SetActionChannel(0, this.act_pickup_boulder_begin, true, 0UL, 0f, 1f, -0.2f, 0.4f, 0f, false, -0.2f, 0, true);
				return;
			}
			if (!this._boulderAddedToEquip && currentActionValue == this.act_pickup_boulder_begin)
			{
				if (base.Owner.GetCurrentActionProgress(0) >= 0.7f)
				{
					this._boulderAddedToEquip = true;
					return;
				}
			}
			else if (!base.Owner.IsMainAgent && this._agentState == AmbushBattleAgentController.AgentState.PickingUpBoulder && currentActionValue != this.act_pickup_boulder_end && currentActionValue != this.act_pickup_boulder_begin)
			{
				base.Owner.ClearTargetFrame();
				if (!this.Aggressive)
				{
					WorldPosition worldPosition = new WorldPosition(Mission.Current.Scene, UIntPtr.Zero, this.OriginalSpawnFrame.origin, false);
					base.Owner.SetScriptedPosition(ref worldPosition, false, Agent.AIScriptedFrameFlags.None);
					this._agentState = AmbushBattleAgentController.AgentState.MovingBackToSpawn;
					return;
				}
				this._agentState = AmbushBattleAgentController.AgentState.None;
			}
		}

		// Token: 0x0400020A RID: 522
		private readonly ActionIndexCache act_pickup_boulder_begin = ActionIndexCache.Create("act_pickup_boulder_begin");

		// Token: 0x0400020B RID: 523
		private readonly ActionIndexCache act_pickup_boulder_end = ActionIndexCache.Create("act_pickup_boulder_end");

		// Token: 0x0400020C RID: 524
		public bool IsAttacker;

		// Token: 0x0400020D RID: 525
		private bool _aggressive;

		// Token: 0x0400020E RID: 526
		public bool IsLeader;

		// Token: 0x0400020F RID: 527
		public GameEntity BoulderTarget;

		// Token: 0x04000210 RID: 528
		public bool HasBeenPlaced;

		// Token: 0x04000211 RID: 529
		public MatrixFrame OriginalSpawnFrame;

		// Token: 0x04000212 RID: 530
		private bool _boulderAddedToEquip;

		// Token: 0x04000213 RID: 531
		private AmbushBattleAgentController.AgentState _agentState = AmbushBattleAgentController.AgentState.SearchingForBoulder;

		// Token: 0x02000148 RID: 328
		private enum AgentState
		{
			// Token: 0x040005A3 RID: 1443
			None,
			// Token: 0x040005A4 RID: 1444
			SearchingForBoulder,
			// Token: 0x040005A5 RID: 1445
			MovingToBoulder,
			// Token: 0x040005A6 RID: 1446
			PickingUpBoulder,
			// Token: 0x040005A7 RID: 1447
			MovingBackToSpawn
		}
	}
}
