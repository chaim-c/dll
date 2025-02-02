using System;
using SandBox.Missions.AgentControllers;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.MissionLogics
{
	// Token: 0x02000047 RID: 71
	public class AmbushIntroLogic : MissionLogic
	{
		// Token: 0x060002A4 RID: 676 RVA: 0x0001122D File Offset: 0x0000F42D
		public override void OnCreated()
		{
			this._ambushMission = base.Mission.GetMissionBehavior<AmbushMissionController>();
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00011240 File Offset: 0x0000F440
		public void StartIntro()
		{
			Action startIntroAction = this.StartIntroAction;
			if (startIntroAction == null)
			{
				return;
			}
			startIntroAction();
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00011252 File Offset: 0x0000F452
		public void OnIntroEnded()
		{
			this._ambushMission.OnIntroductionFinish();
			base.Mission.RemoveMissionBehavior(this);
		}

		// Token: 0x0400013F RID: 319
		private AmbushMissionController _ambushMission;

		// Token: 0x04000140 RID: 320
		public Action StartIntroAction;
	}
}
