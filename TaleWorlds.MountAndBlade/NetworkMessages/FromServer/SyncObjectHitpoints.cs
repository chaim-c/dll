using System;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000C9 RID: 201
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SyncObjectHitpoints : GameNetworkMessage
	{
		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000842 RID: 2114 RVA: 0x0000DFE5 File Offset: 0x0000C1E5
		// (set) Token: 0x06000843 RID: 2115 RVA: 0x0000DFED File Offset: 0x0000C1ED
		public MissionObjectId MissionObjectId { get; private set; }

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000844 RID: 2116 RVA: 0x0000DFF6 File Offset: 0x0000C1F6
		// (set) Token: 0x06000845 RID: 2117 RVA: 0x0000DFFE File Offset: 0x0000C1FE
		public float Hitpoints { get; private set; }

		// Token: 0x06000846 RID: 2118 RVA: 0x0000E007 File Offset: 0x0000C207
		public SyncObjectHitpoints(MissionObjectId missionObjectId, float hitpoints)
		{
			this.MissionObjectId = missionObjectId;
			this.Hitpoints = hitpoints;
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0000E01D File Offset: 0x0000C21D
		public SyncObjectHitpoints()
		{
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0000E028 File Offset: 0x0000C228
		protected override bool OnRead()
		{
			bool result = true;
			this.MissionObjectId = GameNetworkMessage.ReadMissionObjectIdFromPacket(ref result);
			this.Hitpoints = GameNetworkMessage.ReadFloatFromPacket(CompressionMission.UsableGameObjectHealthCompressionInfo, ref result);
			return result;
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0000E057 File Offset: 0x0000C257
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteMissionObjectIdToPacket(this.MissionObjectId);
			GameNetworkMessage.WriteFloatToPacket(MathF.Max(this.Hitpoints, 0f), CompressionMission.UsableGameObjectHealthCompressionInfo);
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0000E07E File Offset: 0x0000C27E
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.MissionObjectsDetailed | MultiplayerMessageFilter.SiegeWeaponsDetailed;
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0000E086 File Offset: 0x0000C286
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Synchronize HitPoints: ",
				this.Hitpoints,
				" of MissionObject with Id: ",
				this.MissionObjectId
			});
		}
	}
}
