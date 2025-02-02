using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond.Ranked;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000047 RID: 71
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class PlayerMMRUpdateMessage : Message
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00002DD8 File Offset: 0x00000FD8
		// (set) Token: 0x0600014D RID: 333 RVA: 0x00002DE0 File Offset: 0x00000FE0
		[JsonProperty]
		public RankBarInfo OldInfo { get; private set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00002DE9 File Offset: 0x00000FE9
		// (set) Token: 0x0600014F RID: 335 RVA: 0x00002DF1 File Offset: 0x00000FF1
		[JsonProperty]
		public RankBarInfo NewInfo { get; private set; }

		// Token: 0x06000150 RID: 336 RVA: 0x00002DFA File Offset: 0x00000FFA
		public PlayerMMRUpdateMessage()
		{
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00002E02 File Offset: 0x00001002
		public PlayerMMRUpdateMessage(RankBarInfo oldInfo, RankBarInfo newInfo)
		{
			this.OldInfo = oldInfo;
			this.NewInfo = newInfo;
		}
	}
}
