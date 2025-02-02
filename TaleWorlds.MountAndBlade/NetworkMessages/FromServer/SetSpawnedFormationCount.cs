using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000B7 RID: 183
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SetSpawnedFormationCount : GameNetworkMessage
	{
		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000758 RID: 1880 RVA: 0x0000C9B2 File Offset: 0x0000ABB2
		// (set) Token: 0x06000759 RID: 1881 RVA: 0x0000C9BA File Offset: 0x0000ABBA
		public int NumOfFormationsTeamOne { get; private set; }

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600075A RID: 1882 RVA: 0x0000C9C3 File Offset: 0x0000ABC3
		// (set) Token: 0x0600075B RID: 1883 RVA: 0x0000C9CB File Offset: 0x0000ABCB
		public int NumOfFormationsTeamTwo { get; private set; }

		// Token: 0x0600075C RID: 1884 RVA: 0x0000C9D4 File Offset: 0x0000ABD4
		public SetSpawnedFormationCount(int numFormationsTeamOne, int numFormationsTeamTwo)
		{
			this.NumOfFormationsTeamOne = numFormationsTeamOne;
			this.NumOfFormationsTeamTwo = numFormationsTeamTwo;
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0000C9EA File Offset: 0x0000ABEA
		public SetSpawnedFormationCount()
		{
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0000C9F4 File Offset: 0x0000ABF4
		protected override bool OnRead()
		{
			bool result = true;
			this.NumOfFormationsTeamOne = GameNetworkMessage.ReadIntFromPacket(CompressionMission.FormationClassCompressionInfo, ref result);
			this.NumOfFormationsTeamTwo = GameNetworkMessage.ReadIntFromPacket(CompressionMission.FormationClassCompressionInfo, ref result);
			return result;
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0000CA28 File Offset: 0x0000AC28
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteIntToPacket(this.NumOfFormationsTeamOne, CompressionMission.FormationClassCompressionInfo);
			GameNetworkMessage.WriteIntToPacket(this.NumOfFormationsTeamTwo, CompressionMission.FormationClassCompressionInfo);
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0000CA4A File Offset: 0x0000AC4A
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Peers;
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0000CA4E File Offset: 0x0000AC4E
		protected override string OnGetLogFormat()
		{
			return "Syncing formation count";
		}
	}
}
