using System;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.MissionLogics
{
	// Token: 0x02000064 RID: 100
	public class SandBoxSallyOutMissionController : SallyOutMissionController
	{
		// Token: 0x060003DE RID: 990 RVA: 0x0001AD10 File Offset: 0x00018F10
		public override void OnBehaviorInitialize()
		{
			base.OnBehaviorInitialize();
			this._mapEvent = MapEvent.PlayerMapEvent;
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0001AD23 File Offset: 0x00018F23
		protected override void GetInitialTroopCounts(out int besiegedTotalTroopCount, out int besiegerTotalTroopCount)
		{
			besiegedTotalTroopCount = this._mapEvent.GetNumberOfInvolvedMen(BattleSideEnum.Defender);
			besiegerTotalTroopCount = this._mapEvent.GetNumberOfInvolvedMen(BattleSideEnum.Attacker);
		}

		// Token: 0x040001C3 RID: 451
		private MapEvent _mapEvent;
	}
}
