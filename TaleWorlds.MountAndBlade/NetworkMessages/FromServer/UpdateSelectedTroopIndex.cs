using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x0200004C RID: 76
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class UpdateSelectedTroopIndex : GameNetworkMessage
	{
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600028A RID: 650 RVA: 0x000050ED File Offset: 0x000032ED
		// (set) Token: 0x0600028B RID: 651 RVA: 0x000050F5 File Offset: 0x000032F5
		public NetworkCommunicator Peer { get; private set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600028C RID: 652 RVA: 0x000050FE File Offset: 0x000032FE
		// (set) Token: 0x0600028D RID: 653 RVA: 0x00005106 File Offset: 0x00003306
		public int SelectedTroopIndex { get; private set; }

		// Token: 0x0600028E RID: 654 RVA: 0x0000510F File Offset: 0x0000330F
		public UpdateSelectedTroopIndex(NetworkCommunicator peer, int selectedTroopIndex)
		{
			this.Peer = peer;
			this.SelectedTroopIndex = selectedTroopIndex;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00005125 File Offset: 0x00003325
		public UpdateSelectedTroopIndex()
		{
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00005130 File Offset: 0x00003330
		protected override bool OnRead()
		{
			bool result = true;
			this.Peer = GameNetworkMessage.ReadNetworkPeerReferenceFromPacket(ref result, false);
			this.SelectedTroopIndex = GameNetworkMessage.ReadIntFromPacket(CompressionMission.SelectedTroopIndexCompressionInfo, ref result);
			return result;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00005160 File Offset: 0x00003360
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteNetworkPeerReferenceToPacket(this.Peer);
			GameNetworkMessage.WriteIntToPacket(this.SelectedTroopIndex, CompressionMission.SelectedTroopIndexCompressionInfo);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000517D File Offset: 0x0000337D
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Equipment;
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00005184 File Offset: 0x00003384
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Update SelectedTroopIndex to: ",
				this.SelectedTroopIndex,
				", on peer: ",
				this.Peer.UserName,
				" with peer-index:",
				this.Peer.Index
			});
		}
	}
}
