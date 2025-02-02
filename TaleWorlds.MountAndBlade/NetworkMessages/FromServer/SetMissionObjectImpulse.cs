using System;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000AB RID: 171
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SetMissionObjectImpulse : GameNetworkMessage
	{
		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060006DE RID: 1758 RVA: 0x0000BFF0 File Offset: 0x0000A1F0
		// (set) Token: 0x060006DF RID: 1759 RVA: 0x0000BFF8 File Offset: 0x0000A1F8
		public MissionObjectId MissionObjectId { get; private set; }

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x0000C001 File Offset: 0x0000A201
		// (set) Token: 0x060006E1 RID: 1761 RVA: 0x0000C009 File Offset: 0x0000A209
		public Vec3 Position { get; private set; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060006E2 RID: 1762 RVA: 0x0000C012 File Offset: 0x0000A212
		// (set) Token: 0x060006E3 RID: 1763 RVA: 0x0000C01A File Offset: 0x0000A21A
		public Vec3 Impulse { get; private set; }

		// Token: 0x060006E4 RID: 1764 RVA: 0x0000C023 File Offset: 0x0000A223
		public SetMissionObjectImpulse(MissionObjectId missionObjectId, Vec3 position, Vec3 impulse)
		{
			this.MissionObjectId = missionObjectId;
			this.Position = position;
			this.Impulse = impulse;
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x0000C040 File Offset: 0x0000A240
		public SetMissionObjectImpulse()
		{
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x0000C048 File Offset: 0x0000A248
		protected override bool OnRead()
		{
			bool result = true;
			this.MissionObjectId = GameNetworkMessage.ReadMissionObjectIdFromPacket(ref result);
			this.Position = GameNetworkMessage.ReadVec3FromPacket(CompressionBasic.LocalPositionCompressionInfo, ref result);
			this.Impulse = GameNetworkMessage.ReadVec3FromPacket(CompressionBasic.ImpulseCompressionInfo, ref result);
			return result;
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x0000C089 File Offset: 0x0000A289
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteMissionObjectIdToPacket(this.MissionObjectId);
			GameNetworkMessage.WriteVec3ToPacket(this.Position, CompressionBasic.LocalPositionCompressionInfo);
			GameNetworkMessage.WriteVec3ToPacket(this.Impulse, CompressionBasic.ImpulseCompressionInfo);
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x0000C0B6 File Offset: 0x0000A2B6
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.MissionObjects;
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x0000C0BE File Offset: 0x0000A2BE
		protected override string OnGetLogFormat()
		{
			return "Set impulse on MissionObject with ID: " + this.MissionObjectId;
		}
	}
}
