using System;
using SandBox.Missions.MissionLogics;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.AgentBehaviors
{
	// Token: 0x0200007E RID: 126
	public class FightBehavior : AgentBehavior
	{
		// Token: 0x060004EC RID: 1260 RVA: 0x0002101B File Offset: 0x0001F21B
		public FightBehavior(AgentBehaviorGroup behaviorGroup) : base(behaviorGroup)
		{
			this._missionAgentHandler = base.Mission.GetMissionBehavior<MissionAgentHandler>();
			if (base.OwnerAgent.HumanAIComponent == null)
			{
				base.OwnerAgent.AddComponent(new HumanAIComponent(base.OwnerAgent));
			}
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00021058 File Offset: 0x0001F258
		public override float GetAvailability(bool isSimulation)
		{
			if (!MissionFightHandler.IsAgentAggressive(base.OwnerAgent))
			{
				return 0.1f;
			}
			return 1f;
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00021074 File Offset: 0x0001F274
		protected override void OnActivate()
		{
			TextObject textObject = new TextObject("{=!}{p0} {p1} activate alarmed behavior group.", null);
			textObject.SetTextVariable("p0", base.OwnerAgent.Name.ToString());
			textObject.SetTextVariable("p1", base.OwnerAgent.Index.ToString());
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x000210C8 File Offset: 0x0001F2C8
		protected override void OnDeactivate()
		{
			TextObject textObject = new TextObject("{=!}{p0} {p1} deactivate fight behavior.", null);
			textObject.SetTextVariable("p0", base.OwnerAgent.Name.ToString());
			textObject.SetTextVariable("p1", base.OwnerAgent.Index.ToString());
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0002111A File Offset: 0x0001F31A
		public override string GetDebugInfo()
		{
			return "Fight";
		}

		// Token: 0x04000245 RID: 581
		private readonly MissionAgentHandler _missionAgentHandler;
	}
}
