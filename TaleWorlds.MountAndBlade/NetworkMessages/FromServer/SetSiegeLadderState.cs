using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000B3 RID: 179
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SetSiegeLadderState : GameNetworkMessage
	{
		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000732 RID: 1842 RVA: 0x0000C6C9 File Offset: 0x0000A8C9
		// (set) Token: 0x06000733 RID: 1843 RVA: 0x0000C6D1 File Offset: 0x0000A8D1
		public MissionObjectId SiegeLadderId { get; private set; }

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x0000C6DA File Offset: 0x0000A8DA
		// (set) Token: 0x06000735 RID: 1845 RVA: 0x0000C6E2 File Offset: 0x0000A8E2
		public SiegeLadder.LadderState State { get; private set; }

		// Token: 0x06000736 RID: 1846 RVA: 0x0000C6EB File Offset: 0x0000A8EB
		public SetSiegeLadderState(MissionObjectId siegeLadderId, SiegeLadder.LadderState state)
		{
			this.SiegeLadderId = siegeLadderId;
			this.State = state;
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0000C701 File Offset: 0x0000A901
		public SetSiegeLadderState()
		{
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0000C70C File Offset: 0x0000A90C
		protected override bool OnRead()
		{
			bool result = true;
			this.SiegeLadderId = GameNetworkMessage.ReadMissionObjectIdFromPacket(ref result);
			this.State = (SiegeLadder.LadderState)GameNetworkMessage.ReadIntFromPacket(CompressionMission.SiegeLadderStateCompressionInfo, ref result);
			return result;
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x0000C73B File Offset: 0x0000A93B
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteMissionObjectIdToPacket(this.SiegeLadderId);
			GameNetworkMessage.WriteIntToPacket((int)this.State, CompressionMission.SiegeLadderStateCompressionInfo);
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x0000C758 File Offset: 0x0000A958
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.SiegeWeaponsDetailed;
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x0000C760 File Offset: 0x0000A960
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Set SiegeLadder State to: ",
				this.State,
				" on SiegeLadderState with ID: ",
				this.SiegeLadderId
			});
		}
	}
}
