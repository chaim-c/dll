using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000067 RID: 103
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SyncPerksForCurrentlySelectedTroop : GameNetworkMessage
	{
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000396 RID: 918 RVA: 0x00006DE0 File Offset: 0x00004FE0
		// (set) Token: 0x06000397 RID: 919 RVA: 0x00006DE8 File Offset: 0x00004FE8
		public NetworkCommunicator Peer { get; private set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000398 RID: 920 RVA: 0x00006DF1 File Offset: 0x00004FF1
		// (set) Token: 0x06000399 RID: 921 RVA: 0x00006DF9 File Offset: 0x00004FF9
		public int[] PerkIndices { get; private set; }

		// Token: 0x0600039A RID: 922 RVA: 0x00006E02 File Offset: 0x00005002
		public SyncPerksForCurrentlySelectedTroop()
		{
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00006E0A File Offset: 0x0000500A
		public SyncPerksForCurrentlySelectedTroop(NetworkCommunicator peer, int[] perkIndices)
		{
			this.Peer = peer;
			this.PerkIndices = perkIndices;
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00006E20 File Offset: 0x00005020
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteNetworkPeerReferenceToPacket(this.Peer);
			for (int i = 0; i < 3; i++)
			{
				GameNetworkMessage.WriteIntToPacket(this.PerkIndices[i], CompressionMission.PerkIndexCompressionInfo);
			}
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00006E58 File Offset: 0x00005058
		protected override bool OnRead()
		{
			bool result = true;
			this.Peer = GameNetworkMessage.ReadNetworkPeerReferenceFromPacket(ref result, false);
			this.PerkIndices = new int[3];
			for (int i = 0; i < 3; i++)
			{
				this.PerkIndices[i] = GameNetworkMessage.ReadIntFromPacket(CompressionMission.PerkIndexCompressionInfo, ref result);
			}
			return result;
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00006EA2 File Offset: 0x000050A2
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.GameMode;
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00006EAC File Offset: 0x000050AC
		protected override string OnGetLogFormat()
		{
			string text = "";
			for (int i = 0; i < 3; i++)
			{
				text += string.Format("[{0}]", this.PerkIndices[i]);
			}
			return string.Concat(new string[]
			{
				"Selected perks for ",
				this.Peer.UserName,
				" has been updated as ",
				text,
				"."
			});
		}
	}
}
