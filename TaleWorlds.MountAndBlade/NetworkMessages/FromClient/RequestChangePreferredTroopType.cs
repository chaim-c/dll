using System;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromClient
{
	// Token: 0x0200000D RID: 13
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class RequestChangePreferredTroopType : GameNetworkMessage
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000051 RID: 81 RVA: 0x0000268D File Offset: 0x0000088D
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00002695 File Offset: 0x00000895
		public TroopType TroopType { get; private set; }

		// Token: 0x06000053 RID: 83 RVA: 0x0000269E File Offset: 0x0000089E
		public RequestChangePreferredTroopType(TroopType troopType)
		{
			this.TroopType = troopType;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000026AD File Offset: 0x000008AD
		public RequestChangePreferredTroopType()
		{
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000026B5 File Offset: 0x000008B5
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteIntToPacket((int)this.TroopType, CompressionBasic.TroopTypeCompressionInfo);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000026C8 File Offset: 0x000008C8
		protected override bool OnRead()
		{
			bool result = true;
			this.TroopType = (TroopType)GameNetworkMessage.ReadIntFromPacket(CompressionBasic.TroopTypeCompressionInfo, ref result);
			return result;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000026EA File Offset: 0x000008EA
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.MissionDetailed;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000026F2 File Offset: 0x000008F2
		protected override string OnGetLogFormat()
		{
			return "Peer requesting preferred troop type change to " + this.TroopType;
		}
	}
}
