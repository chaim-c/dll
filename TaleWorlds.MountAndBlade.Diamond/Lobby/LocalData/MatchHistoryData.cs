using System;
using System.Collections.Generic;

namespace TaleWorlds.MountAndBlade.Diamond.Lobby.LocalData
{
	// Token: 0x0200017C RID: 380
	public class MatchHistoryData : MultiplayerLocalData
	{
		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000A87 RID: 2695 RVA: 0x00011697 File Offset: 0x0000F897
		// (set) Token: 0x06000A88 RID: 2696 RVA: 0x0001169F File Offset: 0x0000F89F
		public string MatchId { get; set; }

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000A89 RID: 2697 RVA: 0x000116A8 File Offset: 0x0000F8A8
		// (set) Token: 0x06000A8A RID: 2698 RVA: 0x000116B0 File Offset: 0x0000F8B0
		public string MatchType { get; set; }

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000A8B RID: 2699 RVA: 0x000116B9 File Offset: 0x0000F8B9
		// (set) Token: 0x06000A8C RID: 2700 RVA: 0x000116C1 File Offset: 0x0000F8C1
		public string GameType { get; set; }

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000A8D RID: 2701 RVA: 0x000116CA File Offset: 0x0000F8CA
		// (set) Token: 0x06000A8E RID: 2702 RVA: 0x000116D2 File Offset: 0x0000F8D2
		public string Map { get; set; }

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000A8F RID: 2703 RVA: 0x000116DB File Offset: 0x0000F8DB
		// (set) Token: 0x06000A90 RID: 2704 RVA: 0x000116E3 File Offset: 0x0000F8E3
		public DateTime MatchDate { get; set; }

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000A91 RID: 2705 RVA: 0x000116EC File Offset: 0x0000F8EC
		// (set) Token: 0x06000A92 RID: 2706 RVA: 0x000116F4 File Offset: 0x0000F8F4
		public int WinnerTeam { get; set; }

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000A93 RID: 2707 RVA: 0x000116FD File Offset: 0x0000F8FD
		// (set) Token: 0x06000A94 RID: 2708 RVA: 0x00011705 File Offset: 0x0000F905
		public string Faction1 { get; set; }

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000A95 RID: 2709 RVA: 0x0001170E File Offset: 0x0000F90E
		// (set) Token: 0x06000A96 RID: 2710 RVA: 0x00011716 File Offset: 0x0000F916
		public string Faction2 { get; set; }

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000A97 RID: 2711 RVA: 0x0001171F File Offset: 0x0000F91F
		// (set) Token: 0x06000A98 RID: 2712 RVA: 0x00011727 File Offset: 0x0000F927
		public int DefenderScore { get; set; }

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000A99 RID: 2713 RVA: 0x00011730 File Offset: 0x0000F930
		// (set) Token: 0x06000A9A RID: 2714 RVA: 0x00011738 File Offset: 0x0000F938
		public int AttackerScore { get; set; }

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000A9B RID: 2715 RVA: 0x00011741 File Offset: 0x0000F941
		// (set) Token: 0x06000A9C RID: 2716 RVA: 0x00011749 File Offset: 0x0000F949
		public List<PlayerInfo> Players { get; set; }

		// Token: 0x06000A9D RID: 2717 RVA: 0x00011752 File Offset: 0x0000F952
		public MatchHistoryData()
		{
			this.Players = new List<PlayerInfo>();
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x00011768 File Offset: 0x0000F968
		public override bool HasSameContentWith(MultiplayerLocalData other)
		{
			MatchHistoryData matchHistoryData;
			if ((matchHistoryData = (other as MatchHistoryData)) == null)
			{
				return false;
			}
			bool flag = this.MatchId == matchHistoryData.MatchId && this.MatchType == matchHistoryData.MatchType && this.GameType == matchHistoryData.GameType && this.Map == matchHistoryData.Map && this.MatchDate == matchHistoryData.MatchDate && this.WinnerTeam == matchHistoryData.WinnerTeam && this.Faction1 == matchHistoryData.Faction1 && this.Faction2 == matchHistoryData.Faction2 && this.DefenderScore == matchHistoryData.DefenderScore && this.AttackerScore == matchHistoryData.AttackerScore;
			if (!flag)
			{
				return false;
			}
			if (this.Players != null || matchHistoryData.Players != null)
			{
				List<PlayerInfo> players = this.Players;
				int? num = (players != null) ? new int?(players.Count) : null;
				List<PlayerInfo> players2 = matchHistoryData.Players;
				int? num2 = (players2 != null) ? new int?(players2.Count) : null;
				if (!(num.GetValueOrDefault() == num2.GetValueOrDefault() & num != null == (num2 != null)))
				{
					return flag;
				}
			}
			for (int i = 0; i < this.Players.Count; i++)
			{
				PlayerInfo playerInfo = this.Players[i];
				PlayerInfo other2 = matchHistoryData.Players[i];
				if (!playerInfo.HasSameContentWith(other2))
				{
					return false;
				}
			}
			return flag;
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x000118FC File Offset: 0x0000FAFC
		private PlayerInfo TryGetPlayer(string id)
		{
			foreach (PlayerInfo playerInfo in this.Players)
			{
				if (playerInfo.PlayerId == id)
				{
					return playerInfo;
				}
			}
			return null;
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x00011960 File Offset: 0x0000FB60
		public void AddOrUpdatePlayer(string id, string username, int forcedIndex, int teamNo)
		{
			PlayerInfo playerInfo = this.TryGetPlayer(id);
			if (playerInfo == null)
			{
				this.Players.Add(new PlayerInfo
				{
					PlayerId = id,
					Username = username,
					ForcedIndex = forcedIndex,
					TeamNo = teamNo
				});
				return;
			}
			playerInfo.TeamNo = teamNo;
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x000119B0 File Offset: 0x0000FBB0
		public bool TryUpdatePlayerStats(string id, int kill, int death, int assist)
		{
			PlayerInfo playerInfo = this.TryGetPlayer(id);
			if (playerInfo != null)
			{
				playerInfo.Kill = kill;
				playerInfo.Death = death;
				playerInfo.Assist = assist;
				return true;
			}
			return false;
		}
	}
}
