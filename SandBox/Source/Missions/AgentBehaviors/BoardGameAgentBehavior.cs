using System;
using SandBox.Missions.AgentBehaviors;
using SandBox.Objects.Usables;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace SandBox.Source.Missions.AgentBehaviors
{
	// Token: 0x02000046 RID: 70
	public class BoardGameAgentBehavior : AgentBehavior
	{
		// Token: 0x0600029A RID: 666 RVA: 0x00010FFC File Offset: 0x0000F1FC
		public BoardGameAgentBehavior(AgentBehaviorGroup behaviorGroup) : base(behaviorGroup)
		{
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00011008 File Offset: 0x0000F208
		public override void Tick(float dt, bool isSimulation)
		{
			switch (this._state)
			{
			case BoardGameAgentBehavior.State.Idle:
				if (base.Navigator.TargetUsableMachine != this._chair && !this._chair.IsAgentFullySitting(base.OwnerAgent))
				{
					base.Navigator.SetTarget(this._chair, false);
					this._state = BoardGameAgentBehavior.State.MovingToChair;
					return;
				}
				break;
			case BoardGameAgentBehavior.State.MovingToChair:
				if (this._chair.IsAgentFullySitting(base.OwnerAgent))
				{
					this._state = BoardGameAgentBehavior.State.Idle;
					return;
				}
				break;
			case BoardGameAgentBehavior.State.Finish:
				if (base.OwnerAgent.IsUsingGameObject && this._waitTimer == null)
				{
					base.Navigator.ClearTarget();
					this._waitTimer = new Timer(base.Mission.CurrentTime, 3f, true);
					return;
				}
				if (this._waitTimer != null)
				{
					if (this._waitTimer.Check(base.Mission.CurrentTime))
					{
						this.RemoveBoardGameBehaviorInternal();
						return;
					}
				}
				else
				{
					this.RemoveBoardGameBehaviorInternal();
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x0600029C RID: 668 RVA: 0x000110FA File Offset: 0x0000F2FA
		protected override void OnDeactivate()
		{
			base.Navigator.ClearTarget();
			this._chair = null;
			this._state = BoardGameAgentBehavior.State.Idle;
			this._waitTimer = null;
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0001111C File Offset: 0x0000F31C
		public override string GetDebugInfo()
		{
			return "BoardGameAgentBehavior";
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00011123 File Offset: 0x0000F323
		public override float GetAvailability(bool isSimulation)
		{
			return 1f;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0001112C File Offset: 0x0000F32C
		private void RemoveBoardGameBehaviorInternal()
		{
			InterruptingBehaviorGroup behaviorGroup = base.OwnerAgent.GetComponent<CampaignAgentComponent>().AgentNavigator.GetBehaviorGroup<InterruptingBehaviorGroup>();
			if (behaviorGroup.GetBehavior<BoardGameAgentBehavior>() != null)
			{
				behaviorGroup.RemoveBehavior<BoardGameAgentBehavior>();
			}
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00011160 File Offset: 0x0000F360
		public static void AddTargetChair(Agent ownerAgent, Chair chair)
		{
			InterruptingBehaviorGroup behaviorGroup = ownerAgent.GetComponent<CampaignAgentComponent>().AgentNavigator.GetBehaviorGroup<InterruptingBehaviorGroup>();
			bool flag = behaviorGroup.GetBehavior<BoardGameAgentBehavior>() == null;
			BoardGameAgentBehavior boardGameAgentBehavior = behaviorGroup.GetBehavior<BoardGameAgentBehavior>() ?? behaviorGroup.AddBehavior<BoardGameAgentBehavior>();
			boardGameAgentBehavior._chair = chair;
			boardGameAgentBehavior._state = BoardGameAgentBehavior.State.Idle;
			boardGameAgentBehavior._waitTimer = null;
			if (flag)
			{
				behaviorGroup.SetScriptedBehavior<BoardGameAgentBehavior>();
			}
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x000111B4 File Offset: 0x0000F3B4
		public static void RemoveBoardGameBehaviorOfAgent(Agent ownerAgent)
		{
			BoardGameAgentBehavior behavior = ownerAgent.GetComponent<CampaignAgentComponent>().AgentNavigator.GetBehaviorGroup<InterruptingBehaviorGroup>().GetBehavior<BoardGameAgentBehavior>();
			if (behavior != null)
			{
				behavior._chair = null;
				behavior._state = BoardGameAgentBehavior.State.Finish;
			}
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x000111E8 File Offset: 0x0000F3E8
		public static bool IsAgentMovingToChair(Agent ownerAgent)
		{
			if (ownerAgent == null)
			{
				return false;
			}
			InterruptingBehaviorGroup behaviorGroup = ownerAgent.GetComponent<CampaignAgentComponent>().AgentNavigator.GetBehaviorGroup<InterruptingBehaviorGroup>();
			BoardGameAgentBehavior boardGameAgentBehavior = (behaviorGroup != null) ? behaviorGroup.GetBehavior<BoardGameAgentBehavior>() : null;
			return boardGameAgentBehavior != null && boardGameAgentBehavior._state == BoardGameAgentBehavior.State.MovingToChair;
		}

		// Token: 0x0400013B RID: 315
		private const int FinishDelayAsSeconds = 3;

		// Token: 0x0400013C RID: 316
		private Chair _chair;

		// Token: 0x0400013D RID: 317
		private BoardGameAgentBehavior.State _state;

		// Token: 0x0400013E RID: 318
		private Timer _waitTimer;

		// Token: 0x02000123 RID: 291
		private enum State
		{
			// Token: 0x04000532 RID: 1330
			Idle,
			// Token: 0x04000533 RID: 1331
			MovingToChair,
			// Token: 0x04000534 RID: 1332
			Finish
		}
	}
}
