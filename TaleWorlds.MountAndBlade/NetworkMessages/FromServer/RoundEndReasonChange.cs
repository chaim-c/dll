using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x0200006B RID: 107
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class RoundEndReasonChange : GameNetworkMessage
	{
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060003BD RID: 957 RVA: 0x000071E3 File Offset: 0x000053E3
		// (set) Token: 0x060003BE RID: 958 RVA: 0x000071EB File Offset: 0x000053EB
		public RoundEndReason RoundEndReason { get; private set; }

		// Token: 0x060003BF RID: 959 RVA: 0x000071F4 File Offset: 0x000053F4
		public RoundEndReasonChange()
		{
			this.RoundEndReason = RoundEndReason.Invalid;
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x00007203 File Offset: 0x00005403
		public RoundEndReasonChange(RoundEndReason roundEndReason)
		{
			this.RoundEndReason = roundEndReason;
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00007212 File Offset: 0x00005412
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteIntToPacket((int)this.RoundEndReason, CompressionMission.RoundEndReasonCompressionInfo);
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00007224 File Offset: 0x00005424
		protected override bool OnRead()
		{
			bool result = true;
			this.RoundEndReason = (RoundEndReason)GameNetworkMessage.ReadIntFromPacket(CompressionMission.RoundEndReasonCompressionInfo, ref result);
			return result;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00007246 File Offset: 0x00005446
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.GameMode;
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00007250 File Offset: 0x00005450
		protected override string OnGetLogFormat()
		{
			return "Change round end reason to: " + this.RoundEndReason.ToString();
		}
	}
}
