using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.PlayerServices;

namespace Messages.FromBattleServer.ToBattleServerManager
{
	// Token: 0x020000D3 RID: 211
	[MessageDescription("BattleServer", "BattleServerManager")]
	[Serializable]
	public class BattleCancelledDueToPlayerQuitMessage : Message
	{
		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x0000499A File Offset: 0x00002B9A
		// (set) Token: 0x060003D2 RID: 978 RVA: 0x000049A2 File Offset: 0x00002BA2
		[JsonProperty]
		public PlayerId PlayerId { get; private set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x000049AB File Offset: 0x00002BAB
		// (set) Token: 0x060003D4 RID: 980 RVA: 0x000049B3 File Offset: 0x00002BB3
		[JsonProperty]
		public string GameType { get; private set; }

		// Token: 0x060003D5 RID: 981 RVA: 0x000049BC File Offset: 0x00002BBC
		public BattleCancelledDueToPlayerQuitMessage()
		{
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x000049C4 File Offset: 0x00002BC4
		public BattleCancelledDueToPlayerQuitMessage(PlayerId playerId, string gameType)
		{
			this.PlayerId = playerId;
			this.GameType = gameType;
		}
	}
}
