using System;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000A8 RID: 168
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SetMissionObjectFrameOverTime : GameNetworkMessage
	{
		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x0000BB01 File Offset: 0x00009D01
		// (set) Token: 0x060006BD RID: 1725 RVA: 0x0000BB09 File Offset: 0x00009D09
		public MissionObjectId MissionObjectId { get; private set; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x0000BB12 File Offset: 0x00009D12
		// (set) Token: 0x060006BF RID: 1727 RVA: 0x0000BB1A File Offset: 0x00009D1A
		public MatrixFrame Frame { get; private set; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x0000BB23 File Offset: 0x00009D23
		// (set) Token: 0x060006C1 RID: 1729 RVA: 0x0000BB2B File Offset: 0x00009D2B
		public float Duration { get; private set; }

		// Token: 0x060006C2 RID: 1730 RVA: 0x0000BB34 File Offset: 0x00009D34
		public SetMissionObjectFrameOverTime(MissionObjectId missionObjectId, ref MatrixFrame frame, float duration)
		{
			this.MissionObjectId = missionObjectId;
			this.Frame = frame;
			this.Duration = duration;
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x0000BB56 File Offset: 0x00009D56
		public SetMissionObjectFrameOverTime()
		{
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0000BB60 File Offset: 0x00009D60
		protected override bool OnRead()
		{
			bool result = true;
			this.MissionObjectId = GameNetworkMessage.ReadMissionObjectIdFromPacket(ref result);
			this.Frame = GameNetworkMessage.ReadMatrixFrameFromPacket(ref result);
			this.Duration = GameNetworkMessage.ReadFloatFromPacket(CompressionMission.FlagCapturePointDurationCompressionInfo, ref result);
			return result;
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x0000BB9C File Offset: 0x00009D9C
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteMissionObjectIdToPacket(this.MissionObjectId);
			GameNetworkMessage.WriteMatrixFrameToPacket(this.Frame);
			GameNetworkMessage.WriteFloatToPacket(this.Duration, CompressionMission.FlagCapturePointDurationCompressionInfo);
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0000BBC4 File Offset: 0x00009DC4
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.MissionObjectsDetailed;
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0000BBCC File Offset: 0x00009DCC
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Set Move-to-frame on MissionObject with ID: ",
				this.MissionObjectId,
				" over a period of ",
				this.Duration,
				" seconds."
			});
		}
	}
}
