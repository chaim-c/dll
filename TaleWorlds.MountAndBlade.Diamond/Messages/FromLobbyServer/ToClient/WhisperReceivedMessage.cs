using System;
using Newtonsoft.Json;
using TaleWorlds.Diamond;

namespace Messages.FromLobbyServer.ToClient
{
	// Token: 0x0200005D RID: 93
	[MessageDescription("LobbyServer", "Client")]
	[Serializable]
	public class WhisperReceivedMessage : Message
	{
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00003269 File Offset: 0x00001469
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x00003271 File Offset: 0x00001471
		[JsonProperty]
		public string FromPlayer { get; private set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001BA RID: 442 RVA: 0x0000327A File Offset: 0x0000147A
		// (set) Token: 0x060001BB RID: 443 RVA: 0x00003282 File Offset: 0x00001482
		[JsonProperty]
		public string ToPlayer { get; private set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001BC RID: 444 RVA: 0x0000328B File Offset: 0x0000148B
		// (set) Token: 0x060001BD RID: 445 RVA: 0x00003293 File Offset: 0x00001493
		[JsonProperty]
		public string Message { get; private set; }

		// Token: 0x060001BE RID: 446 RVA: 0x0000329C File Offset: 0x0000149C
		public WhisperReceivedMessage()
		{
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000032A4 File Offset: 0x000014A4
		public WhisperReceivedMessage(string fromPlayer, string toPlayer, string message)
		{
			this.FromPlayer = fromPlayer;
			this.ToPlayer = toPlayer;
			this.Message = message;
		}
	}
}
