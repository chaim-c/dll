using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromClient
{
	// Token: 0x02000024 RID: 36
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class ApplyOrderWithMissionObject : GameNetworkMessage
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000122 RID: 290 RVA: 0x000036DA File Offset: 0x000018DA
		// (set) Token: 0x06000123 RID: 291 RVA: 0x000036E2 File Offset: 0x000018E2
		public MissionObjectId MissionObjectId { get; private set; }

		// Token: 0x06000124 RID: 292 RVA: 0x000036EB File Offset: 0x000018EB
		public ApplyOrderWithMissionObject(MissionObjectId missionObjectId)
		{
			this.MissionObjectId = missionObjectId;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000036FA File Offset: 0x000018FA
		public ApplyOrderWithMissionObject()
		{
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00003704 File Offset: 0x00001904
		protected override bool OnRead()
		{
			bool result = true;
			this.MissionObjectId = GameNetworkMessage.ReadMissionObjectIdFromPacket(ref result);
			return result;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00003721 File Offset: 0x00001921
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteMissionObjectIdToPacket(this.MissionObjectId);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000372E File Offset: 0x0000192E
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.MissionObjectsDetailed | MultiplayerMessageFilter.Orders;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00003736 File Offset: 0x00001936
		protected override string OnGetLogFormat()
		{
			return "Apply order to MissionObject with ID: " + this.MissionObjectId + " and with name ";
		}
	}
}
