using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace TaleWorlds.MountAndBlade.Diamond.Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000176 RID: 374
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class DisconnectedFromChatRoomMessage : Message
	{
		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000A4F RID: 2639 RVA: 0x00010EE5 File Offset: 0x0000F0E5
		// (set) Token: 0x06000A50 RID: 2640 RVA: 0x00010EED File Offset: 0x0000F0ED
		[JsonProperty]
		public Guid RoomId { get; private set; }

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000A51 RID: 2641 RVA: 0x00010EF6 File Offset: 0x0000F0F6
		// (set) Token: 0x06000A52 RID: 2642 RVA: 0x00010EFE File Offset: 0x0000F0FE
		[JsonProperty]
		public string RoomName { get; private set; }

		// Token: 0x06000A53 RID: 2643 RVA: 0x00010F07 File Offset: 0x0000F107
		public DisconnectedFromChatRoomMessage()
		{
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x00010F0F File Offset: 0x0000F10F
		public DisconnectedFromChatRoomMessage(Guid roomId, string roomName)
		{
			this.RoomId = roomId;
			this.RoomName = roomName;
		}
	}
}
