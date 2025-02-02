using System;
using System.Collections.Generic;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;
using TaleWorlds.PlayerServices;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000066 RID: 102
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SyncMutedPlayers : GameNetworkMessage
	{
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600038C RID: 908 RVA: 0x00006C3B File Offset: 0x00004E3B
		// (set) Token: 0x0600038D RID: 909 RVA: 0x00006C43 File Offset: 0x00004E43
		public int MutedPlayerCount { get; private set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600038E RID: 910 RVA: 0x00006C4C File Offset: 0x00004E4C
		// (set) Token: 0x0600038F RID: 911 RVA: 0x00006C54 File Offset: 0x00004E54
		public List<PlayerId> MutedPlayerIds { get; private set; }

		// Token: 0x06000390 RID: 912 RVA: 0x00006C5D File Offset: 0x00004E5D
		public SyncMutedPlayers()
		{
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00006C65 File Offset: 0x00004E65
		public SyncMutedPlayers(List<PlayerId> mutedPlayerIds)
		{
			this.MutedPlayerIds = mutedPlayerIds;
			List<PlayerId> mutedPlayerIds2 = this.MutedPlayerIds;
			this.MutedPlayerCount = ((mutedPlayerIds2 != null) ? mutedPlayerIds2.Count : 0);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00006C8C File Offset: 0x00004E8C
		protected override bool OnRead()
		{
			bool result = true;
			this.MutedPlayerIds = new List<PlayerId>();
			this.MutedPlayerCount = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.IntermissionVoterCountCompressionInfo, ref result);
			for (int i = 0; i < this.MutedPlayerCount; i++)
			{
				ulong part = GameNetworkMessage.ReadUlongFromPacket(CompressionBasic.DebugULongNonCompressionInfo, ref result);
				ulong part2 = GameNetworkMessage.ReadUlongFromPacket(CompressionBasic.DebugULongNonCompressionInfo, ref result);
				ulong part3 = GameNetworkMessage.ReadUlongFromPacket(CompressionBasic.DebugULongNonCompressionInfo, ref result);
				ulong part4 = GameNetworkMessage.ReadUlongFromPacket(CompressionBasic.DebugULongNonCompressionInfo, ref result);
				this.MutedPlayerIds.Add(new PlayerId(part, part2, part3, part4));
			}
			return result;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00006D18 File Offset: 0x00004F18
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteIntToPacket(this.MutedPlayerCount, CompressionBasic.IntermissionVoterCountCompressionInfo);
			for (int i = 0; i < this.MutedPlayerCount; i++)
			{
				GameNetworkMessage.WriteUlongToPacket(this.MutedPlayerIds[i].Part1, CompressionBasic.DebugULongNonCompressionInfo);
				GameNetworkMessage.WriteUlongToPacket(this.MutedPlayerIds[i].Part2, CompressionBasic.DebugULongNonCompressionInfo);
				GameNetworkMessage.WriteUlongToPacket(this.MutedPlayerIds[i].Part3, CompressionBasic.DebugULongNonCompressionInfo);
				GameNetworkMessage.WriteUlongToPacket(this.MutedPlayerIds[i].Part4, CompressionBasic.DebugULongNonCompressionInfo);
			}
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00006DC1 File Offset: 0x00004FC1
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Administration;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00006DC9 File Offset: 0x00004FC9
		protected override string OnGetLogFormat()
		{
			return string.Format("SyncMutedPlayers {0} muted players.", this.MutedPlayerCount);
		}
	}
}
