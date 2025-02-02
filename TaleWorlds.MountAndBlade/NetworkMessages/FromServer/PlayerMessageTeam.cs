using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x0200003E RID: 62
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class PlayerMessageTeam : GameNetworkMessage
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001FB RID: 507 RVA: 0x000044B1 File Offset: 0x000026B1
		// (set) Token: 0x060001FC RID: 508 RVA: 0x000044B9 File Offset: 0x000026B9
		public string Message { get; private set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001FD RID: 509 RVA: 0x000044C2 File Offset: 0x000026C2
		// (set) Token: 0x060001FE RID: 510 RVA: 0x000044CA File Offset: 0x000026CA
		public NetworkCommunicator Player { get; private set; }

		// Token: 0x060001FF RID: 511 RVA: 0x000044D3 File Offset: 0x000026D3
		public PlayerMessageTeam(NetworkCommunicator player, string message)
		{
			this.Player = player;
			this.Message = message;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x000044E9 File Offset: 0x000026E9
		public PlayerMessageTeam()
		{
		}

		// Token: 0x06000201 RID: 513 RVA: 0x000044F1 File Offset: 0x000026F1
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteNetworkPeerReferenceToPacket(this.Player);
			GameNetworkMessage.WriteStringToPacket(this.Message);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000450C File Offset: 0x0000270C
		protected override bool OnRead()
		{
			bool result = true;
			this.Player = GameNetworkMessage.ReadNetworkPeerReferenceFromPacket(ref result, false);
			this.Message = GameNetworkMessage.ReadStringFromPacket(ref result);
			return result;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00004537 File Offset: 0x00002737
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Messaging;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000453C File Offset: 0x0000273C
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Receiving team message: ",
				this.Message,
				" from peer: ",
				this.Player.UserName,
				" index: ",
				this.Player.Index
			});
		}
	}
}
