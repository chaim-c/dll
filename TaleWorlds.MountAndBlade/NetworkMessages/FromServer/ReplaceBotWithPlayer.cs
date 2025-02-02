using System;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000097 RID: 151
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class ReplaceBotWithPlayer : GameNetworkMessage
	{
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x0000AA43 File Offset: 0x00008C43
		// (set) Token: 0x060005FA RID: 1530 RVA: 0x0000AA4B File Offset: 0x00008C4B
		public int BotAgentIndex { get; private set; }

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x0000AA54 File Offset: 0x00008C54
		// (set) Token: 0x060005FC RID: 1532 RVA: 0x0000AA5C File Offset: 0x00008C5C
		public NetworkCommunicator Peer { get; private set; }

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x0000AA65 File Offset: 0x00008C65
		// (set) Token: 0x060005FE RID: 1534 RVA: 0x0000AA6D File Offset: 0x00008C6D
		public int Health { get; private set; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x0000AA76 File Offset: 0x00008C76
		// (set) Token: 0x06000600 RID: 1536 RVA: 0x0000AA7E File Offset: 0x00008C7E
		public int MountHealth { get; private set; }

		// Token: 0x06000601 RID: 1537 RVA: 0x0000AA87 File Offset: 0x00008C87
		public ReplaceBotWithPlayer(NetworkCommunicator peer, int botAgentIndex, float botAgentHealth, float botAgentMountHealth = -1f)
		{
			this.Peer = peer;
			this.BotAgentIndex = botAgentIndex;
			this.Health = MathF.Ceiling(botAgentHealth);
			this.MountHealth = MathF.Ceiling(botAgentMountHealth);
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x0000AAB6 File Offset: 0x00008CB6
		public ReplaceBotWithPlayer()
		{
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0000AAC0 File Offset: 0x00008CC0
		protected override bool OnRead()
		{
			bool result = true;
			this.Peer = GameNetworkMessage.ReadNetworkPeerReferenceFromPacket(ref result, false);
			this.BotAgentIndex = GameNetworkMessage.ReadAgentIndexFromPacket(ref result);
			this.Health = GameNetworkMessage.ReadIntFromPacket(CompressionMission.AgentHealthCompressionInfo, ref result);
			this.MountHealth = GameNetworkMessage.ReadIntFromPacket(CompressionMission.AgentHealthCompressionInfo, ref result);
			return result;
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x0000AB0F File Offset: 0x00008D0F
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteNetworkPeerReferenceToPacket(this.Peer);
			GameNetworkMessage.WriteAgentIndexToPacket(this.BotAgentIndex);
			GameNetworkMessage.WriteIntToPacket(this.Health, CompressionMission.AgentHealthCompressionInfo);
			GameNetworkMessage.WriteIntToPacket(this.MountHealth, CompressionMission.AgentHealthCompressionInfo);
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x0000AB47 File Offset: 0x00008D47
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Agents;
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x0000AB4F File Offset: 0x00008D4F
		protected override string OnGetLogFormat()
		{
			return "Replace a bot with a player";
		}
	}
}
