using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000B5 RID: 181
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SetSiegeTowerGateState : GameNetworkMessage
	{
		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000746 RID: 1862 RVA: 0x0000C869 File Offset: 0x0000AA69
		// (set) Token: 0x06000747 RID: 1863 RVA: 0x0000C871 File Offset: 0x0000AA71
		public MissionObjectId SiegeTowerId { get; private set; }

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000748 RID: 1864 RVA: 0x0000C87A File Offset: 0x0000AA7A
		// (set) Token: 0x06000749 RID: 1865 RVA: 0x0000C882 File Offset: 0x0000AA82
		public SiegeTower.GateState State { get; private set; }

		// Token: 0x0600074A RID: 1866 RVA: 0x0000C88B File Offset: 0x0000AA8B
		public SetSiegeTowerGateState(MissionObjectId siegeTowerId, SiegeTower.GateState state)
		{
			this.SiegeTowerId = siegeTowerId;
			this.State = state;
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0000C8A1 File Offset: 0x0000AAA1
		public SetSiegeTowerGateState()
		{
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0000C8AC File Offset: 0x0000AAAC
		protected override bool OnRead()
		{
			bool result = true;
			this.SiegeTowerId = GameNetworkMessage.ReadMissionObjectIdFromPacket(ref result);
			this.State = (SiegeTower.GateState)GameNetworkMessage.ReadIntFromPacket(CompressionMission.SiegeTowerGateStateCompressionInfo, ref result);
			return result;
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x0000C8DB File Offset: 0x0000AADB
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteMissionObjectIdToPacket(this.SiegeTowerId);
			GameNetworkMessage.WriteIntToPacket((int)this.State, CompressionMission.SiegeTowerGateStateCompressionInfo);
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0000C8F8 File Offset: 0x0000AAF8
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.SiegeWeaponsDetailed;
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0000C900 File Offset: 0x0000AB00
		protected override string OnGetLogFormat()
		{
			return string.Concat(new object[]
			{
				"Set SiegeTower State to: ",
				this.State,
				" on SiegeTower with ID: ",
				this.SiegeTowerId
			});
		}
	}
}
