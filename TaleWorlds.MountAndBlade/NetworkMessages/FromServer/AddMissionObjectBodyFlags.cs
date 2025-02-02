using System;
using TaleWorlds.Engine;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x0200006F RID: 111
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class AddMissionObjectBodyFlags : GameNetworkMessage
	{
		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x0000755C File Offset: 0x0000575C
		// (set) Token: 0x060003E6 RID: 998 RVA: 0x00007564 File Offset: 0x00005764
		public MissionObjectId MissionObjectId { get; private set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x0000756D File Offset: 0x0000576D
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x00007575 File Offset: 0x00005775
		public BodyFlags BodyFlags { get; private set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0000757E File Offset: 0x0000577E
		// (set) Token: 0x060003EA RID: 1002 RVA: 0x00007586 File Offset: 0x00005786
		public bool ApplyToChildren { get; private set; }

		// Token: 0x060003EB RID: 1003 RVA: 0x0000758F File Offset: 0x0000578F
		public AddMissionObjectBodyFlags(MissionObjectId missionObjectId, BodyFlags bodyFlags, bool applyToChildren)
		{
			this.MissionObjectId = missionObjectId;
			this.BodyFlags = bodyFlags;
			this.ApplyToChildren = applyToChildren;
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x000075AC File Offset: 0x000057AC
		public AddMissionObjectBodyFlags()
		{
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x000075B4 File Offset: 0x000057B4
		protected override bool OnRead()
		{
			bool result = true;
			this.MissionObjectId = GameNetworkMessage.ReadMissionObjectIdFromPacket(ref result);
			this.BodyFlags = (BodyFlags)GameNetworkMessage.ReadIntFromPacket(CompressionBasic.FlagsCompressionInfo, ref result);
			this.ApplyToChildren = GameNetworkMessage.ReadBoolFromPacket(ref result);
			return result;
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x000075F0 File Offset: 0x000057F0
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteMissionObjectIdToPacket(this.MissionObjectId);
			GameNetworkMessage.WriteIntToPacket((int)this.BodyFlags, CompressionBasic.FlagsCompressionInfo);
			GameNetworkMessage.WriteBoolToPacket(this.ApplyToChildren);
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00007618 File Offset: 0x00005818
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.MissionObjectsDetailed;
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00007620 File Offset: 0x00005820
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Add bodyflags: ",
				this.BodyFlags,
				" to MissionObject with ID: ",
				this.MissionObjectId,
				this.ApplyToChildren ? "" : " and to all of its children."
			});
		}
	}
}
