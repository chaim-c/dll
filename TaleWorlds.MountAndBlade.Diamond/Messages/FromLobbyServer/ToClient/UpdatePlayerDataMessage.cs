using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200005A RID: 90
	[MessageDescription("LobbyServer", "Client")]
	[Serializable]
	public class UpdatePlayerDataMessage : Message
	{
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001AC RID: 428 RVA: 0x000031F1 File Offset: 0x000013F1
		// (set) Token: 0x060001AD RID: 429 RVA: 0x000031F9 File Offset: 0x000013F9
		[JsonProperty]
		public PlayerData PlayerData { get; private set; }

		// Token: 0x060001AE RID: 430 RVA: 0x00003202 File Offset: 0x00001402
		public UpdatePlayerDataMessage()
		{
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000320A File Offset: 0x0000140A
		public UpdatePlayerDataMessage(PlayerData playerData)
		{
			this.PlayerData = playerData;
		}
	}
}
