using System;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;
using TaleWorlds.PlayerServices;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000053 RID: 83
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class InitializeLobbyPeer : GameNetworkMessage
	{
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x0000583C File Offset: 0x00003A3C
		// (set) Token: 0x060002D7 RID: 727 RVA: 0x00005844 File Offset: 0x00003A44
		public NetworkCommunicator Peer { get; private set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x0000584D File Offset: 0x00003A4D
		// (set) Token: 0x060002D9 RID: 729 RVA: 0x00005855 File Offset: 0x00003A55
		public PlayerId ProvidedId { get; private set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060002DA RID: 730 RVA: 0x0000585E File Offset: 0x00003A5E
		// (set) Token: 0x060002DB RID: 731 RVA: 0x00005866 File Offset: 0x00003A66
		public string BannerCode { get; private set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060002DC RID: 732 RVA: 0x0000586F File Offset: 0x00003A6F
		// (set) Token: 0x060002DD RID: 733 RVA: 0x00005877 File Offset: 0x00003A77
		public BodyProperties BodyProperties { get; private set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060002DE RID: 734 RVA: 0x00005880 File Offset: 0x00003A80
		// (set) Token: 0x060002DF RID: 735 RVA: 0x00005888 File Offset: 0x00003A88
		public int ChosenBadgeIndex { get; private set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x00005891 File Offset: 0x00003A91
		// (set) Token: 0x060002E1 RID: 737 RVA: 0x00005899 File Offset: 0x00003A99
		public int ForcedAvatarIndex { get; private set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x000058A2 File Offset: 0x00003AA2
		// (set) Token: 0x060002E3 RID: 739 RVA: 0x000058AA File Offset: 0x00003AAA
		public bool IsFemale { get; private set; }

		// Token: 0x060002E4 RID: 740 RVA: 0x000058B4 File Offset: 0x00003AB4
		public InitializeLobbyPeer(NetworkCommunicator peer, VirtualPlayer virtualPlayer, int forcedAvatarIndex)
		{
			this.Peer = peer;
			this.ProvidedId = virtualPlayer.Id;
			this.BannerCode = ((virtualPlayer.BannerCode != null) ? virtualPlayer.BannerCode : string.Empty);
			this.BodyProperties = virtualPlayer.BodyProperties;
			this.ChosenBadgeIndex = virtualPlayer.ChosenBadgeIndex;
			this.IsFemale = virtualPlayer.IsFemale;
			this.ForcedAvatarIndex = forcedAvatarIndex;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00005920 File Offset: 0x00003B20
		public InitializeLobbyPeer()
		{
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00005928 File Offset: 0x00003B28
		protected override bool OnRead()
		{
			bool flag = true;
			this.Peer = GameNetworkMessage.ReadNetworkPeerReferenceFromPacket(ref flag, false);
			ulong part = GameNetworkMessage.ReadUlongFromPacket(CompressionBasic.DebugULongNonCompressionInfo, ref flag);
			ulong part2 = GameNetworkMessage.ReadUlongFromPacket(CompressionBasic.DebugULongNonCompressionInfo, ref flag);
			ulong part3 = GameNetworkMessage.ReadUlongFromPacket(CompressionBasic.DebugULongNonCompressionInfo, ref flag);
			ulong part4 = GameNetworkMessage.ReadUlongFromPacket(CompressionBasic.DebugULongNonCompressionInfo, ref flag);
			this.BannerCode = GameNetworkMessage.ReadStringFromPacket(ref flag);
			string keyValue = GameNetworkMessage.ReadStringFromPacket(ref flag);
			if (flag)
			{
				this.ProvidedId = new PlayerId(part, part2, part3, part4);
				BodyProperties bodyProperties;
				if (BodyProperties.FromString(keyValue, out bodyProperties))
				{
					this.BodyProperties = bodyProperties;
				}
				else
				{
					flag = false;
				}
			}
			this.ChosenBadgeIndex = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.PlayerChosenBadgeCompressionInfo, ref flag);
			this.ForcedAvatarIndex = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.ForcedAvatarIndexCompressionInfo, ref flag);
			this.IsFemale = GameNetworkMessage.ReadBoolFromPacket(ref flag);
			return flag;
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x000059EC File Offset: 0x00003BEC
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteNetworkPeerReferenceToPacket(this.Peer);
			GameNetworkMessage.WriteUlongToPacket(this.ProvidedId.Part1, CompressionBasic.DebugULongNonCompressionInfo);
			GameNetworkMessage.WriteUlongToPacket(this.ProvidedId.Part2, CompressionBasic.DebugULongNonCompressionInfo);
			GameNetworkMessage.WriteUlongToPacket(this.ProvidedId.Part3, CompressionBasic.DebugULongNonCompressionInfo);
			GameNetworkMessage.WriteUlongToPacket(this.ProvidedId.Part4, CompressionBasic.DebugULongNonCompressionInfo);
			GameNetworkMessage.WriteStringToPacket(this.BannerCode);
			GameNetworkMessage.WriteStringToPacket(this.BodyProperties.ToString());
			GameNetworkMessage.WriteIntToPacket(this.ChosenBadgeIndex, CompressionBasic.PlayerChosenBadgeCompressionInfo);
			GameNetworkMessage.WriteIntToPacket(this.ForcedAvatarIndex, CompressionBasic.ForcedAvatarIndexCompressionInfo);
			GameNetworkMessage.WriteBoolToPacket(this.IsFemale);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00005AB3 File Offset: 0x00003CB3
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.Peers;
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x00005AB7 File Offset: 0x00003CB7
		protected override string OnGetLogFormat()
		{
			return "Initialize LobbyPeer from Peer: " + this.Peer.UserName;
		}
	}
}
