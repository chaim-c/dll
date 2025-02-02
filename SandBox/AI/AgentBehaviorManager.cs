using System;
using SandBox.Missions.AgentBehaviors;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;

namespace SandBox.AI
{
	// Token: 0x020000D8 RID: 216
	public class AgentBehaviorManager : IAgentBehaviorManager
	{
		// Token: 0x06000AD8 RID: 2776 RVA: 0x0004F44C File Offset: 0x0004D64C
		public void AddQuestCharacterBehaviors(IAgent agent)
		{
			BehaviorSets.AddQuestCharacterBehaviors(agent);
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x0004F454 File Offset: 0x0004D654
		void IAgentBehaviorManager.AddWandererBehaviors(IAgent agent)
		{
			BehaviorSets.AddWandererBehaviors(agent);
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x0004F45C File Offset: 0x0004D65C
		void IAgentBehaviorManager.AddOutdoorWandererBehaviors(IAgent agent)
		{
			BehaviorSets.AddOutdoorWandererBehaviors(agent);
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x0004F464 File Offset: 0x0004D664
		void IAgentBehaviorManager.AddIndoorWandererBehaviors(IAgent agent)
		{
			BehaviorSets.AddIndoorWandererBehaviors(agent);
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x0004F46C File Offset: 0x0004D66C
		void IAgentBehaviorManager.AddFixedCharacterBehaviors(IAgent agent)
		{
			BehaviorSets.AddFixedCharacterBehaviors(agent);
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x0004F474 File Offset: 0x0004D674
		void IAgentBehaviorManager.AddAmbushPlayerBehaviors(IAgent agent)
		{
			BehaviorSets.AddAmbushPlayerBehaviors(agent);
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x0004F47C File Offset: 0x0004D67C
		void IAgentBehaviorManager.AddStandGuardBehaviors(IAgent agent)
		{
			BehaviorSets.AddStandGuardBehaviors(agent);
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x0004F484 File Offset: 0x0004D684
		void IAgentBehaviorManager.AddPatrollingGuardBehaviors(IAgent agent)
		{
			BehaviorSets.AddPatrollingGuardBehaviors(agent);
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x0004F48C File Offset: 0x0004D68C
		void IAgentBehaviorManager.AddCompanionBehaviors(IAgent agent)
		{
			BehaviorSets.AddCompanionBehaviors(agent);
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x0004F494 File Offset: 0x0004D694
		void IAgentBehaviorManager.AddBodyguardBehaviors(IAgent agent)
		{
			BehaviorSets.AddBodyguardBehaviors(agent);
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x0004F49C File Offset: 0x0004D69C
		public void AddFirstCompanionBehavior(IAgent agent)
		{
			BehaviorSets.AddFirstCompanionBehavior(agent);
		}
	}
}
