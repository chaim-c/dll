using System;
using TaleWorlds.Engine;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x02000096 RID: 150
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class RemoveMissionObjectBodyFlags : GameNetworkMessage
	{
		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060005ED RID: 1517 RVA: 0x0000A921 File Offset: 0x00008B21
		// (set) Token: 0x060005EE RID: 1518 RVA: 0x0000A929 File Offset: 0x00008B29
		public MissionObjectId MissionObjectId { get; private set; }

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x0000A932 File Offset: 0x00008B32
		// (set) Token: 0x060005F0 RID: 1520 RVA: 0x0000A93A File Offset: 0x00008B3A
		public BodyFlags BodyFlags { get; private set; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x0000A943 File Offset: 0x00008B43
		// (set) Token: 0x060005F2 RID: 1522 RVA: 0x0000A94B File Offset: 0x00008B4B
		public bool ApplyToChildren { get; private set; }

		// Token: 0x060005F3 RID: 1523 RVA: 0x0000A954 File Offset: 0x00008B54
		public RemoveMissionObjectBodyFlags(MissionObjectId missionObjectId, BodyFlags bodyFlags, bool applyToChildren)
		{
			this.MissionObjectId = missionObjectId;
			this.BodyFlags = bodyFlags;
			this.ApplyToChildren = applyToChildren;
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0000A971 File Offset: 0x00008B71
		public RemoveMissionObjectBodyFlags()
		{
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0000A97C File Offset: 0x00008B7C
		protected override bool OnRead()
		{
			bool result = true;
			this.MissionObjectId = GameNetworkMessage.ReadMissionObjectIdFromPacket(ref result);
			this.BodyFlags = (BodyFlags)GameNetworkMessage.ReadIntFromPacket(CompressionBasic.FlagsCompressionInfo, ref result);
			this.ApplyToChildren = GameNetworkMessage.ReadBoolFromPacket(ref result);
			return result;
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0000A9B8 File Offset: 0x00008BB8
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteMissionObjectIdToPacket(this.MissionObjectId);
			GameNetworkMessage.WriteIntToPacket((int)this.BodyFlags, CompressionBasic.FlagsCompressionInfo);
			GameNetworkMessage.WriteBoolToPacket(this.ApplyToChildren);
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x0000A9E0 File Offset: 0x00008BE0
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.MissionObjectsDetailed;
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0000A9E8 File Offset: 0x00008BE8
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Remove bodyflags: ",
				this.BodyFlags,
				" from MissionObject with ID: ",
				this.MissionObjectId,
				this.ApplyToChildren ? "" : " and from all of its children."
			});
		}
	}
}
