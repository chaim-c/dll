using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x0200005B RID: 91
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class MultiplayerIntermissionMapItemVoteCountChanged : GameNetworkMessage
	{
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000336 RID: 822 RVA: 0x00006185 File Offset: 0x00004385
		// (set) Token: 0x06000337 RID: 823 RVA: 0x0000618D File Offset: 0x0000438D
		public int MapItemIndex { get; private set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000338 RID: 824 RVA: 0x00006196 File Offset: 0x00004396
		// (set) Token: 0x06000339 RID: 825 RVA: 0x0000619E File Offset: 0x0000439E
		public int VoteCount { get; private set; }

		// Token: 0x0600033A RID: 826 RVA: 0x000061A7 File Offset: 0x000043A7
		public MultiplayerIntermissionMapItemVoteCountChanged()
		{
		}

		// Token: 0x0600033B RID: 827 RVA: 0x000061AF File Offset: 0x000043AF
		public MultiplayerIntermissionMapItemVoteCountChanged(int mapItemIndex, int voteCount)
		{
			this.MapItemIndex = mapItemIndex;
			this.VoteCount = voteCount;
		}

		// Token: 0x0600033C RID: 828 RVA: 0x000061C8 File Offset: 0x000043C8
		protected override bool OnRead()
		{
			bool result = true;
			this.MapItemIndex = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.IntermissionMapVoteItemCountCompressionInfo, ref result);
			this.VoteCount = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.IntermissionVoterCountCompressionInfo, ref result);
			return result;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x000061FC File Offset: 0x000043FC
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteIntToPacket(this.MapItemIndex, CompressionBasic.IntermissionMapVoteItemCountCompressionInfo);
			GameNetworkMessage.WriteIntToPacket(this.VoteCount, CompressionBasic.IntermissionVoterCountCompressionInfo);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000621E File Offset: 0x0000441E
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Administration;
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00006226 File Offset: 0x00004426
		protected override string OnGetLogFormat()
		{
			return string.Format("Vote count changed for map with index: {0}, vote count: {1}.", this.MapItemIndex, this.VoteCount);
		}
	}
}
