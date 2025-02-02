using System;
using TaleWorlds.CampaignSystem.Encounters;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace SandBox.Missions.MissionLogics
{
	// Token: 0x0200004B RID: 75
	public class CampaignSiegeStateHandler : MissionLogic
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x000126FB File Offset: 0x000108FB
		public bool IsSiege
		{
			get
			{
				return this._mapEvent.IsSiegeAssault;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x00012708 File Offset: 0x00010908
		public bool IsSallyOut
		{
			get
			{
				return this._mapEvent.IsSallyOut;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x00012715 File Offset: 0x00010915
		public Settlement Settlement
		{
			get
			{
				return this._mapEvent.MapEventSettlement;
			}
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x00012722 File Offset: 0x00010922
		public CampaignSiegeStateHandler()
		{
			this._mapEvent = PlayerEncounter.Battle;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00012735 File Offset: 0x00010935
		public override void OnRetreatMission()
		{
			this._isRetreat = true;
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0001273E File Offset: 0x0001093E
		public override void OnMissionResultReady(MissionResult missionResult)
		{
			this._defenderVictory = (missionResult.BattleState == BattleState.DefenderVictory);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0001274F File Offset: 0x0001094F
		public override void OnSurrenderMission()
		{
			PlayerEncounter.PlayerSurrender = true;
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00012757 File Offset: 0x00010957
		protected override void OnEndMission()
		{
			if (this.IsSiege && this._mapEvent.PlayerSide == BattleSideEnum.Attacker && !this._isRetreat && !this._defenderVictory)
			{
				this.Settlement.SetNextSiegeState();
			}
		}

		// Token: 0x04000150 RID: 336
		private readonly MapEvent _mapEvent;

		// Token: 0x04000151 RID: 337
		private bool _isRetreat;

		// Token: 0x04000152 RID: 338
		private bool _defenderVictory;
	}
}
