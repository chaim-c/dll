using System;
using SandBox.Missions.MissionLogics;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.AgentBehaviors
{
	// Token: 0x02000083 RID: 131
	public class PatrollingGuardBehavior : AgentBehavior
	{
		// Token: 0x0600051F RID: 1311 RVA: 0x00022E4C File Offset: 0x0002104C
		public PatrollingGuardBehavior(AgentBehaviorGroup behaviorGroup) : base(behaviorGroup)
		{
			this._missionAgentHandler = base.Mission.GetMissionBehavior<MissionAgentHandler>();
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00022E68 File Offset: 0x00021068
		public override void Tick(float dt, bool isSimulation)
		{
			if (this._target == null)
			{
				UsableMachine usableMachine = (base.Navigator.SpecialTargetTag == null || base.Navigator.SpecialTargetTag.IsEmpty<char>()) ? this._missionAgentHandler.FindUnusedPointWithTagForAgent(base.OwnerAgent, "npc_common") : this._missionAgentHandler.FindUnusedPointWithTagForAgent(base.OwnerAgent, base.Navigator.SpecialTargetTag);
				if (usableMachine != null)
				{
					this._target = usableMachine;
					base.Navigator.SetTarget(this._target, false);
					return;
				}
			}
			else if (base.Navigator.TargetUsableMachine == null)
			{
				base.Navigator.SetTarget(this._target, false);
			}
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00022F0D File Offset: 0x0002110D
		public override float GetAvailability(bool isSimulation)
		{
			if (this._missionAgentHandler.GetAllUsablePointsWithTag(base.Navigator.SpecialTargetTag).Count <= 0)
			{
				return 0f;
			}
			return 1f;
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00022F38 File Offset: 0x00021138
		protected override void OnDeactivate()
		{
			this._target = null;
			base.Navigator.ClearTarget();
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00022F4C File Offset: 0x0002114C
		public override string GetDebugInfo()
		{
			return "Guard patrol";
		}

		// Token: 0x04000273 RID: 627
		private readonly MissionAgentHandler _missionAgentHandler;

		// Token: 0x04000274 RID: 628
		private UsableMachine _target;
	}
}
