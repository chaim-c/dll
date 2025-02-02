using System;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000A7 RID: 167
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SetMissionObjectFrame : GameNetworkMessage
	{
		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x0000BA5A File Offset: 0x00009C5A
		// (set) Token: 0x060006B3 RID: 1715 RVA: 0x0000BA62 File Offset: 0x00009C62
		public MissionObjectId MissionObjectId { get; private set; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x0000BA6B File Offset: 0x00009C6B
		// (set) Token: 0x060006B5 RID: 1717 RVA: 0x0000BA73 File Offset: 0x00009C73
		public MatrixFrame Frame { get; private set; }

		// Token: 0x060006B6 RID: 1718 RVA: 0x0000BA7C File Offset: 0x00009C7C
		public SetMissionObjectFrame(MissionObjectId missionObjectId, ref MatrixFrame frame)
		{
			this.MissionObjectId = missionObjectId;
			this.Frame = frame;
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x0000BA97 File Offset: 0x00009C97
		public SetMissionObjectFrame()
		{
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0000BAA0 File Offset: 0x00009CA0
		protected override bool OnRead()
		{
			bool result = true;
			this.MissionObjectId = GameNetworkMessage.ReadMissionObjectIdFromPacket(ref result);
			this.Frame = GameNetworkMessage.ReadMatrixFrameFromPacket(ref result);
			return result;
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0000BACA File Offset: 0x00009CCA
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteMissionObjectIdToPacket(this.MissionObjectId);
			GameNetworkMessage.WriteMatrixFrameToPacket(this.Frame);
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0000BAE2 File Offset: 0x00009CE2
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.MissionObjectsDetailed;
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0000BAEA File Offset: 0x00009CEA
		protected override string OnGetLogFormat()
		{
			return "Set Frame on MissionObject with ID: " + this.MissionObjectId;
		}
	}
}
