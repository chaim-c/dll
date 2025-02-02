using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000CC RID: 204
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class UpdateRoundScores : GameNetworkMessage
	{
		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000860 RID: 2144 RVA: 0x0000E239 File Offset: 0x0000C439
		// (set) Token: 0x06000861 RID: 2145 RVA: 0x0000E241 File Offset: 0x0000C441
		public int AttackerTeamScore { get; private set; }

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000862 RID: 2146 RVA: 0x0000E24A File Offset: 0x0000C44A
		// (set) Token: 0x06000863 RID: 2147 RVA: 0x0000E252 File Offset: 0x0000C452
		public int DefenderTeamScore { get; private set; }

		// Token: 0x06000864 RID: 2148 RVA: 0x0000E25B File Offset: 0x0000C45B
		public UpdateRoundScores(int attackerTeamScore, int defenderTeamScore)
		{
			this.AttackerTeamScore = attackerTeamScore;
			this.DefenderTeamScore = defenderTeamScore;
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0000E271 File Offset: 0x0000C471
		public UpdateRoundScores()
		{
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0000E27C File Offset: 0x0000C47C
		protected override bool OnRead()
		{
			bool result = true;
			this.AttackerTeamScore = GameNetworkMessage.ReadIntFromPacket(CompressionMission.TeamScoreCompressionInfo, ref result);
			this.DefenderTeamScore = GameNetworkMessage.ReadIntFromPacket(CompressionMission.TeamScoreCompressionInfo, ref result);
			return result;
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0000E2B0 File Offset: 0x0000C4B0
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteIntToPacket(this.AttackerTeamScore, CompressionMission.TeamScoreCompressionInfo);
			GameNetworkMessage.WriteIntToPacket(this.DefenderTeamScore, CompressionMission.TeamScoreCompressionInfo);
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0000E2D2 File Offset: 0x0000C4D2
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Mission | MultiplayerMessageFilter.GameMode;
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x0000E2DA File Offset: 0x0000C4DA
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Update round score. Attackers: ",
				this.AttackerTeamScore,
				", defenders: ",
				this.DefenderTeamScore
			});
		}
	}
}
