using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000056 RID: 86
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class KillDeathCountChange : GameNetworkMessage
	{
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000300 RID: 768 RVA: 0x00005CDD File Offset: 0x00003EDD
		// (set) Token: 0x06000301 RID: 769 RVA: 0x00005CE5 File Offset: 0x00003EE5
		public NetworkCommunicator VictimPeer { get; private set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000302 RID: 770 RVA: 0x00005CEE File Offset: 0x00003EEE
		// (set) Token: 0x06000303 RID: 771 RVA: 0x00005CF6 File Offset: 0x00003EF6
		public NetworkCommunicator AttackerPeer { get; private set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000304 RID: 772 RVA: 0x00005CFF File Offset: 0x00003EFF
		// (set) Token: 0x06000305 RID: 773 RVA: 0x00005D07 File Offset: 0x00003F07
		public int KillCount { get; private set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000306 RID: 774 RVA: 0x00005D10 File Offset: 0x00003F10
		// (set) Token: 0x06000307 RID: 775 RVA: 0x00005D18 File Offset: 0x00003F18
		public int AssistCount { get; private set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000308 RID: 776 RVA: 0x00005D21 File Offset: 0x00003F21
		// (set) Token: 0x06000309 RID: 777 RVA: 0x00005D29 File Offset: 0x00003F29
		public int DeathCount { get; private set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600030A RID: 778 RVA: 0x00005D32 File Offset: 0x00003F32
		// (set) Token: 0x0600030B RID: 779 RVA: 0x00005D3A File Offset: 0x00003F3A
		public int Score { get; private set; }

		// Token: 0x0600030C RID: 780 RVA: 0x00005D43 File Offset: 0x00003F43
		public KillDeathCountChange(NetworkCommunicator peer, NetworkCommunicator attackerPeer, int killCount, int assistCount, int deathCount, int score)
		{
			this.VictimPeer = peer;
			this.AttackerPeer = attackerPeer;
			this.KillCount = killCount;
			this.AssistCount = assistCount;
			this.DeathCount = deathCount;
			this.Score = score;
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00005D78 File Offset: 0x00003F78
		public KillDeathCountChange()
		{
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00005D80 File Offset: 0x00003F80
		protected override bool OnRead()
		{
			bool result = true;
			this.VictimPeer = GameNetworkMessage.ReadNetworkPeerReferenceFromPacket(ref result, false);
			this.AttackerPeer = GameNetworkMessage.ReadNetworkPeerReferenceFromPacket(ref result, true);
			this.KillCount = GameNetworkMessage.ReadIntFromPacket(CompressionMatchmaker.KillDeathAssistCountCompressionInfo, ref result);
			this.AssistCount = GameNetworkMessage.ReadIntFromPacket(CompressionMatchmaker.KillDeathAssistCountCompressionInfo, ref result);
			this.DeathCount = GameNetworkMessage.ReadIntFromPacket(CompressionMatchmaker.KillDeathAssistCountCompressionInfo, ref result);
			this.Score = GameNetworkMessage.ReadIntFromPacket(CompressionMatchmaker.ScoreCompressionInfo, ref result);
			return result;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00005DF4 File Offset: 0x00003FF4
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteNetworkPeerReferenceToPacket(this.VictimPeer);
			GameNetworkMessage.WriteNetworkPeerReferenceToPacket(this.AttackerPeer);
			GameNetworkMessage.WriteIntToPacket(this.KillCount, CompressionMatchmaker.KillDeathAssistCountCompressionInfo);
			GameNetworkMessage.WriteIntToPacket(this.AssistCount, CompressionMatchmaker.KillDeathAssistCountCompressionInfo);
			GameNetworkMessage.WriteIntToPacket(this.DeathCount, CompressionMatchmaker.KillDeathAssistCountCompressionInfo);
			GameNetworkMessage.WriteIntToPacket(this.Score, CompressionMatchmaker.ScoreCompressionInfo);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00005E57 File Offset: 0x00004057
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Administration;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00005E60 File Offset: 0x00004060
		protected override string OnGetLogFormat()
		{
			object[] array = new object[11];
			array[0] = "Kill-Death Count Changed. Peer: ";
			int num = 1;
			NetworkCommunicator victimPeer = this.VictimPeer;
			array[num] = (((victimPeer != null) ? victimPeer.UserName : null) ?? "NULL");
			array[2] = " killed peer: ";
			int num2 = 3;
			NetworkCommunicator attackerPeer = this.AttackerPeer;
			array[num2] = (((attackerPeer != null) ? attackerPeer.UserName : null) ?? "NULL");
			array[4] = " and now has ";
			array[5] = this.KillCount;
			array[6] = " kills, ";
			array[7] = this.AssistCount;
			array[8] = " assists, and ";
			array[9] = this.DeathCount;
			array[10] = " deaths.";
			return string.Concat(array);
		}
	}
}
