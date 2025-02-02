using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200001E RID: 30
	[MessageDescription("LobbyServer", "Client")]
	[Serializable]
	public class FindGameAnswerMessage : Message
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000081 RID: 129 RVA: 0x0000257C File Offset: 0x0000077C
		// (set) Token: 0x06000082 RID: 130 RVA: 0x00002584 File Offset: 0x00000784
		[JsonProperty]
		public bool Successful { get; private set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000083 RID: 131 RVA: 0x0000258D File Offset: 0x0000078D
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00002595 File Offset: 0x00000795
		[JsonProperty]
		public string[] SelectedAndEnabledGameTypes { get; private set; }

		// Token: 0x06000085 RID: 133 RVA: 0x0000259E File Offset: 0x0000079E
		public FindGameAnswerMessage()
		{
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000025A6 File Offset: 0x000007A6
		public FindGameAnswerMessage(bool successful, string[] selectedAndEnabledGameTypes)
		{
			this.Successful = successful;
			this.SelectedAndEnabledGameTypes = selectedAndEnabledGameTypes;
		}
	}
}
