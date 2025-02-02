using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromClient
{
	// Token: 0x02000023 RID: 35
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class ApplyOrderWithFormationAndPercentage : GameNetworkMessage
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000116 RID: 278 RVA: 0x000035A6 File Offset: 0x000017A6
		// (set) Token: 0x06000117 RID: 279 RVA: 0x000035AE File Offset: 0x000017AE
		public OrderType OrderType { get; private set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000118 RID: 280 RVA: 0x000035B7 File Offset: 0x000017B7
		// (set) Token: 0x06000119 RID: 281 RVA: 0x000035BF File Offset: 0x000017BF
		public int FormationIndex { get; private set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600011A RID: 282 RVA: 0x000035C8 File Offset: 0x000017C8
		// (set) Token: 0x0600011B RID: 283 RVA: 0x000035D0 File Offset: 0x000017D0
		public int Percentage { get; private set; }

		// Token: 0x0600011C RID: 284 RVA: 0x000035D9 File Offset: 0x000017D9
		public ApplyOrderWithFormationAndPercentage(OrderType orderType, int formationIndex, int percentage)
		{
			this.OrderType = orderType;
			this.FormationIndex = formationIndex;
			this.Percentage = percentage;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000035F6 File Offset: 0x000017F6
		public ApplyOrderWithFormationAndPercentage()
		{
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00003600 File Offset: 0x00001800
		protected override bool OnRead()
		{
			bool result = true;
			this.OrderType = (OrderType)GameNetworkMessage.ReadIntFromPacket(CompressionMission.OrderTypeCompressionInfo, ref result);
			this.FormationIndex = GameNetworkMessage.ReadIntFromPacket(CompressionMission.FormationClassCompressionInfo, ref result);
			this.Percentage = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.PercentageCompressionInfo, ref result);
			return result;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00003646 File Offset: 0x00001846
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteIntToPacket((int)this.OrderType, CompressionMission.OrderTypeCompressionInfo);
			GameNetworkMessage.WriteIntToPacket(this.FormationIndex, CompressionMission.FormationClassCompressionInfo);
			GameNetworkMessage.WriteIntToPacket(this.Percentage, CompressionBasic.PercentageCompressionInfo);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00003678 File Offset: 0x00001878
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Formations | MultiplayerMessageFilter.Orders;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00003680 File Offset: 0x00001880
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Apply order: ",
				this.OrderType,
				", to formation with index: ",
				this.FormationIndex,
				" and percentage: ",
				this.Percentage
			});
		}
	}
}
