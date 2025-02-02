using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000BA RID: 186
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SetUsableMissionObjectIsDisabledForPlayers : GameNetworkMessage
	{
		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000776 RID: 1910 RVA: 0x0000CC01 File Offset: 0x0000AE01
		// (set) Token: 0x06000777 RID: 1911 RVA: 0x0000CC09 File Offset: 0x0000AE09
		public MissionObjectId UsableGameObjectId { get; private set; }

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000778 RID: 1912 RVA: 0x0000CC12 File Offset: 0x0000AE12
		// (set) Token: 0x06000779 RID: 1913 RVA: 0x0000CC1A File Offset: 0x0000AE1A
		public bool IsDisabledForPlayers { get; private set; }

		// Token: 0x0600077A RID: 1914 RVA: 0x0000CC23 File Offset: 0x0000AE23
		public SetUsableMissionObjectIsDisabledForPlayers(MissionObjectId usableGameObjectId, bool isDisabledForPlayers)
		{
			this.UsableGameObjectId = usableGameObjectId;
			this.IsDisabledForPlayers = isDisabledForPlayers;
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x0000CC39 File Offset: 0x0000AE39
		public SetUsableMissionObjectIsDisabledForPlayers()
		{
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0000CC44 File Offset: 0x0000AE44
		protected override bool OnRead()
		{
			bool result = true;
			this.UsableGameObjectId = GameNetworkMessage.ReadMissionObjectIdFromPacket(ref result);
			this.IsDisabledForPlayers = GameNetworkMessage.ReadBoolFromPacket(ref result);
			return result;
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0000CC6E File Offset: 0x0000AE6E
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteMissionObjectIdToPacket(this.UsableGameObjectId);
			GameNetworkMessage.WriteBoolToPacket(this.IsDisabledForPlayers);
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x0000CC86 File Offset: 0x0000AE86
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.MissionObjects;
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x0000CC90 File Offset: 0x0000AE90
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Set IsDisabled for player: ",
				this.IsDisabledForPlayers ? "True" : "False",
				" on UsableMissionObject with ID: ",
				this.UsableGameObjectId
			});
		}
	}
}
