using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TaleWorlds.PlayerServices.Avatar
{
	// Token: 0x0200000D RID: 13
	public class SteamAvatarService : ApiAvatarServiceBase
	{
		// Token: 0x06000070 RID: 112 RVA: 0x00003108 File Offset: 0x00001308
		protected override async Task FetchAvatars()
		{
			await Task.Delay(3000);
			List<ValueTuple<ulong, AvatarData>> waitingAccounts = base.WaitingAccounts;
			lock (waitingAccounts)
			{
				if (base.WaitingAccounts.Count < 1)
				{
					return;
				}
				if (base.WaitingAccounts.Count <= 100)
				{
					base.InProgressAccounts = base.WaitingAccounts;
					base.WaitingAccounts = new List<ValueTuple<ulong, AvatarData>>();
				}
				else
				{
					base.InProgressAccounts = base.WaitingAccounts.GetRange(0, 100);
					base.WaitingAccounts.RemoveRange(0, 100);
				}
			}
			string address = "http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=820D6EC50E6AAE61E460EA207D8966F7&steamids=" + string.Join<ulong>(",", from a in base.InProgressAccounts
			select a.Item1);
			SteamAvatarService.SteamPlayers steamPlayers = null;
			try
			{
				SteamAvatarService.GetPlayerSummariesResult getPlayerSummariesResult = JsonConvert.DeserializeObject<SteamAvatarService.GetPlayerSummariesResult>(await new TimeoutWebClient().DownloadStringTaskAsync(address));
				bool flag2;
				if (getPlayerSummariesResult == null)
				{
					flag2 = (null != null);
				}
				else
				{
					SteamAvatarService.SteamPlayers response = getPlayerSummariesResult.response;
					flag2 = (((response != null) ? response.players : null) != null);
				}
				if (flag2 && getPlayerSummariesResult.response.players.Length != 0)
				{
					steamPlayers = getPlayerSummariesResult.response;
				}
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
			}
			if (steamPlayers == null || steamPlayers.players.Length < 1)
			{
				foreach (ValueTuple<ulong, AvatarData> valueTuple in base.InProgressAccounts)
				{
					valueTuple.Item2.SetFailed();
				}
			}
			else
			{
				List<Task> list = new List<Task>();
				foreach (ValueTuple<ulong, AvatarData> valueTuple2 in base.InProgressAccounts)
				{
					ulong item = valueTuple2.Item1;
					AvatarData item2 = valueTuple2.Item2;
					string b = string.Concat(item);
					string text = null;
					foreach (SteamAvatarService.SteamPlayerSummary steamPlayerSummary in steamPlayers.players)
					{
						if (steamPlayerSummary.steamid == b)
						{
							text = steamPlayerSummary.avatarfull;
							break;
						}
					}
					if (!string.IsNullOrWhiteSpace(text))
					{
						list.Add(this.UpdateAvatarImageData(item, text, item2));
					}
					else
					{
						item2.SetFailed();
					}
				}
				if (list.Count > 0)
				{
					await Task.WhenAll(list);
				}
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003150 File Offset: 0x00001350
		private async Task UpdateAvatarImageData(ulong accountId, string avatarUrl, AvatarData avatarData)
		{
			if (!string.IsNullOrWhiteSpace(avatarUrl))
			{
				byte[] array = await new TimeoutWebClient().DownloadDataTaskAsync(avatarUrl);
				if (array != null && array.Length != 0)
				{
					avatarData.SetImageData(array);
					Dictionary<ulong, AvatarData> avatarImageCache = base.AvatarImageCache;
					lock (avatarImageCache)
					{
						base.AvatarImageCache[accountId] = avatarData;
					}
				}
			}
		}

		// Token: 0x0400002D RID: 45
		private const int FetchTaskWaitTime = 3000;

		// Token: 0x0400002E RID: 46
		private const string SteamWebApiKey = "820D6EC50E6AAE61E460EA207D8966F7";

		// Token: 0x0400002F RID: 47
		private const int MaxAccountsPerRequest = 100;

		// Token: 0x02000013 RID: 19
		private class GetPlayerSummariesResult
		{
			// Token: 0x1700001B RID: 27
			// (get) Token: 0x0600007D RID: 125 RVA: 0x0000351E File Offset: 0x0000171E
			// (set) Token: 0x0600007E RID: 126 RVA: 0x00003526 File Offset: 0x00001726
			public SteamAvatarService.SteamPlayers response { get; set; }
		}

		// Token: 0x02000014 RID: 20
		private class SteamPlayers
		{
			// Token: 0x1700001C RID: 28
			// (get) Token: 0x06000080 RID: 128 RVA: 0x00003537 File Offset: 0x00001737
			// (set) Token: 0x06000081 RID: 129 RVA: 0x0000353F File Offset: 0x0000173F
			public SteamAvatarService.SteamPlayerSummary[] players { get; set; }
		}

		// Token: 0x02000015 RID: 21
		private class SteamPlayerSummary
		{
			// Token: 0x1700001D RID: 29
			// (get) Token: 0x06000083 RID: 131 RVA: 0x00003550 File Offset: 0x00001750
			// (set) Token: 0x06000084 RID: 132 RVA: 0x00003558 File Offset: 0x00001758
			public string avatar { get; set; }

			// Token: 0x1700001E RID: 30
			// (get) Token: 0x06000085 RID: 133 RVA: 0x00003561 File Offset: 0x00001761
			// (set) Token: 0x06000086 RID: 134 RVA: 0x00003569 File Offset: 0x00001769
			public string avatarfull { get; set; }

			// Token: 0x1700001F RID: 31
			// (get) Token: 0x06000087 RID: 135 RVA: 0x00003572 File Offset: 0x00001772
			// (set) Token: 0x06000088 RID: 136 RVA: 0x0000357A File Offset: 0x0000177A
			public string avatarmedium { get; set; }

			// Token: 0x17000020 RID: 32
			// (get) Token: 0x06000089 RID: 137 RVA: 0x00003583 File Offset: 0x00001783
			// (set) Token: 0x0600008A RID: 138 RVA: 0x0000358B File Offset: 0x0000178B
			public int communityvisibilitystate { get; set; }

			// Token: 0x17000021 RID: 33
			// (get) Token: 0x0600008B RID: 139 RVA: 0x00003594 File Offset: 0x00001794
			// (set) Token: 0x0600008C RID: 140 RVA: 0x0000359C File Offset: 0x0000179C
			public int lastlogoff { get; set; }

			// Token: 0x17000022 RID: 34
			// (get) Token: 0x0600008D RID: 141 RVA: 0x000035A5 File Offset: 0x000017A5
			// (set) Token: 0x0600008E RID: 142 RVA: 0x000035AD File Offset: 0x000017AD
			public string personaname { get; set; }

			// Token: 0x17000023 RID: 35
			// (get) Token: 0x0600008F RID: 143 RVA: 0x000035B6 File Offset: 0x000017B6
			// (set) Token: 0x06000090 RID: 144 RVA: 0x000035BE File Offset: 0x000017BE
			public int personastate { get; set; }

			// Token: 0x17000024 RID: 36
			// (get) Token: 0x06000091 RID: 145 RVA: 0x000035C7 File Offset: 0x000017C7
			// (set) Token: 0x06000092 RID: 146 RVA: 0x000035CF File Offset: 0x000017CF
			public int personastateflags { get; set; }

			// Token: 0x17000025 RID: 37
			// (get) Token: 0x06000093 RID: 147 RVA: 0x000035D8 File Offset: 0x000017D8
			// (set) Token: 0x06000094 RID: 148 RVA: 0x000035E0 File Offset: 0x000017E0
			public string primaryclanid { get; set; }

			// Token: 0x17000026 RID: 38
			// (get) Token: 0x06000095 RID: 149 RVA: 0x000035E9 File Offset: 0x000017E9
			// (set) Token: 0x06000096 RID: 150 RVA: 0x000035F1 File Offset: 0x000017F1
			public int profilestate { get; set; }

			// Token: 0x17000027 RID: 39
			// (get) Token: 0x06000097 RID: 151 RVA: 0x000035FA File Offset: 0x000017FA
			// (set) Token: 0x06000098 RID: 152 RVA: 0x00003602 File Offset: 0x00001802
			public string profileurl { get; set; }

			// Token: 0x17000028 RID: 40
			// (get) Token: 0x06000099 RID: 153 RVA: 0x0000360B File Offset: 0x0000180B
			// (set) Token: 0x0600009A RID: 154 RVA: 0x00003613 File Offset: 0x00001813
			public string realname { get; set; }

			// Token: 0x17000029 RID: 41
			// (get) Token: 0x0600009B RID: 155 RVA: 0x0000361C File Offset: 0x0000181C
			// (set) Token: 0x0600009C RID: 156 RVA: 0x00003624 File Offset: 0x00001824
			public string steamid { get; set; }

			// Token: 0x1700002A RID: 42
			// (get) Token: 0x0600009D RID: 157 RVA: 0x0000362D File Offset: 0x0000182D
			// (set) Token: 0x0600009E RID: 158 RVA: 0x00003635 File Offset: 0x00001835
			public int timecreated { get; set; }
		}
	}
}
