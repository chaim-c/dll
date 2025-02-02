using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000049 RID: 73
	[Serializable]
	public class PlayerRemovedFromCustomGame : Message
	{
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00002E20 File Offset: 0x00001020
		// (set) Token: 0x06000154 RID: 340 RVA: 0x00002E28 File Offset: 0x00001028
		[JsonProperty]
		public DisconnectType DisconnectType { get; private set; }

		// Token: 0x06000155 RID: 341 RVA: 0x00002E31 File Offset: 0x00001031
		public PlayerRemovedFromCustomGame()
		{
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00002E39 File Offset: 0x00001039
		public PlayerRemovedFromCustomGame(DisconnectType disconnectType)
		{
			this.DisconnectType = disconnectType;
		}
	}
}
