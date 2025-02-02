using System;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000AA RID: 170
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SetMissionObjectGlobalFrameOverTime : GameNetworkMessage
	{
		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x0000BDCD File Offset: 0x00009FCD
		// (set) Token: 0x060006D3 RID: 1747 RVA: 0x0000BDD5 File Offset: 0x00009FD5
		public MissionObjectId MissionObjectId { get; private set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060006D4 RID: 1748 RVA: 0x0000BDDE File Offset: 0x00009FDE
		// (set) Token: 0x060006D5 RID: 1749 RVA: 0x0000BDE6 File Offset: 0x00009FE6
		public MatrixFrame Frame { get; private set; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x0000BDEF File Offset: 0x00009FEF
		// (set) Token: 0x060006D7 RID: 1751 RVA: 0x0000BDF7 File Offset: 0x00009FF7
		public float Duration { get; private set; }

		// Token: 0x060006D8 RID: 1752 RVA: 0x0000BE00 File Offset: 0x0000A000
		public SetMissionObjectGlobalFrameOverTime(MissionObjectId missionObjectId, ref MatrixFrame frame, float duration)
		{
			this.MissionObjectId = missionObjectId;
			this.Frame = frame;
			this.Duration = duration;
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x0000BE22 File Offset: 0x0000A022
		public SetMissionObjectGlobalFrameOverTime()
		{
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x0000BE2C File Offset: 0x0000A02C
		protected override bool OnRead()
		{
			bool flag = true;
			this.MissionObjectId = GameNetworkMessage.ReadMissionObjectIdFromPacket(ref flag);
			Vec3 s = GameNetworkMessage.ReadVec3FromPacket(CompressionBasic.UnitVectorCompressionInfo, ref flag);
			Vec3 f = GameNetworkMessage.ReadVec3FromPacket(CompressionBasic.UnitVectorCompressionInfo, ref flag);
			Vec3 u = GameNetworkMessage.ReadVec3FromPacket(CompressionBasic.UnitVectorCompressionInfo, ref flag);
			Vec3 scalingVector = GameNetworkMessage.ReadVec3FromPacket(CompressionBasic.ScaleCompressionInfo, ref flag);
			Vec3 o = GameNetworkMessage.ReadVec3FromPacket(CompressionBasic.PositionCompressionInfo, ref flag);
			if (flag)
			{
				this.Frame = new MatrixFrame(new Mat3(s, f, u), o);
				this.Frame.Scale(scalingVector);
			}
			this.Duration = GameNetworkMessage.ReadFloatFromPacket(CompressionMission.FlagCapturePointDurationCompressionInfo, ref flag);
			return flag;
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x0000BEC8 File Offset: 0x0000A0C8
		protected override void OnWrite()
		{
			Vec3 scaleVector = this.Frame.rotation.GetScaleVector();
			MatrixFrame frame = this.Frame;
			frame.Scale(new Vec3(1f / scaleVector.x, 1f / scaleVector.y, 1f / scaleVector.z, -1f));
			GameNetworkMessage.WriteMissionObjectIdToPacket(this.MissionObjectId);
			GameNetworkMessage.WriteVec3ToPacket(frame.rotation.f, CompressionBasic.UnitVectorCompressionInfo);
			GameNetworkMessage.WriteVec3ToPacket(frame.rotation.s, CompressionBasic.UnitVectorCompressionInfo);
			GameNetworkMessage.WriteVec3ToPacket(frame.rotation.u, CompressionBasic.UnitVectorCompressionInfo);
			GameNetworkMessage.WriteVec3ToPacket(scaleVector, CompressionBasic.ScaleCompressionInfo);
			GameNetworkMessage.WriteVec3ToPacket(frame.origin, CompressionBasic.PositionCompressionInfo);
			GameNetworkMessage.WriteFloatToPacket(this.Duration, CompressionMission.FlagCapturePointDurationCompressionInfo);
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x0000BF9A File Offset: 0x0000A19A
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.MissionObjectsDetailed;
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x0000BFA4 File Offset: 0x0000A1A4
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Set Move-to-global-frame on MissionObject with ID: ",
				this.MissionObjectId,
				" over a period of ",
				this.Duration,
				" seconds."
			});
		}
	}
}
