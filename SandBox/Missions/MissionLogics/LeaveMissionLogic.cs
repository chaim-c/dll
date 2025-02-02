using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.MissionLogics
{
	// Token: 0x02000053 RID: 83
	public class LeaveMissionLogic : MissionLogic
	{
		// Token: 0x0600034C RID: 844 RVA: 0x000151DC File Offset: 0x000133DC
		public override bool MissionEnded(ref MissionResult missionResult)
		{
			return base.Mission.MainAgent != null && !base.Mission.MainAgent.IsActive();
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00015200 File Offset: 0x00013400
		public override void OnMissionTick(float dt)
		{
			if (Agent.Main == null || !Agent.Main.IsActive())
			{
				if (this._isAgentDeadTimer == null)
				{
					this._isAgentDeadTimer = new Timer(Mission.Current.CurrentTime, 5f, true);
				}
				if (this._isAgentDeadTimer.Check(Mission.Current.CurrentTime))
				{
					Mission.Current.NextCheckTimeEndMission = 0f;
					Mission.Current.EndMission();
					Campaign.Current.GameMenuManager.SetNextMenu(this.UnconsciousGameMenuID);
					return;
				}
			}
			else if (this._isAgentDeadTimer != null)
			{
				this._isAgentDeadTimer = null;
			}
		}

		// Token: 0x04000198 RID: 408
		public string UnconsciousGameMenuID = "settlement_player_unconscious";

		// Token: 0x04000199 RID: 409
		private Timer _isAgentDeadTimer;
	}
}
