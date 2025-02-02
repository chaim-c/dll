using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000B6 RID: 182
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SetSiegeTowerHasArrivedAtTarget : GameNetworkMessage
	{
		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000750 RID: 1872 RVA: 0x0000C939 File Offset: 0x0000AB39
		// (set) Token: 0x06000751 RID: 1873 RVA: 0x0000C941 File Offset: 0x0000AB41
		public MissionObjectId SiegeTowerId { get; private set; }

		// Token: 0x06000752 RID: 1874 RVA: 0x0000C94A File Offset: 0x0000AB4A
		public SetSiegeTowerHasArrivedAtTarget(MissionObjectId siegeTowerId)
		{
			this.SiegeTowerId = siegeTowerId;
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x0000C959 File Offset: 0x0000AB59
		public SetSiegeTowerHasArrivedAtTarget()
		{
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x0000C964 File Offset: 0x0000AB64
		protected override bool OnRead()
		{
			bool result = true;
			this.SiegeTowerId = GameNetworkMessage.ReadMissionObjectIdFromPacket(ref result);
			return result;
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0000C981 File Offset: 0x0000AB81
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteMissionObjectIdToPacket(this.SiegeTowerId);
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0000C98E File Offset: 0x0000AB8E
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.SiegeWeapons;
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0000C996 File Offset: 0x0000AB96
		protected override string OnGetLogFormat()
		{
			return "SiegeTower with ID: " + this.SiegeTowerId + " has arrived at its target.";
		}
	}
}
