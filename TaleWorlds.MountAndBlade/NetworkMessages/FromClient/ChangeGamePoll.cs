using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromClient
{
	// Token: 0x02000015 RID: 21
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class ChangeGamePoll : GameNetworkMessage
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00002D0F File Offset: 0x00000F0F
		// (set) Token: 0x0600009B RID: 155 RVA: 0x00002D17 File Offset: 0x00000F17
		public string GameType { get; private set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00002D20 File Offset: 0x00000F20
		// (set) Token: 0x0600009D RID: 157 RVA: 0x00002D28 File Offset: 0x00000F28
		public string Map { get; private set; }

		// Token: 0x0600009E RID: 158 RVA: 0x00002D31 File Offset: 0x00000F31
		public ChangeGamePoll(string gameType, string map)
		{
			this.GameType = gameType;
			this.Map = map;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00002D47 File Offset: 0x00000F47
		public ChangeGamePoll()
		{
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00002D50 File Offset: 0x00000F50
		protected override bool OnRead()
		{
			bool result = true;
			this.GameType = GameNetworkMessage.ReadStringFromPacket(ref result);
			this.Map = GameNetworkMessage.ReadStringFromPacket(ref result);
			return result;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00002D7A File Offset: 0x00000F7A
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteStringToPacket(this.GameType);
			GameNetworkMessage.WriteStringToPacket(this.Map);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00002D92 File Offset: 0x00000F92
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Administration;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00002D9A File Offset: 0x00000F9A
		protected override string OnGetLogFormat()
		{
			return "Poll Requested: Change Map to: " + this.Map + " and GameType to: " + this.GameType;
		}
	}
}
