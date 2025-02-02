using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x0200006E RID: 110
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SiegeMoraleChangeMessage : GameNetworkMessage
	{
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x0000740B File Offset: 0x0000560B
		// (set) Token: 0x060003DA RID: 986 RVA: 0x00007413 File Offset: 0x00005613
		public int AttackerMorale { get; private set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060003DB RID: 987 RVA: 0x0000741C File Offset: 0x0000561C
		// (set) Token: 0x060003DC RID: 988 RVA: 0x00007424 File Offset: 0x00005624
		public int DefenderMorale { get; private set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060003DD RID: 989 RVA: 0x0000742D File Offset: 0x0000562D
		// (set) Token: 0x060003DE RID: 990 RVA: 0x00007435 File Offset: 0x00005635
		public int[] CapturePointRemainingMoraleGains { get; private set; }

		// Token: 0x060003DF RID: 991 RVA: 0x0000743E File Offset: 0x0000563E
		public SiegeMoraleChangeMessage()
		{
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00007446 File Offset: 0x00005646
		public SiegeMoraleChangeMessage(int attackerMorale, int defenderMorale, int[] capturePointRemainingMoraleGains)
		{
			this.AttackerMorale = attackerMorale;
			this.DefenderMorale = defenderMorale;
			this.CapturePointRemainingMoraleGains = capturePointRemainingMoraleGains;
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00007464 File Offset: 0x00005664
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteIntToPacket(this.AttackerMorale, CompressionMission.SiegeMoraleCompressionInfo);
			GameNetworkMessage.WriteIntToPacket(this.DefenderMorale, CompressionMission.SiegeMoraleCompressionInfo);
			int[] capturePointRemainingMoraleGains = this.CapturePointRemainingMoraleGains;
			for (int i = 0; i < capturePointRemainingMoraleGains.Length; i++)
			{
				GameNetworkMessage.WriteIntToPacket(capturePointRemainingMoraleGains[i], CompressionMission.SiegeMoralePerFlagCompressionInfo);
			}
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x000074B4 File Offset: 0x000056B4
		protected override bool OnRead()
		{
			bool result = true;
			this.AttackerMorale = GameNetworkMessage.ReadIntFromPacket(CompressionMission.SiegeMoraleCompressionInfo, ref result);
			this.DefenderMorale = GameNetworkMessage.ReadIntFromPacket(CompressionMission.SiegeMoraleCompressionInfo, ref result);
			this.CapturePointRemainingMoraleGains = new int[7];
			for (int i = 0; i < this.CapturePointRemainingMoraleGains.Length; i++)
			{
				this.CapturePointRemainingMoraleGains[i] = GameNetworkMessage.ReadIntFromPacket(CompressionMission.SiegeMoralePerFlagCompressionInfo, ref result);
			}
			return result;
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000751B File Offset: 0x0000571B
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.GameMode;
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00007523 File Offset: 0x00005723
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Morale synched. A: ",
				this.AttackerMorale,
				" D: ",
				this.DefenderMorale
			});
		}
	}
}
