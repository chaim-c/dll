﻿using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromClient
{
	// Token: 0x02000022 RID: 34
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class ApplyOrderWithFormationAndNumber : GameNetworkMessage
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00003473 File Offset: 0x00001673
		// (set) Token: 0x0600010B RID: 267 RVA: 0x0000347B File Offset: 0x0000167B
		public OrderType OrderType { get; private set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00003484 File Offset: 0x00001684
		// (set) Token: 0x0600010D RID: 269 RVA: 0x0000348C File Offset: 0x0000168C
		public int FormationIndex { get; private set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00003495 File Offset: 0x00001695
		// (set) Token: 0x0600010F RID: 271 RVA: 0x0000349D File Offset: 0x0000169D
		public int Number { get; private set; }

		// Token: 0x06000110 RID: 272 RVA: 0x000034A6 File Offset: 0x000016A6
		public ApplyOrderWithFormationAndNumber(OrderType orderType, int formationIndex, int number)
		{
			this.OrderType = orderType;
			this.FormationIndex = formationIndex;
			this.Number = number;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000034C3 File Offset: 0x000016C3
		public ApplyOrderWithFormationAndNumber()
		{
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000034CC File Offset: 0x000016CC
		protected override bool OnRead()
		{
			bool result = true;
			this.OrderType = (OrderType)GameNetworkMessage.ReadIntFromPacket(CompressionMission.OrderTypeCompressionInfo, ref result);
			this.FormationIndex = GameNetworkMessage.ReadIntFromPacket(CompressionMission.FormationClassCompressionInfo, ref result);
			this.Number = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.DebugIntNonCompressionInfo, ref result);
			return result;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00003512 File Offset: 0x00001712
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteIntToPacket((int)this.OrderType, CompressionMission.OrderTypeCompressionInfo);
			GameNetworkMessage.WriteIntToPacket(this.FormationIndex, CompressionMission.FormationClassCompressionInfo);
			GameNetworkMessage.WriteIntToPacket(this.Number, CompressionBasic.DebugIntNonCompressionInfo);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00003544 File Offset: 0x00001744
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Formations | MultiplayerMessageFilter.Orders;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000354C File Offset: 0x0000174C
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Apply order: ",
				this.OrderType,
				", to formation with index: ",
				this.FormationIndex,
				" and number: ",
				this.Number
			});
		}
	}
}
