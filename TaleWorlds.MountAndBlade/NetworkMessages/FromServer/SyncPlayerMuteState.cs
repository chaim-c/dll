using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;
using TaleWorlds.PlayerServices;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000068 RID: 104
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SyncPlayerMuteState : GameNetworkMessage
	{
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x00006F1E File Offset: 0x0000511E
		// (set) Token: 0x060003A1 RID: 929 RVA: 0x00006F26 File Offset: 0x00005126
		public PlayerId PlayerId { get; private set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x00006F2F File Offset: 0x0000512F
		// (set) Token: 0x060003A3 RID: 931 RVA: 0x00006F37 File Offset: 0x00005137
		public bool IsMuted { get; private set; }

		// Token: 0x060003A4 RID: 932 RVA: 0x00006F40 File Offset: 0x00005140
		public SyncPlayerMuteState()
		{
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00006F48 File Offset: 0x00005148
		public SyncPlayerMuteState(PlayerId playerId, bool isMuted)
		{
			this.PlayerId = playerId;
			this.IsMuted = isMuted;
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00006F60 File Offset: 0x00005160
		protected override bool OnRead()
		{
			bool flag = true;
			ulong part = GameNetworkMessage.ReadUlongFromPacket(CompressionBasic.DebugULongNonCompressionInfo, ref flag);
			ulong part2 = GameNetworkMessage.ReadUlongFromPacket(CompressionBasic.DebugULongNonCompressionInfo, ref flag);
			ulong part3 = GameNetworkMessage.ReadUlongFromPacket(CompressionBasic.DebugULongNonCompressionInfo, ref flag);
			ulong part4 = GameNetworkMessage.ReadUlongFromPacket(CompressionBasic.DebugULongNonCompressionInfo, ref flag);
			if (flag)
			{
				this.PlayerId = new PlayerId(part, part2, part3, part4);
			}
			this.IsMuted = GameNetworkMessage.ReadBoolFromPacket(ref flag);
			return flag;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00006FC8 File Offset: 0x000051C8
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteUlongToPacket(this.PlayerId.Part1, CompressionBasic.DebugULongNonCompressionInfo);
			GameNetworkMessage.WriteUlongToPacket(this.PlayerId.Part2, CompressionBasic.DebugULongNonCompressionInfo);
			GameNetworkMessage.WriteUlongToPacket(this.PlayerId.Part3, CompressionBasic.DebugULongNonCompressionInfo);
			GameNetworkMessage.WriteUlongToPacket(this.PlayerId.Part4, CompressionBasic.DebugULongNonCompressionInfo);
			GameNetworkMessage.WriteBoolToPacket(this.IsMuted);
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00007040 File Offset: 0x00005240
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Administration;
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00007048 File Offset: 0x00005248
		protected override string OnGetLogFormat()
		{
			return string.Format("SyncPlayerMuteState Player:{0}, IsMuted:{1}", this.PlayerId, this.IsMuted);
		}
	}
}
