using System;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Network.Messages;

namespace NetworkMessages.FromClient
{
	// Token: 0x02000036 RID: 54
	[DefineGameNetworkMessageType(GameNetworkMessageSendType.FromClient)]
	public sealed class SetMachineRotation : GameNetworkMessage
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00003E9D File Offset: 0x0000209D
		// (set) Token: 0x060001AB RID: 427 RVA: 0x00003EA5 File Offset: 0x000020A5
		public MissionObjectId UsableMachineId { get; private set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00003EAE File Offset: 0x000020AE
		// (set) Token: 0x060001AD RID: 429 RVA: 0x00003EB6 File Offset: 0x000020B6
		public float HorizontalRotation { get; private set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00003EBF File Offset: 0x000020BF
		// (set) Token: 0x060001AF RID: 431 RVA: 0x00003EC7 File Offset: 0x000020C7
		public float VerticalRotation { get; private set; }

		// Token: 0x060001B0 RID: 432 RVA: 0x00003ED0 File Offset: 0x000020D0
		public SetMachineRotation(MissionObjectId missionObjectId, float horizontalRotation, float verticalRotation)
		{
			this.UsableMachineId = missionObjectId;
			this.HorizontalRotation = horizontalRotation;
			this.VerticalRotation = verticalRotation;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00003EED File Offset: 0x000020ED
		public SetMachineRotation()
		{
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00003EF8 File Offset: 0x000020F8
		protected override bool OnRead()
		{
			bool result = true;
			this.UsableMachineId = GameNetworkMessage.ReadMissionObjectIdFromPacket(ref result);
			this.HorizontalRotation = GameNetworkMessage.ReadFloatFromPacket(CompressionBasic.HighResRadianCompressionInfo, ref result);
			this.VerticalRotation = GameNetworkMessage.ReadFloatFromPacket(CompressionBasic.HighResRadianCompressionInfo, ref result);
			return result;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00003F39 File Offset: 0x00002139
		protected override void OnWrite()
		{
			GameNetworkMessage.WriteMissionObjectIdToPacket(this.UsableMachineId);
			GameNetworkMessage.WriteFloatToPacket(this.HorizontalRotation, CompressionBasic.HighResRadianCompressionInfo);
			GameNetworkMessage.WriteFloatToPacket(this.VerticalRotation, CompressionBasic.HighResRadianCompressionInfo);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00003F66 File Offset: 0x00002166
		protected override MultiplayerMessageFilter OnGetLogFilter()
		{
			return MultiplayerMessageFilter.SiegeWeaponsDetailed;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00003F6E File Offset: 0x0000216E
		protected override string OnGetLogFormat()
		{
			return "Set rotation of UsableMachine with ID: " + this.UsableMachineId;
		}
	}
}
