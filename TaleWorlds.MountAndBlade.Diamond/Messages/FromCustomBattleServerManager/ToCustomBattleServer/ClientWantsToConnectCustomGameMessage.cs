using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromCustomBattleServerManager.ToCustomBattleServer
{
	// Token: 0x02000068 RID: 104
	[MessageDescription("CustomBattleServerManager", "CustomBattleServer")]
	[Serializable]
	public class ClientWantsToConnectCustomGameMessage : Message
	{
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000220 RID: 544 RVA: 0x000037B0 File Offset: 0x000019B0
		// (set) Token: 0x06000221 RID: 545 RVA: 0x000037B8 File Offset: 0x000019B8
		[JsonProperty]
		public PlayerJoinGameData[] PlayerJoinGameData { get; private set; }

		// Token: 0x06000222 RID: 546 RVA: 0x000037C1 File Offset: 0x000019C1
		public ClientWantsToConnectCustomGameMessage()
		{
		}

		// Token: 0x06000223 RID: 547 RVA: 0x000037C9 File Offset: 0x000019C9
		public ClientWantsToConnectCustomGameMessage(PlayerJoinGameData[] playerJoinGameData)
		{
			this.PlayerJoinGameData = playerJoinGameData;
		}
	}
}
