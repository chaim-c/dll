using System;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade.MissionRepresentatives;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002A4 RID: 676
	public class MissionMultiplayerFFA : MissionMultiplayerGameModeBase
	{
		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06002478 RID: 9336 RVA: 0x00087D2A File Offset: 0x00085F2A
		public override bool IsGameModeHidingAllAgentVisuals
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06002479 RID: 9337 RVA: 0x00087D2D File Offset: 0x00085F2D
		public override bool IsGameModeUsingOpposingTeams
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600247A RID: 9338 RVA: 0x00087D30 File Offset: 0x00085F30
		public override MultiplayerGameType GetMissionType()
		{
			return MultiplayerGameType.FreeForAll;
		}

		// Token: 0x0600247B RID: 9339 RVA: 0x00087D34 File Offset: 0x00085F34
		public override void AfterStart()
		{
			BasicCultureObject @object = MBObjectManager.Instance.GetObject<BasicCultureObject>(MultiplayerOptions.OptionType.CultureTeam1.GetStrValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions));
			Banner banner = new Banner(@object.BannerKey, @object.BackgroundColor1, @object.ForegroundColor1);
			Team team = base.Mission.Teams.Add(BattleSideEnum.Attacker, @object.BackgroundColor1, @object.ForegroundColor1, banner, false, false, true);
			team.SetIsEnemyOf(team, true);
		}

		// Token: 0x0600247C RID: 9340 RVA: 0x00087D94 File Offset: 0x00085F94
		protected override void HandleEarlyNewClientAfterLoadingFinished(NetworkCommunicator networkPeer)
		{
			networkPeer.AddComponent<FFAMissionRepresentative>();
		}

		// Token: 0x0600247D RID: 9341 RVA: 0x00087D9D File Offset: 0x00085F9D
		protected override void HandleNewClientAfterSynchronized(NetworkCommunicator networkPeer)
		{
			MissionPeer component = networkPeer.GetComponent<MissionPeer>();
			component.Team = base.Mission.AttackerTeam;
			component.Culture = MBObjectManager.Instance.GetObject<BasicCultureObject>(MultiplayerOptions.OptionType.CultureTeam1.GetStrValue(MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions));
		}
	}
}
