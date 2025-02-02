using System;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;
using TaleWorlds.ObjectSystem;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000043 RID: 67
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class CultureVoteServer : GameNetworkMessage
	{
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000228 RID: 552 RVA: 0x00004791 File Offset: 0x00002991
		// (set) Token: 0x06000229 RID: 553 RVA: 0x00004799 File Offset: 0x00002999
		public NetworkCommunicator Peer { get; private set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600022A RID: 554 RVA: 0x000047A2 File Offset: 0x000029A2
		// (set) Token: 0x0600022B RID: 555 RVA: 0x000047AA File Offset: 0x000029AA
		public BasicCultureObject VotedCulture { get; private set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600022C RID: 556 RVA: 0x000047B3 File Offset: 0x000029B3
		// (set) Token: 0x0600022D RID: 557 RVA: 0x000047BB File Offset: 0x000029BB
		public CultureVoteTypes VotedType { get; private set; }

		// Token: 0x0600022E RID: 558 RVA: 0x000047C4 File Offset: 0x000029C4
		public CultureVoteServer()
		{
		}

		// Token: 0x0600022F RID: 559 RVA: 0x000047CC File Offset: 0x000029CC
		public CultureVoteServer(NetworkCommunicator peer, CultureVoteTypes type, BasicCultureObject culture)
		{
			this.Peer = peer;
			this.VotedType = type;
			this.VotedCulture = culture;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x000047EC File Offset: 0x000029EC
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteNetworkPeerReferenceToPacket(this.Peer);
			GameNetworkMessage.WriteIntToPacket((int)this.VotedType, CompressionMission.TeamSideCompressionInfo);
			MBReadOnlyList<BasicCultureObject> objectTypeList = MBObjectManager.Instance.GetObjectTypeList<BasicCultureObject>();
			GameNetworkMessage.WriteIntToPacket((this.VotedCulture == null) ? -1 : objectTypeList.IndexOf(this.VotedCulture), CompressionBasic.CultureIndexCompressionInfo);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00004840 File Offset: 0x00002A40
		protected override bool OnRead()
		{
			bool flag = true;
			this.Peer = GameNetworkMessage.ReadNetworkPeerReferenceFromPacket(ref flag, false);
			this.VotedType = (CultureVoteTypes)GameNetworkMessage.ReadIntFromPacket(CompressionMission.TeamSideCompressionInfo, ref flag);
			int num = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.CultureIndexCompressionInfo, ref flag);
			if (flag)
			{
				MBReadOnlyList<BasicCultureObject> objectTypeList = MBObjectManager.Instance.GetObjectTypeList<BasicCultureObject>();
				this.VotedCulture = ((num < 0) ? null : objectTypeList[num]);
			}
			return flag;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000489F File Offset: 0x00002A9F
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Mission;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x000048A8 File Offset: 0x00002AA8
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Culture ",
				this.VotedCulture.Name,
				" has been ",
				this.VotedType.ToString().ToLower(),
				(this.VotedType == CultureVoteTypes.Ban) ? "ned." : "ed."
			});
		}
	}
}
