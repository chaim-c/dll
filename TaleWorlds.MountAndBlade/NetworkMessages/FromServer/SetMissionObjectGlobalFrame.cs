using System;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000A9 RID: 169
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SetMissionObjectGlobalFrame : GameNetworkMessage
	{
		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x0000BC18 File Offset: 0x00009E18
		// (set) Token: 0x060006C9 RID: 1737 RVA: 0x0000BC20 File Offset: 0x00009E20
		public MissionObjectId MissionObjectId { get; private set; }

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x0000BC29 File Offset: 0x00009E29
		// (set) Token: 0x060006CB RID: 1739 RVA: 0x0000BC31 File Offset: 0x00009E31
		public MatrixFrame Frame { get; private set; }

		// Token: 0x060006CC RID: 1740 RVA: 0x0000BC3A File Offset: 0x00009E3A
		public SetMissionObjectGlobalFrame(MissionObjectId missionObjectId, ref MatrixFrame frame)
		{
			this.MissionObjectId = missionObjectId;
			this.Frame = frame;
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x0000BC55 File Offset: 0x00009E55
		public SetMissionObjectGlobalFrame()
		{
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x0000BC60 File Offset: 0x00009E60
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
			return flag;
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x0000BCEC File Offset: 0x00009EEC
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
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0000BDAE File Offset: 0x00009FAE
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.MissionObjectsDetailed;
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0000BDB6 File Offset: 0x00009FB6
		protected override string OnGetLogFormat()
		{
			return "Set Global Frame on MissionObject with ID: " + this.MissionObjectId;
		}
	}
}
