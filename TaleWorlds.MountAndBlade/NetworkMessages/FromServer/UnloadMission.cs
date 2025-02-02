using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000CB RID: 203
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class UnloadMission : GameNetworkMessage
	{
		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x0000E1D7 File Offset: 0x0000C3D7
		// (set) Token: 0x06000859 RID: 2137 RVA: 0x0000E1DF File Offset: 0x0000C3DF
		public bool UnloadingForBattleIndexMismatch { get; private set; }

		// Token: 0x0600085A RID: 2138 RVA: 0x0000E1E8 File Offset: 0x0000C3E8
		public UnloadMission()
		{
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0000E1F0 File Offset: 0x0000C3F0
		public UnloadMission(bool unloadingForBattleIndexMismatch)
		{
			this.UnloadingForBattleIndexMismatch = unloadingForBattleIndexMismatch;
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0000E200 File Offset: 0x0000C400
		protected override bool OnRead()
		{
			bool result = true;
			this.UnloadingForBattleIndexMismatch = GameNetworkMessage.ReadBoolFromPacket(ref result);
			return result;
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x0000E21D File Offset: 0x0000C41D
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteBoolToPacket(this.UnloadingForBattleIndexMismatch);
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x0000E22A File Offset: 0x0000C42A
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Mission;
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x0000E232 File Offset: 0x0000C432
		protected override string OnGetLogFormat()
		{
			return "Unload Mission";
		}
	}
}
