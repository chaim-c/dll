using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200004A RID: 74
	[Serializable]
	public class PlayerRemovedFromMatchmakerGame : Message
	{
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00002E48 File Offset: 0x00001048
		// (set) Token: 0x06000158 RID: 344 RVA: 0x00002E50 File Offset: 0x00001050
		[JsonProperty]
		public DisconnectType DisconnectType { get; private set; }

		// Token: 0x06000159 RID: 345 RVA: 0x00002E59 File Offset: 0x00001059
		public PlayerRemovedFromMatchmakerGame()
		{
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00002E61 File Offset: 0x00001061
		public PlayerRemovedFromMatchmakerGame(DisconnectType disconnectType)
		{
			this.DisconnectType = disconnectType;
		}
	}
}
