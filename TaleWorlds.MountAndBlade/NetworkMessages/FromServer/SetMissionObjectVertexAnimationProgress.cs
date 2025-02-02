using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000AD RID: 173
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SetMissionObjectVertexAnimationProgress : GameNetworkMessage
	{
		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x0000C1F7 File Offset: 0x0000A3F7
		// (set) Token: 0x060006F9 RID: 1785 RVA: 0x0000C1FF File Offset: 0x0000A3FF
		public MissionObjectId MissionObjectId { get; private set; }

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x0000C208 File Offset: 0x0000A408
		// (set) Token: 0x060006FB RID: 1787 RVA: 0x0000C210 File Offset: 0x0000A410
		public float Progress { get; private set; }

		// Token: 0x060006FC RID: 1788 RVA: 0x0000C219 File Offset: 0x0000A419
		public SetMissionObjectVertexAnimationProgress(MissionObjectId missionObjectId, float progress)
		{
			this.MissionObjectId = missionObjectId;
			this.Progress = progress;
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0000C22F File Offset: 0x0000A42F
		public SetMissionObjectVertexAnimationProgress()
		{
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0000C238 File Offset: 0x0000A438
		protected override bool OnRead()
		{
			bool result = true;
			this.MissionObjectId = GameNetworkMessage.ReadMissionObjectIdFromPacket(ref result);
			this.Progress = GameNetworkMessage.ReadFloatFromPacket(CompressionBasic.AnimationProgressCompressionInfo, ref result);
			return result;
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0000C267 File Offset: 0x0000A467
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteMissionObjectIdToPacket(this.MissionObjectId);
			GameNetworkMessage.WriteFloatToPacket(this.Progress, CompressionBasic.AnimationProgressCompressionInfo);
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x0000C284 File Offset: 0x0000A484
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.MissionObjectsDetailed;
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x0000C28C File Offset: 0x0000A48C
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Set progress of Vertex Animation on MissionObject with ID: ",
				this.MissionObjectId,
				" to: ",
				this.Progress
			});
		}
	}
}
