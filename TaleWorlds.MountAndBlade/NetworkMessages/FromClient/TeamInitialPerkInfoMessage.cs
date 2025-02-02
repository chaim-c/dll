using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromClient
{
	// Token: 0x02000010 RID: 16
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class TeamInitialPerkInfoMessage : GameNetworkMessage
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002858 File Offset: 0x00000A58
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00002860 File Offset: 0x00000A60
		public int[] Perks { get; private set; }

		// Token: 0x0600006D RID: 109 RVA: 0x00002869 File Offset: 0x00000A69
		public TeamInitialPerkInfoMessage(int[] perks)
		{
			this.Perks = perks;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002878 File Offset: 0x00000A78
		public TeamInitialPerkInfoMessage()
		{
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002880 File Offset: 0x00000A80
		protected override bool OnRead()
		{
			bool result = true;
			this.Perks = new int[3];
			for (int i = 0; i < 3; i++)
			{
				this.Perks[i] = GameNetworkMessage.ReadIntFromPacket(CompressionMission.PerkIndexCompressionInfo, ref result);
			}
			return result;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000028BC File Offset: 0x00000ABC
		protected override void OnWrite()
		{
			for (int i = 0; i < 3; i++)
			{
				GameNetworkMessage.WriteIntToPacket(this.Perks[i], CompressionMission.PerkIndexCompressionInfo);
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000028E7 File Offset: 0x00000AE7
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Equipment;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000028EC File Offset: 0x00000AEC
		protected override string OnGetLogFormat()
		{
			return "TeamInitialPerkInfoMessage";
		}
	}
}
