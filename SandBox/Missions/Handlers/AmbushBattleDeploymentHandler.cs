using System;
using SandBox.Missions.AgentControllers;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.Handlers
{
	// Token: 0x02000073 RID: 115
	public class AmbushBattleDeploymentHandler : DeploymentHandler
	{
		// Token: 0x0600046A RID: 1130 RVA: 0x0001E5CD File Offset: 0x0001C7CD
		public AmbushBattleDeploymentHandler(bool isPlayerAttacker) : base(isPlayerAttacker)
		{
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0001E5D6 File Offset: 0x0001C7D6
		public override void FinishDeployment()
		{
			base.FinishDeployment();
			if (base.Mission.GetMissionBehavior<AmbushMissionController>() != null)
			{
				base.Mission.GetMissionBehavior<AmbushMissionController>().OnPlayerDeploymentFinish(false);
			}
		}
	}
}
