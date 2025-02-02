using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromClient.ToLobbyServer
{
	// Token: 0x02000089 RID: 137
	[MessageDescription("Client", "LobbyServer")]
	[Serializable]
	public class CreatePremadeGameMessage : Message
	{
		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600029F RID: 671 RVA: 0x00003D10 File Offset: 0x00001F10
		// (set) Token: 0x060002A0 RID: 672 RVA: 0x00003D18 File Offset: 0x00001F18
		[JsonProperty]
		public string PremadeGameName { get; private set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x00003D21 File Offset: 0x00001F21
		// (set) Token: 0x060002A2 RID: 674 RVA: 0x00003D29 File Offset: 0x00001F29
		[JsonProperty]
		public string GameType { get; private set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x00003D32 File Offset: 0x00001F32
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x00003D3A File Offset: 0x00001F3A
		[JsonProperty]
		public string MapName { get; private set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x00003D43 File Offset: 0x00001F43
		// (set) Token: 0x060002A6 RID: 678 RVA: 0x00003D4B File Offset: 0x00001F4B
		[JsonProperty]
		public string FactionA { get; private set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x00003D54 File Offset: 0x00001F54
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x00003D5C File Offset: 0x00001F5C
		[JsonProperty]
		public string FactionB { get; private set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x00003D65 File Offset: 0x00001F65
		// (set) Token: 0x060002AA RID: 682 RVA: 0x00003D6D File Offset: 0x00001F6D
		[JsonProperty]
		public string Password { get; private set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060002AB RID: 683 RVA: 0x00003D76 File Offset: 0x00001F76
		// (set) Token: 0x060002AC RID: 684 RVA: 0x00003D7E File Offset: 0x00001F7E
		[JsonProperty]
		public PremadeGameType PremadeGameType { get; private set; }

		// Token: 0x060002AD RID: 685 RVA: 0x00003D87 File Offset: 0x00001F87
		public CreatePremadeGameMessage()
		{
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00003D8F File Offset: 0x00001F8F
		public CreatePremadeGameMessage(string premadeGameName, string gameType, string mapName, string factionA, string factionB, string password, PremadeGameType premadeGameType)
		{
			this.PremadeGameName = premadeGameName;
			this.GameType = gameType;
			this.MapName = mapName;
			this.FactionA = factionA;
			this.FactionB = factionB;
			this.Password = password;
			this.PremadeGameType = premadeGameType;
		}
	}
}
