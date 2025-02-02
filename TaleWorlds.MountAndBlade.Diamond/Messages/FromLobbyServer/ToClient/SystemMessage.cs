using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x02000059 RID: 89
	[MessageDescription("LobbyServer", "Client")]
	[Serializable]
	public class SystemMessage : Message
	{
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x000031AC File Offset: 0x000013AC
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x000031B4 File Offset: 0x000013B4
		[JsonProperty]
		public ServerInfoMessage Message { get; private set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x000031BD File Offset: 0x000013BD
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x000031C5 File Offset: 0x000013C5
		[JsonProperty]
		public List<string> Parameters { get; private set; }

		// Token: 0x060001AA RID: 426 RVA: 0x000031CE File Offset: 0x000013CE
		public SystemMessage()
		{
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000031D6 File Offset: 0x000013D6
		public SystemMessage(ServerInfoMessage message, params string[] arguments)
		{
			this.Message = message;
			this.Parameters = new List<string>(arguments);
		}
	}
}
