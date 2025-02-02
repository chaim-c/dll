using System;
using TaleWorlds.Diamond.ChatSystem.Library;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000124 RID: 292
	public interface IChatClientHandler
	{
		// Token: 0x0600067F RID: 1663
		void OnChatMessageReceived(Guid roomId, string roomName, string playerName, string textMessage, string textColor, MessageType type);
	}
}
