using System;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;
using TaleWorlds.ObjectSystem;

namespace NetworkMessages.FromClient
{
	// Token: 0x02000009 RID: 9
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class CultureVoteClient : GameNetworkMessage
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000023CD File Offset: 0x000005CD
		// (set) Token: 0x0600002E RID: 46 RVA: 0x000023D5 File Offset: 0x000005D5
		public BasicCultureObject VotedCulture { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000023DE File Offset: 0x000005DE
		// (set) Token: 0x06000030 RID: 48 RVA: 0x000023E6 File Offset: 0x000005E6
		public CultureVoteTypes VotedType { get; private set; }

		// Token: 0x06000031 RID: 49 RVA: 0x000023EF File Offset: 0x000005EF
		public CultureVoteClient()
		{
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000023F7 File Offset: 0x000005F7
		public CultureVoteClient(CultureVoteTypes type, BasicCultureObject culture)
		{
			this.VotedType = type;
			this.VotedCulture = culture;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000240D File Offset: 0x0000060D
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteIntToPacket((int)this.VotedType, CompressionMission.TeamSideCompressionInfo);
			GameNetworkMessage.WriteIntToPacket(MBObjectManager.Instance.GetObjectTypeList<BasicCultureObject>().IndexOf(this.VotedCulture), CompressionBasic.CultureIndexCompressionInfo);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002440 File Offset: 0x00000640
		protected override bool OnRead()
		{
			bool flag = true;
			this.VotedType = (CultureVoteTypes)GameNetworkMessage.ReadIntFromPacket(CompressionMission.TeamSideCompressionInfo, ref flag);
			int index = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.CultureIndexCompressionInfo, ref flag);
			if (flag)
			{
				this.VotedCulture = MBObjectManager.Instance.GetObjectTypeList<BasicCultureObject>()[index];
			}
			return flag;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002488 File Offset: 0x00000688
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Mission;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002490 File Offset: 0x00000690
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
