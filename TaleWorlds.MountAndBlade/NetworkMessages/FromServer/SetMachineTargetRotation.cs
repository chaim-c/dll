using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromServer
{
	// Token: 0x020000A1 RID: 161
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromServer)]
	public sealed class SetMachineTargetRotation : GameNetworkMessage
	{
		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600066E RID: 1646 RVA: 0x0000B464 File Offset: 0x00009664
		// (set) Token: 0x0600066F RID: 1647 RVA: 0x0000B46C File Offset: 0x0000966C
		public MissionObjectId UsableMachineId { get; private set; }

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x0000B475 File Offset: 0x00009675
		// (set) Token: 0x06000671 RID: 1649 RVA: 0x0000B47D File Offset: 0x0000967D
		public float HorizontalRotation { get; private set; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000672 RID: 1650 RVA: 0x0000B486 File Offset: 0x00009686
		// (set) Token: 0x06000673 RID: 1651 RVA: 0x0000B48E File Offset: 0x0000968E
		public float VerticalRotation { get; private set; }

		// Token: 0x06000674 RID: 1652 RVA: 0x0000B497 File Offset: 0x00009697
		public SetMachineTargetRotation(MissionObjectId usableMachineId, float horizontalRotaiton, float verticalRotation)
		{
			this.UsableMachineId = usableMachineId;
			this.HorizontalRotation = horizontalRotaiton;
			this.VerticalRotation = verticalRotation;
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0000B4B4 File Offset: 0x000096B4
		public SetMachineTargetRotation()
		{
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0000B4BC File Offset: 0x000096BC
		protected override bool OnRead()
		{
			bool result = true;
			this.UsableMachineId = GameNetworkMessage.ReadMissionObjectIdFromPacket(ref result);
			this.HorizontalRotation = GameNetworkMessage.ReadFloatFromPacket(CompressionBasic.HighResRadianCompressionInfo, ref result);
			this.VerticalRotation = GameNetworkMessage.ReadFloatFromPacket(CompressionBasic.HighResRadianCompressionInfo, ref result);
			return result;
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x0000B4FD File Offset: 0x000096FD
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteMissionObjectIdToPacket(this.UsableMachineId);
			GameNetworkMessage.WriteFloatToPacket(this.HorizontalRotation, CompressionBasic.HighResRadianCompressionInfo);
			GameNetworkMessage.WriteFloatToPacket(this.VerticalRotation, CompressionBasic.HighResRadianCompressionInfo);
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0000B52A File Offset: 0x0000972A
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.SiegeWeaponsDetailed;
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0000B532 File Offset: 0x00009732
		protected override string OnGetLogFormat()
		{
			return "Set target rotation of UsableMachine with ID: " + this.UsableMachineId;
		}
	}
}
