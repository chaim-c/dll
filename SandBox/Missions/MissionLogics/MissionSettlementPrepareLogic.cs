using System;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.MissionLogics
{
	// Token: 0x0200005C RID: 92
	public class MissionSettlementPrepareLogic : MissionLogic
	{
		// Token: 0x060003C9 RID: 969 RVA: 0x0001A618 File Offset: 0x00018818
		public override void AfterStart()
		{
			if (Campaign.Current.GameMode == CampaignGameMode.Campaign && Settlement.CurrentSettlement != null && (Settlement.CurrentSettlement.IsTown || Settlement.CurrentSettlement.IsCastle))
			{
				this.OpenGates();
			}
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0001A64C File Offset: 0x0001884C
		private void OpenGates()
		{
			foreach (CastleGate castleGate in Mission.Current.ActiveMissionObjects.FindAllWithType<CastleGate>().ToList<CastleGate>())
			{
				castleGate.OpenDoorAndDisableGateForCivilianMission();
			}
		}
	}
}
