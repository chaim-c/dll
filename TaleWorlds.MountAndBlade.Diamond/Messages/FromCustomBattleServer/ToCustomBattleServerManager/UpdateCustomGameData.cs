using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromCustomBattleServer.ToCustomBattleServerManager
{
	// Token: 0x02000065 RID: 101
	[MessageDescription("CustomBattleServer", "CustomBattleServerManager")]
	[Serializable]
	public class UpdateCustomGameData : Message
	{
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600020C RID: 524 RVA: 0x000036D8 File Offset: 0x000018D8
		// (set) Token: 0x0600020D RID: 525 RVA: 0x000036E0 File Offset: 0x000018E0
		[JsonProperty]
		public string NewGameType { get; private set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600020E RID: 526 RVA: 0x000036E9 File Offset: 0x000018E9
		// (set) Token: 0x0600020F RID: 527 RVA: 0x000036F1 File Offset: 0x000018F1
		[JsonProperty]
		public string NewMap { get; private set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000210 RID: 528 RVA: 0x000036FA File Offset: 0x000018FA
		// (set) Token: 0x06000211 RID: 529 RVA: 0x00003702 File Offset: 0x00001902
		[JsonProperty]
		public int NewMaxNumberOfPlayers { get; private set; }

		// Token: 0x06000212 RID: 530 RVA: 0x0000370B File Offset: 0x0000190B
		public UpdateCustomGameData()
		{
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00003713 File Offset: 0x00001913
		public UpdateCustomGameData(string newGameType, string newMap, int newMaxNumberOfPlayers)
		{
			this.NewGameType = newGameType;
			this.NewMap = newMap;
			this.NewMaxNumberOfPlayers = newMaxNumberOfPlayers;
		}
	}
}
