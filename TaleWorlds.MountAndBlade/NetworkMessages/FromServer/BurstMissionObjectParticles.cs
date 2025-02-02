using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x0200007B RID: 123
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class BurstMissionObjectParticles : GameNetworkMessage
	{
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x00008319 File Offset: 0x00006519
		// (set) Token: 0x06000476 RID: 1142 RVA: 0x00008321 File Offset: 0x00006521
		public MissionObjectId MissionObjectId { get; private set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x0000832A File Offset: 0x0000652A
		// (set) Token: 0x06000478 RID: 1144 RVA: 0x00008332 File Offset: 0x00006532
		public bool DoChildren { get; private set; }

		// Token: 0x06000479 RID: 1145 RVA: 0x0000833B File Offset: 0x0000653B
		public BurstMissionObjectParticles(MissionObjectId missionObjectId, bool doChildren)
		{
			this.MissionObjectId = missionObjectId;
			this.DoChildren = doChildren;
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00008351 File Offset: 0x00006551
		public BurstMissionObjectParticles()
		{
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0000835C File Offset: 0x0000655C
		protected override bool OnRead()
		{
			bool result = true;
			this.MissionObjectId = GameNetworkMessage.ReadMissionObjectIdFromPacket(ref result);
			this.DoChildren = GameNetworkMessage.ReadBoolFromPacket(ref result);
			return result;
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00008386 File Offset: 0x00006586
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteMissionObjectIdToPacket(this.MissionObjectId);
			GameNetworkMessage.WriteBoolToPacket(this.DoChildren);
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0000839E File Offset: 0x0000659E
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.MissionObjectsDetailed | MultiplayerMessageFilter.Particles;
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x000083A6 File Offset: 0x000065A6
		protected override string OnGetLogFormat()
		{
			return "Burst MissionObject particles on MissionObject with ID: " + this.MissionObjectId;
		}
	}
}
