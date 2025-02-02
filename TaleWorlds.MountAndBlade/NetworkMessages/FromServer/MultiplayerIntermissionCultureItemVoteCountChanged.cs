using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000059 RID: 89
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class MultiplayerIntermissionCultureItemVoteCountChanged : GameNetworkMessage
	{
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000324 RID: 804 RVA: 0x00006051 File Offset: 0x00004251
		// (set) Token: 0x06000325 RID: 805 RVA: 0x00006059 File Offset: 0x00004259
		public int CultureItemIndex { get; private set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000326 RID: 806 RVA: 0x00006062 File Offset: 0x00004262
		// (set) Token: 0x06000327 RID: 807 RVA: 0x0000606A File Offset: 0x0000426A
		public int VoteCount { get; private set; }

		// Token: 0x06000328 RID: 808 RVA: 0x00006073 File Offset: 0x00004273
		public MultiplayerIntermissionCultureItemVoteCountChanged()
		{
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000607B File Offset: 0x0000427B
		public MultiplayerIntermissionCultureItemVoteCountChanged(int cultureItemIndex, int voteCount)
		{
			this.CultureItemIndex = cultureItemIndex;
			this.VoteCount = voteCount;
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00006094 File Offset: 0x00004294
		protected override bool OnRead()
		{
			bool result = true;
			this.CultureItemIndex = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.CultureIndexCompressionInfo, ref result);
			this.VoteCount = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.IntermissionVoterCountCompressionInfo, ref result);
			return result;
		}

		// Token: 0x0600032B RID: 811 RVA: 0x000060C8 File Offset: 0x000042C8
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteIntToPacket(this.CultureItemIndex, CompressionBasic.CultureIndexCompressionInfo);
			GameNetworkMessage.WriteIntToPacket(this.VoteCount, CompressionBasic.IntermissionVoterCountCompressionInfo);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x000060EA File Offset: 0x000042EA
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Administration;
		}

		// Token: 0x0600032D RID: 813 RVA: 0x000060F2 File Offset: 0x000042F2
		protected override string OnGetLogFormat()
		{
			return string.Format("Vote count changed for culture with index: {0}, vote count: {1}.", this.CultureItemIndex, this.VoteCount);
		}
	}
}
