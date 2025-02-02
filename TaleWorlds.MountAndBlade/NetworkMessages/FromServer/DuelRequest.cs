using System;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000048 RID: 72
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class DuelRequest : GameNetworkMessage
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000262 RID: 610 RVA: 0x00004D56 File Offset: 0x00002F56
		// (set) Token: 0x06000263 RID: 611 RVA: 0x00004D5E File Offset: 0x00002F5E
		public int RequesterAgentIndex { get; private set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000264 RID: 612 RVA: 0x00004D67 File Offset: 0x00002F67
		// (set) Token: 0x06000265 RID: 613 RVA: 0x00004D6F File Offset: 0x00002F6F
		public int RequestedAgentIndex { get; private set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000266 RID: 614 RVA: 0x00004D78 File Offset: 0x00002F78
		// (set) Token: 0x06000267 RID: 615 RVA: 0x00004D80 File Offset: 0x00002F80
		public TroopType SelectedAreaTroopType { get; private set; }

		// Token: 0x06000268 RID: 616 RVA: 0x00004D89 File Offset: 0x00002F89
		public DuelRequest(int requesterAgentIndex, int requestedAgentIndex, TroopType selectedAreaTroopType)
		{
			this.RequesterAgentIndex = requesterAgentIndex;
			this.RequestedAgentIndex = requestedAgentIndex;
			this.SelectedAreaTroopType = selectedAreaTroopType;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00004DA6 File Offset: 0x00002FA6
		public DuelRequest()
		{
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00004DB0 File Offset: 0x00002FB0
		protected override bool OnRead()
		{
			bool result = true;
			this.RequesterAgentIndex = GameNetworkMessage.ReadAgentIndexFromPacket(ref result);
			this.RequestedAgentIndex = GameNetworkMessage.ReadAgentIndexFromPacket(ref result);
			this.SelectedAreaTroopType = (TroopType)GameNetworkMessage.ReadIntFromPacket(CompressionBasic.TroopTypeCompressionInfo, ref result);
			return result;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00004DEC File Offset: 0x00002FEC
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteAgentIndexToPacket(this.RequesterAgentIndex);
			GameNetworkMessage.WriteAgentIndexToPacket(this.RequestedAgentIndex);
			GameNetworkMessage.WriteIntToPacket((int)this.SelectedAreaTroopType, CompressionBasic.TroopTypeCompressionInfo);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00004E14 File Offset: 0x00003014
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.GameMode;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00004E1C File Offset: 0x0000301C
		protected override string OnGetLogFormat()
		{
			return "Request duel from agent with index: " + this.RequestedAgentIndex;
		}
	}
}
