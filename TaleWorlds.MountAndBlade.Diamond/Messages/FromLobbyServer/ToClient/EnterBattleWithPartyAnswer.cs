using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200001C RID: 28
	[MessageDescription("LobbyServer", "Client")]
	[Serializable]
	public class EnterBattleWithPartyAnswer : Message
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00002514 File Offset: 0x00000714
		// (set) Token: 0x06000078 RID: 120 RVA: 0x0000251C File Offset: 0x0000071C
		[JsonProperty]
		public bool Successful { get; private set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00002525 File Offset: 0x00000725
		// (set) Token: 0x0600007A RID: 122 RVA: 0x0000252D File Offset: 0x0000072D
		[JsonProperty]
		public string[] SelectedAndEnabledGameTypes { get; private set; }

		// Token: 0x0600007B RID: 123 RVA: 0x00002536 File Offset: 0x00000736
		public EnterBattleWithPartyAnswer()
		{
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000253E File Offset: 0x0000073E
		public EnterBattleWithPartyAnswer(bool successful, string[] selectedAndEnabledGameTypes)
		{
			this.Successful = successful;
			this.SelectedAndEnabledGameTypes = selectedAndEnabledGameTypes;
		}
	}
}
