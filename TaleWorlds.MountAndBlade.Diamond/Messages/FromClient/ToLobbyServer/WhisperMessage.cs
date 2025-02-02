using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000D1 RID: 209
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class WhisperMessage : Message
	{
		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060003CA RID: 970 RVA: 0x00004952 File Offset: 0x00002B52
		// (set) Token: 0x060003CB RID: 971 RVA: 0x0000495A File Offset: 0x00002B5A
		[JsonProperty]
		public string TargetPlayerName { get; private set; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060003CC RID: 972 RVA: 0x00004963 File Offset: 0x00002B63
		// (set) Token: 0x060003CD RID: 973 RVA: 0x0000496B File Offset: 0x00002B6B
		[JsonProperty]
		public string Message { get; private set; }

		// Token: 0x060003CE RID: 974 RVA: 0x00004974 File Offset: 0x00002B74
		public WhisperMessage()
		{
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000497C File Offset: 0x00002B7C
		public WhisperMessage(string targetPlayerName, string message)
		{
			this.TargetPlayerName = targetPlayerName;
			this.Message = message;
		}
	}
}
