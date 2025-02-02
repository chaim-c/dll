using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromCustomBattleServer.ToCustomBattleServerManager
{
	// Token: 0x0200005E RID: 94
	[MessageDescription("CustomBattleServer", "CustomBattleServerManager")]
	[Serializable]
	public class AddGameLogsMessage : Message
	{
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x000032C1 File Offset: 0x000014C1
		// (set) Token: 0x060001C1 RID: 449 RVA: 0x000032C9 File Offset: 0x000014C9
		[JsonProperty]
		public GameLog[] GameLogs { get; private set; }

		// Token: 0x060001C2 RID: 450 RVA: 0x000032D2 File Offset: 0x000014D2
		public AddGameLogsMessage()
		{
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x000032DA File Offset: 0x000014DA
		public AddGameLogsMessage(GameLog[] gameLogs)
		{
			this.GameLogs = gameLogs;
		}
	}
}
