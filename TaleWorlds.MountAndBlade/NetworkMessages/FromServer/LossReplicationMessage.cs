using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000D1 RID: 209
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class LossReplicationMessage : GameNetworkMessage
	{
		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000894 RID: 2196 RVA: 0x0000E6BB File Offset: 0x0000C8BB
		// (set) Token: 0x06000895 RID: 2197 RVA: 0x0000E6C3 File Offset: 0x0000C8C3
		internal int LossValue { get; private set; }

		// Token: 0x06000896 RID: 2198 RVA: 0x0000E6CC File Offset: 0x0000C8CC
		public LossReplicationMessage()
		{
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x0000E6D4 File Offset: 0x0000C8D4
		internal LossReplicationMessage(int lossValue)
		{
			this.LossValue = lossValue;
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x0000E6E4 File Offset: 0x0000C8E4
		protected override bool OnRead()
		{
			bool result = true;
			this.LossValue = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.LossValueCompressionInfo, ref result);
			return result;
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x0000E706 File Offset: 0x0000C906
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteIntToPacket(this.LossValue, CompressionBasic.LossValueCompressionInfo);
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x0000E718 File Offset: 0x0000C918
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.MissionDetailed;
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x0000E720 File Offset: 0x0000C920
		protected override string OnGetLogFormat()
		{
			return "LossReplicationMessage";
		}
	}
}
