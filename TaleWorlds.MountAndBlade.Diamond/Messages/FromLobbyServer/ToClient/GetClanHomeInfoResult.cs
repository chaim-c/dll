using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000025 RID: 37
	[Serializable]
	public class GetClanHomeInfoResult : FunctionResult
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600009F RID: 159 RVA: 0x000026B2 File Offset: 0x000008B2
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x000026BA File Offset: 0x000008BA
		[JsonProperty]
		public ClanHomeInfo ClanHomeInfo { get; private set; }

		// Token: 0x060000A1 RID: 161 RVA: 0x000026C3 File Offset: 0x000008C3
		public GetClanHomeInfoResult()
		{
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000026CB File Offset: 0x000008CB
		public GetClanHomeInfoResult(ClanHomeInfo clanHomeInfo)
		{
			this.ClanHomeInfo = clanHomeInfo;
		}
	}
}
