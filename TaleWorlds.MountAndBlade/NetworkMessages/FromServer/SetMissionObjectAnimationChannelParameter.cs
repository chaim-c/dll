using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000A3 RID: 163
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SetMissionObjectAnimationChannelParameter : GameNetworkMessage
	{
		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000688 RID: 1672 RVA: 0x0000B6EE File Offset: 0x000098EE
		// (set) Token: 0x06000689 RID: 1673 RVA: 0x0000B6F6 File Offset: 0x000098F6
		public MissionObjectId MissionObjectId { get; private set; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x0000B6FF File Offset: 0x000098FF
		// (set) Token: 0x0600068B RID: 1675 RVA: 0x0000B707 File Offset: 0x00009907
		public int ChannelNo { get; private set; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x0000B710 File Offset: 0x00009910
		// (set) Token: 0x0600068D RID: 1677 RVA: 0x0000B718 File Offset: 0x00009918
		public float Parameter { get; private set; }

		// Token: 0x0600068E RID: 1678 RVA: 0x0000B721 File Offset: 0x00009921
		public SetMissionObjectAnimationChannelParameter(MissionObjectId missionObjectId, int channelNo, float parameter)
		{
			this.MissionObjectId = missionObjectId;
			this.ChannelNo = channelNo;
			this.Parameter = parameter;
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0000B73E File Offset: 0x0000993E
		public SetMissionObjectAnimationChannelParameter()
		{
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0000B748 File Offset: 0x00009948
		protected override bool OnRead()
		{
			bool flag = true;
			this.MissionObjectId = GameNetworkMessage.ReadMissionObjectIdFromPacket(ref flag);
			bool flag2 = GameNetworkMessage.ReadBoolFromPacket(ref flag);
			if (flag)
			{
				this.ChannelNo = (flag2 ? 1 : 0);
			}
			this.Parameter = GameNetworkMessage.ReadFloatFromPacket(CompressionBasic.AnimationProgressCompressionInfo, ref flag);
			return flag;
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0000B78F File Offset: 0x0000998F
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteMissionObjectIdToPacket(this.MissionObjectId);
			GameNetworkMessage.WriteBoolToPacket(this.ChannelNo == 1);
			GameNetworkMessage.WriteFloatToPacket(this.Parameter, CompressionBasic.AnimationProgressCompressionInfo);
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0000B7BA File Offset: 0x000099BA
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.MissionObjectsDetailed;
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0000B7C4 File Offset: 0x000099C4
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Set animation parameter: ",
				this.Parameter,
				" on channel: ",
				this.ChannelNo,
				" of MissionObject with ID: ",
				this.MissionObjectId
			});
		}
	}
}
