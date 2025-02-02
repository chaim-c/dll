using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromClient
{
	// Token: 0x0200001A RID: 26
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class PollResponse : GameNetworkMessage
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000CA RID: 202 RVA: 0x0000305A File Offset: 0x0000125A
		// (set) Token: 0x060000CB RID: 203 RVA: 0x00003062 File Offset: 0x00001262
		public bool Accepted { get; private set; }

		// Token: 0x060000CC RID: 204 RVA: 0x0000306B File Offset: 0x0000126B
		public PollResponse(bool accepted)
		{
			this.Accepted = accepted;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000307A File Offset: 0x0000127A
		public PollResponse()
		{
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00003084 File Offset: 0x00001284
		protected override bool OnRead()
		{
			bool result = true;
			this.Accepted = GameNetworkMessage.ReadBoolFromPacket(ref result);
			return result;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000030A1 File Offset: 0x000012A1
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteBoolToPacket(this.Accepted);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000030AE File Offset: 0x000012AE
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Administration;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000030B6 File Offset: 0x000012B6
		protected override string OnGetLogFormat()
		{
			return "Receiving poll response: " + (this.Accepted ? "Accepted." : "Not accepted.");
		}
	}
}
