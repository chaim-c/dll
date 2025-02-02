using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000BB RID: 187
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class RegisterCustomGameMessage : Message
	{
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600034F RID: 847 RVA: 0x00004426 File Offset: 0x00002626
		// (set) Token: 0x06000350 RID: 848 RVA: 0x0000442E File Offset: 0x0000262E
		[JsonProperty]
		public string GameModule { get; private set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000351 RID: 849 RVA: 0x00004437 File Offset: 0x00002637
		// (set) Token: 0x06000352 RID: 850 RVA: 0x0000443F File Offset: 0x0000263F
		[JsonProperty]
		public string GameType { get; private set; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000353 RID: 851 RVA: 0x00004448 File Offset: 0x00002648
		// (set) Token: 0x06000354 RID: 852 RVA: 0x00004450 File Offset: 0x00002650
		[JsonProperty]
		public string ServerName { get; private set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000355 RID: 853 RVA: 0x00004459 File Offset: 0x00002659
		// (set) Token: 0x06000356 RID: 854 RVA: 0x00004461 File Offset: 0x00002661
		[JsonProperty]
		public int MaxPlayerCount { get; private set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000357 RID: 855 RVA: 0x0000446A File Offset: 0x0000266A
		// (set) Token: 0x06000358 RID: 856 RVA: 0x00004472 File Offset: 0x00002672
		[JsonProperty]
		public string Map { get; private set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000359 RID: 857 RVA: 0x0000447B File Offset: 0x0000267B
		// (set) Token: 0x0600035A RID: 858 RVA: 0x00004483 File Offset: 0x00002683
		[JsonProperty]
		public string UniqueMapId { get; private set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600035B RID: 859 RVA: 0x0000448C File Offset: 0x0000268C
		// (set) Token: 0x0600035C RID: 860 RVA: 0x00004494 File Offset: 0x00002694
		[JsonProperty]
		public int Port { get; private set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600035D RID: 861 RVA: 0x0000449D File Offset: 0x0000269D
		// (set) Token: 0x0600035E RID: 862 RVA: 0x000044A5 File Offset: 0x000026A5
		[JsonProperty]
		public string GamePassword { get; private set; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600035F RID: 863 RVA: 0x000044AE File Offset: 0x000026AE
		// (set) Token: 0x06000360 RID: 864 RVA: 0x000044B6 File Offset: 0x000026B6
		[JsonProperty]
		public string AdminPassword { get; private set; }

		// Token: 0x06000361 RID: 865 RVA: 0x000044BF File Offset: 0x000026BF
		public RegisterCustomGameMessage()
		{
		}

		// Token: 0x06000362 RID: 866 RVA: 0x000044C8 File Offset: 0x000026C8
		public RegisterCustomGameMessage(string gameModule, string gameType, string serverName, int maxPlayerCount, string map, string uniqueMapId, string gamePassword, string adminPassword, int port)
		{
			this.GameModule = gameModule;
			this.GameType = gameType;
			this.ServerName = serverName;
			this.MaxPlayerCount = maxPlayerCount;
			this.Map = map;
			this.UniqueMapId = uniqueMapId;
			this.GamePassword = gamePassword;
			this.AdminPassword = adminPassword;
			this.Port = port;
		}
	}
}
