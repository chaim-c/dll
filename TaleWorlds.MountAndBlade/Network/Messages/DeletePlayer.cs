using System;

namespace TaleWorlds.MountAndBlade.Network.Messages
{
	// Token: 0x0200039D RID: 925
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class DeletePlayer : GameNetworkMessage
	{
		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x06003208 RID: 12808 RVA: 0x000CF20D File Offset: 0x000CD40D
		// (set) Token: 0x06003209 RID: 12809 RVA: 0x000CF215 File Offset: 0x000CD415
		public int PlayerIndex { get; private set; }

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x0600320A RID: 12810 RVA: 0x000CF21E File Offset: 0x000CD41E
		// (set) Token: 0x0600320B RID: 12811 RVA: 0x000CF226 File Offset: 0x000CD426
		public bool AddToDisconnectList { get; private set; }

		// Token: 0x0600320C RID: 12812 RVA: 0x000CF22F File Offset: 0x000CD42F
		public DeletePlayer(int playerIndex, bool addToDisconnectList)
		{
			this.PlayerIndex = playerIndex;
			this.AddToDisconnectList = addToDisconnectList;
		}

		// Token: 0x0600320D RID: 12813 RVA: 0x000CF245 File Offset: 0x000CD445
		public DeletePlayer()
		{
		}

		// Token: 0x0600320E RID: 12814 RVA: 0x000CF24D File Offset: 0x000CD44D
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteIntToPacket(this.PlayerIndex, CompressionBasic.PlayerCompressionInfo);
			GameNetworkMessage.WriteBoolToPacket(this.AddToDisconnectList);
		}

		// Token: 0x0600320F RID: 12815 RVA: 0x000CF26C File Offset: 0x000CD46C
		protected override bool OnRead()
		{
			bool result = true;
			this.PlayerIndex = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.PlayerCompressionInfo, ref result);
			this.AddToDisconnectList = GameNetworkMessage.ReadBoolFromPacket(ref result);
			return result;
		}

		// Token: 0x06003210 RID: 12816 RVA: 0x000CF29B File Offset: 0x000CD49B
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Peers;
		}

		// Token: 0x06003211 RID: 12817 RVA: 0x000CF29F File Offset: 0x000CD49F
		protected override string OnGetLogFormat()
		{
			return "Delete player with index" + this.PlayerIndex;
		}
	}
}
