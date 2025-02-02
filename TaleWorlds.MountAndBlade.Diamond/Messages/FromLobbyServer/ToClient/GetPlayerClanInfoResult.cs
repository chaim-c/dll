using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200002C RID: 44
	[Serializable]
	public class GetPlayerClanInfoResult : FunctionResult
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000BB RID: 187 RVA: 0x000027CA File Offset: 0x000009CA
		// (set) Token: 0x060000BC RID: 188 RVA: 0x000027D2 File Offset: 0x000009D2
		[JsonProperty]
		public ClanInfo ClanInfo { get; private set; }

		// Token: 0x060000BD RID: 189 RVA: 0x000027DB File Offset: 0x000009DB
		public GetPlayerClanInfoResult()
		{
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000027E3 File Offset: 0x000009E3
		public GetPlayerClanInfoResult(ClanInfo clanInfo)
		{
			this.ClanInfo = clanInfo;
		}
	}
}
