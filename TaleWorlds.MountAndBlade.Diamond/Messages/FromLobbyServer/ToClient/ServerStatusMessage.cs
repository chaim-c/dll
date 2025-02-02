using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000056 RID: 86
	[MessageDescription("LobbyServer", "Client")]
	[Serializable]
	public class ServerStatusMessage : Message
	{
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00003134 File Offset: 0x00001334
		// (set) Token: 0x0600019B RID: 411 RVA: 0x0000313C File Offset: 0x0000133C
		[JsonProperty]
		public ServerStatus ServerStatus { get; private set; }

		// Token: 0x0600019C RID: 412 RVA: 0x00003145 File Offset: 0x00001345
		public ServerStatusMessage()
		{
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000314D File Offset: 0x0000134D
		public ServerStatusMessage(ServerStatus serverStatus)
		{
			this.ServerStatus = serverStatus;
		}
	}
}
