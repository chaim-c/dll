using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x020000D0 RID: 208
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class UpdateUsingClanSigil : Message
	{
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x0000492A File Offset: 0x00002B2A
		// (set) Token: 0x060003C7 RID: 967 RVA: 0x00004932 File Offset: 0x00002B32
		[JsonProperty]
		public bool IsUsed { get; private set; }

		// Token: 0x060003C8 RID: 968 RVA: 0x0000493B File Offset: 0x00002B3B
		public UpdateUsingClanSigil()
		{
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00004943 File Offset: 0x00002B43
		public UpdateUsingClanSigil(bool isUsed)
		{
			this.IsUsed = isUsed;
		}
	}
}
