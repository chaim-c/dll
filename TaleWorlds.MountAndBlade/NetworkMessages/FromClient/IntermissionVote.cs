using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromClient
{
	// Token: 0x02000017 RID: 23
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class IntermissionVote : GameNetworkMessage
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00002E24 File Offset: 0x00001024
		// (set) Token: 0x060000AD RID: 173 RVA: 0x00002E2C File Offset: 0x0000102C
		public int VoteCount { get; private set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00002E35 File Offset: 0x00001035
		// (set) Token: 0x060000AF RID: 175 RVA: 0x00002E3D File Offset: 0x0000103D
		public string ItemID { get; private set; }

		// Token: 0x060000B0 RID: 176 RVA: 0x00002E46 File Offset: 0x00001046
		public IntermissionVote(string itemID, int voteCount)
		{
			this.VoteCount = voteCount;
			this.ItemID = itemID;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00002E5C File Offset: 0x0000105C
		public IntermissionVote()
		{
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00002E64 File Offset: 0x00001064
		protected override bool OnRead()
		{
			bool result = true;
			this.ItemID = GameNetworkMessage.ReadStringFromPacket(ref result);
			this.VoteCount = GameNetworkMessage.ReadIntFromPacket(new CompressionInfo.Integer(-1, 1, true), ref result);
			return result;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00002E96 File Offset: 0x00001096
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteStringToPacket(this.ItemID);
			GameNetworkMessage.WriteIntToPacket(this.VoteCount, new CompressionInfo.Integer(-1, 1, true));
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00002EB6 File Offset: 0x000010B6
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Administration;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00002EBE File Offset: 0x000010BE
		protected override string OnGetLogFormat()
		{
			return string.Format("Intermission vote casted for item with ID: {0} with count: {1}.", this.ItemID, this.VoteCount);
		}
	}
}
