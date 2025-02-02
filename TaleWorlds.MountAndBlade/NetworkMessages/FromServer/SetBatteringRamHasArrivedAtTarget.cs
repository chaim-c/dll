using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x0200009F RID: 159
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SetBatteringRamHasArrivedAtTarget : GameNetworkMessage
	{
		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x0000B322 File Offset: 0x00009522
		// (set) Token: 0x0600065C RID: 1628 RVA: 0x0000B32A File Offset: 0x0000952A
		public MissionObjectId BatteringRamId { get; private set; }

		// Token: 0x0600065D RID: 1629 RVA: 0x0000B333 File Offset: 0x00009533
		public SetBatteringRamHasArrivedAtTarget(MissionObjectId batteringRamId)
		{
			this.BatteringRamId = batteringRamId;
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x0000B342 File Offset: 0x00009542
		public SetBatteringRamHasArrivedAtTarget()
		{
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x0000B34C File Offset: 0x0000954C
		protected override bool OnRead()
		{
			bool result = true;
			this.BatteringRamId = GameNetworkMessage.ReadMissionObjectIdFromPacket(ref result);
			return result;
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x0000B369 File Offset: 0x00009569
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteMissionObjectIdToPacket(this.BatteringRamId);
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x0000B376 File Offset: 0x00009576
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.SiegeWeaponsDetailed;
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x0000B37E File Offset: 0x0000957E
		protected override string OnGetLogFormat()
		{
			return "Battering Ram with ID: " + this.BatteringRamId + " has arrived at its target.";
		}
	}
}
