using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x0200007A RID: 122
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class BurstAllHeavyHitParticles : GameNetworkMessage
	{
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x000082A5 File Offset: 0x000064A5
		// (set) Token: 0x0600046E RID: 1134 RVA: 0x000082AD File Offset: 0x000064AD
		public MissionObjectId MissionObjectId { get; private set; }

		// Token: 0x0600046F RID: 1135 RVA: 0x000082B6 File Offset: 0x000064B6
		public BurstAllHeavyHitParticles(MissionObjectId missionObjectId)
		{
			this.MissionObjectId = missionObjectId;
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x000082C5 File Offset: 0x000064C5
		public BurstAllHeavyHitParticles()
		{
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x000082D0 File Offset: 0x000064D0
		protected override bool OnRead()
		{
			bool result = true;
			this.MissionObjectId = GameNetworkMessage.ReadMissionObjectIdFromPacket(ref result);
			return result;
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x000082ED File Offset: 0x000064ED
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteMissionObjectIdToPacket(this.MissionObjectId);
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x000082FA File Offset: 0x000064FA
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.MissionObjectsDetailed | MultiplayerMessageFilter.SiegeWeaponsDetailed;
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00008302 File Offset: 0x00006502
		protected override string OnGetLogFormat()
		{
			return "Bursting all heavy-hit particles for the DestructableComponent of MissionObject with Id: " + this.MissionObjectId;
		}
	}
}
