using System;
using SandBox.Missions.MissionLogics;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.AgentBehaviors
{
	// Token: 0x02000085 RID: 133
	public class StandGuardBehavior : AgentBehavior
	{
		// Token: 0x06000531 RID: 1329 RVA: 0x000234A2 File Offset: 0x000216A2
		public StandGuardBehavior(AgentBehaviorGroup behaviorGroup) : base(behaviorGroup)
		{
			this._missionAgentHandler = base.Mission.GetMissionBehavior<MissionAgentHandler>();
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x000234BC File Offset: 0x000216BC
		public override void Tick(float dt, bool isSimulation)
		{
			if (base.OwnerAgent.CurrentWatchState == Agent.WatchState.Patrolling)
			{
				if (this._standPoint == null || isSimulation)
				{
					UsableMachine usableMachine = this._oldStandPoint ?? this._missionAgentHandler.FindUnusedPointWithTagForAgent(base.OwnerAgent, base.Navigator.SpecialTargetTag);
					if (usableMachine != null)
					{
						this._oldStandPoint = null;
						this._standPoint = usableMachine;
						base.Navigator.SetTarget(this._standPoint, false);
						return;
					}
				}
			}
			else if (this._standPoint != null)
			{
				this._oldStandPoint = this._standPoint;
				base.Navigator.SetTarget(null, false);
				this._standPoint = null;
			}
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00023556 File Offset: 0x00021756
		protected override void OnDeactivate()
		{
			base.Navigator.ClearTarget();
			this._standPoint = null;
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0002356A File Offset: 0x0002176A
		public override float GetAvailability(bool isSimulation)
		{
			return 1f;
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00023571 File Offset: 0x00021771
		public override string GetDebugInfo()
		{
			return "Guard stand";
		}

		// Token: 0x0400027C RID: 636
		private UsableMachine _oldStandPoint;

		// Token: 0x0400027D RID: 637
		private UsableMachine _standPoint;

		// Token: 0x0400027E RID: 638
		private readonly MissionAgentHandler _missionAgentHandler;

		// Token: 0x02000158 RID: 344
		private enum GuardState
		{
			// Token: 0x040005CE RID: 1486
			StandIdle,
			// Token: 0x040005CF RID: 1487
			StandAttention,
			// Token: 0x040005D0 RID: 1488
			StandCautious,
			// Token: 0x040005D1 RID: 1489
			GotToStandPoint
		}
	}
}
