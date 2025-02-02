using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x02000078 RID: 120
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class AssignAsClanOfficerMessage : Message
	{
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600025D RID: 605 RVA: 0x00003A6F File Offset: 0x00001C6F
		// (set) Token: 0x0600025E RID: 606 RVA: 0x00003A77 File Offset: 0x00001C77
		[JsonProperty]
		public PlayerId AssignedPlayerId { get; private set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600025F RID: 607 RVA: 0x00003A80 File Offset: 0x00001C80
		// (set) Token: 0x06000260 RID: 608 RVA: 0x00003A88 File Offset: 0x00001C88
		[JsonProperty]
		public bool DontUseNameForUnknownPlayer { get; private set; }

		// Token: 0x06000261 RID: 609 RVA: 0x00003A91 File Offset: 0x00001C91
		public AssignAsClanOfficerMessage()
		{
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00003A99 File Offset: 0x00001C99
		public AssignAsClanOfficerMessage(PlayerId assignedPlayerId, bool dontUseNameForUnknownPlayer)
		{
			this.AssignedPlayerId = assignedPlayerId;
			this.DontUseNameForUnknownPlayer = dontUseNameForUnknownPlayer;
		}
	}
}
