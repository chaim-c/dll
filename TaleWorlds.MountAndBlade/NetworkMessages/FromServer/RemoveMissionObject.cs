using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000095 RID: 149
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class RemoveMissionObject : GameNetworkMessage
	{
		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x0000A8B0 File Offset: 0x00008AB0
		// (set) Token: 0x060005E6 RID: 1510 RVA: 0x0000A8B8 File Offset: 0x00008AB8
		public MissionObjectId ObjectId { get; private set; }

		// Token: 0x060005E7 RID: 1511 RVA: 0x0000A8C1 File Offset: 0x00008AC1
		public RemoveMissionObject(MissionObjectId objectId)
		{
			this.ObjectId = objectId;
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0000A8D0 File Offset: 0x00008AD0
		public RemoveMissionObject()
		{
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0000A8D8 File Offset: 0x00008AD8
		protected override bool OnRead()
		{
			bool result = true;
			this.ObjectId = GameNetworkMessage.ReadMissionObjectIdFromPacket(ref result);
			return result;
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0000A8F5 File Offset: 0x00008AF5
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteMissionObjectIdToPacket(this.ObjectId);
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0000A902 File Offset: 0x00008B02
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.MissionObjects;
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x0000A90A File Offset: 0x00008B0A
		protected override string OnGetLogFormat()
		{
			return "Remove MissionObject with ID: " + this.ObjectId;
		}
	}
}
