using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000B4 RID: 180
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SetSiegeMachineMovementDistance : GameNetworkMessage
	{
		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600073C RID: 1852 RVA: 0x0000C799 File Offset: 0x0000A999
		// (set) Token: 0x0600073D RID: 1853 RVA: 0x0000C7A1 File Offset: 0x0000A9A1
		public MissionObjectId UsableMachineId { get; private set; }

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x0600073E RID: 1854 RVA: 0x0000C7AA File Offset: 0x0000A9AA
		// (set) Token: 0x0600073F RID: 1855 RVA: 0x0000C7B2 File Offset: 0x0000A9B2
		public float Distance { get; private set; }

		// Token: 0x06000740 RID: 1856 RVA: 0x0000C7BB File Offset: 0x0000A9BB
		public SetSiegeMachineMovementDistance(MissionObjectId usableMachineId, float distance)
		{
			this.UsableMachineId = usableMachineId;
			this.Distance = distance;
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x0000C7D1 File Offset: 0x0000A9D1
		public SetSiegeMachineMovementDistance()
		{
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0000C7DC File Offset: 0x0000A9DC
		protected override bool OnRead()
		{
			bool result = true;
			this.UsableMachineId = GameNetworkMessage.ReadMissionObjectIdFromPacket(ref result);
			this.Distance = GameNetworkMessage.ReadFloatFromPacket(CompressionBasic.PositionCompressionInfo, ref result);
			return result;
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0000C80B File Offset: 0x0000AA0B
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteMissionObjectIdToPacket(this.UsableMachineId);
			GameNetworkMessage.WriteFloatToPacket(this.Distance, CompressionBasic.PositionCompressionInfo);
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0000C828 File Offset: 0x0000AA28
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.SiegeWeaponsDetailed;
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0000C830 File Offset: 0x0000AA30
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Set Movement Distance: ",
				this.Distance,
				" of SiegeMachine with ID: ",
				this.UsableMachineId
			});
		}
	}
}
