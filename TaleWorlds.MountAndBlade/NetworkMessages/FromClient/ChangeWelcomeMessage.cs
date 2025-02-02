using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromClient
{
	// Token: 0x02000016 RID: 22
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class ChangeWelcomeMessage : GameNetworkMessage
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00002DB7 File Offset: 0x00000FB7
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x00002DBF File Offset: 0x00000FBF
		public string NewWelcomeMessage { get; private set; }

		// Token: 0x060000A6 RID: 166 RVA: 0x00002DC8 File Offset: 0x00000FC8
		public ChangeWelcomeMessage(string newWelcomeMessage)
		{
			this.NewWelcomeMessage = newWelcomeMessage;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00002DD7 File Offset: 0x00000FD7
		public ChangeWelcomeMessage()
		{
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00002DE0 File Offset: 0x00000FE0
		protected override bool OnRead()
		{
			bool result = true;
			this.NewWelcomeMessage = GameNetworkMessage.ReadStringFromPacket(ref result);
			return result;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00002DFD File Offset: 0x00000FFD
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteStringToPacket(this.NewWelcomeMessage);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00002E0A File Offset: 0x0000100A
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Administration;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00002E12 File Offset: 0x00001012
		protected override string OnGetLogFormat()
		{
			return "Requested to change the welcome message to: " + this.NewWelcomeMessage;
		}
	}
}
