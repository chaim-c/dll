using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000AE RID: 174
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SetMissionObjectVisibility : GameNetworkMessage
	{
		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000702 RID: 1794 RVA: 0x0000C2C5 File Offset: 0x0000A4C5
		// (set) Token: 0x06000703 RID: 1795 RVA: 0x0000C2CD File Offset: 0x0000A4CD
		public MissionObjectId MissionObjectId { get; private set; }

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000704 RID: 1796 RVA: 0x0000C2D6 File Offset: 0x0000A4D6
		// (set) Token: 0x06000705 RID: 1797 RVA: 0x0000C2DE File Offset: 0x0000A4DE
		public bool Visible { get; private set; }

		// Token: 0x06000706 RID: 1798 RVA: 0x0000C2E7 File Offset: 0x0000A4E7
		public SetMissionObjectVisibility(MissionObjectId missionObjectId, bool visible)
		{
			this.MissionObjectId = missionObjectId;
			this.Visible = visible;
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0000C2FD File Offset: 0x0000A4FD
		public SetMissionObjectVisibility()
		{
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0000C308 File Offset: 0x0000A508
		protected override bool OnRead()
		{
			bool result = true;
			this.MissionObjectId = GameNetworkMessage.ReadMissionObjectIdFromPacket(ref result);
			this.Visible = GameNetworkMessage.ReadBoolFromPacket(ref result);
			return result;
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0000C332 File Offset: 0x0000A532
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteMissionObjectIdToPacket(this.MissionObjectId);
			GameNetworkMessage.WriteBoolToPacket(this.Visible);
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0000C34A File Offset: 0x0000A54A
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.MissionObjects;
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0000C354 File Offset: 0x0000A554
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Set Visibility of MissionObject with ID: ",
				this.MissionObjectId,
				" to: ",
				this.Visible ? "True" : "False"
			});
		}
	}
}
