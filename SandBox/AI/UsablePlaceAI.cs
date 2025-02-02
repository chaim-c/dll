using System;
using TaleWorlds.MountAndBlade;

namespace SandBox.AI
{
	// Token: 0x020000DA RID: 218
	public class UsablePlaceAI : UsableMachineAIBase
	{
		// Token: 0x06000AE8 RID: 2792 RVA: 0x0004F75C File Offset: 0x0004D95C
		public UsablePlaceAI(UsableMachine usableMachine) : base(usableMachine)
		{
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x0004F765 File Offset: 0x0004D965
		protected override Agent.AIScriptedFrameFlags GetScriptedFrameFlags(Agent agent)
		{
			if (!this.UsableMachine.GameEntity.HasTag("quest_wanderer_target"))
			{
				return Agent.AIScriptedFrameFlags.DoNotRun;
			}
			return Agent.AIScriptedFrameFlags.None;
		}
	}
}
