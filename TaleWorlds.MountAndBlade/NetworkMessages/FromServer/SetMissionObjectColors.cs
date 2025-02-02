using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000A5 RID: 165
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SetMissionObjectColors : GameNetworkMessage
	{
		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600069E RID: 1694 RVA: 0x0000B8F9 File Offset: 0x00009AF9
		// (set) Token: 0x0600069F RID: 1695 RVA: 0x0000B901 File Offset: 0x00009B01
		public MissionObjectId MissionObjectId { get; private set; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x0000B90A File Offset: 0x00009B0A
		// (set) Token: 0x060006A1 RID: 1697 RVA: 0x0000B912 File Offset: 0x00009B12
		public uint Color { get; private set; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x0000B91B File Offset: 0x00009B1B
		// (set) Token: 0x060006A3 RID: 1699 RVA: 0x0000B923 File Offset: 0x00009B23
		public uint Color2 { get; private set; }

		// Token: 0x060006A4 RID: 1700 RVA: 0x0000B92C File Offset: 0x00009B2C
		public SetMissionObjectColors(MissionObjectId missionObjectId, uint color, uint color2)
		{
			this.MissionObjectId = missionObjectId;
			this.Color = color;
			this.Color2 = color2;
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0000B949 File Offset: 0x00009B49
		public SetMissionObjectColors()
		{
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0000B954 File Offset: 0x00009B54
		protected override bool OnRead()
		{
			bool result = true;
			this.MissionObjectId = GameNetworkMessage.ReadMissionObjectIdFromPacket(ref result);
			this.Color = GameNetworkMessage.ReadUintFromPacket(CompressionBasic.ColorCompressionInfo, ref result);
			this.Color2 = GameNetworkMessage.ReadUintFromPacket(CompressionBasic.ColorCompressionInfo, ref result);
			return result;
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0000B995 File Offset: 0x00009B95
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteMissionObjectIdToPacket(this.MissionObjectId);
			GameNetworkMessage.WriteUintToPacket(this.Color, CompressionBasic.ColorCompressionInfo);
			GameNetworkMessage.WriteUintToPacket(this.Color2, CompressionBasic.ColorCompressionInfo);
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0000B9C2 File Offset: 0x00009BC2
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.MissionObjects;
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x0000B9CA File Offset: 0x00009BCA
		protected override string OnGetLogFormat()
		{
			return "Set Colors of MissionObject with ID: " + this.MissionObjectId;
		}
	}
}
