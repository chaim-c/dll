using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000CC RID: 204
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class UpdateCustomGameData : Message
	{
		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x0000485A File Offset: 0x00002A5A
		// (set) Token: 0x060003B3 RID: 947 RVA: 0x00004862 File Offset: 0x00002A62
		[JsonProperty]
		public string NewGameType { get; private set; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x0000486B File Offset: 0x00002A6B
		// (set) Token: 0x060003B5 RID: 949 RVA: 0x00004873 File Offset: 0x00002A73
		[JsonProperty]
		public string NewMap { get; private set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x0000487C File Offset: 0x00002A7C
		// (set) Token: 0x060003B7 RID: 951 RVA: 0x00004884 File Offset: 0x00002A84
		[JsonProperty]
		public int NewMaxNumberOfPlayers { get; private set; }

		// Token: 0x060003B8 RID: 952 RVA: 0x0000488D File Offset: 0x00002A8D
		public UpdateCustomGameData()
		{
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00004895 File Offset: 0x00002A95
		public UpdateCustomGameData(string newGameType, string newMap, int newMaxNumberOfPlayers)
		{
			this.NewGameType = newGameType;
			this.NewMap = newMap;
			this.NewMaxNumberOfPlayers = newMaxNumberOfPlayers;
		}
	}
}
