using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000026 RID: 38
	[Serializable]
	public class GetClanLeaderboardResult : FunctionResult
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x000026DA File Offset: 0x000008DA
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x000026E2 File Offset: 0x000008E2
		[JsonProperty]
		public ClanLeaderboardInfo ClanLeaderboardInfo { get; private set; }

		// Token: 0x060000A5 RID: 165 RVA: 0x000026EB File Offset: 0x000008EB
		public GetClanLeaderboardResult()
		{
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000026F3 File Offset: 0x000008F3
		public GetClanLeaderboardResult(ClanLeaderboardInfo info)
		{
			this.ClanLeaderboardInfo = info;
		}
	}
}
