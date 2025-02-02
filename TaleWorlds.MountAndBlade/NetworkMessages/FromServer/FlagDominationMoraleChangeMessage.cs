using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000042 RID: 66
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class FlagDominationMoraleChangeMessage : GameNetworkMessage
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000220 RID: 544 RVA: 0x00004715 File Offset: 0x00002915
		// (set) Token: 0x06000221 RID: 545 RVA: 0x0000471D File Offset: 0x0000291D
		public float Morale { get; private set; }

		// Token: 0x06000222 RID: 546 RVA: 0x00004726 File Offset: 0x00002926
		public FlagDominationMoraleChangeMessage()
		{
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000472E File Offset: 0x0000292E
		public FlagDominationMoraleChangeMessage(float morale)
		{
			this.Morale = morale;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000473D File Offset: 0x0000293D
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteFloatToPacket(this.Morale, CompressionMission.FlagDominationMoraleCompressionInfo);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00004750 File Offset: 0x00002950
		protected override bool OnRead()
		{
			bool result = true;
			this.Morale = GameNetworkMessage.ReadFloatFromPacket(CompressionMission.FlagDominationMoraleCompressionInfo, ref result);
			return result;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00004772 File Offset: 0x00002972
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.GameMode;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000477A File Offset: 0x0000297A
		protected override string OnGetLogFormat()
		{
			return "Morale synched: " + this.Morale;
		}
	}
}
