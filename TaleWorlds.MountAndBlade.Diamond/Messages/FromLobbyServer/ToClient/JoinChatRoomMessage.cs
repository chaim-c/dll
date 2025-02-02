using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200003A RID: 58
	[MessageDescription("LobbyServer", "Client")]
	[Serializable]
	public class JoinChatRoomMessage : Message
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00002AED File Offset: 0x00000CED
		// (set) Token: 0x06000108 RID: 264 RVA: 0x00002AF5 File Offset: 0x00000CF5
		[JsonProperty]
		public ChatRoomInformationForClient ChatRoomInformaton { get; private set; }

		// Token: 0x06000109 RID: 265 RVA: 0x00002AFE File Offset: 0x00000CFE
		public JoinChatRoomMessage()
		{
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00002B06 File Offset: 0x00000D06
		public JoinChatRoomMessage(ChatRoomInformationForClient chatRoomInformation)
		{
			this.ChatRoomInformaton = chatRoomInformation;
		}
	}
}
