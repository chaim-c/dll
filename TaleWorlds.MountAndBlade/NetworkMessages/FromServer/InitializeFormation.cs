using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x0200008E RID: 142
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class InitializeFormation : GameNetworkMessage
	{
		// Token: 0x17000137 RID: 311
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x0000A2FF File Offset: 0x000084FF
		// (set) Token: 0x0600059C RID: 1436 RVA: 0x0000A307 File Offset: 0x00008507
		public int FormationIndex { get; private set; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x0000A310 File Offset: 0x00008510
		// (set) Token: 0x0600059E RID: 1438 RVA: 0x0000A318 File Offset: 0x00008518
		public int TeamIndex { get; private set; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x0000A321 File Offset: 0x00008521
		// (set) Token: 0x060005A0 RID: 1440 RVA: 0x0000A329 File Offset: 0x00008529
		public string BannerCode { get; private set; }

		// Token: 0x060005A1 RID: 1441 RVA: 0x0000A332 File Offset: 0x00008532
		public InitializeFormation(Formation formation, int teamIndex, string bannerCode)
		{
			this.FormationIndex = (int)formation.FormationIndex;
			this.TeamIndex = teamIndex;
			this.BannerCode = ((!string.IsNullOrEmpty(bannerCode)) ? bannerCode : string.Empty);
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0000A363 File Offset: 0x00008563
		public InitializeFormation()
		{
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x0000A36C File Offset: 0x0000856C
		protected override bool OnRead()
		{
			bool result = true;
			this.FormationIndex = GameNetworkMessage.ReadIntFromPacket(CompressionMission.FormationClassCompressionInfo, ref result);
			this.TeamIndex = GameNetworkMessage.ReadTeamIndexFromPacket(ref result);
			this.BannerCode = GameNetworkMessage.ReadStringFromPacket(ref result);
			return result;
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x0000A3A8 File Offset: 0x000085A8
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteIntToPacket(this.FormationIndex, CompressionMission.FormationClassCompressionInfo);
			GameNetworkMessage.WriteTeamIndexToPacket(this.TeamIndex);
			GameNetworkMessage.WriteStringToPacket(this.BannerCode);
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x0000A3D0 File Offset: 0x000085D0
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Peers;
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x0000A3D4 File Offset: 0x000085D4
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Initialize formation with index: ",
				this.FormationIndex,
				", for team: ",
				this.TeamIndex
			});
		}
	}
}
