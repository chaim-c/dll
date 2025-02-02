using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000AC RID: 172
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SetMissionObjectVertexAnimation : GameNetworkMessage
	{
		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060006EA RID: 1770 RVA: 0x0000C0D5 File Offset: 0x0000A2D5
		// (set) Token: 0x060006EB RID: 1771 RVA: 0x0000C0DD File Offset: 0x0000A2DD
		public MissionObjectId MissionObjectId { get; private set; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060006EC RID: 1772 RVA: 0x0000C0E6 File Offset: 0x0000A2E6
		// (set) Token: 0x060006ED RID: 1773 RVA: 0x0000C0EE File Offset: 0x0000A2EE
		public int BeginKey { get; private set; }

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060006EE RID: 1774 RVA: 0x0000C0F7 File Offset: 0x0000A2F7
		// (set) Token: 0x060006EF RID: 1775 RVA: 0x0000C0FF File Offset: 0x0000A2FF
		public int EndKey { get; private set; }

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x0000C108 File Offset: 0x0000A308
		// (set) Token: 0x060006F1 RID: 1777 RVA: 0x0000C110 File Offset: 0x0000A310
		public float Speed { get; private set; }

		// Token: 0x060006F2 RID: 1778 RVA: 0x0000C119 File Offset: 0x0000A319
		public SetMissionObjectVertexAnimation(MissionObjectId missionObjectId, int beginKey, int endKey, float speed)
		{
			this.MissionObjectId = missionObjectId;
			this.BeginKey = beginKey;
			this.EndKey = endKey;
			this.Speed = speed;
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x0000C13E File Offset: 0x0000A33E
		public SetMissionObjectVertexAnimation()
		{
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0000C148 File Offset: 0x0000A348
		protected override bool OnRead()
		{
			bool result = true;
			this.MissionObjectId = GameNetworkMessage.ReadMissionObjectIdFromPacket(ref result);
			this.BeginKey = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.AnimationKeyCompressionInfo, ref result);
			this.EndKey = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.AnimationKeyCompressionInfo, ref result);
			this.Speed = GameNetworkMessage.ReadFloatFromPacket(CompressionBasic.VertexAnimationSpeedCompressionInfo, ref result);
			return result;
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0000C19B File Offset: 0x0000A39B
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteMissionObjectIdToPacket(this.MissionObjectId);
			GameNetworkMessage.WriteIntToPacket(this.BeginKey, CompressionBasic.AnimationKeyCompressionInfo);
			GameNetworkMessage.WriteIntToPacket(this.EndKey, CompressionBasic.AnimationKeyCompressionInfo);
			GameNetworkMessage.WriteFloatToPacket(this.Speed, CompressionBasic.VertexAnimationSpeedCompressionInfo);
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0000C1D8 File Offset: 0x0000A3D8
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.MissionObjectsDetailed;
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x0000C1E0 File Offset: 0x0000A3E0
		protected override string OnGetLogFormat()
		{
			return "Set Vertex Animation on MissionObject with ID: " + this.MissionObjectId;
		}
	}
}
