using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000CA RID: 202
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class TeamSetIsEnemyOf : GameNetworkMessage
	{
		// Token: 0x170001DB RID: 475
		// (get) Token: 0x0600084C RID: 2124 RVA: 0x0000E0BF File Offset: 0x0000C2BF
		// (set) Token: 0x0600084D RID: 2125 RVA: 0x0000E0C7 File Offset: 0x0000C2C7
		public int Team1Index { get; private set; }

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x0600084E RID: 2126 RVA: 0x0000E0D0 File Offset: 0x0000C2D0
		// (set) Token: 0x0600084F RID: 2127 RVA: 0x0000E0D8 File Offset: 0x0000C2D8
		public int Team2Index { get; private set; }

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000850 RID: 2128 RVA: 0x0000E0E1 File Offset: 0x0000C2E1
		// (set) Token: 0x06000851 RID: 2129 RVA: 0x0000E0E9 File Offset: 0x0000C2E9
		public bool IsEnemyOf { get; private set; }

		// Token: 0x06000852 RID: 2130 RVA: 0x0000E0F2 File Offset: 0x0000C2F2
		public TeamSetIsEnemyOf(int team1Index, int team2Index, bool isEnemyOf)
		{
			this.Team1Index = team1Index;
			this.Team2Index = team2Index;
			this.IsEnemyOf = isEnemyOf;
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x0000E10F File Offset: 0x0000C30F
		public TeamSetIsEnemyOf()
		{
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0000E117 File Offset: 0x0000C317
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteTeamIndexToPacket(this.Team1Index);
			GameNetworkMessage.WriteTeamIndexToPacket(this.Team2Index);
			GameNetworkMessage.WriteBoolToPacket(this.IsEnemyOf);
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0000E13C File Offset: 0x0000C33C
		protected override bool OnRead()
		{
			bool result = true;
			this.Team1Index = GameNetworkMessage.ReadTeamIndexFromPacket(ref result);
			this.Team2Index = GameNetworkMessage.ReadTeamIndexFromPacket(ref result);
			this.IsEnemyOf = GameNetworkMessage.ReadBoolFromPacket(ref result);
			return result;
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x0000E173 File Offset: 0x0000C373
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Mission;
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0000E17C File Offset: 0x0000C37C
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				this.Team1Index,
				" is now ",
				this.IsEnemyOf ? "" : "not an ",
				"enemy of ",
				this.Team2Index
			});
		}
	}
}
