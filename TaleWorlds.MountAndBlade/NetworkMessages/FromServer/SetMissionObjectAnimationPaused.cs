using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000A4 RID: 164
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SetMissionObjectAnimationPaused : GameNetworkMessage
	{
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x0000B81E File Offset: 0x00009A1E
		// (set) Token: 0x06000695 RID: 1685 RVA: 0x0000B826 File Offset: 0x00009A26
		public MissionObjectId MissionObjectId { get; private set; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000696 RID: 1686 RVA: 0x0000B82F File Offset: 0x00009A2F
		// (set) Token: 0x06000697 RID: 1687 RVA: 0x0000B837 File Offset: 0x00009A37
		public bool IsPaused { get; private set; }

		// Token: 0x06000698 RID: 1688 RVA: 0x0000B840 File Offset: 0x00009A40
		public SetMissionObjectAnimationPaused(MissionObjectId missionObjectId, bool isPaused)
		{
			this.MissionObjectId = missionObjectId;
			this.IsPaused = isPaused;
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0000B856 File Offset: 0x00009A56
		public SetMissionObjectAnimationPaused()
		{
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0000B860 File Offset: 0x00009A60
		protected override bool OnRead()
		{
			bool result = true;
			this.MissionObjectId = GameNetworkMessage.ReadMissionObjectIdFromPacket(ref result);
			this.IsPaused = GameNetworkMessage.ReadBoolFromPacket(ref result);
			return result;
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0000B88A File Offset: 0x00009A8A
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteMissionObjectIdToPacket(this.MissionObjectId);
			GameNetworkMessage.WriteBoolToPacket(this.IsPaused);
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0000B8A2 File Offset: 0x00009AA2
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.MissionObjectsDetailed;
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0000B8AC File Offset: 0x00009AAC
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Set animation to be: ",
				this.IsPaused ? "Paused" : "Not paused",
				" on MissionObject with ID: ",
				this.MissionObjectId
			});
		}
	}
}
