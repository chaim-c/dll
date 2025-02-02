using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000A6 RID: 166
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SetMissionObjectDisabled : GameNetworkMessage
	{
		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060006AA RID: 1706 RVA: 0x0000B9E1 File Offset: 0x00009BE1
		// (set) Token: 0x060006AB RID: 1707 RVA: 0x0000B9E9 File Offset: 0x00009BE9
		public MissionObjectId MissionObjectId { get; private set; }

		// Token: 0x060006AC RID: 1708 RVA: 0x0000B9F2 File Offset: 0x00009BF2
		public SetMissionObjectDisabled(MissionObjectId missionObjectId)
		{
			this.MissionObjectId = missionObjectId;
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0000BA01 File Offset: 0x00009C01
		public SetMissionObjectDisabled()
		{
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0000BA0C File Offset: 0x00009C0C
		protected override bool OnRead()
		{
			bool result = true;
			this.MissionObjectId = GameNetworkMessage.ReadMissionObjectIdFromPacket(ref result);
			return result;
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0000BA29 File Offset: 0x00009C29
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteMissionObjectIdToPacket(this.MissionObjectId);
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x0000BA36 File Offset: 0x00009C36
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.MissionObjects;
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x0000BA3E File Offset: 0x00009C3E
		protected override string OnGetLogFormat()
		{
			return "Mission Object with ID: " + this.MissionObjectId + " has been disabled.";
		}
	}
}
