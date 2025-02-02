using System;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000C8 RID: 200
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SyncObjectDestructionLevel : GameNetworkMessage
	{
		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000830 RID: 2096 RVA: 0x0000DDBF File Offset: 0x0000BFBF
		// (set) Token: 0x06000831 RID: 2097 RVA: 0x0000DDC7 File Offset: 0x0000BFC7
		public MissionObjectId MissionObjectId { get; private set; }

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000832 RID: 2098 RVA: 0x0000DDD0 File Offset: 0x0000BFD0
		// (set) Token: 0x06000833 RID: 2099 RVA: 0x0000DDD8 File Offset: 0x0000BFD8
		public int DestructionLevel { get; private set; }

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000834 RID: 2100 RVA: 0x0000DDE1 File Offset: 0x0000BFE1
		// (set) Token: 0x06000835 RID: 2101 RVA: 0x0000DDE9 File Offset: 0x0000BFE9
		public int ForcedIndex { get; private set; }

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000836 RID: 2102 RVA: 0x0000DDF2 File Offset: 0x0000BFF2
		// (set) Token: 0x06000837 RID: 2103 RVA: 0x0000DDFA File Offset: 0x0000BFFA
		public float BlowMagnitude { get; private set; }

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000838 RID: 2104 RVA: 0x0000DE03 File Offset: 0x0000C003
		// (set) Token: 0x06000839 RID: 2105 RVA: 0x0000DE0B File Offset: 0x0000C00B
		public Vec3 BlowPosition { get; private set; }

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x0600083A RID: 2106 RVA: 0x0000DE14 File Offset: 0x0000C014
		// (set) Token: 0x0600083B RID: 2107 RVA: 0x0000DE1C File Offset: 0x0000C01C
		public Vec3 BlowDirection { get; private set; }

		// Token: 0x0600083C RID: 2108 RVA: 0x0000DE25 File Offset: 0x0000C025
		public SyncObjectDestructionLevel(MissionObjectId missionObjectId, int destructionLevel, int forcedIndex, float blowMagnitude, Vec3 blowPosition, Vec3 blowDirection)
		{
			this.MissionObjectId = missionObjectId;
			this.DestructionLevel = destructionLevel;
			this.ForcedIndex = forcedIndex;
			this.BlowMagnitude = blowMagnitude;
			this.BlowPosition = blowPosition;
			this.BlowDirection = blowDirection;
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x0000DE5A File Offset: 0x0000C05A
		public SyncObjectDestructionLevel()
		{
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x0000DE64 File Offset: 0x0000C064
		protected override bool OnRead()
		{
			bool result = true;
			this.MissionObjectId = GameNetworkMessage.ReadMissionObjectIdFromPacket(ref result);
			this.DestructionLevel = GameNetworkMessage.ReadIntFromPacket(CompressionMission.UsableGameObjectDestructionStateCompressionInfo, ref result);
			this.ForcedIndex = (GameNetworkMessage.ReadBoolFromPacket(ref result) ? GameNetworkMessage.ReadIntFromPacket(CompressionBasic.MissionObjectIDCompressionInfo, ref result) : -1);
			this.BlowMagnitude = GameNetworkMessage.ReadFloatFromPacket(CompressionMission.UsableGameObjectBlowMagnitude, ref result);
			this.BlowPosition = GameNetworkMessage.ReadVec3FromPacket(CompressionBasic.PositionCompressionInfo, ref result);
			this.BlowDirection = GameNetworkMessage.ReadVec3FromPacket(CompressionMission.UsableGameObjectBlowDirection, ref result);
			return result;
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x0000DEE8 File Offset: 0x0000C0E8
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteMissionObjectIdToPacket(this.MissionObjectId);
			GameNetworkMessage.WriteIntToPacket(this.DestructionLevel, CompressionMission.UsableGameObjectDestructionStateCompressionInfo);
			GameNetworkMessage.WriteBoolToPacket(this.ForcedIndex != -1);
			if (this.ForcedIndex != -1)
			{
				GameNetworkMessage.WriteIntToPacket(this.ForcedIndex, CompressionBasic.MissionObjectIDCompressionInfo);
			}
			GameNetworkMessage.WriteFloatToPacket(this.BlowMagnitude, CompressionMission.UsableGameObjectBlowMagnitude);
			GameNetworkMessage.WriteVec3ToPacket(this.BlowPosition, CompressionBasic.PositionCompressionInfo);
			GameNetworkMessage.WriteVec3ToPacket(this.BlowDirection, CompressionMission.UsableGameObjectBlowDirection);
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x0000DF6A File Offset: 0x0000C16A
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.MissionObjectsDetailed | MultiplayerMessageFilter.SiegeWeaponsDetailed;
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0000DF74 File Offset: 0x0000C174
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Synchronize DestructionLevel: ",
				this.DestructionLevel,
				" of MissionObject with Id: ",
				this.MissionObjectId,
				(this.ForcedIndex != -1) ? (" (New object will have ID: " + this.ForcedIndex + ")") : ""
			});
		}
	}
}
