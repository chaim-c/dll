using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;
using TaleWorlds.MountAndBlade.Diamond;

namespace Messages.FromCustomBattleServerManager.ToCustomBattleServer
{
	// Token: 0x0200006F RID: 111
	[MessageDescription("CustomBattleServer", "CustomBattleServerManager")]
	[Serializable]
	public class ResponseCustomGameClientConnectionMessage : Message
	{
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0000391F File Offset: 0x00001B1F
		// (set) Token: 0x0600023D RID: 573 RVA: 0x00003927 File Offset: 0x00001B27
		[JsonProperty]
		public PlayerJoinGameResponseDataFromHost[] PlayerJoinData { get; private set; }

		// Token: 0x0600023E RID: 574 RVA: 0x00003930 File Offset: 0x00001B30
		public ResponseCustomGameClientConnectionMessage()
		{
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00003938 File Offset: 0x00001B38
		public ResponseCustomGameClientConnectionMessage(PlayerJoinGameResponseDataFromHost[] playerJoinData)
		{
			this.PlayerJoinData = playerJoinData;
		}
	}
}
