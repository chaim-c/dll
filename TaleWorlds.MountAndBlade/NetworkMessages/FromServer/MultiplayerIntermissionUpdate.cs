using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x0200005C RID: 92
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class MultiplayerIntermissionUpdate : GameNetworkMessage
	{
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000340 RID: 832 RVA: 0x00006248 File Offset: 0x00004448
		// (set) Token: 0x06000341 RID: 833 RVA: 0x00006250 File Offset: 0x00004450
		public MultiplayerIntermissionState IntermissionState { get; private set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000342 RID: 834 RVA: 0x00006259 File Offset: 0x00004459
		// (set) Token: 0x06000343 RID: 835 RVA: 0x00006261 File Offset: 0x00004461
		public float IntermissionTimer { get; private set; }

		// Token: 0x06000344 RID: 836 RVA: 0x0000626A File Offset: 0x0000446A
		public MultiplayerIntermissionUpdate()
		{
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00006272 File Offset: 0x00004472
		public MultiplayerIntermissionUpdate(MultiplayerIntermissionState intermissionState, float intermissionTimer)
		{
			this.IntermissionState = intermissionState;
			this.IntermissionTimer = intermissionTimer;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00006288 File Offset: 0x00004488
		protected override bool OnRead()
		{
			bool result = true;
			int intermissionState = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.IntermissionStateCompressionInfo, ref result);
			this.IntermissionState = (MultiplayerIntermissionState)intermissionState;
			this.IntermissionTimer = GameNetworkMessage.ReadFloatFromPacket(CompressionBasic.IntermissionTimerCompressionInfo, ref result);
			return result;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x000062BE File Offset: 0x000044BE
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteIntToPacket((int)this.IntermissionState, CompressionBasic.IntermissionStateCompressionInfo);
			GameNetworkMessage.WriteFloatToPacket(this.IntermissionTimer, CompressionBasic.IntermissionTimerCompressionInfo);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x000062E0 File Offset: 0x000044E0
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Administration;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x000062E8 File Offset: 0x000044E8
		protected override string OnGetLogFormat()
		{
			return "Receiving runtime intermission state.";
		}
	}
}
