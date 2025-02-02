using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000A2 RID: 162
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SetMissionObjectAnimationAtChannel : GameNetworkMessage
	{
		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x0000B549 File Offset: 0x00009749
		// (set) Token: 0x0600067B RID: 1659 RVA: 0x0000B551 File Offset: 0x00009751
		public MissionObjectId MissionObjectId { get; private set; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x0000B55A File Offset: 0x0000975A
		// (set) Token: 0x0600067D RID: 1661 RVA: 0x0000B562 File Offset: 0x00009762
		public int ChannelNo { get; private set; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x0000B56B File Offset: 0x0000976B
		// (set) Token: 0x0600067F RID: 1663 RVA: 0x0000B573 File Offset: 0x00009773
		public int AnimationIndex { get; private set; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x0000B57C File Offset: 0x0000977C
		// (set) Token: 0x06000681 RID: 1665 RVA: 0x0000B584 File Offset: 0x00009784
		public float AnimationSpeed { get; private set; }

		// Token: 0x06000682 RID: 1666 RVA: 0x0000B58D File Offset: 0x0000978D
		public SetMissionObjectAnimationAtChannel(MissionObjectId missionObjectId, int channelNo, int animationIndex, float animationSpeed)
		{
			this.MissionObjectId = missionObjectId;
			this.ChannelNo = channelNo;
			this.AnimationIndex = animationIndex;
			this.AnimationSpeed = animationSpeed;
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0000B5B2 File Offset: 0x000097B2
		public SetMissionObjectAnimationAtChannel()
		{
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0000B5BC File Offset: 0x000097BC
		protected override bool OnRead()
		{
			bool result = true;
			this.MissionObjectId = GameNetworkMessage.ReadMissionObjectIdFromPacket(ref result);
			this.ChannelNo = (GameNetworkMessage.ReadBoolFromPacket(ref result) ? 1 : 0);
			this.AnimationIndex = GameNetworkMessage.ReadIntFromPacket(CompressionBasic.AnimationIndexCompressionInfo, ref result);
			this.AnimationSpeed = (GameNetworkMessage.ReadBoolFromPacket(ref result) ? GameNetworkMessage.ReadFloatFromPacket(CompressionBasic.AnimationSpeedCompressionInfo, ref result) : 1f);
			return result;
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0000B624 File Offset: 0x00009824
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteMissionObjectIdToPacket(this.MissionObjectId);
			GameNetworkMessage.WriteBoolToPacket(this.ChannelNo == 1);
			GameNetworkMessage.WriteIntToPacket(this.AnimationIndex, CompressionBasic.AnimationIndexCompressionInfo);
			GameNetworkMessage.WriteBoolToPacket(this.AnimationSpeed != 1f);
			if (this.AnimationSpeed != 1f)
			{
				GameNetworkMessage.WriteFloatToPacket(this.AnimationSpeed, CompressionBasic.AnimationSpeedCompressionInfo);
			}
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0000B68C File Offset: 0x0000988C
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.MissionObjectsDetailed;
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0000B694 File Offset: 0x00009894
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Set animation: ",
				this.AnimationIndex,
				" on channel: ",
				this.ChannelNo,
				" of MissionObject with ID: ",
				this.MissionObjectId
			});
		}
	}
}
