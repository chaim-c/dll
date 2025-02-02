using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000051 RID: 81
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class ChangeGamePoll : GameNetworkMessage
	{
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x0000566D File Offset: 0x0000386D
		// (set) Token: 0x060002C5 RID: 709 RVA: 0x00005675 File Offset: 0x00003875
		public string GameType { get; private set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x0000567E File Offset: 0x0000387E
		// (set) Token: 0x060002C7 RID: 711 RVA: 0x00005686 File Offset: 0x00003886
		public string Map { get; private set; }

		// Token: 0x060002C8 RID: 712 RVA: 0x0000568F File Offset: 0x0000388F
		public ChangeGamePoll(string gameType, string map)
		{
			this.GameType = gameType;
			this.Map = map;
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x000056A5 File Offset: 0x000038A5
		public ChangeGamePoll()
		{
		}

		// Token: 0x060002CA RID: 714 RVA: 0x000056B0 File Offset: 0x000038B0
		protected override bool OnRead()
		{
			bool result = true;
			this.GameType = GameNetworkMessage.ReadStringFromPacket(ref result);
			this.Map = GameNetworkMessage.ReadStringFromPacket(ref result);
			return result;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x000056DA File Offset: 0x000038DA
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteStringToPacket(this.GameType);
			GameNetworkMessage.WriteStringToPacket(this.Map);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x000056F2 File Offset: 0x000038F2
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Administration;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x000056FA File Offset: 0x000038FA
		protected override string OnGetLogFormat()
		{
			return "Poll started: Change Map to: " + this.Map + " and GameType to: " + this.GameType;
		}
	}
}
