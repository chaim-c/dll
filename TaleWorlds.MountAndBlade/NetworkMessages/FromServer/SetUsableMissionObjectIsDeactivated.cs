using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000B9 RID: 185
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SetUsableMissionObjectIsDeactivated : GameNetworkMessage
	{
		// Token: 0x1700019E RID: 414
		// (get) Token: 0x0600076C RID: 1900 RVA: 0x0000CB25 File Offset: 0x0000AD25
		// (set) Token: 0x0600076D RID: 1901 RVA: 0x0000CB2D File Offset: 0x0000AD2D
		public MissionObjectId UsableGameObjectId { get; private set; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x0600076E RID: 1902 RVA: 0x0000CB36 File Offset: 0x0000AD36
		// (set) Token: 0x0600076F RID: 1903 RVA: 0x0000CB3E File Offset: 0x0000AD3E
		public bool IsDeactivated { get; private set; }

		// Token: 0x06000770 RID: 1904 RVA: 0x0000CB47 File Offset: 0x0000AD47
		public SetUsableMissionObjectIsDeactivated(MissionObjectId usableGameObjectId, bool isDeactivated)
		{
			this.UsableGameObjectId = usableGameObjectId;
			this.IsDeactivated = isDeactivated;
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x0000CB5D File Offset: 0x0000AD5D
		public SetUsableMissionObjectIsDeactivated()
		{
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x0000CB68 File Offset: 0x0000AD68
		protected override bool OnRead()
		{
			bool result = true;
			this.UsableGameObjectId = GameNetworkMessage.ReadMissionObjectIdFromPacket(ref result);
			this.IsDeactivated = GameNetworkMessage.ReadBoolFromPacket(ref result);
			return result;
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x0000CB92 File Offset: 0x0000AD92
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteMissionObjectIdToPacket(this.UsableGameObjectId);
			GameNetworkMessage.WriteBoolToPacket(this.IsDeactivated);
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x0000CBAA File Offset: 0x0000ADAA
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.MissionObjects;
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0000CBB4 File Offset: 0x0000ADB4
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Set IsDeactivated: ",
				this.IsDeactivated ? "True" : "False",
				" on UsableMissionObject with ID: ",
				this.UsableGameObjectId
			});
		}
	}
}
