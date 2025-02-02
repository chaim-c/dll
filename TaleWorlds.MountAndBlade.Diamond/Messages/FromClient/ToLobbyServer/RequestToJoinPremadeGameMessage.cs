using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000C4 RID: 196
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class RequestToJoinPremadeGameMessage : Message
	{
		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600038E RID: 910 RVA: 0x000046EA File Offset: 0x000028EA
		// (set) Token: 0x0600038F RID: 911 RVA: 0x000046F2 File Offset: 0x000028F2
		[JsonProperty]
		public Guid GameId { get; private set; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000390 RID: 912 RVA: 0x000046FB File Offset: 0x000028FB
		// (set) Token: 0x06000391 RID: 913 RVA: 0x00004703 File Offset: 0x00002903
		[JsonProperty]
		public string Password { get; private set; }

		// Token: 0x06000392 RID: 914 RVA: 0x0000470C File Offset: 0x0000290C
		public RequestToJoinPremadeGameMessage()
		{
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00004714 File Offset: 0x00002914
		public RequestToJoinPremadeGameMessage(Guid gameId, string password)
		{
			this.GameId = gameId;
			this.Password = password;
		}
	}
}
